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

       if (GlobalsScript.HIGHSLICES <= GlobalsScript.SLICES)
        {
            PercentageOfSuccess += 100;
        }
        else if (GlobalsScript.HIGHSLICES > GlobalsScript.SLICES)
        {
            PercentageOfSuccess += 100 * (GlobalsScript.SLICES / GlobalsScript.HIGHSLICES);
        }

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
            + "First, lets see how many slices you put on your sandwich tower: \n"
            + GlobalsScript.SLICES + "\n" 
            + "Now we take a look at your final score: \n"
            + (GlobalsScript.SCORE * 100) + "\n"
            + "And Finally, your overall alignment of your tower: \n"
            + GlobalsScript.ALIGNMENT + "\n" + "\n"
            + "That Leaves you with a score of : " + PercentageOfSuccess + "Out of 100 % Possible."
            + "\n\n" + "How did you do? Are you satisfied with your results?";
    }
}
