using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnController : MonoBehaviour {
    [SerializeField] private List<GameObject> _poliminos = new List<GameObject>();

    private Transform _firstSpawnPoint, _secondSpawnPoint;

    private float _timeToWait = 4f;
    private float _previousTime = 0f;

    private void Start() {
        _firstSpawnPoint = transform.GetChild(0);
        _secondSpawnPoint = transform.GetChild(1);
    }

    private void Update() {
        if (Time.time - _previousTime > _timeToWait) {
            StartCoroutine(InstantiatePolyomino(_firstSpawnPoint));
            StartCoroutine(InstantiatePolyomino(_secondSpawnPoint));
            _previousTime = Time.time;
        }
    }

    private IEnumerator InstantiatePolyomino(Transform positionToSpawn) {
        int randomWait = Random.Range(3, 5);
        int randomObj = Random.Range(0, _poliminos.Count);
        yield return new WaitForSeconds(randomWait);
        GameObject go = Instantiate(_poliminos[randomObj], positionToSpawn.position, ObjRotation());
        //go.GetComponent<Rigidbody2D>().;
    }

    private Quaternion ObjRotation() {
        int randomRotation = Random.Range(0, 4);
        Quaternion rotation;
        switch (randomRotation) {
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
