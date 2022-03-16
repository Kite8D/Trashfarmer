using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Trashfarmer
{

    public class CharacterControl : MonoBehaviour
    {
        public enum ControlState
		{
            GamePad,
            Touch
		}

        [SerializeField]
        private float velocity = 1;
        private Animator animator;
        private new SpriteRenderer renderer;
        private new Rigidbody2D rigidbody;
        private Vector2 moveInput;
        private Vector2 touchPosition;
        private Vector2 targetPosition;
        private ControlState controlState = ControlState.GamePad;

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
                Debug.LogError("Character is missing a renderer component!");
                Debug.Break();
            }

            rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody == null)
			{
                Debug.LogError("Character is missing a RigidBody2D component!");
                Debug.Break();
            }
	    }

        // Update is called once per frame
        private void Update()
        {
            UpdateAnimator();
        }

		private void FixedUpdate()
		{
            MoveCharacter();
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
            switch (controlState)
            {
                case ControlState.GamePad:
                    Vector2 movement = moveInput * Time.fixedDeltaTime * velocity;
                    // Transform property allows us to read and manipulate GameObject's position
                    // in the game world.
                    rigidbody.MovePosition(rigidbody.position + movement);
                    break;
                case ControlState.Touch:
                    // Koska Vector2:sta ei voi vähentää Vector3:a, pitää suorittaa tyyppimuunnos
                    Vector2 travel = targetPosition - (Vector2)transform.position;

                    // Normalisointi muuntaa vektorin pituuden yhdeksi
                    Vector2 frameMovement = travel.normalized * velocity * Time.deltaTime;

                    // Magnitude palauttaa vektorin pituuden. Tässä vektorin pituus kuvaa
                    // jäljellä olevaa matkaa
                    float distance = travel.magnitude;
                    
                    if (frameMovement.magnitude < distance)
					{
                        // Matkaa on vielä jäljellä, kuljetaan kohti kohdepistettä
                        transform.Translate(frameMovement);
                        rigidbody.MovePosition(rigidbody.position + frameMovement);
                    }
					else
					{
                        // Päämäärä saavutettu
                        rigidbody.MovePosition(targetPosition);
                        moveInput = Vector2.zero;
					}

                    break;
            }
	    }

        private void OnMove(InputAction.CallbackContext callbackContext)
	    {
            controlState = ControlState.GamePad;
            moveInput = callbackContext.ReadValue<Vector2>();
	    }

        private void OnTap(InputAction.CallbackContext context)
		{
            Debug.Log("Tap!");
            Debug.Log(context.phase);
		}

        private void OnTouchPosition(InputAction.CallbackContext contextyolo)
		{
            controlState = ControlState.Touch;

            this.touchPosition = contextyolo.ReadValue<Vector2>();

            // Muunnetaan 2D koorinaatti 3D-koordinaatistoon
            Vector3 screenCoordinate = new Vector3(touchPosition.x, touchPosition.y, 0);

            // Muunnetaan näytön koordinaatti pelimaailman koordinaatistoon
            Vector3 worldCoordinate = Camera.main.ScreenToWorldPoint(screenCoordinate);

            // Muunnetaan maailmankoordinaatti 2D-koordinaatistoon. HUOM! implisiittinen
            // tyyppimuunnos Vector3:sta -> Vector2:seen
            targetPosition = worldCoordinate;

            moveInput = (targetPosition - (Vector2)transform.position).normalized;
		}
    }

}
