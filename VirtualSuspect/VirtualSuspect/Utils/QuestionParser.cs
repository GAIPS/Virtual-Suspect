using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VirtualSuspect.Query;
using VirtualSuspect.Exception;

namespace VirtualSuspect.Utils{

    public static class QuestionParser{
        
        public static QueryDto ExtractFromXml(XmlDocument question) {

            QueryDto newQueryDto;

            String questionType = question.SelectSingleNode("/question/type").InnerText;

            if(questionType == "get-information") {

                newQueryDto = new QueryDto(QueryDto.QueryTypeEnum.GetInformation);

            } else if (questionType == "yes-no") {

                newQueryDto = new QueryDto(QueryDto.QueryTypeEnum.YesOrNo);

            } else { //if the type is invalid

                throw new MessageFieldException("Invalid question type: " + questionType);

            }

            //Get Focus Field
            XmlNodeList focusNodeList = question.SelectNodes("/question/focus");

            foreach(XmlNode focusNode in focusNodeList) {

                KnowledgeBase.DimentionsEnum focusDimension = parseDimension(focusNode.SelectSingleNode("dimension").InnerText);

                //Parse the focus according to the dimension
                switch (focusDimension) {
                    case KnowledgeBase.DimentionsEnum.Action:
                        //TODO: newQueryDto.AddFocus(new GetActionFocusPredicate());
                        break;
                    case KnowledgeBase.DimentionsEnum.Manner:
                        //TODO: newQueryDto.AddFocus(new GetMannerFocusPredicate());
                        break;
                    case KnowledgeBase.DimentionsEnum.Agent:
                        //TODO: newQueryDto.AddFocus(new GetAgentFocusPredicate());
                        break;
                    case KnowledgeBase.DimentionsEnum.Location:
                        newQueryDto.AddFocus(new GetLocationFocusPredicate());
                        break;
                    case KnowledgeBase.DimentionsEnum.Time:
                        //TODO: newQueryDto.AddFocus(new GetTimeFocusPredicate());
                        break;
                    case KnowledgeBase.DimentionsEnum.Reason:
                        //TODO: newQueryDto.AddFocus(new GetReasonFocusPredicate());
                        break;
                    case KnowledgeBase.DimentionsEnum.Theme:
                        //TODO: newQueryDto.AddFocus(new GetThemeFocusPredicate());
                        break;
                }

            }

            //Get Conditions Predicate
            XmlNodeList conditionsNodeList = question.SelectNodes("/question/condition");
            foreach(XmlNode conditionNode in conditionsNodeList) {
                
                KnowledgeBase.DimentionsEnum conditionDimension = parseDimension(conditionNode.SelectSingleNode("dimension").InnerText);

                QueryDto.OperatorEnum conditionOperator = parseOperator(conditionDimension, conditionNode.SelectSingleNode("operator").InnerText);

                switch (conditionDimension) {
                    case KnowledgeBase.DimentionsEnum.Action:

                        switch (conditionOperator) {
                            case QueryDto.OperatorEnum.Equal:

                                string action = conditionNode.SelectSingleNode("value").InnerText;

                                newQueryDto.AddCondition(new ActionEqualConditionPredicate(action));

                                break;

                            default:
                                //nothing to do
                                break;
                        }

                        break;

                    case KnowledgeBase.DimentionsEnum.Manner:

                        switch (conditionOperator) {
                            case QueryDto.OperatorEnum.Equal:

                                List<string> manners = new List<string>();

                                foreach(XmlNode mannerNode in conditionNode.SelectNodes("value")) {

                                    manners.Add(mannerNode.InnerText);

                                }
                                
                                newQueryDto.AddCondition(new MannerEqualConditionPredicate(manners));

                                break;

                            default:
                                //nothing to do
                                break;
                        }

                        break;
                    case KnowledgeBase.DimentionsEnum.Agent:

                        switch (conditionOperator) {
                            case QueryDto.OperatorEnum.Equal:

                                List<string> agents = new List<string>();

                                foreach (XmlNode agentNode in conditionNode.SelectNodes("value")) {

                                    agents.Add(agentNode.InnerText);

                                }

                                newQueryDto.AddCondition(new AgentEqualConditionPredicate(agents));

                                break;

                            default:
                                //nothing to do
                                break;
                        }

                        break;

                    case KnowledgeBase.DimentionsEnum.Location:

                        switch (conditionOperator) {

                            case QueryDto.OperatorEnum.Equal:

                                string action = conditionNode.SelectSingleNode("value").InnerText;

                                newQueryDto.AddCondition(new LocationEqualConditionPredicate(action));

                                break;

                            default:
                                //nothing to do
                                break;
                        }

                        break;
                    case KnowledgeBase.DimentionsEnum.Time:

                        switch (conditionOperator) {
                            case QueryDto.OperatorEnum.Equal:
                                //return QueryDto.OperatorEnum.Equal;
                                break;

                            case QueryDto.OperatorEnum.Between:
                                //return QueryDto.OperatorEnum.Between;
                                break;

                            default:
                                //nothing to do
                                break;
                        }
                        break;

                    case KnowledgeBase.DimentionsEnum.Reason:

                        switch (conditionOperator) {
                            case QueryDto.OperatorEnum.Equal:

                                List<string> reasons = new List<string>();

                                foreach (XmlNode reasonNode in conditionNode.SelectNodes("value")) {

                                    reasons.Add(reasonNode.InnerText);

                                }

                                newQueryDto.AddCondition(new ReasonEqualConditionPredicate(reasons));

                                break;

                            default:
                                //nothing to do
                                break;
                        }

                        break;

                    case KnowledgeBase.DimentionsEnum.Theme:

                        switch (conditionOperator) {
                            case QueryDto.OperatorEnum.Equal:

                                List<string> themes = new List<string>();

                                foreach (XmlNode themeNode in conditionNode.SelectNodes("value")) {

                                    themes.Add(themeNode.InnerText);

                                }

                                newQueryDto.AddCondition(new ThemeEqualConditionPredicate(themes));

                                break;

                            default:
                                //nothing to do
                                break;
                        }

                        break;

                    }

                }

            return newQueryDto;
        }

