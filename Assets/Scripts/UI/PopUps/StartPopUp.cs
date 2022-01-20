using UnityEngine;
using TMPro;

public class StartPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startLevelDisplay, _gameLevelDisplay;

    [SerializeField] private LevelPropertyHandler _levelPropertyHandler;
    [SerializeField] private GridAnimation _gridAnimation;

    private void Awake()
    {
        var levelName = $"Level {PlayerSaver.LoadPlayerLevel()}";
        _startLevelDisplay.text = levelName;
        _gameLevelDisplay.text = levelName;
    }

    private void Start()
    {
        _levelPropertyHandler.VictoryCondition().ShowUI();
    }

    public void StartPlay()
    {
        gameObject.SetActive(false);

        //Block shoot before animation
        _gridAnimation.ShowStartGridElements();
    }
}
