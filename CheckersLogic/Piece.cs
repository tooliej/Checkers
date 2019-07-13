using System;

namespace CheckersLogic
{
    internal class Piece
    {
        private char m_TypeOfPiece;
        private int[] m_Position;
        private bool m_IsKing;

        internal Piece(char i_TypeOfPiece)
        {
            this.m_TypeOfPiece = i_TypeOfPiece;
            m_Position = new int[2];
            m_IsKing = false;
        }

        internal char GetPieceType()
        {
            return m_TypeOfPiece;
        }

        internal bool IsKing()
        {
            return m_IsKing;
        }

        internal int[] Position
        {
            get
            {
                return m_Position;
            }

            set
            {
                m_Position = value;
            }
        }

        internal void MakeKing()
        {
            m_IsKing = true;

            if (m_TypeOfPiece == 'X')
            {
                m_TypeOfPiece = 'K';
            }
            else
            {
                m_TypeOfPiece = 'U';
            }
        }
    }
}
