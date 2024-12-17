using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RallyLegends.GameLogic;

namespace RallyLegends.UI
{
    public class StartScreen : Screen
    {
        [SerializeField] private TMP_Text _moneyCount;
        [SerializeField] private CarSelector _carSelector;
        [SerializeField] private MapSelector _mapSelector;
        [SerializeField] private Container _garage;
        [SerializeField] private Container _botStorage;

        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _buyMapButton;

        public event UnityAction StartButtonClick;
        public event UnityAction BackToMenuButtonClick;

        private void Update()
        {
            if (_garage != null)
                _moneyCount.text = _garage.WalletCount.ToString();

            if (CanvasGroup.alpha == 1f)
            {
                SetStartButtonInteracteble(_carSelector.GetCurrentProduct().IsBuyed && _mapSelector.GetCurrentProduct().IsBuyed);
                BuyMapButtonView(!_mapSelector.GetCurrentProduct().IsBuyed);
            }
        }

        public void OnStartButtonClick() => StartButtonClick?.Invoke();

        public void OnMenuButtonClick() => BackToMenuButtonClick?.Invoke();

        public void BuyMapButtonView(bool value)
        {
            _buyMapButton.gameObject.SetActive(value);
            SetBuyButtonInterectable(_mapSelector.GetCurrentProduct().Price <= _garage.WalletCount);

            if (_buyMapButton.gameObject.activeSelf)
                _buyMapButton.gameObject.GetComponentInChildren<TMP_Text>().text = _mapSelector.GetCurrentProduct().Price.ToString();
        }

        public override void Open()
        {
            CanvasGroup.alpha = 1f;
            SetInteractable(true);
        }

        public override void Close()
        {
            CanvasGroup.alpha = 0f;
            SetInteractable(false);
        }

        protected override void SetInteractable(bool value)
        {
            SetStartButtonInteracteble(value);

            _backToMenuButton.interactable = value;
            _backToMenuButton.image.raycastTarget = value;

            _carSelector.gameObject.SetActive(value);
            _mapSelector.gameObject.SetActive(value);
        }

        private void SetStartButtonInteracteble(bool value)
        {
            _startButton.interactable = value;
            _startButton.image.raycastTarget = value;
        }

        private void SetBuyButtonInterectable(bool value)
        {
            _buyMapButton.interactable = value;
            _buyMapButton.image.raycastTarget = value;
        }
    }
}