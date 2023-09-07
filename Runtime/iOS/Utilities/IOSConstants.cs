namespace Chartboost.Core.iOS.Utilities
{
    internal delegate void ChartboostCoreOnResultBoolean(int hashCode, bool completion);
    internal delegate void ChartboostCoreOnResultString(int hashCode, string result);

    public static class IOSConstants
    {
        public const string DLLImport = "__Internal";
    }
}
