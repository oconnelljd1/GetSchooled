using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float maxFollowDist = 10f, minFollowDist = 5f, moveSpeed = 1f, rotSmoothTime = 0.1f;
    [SerializeField]
    private Vector2 rotSpeed;

    private float yaw = 0f, pitch = 0f;
    private Vector2 pitchMinMax = new Vector2(-45f, 89f);
    private Vector3 currentRotation, rotSmoothVel;

    void Start()
    {
        // lastRotation = transform.eulerAngles.y;
        currentRotation = transform.eulerAngles;
        transform.position = target.position - (target.forward * 8) + (Vector3.up * 6);
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * rotSpeed.x;
        pitch -= Input.GetAxis("Mouse Y") * rotSpeed.y;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation =  Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotSmoothVel, rotSmoothTime);
        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * maxFollowDist;

        // Calculate the direction that you want the school to face
        Vector3 direction = new Vector3(
            Mathf.Sin(currentRotation.y * Mathf.Deg2Rad) * Input.GetAxisRaw("Vertical"),
            Input.GetAxisRaw("Oblique"),
            Mathf.Cos(currentRotation.y * Mathf.Deg2Rad) * Input.GetAxisRaw("Vertical")
        ).normalized;

        if(direction != Vector3.zero)
        {
            // rotate the school towards the direction it is supposed to move in
            target.rotation = Quaternion.LookRotation(Vector3.RotateTowards(target.forward, direction, 90 * Time.deltaTime * Mathf.Deg2Rad, 0f));
            // Move the school forwards
            target.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
    }
}
