using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    [SerializeField] private List<GameObject> _poliminos = new List<GameObject>();

    private Transform[] _targetSpawnPoints = new Transform[2];
    private Transform[] _playerSpawnPoints = new Transform[8];

    private float _timeToWait = 4f;
    private float _previousTime = 0f;

    private void Start() {
        for (int i = 0; i < _targetSpawnPoints.Length; i++) {
            _targetSpawnPoints[i] = transform.GetChild(0).GetChild(i);
            StartCoroutine(InstantiatePolyomino(_targetSpawnPoints[i], 0));
        }

        for (int i = 0; i < _playerSpawnPoints.Length; i++) {
            _playerSpawnPoints[i] = transform.GetChild(1).GetChild(i);
            StartCoroutine(InstantiatePolyomino(_playerSpawnPoints[i], 0, false));
        }

        _previousTime = Time.time;
    }

    private void Update() {
        if (Time.time - _previousTime > _timeToWait) {
            float firstRandomWait = Random.Range(3, 5);
            float secondRandomWait = Random.Range(3, 5);
            StartCoroutine(InstantiatePolyomino(_targetSpawnPoints[0], firstRandomWait));
            StartCoroutine(InstantiatePolyomino(_targetSpawnPoints[1], secondRandomWait));

            _previousTime = Time.time;
        }
    }

    private IEnumerator InstantiatePolyomino(Transform positionToSpawn, float waitTime, bool isTarget = true) {
        int randomObj = Random.Range(0, _poliminos.Count);
        yield return new WaitForSeconds(waitTime);
        GameObject go = Instantiate(_poliminos[randomObj], positionToSpawn.position, ObjRotation());

        if (isTarget) {
            go.AddComponent<TargetPolyminoController>();
        }

        else {
            go.AddComponent<PlayerPolyminoController>();
        }
            
    }

    private Quaternion ObjRotation() {
        int randomRotation = Random.Range(0, 4);
        Quaternion rotation;
        switch (randomRotation) {
            case 0:
                rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 2:
                rotation = Quaternion.Euler(0, 0, 180);
                break;
            default:
                rotation = Quaternion.Euler(0, 0, 360);
                break;
        }
        return rotation;
    }
}
