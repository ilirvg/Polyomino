using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPolyminoController : MonoBehaviour {

    private Rigidbody2D rb;

    List<GameObject> currentCollisions = new List<GameObject>();

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        tag = "Player";
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                hit.collider.attachedRigidbody.AddForce(Vector2.up * 25);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Target") {
            Destroy(rb);
            transform.SetParent(collision.transform);

            currentCollisions.Add(collision.gameObject);

            // Print the entire list to the console.
            foreach (GameObject gObject in currentCollisions) {
                Debug.Log(gObject.name);
            }

            //if (IsMatch(collision.gameObject.name)) {
            //    Debug.Log("++");
            //}
            //else {
            //    Debug.Log("--");
            //}
            Destroy(collision.gameObject);
        }
    }

    private bool IsMatch(string targetName) {
        if (gameObject.name == "1(Clone)") {
            if (targetName == "1(Clone)" || targetName == "5(Clone)" || targetName == "8(Clone)") {
                
                return true;
            }
            else
                return false;
        }
        else if(gameObject.name == "2(Clone)") {
            if (targetName == "2(Clone)" || targetName == "7(Clone)") {
                return true;
            }
            else
                return false;
        }
        else if (gameObject.name == "3(Clone)") {
            if (targetName == "4(Clone)") {
                return true;
            }
            else
                return false;
        }
        else if (gameObject.name == "4(Clone)") {
            if (targetName == "3(Clone)" || targetName == "9(Clone)") {
                return true;
            }
            else
                return false;
        }
        else if (gameObject.name == "5(Clone)") {
            if (targetName == "1(Clone)" || targetName == "6(Clone)") {
                return true;
            }
            else
                return false;
        }
        else {
            return false;
        }
    }
}
