public enum PlayerColor
{
    White,
    Black
}

public class BoardState
{
    private Piece[,] _board;
    private PlayerColor _currentPlayer = PlayerColor.White;
    private bool _hasWhiteCastled = false;
    private bool _hasBlackCastled = false;
    private (int, int)? _enPassantTarget = null;

    public PlayerColor GetCurrentPlayer() => _currentPlayer;

    public Piece[,] GetBoard() => _board;

    public bool IsCellEmpty(int x, int y) => _board[x, y] is null;

    public bool IsCellOnBoard(int x, int y) => (0 <= x && x < 8 && 0 <= y && y < 8);

    public void RemovePiece((int, int) cell)
    {
        var (x, y) = cell;
        _board[x, y] = null;
    }

    public void SetPiece((int, int) cell, Piece piece)
    {
        var (x, y) = cell;
        _board[x, y] = piece;
    }

    public void SwitchPlayer()
    {
        _currentPlayer = (_currentPlayer == PlayerColor.White) ? PlayerColor.Black : PlayerColor.White;
    }

    public int GetBoardSize() => _board.GetLength(0);

    public Piece GetPiece(int x, int y) => _board[x, y];

    public (int, int)? GetEnPassantTarget() => _enPassantTarget;

    public BoardState()
    {
        _board = new Piece[8, 8];

        int boardSize = GetBoardSize();
        for (int i = 0; i < boardSize; i++)
        {
            _board[i, 1] = new Piece(PieceType.Pawn, PlayerColor.White);
            _board[i, 6] = new Piece(PieceType.Pawn, PlayerColor.Black);
        }

        _board[5, 5] = new Piece(PieceType.Pawn, PlayerColor.White);

        _board[0, 0] = new Piece(PieceType.Rook, PlayerColor.White);
        _board[1, 0] = new Piece(PieceType.Knight, PlayerColor.White);
        _board[2, 0] = new Piece(PieceType.Bishop, PlayerColor.White);
        _board[3, 0] = new Piece(PieceType.Queen, PlayerColor.White);
        _board[4, 0] = new Piece(PieceType.King, PlayerColor.White);
        _board[5, 0] = new Piece(PieceType.Bishop, PlayerColor.White);
        _board[6, 0] = new Piece(PieceType.Knight, PlayerColor.White);
        _board[7, 0] = new Piece(PieceType.Rook, PlayerColor.White);

        _board[0, 7] = new Piece(PieceType.Rook, PlayerColor.Black);
        _board[1, 7] = new Piece(PieceType.Knight, PlayerColor.Black);
        _board[2, 7] = new Piece(PieceType.Bishop, PlayerColor.Black);
        _board[3, 7] = new Piece(PieceType.Queen, PlayerColor.Black);
        _board[4, 7] = new Piece(PieceType.King, PlayerColor.Black);
        _board[5, 7] = new Piece(PieceType.Bishop, PlayerColor.Black);
        _board[6, 7] = new Piece(PieceType.Knight, PlayerColor.Black);
        _board[7, 7] = new Piece(PieceType.Rook, PlayerColor.Black);
    }
}