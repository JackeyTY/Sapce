using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // play button click
    public void LoadGame() {
        SceneManager.UnloadSceneAsync("StartupScene");
        SceneManager.LoadSceneAsync("GameplayScene", LoadSceneMode.Additive);
    }

    // exit button click
    public void ExitGame() {
        Application.Quit();
        Debug.Log("Application.Quit() only works in build,not in editor");
    }
}
