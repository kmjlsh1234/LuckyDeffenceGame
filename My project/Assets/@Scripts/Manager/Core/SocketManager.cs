using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;

public class SocketManager : SingletonBase<SocketManager>
{
    private TcpClient tcpClient;
    private NetworkStream networkStream;
    
    void Start()
    {
        
    }

    public void ConnectToServer()
    {
        tcpClient = new TcpClient(ServerConfig.In_Game_SERVER_IP_ADDRESS, ServerConfig.in_Game_SERVER_PORT);
        networkStream = tcpClient.GetStream();
        Debug.Log("Connected To Server");
    }

    public void DisConnectToServer()
    {
        if(tcpClient != null)
        {
            tcpClient.Close();
            tcpClient.Dispose();
        }
    }

    private void BeginReadData()
    {
        byte[] buffer = new byte[1024];
        networkStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnDataRecieved), buffer);
    }

    public void SendMessageToServer(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        networkStream.Write(data, 0, data.Length);
        Debug.Log($"Send : {message}");
    }

    public void OnDataRecieved(IAsyncResult ar)
    {
        byte[] buffer = (byte[]) ar.AsyncState;
        int bytesRead = networkStream.EndRead(ar);
        if(bytesRead > 0)
        {
            string message = Encoding.UTF8.GetString(buffer,0, bytesRead);
            Debug.Log($"Receive Message : {message}");
        }

        // 계속해서 데이터를 읽기
        BeginReadData();
    }
}
