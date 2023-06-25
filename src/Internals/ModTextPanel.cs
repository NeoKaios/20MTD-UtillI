
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UtillI.Internals
{
    class ModTextPanel : MonoBehaviour
    {
        private TextMeshProUGUI tmpLeft;
        private TextMeshProUGUI tmpRight;
        void Awake()
        {
            var rect = gameObject.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            SetupTextObject(PanelPosition.BottomLeft);
            SetupTextObject(PanelPosition.BottomRight);
        }

        void SetupTextObject(PanelPosition pos)
        {
            Transform textObjectTemplate = transform.parent.Find("ControlsDisplay/GamepadControls/MoveControls/Dash");
            var textObject = Object.Instantiate(textObjectTemplate.gameObject, gameObject.transform);
            var rect = textObject.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;

            var csf = textObject.AddComponent<ContentSizeFitter>();
            csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            SetTMP(pos, textObject.GetComponent<TextMeshProUGUI>());
            var tmp = GetTMP(pos);
            tmp.verticalAlignment = VerticalAlignmentOptions.Bottom;
            tmp.horizontalAlignment = HorizontalAlignmentOptions.Left;
            tmp.text = "";

            if (pos == PanelPosition.BottomLeft)
            {
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.zero;
                tmp.horizontalAlignment = HorizontalAlignmentOptions.Left;
                rect.pivot = Vector2.zero;
                rect.anchoredPosition = Vector2.one * 5;
            }
            else
            {
                rect.anchorMin = Vector2.right;
                rect.anchorMax = Vector2.right;
                rect.pivot = Vector2.right;
                rect.anchoredPosition = new Vector2(-1f, 1f) * 5;
                tmp.horizontalAlignment = HorizontalAlignmentOptions.Right;
            }
        }

        private void SetTMP(PanelPosition pos, TextMeshProUGUI tmp)
        {
            if (pos == PanelPosition.BottomLeft) tmpLeft = tmp;
            else tmpRight = tmp;
        }
        private TextMeshProUGUI GetTMP(PanelPosition pos)
        {
            if (pos == PanelPosition.BottomLeft) return tmpLeft;
            return tmpRight;
        }

        public void SetText(PanelPosition pos, string text)
        {
            GetTMP(pos).text = text;
        }

        public void Show(PanelPosition pos)
        {
            GetTMP(pos).gameObject.SetActive(true);
        }
        public void Hide(PanelPosition pos)
        {
            GetTMP(pos).gameObject.SetActive(false);
        }
    }
}