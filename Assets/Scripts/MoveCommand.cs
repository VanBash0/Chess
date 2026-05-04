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
        _boardState.RemovePiece(_move.StartCell);
        _boardState.SetPiece(_move.EndCell, _move.ActivePiece);
    }

    public void Undo()
    {
        _boardState.SetPiece(_move.StartCell, _move.ActivePiece);

        if (_move.CapturedPiece != null)
            _boardState.SetPiece(_move.EndCell, _move.CapturedPiece);
        else
            _boardState.RemovePiece(_move.EndCell);
    }
}
