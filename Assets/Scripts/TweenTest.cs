using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TweenTest : MonoBehaviour
{
    
    public GameObject geh;
    public uint a;

    void Start()
    {
        LeanTween.moveY(geh, (geh.transform.position.y - 5), 4).setEaseInOutCubic().setLoopPingPong();  
    }
   
}