using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMapGenerator : MonoBehaviour
{
    public float spriteWidth;
    public float spriteHeight;
    public GameObject tilePrefab;
    public Transform tileParent;

    public MapGenerator mapGenerator;
    public void CreateMapObjects(float[,] noiseMap, int mapWidth, int mapHeight, TerrainType[] regions) {

        Tile[,] tileMap = new Tile[mapWidth, mapHeight];

        float halfMapWidth = spriteWidth * mapWidth/2;
        float halfMapHeight = spriteWidth * mapHeight/ 2;

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++) {
                    if (currentHeight <= regions[i].height) {
                        GameObject tileGO = Instantiate(tilePrefab, tileParent);
                        tileGO.transform.position = new Vector3(x * spriteWidth - halfMapWidth, y * spriteHeight - halfMapHeight, 0f);
                        Tile currentTile = tileGO.GetComponent<Tile>();
                        currentTile.SetTile(x, y, regions[i].name,regions[i]);
                        tileMap[x, y] = currentTile;
                        break;
                    }
                }
            }
        }
        MapAutoTiling(tileMap,mapWidth,mapHeight);
        SetAllSprites(tileMap,mapWidth,mapHeight);
       
       

    }

    public void MapAutoTiling(Tile[,] tileMap,int mapWidth, int mapHeight) {
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                AuoTileGenerator.AutoTiling(tileMap, tileMap[x, y], mapWidth, mapHeight);

            }
        }
    }

    public float MapWidth() => (spriteWidth * mapGenerator.mapWidth);
    public float MapHeight() => (spriteHeight * mapGenerator.mapHeight);


    public void SetAllSprites(Tile[,] tileMap, int mapWidth, int mapHeight) {
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                Tile currentTile = tileMap[x, y];
                currentTile.SetTileSprite();
            }
        }
    }

    public void DeleteMapObjects() {

        foreach (Transform tile in tileParent) {

            Debug.Log(tile.GetSiblingIndex()); 
            DestroyImmediate(tile.gameObject);
        }
        foreach (Transform tile in tileParent) {

            Debug.Log(tile.GetSiblingIndex());
            DestroyImmediate(tile.gameObject);
        }
        foreach (Transform tile in tileParent) {

            Debug.Log(tile.GetSiblingIndex());
            DestroyImmediate(tile.gameObject);
        }
        foreach (Transform tile in tileParent) {

            Debug.Log(tile.GetSiblingIndex());
            DestroyImmediate(tile.gameObject);
        }
        foreach (Transform tile in tileParent) {

            Debug.Log(tile.GetSiblingIndex());
            DestroyImmediate(tile.gameObject);
        }
    }
}
