using System;
using Xamarin.Forms;

namespace PTSPensjon
{
	public interface IStorage 	{
		void SetString(string key, string value);
		string GetString(string key);  		void DeleteCredentials();  		bool DoCredentialsExist(); 	}
}
