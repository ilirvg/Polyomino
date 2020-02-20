using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour {
    public static Transform[,] grid = new Transform[40, 22];


    private void Start() {
        for (int y = 0; y < 22; ++y) {
            for (int x = 0; x < 40; ++x) {

                //if (GameBoard.grid[x, y] != null) {
                    //Debug.Log("Clear");
                    GameBoard.grid[x, y] = null;
                //}
            }
        }
    }
    public static void PrintBoard() {
        string arrayOutput = "";

        //int iMax = grid.GetLength(0) - 1;
        //int jMax = grid.GetLength(1) - 1;

        for (int j = 16; j >= 0; j--) {
            for (int i = 0; i <= 30; i++) {
                if (grid[i, j] == null) {
                    arrayOutput += "N   ";
                }
                else {
                    arrayOutput += "X   ";
                }
            }
            arrayOutput += "\n";
            
        }
        var arratText = GameObject.Find("BoardTExt").GetComponent<Text>();
        arratText.text = arrayOutput;
    }

}
