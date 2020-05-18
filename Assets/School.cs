using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class School : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1, avoidDist = 1f;
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
    void Update()
    {
        Vector3 offset = Vector3.zero;
        offset += transform.forward * Input.GetAxis("Vertical");
        offset += transform.right * Input.GetAxis("Horizontal");
        offset += transform.up * Input.GetAxis("Oblique");
        offset *= moveSpeed * Time.deltaTime;
        offset = offset.normalized;
        offset += transform.position;
        transform.LookAt(offset);
        transform.position = offset;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fish")
        {
            Fish newFish = other.GetComponent<Fish>();
            if(!allFish.Contains(newFish))
            {
                allFish.Add(newFish);
                collider.radius = initRadius + (allFish.Count / 10f);
            }
        }
    }
}
