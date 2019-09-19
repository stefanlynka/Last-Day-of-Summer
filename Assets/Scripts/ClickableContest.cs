using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableContest : MonoBehaviour {
    GameObject ContestManager;
    public GameObject ContestantManager;
    Contest contest;
    // Start is called before the first frame update
    void Start() {
        ContestantManager = GameObject.Find("Contestant Manager");
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetupContest(Contest c) {
        contest = c;
        SpriteRenderer sprite = Tools.GetChildNamed(gameObject,"Contest Sprite").GetComponent<SpriteRenderer>();
        sprite.sprite = Resources.Load<Sprite>("Sprites/sprite");
        GameObject text = Tools.GetChildNamed(gameObject, "Contest Text");
        TextMesh tMesh = text.GetComponent<TextMesh>();
        tMesh.text = c.title+"\n"+c.type+"\n"+"Difficulty: "+c.difficulty;
        MeshRenderer rend = text.GetComponent<MeshRenderer>();
        rend.sortingOrder = 30;
    }

    private void OnMouseDown() {
        StateController.GoToContestState(contest.type);
        transform.parent.GetComponent<ContestManager>().RemoveOtherContest(this.gameObject);
        ContestantManager.GetComponent<ContestantManager>().SetupContest(contest);
    }
}
