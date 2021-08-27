using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public Transform player;
    public Vector3 init = new Vector3(0, 0, 0);
    private float smoothSpeed = 0.5f;
    private Vector3 refVelocity;
    // Start is called before the first frame update
    void Start()
    {
        HandleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
    }

    protected virtual void HandleCamera()
    {
        if (!player)
            return;

        Vector3 finalPosition = player.position + init;

        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed);
    }
}
