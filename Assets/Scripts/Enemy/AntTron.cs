using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntTron : MonoBehaviour
{

    public bool stayOnLedge = false;

    //Movement
    public float rhSpeed;
    private bool h_facingRight = false;
    private float fallMultiplayer = 2;
    private bool moveRight = false;
    //Animation
    private bool MoveAnim = true;
    //Collision
    private int interactLayer = 1 << 12;    //Ground Layermask public LayerMask groundLayer;
    public float collisionRadius;
    public Color gizmoColor = Color.red;
    public Color gizmoColor2 = Color.blue;
    private bool onRightWall;
    private bool onLeftWall = true;
    private Vector2 rightOffset = new Vector2(0.5f, 0.9f);
    private Vector2 leftOffset = new Vector2(-0.5f, 0.9f);
    //If stayOnLedge
    private Vector2 rightGroundOffset = new Vector2(0.5f, -0.7f);  //95
    private Vector2 leftGroundOffset = new Vector2(-0.5f, -0.7f);
    private bool leftPit = true;
    private bool rightPit = true;

    private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(10, 15, true);
        Physics2D.IgnoreLayerCollision(15, 15, true);
        player = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, interactLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, interactLayer);
        if (stayOnLedge)
        {
            rightPit = Physics2D.OverlapCircle((Vector2)transform.position + rightGroundOffset, collisionRadius, interactLayer);
            leftPit = Physics2D.OverlapCircle((Vector2)transform.position + leftGroundOffset, collisionRadius, interactLayer);
        }


        if ((!leftPit || onLeftWall) && !moveRight)
        {
            moveRight = true;
        }
        else if ((!rightPit || onRightWall) && moveRight)
        {
            moveRight = false;
        }

        if (!moveRight) //Walk left
        {
            Vector2 direction = new Vector2(-1, 0);
            FlipLeft();
            Walk(direction);
        }
        else if(moveRight) //Walk right
        {
            Vector2 direction = new Vector2(1, 0);
            FlipRight();
            Walk(direction);
        }


        if (rb.velocity.y < 0)   //Sets Fallspeed
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplayer * Time.deltaTime;

        anim.SetBool("AntMove", MoveAnim);
    }

    public void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * rhSpeed, rb.velocity.y);
    }

    private void FlipLeft()
    {
        if (h_facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            rightOffset = new Vector2(0.5f, 0.9f);
            leftOffset = new Vector2(-0.5f, 0.9f);
            rightGroundOffset = new Vector2(0.5f, -0.7f);
            leftGroundOffset = new Vector2(-0.5f, -0.7f);
            h_facingRight = false;
        }
    }
    private void FlipRight()
    {
        if (!h_facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            rightOffset = new Vector2(0.6f, 0.9f);
            leftOffset = new Vector2(-0.5f, 0.9f);
            rightGroundOffset = new Vector2(0.6f, -0.7f);
            leftGroundOffset = new Vector2(-0.5f, -0.7f);
            h_facingRight = true;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.color = gizmoColor2;
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftGroundOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightGroundOffset, collisionRadius);

    }
}
