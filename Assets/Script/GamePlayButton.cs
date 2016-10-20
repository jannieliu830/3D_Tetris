using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GamePlayButton : MonoBehaviour
{
    public GUISkin skin;

    public static bool isPaused;
    public bool isGameOver;
    private Animator anim;
    public Texture pause;
    public Texture mute;
    public Texture play;
    public Canvas pauseScene;
    public AudioSource bg_music;
    public static bool musicMute;
    private Texture musicButton;
    public Camera mainCamera;

    void Start()
    {
        //initialize BGM and pauseScene
        bg_music.mute = musicMute;

        if (musicMute)
        {
            musicButton = mute;
        }
        else
        {
            musicButton = play;
        }

        pauseScene.enabled = false;
    }

    void OnGUI()
    {
        //Menu button
        if (GUI.Button(new Rect((Screen.width - 100), (Screen.height * 0.02f), 70, 60), "Menu", skin.button)) 
        {
            isPaused = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
        //Restart button
        if (GUI.Button(new Rect((Screen.width - 190), (Screen.height * 0.02f), 70, 60), "Restart", skin.button)) 
        {
            Grid.gameScore = 0;
            isPaused = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //Pause button
        if (GUI.Button(new Rect((Screen.width - 280), (Screen.height * 0.02f), 70, 60), pause, skin.button)) 
        {
            isPaused = !isPaused;
            if (isPaused == true)
            {
                Time.timeScale = 0;
                pauseScene.enabled = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseScene.enabled = false;
            }
        }
        //Mute button
        if (GUI.Button(new Rect((Screen.width - 370), (Screen.height * 0.02f), 70, 60), musicButton, skin.button))
        {
            musicMute = !musicMute;
            bg_music.mute = musicMute;
            if (musicMute)
            {
                musicButton = mute;
            }
            else
            {
                musicButton = play;
            }
        }
        //Back to origin
        if (GUI.Button(new Rect((Screen.width * 0.02f), (Screen.height * 0.90f), 100, 30),"O"))
        {
            mainCamera.transform.position = CameraControl.originPosition;
            mainCamera.transform.rotation = CameraControl.originQuaternion;
        }

        //Game Score display
        GUI.Box(new Rect(100, (Screen.height * 0.02f), 100, 100), "Total Score: \n" + Grid.gameScore.ToString(),skin.box);
    }
}
