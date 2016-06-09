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
using VirtualSuspect;
using VirtualSuspect.KnowledgeBase;
using VirtualSuspect.Query;

namespace VirtualSuspectUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class QuestionInferface : Window {

        private StoryViewer storyViewerWindow;
        
        private VirtualSuspectQuestionAnswer virtualSuspect;

        public QuestionInferface() {

            virtualSuspect = new VirtualSuspectQuestionAnswer(VirtualSuspect.Utils.KnowledgeBaseParser.parseFromFile("C:\\Users\\Diogo Rato\\Documents\\IST\\Virtual Suspect\\Projects\\Virtual Suspect\\Story\\Test1\\GuilhermePOV.xml"));
            
            InitializeComponent();

        }

        private void AddAction(object sender, RoutedEventArgs e) {

            AddDimensionBox("TextBox", "Action");

        }

        private void AddDimensionBox(string boxType, string dimensionName) {

            UserControl newBox = null;

            switch(boxType) {
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

        private void ShowStoryViewer(object sender, RoutedEventArgs e) {

            if (storyViewerWindow == null || !storyViewerWindow.IsLoaded) {
                storyViewerWindow = new StoryViewer();
                storyViewerWindow.Show();
                storyViewerWindow.Update(virtualSuspect.KnowledgeBase);
            } else {
                storyViewerWindow = Application.Current.Windows.OfType<StoryViewer>().ElementAt(0);
                storyViewerWindow.Focus();
            }

            

        }
    
        private void AskQuestion(object sender, RoutedEventArgs e) {

            //Parse Question to Xml
            QueryDto query = new QueryDto(QueryDto.QueryTypeEnum.GetInformation);

            //Iterate Elements from 
            foreach (ConditionBox condition in DimensionsPanel.Children) {

                string dimension = condition.Dimension;
                string value = condition.Value;

                if(condition.Focus) {
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
                }else {

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
            
            //Query Knowledge Base
            QueryResult result = virtualSuspect.Query(query);

            //Open Answer Window
            

            //Update StoryViewer Content
            if(storyViewerWindow != null)
                storyViewerWindow.Update(virtualSuspect.KnowledgeBase);
         
        }
    }
}
