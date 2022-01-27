using UnityEngine;

namespace StartMenu
{
    public class LifeShopPopUp : MonoBehaviour
    {
        [SerializeField] private PopUpAnimation _childe;

        public void Open() => _childe.Open();
        public void Close() => _childe.Close();
    }
}


