using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string type = "";
    public GameObject NorthNode;
    public GameObject EastNode;
    public GameObject SouthNode;
    public GameObject WestNode;
    public List<Exploit> exploits = new List<Exploit>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
