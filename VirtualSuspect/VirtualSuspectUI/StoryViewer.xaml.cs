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

        List<KeyValuePair<EventViewer, List<EventViewer>>> ActiveEventsSiblings;
         
        public StoryViewer() {

            ActiveEventsSiblings = new List<KeyValuePair<EventViewer, List<EventViewer>>>();

            InitializeComponent();
        
        }


        public void Update(KnowledgeBaseManager manager) {

            List<EventNode> originalEvents = manager.Events.FindAll(x => x.OriginalStory);

            List<EventNode> eventsCreated = manager.Events.Except(originalEvents).ToList();

            ActiveEventsSiblings = new List<KeyValuePair<EventViewer, List<EventViewer>>>();

            RealStoryEventsStackPanel.Children.Clear();
            EventsCreatedStackPanel.Children.Clear();

            foreach(EventNode originalNode in originalEvents) {

                
                EventViewer viewer = new EventViewer(originalNode.ID,
                                                    originalNode.Action.Action,
                                                    originalNode.Location.Value,
                                                    originalNode.Time.Value, 
                                                    new List<string>(originalNode.Agent.Select(x=>x.Value)), 
                                                    new List<string>(originalNode.Theme.Select(x => x.Value)), 
                                                    new List<string>(originalNode.Manner.Select(x => x.Value)), 
                                                    new List<string>(originalNode.Reason.Select(x => x.Value)), 
                                                    manager.Story.Contains(originalNode));

                ActiveEventsSiblings.Add(new KeyValuePair<EventViewer, List<EventViewer>>(viewer, new List<EventViewer>()));

                viewer.Margin = new Thickness(4);

                RealStoryEventsStackPanel.Children.Add(viewer);

                //Add arrow after
                if (originalNode != originalEvents.Last()) {

                    Image arrowImageControl = new Image();
                    BitmapImage arrowImage = new BitmapImage(new Uri("pack://application:,,,/Arrow.png"));
                    arrowImageControl.Height = 25;
                    arrowImageControl.Width = 25;
                    arrowImageControl.RenderTransformOrigin = new Point(0.5, 0.5);
                    arrowImageControl.Source = arrowImage;

                    RotateTransform transform = new RotateTransform(90);
                    arrowImageControl.RenderTransform = transform;
                
                    RealStoryEventsStackPanel.Children.Add(arrowImageControl);

                }
            }

            foreach (EventNode node in eventsCreated) {

                EventViewer viewer = new EventViewer(node.ID, 
                                                    node.Action.Action, 
                                                    node.Location.Value, 
                                                    node.Time.Value, 
                                                    new List<string>(node.Agent.Select(x => x.Value)), 
                                                    new List<string>(node.Theme.Select(x => x.Value)), 
                                                    new List<string>(node.Manner.Select(x => x.Value)), 
                                                    new List<string>(node.Reason.Select(x => x.Value)),
                                                    manager.Story.Contains(node));

                viewer.Margin = new Thickness(4);

                EventsCreatedStackPanel.Children.Add(viewer);


            }
        }

        private void AddToSiblings(EventViewer viewer) {

            foreach(KeyValuePair<EventViewer, List<EventViewer>> pair in ActiveEventsSiblings) {

                //If it is the same event
                if(pair.Key.TimeTextBox.Text == viewer.TimeTextBox.Text) {

                }
            }

        }

    }
}
