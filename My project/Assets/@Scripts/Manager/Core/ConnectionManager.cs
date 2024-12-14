using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectionManager : SingletonBase<ConnectionManager>
{
    private string jwtToken;
    private string refreshToken;
    public void Init()
    {
        
    }

    public void SendRequest<T>(string uri, T requestBody, HTTP method, Action<UnityWebRequest> callBack)
    {
        StartCoroutine(HTTPRequest(uri, requestBody, method, callBack));
    }

    public void RefreshToken<T>(T requestBody, Action<UnityWebRequest> callBack)
    {
        string uri = ServerConfig.SERVER_PREFIX + ServerConfig.API_SERVER_IP_ADDRESS + ServerConfig.API_SERVER_PORT + ServerURI.AUTH_TOKEN_REFRESH;
        StartCoroutine(TokenRefreshRequest(uri, requestBody, callBack));
    }

    IEnumerator HTTPRequest<T>(string uri, T requestBody, HTTP method, Action<UnityWebRequest> callBack)
    {
        using(UnityWebRequest req = new UnityWebRequest(uri, method.ToString()))
        {
            SetRequestHeader(req);
            SetRequestBody(req, requestBody);

            yield return req.SendWebRequest();

            if(req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
            {
                UIManager.Instance.Push(UIType.UIPopupMessage, ErrorCode.USER_NOT_EXIST);
            }
            callBack(req);
        }
    }

    IEnumerator TokenRefreshRequest<T>(string uri, T requestBody, Action<UnityWebRequest> callBack)
    {
        using (UnityWebRequest req = new UnityWebRequest(uri, HTTP.POST.ToString()))
        {
            SetRequestHeader(req);
            SetRequestBody(req, requestBody);

            yield return req.SendWebRequest();

            callBack(req);
        }
    }

    public void SetRequestHeader(UnityWebRequest req)
    {
        // JWT 토큰 추가
        req.SetRequestHeader("Authorization", "Bearer " + jwtToken);
    }

    private void SetRequestBody<T>(UnityWebRequest req, T requestBody)
    {
        if (requestBody == null) return;
        string jsonData = JsonConvert.SerializeObject(requestBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
    }
}
