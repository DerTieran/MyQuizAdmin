namespace MyQuizAdmin.Models
{
    public class AnswerOption
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string Result { get; set; }
        public bool IsCorrect { get; set; }
    }
}