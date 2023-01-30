using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UIElements;

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

  private IEnumerator GetRequest(string uri)
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

          StartCoroutine(AnalyzeDataTransferObject(webRequest.downloadHandler.text));
          break;
      }
    }
  }

  private IEnumerator AnalyzeDataTransferObject(string json)
  {
    _users = JsonConvert.DeserializeObject<List<User>>(json);

    foreach (User user in _users)
    {
      UnityWebRequest webRequest = UnityWebRequest.Get(user.AvatarUrl);
      webRequest.timeout = 5;
      yield return webRequest.SendWebRequest();

      string avatarInString = webRequest.downloadHandler.text;
      byte[] avatarInByte = Encoding.ASCII.GetBytes(avatarInString);

      //Image avatarInImage = new MemoryStream(avatarInByte);
      
      //using (var ms = new MemoryStream(avatarInByte))
      //{
        //yield return Image.FromStream(ms);
      //}
      
      yield return new WaitForSeconds(1);
    }
    
    _dynamicCellInitiation.CreateNewCell(_users);

    StartCoroutine(_dynamicCellInitiation.CreateNewCell(_users));

    yield return null;
  }

  #endregion
}
