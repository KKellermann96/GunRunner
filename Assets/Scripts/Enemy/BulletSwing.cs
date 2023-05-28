using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSwing : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    //public GameObject impactEffect;
    public int damage = 1;
    public bool shootLeft = true;
    private float bulletAlive = 10f;

    void Start()
    {
        rb.velocity = shootLeft ? (transform.right * -speed) : (transform.right * speed);
        StartCoroutine(DestroySelf());
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
            //Instantiate(impactEffect, transform.position, transform.rotation);  //Wenn die Kugel eine Aufprallanimation haben soll.
            Destroy(gameObject);
        }

        
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(bulletAlive);
        Destroy(gameObject);

    }

}
