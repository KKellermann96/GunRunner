using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultMovePlatform : MonoBehaviour
{
    [SerializeField] private bool horizontal;
    [SerializeField] private bool START_RIGHT_OR_UP;
    [SerializeField] private float distanceH;
    [SerializeField] private float distanceV;
    [SerializeField] private float time = 3; //less = faster
    [SerializeField] private float delay; //less = faster




    void Start()
    {
        if(horizontal)
        {
            if (START_RIGHT_OR_UP)
                GoRight();
            else
                GoLeft();
        }
        else
        {
            if (START_RIGHT_OR_UP)
                GoUp();
            else
                GoDown();
        } 
    }

    private void GoLeft()
    {
        LeanTween.moveX(gameObject, (transform.position.x - distanceH), time).setEaseInQuart().setDelay(delay).setOnComplete(() =>
        {
            GoDown();
        });
    }
    private void GoRight()
    {
        LeanTween.moveX(gameObject, (transform.position.x + distanceH), time).setEaseInQuart().setDelay(delay).setOnComplete(() =>
        {
            GoUp();
        });
    }
    private void GoUp()
    {
        LeanTween.moveY(gameObject, (transform.position.y + distanceV), time).setEaseInQuart().setDelay(delay).setOnComplete(() =>
        {
            GoLeft();
        });
    }
    private void GoDown()
    {
        LeanTween.moveY(gameObject, (transform.position.y - distanceV), time).setEaseInQuart().setDelay(delay).setOnComplete(() =>
        {
            GoRight();
        });
    }
}
