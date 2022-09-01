using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    float randomXValue;
    float randomZValue;
    Quaternion startpoint;
    Quaternion endpoint;
    public float speed;
    float timeElapsed;

    private void Awake()
    {
        CreateNewRotation();
    }

    
    float x;
    void Update()
    {
        if (transform.rotation == endpoint)
        {
            CreateNewRotation();
        }

        transform.rotation = Quaternion.Lerp(startpoint, endpoint, timeElapsed * speed);


        timeElapsed += Time.deltaTime;
        //Vector3 rotation = new Vector3(numberOneRandom, transform.rotation.y, numberTwoRandom);
        //x += Time.deltaTime * 10;
        ////transform.rotation = Quaternion.Euler(x, 0, 0);
        ////gameObject.transform.eulerAngles = rotation;
        //gameObject.transform.rotation = Random.rotation;
    }

    void CreateNewRotation()
    {
        startpoint = transform.rotation;
        randomXValue = Random.Range(-18, 25);
        randomZValue = Random.Range(-12, 14);
        endpoint = Quaternion.Euler(randomXValue, startpoint.y, randomZValue);
        timeElapsed = 0;
        Debug.Log("Startpoint: " + startpoint + " endpoint: " + endpoint);
    }
}
