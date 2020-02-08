using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    [SerializeField] private List<GameObject> _targetPoliminos = new List<GameObject>();
    [SerializeField] private List<GameObject> _playerPoliminos = new List<GameObject>();

    private Transform[] _targetSpawnPoints = new Transform[2];
    private Transform[] _playerSpawnPoints = new Transform[8];

    private float _timeToWait = 2f;
    private float _previousTime = 0f;

    private int targetSpawnPoint;

    private void Start() {
        for (int i = 0; i < _targetSpawnPoints.Length; i++) {
            _targetSpawnPoints[i] = transform.GetChild(0).GetChild(i);
        }

        for (int i = 0; i < _playerSpawnPoints.Length; i++) {
            _playerSpawnPoints[i] = transform.GetChild(1).GetChild(i);
            //StartCoroutine(InstantiatePolyomino(_playerSpawnPoints[i], 0, false));
        }

    }

    private void Update() {
        if (Time.time - _previousTime > _timeToWait) {
            Instantiate(_targetPoliminos[Random.Range(0, _targetPoliminos.Count)],
                _targetSpawnPoints[targetSpawnPoint].position, Quaternion.identity);

            targetSpawnPoint = targetSpawnPoint == 0 ? 1 : 0;

            _previousTime = Time.time;
        }
    }

    //private void InstantiatePlayers() {
    //    Instantiate(_playerPoliminos[Random.Range(0, _playerPoliminos.Count)], _playerPoliminos[targetSpawnPoint].position, Quaternion.identity);
    //}
}
