using UnityEngine;

/// <summary>
/// Class to show the damage taken with an pop up ui.
/// </summary>
public class DamagePopUp : MonoBehaviour
{
    public static DamagePopUp Instance { get; private set; }

    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _textPrefab;

    // Text configuration
    [SerializeField] private float _activeTime = 0.7f;
    [SerializeField] private float _offset = 1.5f;
    [SerializeField] private float _sidesOffset = 0.2f;
    [SerializeField] private float _minSize = 14;
    [SerializeField] private float _maxSize = 16;

    private DamagePopUpUI _damageUI;

    void OnValidate()
    {
        if (_camera == null) {
            Debug.LogError("No battle camera found.");
        }

        if (_textPrefab == null) {
            Debug.LogError("No prefab object set.");
        }

        if (_textPrefab.GetComponent<DamagePopUpUI>() == null) {
            Debug.LogError("Missing UI component.");
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public void ShowDamage(Vector3 position, int damage)
    {
        Vector3 textPosition = CalculateScreenPosition(position);

        GameObject textObject = Instantiate(_textPrefab, textPosition, Quaternion.identity, transform);
        _damageUI = textObject.GetComponent<DamagePopUpUI>();

        _damageUI.UpdateText(damage, _minSize, _maxSize);

        Destroy(textObject, _activeTime);
    }

    private Vector3 CalculateScreenPosition(Vector3 targetPosition)
    {
        targetPosition += Vector3.up * _offset;
        targetPosition += new Vector3(Random.Range(-1 * _sidesOffset, 1 * _sidesOffset), 0, 0);

        Vector3 textPosition = _camera.WorldToScreenPoint(targetPosition);
        return textPosition;
    }
}
