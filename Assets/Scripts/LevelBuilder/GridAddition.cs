using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelBuilder
{
    public class GridAddition : MonoBehaviour
    {
        [SerializeField] private Symbols _symbols;
        [SerializeField] private EquationBuilder _equationBuilder;
        [SerializeField] private EquationVisualizer _equationVisualizer;
        [SerializeField] private ClearFieldHandler _clearFieldHandler;
        
        private GridAnimation _gridAnimation;
        private GridBuilder _gridBuilder;

        public event Action OnWin;

        private void Awake()
        {
            _equationVisualizer.OnEmptyGrid += AddElementsToGrid;
            _gridBuilder = GetComponent<GridBuilder>();
            _gridAnimation = GetComponent<GridAnimation>();
        }

        private void OnDisable()
            => _equationVisualizer.OnEmptyGrid -= AddElementsToGrid;

        private void AddElementsToGrid()
        {
            AddElementsToGrid(false);
        }

        public void AddElementsToGrid(bool isStartAdding)
        {
            if (isStartAdding)
            {
                _equationBuilder.AddBrokenEquations();
                return;
            }

            if(_clearFieldHandler.IsActiveCondition)
            {
                OnWin?.Invoke();
                return;
            }

            if (Random.Range(0, 101) < 40)
                AddElements();
            else
                _equationBuilder.AddBrokenEquations();

            _gridAnimation.ShowGridElements();
        }

        private void AddElements()
        {
            AddElement(_symbols.Numbers, 3);
            AddElement(_symbols.MathSigns, 1);
            AddElement(_symbols.ComparisonSigns, 1);
        }

        private void AddElement(GridContent[] gridContents, int amount)
        {
            while(amount != 0)
            {
                int x = Random.Range(1, _gridBuilder.SizeX - 1);
                int y = Random.Range(1, _gridBuilder.SizeY - 1);

                if(_gridBuilder.GridElement(x, y).IsTaken == false)
                {
                    _gridBuilder.GridElement(x, y).SetContent(gridContents[Random.Range(0, gridContents.Length)]);
                    amount--;
                }
            }
        }
    }
}

