namespace VirtualSuspect
{
    public class EntityDto{

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

        public EntityDto(string _value, string _type) {

            value = _value;
            type = _type;

        }

    }
}