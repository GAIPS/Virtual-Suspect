using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualSuspect;
using VirtualSuspect.Utils;

namespace VirtualSupectQuestionAnswering
{
    public partial class QuestionAnswerForm : Form {

        KnowledgeBase virtualSuspectKb;

        public QuestionAnswerForm() {
            
            InitializeComponent();

        }

        private void QuestionAnswerForm_Load(object sender, EventArgs e) {

 

        }

        private void btLoadStory_Click(object sender, EventArgs e) {

            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "XML Files(.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                lFilePath.Text = openFileDialog1.FileName;
                
                virtualSuspectKb = KnowledgeBaseParser.parseFromFile(openFileDialog1.FileName);

                lStoryStatus.Text = "Story Loaded";    
            }

            gpAnswer.Enabled = true;
            gpQuestion.Enabled = true;

        }

        private void btAskQuestion_Click(object sender, EventArgs e) {

        }
    }
}
