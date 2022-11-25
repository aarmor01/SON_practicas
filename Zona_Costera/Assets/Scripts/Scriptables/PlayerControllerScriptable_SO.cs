using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputHolder", menuName = "StarterAssets/InputHolder")]
public class PlayerControllerScriptable_SO : ScriptableObject, GameplayInput.IPlayerActions
{
    //Gameplay
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction<Vector2> lookEvent = delegate { };
    public event UnityAction<bool> jumpEvent = delegate { };
    public event UnityAction<bool> sprintEvent = delegate { };

    private GameplayInput gameInput;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameplayInput();
            gameInput.Player.SetCallbacks(this);
        }
        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void EnableGameplayInput()
    {
        gameInput.Player.Enable();
    }

    public void DisableAllInput()
    {
        gameInput.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (jumpEvent != null && context.phase == InputActionPhase.Performed) jumpEvent?.Invoke(true);
        if (jumpEvent != null && context.phase == InputActionPhase.Canceled) jumpEvent?.Invoke(false);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (sprintEvent != null && context.phase == InputActionPhase.Performed) sprintEvent?.Invoke(true);
        if (sprintEvent != null && context.phase == InputActionPhase.Canceled) sprintEvent?.Invoke(false);
    }
}
