using UnityEngine;
using System.Collections;

public class Manager_GlobalData : MonoBehaviour
{
    //A reference to the object that stores data between scenes
    public static GameObject globalData;

    //Determines if the mouse cursor is visible or hidden
    public bool ShowMouseCursor = true;

    static public Color P1HilightColor = new Color(1, 0.2f, 0.2f, 1);
    static public Color P2HilightColor = new Color(0, 0.5f, 1, 1);
    static public Color P3HilightColor = Color.green;
    static public Color P4HilightColor = Color.yellow;


    // Use this for initialization
    void Awake ()
    {
        //If there isn't already a static reference to this global data object, creates a new one
	    if(globalData == null)
        {
            DontDestroyOnLoad(gameObject);
            globalData = gameObject;
        }
        //Otherwise, we already have a global data object created and we can't make a new one
        else if(globalData != gameObject)
        {
            Destroy(gameObject);
        }

        //Determines if the mouse cursor is visible or hidden
        UnityEngine.Cursor.visible = ShowMouseCursor;
	}


    //Closes the application
    public void QuitGame()
    {
        Application.Quit();
    }
}
