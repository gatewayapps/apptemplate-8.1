using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Core.API.Requests
{
	public class RequestBase
	{
		[Newtonsoft.Json.JsonIgnore]
		public System.Net.Http.HttpMethod method { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public String resource { get; set; }

		protected RequestBase(System.Net.Http.HttpMethod _method, String _resource)
		{
			resource = _resource;
			method = _method;

		}

		protected async Task<T> executeAsync<T>()
		{
			var data = JsonConvert.SerializeObject(this);

			var response = await APIClient.Client.ExecuteRequest(this);
			if (response != null)
			{
				var result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
				return result;
			}
			else
			{
				return default(T);
			}
		}

		public String getJSON()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(this);
		}
	}
}
