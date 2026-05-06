using System.Collections.Generic;

public static class MoveGenerator
{
    public static List<Move> GenerateLegalMoves(BoardState state)
    {
        var pseudoLegalMoves = new List<Move>();
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
                pseudoLegalMoves.AddRange(possibleMoves);
            }
        }

        var legalMoves = new List<Move>();
        foreach (var move in pseudoLegalMoves)
        {
            var cmd = new MoveCommand(state, move);

            cmd.Execute();

            if (!CheckChecker.CheckCheck(state, currentPlayerColor))
            {
                legalMoves.Add(move);
            }

            cmd.Undo();
        }

        return legalMoves;
    }
}
