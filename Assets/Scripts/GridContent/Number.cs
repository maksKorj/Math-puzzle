using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/Number")]
public class Number : GridContent
{
    [SerializeField] private int _number;

    public int Num => _number;
}
