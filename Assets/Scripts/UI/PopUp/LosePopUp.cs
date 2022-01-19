using UnityEngine;

public class LosePopUp : MonoBehaviour
{
    [SerializeField] private GameObject _childe;
    [SerializeField] private GameObject _continueBlock;
    [SerializeField] private EquationVisualizer _equationVisualizer;
    [SerializeField] private MoveCounter _moveCounter;

    private void Awake()
    {
        _equationVisualizer.OnLose += ShowPopUp;
        _equationVisualizer.OnEndChecking += ShowContinuePopUp;
    }

    private void ShowPopUp()
    {
        _childe.SetActive(true);
    }

    private void ShowContinuePopUp()
    {
        if (_moveCounter.IsRunOutMoves)
            _continueBlock.SetActive(true);
    }
}
