using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;


namespace ModLibsUtilityContent.Items {
	public partial class MagitechScrapItem : ModItem {
		/// @private
		public override string Texture => ModLibsUtilityContentMod.Instance.Name + "/Items/MagitechScrapItem";



		////////////////

		/// @private
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Magitech Scrap" );
			this.Tooltip.SetDefault( "Assorted machine parts with assorted enchantments" );
		}

		/// @private
		public override void SetDefaults() {
			this.item.width = 12;
			this.item.height = 12;
			this.item.maxStack = 1;
			this.item.consumable = true;
			this.item.value = Item.buyPrice( 0, 3, 0, 0 );
			//this.item.UseSound = SoundID.Item108;
			this.item.rare = ItemRarityID.Orange;
		}
	}
}
