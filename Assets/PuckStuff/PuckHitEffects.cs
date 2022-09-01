using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckHitEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject effectPrefab;

    public GameObject[] effects;
    public bool effectsEnabled = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (!effectsEnabled || collision.transform.CompareTag("Floor") || collision.transform.childCount > 6)
            return;

        GameObject e;
        Destroy(e = Instantiate(effects[Random.Range(0, effects.Length)], collision.collider.ClosestPoint(transform.position), Quaternion.identity), 5);
        e.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        e.transform.parent = collision.transform;
    }
}
/*
        random rotation:

        var euler = transform.eulerAngles;
        euler.y = Random.Range(-0.9f, 0.9f);
        e.transform.eulerAngles = euler;

 */