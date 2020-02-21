using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPolyminoController : MonoBehaviour {
    private float _rightLimit = 2;
    private float _upLimit = 18;

    private float _prevTime;
    private float _waitTime = 0.4f;

    public Vector3 rotationPoint;

    private int[] firstMino = new int[2];
    private int[] secondMino = new int[2];
    private int[] thirdMino = new int[2];
    private int[] fourthMino = new int[2];

    //private int[,] lastMinoPositions = new int[4, 2];

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
        if (gameObject.tag == "Target")
            PolyminoHorizontalMove();
    }

    private void PolyminoHorizontalMove() {
        if (Time.time - _prevTime > _waitTime) { 
            transform.position += new Vector3(-1, 0, 0);

            if (!ValidMove()) {
                Destroy(gameObject);
            }
            else {
                UpdateGrid();
            }
            _prevTime = Time.time;
        }
    }

    public IEnumerator PolyminoVerticalMove(float waitTime) {
        while (transform.position.y <= 18) {
            transform.position += new Vector3(0, 1, 0);
            UpdateGrid();
            yield return new WaitForSeconds(waitTime);
        }
        if(gameObject != null)
            Destroy(gameObject);
    }

    private bool ValidMove() {
        
        foreach (Transform mino in transform) {
            int roundedX = Mathf.RoundToInt(mino.transform.position.x);
            int roundedY = Mathf.RoundToInt(mino.transform.position.y);

            if (roundedX <= _rightLimit)
                return false;

            if (GameBoard.grid[roundedX, roundedY] != null && GameBoard.grid[roundedX, roundedY].parent != transform) {// 
                Debug.Log("X " + roundedX + " Y " + roundedY);
                return false;
            }
                
        }
        return true;
    }

    public void UpdateGrid() {
        //for (int y = 0; y < 22; y++) {
        //    for (int x = 0; x < 40; x++) {
        //        if (GameBoard.grid[x, y] != null ) {//&& GameBoard.grid[x, y].parent == transform
        //            GameBoard.grid[x, y] = null;
        //        }
        //    }
        //}

        for (int i = 0; i < 4; i++) {
            if (i == 0) {
                GameBoard.grid[firstMino[0], firstMino[1]] = null;
            }
            else if (i == 1) {
                GameBoard.grid[secondMino[0], secondMino[1]] = null;
            }
            else if (i == 2) {
                GameBoard.grid[thirdMino[0], thirdMino[1]] = null;
            }
            else if (i == 3) {
                GameBoard.grid[fourthMino[0], fourthMino[1]] = null;
            }
        }
        

        //foreach (Transform mino in transform) {
        //    int roundedX = Mathf.RoundToInt(mino.transform.position.x);
        //    int roundedY = Mathf.RoundToInt(mino.transform.position.y);
        //    GameBoard.grid[roundedX, roundedY] = mino;
        //}
        for (int i = 0; i < transform.childCount; i++) {
            int roundedX = Mathf.RoundToInt(transform.GetChild(i).transform.position.x);
            int roundedY = Mathf.RoundToInt(transform.GetChild(i).transform.position.y);
            GameBoard.grid[roundedX, roundedY] = transform.GetChild(i);

            if (i == 0) {
                firstMino[0] = roundedX;
                firstMino[1] = roundedY;
            }
            else if (i == 1) {
                secondMino[0] = roundedX;
                secondMino[1] = roundedY;
            }
            else if (i == 2) {
                thirdMino[0] = roundedX;
                thirdMino[1] = roundedY;
            }
            else if (i == 3) {
                fourthMino[0] = roundedX;
                fourthMino[1] = roundedY;
            }
        }
        

        GameBoard.PrintBoard();
    }
}
