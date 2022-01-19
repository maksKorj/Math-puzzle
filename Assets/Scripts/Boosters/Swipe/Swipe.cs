using UnityEngine;

namespace Boosters
{
    [CreateAssetMenu(menuName = "Booster/Swipe")]
    public class Swipe : Booster
    {
        private SwipeHandler _swipeHandler;

        public override void ApplyBooster()  => _swipeHandler.Apply();

        public override void Initialize()
        {
            _swipeHandler = (SwipeHandler) FindObjectOfType<BoosterManager>().GetGuideScreen(typeof(SwipeHandler));
            _swipeHandler.Initialize(GetType());
        }
    }
}

