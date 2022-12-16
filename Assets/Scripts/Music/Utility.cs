namespace Music
{
    public static class Utility
    {
        //these two are the same function.
        public static float BeatsToSeconds(float b, float bpm) => 60/(b*bpm);
        public static float SecondsToBeats(float s, float bpm) => 60/(s*bpm);
    }
}