using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Draw;
using ModLibsGeneral.Libraries.UI;


namespace ModLibsUtilityContent.NPCs {
	public enum PropType {
		None = -1,
		Item,
		NPC,
		Projectile,
		//Gore,
		//Buff,
		//Cloud,
		//Extra
	}



	/// <summary>
	/// Implements an NPC able to have its texture and size adjusted dynamically. Is completely passive.
	/// </summary>
	public class PropNPC : ModNPC {
		public static int Create( PropType type, int thingId, Vector2 position, int frame ) {
			string texture = "";
			int frames = 0;

			switch( type ) {
			case PropType.Item:
				texture = "Terraria/Item_"+thingId;
				frames = Main.itemFrame[thingId];
				break;
			case PropType.NPC:
				texture = "Terraria/Npc_"+thingId;
				frames = Main.npcFrameCount[thingId];
				break;
			case PropType.Projectile:
				texture = "Terraria/Projectile_"+thingId;
				frames = Main.projFrames[thingId];
				break;
			/*case PropType.Gore:
				texture = "Terraria/Gore_"+thingId;
				break;
			case PropType.Buff:
				texture = "Terraria/Buff_"+thingId;
				break;
			case PropType.Cloud:
				texture = "Terraria/Cloud_"+thingId;
				break;
			case PropType.Extra:
				texture = "Terraria/Extra_"+thingId;
				break;*/
			}

			//

			int npcWho = NPC.NewNPC(
				X: (int)position.X,
				Y: (int)position.Y,
				Type: ModContent.NPCType<PropNPC>()
			);
			if( npcWho < 0 ) {
				return npcWho;
			}

			//

			NPC npc = Main.npc[npcWho];
			var mynpc = npc.modNPC as PropNPC;

			mynpc.SetTexture( texture, frames );
			mynpc.CurrentFrame = frame;

			//

			return npcWho;
		}



		////////////////


		public int CurrentFrame = 0;

		////////////////

		/// <summary></summary>
		public override string Texture => this._Texture;

		private string _Texture = $"{ModLibsUtilityContentMod.Instance.Name}/NPCs/PropNPC";

		private int TotalTextureFrames = 1;



		////////////////
		
		/// @private
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "A Prop" );
		}

		/// @private
		public override void SetDefaults() {
			this.npc.aiStyle = 0;
			this.npc.width = 16;
			this.npc.height = 16;
			this.npc.damage = 0;
			this.npc.defense = 0;
			this.npc.lifeMax = 5;
			this.npc.HitSound = SoundID.NPCHit1;
			this.npc.DeathSound = SoundID.NPCDeath1;
			this.npc.npcSlots = 0f;
			this.npc.noGravity = true;

			this.npc.friendly = true;
			this.npc.lavaImmune = true;
			this.npc.dontTakeDamage = true;
		}

		////

		/// @private
		public override bool? CanBeHitByItem( Player player, Item item ) {
			return false;
		}

		/// @private
		public override bool? CanBeHitByProjectile( Projectile projectile ) {
			return false;
		}

		/// @private
		public override bool? CanHitNPC( NPC target ) {
			return false;
		}

		/// @private
		public override bool CanHitPlayer( Player target, ref int cooldownSlot ) {
			return false;
		}


		////////////////

		/// @private
		public override bool CheckActive() {
			return false;
		}


		////////////////

		/// @private
		public override void FindFrame( int frameHeight ) {
			this.npc.frame = new Rectangle( 0, 0, this.npc.width, this.npc.height );
		}


		////////////////

		/// @private
		public override bool PreDraw( SpriteBatch sb, Color drawColor ) {
			Texture2D tex = ModContent.GetTexture( this.Texture );
			Vector2 scrPos = UIZoomLibraries.ConvertToScreenPosition( npc.Center, null, true );

			int currFrame = this.CurrentFrame % this.TotalTextureFrames;
			int frameHeight = tex.Height / this.TotalTextureFrames;
			var frameArea = new Rectangle( 0, frameHeight * currFrame, tex.Width, frameHeight );
			var origin = new Vector2( tex.Width / 2, frameHeight / 2 );

			//

			sb.Draw(
				texture: tex,
				position: scrPos,
				sourceRectangle: frameArea,
				color: drawColor,
				rotation: npc.rotation,
				origin: origin,
				scale: npc.scale,
				effects: npc.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
				layerDepth: 0f
			);

			//
			
			if( ModLibsUtilityContentConfig.Instance.DebugModeInfo ) {
				var rect = new Rectangle(
					(int)( scrPos.X - ((npc.scale * (float)tex.Width) / 2f) ),
					(int)( scrPos.Y - ((npc.scale * (float)frameHeight) / 2f) ),
					(int)( npc.scale * (float)tex.Width ),
					(int)( npc.scale * (float)frameHeight )
				);
				DrawLibraries.DrawBorderedRect( sb, Color.Transparent, Color.Red, rect, 2 );
			}

			//

			return false;
		}


		////////////////

		/// <summary></summary>
		/// <param name="texturePath"></param>
		/// <param name="frames"></param>
		public void SetTexture( string texturePath, int frames ) {
			this._Texture = texturePath;
			this.TotalTextureFrames = frames;
		}
	}
}
