using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DpmWebsite
{
    public class DpmGraphQLService
    {
        public DpmGraphQLService()
        {
            _connection = "http://localhost:4000/graphql";
        }

        public string GetHello()
        {
            var client = new GraphQLClient(_connection);
            return client.Query("{ hello }", null).Get<string>("hello");
        }

        // https://github.com/bkniffler/graphql-net-client/blob/ccf1b97a2501bea58fa2139ecabf504dfd97135c/GraphQL.cs
        public class GraphQLClient
        {
            public class GraphQLQueryResult
            {
                private string raw;
                private JObject data;
                private Exception Exception;
                public GraphQLQueryResult(string text, Exception ex = null)
                {
                    Exception = ex;
                    raw = text;
                    data = text != null ? JObject.Parse(text) : null;
                }
                public Exception GetException()
                {
                    return Exception;
                }
                public string GetRaw()
                {
                    return raw;
                }
                public T Get<T>(string key)
                {
                    if (data == null) return default(T);
                    try
                    {
                        return JsonConvert.DeserializeObject<T>(this.data["data"][key].ToString());
                    }
                    catch
                    {
                        return default(T);
                    }
                }
                public dynamic Get(string key)
                {
                    if (data == null) return null;
                    try
                    {
                        return JsonConvert.DeserializeObject<dynamic>(this.data["data"][key].ToString());
                    }
                    catch
                    {
                        return null;
                    }
                }
                public dynamic GetData()
                {
                    if (data == null) return null;
                    try
                    {
                        return JsonConvert.DeserializeObject<dynamic>(this.data["data"].ToString());
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            private string url;
            public GraphQLClient(string url)
            {
                this.url = url;
            }
            public GraphQLQueryResult Query(string query, object variables)
            {
                var fullQuery = new
                {
                    query = query,
                    variables = variables,
                };
                string jsonContent = JsonConvert.SerializeObject(fullQuery);

                Console.WriteLine(jsonContent);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";

                UTF8Encoding encoding = new UTF8Encoding();
                Byte[] byteArray = encoding.GetBytes(jsonContent.Trim());

                request.ContentLength = byteArray.Length;
                request.ContentType = @"application/json";

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                long length = 0;
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        length = response.ContentLength;
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            var json = reader.ReadToEnd();
                            return new GraphQLQueryResult(json);
                        }
                    }
                }
                catch (WebException ex)
                {
                    WebResponse errorResponse = ex.Response;
                    using (Stream responseStream = errorResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                        String errorText = reader.ReadToEnd();
                        Console.WriteLine(errorText);
                        return new GraphQLQueryResult(null, ex);
                    }
                }
            }
        }

        private readonly string _connection;
    }
}
