using System;
using System.Collections.Generic;

public static class PossibleMoveGenerator
{
    private static Dictionary<PieceType, Func<int, int, Piece, BoardState, List<Move>>> possibleMovesByPiece = new Dictionary<PieceType, Func<int, int, Piece, BoardState, List<Move>>>()
    {
        { PieceType.Pawn, GetPawnMoves },
        { PieceType.Knight, GetKnightMoves },
        { PieceType.Bishop, GetBishopMoves },
        { PieceType.Rook, GetRookMoves },
        { PieceType.Queen, GetQueenMoves },
        { PieceType.King, GetKingMoves },
    };

    private static readonly (int, int)[] knightDirections = new (int, int)[]
    {
        (2, 1), (2, -1), (-2, 1), (-2, -1),
        (1, 2), (1, -2), (-1, 2), (-1, -2)
    };

    private static readonly (int, int)[] bishopDirections = new (int, int)[]
    {
        (1, 1), (1, -1), (-1, 1), (-1, -1)
    };

    private static readonly (int, int)[] rookDirections = new (int, int)[]
    {
        (1, 0), (-1, 0), (0, 1), (0, -1)
    };

    public static List<Move> GetPossibleMoves(int x, int y, Piece piece, BoardState boardState)
    {
        PieceType pieceType = piece.Type;

        if (possibleMovesByPiece.TryGetValue(pieceType, out var moveGenerator))
        {
            return moveGenerator(x, y, piece, boardState);

        }
        else
        {
            UnityEngine.Debug.LogError($"No move generation function found for piece type: {pieceType}");
            return new List<Move>();
        }
    }

    private static List<Move> GetPawnMoves(int x, int y, Piece pawn, BoardState state)
    {
        List<Move> moves = new List<Move>();
        var color = pawn.Color;
        int direction = (color == PlayerColor.White) ? 1 : -1;

        if (state.IsSquareOnBoard(x, y + direction) && state.IsSquareEmpty(x, y + direction))
        {
            moves.Add(new Move((x, y), (x, y + direction), pawn));

            bool isOnStartPos = (color == PlayerColor.White && y == 1) || (color == PlayerColor.Black && y == 6);
            if (isOnStartPos && state.IsSquareOnBoard(x, y + 2 * direction) && state.IsSquareEmpty(x, y + 2 * direction))
            {
                moves.Add(new Move((x, y), (x, y + 2 * direction), pawn));
            }
        }

        int[] dx = { 1, -1 };
        foreach (var offsetX in dx)
        {
            int targetX = x + offsetX;
            int targetY = y + direction;
            if (!state.IsSquareOnBoard(targetX, targetY)) continue;

            var targetPiece = state.GetPiece(targetX, targetY);

            if (targetPiece != null && targetPiece.Color != color)
                moves.Add(new Move((x, y), (targetX, targetY), pawn, targetPiece));

            else if (targetPiece == null && state.IsSquareOnBoard(targetX, y) && state.GetEnPassantTarget() == (targetX, targetY))
            {
                var sidePawn = state.GetPiece(targetX, y);
                if (sidePawn is not null && sidePawn.Color != color)
                    moves.Add(new Move((x, y), (targetX, targetY), pawn, sidePawn));
            }
        }

        return moves;
    }

    private static List<Move> GetKnightMoves(int x, int y, Piece knight, BoardState state)
    {
        var moves = new List<Move>();

        foreach (var direction in knightDirections)
        {
            int targetX = x + direction.Item1;
            int targetY = y + direction.Item2;
            
            if (!state.IsSquareOnBoard(targetX, targetY)) continue;

            var targetPiece = state.GetPiece(targetX, targetY);
            if (targetPiece == null || targetPiece.Color != knight.Color)
            {
                moves.Add(new Move((x, y), (targetX, targetY), knight, targetPiece));
            }
        }

        return moves;
    }

    private static List<Move> GetBishopMoves(int x, int y, Piece bishop, BoardState state)
    {
        return GetMovesInDirections(x, y, bishopDirections, bishop, state);
    }

    private static List<Move> GetRookMoves(int x, int y, Piece rook, BoardState state)
    {
        return GetMovesInDirections(x, y, rookDirections, rook, state);
    }

    private static List<Move> GetQueenMoves(int x, int y, Piece queen, BoardState state)
    {
        var diagonalMoves = GetMovesInDirections(x, y, bishopDirections, queen, state);
        var orthogonalMoves = GetMovesInDirections(x, y, rookDirections, queen, state);
        diagonalMoves.AddRange(orthogonalMoves);
        return diagonalMoves;
    }

    private static List<Move> GetKingMoves(int x, int y, Piece king, BoardState state)
    {
        int[] dx = { -1, 0, 1 };
        int[] dy = { -1, 0, 1 };
        var color = king.Color;
        var moves = new List<Move>();
        
        foreach (var offsetX in dx)
        {
            foreach (var offsetY in dy)
            {
                if (offsetX == 0 && offsetY == 0) continue;
                var targetX = x + offsetX;
                var targetY = y + offsetY;
                if (state.IsSquareOnBoard(targetX, targetY))
                {
                    var targetPiece = state.GetPiece(targetX, targetY);
                    if (targetPiece == null || targetPiece.Color != color)
                        moves.Add(new Move((x, y), (targetX, targetY), king, targetPiece));
                }
            }
        }

        return moves;
    }

    private static List<Move> GetMovesInDirections(int startX, int startY, (int, int)[] directions, Piece piece, BoardState state)
    {
        var moves = new List<Move>();
        foreach (var direction in directions)
        {
            var (x, y) = (startX, startY);
            var (dx, dy) = direction;
            while (state.IsSquareOnBoard(x + dx, y + dy))
            {
                x += dx;
                y += dy;
                var targetPiece = state.GetPiece(x, y);
                if (targetPiece == null)
                {
                    moves.Add(new Move((startX, startY), (x, y), piece));
                }
                else if (targetPiece.Color != piece.Color)
                {
                    moves.Add(new Move((startX, startY), (x, y), piece, targetPiece));
                    break;
                }
                else break;
            }
        }

        return moves;
    }
}
