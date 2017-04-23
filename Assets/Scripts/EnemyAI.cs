using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public Transform player;
    public float moveSpeed = 3f;
    public float seekDist;
    public float attackDist = 0.5f;
    public enum AttackMode {IDLE, ATTACK };
    public AttackMode mode;
    private bool canAttack = true;
    public float attackTime = 2f;
    public float damage = 10f;
	// Use this for initialization
	void Start () {
        mode = AttackMode.IDLE;
        canAttack = true;
        if(player == null)
        {
            player = GameObject.FindObjectOfType<PlayerController>().transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.identity;
        float dist = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(player.position.x, player.position.y, 0));
		if(mode == AttackMode.IDLE)
        {
            if (dist <= seekDist)
            {
                mode = AttackMode.ATTACK;
            }
        }
        else if(mode == AttackMode.ATTACK)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            if(dist <= attackDist && canAttack)
            {
                //Send message to take damage
                player.GetComponent<Health>().TakeDamage(damage);
                StartCoroutine(attackTimer());
            }
        }
	}

    IEnumerator attackTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }
}
