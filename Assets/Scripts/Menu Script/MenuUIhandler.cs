using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;


public class MenuUIhandler : MonoBehaviour
{

    private string getUserName="";
    public TMP_Text BestScoreandNameText;



    public Text Name;

    public string getBestScore="";



    // Start is called before the first frame update
    void Start()
    {
        NameandBestScore();
        string a = getUserName + " has the Best Score : " + getBestScore;

        BestScoreandNameText.text = a;
    }


    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
  
    public void Exit()
    {
        EditorApplication.ExitPlaymode();

        Application.Quit();

    }

    [System.Serializable]
    class MenuUI
    {
        public string BestScore;
        public string UserName;
    }

    public void NameandBestScore()
    {
        MenuUI data = new MenuUI();

        string path = Application.persistentDataPath + "/savefile.json";
        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<MenuUI>(json);
        getUserName = data.UserName;
        getBestScore = data.BestScore;

    }


    }


