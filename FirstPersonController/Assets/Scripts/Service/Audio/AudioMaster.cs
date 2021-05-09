using System.Collections.Generic;
using UnityEngine;

namespace VHS.Audio
{
    [CreateAssetMenu(fileName = "AudioMaster", menuName = "FirstPersonController/Audio/AudioMaster", order = 0)]
    public class AudioMaster : ScriptableObject
    {
        public static string FilePath
        {
            get { return "Audio/Master/AudioMaster.asset"; }
        }

        public List<AudioClip> _seAudioClips = new List<AudioClip>();
    }
}
