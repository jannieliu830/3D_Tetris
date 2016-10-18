using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GamePlayButton : MonoBehaviour
{
    public bool isPaused;
    public bool isGameOver;
    private Animator anim;
    public Texture pause;
    void OnGUI()
    {
        if (GUI.Button(new Rect((Screen.width - 100), (Screen.height * 0.02f), 60, 60), "Menu"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (GUI.Button(new Rect((Screen.width - 190), (Screen.height * 0.02f), 60, 60), "Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (GUI.Button(new Rect((Screen.width - 280), (Screen.height * 0.02f), 60, 60), pause))
        {
            isPaused = !isPaused;
            if (isPaused == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
