using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float maxFollowDist = 10f, minFollowDist = 5f, moveSpeed = 1f, rotSpeed = 45f;
    private float lastRotation;

    void Start()
    {
        lastRotation = transform.eulerAngles.y;
        transform.position = target.position - (target.forward * 8) + (Vector3.up * 6);
    }

    void Update()
    {
        if(Input.GetButton("Horizontal"))
        {
            // Rotate camera around school
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime);
            lastRotation = transform.eulerAngles.y;
        }

        // Calculate the direction that you want the school to face
        Vector3 direction = new Vector3(
            Mathf.Sin(lastRotation * Mathf.Deg2Rad) * Input.GetAxisRaw("Vertical"),
            Input.GetAxisRaw("Oblique"),
            Mathf.Cos(lastRotation * Mathf.Deg2Rad) * Input.GetAxisRaw("Vertical")
        ).normalized;

        if(direction != Vector3.zero)
        {
            // rotate the school towards the direction it is supposed to move in
            target.rotation = Quaternion.LookRotation(Vector3.RotateTowards(target.forward, direction , rotSpeed * Time.deltaTime * Mathf.Deg2Rad, 0f));
            // Move the school forwards
            target.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }

        Vector3 distance = transform.position - target.position;
        // Move camera in the direction the school is facing if it is too far away
        if(distance.sqrMagnitude > Mathf.Pow(maxFollowDist, 2))
        {
            transform.position += target.forward * Time.deltaTime * moveSpeed;
        }
        // Move camera away from school if it is too close
        else if(distance.sqrMagnitude < Mathf.Pow(minFollowDist, 2))
        {
            transform.position += distance.normalized * Time.deltaTime * moveSpeed;
        }
        // Look at the school
        transform.LookAt(target.position);
    }
}
