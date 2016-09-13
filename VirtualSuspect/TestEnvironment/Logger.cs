using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestEnvironment {
    public class Logger {

        public List<Log> logs;
        private TextBlock loggerTb;
        
        public Logger(TextBlock logger) {

            logs = new List<Log>();
            this.loggerTb = logger;

        } 

        public void addLog(string question, string answer) {

            Log log = new Log();
            log.question = question;
            log.answer = answer;
            log.hour = DateTime.Now.ToLongTimeString();

            logs.Add(log);

            string message = "Question: " + question + "\n" + "Answer: " + answer + "\n\n";
            loggerTb.Text = message + loggerTb.Text;

        }

    }

    public class Log {

        internal string question;
        internal string answer;
        internal string hour;

    }
}
