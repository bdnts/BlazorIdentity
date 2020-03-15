using BlazorIdentity.Services.Contracts;
using BlazorIdentity.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorIdentity.Services.Implementations
{
    public class AuthorizeApi : IAuthorizeApi
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;

        public AuthorizeApi(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _httpClient = _clientFactory.CreateClient();
        }

        public async Task Login(LoginParameters loginParameters)
        {
            try
            {

                var stringContent = new StringContent(JsonSerializer.Serialize(loginParameters), Encoding.UTF8, "application/json");
                var result = await _httpClient.PostAsync("https://localhost:44365/api/Authorize/Login", stringContent);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
                result.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ex={0}", ex.Message);
                throw ex;
            }
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/Authorize/Logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterParameters registerParameters)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(registerParameters), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Authorize/Register", stringContent);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<UserInfo> GetUserInfo()
        {
            var r1 = await _httpClient.GetStringAsync("https://localhost:44365/api/Authorize/UserInfo");
            var result = JsonSerializer.Deserialize<UserInfo>(r1);
            //var result = await _httpClient.GetJsonAsync<UserInfo>("api/Authorize/UserInfo");
            return result;
        }
    }
}
