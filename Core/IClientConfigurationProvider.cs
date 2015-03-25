using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Core.API
{
	public interface IClientConfigurationProvider
	{
		String getBaseUrl();
		String getUserAgent();
		String getClientPlatform();
		String getClientDevice();
		String getClientVersion();
		String getClientOSVersion();
		String getClientAppID();

		Action<HttpResponseMessage> clientErrorCallback();
	}
}