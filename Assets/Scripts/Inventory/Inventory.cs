using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	// Enum tyypitt�� inventorioon tallennettavat esineet.
	public enum ItemType
	{
        None = 0,
        Plastic,
        Glass,
        Organic,
        Metal,
        Paper

	}

    public class Inventory
    {
        public List<Item> Items { get; set; }

        public float WeightLimit { get; set; }
        private float weight;
        public int score;

        // Inventoryn t�m�nhetkinen paino, lasketaan uudestaan joka kerta kysytt�ess�.
        public float Weight
		{
            get
			{
                weight = 0;
                foreach (Item item in Items)
				{
                    weight += item.TotalWeight;
				}
                return weight;
			}
		}

        public Inventory(float weightLimit)
		{
            WeightLimit = weightLimit;
            Items = new List<Item>();
		}

        public Item GetItem(ItemType Type) 
        {
            foreach (Item item in Items)
            {
                if (item.Type == Type)
                {
                    return item;
                }
            }
            return null;
        }

        // Lis�� uuden itemin inventarioon. Palauttaa true, jos lis�tys onnistui.
        // Paluuarvo on false, jos inventarion painoraja ylittyisi lis�yksen my�t�.
        public bool AddItem(Item item)
		{
            if (Weight + item.TotalWeight > WeightLimit)
			{
                // Inventaarion painoraja ylittyy. Uutta itemi� ei voida lis�t�.
                return false;
			}

            // Selvitet��n, onko inventariossa jo uutta itemi� vastaava item (tyypit vastaavat toisiaan).
            Item existing = null;
            foreach (Item current in Items)
			{
                if (current.Type == item.Type)
				{
                    existing = current;
                    break; // Haettu item  l�ytyi, keskeytet��n loop.
				}
			}

            if (existing != null && existing.CanStack)
			{
                // Uusi item voidaan tallentaa olemassa olevan kanssa samaan slottiin.
                existing.Count += item.Count;
			}
			else
			{
                // Lis�t��n uusi item inventarioon.
                Items.Add(item);
			}

            return true;
		}


        public bool DepositItem(Item item, ItemDeposit itemDeposit)
		{
            // Selvitetään, onko inventaariossa tämän tyyppistä tavaraa.
            Item existing = null;
            foreach (Item current in Items)
			{
                // Jos tämä item on sama kuin ItemDeposit-skriptissä valittu item
                if (current.Type == itemDeposit.takeItem)
				{
                    existing = current;
                    break; // Haettu item  l�ytyi, keskeytet��n loop.
				}
                else 
                {
                    // Itemiä ei ollut.
                    Debug.Log("XAXAXAXAXAAXAXAXAXAXAXAX");
                    return false;
                }
			}

            if (existing != null && existing.CanStack)
			{
                for (int i = 0; i < existing.Count; i++) {
                    score += item.Count;
                    existing.Count -= item.Count;
                    Debug.Log("You got " + score + " points");
                }
                // Items.Remove(item);
			}

            return true;
		}

        public Inventory resetInventory(float weightLimit)
		{
            WeightLimit = weightLimit;
            Items = new List<Item>();
            return null;
        }
    }
}
