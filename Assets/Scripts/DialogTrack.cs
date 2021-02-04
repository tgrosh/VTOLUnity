using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Assets.Scripts
{
    [TrackBindingType(typeof(Dialog))]
    [TrackClipType(typeof(DialogClip))]
    public class DialogTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<DialogTrackMixer>.Create(graph, inputCount);
        }
    }
}