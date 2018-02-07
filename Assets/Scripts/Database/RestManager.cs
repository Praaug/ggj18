using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Databases
{
	public class RestManager : MonoBehaviour
	{
		public void GET(string url, System.Action<string> onComplete)
		{
			var request = UnityWebRequest.Get(m_hostName + url);
			request.SetRequestHeader("Content-Type", "application/json");

			StartCoroutine(WaitForRequest(request, onComplete));
		}

		public void PUT(string url, string postData, System.Action<string> onComplete)
		{
			var request = UnityWebRequest.Put(m_hostName + url, postData);
			request.SetRequestHeader("Content-Type", "application/json");

			StartCoroutine(WaitForRequest(request, onComplete));
		}

		private IEnumerator WaitForRequest(UnityWebRequest request, System.Action<string> onComplete)
		{
			yield return request.SendWebRequest();

			if (request.isNetworkError || request.isHttpError)
			{
				print(request.error);
			}
			else
			{
				//print("Successful rest call with method " + request.method + " and result " + request.downloadHandler.text);
				onComplete?.Invoke(request.downloadHandler.text);
			}
		}

		#region Singleton Logic
		public static RestManager instance { get; private set; }

		private void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(this);
				return;
			}

			instance = this;
		}
		#endregion

		#region Private Members
		[SerializeField]
		private string m_hostName = "";
		#endregion
	}

}