using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAlpha : MonoBehaviour
{
    float OriginAlpha;
    float GoalAlpha;
    bool UpdateLoop = false;
    float t = 0.0f;


    void OnEnable()
    {
        OriginAlpha = GetComponent<CanvasGroup>().alpha;
        GoalAlpha = 1f;
        UpdateLoop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateLoop == true)
        {
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(OriginAlpha, GoalAlpha, t);
            t += 2.5f * Time.deltaTime;
            if (GetComponent<CanvasGroup>().alpha == GoalAlpha)
            {
                UpdateLoop = false;
                t = 0.0f;
                if (GoalAlpha == 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void Disable()
    {
        OriginAlpha = GetComponent<CanvasGroup>().alpha;
        GoalAlpha = 0f;
        UpdateLoop = true;
    }
}