using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private int interactLayer = ((1 << 12) | (1<<17));    //Ground Layermask public LayerMask groundLayer;
    private int groundInteractLayer = ((1 << 12) | (1 << 16) | (1<<17));
    private int celingInteractLayer = 1 << 17;
    [HideInInspector] public bool onGround;
    [HideInInspector] public bool crushed;
    public float collisionRadius;
    public Vector2 groundOffset;
    public Vector2 crushedOffset;
    public Vector2 crushedOffset2;


    public static bool onWall;
    public static bool onRightWall;
    public static bool onLeftWall;
    public Vector2 rightOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset2;
    public Vector2 leftOffset2;
    [HideInInspector] public int side;

    
    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + groundOffset, collisionRadius, groundInteractLayer);
        crushed = Physics2D.OverlapCircle((Vector2)transform.position + crushedOffset, collisionRadius, celingInteractLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + crushedOffset2, collisionRadius, celingInteractLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, interactLayer) 
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, interactLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset2, collisionRadius, interactLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset2, collisionRadius, interactLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, interactLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset2, collisionRadius, interactLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, interactLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset2, collisionRadius, interactLayer);
        side = onRightWall ? 1 : -1;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + groundOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset2, collisionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset2, collisionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position + crushedOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + crushedOffset2, collisionRadius);

    }
}
