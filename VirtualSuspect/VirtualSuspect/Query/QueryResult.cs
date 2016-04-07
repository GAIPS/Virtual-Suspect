using System;
using System.Collections.Generic;
using System.Linq;

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

            public List<string> values;

            public int cardinality;

            public KnowledgeBase.DimentionsEnum dimension;

            public Result(IEnumerable<string> values, int cardinality, KnowledgeBase.DimentionsEnum dimension) {
                this.values = values.ToList();
                this.cardinality = cardinality;
                this.dimension = dimension;
            }

            public Result(string value, int cardinality, KnowledgeBase.DimentionsEnum dimension) {
                
                List<string> values = new List<string>();
                values.Add(value); 

                this.values = values;
                this.cardinality = cardinality;
                this.dimension = dimension;
            }
        }
    }
}