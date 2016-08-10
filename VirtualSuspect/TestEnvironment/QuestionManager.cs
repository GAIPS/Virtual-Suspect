using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VirtualSuspect.KnowledgeBase;
using VirtualSuspect.Query;
using VirtualSuspect.Utils;

namespace TestEnvironment {

    public static class QuestionManager {

        public static List<Question> LoadQuestions(String filePath) {

            List<Question> questions = new List<Question>();

            XmlDocument xmlFile = new XmlDocument();

            xmlFile.Load(filePath);

            XmlNodeList questionNodesList = xmlFile.DocumentElement.SelectNodes("question");

            foreach(XmlNode questionNode in questionNodesList ) {

                //Get Speech from Question
                String speech = questionNode.SelectSingleNode("speech").InnerText;

                //Get type of question
                QueryDto query = QuestionParser.ExtractFromXml(questionNode);

                //Create new Question
                Question newQuestion = new Question(speech, query);

                questions.Add(newQuestion);
            }

            return questions;

        }
    }
}
