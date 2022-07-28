using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    public static DamagePopUp Instance { get; private set; }

    [SerializeField] private float _activeTime = 1.5f;
    [SerializeField] private float _offset;
    [SerializeField] private float _sidesOffset;
    [SerializeField] private float[] _textSizes;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _textPrefab;

    void OnValidate()
    {
        if (_camera == null) {
            Debug.LogError("No battle camera found.");
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Instance._camera = this._camera;
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public void ShowDamage(Vector3 position, int damage)
    {
        position += Vector3.up * _offset;
        position += new Vector3(Random.Range(-1 * _sidesOffset, 1 * _sidesOffset), 0, 0);
        Vector3 textPosition = _camera.WorldToScreenPoint(position);
        GameObject textObject = Instantiate(_textPrefab, textPosition, Quaternion.identity, transform);

        TextMeshProUGUI textCamp = textObject.GetComponent<TextMeshProUGUI>();
        textCamp.text = damage.ToString();
        textCamp.fontSize = Random.Range(_textSizes[0], _textSizes[1]);

        Destroy(textObject, _activeTime);
    }
}
