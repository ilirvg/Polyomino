using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    [SerializeField] private List<GameObject> _targetPoliminos = new List<GameObject>();
    [SerializeField] private List<GameObject> _playerPoliminos = new List<GameObject>();

    public Transform _targetPolyminoContainer;

    private Transform[] _targetSpawnPoints = new Transform[2];
    public static List<Transform> playerSpawnPoints = new List<Transform>();

    private float _waitTime = 2f;
    private float _previousTime = 0f;

    private int targetSpawnPoint;

    private void Start() {
        for (int i = 0; i < _targetSpawnPoints.Length; i++) {
            _targetSpawnPoints[i] = transform.GetChild(0).GetChild(i);
        }

        for (int i = 0; i < 5; i++) {
            playerSpawnPoints.Add(transform.GetChild(1).GetChild(i));
            StartCoroutine(InstantiatePlayers(0f, playerSpawnPoints[i]));
        }
    }

    private void Update() {
        if (Time.time - _previousTime > _waitTime) {
            InstantiateTargets();
            _previousTime = Time.time;
        }
    }

    private void InstantiateTargets() {
        GameObject go = Instantiate(_targetPoliminos[Random.Range(0, _targetPoliminos.Count)], _targetSpawnPoints[targetSpawnPoint].position, Quaternion.identity);
        targetSpawnPoint = targetSpawnPoint == 0 ? 1 : 0;
        go.transform.SetParent(_targetPolyminoContainer);
    }

    public IEnumerator InstantiatePlayers(float waitTime, Transform transform) {
        yield return new WaitForSeconds(waitTime);
        GameObject go = Instantiate(_playerPoliminos[Random.Range(0, _playerPoliminos.Count)], transform.position, Quaternion.identity);
        go.transform.SetParent(transform);
    }
}
