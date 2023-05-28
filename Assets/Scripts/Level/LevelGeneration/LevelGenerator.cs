using log4net;
using System.Collections.Generic;
using UnityEditor.Experimental.U2D.IK;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private static readonly ILog Log = LogManager.GetLogger(type: typeof(LevelGenerator));

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;
    public const int whenRestRoom = 20;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> level1PartList;
    [SerializeField] private List<Transform> level2PartList;
    [SerializeField] private List<Transform> level3PartList;
    [SerializeField] private List<Transform> level4PartList;
    [SerializeField] private List<Transform> level5PartList;
    [SerializeField] private List<Transform> level6PartList;
    [SerializeField] private List<Transform> level7PartList;
    [SerializeField] private List<Transform> level8PartList;
    [SerializeField] private List<Transform> level9PartList;
    [SerializeField] private List<Transform> level10PartList;
    [SerializeField] private List<Transform> levelEndlesPartList;
    [SerializeField] private Transform restRoom;  //RestRoomA und B

    private GameObject player;
    private Vector3 lastEndPosition;
    private bool spawnRest = false;
    private bool SpawnRestSwitch = false;
    private string[] currentLevel = new string[5];
    private int currIndex = 0;

    private short spawnLevelNum;
    //For Testing
    public bool testLevel;
    [SerializeField] private List<Transform> levelPartTest;


    private void Awake()
    {
        player = GameObject.Find("Player");
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        spawnLevelNum = 1;
        for (int i = 0; i < 5; ++i)
            currentLevel[i] = "x";
    }

    void Update()
    {
        Log.Debug(message: "LG Update");

        if (player != null)
        {
            if (!spawnRest && !SpawnRestSwitch && (DistanceCounter.distance % (whenRestRoom)) == 0 && DistanceCounter.distance != 0 && (spawnLevelNum != 11))
                spawnRest = true;
            Log.Debug(message: "LG Update 2");
            //Spawn another Level part
            if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            {
                Log.Debug(message: "LG Update 3");
                if (spawnRest) //RestRoom
                {
                    SpawnLevelPart(0);
                    spawnRest = false;
                    SpawnRestSwitch = true;
                    if(spawnLevelNum < 11)
                        spawnLevelNum += 1;
                } 
                else
                {
                    SpawnLevelPart(spawnLevelNum);
                    SpawnRestSwitch = false;
                }  
            }
        }
    }

    private void SpawnLevelPart(short room)
    {
        Log.Debug(message: "LG Update 4");
        Transform chosenLevelPart;
        
        if(!testLevel)//---------------------------TestIF
        { //---------------------------TestIF
            switch (room)
            {
                case 0: //RestRoom
                    chosenLevelPart = restRoom;
                    //if (GameObject.Find("RestPoint(Clone)"))
                     break;
                case 1: //Level 1
                    chosenLevelPart = ChooseDifferentLevel(level1PartList, level1PartList.Count); break;
                case 2: //Level 2
                    chosenLevelPart = level2PartList[Random.Range(0, level2PartList.Count)];
                    //ChooseDifferentLevel(level2PartList, level2PartList.Count); 
                    break;
                case 3: //Level 3
                    chosenLevelPart = ChooseDifferentLevel(level3PartList, level3PartList.Count); break;
                case 4: //Level 4
                    chosenLevelPart = ChooseDifferentLevel(level4PartList, level4PartList.Count); break;
                case 5: //Level 5
                    chosenLevelPart = ChooseDifferentLevel(level5PartList, level5PartList.Count); break;
                case 6: //Level 6
                    chosenLevelPart = ChooseDifferentLevel(level6PartList, level6PartList.Count); break;
                case 7: //Level 7
                    chosenLevelPart = ChooseDifferentLevel(level7PartList, level7PartList.Count); break;
                case 8: //Level 8
                    chosenLevelPart = ChooseDifferentLevel(level8PartList, level8PartList.Count); break;
                case 9: //Level 9
                    chosenLevelPart = ChooseDifferentLevel(level9PartList, level9PartList.Count); break;
                case 10: //Level 10
                    chosenLevelPart = ChooseDifferentLevel(level10PartList, level10PartList.Count); break;
                case 11: //Level Endles
                    chosenLevelPart = ChooseDifferentLevel(levelEndlesPartList, levelEndlesPartList.Count); break;
                default:
                    chosenLevelPart = level1PartList[0];
                    print("wrong LevelSpawn Number");
                    break;   
            }

            currentLevel[currIndex] = chosenLevelPart.name;
            currIndex = (currIndex + 1) % 5;
            print(currentLevel[0] + " " + currentLevel[1] + " " + currentLevel[2] + " " + currentLevel[3] + " " + currentLevel[4]);

        }//---------------------------TestIF
        else//---------------------------TestIF
            chosenLevelPart = levelPartTest[Random.Range(0, levelPartTest.Count)]; //---------------------------TestIF

        //int[] randHeightArray = { -1, 0, 1 };
        //int randHeight = randHeightArray[Random.Range(0, randHeightArray.Length)];
        //lastEndPosition.y += randHeight;
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        
    }

    private Transform ChooseDifferentLevel(List<Transform> levelPartList, int partLength)
    {
        int levelIndex = Random.Range(0, partLength);
        Transform levelPart = levelPartList[levelIndex];
        bool searchNewLevel = true;

        while (searchNewLevel)
        {
            bool level_was_before = false;
            for(int i=0;i<5;++i)
            {
                if (currentLevel[i] == levelPart.name)
                    level_was_before = true;
            }

            if (level_was_before)
                levelPart = levelPartList[(levelIndex + 1) % partLength]; //Next Level 
            else
                searchNewLevel = false;
        }
        if ((levelPart.name == "LevelPart10") || (levelPart.name == "LevelPart21") || (levelPart.name == "LevelPart22"))
            CheckProbably(levelPart); //For Part 1

        return levelPart;
    }

    private Transform CheckProbably(Transform levelPart)
    {
        int probably = Random.Range(0, 100);
        if (probably <= 50)
            levelPart = level1PartList[Random.Range(0, level1PartList.Count)]; 
        return levelPart;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
        
    }

    /*
    public void ResetLevelGenerator()
    {
        player = GameObject.Find("Player");
        spawnLevelNum = 1;
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        CloseDoor cd = levelPart_Start.GetComponentInChildren<CloseDoor>();
        spawnRest = false;
        SpawnRestSwitch = false;
        currIndex = 0;
        for (int i = 0; i < 5; ++i)
            currentLevel[i] = "x";
        cd.ResetDoor();
    }
    */
}
