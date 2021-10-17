using ChromeTools;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
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
        private readonly string _csrfToken;
        private readonly string _cookies;

        public LeetcodeClient(LeetcodeClientConfiguration config)
        {
            if (!string.IsNullOrEmpty(config.ChromeUserData))
            {
                var chromeCookeReader = new ChromeCookieReader(config.ChromeUserData);
                var cookies = chromeCookeReader.ReadCookies("leetcode.com");
                _cookies = string.Join(';', cookies.Select(c => $"{c.Key}={c.Value}"));
                _csrfToken = cookies.TryGetValue("csrftoken", out var token) ? token : null;
            }
            else
            {
                _csrfToken = config.CsrfToken;
                _cookies = config.Cookie;
            }
        }

        public async Task<CompanyTag> LoadCompanyTagAsync(string companySlug)
        {
            const string Query = @"query getCompanyTag($slug: String!) {  
                                        companyTag(slug: $slug) {    
                                            name    
                                            questions {      
                                                ...questionFields      
                                                __typename
                                            }    
                                            frequencies    
                                            __typename
                                        }  
                                        favoritesLists {    
                                            publicFavorites {      
                                                ...favoriteFields      
                                                __typename
                                            }    privateFavorites {      
                                                ...favoriteFields      
                                                __typename
                                            }    __typename
                                        }
                                    }
                                    fragment favoriteFields on FavoriteNode {  
                                        idHash  
                                        id  
                                        name  
                                        isPublicFavorite  
                                        viewCount  
                                        creator  
                                        isWatched  
                                        questions {    
                                            questionId    
                                            title    
                                            titleSlug    
                                            __typename
                                        }  
                                        __typename
                                    }
                                    fragment questionFields on QuestionNode {  
                                        status  
                                        questionId  
                                        questionFrontendId  
                                        title  
                                        titleSlug  
                                        translatedTitle  
                                        stats  
                                        difficulty  
                                        isPaidOnly  
                                        topicTags {    
                                            name    
                                            translatedName    
                                            slug    
                                            __typename
                                        }  
                                        __typename
                                    }";

            var request = new GraphQLRequest
            {
                OperationName = "getCompanyTag",
                Query = Query,
                Variables = new
                {
                    slug = companySlug
                }
            };

            using var client = CreateGraphClient();
            client.HttpClient.DefaultRequestHeaders.Referrer = new Uri("https://leetcode.com/company/" + companySlug + "/");
            client.HttpClient.DefaultRequestHeaders.Add("x-csrftoken", _csrfToken);
            var response = await client.SendQueryAsync<CompanyTagResponse>(request);
            return response.Data.CompanyTag;
        }

        public async Task<SubmissionList> LoadSubmissionListAsync(string questionSlug)
        {
            const string Query = @"query Submissions($offset: Int!, $limit: Int!, $lastKey: String, $questionSlug: String!) {
              submissionList(offset: $offset, limit: $limit, lastKey: $lastKey, questionSlug: $questionSlug) {
                lastKey
                hasNext
                submissions {
                  id
                  statusDisplay
                  lang
                  runtime
                  timestamp
                  url
                  isPending
                  memory
                  __typename
                }
                __typename
              }
            }";

            var request = new GraphQLRequest
            {
                OperationName = "Submissions",
                Query = Query,
                Variables = new
                {
                    offset = 0,
                    limit = 20,
                    questionSlug
                }
            };

            using var client = CreateGraphClient();
            var response = await client.SendQueryAsync<SubmissionsResponse>(request);
            return response.Data.SubmissionList;
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

        private GraphQLHttpClient CreateGraphClient()
        {
            var options = new GraphQLHttpClientOptions { EndPoint = new Uri("http://leetcode.com/graphql") };
            var httpClient = CreateHttpClient();
            return new GraphQLHttpClient(
                options,
                new NewtonsoftJsonSerializer(),
                httpClient);
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
