using LevelBuilder;
using System.Collections;
using UnityEngine;

public class WinPopUp : MonoBehaviour
{
    [SerializeField] private PopUpAnimation _childe;
    [SerializeField] private EquationVisualizer _equationVisualizer;
    [SerializeField] private GridAddition _gridAddition;
    [SerializeField] private CoinGiver _coinGiver;
    [SerializeField] private PresentGiver _presentGiver;

    public bool IsWin { get; private set; }

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
        IsWin = true; 
        StartCoroutine(WaitAndShow());
    }

    private IEnumerator WaitAndShow()
    {
        yield return new WaitForSeconds(0.5f);

        int currentLevel = PlayerSaver.LoadPlayerLevel();
        PlayerSaver.SavePlayerLevel(currentLevel + 1);

        _coinGiver.UpdateAmount();
        _childe.Open(GivePresentAndPlaySound);
    }

    private void GivePresentAndPlaySound()
    {
        _presentGiver.LevelCompleted();
        AudioController.Instance.PlaySound(SoundItem.WIN);
    }
}
