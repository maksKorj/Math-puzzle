using UnityEngine;

public class MainShop : MonoBehaviour
{
    [SerializeField] private GameObject _childe;
    [SerializeField] private GameObject _wallet;

    public void OpenShop()
    {
        if(_childe.activeInHierarchy == false)
            SetActive(true);
    }
    public void CloseShop() => SetActive(false);

    private void SetActive(bool isActive)
    {
        _childe.SetActive(isActive);
        _wallet.SetActive(isActive);
    }
}
