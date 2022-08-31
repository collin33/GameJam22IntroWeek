using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveButton : MonoBehaviour
{
    public Button CurrentlyActive;

    public void SetActiveButton(Button CurrentButton)
    {
        CurrentlyActive = CurrentButton;
    }

    public void SelectActiveButton()
    {
        CurrentlyActive.Select();
    }
}