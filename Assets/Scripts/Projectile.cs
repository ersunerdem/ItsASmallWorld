using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float moveSpeed = 6f;
    public float timeToDestroy = 3f;
    public bool isFire = false;

    void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    void FixedUpdate () {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);

        Health[] hs = GameObject.FindObjectsOfType<Health>();
        foreach (Health h in hs)
        {
            if (h.gameObject.GetComponent<PlayerController>())
            {
                //Do nothing
            }
            else
            {
                if (Vector3.Distance(transform.position, h.transform.position) < 1f)
                {
                    Debug.Log("Attacked " + h.transform.name);
                    h.TakeDamage(25f);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Ice>())
        {
            col.gameObject.GetComponent<Animator>().SetTrigger("Melt");
            Destroy(col.gameObject, 1f);
            
        }
        Destroy(this.gameObject);
    }
}
