using System.Collections.Generic;

namespace VirtualSuspect
{
    public class ChangeableGroup{

        private IChangeableContent currentValue;

        public IChangeableContent CurrentValue
        {
            get
            {
                return currentValue;
            }
        }

        private List<IChangeableContent> domain;

        public List<IChangeableContent> Domain
        {
            get
            {
                return domain;
            }
        }

        public ChangeableGroup(IChangeableContent currentValue, params IChangeableContent[] alternatives) {

            this.currentValue = currentValue;

            this.domain = new List<IChangeableContent>();

            this.domain.AddRange(alternatives);
        }

        public ChangeableGroup(IChangeableContent currentValue, List<IChangeableContent> alternatives) {

            this.currentValue = currentValue;

            this.domain = alternatives;

        }

        public void AddToDomain(IChangeableContent alternatives) {

            this.domain.Add(alternatives);

        }
    }
}