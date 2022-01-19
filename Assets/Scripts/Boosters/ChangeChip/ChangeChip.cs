using UnityEngine;

namespace Boosters
{
    [CreateAssetMenu(menuName = "Booster/ChangeChip")]
    public class ChangeChip : Booster
    {
        private ChangeChipHandler _changeChipHandler;

        public override void ApplyBooster() => _changeChipHandler.Apply();

        public override void Initialize()
        {
            _changeChipHandler = (ChangeChipHandler) FindObjectOfType<BoosterManager>()
                .GetGuideScreen(typeof(ChangeChipHandler));

            _changeChipHandler.Initialize(GetType());
        }
    }
}

