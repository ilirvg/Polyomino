using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    [SerializeField] private List<GameObject> _poliminos = new List<GameObject>();

    private Transform _firstSpawnPoint, _secondSpawnPoint;

    private float _timeToWait = 5f;
    private float _previousTime = 0f;

    private void Start() {
        _firstSpawnPoint = transform.GetChild(0);
        _secondSpawnPoint = transform.GetChild(1);
    }

    private void Update() {
        if (Time.time - _previousTime > _timeToWait) {
            InstantiatePolyomino(_firstSpawnPoint);
            InstantiatePolyomino(_secondSpawnPoint);
            _previousTime = Time.time;
        }
    }

    private void InstantiatePolyomino(Transform positionToSpawn) {
        int chooseRandomObj = Random.Range(0, _poliminos.Count);
        Instantiate(_poliminos[chooseRandomObj], positionToSpawn.position, ObjRotation());
    }

    private Quaternion ObjRotation() {
        int chooseRandomRotation = Random.Range(0, 4);
        Quaternion rotation;
        switch (chooseRandomRotation) {
            case 0:
                rotation = Quaternion.Euler(0,0,0);
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
