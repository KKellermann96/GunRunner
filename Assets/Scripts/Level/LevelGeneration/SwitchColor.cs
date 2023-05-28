using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SwitchColor : MonoBehaviour
{

    //Light
    private float lerpSpeed; //0.3f
    public Light2D lt;

    private Color startCol;
    private Color endCol = Color.white;
    private float startTime;
    private float t;
    private bool startChangeColor;



    void Update()
    {
        if(startChangeColor)
        {
            if (t < 1)
            {
                t = (Time.time - startTime) * lerpSpeed;
                lt.color = Color.Lerp(startCol, endCol, t);
            }
            else
                startChangeColor = false;
        }
    }

    public void StartColorChange(Color startCol1, Color endCol1, float speed)
    {
        startCol = startCol1;
        endCol = endCol1;
        lerpSpeed = speed;
        t = 0;
        startTime = Time.time;
        startChangeColor = true;
    }

    public Color GetCurrentColor()
    {
        return endCol;
    }
}

