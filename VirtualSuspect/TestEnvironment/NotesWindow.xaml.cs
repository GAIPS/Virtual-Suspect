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
using TestEnvironment.CustomItems;

namespace TestEnvironment
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow(List<string> initialNotes)
        {
            InitializeComponent();

            initialNotes.ForEach(x => addNote(x, false) );
        }

        private void addNote(string note, bool customNote = true) {
            if( !customNote ) {

                InitialNote noteControl = new InitialNote(note);
                spInitialNotes.Children.Add(noteControl);

            } else {

                textBox.AppendText(note);

            }
        }

        
    }
}
