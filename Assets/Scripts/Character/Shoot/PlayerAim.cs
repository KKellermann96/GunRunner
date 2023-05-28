using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private GameObject a_player;

    void Start()
    {
        a_player = GameObject.Find("Player");
    }

        private void Update()
    {
        if(a_player!=null && !PauseMenu.GameIsPaused && Player.PlayerAlive)
        {
            var pos = Camera.main.WorldToScreenPoint(transform.position);
            var dir = Input.mousePosition - pos;    //Mouse Position
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;  //Turn Mouse Position to an angle of one Point

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (((angle >= 100 && angle <= 180) || (angle >= -180 && angle <= -100))) //if Player aims left
            {
                FlipLeft();
            }
            else if (angle >= 90 && angle <= 100 || angle >= -100 && angle <= -90) { } //To prevent Player to flicker if Mouse is above or beneath
            else   //If player aims right
                FlipRight();
        }
    }

    private void FlipRight()
    {
        if (!Playermovement.m_FacingRight)
        {
            a_player.transform.Rotate(0f, 180f, 0f);
            Playermovement.m_FacingRight = true;
        }

    }
    private void FlipLeft()
    {
        if (Playermovement.m_FacingRight)
        {
            a_player.transform.Rotate(0f, 180f, 0f);
            Playermovement.m_FacingRight = false;
        }
    }

    /*
    public void ResetSide()
    {
        a_player = GameObject.Find("Player");
        transform.Rotate(Vector3.zero);
        if (!Playermovement.m_FacingRight)
        {
            a_player.transform.Rotate(0f, 180f, 0f);
            Playermovement.m_FacingRight = true;
        }

    }

    */
}


/*
        //If player is facing right, angle in a specific range
        if(Playermovement.m_FacingRight && ((angle >= -40 && angle <= 0) || (angle >= 0 && angle <= 70)))
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        //If player is facing left, angle in a specific range    
        if ((!Playermovement.m_FacingRight) && ((angle >= -180 && angle <= -140) || (angle >= 110 && angle <= 180)))
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        */
