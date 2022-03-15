using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public const string HorizontalAxisName = "Horizontal";
    public const string VerticalAxisName = "Verical";

    [SerializeField]
    private float speed = 1;

    private Vector2 moveInput;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = this.moveInput * Time.deltaTime * speed;
        transform.Translate(movement);
    }

	private void OnMove(InputAction.CallbackContext callbackContext)
	{
        this.moveInput = callbackContext.ReadValue<Vector2>();
	}
}
