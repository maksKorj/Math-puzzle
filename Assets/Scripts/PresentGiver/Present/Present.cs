using UnityEngine;

[CreateAssetMenu(menuName = "Present")]
public class Present : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _minAmount, _maxAmount;

    public Sprite Sprite => _sprite;
    public int Amount => Random.Range(_minAmount, _maxAmount + 1);
}
