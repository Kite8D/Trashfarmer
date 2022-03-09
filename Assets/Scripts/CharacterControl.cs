using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Trashfarmer
{

public class CharacterControl : MonoBehaviour
{
    [SerializeField]
    private float velocity = 1;
    private Animator animator;
    private new SpriteRenderer renderer;
    private Vector2 moveInput;

	private void Awake()
	{
        animator = GetComponent<Animator>();
        if (animator == null)
		{
            Debug.LogError("Character is missing an animator component!");
            Debug.Break();
		}

        renderer = GetComponent<SpriteRenderer>();
        if (renderer == null)
		{
            Debug.LogError("Character is missing an renderer component!");
            Debug.Break();
        }
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        UpdateAnimator();
    }

    private void UpdateAnimator()
	{
        renderer.flipX = moveInput.x < 0;

        animator.SetFloat("speed", moveInput.magnitude);
        animator.SetFloat("horizontal", moveInput.x);
        animator.SetFloat("vertical", moveInput.y);
    }

    private void MoveCharacter()
	{
        Vector2 movement = moveInput * Time.deltaTime * velocity;
        transform.Translate(movement);
	}

    private void OnMove(InputAction.CallbackContext callbackContext)
	{
        moveInput = callbackContext.ReadValue<Vector2>();
	}
}

}
