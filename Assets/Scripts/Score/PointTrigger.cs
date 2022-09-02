using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    public int score;
    private void OnTriggerEnter(Collider other)
    {
        score++;
    }
    private void OnTriggerExit(Collider other)
    {
        score--;
    }
}
