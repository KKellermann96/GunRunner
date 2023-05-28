using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{

    public GameObject door;
    public int closeSpeed = 3;
    private bool isOpen = true;
    private bool isClosing = false;
    private GameObject a_player;
    private GameObject curDoor;
    private Vector2 trg;

    private void Awake()
    {
        a_player = GameObject.Find("Player");
        curDoor = Instantiate(door, new Vector2(transform.position.x, transform.position.y + 2.9f), Quaternion.identity) as GameObject;
        curDoor.tag = "RestRoom";
        trg = new Vector2(transform.position.x, transform.position.y - 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(a_player && isOpen && isClosing == false)
        {
            if (a_player.transform.position.x > transform.position.x + 1)
                isOpen = false;
        }
        if(!isOpen)
        {
            float step = closeSpeed * Time.deltaTime;
            curDoor.transform.position = Vector2.MoveTowards(curDoor.transform.position, trg, step);
            if (!isClosing)
                StartCoroutine(WaitTillClosed());
        }
    }

    public void ResetDoor()
    {
        a_player = GameObject.Find("Player");
        isOpen = true;
        isClosing = false;
        Destroy(curDoor);
        curDoor = Instantiate(door, new Vector2(transform.position.x, transform.position.y + 2.9f), Quaternion.identity) as GameObject;
    }
    IEnumerator WaitTillClosed()
    {
        isClosing = true;
        yield return new WaitForSeconds(1);
        isOpen = true;
    }
}