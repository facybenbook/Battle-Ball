using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager_ScreenSettings : MonoBehaviour
{
    //A reference to this manager that can be accessed anywhere
    public static Manager_ScreenSettings ScreenManager;

    [HideInInspector]
    public int ScreenResDropdownNum = 0;
    [HideInInspector]
    public ScreenResolution ScreenResDropdownEnum = ScreenResolution.r1920x1200;

    [HideInInspector]
    public float Darkness = 0f;


    void Awake()
    {
        //If there isn't already a static reference to this manager, this instance becomes the static reference
        if(ScreenManager == null)
        {
            ScreenManager = GetComponent<Manager_ScreenSettings>();
        }
    }


	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    //Turns on/off full screen mode
    public void ToggleFullscreen(bool isFullscreenOn)
    {
        Screen.fullScreen = isFullscreenOn;
    }



    //Changes the screen resolution based on the enum given
    public void ToggleScreenResolution(ScreenResolution screenRes_)
    {
        ScreenManager.ScreenResDropdownEnum = screenRes_;

        //Sets the screen resolution based on the enum given
        switch (screenRes_)
        {
            case ScreenResolution.r1024x768:
                Screen.SetResolution(1024, 768, Screen.fullScreen);
                break;

            case ScreenResolution.r1280x800:
                Screen.SetResolution(1280, 800, Screen.fullScreen);
                break;

            case ScreenResolution.r1280x1024:
                Screen.SetResolution(1280, 1024, Screen.fullScreen);
                break;

            case ScreenResolution.r1366x768:
                Screen.SetResolution(1366, 768, Screen.fullScreen);
                break;

            case ScreenResolution.r1440x900:
                Screen.SetResolution(1440, 900, Screen.fullScreen);
                break;

            case ScreenResolution.r1600x900:
                Screen.SetResolution(1600, 900, Screen.fullScreen);
                break;

            case ScreenResolution.r1680x1050:
                Screen.SetResolution(1680, 1050, Screen.fullScreen);
                break;

            case ScreenResolution.r1920x1080:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;

            case ScreenResolution.r1920x1200:
                Screen.SetResolution(1920, 1200, Screen.fullScreen);
                break;
        }
    }



    //Changes the screen resolution based on the enum given
    public void ToggleScreenResolutionDropdown(int screenRes_)
    {
        ScreenManager.ScreenResDropdownNum = screenRes_;

        //Sets the screen resolution based on the enum given
        switch (screenRes_)
        {
            case 8:
                Screen.SetResolution(1024, 768, Screen.fullScreen);
                break;

            case 7:
                Screen.SetResolution(1280, 800, Screen.fullScreen);
                break;

            case 6:
                Screen.SetResolution(1280, 1024, Screen.fullScreen);
                break;

            case 5:
                Screen.SetResolution(1366, 768, Screen.fullScreen);
                break;

            case 4:
                Screen.SetResolution(1440, 900, Screen.fullScreen);
                break;

            case 3:
                Screen.SetResolution(1600, 900, Screen.fullScreen);
                break;

            case 2:
                Screen.SetResolution(1680, 1050, Screen.fullScreen);
                break;

            case 1:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;

            case 0:
                Screen.SetResolution(1920, 1200, Screen.fullScreen);
                break;
        }
    }



    //Changes the alpha of the screen darkness object on the global data object's canvas
    public void SetDarkness(float screenDarkness_)
    {
        ScreenManager.Darkness = 0.8f - screenDarkness_;

        if (ScreenManager.Darkness > 1)
        {
            ScreenManager.Darkness = 1;
        }
        else if (ScreenManager.Darkness < 0)
        {
            ScreenManager.Darkness = 0;
        }

        Manager_EventManager.TriggerEvent("ChangeDarkness");
    }
}
