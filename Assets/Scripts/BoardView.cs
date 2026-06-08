using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BoardView : MonoBehaviour
{
    [SerializeField] private SquareView squarePrefab;

    private PieceView[,] _pieceViews;
    private SquareView[,] _squareViews;
    private PieceLibrarySO _pieceLibrary;
    private SquareLibrarySO _squareLibrary;
    private List<(int, int)> _selectedSquares = new List<(int, int)>();

    private const int SCALE = 2;

    public void Initialize(MatchController controller, PieceLibrarySO pieceLibrary, SquareLibrarySO squareLibrary)
    {
        controller.OnMoveExecuted += MovePiece;
        controller.OnSelectionCleared += ClearPieceSelection;
        controller.OnPieceSelected += HandlePieceSelection;
        _pieceLibrary = pieceLibrary;
        _pieceViews = new PieceView[8, 8];
        _squareLibrary = squareLibrary;
        _squareViews = new SquareView[8, 8];
    }

    private void HandlePieceSelection(object sender, MatchController.PieceSelectedEventArgs e)
    {
        var (x, y) = e.Square;
        _squareViews[x, y].SetSquareType(SquareType.ActivePiece);
        _selectedSquares.Add((x, y));

        var moves = e.LegalMoves;
        foreach (var move in moves)
        {
            var (toX, toY) = move.To;
            _squareViews[toX, toY].SetSquareType(SquareType.Target);
            _selectedSquares.Add((toX, toY));
        }
    }

    private void ClearPieceSelection(object sender, System.EventArgs e)
    {
        foreach (var square in _selectedSquares)
        {
            var (x, y) = square;
            _squareViews[x, y].SetSquareType(SquareType.Idle);
        }
        _selectedSquares.Clear();
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
        SquareView square = Instantiate(squarePrefab, new Vector3(SCALE * x, -1f, SCALE * y), Quaternion.identity, this.transform);
        var color = ((x + y) % 2 == 0) ? PlayerColor.Black : PlayerColor.White;
        var material = _squareLibrary.GetSquareMaterial(color);
        square.SetBaseMaterial(material);
        square.SetCoords(x, y);
        _squareViews[x, y] = square;
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
        GameObject pieceObject = Instantiate(piecePrefab, new Vector3(SCALE * x, 0, SCALE * y), rotation, this.transform);

        var pieceView = pieceObject.GetComponent<PieceView>();
        _pieceViews[x, y] = pieceView;

        var material = _pieceLibrary.GetPieceMaterial(piece.Color);
        pieceView.SetMaterial(material);
        pieceView.SetCoords(x, y);
    }

    private void MovePiece(object sender, MatchController.MoveExecutedEventArgs e)
    {
        var move = e.Move;
        var (fromX, fromY) = move.From;
        var (toX, toY) = move.To;

        var activePiece = _pieceViews[fromX, fromY];
        if (activePiece == null) return;

        if (move.CapturedPiece != null)
        {
            var targetY = move.IsEnPassant ? fromY : toY;
            var capturedPiece = _pieceViews[toX, targetY];
            if (capturedPiece != null)
                capturedPiece.DestroyPiece();
        }

        activePiece.MoveTo(new Vector3(SCALE * toX, 0f, SCALE * toY), toX, toY);

        _pieceViews[fromX, fromY] = null;
        _pieceViews[toX, toY] = activePiece;
    }
}
