public struct Move
{
    public (int, int) From;
    public (int, int) To;
    public Piece ActivePiece;
    public Piece CapturedPiece;
    public bool IsEnPassant;
    public bool IsCastling;

    public Move((int, int) start, (int, int) end, Piece active, Piece captured = null)
    {
        From = start;
        To = end;
        ActivePiece = active;
        CapturedPiece = captured;
        IsEnPassant = false;
        IsCastling = false;
    }
}