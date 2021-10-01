using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState { Middle, Edge, Corner }

public static class AuoTileGenerator {



    public static void AutoTiling(Tile[,] tileMap, Tile tile, int mapWidth, int mapHeight) {

        TileSpriteSelection(tileMap, tile, GetTileState(tile, mapWidth, mapHeight), mapWidth, mapHeight);
    }


    private static TileState GetTileState(Tile tile, int mapWidth, int mapHeight) {
        int curX = tile.x;
        int curY = tile.y;

        int firstX = 0;
        int firstY = 0;

        int lastX = mapWidth - 1;
        int lastY = mapHeight - 1;



        if ((curX < lastX && curX > firstX) && (curY < lastY && curY > firstY))
            return TileState.Middle;
        else if (curX == firstX && (curY == firstY || curY == lastY) || (curX == lastX && (curY == firstY || curY == lastY)))
            return TileState.Edge;
        else
            return TileState.Corner;

    }


    private static Sprite TileSpriteSelection(Tile[,] tileMap, Tile tile, TileState tileState, int mapwidth, int mapHeight) {
        int x = tile.x;
        int y = tile.y;
        Sprite sprite = null;
        //Debug.Log(tile.x + " " + tile.y);

        switch (tileState) {
            case TileState.Corner:
                CornerSelect(tileMap, tile, mapwidth, mapHeight);
                break;
            case TileState.Edge:
                EdgeSelect(tileMap, tile, mapwidth, mapHeight);
                break;
            case TileState.Middle:
                MiddleAutoTile(tileMap, tile);
                break;
            default:
                break;
        }

        return sprite;
    }

    #region Middle
    private static void MiddleAutoTile(Tile[,] tileMap, Tile tile) {

        int x = tile.x;
        int y = tile.y;

        float tileHeight = tile.type.height;

        //Edges(Up,down,left,right)
        if (tileHeight > tileMap[x, y + 1].type.height) {

            tileMap[x, y + 1].bitTile[2] = 1;
            tileMap[x, y + 1].bitTile[3] = 1;
            
        }
        if (tileHeight > tileMap[x, y - 1].type.height) {
            tileMap[x, y - 1].bitTile[0] = 1;
            tileMap[x, y - 1].bitTile[1] = 1;
           
        }
        if (tileHeight > tileMap[x + 1, y].type.height) {
            tileMap[x + 1, y].bitTile[0] = 1;
            tileMap[x + 1, y].bitTile[2] = 1;
        }
        if (tileHeight > tileMap[x - 1, y].type.height) {

            tileMap[x - 1, y].bitTile[1] = 1;
            tileMap[x - 1, y].bitTile[3] = 1;
        }
        //Corners (upleft, downright etc)
        if (tileHeight > tileMap[x - 1, y + 1].type.height)
            tileMap[x - 1, y + 1].bitTile[3] = 1;
        if (tileHeight > tileMap[x - 1, y - 1].type.height)
            tileMap[x - 1, y - 1].bitTile[1] = 1;
        if (tileHeight > tileMap[x + 1, y + 1].type.height)
            tileMap[x + 1, y + 1].bitTile[2] = 1;
        if (tileHeight > tileMap[x + 1, y - 1].type.height)
            tileMap[x + 1, y - 1].bitTile[0] = 1;
    }

    #endregion

    #region Edge
    private static void EdgeSelect(Tile[,] tileMap, Tile tile, int mapwidth, int mapHeight) {

        int x = tile.x;
        int y = tile.y;

        int lastX = mapwidth - 1;
        int lastY = mapHeight - 1;

        float tileHeight = tile.type.height;

        if (x == 0 && y == 0) {
            //tile[0,0]
            if (tileHeight > tileMap[x, y + 1].type.height) {
                tileMap[x, y + 1].bitTile[2] = 1;
                tileMap[x, y + 1].bitTile[3] = 1;
            }
            if (tileHeight > tileMap[x + 1, y].type.height) {
                tileMap[x + 1, y].bitTile[0] = 1;
                tileMap[x + 1, y].bitTile[2] = 1;
            }
            if (tileHeight > tileMap[x + 1, y + 1].type.height)
                tileMap[x + 1, y + 1].bitTile[2] = 1;
        } else if (x == 0 && y == lastY) {
            //tile[0,height]
            if (tileHeight > tileMap[x, y - 1].type.height) {

                tileMap[x, y - 1].bitTile[0] = 1;
                tileMap[x, y - 1].bitTile[1] = 1;
            }
            if (tileHeight > tileMap[x + 1, y].type.height) {
                tileMap[x + 1, y].bitTile[0] = 1;
                tileMap[x + 1, y].bitTile[2] = 1;
            }
            if (tileHeight > tileMap[x + 1, y - 1].type.height)
                tileMap[x + 1, y - 1].bitTile[0] = 1;
        } else if (x == lastX && y == 0) {
            //tile[width,0]
            if (tileHeight > tileMap[x, y + 1].type.height) {
                tileMap[x, y + 1].bitTile[2] = 1;
                tileMap[x, y + 1].bitTile[3] = 1;
            }
            if (tileHeight > tileMap[x - 1, y].type.height) {
                tileMap[x - 1, y].bitTile[1] = 1;
                tileMap[x - 1, y].bitTile[3] = 1;
            }
            if (tileHeight > tileMap[x - 1, y + 1].type.height)
                tileMap[x - 1, y + 1].bitTile[3] = 1;
        } else if (x == lastX && y == lastY) {
            //tile[width,height]
            if (tileHeight > tileMap[x, y - 1].type.height) {
                tileMap[x, y - 1].bitTile[0] = 1;
                tileMap[x, y - 1].bitTile[1] = 1;
            }
            if (tileHeight > tileMap[x - 1, y].type.height) {
                tileMap[x - 1, y].bitTile[1] = 1;
                tileMap[x - 1, y].bitTile[3] = 1;
            }
            if (tileHeight > tileMap[x - 1, y - 1].type.height)
                tileMap[x - 1, y - 1].bitTile[1] = 1;
        }

    }
    #endregion

