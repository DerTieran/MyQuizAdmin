using System;

namespace MyQuizAdmin.Models
{
    public class Question
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public string MultipleChoice { get; set; }
        public string Text { get; set; } = "Frage " + new Random().Next();
    }
}