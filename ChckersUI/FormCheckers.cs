using System;
using System.Windows.Forms;
using System.Drawing;
using CheckersLogic;

namespace CheckersUI
{
    internal class FormCheckers : Form
    {
        private const int k_ButtonSize = 50;
        private const int k_PieceSize = k_ButtonSize - 3;
        private const int k_HeaderSize = 50;
        private FormMenu m_MainMenu;
        private Label m_LabelPlayer1;
        private Label m_LabelPlayer2;
        private Label m_LabelScorePlayer1;
        private Label m_LabelScorePlayer2;
        private Button[][] m_Board;
        private int m_BoardSize;
        private EndGameForm m_EndGame;
        private GameManager m_CheckersGame;
        private PlayerMove m_Move;
        private bool m_ChosePiece;

        internal FormCheckers()
        {
            m_MainMenu = new FormMenu();
            checkMenuResults();
            m_Move = new PlayerMove();
        }

        internal void StartGame()
        {
            m_CheckersGame = new GameManager(m_MainMenu.Player1Name, m_MainMenu.Player2Name, m_BoardSize, m_MainMenu.Opponent);
            registerLogicalEvents();
            m_CheckersGame.StartGame();
            setPlayersScores();
            this.ShowDialog();
        }
      
        private void checkMenuResults()
        {
            if (m_MainMenu.ShowDialog() == DialogResult.OK)
            {
                if (validMenuDetails())
                {
                    initializeComponent();
                }
                else
                {
                    MessageBox.Show("Invalid Entry!" + Environment.NewLine + "Please enter a valid name", "Damka", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    checkMenuResults();
                }
            }
            else
            {
                this.Close();
            }
        }

        private bool validMenuDetails()
        {
            return !m_MainMenu.Player1Name.Equals(string.Empty) && !m_MainMenu.Player2Name.Equals(string.Empty);
        }

        private void registerLogicalEvents()
        {
            m_CheckersGame.InvalidMove += checkersGame_InvalidMove;
            m_CheckersGame.EndOfMove += checkersGame_EndOfMove;
            m_CheckersGame.EndOfGame += checkersGame_EndOfGame;
        }

        private void initializeComponent()
        {
            m_BoardSize = m_MainMenu.BoardSize();
            this.m_LabelPlayer1 = new Label();
            this.m_LabelPlayer2 = new Label();
            this.m_LabelScorePlayer1 = new Label();
            this.m_LabelScorePlayer2 = new Label();
            this.m_Board = new Button[m_BoardSize][];

            // Build Board buttons
            int buttonTag = 0;
            for(int i = 0; i < m_BoardSize; i++)
            {
                m_Board[i] = new Button[m_BoardSize];
                for(int j = 0; j < m_BoardSize; j++)
                {
                    if(i % 2 == 0)
                    {
                        m_Board[i][j] = createGreyPiece(new Point((j * k_PieceSize) + 4, (i * k_PieceSize) + k_HeaderSize), buttonTag);
                        j++;
                        buttonTag++;
                        m_Board[i][j] = createWhitePiece(new Point((j * k_PieceSize) + 4, (i * k_PieceSize) + k_HeaderSize), buttonTag);
                    }
                    else
                    {
                        m_Board[i][j] = createWhitePiece(new Point((j * k_PieceSize) + 4, (i * k_PieceSize) + k_HeaderSize), buttonTag);
                        j++;
                        buttonTag++;
                        m_Board[i][j] = createGreyPiece(new Point((j * k_PieceSize) + 4, (i * k_PieceSize) + k_HeaderSize), buttonTag);
                    }

                    buttonTag++;
                }
            }

            // FormCheckers
            this.AutoSize = false;
            this.BackColor = SystemColors.Window;
            this.ClientSize = new Size((m_BoardSize * k_PieceSize) + 10, (m_BoardSize * k_PieceSize) + k_HeaderSize + 6);
            this.Controls.Add(this.m_LabelScorePlayer2);
            this.Controls.Add(this.m_LabelScorePlayer1);
            this.Controls.Add(this.m_LabelPlayer2);
            this.Controls.Add(this.m_LabelPlayer1);
            this.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Damka";

            // m_LabelPlayer1
            this.m_LabelPlayer1.Text = m_MainMenu.Player1Name + ":";
            this.m_LabelPlayer1.AutoSize = true;
            this.m_LabelPlayer1.Location = new Point((this.Width / 8) - (m_LabelPlayer1.Width / 8), 19);

            // m_LabelPlayer2
            this.m_LabelPlayer2.AutoSize = true;
            if (m_MainMenu.Opponent == eOpponent.Computer)
            {
                this.m_LabelPlayer2.Text = "Computer:";
            }
            else
            {
                this.m_LabelPlayer2.Text = m_MainMenu.Player2Name + ":";
            }

            this.m_LabelPlayer2.Location = new Point(((this.Width / 4) * 3) - ((m_LabelPlayer2.Width / 4) * 3), 19);

            // m_LabelScorePlayer1
            this.m_LabelScorePlayer1.AutoSize = true;
            this.m_LabelScorePlayer1.Location = new Point(m_LabelPlayer1.Location.X + m_LabelPlayer1.Width, 19);
           
            // m_LabelScorePlayer2
            this.m_LabelScorePlayer2.AutoSize = true;
            this.m_LabelScorePlayer2.Location = new Point(m_LabelPlayer2.Location.X + m_LabelPlayer2.Width, 19);          
        }

        private Button createWhitePiece(Point i_Point, int i_ButtonTag)
        {
            Button whiteButton = new Button();
            whiteButton.Tag = i_ButtonTag;
            whiteButton.BackColor = SystemColors.ButtonHighlight;
            whiteButton.Location = i_Point;
            whiteButton.Size = new Size(k_ButtonSize, k_ButtonSize);
            whiteButton.UseVisualStyleBackColor = false;
            whiteButton.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.Controls.Add(whiteButton);
            whiteButton.Click += new EventHandler(this.button_Click);

            return whiteButton;
        }

        private Button createGreyPiece(Point i_Point, int i_ButtonTag)
        {
            Button greyButton = new Button();
            greyButton.Tag = i_ButtonTag;
            greyButton.BackColor = SystemColors.ControlDarkDark;
            greyButton.Enabled = false;
            greyButton.Location = i_Point;
            greyButton.Size = new Size(k_ButtonSize, k_ButtonSize);
            greyButton.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            greyButton.UseVisualStyleBackColor = false;
            this.Controls.Add(greyButton);

            return greyButton;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if (!m_ChosePiece)
            {
                if (pressedButton.Text.Equals(m_CheckersGame.NextPlayerToMove.PieceType.ToString()) || pressedButton.Text.Equals(m_CheckersGame.NextPlayerToMove.GetKingType().ToString()))
                {
                    m_Move.CurrentRow = (int)pressedButton.Tag / m_BoardSize;
                    m_Move.CurrentColumn = (int)pressedButton.Tag % m_BoardSize;
                    pressedButton.BackColor = SystemColors.ActiveCaption;
                    m_ChosePiece = true;
                }
            }
            else
            {
                if ((int)pressedButton.Tag / m_BoardSize != m_Move.CurrentRow || (int)pressedButton.Tag % m_BoardSize != m_Move.CurrentColumn)
                {
                    m_Move.NextRow = (int)pressedButton.Tag / m_BoardSize;
                    m_Move.NextColumn = (int)pressedButton.Tag % m_BoardSize;
                    m_ChosePiece = false;
                    m_Board[m_Move.CurrentRow][m_Move.CurrentColumn].BackColor = SystemColors.ButtonHighlight;
                    m_CheckersGame.PlayTurn(m_Move);
                }
                else
                {
                    m_ChosePiece = false;
                    m_Board[m_Move.CurrentRow][m_Move.CurrentColumn].BackColor = SystemColors.ButtonHighlight;
                }
            }
        }

        private void checkersGame_InvalidMove(object sender, EventArgs e)
        {
            MessageBox.Show("Invalid Move!" + Environment.NewLine + "Please choose another move", "Damka", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void checkersGame_EndOfMove(object sender, EventArgs e)
        {
            setPiecesOnButtons(m_CheckersGame.GetPieceSetup());
        }

        private void checkersGame_EndOfGame(object sender, EventArgs e)
        {
            if (m_EndGame == null)
            {
                m_EndGame = new EndGameForm();
            }

            if (m_CheckersGame.GameState() == eGameState.Tie)
            {
                m_EndGame.ResultText = "Tie!";
            }
            else
            {
                Player winner = sender as Player;
                m_EndGame.ResultText = winner.Name + " Won!";
            }

            if (m_EndGame.ShowDialog() == DialogResult.Yes)
            {
                setPlayersScores();
                m_CheckersGame.RestartGame();
            }
            else
            {
                this.Close();
            }
        }

            private void setPiecesOnButtons(char[][] i_Pieces)
        {
            for(int i = 0; i < m_BoardSize; i++)
            {
                for(int j = 0; j < m_BoardSize; j++)
                {
                    m_Board[i][j].Text = i_Pieces[i][j].ToString();
                }
            }
        }
        
        private void setPlayersScores()
        {
            m_LabelScorePlayer1.Text = m_CheckersGame.Player1Score.ToString();
            m_LabelScorePlayer2.Text = m_CheckersGame.Player2Score.ToString();
        }
    }
}