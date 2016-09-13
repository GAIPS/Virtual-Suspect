using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using TestEnvironment.CustomItems;
using VirtualSuspect.Query;
using VirtualSuspectNaturalLanguage;

namespace TestEnvironment
{
    /// <summary>
    /// Interaction logic for DialogueWindow.xaml
    /// </summary>
    public partial class DialogueWindow : Window {

        private int id;
        private TestSuspect testSuspect;

        private NotesWindow notesWindow;

        private Logger loggerManager;

        public DialogueWindow(int id, TestSuspect testSuspect) {

            this.id = id;
            this.testSuspect = testSuspect;

            InitializeComponent();

            loggerManager = new Logger(logger);
            //Update UI
            BitmapImage avatar = new BitmapImage();
            avatar.BeginInit();
            avatar.UriSource = new Uri(testSuspect.ProfileImagePath);
            avatar.EndInit();
            imageProfilePicture.Source = avatar;

            lname.Content = testSuspect.Name + " (" + testSuspect.Connection + ")";
            tbSummary.Text = testSuspect.Summary;

            //Load First Goal
            Goal currentGoal = testSuspect.CurrentGoal;

            ChangeGoal(currentGoal);

        }

        private void addQuestion(Question question) {

            Button newButton = new Button();
            newButton.Style = this.FindResource("QuestionButtonStyle") as Style;
            newButton.Click += AskQuestion;
            newButton.Content = new TextBlock();
            ((TextBlock) newButton.Content).TextWrapping = TextWrapping.Wrap;
            ((TextBlock) newButton.Content).Text = question.ToString();
            ((TextBlock) newButton.Content).TextAlignment = TextAlignment.Justify;
            questionStackPanel.Children.Add(newButton);

        }

        private void AskQuestion(object sender, RoutedEventArgs e) {

            Button button = (Button) sender;

            QueryDto questionQuery = null;
            String questionSpeech = "";

            foreach(Question question in testSuspect.CurrentGoal.questions ) {
                if(question.Speech == ((TextBlock)button.Content).Text ) {
                    questionSpeech = question.Speech;
                    questionQuery = question.Query;
                }
            }

            //Question Virtual Suspect
            QueryResult answer = testSuspect.VirtualSuspect.Query(questionQuery);

            //Generate Natural Language Answer from Query Result
            String AnswerSpeech = NaturalLanguageGenerator.GenerateAnswer(answer);


            if( AnswerSpeech == "" ) { //If no answer was generated show query's xml
                tbAnswer.Text = ConvertToString(VirtualSuspect.Utils.AnswerGenerator.GenerateAnswer(answer));
            } else {
                tbAnswer.Text = AnswerSpeech;
            }

            loggerManager.addLog(questionSpeech, tbAnswer.Text);

        } 

        #region Utility Methods

        public static bool IsWindowOpen<T>(string name = "") where T : Window {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        private string ConvertToString(XmlDocument doc) {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using( XmlWriter writer = XmlWriter.Create(sb, settings) ) {
                doc.Save(writer);
            }
            return sb.ToString();
        }

        #endregion

        private class QuestionButtonContent {

            internal QueryDto QuestionDto;
            internal string QuestionText;

            internal QuestionButtonContent(string questionText, QueryDto questionDto) {
                QuestionText = questionText;
                QuestionDto = questionDto;
            } 

            public override string ToString() {
                return QuestionText;
            }
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        private void ChangeGoal(Goal newGoal) {

            tbGoal.Text = newGoal.description;

            questionStackPanel.Children.Clear();
            foreach( Question question in newGoal.questions ) {
                addQuestion(question);
            }

            if(notesWindow != null ) {

                foreach(Note noteToAdd in newGoal.notes ) {
                    notesWindow.addNote(noteToAdd.ToString());
                }
                notesWindow.Focus();

            }else {
                notesWindow = new NotesWindow(newGoal.notes);
                notesWindow.Show();
            }
            

        }

        private void NextGoal_Click(object sender, RoutedEventArgs e) {

            Goal newGoal = testSuspect.CompleteCurrentGoal();
            ChangeGoal(newGoal);

            //if it is the last goal
            if( testSuspect.isCurrentGoalTheLast() ) {
                NextGoal.Visibility = Visibility.Hidden;
            }
        }
    }
}
