using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CheckersLogic;

namespace CheckersUI
{
    internal class FormMenu : Form
    {
        private RadioButton m_RadioButtonSix;
        private RadioButton m_RadioButtonEight;
        private RadioButton m_RadioButtonTen;
        private Label m_LabelPlayers;
        private Label m_LabelPlayer1;
        private TextBox m_TextBoxPlayer2Name;
        private CheckBox m_CheckBoxPlayer2;
        private TextBox m_TextBoxPlayer1Name;
        private Button m_ButtonDone;
        private Label m_LabelBoardSize;
        private eOpponent m_Opponent = eOpponent.Computer;

        internal FormMenu()
        {
            InitializeComponent();
        }

        internal eOpponent Opponent
        {
            get
            {
                return m_Opponent;
            }
        }

        internal string Player1Name
        {
            get
            {
                return m_TextBoxPlayer1Name.Text;
            }
        }

        internal string Player2Name
        {
            get
            {
                return m_TextBoxPlayer2Name.Text;
            }
        }

        internal int BoardSize()
        {
            int boardSize = 6;

            if (m_RadioButtonEight.Checked)
            {
                boardSize = 8;
            }
            else if (m_RadioButtonTen.Checked)
            {
                boardSize = 10;
            }

            return boardSize;
        }

        private void InitializeComponent()
        {
            this.m_LabelBoardSize = new System.Windows.Forms.Label();
            this.m_RadioButtonSix = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonEight = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonTen = new System.Windows.Forms.RadioButton();
            this.m_LabelPlayers = new System.Windows.Forms.Label();
            this.m_LabelPlayer1 = new System.Windows.Forms.Label();
            this.m_TextBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.m_CheckBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.m_TextBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.m_ButtonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // m_LabelBoardSize
            this.m_LabelBoardSize.AutoSize = true;
            this.m_LabelBoardSize.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
            this.m_LabelBoardSize.Location = new System.Drawing.Point(22, 19);
            this.m_LabelBoardSize.Name = "m_LabelBoardSize";
            this.m_LabelBoardSize.Size = new System.Drawing.Size(84, 17);
            this.m_LabelBoardSize.TabIndex = 0;
            this.m_LabelBoardSize.Text = "Board Size:";

            // m_RadioButtonSix
            this.m_RadioButtonSix.AutoSize = true;
            this.m_RadioButtonSix.Location = new System.Drawing.Point(39, 49);
            this.m_RadioButtonSix.Name = "m_RadioButtonSix";
            this.m_RadioButtonSix.Size = new System.Drawing.Size(57, 21);
            this.m_RadioButtonSix.TabIndex = 1;
            this.m_RadioButtonSix.TabStop = true;
            this.m_RadioButtonSix.Text = "6 x 6";
            this.m_RadioButtonSix.UseVisualStyleBackColor = true;
   
            // m_RadioButtonEight
            this.m_RadioButtonEight.AutoSize = true;
            this.m_RadioButtonEight.Location = new System.Drawing.Point(117, 49);
            this.m_RadioButtonEight.Name = "m_RadioButtonEight";
            this.m_RadioButtonEight.Size = new System.Drawing.Size(57, 21);
            this.m_RadioButtonEight.TabIndex = 2;
            this.m_RadioButtonEight.TabStop = true;
            this.m_RadioButtonEight.Text = "8 x 8";
            this.m_RadioButtonEight.UseVisualStyleBackColor = true;
  
            // m_RadioButtonTen
            this.m_RadioButtonTen.AutoSize = true;
            this.m_RadioButtonTen.Location = new System.Drawing.Point(193, 49);
            this.m_RadioButtonTen.Name = "m_RadioButtonTen";
            this.m_RadioButtonTen.Size = new System.Drawing.Size(73, 21);
            this.m_RadioButtonTen.TabIndex = 3;
            this.m_RadioButtonTen.TabStop = true;
            this.m_RadioButtonTen.Text = "10 x 10";
            this.m_RadioButtonTen.UseVisualStyleBackColor = true;
         
            // m_LabelPlayers
            this.m_LabelPlayers.AutoSize = true;
            this.m_LabelPlayers.Location = new System.Drawing.Point(22, 83);
            this.m_LabelPlayers.Name = "m_LabelPlayers";
            this.m_LabelPlayers.Size = new System.Drawing.Size(61, 17);
            this.m_LabelPlayers.TabIndex = 4;
            this.m_LabelPlayers.Text = "Players:";
     
            // m_LabelPlayer1
            this.m_LabelPlayer1.AutoSize = true;
            this.m_LabelPlayer1.Location = new System.Drawing.Point(36, 122);
            this.m_LabelPlayer1.Name = "m_LabelPlayer1";
            this.m_LabelPlayer1.Size = new System.Drawing.Size(65, 17);
            this.m_LabelPlayer1.TabIndex = 5;
            this.m_LabelPlayer1.Text = "Player 1:";
            
            // m_TextBoxPlayer2Name
            this.m_TextBoxPlayer2Name.Enabled = false;
            this.m_TextBoxPlayer2Name.Location = new System.Drawing.Point(129, 157);
            this.m_TextBoxPlayer2Name.Name = "m_TextBoxPlayer2Name";
            this.m_TextBoxPlayer2Name.Size = new System.Drawing.Size(137, 25);
            this.m_TextBoxPlayer2Name.TabIndex = 6;
            this.m_TextBoxPlayer2Name.Text = "[Computer]";

            // m_CheckBoxPlayer2
            this.m_CheckBoxPlayer2.AutoSize = true;
            this.m_CheckBoxPlayer2.Location = new System.Drawing.Point(39, 159);
            this.m_CheckBoxPlayer2.Name = "m_CheckBoxPlayer2";
            this.m_CheckBoxPlayer2.Size = new System.Drawing.Size(84, 21);
            this.m_CheckBoxPlayer2.TabIndex = 7;
            this.m_CheckBoxPlayer2.Text = "Player 2:";
            this.m_CheckBoxPlayer2.UseVisualStyleBackColor = true;
            this.m_CheckBoxPlayer2.Click += new System.EventHandler(this.checkBoxPlayer2_Click);

            // m_TextBoxPlayer1Name
            this.m_TextBoxPlayer1Name.Location = new System.Drawing.Point(129, 119);
            this.m_TextBoxPlayer1Name.Name = "m_TextBoxPlayer1Name";
            this.m_TextBoxPlayer1Name.Size = new System.Drawing.Size(137, 25);
            this.m_TextBoxPlayer1Name.TabIndex = 8;
 
            // m_ButtonDone
            this.m_ButtonDone.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonDone.Location = new System.Drawing.Point(179, 210);
            this.m_ButtonDone.Name = "m_ButtonDone";
            this.m_ButtonDone.Size = new System.Drawing.Size(87, 27);
            this.m_ButtonDone.TabIndex = 9;
            this.m_ButtonDone.Text = "Done";
            this.m_ButtonDone.UseVisualStyleBackColor = false;
            this.m_ButtonDone.Click += new System.EventHandler(this.buttonDone_Click);
            
            // FormMenu
            this.ClientSize = new System.Drawing.Size(299, 265);
            this.Controls.Add(this.m_ButtonDone);
            this.Controls.Add(this.m_TextBoxPlayer1Name);
            this.Controls.Add(this.m_CheckBoxPlayer2);
            this.Controls.Add(this.m_TextBoxPlayer2Name);
            this.Controls.Add(this.m_LabelPlayer1);
            this.Controls.Add(this.m_LabelPlayers);
            this.Controls.Add(this.m_LabelBoardSize);
            this.Controls.Add(this.m_RadioButtonSix);
            this.Controls.Add(this.m_RadioButtonEight);
            this.Controls.Add(this.m_RadioButtonTen);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void checkBoxPlayer2_Click(object sender, EventArgs e)
        {
            if (m_CheckBoxPlayer2.Checked)
            {
                m_TextBoxPlayer2Name.Enabled = true;
                m_TextBoxPlayer2Name.Text = string.Empty;
                m_Opponent = eOpponent.Human;
            }
            else
            {
                m_TextBoxPlayer2Name.Enabled = false;
                m_TextBoxPlayer2Name.Text = "[Computer]";
                m_Opponent = eOpponent.Computer;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
