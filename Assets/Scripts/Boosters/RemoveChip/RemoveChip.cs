using UnityEngine;

namespace Boosters
{
    [CreateAssetMenu(menuName = "Booster/RemoveChip")]
    public class RemoveChip : Booster
    {
        private RemoveChipHandler _removeChipHandler;

        public override void ApplyBooster()
            => _removeChipHandler.Apply();

        public override void Initialize()
        {
            _removeChipHandler = (RemoveChipHandler)FindObjectOfType<BoosterManager>().GetGuideScreen(typeof(RemoveChipHandler));
            _removeChipHandler.Initialize(GetType());
        }
    }
}

