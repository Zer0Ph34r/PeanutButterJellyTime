using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

    #region Fields

    #region objects

    // Top object
    GameObject stackTop;
    GameObject inAir;

    // reference to gaem object prefabs
    GameObject bread;
    GameObject peanutButter;
    GameObject jelly;

    #endregion

    // reference to main camera
    Camera mainCamera;

    // reference to walls
    GameObject[] walls;

    // string for storing last object placed on pile
    public string newTag = "Bread";
    public string[] tower;
    List<GameObject> sandwich;
    List<Vector3> slicePos;

    // alignment variable for final scoring
    float alignment = 0;

    // PLayer Score
    int score = 0;
    int lives = 3;

    #region Serialized Fields

    // Serialized Fields
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text livesText;
    [SerializeField]
    Text sandwichText;
    [SerializeField]
    bool infiniteLives;

    // Tutorial Stuff
    [SerializeField]
    Canvas Tutorial;

    #endregion

    // count for sandwich
    int sandwichCount = 0;

    #endregion

    #region Start 

    // Use this for initialization
    void Start () {

        // initialize sandwich list
        sandwich = new List<GameObject>();
        slicePos = new List<Vector3>();

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
        tower = new string[4];
        tower[0] = "PB";
        tower[1] = "Jelly";
        tower[2] = "Bread";
        tower[3] = "Bread";

        // set initial score and life text
        livesText.text = "Lives = " + lives;
        scoreText.text = "Score = " + score * 100;
        sandwichText.text = "Sandwiches = " + GlobalsScript.SANDWICHES;

        // add event resolution
        BelowBoundsScript.resolveCollision += SetScene;
        BelowBoundsScript.resolveDrop += CheckLives;
    }

    #endregion

    #region Update

    // Update is called once per frame
    void Update () {

        if (infiniteLives && lives < 3)
        {
            lives = 3;
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("ScoringScene");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Tutorial.isActiveAndEnabled)
            {
                Tutorial.gameObject.SetActive(false);
            }
            else
            {
                Tutorial.gameObject.SetActive(true);
            }
        }

        // Write strings
        livesText.text = "Lives = " + lives;
        scoreText.text = "Score = " + score * 100;
        sandwichText.text = "Sandwiches = " + GlobalsScript.SANDWICHES;

        // Record alignment of stack
        alignment = 0;
        foreach (Vector3 slice in slicePos)
        {
            alignment += (Math.Abs(slice.x) / slicePos.Count);
        }
        GlobalsScript.ALIGNMENT = alignment;

    }
    #endregion

    #region Methods

    #region SelectObject

    // return a random onject to create
    GameObject selectObject()
    {
        switch ((int)UnityEngine.Random.Range(0, 3))
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


    #endregion

    #region Create New Object

    void CreateNewObject()
    {
        // create a new in air object
        stackTop = inAir;
        inAir = Instantiate<GameObject>(selectObject(), new Vector3(0, 4, 0), Quaternion.identity);
    }

    #endregion

    #region Set Scene
    /// <summary>
    /// Resets scene after a pice falls or is placed
    /// </summary>
    void SetScene()
    {

        // Adds or subtracts score
        if (CheckScoring())
        {
            score++;
        }
        else
        {
            score -= 2;
        }
        GlobalsScript.SCORE = score;

        CreateNewObject();
    }

    #endregion

    #region Check Scoring

    bool CheckScoring()
    {
        // add to list of objects
        sandwich.Add(inAir);
        slicePos.Add(inAir.transform.position);

        bool retValue;
        retValue = CheckOrder();

        #region Sandwich Check
        // Increase tower size count
        sandwichCount++;

        // add new string to list
        tower[3] = tower[2];
        tower[2] = tower[1];
        tower[1] = tower[0];
        tower[0] = newTag;
        // if the tower is 4 tall
        if (sandwichCount >= 4)
        {
            // if a propper sandwich was made
            if (tower[0] == "Bread" &&
                tower[1] == "Jelly" &&
                tower[2] == "PB" &&
                tower[3] == "Bread")
            {
                GlobalsScript.SANDWICHES++;
            }
            else if (tower[0] == "Bread" &&
                tower[1] == "PB" &&
                tower[2] == "Jelly" &&
                tower[3] == "Bread")
            {
                GlobalsScript.SANDWICHES++;
            }
            else
            {
                lives--;
            }
            // Reset Tower strings
            tower[0] = "PB";
            tower[1] = "Jelly";
            tower[2] = "Bread";
            tower[3] = "Bread";
            // delete objects in tower and reset list
            for (int i = sandwich.Count - 1; i >= 0; --i)
            {
                Destroy(sandwich[i]);
            }
            sandwich = new List<GameObject>();
            sandwichCount = 0;
        }

        #endregion

        return retValue;

    }

    #endregion

    #region Check Lives

    /// <summary>
    /// Checks what is on the tower for seeing if the dropped item should have been dropped
    /// </summary>
    void CheckLives()
    {
        if (!CheckOrder())
        {
            score++;
        }
        else
        {
            lives--;
        }

        CreateNewObject();
    }
    #endregion

    #region Check Order
    /// <summary>
    /// Checks if the most recent piece was correct to go on next
    /// </summary>
    /// <returns></returns>
    bool CheckOrder()
    {
        // check for correct order of ingredients
        if (newTag == tower[0])
        {
            return true;
        }
        else if ((newTag == "PB" ||
            newTag == "Jelly") &&
            tower[0] == "Bread")
        {
            return true;
        }
        else if ((newTag == "Jelly" &&
            tower[0] == "PB" &&
            tower[1] == "Bread") ||
            (newTag == "PB" &&
            tower[0] == "Jelly" &&
            tower[1] == "Bread"))
        {
            return true;
        }
        else if (newTag == "bread" &&
            (tower[0] == "Jelly" &&
            tower[1] == "PB" &&
            tower[2] == "Bread") ||
            (tower[0] == "PB" &&
            tower[1] == "Jelly" &&
            tower[2] == "Bread"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #endregion
}
