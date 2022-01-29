using UnityEngine;
using TMPro;

public class StartPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startLevelDisplay, _gameLevelDisplay;
    [SerializeField] private PopUpAnimation _popUpAnimation;
    [SerializeField] private LevelPropertyHandler _levelPropertyHandler;
    [SerializeField] private GridAnimation _gridAnimation;
    [SerializeField] private GuideHandler _guideHandler;

    private void Awake()
    {
        var levelName = $"Level {PlayerSaver.LoadPlayerLevel()}";
        _startLevelDisplay.text = levelName;
        _gameLevelDisplay.text = levelName;

        if (PlayerSaver.LoadPlayerLevel() == 1)
            Instantiate(_guideHandler, transform.parent);
    }

    private void Start()
    {
        _levelPropertyHandler.VictoryCondition().ShowUI();
    }

    public void StartPlay()
    {
        _popUpAnimation.Close();

        _gridAnimation.ShowStartGridElements();
    }
}
