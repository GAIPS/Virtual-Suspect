﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Exception;
using VirtualSuspect.Query;

namespace VirtualSuspect.KnowledgeBase {

    public class KnowledgeBaseManager : IKnowledgeBase{

        #region Enumerates
    
        public enum DimentionsEnum { Action, Agent, Theme, Manner, Location, Time, Reason };
        
        public static DimentionsEnum convertToDimentions(string dimension) {

            dimension = dimension.ToLower();

            switch (dimension) {
                case "action":
                    return KnowledgeBaseManager.DimentionsEnum.Action;
                case "location":
                    return KnowledgeBaseManager.DimentionsEnum.Location;
                case "agent":
                    return KnowledgeBaseManager.DimentionsEnum.Agent;
                case "theme":
                    return KnowledgeBaseManager.DimentionsEnum.Theme;
                case "manner":
                    return KnowledgeBaseManager.DimentionsEnum.Manner;
                case "reason":
                    return KnowledgeBaseManager.DimentionsEnum.Reason;
                case "time":
                    return KnowledgeBaseManager.DimentionsEnum.Time;
                default:
                    throw new DimensionParseException("Wrong dimension: " + dimension);
            }
        }

        public static String convertToString(DimentionsEnum dimension) {

            switch (dimension) {
                case DimentionsEnum.Action:
                    return "action";
                case DimentionsEnum.Agent:
                    return "agent";
                case DimentionsEnum.Location:
                    return "location";
                case DimentionsEnum.Manner:
                    return "manner";
                case DimentionsEnum.Reason:
                    return "reason";
                case DimentionsEnum.Theme:
                    return "theme";
                case DimentionsEnum.Time:
                    return "time";
                default:
                    throw new DimensionParseException("Wrong dimension: " + dimension);
            }
        }

        #endregion

        /// <summary>
        /// List of available entities
        /// </summary>
        private List<EntityNode> entities;

        public List<EntityNode> Entities {
            get {
                return entities;
            }
        }

        /// <summary>
        /// List of available actions
        /// </summary>
        private List<ActionNode> actions;

        public List<ActionNode> Actions
        {
            get
            {
                return actions;
            }
        }

        /// <summary>
        /// List of available events
        /// </summary>
        private List<EventNode> events;

        public List<EventNode> Events
        {
            get
            {
                return events;
            }
        }

        /// <summary>
        /// Current Story
        /// </summary>
        private List<EventNode> story;

        public List<EventNode> Story {
            get {
                return story;
            }
        }

        /// <summary>
        /// Returns the next available id for an node
        /// </summary>
        /// <returns>Available Id for node</returns>
        internal uint getNextNodeId(String nodeType) {

            if (nodeType == "action") {

                return (uint)(actions.Count + 1);

            }
            else if (nodeType == "entity") {

                return (uint)(entities.Count + 1);

            }
            else if (nodeType == "event") {

                return (uint)(events.Count + 1);

            }

            return 0;

        }

        public KnowledgeBaseManager() {

            entities = new List<EntityNode>();

            actions = new List<ActionNode>();
            
            events = new List<EventNode>();

            story = new List<EventNode>();

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
            EventNode newEventNode = new EventNode(newEventNodeId, ev.Incriminatory, true, ev.Action, ev.Time, ev.Location);

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

        public void AddEventToStory(EventNode en) {

            //Test if event exists
            if (!events.Exists(x => x == en))
                throw new DtoFieldException("Event Node not found: " + en);

            //Add event to the story
            story.Add(en);

        }

        public void RemoveEventFromStory(EventNode en) {

            //Test if event exists
            if (!events.Exists(x => x == en))
                throw new DtoFieldException("Event Node not found: " + en);

            //Add event to the story
            story.Remove(en);

        }

        internal void PropagateIncriminaotryValues() {

            Dictionary<EntityNode, List<EventNode>> entityToEvent = new Dictionary<EntityNode, List<EventNode>>();

            foreach(EntityNode entity in entities) {

                entityToEvent.Add(entity, new List<EventNode>());    

            }

            foreach(EventNode eventNode in events) {

                //Time
                entityToEvent[eventNode.Time].Add(eventNode);

                //Location
                entityToEvent[eventNode.Location].Add(eventNode);

                //Agent
                foreach (EntityNode agent in eventNode.Agent) {
                    entityToEvent[agent].Add(eventNode);
                }

                //Manner
                foreach (EntityNode manner in eventNode.Manner) {
                    entityToEvent[manner].Add(eventNode);
                }

                //Theme
                foreach (EntityNode theme in eventNode.Theme) {
                    entityToEvent[theme].Add(eventNode);
                }

                //Reason
                foreach (EntityNode reason in eventNode.Reason) {
                    entityToEvent[reason].Add(eventNode);
                }

            }

            foreach(KeyValuePair<EntityNode,List<EventNode>> pair in entityToEvent) {

                float incriminatory = 0;

                int numIncriminaotryEvents = pair.Value.Count(x => x.Incriminatory != 0);

                foreach(EventNode eventNode in pair.Value.FindAll(x => x.Incriminatory!=0)) {

                    incriminatory += eventNode.Incriminatory / numIncriminaotryEvents;
                }

                pair.Key.Incriminatory = incriminatory;

            }
            
        }
        
        internal List<EntityNode> ExtractSimilarEntities(EntityNode node, bool includeIncriminatory = true) {

            List<EntityNode> result = new List<EntityNode>();

            //Check in the Entity Nodes List for similar Entities
            //Matching type
            foreach(EntityNode entity in entities) {
                if (entity.Type == node.Type)
                    result.Add(entity);
            }

            //If entity has only one semantic role them choose another 
            //entity in the same semantic role
            List<String> semanticRoles = new List<String>();

            //Get all the semantic roles that this entity has
            foreach(EventNode eventNode in events) {

                if(eventNode.ContainsEntity(node)) {

                    if(eventNode.Agent.Contains(node)) {
                        semanticRoles.Add("Agent");
                    }

                    if (eventNode.Theme.Contains(node)) {
                        semanticRoles.Add("Theme");
                    }

                    if (eventNode.Manner.Contains(node)) {
                        semanticRoles.Add("Manner");
                    }

                    if (eventNode.Reason.Contains(node)) {
                        semanticRoles.Add("Reason");
                    }

                    if (eventNode.Time == node) {
                        semanticRoles.Add("Time");
                    }

                    if (eventNode.Location == node) {
                        semanticRoles.Add("Location");
                    }
                }
            }

            //If it only has one semantic role them get all the 
            //entities in that semantic role
            if(semanticRoles.Distinct().Count() == 1) {

                foreach(EventNode eventNode in events) {

                    result.AddRange(eventNode.FindEntitiesByType(semanticRoles[0]));

                }

            }

            //If includeIncriminatory is true
            //remove all the entities that are incriminatory
            List<EntityNode> filteredResult = new List<EntityNode>();
            if(!includeIncriminatory) {

                foreach (EntityNode entity in result) {

                    if(entity.Incriminatory == 0) {
                        filteredResult.Add(entity);
                    }

                }

            }else {
                filteredResult.AddRange(result);
            }
            
            return new List<EntityNode>(filteredResult.Distinct());
        }

    }
}
