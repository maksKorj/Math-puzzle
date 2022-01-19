using UnityEngine;

namespace Boosters
{
    [CreateAssetMenu(menuName = "Booster/SkipSymbol")]
    public class SkipSymbol : Booster
    {
        private SymbolGiver _symbolGiver;

        public override void ApplyBooster() => _symbolGiver.ChangeSymbol();

        public override void Initialize()
            => _symbolGiver = FindObjectOfType<SymbolGiver>();
    }
}

