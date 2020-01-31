using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPolyminoController : MonoBehaviour {
    private float rightLimit = -40f;

    private Rigidbody2D rb;

    private float speed = 2f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        tag = "Target";
    }

    void FixedUpdate() {
        if (transform.position.x > rightLimit) 
            rb.velocity = Vector2.left * speed;
        else 
            Destroy(gameObject);
    }
}
