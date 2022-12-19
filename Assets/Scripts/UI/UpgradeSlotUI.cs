using System;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeSlotUI : MonoBehaviour
    {
        public event Action<UpgradeSlotUI> OnPress;
        
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI mainText;
        [SerializeField] private TextMeshProUGUI flavourText;
        [SerializeField] private Image image;

        private IUpgrade _upgrade;

        public IUpgrade Upgrade
        {
            get => _upgrade;
            set => _upgrade = value;
        }

        public void InvokePress()
        {
            OnPress?.Invoke(this);
        }

        public void UpdateGraphics()
        {
            if (Upgrade == null)
            {
                gameObject.SetActive(false);
                return;
            }
            if(mainText != null) mainText.text = _upgrade.GetUpgradeName();
            if(flavourText != null) flavourText.text = _upgrade.GetFlavourText();
            if(image != null) image.sprite = _upgrade.GetImage();
        }
    }
}