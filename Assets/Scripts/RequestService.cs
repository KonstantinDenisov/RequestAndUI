using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class RequestService : MonoBehaviour
{
  #region Variables

  [SerializeField] private DynamicCellInitiation _dynamicCellInitiation;

  private const string Path = "https://dfu8aq28s73xi.cloudfront.net/testUsers";
  private List<User> _users;

  #endregion
  
  #region Unity LifeCycle

  private void Start()
  {
    StartCoroutine(GetRequest(Path));
  }

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
          AnalyzeDataTransferObject(webRequest.downloadHandler.text);
          break;
      }
    }
  }

  private void AnalyzeDataTransferObject(string json)
  {
    _users = JsonConvert.DeserializeObject<List<User>>(json);

    foreach (User user in _users)
    {
      
    }
    
    _dynamicCellInitiation.CreateNewCell(_users);
  }

  #endregion
}
