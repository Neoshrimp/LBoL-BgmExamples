using HarmonyLib;

namespace BgmExamples
{
    public static class PInfo
    {
        // each loaded plugin needs to have a unique GUID. usually author+generalCategory+Name is good enough
        public const string GUID = "neo.lbol.examples.bgmExamples";
        public const string Name = "bgm Examples";
        public const string version = "1.0.0";
        public static readonly Harmony harmony = new Harmony(GUID);

    }
}
