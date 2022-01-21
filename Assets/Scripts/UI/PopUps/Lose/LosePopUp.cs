using UnityEngine;
using StartMenu;

public class LosePopUp : MonoBehaviour
{
    [SerializeField] private GameObject _mainLosePopUp;
    [SerializeField] private ContinuePopUp _continuePopUp;
    [SerializeField] private EquationVisualizer _equationVisualizer;
    [SerializeField] private MoveCounter _moveCounter;
    [SerializeField] private UnitGrid _unitGrid;
    [SerializeField] private Lives _lives;

    private void Awake()
    {
        _equationVisualizer.OnLose += ShowPopUp;
        _equationVisualizer.OnEndChecking += ShowContinuePopUp;
        _unitGrid.OnEndMovingThroughGrid += ShowContinuePopUp;
    }

    public void ShowPopUp()
    {
        _mainLosePopUp.SetActive(true);
        _lives.ShowAndRemoveLife();
    }

    private void ShowContinuePopUp()
    {
        if (_moveCounter.IsRunOutMoves)
            _continuePopUp.ShowContinuePopUp(this);
    }
}
