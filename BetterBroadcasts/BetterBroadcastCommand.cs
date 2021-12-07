namespace BetterBroadcasts
{
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using System;
    using System.Linq;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class BetterBroadcastCommand : ICommand, IUsageProvider
    {
        public string[] Usage { get; } = new string[]
        {
            "Duration",
            "Message"
        };
        public string Command => "BetterBroadcast";

        public string[] Aliases => new string[] { "bbc", "betterbc" };

        public string Description => "Broadcasts a message with replaced variables";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Plugin.Singleton.Config.UniquePermission && !sender.CheckPermission(global::PlayerPermissions.Broadcasting, out response))
            {
                return false;
            }
            else if (Plugin.Singleton.Config.UniquePermission && !sender.CheckPermission("bbc.broadcast"))
            {
                response = "Missing permission: bbc.broadcast";
                return false;
            }
            if (arguments.Count < 2)
            {
                response = "To execute this command provide at least 2 arguments!\nUsage: " + arguments.Array[0] + " " + this.DisplayCommandUsage();
                return false;
            }
            if (!ushort.TryParse(arguments.At(0), out ushort num) || num < 1)
            {
                response = string.Concat(new string[]
                {
                    "Invalid argument for duration: ",
                    arguments.At(0),
                    " Usage: ",
                    arguments.Array[0],
                    " ",
                    this.DisplayCommandUsage()
                });
                return false;
            }
            string text = Utils.RAUtils.FormatArguments(arguments, 1);
            text = Plugin.Singleton.Config.BroadcastVariables.Aggregate(text, (result, s) => result.Replace(s.Key, s.Value));
            Map.Broadcast(ushort.Parse(arguments.At(0)), text, global::Broadcast.BroadcastFlags.Normal, false);
            Log.Debug("Broadcast content = " + text, Plugin.Singleton.Config.DebugMode);
            response = "Broadcast sent.";
            return true;
        }
    }
}
