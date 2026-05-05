using System.Collections.Generic;

public static class MoveGenerator
{
    public static List<Move> GenerateLegalMoves(BoardState state)
    {
        var legalMoves = new List<Move>();
        PlayerColor currentPlayerColor = state.GetCurrentPlayer();
        PlayerColor oppositePlayerColor = (currentPlayerColor == PlayerColor.Black) ? PlayerColor.White : PlayerColor.Black;
        int boardSize = state.GetBoardSize();
        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                var piece = state.GetPiece(x, y);
                if (piece is null || piece.Color != currentPlayerColor)
                    continue;

                var possibleMoves = PossibleMoveGenerator.GetPossibleMoves(x, y, piece, state);
                legalMoves.AddRange(possibleMoves);
            }
        }
        return legalMoves;
    }
}
