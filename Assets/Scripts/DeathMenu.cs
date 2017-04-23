using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {
    public GUISkin skin;
    public Texture2D tex;
    // Use this for initialization
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);
        GUI.skin = skin;
        if (GUI.Button(new Rect(Screen.width - 500, 3 * Screen.height / 4, 150, 40), "RETRY"))
        {
            SceneManager.LoadScene("ItsASmallWorldGame", LoadSceneMode.Single);
        }
    }
}
