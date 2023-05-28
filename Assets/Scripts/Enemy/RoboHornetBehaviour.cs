using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboHornetBehaviour : MonoBehaviour
{
    private GameObject player;

    public float rhSpeed;
    public float moveTime;
    public float stopTime;
    private bool moving = false;
    private bool h_facingRight = false;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }


    void Update()
    {
        //Enemy movement
        if(player!=null && (((transform.position.x + 10) >= player.transform.position.x) && ((transform.position.x - 10) <= player.transform.position.x)))
        {
            Vector2 trgPosition = player.transform.position;
            Vector2 enemyPosition = new Vector2((transform.position.x), (transform.position.y));



            if (enemyPosition.y > trgPosition.y && !moving) //Move Down
            {
                int[] randmoveArray = { 0, -1, -2, -3 };
                int randmove = randmoveArray[Random.Range(0, randmoveArray.Length)];

                if (enemyPosition.x > trgPosition.x) //Move Left
                {
                    moving = true;
                    Vector2 direction = new Vector2(-1, randmove);
                    FlipLeft();
                    StartMoving(direction);
                }
                else //Move Right
                {
                    moving = true;
                    Vector2 direction = new Vector2(1, randmove);
                    FlipRight();
                    StartMoving(direction);
                }
            }
            else if (enemyPosition.y < trgPosition.y && !moving) //Move Up
            {
                int[] randmoveArray = { 0, 1, 2, 3 };
                int randmove = randmoveArray[Random.Range(0, randmoveArray.Length)];

                if (enemyPosition.x > trgPosition.x) //Move Left
                {
                    moving = true;
                    Vector2 direction = new Vector2(-1, randmove);
                    FlipLeft();
                    StartMoving(direction);
                }
                else //Move right
                {
                    moving = true;
                    Vector2 direction = new Vector2(1, randmove);
                    FlipRight();
                    StartMoving(direction);
                }
            }
        }
        
    }


    void StartMoving(Vector2 dir)
    {
        
        rb.velocity = new Vector2((dir.x * rhSpeed), dir.y);
        StartCoroutine(waitToStop());
    }

    private void FlipLeft()
    {
       if(h_facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            h_facingRight = false;
        }
    }

    private void FlipRight()
    {
        if (!h_facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            h_facingRight = true;
        }
    }


    IEnumerator waitToStop()
    {
        yield return new WaitForSeconds(moveTime);
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(stopTime);
        moving = false;
    }
}
