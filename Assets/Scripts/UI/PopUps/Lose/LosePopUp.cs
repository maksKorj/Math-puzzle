using UnityEngine;

public class LosePopUp : MonoBehaviour
{
    [SerializeField] private GameObject _mainLosePopUp;
    [SerializeField] private ContinuePopUp _continuePopUp;
    [SerializeField] private EquationVisualizer _equationVisualizer;
    [SerializeField] private MoveCounter _moveCounter;

    private void Awake()
    {
        _equationVisualizer.OnLose += ShowPopUp;
        _equationVisualizer.OnEndChecking += ShowContinuePopUp;
    }

    public void ShowPopUp()
    {
        _mainLosePopUp.SetActive(true);
    }

    private void ShowContinuePopUp()
    {
        if (_moveCounter.IsRunOutMoves)
            _continuePopUp.ShowContinuePopUp(this);
    }
}
