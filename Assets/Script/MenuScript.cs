using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Canvas QuitMenu;
    public Canvas GuideMenu;
    public Canvas SettingMenu;
    public Button StartButton;
    public Button ExitButton;
    public Button GuideButton;
    public Button SettingButton;
    public Toggle MenuMute;
    public Toggle GameMute;
    public Slider Difficulty;

    public AudioSource menu_music;    
    
	// Use this for initialization
	void Start () {
        QuitMenu = QuitMenu.GetComponent<Canvas>();
        GuideMenu = GuideMenu.GetComponent<Canvas>();
        SettingMenu = SettingMenu.GetComponent<Canvas>();
        StartButton = StartButton.GetComponent<Button>();
        ExitButton = ExitButton.GetComponent<Button>();
        GuideButton = GuideButton.GetComponent<Button>();
        SettingButton = SettingButton.GetComponent<Button>();
        MenuMute = MenuMute.GetComponent<Toggle>();
        GameMute = GameMute.GetComponent<Toggle>();

        QuitMenu.enabled = false;
        GuideMenu.enabled = false;
        SettingMenu.enabled = false;

        Difficulty.value = Group.gameDifficulty;
        //initialize the BGM
        menu_music.mute = !MenuMute.isOn;
    }

    void Update()
    {
        if (GamePlayButton.musicMute)
        {
            GameMute.isOn = false;
        }
        else
        {
            GameMute.isOn = true;
        }
    }
	
    public void ExitPressed()
    {
        QuitMenu.enabled = true;
        GuideMenu.enabled = false;
        StartButton.enabled = false;
        SettingMenu.enabled = false;
        ExitButton.enabled = false;
        GuideButton.enabled = false;
        SettingButton.enabled = false;
    }

    public void NoPressed()
    {
        QuitMenu.enabled = false;
        GuideMenu.enabled = false;
        SettingMenu.enabled = false;
        StartButton.enabled = true;
        ExitButton.enabled = true;
        GuideButton.enabled = true;
        SettingButton.enabled = true;
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
        SettingMenu.enabled = false;
        StartButton.enabled = false;
        GuideButton.enabled = false;
        ExitButton.enabled = false;
        SettingButton.enabled = false;
    }

    public void BackPressed()
    {
        GuideMenu.enabled = false;
        QuitMenu.enabled = false;
        SettingMenu.enabled = false;
        StartButton.enabled = true;
        ExitButton.enabled = true;
        GuideButton.enabled = true;
        SettingButton.enabled = true;
    }

    public void SettingPressed()
    {
        SettingMenu.enabled = true;
        GuideMenu.enabled = false;
        QuitMenu.enabled = false;
        StartButton.enabled = false;
        ExitButton.enabled = false;
        GuideButton.enabled = false;
        SettingButton.enabled = false;
    }

    public void S_backPressed()
    {
        GuideMenu.enabled = false;
        QuitMenu.enabled = false;
        SettingMenu.enabled = false;
        StartButton.enabled = true;
        ExitButton.enabled = true;
        GuideButton.enabled = true;
        SettingButton.enabled = true;
    }

    public void MenuToggle()
    {
        if(MenuMute.isOn == true)
        {
            menu_music.mute = false;
            
        }
        else
        {
            menu_music.mute = true;
        }
    }

    public void GameToggle()
    {
        if(GameMute.isOn == true)
        {
            GamePlayButton.musicMute = false;
        }
        else
        {
            GamePlayButton.musicMute = true;
            Debug.Log("pressed");
        }

    }

    public void ChangeDifficulty()
    {
        Group.gameDifficulty = (int)Difficulty.value;
    }
}
