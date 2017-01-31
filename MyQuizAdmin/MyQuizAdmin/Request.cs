using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using MyQuizAdmin.Models;

namespace MyQuizAdmin
{
    class Request
    {
        private const string BaseAddress = "http://h2653223.stratoserver.net";

        private async Task<T> GET<T>(string path)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            T result = default(T);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(json);
            }
            return result;
        }

        private async Task<T> POST<T>(string path, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            T result = default(T);
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(path, content);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(json);
            }
            return result;
        }

        private async Task<HttpStatusCode> DELETE(string path)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.DeleteAsync(path);
            return response.StatusCode;
        }

        public async Task <List<GroupResponse>> GetGroups()
        {
            List<GroupResponse> result = await GET<List<GroupResponse>>("/api/groups");
            return result;
        }

        public async Task <List<Topic>> getTopicsForGroup(GroupResponse group)
        {
            //Topic result = await GET<Topic>("/api/groups/" + group.id + "/topics");
            List<Topic> result = new List<Topic>();
            result.Add(new Topic { Id = 1, Name="Peter" });
            result.Add(new Topic { Id = 2, Name = "Klaus" });
            result.Add(new Topic { Id = 3, Name = "Heinz" });
            result.Add(new Topic { Id = 4, Name = "Poket" });
            return result;
        }

        public async Task <List<Result>> getResultForTopicInGroup(Topic topic, Group group)
        {
            //Result result = await GET<Result>("/api/groups/" + group.id + "/topics/" + topic.id + "/results");
            List<Result.Answer> resultAnswers = new List<Result.Answer>();
            resultAnswers.Add(new Result.Answer { amount = 50, text="gut" });
            resultAnswers.Add(new Result.Answer { amount = 10, text = " sehr gut" });
            resultAnswers.Add(new Result.Answer { amount = 20, text = "schlecht" });
            List<Result> result = new List<Result>();
            result.Add(new Result { questionText = "Wie war das Wetter", resultAnswers = resultAnswers });
            result.Add(new Result { questionText = "Wie war der Otter", resultAnswers = resultAnswers });
            return result;
        }

        //public async Task List<PeopleResponse> GetPeople()
        //{
        //    PeopleResponse result = await GET<PeopleResponse>("/api/Group");
        //    return result;
        //}
        //public async Task List<QuestionResponse> GetQuestionsAndAnswers()
        //{
        //    QuestionResponse result = await GET<QuestionResponse>("/api/Group");
        //    return result;
        //}
    }
}
