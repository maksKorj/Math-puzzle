using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace StartMenu
{
    public class Lives : MonoBehaviour
    {
        [SerializeField] private int _maxAmount = 5;
        [SerializeField] private TextMeshProUGUI _livesDisplay;
        [SerializeField] private TimerSaver _timerSaver;
        [SerializeField] private LifeTimer _lifeTimer;

        private int _livesAmount;
        private bool _canCheckPause;
        private bool _updateOnStart = true;
        private TimeSpan _timeToCompleteOneHeart = new TimeSpan(0, 45, 0);
        private TimeSpan RemainingTime => _timeToCompleteOneHeart - _lifeTimer.RemainingTimeToGiveLife;

        public bool HasLives => _livesAmount > 0;

        private void Start()
        {
            if(_updateOnStart)
                UpdateLife();
        }

        public void ShowAndRemoveLife()
        {
            _updateOnStart = false;
            gameObject.SetActive(true);
            
            UpdateLife();
            RemoveLife();
        }

        public void UpdateLife()
        {
            _lifeTimer.OnTimerEnd += AddLife;
            _livesAmount = PlayerSaver.LoadPlayerLives();
            
            SetTimer();
            StartCoroutine(WaitAndProcess());
        }

        private void SetTimer()
        {
            if (_livesAmount == _maxAmount)
            {
                _livesDisplay.text = _livesAmount.ToString();
            }
            else
            {
                var pastTime = _timerSaver.LoadTime(GetType(), true);

                if (_livesAmount <= 0 && PastTimeSmallerThanNeededTime(pastTime))
                    return;

                if (pastTime >= _timeToCompleteOneHeart)
                {
                    while (pastTime >= _timeToCompleteOneHeart)
                    {
                        pastTime -= _timeToCompleteOneHeart;

                        if (pastTime.IsNegative() == false)
                        {
                            _livesAmount++;
                            if (_livesAmount >= _maxAmount)
                            {
                                ShowLives();
                                return;
                            }
                        }
                    }
                    _lifeTimer.StartTimer(_timeToCompleteOneHeart + pastTime);
                }
                else
                {
                    _lifeTimer.StartTimer(_timeToCompleteOneHeart - pastTime);
                }

                ShowLives();
            }
        }

        private bool PastTimeSmallerThanNeededTime(TimeSpan timeSpan)
        {
            if (timeSpan < _timeToCompleteOneHeart)
            {
                ShowLives();
                _lifeTimer.StartTimer(_timeToCompleteOneHeart.Subtract(timeSpan));
                return true;
            }

            return false;
        }

        private void ShowLives()
        {
            _livesDisplay.text = _livesAmount.ToString();
            PlayerSaver.SavePlayerLife(_livesAmount);
        }

        public void AddLife(int amount = 1)
        {
            _livesAmount += amount;

            if (_livesAmount >= _maxAmount)
            {
                _livesAmount = _maxAmount;
                _lifeTimer.StopTimer();
                _timerSaver.CleatTime(GetType());
            }
            ShowLives();
        }

        private void RemoveLife()
        {
            _livesAmount--;
            _livesAmount = Mathf.Max(_livesAmount, 0);

            if(_livesAmount == 4)
                _lifeTimer.StartTimerAfterChange(_timeToCompleteOneHeart);

            ShowLives();
        }

        private IEnumerator WaitAndProcess()
        {
            yield return new WaitForSeconds(1f);
            _canCheckPause = true;
        }

        #region Save
        private void OnDestroy()
        {
            if (_livesAmount >= _maxAmount)
                return;

            _timerSaver.SaveCurrentTime(GetType(), RemainingTime);
        }

        private void OnApplicationQuit()
        {
            if (_livesAmount >= _maxAmount || _canCheckPause == false)
                return;

            _timerSaver.SaveCurrentTime(GetType(), RemainingTime);
        }

        private void OnApplicationPause(bool pause)
        {
            if (_livesAmount >= _maxAmount || _canCheckPause == false)
                return;

            if (pause)
                _timerSaver.SaveCurrentTime(GetType(), RemainingTime);
            else
                SetTimer();
        }
        #endregion
    }
}

