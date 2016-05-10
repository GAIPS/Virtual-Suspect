using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Query;
using VirtualSuspect.KnowledgeBase;

namespace VirtualSuspect.Handler {

    class ModifyKnowledgeBaseHandler : IPreHandler {

        private VirtualSuspectQuestionAnswer virtualSuspect;

        public ModifyKnowledgeBaseHandler(VirtualSuspectQuestionAnswer virtualSuspect) {

            this.virtualSuspect = virtualSuspect;
        
        }

        public QueryDto Modify(QueryDto query) {

            switch(virtualSuspect.Strategy) {
                case VirtualSuspectQuestionAnswer.LieStrategy.AdjustEntity:
                    //TODO:
                    break;
                case VirtualSuspectQuestionAnswer.LieStrategy.AdjustEvent:
                    //TODO:
                    break;
                case VirtualSuspectQuestionAnswer.LieStrategy.Improvise:

                    List<EventNode> nodes = virtualSuspect.FilterEvents(query.QueryConditions);

                    //Duplicate the events that occured and are incriminatory
                    foreach(EventNode node in nodes) {

                        //Is the event Incriminatory
                        if(node.Incriminatory > 0) {

                            //Duplicate the event with known data
                            EventNode duplicateEvent = DuplicateEventKnownData(node, virtualSuspect.KnowledgeBase.getNextNodeId("event"));

                            //Get the difference in number for each dimension
                            Dictionary<string, int> diffEvents = GetCardinalityDiferenceBetweenEvents(node, duplicateEvent);

                            //For each dimension that has less values add some values 
                            foreach(KeyValuePair<string, int> pairDiference in diffEvents) {

                                if(pairDiference.Value < 0) {

                                    for (int i = pairDiference.Value * -1; i > 0; i--) {

                                        //Get the same time
                                        if (pairDiference.Key == "Time") {
                                            duplicateEvent.Time = node.Time;
                                            duplicateEvent.ToMTable.Add(duplicateEvent.Time, false);
                                        }
                                        else if (pairDiference.Key == "Location") {

                                            List<EntityNode> similarNodes = virtualSuspect.KnowledgeBase.ExtractSimilarEntities(node.Location);
                                            Random randomGenerator = new Random();
                                            EntityNode toAdd = similarNodes[randomGenerator.Next(0, similarNodes.Count)];
                                            if(!duplicateEvent.ContainsEntity(toAdd)) {
                                                duplicateEvent.Location = toAdd;
                                                duplicateEvent.ToMTable.Add(duplicateEvent.Location, false);
                                            }

                                        }
                                        else if (pairDiference.Key == "Theme") {

                                            List<EntityNode> similarNodes = virtualSuspect.KnowledgeBase.ExtractSimilarEntities(node.Theme[i-1], false);
                                            Random randomGenerator = new Random();
                                            EntityNode toAdd = similarNodes[randomGenerator.Next(0, similarNodes.Count)];
                                            if (!duplicateEvent.ContainsEntity(toAdd)) {
                                                duplicateEvent.AddTheme(toAdd);
                                            }

                                        }
                                        else if (pairDiference.Key == "Manner") {

                                            List<EntityNode> similarNodes = virtualSuspect.KnowledgeBase.ExtractSimilarEntities(node.Manner[i-1]);
                                            Random randomGenerator = new Random();
                                            EntityNode toAdd = similarNodes[randomGenerator.Next(0, similarNodes.Count)];
                                            if (!duplicateEvent.ContainsEntity(toAdd)) {
                                                duplicateEvent.AddManner(toAdd);
                                            }

                                        }
                                        else if (pairDiference.Key == "Reason") {

                                            List<EntityNode> similarNodes = virtualSuspect.KnowledgeBase.ExtractSimilarEntities(node.Reason[i-1]);
                                            Random randomGenerator = new Random();
                                            EntityNode toAdd = similarNodes[randomGenerator.Next(0, similarNodes.Count)];
                                            if (!duplicateEvent.ContainsEntity(toAdd)) {
                                                duplicateEvent.AddReason(toAdd);
                                            }

                                        }
                                        else if (pairDiference.Key == "Agent") {

                                            List<EntityNode> similarNodes = virtualSuspect.KnowledgeBase.ExtractSimilarEntities(node.Agent[i-1]);
                                            Random randomGenerator = new Random();
                                            EntityNode toAdd = similarNodes[randomGenerator.Next(0, similarNodes.Count)];
                                            if (!duplicateEvent.ContainsEntity(toAdd)) {
                                                duplicateEvent.AddAgent(toAdd);
                                            }

                                        }
                                    }
                                }
                            }

                            //Add event to the events list
                            virtualSuspect.KnowledgeBase.Events.Add(duplicateEvent);

                            //Remove the old node from the story
                            virtualSuspect.KnowledgeBase.RemoveEventFromStory(node);

                            //Add the new node to the story
                            virtualSuspect.KnowledgeBase.AddEventToStory(duplicateEvent);

                        }
                    }

                    break;
            }

            return query;

        }

