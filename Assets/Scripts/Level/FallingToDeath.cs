using System.Collections;
using UnityEngine;

public class FallingToDeath : MonoBehaviour
{
    private Player player;
    private GameObject a_Camera;
    private CameraSwitch maincamera;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        a_Camera = GameObject.Find("Main Camera");
        maincamera = a_Camera.GetComponent<CameraSwitch>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            StartCoroutine(Fall());
            maincamera.SwitchToVcam2();
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.5f);
        player.InstantDeath();
        
    }
}
