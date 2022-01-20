using LevelBuilder;
using UnityEngine;

public class WinPopUp : MonoBehaviour
{
    [SerializeField] private GameObject _childe;
    [SerializeField] private EquationVisualizer _equationVisualizer;
    [SerializeField] private GridAddition _gridAddition;
    [SerializeField] private CoinGiver _coinGiver;
    [SerializeField] private PresentGiver _presentGiver;

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
        int currentLevel = PlayerSaver.LoadPlayerLevel();
        PlayerSaver.SavePlayerLevel(currentLevel + 1);

        _coinGiver.UpdateAmount();
        _childe.SetActive(true);

        _presentGiver.LevelCompleted();
    }
}
