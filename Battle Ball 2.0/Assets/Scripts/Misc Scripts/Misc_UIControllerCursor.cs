using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
public class Misc_UIControllerCursor : MonoBehaviour
{
    //How fast this UI cursor moves across the screen per frame
    public float MovementSpeed = 6f;

    //The player who controls this cursor's actions
    public Players PlayerID = Players.None;
    private Manager_ControllerInput PlayerInput;

    //The buttons that let this cursor interact with UI elements
    public ControllerButtons ConfirmButton = ControllerButtons.A_Button;
    public ControllerButtons BackButton = ControllerButtons.B_Button;

    //Determines if this cursor moves based on input from the right stick (true) or the left stick (false)
    public bool RightStickMovement = true;

    //The camera that this cursor's canvas is using to render
    public Camera CanvasCamera;

    //Private reference to this owner's Image component
    private Image OwnerImage;
    private RectTransform OwnerTransform;
    private RectTransform CanvasTransform;


	// Use this for initialization
	void Start ()
    {
        OwnerImage = GetComponent<Image>();
        OwnerTransform = GetComponent<RectTransform>();

        if (GetComponentInParent<Canvas>() != null)
        {
            CanvasTransform = GetComponentInParent<Canvas>().transform as RectTransform;
        }

	    switch(PlayerID)
        {
            case Players.P1:
                PlayerInput = Manager_ControllerInputManager.P1Controller;
                break;
            case Players.P2:
                PlayerInput = Manager_ControllerInputManager.P2Controller;
                break;
            case Players.P3:
                PlayerInput = Manager_ControllerInputManager.P3Controller;
                break;
            case Players.P4:
                PlayerInput = Manager_ControllerInputManager.P4Controller;
                break;
            default:
                PlayerInput = Manager_ControllerInputManager.P1Controller;
                break;
        }
	}
	

	// Update is called once per frame
	void Update ()
    {
        //When the Confirm Button is pressed, raycasts to the canvas below it and triggers whatever UI element it hits
        if (PlayerInput.CheckButtonPressed(ConfirmButton))
        {
            PointerEventData cursorData = new PointerEventData(EventSystem.current);

            cursorData.position = OwnerTransform.position;
            List<RaycastResult> objHit = new List<RaycastResult>();

            EventSystem.current.RaycastAll(cursorData, objHit);

            if(objHit.Count >= 1 && objHit[0].gameObject.GetComponent<Misc_ButtonMultiplayerLogic>() != null)
            {
                objHit[0].gameObject.GetComponent<Misc_ButtonMultiplayerLogic>().PressThisButton(PlayerID);
            }
        }


        //If this input moves accross the canvas using the right stick
        if(RightStickMovement)
        {
            float newX = OwnerTransform.anchoredPosition.x + (PlayerInput.RightStick.x * MovementSpeed);
            float newY = OwnerTransform.anchoredPosition.y + (-PlayerInput.RightStick.y * MovementSpeed);

            OwnerTransform.anchoredPosition = new Vector2(newX, newY);
        }
        //If this input moves accross the canvas using the left stick
        else
        {
            float newX = OwnerTransform.anchoredPosition.x + (PlayerInput.LeftStick.x * MovementSpeed);
            float newY = OwnerTransform.anchoredPosition.y + (-PlayerInput.LeftStick.y * MovementSpeed);

            OwnerTransform.anchoredPosition = new Vector2(newX, newY);
        }

        //Makes sure that this cursor can't go outside the canvas' edges on the left and right
        if(OwnerTransform.anchoredPosition.x < -(( 1 - CanvasTransform.pivot.x) * CanvasTransform.rect.width))
        {
            OwnerTransform.anchoredPosition = new Vector2(-((1 - CanvasTransform.pivot.x) * CanvasTransform.rect.width), OwnerTransform.anchoredPosition.y);
        }
        else if(OwnerTransform.anchoredPosition.x > (CanvasTransform.pivot.x * CanvasTransform.rect.width))
        {
            OwnerTransform.anchoredPosition = new Vector2((CanvasTransform.pivot.x * CanvasTransform.rect.width), OwnerTransform.anchoredPosition.y);
        }

        //Makes sure that this cursor can't go outside the canvas' edges on the top and bottom
        if (OwnerTransform.anchoredPosition.y < -((1 - CanvasTransform.pivot.y) * CanvasTransform.rect.height))
        {
            OwnerTransform.anchoredPosition = new Vector2(OwnerTransform.anchoredPosition.x, -((1 - CanvasTransform.pivot.y) * CanvasTransform.rect.height));
        }
        else if (OwnerTransform.anchoredPosition.y > (CanvasTransform.pivot.y * CanvasTransform.rect.height))
        {
            OwnerTransform.anchoredPosition = new Vector2(OwnerTransform.anchoredPosition.x, (CanvasTransform.pivot.y * CanvasTransform.rect.height));
        }
    }
}
