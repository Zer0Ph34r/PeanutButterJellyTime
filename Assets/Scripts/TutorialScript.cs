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

    bool dropped = false;

    int i = 0;
    
    #endregion

    // Use this for initialization
    void Start () {

        tutorialText.text = "\n\n\n\n" + "Welcome to the Tutorial.  Here you will see how the game is played and how to earn points."
            + "Hit space to play the game and progress the tutorial";
        
    }
	
	// Update is called once per frame
	void Update () {

        if (airObject.transform.position.y < 2)
        {
            airObject = GameObject.FindGameObjectWithTag("InAir");
            TutorialFSM = PlayMakerFSM.FindFsmOnGameObject(airObject, "Moving");
        }
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProgressTutorial();
        }

        if (i == 2 &&
            (airObject.transform.position.x <= 0.2 &&
            airObject.transform.position.x >= -0.2) &&
            !dropped)
        {
            TutorialFSM.SetState("Dropping");
            tutorialText.text = "\n\n\n\n" + "When you hit space bar in the game, you piece will drop immediately.  You want it in the middle of the little platform.";
            dropped = true;
        }
        if (i == 4 &&
            (airObject.transform.position.x <= 0.2 &&
            airObject.transform.position.x >= -0.2) &&
            !dropped)
        {
            TutorialFSM.SetState("Dropping");
            tutorialText.text = "\n\n\n\n" + "Now you have some jelly, the last the last of the three types of ingredients";
            dropped = true;
        }
        if (i == 5 &&
            (airObject.transform.position.x <= 0.2 &&
            airObject.transform.position.x >= -0.2) &&
            !dropped)
        {
            TutorialFSM.SetState("Dropping");
            tutorialText.text = "\n\n\n\n" + "Uh Oh, more Jelly.  That can't go on next, so what should you do with it?";
            dropped = true;
        }
        if (i == 6 &&
            (airObject.transform.position.x <= -5 ||
            airObject.transform.position.x >= 5) &&
            !dropped)
        {
            TutorialFSM.SetState("Dropping");
            tutorialText.text = "\n\n\n\n" + "Yep, drop it off the side to put it back where it came from";
            dropped = true;
        }
        if (i == 7 &&
            (airObject.transform.position.x <= 0.2 &&
            airObject.transform.position.x >= -0.2) &&
            !dropped)
        {
            TutorialFSM.SetState("Dropping");
            tutorialText.text = "\n\n\n\n" + "With that, you know know how to build a PB & J sandwich, and how to play the game.  Now you get to test your skills";
            dropped = true;
        }

    }

    void ProgressTutorial()
    {
        
        switch (i)
        {
            
            case 0:
                tutorialText.text = "\n\n\n\n" + "Alright, That block floating in the air is a piece of bread"
                    + "You want to build your sandwich on the little platform in the middle.  press space and see what happens";
                i++;
                break;

            case 1:
                i++;
                break;
            case 2:
                tutorialText.text = "\n\n\n\n" + "Next, you have some Peanut Butter, since all that is here is bread, you're good to place it down";
                i++;
                break;
            case 3:
                i++;
                break;
            case 4:
                tutorialText.text = "\n\n\n\n" + "You should know where the Jelly goes by now, but just in case, watch what happens.";
                i++;
                break;
            case 5:
                i++;
                break;
            case 6:
                tutorialText.text = "\n\n\n\n" + "Last but not least, put another piece of bread on top to complete your sandwich";
                i++;
                break;
            case 7:
                SceneManager.LoadScene("TryScene");
                break;

        }
        dropped = false;
    }
}
