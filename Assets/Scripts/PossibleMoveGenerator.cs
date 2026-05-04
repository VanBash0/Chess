using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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

    public static List<Move> GetPossibleMoves(int x, int y, Piece piece, BoardState boardState)
    {
        PieceType pieceType = piece.GetPieceType();

        if (!possibleMovesByPiece.TryGetValue(pieceType, out var moveGenerator))
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
        var board = state.GetBoard();
        var color = state.GetCurrentPlayer();
        int direction = (color == PlayerColor.White) ? 1 : -1;

        if (state.IsCellEmpty(x, y + direction))
            moves.Add(new Move((x, y), (x, y + direction), pawn));

        bool isOnStartPos = (color == PlayerColor.White && y == 1) || (color == PlayerColor.Black && y == 6);
        if (isOnStartPos && state.IsCellEmpty(x, y + 2 * direction))
            moves.Add(new Move((x, y), (x, y + direction * 2), pawn));

        return moves;
    }

    private static List<Move> GetKnightMoves(int x, int y, Piece knight, BoardState state)
    {
        return new List<Move>();
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
