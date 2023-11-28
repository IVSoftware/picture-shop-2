namespace picture_shop
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            buttonDraw = new Button();
            trackBarTolerance = new TrackBar();
            labelTolerance = new Label();
            buttonSave = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBoxWheel = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTolerance).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWheel).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.BackgroundImage = Properties.Resources.background;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(3, 3);
            pictureBox.Name = "pictureBox";
            tableLayoutPanel1.SetRowSpan(pictureBox, 2);
            pictureBox.Size = new Size(394, 394);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // buttonDraw
            // 
            buttonDraw.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonDraw.Location = new Point(403, 403);
            buttonDraw.Name = "buttonDraw";
            buttonDraw.Size = new Size(187, 34);
            buttonDraw.TabIndex = 1;
            buttonDraw.Text = "Draw";
            buttonDraw.UseVisualStyleBackColor = true;
            buttonDraw.Visible = false;
            // 
            // trackBarTolerance
            // 
            trackBarTolerance.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trackBarTolerance.Location = new Point(3, 403);
            trackBarTolerance.Maximum = 100;
            trackBarTolerance.Name = "trackBarTolerance";
            trackBarTolerance.Size = new Size(394, 34);
            trackBarTolerance.TabIndex = 2;
            trackBarTolerance.Value = 50;
            trackBarTolerance.Visible = false;
            // 
            // labelTolerance
            // 
            labelTolerance.Anchor = AnchorStyles.None;
            labelTolerance.AutoSize = true;
            labelTolerance.Location = new Point(157, 447);
            labelTolerance.Name = "labelTolerance";
            labelTolerance.Size = new Size(85, 25);
            labelTolerance.TabIndex = 3;
            labelTolerance.Text = "Tolerance";
            labelTolerance.Visible = false;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonSave.Location = new Point(403, 443);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(187, 34);
            buttonSave.TabIndex = 1;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(buttonDraw, 1, 2);
            tableLayoutPanel1.Controls.Add(trackBarTolerance, 0, 2);
            tableLayoutPanel1.Controls.Add(labelTolerance, 0, 3);
            tableLayoutPanel1.Controls.Add(pictureBox, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonSave, 1, 3);
            tableLayoutPanel1.Controls.Add(pictureBoxWheel, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(10, 10);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(593, 480);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // pictureBoxWheel
            // 
            pictureBoxWheel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxWheel.BackColor = Color.Black;
            pictureBoxWheel.Location = new Point(403, 3);
            pictureBoxWheel.Name = "pictureBoxWheel";
            pictureBoxWheel.Padding = new Padding(2);
            pictureBoxWheel.Size = new Size(187, 194);
            pictureBoxWheel.TabIndex = 4;
            pictureBoxWheel.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(613, 500);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Padding = new Padding(10);
            Text = "PictureShop";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTolerance).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWheel).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox;
        private Button buttonDraw;
        private TrackBar trackBarTolerance;
        private Label labelTolerance;
        private Button buttonSave;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBoxWheel;
    }
}
