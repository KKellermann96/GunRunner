using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public float delay = 0.5f;
    private float time = 0.0f;
    private bool delaypass = true;

    // Update is called once per frame
    void Update()
    {
        if (delaypass)
        {
            if (Input.GetButton("Fire1"))
            {
                shoot();
                delaypass = false;
                time = 0.0f;
            }
        }
        else
        {
            time += Time.deltaTime;
            if (time >= delay)
            {
                time = 0.0f;
                delaypass = true;
            }
        }
    }


    void shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }
}
