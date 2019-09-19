using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    static int money = 0;
    static int food = 60;
    List<Item> Items;
    static GameObject moneyText;
    static GameObject foodText;
    static List<GameObject> itemSlots = new List<GameObject>();

    // Start is called before the first frame update
    void Start(){
        CreateTexts();
        FindItemSlots();
    }

    // Update is called once per frame
    void Update(){
        
    }

    void FindItemSlots() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "Item Slot") {
                itemSlots.Add(child);
            }
        }
    }


    void CreateTexts() {
        moneyText = Tools.MakeText(gameObject, "Money: "+money.ToString(), new Vector3(-0.7f, 0.25f, 0), 45, 5);
        moneyText.GetComponent<TextMesh>().color = Color.yellow;
        foodText = Tools.MakeText(gameObject, "Food: "+food.ToString(), new Vector3(0.8f, 0.25f, 0), 45, 5);
        foodText.GetComponent<TextMesh>().color = new Color(0, 0.5f, 0);
    }
    public static void UpdateMoney(int change) {
        money += change;
        moneyText.GetComponent<TextMesh>().text = "Money: " + money.ToString();
    }
    public static void UpdateFood(int change) {
        food += change;
        foodText.GetComponent<TextMesh>().text = "Food: " + food.ToString();
    }
    public static int FoodCount() {
        return food;
    }
    public static int MoneyCount() {
        return money;
    }

    public static void AddItem(string itemName) {
        GameObject item = new GameObject();
        item.AddComponent<Item>();
        item.GetComponent<Item>().SetupItem(itemName);
        item.GetComponent<Draggable>().PutInSlot(NextAvailableSlot());
    }
    public static GameObject NextAvailableSlot() {
        for(int i = 0; i < itemSlots.Count; i++) {
            if (!itemSlots[i].GetComponent<Slot>().occupied) {
                return itemSlots[i];
            }
        }
        return null;
    }
}

/*
public class Item {
    public string name;
    public string type;
}
*/
