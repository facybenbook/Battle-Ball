using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Misc_SplashScreenLogic : MonoBehaviour
{
    //How long it takes for this logo to fade in from invisible
    public float FadeInTime = 0.2f;
    //How long this logo is on screen between fading in and fading out
    public float OnScreenTime = 2.0f;
    //How long it takes for this logo to fade out
    public float FadeOutTime = 0.2f;
    //What happens when this logo is finished fading out
    public UnityEvent EventOnFinish;

    private Misc_Interpolator MyInterp;
    private CanvasRenderer ThisCanvas;
    private bool IsSkipping = false;



	// Use this for initialization
	void Start ()
    {
        MyInterp = new Misc_Interpolator();
        MyInterp.Ease = EaseType.SineIn;
        MyInterp.SetDuration(FadeInTime);

        ThisCanvas = GetComponent<CanvasRenderer>();
        ThisCanvas.SetAlpha(0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) ||
            Manager_ControllerInputManager.P1Controller.A_Button_Pressed || Manager_ControllerInputManager.P1Controller.Start_Button_Pressed ||
            Manager_ControllerInputManager.P2Controller.A_Button_Pressed || Manager_ControllerInputManager.P2Controller.Start_Button_Pressed ||
            Manager_ControllerInputManager.P3Controller.A_Button_Pressed || Manager_ControllerInputManager.P3Controller.Start_Button_Pressed ||
            Manager_ControllerInputManager.P4Controller.A_Button_Pressed || Manager_ControllerInputManager.P4Controller.Start_Button_Pressed)
        {
            IsSkipping = true;
        }


        //If there's still time left to fade in, fades in
	    if(FadeInTime > 0 && IsSkipping == false)
        {
            FadeInTime -= Time.deltaTime;
            MyInterp.AddTime(Time.deltaTime);

            //Changes the alpha of this image to fade in based on the amount of time passed
            ThisCanvas.SetAlpha(MyInterp.GetProgress());
        }
        //If fading in is finished, stays on screen
        else if(OnScreenTime > 0 && IsSkipping == false)
        {
            OnScreenTime -= Time.deltaTime;

            //Once it's finished staying on screen regularly, sets the interpolater to the fade out time
            if(OnScreenTime <= 0)
            {
                MyInterp.SetDuration(FadeOutTime);
                MyInterp.AddTime(FadeOutTime);
            }
        }
        //If it's done staying on screen normally, fades out
        else if(FadeOutTime > 0)
        {
            FadeOutTime -= Time.deltaTime;
            MyInterp.AddTime(-Time.deltaTime);

            //Changes the alpha of this image to fade out based on the amount of time passed
            ThisCanvas.SetAlpha(MyInterp.GetProgress());

            //Once it's finished fading out, activates the on finish event
            if(FadeOutTime <= 0)
            {
                EventOnFinish.Invoke();
            }
        }
	}
}
