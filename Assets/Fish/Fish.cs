using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f, rotSpeed = 1f;

    [HideInInspector]
    public Vector3 direction;

    private void Update()
    {
        transform.LookAt(transform.position + direction);
        transform.Translate(transform.forward * (Time.deltaTime * moveSpeed));
    }
}