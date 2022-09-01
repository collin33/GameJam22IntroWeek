using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckHitEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject effectPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor") || collision.transform.childCount > 14)
            return;
        GameObject e;
        Destroy(e = Instantiate(effectPrefab, collision.collider.ClosestPoint(transform.position), Quaternion.identity), 5);
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