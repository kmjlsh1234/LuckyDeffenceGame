using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerConfig
{
    public static string SERVER_PREFIX = "http://";
    public static string SPLITTER = ":";
    //API SERVER
    public static string API_SERVER_IP_ADDRESS = "localhost";
    public static string API_SERVER_PORT = "8080";
    
    //IN GAME SOCKET SERVER
    public static string In_Game_SERVER_IP_ADDRESS = "127.0.0.1";
    public static int in_Game_SERVER_PORT = 8081;
}
