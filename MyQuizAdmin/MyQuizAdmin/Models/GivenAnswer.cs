using System;
using System.Collections.Generic;

namespace MyQuizAdmin.Models
{
    public class GivenAnswer
    {
        public long Id { get; set; }
        public Group Group { get; set; }
        public Question Question { get; set; }
        public QuestionBlock QuestionBlock { get; set; }
        public AnswerOption AnswerOption { get; set; }
        public SingleTopic SingleTopic { get; set; }
        //public DateTime TimeStamp { get; set; }
    }
}