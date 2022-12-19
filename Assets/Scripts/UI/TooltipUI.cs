using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TooltipUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tooltip;
        [SerializeField] private List<string> tooltips;

        public void Display(int i)
        {
            tooltip.text = tooltips[i % tooltips.Count];
        }
    }
}