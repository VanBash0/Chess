public static class SquareTranslator
{
    private static readonly string letters = "abcdefgh";

    public static string GetNotation((int, int) square)
    {
        return letters[square.Item1] + (square.Item2 + 1).ToString();
    }
}
