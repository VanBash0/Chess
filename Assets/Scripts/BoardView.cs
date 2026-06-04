using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private SquareView squarePrefab;

    private PieceView[,] _pieceViews;
    private PieceLibrarySO _pieceLibrary;
    private SquareLibrarySO _squareLibrary;

    public void Initialize(MatchController controller, PieceLibrarySO pieceLibrary, SquareLibrarySO squareLibrary)
    {
        controller.OnMoveExecuted += MovePiece;
        _pieceLibrary = pieceLibrary;
        _pieceViews = new PieceView[8, 8];
        _squareLibrary = squareLibrary;
    }

    public void CreateInitialPieceViews(BoardState state)
    {
        int boardSize = state.GetBoardSize();
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                Piece piece = state.GetPiece(x, y);
                if (piece != null)
                {
                    CreatePieceView(x, y, piece);
                }

                CreateSquareView(x, y);
            }
        }
    }

    private void CreateSquareView(int x, int y)
    {
        SquareView square = Instantiate(squarePrefab, new Vector3(2 * x, -1f, 2 * y), Quaternion.identity, this.transform);
        var color = ((x + y) % 2 == 0) ? PlayerColor.White : PlayerColor.Black;
        var material = _squareLibrary.GetSquareMaterial(color);
        square.SetMaterial(material);
    }

    private void CreatePieceView(int x, int y, Piece piece)
    {
        GameObject piecePrefab = _pieceLibrary.GetPiecePrefab(piece.Type, piece.Color);
        if (piecePrefab == null)
        {
            Debug.LogError($"No prefab found for piece type {piece.Type} and color {piece.Color}");
            return;
        }

        var rotation = (piece.Color == PlayerColor.White) ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        GameObject pieceObject = Instantiate(piecePrefab, new Vector3(2 * x, 0, 2 * y), rotation, this.transform);

        var pieceView = pieceObject.GetComponent<PieceView>();
        _pieceViews[x, y] = pieceView;

        var material = _pieceLibrary.GetPieceMaterial(piece.Color);
        pieceView.SetMaterial(material);
    }

    private void MovePiece(object sender, MatchController.MoveExecutedEventArgs e)
    {
        var move = e.Move;
    }
}
