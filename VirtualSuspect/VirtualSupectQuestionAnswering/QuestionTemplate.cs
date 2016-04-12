namespace VirtualSupectQuestionAnswering
{
    internal class QuestionTemplate
    {
        private string message;

        public string Message {
            get {
                return message;
            }
        }

        private string templateContent;

        public string TemplateContent {
            get {
                return templateContent;
            }
        }

        public QuestionTemplate(string message, string templateContent) {
            this.message = message;
            this.templateContent = templateContent;
        }

        public override string ToString() {
            return message;
        }
    }
}