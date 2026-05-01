public struct Move
{
    (int, int) StartCell;
    (int, int) EndCell;
    Piece ActivePiece;
    Piece CapturedPiece;
    bool IsEnPassant;
    bool IsCastling;

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