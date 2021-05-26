using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsUtilityContent.Tiles;


namespace ModLibsUtilityContent {
	/// @private
	class ModLibsUCItem : GlobalItem {
		public override void SetDefaults( Item item ) {
//DataStore.Add( DebugLibraries.GetCurrentContext()+"_"+item.whoAmI+":"+item.type+"_A", 1 );
			if( item.type == ItemID.Coal ) {
				if( ModLibsUtilityContentConfig.Instance.CoalAsTile ) {
					item.maxStack = 999;
					item.rare = ItemRarityID.Green;
					item.value = 1000;

					item.useStyle = ItemUseStyleID.SwingThrow;
					item.useTurn = true;
					item.useAnimation = 15;
					item.useTime = 10;
					item.autoReuse = true;

					item.consumable = true;
					item.createTile = ModContent.TileType<CoalTile>();
				}
			}

			base.SetDefaults( item );
//DataStore.Add( DebugLibraries.GetCurrentContext()+"_"+item.whoAmI+":"+item.type+"_B", 1 );
		}
	}
}
