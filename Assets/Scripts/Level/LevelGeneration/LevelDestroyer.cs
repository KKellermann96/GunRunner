using UnityEngine;


public class LevelDestroyer : MonoBehaviour // + SwitchLight
{
    //Destroy
    public static int LevelCount;
    //Light
    //Stop DistanceCounter
    private DistanceCounter dc;
    private SwitchColor sc;
    private Color startCol;
    private Color endCol;

    void Awake()
    {
        dc = GameObject.Find("Canvas").GetComponentInChildren<DistanceCounter>();
        sc = GameObject.Find("LevelGenerator").GetComponent<SwitchColor>();

        startCol = Color.white;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            string levelTag = ((LevelCount % 2) == 0) ? "LevelPartA" : "LevelPartB";
            LevelCount += 1;
            //ColorChange
            endCol = new Color(233f / 255f, 79f / 255f, 55f / 255f);
            sc.StartColorChange(startCol, endCol, 0.3f);
            startCol = endCol;

            destructLevel(levelTag);
            dc.StopDistanceCounting(false);
            Destroy(gameObject);
        }
    }

    void destructLevel(string tag)
    {
        GameObject[] part = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < part.Length; ++i)
            Destroy(part[i]);
    }
}
