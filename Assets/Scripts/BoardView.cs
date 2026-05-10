using UnityEngine;

public class BoardView : MonoBehaviour
{
    private PieceView[,] _pieceViews;
    private PieceLibrarySO _pieceLibrary;

    public void Initialize(MatchController controller, PieceLibrarySO pieceLibrary)
    {
        controller.OnMoveExecuted += MovePiece;
        _pieceLibrary = pieceLibrary;
        _pieceViews = new PieceView[8, 8];
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
            }
        }
    }

    private void CreatePieceView(int x, int y, Piece piece)
    {
        GameObject piecePrefab = _pieceLibrary.GetPiecePrefab(piece.Type, piece.Color);
        if (piecePrefab == null)
        {
            Debug.LogError($"No prefab found for piece type {piece.Type} and color {piece.Color}");
            return;
        }

        GameObject pieceObject = Instantiate(piecePrefab, new Vector3(2 * x, 0, 2 * y), Quaternion.identity, this.transform);
        _pieceViews[x, y] = pieceObject.GetComponent<PieceView>();
    }

    private void MovePiece(object sender, MatchController.MoveExecutedEventArgs e)
    {
        var move = e.Move;
    }
}
