using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VHS.Audio
{
    [CreateAssetMenu(fileName = "AudioMaster", menuName = "FirstPersonController/Audio/AudioMaster", order = 0)]
    public class AudioMaster : ScriptableObject
    {
        public static string FilePath
        {
            get { return "Audio/Master/AudioMaster.asset"; }
        }

        public List<AssetReference> _bgmAudioClips = new List<AssetReference>();
        public List<AssetReference> _seAudioClips = new List<AssetReference>();
    }
}
