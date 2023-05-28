using UnityEngine;

public class StayOnHMoving : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Player player = coll.GetComponent<Player>();
        if (player != null)
        {
            coll.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        Player player = coll.GetComponent<Player>();
        if (player != null)
        {
            coll.transform.SetParent(null);
        }
    }
}
