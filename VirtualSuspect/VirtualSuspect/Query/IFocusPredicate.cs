using System;

namespace VirtualSuspect.Query
{
    public interface IFocusPredicate{

        /// <summary>
        /// Creates the delegate to be used as predicate
        /// </summary>
        /// <returns>a delgate to the predicate</returns>
        Func<EventNode, QueryResult.Result> CreateFunction();

    }
}