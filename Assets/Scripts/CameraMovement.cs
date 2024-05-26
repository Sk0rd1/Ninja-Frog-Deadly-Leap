using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerPosition pPos;

    [SerializeField]
    private float yOffset = 2;
    [SerializeField]
    private float cameraSpeed = 2;

    private Vector3 pos;

    void Update()
    {
        pos = pPos.Get();

        transform.position = Vector3.Lerp(transform.position, new Vector3(0, pos.y + yOffset, transform.position.z), cameraSpeed * Time.deltaTime);
    }
}
