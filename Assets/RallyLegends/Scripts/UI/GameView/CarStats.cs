using TMPro;
using UnityEngine;
using RallyLegends.GameLogic;
using RallyLegends.Control;

namespace RallyLegends.UI
{
    public class CarStats : MonoBehaviour
    {
        [SerializeField] private TMP_Text _maxSpeedText;
        [SerializeField] private TMP_Text _accelerationText;
        [SerializeField] private TMP_Text _overheatingTimeText;

        [SerializeField] private CarSelector _selector;

        private PlayerSpeedController _currentCar;

        private void Start()
        {
            ShowStats();
        }

        public void ShowStats()
        {
            _currentCar = _selector.GetCurrentProduct().GetComponent<PlayerSpeedController>();

            _maxSpeedText.text = _currentCar.MaxSpeed.ToString();
            _accelerationText.text = _currentCar.Acceleration.ToString();
            _overheatingTimeText.text = _currentCar.OverheatingTime.ToString();
        }
    }
}