using System;
using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool tired;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("Cooldowns")]
		[SerializeField] float sprintDuration;
		[SerializeField] float sprintCooldown;
		bool isSprintCooldown = false;
		float sprintTimer = 0;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
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
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = !isSprintCooldown && newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		private void Update()
		{
			sprintTimer += sprint ? Time.deltaTime : Time.deltaTime * -0.5f;
			sprintTimer = Mathf.Max(sprintTimer, 0);

			if (sprintTimer > sprintDuration && !isSprintCooldown)
				StartCoroutine(SprintCooldown());
		}

		IEnumerator SprintCooldown()
		{
			isSprintCooldown = true;
			sprint = false;
			tired = true;
			GetComponent<FMODUnity.StudioEventEmitter>().Play();
			yield return new WaitForSeconds(sprintCooldown);
			tired = false;
			isSprintCooldown = false;
			sprintTimer = 0;
		}
	}
	
}