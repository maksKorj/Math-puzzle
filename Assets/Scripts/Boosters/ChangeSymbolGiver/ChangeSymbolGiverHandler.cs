using System;
using UnityEngine;

namespace Boosters
{
    public class ChangeSymbolGiverHandler : MonoBehaviour
    {
        [SerializeField] private SymbolGiver _symbolGiver;
        [SerializeField] private SelectSymbolScreen _selectSymbolScreen;
        [SerializeField] private BoosterManager _boosterManager;

        private Type _boosterType;

        public void Initialize(Type boosterType) => _boosterType = boosterType;

        public void Apply()
            => _selectSymbolScreen.ShowWindow(_symbolGiver.ChangeSymbol, GiveBack);

        public void GiveBack()  => _boosterManager.ReceiveBack(_boosterType);
    }
}

