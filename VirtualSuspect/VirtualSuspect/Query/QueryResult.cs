using System;
using System.Collections.Generic;

namespace VirtualSuspect.Query
{
    public class QueryResult
    {
        private QueryDto query;

        private List<Result> results;

        public List<Result> Results {
            get {
                return results;
            }
        }

        public QueryResult(QueryDto query) {
            this.query = query;
            results = new List<Result>();
        }

        internal void AddResults(IEnumerable<Result> results) {

            this.results.AddRange(results);

        }

        public class Result {

            public string value;

            public int cardinality;

            public KnowledgeBase.DimentionsEnum dimension;

            public Result(string value, int cardinality, KnowledgeBase.DimentionsEnum dimension) {
                this.value = value;
                this.cardinality = cardinality;
                this.dimension = dimension;
            }
        }
    }
}