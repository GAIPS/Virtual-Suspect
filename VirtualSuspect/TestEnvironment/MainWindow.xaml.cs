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
using TestEnvironment.CustomItems;

namespace TestEnvironment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach(KeyValuePair<int, TestSuspect> pair in TestManager.TestSuspects ) {

                SuspectInfo newSuspect = new SuspectInfo(pair.Key, pair.Value);

                newSuspect.Margin = new Thickness(0, 5, 0, 5);

                suspectsPanel.Children.Add(newSuspect);

            }
        }
    }
}
