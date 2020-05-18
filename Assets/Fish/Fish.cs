using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 1f, neighborDist = 2, avoidDist = 1;
    [Range(0, 10)]
    public int behaviourFrequency = 5;

    [HideInInspector]
    public Vector3 direction;
    public float moveSpeed{get; private set;} = 1f;

    private void Update()
    {
        // transform.LookAt(transform.position + direction);
        if(Random.Range(0, behaviourFrequency) < 1)
        {
            ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * moveSpeed);
    }

    
    public void ApplyRules()
    {
        Vector3 vCenter = new Vector3();
        Vector3 vAvoid = new Vector3();
        Vector3 displacement = new Vector3();
        int groupSize = 0;
        float gSpeed = 0f;

        foreach (GameObject fish in FishSpawner.allFish)
        {
            if(fish == gameObject)
            {
                continue;
            }
            displacement = fish.transform.position - gameObject.transform.position;
            if(displacement.sqrMagnitude < Mathf.Pow(neighborDist, 2))
            {
                groupSize++;
                vCenter += fish.transform.position;
                gSpeed += fish.GetComponent<Fish>().moveSpeed;
                if(displacement.sqrMagnitude < Mathf.Pow(avoidDist, 2))
                {
                    vAvoid += gameObject.transform.position - fish.transform.position;
                }
            }
        }
        if(groupSize > 0)
        {
            vCenter /= groupSize;
            vCenter += FishSpawner.goalPos - gameObject.transform.position;
            moveSpeed = gSpeed / groupSize;
            Vector3 direction = (vCenter + vAvoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.LookAt(transform.position + direction);
            }
        }
    }
}