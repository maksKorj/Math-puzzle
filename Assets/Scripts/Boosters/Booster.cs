using UnityEngine;

namespace Boosters
{
    public abstract class Booster : ScriptableObject
    {
        [SerializeField] private Sprite _boosterImage;
        [SerializeField] private string _name;
        [Header("Properties")]
        [SerializeField] private int _coinPrice;
        [SerializeField] private int _gemPrice;
        [SerializeField] private bool _isImmediatelyApply = false;
        [SerializeField] private int _availableFromLevel;

        public Sprite BoosterImage => _boosterImage;
        public string Name => _name;
        public int CoinPrice => _coinPrice;
        public int GemPrice => _gemPrice;
        public bool IsImmediatelyApply => _isImmediatelyApply;
        public bool IsAvailable(int level) => _availableFromLevel >= level;

        public abstract void ApplyBooster();
        public abstract void Initialize();
    }
}