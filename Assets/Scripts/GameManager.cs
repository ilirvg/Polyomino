using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public SpawnController spawnController;

    private int selectedPolyminoInt = 0;
    private Transform selectedPlayer;

    private void Start() {
        StartCoroutine(SelectedPlayer(0.1f));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            RelesePlayer();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            selectedPlayer.RotateAround(selectedPlayer.TransformPoint(selectedPlayer.GetComponent<TargetPolyminoController>().rotationPoint), new Vector3(0, 0, 1), 90);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ChangeSelectedPolymino(selectedPolyminoInt + 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ChangeSelectedPolymino(selectedPolyminoInt - 1);
        }
    }

    private void ChangeSelectedPolymino(int i) {
        if (i <= 0)
            i = 0;
        else if (i >= SpawnController.playerSpawnPoints.Count)
            i = SpawnController.playerSpawnPoints.Count - 1;

        selectedPolyminoInt = i;
        StartCoroutine(SelectedPlayer(0f));
    }

    private void RelesePlayer(){
        StartCoroutine(spawnController.InstantiatePlayers(2f, SpawnController.playerSpawnPoints[SpawnController.playerSpawnPoints.IndexOf(selectedPlayer.parent)]));
        selectedPlayer.parent = null;
        ChangeSelectedPolymino(selectedPolyminoInt + 1);
        StartCoroutine(selectedPlayer.GetComponent<TargetPolyminoController>().PolyminoVerticalMove(1f));
    }

    private IEnumerator SelectedPlayer(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if (SpawnController.playerSpawnPoints[selectedPolyminoInt].childCount > 0)
            selectedPlayer = SpawnController.playerSpawnPoints[selectedPolyminoInt].GetChild(0);
    }

}
