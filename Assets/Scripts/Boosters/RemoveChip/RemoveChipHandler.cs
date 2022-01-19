using UnityEngine.EventSystems;
using LevelBuilder;
using UnityEngine;

namespace Boosters
{
    public class RemoveChipHandler : MainScreen
    {
        private WaitForSeconds _delay;

        protected override bool IsCorrectRaycastResult(RaycastResult result)
        {
            if(result.gameObject.TryGetComponent(out GridElement gridElement) && gridElement.IsTaken)
            {
                _canClick = false;
                _mainScreenProperty.GridAnimation.UnSelectAll();

                HidePanel();
                gridElement.PlayHideEffect();

                CheckDelay(gridElement);

                StartCoroutine(WaitAndCheckEquation(_delay));
                return true;
            }

            return false;
        }

        private void CheckDelay(GridElement gridElement)
        {
            if (_delay == null)
                _delay = new WaitForSeconds(gridElement.HidingTime);
        }
    }
}

