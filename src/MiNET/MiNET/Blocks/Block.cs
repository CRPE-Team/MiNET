﻿#region LICENSE

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
using System.Numerics;
using log4net;
using MiNET.Items;
using MiNET.Particles;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public abstract class Block : BlockStateContainer, ICloneable
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Block));

		public const int UnknownRuntimeId = -1; // uint.MaxValue;

		public BlockCoordinates Coordinates { get; set; }

		public float Hardness { get; protected set; } = 0;
		public float BlastResistance { get; protected set; } = 0;
		public short FuelEfficiency { get; protected set; } = 0;
		public float FrictionFactor { get; protected set; } = 0.6f;
		public int LightLevel { get; set; } = 0;

		public bool IsReplaceable { get; protected set; } = false;
		public bool IsSolid { get; protected set; } = true;
		public bool IsBuildable { get; protected set; } = true;
		public bool IsTransparent { get; protected set; } = false;
		public bool IsFlammable { get; protected set; } = false;
		public bool IsBlockingSkylight { get; protected set; } = true;

		public bool Edu { get; protected set; } = false;

		public byte BlockLight { get; set; }
		public byte SkyLight { get; set; }

		public byte BiomeId { get; set; }

		protected Block()
		{

		}

		public virtual Item GetItem(Level world, bool blockItem = false)
		{
			return ItemFactory.GetItem(Id);
		}

		public bool CanPlace(Level world, Player player, BlockCoordinates targetCoordinates, BlockFace face)
		{
			return CanPlace(world, player, Coordinates, targetCoordinates, face);
		}

		protected virtual bool CanPlace(Level world, Player player, BlockCoordinates blockCoordinates, BlockCoordinates targetCoordinates, BlockFace face)
		{
			var playerBbox = (player.GetBoundingBox() - 0.01f);
			var blockBbox = GetBoundingBox();
			if (playerBbox.Intersects(blockBbox))
			{
				Log.Debug($"Player bbox={playerBbox}, block bbox={blockBbox}, intersects={playerBbox.Intersects(blockBbox)}");
				Log.Debug($"Can't build where you are standing");
				return false;
			}

			return world.GetBlock(blockCoordinates).IsReplaceable;
		}

		public virtual void BreakBlock(Level world, BlockFace face, bool silent = false)
		{
			world.SetAir(Coordinates);

			if (!silent)
			{
				var particle = new DestroyBlockParticle(world, this);
				particle.Spawn();
			}

			UpdateBlocks(world);
		}

		protected void UpdateBlocks(Level world)
		{
			world.GetBlock(Coordinates.BlockUp()).BlockUpdate(world, Coordinates);
			world.GetBlock(Coordinates.BlockDown()).BlockUpdate(world, Coordinates);
			world.GetBlock(Coordinates.BlockWest()).BlockUpdate(world, Coordinates);
			world.GetBlock(Coordinates.BlockEast()).BlockUpdate(world, Coordinates);
			world.GetBlock(Coordinates.BlockSouth()).BlockUpdate(world, Coordinates);
			world.GetBlock(Coordinates.BlockNorth()).BlockUpdate(world, Coordinates);
		}

		public virtual bool PlaceBlock(Level world, Player player, BlockCoordinates targetCoordinates, BlockFace face, Vector3 faceCoords)
		{
			// No default placement. Return unhandled.
			return false;
		}

		public virtual void BlockAdded(Level level)
		{
		}

		public virtual bool Interact(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoord)
		{
			// No default interaction. Return unhandled.
			return false;
		}

		public virtual void UseItem(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face)
		{
		}

		public virtual void OnTick(Level level, bool isRandom)
		{
		}

		public virtual void BlockUpdate(Level level, BlockCoordinates blockCoordinates)
		{
		}

		public float GetHardness()
		{
			return Hardness / 5.0F;
		}

		//public double GetMineTime(Item miningTool)
		//{
		//	int multiplier = (int) miningTool.ItemMaterial;
		//	return Hardness*(1.5*multiplier);
		//}

		protected BlockCoordinates GetNewCoordinatesFromFace(BlockCoordinates target, BlockFace face)
		{
			switch (face)
			{
				case BlockFace.Down:
					return target + Level.Down;
				case BlockFace.Up:
					return target + Level.Up;
				case BlockFace.North:
					return target + Level.North;
				case BlockFace.South:
					return target + Level.South;
				case BlockFace.West:
					return target + Level.West;
				case BlockFace.East:
					return target + Level.East;
				default:
					return target;
			}
		}

		public virtual Item[] GetDrops(Level world, Item tool)
		{
			var item = GetItem(world);
			if (item == null) return new Item[0];

			return new[] { item };
		}

		public virtual Item GetSmelt(string block)
		{
			return null;
		}

		public virtual float GetExperiencePoints()
		{
			return 0;
		}

		public virtual void DoPhysics(Level level)
		{
		}

		public virtual BoundingBox GetBoundingBox()
		{
			return new BoundingBox(Coordinates, Coordinates + 1);
		}

		public virtual object Clone()
		{
			return MemberwiseClone();
		}

		protected virtual bool Equals(Block other)
		{
			return RuntimeId == other.RuntimeId;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is Block)) return false;
			return Equals((Block) obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return $"{base.ToString()}, Coordinates: {Coordinates}";
		}
	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	public class StateAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	public class StateBitAttribute : StateAttribute
	{
	}


	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	public class StateRangeAttribute : StateAttribute
	{
		public int Minimum { get; }
		public int Maximum { get; }

		public StateRangeAttribute(int minimum, int maximum)
		{
			Minimum = minimum;
			Maximum = maximum;
		}
	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	public class StateEnumAttribute : StateAttribute
	{
		public StateEnumAttribute(params string[] validValues)
		{
		}
	}
}