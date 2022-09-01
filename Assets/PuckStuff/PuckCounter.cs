using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckCounter : MonoBehaviour
{
    public List<GameObject> pucksOnField = new List<GameObject>();
    public int CountAndDestroy()
    {
        int pucks = pucksOnField.Count;
        pucksOnField.ForEach(x => Destroy(x));
        pucksOnField.Clear();
        return pucks;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puck"))
            pucksOnField.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other) => pucksOnField.Remove(other.gameObject);

}
