using UnityEngine;

[CreateAssetMenu(menuName = "VictoryCondition/ClearField")]
public class ClearField : VictoryCondition
{
    public override void ShowUI()
        => FindObjectOfType<ClearFieldHandler>().ShowStartUi();
}
