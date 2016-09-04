using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VirtualSuspect;
using VirtualSuspect.KnowledgeBase;
using VirtualSuspect.Query;
using VirtualSuspect.Utils;

namespace TestEnvironment
{
    public static class TestManager{

        private static Dictionary<int,TestSuspect> testSuspects = new Dictionary<int, TestSuspect>();

        public static List<KeyValuePair<int, TestSuspect>> TestSuspects {

            get {
                return testSuspects.ToList();
            }
        }

        public static void LoadTestSuspects() {

            testSuspects.Add(0, new TestSuspect("C:\\Users\\ratuspro\\Documents\\Virtual Suspect\\Repos\\Virtual-Suspect\\VirtualSuspect\\TestEnvironment\\Resources\\Story\\RobberyStory.xml", "John Doe","Coworker", "Some Text to chill", "C:\\Users\\ratuspro\\Documents\\Virtual Suspect\\Repos\\Virtual-Suspect\\VirtualSuspect\\TestEnvironment\\Resources\\profileDefaultPlaceholder.jpg"));

        }

        public static TestSuspect getTestSuspect(int id) {

            return testSuspects[id];

        }
    }

    public class TestSuspect {

        private List<Goal> goals;

        private int currentGoalId = 0;

        public Goal CurrentGoal {

            get {
                return goals[currentGoalId];
            }

        }

        private VirtualSuspectQuestionAnswer virtualSuspect;

        public VirtualSuspectQuestionAnswer VirtualSuspect { get { return virtualSuspect; } }

        private string storyFilePath;

        public string StoryFilePath { get { return storyFilePath; } }

        private string name;

        public string Name { get { return name; } }

        private string connection;

        public string Connection { get { return connection; } }

        private string summary;

        public string Summary { get { return summary; } }

        private string profileImagePath;

        public string ProfileImagePath { get { return profileImagePath; } }

        public TestSuspect(string storyFilePath, string name, string connection, string summary, string profileImagePath) {

            this.storyFilePath = storyFilePath;
            this.name = name;
            this.connection = connection;
            this.summary = summary;
            this.profileImagePath = profileImagePath;

            KnowledgeBaseManager kb = KnowledgeBaseParser.parseFromFile(this.storyFilePath);

            virtualSuspect = new VirtualSuspectQuestionAnswer(kb);

            //Load Game Goals
            goals = new List<Goal>();

            XmlDocument xmlFile = new XmlDocument();

            xmlFile.Load(this.storyFilePath);

            XmlNodeList goalsNodeList = xmlFile.DocumentElement.SelectNodes("goal");

            foreach(XmlNode goalNode in goalsNodeList ) {
                goals.Add(new Goal(goalNode));
            }

        }

        /// <summary>
        /// Advances the goal state and returns the new goal
        /// </summary>
        /// <returns></returns>
        public Goal CompleteCurrentGoal() {

            int totalNumGoal = goals.Count;

            if( currentGoalId < totalNumGoal ) {
                currentGoalId++;
                return CurrentGoal;
            } else {
                return null;
            }
            
        }

        internal bool isCurrentGoalTheLast() {

            return goals.IndexOf(CurrentGoal) == goals.Count - 1;

        }
    }

    public class Goal {

        public string description;

        public List<Note> notes;

        public List<Question> questions;

        public Goal(XmlNode goalNode) {
            notes = new List<Note>();
            questions = new List<Question>();

            XmlNode descriptionNode = goalNode.SelectSingleNode("description");
            description = descriptionNode.InnerText;

            XmlNodeList notesNodeList = goalNode.SelectNodes("note");
            foreach(XmlNode noteNode in notesNodeList ) {

                notes.Add(new Note(noteNode.SelectSingleNode("info").InnerText, noteNode.SelectSingleNode("source").InnerText, noteNode.SelectSingleNode("state").InnerText));

            }

            XmlNodeList questionsNodeList = goalNode.SelectNodes("question");
            foreach( XmlNode questionNode in questionsNodeList ) {

                //Get Speech from Question
                String speech = questionNode.SelectSingleNode("speech").InnerText;

                //Get type of question
                QueryDto query = QuestionParser.ExtractFromXml(questionNode.SelectSingleNode("query"));

                //Create new Question
                Question newQuestion = new Question(speech, query);

                questions.Add(newQuestion);
            }

        }

    }

    public class Note {

        public string info;
        public string source;
        public string state;

        public Note(string info, string source, string state) {
            this.info = info;
            this.source = source;
            this.state = state;
        }

        public override string ToString() {
            return info + " (source: " + source + ") [ " + state + " ]" ;
        }

    }

    public class Question {

        public QueryDto Query;
        public string Speech;

        public Question(string speech, QueryDto query) {
            this.Speech = speech;
            this.Query = query;
        }

        public override string ToString() {
            return Speech;
        }
    }
}
