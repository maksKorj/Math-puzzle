using UnityEngine;

namespace StartMenu
{
    public class LifeShopPopUp : MonoBehaviour
    {
        [SerializeField] private GameObject _childe;

        public void Open() => _childe.SetActive(true);
        public void Close() => _childe.SetActive(false);
    }
}


