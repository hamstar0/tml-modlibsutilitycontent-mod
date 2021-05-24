﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ModLibsUtilityContent.Tiles {
	/// <summary>
	/// Supplies a tile type for coal items. Helps generalize their use as an inter-mod standardized item.
	/// </summary>
	public class CoalTile : ModTile {
		/// @private
		public override void SetDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			//Main.tileBlockLight[Type] = true;
			//Main.tileLighted[Type] = true;
			this.dustType = DustID.Granite;
			this.drop = ItemID.Coal;
			this.AddMapEntry( new Color( 64, 48, 64 ) );
		}

		/// @private
		public override void NumDust( int i, int j, bool fail, ref int num ) {
			num = fail ? 1 : 3;
		}
	}
}

