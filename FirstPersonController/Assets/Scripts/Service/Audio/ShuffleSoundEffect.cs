using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VHS.Audio
{
    // MVPでいうModel?
    public class ShuffleSoundEffect
    {
        AudioService _audioService;
        AudioSource _audioSource;
        string[] _seList;
        List<string> _candidate = new List<string>();

        public ShuffleSoundEffect(AudioService audioService, string[] seList, AudioSource audioSource = default)
        {
            _audioService = audioService;
            _audioSource = audioSource;
            _seList = seList;

            Shuffle();
        }

        public void PlaySe()
        {
            _audioService.PlaySe(_candidate[0], _audioSource);
            _candidate.RemoveAt(0);

            if (0 < _candidate.Count) return;
            Shuffle();
        }

        public void Shuffle()
        {
            _candidate = _seList.OrderBy(i => System.Guid.NewGuid()).ToList();
        }
    }
}
