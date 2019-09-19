using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestantManager : MonoBehaviour
{
    public Contest contest;
    public List<GameObject> Contestants;
    public List<GameObject> Slots;
    public GameObject MischiefPost;
    public GameObject BraveryPost;
    public GameObject CharmPost;
    public GameObject RelevantPost;
    public int diceTotal = 0;
    public GameObject TallyColumn;

    // Start is called before the first frame update
    void Start(){
        MischiefPost = GameObject.Find("Mischief Post");
        BraveryPost = GameObject.Find("Bravery Post");
        CharmPost = GameObject.Find("Charm Post");
        TallyColumn = Tools.GetChildNamed(gameObject, "Tally Column");

        GenerateTallyText();

        GetSlots();

    }

    // Update is called once per frame
    void Update(){
        
    }

    public void SetupContest(Contest c) {
        contest = c;

        if (c.type == "Mischief") RelevantPost = MischiefPost;
        else if (c.type == "Bravery") RelevantPost = BraveryPost;
        else if (c.type == "Charm") RelevantPost = CharmPost;

        for (int i = 0; i < RelevantPost.transform.childCount; i++) {
            GameObject box = RelevantPost.transform.GetChild(i).gameObject;
            if (box.tag == "Slot" && box.GetComponent<Slot>().occupied) {
                Contestants.Add(box.GetComponent<Slot>().Occupant);
            }
        }
        MoveContestants();
    }
    void GenerateTallyText() {
        GameObject tallyText = new GameObject();
        tallyText.name = "Tally Text";
        tallyText.transform.parent = TallyColumn.transform;
        tallyText.transform.localPosition = new Vector3(0, 0.5f, 0);
        TextMesh textMesh = tallyText.AddComponent<TextMesh>();
        textMesh.characterSize = 0.1f;
        textMesh.fontSize = 50;
        textMesh.color = new Color(0.1f, 0.1f, 0.1f);
        textMesh.fontStyle = FontStyle.Bold;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        MeshRenderer render = tallyText.GetComponent<MeshRenderer>();
        render.sortingOrder = 5;
    }
    public void GetSlots() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject slot = transform.GetChild(i).gameObject;
            if (slot.tag == "Slot") Slots.Add(slot);
        }
    }
    public void MoveContestants() {
        for (int i = 0; i < Contestants.Count; i++) {
            Contestants[i].GetComponent<Draggable>().TakeOutOfSlot();
            Contestants[i].GetComponent<Draggable>().PutInSlot(Slots[i]);
        }
    }
    public void MoveContestantsBack() {
        for (int i = 0; i < Contestants.Count; i++) {
            Contestants[i].GetComponent<Draggable>().TakeOutOfSlot();
            Contestants[i].GetComponent<Draggable>().PutInSlot(RelevantPost.GetComponent<Post>().Grid[i]);
        }
    }


    public void RollDice() {
        for(int i = 0; i < Slots.Count; i++) {
            GenerateDice(Slots[i]);
        }
        Tools.GetChildNamed(TallyColumn, "Tally Text").GetComponent<TextMesh>().text = diceTotal.ToString();

        bool victory = GetResults();
        if (victory) {
            GiveRewards();
        }
        else {
            GivePunishments();
        }
        StateController.State++;
        print("State = "+StateController.State);
    }


    public void GenerateDice(GameObject box) {
        if (box.GetComponent<Slot>().occupied) {
            GameObject resident = box.GetComponent<Slot>().Occupant;
            int diceCount = 0;
            if (contest.type == "Mischief") diceCount += resident.GetComponent<Character>().Mischief;
            else if (contest.type == "Bravery") diceCount += resident.GetComponent<Character>().Bravery;
            else if (contest.type == "Charm") diceCount += resident.GetComponent<Character>().Charm;

            for(int i = 0; i < diceCount; i++) {
                RollDie(box, new Vector3(0,0.65f+(i/3f),0));
            }
        }
    }
    void RollDie(GameObject box, Vector3 offset) {
        GameObject die = new GameObject();
        die.transform.parent = box.transform;
        die.transform.localPosition = offset;
        TextMesh dieText =  die.AddComponent<TextMesh>();
        dieText.characterSize = 0.1f;
        dieText.fontSize = 50;
        dieText.color = new Color(0.1f, 0.1f, 0.1f);
        dieText.fontStyle = FontStyle.Bold;
        dieText.anchor = TextAnchor.MiddleCenter;
        dieText.alignment = TextAlignment.Center;
        MeshRenderer render = die.GetComponent<MeshRenderer>();
        render.sortingOrder = 5;
        int roll = Random.Range(1, 7);
        dieText.text = roll.ToString();
        diceTotal += roll;
    }
    bool GetResults() {
        if (diceTotal >= contest.difficulty) {
            Tools.GetChildNamed(TallyColumn, "Tally Text").GetComponent<TextMesh>().text += "\nSuccess!";//= diceTotal.ToString()+;
            return true;
        }
        Tools.GetChildNamed(TallyColumn, "Tally Text").GetComponent<TextMesh>().text += "\nFailure!";
        return false;
    }

    void GiveRewards() {
        for(int i = 0; i <Contestants.Count; i++) {
            GameObject contestant = Contestants[i];
            if (contest.type == "Mischief" && contestant.GetComponent<Character>().Mischief<5) contestant.GetComponent<Character>().Mischief++;
            else if (contest.type == "Bravery" && contestant.GetComponent<Character>().Bravery < 5) contestant.GetComponent<Character>().Bravery++;
            else if (contest.type == "Charm" && contestant.GetComponent<Character>().Charm < 5) contestant.GetComponent<Character>().Charm++;
        }
        if (contest.reward == "food") Inventory.UpdateFood(contest.rewardCount);
        if (contest.reward == "money") Inventory.UpdateMoney(contest.rewardCount);
        if (contest.reward == "character") Population.AddCharacterToParty();
        if (contest.reward == "item") Inventory.AddItem(contest.itemName);
        //if (contest.type == "Mischief") Inventory.UpdateFood(diceTotal);
        //if (contest.type == "Bravery") Inventory.UpdateMoney(contest.difficulty);
        //if (contest.type == "Charm") Inventory.UpdateMoney(contest.difficulty);
    }

    void GivePunishments() {

    }
    public void Cleanup() {

        MoveContestantsBack();

        diceTotal = 0;
        Tools.GetChildNamed(TallyColumn, "Tally Text").GetComponent<TextMesh>().text = "";
        for (int i = 0; i < Slots.Count; i++) {
            GameObject box = Slots[i];
            for(int j = 0; j < box.transform.childCount; j++) {
                Destroy(box.transform.GetChild(j).gameObject);
            }
        }

        RemoveContestants();
    }

    void RemoveContestants() {
        Contestants = new List<GameObject>();
    }
}
