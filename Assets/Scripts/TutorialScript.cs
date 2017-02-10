using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour {

    #region Feilds

    // Tutorial Sprites
    Sprite tutorial0;
    Sprite tutorial1;
    Sprite tutorial2;
    Sprite tutorial3;
    Sprite tutorial4;
    Sprite tutorial5;

    // Tutorial Image and text
    [SerializeField]
    Image TutorialImage;
    [SerializeField]
    Text TutorialText;

    int tutorialStep = 0;
    bool pressed = false;

    #endregion

    // Use this for initialization
    void Start () {

        //Load Sprties into Script
        tutorial0 = Resources.Load<Sprite>("Sprites/Start Position");
        tutorial1 = Resources.Load<Sprite>("Sprites/Jelly on Bread");
        tutorial2 = Resources.Load<Sprite>("Sprites/Peanutbutter on Jelly");
        tutorial3 = Resources.Load<Sprite>("Sprites/Error");
        tutorial4 = Resources.Load<Sprite>("Sprites/Error Remove");
        tutorial5 = Resources.Load<Sprite>("Sprites/Complete Sandwich"); 

        TutorialImage.sprite = tutorial0;
        TutorialText.text = "Here's what the game looks like when you start the firts level. \n  You'll see a small platform, two walls" +
            " and a piece of bread floating above the platfrom.";

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && !pressed)
        {
            pressed = true;
            ProgressTutorial();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            pressed = false;
        }
		
	}

    void ProgressTutorial()
    {
        if (tutorialStep == 0)
        {
            TutorialImage.sprite = tutorial1;
            TutorialText.text = "Alright, You put down the first piece of Bread!  Now you have a piece of Jelly ready to fall down onto the bread you " +
                "just placed.  WHat should you do now?";
            tutorialStep++;
        }
        else if (tutorialStep == 1)
        {
            TutorialImage.sprite = tutorial2;
            TutorialText.text = "Right, you need to put it onto the piece of bread.  Looks like you have some peanutbutter to put donw now" +
                "Wht should you do with this peanut butter?";
            tutorialStep++;
        }
        else if (tutorialStep == 2)
        {
            TutorialImage.sprite = tutorial3;
            TutorialText.text = "That's correct!  It needs to go on top of the jelly.  Now you have some more jelly ready to put down.  What should you do now?";
            tutorialStep++;
        }
        else if(tutorialStep == 3)
        {
            TutorialImage.sprite = tutorial4;
            TutorialText.text = "Right, you should have a piece of bread next, so you need to put the jelly back in the jar.  DO this by letting it fall of the side of your tower";
            tutorialStep++;
        }
        else if(tutorialStep == 4)
        {
            TutorialImage.sprite = tutorial5;
            TutorialText.text = "Alright, now you know how to make a full Peanut Butter and Jelly sandwich.  Ready to test your Knowledge?";
            tutorialStep++;
        }
        else if (tutorialStep == 5)
        {
            SceneManager.LoadScene("TryScene");
        }
    }
}
