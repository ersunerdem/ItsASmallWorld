using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsUpDisplay : MonoBehaviour {
    PlayerController pc;
    Health h;
    public Health shadowDemonH;
    public LevelController lastLevel;
    public GUISkin blackBar;
    public GUISkin redBar;
    public GUISkin greenBar;

    // Use this for initialization
    void Start () {
        pc = gameObject.GetComponent<PlayerController>();
        h = gameObject.GetComponent<Health>();
	}


	void OnGUI () {
        //Draw health bar
        //Outer (background) for Player health bar
        float HPBarLen = 400f;
        GUI.skin = blackBar;
        //Debug.Log("Drawing health bar");
        GUI.Box(new Rect(Screen.width - (HPBarLen + 10f), 60, HPBarLen, 40), "");
        //Inner (foreground) for Player health bar
        float newHPBarLen = (h.currentHealth * HPBarLen) / h.maxHealth;
        GUI.skin = greenBar;
        GUI.Box(new Rect(Screen.width - (HPBarLen + 10f), 60, newHPBarLen, 40), "");

        if (lastLevel.isCompleted)
        {
            float HPBarLen2 = Screen.width * (3f / 4f);
            GUI.skin = blackBar;
            GUI.Box(new Rect(Screen.width / 8, 10, HPBarLen2, 40), "");
            float newHPBarLen2 = (shadowDemonH.currentHealth * HPBarLen2) / shadowDemonH.maxHealth;
            GUI.skin = redBar;
            GUI.Box(new Rect(Screen.width / 8, 10, newHPBarLen2, 40), "");
        }
	}
}
