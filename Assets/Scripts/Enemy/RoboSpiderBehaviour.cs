using System.Collections;
using UnityEngine;

public class RoboSpiderBehaviour : MonoBehaviour
{
    public bool flip = false;
    public bool aimAtPlayer;
    public bool stayInPlace;
    public short maxSteps;
    public float walkSpeed;
    public float moveTime;
    public float stopTime; // 1 or 2. if less then 1. make runMultiplier.
    //Moving
    private short steps = 0;
    private bool moving = false;
    private bool h_facingRight = false;
    private float direction;
    private bool startWalkting = true;
    private bool toRight;
    private bool waitToMove = true;
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
    


    void Awake()
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
        //if player nearby
        if (player != null && (((transform.position.x + 10) >= player.transform.position.x) && ((transform.position.x - 10) <= player.transform.position.x)))
        {
            headUp = true;
            int[] randmoveArray = {0, 1};
            int randmove = randmoveArray[Random.Range(0, randmoveArray.Length)];
            if (startWalkting && !moving)
                MovementManager(randmove);

            //Flip Enemy
            if (transform.position.x > player.transform.position.x && h_facingRight)
                FlipLeft();
            else if (transform.position.x <= player.transform.position.x && !h_facingRight)
                FlipRight();
        }
        else //If PLayer is not nearby
            headUp = false;

        //Movement
        if (moving)
        {
            if (!waitToMove && !PauseMenu.GameIsPaused)
                transform.position = new Vector2(transform.position.x + direction, transform.position.y);
            if (startWalkting)
            {
                startWalkting = false;
                StartCoroutine(waitToStop());
            }
        }

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
        if (aimAtPlayer && player != null && !PauseMenu.GameIsPaused)
        {
            if (shootUp)
            {
                if (angle > -45f)
                    angle -= 1f;
            }
            else
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

    private void MovementManager(int randmove)
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
                direction = walkSpeed;
                toRight = h_facingRight ? false : true;
                moving = true;
                steps += 1;  break; //move right
            case 1:
                direction = -walkSpeed;
                toRight = h_facingRight ? true : false;
                moving = true;
                steps -= 1; break; //move left
            case 2:
                if (startWalkting)
                {
                    startWalkting = false;
                    StartCoroutine(waitToStop());
                }
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

    IEnumerator waitToStop()
    {
        yield return new WaitForSeconds(1f);
        waitToMove = false;
        Shoot();
        yield return new WaitForSeconds(moveTime);
        anim.SetTrigger("standStill");
        moving = false;

        yield return new WaitForSeconds(stopTime);
        startWalkting = true;
        waitToMove = true;
    }
}