using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using VirtualSuspect;
using VirtualSuspect.KnowledgeBase;
using VirtualSuspect.Query;
using VirtualSuspect.Utils;
using VirtualSuspectNaturalLanguage;

namespace VirtualSuspectUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        //Console Variables
        ConsoleContent console;

        //Virtual Suspect Variables
        VirtualSuspectQuestionAnswer virtualSuspect;
        String KnowledgeBasePath;

        public MainWindow() {

            InitializeComponent();

            //Prepare UI
            AskQuestionButton.IsEnabled = false;

            //Start Console
            console = new ConsoleContent(this);
            DataContext = console;
            InputBlock.KeyDown += InputBlock_KeyDown;

            //Start Virtual Suspect
            //Select Knowledge Base To Open
            console.AddLineWithTag("setup", "Selecting Knowledge Base File...");
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true) {
                KnowledgeBasePath = dialog.FileName;
            }

            console.AddLineWithTag("setup", "Parsing Knowledge Base File...");
            KnowledgeBaseManager kbm = KnowledgeBaseParser.parseFromFile(KnowledgeBasePath);
            console.AddLineWithTag("setup", "Knowledge Base parsed successfully.");

            virtualSuspect = new VirtualSuspectQuestionAnswer(kbm);
            console.AddLineWithTag("setup", "Virtual Suspect loaded successfully.");

            UpdateStory(kbm);

        }

        /// <summary>
        /// Event handler for console Commands
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InputBlock_KeyDown(object sender, KeyEventArgs e) {
            
            if(e.Key == Key.Enter) {
                console.ConsoleInput = InputBlock.Text;
                console.RunCommand();
                InputBlock.Focus();
                ConsoleScroller.ScrollToBottom();
                InputBlock.SelectionStart = InputBlock.Text.Length ;

            }
        }

        /// <summary>
        /// Updates the Content of the Story Viewer
        /// </summary>
        /// <param name="manager"></param>
        public void UpdateStory(KnowledgeBaseManager manager) {

            List<EventNode> originalEvents = manager.Events.FindAll(x => x.OriginalStory);

            List<EventNode> eventsCreated = manager.Events.Except(originalEvents).ToList();
            
            RealStoryEventsStackPanel.Children.Clear();
            EventsCreatedStackPanel.Children.Clear();

            foreach (EventNode originalNode in originalEvents) {


                EventViewer viewer = new EventViewer(originalNode.ID,
                                                    originalNode.Action.Action,
                                                    originalNode.Location.Value,
                                                    originalNode.Time.Value,
                                                    new List<string>(originalNode.Agent.Select(x => x.Value)),
                                                    new List<string>(originalNode.Theme.Select(x => x.Value)),
                                                    new List<string>(originalNode.Manner.Select(x => x.Value)),
                                                    new List<string>(originalNode.Reason.Select(x => x.Value)),
                                                    manager.Story.Contains(originalNode));
                                                    
                viewer.Margin = new Thickness(4);

                RealStoryEventsStackPanel.Children.Add(viewer);

                //Add arrow after
                if (originalNode != originalEvents.Last()) {

                    Image arrowImageControl = new Image();
                    BitmapImage arrowImage = new BitmapImage(new Uri("pack://application:,,,/Images/Arrow.png"));
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

        #region QuestionAnswering Controls

        private void AddDimensionBox(string boxType, string dimensionName) {

            UserControl newBox = null;

            switch (boxType) {
                case "ComboBox":

                    newBox = new ComboBoxDimensionPropertiesBox();

                    //Set dimensions name
                    ((ComboBoxDimensionPropertiesBox)newBox).DimensionLabel.Content = dimensionName;

                    break;
                case "TextBox":

                    newBox = new TextBoxDimensionPropertiesBox();

                    //Set dimensions name
                    ((TextBoxDimensionPropertiesBox)newBox).DimensionLabel.Content = dimensionName;
                    break;
            }


            //Set Margins
            Thickness margin = new Thickness(10);
            newBox.Margin = margin;

            DimensionsPanel.Children.Add(newBox);

        }

        private void AddAction(object sender, RoutedEventArgs e) {

            AddDimensionBox("TextBox", "Action");

        }

        private void AddTime(object sender, RoutedEventArgs e) {

            AddDimensionBox("TextBox", "Time");

        }

        private void AddLocation(object sender, RoutedEventArgs e) {

            AddDimensionBox("TextBox", "Location");

        }

        private void AddTheme(object sender, RoutedEventArgs e) {

            AddDimensionBox("TextBox", "Theme");

        }

        private void AddAgent(object sender, RoutedEventArgs e) {

            AddDimensionBox("TextBox", "Agent");

        }

        private void AddManner(object sender, RoutedEventArgs e) {

            AddDimensionBox("TextBox", "Manner");

        }

        private void AddReason(object sender, RoutedEventArgs e) {

            AddDimensionBox("TextBox", "Reason");

        }

        private void AskQuestion(object sender, RoutedEventArgs e) {

            //Parse Question to Xml
            QueryDto query = new QueryDto(QueryDto.QueryTypeEnum.GetInformation);

            //Iterate Elements from 
            foreach (ConditionBox condition in DimensionsPanel.Children) {

                string dimension = condition.Dimension;
                string value = condition.Value;

                if (condition.Focus) {
                    KnowledgeBaseManager.DimentionsEnum focusDimension = KnowledgeBaseManager.convertToDimentions(dimension);

                    //Parse the focus according to the dimension
                    switch (focusDimension) {
                        case KnowledgeBaseManager.DimentionsEnum.Manner:
                            query.AddFocus(new GetMannerFocusPredicate());
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Agent:
                            query.AddFocus(new GetAgentFocusPredicate());
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Location:
                            query.AddFocus(new GetLocationFocusPredicate());
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Time:
                            query.AddFocus(new GetTimeFocusPredicate());
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Reason:
                            query.AddFocus(new GetReasonFocusPredicate());
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Theme:
                            query.AddFocus(new GetThemeFocusPredicate());
                            break;
                    }
                }
                else {

                    KnowledgeBaseManager.DimentionsEnum conditionDimension = KnowledgeBaseManager.convertToDimentions(dimension);

                    switch (conditionDimension) {
                        case KnowledgeBaseManager.DimentionsEnum.Action:
                            string action = value;
                            query.AddCondition(new ActionEqualConditionPredicate(action));
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Location:
                            string location = value;
                            query.AddCondition(new LocationEqualConditionPredicate(location));
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Time:
                            string beginTime = value;
                            string endTime = value;
                            query.AddCondition(new TimeBetweenConditionPredicate(beginTime, endTime));
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Manner:
                            List<string> manners = new List<string>();
                            manners.Add(value);
                            query.AddCondition(new MannerEqualConditionPredicate(manners));
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Reason:
                            List<string> reasons = new List<string>();
                            reasons.Add(value);
                            query.AddCondition(new ReasonEqualConditionPredicate(reasons));
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Theme:
                            List<string> themes = new List<string>();
                            themes.Add(value);
                            query.AddCondition(new ThemeEqualConditionPredicate(themes));
                            break;
                        case KnowledgeBaseManager.DimentionsEnum.Agent:
                            List<string> agents = new List<string>();
                            agents.Add(value);
                            query.AddCondition(new AgentEqualConditionPredicate(agents));
                            break;

                    }

                }

            }

            console.AddLineWithTag("answering", "Asking question...");
            //Query Knowledge Base
            QueryResult result = virtualSuspect.Query(query);
            console.AddLineWithTag("answering", "Question asked.");

            //Create Speech from Result
            console.AddLineWithTag("answering", "Generating Natural Language Answer...");
            String AnswerSpeech = NaturalLanguageGenerator.GenerateAnswer(result);
            

            if (AnswerSpeech == "") {
                console.AddLineWithTag("ERROR answering", "Impossible to generate natural language answer!");
                console.AddLineWithTag("answering", "Showing Answer Structure!");
                AnswerTextBlock.Text = ConvertToString(VirtualSuspect.Utils.AnswerGenerator.GenerateAnswer(result));
            } else {
                console.AddLineWithTag("answering", "Generated Natural Language Answer Successfully");
                AnswerTextBlock.Text = AnswerSpeech;
            }
                

            //Update StoryViewer Content
            UpdateStory(virtualSuspect.KnowledgeBase);
            console.AddLineWithTag("Story", "Updated Story Successfully.");

        }

        private void ValidateQuestion(object sender, RoutedEventArgs e) {

            bool validQuestion = true;
            string QuestionError = "Unexpected error";
            
            //Validate Question
            if(validQuestion) {
                console.AddLineWithTag("Valid Question", "The Question created is Valid");
                //Enable Ask Question Button
                AskQuestionButton.IsEnabled = true;
            } else {
                console.AddLineWithTag("question error", "The Question created is Invalid");
                console.AddLineWithTag("question error", QuestionError);
            }
            
        }

        #endregion 

        private string ConvertToString(XmlDocument doc) {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings)) {
                doc.Save(writer);
            }
            return sb.ToString();
        }
    }

}
