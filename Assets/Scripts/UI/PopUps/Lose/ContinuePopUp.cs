using UnityEngine;

public class ContinuePopUp : MonoBehaviour
{
    [SerializeField] private GameObject _wallet;

    private LosePopUp _losePop;

    public void ShowContinuePopUp(LosePopUp losePopUp)
    {
        _losePop = losePopUp;
        gameObject.SetActive(true);
        _wallet.SetActive(true);
    }

    public void HideContinueBlock()
    {
        gameObject.SetActive(false);
        _wallet.SetActive(false);
    }

    public void GiveUp()
    {
        HideContinueBlock();
        _losePop.ShowPopUp();
    }
}
