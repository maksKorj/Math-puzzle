using System.Collections;
using UnityEngine;

namespace StartMenu
{
    public class FreeLivesButton : FreeButton
    {
        [SerializeField] private Lives _lives;
        [SerializeField] private LifeShopPopUp _lifeShopPop;

        private bool _isAdWatched;

        public override void GetFree()
        {
            _isAdWatched = false;
            StartCoroutine(WaitAndGiveLives());
           
            base.GetFree();
        }

        protected override void Give()
        {
            _isAdWatched = true;
            _lifeShopPop.Close();
        }

        private IEnumerator WaitAndGiveLives()
        {
            yield return new WaitWhile(() => _isAdWatched == false);
            
            _lives.AddLife(5);
        }
    }
}

