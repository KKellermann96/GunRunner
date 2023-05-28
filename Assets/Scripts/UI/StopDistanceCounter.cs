using UnityEngine;

public class StopDistanceCounter : MonoBehaviour
{
    private DistanceCounter dc;
    //private GameObject nextTrigger;
    //private int distTrigger;
    //private static bool startDelRest = false;
    void Awake()
    {
        dc = GameObject.Find("Canvas").GetComponentInChildren<DistanceCounter>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            //Destroy other RestRooms
            //if (startDelRest)
            //    if (GameObject.Find("RestPoint(Clone)"))
            //        Destroy(GameObject.FindGameObjectWithTag("RestRoom"));

            //nextTrigger = GameObject.Find("DeleteLevelTrigger 1"); //Position of next Trigger when entering RestRoom   !!!Refrence of old DeleteLevelTrigger 1
            //distTrigger = (int)(nextTrigger.transform.position.x - transform.position.x);
            //print("NextTrigger: " + nextTrigger.transform.position.x + "Akt Pos: " + transform.position.x);

            
            dc.StopDistanceCounting(true);
            //startDelRest = true;
            Destroy(gameObject);
        }
            
    }
}
