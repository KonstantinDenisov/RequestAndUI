using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class RequestService : MonoBehaviour
{
  #region Variables

  private const string Path = "https://dfu8aq28s73xi.cloudfront.net/testUsers";
  private User[] _users;

  #endregion


  #region Constructor

  public RequestService()
  {
    
  }

  #endregion


  #region Unity LifeCycle

  private void Start()
  {
    StartCoroutine(GetRequest(Path));
  }

  #endregion


  #region Public Methods
  
  
  

  #endregion


  #region Private Methods

  IEnumerator GetRequest(string uri)
  {
    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    {
      webRequest.timeout = 5;
      yield return webRequest.SendWebRequest();

      switch (webRequest.result)
      {
        case UnityWebRequest.Result.ConnectionError:
        case UnityWebRequest.Result.DataProcessingError:
          Debug.LogError(": Error: " + webRequest.error);
          break;
        case UnityWebRequest.Result.ProtocolError:
          Debug.LogError(": HTTP Error: " + webRequest.error);
          break;
        
        case UnityWebRequest.Result.Success:
          Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
          break;
      }
    }
  }

  #endregion
}
