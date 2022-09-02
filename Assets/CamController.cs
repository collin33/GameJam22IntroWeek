using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject far;
    public GameObject mid;
    public GameObject close;

    public void SetFar()
    {
        far.SetActive(true);
        mid.SetActive(false);
        close.SetActive(false);
    }
    public void SetMid()
    {
        far.SetActive(false);
        mid.SetActive(true);
        close.SetActive(false);
    }
    public void SetClose()
    {
        far.SetActive(false);
        mid.SetActive(false);
        close.SetActive(true);
    }
}
