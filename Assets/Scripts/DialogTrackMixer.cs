using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Assets.Scripts
{
    public class DialogTrackMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Dialog dialog = playerData as Dialog;
            List<DialogMessage> dialogs = new List<DialogMessage>();

            int inputCount = playable.GetInputCount();
            for (int i = 0; i< inputCount; i++)
            {
                if (playable.GetInputWeight(i) > 0f)
                {
                    DialogBehaviour dialogBehaviour = ((ScriptPlayable<DialogBehaviour>)playable.GetInput(i)).GetBehaviour();
                    dialogs.Add(dialogBehaviour.dialog);
                }
            }


            dialog.dialogs = dialogs;
        }
    }
}