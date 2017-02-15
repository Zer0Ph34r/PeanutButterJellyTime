using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayScript : MonoBehaviour {

    [SerializeField]
    Text scoreText;

    // Completion percentage
    float PercentageOfSuccess = 0;

	// Use this for initialization
	void Start () {

        #region Scoring
        // Give percentage of alignment
        if (GlobalsScript.ALIGNMENT < 1)
        {
            PercentageOfSuccess = 100;
        }
       else if (GlobalsScript.ALIGNMENT < 2)
        {
            PercentageOfSuccess = 90;
        }
        else if (GlobalsScript.ALIGNMENT < 5)
        {
            PercentageOfSuccess = 80;
        }
        else
        {
            PercentageOfSuccess = 50;
        }

        // Compare Snadwiches made to highscore
       if (GlobalsScript.HIGHSANDWICHES <= GlobalsScript.SANDWICHES)
        {
            PercentageOfSuccess += 100;
        }
        else if (GlobalsScript.HIGHSANDWICHES > GlobalsScript.SANDWICHES)
        {
            PercentageOfSuccess += 100 * (GlobalsScript.SANDWICHES / GlobalsScript.HIGHSANDWICHES);
        }

       // compare score with highscore
        if (GlobalsScript.HIGHSCORE <= GlobalsScript.SCORE)
        {
            PercentageOfSuccess += 100;
        }
        else if (GlobalsScript.HIGHSCORE > GlobalsScript.SCORE)
        {
            PercentageOfSuccess += 100 * (GlobalsScript.SCORE / GlobalsScript.HIGHSCORE);
        }

        PercentageOfSuccess = PercentageOfSuccess / 300;
        #endregion

        scoreText.text = "Alright, after all that work, let's see how you did. \n" 
            + "First, lets see how many sandwiches you made: \n"
            + GlobalsScript.SANDWICHES + "\n" 
            + "Now we take a look at your final score: \n"
            + (GlobalsScript.SCORE * 100) + "\n"
            + "And Finally, your overall alignment of your tower: \n"
            + GlobalsScript.ALIGNMENT + "\n" + "\n"
            + "That Leaves you with a score of : " + (PercentageOfSuccess * 100) + " Out of 100 % Possible."
            + "\n\n" + "How did you do? Are you satisfied with your results?";
    }
}
