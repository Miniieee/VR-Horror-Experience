using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class PostProcessingControlClip : PlayableAsset, ITimelineClipAsset
{
    public PostProcessingControlBehaviour template = new PostProcessingControlBehaviour();
    [SerializeField] private AnimationCurve _intensityCurve = AnimationCurve.Linear(0, 1, 1, 1);

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public AnimationCurve IntensityCurve
    {
        get => _intensityCurve;
        set => _intensityCurve = value;
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<PostProcessingControlBehaviour>.Create(graph, template);
        playable.GetBehaviour().IntensityCurve = _intensityCurve;
        return playable;
    }
}
