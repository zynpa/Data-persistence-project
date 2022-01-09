using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public string input;



    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void ReadStringInput(string s)
    {
        input = s;
        //Debug.Log(input);
        var playerName = PlayerPrefs.GetString("playerNamekey", input);
    }

}
