using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GamePlayButton : MonoBehaviour
{
    public GUISkin skin;

    public bool isPaused;
    public bool isGameOver;
    private Animator anim;
    public Texture pause;
    void OnGUI()
    {
        //Menu button
        if (GUI.Button(new Rect((Screen.width - 100), (Screen.height * 0.02f), 60, 60), "Menu", skin.button)) 
        {
            isPaused = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
        //Restart button
        if (GUI.Button(new Rect((Screen.width - 190), (Screen.height * 0.02f), 60, 60), "Restart", skin.button)) 
        {
            isPaused = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //Pause button
        if (GUI.Button(new Rect((Screen.width - 280), (Screen.height * 0.02f), 60, 60), pause, skin.button)) 
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

        //Game Score display
        GUI.Box(new Rect(100, (Screen.height * 0.02f), 100, 100), "Total Score: \n" + Grid.gameScore.ToString(),skin.box);
    }
}
