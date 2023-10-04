namespace GenerateTicket.Common;
public static class StringGenerator
{
    private static readonly string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private static readonly Random _random = new();
    public static string Generate(int len = 10)
    {
        var randStr = Enumerable.Repeat(_chars, len).Select(s => s[_random.Next(s.Length)]).ToArray();
        return new string(randStr);
    }

    public static string Generate2(int i = 3, int j = 5)
    {

        var len = _random.Next(i, j);
        var randStr = Enumerable.Repeat(_chars, len).Select(s => s[_random.Next(s.Length)]).ToArray();
        return new string(randStr);
    }
}
