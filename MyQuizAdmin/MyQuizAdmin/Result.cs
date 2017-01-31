using System.Collections.Generic;

namespace MyQuizAdmin
{
    public class Result
    {
        public string questionText  { get; set; }
        public List<Answer> resultAnswers { get; set; }

        public class Answer
        {
            public int amount { get; set; }
            public string text { get; set; }
        }
    }
}