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

    public Piece(PieceType pieceType, PlayerColor color)
    {
        _pieceType = pieceType;
        _color = color;
    }
}