namespace BetterBroadcasts
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;
    using System.Collections.Generic;

    internal sealed class BroadcastHandler
    {
        public static List<CoroutineHandle> BroadcastCoroutines = new List<CoroutineHandle>();
        public void OnRoundStarted()
        {
            foreach (var timedbc in Plugin.Singleton.Config.TimedBroadcasts)
            {
                Log.Debug($"Dleay: {timedbc.Delay}, Duration: {timedbc.Duration}, Text: {timedbc.Text}", Plugin.Singleton.Config.DebugMode);
                Timing.CallDelayed(timedbc.Delay, () =>
                {
                    Map.Broadcast(timedbc.Duration, timedbc.Text);
                    Log.Debug($"Timed Broadcast fired: Dleay: {timedbc.Delay}, Duration: {timedbc.Duration}, Text: {timedbc.Text}", Plugin.Singleton.Config.DebugMode);
                });
            }
            foreach (var repeatedbc in Plugin.Singleton.Config.RepeatedBroadcasts)
            {
                Log.Debug($"Interval: {repeatedbc.Interval}, Duration: {repeatedbc.Duration}, Text: {repeatedbc.Text}", Plugin.Singleton.Config.DebugMode);
                BroadcastCoroutines.Add(Timing.RunCoroutine(CallBroadcastRepetitively(repeatedbc.Interval, repeatedbc.Duration, repeatedbc.Text)));

            }
        }
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            foreach (CoroutineHandle coroutine in BroadcastCoroutines)
            {
                Timing.KillCoroutines(coroutine);
            }
            BroadcastCoroutines.Clear();
        }
        public IEnumerator<float> CallBroadcastRepetitively(float interval, ushort duration, string text)
        {
            for (; ; )
            {
                Map.Broadcast(duration, text, global::Broadcast.BroadcastFlags.Normal, true);
                Log.Debug($"Repeated Broadcast fired: Interval: {interval}, Duration: {duration}, Text: {text}", Plugin.Singleton.Config.DebugMode);
                yield return Timing.WaitForSeconds(interval);
            }
        }
    }
}
