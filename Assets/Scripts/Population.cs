using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    GameObject[] characterList = new GameObject[21];
    List<string> nameList = new List<string>();
    int nameListLength = 0;
    int startingPartySize = 6;
    public static List<GameObject> Party = new List<GameObject>();
    public static List<GameObject> Standby = new List<GameObject>();

    void Awake() {
        SetupNames();
        PopulateTown();
    }
    // Start is called before the first frame update
    void Start(){
        SetupParty(startingPartySize);
    }

    // Update is called once per frame
    void Update(){
        
    }

    void PopulateTown() {
        for (int i = 0; i < 20; i++) {
            
            GameObject newCharacter = new GameObject();
            newCharacter.AddComponent<Character>();
            bool female = newCharacter.GetComponent<Character>().female;
            string cName = TakeName(female);
            newCharacter.GetComponent<Character>().SetCharacteristics(cName, "normal", "stubborn", i);

            characterList[i] = newCharacter;
            newCharacter.transform.parent = this.transform;
            newCharacter.name = cName;
            Standby.Add(newCharacter);
            newCharacter.SetActive(false);
        }
    }

    void SetupParty(int startingPartySize) {
        for (int i = 0; i < startingPartySize; i++) {
            AddCharacterToParty();
        }
    }

    public static void AddCharacterToParty() {
        if (Standby.Count > 0) {
            GameObject character = Standby[0];
            character.SetActive(true);
            Party.Add(character);
            character.GetComponent<Character>().StartInResting();
            Standby.RemoveAt(0);
        }
    }

    void SetupNames() {
        AddName("Ponyboy");
        AddName("Pip");
        AddName("Artemis");
        AddName("Klaus");
        AddName("Finn");
        AddName("Brian");
        AddName("Oliver");
        AddName("Holden");
        AddName("The Kid");
        AddName("Danny");

        AddName("Ed");
        AddName("Violet");
        AddName("Sunny");
        AddName("Wendy");
        AddName("Satsuki");
        AddName("Dorothy");
        AddName("Hermione");
        AddName("Haku");
        AddName("Cosette");
        AddName("Charlotte");
    }
    void AddName(string newName) {
        nameList.Add(newName);
        nameListLength++;
    }
    string TakeName(bool female) {
        int randInt = Random.Range(0, nameListLength/2);
        if (female) randInt += nameListLength / 2;
        string cName = nameList[randInt];
        nameList.RemoveAt(randInt);
        nameListLength--;
        return cName;
    }
    public static void RemoveCharacter(GameObject Character) {
        Party.Remove(Character);
    }
}
