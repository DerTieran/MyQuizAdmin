﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using MyQuizAdmin.Models;
using System.Collections.ObjectModel;

namespace MyQuizAdmin
{
    public class Request
    {
        private const string BaseAddress = "http://h2653223.stratoserver.net";
        private HttpClient getClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string deviceID = (string)Windows.Storage.ApplicationData.Current.RoamingSettings.Values["deviceID"];
            if (deviceID != null && deviceID.Length > 0)
            {
                client.DefaultRequestHeaders.Add("DeviceID", deviceID);
            }
            return client;
        }

        private async Task<T> GET<T>(string path)
        {
            HttpClient client = getClient();
     
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
            HttpClient client = getClient();

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
            HttpClient client = getClient();

            HttpResponseMessage response = await client.DeleteAsync(path);
            return response.StatusCode;
        }
      
        public async Task<RegistrationResponse> register(RegistrationData auth)
        {
            RegistrationResponse result = await POST<RegistrationResponse>("/api/devices", auth);
            return result;
        }

        public async Task <List<Group>> GetGroups()
        {
            List<Group> result = await GET<List<Group>>("/api/groups");
            return result;
        }

        public async Task <List<SingleTopic>> getTopicsForGroup(Group group)
        {
            List<SingleTopic> result = await GET<List<SingleTopic>>("/api/groups/" + group.Id + "/topics");          
            return result;
        }

        Random rnd = new Random();
        private Result randResult()
        {
            List<Result.Answer> resultAnswers = new List<Result.Answer>();
            resultAnswers.Add(new Result.Answer { amount = rnd.Next(1, 50), text = "A" });
            resultAnswers.Add(new Result.Answer { amount = rnd.Next(1, 50), text = "B" });
            resultAnswers.Add(new Result.Answer { amount = rnd.Next(1, 50), text = "C" });
            return new Result { questionText = "A, B oder C", resultAnswers = resultAnswers };
        }

        public async Task <List<Result>> getResultForTopicInGroup(SingleTopic topic, Group group)
        {
            //Result result = await GET<Result>("/api/groups/" + group.id + "/topics/" + topic.id + "/results");
            List<Result> result = new List<Result>();
            result.Add(randResult());
            result.Add(randResult());
            result.Add(randResult());
            result.Add(randResult());
            return result;
        }

        public async Task<List<Result>> getResultForGroup(Group group)
        {
            //Result result = await GET<Result>("/api/groups/" + group.id + "/results");
            List<Result> result = new List<Result>();
            result.Add(randResult());
            result.Add(randResult());
            result.Add(randResult());
            result.Add(randResult());
            return result;
        }

        public async Task<ObservableCollection<QuestionBlock>> questionnaireRequest()
        {

            ObservableCollection<QuestionBlock> result = new ObservableCollection<QuestionBlock>();
            result = await GET<ObservableCollection<QuestionBlock>>("api/questionBlock/");

            /*****  Testdaten  *****/
            //if (result == null)
            //{
            //    QuestionBlock testQuestionnaire = new QuestionBlock();
            //    testQuestionnaire.title = "Testliste";
            //    Request request = new Request();
            //    testQuestionnaire.questionList = await request.questionRequest();
            //    result.Add(testQuestionnaire);
            //}


            return result;
        }

        private async void questionairePost(ObservableCollection<QuestionBlock> questionlist)
        {
            await POST<ObservableCollection<QuestionBlock>>("api/questionBlock/", questionlist);
        }

        public async Task<ObservableCollection<Question>> questionRequest()
        {
            ObservableCollection<Question> result = await GET<ObservableCollection<Question>>("api/questions/");
            return result;
        }
    }
}
