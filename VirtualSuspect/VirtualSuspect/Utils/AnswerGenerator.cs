using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VirtualSuspect.Query;

namespace VirtualSuspect.Utils
{
    public static class AnswerGenerator{

        public static XmlDocument GenerateAnswer(QueryResult queryResult) {

            XmlDocument newAnswer = new XmlDocument();
            newAnswer.AppendChild(newAnswer.CreateElement("answer"));

            foreach(QueryResult.Result result in queryResult.Results) {

                XmlElement newResponseXml = newAnswer.CreateElement("response");

                XmlElement newDimensionNode = newAnswer.CreateElement("dimension");
                newDimensionNode.InnerText = KnowledgeBase.convertToString(result.dimension);

                XmlElement newCardinalityNode = newAnswer.CreateElement("cardinality");
                newCardinalityNode.InnerText = "" + result.cardinality;

                newResponseXml.AppendChild(newDimensionNode);
                newResponseXml.AppendChild(newCardinalityNode);
                
                foreach(string value in result.values) {

                    XmlElement newValueNode = newAnswer.CreateElement("value");
                    newValueNode.InnerText = value;

                    newResponseXml.AppendChild(newValueNode);
                }

                XmlNode refElem = newAnswer.DocumentElement.LastChild;

                newAnswer.DocumentElement.InsertAfter(newResponseXml,refElem);

            }

            return newAnswer;
        }

    }
}
