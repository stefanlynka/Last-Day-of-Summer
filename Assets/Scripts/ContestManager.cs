using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestManager : MonoBehaviour
{
    public GameObject Contest1;
    public GameObject Contest2;
    public bool growing = true;
    public Exploit exploit;
    public Encounter encounter;
    Contest easyContest;
    Contest hardContest;
    float natScale = 1.0f;

    // Start is called before the first frame update
    void Start(){
        GameObject CMSprite = Tools.GetChildNamed(gameObject, "Contest Post Title");
        CMSprite.GetComponent<MeshRenderer>().sortingOrder = 25;
        GetComponent<Panner>().SetTargetLocal(new Vector3(-2.25f, 2.9f, 0));

        encounter = exploit.encounters[Random.Range(0, exploit.encounters.Count)];
        easyContest = encounter.easyContest;
        hardContest = encounter.hardContest;
        Tools.GetChildNamed(gameObject,"Contest Post Title").GetComponent<TextMesh>().text = encounter.title;

        Contest1 = CreateContest(Contest1, new Vector3(-2.5f, -0.4f,0), easyContest);
        Contest2 = CreateContest(Contest2, new Vector3(2.5f, -0.4f, 0), hardContest);

        transform.localScale = new Vector2(0.01f, 0.01f);
    }

    // Update is called once per frame
    void Update(){
        UpdateScale();
        CheckForDestruction();
    }

    GameObject CreateContest(GameObject contestObj, Vector3 offset, Contest contest) {
        contestObj = new GameObject();
        contestObj.transform.parent = this.transform;
        contestObj.transform.localScale = new Vector2(1, 1);
        contestObj.transform.localPosition = offset;
        contestObj.name = "Contest";
        BoxCollider2D collider = contestObj.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(2.5f, 3f);
        collider.offset = new Vector2(0f, -0.5f);

        GameObject contestSprite = new GameObject();
        contestSprite.name = "Contest Sprite";
        contestSprite.transform.parent = contestObj.transform;
        contestSprite.transform.localPosition = new Vector2(0, 0.4f);
        contestSprite.transform.localScale = new Vector2(0.6f, 0.6f);
        SpriteRenderer sprite = contestSprite.AddComponent<SpriteRenderer>();
        sprite.sortingOrder = 30;



        GameObject contestText = new GameObject();
        contestText.name = "Contest Text";
        contestText.transform.parent = contestObj.transform;
        contestText.transform.localPosition = new Vector3(0, -0.9f, 0);
        TextMesh textMesh = contestText.AddComponent<TextMesh>();
        textMesh.characterSize = 0.1f;
        textMesh.fontSize = 25;
        textMesh.color = new Color(0.1f, 0.1f, 0.1f);
        textMesh.fontStyle = FontStyle.Bold;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        MeshRenderer render = textMesh.GetComponent<MeshRenderer>();
        render.sortingOrder = 32;

        contestObj.AddComponent<ClickableContest>();
        contestObj.GetComponent<ClickableContest>().SetupContest(contest);

        return contestObj;
    }

    void UpdateScale() {
        if (growing) {
            transform.localScale += new Vector3(0.025f, 0.025f, 0);
            if (transform.localScale.x > natScale) {
                growing = false;
                transform.localScale = new Vector3(natScale, natScale, 0);
            }
        }
    }
    public void RemoveOtherContest(GameObject chosenContest) {
        if (Contest1 == chosenContest) Destroy(Contest2);
        else Destroy(Contest1);
    }
    void CheckForDestruction() {
        if (StateController.State == 4) {
            Destroy(gameObject);
        }
    }
}
