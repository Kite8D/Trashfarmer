using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using InventorySystem;
using InventorySystem.Visual;
using InventorySystem.UI;

namespace Trashfarmer
{

    public class CharacterControl : MonoBehaviour
    {
        // Staattista fieldi� ei tuhota scenen unloadin my�t� (koska se ei ole olion omaisuutta, vaan luokan).
        private static Inventory Inventory;

        public enum ControlState
		{
            GamePad,
            Touch
		}

        [SerializeField]
        private float velocity = 1;

        [SerializeField]
        private float inventoryWeightLimit = 30;

        private Animator animator;
        private new SpriteRenderer renderer;
        private new Rigidbody2D rigidbody;
        private Vector2 moveInput;
        private Vector2 touchPosition;
        private Vector2 targetPosition;
        private ControlState controlState = ControlState.GamePad;
        private InventoryUI inventoryUI;
        private ItemDeposit itemDeposit;
        public GameObject gameOverText, restartButton;

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

            if (Inventory == null)
			{
                // Luodaan uusi inventario vain siin� tapauksessa, ett� sit� ei aiemmin ollut olemassa.
                Inventory = new Inventory(inventoryWeightLimit);
			}

            inventoryUI = FindObjectOfType<InventoryUI>();
	    }

		private void Start()
		{
            gameOverText.SetActive(false);
            restartButton.SetActive(false);
            inventoryUI.SetInventory(Inventory);
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

		private void OnTriggerEnter2D(Collider2D other)
		{
			ItemVisual itemVisual = other.GetComponent<ItemVisual>();
			Collect(itemVisual);

            itemDeposit = other.GetComponent<ItemDeposit>();

            if (itemDeposit != null)
            {
                Deposit(Inventory.GetItem(itemDeposit.takeItem));
            }
            if(other.CompareTag("Enemy"))
                {
                    DestroyPlayer();
                }
		}

        public void DestroyPlayer()
            {
                Destroy(gameObject);
                gameOverText.SetActive(true);
                restartButton.SetActive(true);
            }

        private bool Deposit(Item item)
		{
            if (item != null && Inventory.DepositItem(item, itemDeposit))
            {
                AudioSource audio = itemDeposit.GetComponent<AudioSource>();
                float delay = 1;

                if (audio != null)
				{
                    audio.Play();
                    delay = audio.clip.length;
				}

                // Jos ItemDeposit-skriptistä valittu Itemi on Plastic 
                if (itemDeposit.takeItem.Equals(ItemType.Plastic)) 
                {
                    inventoryUI.UpdateInventory();
                    Debug.Log("Plastic item has been deposited");
                } 
                else if (itemDeposit.takeItem.Equals(ItemType.Glass)) 
                {
                    inventoryUI.UpdateInventory();
                    Debug.Log("Glass item has been deposited");
                } 
                else if (itemDeposit.takeItem.Equals(ItemType.Organic)) 
                {
                    inventoryUI.UpdateInventory();
                    Debug.Log("Organic item has been deposited");
                } 
                else if (itemDeposit.takeItem.Equals(ItemType.Metal)) 
                {
                    inventoryUI.UpdateInventory();
                    Debug.Log("Metal item has been deposited");
                } 
                else if (itemDeposit.takeItem.Equals(ItemType.Paper)) 
                {
                    inventoryUI.UpdateInventory();
                    Debug.Log("Paper item has been deposited");
                }
                
                Debug.Log("Item has been removed from inventory");
                return true;
            }

            Debug.Log("You are trying to deposit wrong type of a item!");
            return false;
        }			
        
        private bool Collect(ItemVisual itemVisual)
		{
			if (itemVisual != null && Inventory.AddItem(itemVisual.Item))
			{
				Debug.Log("Item added to the inventory!");
				if (inventoryUI != null)
				{
					inventoryUI.UpdateInventory();
				}

                float delay = 0;
                AudioSource audio = itemVisual.GetComponent<AudioSource>();
                if (audio != null)
				{
                    audio.Play();
                    delay = audio.clip.length;
				}

                // Delay the destruction until the audio is played.
				Destroy(itemVisual.gameObject, delay);
                return true;
			}

			Debug.Log("Inventory weight limit met!");
            return false;
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
                    // Koska Vector2:sta ei voi v�hent�� Vector3:a, pit�� suorittaa tyyppimuunnos
                    Vector2 travel = targetPosition - (Vector2)transform.position;

                    // Normalisointi muuntaa vektorin pituuden yhdeksi
                    Vector2 frameMovement = travel.normalized * velocity * Time.deltaTime;

                    // Magnitude palauttaa vektorin pituuden. T�ss� vektorin pituus kuvaa
                    // j�ljell� olevaa matkaa
                    float distance = travel.magnitude;
                    
                    if (frameMovement.magnitude < distance)
					{
                        // Matkaa on viel� j�ljell�, kuljetaan kohti kohdepistett�
                        transform.Translate(frameMovement);
                        rigidbody.MovePosition(rigidbody.position + frameMovement);
                    }
					else
					{
                        // P��m��r� saavutettu
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

  //      private void OnTouchPosition(InputAction.CallbackContext contextyolo)
		//{
  //          controlState = ControlState.Touch;

  //          this.touchPosition = contextyolo.ReadValue<Vector2>();

  //          // Muunnetaan 2D koorinaatti 3D-koordinaatistoon
  //          Vector3 screenCoordinate = new Vector3(touchPosition.x, touchPosition.y, 0);

  //          // Muunnetaan n�yt�n koordinaatti pelimaailman koordinaatistoon
  //          Vector3 worldCoordinate = Camera.main.ScreenToWorldPoint(screenCoordinate);

  //          // Muunnetaan maailmankoordinaatti 2D-koordinaatistoon. HUOM! implisiittinen
  //          // tyyppimuunnos Vector3:sta -> Vector2:seen
  //          targetPosition = worldCoordinate;

  //          moveInput = (targetPosition - (Vector2)transform.position).normalized;
		//}
    }

}
