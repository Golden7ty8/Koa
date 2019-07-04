using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ChangelogManager : MonoBehaviour {

    public string currentLanguage = "changelog_en";

    public Dictionary<string, string> localizedText;
    public bool isReady = false;
    public string missingTextString = "Changelog not found";

    // Use this for initialization
    void Awake() {

        LoadLocalizedText(currentLanguage);
    }

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Application.streamingAssetsPath + "/" + fileName + ".json";
        //Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            ChangelogData loadedData = JsonUtility.FromJson<ChangelogData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            //Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }
        currentLanguage = fileName;
        //Debug.Log("Current language file: " + currentLanguage);
        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;

    }

    public bool GetIsReady()
    {
        return isReady;
    }

}