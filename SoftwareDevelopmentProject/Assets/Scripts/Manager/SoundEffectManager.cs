using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : SingletonMonobehaviour<SoundEffectManager>
{
    public enum EnumSound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver,
    }

    private AudioSource audioSource;
    private Dictionary<EnumSound, AudioClip> soundAudioClipDictionary;

    private float volume = 0.5f;

    //===========================================================================
    protected override void Awake()
    {
        Singleton();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        soundAudioClipDictionary = new Dictionary<EnumSound, AudioClip>();
        foreach (EnumSound sound in System.Enum.GetValues(typeof(EnumSound)))
        {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }

        BuildingManager.Instance.OnPlaceBuilding += BuildingManager_OnPlaceBuildingHandler;
    }

    private void OnDestroy()
    {
        BuildingManager.Instance.OnPlaceBuilding -= BuildingManager_OnPlaceBuildingHandler;
    }

    //===========================================================================
    public void PlaySound(EnumSound sound)
    {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound], volume);
    }

    //===========================================================================
    private void BuildingManager_OnPlaceBuildingHandler(object sender, System.EventArgs e)
    {
        PlaySound(EnumSound.BuildingPlaced);
    }

    private void HealthSystem_OnDestroyHandler(object sender, System.EventArgs e)
    {
        PlaySound(EnumSound.BuildingDestroyed);
    }

    public void IncreaseVolume()
    {
        volume += 0.05f;
        volume = Mathf.Clamp01(volume);
    }

    public void DecreaseVolume()
    {
        volume -= 0.05f;
        volume = Mathf.Clamp01(volume);
    }

    public float GetVolume() { return volume; }
}