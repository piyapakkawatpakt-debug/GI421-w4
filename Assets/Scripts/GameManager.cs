using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGenerateGrass;

    [SerializeField]
    private List<EdiblePlant> _generateEdible;

    [SerializeField]
    private Transform _holder;

    [SerializeField]
    private List<EdiblePlant> _ediblePrefabs;

    [SerializeField]
    private Goat _goatPrefab;

    [SerializeField]
    private int _rows = 3;

    [SerializeField]
    private int _cols = 5;

    [SerializeField]
    private float _spacing = 2f;

    [Header("UI Section")]
    [SerializeField]
    private TextMeshProUGUI _textMeshGrass;

    [SerializeField]
    private TextMeshProUGUI _textMeshFlower;

    [SerializeField]
    private TextMeshProUGUI _textMeshMushroom;

    [SerializeField]
    private TextMeshProUGUI _textMeshPlant;

    public static GameManager Instance;

    private List<EdiblePlant> _allEdible;
    private int _grassEatenCount = 0;
    private int _flowerEatenCount = 0;
    private int _mushroomEatenCount = 0;
    private int _plantEatenCount = 0;

    private void Awake()
    {
        Instance = this;

        _allEdible = new List<EdiblePlant>();
    }

    private void Start()
    {
        if (_isGenerateGrass)
        {
            foreach (var g in _generateEdible)
            {
                if (g == null) continue;
                g.gameObject.SetActive(false);
            }

            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _cols; c++)
                {
                    // pick a random grass prefab from the list
                    EdiblePlant prefab = _ediblePrefabs[Random.Range(0, _ediblePrefabs.Count)];

                    Vector3 pos = new Vector3(c * _spacing, 0, r * _spacing);
                    EdiblePlant grass = Instantiate(prefab, pos, Quaternion.identity);
                    grass.transform.parent = _holder;
                    _allEdible.Add(grass);
                }
            }
        }
        else
        {
            _allEdible.AddRange(_generateEdible);
        }

        Instantiate(_goatPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public List<EdiblePlant> GetAllGrass()
    {
        return _allEdible;
    }

    public void OnPlantEaten(PlantType type)
    {
        switch (type)
        {
            case PlantType.Grass:
                _grassEatenCount++;
                _textMeshGrass.text = $"X {_grassEatenCount}";
                break;
            case PlantType.Flower:
                _flowerEatenCount++;
                _textMeshFlower.text = $"X {_flowerEatenCount}";
                break;
            case PlantType.Mushroom:
                _mushroomEatenCount++;
                _textMeshMushroom.text = $"X {_mushroomEatenCount}";
                break;
            case PlantType.Plant:
                _plantEatenCount++;
                _textMeshPlant.text = $"X {_plantEatenCount}";
                break;
        }
    }
}