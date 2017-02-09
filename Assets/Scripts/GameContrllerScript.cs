using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameContrllerScript : MonoBehaviour {

    #region Fields

    // Top object
    GameObject stackTop;
    GameObject inAir;

    // reference to gaem object prefabs
    GameObject bread;
    GameObject peanutButter;
    GameObject jelly;

    // Current y position
    int yPosition = 0;

    // reference to main camera
    Camera mainCamera;

    // reference to walls
    GameObject[] walls;

    // string for storing last object placed on pile
    string newTag = "Bread";
    public string[] tower;


    // PLayer Score
    int score = 0;
    int lives = 3;

    // Serialized Fields
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text livesText;
    [SerializeField]
    bool infiniteLives;
    #endregion

    // Use this for initialization
    void Start () {

        // Load in game Prefabs
        bread = Resources.Load<GameObject>("Prefabs/BreadPrefab");
        peanutButter = Resources.Load<GameObject>("Prefabs/PeanutButterPrefab");
        jelly = Resources.Load<GameObject>("Prefabs/JellyPrefab");

        // Get Camera
        mainCamera = Camera.main;

        // Reference objects 
        walls = new GameObject[2];
        walls = GameObject.FindGameObjectsWithTag("Wall");

        // GetComponent current object in the air
        inAir = GameObject.FindGameObjectWithTag("InAir");
        tower = new string[3];
        tower[0] = "PB";
        tower[1] = "Jelly";
        tower[2] = "Bread";

        // set initial score and life text
        livesText.text = "Lives = " + lives;
        scoreText.text = "Score = " + score * 100;

        // add event resolution
        BelowBoundsScript.resolveCollision += SetScene;
        BelowBoundsScript.resolveDrop += CheckLives;
    }
	
	// Update is called once per frame
	void Update () {

        if (infiniteLives && lives < 3)
        {
            lives = 3;
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("MainMenuScene");
        }

        // Write strings
        livesText.text = "Lives = " + lives;
        scoreText.text = "Score = " + score * 100;

    }

    #region Methods

    // return a random onject to create
    GameObject selectObject()
    {
        switch ((int)Random.Range(0, 3))
        {
            case 0:
                newTag = "Bread";
                return bread;
                
            case 1:
                newTag = "PB";
                return peanutButter;
                
            case 2:
                newTag = "Jelly";
                return jelly;
                
        }
        return null;
    }

    void CreateNewObject()
    {
        // create a new in air object
        stackTop = inAir;
        inAir = Instantiate<GameObject>(selectObject(), new Vector3(0, yPosition, 0), Quaternion.identity);
    }

    void SetScene()
    {
        // Move camera and wall up by one so the tower can get as tall as possible
        yPosition++;
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, yPosition, mainCamera.transform.position.z);
        foreach (GameObject wall in walls)
        {
            wall.transform.position = new Vector3(wall.transform.position.x, yPosition, wall.transform.position.z);
        }
        // add new string to list
        tower[2] = tower[1];
        tower[1] = tower[0];
        tower[0] = newTag;

        if (CheckScoring())
        {
            score++;
        }
        else
        {
            score -= 2;
        }

        CreateNewObject();
    }

    bool CheckScoring()
    {
        // check for correct order of ingredients
        if (tower[1] == "Bread" &&
            (tower[0] == "PB" || tower[0] == "Jelly"))
        {
            return true;
        }
        else if ((tower[0] == "Jelly" &&
            tower[1] == "PB") ||
            (tower[0] == "PB" &&
            tower[1] == "Jelly"))
        {
            return true;
        }
        else if (tower[0] == "bread" &&
            (tower[1] == "Jelly" &&
            tower[2] == "PB") ||
            (tower[1] == "PB" &&
            tower[2] == "Jelly"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CheckLives()
    {
        // check for correct order of ingredients
        if (tower[0] == "Bread" &&
            (newTag == "PB" || newTag == "Jelly"))
        {
            lives--;
        }
        else if ((newTag == "Jelly" &&
            tower[0] == "PB") ||
            (newTag == "PB" &&
            tower[0] == "Jelly"))
        {
            lives--;
        }
        else if (newTag == "bread" &&
            (tower[0] == "Jelly" &&
            tower[1] == "PB") ||
            (tower[0] == "PB" &&
            tower[1] == "Jelly"))
        {
            lives--;
        }
        else
        {
            score++;
        }

        CreateNewObject();
    }

    #endregion
}
