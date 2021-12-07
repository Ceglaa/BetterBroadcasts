namespace BetterBroadcasts
{
    using Exiled.API.Features;
    using System;
    using Server = Exiled.Events.Handlers.Server;
    public class Plugin : Plugin<Config>
    {
        private BroadcastHandler broadcastHandler;

        public override string Author => "Cegla";
        public override string Name => "BetterBroadcast";
        public override string Prefix => "bbc";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(4, 0, 0);

        public static Plugin Singleton;
        public override void OnEnabled()
        {
            Singleton = this;
            RegisterEvents();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            UnregisterEvents();
            Singleton = null;
            base.OnDisabled();
        }
        private void RegisterEvents()
        {
            broadcastHandler = new BroadcastHandler();
            Server.RoundStarted += broadcastHandler.OnRoundStarted;
            Server.RoundEnded += broadcastHandler.OnRoundEnded;
        }
        private void UnregisterEvents()
        {
            Server.RoundStarted -= broadcastHandler.OnRoundStarted;
            Server.RoundEnded -= broadcastHandler.OnRoundEnded;
            broadcastHandler = null;
        }
    }
}
