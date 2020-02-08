using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPolyminoController : MonoBehaviour {
    private float rightLimit = -5;

    private float prevTime;
    private float fallTime = 0.4f;

    public Vector3 rotationPoint;

    private void Start() {
        int randomRotation = Random.Range(0, 4);
        switch (randomRotation) {
            case 0:
                break;
            case 1:
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                break;
            case 2:
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 180);
                break;
            default:
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 360);
                break;
        }
    }

    void FixedUpdate() {
        if (transform.position.x < rightLimit)
            Destroy(gameObject);

        if (Time.time - prevTime > fallTime) {
            transform.position += new Vector3(-1, 0, 0);
            prevTime = Time.time;
        }
    }
}
