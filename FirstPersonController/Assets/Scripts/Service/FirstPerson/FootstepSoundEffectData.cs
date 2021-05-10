using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "FootstepSoundEffectData", menuName = "FirstPersonController/FirstPerson/FootstepSoundEffectData", order = 0)]
public class FootstepSoundEffectData : ScriptableObject
{
    public List<AssetReference> _defaultFootstepSeAudioClips = new List<AssetReference>();

    public string[] NameArray
    {
        get
        {
            var list = new string[_defaultFootstepSeAudioClips.Count];
            for (int i = 0; i < list.Length; i++)
            {
                // FIXME: editorAsset
                list[i] = _defaultFootstepSeAudioClips[i].editorAsset.name;
            }
            return list;
        }
    }
}
