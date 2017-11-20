using System;
using System.Collections.Generic;
using System.Net.Http;
using myservice.mvc.Application.Models.Identity;

namespace myservice.mvc.Application.Services
{
    public interface IIdentityService
    {
        IdentityServiceUserResponse GetUsers(IEnumerable<string> aliases);
    }

    public class IdentityService : IIdentityService
    {
        public IdentityServiceUserResponse GetUsers(IEnumerable<string> aliases)
        {
            using (var httpClient = new HttpClient {BaseAddress = new Uri("https://identityserver/api")})
            {
                var response = httpClient.PostAsJsonAsync("user", aliases).Result;
                response.EnsureSuccessStatusCode();

                var details = response.Content.ReadAsAsync<IdentityServiceUserResponse>().Result;

                return details;
            }
        }
    }
}