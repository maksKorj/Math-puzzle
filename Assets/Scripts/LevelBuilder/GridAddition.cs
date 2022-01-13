using UnityEngine;

namespace LevelBuilder
{
    public class GridAddition : MonoBehaviour
    {
        [SerializeField] private Symbols _symbols;
        [SerializeField] private EquationCreator _equationCreator;

        private GridBuilder _gridBuilder;

        private void Awake()
            => _gridBuilder = GetComponent<GridBuilder>();

        public void AddElementsToGrid(bool isStartAdding = false)
        {
            AddElements();
            /*if(isStartAdding)
            {
                AddEquations();
                return;
            }    

            if(Random.Range(0, 101) < 40)
                AddElements();
            else
                AddEquations();*/
        }

        private void AddElements()
        {
            AddElement(_symbols.Numbers, 3);
            AddElement(_symbols.MathSigns, 1);
            AddElement(_symbols.ComparisonSigns, 1);
        }

        private void AddEquations()
        {
            //
        }

        private void AddElement(GridContent[] gridContents, int amount)
        {
            while(amount != 0)
            {
                int x = Random.Range(1, _gridBuilder.SizeX - 1);
                int y = Random.Range(1, _gridBuilder.SizeY - 1);

                if(_gridBuilder.GridElement(x, y).IsTaken == false)
                {
                    _gridBuilder.GridElement(x, y).ShowContent(gridContents[Random.Range(0, gridContents.Length)]);
                    amount--;
                }
            }
        }
    }
}

