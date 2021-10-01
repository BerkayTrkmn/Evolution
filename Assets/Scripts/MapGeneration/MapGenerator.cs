using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public static MapGenerator Instance;
    public MapDisplay mapDisplay;

    public enum DrawMode { NoiseMap, ColorMap, ObjectMap }

    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;
    public bool autoUpdate;
    public bool planeActive;
    public TerrainType[] regions;

    public SpriteMapGenerator spriteGenerator;
    private void Awake() {
        spriteGenerator.mapGenerator = this;
        if (Instance == null) Instance = this; else Destroy(this);
    }
    private void Start() {
        GenerateMap();
       
    }
    public void GenerateMap() {

        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        if (drawMode == DrawMode.NoiseMap)
            mapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        else if (drawMode == DrawMode.ColorMap) {
            Color[] colorMap = GenerateColorsInMap(noiseMap);
            mapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        } else if (drawMode == DrawMode.ObjectMap)
            spriteGenerator.CreateMapObjects(noiseMap, mapWidth, mapHeight, regions);

    }

    //private void OnValidate() {
    //    if (mapWidth < 1) mapWidth = 1;
    //    if (mapHeight < 1) mapHeight = 1;
    //    if (lacunarity < 1) lacunarity = 1;
    //    if (octaves < 0) octaves = 0;

    //    foreach (Transform tile in spriteGenerator.tileParent) {
    //        DestroyImmediate(tile.gameObject);
    //    }
    //    if (planeActive)
    //        mapDisplay.textureRender.gameObject.SetActive(true);
    //    else
    //        mapDisplay.textureRender.gameObject.SetActive(false);
    //}

    public Color[] GenerateColorsInMap(float[,] noiseMap) {

        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++) {
                    if (currentHeight <= regions[i].height) {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        return colorMap;
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
    //
    public Sprite[] rules;
}


