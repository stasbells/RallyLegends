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
        [SerializeField] private float _rewardIndex;

        [Header("Text")]
        [SerializeField] private TMP_Text _rewardForWinningText;
        [SerializeField] private TMP_Text _rewardForTimeText;

        [Header("UI Elements")]
        [SerializeField] private LapCounter _counter;
        [SerializeField] private Stopwatch _stopwatch;


        private const int MaxRewardForTime = 100;
        private const int RewardForWinning = 50;
        private const int MinReward = 0;

        private int _result;
        private int _reward;
        private int _previousRecord;
        private string _technoName;

        private void OnEnable()
        {
            _counter.Finished += GetReward;
            YandexGame.onGetLeaderboard += OnGetLeaderboard;
        }

        private void OnDisable()
        {
            _counter.Finished -= GetReward;
            YandexGame.onGetLeaderboard -= OnGetLeaderboard;
        }

        public void GetLeaderboard(string technoName)
        {
            _technoName = technoName;
            YandexGame.GetLeaderboard(technoName, 10, 3, 3, "small");
        }

        public void ExampleOpenRewardAd(int id)
        {
            GetDoubleReward();
            YandexGame.RewVideoShow(id);
        }

        private int GetRewardForTime() => Mathf.Clamp(MaxRewardForTime - (int)(_stopwatch.Resault / _rewardIndex), MinReward, MaxRewardForTime);

        private int GetRewardForWinning(bool value) => value ? RewardForWinning : MinReward;

        private void GetDoubleReward() => YandexGame.savesData.Reward += _reward;

        private void GetReward()
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

            string rec = secondsScore.ToString();
            string sec = rec.Remove(indexComma);
            string milSec = rec.Remove(0, indexComma + 1);

            if (milSec.Length > 3)
                milSec = milSec.Remove(3);
            else if (milSec.Length == 2)
                milSec += "0";
            else if (milSec.Length == 1)
                milSec += "00";

            rec = sec + milSec;
            _result = int.Parse(rec);

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