using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private float musicVolume = 0.5f;
    private AudioSource audioSource;

    //===========================================================================
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //===========================================================================
    public void IncreaseVolume()
    {
        musicVolume += 0.05f;
        musicVolume = Mathf.Clamp01(musicVolume);
        audioSource.volume = musicVolume;
    }

    public void DecreaseVolume()
    {
        musicVolume -= 0.05f;
        musicVolume = Mathf.Clamp01(musicVolume);
        audioSource.volume = musicVolume;
    }

    public float GetVolume() { return musicVolume; }
}
