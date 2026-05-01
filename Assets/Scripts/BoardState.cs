public enum PlayerColor
{
    White,
    Black
}

public class BoardState
{
    private Piece[,] _board;
    private PlayerColor _currentPlayer = PlayerColor.White;
    bool _hasWhiteCastled = false;
    bool _hasBlackCastled = false;
    bool _isWhiteUnderCheck = false;
    bool _isBlackUnderCheck = false;

    public PlayerColor GetCurrentPlayer() => _currentPlayer;

    public Piece[,] GetBoard() => _board;

    public bool IsCellEmpty(int x, int y) => _board[x, y] is null; 
}