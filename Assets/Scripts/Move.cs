public struct Move
{
    public (int, int) StartCell;
    public (int, int) EndCell;
    public Piece ActivePiece;
    public Piece CapturedPiece;
    public bool IsEnPassant;
    public bool IsCastling;

    public Move((int, int) start, (int, int) end, Piece active, Piece captured = null)
    {
        StartCell = start;
        EndCell = end;
        ActivePiece = active;
        CapturedPiece = captured;
        IsEnPassant = false;
        IsCastling = false;
    }
}