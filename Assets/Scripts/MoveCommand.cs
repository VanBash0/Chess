using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class MoveCommand
{
    private readonly BoardState _boardState;
    private readonly Move _move;

    public MoveCommand(BoardState boardState, Move move)
    {
        _boardState = boardState;
        _move = move;
    }

    public void Execute()
    {
        _boardState.RemovePiece(_move.From);
        _boardState.SetPiece(_move.To, _move.ActivePiece);

        if (_move.ActivePiece.Type == PieceType.King)
            _boardState.SetKingPosition(_move.ActivePiece.Color, _move.To);
    }

    public void Undo()
    {
        _boardState.SetPiece(_move.From, _move.ActivePiece);

        if (_move.CapturedPiece != null)
            _boardState.SetPiece(_move.To, _move.CapturedPiece);
        else
            _boardState.RemovePiece(_move.To);

        if (_move.ActivePiece.Type == PieceType.King)
            _boardState.SetKingPosition(_move.ActivePiece.Color, _move.From);
    }
}
