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

        if (state.IsCellOnBoard(x, y + direction) && state.IsCellEmpty(x, y + direction))
        {
            moves.Add(new Move((x, y), (x, y + direction), pawn));

            bool isOnStartPos = (color == PlayerColor.White && y == 1) || (color == PlayerColor.Black && y == 6);
            if (isOnStartPos && state.IsCellOnBoard(x, y + 2 * direction) && state.IsCellEmpty(x, y + 2 * direction))
            {
                moves.Add(new Move((x, y), (x, y + 2 * direction), pawn));
            }
        }

        int[] dx = { 1, -1 };
        foreach (var offsetX in dx)
        {
            int targetX = x + offsetX;
            int targetY = y + direction;
            if (!state.IsCellOnBoard(targetX, targetY)) continue;

            var targetPiece = state.GetPiece(targetX, targetY);

            if (targetPiece != null && targetPiece.Color != color)
                moves.Add(new Move((x, y), (targetX, targetY), pawn, targetPiece));

            else if (targetPiece == null && state.IsCellOnBoard(targetX, y) && state.GetEnPassantTarget() == (targetX, targetY))
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
            
            if (!state.IsCellOnBoard(targetX, targetY)) continue;

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
        return new List<Move>();
    }

    private static List<Move> GetRookMoves(int x, int y, Piece rook, BoardState state)
    {
        return new List<Move>();
    }

    private static List<Move> GetQueenMoves(int x, int y, Piece queen, BoardState state)
    {
        return new List<Move>();
    }

    private static List<Move> GetKingMoves(int x, int y, Piece king, BoardState state)
    {
        return new List<Move>();
    }
}
