using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject Occupant;
    public bool occupied = false;
    public int size = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if (Occupant) {
            Vector3 newPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
            newPos.z -= 0.5f;
            Occupant.transform.position = newPos;
        }
    }
}
