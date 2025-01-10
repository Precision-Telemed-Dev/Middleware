
namespace Precision.API
{
    public static class ExtensionMethods
    {
        public static string RemoveUselessChars(this string s)
        => s.Replace("\r", "").Replace("\n", "");
    }
}
