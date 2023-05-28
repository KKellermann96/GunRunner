using UnityEngine;
using Cinemachine;


public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private GameObject vcam1;
    [SerializeField] private GameObject vcam2;

    private CinemachineVirtualCamera cB1;
    private CinemachineVirtualCamera cB2;
    private Transform cB2Pos;
    private GameObject a_player;
    private Playermovement plmov;
    

    void Awake()
    {
        a_player = GameObject.Find("Player");
        plmov = a_player.GetComponent<Playermovement>(); 
        cB1 = vcam1.GetComponent<CinemachineVirtualCamera>();
        cB2 = vcam2.GetComponent<CinemachineVirtualCamera>();
        cB2Pos = vcam2.GetComponent<Transform>();
    }

    public void SwitchToVcam2()
    {
        //Place secound camera to the right position
        if(plmov.moveDirectionRight)
            cB2Pos.position = new Vector3(a_player.transform.position.x + 3, a_player.transform.position.y+5, -10);
        else
            cB2Pos.position = new Vector3(a_player.transform.position.x - 3, a_player.transform.position.y + 5, -10);
        //Switch Camera by setting the main camera to a lower priority
        cB1.Priority = 0;
        cB2.Priority = 10;

    }

    public void SwitchToVcam1()
    {
        cB2.Priority = 0;
        cB1.Priority = 10;
        
    }

    /*
    public void ResetCam()
    {
        a_player = GameObject.Find("Player");
        plmov = a_player.GetComponent<Playermovement>();
    }
    */
}


