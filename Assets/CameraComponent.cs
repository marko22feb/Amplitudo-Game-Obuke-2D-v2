using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    Transform playerTransform;
    public Vector3 offset;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = playerTransform.position + offset;
    }
}
