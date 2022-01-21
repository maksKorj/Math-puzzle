using UnityEngine;

public class ContinuePopUp : MonoBehaviour
{
    [SerializeField] private MainWallet _wallet;

    private LosePopUp _losePop;

    public void ShowContinuePopUp(LosePopUp losePopUp)
    {
        _losePop = losePopUp;
        gameObject.SetActive(true);
        _wallet.ShowWallet();
    }

    public void HideContinueBlock()
    {
        gameObject.SetActive(false);
        _wallet.HideWallet();
    }

    public void GiveUp()
    {
        HideContinueBlock();
        _losePop.ShowPopUp();
    }
}
