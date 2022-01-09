
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // my writings
    //public static MainManager   Instance;
    private string stringBestScore;
    private string getUserName;
    public int bestScore = 0;
    public Text BestScoreName;

    public Text Name;

    private bool isBestScore;
    private string bestUserName;
    public string getName;
    // Start is called before the first frame update
    SaveData data = new SaveData();

    void Start()
    {
        //Debug.Log(Application.persistentDataPath.ToString());
       
        LoadBestScore();
        WriteScreenGetName();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        WriteText();
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
       GameOverText.SetActive(true);
       calculateBestScore();
            if (isBestScore == true)
            {
                SaveBestScore();
            }
            WriteText();
        
        
    }

    // my writing
    [System.Serializable]
    class SaveData
    {
        public string BestScore;
        public string UserName;

    }
    public void SaveBestScore()
    {
        //calculateBestScore();
        data.BestScore = bestScore.ToString();

        getName = MenuManager.Instance.input;
        data.UserName = getName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log(Application.persistentDataPath.ToString());
    }
    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
            stringBestScore = data.BestScore;
            bestScore = int.Parse(stringBestScore);

            getUserName = data.UserName;

        }
        else
        {
            bestScore = 0;
        }
     

    }

    public void calculateBestScore()
    {
        if (m_Points > bestScore)
        {
            bestScore = m_Points;
            isBestScore = true;
        }

    }

    public void WriteScreenGetName()
    {
        Name.text = MenuManager.Instance.input + "  ";

    }

    public void WriteText()
    {
        BestScoreName.text = getUserName + "  has the Best Score : " + stringBestScore;

    }


}







