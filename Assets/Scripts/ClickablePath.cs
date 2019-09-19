using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickablePath : MonoBehaviour
{
    public Exploit exploit;
    GameObject StateControl;
    GameObject ContestManager;
    GameObject ContestManagerPrefab;
    public GameObject Node;

    // Start is called before the first frame update
    void Start() {
        StateControl = GameObject.Find("State Controller");
        //ContestManager = GameObject.Find("Contest Manager");
        ContestManagerPrefab = Resources.Load<GameObject>("Prefabs/Contest Manager");
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetupExploit(Exploit e, string direction) {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = Resources.Load<Sprite>("Sprites/sprite");
        GameObject text = transform.GetChild(0).gameObject;
        TextMesh tMesh = text.GetComponent<TextMesh>();
        tMesh.text = e.title + "\n"+direction+" Path";
        tMesh.alignment = TextAlignment.Center;
        exploit = e;
    }

    public void OnMouseDown() {
        StateController.GoToNextState();
        ContestManager = Instantiate(ContestManagerPrefab, transform.position+new Vector3(0,0,-5f), Quaternion.identity);
        ContestManager.GetComponent<ContestManager>().exploit = exploit;
        transform.parent.GetComponent<Scouting>().DestroyAllPaths();
        StateController.UpdateCurrentNode(Node);
    }

}

