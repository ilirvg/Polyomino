using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyminoController : MonoBehaviour {
    private float _rightLimit = 0;
    private float _upLimit = 16;

    private bool isRightLimit;
    private bool brakeLoop;

    private float _prevTime;
    private float _waitTime = 0.3f;

    public Vector3 rotationPoint;

    private List<int[]> minoPrevPositionList = new List<int[]>();

    public List<int[]> minoOccupiedPositionList = new List<int[]>();

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
        foreach (Transform mino in transform) 
            minoPrevPositionList.Add(new int[2]);
    }

    void Update() {
        if (gameObject.tag == "Target")
            PolyminoHorizontalMove();
    }

    private void PolyminoHorizontalMove() {
        if (Time.time - _prevTime > _waitTime) { 
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove()) {
                transform.position -= new Vector3(-1, 0, 0);
                DestroyPolymino();
                return;
            }
            else
                UpdateBoard();

            if (brakeLoop) {
                _prevTime = Time.time;
                Destroy(gameObject);
                return;
            }

            _prevTime = Time.time;
        }
    }

    public IEnumerator PolyminoVerticalMove() {
        while (transform.position.y <= _upLimit) {
            transform.position += new Vector3(0, 1, 0);
            if (!ValidMove()) {
                transform.position -= new Vector3(0, 1, 0);
                DestroyPolymino();
                break;
            }
            else
                UpdateBoard();

            if (brakeLoop) {
                Destroy(gameObject);
                break;
            }
                
            yield return new WaitForSeconds(_waitTime);
        }
        if (gameObject != null)
            Destroy(gameObject);
    }

    private bool ValidMove() {
        foreach (Transform mino in transform) {
            int roundedX = Mathf.RoundToInt(mino.transform.position.x);
            int roundedY = Mathf.RoundToInt(mino.transform.position.y);
            if (roundedX <= _rightLimit) {
                isRightLimit = true;
                return false;
            }
                
            if (GameBoard.grid[roundedX, roundedY] != null && GameBoard.grid[roundedX, roundedY].parent != transform) {
                minoOccupiedPositionList.Add(new int[] { roundedX, roundedY });
                return false;
            }
                
        }
        return true;
    }

    public void UpdateBoard() {
        for (int i = 0; i < transform.childCount; i++) {
            if (GameBoard.grid[minoPrevPositionList[i][0], minoPrevPositionList[i][1]] != null)
                GameBoard.grid[minoPrevPositionList[i][0], minoPrevPositionList[i][1]] = null;
        }

        for (int i = 0; i < transform.childCount; i++) {
            int roundedX = Mathf.RoundToInt(transform.GetChild(i).transform.position.x);
            int roundedY = Mathf.RoundToInt(transform.GetChild(i).transform.position.y);
            GameBoard.grid[roundedX, roundedY] = transform.GetChild(i);
            minoPrevPositionList[i] = new int[] { roundedX, roundedY };
        }
        GameBoard.PrintBoard();
    }

    private void DestroyPolymino() {
        if (isRightLimit)
            Destroy(gameObject);

        else {
            for (int i = 0; i < transform.childCount; i++) {
                if (GameBoard.grid[minoPrevPositionList[i][0], minoPrevPositionList[i][1]] != null)
                    GameBoard.grid[minoPrevPositionList[i][0], minoPrevPositionList[i][1]] = null;
            }

            GameObject go = GameBoard.grid[minoOccupiedPositionList[0][0], minoOccupiedPositionList[0][1]].transform.parent.gameObject;




            int firstMinX = 50;
            int firstMaxX = 0;
            int firstMinY = 50;
            int firstMaxY = 0;

            int secondMinX = 50;
            int secondMaxX = 0;
            int secondMinY = 50;
            int secondMaxY = 0;

            foreach (Transform mino in transform) {
                if (mino.position.x > firstMaxX) {
                    firstMaxX = Mathf.RoundToInt(mino.position.x);
                    
                }

                if (mino.position.x < firstMinX){
                    firstMinX = Mathf.RoundToInt(mino.position.x);
                    
                }

                if (mino.position.y > firstMaxY) {
                    firstMaxY = Mathf.RoundToInt(mino.position.y);
                    
                }

                if (mino.position.y < firstMinY) {
                    firstMinY = Mathf.RoundToInt(mino.position.y);
                    
                }

            }

            foreach (Transform mino in go.transform) {
                if (mino.position.x > secondMaxX) {
                    secondMaxX = Mathf.RoundToInt(mino.position.x);
                    
                }

                if (mino.position.x < secondMinX) {
                    secondMinX = Mathf.RoundToInt(mino.position.x);
                    
                }

                if (mino.position.y > secondMaxY) {
                    secondMaxY = Mathf.RoundToInt(mino.position.y);
                    
                }

                if (mino.position.y < secondMinY) {
                    secondMinY = Mathf.RoundToInt(mino.position.y);
                    
                }
            }
            //Debug.Log("1Max X " + firstMaxX);
            //Debug.Log("1Min X " + firstMinX);
            //Debug.Log("1Max Y " + firstMaxY);
            //Debug.Log("2Min Y " + firstMinY);


            //Debug.Log("2Max X " + secondMaxX);
            //Debug.Log("2Min X " + secondMinX);
            //Debug.Log("2Max Y " + secondMaxY);
            //Debug.Log("2Min Y " + secondMinY);


            if (firstMinX == secondMinX && firstMaxX == secondMaxX) {
                Debug.Log("X is OK");
            }


            go.GetComponent<PolyminoController>().brakeLoop = true;
            Destroy(gameObject);
            return;
        }
    }
}
