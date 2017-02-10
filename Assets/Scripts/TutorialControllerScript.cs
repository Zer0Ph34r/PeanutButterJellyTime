using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialControllerScript : MonoBehaviour
{
    #region Tutorial Phase Enum

    enum Phase {First, Second, Third };
    
    #endregion


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
    Text feedbackText;
    [SerializeField]
    bool infiniteLives;

    // Tutorial Stuff
    [SerializeField]
    Canvas Tutorial;

    Phase tutorialPhase = Phase.First;
    int sliceCount = 0;

    #endregion

    // Use this for initialization
    void Start()
    {

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
        feedbackText.text = "";

        // add event resolution
        BelowBoundsScript.resolveCollision += SetScene;
        BelowBoundsScript.resolveDrop += CheckLives;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (infiniteLives && lives < 3 && tutorialPhase != Phase.Third)
        {
            lives = 3;
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("MainMenuScene");
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
        if (tutorialPhase == Phase.Third)
        {
            feedbackText.text = "Alright, you've done well so far, now you don't get anymore infinite lives.  The platform in the final test will be smaller than this one, so hone your skills while you can";
        }
    }

    #region Methods

    // return a random onject to create
    GameObject selectObject()
    {
        if (tutorialPhase == Phase.First)
        {
            switch ((int)Random.Range(0, 3))
            {

                case 0:
                    newTag = "Bread";
                    feedbackText.text = "Alright, You have a slice of bread, where should it go?";
                    return bread;

                case 1:
                    newTag = "PB";
                    feedbackText.text = "Alright, You have some Peanut Butter, where should it go?";
                    return peanutButter;

                case 2:
                    newTag = "Jelly";
                    feedbackText.text = "Alright, You have some Jelly, where should it go?";
                    return jelly;
            }
            
        }
        else
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
        // Check for changing phase
        if (sliceCount > 4)
        {
            tutorialPhase = Phase.Second;
        }
        else if (sliceCount > 12)
        {
            tutorialPhase = Phase.Third;
        }

        // check for correct order of ingredients
        if (tower[1] == "Bread" &&
            (tower[0] == "PB" || tower[0] == "Jelly"))
        {
            feedbackText.text = "Yep, Peanut Butter or Jelly can go on a slice of bread";
            sliceCount++;
            return true;

        }
        else if ((tower[0] == "Jelly" &&
            tower[1] == "PB") ||
            (tower[0] == "PB" &&
            tower[1] == "Jelly"))
        {
            feedbackText.text = "That's right, you can put Peanutbutter on Jelly or Jelly on Peanut butter";
            sliceCount++;
            return true;
        }
        else if (tower[0] == "bread" &&
            (tower[1] == "Jelly" &&
            tower[2] == "PB") ||
            (tower[1] == "PB" &&
            tower[2] == "Jelly"))
        {
            feedbackText.text = "Good job, bread should go on the sandwich after you put down peanut butter and jelly";
            sliceCount++;
            return true;
        }
        else
        {
            feedbackText.text = "Oops, that doesn't go on yet.  Maybe you should check the order of ingredients again";
            return false;
        }
        
    }

    void CheckLives()
    {
        // check for correct order of ingredients
        if (tower[0] == "Bread" &&
            (newTag == "PB" || newTag == "Jelly"))
        {
            feedbackText.text = "Uh Oh, Peanut Butter of Jelly can go on a slice of bread, you shouldn't have thrown that away!";
            lives--;
        }
        else if ((newTag == "Jelly" &&
            tower[0] == "PB") ||
            (newTag == "PB" &&
            tower[0] == "Jelly"))
        {
            feedbackText.text = "No, That's all wrong.  You could have put that on the sandwich.  Peanut Butter and go on jelly and Jelly can go on peanut Butter.";
            lives--;
        }
        else if (newTag == "bread" &&
            (tower[0] == "Jelly" &&
            tower[1] == "PB") ||
            (tower[0] == "PB" &&
            tower[1] == "Jelly"))
        {
            feedbackText.text = "Oops, you didn't finish your sandwich.  Once both Peanutbutter and jelly are on the sandwich, you can put on the last slice.";
            lives--;
        }
        else
        {
            feedbackText.text = "Alright, looks like you're getting it.  That " + newTag + "can't go on the sandwich yet so you should have put it back";
            score++;
        }

        CreateNewObject();
    }

    #endregion
}
