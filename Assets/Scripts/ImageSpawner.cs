using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private int _count;

    [SerializeField]
    private List<Sprite> _sprites;

    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            GameObject imgGO = new GameObject("Image_" + i, typeof(RectTransform), typeof(Image));
            imgGO.transform.SetParent(_parent, false);

            Image img = imgGO.GetComponent<Image>();

            Sprite spriteToUse = null;

            if (_sprites.Count > 0)
            {
                spriteToUse = _sprites[i % _sprites.Count];
            }
            else
            {
                return;
            }

            img.sprite = spriteToUse;

            RectTransform rt = imgGO.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(100, 100);

            int columns = 20;
            float spacing = 10f;
            int row = i / columns;
            int col = i % columns;
            rt.anchoredPosition = new Vector2(col * (100 + spacing), -row * (100 + spacing));
        }
    }
}