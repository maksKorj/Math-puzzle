using LevelBuilder;
using UnityEngine;

public class WinPopUp : MonoBehaviour
{
    [SerializeField] private GameObject _childe;
    [SerializeField] private EquationVisualizer _equationVisualizer;
    [SerializeField] private GridAddition _gridAddition;

    private void Awake()
    {
        _equationVisualizer.OnWin += ShowPopUp;
        _gridAddition.OnWin += ShowPopUp;
    }

    private void OnDisable()
    {
        _equationVisualizer.OnWin -= ShowPopUp;
        _gridAddition.OnWin -= ShowPopUp;
    }

    private void ShowPopUp()
    {
        _childe.SetActive(true);
    }
}
