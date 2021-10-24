using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public int x;
    public int y;
    public string tileName;
    public Sprite sprite;
    public SpriteRenderer tileRenderer;
    public int[] bitTile;
    public TerrainType type;
   public void SetTile(int _x, int _y,string _tileName, TerrainType _type) {
        this.x = _x;
        this.y = _y;
        this.tileName = _tileName;
        //TODO : Height and temperature random values must be balanced
        //new words feels bad if you can this needs better algorithm
        type = _type;
       
        bitTile = new int[4];
    }

    public void SetTileSprite() {
        this.sprite = type.rules[GetTileSpriteWithRule()];
        tileRenderer.sprite = sprite;
    }

    public int GetTileSpriteWithRule() {

        return bitTile[0] + bitTile[1] * 2 + bitTile[2] * 4 + bitTile[3] * 8;
    }
}
[System.Serializable]
public class TerrainType {

    public int id;
    public string name;
    public float height;
    public Color color;
    // leftup, rightup,leftdown,rightDown, up, down, left, right
    public Sprite sprite;
    public float temperature;
    public Sprite[] rules;
}
