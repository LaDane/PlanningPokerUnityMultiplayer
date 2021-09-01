using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChange : MonoBehaviour {

    private Dictionary<string, GameObject> menuDict = new Dictionary<string, GameObject>();
    [SerializeField] private bool debug = false;

    void Start() {
        AddToDict("Main", "MenuMain");
        AddToDict("Start", "MenuStart");
        AddToDict("Settings", "MenuSettings");
        AddToDict("Host", "MenuHost");
        AddToDict("Join", "MenuJoin");
        AddToDict("Lobby", "MenuLobby");

        EnableMenu("Main");
        // PrintDict();
    }

    private void AddToDict(string key, string tag) {
        if (GameObject.FindWithTag(tag) != null) {
            menuDict.Add(key, GameObject.FindWithTag(tag));
        }
        else if (debug) {
            Debug.Log("Could not find menu with tag: "+ tag);
        }
    }

    public void EnableMenu(string key) {
        DisableAllMenus();
        // if (menuDict.ContainsKey(key) && menuDict[key] != null) {
        if (menuDict.ContainsKey(key)) {
            menuDict[key].SetActive(true);
            if (debug) {
                Debug.Log(key +" menu enabled");
            }
        }
        else if (debug) {
            Debug.Log("menuDict does not contain key: "+ key);
        }
    }

    private void DisableAllMenus() {
        foreach(GameObject menu in menuDict.Values) {
            if (menu != null) {
                menu.SetActive(false);
            }
        }
    }

    public void ExitButton() {
        Application.Quit();
        if (debug)
            Debug.Log("Game closed");
    }

    private void PrintDict() {
        Debug.Log("Printing Dict \t\t Count = "+ menuDict.Count);
        foreach (KeyValuePair<string, GameObject> kvp in menuDict) {
            Debug.Log(kvp.Key +" : "+ kvp.Value);
        }
    }
}
