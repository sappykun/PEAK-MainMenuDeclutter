using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

[BepInPlugin("sappykun.PEAK_MainMenuDeclutter", "PEAK Main Menu Declutter", "1.1.0")]
public partial class Plugin : BaseUnityPlugin
{
	internal static ManualLogSource? Log;
    internal static ConfigEntry<bool>? BlockStoreButtons;
	internal static ConfigEntry<bool>? BlockDeveloperButtons;
    internal static ConfigEntry<bool>? BlockDiscordButton;
	internal static ConfigEntry<bool>? BlockSpeechBubble;
	internal static ConfigEntry<bool>? BlockCreditsButton;

	private void Awake()
	{
		Harmony val = new Harmony("MainMenuDeclutter");
		val.PatchAll();
		Log = Logger;
		Log?.LogInfo("Plugin MainMenuDeclutter is loaded!");

        BlockStoreButtons = Config.Bind("General", "BlockStoreButtons", true, "If enabled, removes the store buttons from the main menu.");
		BlockDeveloperButtons = Config.Bind("General", "BlockDeveloperButtons", true, "If enabled, removes the developer buttons from the main menu.");
		BlockDiscordButton = Config.Bind("General", "BlockDiscordButton", true, "If enabled, removes the discord button from the main menu.");
		BlockSpeechBubble = Config.Bind("General", "BlockSpeechBubble", false, "If enabled, removes the speech bubble from the main menu.");
		BlockCreditsButton = Config.Bind("General", "BlockCreditsButton", false, "If enabled, removes the credits button from the main menu.");
	}

	[HarmonyPatch(typeof(MainMenu))]
	public static class MainMenu_Patch
	{
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
		public static void Postfix(MainMenu __instance)
		{
			if (BlockStoreButtons is { Value: true })
			{
				GameObject.Find("Button_FG")?.SetActive(false);
				GameObject.Find("Button_YT")?.SetActive(false);
				GameObject.Find("Button_MM")?.SetActive(false);
			}
			
			if (BlockDeveloperButtons is { Value: true })
			{
				__instance.landfallButton?.gameObject?.SetActive(false);
				__instance.aggrocrabButton?.gameObject?.SetActive(false);
			}
			
			if (BlockDiscordButton is { Value: true })
				__instance.discordButton?.gameObject?.SetActive(false);

			if (BlockSpeechBubble is { Value: true })
				GameObject.Find("DevMessages")?.SetActive(false);

			if (BlockCreditsButton is { Value: true })
				__instance.creditsButton?.gameObject?.SetActive(false);
		}
	}
} 

