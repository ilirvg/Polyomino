using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private int selectedPolyminoInt = 0;
    private Transform selectedPlayer;

    private void Start() {
        StartCoroutine(SelectedPlayer());
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
        selectedPlayer = SpawnController.playerSpawnPoints[selectedPolyminoInt].GetChild(0);
    }

    private IEnumerator RelesePlayer(float waitTime, Transform obj) {
        obj.transform.parent = null;
        SpawnController.playerSpawnPoints.RemoveAt(selectedPolyminoInt);
        Debug.Log(SpawnController.playerSpawnPoints.Count);
        while (obj.position.y < 20) {
            obj.position += new Vector3(0, 1, 0);
            yield return new WaitForSeconds(waitTime);
        }

    }

    private IEnumerator SelectedPlayer() {
        yield return new WaitForSeconds(0.1f);
        selectedPlayer = SpawnController.playerSpawnPoints[selectedPolyminoInt].GetChild(0);
        Debug.Log("AA");
    }

}
