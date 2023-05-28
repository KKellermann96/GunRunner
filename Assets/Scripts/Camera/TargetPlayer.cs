using UnityEngine;
using Cinemachine;


public class TargetPlayer : MonoBehaviour
{
    private GameObject player;
    private Transform a_body, body;

    private CinemachineVirtualCamera cB1;

    void Start()
    {
        LockOnPlayer();
    }

    public void LockOnPlayer()
    {
        player = GameObject.Find("Player");
        a_body = player.transform.Find("PlayerBody");
        body = a_body.Find("Body");
        cB1 = GetComponent<CinemachineVirtualCamera>();
        cB1.Follow = body;
    }
}
