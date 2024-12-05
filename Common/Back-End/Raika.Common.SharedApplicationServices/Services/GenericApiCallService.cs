using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Net;
using System.Text;

namespace Raika.Common.SharedApplicationServices.Services
{
    public class GenericApiCallService : IGenericApiCallService
    {
        private readonly ICurrentUserService _currentUserService;

        public GenericApiCallService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }


        public async Task<TResponse> GetJsonApiAsync<TResponse>(string apiHost, string apiUrl, CancellationToken cancellationToken) where TResponse : class
        {
            CookieContainer cookieContainer = new CookieContainer();
            try
            {
                cookieContainer.SetCookies(new Uri(apiHost), $"{_currentUserService.AuthCookieName}={_currentUserService.AuthCookievalue}");
                HttpClientHandler handler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer
                };
                handler.CookieContainer = cookieContainer;
                handler.UseCookies = true;
                handler.UseDefaultCredentials = true;
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(apiHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync($"{client.BaseAddress}{apiUrl}", cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<TResponse>();
                        if (result is not null)
                            return result;
                    }
                    //
                    // Logging
                    //

                    return null;
                }
            }
            catch (Exception)
            {
                //
                // Logging
                //

                return null;
            }
        }

        public async Task<bool> PostJsonApiAsync<TRequest>(string apiHost, string apiUrl, TRequest dto, CancellationToken cancellationToken) where TRequest : class
        {
            CookieContainer cookieContainer = new CookieContainer();
            try
            {
                cookieContainer.SetCookies(new Uri(apiHost), $"{_currentUserService.AuthCookieName}={_currentUserService.AuthCookievalue}");
                HttpClientHandler handler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer
                };
                handler.CookieContainer = cookieContainer;
                handler.UseCookies = true;
                handler.UseDefaultCredentials = true;
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(apiHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                    var response = await client.PostAsJsonAsync($"{client.BaseAddress}{apiUrl}", dto, cancellationToken);
                    response.EnsureSuccessStatusCode();
                }
                return true;
            }
            catch (Exception)
            {
                //
                // Logging
                //

                return false;
            }
        }

        public async Task<bool> PostJsonApiAsync(string apiHost, string apiUrl, object data, CancellationToken cancellationToken)
        {
            CookieContainer cookieContainer = new CookieContainer();
            try
            {
                cookieContainer.SetCookies(new Uri(apiHost), $"{_currentUserService.AuthCookieName}={_currentUserService.AuthCookievalue}");
                HttpClientHandler handler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer
                };
                handler.CookieContainer = cookieContainer;
                handler.UseCookies = true;
                handler.UseDefaultCredentials = true;
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(apiHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                    var postData = JsonConvert.SerializeObject(data);
                    var stringContent = new StringContent(postData, UnicodeEncoding.UTF8, MediaTypeNames.Application.Json);
                    var response = await client.PostAsync($"{client.BaseAddress}{apiUrl}", stringContent, cancellationToken);
                    response.EnsureSuccessStatusCode();
                }
                return true;
            }
            catch (Exception)
            {
                //
                // Logging
                //

                return false;
            }
        }
    }
}
