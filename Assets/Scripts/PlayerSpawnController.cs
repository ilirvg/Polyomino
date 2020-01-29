using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour {
    [SerializeField] private List<GameObject> _poliminos = new List<GameObject>();

    private Transform _firstSpawnPoint, _secondSpawnPoint, _thirdSpawnPoint, _fourthSpawnPoint,
        _fifthSpawnPoint, _sixthSpawnPoint, _seventhSpawnPoint, _eighthSpawnPoint;

    private void Start() {
        _firstSpawnPoint = transform.GetChild(0);
        _secondSpawnPoint = transform.GetChild(1);
        _thirdSpawnPoint = transform.GetChild(2);
        _fourthSpawnPoint = transform.GetChild(3);
        _fifthSpawnPoint = transform.GetChild(4);
        _sixthSpawnPoint = transform.GetChild(5);
        _seventhSpawnPoint = transform.GetChild(6);
        _eighthSpawnPoint = transform.GetChild(7);

        InstantiatePolyomino(_firstSpawnPoint);
        InstantiatePolyomino(_secondSpawnPoint);
        InstantiatePolyomino(_thirdSpawnPoint);
        InstantiatePolyomino(_fourthSpawnPoint);
        InstantiatePolyomino(_fifthSpawnPoint);
        InstantiatePolyomino(_sixthSpawnPoint);
        InstantiatePolyomino(_seventhSpawnPoint);
        InstantiatePolyomino(_eighthSpawnPoint);
    }

    private void InstantiatePolyomino(Transform positionToSpawn) {
        int randomObj = Random.Range(0, _poliminos.Count);
        
        GameObject go = Instantiate(_poliminos[randomObj], positionToSpawn.position, ObjRotation());
        go.GetComponent<PolyminoController>().enabled = false;
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
