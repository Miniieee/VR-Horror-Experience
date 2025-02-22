using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable]
public class PostProcessingControlBehaviour : PlayableBehaviour
{
    public AnimationCurve IntensityCurve;

    private Volume volume;
    private Bloom bloom;
    private float initialIntensity;

    public override void OnGraphStart(Playable playable)
    {
        volume = GameObject.FindObjectOfType<Volume>();
        if (volume != null)
        {
            if (volume.profile.TryGet(out bloom))
            {
                initialIntensity = bloom.intensity.value;
            }
        }
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        if (bloom != null)
        {
            bloom.intensity.value = initialIntensity;
        }
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (bloom != null && IntensityCurve != null)
        {
            double time = playable.GetTime();
            float curveValue = IntensityCurve.Evaluate((float)time);
            bloom.intensity.value = curveValue;
        }
    }
}


