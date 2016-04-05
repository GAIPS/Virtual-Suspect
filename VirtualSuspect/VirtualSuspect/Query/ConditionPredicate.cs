using System;

namespace VirtualSuspect.Query {

    public interface IConditionPredicate{
        
        /// <summary>
        /// Creates the delegate to be used as predicate
        /// </summary>
        /// <returns>a delgate to the predicate</returns>
        Predicate<EventNode> CreatePredicate();

    }

}