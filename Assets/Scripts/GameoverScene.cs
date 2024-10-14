using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // current score display
        int curScore = PlayerPrefs.GetInt("CurScore", 0);
        GameObject.Find("ScoreDisplay").GetComponent<Text>().text = curScore.ToString();
        //update highest score if necessary and display
        if (curScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            GameObject.Find("NewHighScore").SetActive(true);
            PlayerPrefs.SetInt("HighScore", curScore);
            PlayerPrefs.Save();
        }
        else {
            GameObject.Find("NewHighScore").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // play again button
    public void LoadGame()
    {
        SceneManager.UnloadSceneAsync("GameoverScene");
        SceneManager.LoadSceneAsync("GameplayScene", LoadSceneMode.Additive);
    }

    // back to menu button
    public void BackToMainMenu()
    {
        SceneManager.UnloadSceneAsync("GameoverScene");
        SceneManager.LoadSceneAsync("StartupScene", LoadSceneMode.Additive);
    }
}
