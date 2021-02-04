using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogClip : PlayableAsset
{
    public DialogMessage dialog;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogBehaviour>.Create(graph);

        DialogBehaviour dialogBehaviour = playable.GetBehaviour();
        dialogBehaviour.dialog = dialog;

        return playable;
    }
}
