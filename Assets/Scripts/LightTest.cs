using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightTest : MonoBehaviour
{
    [SerializeField] Color col1;
    [SerializeField] [Range(0f, 3f)] float lerpSpeed;
    public Light2D lt;
    private float startTime;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        print(" start " + startTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(t < 1)
        {
            t = (Time.time - startTime) * lerpSpeed;
            lt.color = Color.Lerp(Color.white, col1, t);
        }
        
        //t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        //lt.color = Color.Lerp(Color.white, col1, t);

        //float t = Mathf.PingPong(Time.time, duration) / duration;
        //lt.color = Color.Lerp(Color.white, col1, t);
    }
}
