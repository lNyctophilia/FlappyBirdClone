using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _muteButtonImage;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    [Header("Settings")]
    [SerializeField] private bool _isMuted;

    private void Awake()
    {
        // PlayerPrefs kontrolü (Varsayılan değer: 0 = Ses Açık)
        _isMuted = PlayerPrefs.GetInt("MUTED", 0) == 1;
        ApplyAudioState();
        UpdateButtonVisual();
    }

    public void ToggleMute()
    {
        _isMuted = !_isMuted;
        PlayerPrefs.SetInt("MUTED", _isMuted ? 1 : 0);
        PlayerPrefs.Save(); // Değişiklikleri kaydet
        
        ApplyAudioState();
        UpdateButtonVisual();
    }

    private void ApplyAudioState()
    {
        AudioListener.volume = _isMuted ? 0 : 1; // Ters mantık düzeltildi
    }

    private void UpdateButtonVisual()
    {
        _muteButtonImage.sprite = _isMuted ? _soundOffSprite : _soundOnSprite;
    }
}