using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtualSuspect
{
    public class EventNode {

        private uint id;

        public uint ID
        {
            get
            {
                return id;
            }
        }

        private int incriminatory;

        public int Incriminatory
        {
            get {
                return incriminatory;
            }
        }

        private ActionNode action;

        public ActionNode Action
        {
            get
            {
                return action;
            }
        }

        private EntityNode time;

        public EntityNode Time
        {
            get
            {
                return time;
            }
        }

        private EntityNode location;

        public EntityNode Location
        {
            get
            {
                return location;
            }
        }

        private List<EntityNode> agent;

        public List<EntityNode> Agent
        {
            get
            {
                return agent;
            }
        }

        private List<EntityNode> theme;

        public List<EntityNode> Theme
        {
            get
            {
                return theme;
            }
        }

        private List<EntityNode> manner;

        public List<EntityNode> Manner
        {
            get
            {
                return manner;
            }
        }

        private List<EntityNode> reason;

        public List<EntityNode> Reason
        {
            get
            {
                return reason;
            }
        }

        public float Know {

            get {

                int totalNumEntities = ToMTable.Count;
                int numKnownEntities = ToMTable.Count( x=> x.Value == true);

                return 100.0f * numKnownEntities / totalNumEntities ;
            }
        }

        private Dictionary<EntityNode, bool> ToMTable;

        public EventNode(uint id, int incriminatory, ActionNode action, EntityNode time, EntityNode location) {

            this.id = id;
            this.incriminatory = incriminatory;
            this.action = action;
            this.time = time;
            this.location = location;

            agent = new List<EntityNode>();
            theme = new List<EntityNode>();
            manner = new List<EntityNode>();
            reason = new List<EntityNode>();

            ToMTable = new Dictionary<EntityNode, bool>();

            ToMTable.Add(time, false);
            ToMTable.Add(location, false);

        }

        public void AddAgent(EntityNode agent) {

            this.agent.Add(agent);
            ToMTable.Add(agent, false);

        }

        public void AddAgent(params EntityNode[] agents) {

            this.agent.AddRange(agents);
            foreach(EntityNode agent in agents) {
                ToMTable.Add(agent, false);
            }
        }

        public void AddAgent(List<EntityNode> agents) {

            this.agent.AddRange(agents);
            foreach (EntityNode agent in agents) {
                ToMTable.Add(agent, false);
            }
        }

        public void AddTheme(EntityNode theme) {

            this.theme.Add(theme);
            ToMTable.Add(theme, false);
        }

        public void AddTheme(params EntityNode[] themes) {

            this.theme.AddRange(themes);
            foreach (EntityNode theme in themes) {
                ToMTable.Add(theme, false);
            }
        }

        public void AddTheme(List<EntityNode> themes) {

            this.theme.AddRange(themes);
            foreach (EntityNode theme in themes) {
                ToMTable.Add(theme, false);
            }
        }

        public void AddManner(EntityNode manner) {

            this.manner.Add(manner);
            ToMTable.Add(manner, false);
        }

        public void AddManner(params EntityNode[] manners) {

            this.manner.AddRange(manners);
            foreach (EntityNode manner in manners) {
                ToMTable.Add(manner, false);
            }
        }

        public void AddManner(List<EntityNode> manners) {

            this.manner.AddRange(manners);
            foreach (EntityNode manner in manners) {
                ToMTable.Add(manner, false);
            }
        }

        public void AddReason(EntityNode reason) {

            this.reason.Add(reason);
            ToMTable.Add(reason, false);
        }

        public void AddReason(params EntityNode[] reasons) {

            this.reason.AddRange(reasons);
            foreach (EntityNode reason in reasons) {
                ToMTable.Add(reason, false);
            }
        }

        public void AddReason(List<EntityNode> reasons) {

            this.reason.AddRange(reasons);
            foreach (EntityNode reason in reasons) {
                ToMTable.Add(reason, false);
            }
        }

        /// <summary>
        /// Filters all entities in this event by semantic role and value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>returns the entity if there is a match or null otherwise</returns>
        /// 
        public EntityNode FindEntity(string type, string value) {

            switch(type) {
                case "Time":
                    if (Time.Value == value)
                        return time;
                    break;
                case "Location":
                    if (Location.Value == value)
                        return location;
                    break;
                case "Agent":
                    return Agent.Find(x => x.Value == value);
                case "Theme":
                    return Theme.Find(x => x.Value == value);
                case "Reason":
                    return Reason.Find(x => x.Value == value);
                case "Manner":
                    return Manner.Find(x => x.Value == value);
                default:
                    return null;

            }

            return null;
        }
        
        public List<EntityNode> FindEntitiesByType(string type) {

            List<EntityNode> nodes = new List<EntityNode>();

            switch (type) {
                case "Time":
                    nodes.Add(Time);
                    break;
                case "Location":
                    nodes.Add(Location); 
                    break;
                case "Agent":
                    nodes.AddRange(Agent);
                    break;
                case "Theme":
                    nodes.AddRange(Theme);
                    break;
                case "Reason":
                    nodes.AddRange(Reason);
                    break;
                case "Manner":
                    nodes.AddRange(Manner);
                    break;
            }

            return nodes;

        }

        /// <summary>
        /// Marks an entitiy Node as known by the user
        /// </summary>
        /// <param name="node"></param>
        public void TagAsKnwon(EntityNode node) {

            ToMTable[node] = true;
        }

        public bool IsKnown(EntityNode node) {

            return ToMTable[node];
        }

        public bool ContainsEntity(EntityNode node) {

            return Time == node ||
                    Location == node ||
                    Theme.Contains(node) ||
                    Agent.Contains(node) ||
                    Reason.Contains(node) ||
                    Manner.Contains(node);

        }
    }
}