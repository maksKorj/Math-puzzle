using UnityEngine;

namespace StartMenu.Inventory
{
    public class InventoryPopUp : MonoBehaviour
    {
        [SerializeField] private InventoryItem[] _inventoryItems;

        public void OpenPopUp()
        {
            for (int i = 0; i < BoosterSaverManager.Instance.BoosterItems.Count; i++)
                _inventoryItems[i].SetBooster(BoosterSaverManager.Instance.BoosterItems[i]);

            gameObject.SetActive(true);
        }

        public void Close() => gameObject.SetActive(false);
    }        
}

