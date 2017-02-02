using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyQuizAdmin.Models
{
    public class Request
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

        public async Task<RegistrationResponse> register(RegistrationData auth)
        {
            RegistrationResponse result = await POST<RegistrationResponse>("/api/devices", auth);
            return result;
        }


        public async Task<ObservableCollection<QuestionBlock>> questionnaireRequest()
        {

            ObservableCollection<QuestionBlock> result = new ObservableCollection<QuestionBlock>();
            result = await GET<ObservableCollection<QuestionBlock>>("api/questionBlock/");

            /*****  Testdaten  *****/
            if (result == null)
            {
                QuestionBlock testQuestionnaire = new QuestionBlock();
                testQuestionnaire.title = "Testliste";
                Request request = new Request();
                testQuestionnaire.questionList = await request.questionRequest();
                result.Add(testQuestionnaire);
            }


            return result;
        }

        public async Task<ObservableCollection<Question>> questionRequest()
        {
            ObservableCollection<Question> result = await GET<ObservableCollection<Question>>("api/questions/");
            return result;
        }

    }

    public class RegistrationData
    {
        public string password { get; set; }
        //public string token { get; set; }
        //public string deviceID { get; set; }
    }

    public class RegistrationResponse
    {
        public long Id { get; set; }
        public long? IsAdmin { get; set; }
        //public string PushUpToken { get; set; }
    }
}
