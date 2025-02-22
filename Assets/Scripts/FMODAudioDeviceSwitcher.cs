using UnityEngine;
using FMODUnity;
using FMOD;

public class FMODAudioDeviceSwitcher : MonoBehaviour
{
    private void OnEnable()
    {
        AudioSettings.OnAudioConfigurationChanged += OnAudioConfigurationChanged;
    }

    private void OnDisable()
    {
        AudioSettings.OnAudioConfigurationChanged -= OnAudioConfigurationChanged;
    }

    private void OnAudioConfigurationChanged(bool deviceWasChanged)
    {
        if (deviceWasChanged)
        {
            UpdateFMODOutputDevice();
        }
    }

    private void UpdateFMODOutputDevice()
    {
        FMOD.System system;
        RuntimeManager.StudioSystem.getCoreSystem(out system);

        system.setOutput(OUTPUTTYPE.AUTODETECT);
        system.update(); // Force FMOD system to update and apply the new output type

        int defaultDriver = 0;
        system.setDriver(defaultDriver);
    }
}