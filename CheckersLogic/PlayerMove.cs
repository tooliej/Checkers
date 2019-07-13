using System;

namespace CheckersLogic
{
    public class PlayerMove
    {
        private int m_CurrentRow;
        private int m_CurrentColumn;
        private int m_NextRow;
        private int m_NextColumn;

        public PlayerMove()
        {
        }

        public PlayerMove(int i_CurrentRow, int i_CurrentColumn, int i_NextRow, int i_NextColumn)
        {
            m_CurrentRow = i_CurrentRow;
            m_CurrentColumn = i_CurrentColumn;
            m_NextRow = i_NextRow;
            m_NextColumn = i_NextColumn;
        }

        public int CurrentRow
        {
            get
            {
                return m_CurrentRow;
            }

            set
            {
                m_CurrentRow = value;
            }
        }

        public int CurrentColumn
        {
            get
            {
                return m_CurrentColumn;
            }
        
            set
            {
                m_CurrentColumn = value;
            }
        }

        public int NextRow
        {
            get
            {
                return m_NextRow;
            }

            set
            {
                m_NextRow = value;
            }
        }

        public int NextColumn
        {
            get
            {
                return m_NextColumn;
            }

            set
            {
                m_NextColumn = value;
            }
        }

        public override bool Equals(object obj)
        {
            bool v_PlayersMoveEquals = true;
            PlayerMove playerMove = obj as PlayerMove;
            if (playerMove != null)
            {
                v_PlayersMoveEquals = v_PlayersMoveEquals && (playerMove.m_CurrentColumn == this.CurrentColumn) && (playerMove.m_CurrentRow == this.CurrentRow) && (playerMove.m_NextRow == this.NextRow) && (playerMove.m_NextColumn == this.NextColumn);
            }
            else
            {
                v_PlayersMoveEquals = false;
            }

            return v_PlayersMoveEquals;
        }
    }
}
