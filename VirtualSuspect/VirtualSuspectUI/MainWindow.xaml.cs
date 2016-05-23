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

namespace VirtualSuspectUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void AddAction(object sender, RoutedEventArgs e) {

            AddDimensionBox("Action");

        }

        private void AddDimensionBox(string dimensionName) {

            DimensionPropertiesBox newBox = new DimensionPropertiesBox();

            //Set Margins
            Thickness margin = new Thickness(10);
            newBox.Margin = margin;

            //Set dimensions name
            newBox.DimensionLabel.Content = dimensionName;

            DimensionsPanel.Children.Add(newBox);
        }

        private void AddTime(object sender, RoutedEventArgs e) {

            AddDimensionBox("Time");

        }

        private void AddLocation(object sender, RoutedEventArgs e) {

            AddDimensionBox("Location");

        }

        private void AddTheme(object sender, RoutedEventArgs e) {

            AddDimensionBox("Theme");

        }

        private void AddAgent(object sender, RoutedEventArgs e) {

            AddDimensionBox("Agent");

        }

        private void AddManner(object sender, RoutedEventArgs e) {

            AddDimensionBox("Manner");

        }

        private void AddReason(object sender, RoutedEventArgs e) {

            AddDimensionBox("Reason");

        }

        private void ShowStoryViewer(object sender, RoutedEventArgs e) {

            StoryViewer viewer = new StoryViewer();
            viewer.Show();

        }
    }
}
