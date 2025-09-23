using UnityEngine;
using UnityEngine.UI;

namespace GameCore.Mole
{
    public class MoleFieldConstructor : MonoBehaviour
    {
        [SerializeField] private RectTransform _fieldParent;
        [SerializeField] private MolePool _molePool;
        [SerializeField] private Sprite _frontHoleSprite;
        [SerializeField] private Sprite _backHoleSprite;
        
        private readonly Vector2 _spacing = new Vector2(330f, 330f);
        private readonly Vector2Int _gridSize = new Vector2Int(3, 3);

        public void BuildField()
        {
            _molePool.Init(_gridSize.x * _gridSize.y);

            float startX = -(_gridSize.x - 1) * _spacing.x / 2f;
            float startY = -(_gridSize.y - 1) * _spacing.y / 2f;

            int idx = 0;
            foreach (var mole in _molePool.GetAllMoles())
            {
                int x = idx % _gridSize.x;
                int y = idx / _gridSize.x;

                Vector2 pos = new Vector2(startX + x * _spacing.x, startY + y * _spacing.y);

                // Создаём "нору" с RectTransform
                GameObject holeRoot = new GameObject($"Hole_{idx}", typeof(RectTransform));
                var rootRect = holeRoot.GetComponent<RectTransform>();
                holeRoot.transform.SetParent(_fieldParent, false);
                rootRect.anchorMin = rootRect.anchorMax = new Vector2(0.5f, 0.5f);
                rootRect.pivot = new Vector2(0.5f, 0.5f);
                rootRect.anchoredPosition = pos;
                rootRect.localScale = Vector3.one;

                // Задняя яма
                GameObject backGo = new GameObject($"HoleBack_{idx}", typeof(RectTransform), typeof(Image));
                backGo.transform.SetParent(holeRoot.transform, false);
                var backImg = backGo.GetComponent<Image>();
                backImg.sprite = _backHoleSprite;
                backImg.SetNativeSize();
                backImg.raycastTarget = false;
                backGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                // Крот
                mole.transform.SetParent(holeRoot.transform, false);
                var moleRect = mole.GetComponent<RectTransform>();
                moleRect.anchorMin = moleRect.anchorMax = new Vector2(0.5f, 0.5f);
                moleRect.pivot = new Vector2(0.5f, 0.5f);
                moleRect.anchoredPosition = Vector2.zero;
                moleRect.localScale = Vector3.one;

                // Передняя яма
                GameObject frontGo = new GameObject($"HoleFront_{idx}", typeof(RectTransform), typeof(Image));
                frontGo.transform.SetParent(holeRoot.transform, false);
                var frontImg = frontGo.GetComponent<Image>();
                frontImg.sprite = _frontHoleSprite;
                frontImg.SetNativeSize();
                frontImg.raycastTarget = false;
                frontGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                mole.HideOnlyMole();
                mole.OnMoleHidden = (m) => _molePool.ReturnMole(m);

                idx++;
            }
        }
    }
}