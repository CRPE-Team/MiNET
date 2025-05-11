#region LICENSE

// The contents of this file are subject to the Common Public Attribution
// License Version 1.0. (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
// https://github.com/NiclasOlofsson/MiNET/blob/master/LICENSE.
// The License is based on the Mozilla Public License Version 1.1, but Sections 14
// and 15 have been added to cover use of software over a computer network and
// provide for limited attribution for the Original Developer. In addition, Exhibit A has
// been modified to be consistent with Exhibit B.
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// The Original Code is MiNET.
// 
// The Original Developer is the Initial Developer.  The Initial Developer of
// the Original Code is Niclas Olofsson.
// 
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2020 Niclas Olofsson.
// All Rights Reserved.

#endregion

using System;
using System.Runtime.CompilerServices;
using log4net;
using MiNET.Utils;

namespace MiNET.Blocks
{
	public static class BlockFactory
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(BlockFactory));

		private static readonly object _lockObj = new object();

		public static IBlockFactoryProfile FactoryProfile { get; set; }

		public static IBlockPalette BlockPalette => FactoryProfile.BlockPalette;

		static BlockFactory()
		{
			lock (_lockObj)
			{
				if (Config.GetProperty("BlockRuntimeIdsAreHashes", false))
				{
					FactoryProfile = new HashBlockFactoryProfile();
				}
				else
				{
					FactoryProfile = new DefaultBlockFactoryProfile();
				}

				FactoryProfile.Load();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetBlockIdFromItemId(string id)
		{
			return FactoryProfile.GetBlockIdFromItemId(id);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetIdByType<T>(bool withRoot = true) where T : Block
		{
			return FactoryProfile.GetIdByType<T>(withRoot);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetIdByType(Type type, bool withRoot = true)
		{
			return FactoryProfile.GetIdByType(type, withRoot);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetIdByRuntimeId(int id)
		{
			return FactoryProfile.GetIdByRuntimeId(id);
		}

		[Obsolete("Use block states")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Block GetBlockById(string id, short metadata)
		{
			return FactoryProfile.GetBlockById(id, metadata);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetBlockById<T>(string id) where T : Block
		{
			return FactoryProfile.GetBlockById<T>(id);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Block GetBlockById(string id)
		{
			return FactoryProfile.GetBlockById(id);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Block GetBlockByRuntimeId(int runtimeId)
		{
			return FactoryProfile.GetBlockByRuntimeId(runtimeId);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsBlock<T>(int runtimeId) where T : Block
		{
			return FactoryProfile.IsBlock<T>(runtimeId);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsBlock<T>(string id) where T : Block
		{
			return FactoryProfile.IsBlock<T>(id);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsBlock(int runtimeId, Type blockType)
		{
			return FactoryProfile.IsBlock(runtimeId, blockType);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsBlock(string id, Type blockType)
		{
			return FactoryProfile.IsBlock(id, blockType);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsTransparent(int runtimeId)
		{
			return FactoryProfile.IsTransparent(runtimeId);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IBlockStateContainer GetStateContainer(int runtimeId)
		{
			return FactoryProfile.GetStateContainer(runtimeId);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GetStateContainer(IBlockStateContainer container, out IBlockStateContainer palette)
		{
			return FactoryProfile.GetStateContainer(container, out palette);
		}
	}
}