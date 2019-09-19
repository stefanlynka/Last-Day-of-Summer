using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panner : MonoBehaviour
{
    public Vector3 Target;
    Vector3 TargetLocal;
    int panSpeed = 20;
    // Start is called before the first frame update
    void Start(){
        Target = transform.position;
        TargetLocal = transform.localPosition;
    }

    // Update is called once per frame
    void Update(){
        UpdatePosition();
    }

    public void SetTarget(Vector3 target) {
        Target = target;
    }

    public void SetTargetLocal(Vector3 target) {
        TargetLocal = target;
    }

    void UpdatePosition() {
        if(Vector3.Distance(transform.position, Target)>0.01f) {
            //transform.position += (Target - transform.position) / panSpeed;
        }
        if (Vector3.Distance(transform.localPosition, TargetLocal) > 0.01f) {
            transform.localPosition += (TargetLocal - transform.localPosition) / panSpeed;
        }
    }
}
