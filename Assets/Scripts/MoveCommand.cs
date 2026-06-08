using UnityEngine;

public class MoveCommand
{
    private readonly BoardState _boardState;
    private readonly Move _move;
    private readonly (int, int)? _previousEnPassantTarget;

    public MoveCommand(BoardState boardState, Move move)
    {
        _boardState = boardState;
        _move = move;
        _previousEnPassantTarget = _boardState.GetEnPassantTarget();
    }

    public void Execute()
    {
        _boardState.RemovePiece(_move.From);
        _boardState.SetPiece(_move.To, _move.ActivePiece);

        if (_move.IsEnPassant)
        {
            var capturedPawnSquare = (_move.To.Item1, _move.From.Item2);
            _boardState.RemovePiece(capturedPawnSquare);
        }

        if (_move.ActivePiece.Type == PieceType.King)
            _boardState.SetKingPosition(_move.ActivePiece.Color, _move.To);

        _boardState.SetEnPassantTarget(_move.NewEnPassantTarget);
    }

    public void Undo()
    {
        _boardState.SetPiece(_move.From, _move.ActivePiece);

        if ( _move.IsEnPassant)
        {
            _boardState.RemovePiece(_move.To);
            var capturedPawnSquare = (_move.To.Item1, _move.From.Item2);
            _boardState.SetPiece(capturedPawnSquare, _move.CapturedPiece);
        }
        else
        {
            if (_move.CapturedPiece != null)
                _boardState.SetPiece(_move.To, _move.CapturedPiece);
            else
                _boardState.RemovePiece(_move.To);
        }

        if (_move.ActivePiece.Type == PieceType.King)
            _boardState.SetKingPosition(_move.ActivePiece.Color, _move.From);

        _boardState.SetEnPassantTarget(_previousEnPassantTarget);
    }
}
