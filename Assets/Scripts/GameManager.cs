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
            StartCoroutine(RelesePlayer(1f, selectedPlayer));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            selectedPlayer.RotateAround(selectedPlayer.TransformPoint(selectedPlayer.GetComponent<PlayerPolyminoController>().rotationPoint), new Vector3(0, 0, 1), 90);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ChangeSelectedpolymino(selectedPolyminoInt + 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ChangeSelectedpolymino(selectedPolyminoInt - 1);
        }
    }

    private void ChangeSelectedpolymino(int i) {
        if (i <= 0)
            i = 0;
        else if (i >= SpawnController.playerSpawnPoints.Count)
            i = SpawnController.playerSpawnPoints.Count - 1;

        selectedPolyminoInt = i;
        StartCoroutine(SelectedPlayer(0f));
    }

    private IEnumerator RelesePlayer(float waitTime, Transform obj) {
        StartCoroutine(spawnController.InstantiatePlayers(2f, SpawnController.playerSpawnPoints[SpawnController.playerSpawnPoints.IndexOf(obj.parent)]));
        obj.parent = null;
        ChangeSelectedpolymino(selectedPolyminoInt + 1);

        while (obj.position.y <= 18) {
            Debug.Log(obj.position.y);
            obj.position += new Vector3(0, 1, 0);
            obj.GetComponent<PlayerPolyminoController>().UpdateGrid();
            yield return new WaitForSeconds(waitTime);
        }
        Destroy(obj.gameObject);
    }

    private IEnumerator SelectedPlayer(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if (SpawnController.playerSpawnPoints[selectedPolyminoInt].childCount > 0)
            selectedPlayer = SpawnController.playerSpawnPoints[selectedPolyminoInt].GetChild(0);
    }

}
