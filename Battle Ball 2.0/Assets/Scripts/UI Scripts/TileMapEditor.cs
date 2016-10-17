using UnityEngine;
using UnityEditor;

class TileMapEditor: EditorWindow
{
    //The transform that serves as the origin of this tile map
    [HideInInspector]
    public TileMapOrigin mapOrigin;
    //When true, the user can edit the selected tile map and prevents selecting other objects
    private bool enableMapEditing = false;
    


    //Creates a new instance of a Window menu for this Tile Map Editor 
	[MenuItem("Window/Tile Map Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TileMapEditor));
    }


    //Handles the options that appear on this Editor Window that the user can change
    private void OnGUI()
    {
        //If the user is selecting an object that has a Tile Map, the map's settings can be changed
        if(this.mapOrigin != null)
        {
            //Creating a section on the window that lets the user set the width and height of the selected tile map
            GUILayout.Label("Dimensions", EditorStyles.boldLabel);

            //Created an input field to set the width/height of tiles on the map
            this.mapOrigin.tileSize = EditorGUILayout.FloatField("Tile Height and Width", this.mapOrigin.tileSize);

            EditorGUILayout.Space();

            //Created an input field to set the number of rows above the origin
            this.mapOrigin.tilesUp = EditorGUILayout.IntField("Number of Rows Up", this.mapOrigin.tilesUp);
            //Created an input field to set the number of rows below the origin
            this.mapOrigin.tilesDown = EditorGUILayout.IntField("Number of Rows Down", this.mapOrigin.tilesDown);
            //Created an input field to set the number of columns left of the origin
            this.mapOrigin.tilesLeft = EditorGUILayout.IntField("Number of Columns Left", this.mapOrigin.tilesLeft);
            //Created an input field to set the number of columns right of the origin
            this.mapOrigin.tilesRight = EditorGUILayout.IntField("Number of Columns Right", this.mapOrigin.tilesRight);

            EditorGUILayout.Space();

            //Created a boolean input box that allows the player to toggle 
            this.enableMapEditing = EditorGUILayout.Toggle("Enable Tile Map Editing", this.ToggleMapEditing(this.enableMapEditing));
        }
        //Otherwise, there are no options to change because no tile map is selected
        else
        {
            GUILayout.Label("No tile map selected");
        }

        //Creating a section on the window that lets the user set the size of each tile

        //Creating a 2D List of Tiles (GameObject prefabs for each tile) that can be selected and placed on the tile map
    }


    //Function called when the user changes the object(s) they have selected
    private void OnSelectionChange()
    {
        //If the current object selected has a Tile Map component
        if(Selection.gameObjects.GetLength(0) == 1 && Selection.gameObjects[0].GetComponent<TileMapOrigin>() != null)
        {
            //Saves the reference to the Tile Map component
            this.mapOrigin = Selection.gameObjects[0].GetComponent<TileMapOrigin>();
        }
        //If the object selected doesn't have a Tile Map
        else
        {
            //Clears the reference to the Tile Map component
            this.mapOrigin = null;
        }

        //Refreshes this window immediately to either show or hide the user options
        this.Repaint();
    }


    //Function that enables or disables player object selection so that they don't change objects when setting tiles
    public bool ToggleMapEditing(bool mapEditOn_)
    {
        //If true, disables object selection until turned off
        if(mapEditOn_)
        {
            Debug.Log("Editing ON");
            //HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
        //If false, lets the player select objects as normal
        else
        {
            Debug.Log("Editing OFF");
            //HandleUtility.Repaint();
        }


        return mapEditOn_;
    }
}
