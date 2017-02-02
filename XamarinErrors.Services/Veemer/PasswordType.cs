using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace XamarinErrors.Services.Veemer
{

	public enum PasswordType
	{
		Password,
		Pin
	}
}