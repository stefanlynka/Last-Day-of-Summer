using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedButton : MonoBehaviour
{
    public List<GameObject> Party;
    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        
    }
    public void OnMouseDown() {
        if (StateController.State == 4) {
            print("Population.count = " + Population.Party.Count);
            if (Inventory.FoodCount()>= Population.Party.Count) {
                StateController.GoToAllocateState();
                Inventory.UpdateFood(-1 * Population.Party.Count);
                print("Mouths 2 feed = " + Population.Party.Count);
            }
        }
    }
}
