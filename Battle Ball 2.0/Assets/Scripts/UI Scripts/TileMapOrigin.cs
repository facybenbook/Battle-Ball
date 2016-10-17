using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMapOrigin : MonoBehaviour
{
    //The distance between one tile's center to the next
    [HideInInspector]
    public float tileSize = 1;

    //Number of tiles to the left of the origin
    [HideInInspector]
    public int tilesLeft;
    //Number of tiles to the right of the origin
    [HideInInspector]
    public int tilesRight;
    //Number of tiles above the origin
    [HideInInspector]
    public int tilesUp;
    //Number of tiles below the origin
    [HideInInspector]
    public int tilesDown;

    //The 2D list that contains every individual tile for this map
    private List<List<TileInfo>> tileGrid;
    //2D list that contains all collision verteces for each tile in this tile map
    private List<List<Vector3>> colliderVerts;


    
    //Function that increases the tile grid in the direction given
    public void IncreaseGrid(Directions direction_ = Directions.Right, int numToAdd_ = 1)
    {
        //Does nothing if the number to add isn't a positive number
        if (numToAdd_ < 1)
            return;

        //Inserts new rows at the beginning of the first list
        if(direction_ == Directions.Up)
        {
            this.tilesUp += numToAdd_;

            //Loops through a number of times equal to the num to add
            for(int n = 0; n < numToAdd_; ++n)
            {
                this.tileGrid.Insert(0, new List<TileInfo>(this.tilesLeft + this.tilesRight));
            }
        }
        //Adds new rows at the end of the first list
        else if(direction_ == Directions.Down)
        {
            this.tilesDown += numToAdd_;

            //Loops through a number of times equal to the num to add
            for(int n = 0; n < numToAdd_; ++n)
            {
                this.tileGrid.Add(new List<TileInfo>(this.tilesLeft + this.tilesRight));
            }
        }
        //Loops through each row in the first list and inserts new columns at the beginning of the inner lists
        else if(direction_ == Directions.Left)
        {
            this.tilesLeft += numToAdd_;

            //Loops through each row
            for(int r = 0; r < this.tileGrid.Count; ++r)
            {
                for (int n = 0; n < numToAdd_; ++n)
                {
                    this.tileGrid[r].Insert(0, new TileInfo());
                }
            }
        }
        //Loops through each row in the first list and inserts new columns at the end of the inner lists
        else if(direction_ == Directions.Right)
        {
            this.tilesRight += numToAdd_;

            //Loops through each row
            for(int r = 0; r < this.tileGrid.Count; ++r)
            {
                for (int n = 0; n < numToAdd_; ++n)
                {
                    this.tileGrid[r].Add(new TileInfo());
                }
            }
        }
    }


    //Function that decreases the tile grid in the direction given
    public void DecreaseGrid(Directions direction_ = Directions.Right, int numToRemove_ = 1)
    {
        //Does nothing if the number to remove isn't a positive number
        if (numToRemove_ < 1)
            return;

        int tilesRemoved = numToRemove_;

        //Removes rows at the end of the first list
        if(direction_ == Directions.Up)
        {
            //Makes sure that we can't subtract from a direction enough to drop below 0
            if (tilesRemoved > this.tilesUp)
                tilesRemoved = this.tilesUp;

            this.tilesUp -= tilesRemoved;

            //Loops through a number of times equal to the rows removed
            for(int n = 0; n < tilesRemoved; ++n)
            {
                //Nulls and destroys each tile in the removed row
                for(int r = 0; r < this.tileGrid[0].Count; ++r)
                {
                    this.tileGrid[0][r] = null;
                }

                this.tileGrid.RemoveAt(0);
            }
        }
        //Removes rows at the end of the first list
        else if(direction_ == Directions.Down)
        {
            //Makes sure that we can't subtract from a direction enough to drop below 0
            if (tilesRemoved > this.tilesDown)
                tilesRemoved = this.tilesDown;

            this.tilesDown -= tilesRemoved;

            //Loops through a number of times equal to the rows removed
            for(int n = 0; n < tilesRemoved; ++n)
            {
                //Nulls and destroys each tile in the removed row
                for (int r = 0; r < this.tileGrid[0].Count; ++r)
                {
                    this.tileGrid[this.tileGrid.Count - 1][r] = null;
                }

                this.tileGrid.RemoveAt(this.tileGrid.Count - 1);
            }
        }
        //Loops through each row in the first list and removes columns at the beginning of the inner lists
        else if(direction_ == Directions.Left)
        {
            //Makes sure that we can't subtract from a direction enough to drop below 0
            if (tilesRemoved > this.tilesLeft)
                tilesRemoved = this.tilesLeft;

            this.tilesLeft -= tilesRemoved;

            //Loops through each row
            for(int r = 0; r < this.tileGrid.Count; ++r)
            {
                //Destroys, nulls, and removes the first tile in each row
                for(int n = 0; n < tilesRemoved; ++n)
                {
                    this.tileGrid[r][0] = null;
                    this.tileGrid[r].RemoveAt(0);
                }
            }
        }
        else if(direction_ == Directions.Right)
        {
            //Makes sure that we can't subtract from a direction enough to drop below 0
            if (tilesRemoved > this.tilesRight)
                tilesRemoved = this.tilesRight;

            this.tilesRight -= tilesRemoved;

            //Loops through each row
            for(int r = 0; r < this.tileGrid.Count; ++r)
            {
                //Destroys, nulls, and removes the last tile in each row
                for(int n = 0; n < tilesRemoved; ++n)
                {
                    this.tileGrid[r][this.tileGrid.Count - 1] = null;
                    this.tileGrid[r].RemoveAt(this.tileGrid.Count - 1);
                }
            }
        }
    }


    /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ NOT NEEDED? REPLACING GAMEOBJ W/ VERTS
    //Function that makes sure all the tiles in this tile map are set to the correct position based on the width
    private void UpdateTilePositions()
    {
        //Position of the top-left corner of this tile map (starting point)
        Vector3 startPos = this.transform.localPosition;
        startPos += new Vector3(this.tileSize * -this.tilesLeft,
                                this.tileSize * this.tilesUp,
                                0);

        //The current position where we're setting tiles
        Vector3 currentPos = startPos;

        //Loops through each row
        for(int r = 0; r < (this.tilesLeft + this.tilesRight); ++r)
        {
            //Loops through each column
            for(int c = 0; c < (this.tilesUp + this.tilesDown); ++c)
            {
                //Sets the current tile at the offset of the startPos if it's not empty
                if (this.tileGrid[r][c] != null)
                    this.tileGrid[r][c].transform.localPosition = startPos + new Vector3(this.tileSize * r,
                                                                                     this.tileSize * c,
                                                                                     0);
            }
        }
    }*/


    //Function that sets a tile at a given location in space. If tileToSet_ is NULL, deletes a tile
    public void SetTile(Vector3 clickPos_, GameObject tileToSet_ = null)
    {
        //Finds the position clicked in local space relative to this tile map
        Vector3 localPos = this.transform.InverseTransformPoint(clickPos_);

        //Now we round the local X position to the nearest grid location
        float roundedXCoord = localPos.x;
        if(roundedXCoord % this.tileSize < (this.tileSize / 2))
        {
            roundedXCoord = Mathf.Floor(roundedXCoord / this.tileSize) * this.tileSize;
        }
        else
        {
            roundedXCoord = Mathf.Ceil(roundedXCoord / this.tileSize) * this.tileSize;
        }

        //And we do the same for the local Y position as well
        float roundedYCoord = localPos.y;
        if (roundedYCoord % this.tileSize < (this.tileSize / 2))
        {
            roundedYCoord = Mathf.Floor(roundedYCoord / this.tileSize) * this.tileSize;
        }
        else
        {
            roundedYCoord = Mathf.Ceil(roundedYCoord / this.tileSize) * this.tileSize;
        }

        /*Sets the local position to the new, rounded grid locations, and
        Zeroing out the local Z pos so everything's on the same plane */
        localPos = new Vector3();

        //If this tile is in the Top-Right quadrant
        if (localPos.x >= 0 && localPos.y >= 0)
        {
            //Does nothing if the X or Y coords are out of bounds
            if(localPos.x > (this.tilesRight * this.tileSize) || localPos.y > (this.tilesUp * this.tileSize))
            {
                return;
            }
        }
        //If this tile is in the Top-Left quadrant
        else if(localPos.x < 0 && localPos.y >= 0)
        {
            //Does nothing if the X or Y coords are out of bounds
            if (localPos.x < (this.tilesLeft * this.tileSize) || localPos.y > (this.tilesUp * this.tileSize))
            {
                return;
            }
        }
        //If this tile is in the Bottom-Left quadrant
        else if(localPos.x < 0 && localPos.y < 0)
        {
            //Does nothing if the X or Y coords are out of bounds
            if (localPos.x < (this.tilesLeft * this.tileSize) || localPos.y < (this.tilesDown * this.tileSize))
            {
                return;
            }
        }
        //If this tile is in the Bottom-Right quadrant
        else if(localPos.x >= 0 && localPos.y < 0)
        {
            //Does nothing if the X or Y coords are out of bounds
            if (localPos.x > (this.tilesRight * this.tileSize) || localPos.y < (this.tilesDown * this.tileSize))
            {
                return;
            }
        }
    }


    //Function that generates a custom mesh for this tile map's collider
    public void GenerateCollider()
    {
        //Clears this map's current collider since it's now outdated

        //Loops through each tile in the tileGrid list
            //Create vec2 that tracks the XY offset based on the current tile location and tile width
            //For each triangle in each tile, create 3 placeholder verts
            //Placeholder verts = XY offset + value of the verts from TileInfo * tile width
            //Push placeholder verts to the current list of total verts for the new mesh
            //Create a new triangle using placeholder vert indexes and push to the total list of triangles
        
        //Once all triangles have been created, call the CleanUp function below
    }


    //Function that combines duplicate vertices for the mesh collider we're creating so that it's more efficient
    private void CleanUpVerts()
    {
        //Create an empty list of ints. These are index locations that are duplicates and will be popped from the total vert list
        List<int> duplicateVertIndex = new List<int>();

        //Iterate through each vertex in each triangle, checking each one against all verts in subsequent triangles
            //If the verts we're checking against are the same position
                //Push the index of the duplicate vert to the duplicateVertIndex list (check if it already exists first)
                //Replace the current index in the triangle's vertex list with the one we're checking against
        
        // ########## COME BACK TO THIS LATER!! If any verts are popped from the main list, ALL triangles will be screwed up because
        // the indexes will be shifted down
    }
}


//Enum used in TileMapOrigin class to determine the direction when changing the size
public enum Directions
{
    Up,
    Down,
    Left,
    Right
}