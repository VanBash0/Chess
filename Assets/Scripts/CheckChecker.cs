public static class CheckChecker
{
    public static bool CheckCheck(BoardState boardState, PlayerColor targetColor)
    {
        var kingPosition = boardState.GetKingPosition(targetColor);
        return IsKingAttackedFromDirections(boardState, targetColor, orthogonalDirections, kingPosition, PieceType.Rook, PieceType.Queen) ||
               IsKingAttackedFromDirections(boardState, targetColor, diagonalDirections, kingPosition, PieceType.Bishop, PieceType.Queen) ||
               IsKingAttackedByKnight(boardState, targetColor, kingPosition) || IsKingAttackedByPawn(boardState, targetColor, kingPosition);
    }

    private static (int, int)[] orthogonalDirections = new (int, int)[]
    {
        (1, 0), (-1, 0), (0, 1), (0, -1)
    };

    private static (int, int)[] diagonalDirections = new (int, int)[]
    {
        (1, 1), (1, -1), (-1, 1), (-1, -1)
    };

    private static (int, int)[] knightMoves = new (int, int)[]
    {
        (2, 1), (2, -1), (-2, 1), (-2, -1),
        (1, 2), (1, -2), (-1, 2), (-1, -2)
    };

    private static bool IsKingAttackedFromDirections(BoardState boardState, PlayerColor targetColor, (int, int)[] directions, 
                                                     (int, int) kingPosition, PieceType piece1, PieceType piece2)
    {
        foreach (var (dx, dy) in directions)
        {
            var (x, y) = kingPosition;
            while (boardState.IsSquareOnBoard(x + dx, y + dy))
            {
                x += dx;
                y += dy;
                var piece = boardState.GetPiece(x, y);
                if (piece != null)
                {
                    if (piece.Color != targetColor && (piece.Type == piece1 || piece.Type == piece2))
                    {
                        return true;
                    }
                    break;
                }
            }
        }
        return false;
    }

    private static bool IsKingAttackedByKnight(BoardState boardState, PlayerColor targetColor, (int, int) kingPosition)
    {
        foreach (var (dx, dy) in knightMoves)
        {
            var (x, y) = kingPosition;
            x += dx;
            y += dy;
            if (boardState.IsSquareOnBoard(x, y))
            {
                var piece = boardState.GetPiece(x, y);
                if (piece != null && piece.Color != targetColor && piece.Type == PieceType.Knight)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static bool IsKingAttackedByPawn(BoardState boardState, PlayerColor targetColor, (int, int) kingPosition)
    {
        int direction = (targetColor == PlayerColor.White) ? 1 : -1;
        var (x, y) = kingPosition;
        int[] dx = { 1, -1 };
        foreach (var offsetX in dx)
        {
            int targetX = x + offsetX;
            int targetY = y + direction;
            if (boardState.IsSquareOnBoard(targetX, targetY))
            {
                var piece = boardState.GetPiece(targetX, targetY);
                if (piece != null && piece.Color != targetColor && piece.Type == PieceType.Pawn)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
