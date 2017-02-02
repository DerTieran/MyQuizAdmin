using System;
using System.Collections.Generic;

namespace MyQuizAdmin.Models
{
    public class GivenAnswer
    {
        public long Id { get; set; }
        public Group Group { get; set; }
        public Question Question { get; set; } = new Question();
        public QuestionBlock QuestionBlock { get; set; } = new QuestionBlock();
        public AnswerOption AnswerOption { get; set; } = new AnswerOption();
        public SingleTopic SingleTopic { get; set; }
        public DateTime TimeStamp { get; set; } = new DateTime();
    }
}