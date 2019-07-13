using System;
using System.Collections.Generic;

namespace CheckersLogic
{
    public class Player
    {
        private const int k_KingPoints = 4;
        private readonly int r_BoardSize;
        private readonly string r_Name;
        private readonly char r_TypeOfPiece;
        private int m_GameScore;
        private int m_TotalScore;
        private Piece[] m_BoardPieces;
        private List<PlayerMove> m_ValidMoves;
        private bool m_CanEat;

        internal Player(string i_Name, char i_TypeOfPiece, int i_BoardSize)
        {
            this.r_Name = i_Name;
            this.r_TypeOfPiece = i_TypeOfPiece;
            this.r_BoardSize = i_BoardSize;
            this.m_CanEat = false;
            m_TotalScore = 0;
        }

        public int TotalScore
        {
            get
            {
                return m_TotalScore;
            }

            set
            {
                m_TotalScore = value;
            }
        }

        public int GameScore
        {
            get
            {
                return m_GameScore;
            }

            set
            {
                m_GameScore = value;
            }
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public char PieceType
        {
            get
            {
                return r_TypeOfPiece;
            }
        }

        public char GetKingType()
        {
            char kingType;

            if (r_TypeOfPiece == 'X')
            {
                kingType = 'K';
            }
            else
            {
                kingType = 'U';
            }

            return kingType;
        }

        internal void InitializePieces()
        {
            int advanceRows = 0;
            int advanceColumns = 0;
            int index = 0;
            m_BoardPieces = new Piece[getNumberOfPieces(r_BoardSize)];

            // Sets the game score to be zero at the beginning of a game
            m_GameScore = 0;

            if (r_TypeOfPiece == 'X')
            {
                advanceRows = (r_BoardSize / 2) + 1;
            }

            for (int i = advanceRows; i < r_BoardSize && index < m_BoardPieces.Length; i++)
            {
                if (i % 2 == 0)
                {
                    advanceColumns = 1;
                }
                else
                {
                    advanceColumns = 0;
                }

                for (int j = advanceColumns; j < r_BoardSize && index < m_BoardPieces.Length; j += 2)
                {
                    m_BoardPieces[index] = new Piece(r_TypeOfPiece);
                    int[] position = { i, j };
                    m_BoardPieces[index].Position = position;
                    index++;
                    m_GameScore++;
                }
            }
        }

        internal List<PlayerMove> ValidMoves
        {
            get
            {
                return m_ValidMoves;
            }

            set
            {
                m_ValidMoves = value;
            }
        }

       
        internal bool CanEat
        {
            get
            {
                return m_CanEat;
            }

            set
            {
                m_CanEat = value;
            }
        }

        internal Piece[] Pieces
        {
            get
            {
                return m_BoardPieces;
            }
        }

        internal char GetOppositionPieceType()
        {
            char oppositionPieceType;
            if (this.PieceType == 'X')
            {
                oppositionPieceType = 'O';
            }
            else
            {
                oppositionPieceType = 'X';
            }

            return oppositionPieceType;
        }

        internal char GetOppositionKingType()
        {
            char oppositionKingType;

            if (this.GetKingType() == 'K')
            {
                oppositionKingType = 'U';
            }
            else
            {
                oppositionKingType = 'K';
            }

            return oppositionKingType;
        }

        internal bool MovePiece(PlayerMove i_Move)
        {
            // Checks if the move can be made
            bool v_Moved = false;

            if (m_ValidMoves.Contains(i_Move))
            {
                updatePiecePosition(i_Move);
                v_Moved = true;
            }

            return v_Moved;
        }

       
        internal void RemovePiece(PlayerMove i_Move)
        {
            for (int i = 0; i < m_BoardPieces.Length; i++)
            {
                if ((m_BoardPieces[i] != null) && (((i_Move.CurrentRow + i_Move.NextRow) / 2) == m_BoardPieces[i].Position[0]) && (((i_Move.CurrentColumn + i_Move.NextColumn) / 2) == m_BoardPieces[i].Position[1]))
                {
                    // After removing a piece updates the players score
                    if (m_BoardPieces[i].IsKing())
                    {
                        m_GameScore -= k_KingPoints;
                    }
                    else
                    {
                        m_GameScore--;
                    }

                    // A removed piece is given a null value
                    m_BoardPieces[i] = null;
                    break;
                }
            }
        }

        // The number of pieces that each player gets is the size of the row divided by 2 multiplied
        // by the number of colums divided by 2 minus 1 so that there will be two empty rows
        private static int getNumberOfPieces(int i_BoardSize)
        {
            return (i_BoardSize / 2) * ((i_BoardSize / 2) - 1);
        }

        private void updatePiecePosition(PlayerMove i_Move)
        {
            foreach (Piece piece in m_BoardPieces)
            {
                if (piece != null && i_Move.CurrentRow == piece.Position[0] && i_Move.CurrentColumn == piece.Position[1])
                {
                    piece.Position[0] = i_Move.NextRow;
                    piece.Position[1] = i_Move.NextColumn;

                    if (!piece.IsKing())
                    {
                        checkMakeKing(piece);
                    }

                    break;
                }
            }
        }

        private void checkMakeKing(Piece i_Piece)
        {
            if (i_Piece.Position[0] == 0 || i_Piece.Position[0] == r_BoardSize - 1)
            {
                i_Piece.MakeKing();

                // Adds 3 points if piece turns into a king
                m_GameScore += k_KingPoints - 1;
            }
        }
    }
}