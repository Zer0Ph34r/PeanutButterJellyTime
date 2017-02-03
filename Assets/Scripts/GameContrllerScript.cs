﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    string[] tower;


    // PLayer Score
    int score = 0;

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
        tower = new string[2];
	}
	
	// Update is called once per frame
	void Update () {

        if (inAir.transform.position.y < mainCamera.transform.position.y - 3)
        {
            SetScene();
        }
		
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

    void SetScene()
    {
        // Move camera and wall up by one so the tower can get as tall as possible
        yPosition++;
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, yPosition, mainCamera.transform.position.z);
        foreach (GameObject wall in walls)
        {
            wall.transform.position = new Vector3(wall.transform.position.x, yPosition, wall.transform.position.z);
        }
        // create a new in air object
        stackTop = inAir;
        inAir = Instantiate<GameObject>(selectObject(), new Vector3(0, yPosition, 0), Quaternion.identity);
    }

    #endregion
}
