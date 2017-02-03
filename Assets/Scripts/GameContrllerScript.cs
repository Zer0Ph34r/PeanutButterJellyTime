using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContrllerScript : MonoBehaviour {

    #region Fields

    // Top object
    GameObject stackTop;

    // reference to gaem object prefabs
    GameObject bread;
    GameObject peanutButter;
    GameObject jelly;

    // Current y position
    int yPosition = 0;

    #endregion


    // Use this for initialization
    void Start () {

        // Load in game Prefabs
        bread = Resources.Load<GameObject>("Prefabs/BreadPrefab");
        peanutButter = Resources.Load<GameObject>("Prefabs/PeanutButterPrefab");
        jelly = Resources.Load<GameObject>("Prefabs/JellyPrefab");

        // GetComponent current object in the air
        stackTop = GameObject.FindGameObjectWithTag("InAir");
	}
	
	// Update is called once per frame
	void Update () {

        if (stackTop.tag == "Floor")
        {
            Instantiate<GameObject>(selectObject(), new Vector3(0, yPosition, 0), Quaternion.identity);
        }
		
	}

    #region Methods

    // return a random onject to create
    GameObject selectObject()
    {
        switch ((int)Random.Range(0, 3))
        {
            case 0:
                return bread;
            case 1:
                return peanutButter;
            case 2:
                return jelly;
        }
        return null;
    }

    #endregion
}
