using MakeMyResume.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace MakeMyResume.Services
{
    public class AditionalSkillsService : IAditionalSkillsService
    {
        private readonly string _token;

        public AditionalSkillsService(IConfiguration configuration)
        {
            _token = configuration["ApiTokens:Skills"];
        }

        public async Task<List<string>> GetAditionalSkills(string filter)
        {
            var resultList = new List<string>();
            using (HttpClient httpClient = new())
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"https://api.apilayer.com/skills?q={filter}");
                request.Headers.Add("apikey", _token);

                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                resultList = JsonConvert.DeserializeObject<List<string>>(responseContent);
            }

            return resultList;
        }
    }
}
