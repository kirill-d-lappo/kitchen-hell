using System.Text;

namespace KitchenHell.Messaging.Kafka.Generators;

internal class SourceStringBuilder
{
  private readonly StringBuilder _stringBuilder;
  private int _tabLevel;

  public SourceStringBuilder()
  {
    _stringBuilder = new StringBuilder();
  }

  public SourceStringBuilder(string value)
  {
    _stringBuilder = new StringBuilder(value);
  }

  public IDisposable StartTabulationToRight()
  {
    TabRight();

    return new TabToLeftScope(this);
  }

  public SourceStringBuilder TabRight()
  {
    _tabLevel++;

    return this;
  }

  public SourceStringBuilder TabLeft()
  {
    if (_tabLevel > 0)
    {
      _tabLevel--;
    }

    return this;
  }

  private string GetTabbed(string value)
  {
    return $"{GetCurrentScopeTabulation()}{value}";
  }

  private string GetCurrentScopeTabulation()
  {
    if (_tabLevel <= 0)
    {
      return string.Empty;
    }

    return new string(' ', _tabLevel * 4);
  }

  public SourceStringBuilder AppendFormat(string format, object arg0)
  {
    PadWhenNeeded();

    _stringBuilder.AppendFormat(format, arg0);

    return this;
  }

  public SourceStringBuilder AppendFormat(string format, object arg0, object arg1)
  {
    PadWhenNeeded();

    _stringBuilder.AppendFormat(format, arg0, arg1);

    return this;
  }

  public SourceStringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
  {
    PadWhenNeeded();

    _stringBuilder.AppendFormat(format, arg0, arg1, arg2);

    return this;
  }

  public SourceStringBuilder AppendFormatLine(string format, object arg0)
  {
    PadWhenNeeded();

    _stringBuilder.AppendFormat(format, arg0);
    _stringBuilder.AppendLine();

    return this;
  }

  public SourceStringBuilder AppendFormatLine(string format, object arg0, object arg1)
  {
    PadWhenNeeded();

    _stringBuilder.AppendFormat(format, arg0, arg1);
    _stringBuilder.AppendLine();

    return this;
  }

  public SourceStringBuilder AppendFormatLine(string format, object arg0, object arg1, object arg2)
  {
    PadWhenNeeded();

    _stringBuilder.AppendFormat(format, arg0, arg1, arg2);
    _stringBuilder.AppendLine();

    return this;
  }

  public SourceStringBuilder AppendLine(string format)
  {
    PadWhenNeeded();

    _stringBuilder.AppendLine(format);

    return this;
  }

  private void PadWhenNeeded()
  {
    if (_stringBuilder.IsLastNewLine())
    {
      _stringBuilder.Append(GetCurrentScopeTabulation());
    }
  }

  public SourceStringBuilder AppendLine()
  {
    _stringBuilder.AppendLine();

    return this;
  }

  public override string ToString()
  {
    return _stringBuilder.ToString();
  }

  private class TabToLeftScope : IDisposable
  {
    private readonly SourceStringBuilder _sourceStringBuilder;

    public TabToLeftScope(SourceStringBuilder sourceStringBuilder)
    {
      _sourceStringBuilder = sourceStringBuilder;
    }

    public void Dispose()
    {
      _sourceStringBuilder.TabLeft();
    }
  }
}
