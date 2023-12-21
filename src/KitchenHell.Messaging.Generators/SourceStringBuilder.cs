using System.Text;

namespace KitchenHell.Messaging.Generators;

internal class SourceStringBuilder
{
    private int _tabLevel = 0;
    private readonly StringBuilder _stringBuilder;

    public SourceStringBuilder()
    {
        _stringBuilder = new StringBuilder();
    }

    public SourceStringBuilder(string value)
    {
        _stringBuilder = new StringBuilder(value);
    }

    public IDisposable StartNestedScope()
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

    private string GetPadded(string value)
    {
        return $"{GetCurrentTabulation()}{value}";
    }

    private string GetCurrentTabulation()
    {
        if (_tabLevel <= 0)
        {
            return string.Empty;
        }

        return new string(' ', _tabLevel * 4);
    }

    public SourceStringBuilder AppendFormat(string format, object arg0)
    {
        _stringBuilder.AppendFormat(GetPadded(format), arg0);

        return this;
    }

    public SourceStringBuilder AppendFormat(string format, object arg0, object arg1)
    {
        _stringBuilder.AppendFormat(GetPadded(format), arg0, arg1);

        return this;
    }

    public SourceStringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
    {
        _stringBuilder.AppendFormat(GetPadded(format), arg0, arg1, arg2);

        return this;
    }

    public SourceStringBuilder AppendFormatLine(string format, object arg0)
    {
        _stringBuilder.AppendFormat(GetPadded(format), arg0);
        _stringBuilder.AppendLine();

        return this;
    }

    public SourceStringBuilder AppendFormatLine(string format, object arg0, object arg1)
    {
        _stringBuilder.AppendFormat(GetPadded(format), arg0, arg1);
        _stringBuilder.AppendLine();

        return this;
    }

    public SourceStringBuilder AppendFormatLine(string format, object arg0, object arg1, object arg2)
    {
        _stringBuilder.AppendFormat(GetPadded(format), arg0, arg1, arg2);
        _stringBuilder.AppendLine();

        return this;
    }

    public SourceStringBuilder AppendLine(string format)
    {
        _stringBuilder.AppendLine(GetPadded(format));

        return this;
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
