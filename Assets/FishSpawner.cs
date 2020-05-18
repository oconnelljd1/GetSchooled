using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject fishPrefab;
    [SerializeField]
    private int initialFish = 2;
    [SerializeField]
    private Vector3 tankDimensions = new Vector3(1,1,1);
    
    [HideInInspector]
    public static List<GameObject> allFish = new List<GameObject>();
    
    [HideInInspector]
    public static Vector3 goalPos;
    
    void Start()
    {
        goalPos = RandomTankPos();
        for(var i = 0; i < initialFish; i++)
        {
            SpawnFish();
        }
    }

    private void Update() {
        if(Random.Range(0, 50) < 1)
        {
            goalPos = RandomTankPos();
        }
    }


    void SpawnFish()
    {
        Vector3 position = RandomTankPos();
        Quaternion rotation = Quaternion.identity;
        // rotation.eulerAngles = new Vector3(
        //     Random.Range(0, 360),
        //     Random.Range(0, 360),
        //     Random.Range(0, 360)
        // );
        allFish.Add(Instantiate(fishPrefab, position, rotation));
    }

    private Vector3 RandomTankPos()
    {
        return new Vector3(
            Random.Range(-tankDimensions.x, tankDimensions.x),
            Random.Range(-tankDimensions.y, tankDimensions.y),
            Random.Range(-tankDimensions.z, tankDimensions.z)
        );
    }
}
