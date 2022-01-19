using UnityEngine;

public class StartPopUp : MonoBehaviour
{
    [SerializeField] private LevelPropertyHandler _levelPropertyHandler;
    [SerializeField] private GridAnimation _gridAnimation;

    private void Start()
    {
        _levelPropertyHandler.VictoryCondition().ShowUI();
    }

    public void StartPlay()
    {
        gameObject.SetActive(false);

        //Block shoot before animation
        _gridAnimation.ShowGridElements();
    }
}
