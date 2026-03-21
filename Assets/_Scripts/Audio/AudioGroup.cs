using System;
using System.Collections.Generic;

namespace TilePuzzle.Audio
{
    [Serializable]
    public class AudioGroup
    {
        public AudioType type;
        public List<AudioFile> audioFiles;
    }
}