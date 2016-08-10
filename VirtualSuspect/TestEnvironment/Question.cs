using VirtualSuspect.Query;

namespace TestEnvironment {
    public class Question {

        public QueryDto Query;
        public string Speech;

        public Question(string speech, QueryDto query) {
            this.Speech = speech;
            this.Query = query;
        }

        public override string ToString() {
            return Speech;
        }
    }
}