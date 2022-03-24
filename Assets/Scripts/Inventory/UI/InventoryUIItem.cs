using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace InventorySystem.UI
{
    public class InventoryUIItem : MonoBehaviour
    {
        private Image image;
        private TMP_Text countText;
        private InventoryUI ui;

        public Item Item
		{
            // Julkinen get m��ritt��, ett� propertyn arvon voi lukea mist� vain.
            get;
            // Privaatti set m��ritt��, ett� propertyn arvoa ei voi vasettaa olion ulkopuolelta.
            private set;
		}

        public ItemType Type
		{
            get
			{
                if (Item != null)
				{
                    return Item.Type;
				}

                return ItemType.None;
			}
		}

		public void Setup(InventoryUI inventoryUI)
		{
            ui = inventoryUI;
            countText.gameObject.SetActive(false); // Piilota count tyhjist� itemeist�.
		}

		private void Awake()
		{
            image = GetComponent<Image>();
            countText = GetComponentInChildren<TMP_Text>();
		}

		public void SetItem(Item item)
		{
            Item = item;
            image.sprite = ui.GetImage(item.Type);
            countText.text = item.Count.ToString();
            countText.gameObject.SetActive(true); // Teksti voi olla piilotettu. Aktivoidaan se.
		}
    }
}