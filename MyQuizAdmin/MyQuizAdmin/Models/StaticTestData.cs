using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuizAdmin.Models
{
    public class StaticTestData
    {
        public int id { get; set; }
        public List<Group> group { get; set; }


        public StaticTestData()
        {
            group = new List<Group>();
        }
    }

    public class Group
    {
        public int id { get; set; }
        public string titel { get; set; }
        public List<Question> question { get; set; }
        public List<People> people { get; set; }

        public Group()
        {
            question = new List<Question>();
            people = new List<People>();
        }
    }

    public class People
    {
        public int idPeop { get; set; }
        public string name { get; set; }
        public List<QuestionPeople> questionPeople { get; set; }

        public People()
        {
            questionPeople = new List<QuestionPeople>();
        }
    }

    public class QuestionPeople
    {
        public int id { get; set; }
        public string textPeop { get; set; }
        public List<ListAnswersID> listAnswerId { get; set; }

        public QuestionPeople()
        {
            listAnswerId = new List<ListAnswersID>();
        }
    }


    public class Question
    {
        public int id { get; set; }
        public string text { get; set; }
        public List<ListAnswersID> listAnswerId { get; set; }

        public Question()
        {
            listAnswerId = new List<ListAnswersID>();
        }
    }

    public class ListAnswersID
    {
        public string answer { get; set; }
        public int amount { get; set; }
    }

    public class SQuestionPeople
    {
        public int id { get; set; }
        public string textPeop { get; set; }
        public List<ProtStatValues> listAnswerId { get; set; }

        public SQuestionPeople()
        {
            listAnswerId = new List<ProtStatValues>();
        }
    }

    public class ProtStatValues
    {
        public  string Name { get; set; }
        public int Amount { get; set; }
    }
}
