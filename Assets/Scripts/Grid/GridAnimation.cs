using UnityEngine;
using LevelBuilder;
using System;
using Boosters;
using System.Collections;

public class GridAnimation : MonoBehaviour
{
    [SerializeField] private UnitGrid _unitGrid;
    [SerializeField] private BoosterManager _boosterManager;

    private GridBuilder _gridBuilder;
    public event Action OnEndStartAnimation;

    private void Awake()
        => _gridBuilder = GetComponent<GridBuilder>();

    public void ShowStartGridElements()
    {
        _unitGrid.BlockShoting();
        _boosterManager.SetButtonInteractable(false);

        StartCoroutine(ShowElement());
    }

    private IEnumerator ShowElement()
    {
        float time = _gridBuilder.GridElement(1, 1).ShowingTime;
        var _delay = new WaitForSeconds(time / 4);

        for (int x = 0; x < _gridBuilder.SizeX; x++)
        {
            for (int y = 0; y < _gridBuilder.SizeY; y++)
            {
                if (_gridBuilder.GridElement(x, y) == null)
                    continue;

                if(_gridBuilder.GridElement(x, y).IsTaken)
                {
                    _gridBuilder.GridElement(x, y).ScaleToFullSizeWithSound();

                    yield return _delay;
                }
            }
        }

        yield return new WaitForSeconds(0.25f);

        OnEndStartAnimation?.Invoke();

        _unitGrid.UnBlockShoting();
        _boosterManager.SetButtonInteractable(true);
    }

    public void ShowGridElements()
        => MakeAction((int x, int y) => _gridBuilder.GridElement(x, y).ScaleToFullSize());

    public void SelectAll() 
        => MakeAction((int x, int y) => _gridBuilder.GridElement(x, y).JumpAnimation());

    public void UnSelectAll()
        => MakeAction((int x, int y) => _gridBuilder.GridElement(x, y).StopJumpAnimation());

    private void MakeAction(Action<int, int> action)
    {
        for (int x = 0; x < _gridBuilder.SizeX; x++)
        {
            for (int y = 0; y < _gridBuilder.SizeY; y++)
            {
                if (_gridBuilder.GridElement(x, y) == null)
                    continue;

                action(x, y);
            }
        }
    }
}
