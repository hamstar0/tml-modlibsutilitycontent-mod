using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace ModLibsUtilityContent {
	/// <summary>
	/// Defines config settings.
	/// </summary>
	[Label( "Mod Libs - Utility Content - Settings" )]
	public class ModLibsUtilityContentConfig : ModConfig {
		/// <summary>
		/// Gets the stack-merged singleton instance of this config file.
		/// </summary>
		public static ModLibsUtilityContentConfig Instance => ModContent.GetInstance<ModLibsUtilityContentConfig>();



		////////////////

		/// @private
		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		/// <summary>
		/// Shows misc. debug info.
		/// </summary>
		[Label( "Debug Mode - Display misc info" )]
		[Tooltip( "Shows misc. debug info." )]
		public bool DebugModeInfo { get; set; } = false;

		////

		/// <summary>
		/// Coal items can be placed like tiles.
		/// </summary>
		[ReloadRequired]
		[Label( "Coal items can be placed like tiles" )]
		[DefaultValue( true )]
		public bool CoalAsTile { get; set; } = true;

		/// <summary>
		/// Adds crimson biome alternatve recipe for leather.
		/// </summary>
		[Label( "Add crimson biome leather recipe" )]
		[Tooltip( "Adds crimson biome alternatve recipe for leather." )]
		[DefaultValue( true )]
		public bool AddCrimsonLeatherRecipe { get; set; } = true;
	}
}
