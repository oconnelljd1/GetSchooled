using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class School : MonoBehaviour
{
    [Range(1, 10)]
    public int behaviourFrequency = 5;

    [SerializeField]
    private float moveSpeed = 1, rotSpeed = 1, avoidDist = 1f;
    [SerializeField]
    private SphereCollider collider;

    [HideInInspector]
    public static List<Fish> allFish = new List<Fish>();
    [HideInInspector]
    public Vector3 averageHeading{get;private set;}
    [HideInInspector]
    public Vector3 averagePosition{get;private set;}

    private float initRadius;
    // private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        // targetPosition = transform.position;
        initRadius = collider.radius;
    }

    // Update is called once per frame
    private void Update()
    {
        // Vector3 offset = Vector3.zero;
        // offset += transform.forward * Input.GetAxis("Vertical");
        // offset += transform.right * Input.GetAxis("Horizontal");
        // offset += transform.up * Input.GetAxis("Oblique");
        // offset = offset.normalized * (moveSpeed * Time.deltaTime);
        // offset += transform.position;
        // transform.LookAt(offset);
        // transform.position = offset;
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
        Vector3 vAvoid;
        Vector3 displacement;
        Vector3 direction;
        foreach(Fish fish in allFish)
        {
            // Debug.Log(vCenter, fish);
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
            direction = (transform.position - fish.transform.position) + vAvoid;
            if(direction != Vector3.zero)
            {
                // Debug.Log(direction);
                fish.transform.LookAt(fish.transform.position + direction);
                // fish.transform.Rotate(direction.normalized * rotSpeed * Time.deltaTime);
                // fish.transform.Translate(0, 0, Time.deltaTime * moveSpeed);
            }
            else
            {
                // add perlin noise
            }
        }
    }
}