        /// <summary>
        /// Helper method to map string dimensions to dimensions in the enum
        /// </summary>
        /// <param name="dimensionToParse">dimension to parse</param>
        /// <returns></returns>
        private static KnowledgeBase.DimentionsEnum parseDimension(String dimensionToParse) {

            switch(dimensionToParse) {
                case "action":
                    return KnowledgeBase.DimentionsEnum.Action;
                case "location":
                    return KnowledgeBase.DimentionsEnum.Location;
                case "agent":
                    return KnowledgeBase.DimentionsEnum.Location;
                case "theme":
                    return KnowledgeBase.DimentionsEnum.Theme;
                case "manner":
                    return KnowledgeBase.DimentionsEnum.Manner;
                case "reason":
                    return KnowledgeBase.DimentionsEnum.Reason;
                case "time":
                    return KnowledgeBase.DimentionsEnum.Time;
                default:
                    throw new MessageFieldException("Invalid dimension: " + dimensionToParse);
            }
            
        }

        /// <summary>
        /// Helper method to map string operator to the enum field
        /// Tests if the operator is available for that particular dimension
        /// </summary>
        /// <param name="dimension">test availability of operator in this dimension</param>
        /// <param name="operatorToParse">operator to be parsed</param>
        /// <returns></returns>
        private static QueryDto.OperatorEnum parseOperator(KnowledgeBase.DimentionsEnum dimension, string operatorToParse) {

            switch (dimension) {
                case KnowledgeBase.DimentionsEnum.Action:
                    switch(operatorToParse) {
                        case "equal":
                            return QueryDto.OperatorEnum.Equal;
                        default:
                            throw new MessageFieldException("Invalid operator for dimension Action: " + operatorToParse);
                    }
                case KnowledgeBase.DimentionsEnum.Manner:
                    switch (operatorToParse) {
                        case "equal":
                            return QueryDto.OperatorEnum.Equal;
                        default:
                            throw new MessageFieldException("Invalid operator for dimension Manner: " + operatorToParse);
                    }
                case KnowledgeBase.DimentionsEnum.Agent:
                    switch (operatorToParse) {
                        case "equal":
                            return QueryDto.OperatorEnum.Equal;
                        default:
                            throw new MessageFieldException("Invalid operator for dimension Agent: " + operatorToParse);
                    }
                case KnowledgeBase.DimentionsEnum.Location:
                    switch (operatorToParse) {
                        case "equal":
                            return QueryDto.OperatorEnum.Equal;
                        default:
                            throw new MessageFieldException("Invalid operator for dimension Location: " + operatorToParse);
                    }
                case KnowledgeBase.DimentionsEnum.Time:
                    switch (operatorToParse) {
                        case "equal":
                            return QueryDto.OperatorEnum.Equal;
                        case "between":
                            return QueryDto.OperatorEnum.Between;
                        default:
                            throw new MessageFieldException("Invalid operator for dimension Time: " + operatorToParse);
                    }
                case KnowledgeBase.DimentionsEnum.Reason:
                    switch (operatorToParse) {
                        case "equal":
                            return QueryDto.OperatorEnum.Equal;
                        default:
                            throw new MessageFieldException("Invalid operator for dimension Reason: " + operatorToParse);
                    }
                case KnowledgeBase.DimentionsEnum.Theme:
                    switch (operatorToParse) {
                        case "equal":
                            return QueryDto.OperatorEnum.Equal;
                        default:
                            throw new MessageFieldException("Invalid operator for dimension Theme: " + operatorToParse);
                    }

                default:
                    //Intentionally left empty (will never reach)
                    throw new MessageFieldException("Invalid dimension");
            }
        }
    }
}
