

namespace AmazonBedrockWindowsApp
{
    partial class DocumentBotApp
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
            btnUpload = new Button();
            rtxtResponse = new RichTextBox();
            label1 = new Label();
            txtDocumentContent = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtQuestion = new TextBox();
            btnSend = new Button();
            lblStatus = new Label();
            label5 = new Label();
            groupBox1 = new GroupBox();
            prpGrd = new PropertyGrid();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnUpload
            // 
            btnUpload.BackColor = SystemColors.Control;
            btnUpload.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUpload.ForeColor = SystemColors.ActiveCaptionText;
            btnUpload.Location = new Point(557, 23);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(214, 37);
            btnUpload.TabIndex = 0;
            btnUpload.Text = "Upload a Document";
            btnUpload.UseVisualStyleBackColor = false;
            btnUpload.Click += btnUpload_Click;
            // 
            // rtxtResponse
            // 
            rtxtResponse.Location = new Point(44, 241);
            rtxtResponse.Name = "rtxtResponse";
            rtxtResponse.ReadOnly = true;
            rtxtResponse.Size = new Size(727, 274);
            rtxtResponse.TabIndex = 1;
            rtxtResponse.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 22);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 2;
            // 
            // txtDocumentContent
            // 
            txtDocumentContent.AllowDrop = true;
            txtDocumentContent.Location = new Point(44, 76);
            txtDocumentContent.Multiline = true;
            txtDocumentContent.Name = "txtDocumentContent";
            txtDocumentContent.ReadOnly = true;
            txtDocumentContent.ScrollBars = ScrollBars.Vertical;
            txtDocumentContent.Size = new Size(727, 144);
            txtDocumentContent.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(44, 58);
            label2.Name = "label2";
            label2.Size = new Size(114, 15);
            label2.TabIndex = 4;
            label2.Text = "Document Content";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(44, 223);
            label3.Name = "label3";
            label3.Size = new Size(158, 15);
            label3.TabIndex = 6;
            label3.Text = "Conversation with the bot..";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(44, 518);
            label4.Name = "label4";
            label4.Size = new Size(221, 15);
            label4.TabIndex = 7;
            label4.Text = "Ask a question based on the document";
            // 
            // txtQuestion
            // 
            txtQuestion.AcceptsReturn = true;
            txtQuestion.AllowDrop = true;
            txtQuestion.Location = new Point(44, 536);
            txtQuestion.Name = "txtQuestion";
            txtQuestion.ScrollBars = ScrollBars.Vertical;
            txtQuestion.Size = new Size(727, 23);
            txtQuestion.TabIndex = 8;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.Orange;
            btnSend.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSend.ForeColor = SystemColors.ButtonFace;
            btnSend.Location = new Point(651, 573);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(120, 50);
            btnSend.TabIndex = 9;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.DarkOrange;
            lblStatus.Location = new Point(42, 569);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 15);
            lblStatus.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(788, 241);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 11;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(prpGrd);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(780, 68);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(275, 491);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Settings";
            // 
            // prpGrd
            // 
            prpGrd.Location = new Point(13, 22);
            prpGrd.Name = "prpGrd";
            prpGrd.Size = new Size(247, 451);
            prpGrd.TabIndex = 0;
            // 
            // Form1
            // 
            AcceptButton = btnSend;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1064, 667);
            Controls.Add(groupBox1);
            Controls.Add(label5);
            Controls.Add(lblStatus);
            Controls.Add(btnSend);
            Controls.Add(txtQuestion);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtDocumentContent);
            Controls.Add(rtxtResponse);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnUpload);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Amazon Bedrock Document Bot";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnUpload;
        private RichTextBox rtxtResponse;
        private Label label1;
        private TextBox txtDocumentContent;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtQuestion;
        private Button btnSend;
        private Label lblStatus;
        private Label label5;
        private GroupBox groupBox1;
        private PropertyGrid prpGrd;
    }
}