        /// <summary>
        /// Return a dictionary containing the diference between the number of values of the oldNode and the newNode
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        /// <returns>the diference is negative if the oldNode has more values than the new node, 0 if no diference exists and positive otherwiese</returns>
        private Dictionary<string, int> GetCardinalityDiferenceBetweenEvents(EventNode oldNode, EventNode newNode) {

            Dictionary<string, int> diference = new Dictionary<string, int>();
            
            if (oldNode.Time != null && newNode.Time == null) {
                diference.Add("Time", -1);
            }else if (oldNode.Time == null && newNode.Time != null) {
                diference.Add("Time", 1);
            }else {
                diference.Add("Time", 0);
            }

            if (oldNode.Location != null && newNode.Location == null) {
                diference.Add("Location", -1);
            }
            else if (oldNode.Location == null && newNode.Location != null) {
                diference.Add("Location", 1);
            }
            else {
                diference.Add("Location", 0);
            }

            diference.Add("Agent", newNode.Agent.Count() - oldNode.Agent.Count());
                                    
            diference.Add("Theme", newNode.Theme.Count() - oldNode.Theme.Count());

            diference.Add("Reason", newNode.Reason.Count() - oldNode.Reason.Count());
            
            diference.Add("Manner", newNode.Manner.Count() - oldNode.Manner.Count());
            
            return diference;

        }


        private EventNode DuplicateEventKnownData(EventNode old, uint newID) {

            EventNode eventCopy = new EventNode(newID, 0, old.Action);

            //Copy each dimension if they are known
            if(old.IsKnown(old.Time)) {
                eventCopy.Time = old.Time;
                eventCopy.ToMTable.Add(eventCopy.Time, true); 
            }

            if (old.IsKnown(old.Location)) {
                eventCopy.Location = old.Location;
                eventCopy.ToMTable.Add(old.Location, true);
            }

            foreach(EntityNode AgentNode in old.Agent) {
                if (old.IsKnown(AgentNode)) {
                    eventCopy.AddAgent(AgentNode);
                    eventCopy.TagAsKnwon(AgentNode);
                }
            }

            foreach (EntityNode ThemeNode in old.Theme) {
                if (old.IsKnown(ThemeNode)) {
                    eventCopy.AddTheme(ThemeNode);
                    eventCopy.TagAsKnwon(ThemeNode);
                }
            }

            foreach (EntityNode ReasonNode in old.Reason) {
                if (old.IsKnown(ReasonNode)) {
                    eventCopy.AddReason(ReasonNode);
                    eventCopy.TagAsKnwon(ReasonNode);
                }
            }

            foreach (EntityNode MannerNode in old.Manner) {
                if (old.IsKnown(MannerNode)) {
                    eventCopy.AddManner(MannerNode);
                    eventCopy.TagAsKnwon(MannerNode);
                }
            }

            return eventCopy;

        }
    
    }
}
