using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Misc_LoadLevel : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    //Loads the level with the given name
    public void LoadLevelByName(string levelName_)
    {
        //loads in the scene
        SceneManager.LoadScene(levelName_);
    }


    //Loads the level with the given scene index
    public void LoadLevelByIndex(int levelIndex_)
    {
        SceneManager.LoadScene(levelIndex_);
    }
}
