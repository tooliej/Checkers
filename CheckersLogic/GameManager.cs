using System;
using System.Collections.Generic;

namespace CheckersLogic
{
    public enum eOpponent
    {
        Computer,
        Human
    }

    public enum eGameState
    {
        Play,
        Lose,
        Tie,
        EndGame
    }

    public class GameManager
    {
        public event EventHandler InvalidMove;

        public event EventHandler EndOfMove;
    
        public event EventHandler EndOfGame;

        private readonly eOpponent r_Opponent;
        private readonly int r_BoardSize;
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;
        private Player m_Player1;
        private Player m_Player2;
        private Board m_Board;
        private eGameState m_GameState;

        public GameManager(string i_Player1Name, string i_Player2Name, int i_BoardSize, eOpponent i_Opponent)
        {
            r_BoardSize = i_BoardSize;
            r_Player1Name = i_Player1Name;
            r_Player2Name = i_Player2Name;
            r_Opponent = i_Opponent;
        }

        public void StartGame()
        {
            initializePlayers();
            RestartGame();
        }

        public void RestartGame()
        {
            setUpNewGame();
            m_GameState = eGameState.Play;
            if (EndOfMove != null)
            {
                EndOfMove.Invoke(this, EventArgs.Empty);
            }
        }

        public void PlayTurn(PlayerMove i_Move)
        {
            if (m_GameState.Equals(eGameState.Play))
            {
                if (!NextPlayerToMove.MovePiece(i_Move))
                {
                    if(InvalidMove != null)
                    {
                        InvalidMove.Invoke(this, EventArgs.Empty);
                    }   
                }
                else
                {
                    if (NextPlayerToMove.CanEat)
                    {
                        removeEatenPiece(i_Move);
                        if (!hasEaten(i_Move))
                        {
                            continueToNextMove();
                        }
                        else
                        {
                            if (EndOfMove != null)
                            {
                                EndOfMove.Invoke(this, EventArgs.Empty);
                            }
                        }
                    }
                    else
                    {
                        continueToNextMove();
                    }

                    m_GameState = GameState();

                    if (m_GameState.Equals(eGameState.Play))
                    {
                        if (r_Opponent.Equals(eOpponent.Computer) && NextPlayerToMove.PieceType == 'O')
                        {
                            // If it is the computers turn, randomly chooses a move from the valid moves
                            PlayTurn(getComputersMove());
                        }
                    }
                }
            }

            if (m_GameState.Equals(eGameState.Tie))
            {
                exitGame();
            }

            if (m_GameState.Equals(eGameState.Lose))
            {
                Player winner = getOppositionPlayer();
                Player loser = NextPlayerToMove;
                winner.TotalScore += winner.GameScore - loser.GameScore;
                exitGame();
            }
        }

        public char[][] GetPieceSetup()
        {
            return m_Board.Pieces;
        }

        public Player NextPlayerToMove
        {
            get
            {
                return m_Board.NextPlayerToMove;
            }
        }

        public int Player1Score
        {
            get
            {
                return m_Player1.TotalScore;
            }
        }

        public int Player2Score
        {
            get
            {
                return m_Player2.TotalScore;
            }
        }

        public eGameState GameState()
        {
            eGameState state = eGameState.Play;

            // An empty valid move string either means the player has no more pieces or doesnt have a valid move
            if (NextPlayerToMove.ValidMoves.Count == 0)
            {
                m_Board.SetValidMoves(getOppositionPlayer());

                if (getOppositionPlayer().ValidMoves.Count == 0)
                {
                    state = eGameState.Tie;
                }
                else
                {
                    state = eGameState.Lose;
                }
            }

            return state;
        }

        private void exitGame()
        {
            if (InvalidMove != null)
            {
                EndOfGame.Invoke(getOppositionPlayer(), EventArgs.Empty);
            }
        }

        private void continueToNextMove()
        {
            switchPlayerToMove();
            m_Board.PieceSetup();
            m_Board.SetValidMoves(NextPlayerToMove);
            if (EndOfMove != null)
            {
                EndOfMove.Invoke(this, EventArgs.Empty);
            }
        }

        private bool hasEaten(PlayerMove i_Move)
        {
            List<PlayerMove> validMoves = new List<PlayerMove>();
            Piece currentPiece = findPiece(i_Move);

            resetCanEat(NextPlayerToMove);
            m_Board.PieceSetup();
            m_Board.GetLegalMoves(currentPiece, validMoves);

            if (NextPlayerToMove.CanEat)
            {
                NextPlayerToMove.ValidMoves = validMoves;
            }

            return NextPlayerToMove.CanEat;
        }

        private Player getOppositionPlayer()
        {
            Player opposition;
            if (this.NextPlayerToMove == m_Player1)
            {
                opposition = m_Player2;
            }
            else
            {
                opposition = m_Player1;
            }

            return opposition;
        }

        private PlayerMove getComputersMove()
        {
            List<PlayerMove> validMoves = NextPlayerToMove.ValidMoves;

            Random random = new Random();
            int index = random.Next(validMoves.Count);

            return validMoves[index];
        }

        private void removeEatenPiece(PlayerMove i_Move)
        {
            if (NextPlayerToMove == m_Player1)
            {
                m_Player2.RemovePiece(i_Move);
            }
            else
            {
                m_Player1.RemovePiece(i_Move);
            }
        }

        private void switchPlayerToMove()
        {
            if (NextPlayerToMove == m_Player1)
            {
                m_Board.NextPlayerToMove = m_Player2;
            }
            else
            {
                m_Board.NextPlayerToMove = m_Player1;
            }

            resetCanEat(NextPlayerToMove);
        }

        private void resetCanEat(Player i_Player)
        {
            i_Player.CanEat = false;
        }

        private Piece findPiece(PlayerMove i_Move)
        {
            Piece piece = null;
            foreach (Piece current in NextPlayerToMove.Pieces)
            {
                if (current != null && current.Position[0] == i_Move.NextRow && current.Position[1] == i_Move.NextColumn)
                {
                    piece = current;
                    break;
                }
            }

            return piece;
        }

        private void setUpNewGame()
        {
            m_Player1.InitializePieces();
            m_Player2.InitializePieces();
            resetCanEat(m_Player1);
            resetCanEat(m_Player2);

            // Sets player1 to be the first player to move 
            m_Board = new Board(m_Player1, m_Player2, r_BoardSize, m_Player1);
            m_Board.SetValidMoves(m_Player1);
        }

        private void initializePlayers()
        {
            m_Player1 = new Player(r_Player1Name, 'X', r_BoardSize);
            if (r_Opponent.Equals(eOpponent.Human))
            {
                m_Player2 = new Player(r_Player2Name, 'O', r_BoardSize);
            }
            else
            {
                m_Player2 = new Player("Computer", 'O', r_BoardSize);
            }
        }
    }
}
