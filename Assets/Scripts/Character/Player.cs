using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Player : MonoBehaviour
{
    [HideInInspector] public static bool PlayerAlive;
    public int maxHealth = 3;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public bool invulnerable = false;

    public HealthBar hB;

    //[HideInInspector] public bool death = false;
    public Light2D shineEffect;
    [SerializeField] List<GameObject> expParts;
    [SerializeField] List<GameObject> deathcount;

    private Animator anim;

    [SerializeField] List<Renderer> rend;
    [SerializeField] List<GameObject> bodyParts;

    private Material matinvisible;
    private Material matShine;
    private Material[] matDefault = new Material[9];
    private Collider2D m_coll;
    private GameObject deathScreen;

    void Start()
    {
        PlayerAlive = true;
        currentHealth = maxHealth;
        hB = GameObject.Find("Canvas").GetComponentInChildren<HealthBar>();
        anim = GetComponentInChildren<Animator>();
        
        matinvisible = Resources.Load("Empty", typeof(Material)) as Material;
        matShine = Resources.Load("Shine", typeof(Material)) as Material;
        for (int i = 0; i < rend.Count; ++i)
            matDefault[i] = rend[i].material;
        m_coll = gameObject.GetComponent<Collider2D>();
        deathScreen = GameObject.Find("Canvas").transform.Find("DeathScreen").gameObject;
        deathScreen.SetActive(false);
    }
 

    public void StandStill(int sec)
    {
        Instantiate(deathcount[sec]);
    }

    public void TakeDamage(int damage, bool noInvulnerability = false)
    {
        if (!invulnerable && PlayerAlive)
        {
            anim.SetTrigger("HitTrigger");
            currentHealth -= damage;
            hB.SetHealth(currentHealth);    //Set Healthbar 
            if(currentHealth<=0)
                StartCoroutine(Die("dying"));
            else
            {
                //Start transparent
                for (int i = 0; i < rend.Count; ++i)
                    rend[i].material = matinvisible;
                invulnerable = true;
                float t = noInvulnerability ? 0.2f : 3f;
                Invoke("ResetInvulnerability", t);
            }
               
            anim.SetTrigger("HitEnd");
        }
    }
    public void ResetInvulnerability()
    {
        invulnerable = false;
        for (int i = 0; i < rend.Count; ++i)
            rend[i].material = matDefault[i];
    }

    public void Dead()
    {
        m_coll.enabled = !m_coll.enabled;
        StartCoroutine(Die("dying"));
    }

    public void InstantDeath()
    {
        PlayerAlive = false;
        Destroy(gameObject);
        StartDeathScreen();
    } 

    public void Crushed()
    {
        m_coll.enabled = !m_coll.enabled;
        StartCoroutine(Die("crushing"));
    }

    public void BoostColor(Color col, float colSpeed)
    {
        for (int i = 0; i < bodyParts.Count; ++i)
            LeanTween.color(bodyParts[i], col, colSpeed);
    }
    public void ResetColor(float colSpeed)
    {
        for (int i = 0; i < bodyParts.Count; ++i)
            LeanTween.color(bodyParts[i], Color.white, colSpeed);
    }

    private void StartDeathScreen()
    {
        deathScreen.SetActive(true);
    }
    IEnumerator Die(string cause_of_death)
    {
        PlayerAlive = false;
        anim.SetTrigger(cause_of_death);
        for (int i = 0; i < bodyParts.Count; ++i)
            LeanTween.color(bodyParts[i], Color.white, 0);
        for (int i = 0; i < rend.Count; ++i)
            rend[i].material = matShine;
        Vector2 lightPos = new Vector2(transform.position.x, transform.position.y + 1);
        Light2D lit = Instantiate(shineEffect, lightPos, Quaternion.identity) as Light2D;   //Explosion
        lit.transform.parent = GameObject.Find("Player").transform;

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        int[] scatter = { 2, 3, 4, 9, 11, 15};
        for (int i=0;i< expParts.Count; ++i)
        {
            int randX = scatter[Random.Range(0, scatter.Length)];
            int randY = scatter[Random.Range(0, scatter.Length)];
            GameObject part = Instantiate(expParts[i], new Vector2(transform.position.x, (transform.position.y + 2)), Quaternion.identity) as GameObject;
            if(i<4)
            {
                part.GetComponent<Rigidbody2D>().velocity = new Vector2(-randX, randY);
            }
            else if(i==4)
            {
                part.GetComponent<Rigidbody2D>().velocity = new Vector2(0, randY);
            }
            else
            {
                part.GetComponent<Rigidbody2D>().velocity = new Vector2(randX, randY);
            }
        }
        StartDeathScreen();
    }
}
