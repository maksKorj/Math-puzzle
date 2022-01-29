using UnityEngine;
using TMPro;

public class InventoryLabelAnimation : LabelNotAvailableAnimation
{
    [SerializeField] private TextMeshProUGUI _text;

    private bool _isUpdate;

    public void UpdateLevel(int level)
    {
        if (_isUpdate)
            return;

        _text.text = $"This booster will unlock at {level} level";
        _isUpdate = true;
    }
}
