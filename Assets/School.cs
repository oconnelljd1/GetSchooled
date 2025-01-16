using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1, avoidDist = 1f;
    [Range(0, 10)] public int behaviourFrequency = 5;
    [HideInInspector] public static List<Fish> allFish = new List<Fish>();
    [HideInInspector] public Vector3 averageHeading{get;private set;}
    [HideInInspector] public Vector3 averagePosition{get;private set;}
    private Vector3 targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = Vector3.zero;
        offset += transform.forward * Input.GetAxis("Vertical");
        offset += transform.right * Input.GetAxis("Horizontal");
        offset += transform.up * Input.GetAxis("Oblique");
        offset = offset.normalized;
        targetPosition = transform.position + offset;

        ApplyRules();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fish")
        {
            Fish newFish = other.GetComponent<Fish>();
            if(!allFish.Contains(newFish))
            {
                allFish.Add(newFish);
            }
        }
    }

    public void ApplyRules()
    {
        Vector3 totalPosition = Vector3.zero;
        Vector3 totalHeading = Vector3.zero;
        Vector3 distance = new Vector3();
        Vector3 vAvoid = new Vector3();
        foreach (Fish fish in allFish)
        {
            totalPosition += fish.gameObject.transform.position;
            totalHeading += fish.direction;
            if(Random.Range(0, behaviourFrequency) > 1)
            {
                continue;
            }
            vAvoid = Vector3.zero;
            foreach(Fish otherFish in allFish)
            {
                if(fish == otherFish)
                {
                    continue;
                }
                distance = fish.transform.position - otherFish.transform.position;
                if(distance.sqrMagnitude < Mathf.Pow(avoidDist, 2))
                {
                    vAvoid += distance;
                }
            }
            fish.direction = averagePosition + vAvoid + targetPosition;
        }
        averagePosition = totalPosition / allFish.Count;
        averageHeading = totalHeading / allFish.Count;
    }
}
