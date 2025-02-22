using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class InteractionTriggers : MonoBehaviour
{
    [SerializeField] private Volume postProcessVolume;
    [SerializeField] private int nextSceneIndex;
 
    private VolumeProfile _volumeProfile;
    private Bloom _bloom;
    private Vignette _vignette;

    private void Start()
    {
        _volumeProfile = postProcessVolume.profile;
        if (!_volumeProfile.TryGet(out _bloom))
        {
            Debug.LogError("Bloom not found in PostProcessVolume");
        }

        if (!_volumeProfile.TryGet(out _vignette))
        {
            Debug.LogError("Vignette not found in PostProcessVolume");
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void EnableVignette()
    {
        if (_vignette != null)
        {
            _vignette.active = true;
        }
    }
}
