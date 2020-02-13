using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPolyminoController : MonoBehaviour {
    private float _rightLimit = -5;

    private float _prevTime;
    private float _waitTime = 0.4f;

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

    void Update() {
        if(ValidMove() == false) {
            Debug.Log("Collide");
        }
        if (transform.position.x < _rightLimit)
            Destroy(gameObject);

        if (Time.time - _prevTime > _waitTime) {
            transform.position += new Vector3(-1, 0, 0);
            _prevTime = Time.time;
        }
    }

    private bool ValidMove() {
        foreach (Transform mino in transform) {

            int roundedX = Mathf.RoundToInt(mino.transform.position.x);
            int roundedY = Mathf.RoundToInt(mino.transform.position.y);

            if (GameBoard.grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }

    public void UpdateGameBoard() {

    }
}
