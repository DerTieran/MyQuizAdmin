using System.Collections.Generic;

namespace MyQuizAdmin.Models
{
    public class Group
    {       
        public long Id { get; set; }
        public string Title { get; set; }
        //public long? EnterGroupPin { get; set; }

        public List<SingleTopic> SingleTopics { get; set; }
    }
}