    #region Corner
    private static void CornerSelect(Tile[,] tileMap, Tile tile, int mapwidth, int mapHeight) {

        int x = tile.x;
        int y = tile.y;


        int lastX = mapwidth - 1;
        int lastY = mapHeight - 1;

        float tileHeight = tile.type.height;


        if (x == 0) {
            //tile[0,y]
            if (tileHeight > tileMap[x, y + 1].type.height) {
                tileMap[x, y + 1].bitTile[2] = 1;
                tileMap[x, y + 1].bitTile[3] = 1;
            }
            if (tileHeight > tileMap[x, y - 1].type.height) {

                tileMap[x, y - 1].bitTile[0] = 1;
                tileMap[x, y - 1].bitTile[1] = 1;
            }
            if (tileHeight > tileMap[x + 1, y].type.height) {
                tileMap[x + 1, y].bitTile[0] = 1;
                tileMap[x + 1, y].bitTile[2] = 1;
            }
            if (tileHeight > tileMap[x + 1, y + 1].type.height)
                tileMap[x + 1, y + 1].bitTile[2] = 1;
            if (tileHeight > tileMap[x + 1, y - 1].type.height)
                tileMap[x + 1, y - 1].bitTile[0] = 1;
        } else if (x == lastX) {
            //tile[width,y]
            if (tileHeight > tileMap[x, y + 1].type.height) {
                tileMap[x, y + 1].bitTile[2] = 1;
                tileMap[x, y + 1].bitTile[3] = 1;
            }
            if (tileHeight > tileMap[x, y - 1].type.height) {
                tileMap[x, y - 1].bitTile[0] = 1;
                tileMap[x, y - 1].bitTile[1] = 1;
            }
            if (tileHeight > tileMap[x - 1, y].type.height) {
                tileMap[x - 1, y].bitTile[1] = 1;
                tileMap[x - 1, y].bitTile[3] = 1;
            }
            if (tileHeight > tileMap[x - 1, y + 1].type.height)
                tileMap[x - 1, y + 1].bitTile[3] = 1;
            if (tileHeight > tileMap[x - 1, y - 1].type.height)
                tileMap[x - 1, y - 1].bitTile[1] = 1;
        } else if (y == 0) {
            //tile [x,0]
            if (tileHeight > tileMap[x + 1, y].type.height) {
                tileMap[x + 1, y].bitTile[0] = 1;
                tileMap[x + 1, y].bitTile[2] = 1;
            }
            if (tileHeight > tileMap[x - 1, y].type.height) {
                tileMap[x - 1, y].bitTile[1] = 1;
                tileMap[x - 1, y].bitTile[3] = 1;
            }
            if (tileHeight > tileMap[x, y + 1].type.height) {
                tileMap[x, y + 1].bitTile[2] = 1;
                tileMap[x, y + 1].bitTile[3] = 1;
            }
            if (tileHeight > tileMap[x - 1, y + 1].type.height)
                tileMap[x - 1, y + 1].bitTile[3] = 1;
            if (tileHeight > tileMap[x + 1, y + 1].type.height)
                tileMap[x + 1, y + 1].bitTile[2] = 1;
        } else if (y == lastY) {
            //tile[x,height]
            if (tileHeight > tileMap[x + 1, y].type.height) {
                tileMap[x + 1, y].bitTile[0] = 1;
                tileMap[x + 1, y].bitTile[2] = 1;
            }
            if (tileHeight > tileMap[x - 1, y].type.height) {
                tileMap[x - 1, y].bitTile[1] = 1;
                tileMap[x - 1, y].bitTile[3] = 1;
            }
            if (tileHeight > tileMap[x, y - 1].type.height) {
                tileMap[x, y - 1].bitTile[0] = 1;
                tileMap[x, y - 1].bitTile[1] = 1;
            }
            if (tileHeight > tileMap[x - 1, y - 1].type.height)
                tileMap[x - 1, y - 1].bitTile[1] = 1;

            if (tileHeight > tileMap[x + 1, y - 1].type.height)
                tileMap[x + 1, y - 1].bitTile[0] = 1;
        }
    }
    #endregion


}

