using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Visual
{
    public class ItemVisual : MonoBehaviour
    {
        [SerializeField]
        private Item item;

        public Item Item { get { return item; } }
    }
}
