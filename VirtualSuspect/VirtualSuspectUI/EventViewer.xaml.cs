using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for EventViewer.xaml
    /// </summary>
    public partial class EventViewer : UserControl {

        public EventViewer() {

            InitializeComponent();

        }

        public EventViewer(uint ID, string action, string location, string time, List<string> agents, List<string> themes, List<string> manners, List<string> reasons, bool isActive) {

            InitializeComponent();

            EventIdLabel.Content = "Event ID: " + ID;
            ActiveLabel.Visibility = isActive ? Visibility.Visible : Visibility.Collapsed;

            ActionTextBox.Text = action;
            TimeTextBox.Text = time;
            LocationTextBox.Text = location;

            AgentTextBox.Text = CreateStringForList(agents);
            ThemeTextBox.Text = CreateStringForList(themes);
            ReasonTextBox.Text = CreateStringForList(reasons);
            MannerTextBox.Text = CreateStringForList(manners);


            
        }

        private string CreateStringForList(List<string> objects) {

            string result = "";

            switch (objects.Count) {
                case 0:
                    result = "none";
                    break;
                case 1:
                    result = objects[0];
                    break;
                default:
                    result += "{ ";
                    for (int i = 0; i < objects.Count; i++) {
                        result += objects[i];
                        if (i < objects.Count - 1) {
                            result += ", ";
                        }
                    }
                    result += "}";
                    break;
            }

            return result;

        }

    }
}
