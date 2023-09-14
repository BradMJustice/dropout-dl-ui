using System.Net.Http.Headers;

namespace dropout_dl.Common.Logic
{
    public class AuthLogic
    {
        public static async Task<(string session, string cf_bm, string authenticationToken)> LoginWithCredentials
        (
            string email,
            string password
        )
        {
            var session = "";
            var cf_bm = "";
            var authenticationToken = "";

            // Initialize HttpClient
            using var httpClient = new HttpClient();
            // Set common headers
            httpClient.DefaultRequestHeaders.Add("Origin", "https://www.dropout.tv");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            httpClient.DefaultRequestHeaders.Add("Referer", "https://www.dropout.tv/login");
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("my-user-agent/1.0");

            // Prepare cookies and headers
            var cookies = $"locale_det=en; referrer_url=https%3A%2F%2Fwww.dropout.tv%2F; _session={session}; __cf_bm={cf_bm}";
            httpClient.DefaultRequestHeaders.Add("Cookie", cookies);

            // Prepare form data
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("authenticity_token", authenticationToken)
            });

            // Prepare HttpRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.dropout.tv/login")
            {
                Content = formData
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            // Make the request
            var response = await httpClient.SendAsync(request);

            // Process cookies to update the session (this is a simplified example; actual logic may vary)
            if (response.Headers.TryGetValues("Set-Cookie", out var setCookies))
            {
                foreach (var setCookie in setCookies)
                {
                    if (setCookie.StartsWith("_session="))
                    {
                        session = setCookie.Split(';')[0].Substring("_session=".Length);
                    }
                }
            }

            return (session, cf_bm, authenticationToken);
        }
    }
}
