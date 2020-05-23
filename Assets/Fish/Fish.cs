using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [HideInInspector]
    public Vector3 direction = Vector3.zero;
    [SerializeField]
    private float moveSpeed = 1f, rotSpeed = 1f, detectionDist = 1f;
    private int layerMask;

    private void Awake() {
        layerMask = ~LayerMask.GetMask("");
    }

    private void Update()
    {
        RaycastHit hit1;
        Physics.Raycast(transform.position, transform.forward, out hit1, detectionDist, layerMask, QueryTriggerInteraction.Ignore);
        Debug.DrawLine(transform.position, transform.position + (transform.forward * detectionDist), Color.red);
        if(hit1.collider != null)
        {
            RaycastHit hit2;
            Physics.Raycast(
                transform.position,
                Vector3.RotateTowards(transform.forward, transform.right, rotSpeed * Time.deltaTime, 0f),
                out hit1,
                detectionDist,
                layerMask,
                QueryTriggerInteraction.Ignore
            );
            Physics.Raycast(
                transform.position,
                Vector3.RotateTowards(transform.forward, -transform.right, rotSpeed * Time.deltaTime, 0f),
                out hit2,
                detectionDist,
                layerMask,
                QueryTriggerInteraction.Ignore
            );
            if(hit1.distance < hit2.distance)
            {
                direction = -transform.right;
            }
            else
            {
                direction = transform.right;
            }
        }
        transform.Translate(0, 0, Time.deltaTime * moveSpeed);
        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, rotSpeed * Time.deltaTime, 0f));
        }
    }
}