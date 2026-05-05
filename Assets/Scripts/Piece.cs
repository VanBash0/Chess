public enum PieceType
{
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King
}

public class Piece
{
    public PieceType Type { get; }
    public PlayerColor Color { get; }

    public Piece(PieceType pieceType, PlayerColor color)
    {
        Type = pieceType;
        Color = color;
    }
}