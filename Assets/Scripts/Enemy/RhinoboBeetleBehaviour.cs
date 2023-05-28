using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoboBeetleBehaviour : MonoBehaviour
{
    public bool aggressive;
    private bool noticed = false;
    public bool stayOnLedge = false;
    
    //Movement
    public float rhSpeed;
    private bool h_facingRight = false;
    private float fallMultiplayer = 2;
    private bool moveRight = false;
    //Animation
    private bool MoveAnim = true;
    private bool StartRunAnim = false;
    private bool RunAnim = false;

    //Collision
    private int interactLayer = 1 << 12;    //Ground Layermask public LayerMask groundLayer;
    private int groundInteractLayer = ((1 << 12) | (1 << 16));
    public float collisionRadius;
    public Color gizmoColor = Color.red;
    public Color gizmoColor2 = Color.blue;
    private bool onRightWall;
    private bool onLeftWall = true;
    private Vector2 rightOffset = new Vector2(0.56f, 0.46f);
    private Vector2 leftOffset = new Vector2(-0.33f, 0.45f);
    //If stayOnLedge
    private Vector2 rightGroundOffset = new Vector2(0.56f, -0.7f);
    private Vector2 leftGroundOffset = new Vector2(-0.33f, -0.7f);
    private bool leftPit = true;
    private bool rightPit = true;

    public GameObject noticedEffect;
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
        if(stayOnLedge)
        {
            rightPit = Physics2D.OverlapCircle((Vector2)transform.position + rightGroundOffset, collisionRadius, groundInteractLayer);
            leftPit = Physics2D.OverlapCircle((Vector2)transform.position + leftGroundOffset, collisionRadius, groundInteractLayer);
        }


        if ((!leftPit || onLeftWall) && !moveRight)
        {
            moveRight = true;
        }
        else if((!rightPit || onRightWall) && moveRight)
        {
            moveRight = false;
        }

        if (!moveRight) //Walk left
        {
            Vector2 direction = new Vector2(-1, 0);
            FlipLeft();
            Walk(direction);
        }
        else //Walk right
        {
            Vector2 direction = new Vector2(1, 0);
            FlipRight();
            Walk(direction);
        }
        


        if(aggressive)
        {
            if (!noticed && player != null && (((transform.position.x + 5) >= player.transform.position.x) && ((transform.position.x - 5) <= player.transform.position.x)))
            {
                noticed = true;
                //Turn to Player and Notice
                if (player.transform.position.x < transform.position.x)
                {
                    if (h_facingRight)
                    {
                        FlipLeft();
                        moveRight = false;
                    }
                        
                }
                else
                {
                    if (!h_facingRight)
                    {
                        FlipRight();
                        moveRight = true;
                    }
                }
                GameObject noEff = (GameObject) Instantiate(noticedEffect, new Vector2((transform.position.x + 0.3f), (transform.position.y + 2.5f)), Quaternion.identity);
                StartCoroutine(StartRunning(noEff));
            }
        }


        if (rb.velocity.y < 0)   //Sets Fallspeed
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplayer * Time.deltaTime;

        anim.SetBool("BeetleMove", MoveAnim);
        anim.SetBool("BeetleStartRunning", StartRunAnim);
        anim.SetBool("BeetleRun", RunAnim);
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
            rightOffset = new Vector2(0.56f, 0.46f);
            leftOffset = new Vector2(-0.33f, 0.45f);
            h_facingRight = false;
        }
    }
    private void FlipRight()
    {
        if (!h_facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            rightOffset = new Vector2(0.32f, 0.46f);
            leftOffset = new Vector2(-0.57f, 0.45f);
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


    IEnumerator StartRunning(GameObject noEff)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(noEff);
        rhSpeed += 1;
        MoveAnim = false;
        StartRunAnim = true;
        yield return new WaitForSeconds(1.2f);
        StartRunAnim = false;
        RunAnim = true;
        rhSpeed += 2;
    }
}
