using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    string characterName = "";
    int id;
    public int Bravery = 1;
    public int Mischief = 1;
    public int Charm = 1;
    int Luck = 0;
    int[] stats = new int[4];
    string archeype = "";
    string trait = "";

    float scale = 0.15f;

    GameObject Statholder;
    GameObject NameTag;

    string pantType;
    string shirtType;
    string hairTint;
    string genderString;
    string eyeType;
    int genderNum;
    public bool female;
    int hairType;
    Dictionary<int, string> pantsDict = new Dictionary<int, string>();
    Dictionary<int, string> shirtDict = new Dictionary<int, string>();
    Dictionary<int, string> hairDict = new Dictionary<int, string>();
    Dictionary<int, string> eyeDict = new Dictionary<int, string>();
    List<string> pantsTypes = new List<string>(new string[] { "Blue1", "Blue2", "Brown", "Green", "Grey", "LightBlue", "Navy", "Pine", "Red", "Tan", "White", "Yellow" });
    List<string> shirtTypes = new List<string>(new string[] { "blue", "green", "grey", "navy", "pine", "red", "white", "yellow" });
    List<string> hairColour = new List<string>(new string[] { "black", "blonde", "brown1", "brown2","red", "tan"});
    List<string> eyeColours = new List<string>(new string[] { "Black", "Blue", "Brown", "Green", "Pine" });
    //Relation[] relations = new Relation[21];


    // Start is called before the first frame update
    void Awake()
    {
        pantsDict = PrepareDictionary(pantsDict, pantsTypes);
        shirtDict = PrepareDictionary(shirtDict, shirtTypes);
        hairDict = PrepareDictionary(hairDict, hairColour);
        eyeDict = PrepareDictionary(eyeDict, eyeColours);

        GiveForm();
        IncreaseStat("random", 1);

        CreateComponents();

        transform.position = new Vector2(-7 + id * 3, 0);
        transform.localScale *= scale;

        //gameObject.SetActive(false);
        //StartInResting();
    }

    // Update is called once per frame
    void Update(){
        UpdateStats();
    }

    void UpdateStats() {
        for (int i = 0; i < Statholder.transform.childCount; i++) {
            GameObject child = Statholder.transform.GetChild(i).gameObject;
            switch (child.name) {
                case "Bravery text":
                    child.GetComponent<TextMesh>().text = Bravery.ToString();
                    break;
                case "Mischief text":
                    child.GetComponent<TextMesh>().text = Mischief.ToString();
                    break;
                case "Charm text":
                    child.GetComponent<TextMesh>().text = Charm.ToString();
                    break;
            }
        }
    }


    void CreateComponents() {
        gameObject.AddComponent<Draggable>();
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(3, 4);
        collider.offset = new Vector2(0.5f, 0.5f);
        collider.isTrigger = true;
        gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Rigidbody2D>().freezeRotation = true;

        MakeStatHolder();
        MakeNameTag();
    }

    public void StartInResting() {
        GameObject rest = GameObject.Find("Rest Post");
        for(int i = 0; i< rest.transform.childCount; i++) {
            GameObject child = rest.transform.GetChild(i).gameObject;
            if (!child.GetComponent<Slot>().occupied) {
                GetComponent<Draggable>().PutInSlot(child);
                break;
            }
        }
    }

    void MakeStatHolder() {
        Statholder = MakeBodyPart("Sprites/statHolder", gameObject, 5);
        Statholder.transform.localPosition = new Vector2(3, 0f);
        Statholder.transform.localScale = new Vector2(3f, 8f);
        Statholder.name = "Statholder";

        MakeStatText(Statholder, "Mischief");
        MakeStatText(Statholder, "Bravery");
        MakeStatText(Statholder, "Charm");
    }

    void MakeNameTag() {
        NameTag = MakeBodyPart("Sprites/statHolder", gameObject, 5);
        NameTag.transform.localPosition = new Vector2(0f, -2.7f);
        NameTag.transform.localScale = new Vector2(1.5f, 6f);
        NameTag.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        NameTag.name = "NameTag";

        GameObject NameTagText = new GameObject();
        NameTagText.transform.parent = NameTag.transform;
        NameTagText.transform.position = NameTag.transform.position;
        NameTagText.name = "NameTagText";
        MeshRenderer renderer = NameTagText.AddComponent<MeshRenderer>();
        TextMesh textMesh = NameTagText.AddComponent<TextMesh>();
        renderer.sortingOrder = 10;
        textMesh.characterSize = 0.05f;
        textMesh.fontSize = 200;
        textMesh.text = characterName;
        textMesh.fontStyle = FontStyle.Bold;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.color = new Color(0, 0, 0);
    }

    void MakeStatText(GameObject parent, string stat) {
        GameObject StatHolderText = new GameObject();
        StatHolderText.transform.parent = parent.transform;
        StatHolderText.name = stat+" text";
        
        StatHolderText.AddComponent<MeshRenderer>();
        Renderer rend = StatHolderText.GetComponent<MeshRenderer>();
        rend.sortingOrder = 10;

        TextMesh statText = StatHolderText.AddComponent<TextMesh>();
        statText.characterSize = 0.05f;
        statText.fontSize = 300;
        statText.text = "0";
        statText.fontStyle = FontStyle.Bold;
        statText.anchor = TextAnchor.MiddleCenter;

        Vector3 new_pos = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);
        if (stat == "Mischief") {
            statText.color = new Color(0, 1, 0);
            new_pos.y += 1.5f;
        }
        if (stat == "Bravery") {
            statText.color = new Color(0, 0, 1);
        }
        if (stat == "Charm") {
            statText.color = new Color(1, 0, 0);
            new_pos.y -= 1.5f;
        }
        StatHolderText.transform.position = new_pos;
    }

    void GiveForm() {
        int skinTint = Random.Range(1, 9);
        int shoeTint = Random.Range(1, 8);
        pantType = pantsDict[Random.Range(0,12)];
        //print("pantType:" + pantType);
        shirtType = shirtDict[Random.Range(0, 8)];
        //print("shirtType:" + shirtType);
        genderNum = Random.Range(1, 3);
        female = false;
        if (genderNum == 2) female = true;
        genderString = "Man";
        if (genderNum == 2) genderString = "Woman";
        //print("Gender: " + genderString);
        hairType = Random.Range(1, 7);
        hairTint = hairDict[Random.Range(0, 6)];
        //print("hairTint: " + hairTint);
        eyeType = eyeDict[Random.Range(0, 5)];

        GiveHead(skinTint, hairTint, hairType, genderString, eyeType);
        GiveBody(skinTint, pantType, shirtType, genderNum);
        GiveLeftArm(skinTint, shirtType);
        GiveRightArm(skinTint, shirtType);
        GiveLeftLeg(skinTint, shoeTint, pantType);
        GiveRightLeg(skinTint, shoeTint, pantType);
    }

    void GiveHead(int skinTint, string hairTint, int hairType, string genderString, string eyeType) {
        //GameObject face = MakeBodyPart("Sprites/Character/Complete Faces/face1");//y 2.9
        //face.transform.position = new Vector3(0, 2.7f, -2f);
        //transform.localPosition = new Vector2(-30+id*10, 0);
        GameObject Head = new GameObject();
        Head.transform.position = new Vector3(0, 1, -2f);
        Head.transform.parent = this.transform;
        Head.name = "Head";

        GameObject hair = MakeBodyPart("Sprites/Character/Hair/"+hairTint+"/"+ hairTint + genderString + hairType, Head, 4);
        float yOffset = 0;
        float xOffset = 0;
        if (genderString == "Woman") {
            if (hairType == 1) yOffset = -1.2f;
            if (hairType == 2) yOffset = -0.85f; 
            if (hairType == 3) yOffset = -1.2f;
            if (hairType == 4) yOffset = -0.8f;
            if (hairType == 5) yOffset = -0.7f;
            if (hairType == 6) yOffset = -0.76f;
        }
        else {
            if (hairType == 5) xOffset = 0.05f;
        }
        hair.transform.localPosition = new Vector3(0f+xOffset, 1.5f+yOffset, -0);
        hair.transform.localScale = new Vector2(1.4f, 1.34f);

        string headPath = "Sprites/Character/Skin/Tint " + skinTint + "/head";
        GameObject head = MakeBodyPart(headPath, Head, 3);
        head.transform.localPosition = new Vector3(0, 1, 1);
        head.transform.localScale = new Vector2(1.4f, 1.34f);

        GameObject eyeL = MakeBodyPart("Sprites/Character/Face/Eyes/eye"+ eyeType+"_large", Head, 4);
        eyeL.transform.localPosition = new Vector3(-0.33f, 0.58f, -1);
        eyeL.transform.localScale = new Vector2(1.5f, 1.5f);
        GameObject eyeR = MakeBodyPart("Sprites/Character/Face/Eyes/eye" + eyeType + "_large", Head, 4);
        eyeR.transform.localPosition = new Vector3(0.33f, 0.58f, -1);
        eyeR.transform.localScale = new Vector2(1.5f, 1.5f);
        /*
        GameObject eyeWhiteL = MakeBodyPart("Sprites/Character/Face/Eyes/eyeWhite", Head);
        eyeWhiteL.transform.localPosition = new Vector3(-0.33f, 0.58f, 0);
        eyeWhiteL.transform.localScale = new Vector2(1.8f, 1.75f);
        GameObject eyeWhiteR = MakeBodyPart("Sprites/Character/Face/Eyes/eyeWhite", Head);
        eyeWhiteR.transform.localPosition = new Vector3(0.33f, 0.58f, 0);
        eyeWhiteR.transform.localScale = new Vector2(1.8f, 1.75f);
        */
        GameObject eyeBrowL = MakeBodyPart("Sprites/Character/Face/Eyebrows/blackBrow2", Head, 4);
        eyeBrowL.transform.localPosition = new Vector3(-0.41f, 0.85f, 0);
        eyeBrowL.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 12));
        eyeBrowL.transform.localScale = new Vector2(0.8f, 0.8f);
        GameObject eyeBrowR = MakeBodyPart("Sprites/Character/Face/Eyebrows/blackBrow2", Head, 4);
        eyeBrowR.transform.localPosition = new Vector3(0.41f, 0.85f, 0);
        eyeBrowR.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 12));
        eyeBrowR.transform.localScale = new Vector2(0.8f, 0.8f);

        GameObject nose = MakeBodyPart("Sprites/Character/Face/Nose/Tint 1/tint1Nose3", Head, 4);
        nose.transform.localPosition = new Vector3(0, 0.3f, 0);
        nose.transform.localScale = new Vector2(1f, 1f);
        GameObject mouth = MakeBodyPart("Sprites/Character/Face/Mouth/mouth"+Random.Range(1,5), Head, 4);
        mouth.transform.localPosition = new Vector3(0, 0.1f, 0);
        mouth.transform.localScale = new Vector2(0.84f, 0.8f);

    }

    void GiveBody(int skinTint, string pantType, string shirtType, int gender) {
        GameObject Body= new GameObject();
        Body.transform.position = new Vector3(0, -1, 1f);
        Body.transform.parent = this.transform;
        Body.name = "Body";

        GameObject neck = MakeBodyPart("Sprites/Character/Skin/Tint "+skinTint+"/tint"+skinTint+"_neck", Body, 1);
        neck.transform.localPosition = new Vector3(0, 2f, 1f);
        GameObject shirt = MakeBodyPart("Sprites/Character/Shirts/"+ shirtType +"/"+shirtType+"Shirt"+Random.Range(1,7), Body, 2);
        shirt.transform.localPosition = new Vector3(0, 1.25f, -1f);
        shirt.transform.localScale = new Vector2(0.9f, 1);
        GameObject pants = MakeBodyPart("Sprites/Character/Pants/"+ pantType +"/pants"+pantType+""+ Random.Range(1, 5), Body, 1);
        pants.transform.localPosition = new Vector3(0, 0.34f, -2f);
        pants.transform.localScale = new Vector3(0.87f, 1, 1);

    }
    void GiveLeftArm(int skinTint, string shirtType) {
        GameObject ArmLeft= new GameObject();
        ArmLeft.transform.position = new Vector3(0, -1, 0);
        ArmLeft.transform.parent = this.transform;
        ArmLeft.name = "ArmLeft";

        GameObject shirtArmL = MakeBodyPart("Sprites/Character/Shirts/"+shirtType+"/"+shirtType+"Arm_long", ArmLeft, 2); 
        shirtArmL.transform.localPosition = new Vector2(-0.81f, 1.25f);
        shirtArmL.transform.rotation = Quaternion.Euler(new Vector3(0, 180, -30));
        shirtArmL.transform.localScale = new Vector2(0.88f, 0.85f);
        GameObject armL = MakeBodyPart("Sprites/Character/Skin/Tint " + skinTint + "/tint" + skinTint + "_arm", ArmLeft, 1);
        armL.transform.localPosition = new Vector3(-0.81f, 1.25f, 1f);
        armL.transform.rotation = Quaternion.Euler(new Vector3(0, 180, -30));
        armL.transform.localScale = new Vector2(0.88f, 0.85f);
        GameObject handL = MakeBodyPart("Sprites/Character/Skin/Tint " + skinTint + "/tint" + skinTint + "_hand", ArmLeft, 1); 
        handL.transform.localPosition = new Vector3(-1.1f, 0.6f, 1f);
        handL.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        handL.transform.localScale = new Vector2(0.75f, 0.8f);
    }
    void GiveRightArm(int skinTint, string shirtType) {
        GameObject ArmRight = new GameObject();
        ArmRight.transform.position = new Vector3(0, -1, 0);
        ArmRight.transform.parent = this.transform;
        ArmRight.name = "ArmRight";

        GameObject shirtArmR = MakeBodyPart("Sprites/Character/Shirts/" + shirtType + "/" + shirtType + "Arm_long", ArmRight, 2);
        shirtArmR.transform.localPosition = new Vector2(0.81f, 1.25f);
        shirtArmR.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -30));
        shirtArmR.transform.localScale = new Vector2(0.88f, 0.85f);
        GameObject handR = MakeBodyPart("Sprites/Character/Skin/Tint " + skinTint + "/tint" + skinTint + "_hand", ArmRight, 1);
        handR.transform.localPosition = new Vector3(1.1f, 0.6f, 1f);
        handR.transform.localScale = new Vector2(0.75f, 0.8f);
        GameObject armR = MakeBodyPart("Sprites/Character/Skin/Tint " + skinTint + "/tint" + skinTint + "_arm", ArmRight, 1);
        armR.transform.localPosition = new Vector3(0.81f, 1.25f, 1f);
        armR.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -30));
        armR.transform.localScale = new Vector2(0.88f, 0.85f);
    }
    void GiveLeftLeg(int skinTint, int shoeTint, string pantType) {
        GameObject LeftLeg = new GameObject();
        LeftLeg.transform.localPosition = new Vector3(0, -1, 0);
        LeftLeg.transform.parent = this.transform;
        LeftLeg.name = "LeftLeg";

        GameObject shoeL = MakeBodyPart("Sprites/Character/Shoes/" + shoeTint + "/shoe", LeftLeg, 0);
        shoeL.transform.localPosition = new Vector3(-0.42f, -0.85f, 1f);
        shoeL.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        shoeL.transform.localScale = new Vector2(0.7f, 1);
        GameObject pantsLegL = MakeBodyPart("Sprites/Character/Pants/"+ pantType+"/pants"+ pantType+"_long", LeftLeg, 1);
        pantsLegL.transform.localPosition = new Vector3(-0.34f, -0.15f, 0);
        pantsLegL.transform.rotation = Quaternion.Euler(new Vector3(0, 180, -8.8f));
        pantsLegL.transform.localScale = new Vector3(0.9f, 0.8f, 1);
        GameObject legL = MakeBodyPart("Sprites/Character/Skin/Tint " + skinTint + "/tint" + skinTint + "_leg", LeftLeg, 0);
        legL.transform.localPosition = new Vector3(-0.34f, -0.15f, 1);
        legL.transform.rotation = Quaternion.Euler(new Vector3(0, 180, -8.8f));
        legL.transform.localScale = new Vector3(0.9f, 0.8f, 1);

    }
    void GiveRightLeg(int skinTint, int shoeTint, string pantType) {
        GameObject RightLeg = new GameObject();
        RightLeg.transform.position = new Vector3(0, -1, 0);
        RightLeg.transform.parent = this.transform;
        RightLeg.name = "RightLeg";

        GameObject shoeR = MakeBodyPart("Sprites/Character/Shoes/"+shoeTint+"/shoe", RightLeg, 0);
        shoeR.transform.localPosition = new Vector3(0.42f, -0.85f, 1f);
        shoeR.transform.localScale = new Vector2(0.7f, 1);
        GameObject pantsLegR = MakeBodyPart("Sprites/Character/Pants/" + pantType + "/pants" + pantType + "_long", RightLeg, 1);
        pantsLegR.transform.localPosition = new Vector3(0.34f,-0.15f,0);
        pantsLegR.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -8.8f));
        pantsLegR.transform.localScale = new Vector3(0.9f, 0.8f, 1);
        GameObject legR = MakeBodyPart("Sprites/Character/Skin/Tint " + skinTint + "/tint" + skinTint + "_leg", RightLeg, 0);
        legR.transform.localPosition = new Vector3(0.34f, -0.15f, 1);
        legR.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -8.8f));
        legR.transform.localScale = new Vector3(0.9f, 0.8f, 1);
    }

    GameObject MakeBodyPart(string path, GameObject parent, int layer) {
        Sprite newSprite = Resources.Load<Sprite>(path);
        GameObject bodyPart = new GameObject();
        SpriteRenderer renderer = bodyPart.AddComponent<SpriteRenderer>();
        renderer.sprite = newSprite;
        bodyPart.transform.parent = parent.transform;
        renderer.sortingOrder = layer;
        return bodyPart;
    }

    public void SetCharacteristics(string name, string archetype, string trait, int id) {
        characterName = name;
        this.archeype = archetype;
        this.trait = trait;
        this.id = id;
        UpdateName(characterName);
    }

    void UpdateName(string newName) {
        GameObject nameTag = Tools.GetChildNamed(gameObject, "NameTag");
        Tools.GetChildNamed(nameTag, "NameTagText").GetComponent<TextMesh>().text = newName;
    }

    void IncreaseStat(string stat, int amount) {
        switch (stat) {
            case "Bravery":
                Bravery++;
                break;
            case "Mischief":
                Mischief++;
                break;
            case "Charm":
                Charm++;
                break;
            case "Luck":
                Luck++;
                break;
            case "random":
                int randInt = Random.Range(0, 3);
                if (randInt == 0) Bravery++;
                if (randInt == 1) Mischief++;
                if (randInt == 2) Charm++;
                break;
        }
    }
    Dictionary<int, string> PrepareDictionary(Dictionary<int, string> dict, List<string> list) {
        for (int i = 0; i < list.Count; i++) {
            dict.Add(i, list[i]);
        }
        return dict;
    }
}
/*
class Relation {
    int targetId = 0;
    int status = 0;
}
*/