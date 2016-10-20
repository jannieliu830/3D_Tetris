using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowSliderValue : MonoBehaviour {

    public Text Difficulity;
	// Use this for initialization
	void Update () {

        Difficulity.text = Group.gameDifficulty.ToString();
	}
}
