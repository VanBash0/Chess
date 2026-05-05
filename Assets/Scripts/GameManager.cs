using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoardView boardView;
    [SerializeField ] PieceLibrarySO pieceLibrary;

    private MatchController _matchController;
    private BoardState _boardState;

    private void Start()
    {
        _boardState = new BoardState();
        _matchController = new MatchController(_boardState);
        
        boardView.Initialize(_matchController, pieceLibrary);
        boardView.CreateInitialPieceViews(_boardState);

        Test();
    }

    private void Test()
    {
        var legalMoves = MoveGenerator.GenerateLegalMoves(_boardState);
        foreach (var move in legalMoves)
        {
            var from = SquareTranslator.GetNotation(move.From);
            var to = SquareTranslator.GetNotation(move.To);
            if (move.CapturedPiece != null)
                UnityEngine.Debug.Log($"Legal move: {from} to {to} by {move.ActivePiece.Color} {move.ActivePiece.Type} with taking of {move.CapturedPiece.Color} {move.CapturedPiece.Type}");
            else
                UnityEngine.Debug.Log($"Legal move: {from} to {to} by {move.ActivePiece.Color} {move.ActivePiece.Type}");
        }
    }
}
