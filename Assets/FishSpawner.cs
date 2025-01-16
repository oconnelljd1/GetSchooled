using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private int initialFish = 2;
    [SerializeField] private Vector3 tankDimensions = new Vector3(1,1,1);

    // Start is called before the first frame update
    void Start()
    {
        for(var i = 0; i < initialFish; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-tankDimensions.x, tankDimensions.x),
                Random.Range(-tankDimensions.y, tankDimensions.y),
                Random.Range(-tankDimensions.z, tankDimensions.z)
            );
            Instantiate(fishPrefab, position, new Quaternion());
        }
    }
}
