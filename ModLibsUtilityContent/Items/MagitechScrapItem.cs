using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ModLibsUtilityContent.Items {
	/// <summary></summary>
	public partial class MagitechScrapItem : ModItem {
		public delegate void SetDefaults_Deleg( MagitechScrapItem myitem );

		public delegate void ModifyTooltips_Deleg( MagitechScrapItem myitem, List<TooltipLine> tooltips );

		public delegate bool CanRightClick_Deleg( MagitechScrapItem myitem );



		////////////////

		public SetDefaults_Deleg SetDefaults_Hook;

		public ModifyTooltips_Deleg ModifyTooltips_Hook;

		public CanRightClick_Deleg CanRightClick_Hook;



		////////////////

		/// @private
		public override string Texture => ModLibsUtilityContentMod.Instance.Name + "/Items/MagitechScrapItem";



		////////////////

		/// @private
		public override void SetDefaults() {
			this.item.width = 12;
			this.item.height = 12;
			this.item.maxStack = 1;
			this.item.consumable = true;
			this.item.value = Item.buyPrice( 0, 3, 0, 0 );
			//this.item.UseSound = SoundID.Item108;
			this.item.rare = ItemRarityID.Orange;

			//

			var myitem = ModContent.GetInstance<MagitechScrapItem>();

			myitem.SetDefaults_Hook?.Invoke( this );
		}

		////

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			var myitem = ModContent.GetInstance<MagitechScrapItem>();

			myitem.ModifyTooltips_Hook?.Invoke( this, tooltips );
		}

		public override bool CanRightClick() {
			var myitem = ModContent.GetInstance<MagitechScrapItem>();

			return myitem.CanRightClick_Hook?.Invoke( this ) ?? false;
		}
	}
}
