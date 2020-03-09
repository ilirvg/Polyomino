using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public SpawnController spawnController;

    private Transform selectedPlayer;
    private int selectedPolyminoInt = 0;
    private int nextPlayer = 1;

    private void Start() {
        StartCoroutine(SelectedPlayer(0.1f));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
            RelesePlayer();
        
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
            selectedPlayer.RotateAround(selectedPlayer.TransformPoint(selectedPlayer.GetComponent<PolyminoController>().rotationPoint), new Vector3(0, 0, 1), 90);
        
        else if (Input.GetKeyDown(KeyCode.RightArrow)) 
            ChangeSelectedPolymino(1);
        
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
            ChangeSelectedPolymino(-1);
    }

    private void RelesePlayer() {
        if (selectedPlayer.parent == null) 
            return;
        
        StartCoroutine(spawnController.InstantiatePlayers(2f, SpawnController.playerSpawnPoints[SpawnController.playerSpawnPoints.IndexOf(selectedPlayer.parent)]));
        selectedPlayer.parent = null;
        ChangeSelectedPolymino(nextPlayer);
        StartCoroutine(selectedPlayer.GetComponent<PolyminoController>().PolyminoVerticalMove());
    }

    private void ChangeSelectedPolymino(int nr) {
        nextPlayer = nr;
        int i = 0;

        foreach (Transform mino in selectedPlayer) {
            mino.GetComponent<SpriteRenderer>().color -= new Color32(255, 255, 255, 255);
        }

        i = selectedPolyminoInt + nr;

        i = GetValidPlayer(i);

        if (SpawnController.playerSpawnPoints[i].childCount <= 0) {
            i = i + nr;
            i = GetValidPlayer(i);
        }

        selectedPolyminoInt = i;
        StartCoroutine(SelectedPlayer(0f));
    }

    private static int GetValidPlayer(int i) {
        if (i < 0) 
            i = SpawnController.playerSpawnPoints.Count - 1;
            
        else if (i >= SpawnController.playerSpawnPoints.Count) 
            i = 0;
        
        return i;
    }

    private IEnumerator SelectedPlayer(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        if (SpawnController.playerSpawnPoints[selectedPolyminoInt].childCount > 0)
            selectedPlayer = SpawnController.playerSpawnPoints[selectedPolyminoInt].GetChild(0);
        else {
            foreach (Transform mino in selectedPlayer) {
                mino.GetComponent<SpriteRenderer>().color -= new Color32(255, 255, 255, 255);
            }
            StartCoroutine(SelectedPlayer(2f));
        }
            

        foreach (Transform mino in selectedPlayer)
            mino.GetComponent<SpriteRenderer>().color += new Color32(255, 255, 255, 255);
    }
}
