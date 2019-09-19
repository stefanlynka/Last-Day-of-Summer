using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollButton : MonoBehaviour
{
    public GameObject ContestantManager;
    // Start is called before the first frame update
    void Start(){
        ContestantManager = GameObject.Find("Contestant Manager");
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void OnMouseDown() {
        if (StateController.State==2) {
            ContestantManager.GetComponent<ContestantManager>().RollDice();
        }
    }
}
