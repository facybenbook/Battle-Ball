using UnityEngine;
using UnityEditor;

class TileMapEditor: EditorWindow
{
    //The transform that serves as the origin of this tile map
    public Object mapOrigin;
    
    


    //Creates a new instance of a Window menu for this Tile Map Editor 
	[MenuItem("Window/Tile Map Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TileMapEditor));
    }



    private void OnGUI()
    {
        GUILayout.Label("Map Origin", EditorStyles.boldLabel);
        //this.mapOrigin = EditorGUILayout.PropertyField(SerializedPropertyType.ObjectReference, "Tile Map Origin");

        //Inserting a space between the origin and dimension inputs
        EditorGUILayout.Space();

        //Creating a section on the window that lets the user set the width and height of the selected tile map
        GUILayout.Label("Dimensions", EditorStyles.boldLabel);
        /*this.tilesLeft = EditorGUILayout.IntField("Tiles Left of the Origin", this.tilesLeft);
        this.tilesRight = EditorGUILayout.IntField("Tiles Right of the Origin", this.tilesRight);
        this.tilesUp = EditorGUILayout.IntField("Tiles Above the Origin", this.tilesUp);
        this.tilesDown = EditorGUILayout.IntField("Tiles Below the Origin", this.tilesDown);*/

        //Creating a section on the window that lets the user set the size of each tile

        //Creating a 2D List of Tiles (GameObject prefabs for each tile) that can be selected and placed on the tile map
    }


    private void OnSelectionChange()
    {
        if(Selection.gameObjects[0].GetComponent<TileMapOrigin>())
        {

        }
    }
}
