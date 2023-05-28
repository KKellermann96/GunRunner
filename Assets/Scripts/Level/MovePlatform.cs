using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [Header("SwingEffect")]
    [SerializeField] private bool horizontal;
    [SerializeField] private float distance;
    [SerializeField] private float time = 3; //less = faster
    [SerializeField] private bool START_RIGHT_OR_UP;
    [Header("StompEffect")]
    [SerializeField] private float delay = 0f;
    [SerializeField] private bool bounce;
    [SerializeField] private bool StartUp;
    //If in Prefab
    private Transform parentPrefab;
    private float parentXPosition;
    private Transform a_player;

    void Awake()
    {
        parentPrefab = this.transform.parent;
        parentXPosition = parentPrefab.position.x + 7;
        a_player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Start()
    {

        if (START_RIGHT_OR_UP)
            distance = -distance;
        if(horizontal)
            LeanTween.moveX(gameObject, (transform.position.x - distance), time).setEaseInOutCubic().setLoopPingPong();
        else
        {
            if (bounce)
                if (StartUp)
                    MoveBack();
                else
                    StartStomp();
            else
                LeanTween.moveY(gameObject, (transform.position.y - distance), time).setEaseInOutCubic().setLoopPingPong();
        }
    }

    void StartStomp()
    {
        LeanTween.moveY(gameObject, (transform.position.y - distance), time).setEaseInQuart().setDelay(delay).setOnComplete(() => 
        {
            if (Player.PlayerAlive && (((parentXPosition + 17) >= a_player.position.x) && ((parentXPosition - 17) <= a_player.position.x)))
                CameraShake.Instance.ShakeCamera(1.5f, 0.5f);
            MoveBack(); 
        });
    }

    void MoveBack()
    {
        LeanTween.moveY(gameObject, (transform.position.y + distance), time+1f).setEaseInSine().setDelay(1f).setOnComplete(()=> { StartStomp(); });
        
    }
}
