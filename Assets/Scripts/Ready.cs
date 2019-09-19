using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready : MonoBehaviour
{
    GameObject Gamecontroller;
    // Start is called before the first frame update
    void Start()
    {
        Gamecontroller = GameObject.Find("Game Controller");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNextPhase() {
        print("eskketit");
    }
}
