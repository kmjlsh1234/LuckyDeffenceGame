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

    public void SendRequest(string uri, string requestBody, HTTP method)
    {
        StartCoroutine(HTTPRequest(uri, requestBody, method));
    }

    public void ReceiveResponse(UnityWebRequest.Result res)
    {

    }

    IEnumerator HTTPRequest(string uri, string requestBody, HTTP method)
    {
        using(UnityWebRequest req = new UnityWebRequest(uri, method.ToString()))
        {
            SetRequestHeader(req);
            SetRequestBody(req, requestBody);

            yield return req.SendWebRequest();

            if(req.result == UnityWebRequest.Result.Success)
            {
                ReceiveResponse(req.result);
            }
            else
            {

            }
        }
    }

    public void SetRequestHeader(UnityWebRequest req)
    {
        // JWT 토큰 추가
        req.SetRequestHeader("Authorization", "Bearer " + jwtToken);
    }

    private void SetRequestBody(UnityWebRequest req, string requestBody)
    {
        if (requestBody == null) return;

        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
    }
}
