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

        _matchController.Test();
    }
}
