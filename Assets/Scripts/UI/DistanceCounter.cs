using UnityEngine;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour
{

    public static int distance;
    Text score;
    [SerializeField] private Transform distance_Start;
    private float actuallStartPos;
    private GameObject player;
    private float slowCounter;
    private bool stopCounting = false;

    void Start()
    {
        score = GetComponent<Text>();
        player = GameObject.Find("Player");
        actuallStartPos = distance_Start.position.x;
    }

    void Update()
    {
        if(player)
        {
            if (player.transform.position.x >= distance_Start.position.x && !stopCounting)
            {
                slowCounter = ((player.transform.position.x - 20) + distance_Start.position.x)  * 0.2f;
                distance = (int) slowCounter;
                score.text = distance + " m";
            }
        }
    }
 
    public void StopDistanceCounting(bool stopDist)
    {
        if(stopDist)//in
        {
            stopCounting = true;
            distance_Start.position = new Vector2(distance_Start.position.x - 16, distance_Start.position.y);
        }
        else//Out
            stopCounting = false;
    }

        /*
     public void ResetDistCounter()
     {
         player = GameObject.Find("Player");
         distance_Start.position = new Vector2(actuallStartPos, distance_Start.position.y);
         distance = 0;
         slowCounter = 0;
         score.text = distance + " m";
     }
     */
}
