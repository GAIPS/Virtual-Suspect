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

        private bool known;

        public bool Known {

            get {
                return known;
            }

            set {
                known = value;
            }
        }

        public EntityNode(uint _id, string _value, string _type){

            id = _id;
            value = _value;
            type = _type;

        }
        
    }
}