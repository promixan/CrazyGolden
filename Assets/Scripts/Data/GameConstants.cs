namespace Data
{
    public static class GameConstants
    {
        public static class Audio
        {
            public const string GeneralKey = "VOL_GENERAL";
            public const string MusicKey = "VOL_MUSIC";
            public const string SfxKey = "VOL_SFX";

            public enum VolumeType { General, Music, Sfx }
        }
    
        public static class BestScores
        {
            public const int MaxNumber = 30;
        }

        public static class Player
        {
            public const int MaxPlayerNameLength = 10;
        }
    }
}
