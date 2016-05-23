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
using VirtualSuspect.KnowledgeBase;

namespace VirtualSuspectUI {
    /// <summary>
    /// Interaction logic for StoryViewer.xaml
    /// </summary>
    public partial class StoryViewer : Window {
        public StoryViewer() {

            InitializeComponent();
            Update(VirtualSuspect.Utils.KnowledgeBaseParser.parseFromFile("C:\\Users\\Diogo Rato\\Documents\\IST\\Virtual Suspect\\Projects\\Virtual Suspect\\Story\\Test1\\JoãoPOV.xml"));
        
        }


        public void Update(KnowledgeBaseManager manager) {

            List<EventNode> realEvents = manager.Story;
            List<EventNode> eventsCreated = manager.Events.Except(realEvents).ToList();

            foreach(EventNode node in realEvents) {

                EventViewer viewer = new EventViewer(node.ID, node.Action.Action, node.Location.Value, node.Time.Value, new List<string>(node.Agent.Select(x=>x.Value)), new List<string>(node.Theme.Select(x => x.Value)), new List<string>(node.Manner.Select(x => x.Value)) , new List<string>(node.Reason.Select(x => x.Value)), true );

                viewer.Margin = new Thickness(4);

                RealStoryEventsStackPanel.Children.Add(viewer);


            }

            foreach (EventNode node in eventsCreated) {

                EventViewer viewer = new EventViewer(node.ID, node.Action.Action, node.Location.Value, node.Time.Value, new List<string>(node.Agent.Select(x => x.Value)), new List<string>(node.Theme.Select(x => x.Value)), new List<string>(node.Manner.Select(x => x.Value)), new List<string>(node.Reason.Select(x => x.Value)), true);

                viewer.Margin = new Thickness(4);

                EventsCreatedStackPanel.Children.Add(viewer);


            }

        }
    }
}
