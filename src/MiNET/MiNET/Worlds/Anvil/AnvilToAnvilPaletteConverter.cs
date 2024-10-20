﻿using System.Collections.Generic;
using System.Linq;
using fNbt;
using log4net;
using MiNET.Worlds.Anvil.Mapping;

namespace MiNET.Worlds.Anvil
{
	public class AnvilToAnvilPaletteConverter
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(AnvilToAnvilPaletteConverter));

		private static readonly List<AnvilVersionedStateMapper> _mappers = new List<AnvilVersionedStateMapper>();

		static AnvilToAnvilPaletteConverter()
		{
			var _3693_mapper = new AnvilVersionedStateMapper(3693); // Java Edition 1.20.3 Pre-Release 1
			_mappers.Add(_3693_mapper);

			_3693_mapper.Mapper.Add(new BlockStateMapper("minecraft:grass", "minecraft:short_grass"));
		}

		public static void MapStates(NbtCompound palette, int dataVersion)
		{
			var name = palette["Name"].StringValue;
			var properties = palette["Properties"] as NbtCompound;

			var context = new BlockStateMapperContext(name, properties ?? new NbtCompound("Properties"));

			foreach (var mapper in _mappers)
			{
				if (dataVersion < mapper.Version)
				{
					name = mapper.Mapper.Resolve(context);
				}
			}

			palette["Name"] = new NbtString("Name", name);

			if (properties == null && context.Properties.Any())
			{
				palette.Add(context.Properties);
			}
		}

		private class AnvilVersionedStateMapper
		{
			public int Version { get; set; }

			public StateMapper Mapper { get; set; } = new StateMapper();

			public AnvilVersionedStateMapper(int version)
			{
				Version = version;
			}
		}
	}
}
