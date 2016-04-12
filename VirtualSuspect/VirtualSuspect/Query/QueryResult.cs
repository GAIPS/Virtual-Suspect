using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VirtualSuspect.Query
{
    public class QueryResult
    {

        private bool yesNoResult;

        public bool YesNoResult {
            get {
                return yesNoResult;
            }
        }

        private QueryDto query;

        public QueryDto Query {
            get {
                return query;
            }
        }

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

        internal void AddBooleanResult(bool result) {

            yesNoResult = result;

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

        internal void CountResult() {

            List<Result> newUniqueResults = new List<Result>();

            IEqualityComparer<Result> comparer = new GroupByComparer();
            newUniqueResults.AddRange(results.GroupBy(x => x, comparer).Select(x => x.First()));

            foreach(Result result in newUniqueResults) {
            
                result.cardinality = results.Count(x => comparer.Equals(x ,result));

            }

            results = newUniqueResults;
        }

        private class GroupByComparer : IEqualityComparer<Result>
        {
            public bool Equals(Result x, Result y) {

                return x.dimension == y.dimension && Enumerable.SequenceEqual(x.values.OrderBy(t=>t), y.values.OrderBy(t => t));

            }

            public int GetHashCode(Result obj) {
                int test1=obj.dimension.GetHashCode();
                int test2 = obj.values.Sum(x => x.GetHashCode());
                return obj.dimension.GetHashCode() + obj.values.Sum(x => x.GetHashCode());
            }
        }
    }
}