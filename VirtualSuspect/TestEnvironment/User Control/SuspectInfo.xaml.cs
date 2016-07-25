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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestEnvironment.CustomItems
{
    /// <summary>
    /// Interaction logic for SuspectInfo.xaml
    /// </summary>
    public partial class SuspectInfo : UserControl
    {

        private int id;

        public int ID {

            get {
                return id;
            }

            set {
                id = value;
            }
        }

        private TestSuspect suspect;

        public TestSuspect Suspect {
            get {
                return suspect;
            }
        }

        private string suspectStoryFilePath;

        public string SuspectStoryFilePath
        {
            get
            {
                return suspectStoryFilePath;
            }

            set
            {
                suspectStoryFilePath = value;
            }
        }

        private string suspectImageFilePath;

        public string SuspectImageFilePath
        {
            get
            {
                return suspectImageFilePath;
            }

            set
            {
                suspectImageFilePath = value;
                BitmapImage avatar = new BitmapImage();
                avatar.BeginInit();
                avatar.UriSource = new Uri(SuspectImageFilePath);
                avatar.EndInit();
                imageProfile.Source = avatar;
            }
        }

        private string suspectName;

        public string SuspectName
        {
            get
            {
                return suspectName;
            }

            set
            {
                suspectName = value;
                lName.Content = suspectName;
            }
        }

        private string suspectConnection;

        public string SuspectConnection
        {
            get
            {
                return suspectConnection;
            }

            set
            {
                suspectConnection = value;
                lConnection.Content = suspectConnection;
            }
        }

        private string suspectSummary;

        public string SuspectSummary
        {
            get
            {
                return suspectSummary;
            }

            set
            {
                suspectSummary = value;
                lSummary.Content = suspectSummary;
            }
        }

        public SuspectInfo( int id, TestSuspect testSuspect) {

            InitializeComponent();

            this.id = id;
            this.suspect = testSuspect;
            SuspectStoryFilePath = testSuspect.StoryFilePath;
            SuspectImageFilePath = testSuspect.ProfileImagePath;
            SuspectName = testSuspect.Name;
            SuspectConnection = testSuspect.Connection;
            SuspectSummary = testSuspect.Summary;

        }

        private void LaunchDialogueWindow(object sender, RoutedEventArgs e)
        {
            DialogueWindow newDialogWindow = new DialogueWindow(id, Suspect);
            newDialogWindow.Show();
            Window.GetWindow(this).Hide();
        }
    }
}
