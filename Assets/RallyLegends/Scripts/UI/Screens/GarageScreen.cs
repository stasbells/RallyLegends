using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RallyLegends.GameLogic;

namespace RallyLegends.UI
{
    public class GarageScreen : Screen
    {
        [SerializeField] private CarSelector _carSelector;
        [SerializeField] private ColorSelector _colorSelector;
        [SerializeField] private TMP_Text _moneyCount;
        [SerializeField] private Container _garage;

        [Header("Buttons")]
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _buyCarButton;
        [SerializeField] private Button _buyColorButton;

        public event UnityAction BackToMenuButtonClicked;

        private void Update()
        {
            if (_garage != null)
                _moneyCount.text = _garage.WalletCount.ToString();

            if (CanvasGroup.alpha == 1f)
            {
                BuyButtonView(_buyCarButton, !_carSelector.GetCurrentProduct().IsBuyed);
                BuyColorButtonView(_buyColorButton, !_colorSelector.GetCurrentProduct().IsBuyed);
            }
        }

        public void OnMenuButtonClick() => BackToMenuButtonClicked?.Invoke();

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
            _buyColorButton.interactable = value;
            _buyColorButton.image.raycastTarget = value;

            _buyCarButton.interactable = value;
            _buyCarButton.image.raycastTarget = value;

            _backToMenuButton.interactable = value;
            _backToMenuButton.image.raycastTarget = value;

            _carSelector.gameObject.SetActive(value);
            _colorSelector.gameObject.SetActive(value);
        }

        private void BuyButtonView(Button button, bool value)
        {
            button.gameObject.SetActive(value);
            SetButtonInterectable(button, _carSelector.GetCurrentProduct().Price <= _garage.WalletCount);

            if (button.gameObject.activeSelf)
                button.gameObject.GetComponentInChildren<TMP_Text>().text =
                    _carSelector.GetCurrentProduct().Price.ToString();
        }

        private void BuyColorButtonView(Button button, bool value)
        {
            button.gameObject.SetActive(value);
            SetButtonInterectable(button, _colorSelector.GetCurrentProduct().Price <= _garage.WalletCount);

            if (button.gameObject.activeSelf)
                button.gameObject.GetComponentInChildren<TMP_Text>().text =
                    _colorSelector.GetCurrentProduct().Price.ToString();
        }

        private void SetButtonInterectable(Button button, bool value)
        {
            button.interactable = value;
            button.image.raycastTarget = value;
        }
    }
}