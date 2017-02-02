using System;

namespace MyQuizAdmin.Models
{
    public class AnswerOption
    {
        public long Id { get; set; }
        public string Text { get; set; } = "Antwort " + new Random().Next();
        public string IsCorrect { get; set; }
    }
}