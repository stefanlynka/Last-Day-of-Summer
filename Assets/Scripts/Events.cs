using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{

    public static Dictionary<string, Exploit> ExploitDict = new Dictionary<string, Exploit>();
    public static Dictionary<string, Encounter> EncounterDict = new Dictionary<string, Encounter>();

    private void Awake() {
        GenerateEncounters();
        GenerateExploits();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

    void GenerateEncounters() {
        EncounterDict.Add("Bullies", Bullies());
        EncounterDict.Add("Railroad", Railroad());
        EncounterDict.Add("YardSale", YardSale());
        EncounterDict.Add("Flora", Flora());
        EncounterDict.Add("Fauna", Fauna());

        EncounterDict.Add("Aisles", Aisles());
        EncounterDict.Add("Merchant", Merchant());
        EncounterDict.Add("OlderKids", OlderKids());

        EncounterDict.Add("Adult", Adult());
        EncounterDict.Add("GuardDog", GuardDog());
        EncounterDict.Add("PetCat", PetCat());
        EncounterDict.Add("Pantry",Pantry());
        EncounterDict.Add("Recruit", Recruit());
        EncounterDict.Add("Parent", Parent());

        EncounterDict.Add("PetDog", PetDog());
        EncounterDict.Add("Barbecue",Barbecue());
    }

    void GenerateExploits() {
        ExploitDict.Add("Wander", Wander());
        ExploitDict.Add("Park", Park());
        ExploitDict.Add("IntoStore", IntoStore());
        ExploitDict.Add("ThroughFrontyard", ThroughFrontyard());
        ExploitDict.Add("ThroughHouse", ThroughHouse());
        ExploitDict.Add("ThroughBackyard", ThroughBackyard());
    }

    Exploit Wander() {
        Exploit e = new Exploit("Wander");
        e.encounters.Add(EncounterDict["Bullies"]);
        e.encounters.Add(EncounterDict["Railroad"]);
        e.encounters.Add(EncounterDict["YardSale"]);
        return e;
    }

    Exploit Park() {
        Exploit e = new Exploit("Park");
        e.encounters.Add(EncounterDict["Flora"]);
        e.encounters.Add(EncounterDict["Fauna"]);
        return e;
    }

    Exploit IntoStore() {
        Exploit e = new Exploit("Into Store");
        e.encounters.Add(EncounterDict["Aisles"]);
        e.encounters.Add(EncounterDict["Merchant"]);
        e.encounters.Add(EncounterDict["OlderKids"]);
        return e;
    }

    Exploit ThroughFrontyard() {
        Exploit e = new Exploit("Through Frontyard");
        e.encounters.Add(EncounterDict["Adult"]);  
        e.encounters.Add(EncounterDict["GuardDog"]); 
        return e;
    }

    Exploit ThroughHouse() {
        Exploit e = new Exploit("Through House");
        e.encounters.Add(EncounterDict["PetCat"]);
        e.encounters.Add(EncounterDict["Pantry"]);
        e.encounters.Add(EncounterDict["Recruit"]);
        e.encounters.Add(EncounterDict["Parent"]);
        return e;
    }
    Exploit ThroughBackyard() {
        Exploit e = new Exploit("Through Backyard");
        e.encounters.Add(EncounterDict["Barbecue"]);
        e.encounters.Add(EncounterDict["PetDog"]);
        return e;
    }


    Encounter Pantry() {
        Encounter E = new Encounter();
        E.title = "Pantry";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Grab Snacks";
        easyC.type = "Mischief";
        easyC.difficulty = 6;
        easyC.punishment = "";
        easyC.reward = "food";
        easyC.rewardCount = 10;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Loot The Whole House";
        hardC.type = "Mischief";
        hardC.difficulty = 12;
        hardC.punishment = "";
        hardC.reward = "food";
        hardC.rewardCount = 20;
        
        return E;
    }
    Encounter Barbecue() {
        Encounter E = new Encounter();
        E.title = "Barbecue";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Take Leftovers";
        easyC.type = "Mischief";
        easyC.difficulty = 8;
        easyC.punishment = "";
        easyC.reward = "food";
        easyC.rewardCount = 14;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Ask For Burgers";
        hardC.type = "Charm";
        hardC.difficulty = 14;
        hardC.punishment = "";
        hardC.reward = "food";
        hardC.rewardCount = 22;

        return E;
    }
    Encounter Aisles() {
        Encounter E = new Encounter();
        E.title = "Store";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Collect Free Samples";
        easyC.type = "Mischief";
        easyC.difficulty = 3;
        easyC.punishment = "";
        easyC.reward = "food";
        easyC.rewardCount = 8;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Five Finger Discount";
        hardC.type = "Mischief";
        hardC.difficulty = 18;
        hardC.punishment = "";
        hardC.reward = "food";
        hardC.rewardCount = 24;

        return E;
    }
    Encounter Bullies() {
        Encounter E = new Encounter();
        E.title = "Bullies";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Fight 'Em";
        easyC.type = "Bravery";
        easyC.difficulty = 12;
        easyC.punishment = "";
        easyC.reward = "money";
        easyC.rewardCount = 10;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Convince Them To Join";
        hardC.type = "Charm";
        hardC.difficulty = 22;
        hardC.punishment = "";
        hardC.reward = "character";
        hardC.rewardCount = 1;

        return E;
    }
    Encounter GuardDog() {
        Encounter E = new Encounter();
        E.title = "Guard Dog";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Run Away";
        easyC.type = "Bravery";
        easyC.difficulty = 9;
        easyC.punishment = "";
        easyC.reward = "money";
        easyC.rewardCount = 4;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Fight The Foe";
        hardC.type = "Bravery";
        hardC.difficulty = 18;
        hardC.punishment = "";
        hardC.reward = "item";
        hardC.rewardCount = 1;

        return E;
    }
    Encounter PetCat() {
        Encounter E = new Encounter();
        E.title = "Pet Cat";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Fight The Foe";
        easyC.type = "Bravery";
        easyC.difficulty = 6;
        easyC.punishment = "";
        easyC.reward = "money";
        easyC.rewardCount = 6;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Tame The Beast";
        hardC.type = "Charm";
        hardC.difficulty = 20;
        hardC.punishment = "";
        hardC.reward = "cat";
        hardC.rewardCount = 1;

        return E;
    }
    Encounter PetDog() {
        Encounter E = new Encounter();
        E.title = "Pet Dog";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "See What They're Digging Up";
        easyC.type = "Bravery";
        easyC.difficulty = 3;
        easyC.punishment = "";
        easyC.reward = "item";
        easyC.rewardCount = 1;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Bring Them With You";
        hardC.type = "Charm";
        hardC.difficulty = 12;
        hardC.punishment = "";
        hardC.reward = "dog";
        hardC.rewardCount = 1;

        return E;
    }
    Encounter Adult() {
        Encounter E = new Encounter();
        E.title = "Caught By Adult";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Weasel Out Of Trouble";
        easyC.type = "Charm";
        easyC.difficulty = 6;
        easyC.punishment = "";
        easyC.reward = "money";
        easyC.rewardCount = 6;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "...And Ask For Gifts";
        hardC.type = "Charm";
        hardC.difficulty = 12;
        hardC.punishment = "";
        hardC.reward = "item";
        hardC.rewardCount = 1;

        return E;
    }
    Encounter Parent() {
        Encounter E = new Encounter();
        E.title = "Caught By Parent";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Weasel Out Of Trouble";
        easyC.type = "Charm";
        easyC.difficulty = 6;
        easyC.punishment = "";
        easyC.reward = "money";
        easyC.rewardCount = 8;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "...And Ask For Playdate";
        hardC.type = "Charm";
        hardC.difficulty = 16;
        hardC.punishment = "";
        hardC.reward = "character";
        hardC.rewardCount = 1;

        return E;
    }
    Encounter Merchant() {
        Encounter E = new Encounter();
        E.title = "Caught By Merchant";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Weasel Out Of Trouble";
        easyC.type = "Charm";
        easyC.difficulty = 8;
        easyC.punishment = "";
        easyC.reward = "money";
        easyC.rewardCount = 4;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "...And Ask For Free Stuff";
        hardC.type = "Charm";
        hardC.difficulty = 16;
        hardC.punishment = "";
        hardC.reward = "item";
        hardC.rewardCount = 1;

        return E;
    }
    Encounter Recruit() {
        Encounter E = new Encounter();
        E.title = "Meet Potential Recruit";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Challenge To A Game";
        easyC.type = "Bravery";
        easyC.difficulty = 6;
        easyC.punishment = "";
        easyC.reward = "money";
        easyC.rewardCount = 6;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Convince Them To Join You";
        hardC.type = "Charm";
        hardC.difficulty = 12;
        hardC.punishment = "";
        hardC.reward = "character";
        hardC.rewardCount = 1;

        return E;
    }
    Encounter Railroad() {
        Encounter E = new Encounter();
        E.title = "Railroad";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Collect Pennies";
        easyC.type = "Mischief";
        easyC.difficulty = 2;
        easyC.punishment = "";
        easyC.reward = "money";
        easyC.rewardCount = 4;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Search For Loot";
        hardC.type = "Mischief";
        hardC.difficulty = 16;
        hardC.punishment = "";
        hardC.reward = "item";
        hardC.rewardCount = 1;

        return E;
    }
    Encounter YardSale() {
        Encounter E = new Encounter();
        E.title = "YardSale";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Borrow Something";
        easyC.type = "Mischief";
        easyC.difficulty = 8;
        easyC.punishment = "";
        easyC.reward = "item";
        easyC.rewardCount = 1;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Barter";
        hardC.type = "Charm";
        hardC.difficulty = 14;
        hardC.punishment = "";
        hardC.reward = "money";
        hardC.rewardCount = 10;

        return E;
    }
    Encounter Flora() {
        Encounter E = new Encounter();
        E.title = "Flora";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Pick Berries";
        easyC.type = "Mischief";
        easyC.difficulty = 6;
        easyC.punishment = "";
        easyC.reward = "food";
        easyC.rewardCount = 12;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Brave The Bees";
        hardC.type = "Bravery";
        hardC.difficulty = 14;
        hardC.punishment = "";
        hardC.reward = "food";
        hardC.rewardCount = 20;

        return E;
    }
    Encounter Fauna() {
        Encounter E = new Encounter();
        E.title = "Fauna";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Run Past Raccoon";
        easyC.type = "Bravery";
        easyC.difficulty = 6;
        easyC.punishment = "";
        easyC.reward = "food";
        easyC.rewardCount = 4;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Scare Away Raccoon";
        hardC.type = "Bravery";
        hardC.difficulty = 18;
        hardC.punishment = "";
        hardC.reward = "food";
        hardC.rewardCount = 14;

        return E;
    }
    Encounter OlderKids() {
        Encounter E = new Encounter();
        E.title = "Older Kids";
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "Ask To Borrow Cool Item";
        easyC.type = "Charm";
        easyC.difficulty = 10;
        easyC.punishment = "";
        easyC.reward = "item";
        easyC.rewardCount = 1;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "Steal It";
        hardC.type = "Mischief";
        hardC.difficulty = 18;
        hardC.punishment = "";
        hardC.reward = "item";
        hardC.rewardCount = 1;

        return E;
    }
    Encounter Template() {
        Encounter E = new Encounter();
        Contest easyC = new Contest();
        E.easyContest = easyC;
        easyC.title = "";
        easyC.type = "";
        easyC.difficulty = 0;
        easyC.punishment = "";
        easyC.reward = "";
        easyC.rewardCount = 1;
        Contest hardC = new Contest();
        E.hardContest = hardC;
        hardC.title = "";
        hardC.type = "";
        hardC.difficulty = 0;
        hardC.punishment = "";
        hardC.reward = "";
        hardC.rewardCount = 1;

        return E;
    }

}

public class Exploit {
    public string title;
    public List<Encounter> encounters = new List<Encounter>();

    public Exploit(string title) {
        this.title = title;
    }
}
public class Encounter {
    public string title;
    public Contest easyContest;
    public Contest hardContest;
    public string modifier;
}

public class Contest {
    public string title;
    public string type;
    public int difficulty;
    public string punishment;
    public string reward;
    public int rewardCount;
    public string itemName = "8ball";
}