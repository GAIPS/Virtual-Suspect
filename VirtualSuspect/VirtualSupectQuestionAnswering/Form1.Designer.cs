namespace VirtualSupectQuestionAnswering
{
    partial class QuestionAnswerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestionAnswerForm));
            this.btProcessSentence = new System.Windows.Forms.Button();
            this.tbQuestionSentence = new System.Windows.Forms.TextBox();
            this.tbQuestionStructure = new System.Windows.Forms.TextBox();
            this.gpQuestion = new System.Windows.Forms.GroupBox();
            this.btAskQuestion = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gpAnswer = new System.Windows.Forms.GroupBox();
            this.btGenerateAnswer = new System.Windows.Forms.Button();
            this.tbAnswerSentence = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAnswerStructure = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lStoryStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lFilePath = new System.Windows.Forms.Label();
            this.btLoadStory = new System.Windows.Forms.Button();
            this.gpQuestion.SuspendLayout();
            this.gpAnswer.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btProcessSentence
            // 
            this.btProcessSentence.Location = new System.Drawing.Point(158, 58);
            this.btProcessSentence.Name = "btProcessSentence";
            this.btProcessSentence.Size = new System.Drawing.Size(107, 23);
            this.btProcessSentence.TabIndex = 0;
            this.btProcessSentence.Text = "Process Sentence";
            this.btProcessSentence.UseVisualStyleBackColor = true;
            // 
            // tbQuestionSentence
            // 
            this.tbQuestionSentence.Location = new System.Drawing.Point(6, 32);
            this.tbQuestionSentence.Multiline = true;
            this.tbQuestionSentence.Name = "tbQuestionSentence";
            this.tbQuestionSentence.Size = new System.Drawing.Size(259, 20);
            this.tbQuestionSentence.TabIndex = 1;
            // 
            // tbQuestionStructure
            // 
            this.tbQuestionStructure.Location = new System.Drawing.Point(6, 90);
            this.tbQuestionStructure.Multiline = true;
            this.tbQuestionStructure.Name = "tbQuestionStructure";
            this.tbQuestionStructure.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbQuestionStructure.Size = new System.Drawing.Size(259, 148);
            this.tbQuestionStructure.TabIndex = 2;
            this.tbQuestionStructure.Text = resources.GetString("tbQuestionStructure.Text");
            // 
            // gpQuestion
            // 
            this.gpQuestion.Controls.Add(this.btAskQuestion);
            this.gpQuestion.Controls.Add(this.label2);
            this.gpQuestion.Controls.Add(this.btProcessSentence);
            this.gpQuestion.Controls.Add(this.tbQuestionStructure);
            this.gpQuestion.Controls.Add(this.label1);
            this.gpQuestion.Controls.Add(this.tbQuestionSentence);
            this.gpQuestion.Enabled = false;
            this.gpQuestion.Location = new System.Drawing.Point(12, 103);
            this.gpQuestion.Name = "gpQuestion";
            this.gpQuestion.Size = new System.Drawing.Size(271, 281);
            this.gpQuestion.TabIndex = 3;
            this.gpQuestion.TabStop = false;
            this.gpQuestion.Text = "Question";
            // 
            // btAskQuestion
            // 
            this.btAskQuestion.Location = new System.Drawing.Point(158, 244);
            this.btAskQuestion.Name = "btAskQuestion";
            this.btAskQuestion.Size = new System.Drawing.Size(107, 23);
            this.btAskQuestion.TabIndex = 4;
            this.btAskQuestion.Text = "Ask Question";
            this.btAskQuestion.UseVisualStyleBackColor = true;
            this.btAskQuestion.Click += new System.EventHandler(this.btAskQuestion_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Structure:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sentence:";
            // 
            // gpAnswer
            // 
            this.gpAnswer.Controls.Add(this.btGenerateAnswer);
            this.gpAnswer.Controls.Add(this.tbAnswerSentence);
            this.gpAnswer.Controls.Add(this.label4);
            this.gpAnswer.Controls.Add(this.label3);
            this.gpAnswer.Controls.Add(this.tbAnswerStructure);
            this.gpAnswer.Enabled = false;
            this.gpAnswer.Location = new System.Drawing.Point(289, 12);
            this.gpAnswer.Name = "gpAnswer";
            this.gpAnswer.Size = new System.Drawing.Size(274, 372);
            this.gpAnswer.TabIndex = 4;
            this.gpAnswer.TabStop = false;
            this.gpAnswer.Text = "Answer";
            // 
            // btGenerateAnswer
            // 
            this.btGenerateAnswer.Location = new System.Drawing.Point(161, 306);
            this.btGenerateAnswer.Name = "btGenerateAnswer";
            this.btGenerateAnswer.Size = new System.Drawing.Size(107, 23);
            this.btGenerateAnswer.TabIndex = 5;
            this.btGenerateAnswer.Text = "Generate Answer";
            this.btGenerateAnswer.UseVisualStyleBackColor = true;
            // 
            // tbAnswerSentence
            // 
            this.tbAnswerSentence.Location = new System.Drawing.Point(6, 346);
            this.tbAnswerSentence.Multiline = true;
            this.tbAnswerSentence.Name = "tbAnswerSentence";
            this.tbAnswerSentence.Size = new System.Drawing.Size(262, 20);
            this.tbAnswerSentence.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 330);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Sentence:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Structure:";
            // 
            // tbAnswerStructure
            // 
            this.tbAnswerStructure.Location = new System.Drawing.Point(6, 32);
            this.tbAnswerStructure.Multiline = true;
            this.tbAnswerStructure.Name = "tbAnswerStructure";
            this.tbAnswerStructure.Size = new System.Drawing.Size(262, 268);
            this.tbAnswerStructure.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lStoryStatus);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lFilePath);
            this.groupBox3.Controls.Add(this.btLoadStory);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 85);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Story";
            // 
            // lStoryStatus
            // 
            this.lStoryStatus.AutoSize = true;
            this.lStoryStatus.Location = new System.Drawing.Point(79, 55);
            this.lStoryStatus.Name = "lStoryStatus";
            this.lStoryStatus.Size = new System.Drawing.Size(63, 13);
            this.lStoryStatus.TabIndex = 3;
            this.lStoryStatus.Text = "Not Loaded";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Story Status:";
            // 
            // lFilePath
            // 
            this.lFilePath.AutoEllipsis = true;
            this.lFilePath.Location = new System.Drawing.Point(6, 24);
            this.lFilePath.Name = "lFilePath";
            this.lFilePath.Size = new System.Drawing.Size(178, 18);
            this.lFilePath.TabIndex = 1;
            this.lFilePath.Text = "No file path selected";
            // 
            // btLoadStory
            // 
            this.btLoadStory.Location = new System.Drawing.Point(190, 19);
            this.btLoadStory.Name = "btLoadStory";
            this.btLoadStory.Size = new System.Drawing.Size(75, 23);
            this.btLoadStory.TabIndex = 0;
            this.btLoadStory.Text = "Load Story";
            this.btLoadStory.UseVisualStyleBackColor = true;
            this.btLoadStory.Click += new System.EventHandler(this.btLoadStory_Click);
            // 
            // QuestionAnswerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 396);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gpAnswer);
            this.Controls.Add(this.gpQuestion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QuestionAnswerForm";
            this.Text = "Virtual Suspect - Question Answer";
            this.Load += new System.EventHandler(this.QuestionAnswerForm_Load);
            this.gpQuestion.ResumeLayout(false);
            this.gpQuestion.PerformLayout();
            this.gpAnswer.ResumeLayout(false);
            this.gpAnswer.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btProcessSentence;
        private System.Windows.Forms.TextBox tbQuestionSentence;
        private System.Windows.Forms.TextBox tbQuestionStructure;
        private System.Windows.Forms.GroupBox gpQuestion;
        private System.Windows.Forms.Button btAskQuestion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpAnswer;
        private System.Windows.Forms.Button btGenerateAnswer;
        private System.Windows.Forms.TextBox tbAnswerSentence;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAnswerStructure;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lFilePath;
        private System.Windows.Forms.Button btLoadStory;
        private System.Windows.Forms.Label lStoryStatus;
    }
}

