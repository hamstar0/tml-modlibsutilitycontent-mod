using System;
using Microsoft.Xna.Framework;
using ModLibsCore.Libraries.Debug;
using ModLibsUtilityContent.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ModLibsUtilityContent.Commands {
	/// @private
	public class CreatePropCommand : ModCommand {
		public static bool ParseArguments(
					string[] args,
					out PropType propType,
					out int propSubType,
					out string result ) {
			if( args.Length != 3 ) {
				propType = PropType.None;
				propSubType = -1;
				result = "Invalid number of arguments.";
				return false;
			}

			//

			if( !int.TryParse(args[1], out int propTypeRaw) ) {
				propType = PropType.None;
				propSubType = -1;
				result = "Invalid type argument.";
				return false;
			}
			if( propTypeRaw < 0 || propTypeRaw > 6 ) {
				propType = PropType.None;
				propSubType = -1;
				result = "Type argument out of range.";
				return false;
			}

			propType = (PropType)propTypeRaw;

			//

			if( !int.TryParse(args[2], out propSubType) ) {
				propSubType = -1;
				result = "Invalid sub type argument.";
				return false;
			}

			//

			int maxSubType = -1;

			switch( propType ) {
			case PropType.Item:
				maxSubType = ItemLoader.ItemCount;
				break;
			case PropType.NPC:
				maxSubType = NPCLoader.NPCCount;
				break;
			case PropType.Projectile:
				maxSubType = ProjectileLoader.ProjectileCount;
				break;
			/*case PropType.Gore:
				maxSubType = GoreLoader.GoreCount;
				break;
			case PropType.Buff:
				maxSubType = BuffLoader.BuffCount;
				break;
			case PropType.Cloud:
				maxSubType = ItemLoader.ItemCount;
				break;
			case PropType.Extra:
				maxSubType = ItemLoader.ItemCount;
				break;*/
			}

			if( propSubType < 0 || propTypeRaw > 6 ) {
				result = "Sub type argument out of range.";
				return false;
			}

			//

			result = "Success.";
			return true;
		}



		////////////////

		/// @private
		public override CommandType Type {
			get {
				if( Main.netMode == NetmodeID.SinglePlayer && !Main.dedServ ) {
					return CommandType.World;
				}
				return CommandType.Console | CommandType.World;
			}
		}
		/// @private
		public override string Command => "mluc-prop";
		/// @private
		public override string Usage => $"/{this.Command} <type> <sub type>";
		/// @private
		public override string Description => "Spawns a prop. `type` indicates either item (0), npc (1), projectile (2)."
			+" `sub type` indicates the entity type index.";    //gore (3), buff (4), cloud (5), extra (6)



		////////////////

		/// @private
		public override void Action( CommandCaller caller, string input, string[] args ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				LogLibraries.Warn( "Not supposed to run on client." );
				return;
			}

			//

			PropType propType;
			int propSubType;

			if( !CreatePropCommand.ParseArguments(args, out propType, out propSubType, out string result) ) {
				caller.Reply( result, Color.Yellow );
				return;
			}

			//

			int npcWho = PropNPC.Create( propType, propSubType, Main.MouseWorld );
			Main.npc[npcWho].direction = caller.Player.direction;

			//

			caller.Reply( result, Color.Lime );
		}
	}
}
