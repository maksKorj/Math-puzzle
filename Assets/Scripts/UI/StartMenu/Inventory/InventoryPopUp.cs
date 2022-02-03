using UnityEngine;

namespace StartMenu.Inventory
{
    public class InventoryPopUp : MonoBehaviour
    {
        [SerializeField] private InventoryItem[] _inventoryItems;
        [SerializeField] private PopUpAnimation _popUpAnimation;

        public void OpenPopUp()
        {
            for (int i = 0; i < BoosterSaverManager.Instance.BoosterItems.Count; i++)
                _inventoryItems[i].SetBooster(BoosterSaverManager.Instance.BoosterItems[i]);

            _popUpAnimation.Open();
            BackButton.Instance.AddBackButtonAction(_popUpAnimation.Close);
        }

        public void Close()
        {
            _popUpAnimation.Close();
            BackButton.Instance.RemoveLastAction();
        }
    }        
}

