using $ext_safeprojectname$.Core.API.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Core.API
{
	public class APIClient
	{

		private System.Net.Http.HttpClientHandler handler { get; set; }
		private System.Net.Http.HttpClient http { get; set; }
		private Action<HttpResponseMessage> errorCallback { get; set; }
		
		public System.Net.CookieContainer CookieContainer { get; set; }

		public IClientConfigurationProvider provider { get; private set; }

		private static APIClient _client;

		private APIClient()
		{
			CookieContainer = new CookieContainer();
			handler = new HttpClientHandler() { CookieContainer = CookieContainer, UseCookies = true };
			http = new System.Net.Http.HttpClient(handler);

		}

		public void SetCookie(Cookie cookie, Uri domain)
		{
			handler.CookieContainer.Add(domain, cookie);
		}

		public void SetConfigurationProvider(IClientConfigurationProvider _provider)
		{
			provider = _provider;

			SetDefaultRequestHeader("User-Agent", provider.getUserAgent());

		}

		public void SetDefaultRequestHeader(String name, String value)
		{
			http.DefaultRequestHeaders.TryAddWithoutValidation(name, value);
		}

		
		public static APIClient Client
		{
			get
			{
				if (_client == null)
				{
					_client = new APIClient();
				}
				return _client;
			}
		}

		public async Task<HttpResponseMessage> ExecuteRequest(RequestBase request)
		{
			var message = new HttpRequestMessage(
				request.method,
				new Uri(String.Format("{0}{1}", provider.getBaseUrl(), request.resource))
				);

			if (request.method != System.Net.Http.HttpMethod.Get)
			{
				message.Content = new StringContent(request.getJSON(), Encoding.UTF8, "application/json");
			}

			var response = await http.SendAsync(message);

			if (response.Content != null)
			{
				return response;
			}

			else
			{
				errorCallback(response);
			}
			return null;




		}
	}
}
