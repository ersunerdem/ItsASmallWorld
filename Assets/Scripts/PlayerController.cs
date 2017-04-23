using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Animator anim;
    public Camera cam;
    public Transform shieldUp;
    public Transform shieldDown;
    public Transform shieldRight;
    public Transform shieldLeft;
    public Transform swordUp;
    public Transform swordDown;
    public Transform swordRight;
    public Transform swordLeft;
    public Transform teleportPlayer;
    public Transform flameProjectile;
    public Transform arrowProjectile;
    public bool knowsArrow = false;
    public bool knowsTeleport = false;
    public bool knowsFire = false;
    float moveSpeed = 3.0f;
    bool isAttacking = false;
    public bool isShielding = false;
    bool isTeleporting = false;
    bool canShoot = false;
    public bool canControl = true;
    public int direction = -1;

	void Start () {
        direction = 1;
        anim.SetInteger("direction", direction);
        anim.SetInteger("speed", 0);

        shieldUp.gameObject.SetActive(false);
        shieldDown.gameObject.SetActive(false);
        shieldRight.gameObject.SetActive(false);
        shieldLeft.gameObject.SetActive(false);
        swordUp.gameObject.SetActive(false);
        swordDown.gameObject.SetActive(false);
        swordLeft.gameObject.SetActive(false);
        swordRight.gameObject.SetActive(false);
        teleportPlayer.gameObject.SetActive(false);

        canShoot = true;
    }

    void Update()
    {
        if (canControl)
        {
            ScreenshotController();
            ShieldController();
            SwordController();
            if (knowsTeleport)
                TeleportController();
            KeepRotation();
            if (knowsArrow)
                ArrowController();
            if (knowsFire)
                FireController();
        }
    }

	void FixedUpdate () {
        if (canControl)
        {
            Motion();
        }
	}

    void ScreenshotController()
    {
        if (Input.GetButtonDown("Screenshot"))
        {
            Application.CaptureScreenshot("Screenshot.png");
        }
    }

    void KeepRotation()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    void Motion()
    {
        if (isAttacking)
        {
            anim.SetInteger("speed", 0);
        }
        else if (isShielding || isTeleporting)
        {
            anim.SetInteger("speed", 0);
        }
        else
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                direction = 1;
                anim.SetInteger("direction", direction);
                anim.SetInteger("speed", 1);
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                direction = -1;
                anim.SetInteger("direction", direction);
                anim.SetInteger("speed", 1);
                transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                direction = 2;
                anim.SetInteger("direction", direction);
                anim.SetInteger("speed", 1);
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                direction = -2;
                anim.SetInteger("direction", direction);
                anim.SetInteger("speed", 1);
                transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetInteger("speed", 0);
            }
        }
    }

    void FireController()
    {
        if (Input.GetButtonDown("Fire"))
        {
            if (canShoot && !isTeleporting && !isShielding && !isAttacking)
            {
                Transform fire = Instantiate(flameProjectile, transform.position, Quaternion.identity);

                TeleportIgnoreCollision[] ts = GameObject.FindObjectsOfType<TeleportIgnoreCollision>();
                //ArrowIgnoreCollision[] agos = GameObject.FindObjectsOfType<ArrowIgnoreCollision>();

                if (direction == 1) //Up
                {
                    fire.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                else if (direction == -1) //Down
                {
                    fire.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                }
                else if (direction == 2) //Right
                {
                    fire.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else //Left
                {
                    fire.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                }

                foreach (TeleportIgnoreCollision t in ts)
                {
                    Physics2D.IgnoreCollision(fire.GetComponent<Collider2D>(), t.gameObject.GetComponent<Collider2D>());
                }

                Physics2D.IgnoreCollision(fire.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                StartCoroutine(shootTimer());
            }
        }
    }

    void ArrowController()
    {
        if (Input.GetButtonDown("Arrow"))
        {
            if (canShoot && !isTeleporting && !isShielding && !isAttacking)
            {
                Transform arrow = Instantiate(arrowProjectile, transform.position, Quaternion.identity);

                TeleportIgnoreCollision[] ts = GameObject.FindObjectsOfType<TeleportIgnoreCollision>();
                ArrowIgnoreCollision[] agos = GameObject.FindObjectsOfType<ArrowIgnoreCollision>();

                if (direction == 1) //Up
                {
                    arrow.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                else if(direction == -1) //Down
                {
                    arrow.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                }
                else if(direction == 2) //Right
                {
                    arrow.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else //Left
                {
                    arrow.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                }

                foreach (TeleportIgnoreCollision t in ts)
                {
                    Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), t.gameObject.GetComponent<Collider2D>());
                }

                foreach (ArrowIgnoreCollision a in agos)
                {
                    Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), a.gameObject.GetComponent<Collider2D>());
                }

                Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                StartCoroutine(shootTimer());
            }
        }
    }

    IEnumerator shootTimer()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    void ShieldController()
    {
        if (!isAttacking && !isTeleporting)
        {
            if (Input.GetButtonDown("Shield"))
            {
                isShielding = true;
                if (direction == -1) // down
                {
                    shieldDown.gameObject.SetActive(true);
                }
                else if (direction == 1) //up
                {
                    shieldUp.gameObject.SetActive(true);
                }
                else if (direction == 2) //right
                {
                    shieldRight.gameObject.SetActive(true);
                }
                else //left
                {
                    shieldLeft.gameObject.SetActive(true);
                }
            }
        }
        if (!Input.GetButton("Shield"))
        {
            shieldUp.gameObject.SetActive(false);
            shieldDown.gameObject.SetActive(false);
            shieldRight.gameObject.SetActive(false);
            shieldLeft.gameObject.SetActive(false);
            isShielding = false;
        }
    }

    void SwordController()
    {
        if (!isShielding && !isAttacking && !isTeleporting)
        {
            if (Input.GetButtonDown("Sword"))
            {
                isAttacking = true;
                if (direction == -1) // down
                {
                    swordDown.gameObject.SetActive(true);
                }
                else if (direction == 1) //up
                {
                    swordUp.gameObject.SetActive(true);
                }
                else if (direction == 2) //right
                {
                    swordRight.gameObject.SetActive(true);
                }
                else //left
                {
                    swordLeft.gameObject.SetActive(true);
                }

                Health[] hs = GameObject.FindObjectsOfType<Health>();
                foreach(Health h in hs)
                {
                    if(h.gameObject == this.gameObject)
                    {
                        //Do nothing
                    }
                    else
                    {
                        if(Vector3.Distance(transform.position, h.transform.position) < 1.75f)
                        {
                            Debug.Log("Attacked " + h.transform.name);
                            Vector3 diff = transform.position - h.transform.position;
                            h.transform.Translate(-diff);
                            h.TakeDamage(25f);
                        }
                    }
                }

                StartCoroutine(SwordAttack());
            }
        }
    }

    IEnumerator SwordAttack()
    {
        yield return new WaitForSeconds(0.25f);
        swordUp.gameObject.SetActive(false);
        swordDown.gameObject.SetActive(false);
        swordRight.gameObject.SetActive(false);
        swordLeft.gameObject.SetActive(false);
        isAttacking = false;
    }

    void TeleportController()
    {
        if(!isAttacking && !isShielding)
        {
            if (isTeleporting)
            {
                teleportPlayer.GetChild(0).localRotation = Quaternion.identity;
                if (Input.GetButtonDown("Escape"))
                {
                    teleportPlayer.gameObject.SetActive(false);
                    isTeleporting = false;
                }
                TeleportIgnoreCollision[] gos = GameObject.FindObjectsOfType<TeleportIgnoreCollision>();
                foreach(TeleportIgnoreCollision t in gos)
                {
                    Physics2D.IgnoreCollision(teleportPlayer.GetChild(0).GetComponent<Collider2D>(), t.gameObject.GetComponent<Collider2D>());
                }

                Physics2D.IgnoreCollision(teleportPlayer.GetChild(0).GetComponent<Collider2D>(), GetComponent<Collider2D>());
                

                Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(screenRay, out hit))
                {
                    Debug.Log("Mousing over object.");
                }
                else
                {
                    Vector3 pos = new Vector3(Input.mousePosition.x * 0.0175f, Input.mousePosition.y * 0.0175f, Input.mousePosition.z * 0.0175f);
                    teleportPlayer.localPosition = pos;

                    if (Input.GetButton("Fire1"))
                    {
                        transform.position = teleportPlayer.GetChild(0).position;
                        teleportPlayer.gameObject.SetActive(false);
                        isTeleporting = false;
                    }
                }

                
            }
            else
            {
                if (Input.GetButtonDown("Teleport"))
                {
                    isTeleporting = true;
                    teleportPlayer.gameObject.SetActive(true);
                }
            }
        }
    }
}
