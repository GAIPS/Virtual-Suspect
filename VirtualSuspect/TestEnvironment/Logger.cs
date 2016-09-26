using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestEnvironment {
    public class Logger {

        public List<Log> logs;
        private TextBlock loggerTb;
        private StreamWriter file;

        public Logger(TextBlock logger) {

            file = File.AppendText("C:\\Users\\Public\\VirtualSuspectLogs\\Log" + DateTime.Now.Day + "-" +DateTime.Now.Month + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute +  ".txt");
            logs = new List<Log>();
            this.loggerTb = logger;

        } 

        public void addLog(string question, string answer) {

            Log log = new Log();
            log.question = question;
            log.answer = answer;
            log.hour = DateTime.Now.ToLongTimeString();

            logs.Add(log);

            string message = "Question: " + question + "\n " + "Answer: " + answer + "\n\n";
            loggerTb.Text = message + loggerTb.Text;

            file.WriteLine(message);
        }

    }

    public class Log {

        internal string question;
        internal string answer;
        internal string hour;

    }
}
