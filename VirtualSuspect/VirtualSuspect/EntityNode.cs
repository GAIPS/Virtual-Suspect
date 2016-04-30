namespace VirtualSuspect
{
    public class EntityNode : IChangeableContent{

        private uint id;

        public uint ID
        {
            get
            {
                return id;
            }
        }

        private float incriminatory;

        public float Incriminatory {

            get {
                return incriminatory;
            }

            set { 
                incriminatory = value;
            }
        
        }

        private string value;

        public string Value
        {
            get
            {
                return value;
            }
        }

        private string type;

        public string Type
        {
            get
            {
                return type;
            }
        }

        public EntityNode(uint _id, string _value, string _type){

            id = _id;
            value = _value;
            type = _type;

        }
        
        public float EvaluateKnowledge(KnowledgeBase kb) {

            float total = 0;
            float known = 0;

            foreach(EventNode node in kb.Story) {

                if(node.ContainsEntity(this)) {

                    total++;
                    if (node.IsKnown(this))
                        known++;

                }

            }

            return known / total * 100;

        }
    
    }
}