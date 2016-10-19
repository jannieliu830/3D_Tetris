using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

    public GUISkin skin;

    void OnGUI()
    {
        if (GUI.Button(new Rect((Screen.width * 0.10f), (Screen.height * 0.75f), (Screen.width * 0.10f), 100), "Back to Menu", skin.button)) 
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (GUI.Button(new Rect((Screen.width * 0.80f), (Screen.height * 0.75f), (Screen.width * 0.10f), 100), "Restart",skin.button))
        {
            SceneManager.LoadScene(1);
        }
    }
}
