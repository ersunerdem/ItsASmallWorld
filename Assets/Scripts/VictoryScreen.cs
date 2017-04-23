using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour {
    public GUISkin skin;
    public Texture2D tex;
    //AudioSource src;

    private void Start()
    {
        //src = transform.GetComponent<AudioSource>();
    }
    // Use this for initialization
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);
        GUI.skin = skin;
        if (GUI.Button(new Rect(Screen.width - 500, 3 * Screen.height / 4, 150, 40), "RETURN"))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
