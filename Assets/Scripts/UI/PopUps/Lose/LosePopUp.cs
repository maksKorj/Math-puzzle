using UnityEngine;
using StartMenu;
using System.Collections;

public class LosePopUp : MonoBehaviour
{
    [SerializeField] private GameObject _mainLosePopUp;
    [SerializeField] private ContinuePopUp _continuePopUp;
    [SerializeField] private EquationVisualizer _equationVisualizer;
    [SerializeField] private MoveCounter _moveCounter;
    [SerializeField] private UnitGrid _unitGrid;
    [SerializeField] private Lives _lives;
    [SerializeField] private WinPopUp _winPopUp;

    private void Awake()
    {
        _equationVisualizer.OnLose += ShowPopUp;
        _equationVisualizer.OnEndChecking += ShowContinuePopUp;
        _unitGrid.OnEndMovingThroughGrid += ShowContinuePopUp;
    }

    public void ShowPopUp()
    {
        AudioController.Instance.PlaySound(SoundItem.LOSE);
        _mainLosePopUp.SetActive(true);
        _lives.ShowAndRemoveLife();
    }

    private void ShowContinuePopUp()
    {
        if (_moveCounter.IsRunOutMoves)
            StartCoroutine(WaitAndShow());
    }

    private WaitForSeconds _delay = new WaitForSeconds(0.6f);
    private IEnumerator WaitAndShow()
    {
        yield return _delay;
        if(_winPopUp.IsWin == false)
            _continuePopUp.ShowContinuePopUp(this);
    }
}
