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

        public NotesWindow(List<Note> initialNotes)
        {
            InitializeComponent();

            initialNotes.ForEach(x => addNote(x.ToString()) );
        }

        public void addNote(string note) {

            TextBlock newNote = new TextBlock();
            newNote.Text = note;
            newNote.Background = Brushes.Transparent;
            newNote.TextWrapping = TextWrapping.Wrap;
            newNote.Margin = new Thickness(7, 7, 7, 7);
            newNote.FontSize = 14;
            newNote.Foreground = Brushes.White;
            spNotes.Children.Add(newNote);

        }

        
    }
}
