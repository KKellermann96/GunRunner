using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedJumpBoost : MonoBehaviour
{
    public static int overlapEffect = 0;
    
    [Header("Jump")]
    public bool JumpBoost;
    public float JumpPower = 5f;
    [Header("Speed")]
    public bool SpeedBoost;
    public float SpeedIncrease = 10f;
    [Header("Slow")]
    public bool SlowBooster;
    public float SpeedDecrease = 1f;
    [Header("For Speed and Slow")]
    public float EffectDuration = 5f;
    [Header("Color")]
    public Color BoostColor;
    [SerializeField] [Range(0f, 1f)] float colSpeed;

    private float actualRunSpeed;
    private float actualRunMultipier;
    private Playermovement playerMov;
    private Player player;


    void Start()
    {
        playerMov = GameObject.Find("Player").GetComponent<Playermovement>();
        player = GameObject.Find("Player").GetComponent<Player>();

        actualRunSpeed = playerMov.runSpeed;
        actualRunMultipier = playerMov.GetRunMuliplier();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Player")
        {
            if (JumpBoost)
            {
                playerMov.Jump(new Vector2(0, JumpPower));
                player.BoostColor(BoostColor, colSpeed);
                StartCoroutine(ResetCol(0.7f));
            }
            else
                overlapEffect += 1;
            if (SpeedBoost)
                StartCoroutine(StartFastRunning());
            if (SlowBooster)
                StartCoroutine(StartSlowRunning());
        }
    }

    private void resetValue()
    {
        playerMov.runSpeed = playerMov.actualRunSpeed;
        playerMov.SetRunMultiplier(actualRunMultipier);
    }
    IEnumerator StartFastRunning()
    {
        playerMov.runSpeed = SpeedIncrease;
        playerMov.SetRunMultiplier(2f);
        player.BoostColor(BoostColor, colSpeed);
        yield return new WaitForSeconds(EffectDuration);
        if(overlapEffect == 1 && player != null)
        {
            resetValue();
            player.ResetColor(colSpeed);
        }
            
        overlapEffect -= 1;
    }
    IEnumerator StartSlowRunning()
    {
        player.BoostColor(BoostColor, colSpeed);
        yield return new WaitForSeconds(0.1f);
        playerMov.runSpeed = SpeedDecrease;
        playerMov.SetRunMultiplier(0.3f);
        yield return new WaitForSeconds(EffectDuration);
        if (overlapEffect == 1 && player != null)
        {
            resetValue();
            player.ResetColor(colSpeed);
        }
        overlapEffect -= 1;
    }
    IEnumerator ResetCol(float t)
    {
        yield return new WaitForSeconds(t);
        if(player != null)
            player.ResetColor(colSpeed);
    }
}
