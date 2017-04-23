using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {
    public List<string> messageQueue;
    public GUISkin skin;
    bool checkQueue = true;
    // Use this for initialization
    private void Start()
    {
        checkQueue = true;
        messageQueue = new List<string>();
        //CreateMessage("test");
    }

	void OnGUI () {
        GUI.skin = skin;
        //GUI.Box(new Rect(10, 40, Screen.width - 20, Screen.height / 4), "test");
        if (messageQueue.Count > 0)
        {
            GUI.Box(new Rect(Screen.width / 7f, Screen.height - 70, Screen.width  * (5f/7f), 60), messageQueue[0]);
            if (checkQueue)
            {
                StartCoroutine(messageWait());
            }
            checkQueue = false;
        }
	}

    private void Update()
    {
        if (Input.GetButtonDown("Dialog"))
        {
            if (messageQueue.Count > 0)
            {
                messageQueue.RemoveAt(0);
                if (messageQueue.Count == 0)
                {
                    PlayerController pc = GameObject.FindObjectOfType<PlayerController>();
                    pc.canControl = true;
                }
            }
        }
    }

    IEnumerator messageWait()
    {
        yield return new WaitForSeconds(4f);
        if(messageQueue.Count > 0)
        {
            messageQueue.RemoveAt(0);
        }
        checkQueue = true;
    }
	
	public void CreateMessage(string s)
    {
        messageQueue.Add(s);
    }
}
