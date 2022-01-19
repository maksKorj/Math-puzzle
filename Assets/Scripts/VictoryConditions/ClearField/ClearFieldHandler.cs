using TMPro;
using UnityEngine;

public class ClearFieldHandler : MonoBehaviour
{
    [SerializeField] private GameObject _startWindowConditionDisplay;
    [SerializeField] private GameObject _conditionDisplay;

    public bool IsActiveCondition => _conditionDisplay.activeInHierarchy;

    public void ShowStartUi()
    {
        _startWindowConditionDisplay.SetActive(true);
        _conditionDisplay.SetActive(true);
    }
}
