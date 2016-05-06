using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Exception;

namespace VirtualSuspect.Query
{
    public class TimeEqualConditionPredicate : IConditionPredicate{

        private DateTime datetime;

        public TimeEqualConditionPredicate(string time){
        
             datetime = DateTime.ParseExact(time, "dd/MM/yyyyTHH:mm:ss", CultureInfo.InvariantCulture);

        }

        public Predicate<EventNode> CreatePredicate() {
            return  
                delegate (EventNode node) {
                    
                    DateTime value = DateTime.Now;
                    
                    if (node.Time.Type== "TimeInstant") { //Example: dd/MM/yyyyTHH:mm:ss

                         value = DateTime.ParseExact(node.Time.Value, "dd/MM/yyyyTHH:mm:ss", CultureInfo.InvariantCulture);

                    }else if (node.Time.Type == "TimeSpan") {

                        throw new MessageFieldException("Cannot test equality between a Time Instant and a Time Span");

                    }

                    return datetime.Equals(value);
                };
        }

        public string GetSemanticRole() {

            return "TimeInstant";

        }

        public List<string> GetValues() {

            List<string> entities = new List<string>();

            String date = "" + datetime.ToString("dd/MM/yyyyTHH:mm:ss");

            entities.Add(date);

            return entities;

        }
    }
}
