namespace Intervals.Utils;

internal static class BitHelper
{
    /// <summary>
    /// Convert least significant bit to sign int. From 0 to 1, from 1 to -1
    /// </summary>
    /// <param name="bit">least significant bit</param>
    /// <returns>sign int</returns>
    public static int ToSign(int bit) => -bit | 1;
}