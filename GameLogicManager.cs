using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Checkers
{
    /// <summary>
    /// Represents a directional offset for diagonal movement on the board.
    /// </summary>
    public class MoveOffset
    {
        /// <summary>Row change component of the offset.</summary>
        public int DRow { get; }

        /// <summary>Column change component of the offset.</summary>
        public int DCol { get; }

        /// <summary>
        /// Constructs a new MoveOffset with specified row and column deltas.
        /// </summary>
        public MoveOffset(int dRow, int dCol)
        {
            DRow = dRow;
            DCol = dCol;
        }

        /// <summary>
        /// Applies this offset to a given board coordinate.
        /// </summary>
        /// <param name="row">Original row index.</param>
        /// <param name="col">Original column index.</param>
        /// <returns>Tuple of new (row, col) after applying offset.</returns>
        public (int newRow, int newCol) Apply(int row, int col)
            => (row + DRow, col + DCol);
    }

    /// <summary>
    /// Provides game logic utilities: move generation, win detection, and board simulation.
    /// </summary>
    internal class GameLogicManager
    {
        private readonly int arraySize;

        // Piece encoding constants
        private const int EMPTY = 0;
        private const int P1_PAWN = 1;
        private const int P1_KING = 2;
        private const int P2_PAWN = -1;
        private const int P2_KING = -2;

        /// <summary>
        /// Initializes manager with specified board dimensions.
        /// </summary>
        /// <param name="arraySize">Number of rows/columns on the square board.</param>
        public GameLogicManager(int arraySize)
        {
            this.arraySize = arraySize;
        }

        /// <summary>
        /// Generates all legal moves for the piece at a given location.
        /// </summary>
        /// <param name="board">Current board state array.</param>
        /// <param name="row">Row index of the piece.</param>
        /// <param name="col">Column index of the piece.</param>
        /// <param name="white">True if it is player1's turn, affecting move direction.</param>
        /// <returns>List of possible Move objects including captures.</returns>
        public List<Move> GetMoves(int[,] board, int row, int col, bool white)
        {
            int size = board.GetLength(0);
            int piece = board[row, col];
            bool isKing = Math.Abs(piece) == P1_KING;
            int forward = white ? -1 : 1;
            int enemyPawn = white ? P2_PAWN : P1_PAWN;
            int enemyKing = white ? P2_KING : P1_KING;

            // Define diagonal offsets: kings move both directions, pawns only forward
            var offsets = isKing
                ? new[] { new MoveOffset(-1, -1), new MoveOffset(-1, 1), new MoveOffset(1, -1), new MoveOffset(1, 1) }
                : new[] { new MoveOffset(forward, -1), new MoveOffset(forward, 1) };

            var moves = new List<Move>();

            foreach (var offset in offsets)
            {
                var (nr, nc) = offset.Apply(row, col);
                if (!InBounds(nr, nc, size)) continue;

                // Simple step move
                if (board[nr, nc] == EMPTY)
                {
                    var newBoard = SimulateMove(board, row, col, nr, nc, white, piece, nr);
                    int score = AiEngine.CalculateHeuristicScore(newBoard);
                    moves.Add(new Move(row, col, 0, 0, nr, nc, score));
                }
                // Capture move: adjacent enemy, landing behind empty
                else if (board[nr, nc] == enemyPawn || board[nr, nc] == enemyKing)
                {
                    var (jr, jc) = offset.Apply(nr, nc);
                    if (!InBounds(jr, jc, size) || board[jr, jc] != EMPTY) continue;

                    var newBoard = SimulateCapture(board, row, col, nr, nc, jr, jc, white, piece);
                    int score = AiEngine.CalculateHeuristicScore(newBoard);
                    moves.Add(new Move(row, col, nr, nc, jr, jc, score));
                }
            }

            return moves;
        }

        /// <summary>
        /// Checks if specified indices lie within board boundaries.
        /// </summary>
        private bool InBounds(int r, int c, int size) => r >= 0 && r < size && c >= 0 && c < size;

        /// <summary>
        /// Returns a new board state after a non-capturing move or promotion.
        /// </summary>
        private int[,] SimulateMove(int[,] board, int fromRow, int fromCol, int toRow, int toCol, bool white, int piece, int promoteRow)
        {
            var newBoard = (int[,])board.Clone();
            bool promote = (white && promoteRow == 0) || (!white && promoteRow == board.GetLength(0) - 1);
            newBoard[toRow, toCol] = promote ? (white ? P1_KING : P2_KING) : piece;
            newBoard[fromRow, fromCol] = EMPTY;
            return newBoard;
        }

        /// <summary>
        /// Returns a new board state after a capturing jump and promotion if applicable.
        /// </summary>
        private int[,] SimulateCapture(int[,] board, int fromRow, int fromCol, int midRow, int midCol, int toRow, int toCol, bool white, int piece)
        {
            var newBoard = SimulateMove(board, fromRow, fromCol, toRow, toCol, white, piece, toRow);
            newBoard[midRow, midCol] = EMPTY;
            return newBoard;
        }

        /// <summary>
        /// Determines whether the opponent has any moves; if none, current player wins.
        /// </summary>
        /// <param name="board">Current board state.</param>
        /// <param name="isPlayer1Turn">True if checking after player1's move.</param>
        /// <returns>True if opponent cannot move (win), otherwise false.</returns>
        public bool CheckWin(int[,] board, bool isPlayer1Turn)
        {
            int targetSign = isPlayer1Turn ? -1 : 1; // look for enemy pieces
            for (int r = 0; r < arraySize; r++)
            {
                for (int c = 0; c < arraySize; c++)
                {
                    if (Math.Sign(board[r, c]) == targetSign)
                    {
                        var moves = GetMoves(board, r, c, !isPlayer1Turn);
                        if (moves.Count > 0) return false;
                    }
                }
            }
            return true;
        }
    }
}