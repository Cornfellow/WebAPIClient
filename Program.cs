using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();
        static void Main(string[] args)
        {
            //ProcessRepositories().Wait();
            var repositories = ProcessRepositories().Result;
            foreach (var repo in repositories)
            {   if (repo.Name == "corefx") { 
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Description);
                Console.WriteLine(repo.GitHubHomeUrl);
                Console.WriteLine(repo.Homepage);
                Console.WriteLine(repo.Watchers);
                Console.WriteLine(repo.Language);
                Console.WriteLine();
                }
            }
        }

        private static async Task<List<Repository>> ProcessRepositories()
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", ".Net Foundation Repository Reporter");
            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));
            var streamTask = Client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = serializer.ReadObject(await streamTask) as List<Repository>;
            return repositories;
 
        }
    }
}
