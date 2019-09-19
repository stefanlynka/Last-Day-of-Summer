using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{

    public GameObject[] Grid = new GameObject[12];
    public string attribute = "";
    int gridSize = 0;
    public int tallyValue = 0;
    GameObject Tally;
    GameObject Title;

    // Start is called before the first frame update
    void Start() {
        CreateGrid();
        CreateTally();
        CreateTitle();
    }

    // Update is called once per frame
    void Update() {
        UpdateTally();
    }

    void CreateGrid() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "Slot" || child.tag == "Item Slot") {
                child.AddComponent<Slot>();
                child.GetComponent<Slot>().size =  (int)transform.localScale.x;
                gridSize++;
            }
        }
        Grid = new GameObject[gridSize];
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "Slot" || child.tag == "Item Slot") {
                Grid[i] = child;
            }
        }
    }

    void CreateTitle() {       
        Title = Tools.MakeText(gameObject, attribute, new Vector3(0, 0, 0), 75, 10);
        Title.name = attribute + " Post Title";
        TextMesh titleMesh = Title.GetComponent<TextMesh>();

        float offset = 0f;

        if (attribute == "Mischief") {
            titleMesh.text = "Scavenging";
            titleMesh.color = new Color(0.3f, 1f, 0.3f);
            offset = 0.6f;
        }
        else if (attribute == "Bravery") {
            titleMesh.text = "Protecting";
            titleMesh.color = new Color(0, 0, 0.8f);
            offset = 0.6f;
        }
        else if (attribute == "Charm") {
            titleMesh.text = "Leading";
            titleMesh.color = new Color(0.8f, 0, 0);
            offset = 0.6f;
        }
        else if (attribute == "Scouting Options") {
        }
        else if (attribute == "Snack Time") titleMesh.fontSize = 55; 

        GameObject PostSprite = Tools.GetChildNamed(gameObject,"Post Sprite");
        float SpriteScaleY = PostSprite.transform.localScale.y;
        Title.transform.localPosition = new Vector3(0, SpriteScaleY * (4.75f + offset), 0);
    }

    void CreateTally() {
        if (attribute == "Mischief" || attribute == "Bravery" || attribute == "Charm") {
            Tally = Tools.MakeText(gameObject, attribute, new Vector3(0, 0, 0),40,10);
            TextMesh tallyMesh = Tally.GetComponent<TextMesh>();

            if (attribute == "Mischief") tallyMesh.color = new Color(0, 1, 0);
            else if (attribute == "Bravery") tallyMesh.color = new Color(0, 0, 1);
            else if (attribute == "Charm") tallyMesh.color = new Color(1, 0, 0);

            GameObject PostSprite = Tools.GetChildNamed(gameObject, "Post Sprite");
            float SpriteScaleY = PostSprite.transform.localScale.y;
            Tally.transform.localPosition = new Vector3(0, SpriteScaleY * 4f, 0);
        }
    }

    void UpdateTally() {
        tallyValue = 0;
        for (int i = 0; i < Grid.Length; i++) {
            GameObject slot = Grid[i];
            GameObject player = slot.GetComponent<Slot>().Occupant;
            if (player != null) {
                switch (attribute) {
                    case "Mischief":
                        tallyValue += player.GetComponent<Character>().Mischief;
                        break;
                    case "Bravery":
                        tallyValue += player.GetComponent<Character>().Bravery;
                        break;
                    case "Charm":
                        tallyValue += player.GetComponent<Character>().Charm;
                        break;
                    case "Scouting":
                        tallyValue += 1;
                        break;
                    case "Resting":
                        tallyValue += 1;
                        break;
                }
            }
        }
        if (attribute == "Mischief" || attribute == "Bravery" || attribute == "Charm") {
            Tally.GetComponent<TextMesh>().text = attribute + " " + tallyValue;
        }
    }
}
