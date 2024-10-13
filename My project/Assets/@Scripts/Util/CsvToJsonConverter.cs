using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CsvToJsonConverter : EditorWindow
{
    private string csvFilePath = "";
    private string jsonSavePath = "";

    [MenuItem("Tools/CSV to JSON Converter")]
    public static void ShowWindow()
    {
        GetWindow<CsvToJsonConverter>("CSV to JSON Converter");
    }

    private void OnGUI()
    {
        GUILayout.Label("CSV to JSON Converter", EditorStyles.boldLabel);

        if (GUILayout.Button("Select CSV File"))
        {
            csvFilePath = EditorUtility.OpenFilePanel("Select CSV File", "", "csv");
        }

        GUILayout.Label("Selected CSV: " + csvFilePath);

        if (GUILayout.Button("Select Save Location"))
        {
            jsonSavePath = EditorUtility.SaveFilePanel("Save JSON File", "", "data.json", "json");
        }

        GUILayout.Label("Save JSON to: " + jsonSavePath);

        if (GUILayout.Button("Convert and Save JSON"))
        {
            if (string.IsNullOrEmpty(csvFilePath) || string.IsNullOrEmpty(jsonSavePath))
            {
                Debug.LogError("Please select both a CSV file and a save location for the JSON file.");
                return;
            }

            try
            {
                string jsonContent = ConvertCsvToJson(csvFilePath);
                File.WriteAllText(jsonSavePath, jsonContent);
                Debug.Log("CSV successfully converted to JSON and saved at: " + jsonSavePath);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error converting CSV to JSON: " + e.Message);
            }
        }
    }

    private string ConvertCsvToJson(string csvPath)
    {
        var lines = File.ReadAllLines(csvPath);
        var headers = lines[0].Split(',');

        var jsonList = new List<Dictionary<string, string>>();

        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');
            var entry = new Dictionary<string, string>();

            for (int j = 0; j < headers.Length; j++)
            {
                entry[headers[j]] = values[j];
            }

            jsonList.Add(entry);
        }

        return JsonUtility.ToJson(new Wrapper { Items = jsonList }, true);
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<Dictionary<string, string>> Items;
    }
}
