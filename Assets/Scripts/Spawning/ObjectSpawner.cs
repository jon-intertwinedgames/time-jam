using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject objectPrefab = null;

    [SerializeField]
    float spawnFreq = 5f;
    [SerializeField]
    bool shouldSpawn = true;


    float time; // I could use a coroutine but... I think that would be over doing it
    System.Random ran = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSpawn)
        {
            time += Time.deltaTime;
            if(time > spawnFreq)
            {
                SpawnObjectAtRandomSpawnPoint();
                time = 0;
            }
        }
        else
        {
            time = 0;
        }
    }

    private void SpawnObjectAtRandomSpawnPoint()
    {
        int i = ran.Next(0, this.transform.childCount-1);

        SpawnObjectAtSpawnPoint(i);
    }

    private void SpawnObjectAtSpawnPoint(int i)
    {
        var position = this.transform.GetChild(i).position;

        Instantiate(objectPrefab, position, this.transform.rotation)
            .transform.SetParent(this.transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        foreach (Transform child in this.transform)
        {
            Gizmos.DrawCube(child.position, new Vector3(1,1,1) * 1f);
        }
        
    }
}
