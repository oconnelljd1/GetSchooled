using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [HideInInspector]
    public Vector3 direction = Vector3.zero;
    [SerializeField]
    private float moveSpeed = 1f, rotSpeed = 1f;

    private void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * moveSpeed);
        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, rotSpeed * Time.deltaTime, 0f));
        }
    }
}