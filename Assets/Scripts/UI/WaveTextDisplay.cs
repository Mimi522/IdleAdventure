using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WaveTextDisplay : MonoBehaviour
{
    private int _waveCount = 0;

    public void IncrementWave()
    {
        _waveCount++;
        GetComponent<TextMeshProUGUI>().text = $"Wave {_waveCount}/10";
    }
}
