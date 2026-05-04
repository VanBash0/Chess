using System.Collections.Generic;

public static class MoveGenerator
{
    public static List<Move> GenerateLegalMoves(BoardState state)
    {
        var legalMoves = new List<Move>();
        PlayerColor currentPlayerColor = state.GetCurrentPlayer();
        PlayerColor oppositePlayerColor = (currentPlayerColor == PlayerColor.Black) ? PlayerColor.White : PlayerColor.Black;
        Piece[,] board = state.GetBoard();
        int boardLength = board.GetLength(0);
        for (int y = 0; y < boardLength; y++)
        {
            for (int x = 0; x < boardLength; x++)
            {
                var piece = board[x, y];
                if (piece is null || piece.GetColor() != currentPlayerColor)
                    continue;

                var possibleMoves = PossibleMoveGenerator.GetPossibleMoves(x, y, piece, state);
                legalMoves.AddRange(possibleMoves);
            }
        }
        return legalMoves;
    }
}
