using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    //public GameObject impactEffect;
    public int damage = 10;
    private float bulletAlive = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        StartCoroutine(DestroySelf());
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy!=null)
        {
            enemy.TakeDamage(damage);
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);  //Wenn die Kugel eine Aufprallanimation haben soll.
        if((!(hitInfo.tag == "Platform")) && (!(hitInfo.tag == "Trigger")))
            Destroy(gameObject);
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(bulletAlive);
        Destroy(gameObject);
        
    }
}
