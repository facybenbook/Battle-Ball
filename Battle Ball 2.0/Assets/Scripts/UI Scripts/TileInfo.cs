using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* ~~~~~~~~~~~~ Positions of Collider Verts ~~~~~~~~~~~~~~
               0,1---------.5,1--------1,1
                |                       |
                |                       |
                |                       |
                |                       |
               0,.5        .5,.5       1,.5   
                |                       |
                |                       |
                |                       |
                |                       |
               0,0---------.5,0--------1,0
*/


public class TileInfo
{
    //The image that's displayed on this tile
    public Sprite tileImage;

    //A list of positions that determine the location of verteces that compose each triangle on this tile's collider
    public List< List<Vector2> > colliderTriangles;




    //Constructor that sets this tile's image and the shape of its collider
    public TileInfo(Sprite tileImage_ = null, TileShapes colliderShape_ = TileShapes.Empty)
    {
        this.tileImage = tileImage_;

        //Switch statement that sets the triangles that make up this tile's collider mesh
        switch (colliderShape_)
        {
            case TileShapes.Empty:
                this.colliderTriangles = new List<List<Vector2>>();
                break;

            case TileShapes.FullBox:
                this.colliderTriangles = new List<List<Vector2>>(2);
                this.colliderTriangles[0] = new List<Vector2>(3) { new Vector2(0,0), new Vector2(0,1), new Vector2(1,0) };
                this.colliderTriangles[1] = new List<Vector2>(3) { new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
                break;

            case TileShapes.TopHalf:
                this.colliderTriangles = new List<List<Vector2>>(2);
                this.colliderTriangles[0] = new List<Vector2>(3) { new Vector2(0, 0.5f), new Vector2(0, 1), new Vector2(1, 0.5f) };
                this.colliderTriangles[1] = new List<Vector2>(3) { new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0.5f) };
                break;

            case TileShapes.BottomHalf:
                this.colliderTriangles = new List<List<Vector2>>(2);
                this.colliderTriangles[0] = new List<Vector2>(3) { new Vector2(0, 0), new Vector2(0, 0.5f), new Vector2(1, 0) };
                this.colliderTriangles[1] = new List<Vector2>(3) { new Vector2(0, 0.5f), new Vector2(1, 0.5f), new Vector2(1, 0) };
                break;

            case TileShapes.LeftHalf:
                this.colliderTriangles = new List<List<Vector2>>(2);
                this.colliderTriangles[0] = new List<Vector2>(3) { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0.5f, 0) };
                this.colliderTriangles[1] = new List<Vector2>(3) { new Vector2(0, 1), new Vector2(0.5f, 1), new Vector2(0.5f, 0) };
                break;

            case TileShapes.RightHalf:
                this.colliderTriangles = new List<List<Vector2>>(2);
                this.colliderTriangles[0] = new List<Vector2>(3) { new Vector2(0.5f, 0), new Vector2(0.5f, 1), new Vector2(1, 0) };
                this.colliderTriangles[1] = new List<Vector2>(3) { new Vector2(0.5f, 1), new Vector2(1, 1), new Vector2(1, 0) };
                break;

            //The default case is considered to be empty
            default:
                this.colliderTriangles = new List<List<Vector2>>();
                break;
        }
    }

}


/*Enum used in the TileInfo.cs class to represent the different shapes its collider should be
 *  NOTE: If you add a new enum to this list, make sure it's included in the switch statement
 *         in the TileInfo constructor */
public enum TileShapes
{
    Empty,
    FullBox,

    TopHalf,
    BottomHalf,
    LeftHalf,
    RightHalf,

    TopLeftBox,
    TopRightBox,
    BottomLeftBox,
    BottomRightBox/*,

    LeftSlope,
    RightSlope,
    InvertedLeftSlope,
    InvertedRightSlope,

    LeftHalfSlope,
    RightHalfSlope,
    InvertedLeftHalfSlope,
    InvertedRightHalfSlope*/
}