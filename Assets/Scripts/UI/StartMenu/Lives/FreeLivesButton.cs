using UnityEngine;

namespace StartMenu
{
    public class FreeLivesButton : FreeButton
    {
        [SerializeField] private Lives _lives;
        [SerializeField] private LifeShopPopUp _lifeShopPop;

        protected override void Give()
        {
            _lives.AddLife(5);
            _lifeShopPop.Close();
        }
    }
}

