namespace CypherSenseBotPart2
{
    partial class Form1
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
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            textBox1 = new TextBox();
            sendButton = new Button();
            refreshButton = new Button();
            deleteButton = new Button();
            actLButton = new Button();
            dataGridView1 = new DataGridView();
            chatlbl = new Label();
            tasklbl = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.DarkCyan;
            label1.Font = new Font("Elephant", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(426, 9);
            label1.Name = "label1";
            label1.Size = new Size(259, 27);
            label1.TabIndex = 0;
            label1.Text = "CYPHERSENSEBOT";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.ControlText;
            richTextBox1.Location = new Point(12, 81);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(558, 376);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ActiveCaptionText;
            textBox1.ForeColor = SystemColors.Control;
            textBox1.Location = new Point(12, 466);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(244, 23);
            textBox1.TabIndex = 2;
            // 
            // sendButton
            // 
            sendButton.BackColor = Color.DarkRed;
            sendButton.ForeColor = Color.Black;
            sendButton.Location = new Point(305, 465);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(75, 23);
            sendButton.TabIndex = 3;
            sendButton.Text = "SEND";
            sendButton.UseVisualStyleBackColor = false;
            sendButton.Click += sendButton_Click;
            // 
            // refreshButton
            // 
            refreshButton.Location = new Point(783, 484);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(120, 23);
            refreshButton.TabIndex = 4;
            refreshButton.Text = "REFRESH TASKS";
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshTasksButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(920, 484);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(93, 23);
            deleteButton.TabIndex = 6;
            deleteButton.Text = "DELETE TASKS";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteTaskButton_Click;
            // 
            // actLButton
            // 
            actLButton.Location = new Point(637, 484);
            actLButton.Name = "actLButton";
            actLButton.Size = new Size(128, 23);
            actLButton.TabIndex = 7;
            actLButton.Text = "ACTIVITY LOG";
            actLButton.UseVisualStyleBackColor = true;
            actLButton.Click += activityLogButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(620, 95);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(409, 372);
            dataGridView1.TabIndex = 8;
            // 
            // chatlbl
            // 
            chatlbl.AutoSize = true;
            chatlbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chatlbl.Location = new Point(83, 47);
            chatlbl.Name = "chatlbl";
            chatlbl.Size = new Size(150, 21);
            chatlbl.TabIndex = 9;
            chatlbl.Text = "Chat Conversation";
            // 
            // tasklbl
            // 
            tasklbl.AutoSize = true;
            tasklbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tasklbl.Location = new Point(796, 66);
            tasklbl.Name = "tasklbl";
            tasklbl.Size = new Size(50, 21);
            tasklbl.TabIndex = 10;
            tasklbl.Text = "Tasks";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1041, 528);
            Controls.Add(tasklbl);
            Controls.Add(chatlbl);
            Controls.Add(dataGridView1);
            Controls.Add(actLButton);
            Controls.Add(deleteButton);
            Controls.Add(refreshButton);
            Controls.Add(sendButton);
            Controls.Add(textBox1);
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private RichTextBox richTextBox1;
        private TextBox textBox1;
        private Button sendButton;
        private Button refreshButton;
        private Button deleteButton;
        private Button actLButton;
        private DataGridView dataGridView1;
        private Label chatlbl;
        private Label tasklbl;
    }
}
