using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class ConstantResources
    {
        // public const string FolderPath = "Assets\\Music\\";
        public const string FileExtensionMidi = ".mid";
        public const string FileExtensionMp3 = ".mp3";
        
        public static class Scenes
        {
            public const string FileBrowserScene = "FileBrowser";
            public const string MainGameScene = "MainGameScene";
        }

        public static class Strings
        {
            public static readonly Dictionary<string, Dictionary<string, string>> MainGameScene =
                new Dictionary<string, Dictionary<string, string>>
                {
                    {
                        Languages.Spanish, new Dictionary<string, string>
                        {
                            {Buttons.PlayButton, "Jugar"},
                        }
                    },
                    {
                        Languages.English, new Dictionary<string, string>
                        {
                            {Buttons.PlayButton, "Play"},
                        }
                    }
                };
        }

        public static class Languages
        {
            public const string Spanish = "Spanish";
            public const string English = "English";
        }
        
        public static class Buttons
        {
            public const string PlayButton = "btnPlay";
        }

        public static class Configuration
        {
            private const string _prefString = "Configuration=>";
            public static class Cameras
            {
                public const string PrefString = _prefString + "Cameras=>";
                public const string Perspective = "Perspective";
                public const string Orthographic = "Orthographic";
            }
            public static class MusicSheet
            {
                public const string PrefString = _prefString + "MusicSheet=>";
                public const string Original = "Original";
                public const string Matias = "Matías";
            }
        }

        public static class Records
        {
            private const string PrefStringRecords = "Records=>";
            private const string PrefStringScore = "Score=>";
            private const string PrefStringMaxStreak = "MaxStreak=>";
            private const string PrefStringPercentage = "Percentage=>";
            private const string PrefStringSongTitle = "SongTitle=>{0}";
            
            public const string Score = PrefStringRecords + PrefStringScore + PrefStringSongTitle;
            public const string MaxStreak = PrefStringRecords + PrefStringMaxStreak + PrefStringSongTitle;
            public const string Percentage = PrefStringRecords + PrefStringPercentage + PrefStringSongTitle;
        }

        public static class Logs
        {
            public static class Colors
            {
                public const string SongHolder = "#E637BF";
            }
        }
    }
}