using ChromeTools;
using LeetcodeApi.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace LeetcodeApi
{
    public class LeetcodeClient
    {
        private readonly string _cookies;

        public LeetcodeClient(LeetcodeClientConfiguration config)
        {
            if (!string.IsNullOrEmpty(config.ChromeUserData))
            {
                var chromeCookeReader = new ChromeCookieReader(config.ChromeUserData);
                var cookies = chromeCookeReader.ReadCookies("leetcode.com");
                _cookies = string.Join(';', cookies.Select(c => $"{c.Key}={c.Value}"));
            }
            else
            {
                _cookies = config.Cookie;
            }
        }

        public async Task<CompanyTag> LoadCompanyTagAsync(string companySlug)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync("https://leetcode.com/problems/tag-data/company-tags/" + companySlug + "/");
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CompanyTag>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<SubmissionHistory> GetSubmissionHistoryAsync(int offset, int limit, string lastKey)
        {
            var uriBuilder = new UriBuilder("https://leetcode.com/api/submissions/")
            {
                Query = BuildSubmissionHistoryUri(offset, limit, lastKey)
            };
            var uri = uriBuilder.ToString();
            using var httpClient = CreateHttpClient();
            using var response = await httpClient.GetAsync(uri);
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<SubmissionHistory>(responseStream);
        }

        private static string BuildSubmissionHistoryUri(int offset, int limit, string lastKey)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["offset"] = offset.ToString();
            query["limit"] = limit.ToString();
            query["lastkey"] = lastKey;
            return query.ToString();
        }

        private HttpClient CreateHttpClient()
        {
            var httpMessageHandler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.All };
            var client = new HttpClient(httpMessageHandler);
            client.DefaultRequestHeaders.Add("Cookie", _cookies);
            return client;
        }
    }
}
