using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    public GameObject[] _gameObjects;
    public GameObject[] _invisibleGameObjects;
    Color clear = new Color(1, 1, 1, 0.24f);
    Color semiTrans = Color.white;

    Color goalColorMain = Color.black;
    Color goalColorSecond = Color.red;
    Boolean updateColors = false;
    Vector3 storedPosition;
    Vector3 goalposition;

    int fadeSpeed = 8;
    int currentLevelProgress;

    void Awake()
    {
        storedPosition = transform.parent.GetComponent<Transform>().localPosition;
    }

    void Update()
    {
        if (updateColors == true)
        {
            if (_invisibleGameObjects != null)
            {
                foreach (GameObject x in _invisibleGameObjects)
                {
                    x.GetComponent<Image>().color = Color.Lerp(x.GetComponent<Image>().color, goalColorSecond, fadeSpeed * Time.deltaTime);
                }
            }
            if (_gameObjects != null)
            {
                foreach (GameObject x in _gameObjects)
                {
                    x.GetComponent<Image>().color = Color.Lerp(x.GetComponent<Image>().color, goalColorMain, fadeSpeed * Time.deltaTime);
                }
                if (_gameObjects[0].GetComponent<Image>().color == goalColorMain)
                {
                    updateColors = false;
                }
            }
            transform.parent.GetComponent<Transform>().localPosition = Vector3.Lerp(transform.parent.GetComponent<Transform>().localPosition, goalposition, fadeSpeed * Time.deltaTime);
        }
    }

    public void makeTransparent()
    {
        goalColorMain = clear;
        goalColorSecond = Color.clear;
        goalposition = storedPosition;
        updateColors = true;
    }

    public void makeSolid()
    {
        goalColorMain = Color.white;
        goalColorSecond = Color.white;
        goalposition = new Vector3(storedPosition.x, storedPosition.y, storedPosition.z + 30);
        updateColors = true;
    }
}