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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2018 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using fNbt.Serialization;

namespace MiNET.BlockEntities
{
	public class MobSpawnerBlockEntity : BlockEntity
	{
		public short Delay { get; set; } = 20;
		public float DisplayEntityHeight { get; set; } = 1.8f;
		public float DisplayEntityScale { get; set; } = 1.0f;
		public float DisplayEntityWidth { get; set; } = 0.8f;

		[NbtProperty("EntityId")]
		public int EntityTypeId { get; set; } = 1;
		public short MaxNearbyEntities { get; set; } = 4;
		public short MinSpawnDelay { get; set; } = 200;
		public short MaxSpawnDelay { get; set; } = 800;
		public short RequiredPlayerRange { get; set; } = 16;
		public short SpawnCount { get; set; } = 4;
		public short SpawnRange { get; set; } = 4;

		public MobSpawnerBlockEntity() : base(BlockEntityIds.MobSpawner)
		{
		}
	}
}