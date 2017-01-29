using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuizAdmin.Models
{
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
