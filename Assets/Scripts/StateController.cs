using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateController : MonoBehaviour
{
    public static int State = 0;
    public static GameObject CurrentNode;
    public bool viewingMap = false;
    static GameObject ActivePosts;
    static GameObject MischiefPost;
    static GameObject BraveryPost;
    static GameObject CharmPost;
    static GameObject RestPost;
    static GameObject ScoutPost;
    static GameObject ScoutingOptions;
    static GameObject Map;
    static GameObject ContestantManager;
    static GameObject ReadyButton;
    static GameObject FeedPost;
    // Start is called before the first frame update
    void Start(){
        ActivePosts = GameObject.Find("Active Posts");
        RestPost = GameObject.Find("Rest Post");
        ScoutPost = GameObject.Find("Scouting Post");
        ScoutingOptions = GameObject.Find("Scouting Options");
        Map = GameObject.Find("Map");
        UpdateCurrentNode(Map.GetComponent<Town>().Nodes[0][1]);
        ReadyButton = GameObject.Find("Ready Button");
        ContestantManager = GameObject.Find("Contestant Manager");
        MischiefPost = GameObject.Find("Mischief Post");
        BraveryPost = GameObject.Find("Bravery Post");
        CharmPost = GameObject.Find("Charm Post");
        FeedPost = GameObject.Find("Feeding Post");
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void NextState() {
        GoToNextState();
    }

    static void SetPanningTargetLocal(GameObject obj, Vector3 target) {
        obj.GetComponent<Panner>().SetTargetLocal(target);
    }

    public void GoToMap() {
        if (!viewingMap) {
            SetPanningTargetLocal(Map, new Vector3(0, -5.1f, 0));
            viewingMap = true;
        }
        else {
            SetPanningTargetLocal(Map, new Vector3(20, -5.1f, 0));
            viewingMap = false;
        }
    }

    public static void GoToNextState() {
        if (State == 0)GoToScoutingState();
        if (State == 3) GoToUpkeepState();//GoToAllocateState();
    }
    public static void GoToScoutingState() {
        if (State == 0) {
            State = 1;
            print("Enter State " + State);
            SetPanningTargetLocal(ActivePosts, new Vector3(0, -2.2f, 0));
            SetPanningTargetLocal(RestPost, new Vector3(0, -7.5f, 0));
            SetPanningTargetLocal(ScoutPost, new Vector3(ScoutPost.transform.position.x, 4.35f, 0));
            SetPanningTargetLocal(ScoutingOptions, new Vector3(ScoutingOptions.transform.position.x, 2.9f, 0));
            SetPanningTargetLocal(ReadyButton, new Vector3(533, -400, 0));

            ScoutingOptions.GetComponent<Scouting>().LoadPaths();
        }
    }
    public static void GoToContestState(string attribute) {
        if (State == 1) {
            State = 2;
            print("Enter State " + State);
            SetPanningTargetLocal(ActivePosts, new Vector3(ActivePosts.transform.position.x, -10, 0));
            SetPanningTargetLocal(RestPost, new Vector3(0, -13.5f, 0));
            SetPanningTargetLocal(ScoutPost, new Vector3(ScoutPost.transform.position.x, 0, 0));
            SetPanningTargetLocal(ContestantManager, new Vector3(-2.2f, -2, 0));
            SetPanningTargetLocal(ReadyButton, new Vector3(533, -300, 0));
        }
    }
    public static void GoToUpkeepState() {
        if (State == 3) {
            State = 4;
            ContestantManager.GetComponent<ContestantManager>().Cleanup();
            SetPanningTargetLocal(ActivePosts, new Vector3(0, 2.2f, 0));
            SetPanningTargetLocal(RestPost, new Vector3(0, -3f, 0));
            SetPanningTargetLocal(ScoutingOptions, new Vector3(-2.25f, 8, 0));
            SetPanningTargetLocal(ContestantManager, new Vector3(-17, -2.2f, 0));
            SetPanningTargetLocal(FeedPost, new Vector3(-6.75f, -3, 0));
            SetPanningTargetLocal(ReadyButton, new Vector3(533, -400, 0));
        }
    }
    public static void GoToAllocateState() {
        State = 0;
        SetPanningTargetLocal(ActivePosts, new Vector3(0, 2.2f, 0));
        SetPanningTargetLocal(RestPost, new Vector3(0, -3f, 0));
        SetPanningTargetLocal(ScoutingOptions, new Vector3(-2.25f, 8, 0));
        SetPanningTargetLocal(ContestantManager, new Vector3(-17, -2.2f, 0));
        SetPanningTargetLocal(FeedPost, new Vector3(-6.75f, -7, 0));
        SetPanningTargetLocal(ReadyButton, new Vector3(533, -300, 0));

        Node node = CurrentNode.GetComponent<Node>();
        if (node.forwardTile == null && node.leftTile == null && node.rightTile == null) {
            State = 6;
            if (Inventory.MoneyCount() >= Population.Party.Count*10) print("VICTORY");
        }
    }

    public static void UpdateCurrentNode(GameObject newNode) {
        CurrentNode = newNode;
        Tools.GetChildNamed(CurrentNode, "Node Sprite").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/flagGreen");
    }
}
