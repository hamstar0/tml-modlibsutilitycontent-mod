using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Utilities;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;


namespace ModLibsUtilityContent.Buffs {
	/// <summary>
	/// Invulnerability buff.
	/// </summary>
	public class DegreelessnessBuff : ModBuff {
		public bool SuppressFx = false;



		////////////////

		/// @private
		public override bool Autoload( ref string name, ref string texture ) {
			texture = ModLibsUtilityContentMod.Instance.Name + "/Buffs/DegreelessnessBuff";

			return base.Autoload( ref name, ref texture );
		}



		////////////////

		/// <summary>
		/// Shows the typical animation effect of invulnerable entities.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		/// <param name="particles"></param>
		public static void AnimateAttackBurstFX( Vector2 position, float radius, int particles ) {
			UnifiedRandom rand = Main.rand;

			for( int i = 0; i < particles; i++ ) {
				Vector2 dir = new Vector2( rand.NextFloat() - 0.5f, rand.NextFloat() - 0.5f );
				dir.Normalize();
				Vector2 dustPos = position + ( dir * rand.NextFloat() * radius );

				int dustIdx = Dust.NewDust(
					Position: dustPos,
					Width: 1,
					Height: 1,
					Type: 269,	//DustID.Sandnado,
					SpeedX: 0f,
					SpeedY: 0f,
					Alpha: 0,
					newColor: Color.White,
					Scale: 1f
				);
				Dust dust = Main.dust[dustIdx];
				dust.noGravity = true;
			}
		}



		////////////////

		/// @private
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Degreelessness Mode" );
			this.Description.SetDefault( "Power overwhelming!" );
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}


		////////////////

		/// @private
		public override void Update( Player player, ref int buffIndex ) {
			if( !ModContent.GetInstance<DegreelessnessBuff>().SuppressFx ) {
				this.ApplyFx( player );
			}

			player.immune = true;

			Timers.SetTimer( "ModLibsGodMode_P_" + player.whoAmI, 1, false, () => {
				player.immune = false;
				return true;
			} );
		}


		/// @private
		public override void Update( NPC npc, ref int buffIndex ) {
			this.ApplyFx( npc );

			//npc.immortal = true;
			npc.dontTakeDamage = true;

			Timers.SetTimer( "ModLibsGodMode_N_" + npc.whoAmI, 1, false, () => {
				//npc.immortal = false;
				npc.dontTakeDamage = false;
				return true;
			} );
		}

		////////////////

		private void ApplyFx( Entity entity ) {
			int radius = ( entity.width + entity.height ) / 2;
			DegreelessnessBuff.AnimateAttackBurstFX( entity.Center, radius, radius / 10 );
		}
	}
}
