using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntTronFly : MonoBehaviour
{

    public bool Fly_Horizontal = true;
    public bool left_or_up = true;
    public bool Fly_In_Curve;
    public float FlySpeed; // for x and y
    public float flyDistance = 20f;
    public float curveDistance = 2f;
    public float curveSpeed = 0.5f;

    private bool FlyAnim = true;
    private bool h_facingRight = false;
    private Vector2 startPos;
    private Vector2 endPos;
    private GameObject player;
    private Animator anim;


    
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();
        startPos = transform.position;
        if (!left_or_up)
            flyDistance = -flyDistance;

        endPos = new Vector2(startPos.x - flyDistance, startPos.y + flyDistance);
        if (Fly_Horizontal)
            LeanTween.moveX(gameObject, (startPos.x - flyDistance), FlySpeed).setEaseInOutSine().setLoopPingPong();
        else
            LeanTween.moveY(gameObject, (startPos.y + flyDistance), FlySpeed).setEaseInOutSine().setLoopPingPong();

        if(Fly_In_Curve)
        {
            if(Fly_Horizontal)
                LeanTween.moveY(gameObject, (startPos.y + curveDistance), curveSpeed).setEaseInOutSine().setLoopPingPong();
            else
                LeanTween.moveX(gameObject, (startPos.x + curveDistance), curveSpeed).setEaseInOutSine().setLoopPingPong();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Fly_Horizontal)
        {
            if(left_or_up)
            {
                if (transform.position.x == startPos.x)
                    FlipLeft();
                if (transform.position.x == endPos.x)
                    FlipRight();
            }
            else
            {
                if (transform.position.x == startPos.x)
                    FlipRight();
                if (transform.position.x == endPos.x)
                    FlipLeft();
            }
        }
        else
        {
            if(player != null)
            {
                if (player.transform.position.x < transform.position.x)
                    FlipLeft();
                else
                    FlipRight();
            }
        }
    
        
        anim.SetBool("AntFly", FlyAnim);
    }


    private void FlipLeft()
    {
        if (h_facingRight)
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

}
