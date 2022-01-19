using UnityEngine;

namespace Boosters
{
    [CreateAssetMenu(menuName = "Booster/ChangePosition")]
    public class ChangePosition : Booster
    {
        private ChangePositionHandler _changePositionHandler;

        public override void ApplyBooster() => _changePositionHandler.Apply();

        public override void Initialize()
        {
            _changePositionHandler = (ChangePositionHandler) FindObjectOfType<BoosterManager>()
                .GetGuideScreen(typeof(ChangePositionHandler));

            _changePositionHandler.Initialize(GetType());
        }
    }
}


