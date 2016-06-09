using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspectNaturalLanguage.Component {

    class NaturalLanguageTimeComponent : INaturalLanguageGenerationComponent {

        private string timeDateValue;

        public NaturalLanguageTimeComponent(string TimeDate) {
            
            timeDateValue = TimeDate;

        }

        public string GenerateNaturalLanguage() {

            return ConvertDateTimeToText(timeDateValue);

        }

        private static string ConvertDateTimeToText(string TimeDate) {

            //2 Dates
            if (TimeDate.Contains(">")) {

                DateTime firstDate = DateTime.ParseExact(TimeDate.Split('>')[0], "dd/MM/yyyyTHH:mm:ss", CultureInfo.InvariantCulture);
                DateTime secondDate = DateTime.ParseExact(TimeDate.Split('>')[1], "dd/MM/yyyyTHH:mm:ss", CultureInfo.InvariantCulture);

                //Same day Answer once(Ex. 3rd of March 2016 between 14:00 and 15:00)
                if (firstDate.Day == secondDate.Day && firstDate.Month == secondDate.Month && firstDate.Year == secondDate.Year) {

                    //Convert to Text Date
                    String date = "the " + ConvertDayToString(firstDate.Day) + " of " + firstDate.ToString("MMMM", CultureInfo.InvariantCulture) + " " + firstDate.Year;

                    //Convert to Text First Hour
                    String firstHour = firstDate.Hour + ":" + firstDate.ToString("mm", CultureInfo.InvariantCulture);

                    //Convert to Text Second Hour
                    String lastHour = secondDate.Hour + ":" + secondDate.ToString("mm", CultureInfo.InvariantCulture);

                    return date + ", between " + firstHour + " and " + lastHour;

                }
                else {//Different days

                    return "";
                }

            }
            else { //Single Dat
                return "";
            }


        }

        private static string ConvertDayToString(int day) {

            switch (day) {
                case 1:
                    return "1st";
                case 2:
                    return "2nd";
                case 3:
                    return "3rd";
                case 21:
                    return "21st";
                case 22:
                    return "22nd";
                case 23:
                    return "23rd";
                case 31:
                    return "31st";
                default:
                    return day + "th";
            }
        }

    }

}
