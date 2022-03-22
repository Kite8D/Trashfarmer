using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Trashfarmer
{
    public class InputProcessor : MonoBehaviour
    {
        // Member variables (jäsenmuuttujat) are defined here. They define the state
        // of this object. Member variables can be accessed from methods.

        // When you want to assign variable's value in Unity editor, mark the variable
        // with the SerializeField attribute.
        [SerializeField]
        private InputActionAsset inputControls;

        [SerializeField]
        private string actionMapName;

        private InputAction[] inputActions;

        // Array which contains all actions for the action map defined by the actionMapName variable.
        void Awake()
        {
            InputActionMap actionMap = inputControls.FindActionMap(actionMapName);
            inputActions = actionMap.actions.ToArray();
        }

		private void OnEnable()
		{
			// Register to listen to input events
            foreach (var action in inputActions)
			{
                action.Enable();
                action.performed += ProcessInput;
                action.canceled += ProcessInput;
			}
		}

		private void OnDisable()
		{
            foreach (var action in inputActions)
            {
                action.Disable();
                action.performed -= ProcessInput;
                action.canceled -= ProcessInput;
            }
        }


		private void ProcessInput(InputAction.CallbackContext callbackContext)
        {
            SendMessage($"On{callbackContext.action.name}", callbackContext, SendMessageOptions.DontRequireReceiver);
        }
    }
}
