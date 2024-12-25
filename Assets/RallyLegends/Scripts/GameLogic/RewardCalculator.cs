using TMPro;
using UnityEngine;
using YG;
using RallyLegends.Data;
using RallyLegends.UI;
using YG.Utils.LB;

namespace RallyLegends.GameLogic
{
    public class RewardCalculator : MonoBehaviour
    {
        [SerializeField] private LevelInstaller _level;

        [Header("Text")]
        [SerializeField] private TMP_Text _rewardForWinningText;
        [SerializeField] private TMP_Text _rewardForTimeText;

        [Header("UI Elements")]
        [SerializeField] private LapCounter _counter;
        [SerializeField] private Stopwatch _stopwatch;

        [Header("Reward Parameters")]
        [SerializeField] private int _maxRewardForTime;
        [SerializeField] private int _rewardForWinning;
        [SerializeField] private int _minReward;
        [SerializeField] private float _rewardIndex;

        private int _result;
        private int _reward;
        private int _previousRecord;
        private string _technoName;

        private void OnEnable()
        {
            _counter.Finished += OnFinishedGetReward;
            YandexGame.onGetLeaderboard += OnGetLeaderboard;
        }

        private void OnDisable()
        {
            _counter.Finished -= OnFinishedGetReward;
            YandexGame.onGetLeaderboard -= OnGetLeaderboard;
        }

        public void GetLeaderboard(string technoName)
        {
            int maxQuantityPlayers = 10;
            int quantityTop = 3;
            int quantityAround = 3;
            string photoSize = "small";

            _technoName = technoName;

            YandexGame.GetLeaderboard(technoName, maxQuantityPlayers, quantityTop, quantityAround, photoSize);
        }

        public void ExampleOpenRewardAd(int id)
        {
            GetDoubleReward();
            YandexGame.RewVideoShow(id);
        }

        private int GetRewardForTime() => Mathf.Clamp(_maxRewardForTime - (int)(_stopwatch.Resault / _rewardIndex), _minReward, _maxRewardForTime);

        private int GetRewardForWinning(bool value) => value ? _rewardForWinning : _minReward;

        private void GetDoubleReward() => YandexGame.savesData.Reward += _reward;

        private void OnFinishedGetReward()
        {
            _reward = GetRewardForTime() + GetRewardForWinning(_level.GetFinishResault());
            YandexGame.savesData.Reward = _reward;

            _rewardForTimeText.text = GetRewardForTime().ToString();
            _rewardForWinningText.text = GetRewardForWinning(_level.GetFinishResault()).ToString();
#if !UNITY_EDITOR
        SaveScore();
#endif
        }

        private void SaveScore()
        {
            switch (_level.MapId)
            {
                case Constants.ForestMapId:
                    CheckScore(Constants.BestTimeForestMapLiaderboard);
                    break;

                case Constants.WinterMapId:
                    CheckScore(Constants.BestTimeWinterMapLiaderboard);
                    break;

                case Constants.SwampMapId:
                    CheckScore(Constants.BestTimeSwampMapLiaderboard);
                    break;
            }
        }

        private void CheckScore(string technoName)
        {
            float secondsScore = _stopwatch.Resault;
            int indexComma = secondsScore.ToString().IndexOf(",");

            string result = secondsScore.ToString();
            string sec = result.Remove(indexComma);
            string milSec = result.Remove(0, indexComma + 1);

            if (milSec.Length > 3)
                milSec = milSec.Remove(3);
            else if (milSec.Length == 2)
                milSec += "0";
            else if (milSec.Length == 1)
                milSec += "00";

            result = sec + milSec;
            _result = int.Parse(result);

            GetLeaderboard(technoName);
        }

        private void OnGetLeaderboard(LBData lb)
        {
            if (lb.technoName == _technoName)
                _previousRecord = lb.thisPlayer.score;

            if (_previousRecord > _result || _previousRecord <= 0)
                YandexGame.NewLBScoreTimeConvert(lb.technoName, _result);
        }
    }
}