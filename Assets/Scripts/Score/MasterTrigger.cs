using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterTrigger : MonoBehaviour
{
    public Text txt;
    private void OnTriggerEnter(Collider other)
    {
        Calculate();
    }
    private void OnTriggerExit(Collider other)
    {
        Calculate();
    }
    public void Calculate()
    {
        int score = 0;
        int scoreOne = GameObject.Find("Point 1 Trigger").GetComponent<PointTrigger>().score;
        int scoreTwo = GameObject.Find("Point 2 Trigger").GetComponent<PointTrigger>().score;
        int scoreThree = GameObject.Find("Point 3 Trigger").GetComponent<PointTrigger>().score;
        int scoreFour = GameObject.Find("Point 4 Trigger").GetComponent<PointTrigger>().score;

        for (int i = 0; i < 6; i++)
        {
            if (scoreOne >= 1 && scoreTwo >= 1 && scoreThree >= 1 && scoreFour >= 1)
            {
                score += 20;
                scoreOne--;
                scoreTwo--;
                scoreThree--;
                scoreFour--;
            }
        }
        score += scoreOne + (scoreTwo * 2) + (scoreThree * 3) + (scoreFour * 4);
        txt.text = score.ToString();
    }
}
