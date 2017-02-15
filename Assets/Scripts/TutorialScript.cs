using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour {

    #region Feilds

    // Tutorial Objects
    [SerializeField]
    GameObject airObject;


    // Tutorial Image and text
    [SerializeField]
    Text tutorialText;

    int i = 0;
    
    #endregion

    // Use this for initialization
    void Start () {

        tutorialText.text = "Welcome to the Tutorial.  Here you will see how the game is played and how to earn points."
            + "\n\n\n\n" + "Hit space to play the game and progress the tutorial";

    }
	
	// Update is called once per frame
	void Update () {

        if (airObject == null)
        {
            airObject = GameObject.FindGameObjectWithTag("InAir");
        }
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProgressTutorial();
        }
		
	}

    void ProgressTutorial()
    {
        switch (i)
        {
            case 0:
                tutorialText.text = "Alright, now you see that pressing space causes the piece in the air to fall down"
                    + "\n\n\n\n" + "You want to build your sandwich on the little platform in the middle";
                break;
        }
    }
}
