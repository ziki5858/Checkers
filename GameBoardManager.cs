using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
    /// <summary>
    /// Manages the checkers game board UI, including building, resetting, highlighting moves, and resizing.
    /// </summary>
    internal class GameBoardManager
    {
        // Panel where board buttons are displayed
        private readonly Panel panel;
        // Number of rows/columns for the board (e.g., 8 for an 8x8 board)
        private readonly int arraySize;
        // 2D arrays for buttons and underlying game logic
        private Button[,] boardButtons;
        private int[,] boardLogic;
        // Color used to highlight valid move destinations
        private Color highlightColor;
        // Callback to update UI label with current turn
        private readonly Action updateTurnLabelCallback;

        /// <summary>
        /// Initializes a new instance of GameBoardManager.
        /// </summary>
        /// <param name="panel">Panel control to host board buttons.</param>
        /// <param name="arraySize">Size of the board (rows and columns).</param>
        /// <param name="highlightColor">Color used for highlighting valid moves.</param>
        /// <param name="updateTurnLabelCallback">Action to call when turn label should be updated.</param>
        public GameBoardManager(
            Panel panel,
            int arraySize,
            Color highlightColor,
            Action updateTurnLabelCallback)
        {
            this.panel = panel;
            this.arraySize = arraySize;
            this.highlightColor = highlightColor;
            this.updateTurnLabelCallback = updateTurnLabelCallback;

            boardButtons = new Button[arraySize, arraySize];
            boardLogic = new int[arraySize, arraySize];
        }

        /// <summary>Returns the buttons representing board cells.</summary>
        public Button[,] GetBoardButtons() => boardButtons;

        /// <summary>Returns the underlying board logic array.</summary>
        public int[,] GetBoardLogic() => boardLogic;

        /// <summary>
        /// Builds and lays out the board buttons, clearing existing controls.
        /// </summary>
        /// <param name="onCellClick">Event handler for each button's click event.</param>
        public void BuildBoard(EventHandler onCellClick)
        {
            panel.Controls.Clear();
            int cellWidth = panel.Width / arraySize;
            int cellHeight = panel.Height / arraySize;
            int fontSize = arraySize == 6 ? 40 : arraySize == 8 ? 33 : 24;

            for (int row = 0; row < arraySize; row++)
            {
                for (int col = 0; col < arraySize; col++)
                {
                    boardLogic[row, col] = 0;

                    var button = new Button
                    {
                        Size = new Size(cellWidth, cellHeight),
                        Location = new Point(col * cellWidth, row * cellHeight),
                        Font = new Font("Microsoft Sans Serif", fontSize),
                        FlatStyle = FlatStyle.Flat,
                        ForeColor = highlightColor,
                        Tag = row * arraySize + col
                    };

                    button.FlatAppearance.BorderColor = Color.White;
                    button.Click += onCellClick;

                    boardButtons[row, col] = button;
                    panel.Controls.Add(button);
                }
            }
        }

        /// <summary>
        /// Resets the board to its initial layout with player pieces.
        /// </summary>
        /// <param name="player1Symbol">Symbol for player 1 pieces.</param>
        /// <param name="player2Symbol">Symbol for player 2 pieces.</param>
        /// <param name="emptySymbol">Symbol for empty cells.</param>
        public void ResetBoard(string player1Symbol, string player2Symbol, string emptySymbol)
        {
            // Number of rows each player occupies at start
            int rowsPerPlayer = (arraySize / 2) - 1;

            for (int row = 0; row < arraySize; row++)
            {
                for (int col = 0; col < arraySize; col++)
                {
                    bool isDarkSquare = (row + col) % 2 != 0;

                    // Clear cell
                    boardButtons[row, col].Text = emptySymbol;
                    boardLogic[row, col] = (int)Piece.Empty;
                    boardButtons[row, col].BackColor = isDarkSquare ? Color.Black : Color.White;

                    // Place pieces only on dark squares
                    if (!isDarkSquare) continue;

                    if (row < rowsPerPlayer)
                    {
                        // Top rows for computer
                        boardButtons[row, col].Text = player2Symbol;
                        boardLogic[row, col] = (int)Piece.ComputerPawn;
                    }
                    else if (row >= arraySize - rowsPerPlayer)
                    {
                        // Bottom rows for human
                        boardButtons[row, col].Text = player1Symbol;
                        boardLogic[row, col] = (int)Piece.HumanPawn;
                    }
                }
            }

            // Notify UI that turn label should update
            updateTurnLabelCallback();
        }

        /// <summary>
        /// Highlights possible moves provided in the move list.
        /// </summary>
        public void HighlightAvailableMoves(List<Move> moves)
        {
            foreach (var move in moves)
            {
                boardButtons[move.YTo, move.XTo].BackColor = highlightColor;

                // If a piece is captured, highlight in red
                if (move.yeat != 0)
                    boardButtons[move.yeat, move.xeat].BackColor = Color.Tomato;
            }
        }

        /// <summary>
        /// Clears all move highlights, resetting to default dark color.
        /// </summary>
        public void ResetMoveHighlights()
        {
            for (int row = 0; row < arraySize; row++)
            {
                for (int col = 0; col < arraySize; col++)
                {
                    var button = boardButtons[row, col];
                    if (button.BackColor == highlightColor || button.BackColor == Color.Tomato)
                        button.BackColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// Opens a dialog to allow the user to choose a new highlight color.
        /// </summary>
        /// <param name="colorButton">Button whose background shows the current color.</param>
        public void PickHighlightColor(Button colorButton)
        {
            using (var colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() != DialogResult.OK) return;
                var selected = colorDialog.Color;
                if (selected == Color.Black || selected == Color.White)
                {
                    MessageBox.Show("Black or white cannot be selected as highlight color.");
                    return;
                }
                highlightColor = selected;
                colorButton.BackColor = selected;
                // Update all existing buttons' forecolor to match new highlight
                for (int y = 0; y < arraySize; y++)
                    for (int x = 0; x < arraySize; x++)
                        boardButtons[y, x].ForeColor = selected;
            }
        }

        /// <summary>
        /// Returns the current highlight color.
        /// </summary>
        public Color GetHighlightColor() => highlightColor;

        /// <summary>
        /// Adjusts size and location of all board buttons when the panel is resized.
        /// </summary>
        public void UpdateBoardLayout()
        {
            int cellWidth = panel.Width / arraySize;
            int cellHeight = panel.Height / arraySize;

            for (int row = 0; row < arraySize; row++)
            {
                for (int col = 0; col < arraySize; col++)
                {
                    var button = boardButtons[row, col];
                    button.Size = new Size(cellWidth, cellHeight);
                    button.Location = new Point(col * cellWidth, row * cellHeight);
                }
            }
        }
    }
}
