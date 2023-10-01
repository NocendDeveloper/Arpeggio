using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class ConstantResources
    {
        public const string FolderPath = "P:\\Unity\\Arpeggio\\Assets\\Music\\";
        public const string FileExtensionMidi = ".mid";
        public const string FileExtensionMp3 = ".mp3";
        
        public static class Scenes
        {
            public const string FileBrowserScene = "FileBrowser";
            public const string MainGameScene = "MainGameScene";
            public const string LoadingScene = "LoadingScene";
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
    }
}