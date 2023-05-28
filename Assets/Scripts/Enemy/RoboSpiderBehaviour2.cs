using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboSpiderBehaviour2 : MonoBehaviour
{
    public bool flip = false;
    public bool aimAtPlayer;
    public bool stayInPlace;
    public short maxSteps = 1;
    public float distance = 1;
    public float moveTime = 2f;

    //Moving
    private bool moving;
    
    private bool h_facingRight = false;
    private short steps = 0;
    private bool toRight; //Animation

    //Headpos
    private bool headUp = false;
    private float headPos;
    //Shooting
    private float angle;
    private float zAngle;
    private bool shootUp = false;
    //Components
    private GameObject player;
    private Animator anim;
    private Transform head, weapon;
    public Transform firepoint;
    public GameObject bulletPrefab;

    void Start()
    {
        GameObject spider;
        if (flip)
        {
            FlipY();
            Enemy enemyScript = GetComponent<Enemy>();
            enemyScript.flipY = true;
        }
        anim = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
        spider = gameObject.transform.Find("RoboSpiderBody1").gameObject;
        head = spider.transform.Find("Head1");
        weapon = head.transform.Find("Weapon");
        headPos = head.position.y;
        angle = weapon.rotation.z;
    }


    void Update()
    {
        if (player != null && (((transform.position.x + 10) >= player.transform.position.x) && ((transform.position.x - 10) <= player.transform.position.x)))
        {
            headUp = true;
            short randmove = (short) Random.Range(0, 1);
            if (!moving)
            {
                moving = true;
                MoveManager(randmove);
            }
                

            //Flip Enemy
            if (transform.position.x > player.transform.position.x && h_facingRight)
                FlipLeft();
            else if (transform.position.x <= player.transform.position.x && !h_facingRight)
                FlipRight();
        }
        else //PLayer is not nearby
            headUp = false;

        //Rise and lower the enemy head
        if (headUp)
        {
            if (!flip && (head.position.y < headPos + 0.15f))
                head.position = new Vector2(head.position.x, head.position.y + 0.01f);
            else if (flip && (head.position.y > headPos - 0.15f))
                head.position = new Vector2(head.position.x, head.position.y - 0.01f);
        }
        else
        {
            if (!flip && (head.position.y > headPos))
                head.position = new Vector2(head.position.x, head.position.y - 0.01f);
            else if (flip && (head.position.y < headPos))
                head.position = new Vector2(head.position.x, head.position.y + 0.01f);
        }

        //Aim at player
        if (aimAtPlayer && player != null)
        {
            if (shootUp)
            {
                if (angle > -45f)
                    angle -= 1f;
            } else
            {
                if (angle != 0)
                    angle += 1f;
            }

            if (!flip && h_facingRight)
                zAngle = Mathf.Abs(angle);
            else if (flip && h_facingRight)
                zAngle = angle;
            else if (flip)
                zAngle = Mathf.Abs(angle);
            else
                zAngle = angle;
            weapon.rotation = Quaternion.AngleAxis(zAngle, Vector3.forward);
        }

        anim.SetBool("toRight", toRight);
    }

    private void MoveManager(short randmove)
    {
        if (stayInPlace)
            randmove = 2;
        else if (steps == maxSteps)
            randmove = 1;
        else if (steps == -(maxSteps))
            randmove = 0;
        switch (randmove)
        {
            case 0:
                toRight = h_facingRight ? false : true;
                LeanTween.moveX(gameObject, (transform.position.x + distance), moveTime).setEaseInSine().setOnComplete(() =>
                {
                    anim.SetTrigger("standStill");
                    moving = false;
                    steps += 1;
                }); break; //move right
            case 1:
                toRight = h_facingRight ? true : false;
                LeanTween.moveX(gameObject, (transform.position.x - distance), moveTime).setEaseInSine().setOnComplete(() =>
                {
                    anim.SetTrigger("standStill");
                    moving = false;
                    steps -= 1;
                }); break; //move right
            case 2:
                /*
                if (startWalkting)
                {
                    startWalkting = false;
                    StartCoroutine(waitToStop());
                }
                */
                break; //stand still
        }
        if (randmove != 2)
            anim.SetTrigger("isMoving");

        //set Angle Pos
        if (!flip && (player.transform.position.y > transform.position.y + 1))
            shootUp = true;
        else if (flip && (player.transform.position.y < transform.position.y - 1))
            shootUp = true;
        else
            shootUp = false;
    }


    private void FlipLeft()
    {
        if (h_facingRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            h_facingRight = false;
        }
    }
    private void FlipRight()
    {
        if (!h_facingRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            h_facingRight = true;

        }
    }

    private void FlipY()
    {
        Vector3 newScale = transform.localScale;
        newScale.y *= -1;
        transform.localScale = newScale;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, weapon.rotation);
        bullet.GetComponent<BulletSwing>().shootLeft = !h_facingRight;
    }
}
