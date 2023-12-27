using System.Text;

namespace KitchenHell.Common;

public static class StringBuilderExtensions
{
  public static char? Last(this StringBuilder stringBuilder)
  {
    if (stringBuilder.Length <= 0)
    {
      return default;
    }

    return stringBuilder[stringBuilder.Length - 1];
  }

  public static bool IsLastNewLine(this StringBuilder stringBuilder)
  {
    return stringBuilder.Last() == '\n';
  }
}
