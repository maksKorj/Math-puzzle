using UnityEngine;

namespace Boosters
{
    [CreateAssetMenu(menuName = "Booster/ChangeSymbolGiver")]
    public class ChangeSymbolGiver : Booster
    {
        ChangeSymbolGiverHandler _changeSymbolGiverHandler;
        
        public override void ApplyBooster() => _changeSymbolGiverHandler.Apply();

        public override void Initialize()
        {
            _changeSymbolGiverHandler = FindObjectOfType<ChangeSymbolGiverHandler>();
            _changeSymbolGiverHandler.Initialize(GetType());
        }
    }
}

