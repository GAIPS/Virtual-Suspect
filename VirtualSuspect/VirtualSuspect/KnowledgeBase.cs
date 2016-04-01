using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Exception;

namespace VirtualSuspect
{
    public class KnowledgeBase : IKnowledgeBase
    {

        private List<EntityNode> entities;

        private List<ActionNode> actions;

        private List<EventNode> events;

        public KnowledgeBase() {

            entities = new List<EntityNode>();

            actions = new List<ActionNode>();
            
            events = new List<EventNode>();
        }


        public ActionNode CreateNewAction(ActionDto ac) {
            
            //test if the Dto is valid
            if(ac.Action == null) {
                throw new DtoFieldException("ActionDto should have an Action field");
            }

            //Get new id for this action
            uint newActionId = getNextNodeId("action");

            //Create the node
            ActionNode newActionNode = new ActionNode(newActionId, ac.Action);

            //Add to the list of avilable actions
            actions.Add(newActionNode);

            return newActionNode;
        }

        public EntityNode CreateNewEntity(EntityDto en) {

            //Test if the Dto is valid
            if (en.Value == null)
                throw new DtoFieldException("EntityDto should have an Value");


            //Get new id for the Entity Node
            uint newEntityId = getNextNodeId("entity");
            
            //Create the node according to the type
            EntityNode newEntityNode = new EntityNode(newEntityId, en.Value, en.Type);

            //Add to the list of available entities
            entities.Add(newEntityNode);

            return newEntityNode;

        }

        public EventNode CreateNewEvent(EventDto ev) {

            //Test dto validity
            if (ev.Action == null) //Test if has action
                throw new DtoFieldException("EventDto should have the field 'Action'");

            if (ev.Time == null) //Test if has time
                throw new DtoFieldException("EventDto should have the field 'Time'");

            if (ev.Location == null) //Test if has Location
                throw new DtoFieldException("EventDto should have the field 'Location'");

            //Get new id for the Event Node
            uint newEventNodeId = getNextNodeId("event");

            //Create a new node with the default fields
            EventNode newEventNode = new EventNode(newEventNodeId, ev.Action, ev.Time, ev.Location);

            //Add other fields
            newEventNode.AddAgent(ev.Agent);
            newEventNode.AddManner(ev.Manner);
            newEventNode.AddReason(ev.Reason);
            newEventNode.AddTheme(ev.Theme);

            //Add to the list of events available
            events.Add(newEventNode);

            return newEventNode;

        }

        public ActionNode GetOrCreateAction(string actionName) {

            ActionNode nodeResult = actions.Find(action => action.Action == actionName);

            if(nodeResult == null) { // If it does not exists

                nodeResult = CreateNewAction(new ActionDto(actionName));

            }

            return nodeResult;
        }


        /// <summary>
        /// Returns the next available id for an node
        /// </summary>
        /// <returns>Available Id for node</returns>
        private uint getNextNodeId(String nodeType) {

            if(nodeType == "action") {
                
                return (uint)(actions.Count + 1);

            }else if (nodeType == "entity") {
                
                return (uint)(entities.Count + 1);
            
            }else if (nodeType == "event") {
                
                return (uint)(events.Count + 1);
            
            }

            return 0;

        }
    }
}
