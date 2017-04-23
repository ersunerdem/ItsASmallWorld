using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public Transform enemyHolder;
    public Transform cutScenePlayerLocation;
    public Transform mentor;
    public Transform mentorsGem;
    public string[] pregemDialog;
    public string[] postgemDialog;
    public float cutsceneTime;
    public Transform[] toDestroy;
    public Transform[] toEnable;
    Transform player;
    public bool isCompleted = false;
    public bool isFinalLevel = false;
    public int levelNum = 0;
    public Transform levelCompleteSound;
	
	void Start () {
        isCompleted = false;
        cutsceneTime = (postgemDialog.Length + pregemDialog.Length) * 4;
        player = GameObject.FindObjectOfType<PlayerController>().transform;
    }
	
	void Update () {
        if (!isCompleted)
        {
            if(enemyHolder.childCount == 0)
            {
                isCompleted = true;
                if (isFinalLevel)
                {
                    SceneManager.LoadScene("VictoryScreen", LoadSceneMode.Single);
                }
                else
                {
                    Instantiate(levelCompleteSound, transform.position, transform.rotation);
                    player.GetComponent<PlayerController>().direction = 1;
                    //Cutscene time
                    player.GetComponent<PlayerController>().canControl = false;
                    player.position = cutScenePlayerLocation.position;
                    Dialog d = player.GetComponent<Dialog>();
                    foreach (string s in pregemDialog)
                    {
                        d.CreateMessage(s);
                    }
                    StartCoroutine(Cutscene());
                    foreach (string s in postgemDialog)
                    {
                        d.CreateMessage(s);
                    }
                    foreach (Transform t in toDestroy)
                    {
                        Destroy(t.gameObject);
                    }
                    foreach (Transform t in toEnable)
                    {
                        t.gameObject.SetActive(true);
                    }

                    if (levelNum == 2)//Arrow
                        player.GetComponent<PlayerController>().knowsArrow = true;
                    if (levelNum == 3)//Fire
                        player.GetComponent<PlayerController>().knowsFire = true;
                    if (levelNum == 4)//Teleport
                        player.GetComponent<PlayerController>().knowsTeleport = true;
                }
            }
        }
	}

    IEnumerator Cutscene()
    {
        Transform gem = Instantiate(mentorsGem, new Vector3(mentor.position.x + 1f, mentor.position.y, mentor.position.z), Quaternion.identity);
        //gem.position = Vector3.MoveTowards(new Vector3(mentor.position.x + 1f, mentor.position.y, mentor.position.z), player.position, cutsceneTime);
        //Moving the gem
        yield return new WaitForSeconds(cutsceneTime);
        Destroy(gem.gameObject);
        player.GetComponent<PlayerController>().canControl = true;
    }
}
