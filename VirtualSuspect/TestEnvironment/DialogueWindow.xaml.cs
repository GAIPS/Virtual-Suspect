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

namespace TestEnvironment
{
    /// <summary>
    /// Interaction logic for DialogueWindow.xaml
    /// </summary>
    public partial class DialogueWindow : Window {

        private int id;
        private TestSuspect testSuspect;

        public DialogueWindow(int id, TestSuspect testSuspect) {

            this.id = id;
            this.testSuspect = testSuspect;

            InitializeComponent();

            //Update UI
            BitmapImage avatar = new BitmapImage();
            avatar.BeginInit();
            avatar.UriSource = new Uri(testSuspect.ProfileImagePath);
            avatar.EndInit();
            imageProfilePicture.Source = avatar;

            lname.Content = testSuspect.Name + " (" + testSuspect.Connection + ")";
            tbSummary.Text = testSuspect.Summary;

            //Test 
            addQuestion("Hello from the other sideeeeee", null);

            addQuestion("I must've called a thousand times", null);

            addQuestion("To tell you I'm sorry", null);

            addQuestion("To tell you I'm sorry", null);

            addQuestion("To tell you I'm sorry", null);

            addQuestion("To tell you I'm sorry", null);

        }

        private void addQuestion(string question, object o) {

            Button newButton = new Button();
            newButton.Content = question;
            newButton.Style = this.FindResource("QuestionButtonStyle") as Style;
            newButton.Click += AskQuestion;
            questionStackPanel.Children.Add(newButton);

        }

        private void AskQuestion(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }

        private void NotesButton_Click(object sender, RoutedEventArgs e) {

            NotesWindow window = new NotesWindow(new List<string>());
            window.Show();
        }
    }
}
