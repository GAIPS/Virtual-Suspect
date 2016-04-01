namespace VirtualSuspect
{
    public class EntityNode{

        private uint id;

        public uint ID
        {
            get
            {
                return id;
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

    }
}