namespace BetterBroadcasts
{
    using BetterBroadcasts.BroadcastObjects;
    using Exiled.API.Interfaces;
    using System.Collections.Generic;
    using System.ComponentModel;

    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool DebugMode { get; private set; } = false;
        [Description("Plugin is using basegame Broadcast Permission. If set to true, you need to grant bbc.broadcast permission for command to work")]
        public bool UniquePermission { get; private set; } = false;
        [Description("When you use variables in BetterBroadcast Command they are replaced with provided text")]
        public Dictionary<string, string> BroadcastVariables { get; private set; } = new Dictionary<string, string>
        {
            { "%variable%", "this is a test variable" }
        };
        public List<TimedBroadcast> TimedBroadcasts { get; private set; } = new List<TimedBroadcast>
        {
            new TimedBroadcast
            {
                Text = "Test Timed Broadcast",
                Delay = 10f,
                Duration = 10
            }
        };
        public List<RepeatedBroadcast> RepeatedBroadcasts { get; private set; } = new List<RepeatedBroadcast>
        {
            new RepeatedBroadcast
            {
                Text = "Test Repeated Broadcast",
                Interval = 100f,
                Duration = 10
            }
        };
    }
}
