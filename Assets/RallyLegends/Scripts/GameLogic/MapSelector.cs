using UnityEngine;
using UnityEngine.UI;
using RallyLegends.Objects;
using YG;

namespace RallyLegends.GameLogic
{
    public class MapSelector : Selector
    {
        [SerializeField] private UI.Screen _screen;
        [SerializeField] private Container _mapStorage;
        [SerializeField] private Image _mapImage;

        private Map _currentMap;

        private void OnEnable()
        {
            SelectProduct(CurrentProductIndex);
            SaveCurrentMapIndex();
        }

        private void OnDisable()
        {
            for (int i = 0; i < _mapStorage.ItemsCount; i++)
                _mapStorage.GetItem(i).gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_currentMap.IsBuyed)
                LockImage.gameObject.SetActive(false);
        }

        public override void ChangeProduct(int changer)
        {
            CurrentProductIndex += changer;
            SelectProduct(CurrentProductIndex);
        }

        public override Product GetCurrentProduct() => _currentMap;

        public override void PayProduct() => _mapStorage.BuyItem(_currentMap);

        protected override void SelectProduct(int mapIndex)
        {
            NextButton.interactable = (mapIndex != _mapStorage.ItemsCount - 1);
            PrevButton.interactable = (mapIndex != 0);

            _currentMap = _mapStorage.GetItem(mapIndex).GetComponent<Map>();
            _mapImage.sprite = _currentMap.Sprite;

            SaveCurrentMapIndex();

            if (_screen.GetComponent<CanvasGroup>().alpha == 1f)
                ShowInfo();
        }

        protected override void ShowInfo() => LockImage.gameObject.SetActive(!_currentMap.IsBuyed);

        private void SaveCurrentMapIndex() => YandexGame.savesData.MapIndex = CurrentProductIndex;
    }
}