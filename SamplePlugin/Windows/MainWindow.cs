using System;
using System.Globalization;
using System.Numerics;
using Dalamud.Game.Inventory;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using ImGuiNET;
using static SamplePlugin.Services;
using Lumina.Extensions;
using Lumina.Excel.GeneratedSheets;
using Lumina.Data.Parsing;
using FFXIVClientStructs.FFXIV.Client.UI;
using Dalamud.IoC;

namespace SamplePlugin.Windows;

public class MainWindow : Window, IDisposable
{
    private IDalamudTextureWrap GoatImage;
    private Plugin Plugin;

    private InventoryType InventoryType { get; init; }
    private InventoryItem inventoryItem { get; init; }
    private InventoryContainer InventoryContainer { get; init; }
    private PouchInventoryItem[] PouchInventoryItems { get; init; }

    public MainWindow(Plugin plugin, IDalamudTextureWrap goatImage) : base(
        "My Amazing Window", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        this.GoatImage = goatImage;
        this.Plugin = plugin;
    }

    public void Dispose()
    {
        this.GoatImage.Dispose();
    }

    private InventoryManager InventoryManager { get; init; }

    unsafe public override void Draw()
    {
        ImGui.Text($"The random config bool is {this.Plugin.Configuration.SomePropertyToBeSavedAndWithADefault}");

        if (ImGui.Button("Show Settings"))
        {
            this.Plugin.DrawConfigUI();
        }
        InventoryItem  *i = InventoryManager.Instance()->GetInventorySlot(inventoryType: InventoryType.Inventory1, 0);
        ImGui.Spacing();
        ImGui.Text("Have a goat:");
        ImGui.Text(InventoryManager.Instance()->GetGil().ToString());
        var itemId = InventoryManager.Instance()->Inventories->GetInventorySlot(0)->GetItemId();
        ImGui.Text(itemId.ToString());
        var lumina = DataManager.GetExcelSheet<Item>().GetRow((uint)itemId);
        ImGui.Text(lumina.Singular);
        ImGui.Indent(55);
        ImGui.Image(this.GoatImage.ImGuiHandle, new Vector2(this.GoatImage.Width, this.GoatImage.Height));
        ImGui.Unindent(55);
    }
}
