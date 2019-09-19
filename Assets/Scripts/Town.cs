using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject nodePrefab;
    public List<GameObject> TileList = new List<GameObject>();
    public GameObject[][] Nodes;
    public GameObject[][] Tiles;
    int mapHeight;

    private void Awake() {
        GenerateMap(7);
        AddLocations();
    }

    // Start is called before the first frame update
    void Start() {
        AttachNodes();
        AttachTiles();
        SpreadExploits();
    }



    // Update is called once per frame
    void Update() {

    }

    void GenerateMap(int maxBreadth) {
        int minBreadth = 2;
        int breadth = minBreadth;
        bool increasing = true;
        int row = 1;

        //TileList = new GameObject[GetTileTotal(maxBreadth, minBreadth)];

        mapHeight = ((maxBreadth - minBreadth) * 2) + 1;
        Tiles = new GameObject[mapHeight][];
        Nodes = new GameObject[mapHeight][];

        while (breadth >= minBreadth) {
            MakeLayer(row, breadth);
            if (increasing && breadth < maxBreadth) breadth++;
            else {
                increasing = false;
                breadth--;
            }
            row++;
        }
    }

    void SpreadExploits() {
        for (int i = 0; i < TileList.Count; i++) {
            GameObject tile = TileList[i];
            //print("Tile: " + tile.GetComponent<Tile>().type);
            //print("Dict: " + Events.ExploitDict["Wander"]);
            switch (tile.GetComponent<Tile>().type) {
                case "":
                    tile.GetComponent<Tile>().exploits.Add(Events.ExploitDict["Wander"]);
                    tile.GetComponent<Tile>().exploits.Add(Events.ExploitDict["Park"]);
                    break;
                case "Store":
                    tile.GetComponent<Tile>().exploits.Add(Events.ExploitDict["IntoStore"]);
                    break;
                case "Home":
                    tile.GetComponent<Tile>().exploits.Add(Events.ExploitDict["ThroughFrontyard"]);
                    tile.GetComponent<Tile>().exploits.Add(Events.ExploitDict["ThroughHouse"]);
                    tile.GetComponent<Tile>().exploits.Add(Events.ExploitDict["ThroughBackyard"]);
                    break;
            }
        }
    }
    /*
    void GiveNodesExploits(GameObject tile, Exploit exploit) {
        if (tile.GetComponent<Tile>().NorthNode) tile.GetComponent<Tile>().NorthNode.GetComponent<Node>().exploits.Add(exploit);
        if (tile.GetComponent<Tile>().SouthNode) tile.GetComponent<Tile>().SouthNode.GetComponent<Node>().exploits.Add(exploit);
        if (tile.GetComponent<Tile>().EastNode) tile.GetComponent<Tile>().EastNode.GetComponent<Node>().exploits.Add(exploit);
        if (tile.GetComponent<Tile>().WestNode) tile.GetComponent<Tile>().WestNode.GetComponent<Node>().exploits.Add(exploit);
    }
    */

    void AttachNodes() {
        for (int i = 0; i< mapHeight-1; i++) {
            for(int j = 0; j < Nodes[i].Length; j++) {
                if (i < Mathf.FloorToInt(mapHeight / 2)) {
                    Nodes[i][j].GetComponent<Node>().leftNeighbour = Nodes[i + 1][j];
                    Nodes[i][j].GetComponent<Node>().rightNeighbour = Nodes[i + 1][j+1];
                }
                else {
                    if (j > 0) Nodes[i][j].GetComponent<Node>().leftNeighbour = Nodes[i+1][j-1];
                    if (j <Nodes[i].Length-1) Nodes[i][j].GetComponent<Node>().rightNeighbour = Nodes[i + 1][j];
                }
            }
        }
    }

    void AttachTiles() {
        for (int i = 0; i < mapHeight; i++) {
            for (int j = 0; j < Tiles[i].Length; j++) {
                GameObject tile = Tiles[i][j];
                Nodes[i][j].GetComponent<Node>().rightTile = tile;
                Nodes[i][j + 1].GetComponent<Node>().leftTile = tile;
                if (i < Mathf.FloorToInt(mapHeight / 2)) {
                    if (i > 0) Nodes[i - 1][j].GetComponent<Node>().forwardTile = tile;
                }
                else if (i == Mathf.FloorToInt(mapHeight / 2)){
                    if (i > 0) Nodes[i - 1][j].GetComponent<Node>().forwardTile = tile;
                }
                else {
                    Nodes[i - 1][j + 1].GetComponent<Node>().forwardTile = tile;
                    //if (i < mapHeight - 1) tile.GetComponent<Tile>().NorthNode = Nodes[i + 1][j];
                }
            }
        }
    }

    void AddLocations() {
        AddLocation(Tiles[1][1], "Store", "7-11", new Vector2(0.15f,0.15f));
        AddLocation(Tiles[3][1], "Store", "7-11", new Vector2(0.15f, 0.15f));
        AddLocation(Tiles[3][4], "Store", "7-11", new Vector2(0.15f, 0.15f));
        AddLocation(Tiles[4][3], "Store", "7-11", new Vector2(0.15f, 0.15f));
        AddLocation(Tiles[5][1], "Store", "7-11", new Vector2(0.15f, 0.15f));
        AddLocation(Tiles[5][6], "Store", "7-11", new Vector2(0.15f, 0.15f));
        AddLocation(Tiles[6][2], "Store", "7-11", new Vector2(0.15f, 0.15f));
        AddLocation(Tiles[6][4], "Store", "7-11", new Vector2(0.15f, 0.15f));
        AddLocation(Tiles[8][0], "Store", "7-11", new Vector2(0.15f, 0.15f));
        AddLocation(Tiles[8][2], "Store", "7-11", new Vector2(0.15f, 0.15f));

        AddHouses(20);

    }

    void AddHouses(int houseNum) {
        while (houseNum > 0) {
            List<GameObject> tileListCopy = new List<GameObject>(TileList);
            GameObject chosenTile = tileListCopy[Random.Range(0, tileListCopy.Count)];
            while(chosenTile.GetComponent<Tile>().type != "") {
                tileListCopy.Remove(chosenTile);
                chosenTile = tileListCopy[Random.Range(0, tileListCopy.Count)];
            }
            AddLocation(chosenTile, "Home", "House", new Vector2(0.65f, 0.65f));
            houseNum--;
        }
    }

    void AddLocation(GameObject tile, string typeName, string spriteName, Vector2 scale) {
        tile.GetComponent<Tile>().type = typeName;
        GameObject locationSprite = new GameObject();
        SpriteRenderer rend = locationSprite.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>("Sprites/" + spriteName);
        locationSprite.transform.parent = tile.transform;
        locationSprite.transform.position = tile.transform.position;
        rend.sprite = sprite;
        rend.sortingOrder = 42;
        locationSprite.transform.localScale = scale;
    }

    void MakeLayer(int row, int breadth) {
        Tiles[row - 1] = new GameObject[breadth];
        Nodes[row - 1] = new GameObject[breadth+1];
        float xSpacing = 1.7f;
        float ySpacing = xSpacing / 2;
        for (int i = 0; i < breadth; i++) {
            Nodes[row - 1][i] = MakeNode(new Vector3(((-breadth) * xSpacing / 2) + ((i) * xSpacing), row * ySpacing, 0));
            Tiles[row-1][i] = MakeTile(new Vector3(((-breadth + 1) * xSpacing / 2) + (i * xSpacing), row * ySpacing, 0));
            TileList.Add(Tiles[row - 1][i]);
        }
        Nodes[row - 1][breadth] = MakeNode(new Vector3(((breadth) * xSpacing/2), row * ySpacing, 0));
    }

    GameObject MakeTile(Vector3 pos) {
        GameObject tile = Instantiate(tilePrefab);
        tile.transform.parent = this.transform;
        tile.transform.position = tile.transform.parent.position + pos;
        
        return tile;
    }

    GameObject MakeNode(Vector3 pos) {
        GameObject node = Instantiate(nodePrefab);
        node.transform.parent = this.transform;
        node.transform.position = node.transform.parent.position + pos;


        return node;
    }

    int GetTileTotal(int maxBreadth, int minBreadth) {
        int sum = 0;
        for (int i = minBreadth; i <= maxBreadth; i++) {
            sum += i;
            if (i < maxBreadth) sum += i;
        }
        return sum;
    }

}
