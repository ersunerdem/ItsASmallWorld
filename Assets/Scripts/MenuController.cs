using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GUISkin skin;
    public Texture2D background;
    public Texture2D controls;
    string currentMenu = "main";

	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI () {
        if(currentMenu == "main")
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
            GUI.skin = skin;
            if (GUI.Button(new Rect(Screen.width - 500, 3 * Screen.height / 4, 150, 40), "PLAY!"))
            {
                SceneManager.LoadScene("ItsASmallWorldGame", LoadSceneMode.Single);
            }
            if (GUI.Button(new Rect(Screen.width - 500, 3 * Screen.height / 4 + 50, 150, 40), "CONTROLS"))
            {
                currentMenu = "controls";
            }
        }
        if(currentMenu == "controls")
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), controls);
            GUI.skin = skin;
            if (GUI.Button(new Rect(Screen.width - 500, 3 * Screen.height / 4, 150, 40), "BACK"))
            {
                currentMenu = "main";
            }
        }
	}
}
