using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject objectPrefab = null;

    [SerializeField]
    int maxObjects = 20;

    [SerializeField]
    float spawnFreq = 5f;
    [SerializeField]
    bool shouldSpawn = true;

    private List<GameObject> _gameObjects;

    float time; // I could use a coroutine but... I think that would be over doing it
    System.Random ran = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        _gameObjects = new List<GameObject>();
        time = spawnFreq + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSpawn)
        {
            time += Time.deltaTime;
            if(time > spawnFreq)
            {
                Spawn();
                time = 0;
            }
        }
        else
        {
            time = 0;
        }

        for(int i = 0; i < _gameObjects.Count; i++)
        {
            if (_gameObjects[i] == null)
                _gameObjects.Remove(_gameObjects[i]);
        }
    }

    private void Spawn()
    {
        if(_gameObjects.Count < maxObjects)
            SpawnObjectAtRandomSpawnPoint();
    }

    private void SpawnObjectAtRandomSpawnPoint()
    {
        int i = ran.Next(0, this.transform.childCount-1);

        SpawnObjectAtSpawnPoint(i);
    }

    private void SpawnObjectAtSpawnPoint(int i)
    {
        var position = this.transform.GetChild(i).position;

        var g = Instantiate(objectPrefab, position, this.transform.rotation);
        g.transform.SetParent(this.transform);
        _gameObjects.Add(g);
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
