using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scouting : MonoBehaviour
{

    public GameObject ScoutingPost;
    public GameObject EventSystem;
    public GameObject Path1;
    public GameObject Path2;
    public GameObject Path3;
    int scoutingPower;

    // Start is called before the first frame update
    void Start() {
        ScoutingPost = GameObject.Find("Scouting Post");

        EventSystem = GameObject.Find("Event Structure");

    }

    // Update is called once per frame
    void Update() {
        
    }

    GameObject SetupPath(GameObject path, Vector3 offset) {
        path = new GameObject();
        path.name = "path";
        path.transform.parent = this.transform;
        path.transform.position = this.transform.position + offset;
        BoxCollider2D collider = path.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(2.5f, 3.0f);
        collider.offset = new Vector2(0f, -0.25f);
        SpriteRenderer sprite = path.AddComponent<SpriteRenderer>();
        sprite.sortingOrder = 10;

        GameObject pathText = new GameObject();
        pathText.transform.parent = path.transform;
        pathText.transform.position = path.transform.position + new Vector3(0,-1.3f,0);
        TextMesh textMesh = pathText.AddComponent<TextMesh>();
        textMesh.characterSize = 0.1f;
        textMesh.fontSize = 25;
        textMesh.color = new Color(0.1f, 0.1f, 0.1f);
        textMesh.fontStyle = FontStyle.Bold;
        textMesh.anchor = TextAnchor.MiddleCenter;
        MeshRenderer render = textMesh.GetComponent<MeshRenderer>();
        render.sortingOrder = 12;
        return path;
    }

    public void LoadPaths() {

        Path1 = SetupPath(Path1, new Vector3(-3.8f, -0.2f, -1f));
        Path2 = SetupPath(Path2, new Vector3(0f, -0.2f, -1f));
        Path3 = SetupPath(Path3, new Vector3(3.8f, -0.2f, -1f));


        scoutingPower = ScoutingPost.GetComponent<Post>().tallyValue;
        GameObject nextNode = NextAvailableNode(StateController.CurrentNode, "left");
        GetExploitForPath(nextNode, Path1);
        nextNode = NextAvailableNode(StateController.CurrentNode, "right");
        if (scoutingPower >= 1) GetExploitForPath(nextNode, Path2);
        nextNode = NextAvailableNode(StateController.CurrentNode, "left");
        if (scoutingPower >= 2) GetExploitForPath(nextNode, Path3);
    }

    GameObject NextAvailableNode(GameObject currentNode, string priority) {
        GameObject nextNode;
        GameObject leftNode = currentNode.GetComponent<Node>().leftNeighbour;
        GameObject rightNode = currentNode.GetComponent<Node>().rightNeighbour;
        if ((leftNode) && (priority == "left") || (rightNode == null)) {
            nextNode = leftNode;
            nextNode.name = "Left";
        }
        else {
            nextNode = rightNode;
            nextNode.name = "Right";
        }
        return nextNode;
    }

    void GetExploitForPath(GameObject node, GameObject path) {
        List<Exploit> exploitList = new List<Exploit>();
        if (StateController.CurrentNode.GetComponent<Node>().forwardTile) exploitList =  StateController.CurrentNode.GetComponent<Node>().forwardTile.GetComponent<Tile>().exploits;
        if (node.name == "Left" && StateController.CurrentNode.GetComponent<Node>().leftTile) {
            List<Exploit> leftExploits = StateController.CurrentNode.GetComponent<Node>().leftTile.GetComponent<Tile>().exploits;
            for(int i = 0; i < leftExploits.Count; i++) {
                exploitList.Add(leftExploits[i]);
            }
        }
        if (node.name == "Right" && StateController.CurrentNode.GetComponent<Node>().rightTile) {
            List<Exploit> rightExploits = StateController.CurrentNode.GetComponent<Node>().rightTile.GetComponent<Tile>().exploits;
            for (int i = 0; i < rightExploits.Count; i++) {
                exploitList.Add(rightExploits[i]);
            }
        }
        Exploit exploit = exploitList[Random.Range(0, exploitList.Count)];

        path.AddComponent<ClickablePath>();
        path.GetComponent<ClickablePath>().SetupExploit(exploit, node.name);
        path.GetComponent<ClickablePath>().Node = node;
    }

    public void DestroyAllPaths() {
        Destroy(Path1);
        Destroy(Path2);
        Destroy(Path3);
    }
}
