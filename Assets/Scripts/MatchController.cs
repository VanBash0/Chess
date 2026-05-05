using System.Collections.Generic;
using System;

public class MatchController
{
    private BoardState _boardState;
    private Stack<MoveCommand> _moveHistory;

    public event EventHandler<MoveExecutedEventArgs> OnMoveExecuted;

    public class MoveExecutedEventArgs : EventArgs
    {
        public Move Move { get; set; }
    }

    public MatchController(BoardState boardState)
    {
        _boardState = boardState;
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

    public void Test()
    {
        var legalMoves = MoveGenerator.GenerateLegalMoves(_boardState);
        foreach (var move in legalMoves)
        {
            if (move.CapturedPiece != null)
                UnityEngine.Debug.Log($"Legal move: {move.From} to {move.To} by {move.ActivePiece.Color} {move.ActivePiece.Type} with taking of {move.CapturedPiece.Color} {move.CapturedPiece.Type}");
            else
                UnityEngine.Debug.Log($"Legal move: {move.From} to {move.To} by {move.ActivePiece.Color} {move.ActivePiece.Type}");
        }
    }
}