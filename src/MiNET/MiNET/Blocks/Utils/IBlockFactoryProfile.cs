using System;
using System.Collections.Generic;

namespace MiNET.Blocks
{
	public interface IBlockFactoryProfile
	{
		Dictionary<string, string> ItemIdToBlockId { get; }
		HashSet<string> Ids { get; }
		IBlockPalette BlockPalette { get; }

		bool BlockRuntimeIdsAreHashes { get; }

		[Obsolete("Use block states")]
		Block GetBlockById(string id, short metadata);
		T GetBlockById<T>(string id) where T : Block;
		Block GetBlockById(string id);
		Block GetBlockByRuntimeId(int runtimeId);
		string GetBlockIdFromItemId(string id);
		string GetIdByRuntimeId(int id);
		string GetIdByType<T>(bool withRoot = true) where T : Block;
		string GetIdByType(Type type, bool withRoot = true);
		IBlockStateContainer GetStateContainer(int runtimeId);
		bool GetStateContainer(IBlockStateContainer container, out IBlockStateContainer palette);
		bool IsBlock<T>(int runtimeId) where T : Block;
		bool IsBlock(int runtimeId, Type blockType);
		bool IsBlock<T>(string id) where T : Block;
		bool IsBlock(string id, Type blockType);
		bool IsTransparent(int runtimeId);
		bool Load();
	}
}
