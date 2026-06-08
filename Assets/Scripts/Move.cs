public struct Move
{
    public (int, int) From;
    public (int, int) To;
    public Piece ActivePiece;
    public Piece CapturedPiece;
    public bool IsEnPassant;
    public bool IsCastling;
    public (int, int)? NewEnPassantTarget;

    public Move((int, int) start, (int, int) end, Piece active, Piece captured = null, bool isEnPassant = false, bool isCastling = false, (int, int)? newEnPassantTarget = null)
    {
        From = start;
        To = end;
        ActivePiece = active;
        CapturedPiece = captured;
        IsEnPassant = isEnPassant;
        IsCastling = isCastling;
        NewEnPassantTarget = newEnPassantTarget;
    }
}