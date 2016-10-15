using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GamePlayButton : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect((Screen.width - 100), (Screen.height * 0.02f), 60, 60), "Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
