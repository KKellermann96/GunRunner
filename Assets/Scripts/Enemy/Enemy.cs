using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
    public GameObject deathEffect;
    public int damage;
    public bool flipY;
    [SerializeField] List<SpriteRenderer> sr;
    private Material matFlash;
    private Material[] matDefault;

    private GameObject instEffect;

    void Start()
    {
        //Flash material set
        matDefault = new Material[sr.Count];
        matFlash = Resources.Load("Hit", typeof(Material)) as Material;
        for (int i = 0; i < sr.Count; ++i)
            matDefault[i] = sr[i].material; //Muss eventuell in TakeDamage rein, weil die Farbe sich im laufe der Zeit ändert. 
    }

    //Deal Damage on hit
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

        public void TakeDamage(int damage)
    {
        health -= damage;

        //Flash Effect
        for (int i = 0; i < sr.Count; ++i)
            sr[i].material = matFlash;

        if (health <= 0)
            Die();
        else
            Invoke("ResetMaterial", 0.05f); //Reset Flash after time
    }

    void ResetMaterial()
    {
        for (int i = 0; i < sr.Count; ++i)
            sr[i].material = matDefault[i];
    }

    void Die()
    {
        if (flipY)
            instEffect = (GameObject)Instantiate(deathEffect, new Vector2(transform.position.x, (transform.position.y - 1)), Quaternion.identity);   //Explosion
        else
            instEffect = (GameObject) Instantiate(deathEffect, new Vector2(transform.position.x, (transform.position.y + 1)), Quaternion.identity);   //Explosion
        if(gameObject.name == "Rhinobo_beetle1")
            Destroy(gameObject.transform.parent.gameObject);
        else
            Destroy(gameObject); //Delete Enemy
        Destroy(instEffect, 2); //Delete DeathEffect
    }
}