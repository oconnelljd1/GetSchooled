using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class School : MonoBehaviour
{
    [Range(1, 10)]
    public int behaviourFrequency = 5;

    [SerializeField]
    private float moveSpeed = 1, avoidDist = 1f;
    [SerializeField]
    private SphereCollider collider;

    [HideInInspector]
    public static List<Fish> allFish = new List<Fish>();

    private float initRadius;
    
    void Start()
    {
        initRadius = collider.radius;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 offset = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Oblique"),
            Input.GetAxis("Vertical")
        ).normalized * (Time.deltaTime * moveSpeed);
        transform.position += offset;

        if(allFish.Count > 0)
        {
            ApplyRules();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fish")
        {
            Fish newFish = other.GetComponentInParent<Fish>();
            if(!allFish.Contains(newFish))
            {
                Debug.Log("Adding new fish!");
                allFish.Add(newFish);
                collider.radius = initRadius + (allFish.Count / 10f);
            }
        }
    }

    private void ApplyRules()
    {
        Vector3 vAvoid = new Vector3();
        Vector3 displacement = new Vector3();
        Vector3 direction = new Vector3();
        foreach(Fish fish in allFish)
        {
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
                displacement = otherFish.transform.position - fish.transform.position;
                if(displacement.sqrMagnitude < Mathf.Pow(avoidDist, 2))
                {
                    vAvoid += (fish.transform.position - otherFish.transform.position);
                }
            }
            fish.direction = transform.position - fish.transform.position + vAvoid;
        }
    }
}
