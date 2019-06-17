using UnityEngine;
using System.Collections;

public class DropDown : MonoBehaviour {

    public GameObject list;

    public void dropdownClick(int currentChoice) {
        list.SetActive(!list.activeInHierarchy);
    }

}
