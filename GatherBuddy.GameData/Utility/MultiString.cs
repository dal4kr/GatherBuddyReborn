using System;
using Dalamud.Game;
using Dalamud.Plugin.Services;
using Lumina.Excel.Sheets;
using Lumina.Text.ReadOnly;

namespace GatherBuddy.Utility;

public readonly struct MultiString(string en, string de, string fr, string jp, string ko)
{
    public static string ParseSeStringLumina(ReadOnlySeString? luminaString)
        => luminaString?.ExtractText() ?? string.Empty;

    public readonly string English  = en;
    public readonly string German   = de;
    public readonly string French   = fr;
    public readonly string Japanese = jp;
    public readonly string Korean   = ko;

    public string this[ClientLanguage lang]
        => Name(lang);

    public override string ToString()
        => Name(ClientLanguage.English);

    public string ToWholeString()
        => $"{English}|{German}|{French}|{Japanese}|{Korean}";


    public static MultiString FromPlaceName(IDataManager gameData, uint id)
    {
        var en = ParseSeStringLumina(gameData.GetExcelSheet<PlaceName>(ClientLanguage.English).GetRowOrDefault(id)?.Name);
        var de = ParseSeStringLumina(gameData.GetExcelSheet<PlaceName>(ClientLanguage.German).GetRowOrDefault(id)?.Name);
        var fr = ParseSeStringLumina(gameData.GetExcelSheet<PlaceName>(ClientLanguage.French).GetRowOrDefault(id)?.Name);
        var jp = ParseSeStringLumina(gameData.GetExcelSheet<PlaceName>(ClientLanguage.Japanese).GetRowOrDefault(id)?.Name);
        var ko = ParseSeStringLumina(gameData.GetExcelSheet<PlaceName>(ClientLanguage.Korean).GetRowOrDefault(id)?.Name);
        return new MultiString(en, de, fr, jp, ko);
    }

    public static MultiString FromItem(IDataManager gameData, uint id)
    {
        var en = ParseSeStringLumina(gameData.GetExcelSheet<Item>(ClientLanguage.English).GetRowOrDefault(id)?.Name);
        var de = ParseSeStringLumina(gameData.GetExcelSheet<Item>(ClientLanguage.German).GetRowOrDefault(id)?.Name);
        var fr = ParseSeStringLumina(gameData.GetExcelSheet<Item>(ClientLanguage.French).GetRowOrDefault(id)?.Name);
        var jp = ParseSeStringLumina(gameData.GetExcelSheet<Item>(ClientLanguage.Japanese).GetRowOrDefault(id)?.Name);
        var ko = ParseSeStringLumina(gameData.GetExcelSheet<Item>(ClientLanguage.Korean).GetRowOrDefault(id)?.Name);
        return new MultiString(en, de, fr, jp, ko);
    }

    private string Name(ClientLanguage lang)
        => lang switch
        {
            ClientLanguage.English  => English,
            ClientLanguage.German   => German,
            ClientLanguage.Japanese => Japanese,
            ClientLanguage.French   => French,
            ClientLanguage.Korean   => Korean,
            _                       => throw new ArgumentException(),
        };

    public static readonly MultiString Empty = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
}
