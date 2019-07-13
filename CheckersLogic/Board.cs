using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    internal class Board
    {
        private const int k_MoveUp = 1;
        private const int k_MoveDown = -1;
        private readonly int r_SizeOfBoard;
        private Player m_Player1;
        private Player m_Player2;
        private Player m_NextPlayerToMove;
        private char[][] m_Pieces;

        internal Board(Player i_Player1, Player i_Player2, int i_SizeOfBoard, Player i_NextPlayerToMove)
        {
            this.m_Player1 = i_Player1;
            this.m_Player2 = i_Player2;
            r_SizeOfBoard = i_SizeOfBoard;
            this.m_NextPlayerToMove = i_NextPlayerToMove;
            PieceSetup();
        }

        internal Player NextPlayerToMove
        {
            get
            {
                return m_NextPlayerToMove;
            }

            set
            {
                m_NextPlayerToMove = value;
            }
        }

        // Sets the the current player valid moves field with all the legal moves
        internal void SetValidMoves(Player i_Player)
        {
            List<PlayerMove> legalMoves = new List<PlayerMove>();

            foreach (Piece piece in i_Player.Pieces)
            {
                // Checks that the piece is on the board
                if (piece != null)
                {
                    GetLegalMoves(piece, legalMoves);
                }
            }

            i_Player.ValidMoves = legalMoves;
        }

        // Sets the position of the pieces in a char array before printing the board
        internal void PieceSetup()
        {
            m_Pieces = new char[r_SizeOfBoard][];
            for (char c = (char)0; c < r_SizeOfBoard; c++)
            {
                m_Pieces[c] = new char[r_SizeOfBoard];
                for (char j = (char)0; j < r_SizeOfBoard; j++)
                {
                    m_Pieces[c][j] = ' ';
                }
            }

            Piece[] player1Pieces = m_Player1.Pieces;
            Piece[] player2Pieces = m_Player2.Pieces;

            for (int i = 0; i < player1Pieces.Length; i++)
            {
                if (player1Pieces[i] != null)
                {
                    int[] positionsPlayer1 = player1Pieces[i].Position;
                    m_Pieces[positionsPlayer1[0]][positionsPlayer1[1]] = player1Pieces[i].GetPieceType();
                }

                if (player2Pieces[i] != null)
                {
                    int[] positionsPlayer2 = player2Pieces[i].Position;
                    m_Pieces[positionsPlayer2[0]][positionsPlayer2[1]] = player2Pieces[i].GetPieceType();
                }
            }
        }

        internal char[][] Pieces
        {
            get
            {
                return m_Pieces;
            }
        }

        internal void GetLegalMoves(Piece i_Piece, List<PlayerMove> o_LegalMoves)
        {
            if (i_Piece.IsKing())
            {
                checkSurroundingOptions(o_LegalMoves, i_Piece, k_MoveUp);
                checkSurroundingOptions(o_LegalMoves, i_Piece, k_MoveDown);
            }
            else
            {
                if (m_NextPlayerToMove.PieceType == 'X')
                {
                    checkSurroundingOptions(o_LegalMoves, i_Piece, k_MoveUp);
                }
                else
                {
                    // Setting k_MoveDown to -1 will give the mirror image for all of player X's valid moves which is player O's valid moves 
                    checkSurroundingOptions(o_LegalMoves, i_Piece, k_MoveDown);
                }
            }
        }

        private void checkSurroundingOptions(List<PlayerMove> o_LegalMoves, Piece i_Piece, int i_Direction)
        {
            // Checks move to the left for X or right for O
            if (!checkOutOfBounds(i_Piece.Position, -1 * i_Direction, -1 * i_Direction))
            {
                // EATING
                // If there is an opposition piece next to the cuurrent piece checks if can eat
                if (m_Pieces[i_Piece.Position[0] + (-1 * i_Direction)][i_Piece.Position[1] + (-1 * i_Direction)] == m_NextPlayerToMove.GetOppositionPieceType() ||
                    m_Pieces[i_Piece.Position[0] + (-1 * i_Direction)][i_Piece.Position[1] + (-1 * i_Direction)] == m_NextPlayerToMove.GetOppositionKingType())
                {
                    if (!checkOutOfBounds(i_Piece.Position, -2 * i_Direction, -2 * i_Direction))
                    {
                        if (m_Pieces[i_Piece.Position[0] + (-2 * i_Direction)][i_Piece.Position[1] + (-2 * i_Direction)] == ' ')
                        {
                            if (!m_NextPlayerToMove.CanEat)
                            {
                                // If this is the first option to eat a piece, make this the only legal move
                                if (o_LegalMoves.Count > 0)
                                {
                                    o_LegalMoves.Clear();
                                }

                                m_NextPlayerToMove.CanEat = true;
                            }

                            o_LegalMoves.Add(new PlayerMove(i_Piece.Position[0], i_Piece.Position[1], i_Piece.Position[0] + (-2 * i_Direction), i_Piece.Position[1] + (-2 * i_Direction)));
                        }
                    }
                }

                if (!m_NextPlayerToMove.CanEat)
                {
                    // If there is no option to eat
                    if (m_Pieces[i_Piece.Position[0] + (-1 * i_Direction)][i_Piece.Position[1] + (-1 * i_Direction)] == ' ')
                    {
                        o_LegalMoves.Add(new PlayerMove(i_Piece.Position[0], i_Piece.Position[1], i_Piece.Position[0] + (-1 * i_Direction), i_Piece.Position[1] + (-1 * i_Direction)));
                    }
                }
            }

            // Checks move to the right for X or left for O
            if (!checkOutOfBounds(i_Piece.Position, -1 * i_Direction, 1 * i_Direction))
            {
                // Eating. If there is an opposition piece next to the cuurrent piece checks if can eat
                if (m_Pieces[i_Piece.Position[0] + (-1 * i_Direction)][i_Piece.Position[1] + (1 * i_Direction)] == m_NextPlayerToMove.GetOppositionPieceType() ||
                    (m_Pieces[i_Piece.Position[0] + (-1 * i_Direction)][i_Piece.Position[1] + (1 * i_Direction)] == m_NextPlayerToMove.GetOppositionKingType()))
                {
                    if (!checkOutOfBounds(i_Piece.Position, -2 * i_Direction, 2 * i_Direction))
                    {
                        if (m_Pieces[i_Piece.Position[0] + (-2 * i_Direction)][i_Piece.Position[1] + (2 * i_Direction)] == ' ')
                        {
                            if (!m_NextPlayerToMove.CanEat)
                            {
                                // If this is the first option to eat a piece, make this the only legal move
                                if (o_LegalMoves.Count > 0)
                                {
                                    o_LegalMoves.Clear();
                                }

                                m_NextPlayerToMove.CanEat = true;
                            }

                            o_LegalMoves.Add(new PlayerMove(i_Piece.Position[0], i_Piece.Position[1], i_Piece.Position[0] + (-2 * i_Direction), i_Piece.Position[1] + (2 * i_Direction)));
                        }
                    }
                }

                if (!m_NextPlayerToMove.CanEat)
                {
                    // If there is no option to eat
                    if (m_Pieces[i_Piece.Position[0] + (-1 * i_Direction)][i_Piece.Position[1] + (1 * i_Direction)] == ' ')
                    {
                        o_LegalMoves.Add(new PlayerMove(i_Piece.Position[0], i_Piece.Position[1], i_Piece.Position[0] + (-1 * i_Direction), i_Piece.Position[1] + (1 * i_Direction)));
                    }
                }
            }
        }

        private bool checkOutOfBounds(int[] i_Position, int i_RowsChange, int i_ColumnsChange)
        {
            return i_Position[0] + i_RowsChange < 0 || i_Position[1] + i_ColumnsChange < 0 || i_Position[0] + i_RowsChange >= r_SizeOfBoard || i_Position[1] + i_ColumnsChange >= r_SizeOfBoard;
        }
    }
}
