using UnityEngine;

public class ContinuePopUp : MonoBehaviour
{
    [SerializeField] private PopUpAnimationAditional _popUpAnimationAditional;

    private LosePopUp _losePop;

    public void ShowContinuePopUp(LosePopUp losePopUp)
    {
        _losePop = losePopUp;
        gameObject.SetActive(true);
        _popUpAnimationAditional.Open();
    }

    public void HideContinueBlock()
    {
        _popUpAnimationAditional.Close();
    }

    public void GiveUp()
    {
        HideContinueBlock();
        _losePop.ShowPopUp();
    }
}
