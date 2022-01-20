using UnityEngine;

namespace StartMenu
{
    public class InventoryShopPopUp : MonoBehaviour
    {
        [SerializeField] private InventoryShopButton[] _inventoryShopButtons;
        [SerializeField] private InventoryFreeShopButton _inventoryFreeShopButton;

        private void Awake()
        {
            for (int i = 0; i < _inventoryShopButtons.Length; i++)
                _inventoryShopButtons[i].Initialize();

            gameObject.SetActive(false);
        }

        public void OpenShop(InventoryItem inventory)
        {
            for (int i = 0; i < _inventoryShopButtons.Length; i++)
                _inventoryShopButtons[i].UpdateButton(inventory);

            _inventoryFreeShopButton.UpdeteUi(inventory);

            gameObject.SetActive(true);
        }

        public void Close() => gameObject.SetActive(false);
    }
}

