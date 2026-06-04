using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoardView boardView;
    [SerializeField] PieceLibrarySO pieceLibrary;
    [SerializeField] SquareLibrarySO squareLibrary;
    [SerializeField] PlayerInput playerInput;

    private MatchController _matchController;
    private BoardState _boardState;

    private void Start()
    {
        _boardState = new BoardState();
        _matchController = new MatchController(_boardState);
        
        boardView.Initialize(_matchController, pieceLibrary, squareLibrary);
        boardView.CreateInitialPieceViews(_boardState);

        playerInput.OnSquareSelected += HandleSquareSelected;
    }

    private void HandleSquareSelected(object sender, PlayerInput.SquareSelectedEventArgs e)
    {
        _matchController.SelectSquare(e.SquareCoord);
    }
}
