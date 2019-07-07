using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    new Camera camera;
    public float scrollSpeed = -0.1f;
    public float maxZoom = 2.2f;
    public float minZoom = 12f;
    
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        camera.orthographicSize = constrain(camera.orthographicSize + scrollSpeed * Input.mouseScrollDelta.y, maxZoom, minZoom);
    }

    float constrain(float target, float min, float max) {
        if(target > max) {
            return max;
        }
        else if (target < min) {
            return min;
        }
        return target;
    }
}
