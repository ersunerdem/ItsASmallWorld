using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Health : MonoBehaviour {
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public bool isPlayer;
    public bool isShadowDemon;
    public bool isGem;
    Health demonHealth;
    public Transform enemyDeathSound;
    public Transform playerHurtSound;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        if (isGem)
        {
            demonHealth = GameObject.FindObjectOfType<ShadowDemonAI>().gameObject.GetComponent<Health>();
        }
        if (transform.GetComponent<ShadowDemonAI>())
        {
            isShadowDemon = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0f)
        {
            //Die
            if (isPlayer)
            {
                //Load death level
                SceneManager.LoadScene("DeathScene", LoadSceneMode.Single);
            }
            else if (isGem)
            {
                demonHealth.TakeDamage(1000f);
                Destroy(this.gameObject);
            }
            else if (isShadowDemon)
            {
                //Load victory level
                SceneManager.LoadScene("VictoryScreen", LoadSceneMode.Single);
            }
            else
            {
                Instantiate(enemyDeathSound, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
	}

    public void TakeDamage(float damage)
    {
        if (isPlayer)
        {
            Instantiate(playerHurtSound, transform.position, transform.rotation);
            PlayerController pc = transform.GetComponent<PlayerController>();
            if (!pc.isShielding)
            {
                currentHealth -= damage;
            }
        }
        else
        {
            currentHealth -= damage;
        }
    }
}
