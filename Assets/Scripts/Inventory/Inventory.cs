using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	// Enum tyypitt‰‰ inventorioon tallennettavat esineet.
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
        public List<Item> Items { get; }

        public float WeightLimit { get; }

        // Inventoryn t‰m‰nhetkinen paino, lasketaan uudestaan joka kerta kysytt‰ess‰.
        public float Weight
		{
            get
			{
                float weight = 0;
                foreach (Item item  in Items)
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

        // Lis‰‰ uuden itemin inventarioon. Palauttaa true, jos lis‰tys onnistui.
        // Paluuarvo on false, jos inventarion painoraja ylittyisi lis‰yksen myˆt‰.
        public bool AddItem(Item item)
		{
            if (Weight + item.TotalWeight > WeightLimit)
			{
                // Inventaarion painoraja ylittyy. Uutta itemi‰ ei voida lis‰t‰.
                return false;
			}

            // Selvitet‰‰n, onko inventariossa jo uutta itemi‰ vastaava item (tyypit vastaavat toisiaan).
            Item existing = null;
            foreach (Item current in Items)
			{
                if (current.Type == item.Type)
				{
                    existing = current;
                    break; // Haettu item  lˆytyi, keskeytet‰‰n loop.
				}
			}

            if (existing != null && existing.CanStack)
			{
                // Uusi item voidaan tallentaa olemassa olevan kanssa samaan slottiin.
                existing.Count += item.Count;
			}
			else
			{
                // Lis‰t‰‰n uusi item inventarioon.
                Items.Add(item);
			}

            return true;
		}
    }
}
