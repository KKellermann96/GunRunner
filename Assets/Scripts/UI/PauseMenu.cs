using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameRestart = false;
    public GameObject PauseMenuUI;
    /*
    [SerializeField] private List<GameObject> playerList;
    private Playermovement playerMovSc;
    private Player playerSc;
    private HealthBar healthBarSr;
    private DistanceCounter distCountSc;
    private LevelGenerator levelGenSc;
    private SwitchColor switchColSc;
    private CameraSwitch camSwitchSc;
    private TargetPlayer targPlSc;
    private PlayerAim playerAimSc;
    private GameObject a_Player;
    */

    void Awake()
    {
        PauseMenuUI.SetActive(false);

        /*
        a_Player = GameObject.Find("Player");
        playerSc = a_Player.GetComponent<Player>();
        playerMovSc = a_Player.GetComponent<Playermovement>();
        healthBarSr = GameObject.Find("Canvas").GetComponentInChildren<HealthBar>();
        distCountSc = GameObject.Find("Canvas").GetComponentInChildren<DistanceCounter>();
        levelGenSc = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        switchColSc = GameObject.Find("LevelGenerator").GetComponent<SwitchColor>();
        targPlSc = GameObject.Find("CM vcam1").GetComponent<TargetPlayer>();
        camSwitchSc = Camera.main.GetComponent<CameraSwitch>();
        playerAimSc = a_Player.GetComponentInChildren<PlayerAim>();
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Player.PlayerAlive)
        {
            if(!GameIsPaused)
                Pause();
            else
                Resume();
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenue()
    {
        Time.timeScale = 1f;
        print("Load Menu");
    }

    public void Restart()
    {
        GameRestart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(WaitToDisable());
        Resume();
    }

    IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(0.1f);
        Playermovement playerMovSc = GameObject.Find("Player").GetComponent<Playermovement>();
        SpeedJumpBoost.overlapEffect = 0;
        playerMovSc.runSpeed = playerMovSc.actualRunSpeed;
        playerMovSc.SetRunMultiplier(playerMovSc.GetRunMuliplier());
        LevelDestroyer.LevelCount = 0;
        GameRestart = false;
    }
}



/*
        GameRestart = true;
        //Start Transition or 
        Player.PlayerAlive = false;
        
        //Destroy Level
        GameObject[] partA = GameObject.FindGameObjectsWithTag("LevelPartA"); 
        GameObject[] partB = GameObject.FindGameObjectsWithTag("LevelPartB");
        GameObject[] partC = GameObject.FindGameObjectsWithTag("RestRoom");
        for (int i = 0; i < partA.Length; ++i)
            Destroy(partA[i]);
        for (int i = 0; i < partB.Length; ++i)
            Destroy(partB[i]);
        for (int i = 0; i < partC.Length; ++i)
            Destroy(partC[i]);

        //Player
        if (a_Player == null)
        {
            a_Player = Instantiate(playerList[0], Vector3.zero, Quaternion.identity) as GameObject;
            a_Player.name = "Player";
            playerMovSc = a_Player.GetComponent<Playermovement>();
            playerSc = a_Player.GetComponent<Player>();
            playerAimSc = a_Player.GetComponentInChildren<PlayerAim>();
        } 
        else
        {
            a_Player.transform.position = Vector3.zero;
            if (!Playermovement.m_FacingRight)
                a_Player.transform.Rotate(0f, 180f, 0f);
            playerMovSc.ResetMovement();
            playerAimSc.ResetSide();
        }
         

        playerSc.currentHealth = playerSc.maxHealth;
        healthBarSr.SetHealth(playerSc.maxHealth);
        //Remove Boosteffects
        playerMovSc.runSpeed = playerMovSc.actualRunSpeed;
        playerMovSc.SetRunMultiplier(playerMovSc.GetRunMuliplier());
        playerSc.ResetColor(0.01f);

        //Reset Level and open StartDoor
        levelGenSc.ResetLevelGenerator();
        LevelDestroyer.LevelCount = 0;
        //Delete ExplosionParts
        for (int i = 1; i <= 7; ++i)
            Destroy(GameObject.Find("Expo" + i + "(Clone)"));
        //Reset Color
        switchColSc.StartColorChange(switchColSc.GetCurrentColor(), Color.white, 3f);
        //Reset Score
        distCountSc.ResetDistCounter();
        Resume();
        //Set Camera 
        targPlSc.LockOnPlayer();
        camSwitchSc.ResetCam();
        camSwitchSc.SwitchToVcam1();
        StartCoroutine(WaitToDisable());

    */
