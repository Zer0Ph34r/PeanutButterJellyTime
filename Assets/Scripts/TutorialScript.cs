using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayMaker;

public class TutorialScript : MonoBehaviour {

    #region Feilds

    // Tutorial Objects
    [SerializeField]
    GameObject airObject;


    // Tutorial Image and text
    [SerializeField]
    Text tutorialText;

    [SerializeField]
    PlayMakerFSM TutorialFSM;

    int i = 0;
    
    #endregion

    // Use this for initialization
    void Start () {

        tutorialText.text = "\n\n\n\n" + "Welcome to the Tutorial.  Here you will see how the game is played and how to earn points."
            + "Hit space to play the game and progress the tutorial";
        
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

        if (i == 2 &&
            (airObject.transform.position.x <= 0.2 &&
            airObject.transform.position.x >= -0.2))
        {
            TutorialFSM.SetState("Dropping");
            tutorialText.text = "\n\n\n\n" + "That's where you want to drop all the correct ingredients on your sandwich.";
            ++i;
        }
		
	}

    void ProgressTutorial()
    {
        switch (i)
        {
            case 0:
                tutorialText.text = "\n\n\n\n" + "Alright, now you see that pressing space causes the piece in the air to fall down"
                    + "You want to build your sandwich on the little platform in the middle";
                i++;
                break;

            case 1:
                tutorialText.text = "\n\n\n\n" +  "Watch as the bread falls onto the platform, this is what you need to do";
                i++;
                break;
            case 3:
                tutorialText.text = "\n\n\n\n" + "Next, you have some Peanut Butter";
                i++;
                break;
        }
    }
}
