using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{

    public static LocalizationManager instance;

    public string currentLanguage = "localizationText_en";

    public Dictionary<string, string> localizedText;
    public bool isReady = false;
    public string missingTextString = "Localized text not found";

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        currentLanguage = PlayerPrefs.GetString("savedLanguageChoice", "localizationText_en");
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
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

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
        if (instance.localizedText.ContainsKey(key))
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