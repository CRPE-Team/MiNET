using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MiNET.Blocks;
using MiNET.Plugins.Attributes;
using MiNET.Utils.Vectors;

namespace MiNET.Plugins.Commands
{
	public class DebugCommands
	{
		private readonly Dictionary<string, List<BlockCoordinates>> _blocksLocationMap = new Dictionary<string, List<BlockCoordinates>>();
		private readonly List<Player> _blockStatesTracerSubscribers = new List<Player>();

		/// <summary>
		/// Debug command to teleport to block from Anvil 'DebugWorld'
		/// </summary>
		[Command(Name = "toblock")]
		public void TpAnvilDebugWorldBlock(Player player, BlockTypeEnum blockType, int stateIndex = 0)
		{
			if (!_blocksLocationMap.Any())
			{
				var y = 70;
				var squareSize = 500;
				for (var x = 1; x < squareSize; x += 2)
				{
					for (var z = 1; z < squareSize; z += 2)
					{
						var block = player.Level.GetBlock(x, y, z);

						if (!_blocksLocationMap.TryGetValue(block.Id, out var coords))
						{
							_blocksLocationMap.Add(block.Id, coords = new List<BlockCoordinates>());
						}

						coords.Add(block.Coordinates);
					}
				}
			}

			var requiredBlockName = blockType.Value;
			if (!requiredBlockName.Contains("minecraft:"))
			{
				requiredBlockName = $"minecraft:{requiredBlockName}";
			}

			var existingBlock = BlockFactory.GetBlockById(requiredBlockName);
			if (existingBlock == null)
			{
				player.SendMessage($"Unknown block with id [{requiredBlockName}]");
				return;
			}

			if (!_blocksLocationMap.TryGetValue(requiredBlockName, out var existingBlockLocations))
			{
				player.SendMessage($"Can't find the block with id [{requiredBlockName}]");
				return;
			}

			stateIndex = Math.Min(stateIndex, existingBlockLocations.Count - 1);

			player.Teleport(existingBlockLocations[stateIndex] + new PlayerLocation(0.5f, 1, 0.5f));
			player.SendMessage($"Found block [{requiredBlockName}] at [{existingBlockLocations[stateIndex]}]");
		}

		[Command(Name = "statestracer")]
		public void BlockStatesTracer(Player player, bool enable)
		{
			lock (_blockStatesTracerSubscribers)
			{
				if (enable && !_blockStatesTracerSubscribers.Contains(player))
				{
					player.Ticking += TraceBlock;
					player.PlayerLeave += RemoveTraceSubscriptionCache;
					_blockStatesTracerSubscribers.Add(player);
				}
				else
				{
					player.Ticking -= TraceBlock;
					player.PlayerLeave -= RemoveTraceSubscriptionCache;
					_blockStatesTracerSubscribers.Remove(player);
				}
			}
		}

		private void RemoveTraceSubscriptionCache(object soruce, PlayerEventArgs args)
		{
			_blockStatesTracerSubscribers.Remove(args.Player);
		}

		private void TraceBlock(object soruce, PlayerEventArgs args)
		{
			var ray = new Ray(args.Player.KnownPosition + new Vector3(0, 1.62f, 0), args.Player.KnownPosition.GetHeadDirection());
			var point = ray.Position;
			var shift = ray.Direction / Vector3.Abs(ray.Direction);

			for (var i = 0; i < 20; i++)
			{
				if (Check(point)
					|| Check(point + Vector3.UnitX * shift)
					|| Check(point + Vector3.UnitY * shift)
					|| Check(point + Vector3.UnitZ * shift)
					|| Check(point + (Vector3.UnitX + Vector3.UnitY) * shift)
					|| Check(point + (Vector3.UnitX + Vector3.UnitZ) * shift)
					|| Check(point + (Vector3.UnitY + Vector3.UnitZ) * shift))
				{
					break;
				}

				bool Check(BlockCoordinates coords)
				{
					var block = args.Level.GetBlock(coords);
					if (block is Air)
						return false;

					var ds = ray.Intersects(block.GetBoundingBox());
					if (!ds.HasValue || ds <= 0)
						return false;

					var chunk = args.Level.GetChunk(coords);
					chunk.BlockEntities.TryGetValue(coords, out var k);

					args.Player.SendMessage($"Id: {block.Id}:{chunk.GetBlockRuntimeId(coords.X & 0x0f, coords.Y, coords.Z & 0x0f)}\n" +
						$"{string.Join("\n", block.States.Select(s => $"{s.Name}: {s.GetValue()}"))}\n" +
						$"{k}", MessageType.Tip);

					return true;
				}

				point += ray.Direction;
			}
		}
	}
}
