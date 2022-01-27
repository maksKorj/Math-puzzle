using UnityEngine;

public class ExitPopup : MonoBehaviour
{
    [SerializeField] private PopUpAnimation _popUpAnimation;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _popUpAnimation && _popUpAnimation.IsOpen == false)
        {
            _popUpAnimation.Open();
        }
    }

    public void ClosePopUp() => _popUpAnimation.Close();
    public void Exit() => Application.Quit();
}
