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
    private PieceType _pieceType;
    private PlayerColor _color;

    public PlayerColor GetColor() => _color;
    public PieceType GetPieceType() => _pieceType;
}