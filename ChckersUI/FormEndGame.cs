using System;
using System.Windows.Forms;

namespace CheckersUI
{
    internal class EndGameForm : Form
    {
        private Button m_ButtonNo;
        private Label m_LabelAnotherRound;
        private Label m_LabelGameResult;
        private PictureBox pictureBox1;
        private Button m_ButtonYes;

        internal EndGameForm()
        {
            InitializeComponent();
        }

        internal string ResultText
        {
            set
            {
                this.m_LabelGameResult.Text = value;
            }
        }

        private void InitializeComponent()
        {
            this.m_ButtonNo = new System.Windows.Forms.Button();
            this.m_ButtonYes = new System.Windows.Forms.Button();
            this.m_LabelAnotherRound = new System.Windows.Forms.Label();
            this.m_LabelGameResult = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
            this.SuspendLayout();

            // m_ButtonNo
            this.m_ButtonNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.m_ButtonNo.Location = new System.Drawing.Point(173, 120);
            this.m_ButtonNo.Name = "m_ButtonNo";
            this.m_ButtonNo.Size = new System.Drawing.Size(99, 39);
            this.m_ButtonNo.TabIndex = 0;
            this.m_ButtonNo.Text = "No";
            this.m_ButtonNo.UseVisualStyleBackColor = true;
            this.m_ButtonNo.Click += new System.EventHandler(this.button_Load);
 
            // m_ButtonYes
            this.m_ButtonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_ButtonYes.Location = new System.Drawing.Point(68, 120);
            this.m_ButtonYes.Name = "m_ButtonYes";
            this.m_ButtonYes.Size = new System.Drawing.Size(99, 39);
            this.m_ButtonYes.TabIndex = 1;
            this.m_ButtonYes.Text = "Yes";
            this.m_ButtonYes.UseVisualStyleBackColor = true;
            this.m_ButtonYes.Click += new System.EventHandler(this.button_Load);

            // m_LabelAnotherRound
            this.m_LabelAnotherRound.AutoSize = true;
            this.m_LabelAnotherRound.Location = new System.Drawing.Point(104, 68);
            this.m_LabelAnotherRound.Name = "m_LabelAnotherRound";
            this.m_LabelAnotherRound.Size = new System.Drawing.Size(113, 17);
            this.m_LabelAnotherRound.TabIndex = 2;
            this.m_LabelAnotherRound.Text = "Another Round?";

            // m_LabelGameResult
            this.m_LabelGameResult.AutoSize = true;
            this.m_LabelGameResult.Location = new System.Drawing.Point(104, 37);
            this.m_LabelGameResult.Name = "m_LabelGameResult";
            this.m_LabelGameResult.Size = new System.Drawing.Size(19, 17);
            this.m_LabelGameResult.TabIndex = 3;
            this.m_LabelGameResult.Text = "l1";

            // pictureBox1
            this.pictureBox1.Image = global::ChckersUI.Properties.Resources.Question_Mark_Logo_185;
            this.pictureBox1.Location = new System.Drawing.Point(28, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;

            // EndGameForm
            this.ClientSize = new System.Drawing.Size(284, 176);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.m_LabelGameResult);
            this.Controls.Add(this.m_LabelAnotherRound);
            this.Controls.Add(this.m_ButtonYes);
            this.Controls.Add(this.m_ButtonNo);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EndGameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damka";
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void button_Load(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
