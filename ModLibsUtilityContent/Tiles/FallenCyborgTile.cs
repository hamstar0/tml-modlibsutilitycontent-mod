using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace ModLibsUtilityContent.Tiles {
	public class FallenCyborgTile : ModTile {
		public delegate void KillMultiTile_Deleg( int i, int j, int frameX, int frameY );

		public delegate void MouseOverFar_Deleg(int i, int j);



		////////////////

		public KillMultiTile_Deleg KillMultiTile_Hook;

		public MouseOverFar_Deleg MouseOverFar_Hook;



		////////////////

		/// @private
		public override bool Autoload( ref string name, ref string texture ) {
			texture = ModLibsUtilityContentMod.Instance.Name + "/Tiles/FallenCyborgTile";

			return base.Autoload( ref name, ref texture );
		}

		/// @private
		public override void SetDefaults() {
			Main.tileLighted[ this.Type ] = true;
			Main.tileFrameImportant[ this.Type ] = true;
			Main.tileObsidianKill[ this.Type ] = true;
			//Main.tileNoAttach[ this.Type ] = true;

			TileObjectData.newTile.CopyFrom( TileObjectData.Style2xX );
			TileObjectData.addTile( this.Type );

			ModTranslation name = this.CreateMapEntryName();
			name.SetDefault( "Fallen Cyborg" );

			this.AddMapEntry( new Color(160, 160, 180), name );

			this.dustType = 1;
			this.disableSmartCursor = true;
			this.adjTiles = new int[] { this.Type };
		}


		/// @private
		public override void NumDust( int i, int j, bool fail, ref int num ) {
			num = fail ? 1 : 3;
		}


		////////////////

		public override void KillMultiTile( int i, int j, int frameX, int frameY ) {
			var myitem = ModContent.GetInstance<FallenCyborgTile>();

			myitem.KillMultiTile_Hook?.Invoke( i, j, frameX, frameY );
		}

		public override void MouseOverFar( int i, int j ) {
			var myitem = ModContent.GetInstance<FallenCyborgTile>();

			myitem.MouseOverFar_Hook?.Invoke( i, j );
		}
	}
}
