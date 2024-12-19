using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectionManager : SingletonBase<ConnectionManager>
{
    private string jwtToken;
    private string refreshToken;
    
    private List<string> excludeURL = new List<String> { 
        ServerURI.AUTH_LOGIN_REQUEST, 
        ServerURI.AUTH_JOIN_REQUEST 
    };
    
    public void Init()
    {
        
    }

    public void SendRequest<T>(string uri, T requestBody, HTTP method, Action<UnityWebRequest> callBack)
    {
        StartCoroutine(HTTPRequest(uri, requestBody, method, callBack));
    }

    IEnumerator HTTPRequest<T>(string uri, T requestBody, HTTP method, Action<UnityWebRequest> callBack)
    {
        string detailUri = ServerConfig.SERVER_PREFIX 
            + ServerConfig.API_SERVER_IP_ADDRESS 
            + ServerConfig.SPLITTER 
            + ServerConfig.API_SERVER_PORT 
            + uri;

        using (UnityWebRequest req = new UnityWebRequest(detailUri, method.ToString()))
        {
            SetRequestHeader(req, uri);
            SetRequestBody(req, requestBody);

            yield return req.SendWebRequest();

            //토큰이 만료됨
            if(req.responseCode == 401)
            {
                StartCoroutine(TokenRefreshRequest<UnityWebRequest>(callBack));
                yield break;
            }

            callBack.Invoke(req);
        }
    }

    IEnumerator TokenRefreshRequest<T>(Action<UnityWebRequest> callBack)
    {
        string detailUri = ServerConfig.SERVER_PREFIX 
            + ServerConfig.API_SERVER_IP_ADDRESS 
            + ServerConfig.SPLITTER 
            + ServerConfig.API_SERVER_PORT 
            + ServerURI.AUTH_TOKEN_REFRESH;

        RefreshTokenParam requestBody = new RefreshTokenParam(DataManager.Instance.authToken.refreshToken);

        using (UnityWebRequest req = new UnityWebRequest(detailUri, HTTP.POST.ToString()))
        {
            SetRequestHeader(req, detailUri);
            SetRequestBody(req, requestBody);

            yield return req.SendWebRequest();

            callBack.Invoke(req);
        }
    }

    public void SetRequestHeader(UnityWebRequest req, string uri)
    {       
        AuthToken authToken = DataManager.Instance.authToken;

        //JWT 토큰 필요한지 체크
        if (excludeURL.Contains(uri)) { return; }
        
        req.SetRequestHeader("Authorization", "Bearer " + jwtToken); // JWT 토큰 추가
        req.SetRequestHeader("Content-Type", "application/json");
        req.SetRequestHeader("Accept", "*/*");
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
