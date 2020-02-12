using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPolyminoController : MonoBehaviour {
    public Vector3 rotationPoint;
    
    private void Start() {
        int randomRotation = Random.Range(0, 4);
        switch (randomRotation) {
            case 0:
                break;
            case 1:
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                break;
            case 2:
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 180);
                break;
            default:
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 360);
                break;
        }
    }

    private void Update() {
        //if (Input.GetMouseButtonDown(0)) {
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        //    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        //    if (hit.collider != null) {
        //        Debug.Log(hit.collider.name);
        //        StartCoroutine(LoopFunction(1f, hit.transform.parent));
        //    }
        //}

        
    }

    private IEnumerator LoopFunction(float waitTime, Transform obj) {
        Debug.Log("Called");
        Debug.Log(obj.name);
        int i = 0;
        while (obj.position.y < 20) {
            obj.position += new Vector3(0, 1, 0);
            Debug.Log("step " + i);
            i++;
            yield return new WaitForSeconds(waitTime);
        }

    }
}
