using UnityEngine;
using LevelBuilder;

public class UnitGridAmountChecker : MonoBehaviour
{
    private int _maxAmount;
    private int _totalAmount;
    private GridBuilder _gridBuilder;

    public bool IsFull => _totalAmount >= _maxAmount - 4;
    public bool IsEmpty => _totalAmount <= 0;

    private void Awake()
        => _gridBuilder = GetComponent<GridBuilder>();

    private void Start()
        => _maxAmount = _gridBuilder.SizeX * _gridBuilder.SizeY;

    public void CountAmount()
        => _totalAmount = GetAmount();

    private int GetAmount()
    {
        int count = 0;

        for(int x = 0; x < _gridBuilder.SizeX; x++)
        {
            for (int y = 0; y < _gridBuilder.SizeY; y++)
            {
                if (IsTaken(x, y))
                    count++;
            }
        }

        return count;
    }

    private bool IsTaken(int x, int y) => _gridBuilder.GridElement(x, y) != null 
        && _gridBuilder.GridElement(x, y).IsTaken;
}
