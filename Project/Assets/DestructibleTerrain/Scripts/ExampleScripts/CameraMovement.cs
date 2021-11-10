using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float ScrollSpeed;

    // Update is called once per frame
    void Update()
    {
        Camera.main.orthographicSize -= Input.mouseScrollDelta.y * ScrollSpeed;
        Camera.main.orthographicSize = Mathf.Max(1, Camera.main.orthographicSize);
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * (MoveSpeed * Time.deltaTime * Camera.main.orthographicSize));
    }
}
