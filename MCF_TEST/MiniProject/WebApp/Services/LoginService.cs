using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ViewModel;


namespace WebApp.Services
{
    public class LoginService
    {
        private readonly IConfiguration _configuration;
        private readonly string WebApiBaseUrl;

        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
            WebApiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
        }

        public async Task<UserViewModel> Authentication(UserViewModel login)
        {
            UserViewModel result = new UserViewModel();
            using (var httpClient = new HttpClient())
            {
                string strPayload = JsonConvert.SerializeObject(login);
                HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(WebApiBaseUrl + "/login", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<UserViewModel>(apiResponse);
                }
            }
            return result;
        }


        public async Task<bool> SendOtp(string receipter)
        {
            try
            {

                Random _rdm = new Random();
                int rdm = _rdm.Next(0000, 9999);
                string sender = _configuration.GetSection("emailSender").GetValue<string>("account");
                string pwd = _configuration.GetSection("emailSender").GetValue<string>("password");
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(sender, pwd),
                    EnableSsl = true
                };
                client.Send(sender, receipter, "OTP", $"OTP: {rdm}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public async Task<bool> ValidateOtp(string Otp)
        //{
        //    try
        //    {
           
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //}

    }
}
