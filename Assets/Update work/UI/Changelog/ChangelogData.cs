using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangelogData {
    public LocalizationItem[] items;
}

[System.Serializable]
public class ChangelogItem {
    public string key;
    public string value;
}