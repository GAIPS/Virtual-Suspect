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
using VirtualSuspect.KnowledgeBase;
using VirtualSuspect.Utils;

namespace VirtualSupectQuestionAnswering
{
    public partial class SetupForm : Form
    {
        public SetupForm() {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) {

            KnowledgeBaseManager suspectKB;

            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "XML Files(.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                lFilePath.Text = openFileDialog1.FileName;

                suspectKB = KnowledgeBaseParser.parseFromFile(openFileDialog1.FileName);

                this.Hide();
                QuestionAnswerForm form = new QuestionAnswerForm(suspectKB);
                form.Closed += (s, args) => this.Close();
                form.Show();
                
            }
        }
    }
}
