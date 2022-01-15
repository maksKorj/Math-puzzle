using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace LevelBuilder
{
    public class EquationBuilder : MonoBehaviour
    {
        [SerializeField] private EquationCreator _equationCreator;
        [SerializeField] private EquationChecker _equationChecker;

        private GridBuilder _gridBuilder;
        private EquationBuilderHelper _equationBuilderHelper;
        private Action<GridElement> GetEquation;
        private List<Func<GridElement, bool>> AddingEquation = new List<Func<GridElement, bool>>();
        private List<GridContent> _equation;

        public List<GridContent> _givenGridContents = new List<GridContent>();

        private void Awake()
        {
            _gridBuilder = GetComponent<GridBuilder>();
            _equationBuilderHelper = new EquationBuilderHelper(_gridBuilder, _givenGridContents);

            AddingEquation.Add(CanSetEquationFromFirstNumber);
            AddingEquation.Add(CanSetEquationFromMathSign);
            AddingEquation.Add(CanSetEquationFromSecondNumber);
            AddingEquation.Add(CanSetEquationFromComparsionSing);
        }

        //Remove
        public void ShowNew()
        {
            for(int x = 0; x < _gridBuilder.SizeX; x++)
            {
                for(int y = 0; y < _gridBuilder.SizeY; y++)
                {
                    if (_gridBuilder.GridElement(x, y) == null)
                        continue;

                    _gridBuilder.GridElement(x, y).RemoveGridContent();
                }
            }

            AddBrokenEquations();
        }

        public void AddBrokenEquations()
        {
            _givenGridContents.Clear();
            int amountEquationToAdd = Random.Range(3, _gridBuilder.SizeX);

            for (int i = 0; i < amountEquationToAdd; i++)
            {
                int totalAttemptToAdd = 0;
                int index = 0;
                AddingEquation.Shuffle();

                while (totalAttemptToAdd < 10)
                {
                    var element = _gridBuilder.GridElement(Random.Range(0, _gridBuilder.SizeX), Random.Range(0, _gridBuilder.SizeY));
                    
                    if (element != null)
                    {
                        if (index >= AddingEquation.Count)
                            index = 0;

                        if (AddingEquation[index](element))
                            break;

                        index++;
                    }
                    else
                        totalAttemptToAdd++;
                }
            }

            RemoveCorrectEquation();
        }

        public bool HasAvailableContent(out GridContent gridContent)
        {
            gridContent = null;

            if(_givenGridContents.Count > 0)
            {
                int index = Random.Range(0, _givenGridContents.Count);
                gridContent = _givenGridContents[index];
                _givenGridContents.RemoveAt(index);

                return true;
            }

            return false;
        }

        private void RemoveCorrectEquation()
        {
            for(int x = 0; x < _gridBuilder.SizeX; x++)
            {
                for(int y = 0; y < _gridBuilder.SizeY; y++)
                {
                    if (_gridBuilder.GridElement(x, y) == null)
                        continue;

                    if (_gridBuilder.GridElement(x, y).IsTakenComparisonSigns)
                        _equationChecker.RemoveCorrectEquation(_gridBuilder.GridElement(x, y).GridPosition);

                }
            }
        }

        #region FromNumber
        private bool CanSetEquationFromFirstNumber(GridElement gridElement)
        {
            GetEquation = FromFirstNumber;
            return CanSetEquation(gridElement, 0, 5);
        }

        private bool CanSetEquationFromSecondNumber(GridElement gridElement)
        {
            GetEquation = FromSecondNumber;
            return CanSetEquation(gridElement, 3, 3);
        }

        private void FromFirstNumber(GridElement gridElement) => NumberHelper(gridElement);
        private void FromSecondNumber(GridElement gridElement) => NumberHelper(gridElement, false);

        private void NumberHelper(GridElement gridElement, bool isFirstNumber = true)
        {
            int number = -1;
            if (gridElement.IsTakenNumber)
                number = gridElement.Number();

            if (number >= 0)
                _equation = _equationCreator.GetEquation(number, isFirstNumber);
            else
                _equation = _equationCreator.GetEquation();

            _equationBuilderHelper.SetRemoveIndex(Random.Range(1, 3), _equation.Count);
        }
        #endregion

        #region FromMathSign
        private bool CanSetEquationFromMathSign(GridElement gridElement)
        {
            GetEquation = MathSignHepler;
            return CanSetEquation(gridElement, 2, 3);
        }

        private void MathSignHepler(GridElement gridElement)
        {
            MathSign mathSign = null;
            if (gridElement.IsTakenMathSign)
                mathSign = gridElement.MathSign();

            if (mathSign != null)
                _equation = _equationCreator.GetEquation(mathSign);
            else
                _equation = _equationCreator.GetEquation();

            _equationBuilderHelper.SetRemoveIndex(Random.Range(1, 3), _equation.Count);
        }
        #endregion

        #region FromComparsionSign
        private bool CanSetEquationFromComparsionSing(GridElement gridElement)
        {
            GetEquation = ComparsionSingHelper;
            return CanSetEquation(gridElement, 2, 2);
        }

        private void ComparsionSingHelper(GridElement gridElement)
        {
            if (gridElement.IsTakenComparisonSigns)
                _equation = GetEquationWithComparsionSign(gridElement.ComparisonSign());
            else
                _equation = GetEquationWithComparsionSign(_equationCreator.GetRamdomComparisonSign());

            _equationBuilderHelper.SetRemoveIndex(Random.Range(1, 3), _equation.Count);
        }

        private List<GridContent> GetEquationWithComparsionSign(ComparisonSign comparisonSign)
        {
            int number = Random.Range(2, 97);

            comparisonSign.CanCorrecting(number, number, out int correctingRightPart);

            return _equationCreator.GetSimpleEquation(number, comparisonSign, correctingRightPart);
        }
        #endregion

        #region Check
        private bool CanSetEquation(GridElement gridElement, int leftRestriction, int rightRestriction)
        {
            int totalRestriction = leftRestriction + rightRestriction;

            if (CanPlaceHorizontal(gridElement.GridPosition, leftRestriction, rightRestriction))
            {
                if (gridElement.GridPosition.y == 0 || gridElement.GridPosition.y == _gridBuilder.SizeY - 1)
                    return false;

                GetEquation(gridElement);
                var position = new Vector2Int(gridElement.GridPosition.x - leftRestriction, gridElement.GridPosition.y);

                _equationBuilderHelper.SetHorizontalEquation(position, totalRestriction, _equation);
                return true;
            }

            if (CanPlaceVertical(gridElement.GridPosition, leftRestriction, rightRestriction))
            {
                if (gridElement.GridPosition.x == 0 || gridElement.GridPosition.x == _gridBuilder.SizeX - 1)
                    return false;

                GetEquation(gridElement);
                var position = new Vector2Int(gridElement.GridPosition.x, gridElement.GridPosition.y + leftRestriction);

                _equationBuilderHelper.SetVerticalEquation(position, totalRestriction, _equation);
                return true;
            }

            return false;
        }

        private bool CanPlaceHorizontal(Vector2Int position, int leftPartRestriction = -1, int rightPartRestriction =- 1)
        {
            if(leftPartRestriction > 0)
            {
                for (int x = position.x - 1; x >= position.x - leftPartRestriction; x--)
                {
                    if (x >= 0)
                    {
                        if (_gridBuilder.GridElement(x, position.y) == null || _gridBuilder.GridElement(x, position.y).IsTaken)
                            return false;
                    }
                    else
                        return false;
                }
            }

            if(rightPartRestriction > 0)
            {
                for(int x = position.x + 1; x <= position.x + rightPartRestriction; x++)
                {
                    if (x < _gridBuilder.SizeX)
                    {
                        if (_gridBuilder.GridElement(x, position.y) == null || _gridBuilder.GridElement(x, position.y).IsTaken)
                            return false;
                    }
                    else
                        return false;
                }
            }

            return true;
        }

        private bool CanPlaceVertical(Vector2Int position, int leftPartRestriction = -1, int rightPartRestriction = -1)
        {
            if (leftPartRestriction > 0)
            {
                for (int y = position.y + 1; y <= position.y + leftPartRestriction; y++)
                {
                    if (y < _gridBuilder.SizeY)
                    {
                        if (_gridBuilder.GridElement(position.x, y) == null || _gridBuilder.GridElement(position.x, y).IsTaken)
                            return false;
                    }
                    else
                        return false;
                }
            }

            if (rightPartRestriction > 0)
            {
                for (int y = position.y - 1; y >= position.y - rightPartRestriction; y--)
                {
                    if (y >= 0)
                    {
                        if (_gridBuilder.GridElement(position.x, y) == null || _gridBuilder.GridElement(position.x, y).IsTaken)
                            return false;
                    }
                    else
                        return false;
                }
            }

            return true;
        }
        #endregion
    }

    public class EquationBuilderHelper
    {
        private List<int> _removedEquationIndexes = new List<int>();
        private GridBuilder _gridBuilder;
        List<GridContent> _givenGridContent;

        public EquationBuilderHelper(GridBuilder gridBuilder, List<GridContent> givenGridContent)
        {
            _gridBuilder = gridBuilder;
            _givenGridContent = givenGridContent;
        }

        public void SetHorizontalEquation(Vector2Int gridPosition, int restriction, List<GridContent> equation, int startIndex = 0)
        {
            for (int x = gridPosition.x; x <= gridPosition.x + restriction; x++)
            {
                if (startIndex < equation.Count)
                {
                    if (x >= _gridBuilder.SizeX || x < 0)
                        break;

                    if (IsRemovedIndex(startIndex))
                        _givenGridContent.Add(equation[startIndex]);
                    else
                        _gridBuilder.GridElement(x, gridPosition.y).SetContent(equation[startIndex]);
                }
                else
                    break;

                startIndex++;
            }
        }

        public void SetVerticalEquation(Vector2Int gridPosition, int restriction, List<GridContent> equation, int index = 0)
        {
            for (int y = gridPosition.y; y >= gridPosition.y - restriction; y--)
            {
                if (index < equation.Count)
                {
                    if (y < 0 || y >= _gridBuilder.SizeY)
                        break;

                    if (IsRemovedIndex(index))
                        _givenGridContent.Add(equation[index]);
                    else
                        _gridBuilder.GridElement(gridPosition.x, y).SetContent(equation[index]);
                }
                else
                    break;

                index++;
            }
        }

        public void SetRemoveIndex(int removeAmount, int euqationLength)
        {
            _removedEquationIndexes.Clear();

            while (removeAmount > 0)
            {
                _removedEquationIndexes.Add(Random.Range(0, euqationLength));
                removeAmount--;
            }
        }

        public bool IsRemovedIndex(int index)
        {
            for (int i = 0; i < _removedEquationIndexes.Count; i++)
            {
                if (_removedEquationIndexes[i] == index)
                    return true;

            }

            return false;
        }
    }
}




