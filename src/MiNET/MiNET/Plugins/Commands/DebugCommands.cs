﻿using System.Collections.Generic;
using System.Linq;
using MiNET.Blocks;
using MiNET.Plugins.Attributes;
using MiNET.Utils.Vectors;

namespace MiNET.Plugins.Commands
{
	public class DebugCommands
	{
		private readonly Dictionary<string, BlockCoordinates> _blocksLocationMap = new Dictionary<string, BlockCoordinates>();

		[Command(Name = "toblock")]
		public void TpAnvilDebugWorldBlock(Player player, BlockTypeEnum blockType)
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

						if (_blocksLocationMap.ContainsKey(block.Id)) continue;

						_blocksLocationMap.Add(block.Id, block.Coordinates);
					}
				}
			}

			var requieredBlockName = blockType.Value;
			if (!requieredBlockName.Contains("minecraft:"))
			{
				requieredBlockName = $"minecraft:{requieredBlockName}";
			}

			var existingBlock = BlockFactory.GetBlockById(requieredBlockName);
			if (existingBlock == null)
			{
				player.SendMessage($"Unknown block with id [{requieredBlockName}]");
				return;
			}

			if (!_blocksLocationMap.TryGetValue(requieredBlockName, out var existingBlockLocation))
			{
				player.SendMessage($"Can't find the block with id [{requieredBlockName}]");
				return;
			}

			player.Teleport(existingBlockLocation + new PlayerLocation(0.5f, 1, 0.5f));
			player.SendMessage($"Found block [{requieredBlockName}] at [{existingBlockLocation}]");
		}
	}
}
