using System.Collections.Generic;
using System;

public enum MatchPhase
{
    WaitingForSelection,
    PieceSelected,
    MoveResolving,
    GameOver
}

public class MatchController
{
    private BoardState _boardState;
    private Stack<MoveCommand> _moveHistory;
    private MatchPhase _phase;
    private List<Move> _legalMoves;
    private (int, int)? _selectedSquare;
    private List<Move> _selectedPieceMoves;

    public event EventHandler<MoveExecutedEventArgs> OnMoveExecuted;
    public event EventHandler<PieceSelectedEventArgs> OnPieceSelected;
    public event EventHandler OnSelectionCleared;

    public class MoveExecutedEventArgs : EventArgs
    {
        public Move Move { get; set; }
    }

    public class PieceSelectedEventArgs : EventArgs
    {
        public (int, int) Square { get; set; }
        public IReadOnlyList<Move> LegalMoves { get; set; } 
    }

    public MatchController(BoardState boardState)
    {
        _boardState = boardState;
        _moveHistory = new Stack<MoveCommand>();
        StartTurn();
    }
    
    public bool TryMakeMove(Move move)
    {
        if (!_legalMoves.Contains(move)) return false;

        ExecuteMove(move);
        EndTurn();
        return true;
    }

    public void SelectSquare((int, int) squareCoord)
    {
        switch (_phase)
        {
            case MatchPhase.WaitingForSelection:
                HandleSelection(squareCoord);
                break;

            case MatchPhase.PieceSelected:
                HandleSelectedPieceInput(squareCoord);
                break;
        }
    }

    public void StartTurn()
    {
        _legalMoves = MoveGenerator.GenerateLegalMoves(_boardState);
        _selectedSquare = null;
        _selectedPieceMoves = null;
        _phase = _legalMoves.Count == 0 ? MatchPhase.GameOver : MatchPhase.WaitingForSelection;
    }

    private void HandleSelection((int, int) squareCoord)
    {
        var piece = _boardState.GetPiece(squareCoord);
        if (piece == null || piece.Color != _boardState.GetCurrentPlayer()) return;

        var pieceMoves = GetMovesFrom(squareCoord);

        _selectedSquare = squareCoord;
        _selectedPieceMoves = pieceMoves;
        _phase = MatchPhase.PieceSelected;

        OnPieceSelected?.Invoke(this, new PieceSelectedEventArgs { Square = squareCoord, LegalMoves = pieceMoves }); 
    }

    private void HandleSelectedPieceInput((int, int) squareCoord)
    {
        var targetPiece = _boardState.GetPiece(squareCoord);

        if (targetPiece != null && targetPiece.Color == _boardState.GetCurrentPlayer())
        {
            ClearSelection();
            HandleSelection(squareCoord);
            return;
        }

        var move = FindSelectedMoveTo(squareCoord);
        if (move == null)
        {
            ClearSelection();
            return;
        }

        ClearSelection();
        ExecuteMove(move.Value);
        EndTurn();
    }

    private List<Move> GetMovesFrom((int, int) squareCoord)
    {
        var moves = new List<Move>();

        foreach (var move in _legalMoves)
        {
            if (move.From == squareCoord)
                moves.Add(move);
        }

        return moves;
    }

    private Move? FindSelectedMoveTo((int, int) squareCoord)
    {
        if (_selectedPieceMoves == null) return null;

        foreach (var move in _selectedPieceMoves)
        {
            if (move.To == squareCoord) return move;
        }

        return null;
    }

    private void ClearSelection()
    {
        _selectedSquare = null;
        _selectedPieceMoves = null;
        _phase = MatchPhase.WaitingForSelection;

        OnSelectionCleared?.Invoke(this, EventArgs.Empty);
    }

    private void ExecuteMove(Move move)
    {
        var cmd = new MoveCommand(_boardState, move);
        cmd.Execute();
        _moveHistory.Push(cmd);

        OnMoveExecuted?.Invoke(this, new MoveExecutedEventArgs { Move = move });
    }

    private void EndTurn()
    {
        _boardState.SwitchPlayer();
        StartTurn();
    }
}
