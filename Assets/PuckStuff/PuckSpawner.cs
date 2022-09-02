using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuckSpawner : MonoBehaviour
{
    public int pucks = 30;
    public int remaining;

    public GameObject puckPrefab;

    public PuckCounter puckCounter;

    private GameObject secondToLastSpawned;

    private GameObject lastSpawned;
    //public InputAction SpawnKey;

    public float time;
    public bool canSpawn;
    private void Awake()
    {
        //SpawnKey.Enable();
        remaining = pucks;
    }
    // Update is called once per frame
    void Update()
    {
        if (remaining == 0)
        {
            int i = puckCounter.CountAndDestroy();
            remaining = i;
            return;
        }
    }

    public GameObject Spawn()
    {
        Debug.Log("SPAWN");
        if (Time.time < time)
            return null;
        time = Time.time + Time.deltaTime * 40;
        if (secondToLastSpawned != null)
            secondToLastSpawned.GetComponent<PuckHitEffects>().effectsEnabled = false;

        secondToLastSpawned = lastSpawned;
        var e = Instantiate(puckPrefab, transform.position, Quaternion.identity);
        lastSpawned = e;
        remaining--;
        return lastSpawned;
    }
}
