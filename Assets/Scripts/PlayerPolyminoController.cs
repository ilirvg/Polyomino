using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPolyminoController : MonoBehaviour {
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

    public void AddToGrid() {
        foreach (Transform mino in transform) {

            int roundedX = Mathf.RoundToInt(mino.transform.position.x);
            int roundedY = Mathf.RoundToInt(mino.transform.position.y);

            GameBoard.grid[roundedX, roundedY] = mino;

            Debug.Log(GameBoard.grid[roundedX, roundedY]);
        }

    }

}
