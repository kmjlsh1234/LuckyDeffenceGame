using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ServerEditor : EditorWindow
{
    [MenuItem("Tools/Server Config Editor")]
    public static void ShowWindow()
    {
        GetWindow<ServerEditor>("Server Config Editor");
    }

    private string api_server_ip_address;
    private string api_server_port;
    private string in_game_server_ip_address;
    private string in_game_server_port;

    private void OnGUI()
    {
        EditorGUILayout.TextField("API_SERVER_IP_ADDRESS", ServerConfig.API_SERVER_IP_ADDRESS);
        
        GUILayout.Space(10f);
        
        EditorGUILayout.TextField("API_SERVER_PORT", ServerConfig.API_SERVER_PORT);
        
        GUILayout.Space(10f);
        
        EditorGUILayout.TextField("IN_GAME_SERVER_IP_ADDRESS", ServerConfig.In_Game_SERVER_IP_ADDRESS);
        
        GUILayout.Space(10f);
        
        EditorGUILayout.TextField("IN_GAME_SERVER_PORT", ServerConfig.in_Game_SERVER_PORT);
        
        GUILayout.Space(10f);
        
        if (GUILayout.Button("Save"))
        {

        }
    }
}
