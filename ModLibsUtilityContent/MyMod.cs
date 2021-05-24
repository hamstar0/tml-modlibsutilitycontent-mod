using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace ModLibsUtilityContent {
	/// @private
	partial class ModLibsUtilityContentMod : Mod {
		public static ModLibsUtilityContentMod Instance { get; private set; }



		////////////////

		public ModLibsUtilityContentMod() {
			ModLibsUtilityContentMod.Instance = this;
		}


		public override void Load() {
		}

		////

		public override void Unload() {
			try {
				LogLibraries.Alert( "Unloading mod..." );
			} catch { }

			ModLibsUtilityContentMod.Instance = null;
		}


		////////////////

		public override void AddRecipes() {
			if( ModLibsUtilityContentConfig.Instance.AddCrimsonLeatherRecipe ) {
				var vertebraeToLeather = new ModRecipe( this );

				vertebraeToLeather.AddIngredient( ItemID.Vertebrae, 5 );
				vertebraeToLeather.SetResult( ItemID.Leather );
				vertebraeToLeather.AddRecipe();
			}
		}
	}
}
