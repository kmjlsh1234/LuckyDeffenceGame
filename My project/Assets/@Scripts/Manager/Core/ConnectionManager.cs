using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectionManager : SingletonBase<ConnectionManager>
{
    private string jwtToken;
    private string refreshToken;
    private List<string> excludeURL = new List<String>{ ServerURI.AUTH_LOGIN_REQUEST, ServerURI.AUTH_JOIN_REQUEST };
    
    public void Init()
    {
        
    }

    public void SendRequest<T>(string uri, T requestBody, HTTP method, Action<UnityWebRequest> callBack)
    {
        StartCoroutine(HTTPRequest(uri, requestBody, method, callBack));
    }

    public void RefreshToken<T>(T requestBody, Action<UnityWebRequest> callBack)
    {
        string uri = ServerURI.AUTH_TOKEN_REFRESH;
        StartCoroutine(TokenRefreshRequest(uri, requestBody, callBack));
    }

    IEnumerator HTTPRequest<T>(string uri, T requestBody, HTTP method, Action<UnityWebRequest> callBack)
    {
        string detailUri = ServerConfig.SERVER_PREFIX + ServerConfig.API_SERVER_IP_ADDRESS + ServerConfig.SPLITTER + ServerConfig.API_SERVER_PORT + uri;
        using (UnityWebRequest req = new UnityWebRequest(detailUri, method.ToString()))
        {
            SetRequestHeader(req, uri);
            SetRequestBody(req, requestBody);

            yield return req.SendWebRequest();

            callBack(req);
        }
    }

    IEnumerator TokenRefreshRequest<T>(string uri, T requestBody, Action<UnityWebRequest> callBack)
    {
        string detailUri = ServerConfig.SERVER_PREFIX + ServerConfig.API_SERVER_IP_ADDRESS + ServerConfig.SPLITTER + ServerConfig.API_SERVER_PORT + uri;
        using (UnityWebRequest req = new UnityWebRequest(uri, HTTP.POST.ToString()))
        {
            SetRequestHeader(req, uri);
            SetRequestBody(req, requestBody);

            yield return req.SendWebRequest();

            callBack(req);
        }
    }

    public void SetRequestHeader(UnityWebRequest req, string uri)
    {       
        req.SetRequestHeader("Accept", "*/*");
        req.SetRequestHeader("Content-Type", "application/json");

        AuthToken authToken = DataManager.Instance.authToken;

        //JWT 토큰 필요한지 체크
        if (excludeURL.Contains(uri)) { return; }
        
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
        Debug.LogError("jsonData : " + jsonData);
    }
}
