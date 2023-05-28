using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damage = 1;
    public bool upsidedown;
    public bool pointSide;
    private Playermovement pm;
    void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<Playermovement>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage, true);
            if (upsidedown)
                pm.Jump(new Vector2(0, -0.01f));
            else
                pm.Jump(Vector2.up);

            if (pointSide)
                pm.moveDirectionRight = !pm.moveDirectionRight;
        }
    }
}
