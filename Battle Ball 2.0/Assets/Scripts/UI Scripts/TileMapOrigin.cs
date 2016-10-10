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
    private List<List<GameObject>> tileGrid;


    
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
                this.tileGrid.Insert(0, new List<GameObject>(this.tilesLeft + this.tilesRight));
            }
        }
        //Adds new rows at the end of the first list
        else if(direction_ == Directions.Down)
        {
            this.tilesDown += numToAdd_;

            //Loops through a number of times equal to the num to add
            for(int n = 0; n < numToAdd_; ++n)
            {
                this.tileGrid.Add(new List<GameObject>(this.tilesLeft + this.tilesRight));
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
                    this.tileGrid[r].Insert(0, new GameObject());
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
                    this.tileGrid[r].Add(new GameObject());
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
                    Destroy(this.tileGrid[0][r]);
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
                    Destroy(this.tileGrid[this.tileGrid.Count - 1][r]);
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
                    Destroy(this.tileGrid[r][0]);
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
                    Destroy(this.tileGrid[r][this.tileGrid.Count - 1]);
                    this.tileGrid[r][this.tileGrid.Count - 1] = null;
                    this.tileGrid[r].RemoveAt(this.tileGrid.Count - 1);
                }
            }
        }
    }


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
    }


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
}

//Enum used in TileMapOrigin class to determine the direction when changing the size
public enum Directions
{
    Up,
    Down,
    Left,
    Right
}