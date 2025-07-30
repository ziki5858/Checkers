using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    /// <summary>
    /// Represents the pieces on the board.
    /// </summary>
    internal enum Piece
    {
        Empty = 0,
        HumanPawn = 1,
        HumanQueen = 2,
        ComputerPawn = -1,
        ComputerQueen = -2
    }

    /// <summary>
    /// AI Engine — contains all logic related to board evaluation, move generation,
    /// and the Alpha-Beta-pruned Minimax search used by the computer player.
    /// </summary>
    internal static class AiEngine
    {
        private const int PAWN_VALUE = 10;
        private const int QUEEN_VALUE = 30;
        private const int PROGRESS_BONUS_PER_ROW = 2;


        /// <summary>
        /// Calculates the heuristic evaluation score of the current board state.
        /// Positive ⇒ advantage to the computer (Player 2 / negative pieces),
        /// Negative ⇒ advantage to the human (Player 1 / positive pieces).
        /// - Pawn = 10 pts + progress bonus  
        /// - Queen = 30 pts  
        /// - Progress bonus = 2 pts per rank advanced toward promotion.
        /// </summary>
        public static int CalculateHeuristicScore(int[,] board)
        {
            int boardSize = board.GetLength(0);
            int totalScore = 0;

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    switch ((Piece)board[row, col])
                    {
                        case Piece.HumanPawn:
                            totalScore -= PAWN_VALUE
                                + PROGRESS_BONUS_PER_ROW * (boardSize - row - 1);
                            break;
                        case Piece.HumanQueen:
                            totalScore -= QUEEN_VALUE;
                            break;
                        case Piece.ComputerPawn:
                            totalScore += PAWN_VALUE
                                + PROGRESS_BONUS_PER_ROW * row;
                            break;
                        case Piece.ComputerQueen:
                            totalScore += QUEEN_VALUE;
                            break;
                        default:
                            break;
                    }
                }
            }

            return totalScore;
        }

        /// <summary>
        /// Returns a new board array with <paramref name="move"/> applied
        /// (handles capture & automatic promotion for computer pawns).
        /// </summary>
        public static int[,] ApplyMoveToBoard(int[,] board, Move move)
        {
            var clone = (int[,])board.Clone();
            int lastRow = clone.GetLength(0) - 1;

            bool isPromotion = move.YTo == lastRow;
            clone[move.YTo, move.XTo] = isPromotion
                ? (int)Piece.ComputerQueen
                : clone[move.YFrom, move.XFrom];

            clone[move.YFrom, move.XFrom] = (int)Piece.Empty;

            if (move.IsCapture())
            {
                int eatY = (move.YFrom + move.YTo) / 2;
                int eatX = (move.XFrom + move.XTo) / 2;
                clone[eatY, eatX] = (int)Piece.Empty;
            }

            return clone;
        }

        public static List<Move> GetAllComputerMoves(int[,] board, GameLogicManager logic)
            => GetAllMoves(board, logic, forComputer: true);

        public static List<Move> GetAllPlayerMoves(int[,] board, GameLogicManager logic)
            => GetAllMoves(board, logic, forComputer: false);

        /// <summary>
        /// Main entry for Minimax with Alpha-Beta pruning.
        /// </summary>
        public static int MiniMaxAlphaBeta(
            int[,] board,
            int depth,
            int alpha,
            int beta,
            bool maximizing,
            GameLogicManager logic)
        {
            if (depth == 0)
                return CalculateHeuristicScore(board);

            var moves = maximizing
                ? GetAllComputerMoves(board, logic)
                : GetAllPlayerMoves(board, logic);

            if (moves.Count == 0)
                return CalculateHeuristicScore(board);

            SortMoves(ref moves, maximizing);

            return maximizing
                ? MaxBranch(board, depth, alpha, beta, logic, moves)
                : MinBranch(board, depth, alpha, beta, logic, moves);
        }

        /// <summary>
        /// Assigns a Minimax score to every legal computer move and
        /// returns all moves that share the highest score (for random tie-break).
        /// </summary>
        public static List<Move> CalculateComputerMoves(
            int[,] board,
            int depth,
            GameLogicManager logic)
        {
            var list = new List<Move>();
            foreach (var mv in GetAllComputerMoves(board, logic))
            {
                int[,] after = ApplyMoveToBoard(board, mv);
                mv.Nikot = MiniMaxAlphaBeta(
                    after,
                    depth - 1,
                    int.MinValue,
                    int.MaxValue,
                    maximizing: false,
                    logic);
                list.Add(mv);
            }

            if (!list.Any()) return list;
            int best = list.Max(m => m.Nikot);
            return list.Where(m => m.Nikot == best).ToList();
        }

        /// <summary>
        /// Categorizes a move as either Winning, Threatening, or Risky.
        /// </summary>
        public static void ClassifyMove(
            Move move,
            int[,] board,
            GameLogicManager logic,
            ref List<Move> winning,
            ref List<Move> threats,
            ref List<Move> risky,
            int depth)
        {
            int[,] simulated = ApplyMoveToBoard(board, move);

            if (logic.CheckWin(simulated, false))
            {
                winning.Add(move);
                return;
            }

            if (depth <= 0)
            {
                risky.Add(move);
                return;
            }

            Move counter = GetBestEnemyResponse(simulated, move, logic);
            if (counter.YFrom != -1)
                threats.Add(counter);
        }

        /// <summary>
        /// Searches for the opponent’s highest-scoring move in response.
        /// </summary>
        public static Move GetBestEnemyResponse(
            int[,] board,
            Move origin,
            GameLogicManager logic)
        {
            int bestScore = int.MinValue;
            Move bestMove = new Move { YFrom = -1 };

            foreach (var mv in GetAllPlayerMoves(board, logic))
            {
                if (mv.Nikot > bestScore)
                {
                    bestScore = mv.Nikot;
                    bestMove = mv;
                    bestMove.firstMove = origin;
                }
            }

            return bestMove;
        }

        public static bool IsCapture(this Move m)
            => Math.Abs(m.XTo - m.XFrom) == 2;


        private static List<Move> GetAllMoves(
            int[,] board,
            GameLogicManager logic,
            bool forComputer)
        {
            var list = new List<Move>();
            int size = board.GetLength(0);

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    int cell = board[r, c];
                    if ((forComputer && cell < 0) || (!forComputer && cell > 0))
                        list.AddRange(logic.GetMoves(board, r, c, !forComputer));
                }
            }

            return list;
        }

        private static void SortMoves(ref List<Move> moves, bool maximizing)
        {
            moves = maximizing
                ? moves.OrderByDescending(m => m.Nikot).ToList()
                : moves.OrderBy(m => m.Nikot).ToList();
        }

        private static int MaxBranch(
            int[,] board,
            int depth,
            int alpha,
            int beta,
            GameLogicManager logic,
            List<Move> moves)
        {
            int best = int.MinValue;
            foreach (var mv in moves)
            {
                int score = MiniMaxAlphaBeta(
                    ApplyMoveToBoard(board, mv),
                    depth - 1,
                    alpha, beta,
                    maximizing: false,
                    logic);

                best = Math.Max(best, score);
                alpha = Math.Max(alpha, score);
                if (beta <= alpha) break; // prune
            }
            return best;
        }

        private static int MinBranch(
            int[,] board,
            int depth,
            int alpha,
            int beta,
            GameLogicManager logic,
            List<Move> moves)
        {
            int best = int.MaxValue;
            foreach (var mv in moves)
            {
                int score = MiniMaxAlphaBeta(
                    ApplyMoveToBoard(board, mv),
                    depth - 1,
                    alpha, beta,
                    maximizing: true,
                    logic);

                best = Math.Min(best, score);
                beta = Math.Min(beta, score);
                if (beta <= alpha) break; // prune
            }
            return best;
        }
    }
}
