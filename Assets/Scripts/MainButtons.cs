using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartNew()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    public void Exit()
    {
        EditorApplication.ExitPlaymode();

        Application.Quit();

    }
}
