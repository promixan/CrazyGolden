using UnityEngine;

public static class GameConstants
{
    public static class Audio
    {
        public const string GENERAL_KEY = "VOL_GENERAL";
        public const string MUSIC_KEY = "VOL_MUSIC";
        public const string SFX_KEY = "VOL_SFX";

        public enum VolumeType { General, Music, SFX }
    }
}
