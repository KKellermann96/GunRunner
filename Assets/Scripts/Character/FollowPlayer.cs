using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject a_player;

    void Start()
    {
        a_player = GameObject.Find("Player");
    }

    void Update()
    {
        if(a_player)
        {
            gameObject.transform.position = new Vector2(a_player.transform.position.x, a_player.transform.position.y + 3);
            StartCoroutine(DestroyInOne());
        }
    }

    IEnumerator DestroyInOne()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
