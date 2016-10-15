using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Canvas QuitMenu;
    public Canvas GuideMenu;
    public Button StartButton;
    public Button ExitButton;
    public Button GuideButton;


	// Use this for initialization
	void Start () {
        QuitMenu = QuitMenu.GetComponent<Canvas>();
        GuideMenu = GuideMenu.GetComponent<Canvas>();
        StartButton = StartButton.GetComponent<Button>();
        ExitButton = ExitButton.GetComponent<Button>();
        GuideButton = GuideButton.GetComponent<Button>();

        QuitMenu.enabled = false;
        GuideMenu.enabled = false;
	}
	
    public void ExitPressed()
    {
        QuitMenu.enabled = true;
        GuideMenu.enabled = false;
        StartButton.enabled = false;
        ExitButton.enabled = false;
        GuideButton.enabled = false;
    }

    public void NoPressed()
    {
        QuitMenu.enabled = false;
        GuideMenu.enabled = false;
        StartButton.enabled = true;
        ExitButton.enabled = true;
        GuideButton.enabled = true;
    }

    public void YesPressed()
    {
        Application.Quit();
    }

    public void startLevel()
    {
        Debug.Log("PRESSED");
        SceneManager.LoadScene(1);
    }

    public void GuidePressed()
    {
        GuideMenu.enabled = true;
        QuitMenu.enabled = false;
        StartButton.enabled = false;
        GuideButton.enabled = false;
        ExitButton.enabled = false;
    }

    public void BackPressed()
    {
        GuideMenu.enabled = false;
        QuitMenu.enabled = false;
        StartButton.enabled = true;
        ExitButton.enabled = true;
        GuideButton.enabled = true;
    }
}
