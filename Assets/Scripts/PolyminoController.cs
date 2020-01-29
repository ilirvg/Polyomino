using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyminoController : MonoBehaviour {
    public float rightLimit = 0f;
    public float speed = 2.0f;
    private void Start() {
        //speed = Random.Range(speed, speed + 0.2f);
    }
    void Update() {
        if (transform.position.x > rightLimit) {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        else {
            Destroy(gameObject);
        }
    }
}
