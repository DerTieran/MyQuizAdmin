using System;

namespace MyQuizAdmin.Models
{
    public class QuestionBlock
    {
        public long Id { get; set; }
        public string Title { get; set; } = "Fragen Block " + new Random().Next();
    }
}