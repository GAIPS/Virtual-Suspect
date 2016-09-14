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

            testSuspects.Add(0, new TestSuspect("C:\\Users\\ratuspro\\Documents\\Virtual Suspect\\Repos\\Virtual-Suspect\\VirtualSuspect\\TestEnvironment\\Resources\\Story\\RobberyStory.xml", 
                "Peter Barker",
                "Suspect of Jewlery Shop Robbery", 
                "Peter Barker was seen near the Jewelry Shop days before it was robbed. He is the main suspect of the crime and some facts need to be clarified. He was brought by the police to be interviewed and now its your job to discover more about the case and the suspect.", 
                "C:\\Users\\ratuspro\\Documents\\Virtual Suspect\\Repos\\Virtual-Suspect\\VirtualSuspect\\TestEnvironment\\Resources\\profileDefaultPlaceholder.jpg"));

        }

        public static TestSuspect getTestSuspect(int id) {

            return testSuspects[id];

        }
    }

    public class TestSuspect {

        private List<Goal> goals;

        public List<Question> Questions {
            get {
                List<Question> questions = new List<Question>();

                foreach(Goal goal in goals ) {
                    questions.AddRange(goal.questions);
                }

                return questions;
            }
        }

        public List<Note> Notes {
            get {
                List<Note> notes = new List<Note>();

                foreach( Goal goal in goals ) {
                    notes.AddRange(goal.notes);
                }

                return notes;
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

    }

    public class Goal {

        public List<Note> notes;

        public List<Question> questions;

        public Goal(XmlNode goalNode) {
            notes = new List<Note>();
            questions = new List<Question>();

            XmlNode descriptionNode = goalNode.SelectSingleNode("description");
            notes.Add(new Note(descriptionNode.InnerText, "To Discover", "Not Verified"));

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
            return info + " (source: " + source + ")" ;
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
