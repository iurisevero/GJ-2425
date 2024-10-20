using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static string OnEscClickEvent = "InputHandler.OnEscClick";

    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool fire;
    public bool fireRight;
    public bool actionInput;
    public bool reloadInput;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if(cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnFire(InputValue value)
    {
        FireInput(value.isPressed);
    }

    public void OnFireRight(InputValue value)
    {
        FireRightInput(value.isPressed);
    }

    public void OnAction(InputValue value)
    {
        ActionInput(value.isPressed);
    }

    public void OnReload(InputValue value)
    {
        ReloadInput(value.isPressed);
    }

    public void OnEsc(InputValue value)
    {
        this.PostNotification(OnEscClickEvent);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        if(Time.timeScale == 0) return;
        move = newMoveDirection;
    } 

    public void LookInput(Vector2 newLookDirection)
    {
        if(Time.timeScale == 0) return;
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        if(Time.timeScale == 0) return;
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        if(Time.timeScale == 0) return;
        sprint = newSprintState;
    }

    public void FireInput(bool newFireState)
    {
        if(Time.timeScale == 0) return;
        fire = newFireState;
    }

    public void FireRightInput(bool newFireState)
    {
        if(Time.timeScale == 0) return;
        fireRight = newFireState;
    }

    public void ActionInput(bool newActionInputState)
    {
        if(Time.timeScale == 0) return;
        actionInput = newActionInputState;
    }

    public void ReloadInput(bool newReloadState)
    {
        if(Time.timeScale == 0) return;
        reloadInput = newReloadState;
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
