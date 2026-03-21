using System.Collections.Generic;
using UnityEngine;

namespace TilePuzzle.Audio
{
    [CreateAssetMenu(fileName = "AudioConfiguration", menuName = "Scriptable Objects/AudioConfiguration")]
    public class AudioConfiguration : ScriptableObject
    {
        [Header("Audio Groups")]
        public List<AudioGroup> audioGroups = new();
    }
}