using Dalamud.IoC;
using FFXIVClientStructs.FFXIV.Client.Game;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalamud.Plugin.Services;

namespace SamplePlugin
{
    internal class Services
    {
        [PluginService] public static IChatGui ChatGui { get; private set; } = null!;
        [PluginService] public static IDataManager DataManager { get; private set; } = null!;
        public static void Initialize(DalamudPluginInterface pluginInterface)
        {
            pluginInterface.Create<Services>();
        }
    }
}
