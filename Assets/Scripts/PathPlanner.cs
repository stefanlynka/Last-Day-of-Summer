using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlanner : MonoBehaviour
{
    GameObject ScoutingPost;
    int scoutingScore;
    // Start is called before the first frame update
    void Start()
    {
        ScoutingPost = GameObject.Find("Scouting Post");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetScoutingScore() {
        scoutingScore = ScoutingPost.GetComponent<Post>().tallyValue;
    }
}
