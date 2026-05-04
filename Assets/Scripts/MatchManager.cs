using System.Collections.Generic;
using System;

public class MatchManager
{
    private BoardState _boardState;
    private Stack<MoveCommand> _moveHistory;

    public event EventHandler<MoveExecutedEventArgs> OnMoveExecuted;

    public class MoveExecutedEventArgs : EventArgs
    {
        public Move Move { get; set; }
    }

    public MatchManager()
    {
        _boardState = new BoardState();
        _moveHistory = new Stack<MoveCommand>();
    }
    
    public bool TryMakeMove(Move move)
    {
        if (MoveGenerator.GenerateLegalMoves(_boardState).Contains(move))
        {
            var cmd = new MoveCommand(_boardState, move);
            cmd.Execute();
            _moveHistory.Push(cmd);

            OnMoveExecuted?.Invoke(this, new MoveExecutedEventArgs { Move = move });

            _boardState.SwitchPlayer();
            return true;
        }
        return false;
    }
}