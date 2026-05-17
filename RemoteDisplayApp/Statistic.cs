using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteDisplayApp
{

    public static class Statistic
    {
        private static long capturedFramesThisSecond = 0;
        private static long sentFramesThisSecond = 0;
        private static long frameSizeSum = 0;
        private static long encodeTimes = 0;
        private static int decodeCount = 0;

        public static int CapturedFPS { get; private set; }
        public static int SentFPS { get; private set; }
        public static long NetworkUsage { get; private set; }
        public static long AverageFrameSize { get; private set; }
        public static long AverageDecodeTime { get; private set; }

        private static DateTime lastResetTime = DateTime.Now;

        public static void RecordCapturedFrame()
        {
            Interlocked.Increment(ref capturedFramesThisSecond);
        }

        public static void RecordSentFrame(byte[] frameData)
        {
            Interlocked.Increment(ref sentFramesThisSecond);
            Interlocked.Add(ref frameSizeSum, frameData.Length);
        }

        public static void RecordEncodeTime(long milliseconds)
        {
            Interlocked.Add(ref encodeTimes, milliseconds);
            Interlocked.Increment(ref decodeCount);
        }
        public static void UpdateStatistics()
        {
            var now = DateTime.Now;
            var elapsed = (now - lastResetTime).TotalSeconds;

            if (elapsed >= 1.0)
            {

                CapturedFPS = (int)(capturedFramesThisSecond / elapsed);
                SentFPS = (int)(sentFramesThisSecond / elapsed);
                NetworkUsage = (long)(frameSizeSum / elapsed);

                AverageFrameSize = sentFramesThisSecond > 0
                    ? frameSizeSum / sentFramesThisSecond
                    : 0;

                AverageDecodeTime = decodeCount > 0
                    ? encodeTimes / decodeCount
                    : 0;


                capturedFramesThisSecond = 0;
                sentFramesThisSecond = 0;
                frameSizeSum = 0;
                encodeTimes = 0;
                decodeCount = 0;
                lastResetTime = now;
            }
        }
        }
}
