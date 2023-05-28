using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Playermovement : MonoBehaviour
{

    float horizontalMove = 0f;  //Momentane Geschwindigkeit wird hier gespeichert
    public float runSpeed = 2f;    //Dazu multiplizierte Geschwindigkeit. (Endgeschwindigkeit)
    private float runMultiplier = 1.0f;
    [HideInInspector] public float actualRunSpeed;
    public float jumpPower;
    public float fallMultiplayer = 2;
    public float lowJumpMultiplayer = 1;
    public static bool m_FacingRight;
    public int side;
    public bool moveDirectionRight;
    public int maxDeathSec = 3;
    private int deathsec;
    private bool gotCrushed;
    private bool startCountdown;
    private bool countUp;
    
    private bool blockCount = false;
    
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerCollision playercollision;
    private Player player;

    public bool DEBUG_MODE = false;
    [Range(0f, 3f)] public float DEBUG_MODE_SPEED = 0.5f;
   void Start()
    {
        //Components
        anim = GetComponentInChildren<Animator>();  
        rb = GetComponent<Rigidbody2D>();
        playercollision = GetComponent<PlayerCollision>();
        player = GetComponent<Player>();
        actualRunSpeed = runSpeed;

        m_FacingRight = true;
        moveDirectionRight = true;
        deathsec = maxDeathSec;
        gotCrushed = false;
        startCountdown = true;
        countUp = true;
    }

    void Update()
    {
        if(Player.PlayerAlive)
        {
            if(DEBUG_MODE)
            {
                //Manuel movement

                if (Input.GetKey("d"))
                    transform.position = new Vector2(transform.position.x + DEBUG_MODE_SPEED, transform.position.y);
                else if (Input.GetKey("a"))
                    transform.position = new Vector2(transform.position.x - DEBUG_MODE_SPEED, transform.position.y);


                if (Input.GetKey("w"))
                    transform.position = new Vector2(transform.position.x , transform.position.y + DEBUG_MODE_SPEED);
                else if (Input.GetKey("s"))
                    transform.position = new Vector2(transform.position.x, transform.position.y - DEBUG_MODE_SPEED);

                if(Input.GetKey("space"))
                    rb.velocity = Vector3.zero;

            }
            else  //---------------------------------
            {
                if (PlayerCollision.onRightWall)
                {
                    moveDirectionRight = false;
                }
                if (PlayerCollision.onLeftWall)
                {
                    moveDirectionRight = true;
                }

                //Automatic moving and turning if Player touches a Wall
                if (Input.GetKey("s"))
                {
                    if (startCountdown)
                    {
                        startCountdown = false;
                        countUp = false;
                        if (!blockCount)
                            StartCoroutine(WaitForStandStill(deathsec));
                    }
                    horizontalMove = 0;
                }
                else if (moveDirectionRight)
                {
                    if (!startCountdown && deathsec <= maxDeathSec)
                    {
                        startCountdown = true;
                        countUp = true;
                        if (!blockCount)
                            StartCoroutine(WaitForStandStill(deathsec));
                    }
                    horizontalMove = 1;
                }
                else if (!moveDirectionRight)
                {
                    if (!startCountdown && deathsec <= maxDeathSec)
                    {
                        startCountdown = true;
                        countUp = true;
                        if (!blockCount)
                            StartCoroutine(WaitForStandStill(deathsec));
                    }
                    horizontalMove = -1;
                }

                //Move function
                float verticalMove = Input.GetAxis("Vertical");
                Vector2 direction = new Vector2(horizontalMove, verticalMove);
                Run(direction);

                //Jump 
                if (Input.GetButtonDown("Jump"))
                {
                    if (playercollision.onGround)
                    {
                        anim.SetTrigger("Jump");
                        Jump(Vector2.up);
                    }
                }

                //Animations
                anim.SetFloat("HorizontalAxes", horizontalMove); //If Players movement is more or less them 0.001/-0.001 -> run Animation. 
                anim.SetBool("OnGround", playercollision.onGround);

                if (rb.velocity.y < 0)   //Sets Fallspeed
                    rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplayer * Time.deltaTime;
                if (rb.velocity.y > 0 && !Input.GetButton("Jump")) //Sets the shortjump hight
                    rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplayer * Time.deltaTime;

                
            } //---------------------------------
            
        }
        else
        {
            rb.velocity = Vector3.zero;
            if(!gotCrushed)
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.03f);
            else
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.007f);
        }

        if(playercollision.crushed && !gotCrushed)
        {
            gotCrushed = true;
            player.Crushed();
        }
    }


    public void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpPower;
    }

    public void Run(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * runSpeed, rb.velocity.y);
    }

    IEnumerator WaitForStandStill(int sec)
    {
        blockCount = true;

        if (sec >= 0)
            player.StandStill(sec);
        else
            player.Dead();

        yield return new WaitForSeconds(1);

        if (sec >= 0 && !countUp)
        {
            startCountdown = true;
            --deathsec;
        }
        else if (sec <= 3 && countUp)
        {
            startCountdown = false;
            ++deathsec;
        }

        if (deathsec == maxDeathSec + 1)
        {
            --deathsec;
            startCountdown = true;
        }
        blockCount = false;
    }

    public void SetRunMultiplier(float multipier)
    {
        anim.SetFloat("Run_Multiplier", (runMultiplier * multipier));
    }
    public float GetRunMuliplier()
    {
        return runMultiplier;
    }

    /*
    public void ResetMovement()
    {
        m_FacingRight = true;
        moveDirectionRight = true;
        deathsec = maxDeathSec;
        gotCrushed = false;
        startCountdown = true;
        countUp = true;
    }
    */
}
