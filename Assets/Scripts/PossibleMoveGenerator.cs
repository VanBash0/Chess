using System;
using System.Collections.Generic;

public class PossibleMoveGenerator
{
    private Dictionary<PieceType, Func<int, int, Piece, BoardState, List<Move>>> GetPossibleMovesByPiece = new Dictionary<PieceType, Func<int, int, Piece, BoardState, List<Move>>>()
    {
        {  }
    };

    public List<Move> GetPossibleMoves(int x, int y, Piece piece, BoardState boardState)
    {
        List<Move> possibleMoves = new List<Move>();
        PieceType pieceType = piece.GetPieceType();

        

        return possibleMoves;
    }

    private List<Move> GetPawnMoves(int x, int y, Piece pawn, BoardState state)
    {
        List<Move> moves = new List<Move>();
        var board = state.GetBoard();
        int newY = x + 1;
        if (newY == board.GetLength(0) || !state.IsCellEmpty(x, newY))
            return moves;
        moves.Add(new Move());
    }
}
