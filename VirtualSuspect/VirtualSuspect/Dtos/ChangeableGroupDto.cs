using System.Collections.Generic;
using System.Linq;
using VirtualSuspect.Exception;

namespace VirtualSuspect
{
    public class ChangeableGroupDto{

        private IChangeableContent currentValue;

        public IChangeableContent CurrentValue {
            get {
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

        public ChangeableGroupDto(IChangeableContent currentValue, params IChangeableContent[] alternatives ) {

            this.currentValue = currentValue;

            this.domain = new List<IChangeableContent>();

            this.domain.AddRange(alternatives);

            ValidateDomain();

        }

        public ChangeableGroupDto(IChangeableContent currentValue, List<IChangeableContent> alternatives) {

            this.currentValue = currentValue;

            this.domain = alternatives;

            ValidateDomain();

        }

        public void AddToDomain(IChangeableContent alternatives) {

            this.domain.Add(alternatives);

            ValidateDomain();

        }

        private void ValidateDomain() {

            //Test if there are no duplicates
            if (Domain.Distinct().Count() != Domain.Count)
                throw new DtoFieldException("Duplicate values inside domain");

            //Test if values inside domain are all the same type
            if (Domain.Select(x => x.GetType()).Count() != 1)
                throw new DtoFieldException("Domain has multiple types of contents");

            //Test if current value is inside the domain
            if (Domain.Exists(x => x != CurrentValue))
                throw new DtoFieldException("Current Value should be inside domain");
                                
        }

    }
}