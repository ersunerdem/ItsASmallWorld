using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDemonAI : MonoBehaviour {
    public Transform anchor;
    public float targetMoveRadius = 20f;
    public float moveSpeed = 8f;
    public float attackDist;
    public Transform projectile;
    public Vector3 targetPos;
    bool canChooseTarget = true;
	// Use this for initialization
	void Start () {
        canChooseTarget = true;
    }

    private void Awake()
    {
        StartCoroutine(Attack());
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void OnDestroy()
    {
        Debug.Log("You won!");
    }

    void Move()
    {
        if (canChooseTarget)
        {
            StartCoroutine(PickTarget());
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);
    }

    IEnumerator PickTarget()
    {
        canChooseTarget = false;
        float xOff = Random.Range(-targetMoveRadius, targetMoveRadius);
        float yOff = Random.Range(-targetMoveRadius, targetMoveRadius);
        targetPos = new Vector3(anchor.position.x + xOff, anchor.position.y + yOff, transform.position.z);
        yield return new WaitForSeconds(5f);
        canChooseTarget = true;
    }
}
