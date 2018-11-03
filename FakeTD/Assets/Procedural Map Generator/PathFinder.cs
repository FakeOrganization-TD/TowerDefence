using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    public Terrain[,] grid;// = new Tile[AStartTest.gridWidth, AStartTest.gridHeight];
    Vector2 startTile;
    Vector2 endTile;
    Vector2 currentTile;

    private int gridWidth;
    private int gridHeight;

    //Create the lists that store allready checked tiles
    List<Vector2> closedList = new List<Vector2>();
    List<Vector2> openList = new List<Vector2>();

    public Pathfinder(Terrain[,] grid,int gridWidth,int gridHeight)
    {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        this.grid = new Terrain[gridWidth, gridHeight];
        this.grid = grid;
    }

    #region A* pseudo
    /////////////////////// A* ///////////////////////
    ///add the starting node to the open list
    ///while the open list is not empty
    /// {
    ///  current node = node from open list with the lowest cost
    ///  if currentnode = goal
    ///    path complete
    ///  else
    ///    move current node to the closed list
    ///    for each adjacent node
    ///      if it lies with the field
    ///        and it isn't an obstacle
    ///          and it isn't on the open list
    ///            and isn't on the closed list
    ///              move it to the open list and calculate cost
    //////////////////////////////////////////////////
    #endregion

    public Terrain[,] SearchPath(Vector2 startTile, Vector2 endTile)
    {
        this.startTile = startTile;
        this.endTile = endTile;

        //Reset all the values
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                grid[i, j].cost = 0;
                grid[i, j].heuristic = 0;
            }
        }

        #region Path validation
        bool canSearch = true;

        if (grid[(int)startTile.x, (int)startTile.y].TerrainType!=TerrainType.Normal)//walkable == false)
        {
            Debug.Log("The start tile is non walkable. Choose a different value than: " + startTile.ToString());
            canSearch = false;
        }
        if (grid[(int)endTile.x, (int)endTile.y].TerrainType != TerrainType.Normal)
        {
            Debug.Log("The end tile is non walkable. Choose a different value than: " + endTile.ToString());
            canSearch = false;
        }
        #endregion
        bool canFindWay = false;
        //Start the A* algorithm
        if (canSearch)
        {
            //add the starting tile to the open list
            openList.Add(startTile);
            currentTile = new Vector2(-1, -1);
            
            //while Openlist is not empty
            while (openList.Count != 0)
            {
                //current node = node from open list with the lowest cost
                currentTile = GetTileWithLowestTotal(openList);

                //If the currentTile is the endtile, then we can stop searching
                if (currentTile.x == endTile.x && currentTile.y == endTile.y)
                {
                    canFindWay = true;
                    Debug.Log("YEHA, We found the end tile!!!! :D");
                    break;
                }
                else
                {
                    //move the current tile to the closed list and remove it from the open list
                    openList.Remove(currentTile);
                    closedList.Add(currentTile);

                    //Get all the adjacent Tiles
                    List<Vector2> adjacentTiles = GetAdjacentTiles(currentTile);

                    foreach (Vector2 adjacentTile in adjacentTiles)
                    {
                        //adjacent tile can not be in the open list
                        if (!openList.Contains(adjacentTile))
                        {
                            //adjacent tile can not be in the closed list
                            if (!closedList.Contains(adjacentTile))
                            {
                                //move it to the open list and calculate cost
                                openList.Add(adjacentTile);

                                Terrain tile = grid[(int)adjacentTile.x, (int)adjacentTile.y];

                                //Calculate the cost
                                tile.cost = grid[(int)currentTile.x, (int)currentTile.y].cost + 1;

                                //Calculate the manhattan distance
                                tile.heuristic = ManhattanDistance(adjacentTile);

                                //calculate the total amount
                                tile.total = tile.cost + tile.heuristic;

                                //make this tile green
                               // LE.EntityColor(tile.cube, Tile.GREEN);
                            }
                        }
                    }
                }
            }
        }
        if (!canFindWay)
        {
            throw new Exception("Nie znajdę ścieżki, losuj jeszcze raz");
        }

        //Make the start and end path 
       

        //Show the path
        ShowPath();
        grid[(int)startTile.x, (int)startTile.y].Height = 1;
        grid[(int)startTile.x, (int)startTile.y].TerrainType = TerrainType.Path;

        grid[(int)endTile.x, (int)endTile.y].Height = 1;
        grid[(int)endTile.x, (int)endTile.y].TerrainType = TerrainType.Path;
        return grid;
    }

    public void ShowPath()
    {
        bool startFound = false;

        Vector2 currentTile = endTile;
        List<Vector2> pathTiles = new List<Vector2>();

        while (startFound == false)
        {
            List<Vector2> adjacentTiles = GetAdjacentTiles(currentTile);
            if(adjacentTiles.Count == 0)
            {
                Debug.Log("przejebane, nie ma ścieżki");
                //throw new Exception("Przejebane, chyba nie ma ściezki");
            }
            //check to see what newest current tile
            foreach (Vector2 adjacentTile in adjacentTiles)
            {
                //Check if it is the start tile
                if (adjacentTile.x == startTile.x && adjacentTile.y == startTile.y)
                    startFound = true;
                //it has to be inside the closed as well as in the open list 
                if (closedList.Contains(adjacentTile) || openList.Contains(adjacentTile))
                {
                    if (grid[(int)adjacentTile.x, (int)adjacentTile.y].cost <= grid[(int)currentTile.x, (int)currentTile.y].cost
                        && grid[(int)adjacentTile.x, (int)adjacentTile.y].cost > 0)
                    {
                        //Change the current Tile
                        currentTile = adjacentTile;

                        //Add this adjacent tile to the path list
                        pathTiles.Add(adjacentTile);

                        //Show the ball //this is the path
                        grid[(int)adjacentTile.x, (int)adjacentTile.y].Height = 1;
                        grid[(int)adjacentTile.x, (int)adjacentTile.y].TerrainType = TerrainType.Path;
                      //  LE.ShowEntity(grid[(int)adjacentTile.x, (int)adjacentTile.y].ball);

                        break;
                    }
                }
            }
        }
    }

    //Calculate the manhattan distance
    public int ManhattanDistance(Vector2 adjacentTile)
    {
        int manhattan = Math.Abs((int)(endTile.x - adjacentTile.x)) + Math.Abs((int)(endTile.y - adjacentTile.y));
        return manhattan;
    }


    //Check if it is in the boundry and if it walkable
    public List<Vector2> GetAdjacentTiles(Vector2 currentTile)
    {
        List<Vector2> adjacentTiles = new List<Vector2>();
        Vector2 adjacentTile;

        //Tile above
        adjacentTile = new Vector2(currentTile.x, currentTile.y + 1);
        if (adjacentTile.y < gridHeight && grid[(int)adjacentTile.x, (int)adjacentTile.y].TerrainType == TerrainType.Normal)
            adjacentTiles.Add(adjacentTile);

        //Tile underneath
        adjacentTile = new Vector2(currentTile.x, currentTile.y - 1);
        if (adjacentTile.y >= 0 && grid[(int)adjacentTile.x, (int)adjacentTile.y].TerrainType == TerrainType.Normal)
            adjacentTiles.Add(adjacentTile);

        //Tile to the right
        adjacentTile = new Vector2(currentTile.x + 1, currentTile.y);
        if (adjacentTile.x < gridWidth && grid[(int)adjacentTile.x, (int)adjacentTile.y].TerrainType == TerrainType.Normal)
            adjacentTiles.Add(adjacentTile);

        //Tile to the left
        adjacentTile = new Vector2(currentTile.x - 1, currentTile.y);
        if (adjacentTile.x >= 0 && grid[(int)adjacentTile.x, (int)adjacentTile.y].TerrainType == TerrainType.Normal)
            adjacentTiles.Add(adjacentTile);

        //OPTIONAL DIAGONAL

        return adjacentTiles;
    }

    //Get the tile with the lowest total value
    public Vector2 GetTileWithLowestTotal(List<Vector2> openList)
    {
        //temp variables
        Vector2 tileWithLowestTotal = new Vector2(-1, -1);
        int lowestTotal = int.MaxValue;

        //search all the open tiles and get the tile with the lowest total cost
        foreach (Vector2 openTile in openList)
        {
            if (grid[(int)openTile.x, (int)openTile.y].total <= lowestTotal)
            {
                lowestTotal = grid[(int)openTile.x, (int)openTile.y].total;
                tileWithLowestTotal = new Vector2((int)openTile.x, (int)openTile.y);
            }
        }

        return tileWithLowestTotal;
    }
}