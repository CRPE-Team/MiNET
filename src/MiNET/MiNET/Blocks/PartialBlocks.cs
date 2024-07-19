using System;
using System.Collections.Generic;
using MiNET.Utils;

namespace MiNET.Blocks
{

	public partial class AcaciaButton
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:acacia_button";

		[StateBit]
		public override bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class AcaciaDoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:acacia_door";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class AcaciaFence : Block
	{
		public override string Id => "minecraft:acacia_fence";
	} // class

	public partial class AcaciaFenceGate
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:acacia_fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class AcaciaHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:acacia_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class AcaciaLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:acacia_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class AcaciaPressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:acacia_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class AcaciaStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:acacia_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class AcaciaStandingSign
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:acacia_standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class AcaciaTrapdoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:acacia_trapdoor";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class AcaciaWallSign
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:acacia_wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class ActivatorRail : Block
	{
		private readonly BlockStateByte _railDataBit = new BlockStateByte() { Name = "rail_data_bit", Value = 0 };
		private readonly BlockStateInt _railDirection = new BlockStateInt() { Name = "rail_direction", Value = 0 };

		public override string Id => "minecraft:activator_rail";

		[StateBit]
		public bool RailDataBit { get => Convert.ToBoolean(_railDataBit.Value); set => NotifyStateUpdate(_railDataBit, value); }

		[StateRange(0, 5)]
		public int RailDirection { get => _railDirection.Value; set => NotifyStateUpdate(_railDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "rail_data_bit":
						NotifyStateUpdate(_railDataBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "rail_direction":
						NotifyStateUpdate(_railDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _railDataBit;
			yield return _railDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _railDataBit, _railDirection);
		} // method
	} // class

	public partial class Air : Block
	{
		public override string Id => "minecraft:air";
	} // class

	public partial class Allow : Block
	{
		public override string Id => "minecraft:allow";
	} // class

	public partial class AmethystBlock : Block
	{
		public override string Id => "minecraft:amethyst_block";
	} // class

	public partial class AmethystCluster : Block
	{
		private readonly BlockStateString _blockFace = new BlockStateString() { Name = "minecraft:block_face", Value = "down" };

		public override string Id => "minecraft:amethyst_cluster";

		[StateEnum("down", "east", "north", "south", "up", "west")]
		public string BlockFace { get => _blockFace.Value; set => NotifyStateUpdate(_blockFace, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:block_face":
						NotifyStateUpdate(_blockFace, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _blockFace;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _blockFace);
		} // method
	} // class

	public partial class AncientDebris : Block
	{
		public override string Id => "minecraft:ancient_debris";
	} // class

	public partial class AndesiteStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:andesite_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class Anvil : Block
	{
		private readonly BlockStateString _damage = new BlockStateString() { Name = "damage", Value = "undamaged" };
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:anvil";

		[StateEnum("broken", "slightly_damaged", "undamaged", "very_damaged")]
		public string Damage { get => _damage.Value; set => NotifyStateUpdate(_damage, value); }

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "damage":
						NotifyStateUpdate(_damage, s.Value);
						break;
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _damage;
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _damage, _cardinalDirection);
		} // method
	} // class

	public partial class Azalea : Block
	{
		public override string Id => "minecraft:azalea";
	} // class

	public partial class AzaleaLeaves : Block
	{
		private readonly BlockStateByte _persistentBit = new BlockStateByte() { Name = "persistent_bit", Value = 0 };
		private readonly BlockStateByte _updateBit = new BlockStateByte() { Name = "update_bit", Value = 0 };

		public override string Id => "minecraft:azalea_leaves";

		[StateBit]
		public bool PersistentBit { get => Convert.ToBoolean(_persistentBit.Value); set => NotifyStateUpdate(_persistentBit, value); }

		[StateBit]
		public bool UpdateBit { get => Convert.ToBoolean(_updateBit.Value); set => NotifyStateUpdate(_updateBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "persistent_bit":
						NotifyStateUpdate(_persistentBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "update_bit":
						NotifyStateUpdate(_updateBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _persistentBit;
			yield return _updateBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _persistentBit, _updateBit);
		} // method
	} // class

	public partial class AzaleaLeavesFlowered : Block
	{
		private readonly BlockStateByte _persistentBit = new BlockStateByte() { Name = "persistent_bit", Value = 0 };
		private readonly BlockStateByte _updateBit = new BlockStateByte() { Name = "update_bit", Value = 0 };

		public override string Id => "minecraft:azalea_leaves_flowered";

		[StateBit]
		public bool PersistentBit { get => Convert.ToBoolean(_persistentBit.Value); set => NotifyStateUpdate(_persistentBit, value); }

		[StateBit]
		public bool UpdateBit { get => Convert.ToBoolean(_updateBit.Value); set => NotifyStateUpdate(_updateBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "persistent_bit":
						NotifyStateUpdate(_persistentBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "update_bit":
						NotifyStateUpdate(_updateBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _persistentBit;
			yield return _updateBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _persistentBit, _updateBit);
		} // method
	} // class

	public partial class Bamboo : Block
	{
		private readonly BlockStateByte _ageBit = new BlockStateByte() { Name = "age_bit", Value = 0 };
		private readonly BlockStateString _bambooLeafSize = new BlockStateString() { Name = "bamboo_leaf_size", Value = "no_leaves" };
		private readonly BlockStateString _bambooStalkThickness = new BlockStateString() { Name = "bamboo_stalk_thickness", Value = "thin" };

		public override string Id => "minecraft:bamboo";

		[StateBit]
		public bool AgeBit { get => Convert.ToBoolean(_ageBit.Value); set => NotifyStateUpdate(_ageBit, value); }

		[StateEnum("large_leaves", "no_leaves", "small_leaves")]
		public string BambooLeafSize { get => _bambooLeafSize.Value; set => NotifyStateUpdate(_bambooLeafSize, value); }

		[StateEnum("thick", "thin")]
		public string BambooStalkThickness { get => _bambooStalkThickness.Value; set => NotifyStateUpdate(_bambooStalkThickness, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "age_bit":
						NotifyStateUpdate(_ageBit, s.Value);
						break;
					case BlockStateString s when s.Name == "bamboo_leaf_size":
						NotifyStateUpdate(_bambooLeafSize, s.Value);
						break;
					case BlockStateString s when s.Name == "bamboo_stalk_thickness":
						NotifyStateUpdate(_bambooStalkThickness, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _ageBit;
			yield return _bambooLeafSize;
			yield return _bambooStalkThickness;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _ageBit, _bambooLeafSize, _bambooStalkThickness);
		} // method
	} // class

	public partial class BambooBlock : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:bamboo_block";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class BambooButton : Block
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:bamboo_button";

		[StateBit]
		public bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class BambooDoor : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:bamboo_door";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class BambooDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:bamboo_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class BambooFence : Block
	{
		public override string Id => "minecraft:bamboo_fence";
	} // class

	public partial class BambooFenceGate : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:bamboo_fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class BambooHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:bamboo_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class BambooMosaic : Block
	{
		public override string Id => "minecraft:bamboo_mosaic";
	} // class

	public partial class BambooMosaicDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:bamboo_mosaic_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class BambooMosaicSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:bamboo_mosaic_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class BambooMosaicStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:bamboo_mosaic_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class BambooPlanks : Block
	{
		public override string Id => "minecraft:bamboo_planks";
	} // class

	public partial class BambooPressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:bamboo_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class BambooSapling : Block
	{
		private readonly BlockStateByte _ageBit = new BlockStateByte() { Name = "age_bit", Value = 0 };
		private readonly BlockStateString _saplingType = new BlockStateString() { Name = "sapling_type", Value = "oak" };

		public override string Id => "minecraft:bamboo_sapling";

		[StateBit]
		public bool AgeBit { get => Convert.ToBoolean(_ageBit.Value); set => NotifyStateUpdate(_ageBit, value); }

		[StateEnum("acacia", "birch", "dark_oak", "jungle", "oak", "spruce")]
		public string SaplingType { get => _saplingType.Value; set => NotifyStateUpdate(_saplingType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "age_bit":
						NotifyStateUpdate(_ageBit, s.Value);
						break;
					case BlockStateString s when s.Name == "sapling_type":
						NotifyStateUpdate(_saplingType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _ageBit;
			yield return _saplingType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _ageBit, _saplingType);
		} // method
	} // class

	public partial class BambooSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:bamboo_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class BambooStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:bamboo_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class BambooStandingSign : Block
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:bamboo_standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class BambooTrapdoor : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:bamboo_trapdoor";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class BambooWallSign : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:bamboo_wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class Barrel : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:barrel";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection, _openBit);
		} // method
	} // class

	public partial class Barrier : Block
	{
		public override string Id => "minecraft:barrier";
	} // class

	public partial class Basalt : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:basalt";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class Beacon : Block
	{
		public override string Id => "minecraft:beacon";
	} // class

	public partial class Bed : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _headPieceBit = new BlockStateByte() { Name = "head_piece_bit", Value = 0 };
		private readonly BlockStateByte _occupiedBit = new BlockStateByte() { Name = "occupied_bit", Value = 0 };

		public override string Id => "minecraft:bed";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool HeadPieceBit { get => Convert.ToBoolean(_headPieceBit.Value); set => NotifyStateUpdate(_headPieceBit, value); }

		[StateBit]
		public bool OccupiedBit { get => Convert.ToBoolean(_occupiedBit.Value); set => NotifyStateUpdate(_occupiedBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "head_piece_bit":
						NotifyStateUpdate(_headPieceBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "occupied_bit":
						NotifyStateUpdate(_occupiedBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _headPieceBit;
			yield return _occupiedBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _headPieceBit, _occupiedBit);
		} // method
	} // class

	public partial class Bedrock : Block
	{
		private readonly BlockStateByte _infiniburnBit = new BlockStateByte() { Name = "infiniburn_bit", Value = 0 };

		public override string Id => "minecraft:bedrock";

		[StateBit]
		public bool InfiniburnBit { get => Convert.ToBoolean(_infiniburnBit.Value); set => NotifyStateUpdate(_infiniburnBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "infiniburn_bit":
						NotifyStateUpdate(_infiniburnBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _infiniburnBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _infiniburnBit);
		} // method
	} // class

	public partial class BeeNest : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateInt _honeyLevel = new BlockStateInt() { Name = "honey_level", Value = 0 };

		public override string Id => "minecraft:bee_nest";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateRange(0, 5)]
		public int HoneyLevel { get => _honeyLevel.Value; set => NotifyStateUpdate(_honeyLevel, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateInt s when s.Name == "honey_level":
						NotifyStateUpdate(_honeyLevel, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _honeyLevel;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _honeyLevel);
		} // method
	} // class

	public partial class Beehive : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateInt _honeyLevel = new BlockStateInt() { Name = "honey_level", Value = 0 };

		public override string Id => "minecraft:beehive";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateRange(0, 5)]
		public int HoneyLevel { get => _honeyLevel.Value; set => NotifyStateUpdate(_honeyLevel, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateInt s when s.Name == "honey_level":
						NotifyStateUpdate(_honeyLevel, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _honeyLevel;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _honeyLevel);
		} // method
	} // class

	public partial class Beetroot
	{
		private readonly BlockStateInt _growth = new BlockStateInt() { Name = "growth", Value = 0 };

		public override string Id => "minecraft:beetroot";

		[StateRange(0, 7)]
		public override int Growth { get => _growth.Value; set => NotifyStateUpdate(_growth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growth":
						NotifyStateUpdate(_growth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growth);
		} // method
	} // class

	public partial class Bell : Block
	{
		private readonly BlockStateString _attachment = new BlockStateString() { Name = "attachment", Value = "standing" };
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _toggleBit = new BlockStateByte() { Name = "toggle_bit", Value = 0 };

		public override string Id => "minecraft:bell";

		[StateEnum("hanging", "multiple", "side", "standing")]
		public string Attachment { get => _attachment.Value; set => NotifyStateUpdate(_attachment, value); }

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool ToggleBit { get => Convert.ToBoolean(_toggleBit.Value); set => NotifyStateUpdate(_toggleBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "attachment":
						NotifyStateUpdate(_attachment, s.Value);
						break;
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "toggle_bit":
						NotifyStateUpdate(_toggleBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachment;
			yield return _direction;
			yield return _toggleBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachment, _direction, _toggleBit);
		} // method
	} // class

	public partial class BigDripleaf : Block
	{
		private readonly BlockStateByte _bigDripleafHead = new BlockStateByte() { Name = "big_dripleaf_head", Value = 0 };
		private readonly BlockStateString _bigDripleafTilt = new BlockStateString() { Name = "big_dripleaf_tilt", Value = "none" };
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:big_dripleaf";

		[StateBit]
		public bool BigDripleafHead { get => Convert.ToBoolean(_bigDripleafHead.Value); set => NotifyStateUpdate(_bigDripleafHead, value); }

		[StateEnum("full_tilt", "none", "partial_tilt", "unstable")]
		public string BigDripleafTilt { get => _bigDripleafTilt.Value; set => NotifyStateUpdate(_bigDripleafTilt, value); }

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "big_dripleaf_head":
						NotifyStateUpdate(_bigDripleafHead, s.Value);
						break;
					case BlockStateString s when s.Name == "big_dripleaf_tilt":
						NotifyStateUpdate(_bigDripleafTilt, s.Value);
						break;
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _bigDripleafHead;
			yield return _bigDripleafTilt;
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _bigDripleafHead, _bigDripleafTilt, _cardinalDirection);
		} // method
	} // class

	public partial class BirchButton
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:birch_button";

		[StateBit]
		public override bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class BirchDoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:birch_door";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class BirchFence : Block
	{
		public override string Id => "minecraft:birch_fence";
	} // class

	public partial class BirchFenceGate
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:birch_fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class BirchHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:birch_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class BirchLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:birch_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class BirchPressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:birch_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class BirchStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:birch_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class BirchStandingSign
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:birch_standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class BirchTrapdoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:birch_trapdoor";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class BirchWallSign
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:birch_wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class BlackCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:black_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class BlackCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:black_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class BlackCarpet : CarpetBase
	{
		public override string Id => "minecraft:black_carpet";
	} // class

	public partial class BlackConcrete : ConcreteBase
	{
		public override string Id => "minecraft:black_concrete";
	} // class

	public partial class BlackConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:black_concrete_powder";
	} // class

	public partial class BlackGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:black_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class BlackShulkerBox : Block
	{
		public override string Id => "minecraft:black_shulker_box";
	} // class

	public partial class BlackStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:black_stained_glass";
	} // class

	public partial class BlackStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:black_stained_glass_pane";
	} // class

	public partial class BlackTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:black_terracotta";
	} // class

	public partial class BlackWool : WoolBase
	{
		public override string Id => "minecraft:black_wool";
	} // class

	public partial class Blackstone : Block
	{
		public override string Id => "minecraft:blackstone";
	} // class

	public partial class BlackstoneDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:blackstone_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class BlackstoneSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:blackstone_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class BlackstoneStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:blackstone_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class BlackstoneWall : Block
	{
		private readonly BlockStateString _wallConnectionTypeEast = new BlockStateString() { Name = "wall_connection_type_east", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeNorth = new BlockStateString() { Name = "wall_connection_type_north", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeSouth = new BlockStateString() { Name = "wall_connection_type_south", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeWest = new BlockStateString() { Name = "wall_connection_type_west", Value = "none" };
		private readonly BlockStateByte _wallPostBit = new BlockStateByte() { Name = "wall_post_bit", Value = 0 };

		public override string Id => "minecraft:blackstone_wall";

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeEast { get => _wallConnectionTypeEast.Value; set => NotifyStateUpdate(_wallConnectionTypeEast, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeNorth { get => _wallConnectionTypeNorth.Value; set => NotifyStateUpdate(_wallConnectionTypeNorth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeSouth { get => _wallConnectionTypeSouth.Value; set => NotifyStateUpdate(_wallConnectionTypeSouth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeWest { get => _wallConnectionTypeWest.Value; set => NotifyStateUpdate(_wallConnectionTypeWest, value); }

		[StateBit]
		public bool WallPostBit { get => Convert.ToBoolean(_wallPostBit.Value); set => NotifyStateUpdate(_wallPostBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wall_connection_type_east":
						NotifyStateUpdate(_wallConnectionTypeEast, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_north":
						NotifyStateUpdate(_wallConnectionTypeNorth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_south":
						NotifyStateUpdate(_wallConnectionTypeSouth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_west":
						NotifyStateUpdate(_wallConnectionTypeWest, s.Value);
						break;
					case BlockStateByte s when s.Name == "wall_post_bit":
						NotifyStateUpdate(_wallPostBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _wallConnectionTypeEast;
			yield return _wallConnectionTypeNorth;
			yield return _wallConnectionTypeSouth;
			yield return _wallConnectionTypeWest;
			yield return _wallPostBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _wallConnectionTypeEast, _wallConnectionTypeNorth, _wallConnectionTypeSouth, _wallConnectionTypeWest, _wallPostBit);
		} // method
	} // class

	public partial class BlastFurnace
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:blast_furnace";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class BlueCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:blue_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class BlueCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:blue_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class BlueCarpet : CarpetBase
	{
		public override string Id => "minecraft:blue_carpet";
	} // class

	public partial class BlueConcrete : ConcreteBase
	{
		public override string Id => "minecraft:blue_concrete";
	} // class

	public partial class BlueConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:blue_concrete_powder";
	} // class

	public partial class BlueGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:blue_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class BlueIce : Block
	{
		public override string Id => "minecraft:blue_ice";
	} // class

	public partial class BlueShulkerBox : Block
	{
		public override string Id => "minecraft:blue_shulker_box";
	} // class

	public partial class BlueStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:blue_stained_glass";
	} // class

	public partial class BlueStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:blue_stained_glass_pane";
	} // class

	public partial class BlueTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:blue_terracotta";
	} // class

	public partial class BlueWool : WoolBase
	{
		public override string Id => "minecraft:blue_wool";
	} // class

	public partial class BoneBlock : Block
	{
		private readonly BlockStateInt _deprecated = new BlockStateInt() { Name = "deprecated", Value = 0 };
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:bone_block";

		[StateRange(0, 3)]
		public int Deprecated { get => _deprecated.Value; set => NotifyStateUpdate(_deprecated, value); }

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "deprecated":
						NotifyStateUpdate(_deprecated, s.Value);
						break;
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _deprecated;
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _deprecated, _pillarAxis);
		} // method
	} // class

	public partial class Bookshelf : Block
	{
		public override string Id => "minecraft:bookshelf";
	} // class

	public partial class BorderBlock : Block
	{
		private readonly BlockStateString _wallConnectionTypeEast = new BlockStateString() { Name = "wall_connection_type_east", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeNorth = new BlockStateString() { Name = "wall_connection_type_north", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeSouth = new BlockStateString() { Name = "wall_connection_type_south", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeWest = new BlockStateString() { Name = "wall_connection_type_west", Value = "none" };
		private readonly BlockStateByte _wallPostBit = new BlockStateByte() { Name = "wall_post_bit", Value = 0 };

		public override string Id => "minecraft:border_block";

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeEast { get => _wallConnectionTypeEast.Value; set => NotifyStateUpdate(_wallConnectionTypeEast, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeNorth { get => _wallConnectionTypeNorth.Value; set => NotifyStateUpdate(_wallConnectionTypeNorth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeSouth { get => _wallConnectionTypeSouth.Value; set => NotifyStateUpdate(_wallConnectionTypeSouth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeWest { get => _wallConnectionTypeWest.Value; set => NotifyStateUpdate(_wallConnectionTypeWest, value); }

		[StateBit]
		public bool WallPostBit { get => Convert.ToBoolean(_wallPostBit.Value); set => NotifyStateUpdate(_wallPostBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wall_connection_type_east":
						NotifyStateUpdate(_wallConnectionTypeEast, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_north":
						NotifyStateUpdate(_wallConnectionTypeNorth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_south":
						NotifyStateUpdate(_wallConnectionTypeSouth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_west":
						NotifyStateUpdate(_wallConnectionTypeWest, s.Value);
						break;
					case BlockStateByte s when s.Name == "wall_post_bit":
						NotifyStateUpdate(_wallPostBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _wallConnectionTypeEast;
			yield return _wallConnectionTypeNorth;
			yield return _wallConnectionTypeSouth;
			yield return _wallConnectionTypeWest;
			yield return _wallPostBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _wallConnectionTypeEast, _wallConnectionTypeNorth, _wallConnectionTypeSouth, _wallConnectionTypeWest, _wallPostBit);
		} // method
	} // class

	public partial class BrainCoral : Block
	{
		public override string Id => "minecraft:brain_coral";
	} // class

	public partial class BrewingStand : Block
	{
		private readonly BlockStateByte _brewingStandSlotABit = new BlockStateByte() { Name = "brewing_stand_slot_a_bit", Value = 0 };
		private readonly BlockStateByte _brewingStandSlotBBit = new BlockStateByte() { Name = "brewing_stand_slot_b_bit", Value = 0 };
		private readonly BlockStateByte _brewingStandSlotCBit = new BlockStateByte() { Name = "brewing_stand_slot_c_bit", Value = 0 };

		public override string Id => "minecraft:brewing_stand";

		[StateBit]
		public bool BrewingStandSlotABit { get => Convert.ToBoolean(_brewingStandSlotABit.Value); set => NotifyStateUpdate(_brewingStandSlotABit, value); }

		[StateBit]
		public bool BrewingStandSlotBBit { get => Convert.ToBoolean(_brewingStandSlotBBit.Value); set => NotifyStateUpdate(_brewingStandSlotBBit, value); }

		[StateBit]
		public bool BrewingStandSlotCBit { get => Convert.ToBoolean(_brewingStandSlotCBit.Value); set => NotifyStateUpdate(_brewingStandSlotCBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "brewing_stand_slot_a_bit":
						NotifyStateUpdate(_brewingStandSlotABit, s.Value);
						break;
					case BlockStateByte s when s.Name == "brewing_stand_slot_b_bit":
						NotifyStateUpdate(_brewingStandSlotBBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "brewing_stand_slot_c_bit":
						NotifyStateUpdate(_brewingStandSlotCBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _brewingStandSlotABit;
			yield return _brewingStandSlotBBit;
			yield return _brewingStandSlotCBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _brewingStandSlotABit, _brewingStandSlotBBit, _brewingStandSlotCBit);
		} // method
	} // class

	public partial class BrickBlock : Block
	{
		public override string Id => "minecraft:brick_block";
	} // class

	public partial class BrickStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:brick_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class BrownCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:brown_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class BrownCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:brown_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class BrownCarpet : CarpetBase
	{
		public override string Id => "minecraft:brown_carpet";
	} // class

	public partial class BrownConcrete : ConcreteBase
	{
		public override string Id => "minecraft:brown_concrete";
	} // class

	public partial class BrownConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:brown_concrete_powder";
	} // class

	public partial class BrownGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:brown_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class BrownMushroom : Block
	{
		public override string Id => "minecraft:brown_mushroom";
	} // class

	public partial class BrownMushroomBlock : Block
	{
		private readonly BlockStateInt _hugeMushroomBits = new BlockStateInt() { Name = "huge_mushroom_bits", Value = 0 };

		public override string Id => "minecraft:brown_mushroom_block";

		[StateRange(0, 15)]
		public int HugeMushroomBits { get => _hugeMushroomBits.Value; set => NotifyStateUpdate(_hugeMushroomBits, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "huge_mushroom_bits":
						NotifyStateUpdate(_hugeMushroomBits, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _hugeMushroomBits;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _hugeMushroomBits);
		} // method
	} // class

	public partial class BrownShulkerBox : Block
	{
		public override string Id => "minecraft:brown_shulker_box";
	} // class

	public partial class BrownStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:brown_stained_glass";
	} // class

	public partial class BrownStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:brown_stained_glass_pane";
	} // class

	public partial class BrownTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:brown_terracotta";
	} // class

	public partial class BrownWool : WoolBase
	{
		public override string Id => "minecraft:brown_wool";
	} // class

	public partial class BubbleColumn : Block
	{
		private readonly BlockStateByte _dragDown = new BlockStateByte() { Name = "drag_down", Value = 0 };

		public override string Id => "minecraft:bubble_column";

		[StateBit]
		public bool DragDown { get => Convert.ToBoolean(_dragDown.Value); set => NotifyStateUpdate(_dragDown, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "drag_down":
						NotifyStateUpdate(_dragDown, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _dragDown;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _dragDown);
		} // method
	} // class

	public partial class BubbleCoral : Block
	{
		public override string Id => "minecraft:bubble_coral";
	} // class

	public partial class BuddingAmethyst : Block
	{
		public override string Id => "minecraft:budding_amethyst";
	} // class

	public partial class Cactus : Block
	{
		private readonly BlockStateInt _age = new BlockStateInt() { Name = "age", Value = 0 };

		public override string Id => "minecraft:cactus";

		[StateRange(0, 15)]
		public int Age { get => _age.Value; set => NotifyStateUpdate(_age, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "age":
						NotifyStateUpdate(_age, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _age;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _age);
		} // method
	} // class

	public partial class Cake : Block
	{
		private readonly BlockStateInt _biteCounter = new BlockStateInt() { Name = "bite_counter", Value = 0 };

		public override string Id => "minecraft:cake";

		[StateRange(0, 6)]
		public int BiteCounter { get => _biteCounter.Value; set => NotifyStateUpdate(_biteCounter, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "bite_counter":
						NotifyStateUpdate(_biteCounter, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _biteCounter;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _biteCounter);
		} // method
	} // class

	public partial class Calcite : Block
	{
		public override string Id => "minecraft:calcite";
	} // class

	public partial class CalibratedSculkSensor : Block
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };
		private readonly BlockStateInt _sculkSensorPhase = new BlockStateInt() { Name = "sculk_sensor_phase", Value = 0 };

		public override string Id => "minecraft:calibrated_sculk_sensor";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		[StateRange(0, 2)]
		public int SculkSensorPhase { get => _sculkSensorPhase.Value; set => NotifyStateUpdate(_sculkSensorPhase, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "sculk_sensor_phase":
						NotifyStateUpdate(_sculkSensorPhase, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
			yield return _sculkSensorPhase;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection, _sculkSensorPhase);
		} // method
	} // class

	public partial class Camera : Block
	{
		public override string Id => "minecraft:camera";
	} // class

	public partial class Campfire : Block
	{
		private readonly BlockStateByte _extinguished = new BlockStateByte() { Name = "extinguished", Value = 0 };
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:campfire";

		[StateBit]
		public bool Extinguished { get => Convert.ToBoolean(_extinguished.Value); set => NotifyStateUpdate(_extinguished, value); }

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "extinguished":
						NotifyStateUpdate(_extinguished, s.Value);
						break;
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _extinguished;
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _extinguished, _cardinalDirection);
		} // method
	} // class

	public partial class Candle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class CandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class Carrots
	{
		private readonly BlockStateInt _growth = new BlockStateInt() { Name = "growth", Value = 0 };

		public override string Id => "minecraft:carrots";

		[StateRange(0, 7)]
		public override int Growth { get => _growth.Value; set => NotifyStateUpdate(_growth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growth":
						NotifyStateUpdate(_growth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growth);
		} // method
	} // class

	public partial class CartographyTable : Block
	{
		public override string Id => "minecraft:cartography_table";
	} // class

	public partial class CarvedPumpkin : Block
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:carved_pumpkin";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class Cauldron : Block
	{
		private readonly BlockStateString _cauldronLiquid = new BlockStateString() { Name = "cauldron_liquid", Value = "water" };
		private readonly BlockStateInt _fillLevel = new BlockStateInt() { Name = "fill_level", Value = 0 };

		public override string Id => "minecraft:cauldron";

		[StateEnum("lava", "powder_snow", "water")]
		public string CauldronLiquid { get => _cauldronLiquid.Value; set => NotifyStateUpdate(_cauldronLiquid, value); }

		[StateRange(0, 6)]
		public int FillLevel { get => _fillLevel.Value; set => NotifyStateUpdate(_fillLevel, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "cauldron_liquid":
						NotifyStateUpdate(_cauldronLiquid, s.Value);
						break;
					case BlockStateInt s when s.Name == "fill_level":
						NotifyStateUpdate(_fillLevel, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cauldronLiquid;
			yield return _fillLevel;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cauldronLiquid, _fillLevel);
		} // method
	} // class

	public partial class CaveVines : Block
	{
		private readonly BlockStateInt _growingPlantAge = new BlockStateInt() { Name = "growing_plant_age", Value = 0 };

		public override string Id => "minecraft:cave_vines";

		[StateRange(0, 25)]
		public int GrowingPlantAge { get => _growingPlantAge.Value; set => NotifyStateUpdate(_growingPlantAge, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growing_plant_age":
						NotifyStateUpdate(_growingPlantAge, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growingPlantAge;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growingPlantAge);
		} // method
	} // class

	public partial class CaveVinesBodyWithBerries : Block
	{
		private readonly BlockStateInt _growingPlantAge = new BlockStateInt() { Name = "growing_plant_age", Value = 0 };

		public override string Id => "minecraft:cave_vines_body_with_berries";

		[StateRange(0, 25)]
		public int GrowingPlantAge { get => _growingPlantAge.Value; set => NotifyStateUpdate(_growingPlantAge, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growing_plant_age":
						NotifyStateUpdate(_growingPlantAge, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growingPlantAge;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growingPlantAge);
		} // method
	} // class

	public partial class CaveVinesHeadWithBerries : Block
	{
		private readonly BlockStateInt _growingPlantAge = new BlockStateInt() { Name = "growing_plant_age", Value = 0 };

		public override string Id => "minecraft:cave_vines_head_with_berries";

		[StateRange(0, 25)]
		public int GrowingPlantAge { get => _growingPlantAge.Value; set => NotifyStateUpdate(_growingPlantAge, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growing_plant_age":
						NotifyStateUpdate(_growingPlantAge, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growingPlantAge;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growingPlantAge);
		} // method
	} // class

	public partial class Chain : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:chain";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class ChainCommandBlock : Block
	{
		private readonly BlockStateByte _conditionalBit = new BlockStateByte() { Name = "conditional_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:chain_command_block";

		[StateBit]
		public bool ConditionalBit { get => Convert.ToBoolean(_conditionalBit.Value); set => NotifyStateUpdate(_conditionalBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "conditional_bit":
						NotifyStateUpdate(_conditionalBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _conditionalBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _conditionalBit, _facingDirection);
		} // method
	} // class

	public partial class ChemicalHeat : Block
	{
		public override string Id => "minecraft:chemical_heat";
	} // class

	public partial class ChemistryTable : Block
	{
		private readonly BlockStateString _chemistryTableType = new BlockStateString() { Name = "chemistry_table_type", Value = "compound_creator" };
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };

		public override string Id => "minecraft:chemistry_table";

		[StateEnum("compound_creator", "element_constructor", "lab_table", "material_reducer")]
		public string ChemistryTableType { get => _chemistryTableType.Value; set => NotifyStateUpdate(_chemistryTableType, value); }

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "chemistry_table_type":
						NotifyStateUpdate(_chemistryTableType, s.Value);
						break;
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _chemistryTableType;
			yield return _direction;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _chemistryTableType, _direction);
		} // method
	} // class

	public partial class CherryButton : Block
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:cherry_button";

		[StateBit]
		public bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class CherryDoor : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:cherry_door";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class CherryDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:cherry_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class CherryFence : Block
	{
		public override string Id => "minecraft:cherry_fence";
	} // class

	public partial class CherryFenceGate : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:cherry_fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class CherryHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:cherry_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class CherryLeaves : Block
	{
		private readonly BlockStateByte _persistentBit = new BlockStateByte() { Name = "persistent_bit", Value = 0 };
		private readonly BlockStateByte _updateBit = new BlockStateByte() { Name = "update_bit", Value = 0 };

		public override string Id => "minecraft:cherry_leaves";

		[StateBit]
		public bool PersistentBit { get => Convert.ToBoolean(_persistentBit.Value); set => NotifyStateUpdate(_persistentBit, value); }

		[StateBit]
		public bool UpdateBit { get => Convert.ToBoolean(_updateBit.Value); set => NotifyStateUpdate(_updateBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "persistent_bit":
						NotifyStateUpdate(_persistentBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "update_bit":
						NotifyStateUpdate(_updateBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _persistentBit;
			yield return _updateBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _persistentBit, _updateBit);
		} // method
	} // class

	public partial class CherryLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:cherry_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class CherryPlanks : Block
	{
		public override string Id => "minecraft:cherry_planks";
	} // class

	public partial class CherryPressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:cherry_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class CherrySapling : Block
	{
		private readonly BlockStateByte _ageBit = new BlockStateByte() { Name = "age_bit", Value = 0 };

		public override string Id => "minecraft:cherry_sapling";

		[StateBit]
		public bool AgeBit { get => Convert.ToBoolean(_ageBit.Value); set => NotifyStateUpdate(_ageBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "age_bit":
						NotifyStateUpdate(_ageBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _ageBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _ageBit);
		} // method
	} // class

	public partial class CherrySlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:cherry_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class CherryStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:cherry_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class CherryStandingSign : Block
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:cherry_standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class CherryTrapdoor : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:cherry_trapdoor";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class CherryWallSign : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:cherry_wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class CherryWood : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };
		private readonly BlockStateByte _strippedBit = new BlockStateByte() { Name = "stripped_bit", Value = 0 };

		public override string Id => "minecraft:cherry_wood";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		[StateBit]
		public bool StrippedBit { get => Convert.ToBoolean(_strippedBit.Value); set => NotifyStateUpdate(_strippedBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
					case BlockStateByte s when s.Name == "stripped_bit":
						NotifyStateUpdate(_strippedBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
			yield return _strippedBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis, _strippedBit);
		} // method
	} // class

	public partial class Chest
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "north" };

		public override string Id => "minecraft:chest";

		[StateEnum("east", "north", "south", "west")]
		public override string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class ChiseledBookshelf : Block
	{
		private readonly BlockStateInt _booksStored = new BlockStateInt() { Name = "books_stored", Value = 0 };
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };

		public override string Id => "minecraft:chiseled_bookshelf";

		[StateRange(0, 63)]
		public int BooksStored { get => _booksStored.Value; set => NotifyStateUpdate(_booksStored, value); }

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "books_stored":
						NotifyStateUpdate(_booksStored, s.Value);
						break;
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _booksStored;
			yield return _direction;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _booksStored, _direction);
		} // method
	} // class

	public partial class ChiseledDeepslate : Block
	{
		public override string Id => "minecraft:chiseled_deepslate";
	} // class

	public partial class ChiseledNetherBricks : Block
	{
		public override string Id => "minecraft:chiseled_nether_bricks";
	} // class

	public partial class ChiseledPolishedBlackstone : Block
	{
		public override string Id => "minecraft:chiseled_polished_blackstone";
	} // class

	public partial class ChorusFlower : Block
	{
		private readonly BlockStateInt _age = new BlockStateInt() { Name = "age", Value = 0 };

		public override string Id => "minecraft:chorus_flower";

		[StateRange(0, 5)]
		public int Age { get => _age.Value; set => NotifyStateUpdate(_age, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "age":
						NotifyStateUpdate(_age, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _age;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _age);
		} // method
	} // class

	public partial class ChorusPlant : Block
	{
		public override string Id => "minecraft:chorus_plant";
	} // class

	public partial class Clay : Block
	{
		public override string Id => "minecraft:clay";
	} // class

	public partial class ClientRequestPlaceholderBlock : Block
	{
		public override string Id => "minecraft:client_request_placeholder_block";
	} // class

	public partial class CoalBlock : Block
	{
		public override string Id => "minecraft:coal_block";
	} // class

	public partial class CoalOre : Block
	{
		public override string Id => "minecraft:coal_ore";
	} // class

	public partial class CobbledDeepslate : Block
	{
		public override string Id => "minecraft:cobbled_deepslate";
	} // class

	public partial class CobbledDeepslateDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:cobbled_deepslate_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class CobbledDeepslateSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:cobbled_deepslate_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class CobbledDeepslateStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:cobbled_deepslate_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class CobbledDeepslateWall : Block
	{
		private readonly BlockStateString _wallConnectionTypeEast = new BlockStateString() { Name = "wall_connection_type_east", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeNorth = new BlockStateString() { Name = "wall_connection_type_north", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeSouth = new BlockStateString() { Name = "wall_connection_type_south", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeWest = new BlockStateString() { Name = "wall_connection_type_west", Value = "none" };
		private readonly BlockStateByte _wallPostBit = new BlockStateByte() { Name = "wall_post_bit", Value = 0 };

		public override string Id => "minecraft:cobbled_deepslate_wall";

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeEast { get => _wallConnectionTypeEast.Value; set => NotifyStateUpdate(_wallConnectionTypeEast, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeNorth { get => _wallConnectionTypeNorth.Value; set => NotifyStateUpdate(_wallConnectionTypeNorth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeSouth { get => _wallConnectionTypeSouth.Value; set => NotifyStateUpdate(_wallConnectionTypeSouth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeWest { get => _wallConnectionTypeWest.Value; set => NotifyStateUpdate(_wallConnectionTypeWest, value); }

		[StateBit]
		public bool WallPostBit { get => Convert.ToBoolean(_wallPostBit.Value); set => NotifyStateUpdate(_wallPostBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wall_connection_type_east":
						NotifyStateUpdate(_wallConnectionTypeEast, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_north":
						NotifyStateUpdate(_wallConnectionTypeNorth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_south":
						NotifyStateUpdate(_wallConnectionTypeSouth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_west":
						NotifyStateUpdate(_wallConnectionTypeWest, s.Value);
						break;
					case BlockStateByte s when s.Name == "wall_post_bit":
						NotifyStateUpdate(_wallPostBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _wallConnectionTypeEast;
			yield return _wallConnectionTypeNorth;
			yield return _wallConnectionTypeSouth;
			yield return _wallConnectionTypeWest;
			yield return _wallPostBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _wallConnectionTypeEast, _wallConnectionTypeNorth, _wallConnectionTypeSouth, _wallConnectionTypeWest, _wallPostBit);
		} // method
	} // class

	public partial class Cobblestone : Block
	{
		public override string Id => "minecraft:cobblestone";
	} // class

	public partial class CobblestoneWall : Block
	{
		private readonly BlockStateString _wallBlockType = new BlockStateString() { Name = "wall_block_type", Value = "cobblestone" };
		private readonly BlockStateString _wallConnectionTypeEast = new BlockStateString() { Name = "wall_connection_type_east", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeNorth = new BlockStateString() { Name = "wall_connection_type_north", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeSouth = new BlockStateString() { Name = "wall_connection_type_south", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeWest = new BlockStateString() { Name = "wall_connection_type_west", Value = "none" };
		private readonly BlockStateByte _wallPostBit = new BlockStateByte() { Name = "wall_post_bit", Value = 0 };

		public override string Id => "minecraft:cobblestone_wall";

		[StateEnum("andesite", "brick", "cobblestone", "diorite", "end_brick", "granite", "mossy_cobblestone", "mossy_stone_brick", "nether_brick", "prismarine", "red_nether_brick", "red_sandstone", "sandstone", "stone_brick")]
		public string WallBlockType { get => _wallBlockType.Value; set => NotifyStateUpdate(_wallBlockType, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeEast { get => _wallConnectionTypeEast.Value; set => NotifyStateUpdate(_wallConnectionTypeEast, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeNorth { get => _wallConnectionTypeNorth.Value; set => NotifyStateUpdate(_wallConnectionTypeNorth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeSouth { get => _wallConnectionTypeSouth.Value; set => NotifyStateUpdate(_wallConnectionTypeSouth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeWest { get => _wallConnectionTypeWest.Value; set => NotifyStateUpdate(_wallConnectionTypeWest, value); }

		[StateBit]
		public bool WallPostBit { get => Convert.ToBoolean(_wallPostBit.Value); set => NotifyStateUpdate(_wallPostBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wall_block_type":
						NotifyStateUpdate(_wallBlockType, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_east":
						NotifyStateUpdate(_wallConnectionTypeEast, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_north":
						NotifyStateUpdate(_wallConnectionTypeNorth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_south":
						NotifyStateUpdate(_wallConnectionTypeSouth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_west":
						NotifyStateUpdate(_wallConnectionTypeWest, s.Value);
						break;
					case BlockStateByte s when s.Name == "wall_post_bit":
						NotifyStateUpdate(_wallPostBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _wallBlockType;
			yield return _wallConnectionTypeEast;
			yield return _wallConnectionTypeNorth;
			yield return _wallConnectionTypeSouth;
			yield return _wallConnectionTypeWest;
			yield return _wallPostBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _wallBlockType, _wallConnectionTypeEast, _wallConnectionTypeNorth, _wallConnectionTypeSouth, _wallConnectionTypeWest, _wallPostBit);
		} // method
	} // class

	public partial class Cocoa : Block
	{
		private readonly BlockStateInt _age = new BlockStateInt() { Name = "age", Value = 0 };
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };

		public override string Id => "minecraft:cocoa";

		[StateRange(0, 2)]
		public int Age { get => _age.Value; set => NotifyStateUpdate(_age, value); }

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "age":
						NotifyStateUpdate(_age, s.Value);
						break;
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _age;
			yield return _direction;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _age, _direction);
		} // method
	} // class

	public partial class ColoredTorchBp : Block
	{
		private readonly BlockStateByte _colorBit = new BlockStateByte() { Name = "color_bit", Value = 0 };
		private readonly BlockStateString _torchFacingDirection = new BlockStateString() { Name = "torch_facing_direction", Value = "unknown" };

		public override string Id => "minecraft:colored_torch_bp";

		[StateBit]
		public bool ColorBit { get => Convert.ToBoolean(_colorBit.Value); set => NotifyStateUpdate(_colorBit, value); }

		[StateEnum("east", "north", "south", "top", "unknown", "west")]
		public string TorchFacingDirection { get => _torchFacingDirection.Value; set => NotifyStateUpdate(_torchFacingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "color_bit":
						NotifyStateUpdate(_colorBit, s.Value);
						break;
					case BlockStateString s when s.Name == "torch_facing_direction":
						NotifyStateUpdate(_torchFacingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _colorBit;
			yield return _torchFacingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _colorBit, _torchFacingDirection);
		} // method
	} // class

	public partial class ColoredTorchRg : Block
	{
		private readonly BlockStateByte _colorBit = new BlockStateByte() { Name = "color_bit", Value = 0 };
		private readonly BlockStateString _torchFacingDirection = new BlockStateString() { Name = "torch_facing_direction", Value = "unknown" };

		public override string Id => "minecraft:colored_torch_rg";

		[StateBit]
		public bool ColorBit { get => Convert.ToBoolean(_colorBit.Value); set => NotifyStateUpdate(_colorBit, value); }

		[StateEnum("east", "north", "south", "top", "unknown", "west")]
		public string TorchFacingDirection { get => _torchFacingDirection.Value; set => NotifyStateUpdate(_torchFacingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "color_bit":
						NotifyStateUpdate(_colorBit, s.Value);
						break;
					case BlockStateString s when s.Name == "torch_facing_direction":
						NotifyStateUpdate(_torchFacingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _colorBit;
			yield return _torchFacingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _colorBit, _torchFacingDirection);
		} // method
	} // class

	public partial class CommandBlock : Block
	{
		private readonly BlockStateByte _conditionalBit = new BlockStateByte() { Name = "conditional_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:command_block";

		[StateBit]
		public bool ConditionalBit { get => Convert.ToBoolean(_conditionalBit.Value); set => NotifyStateUpdate(_conditionalBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "conditional_bit":
						NotifyStateUpdate(_conditionalBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _conditionalBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _conditionalBit, _facingDirection);
		} // method
	} // class

	public partial class Composter : Block
	{
		private readonly BlockStateInt _composterFillLevel = new BlockStateInt() { Name = "composter_fill_level", Value = 0 };

		public override string Id => "minecraft:composter";

		[StateRange(0, 8)]
		public int ComposterFillLevel { get => _composterFillLevel.Value; set => NotifyStateUpdate(_composterFillLevel, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "composter_fill_level":
						NotifyStateUpdate(_composterFillLevel, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _composterFillLevel;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _composterFillLevel);
		} // method
	} // class

	public partial class Conduit : Block
	{
		public override string Id => "minecraft:conduit";
	} // class

	public partial class CopperBlock : Block
	{
		public override string Id => "minecraft:copper_block";
	} // class

	public partial class CopperOre : Block
	{
		public override string Id => "minecraft:copper_ore";
	} // class

	public partial class CoralBlock : Block
	{
		private readonly BlockStateString _coralColor = new BlockStateString() { Name = "coral_color", Value = "blue" };
		private readonly BlockStateByte _deadBit = new BlockStateByte() { Name = "dead_bit", Value = 0 };

		public override string Id => "minecraft:coral_block";

		[StateEnum("blue", "pink", "purple", "red", "yellow")]
		public string CoralColor { get => _coralColor.Value; set => NotifyStateUpdate(_coralColor, value); }

		[StateBit]
		public bool DeadBit { get => Convert.ToBoolean(_deadBit.Value); set => NotifyStateUpdate(_deadBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "coral_color":
						NotifyStateUpdate(_coralColor, s.Value);
						break;
					case BlockStateByte s when s.Name == "dead_bit":
						NotifyStateUpdate(_deadBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _coralColor;
			yield return _deadBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _coralColor, _deadBit);
		} // method
	} // class

	public partial class CoralFan : Block
	{
		private readonly BlockStateString _coralColor = new BlockStateString() { Name = "coral_color", Value = "blue" };
		private readonly BlockStateInt _coralFanDirection = new BlockStateInt() { Name = "coral_fan_direction", Value = 0 };

		public override string Id => "minecraft:coral_fan";

		[StateEnum("blue", "pink", "purple", "red", "yellow")]
		public string CoralColor { get => _coralColor.Value; set => NotifyStateUpdate(_coralColor, value); }

		[StateRange(0, 1)]
		public int CoralFanDirection { get => _coralFanDirection.Value; set => NotifyStateUpdate(_coralFanDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "coral_color":
						NotifyStateUpdate(_coralColor, s.Value);
						break;
					case BlockStateInt s when s.Name == "coral_fan_direction":
						NotifyStateUpdate(_coralFanDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _coralColor;
			yield return _coralFanDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _coralColor, _coralFanDirection);
		} // method
	} // class

	public partial class CoralFanDead : Block
	{
		private readonly BlockStateString _coralColor = new BlockStateString() { Name = "coral_color", Value = "blue" };
		private readonly BlockStateInt _coralFanDirection = new BlockStateInt() { Name = "coral_fan_direction", Value = 0 };

		public override string Id => "minecraft:coral_fan_dead";

		[StateEnum("blue", "pink", "purple", "red", "yellow")]
		public string CoralColor { get => _coralColor.Value; set => NotifyStateUpdate(_coralColor, value); }

		[StateRange(0, 1)]
		public int CoralFanDirection { get => _coralFanDirection.Value; set => NotifyStateUpdate(_coralFanDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "coral_color":
						NotifyStateUpdate(_coralColor, s.Value);
						break;
					case BlockStateInt s when s.Name == "coral_fan_direction":
						NotifyStateUpdate(_coralFanDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _coralColor;
			yield return _coralFanDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _coralColor, _coralFanDirection);
		} // method
	} // class

	public partial class CoralFanHang : Block
	{
		private readonly BlockStateInt _coralDirection = new BlockStateInt() { Name = "coral_direction", Value = 0 };
		private readonly BlockStateByte _coralHangTypeBit = new BlockStateByte() { Name = "coral_hang_type_bit", Value = 0 };
		private readonly BlockStateByte _deadBit = new BlockStateByte() { Name = "dead_bit", Value = 0 };

		public override string Id => "minecraft:coral_fan_hang";

		[StateRange(0, 3)]
		public int CoralDirection { get => _coralDirection.Value; set => NotifyStateUpdate(_coralDirection, value); }

		[StateBit]
		public bool CoralHangTypeBit { get => Convert.ToBoolean(_coralHangTypeBit.Value); set => NotifyStateUpdate(_coralHangTypeBit, value); }

		[StateBit]
		public bool DeadBit { get => Convert.ToBoolean(_deadBit.Value); set => NotifyStateUpdate(_deadBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "coral_direction":
						NotifyStateUpdate(_coralDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "coral_hang_type_bit":
						NotifyStateUpdate(_coralHangTypeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "dead_bit":
						NotifyStateUpdate(_deadBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _coralDirection;
			yield return _coralHangTypeBit;
			yield return _deadBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _coralDirection, _coralHangTypeBit, _deadBit);
		} // method
	} // class

	public partial class CoralFanHang2 : Block
	{
		private readonly BlockStateInt _coralDirection = new BlockStateInt() { Name = "coral_direction", Value = 0 };
		private readonly BlockStateByte _coralHangTypeBit = new BlockStateByte() { Name = "coral_hang_type_bit", Value = 0 };
		private readonly BlockStateByte _deadBit = new BlockStateByte() { Name = "dead_bit", Value = 0 };

		public override string Id => "minecraft:coral_fan_hang2";

		[StateRange(0, 3)]
		public int CoralDirection { get => _coralDirection.Value; set => NotifyStateUpdate(_coralDirection, value); }

		[StateBit]
		public bool CoralHangTypeBit { get => Convert.ToBoolean(_coralHangTypeBit.Value); set => NotifyStateUpdate(_coralHangTypeBit, value); }

		[StateBit]
		public bool DeadBit { get => Convert.ToBoolean(_deadBit.Value); set => NotifyStateUpdate(_deadBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "coral_direction":
						NotifyStateUpdate(_coralDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "coral_hang_type_bit":
						NotifyStateUpdate(_coralHangTypeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "dead_bit":
						NotifyStateUpdate(_deadBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _coralDirection;
			yield return _coralHangTypeBit;
			yield return _deadBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _coralDirection, _coralHangTypeBit, _deadBit);
		} // method
	} // class

	public partial class CoralFanHang3 : Block
	{
		private readonly BlockStateInt _coralDirection = new BlockStateInt() { Name = "coral_direction", Value = 0 };
		private readonly BlockStateByte _coralHangTypeBit = new BlockStateByte() { Name = "coral_hang_type_bit", Value = 0 };
		private readonly BlockStateByte _deadBit = new BlockStateByte() { Name = "dead_bit", Value = 0 };

		public override string Id => "minecraft:coral_fan_hang3";

		[StateRange(0, 3)]
		public int CoralDirection { get => _coralDirection.Value; set => NotifyStateUpdate(_coralDirection, value); }

		[StateBit]
		public bool CoralHangTypeBit { get => Convert.ToBoolean(_coralHangTypeBit.Value); set => NotifyStateUpdate(_coralHangTypeBit, value); }

		[StateBit]
		public bool DeadBit { get => Convert.ToBoolean(_deadBit.Value); set => NotifyStateUpdate(_deadBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "coral_direction":
						NotifyStateUpdate(_coralDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "coral_hang_type_bit":
						NotifyStateUpdate(_coralHangTypeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "dead_bit":
						NotifyStateUpdate(_deadBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _coralDirection;
			yield return _coralHangTypeBit;
			yield return _deadBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _coralDirection, _coralHangTypeBit, _deadBit);
		} // method
	} // class

	public partial class CrackedDeepslateBricks : Block
	{
		public override string Id => "minecraft:cracked_deepslate_bricks";
	} // class

	public partial class CrackedDeepslateTiles : Block
	{
		public override string Id => "minecraft:cracked_deepslate_tiles";
	} // class

	public partial class CrackedNetherBricks : Block
	{
		public override string Id => "minecraft:cracked_nether_bricks";
	} // class

	public partial class CrackedPolishedBlackstoneBricks : Block
	{
		public override string Id => "minecraft:cracked_polished_blackstone_bricks";
	} // class

	public partial class CraftingTable : Block
	{
		public override string Id => "minecraft:crafting_table";
	} // class

	public partial class CrimsonButton : Block
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:crimson_button";

		[StateBit]
		public bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class CrimsonDoor : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:crimson_door";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class CrimsonDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:crimson_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class CrimsonFence : Block
	{
		public override string Id => "minecraft:crimson_fence";
	} // class

	public partial class CrimsonFenceGate : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:crimson_fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class CrimsonFungus : Block
	{
		public override string Id => "minecraft:crimson_fungus";
	} // class

	public partial class CrimsonHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:crimson_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class CrimsonHyphae : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:crimson_hyphae";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class CrimsonNylium : Block
	{
		public override string Id => "minecraft:crimson_nylium";
	} // class

	public partial class CrimsonPlanks : Block
	{
		public override string Id => "minecraft:crimson_planks";
	} // class

	public partial class CrimsonPressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:crimson_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class CrimsonRoots : Block
	{
		public override string Id => "minecraft:crimson_roots";
	} // class

	public partial class CrimsonSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:crimson_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class CrimsonStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:crimson_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class CrimsonStandingSign
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:crimson_standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class CrimsonStem : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:crimson_stem";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class CrimsonTrapdoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:crimson_trapdoor";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class CrimsonWallSign
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:crimson_wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class CryingObsidian : Block
	{
		public override string Id => "minecraft:crying_obsidian";
	} // class

	public partial class CutCopper : Block
	{
		public override string Id => "minecraft:cut_copper";
	} // class

	public partial class CutCopperSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class CutCopperStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:cut_copper_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class CyanCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:cyan_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class CyanCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:cyan_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class CyanCarpet : CarpetBase
	{
		public override string Id => "minecraft:cyan_carpet";
	} // class

	public partial class CyanConcrete : ConcreteBase
	{
		public override string Id => "minecraft:cyan_concrete";
	} // class

	public partial class CyanConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:cyan_concrete_powder";
	} // class

	public partial class CyanGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:cyan_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class CyanShulkerBox : Block
	{
		public override string Id => "minecraft:cyan_shulker_box";
	} // class

	public partial class CyanStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:cyan_stained_glass";
	} // class

	public partial class CyanStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:cyan_stained_glass_pane";
	} // class

	public partial class CyanTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:cyan_terracotta";
	} // class

	public partial class CyanWool : WoolBase
	{
		public override string Id => "minecraft:cyan_wool";
	} // class

	public partial class DarkOakButton
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:dark_oak_button";

		[StateBit]
		public override bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class DarkOakDoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:dark_oak_door";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class DarkOakFence : Block
	{
		public override string Id => "minecraft:dark_oak_fence";
	} // class

	public partial class DarkOakFenceGate
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:dark_oak_fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class DarkOakHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:dark_oak_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class DarkOakLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:dark_oak_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class DarkOakPressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:dark_oak_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class DarkOakStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:dark_oak_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class DarkOakTrapdoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:dark_oak_trapdoor";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class DarkPrismarineStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:dark_prismarine_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class DarkoakStandingSign
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:darkoak_standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class DarkoakWallSign
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:darkoak_wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class DaylightDetector : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:daylight_detector";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class DaylightDetectorInverted : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:daylight_detector_inverted";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class DeadBrainCoral : Block
	{
		public override string Id => "minecraft:dead_brain_coral";
	} // class

	public partial class DeadBubbleCoral : Block
	{
		public override string Id => "minecraft:dead_bubble_coral";
	} // class

	public partial class DeadFireCoral : Block
	{
		public override string Id => "minecraft:dead_fire_coral";
	} // class

	public partial class DeadHornCoral : Block
	{
		public override string Id => "minecraft:dead_horn_coral";
	} // class

	public partial class DeadTubeCoral : Block
	{
		public override string Id => "minecraft:dead_tube_coral";
	} // class

	public partial class Deadbush : Block
	{
		public override string Id => "minecraft:deadbush";
	} // class

	public partial class DecoratedPot : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };

		public override string Id => "minecraft:decorated_pot";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction);
		} // method
	} // class

	public partial class Deepslate : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:deepslate";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class DeepslateBrickDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:deepslate_brick_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class DeepslateBrickSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:deepslate_brick_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class DeepslateBrickStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:deepslate_brick_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class DeepslateBrickWall : Block
	{
		private readonly BlockStateString _wallConnectionTypeEast = new BlockStateString() { Name = "wall_connection_type_east", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeNorth = new BlockStateString() { Name = "wall_connection_type_north", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeSouth = new BlockStateString() { Name = "wall_connection_type_south", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeWest = new BlockStateString() { Name = "wall_connection_type_west", Value = "none" };
		private readonly BlockStateByte _wallPostBit = new BlockStateByte() { Name = "wall_post_bit", Value = 0 };

		public override string Id => "minecraft:deepslate_brick_wall";

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeEast { get => _wallConnectionTypeEast.Value; set => NotifyStateUpdate(_wallConnectionTypeEast, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeNorth { get => _wallConnectionTypeNorth.Value; set => NotifyStateUpdate(_wallConnectionTypeNorth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeSouth { get => _wallConnectionTypeSouth.Value; set => NotifyStateUpdate(_wallConnectionTypeSouth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeWest { get => _wallConnectionTypeWest.Value; set => NotifyStateUpdate(_wallConnectionTypeWest, value); }

		[StateBit]
		public bool WallPostBit { get => Convert.ToBoolean(_wallPostBit.Value); set => NotifyStateUpdate(_wallPostBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wall_connection_type_east":
						NotifyStateUpdate(_wallConnectionTypeEast, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_north":
						NotifyStateUpdate(_wallConnectionTypeNorth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_south":
						NotifyStateUpdate(_wallConnectionTypeSouth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_west":
						NotifyStateUpdate(_wallConnectionTypeWest, s.Value);
						break;
					case BlockStateByte s when s.Name == "wall_post_bit":
						NotifyStateUpdate(_wallPostBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _wallConnectionTypeEast;
			yield return _wallConnectionTypeNorth;
			yield return _wallConnectionTypeSouth;
			yield return _wallConnectionTypeWest;
			yield return _wallPostBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _wallConnectionTypeEast, _wallConnectionTypeNorth, _wallConnectionTypeSouth, _wallConnectionTypeWest, _wallPostBit);
		} // method
	} // class

	public partial class DeepslateBricks : Block
	{
		public override string Id => "minecraft:deepslate_bricks";
	} // class

	public partial class DeepslateCoalOre : Block
	{
		public override string Id => "minecraft:deepslate_coal_ore";
	} // class

	public partial class DeepslateCopperOre : Block
	{
		public override string Id => "minecraft:deepslate_copper_ore";
	} // class

	public partial class DeepslateDiamondOre : Block
	{
		public override string Id => "minecraft:deepslate_diamond_ore";
	} // class

	public partial class DeepslateEmeraldOre : Block
	{
		public override string Id => "minecraft:deepslate_emerald_ore";
	} // class

	public partial class DeepslateGoldOre : Block
	{
		public override string Id => "minecraft:deepslate_gold_ore";
	} // class

	public partial class DeepslateIronOre : Block
	{
		public override string Id => "minecraft:deepslate_iron_ore";
	} // class

	public partial class DeepslateLapisOre : Block
	{
		public override string Id => "minecraft:deepslate_lapis_ore";
	} // class

	public partial class DeepslateRedstoneOre : Block
	{
		public override string Id => "minecraft:deepslate_redstone_ore";
	} // class

	public partial class DeepslateTileDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:deepslate_tile_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class DeepslateTileSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:deepslate_tile_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class DeepslateTileStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:deepslate_tile_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class DeepslateTileWall : Block
	{
		private readonly BlockStateString _wallConnectionTypeEast = new BlockStateString() { Name = "wall_connection_type_east", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeNorth = new BlockStateString() { Name = "wall_connection_type_north", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeSouth = new BlockStateString() { Name = "wall_connection_type_south", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeWest = new BlockStateString() { Name = "wall_connection_type_west", Value = "none" };
		private readonly BlockStateByte _wallPostBit = new BlockStateByte() { Name = "wall_post_bit", Value = 0 };

		public override string Id => "minecraft:deepslate_tile_wall";

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeEast { get => _wallConnectionTypeEast.Value; set => NotifyStateUpdate(_wallConnectionTypeEast, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeNorth { get => _wallConnectionTypeNorth.Value; set => NotifyStateUpdate(_wallConnectionTypeNorth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeSouth { get => _wallConnectionTypeSouth.Value; set => NotifyStateUpdate(_wallConnectionTypeSouth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeWest { get => _wallConnectionTypeWest.Value; set => NotifyStateUpdate(_wallConnectionTypeWest, value); }

		[StateBit]
		public bool WallPostBit { get => Convert.ToBoolean(_wallPostBit.Value); set => NotifyStateUpdate(_wallPostBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wall_connection_type_east":
						NotifyStateUpdate(_wallConnectionTypeEast, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_north":
						NotifyStateUpdate(_wallConnectionTypeNorth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_south":
						NotifyStateUpdate(_wallConnectionTypeSouth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_west":
						NotifyStateUpdate(_wallConnectionTypeWest, s.Value);
						break;
					case BlockStateByte s when s.Name == "wall_post_bit":
						NotifyStateUpdate(_wallPostBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _wallConnectionTypeEast;
			yield return _wallConnectionTypeNorth;
			yield return _wallConnectionTypeSouth;
			yield return _wallConnectionTypeWest;
			yield return _wallPostBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _wallConnectionTypeEast, _wallConnectionTypeNorth, _wallConnectionTypeSouth, _wallConnectionTypeWest, _wallPostBit);
		} // method
	} // class

	public partial class DeepslateTiles : Block
	{
		public override string Id => "minecraft:deepslate_tiles";
	} // class

	public partial class Deny : Block
	{
		public override string Id => "minecraft:deny";
	} // class

	public partial class DetectorRail : Block
	{
		private readonly BlockStateByte _railDataBit = new BlockStateByte() { Name = "rail_data_bit", Value = 0 };
		private readonly BlockStateInt _railDirection = new BlockStateInt() { Name = "rail_direction", Value = 0 };

		public override string Id => "minecraft:detector_rail";

		[StateBit]
		public bool RailDataBit { get => Convert.ToBoolean(_railDataBit.Value); set => NotifyStateUpdate(_railDataBit, value); }

		[StateRange(0, 5)]
		public int RailDirection { get => _railDirection.Value; set => NotifyStateUpdate(_railDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "rail_data_bit":
						NotifyStateUpdate(_railDataBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "rail_direction":
						NotifyStateUpdate(_railDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _railDataBit;
			yield return _railDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _railDataBit, _railDirection);
		} // method
	} // class

	public partial class DiamondBlock : Block
	{
		public override string Id => "minecraft:diamond_block";
	} // class

	public partial class DiamondOre : Block
	{
		public override string Id => "minecraft:diamond_ore";
	} // class

	public partial class DioriteStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:diorite_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class Dirt : Block
	{
		private readonly BlockStateString _dirtType = new BlockStateString() { Name = "dirt_type", Value = "normal" };

		public override string Id => "minecraft:dirt";

		[StateEnum("coarse", "normal")]
		public string DirtType { get => _dirtType.Value; set => NotifyStateUpdate(_dirtType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "dirt_type":
						NotifyStateUpdate(_dirtType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _dirtType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _dirtType);
		} // method
	} // class

	public partial class DirtWithRoots : Block
	{
		public override string Id => "minecraft:dirt_with_roots";
	} // class

	public partial class Dispenser : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateByte _triggeredBit = new BlockStateByte() { Name = "triggered_bit", Value = 0 };

		public override string Id => "minecraft:dispenser";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateBit]
		public bool TriggeredBit { get => Convert.ToBoolean(_triggeredBit.Value); set => NotifyStateUpdate(_triggeredBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "triggered_bit":
						NotifyStateUpdate(_triggeredBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
			yield return _triggeredBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection, _triggeredBit);
		} // method
	} // class

	public partial class DoubleCutCopperSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:double_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class DoublePlant : Block
	{
		private readonly BlockStateString _doublePlantType = new BlockStateString() { Name = "double_plant_type", Value = "sunflower" };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:double_plant";

		[StateEnum("fern", "grass", "paeonia", "rose", "sunflower", "syringa")]
		public string DoublePlantType { get => _doublePlantType.Value; set => NotifyStateUpdate(_doublePlantType, value); }

		[StateBit]
		public bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "double_plant_type":
						NotifyStateUpdate(_doublePlantType, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _doublePlantType;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _doublePlantType, _upperBlockBit);
		} // method
	} // class

	public partial class DoubleStoneBlockSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };
		private readonly BlockStateString _stoneSlabType = new BlockStateString() { Name = "stone_slab_type", Value = "smooth_stone" };

		public override string Id => "minecraft:double_stone_block_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		[StateEnum("brick", "cobblestone", "nether_brick", "quartz", "sandstone", "smooth_stone", "stone_brick", "wood")]
		public string StoneSlabType { get => _stoneSlabType.Value; set => NotifyStateUpdate(_stoneSlabType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
					case BlockStateString s when s.Name == "stone_slab_type":
						NotifyStateUpdate(_stoneSlabType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
			yield return _stoneSlabType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf, _stoneSlabType);
		} // method
	} // class

	public partial class DoubleStoneBlockSlab2 : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };
		private readonly BlockStateString _stoneSlabType2 = new BlockStateString() { Name = "stone_slab_type_2", Value = "red_sandstone" };

		public override string Id => "minecraft:double_stone_block_slab2";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		[StateEnum("mossy_cobblestone", "prismarine_brick", "prismarine_dark", "prismarine_rough", "purpur", "red_nether_brick", "red_sandstone", "smooth_sandstone")]
		public string StoneSlabType2 { get => _stoneSlabType2.Value; set => NotifyStateUpdate(_stoneSlabType2, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
					case BlockStateString s when s.Name == "stone_slab_type_2":
						NotifyStateUpdate(_stoneSlabType2, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
			yield return _stoneSlabType2;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf, _stoneSlabType2);
		} // method
	} // class

	public partial class DoubleStoneBlockSlab3 : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };
		private readonly BlockStateString _stoneSlabType3 = new BlockStateString() { Name = "stone_slab_type_3", Value = "end_stone_brick" };

		public override string Id => "minecraft:double_stone_block_slab3";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		[StateEnum("andesite", "diorite", "end_stone_brick", "granite", "polished_andesite", "polished_diorite", "polished_granite", "smooth_red_sandstone")]
		public string StoneSlabType3 { get => _stoneSlabType3.Value; set => NotifyStateUpdate(_stoneSlabType3, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
					case BlockStateString s when s.Name == "stone_slab_type_3":
						NotifyStateUpdate(_stoneSlabType3, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
			yield return _stoneSlabType3;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf, _stoneSlabType3);
		} // method
	} // class

	public partial class DoubleStoneBlockSlab4 : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };
		private readonly BlockStateString _stoneSlabType4 = new BlockStateString() { Name = "stone_slab_type_4", Value = "mossy_stone_brick" };

		public override string Id => "minecraft:double_stone_block_slab4";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		[StateEnum("cut_red_sandstone", "cut_sandstone", "mossy_stone_brick", "smooth_quartz", "stone")]
		public string StoneSlabType4 { get => _stoneSlabType4.Value; set => NotifyStateUpdate(_stoneSlabType4, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
					case BlockStateString s when s.Name == "stone_slab_type_4":
						NotifyStateUpdate(_stoneSlabType4, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
			yield return _stoneSlabType4;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf, _stoneSlabType4);
		} // method
	} // class

	public partial class DoubleWoodenSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };
		private readonly BlockStateString _woodType = new BlockStateString() { Name = "wood_type", Value = "oak" };

		public override string Id => "minecraft:double_wooden_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		[StateEnum("acacia", "birch", "dark_oak", "jungle", "oak", "spruce")]
		public string WoodType { get => _woodType.Value; set => NotifyStateUpdate(_woodType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
					case BlockStateString s when s.Name == "wood_type":
						NotifyStateUpdate(_woodType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
			yield return _woodType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf, _woodType);
		} // method
	} // class

	public partial class DragonEgg : Block
	{
		public override string Id => "minecraft:dragon_egg";
	} // class

	public partial class DriedKelpBlock : Block
	{
		public override string Id => "minecraft:dried_kelp_block";
	} // class

	public partial class DripstoneBlock : Block
	{
		public override string Id => "minecraft:dripstone_block";
	} // class

	public partial class Dropper : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateByte _triggeredBit = new BlockStateByte() { Name = "triggered_bit", Value = 0 };

		public override string Id => "minecraft:dropper";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateBit]
		public bool TriggeredBit { get => Convert.ToBoolean(_triggeredBit.Value); set => NotifyStateUpdate(_triggeredBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "triggered_bit":
						NotifyStateUpdate(_triggeredBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
			yield return _triggeredBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection, _triggeredBit);
		} // method
	} // class

	public partial class Element0 : ElementBase
	{
		public override string Id => "minecraft:element_0";
	} // class

	public partial class Element1 : ElementBase
	{
		public override string Id => "minecraft:element_1";
	} // class

	public partial class Element10 : ElementBase
	{
		public override string Id => "minecraft:element_10";
	} // class

	public partial class Element100 : ElementBase
	{
		public override string Id => "minecraft:element_100";
	} // class

	public partial class Element101 : ElementBase
	{
		public override string Id => "minecraft:element_101";
	} // class

	public partial class Element102 : ElementBase
	{
		public override string Id => "minecraft:element_102";
	} // class

	public partial class Element103 : ElementBase
	{
		public override string Id => "minecraft:element_103";
	} // class

	public partial class Element104 : ElementBase
	{
		public override string Id => "minecraft:element_104";
	} // class

	public partial class Element105 : ElementBase
	{
		public override string Id => "minecraft:element_105";
	} // class

	public partial class Element106 : ElementBase
	{
		public override string Id => "minecraft:element_106";
	} // class

	public partial class Element107 : ElementBase
	{
		public override string Id => "minecraft:element_107";
	} // class

	public partial class Element108 : ElementBase
	{
		public override string Id => "minecraft:element_108";
	} // class

	public partial class Element109 : ElementBase
	{
		public override string Id => "minecraft:element_109";
	} // class

	public partial class Element11 : ElementBase
	{
		public override string Id => "minecraft:element_11";
	} // class

	public partial class Element110 : ElementBase
	{
		public override string Id => "minecraft:element_110";
	} // class

	public partial class Element111 : ElementBase
	{
		public override string Id => "minecraft:element_111";
	} // class

	public partial class Element112 : ElementBase
	{
		public override string Id => "minecraft:element_112";
	} // class

	public partial class Element113 : ElementBase
	{
		public override string Id => "minecraft:element_113";
	} // class

	public partial class Element114 : ElementBase
	{
		public override string Id => "minecraft:element_114";
	} // class

	public partial class Element115 : ElementBase
	{
		public override string Id => "minecraft:element_115";
	} // class

	public partial class Element116 : ElementBase
	{
		public override string Id => "minecraft:element_116";
	} // class

	public partial class Element117 : ElementBase
	{
		public override string Id => "minecraft:element_117";
	} // class

	public partial class Element118 : ElementBase
	{
		public override string Id => "minecraft:element_118";
	} // class

	public partial class Element12 : ElementBase
	{
		public override string Id => "minecraft:element_12";
	} // class

	public partial class Element13 : ElementBase
	{
		public override string Id => "minecraft:element_13";
	} // class

	public partial class Element14 : ElementBase
	{
		public override string Id => "minecraft:element_14";
	} // class

	public partial class Element15 : ElementBase
	{
		public override string Id => "minecraft:element_15";
	} // class

	public partial class Element16 : ElementBase
	{
		public override string Id => "minecraft:element_16";
	} // class

	public partial class Element17 : ElementBase
	{
		public override string Id => "minecraft:element_17";
	} // class

	public partial class Element18 : ElementBase
	{
		public override string Id => "minecraft:element_18";
	} // class

	public partial class Element19 : ElementBase
	{
		public override string Id => "minecraft:element_19";
	} // class

	public partial class Element2 : ElementBase
	{
		public override string Id => "minecraft:element_2";
	} // class

	public partial class Element20 : ElementBase
	{
		public override string Id => "minecraft:element_20";
	} // class

	public partial class Element21 : ElementBase
	{
		public override string Id => "minecraft:element_21";
	} // class

	public partial class Element22 : ElementBase
	{
		public override string Id => "minecraft:element_22";
	} // class

	public partial class Element23 : ElementBase
	{
		public override string Id => "minecraft:element_23";
	} // class

	public partial class Element24 : ElementBase
	{
		public override string Id => "minecraft:element_24";
	} // class

	public partial class Element25 : ElementBase
	{
		public override string Id => "minecraft:element_25";
	} // class

	public partial class Element26 : ElementBase
	{
		public override string Id => "minecraft:element_26";
	} // class

	public partial class Element27 : ElementBase
	{
		public override string Id => "minecraft:element_27";
	} // class

	public partial class Element28 : ElementBase
	{
		public override string Id => "minecraft:element_28";
	} // class

	public partial class Element29 : ElementBase
	{
		public override string Id => "minecraft:element_29";
	} // class

	public partial class Element3 : ElementBase
	{
		public override string Id => "minecraft:element_3";
	} // class

	public partial class Element30 : ElementBase
	{
		public override string Id => "minecraft:element_30";
	} // class

	public partial class Element31 : ElementBase
	{
		public override string Id => "minecraft:element_31";
	} // class

	public partial class Element32 : ElementBase
	{
		public override string Id => "minecraft:element_32";
	} // class

	public partial class Element33 : ElementBase
	{
		public override string Id => "minecraft:element_33";
	} // class

	public partial class Element34 : ElementBase
	{
		public override string Id => "minecraft:element_34";
	} // class

	public partial class Element35 : ElementBase
	{
		public override string Id => "minecraft:element_35";
	} // class

	public partial class Element36 : ElementBase
	{
		public override string Id => "minecraft:element_36";
	} // class

	public partial class Element37 : ElementBase
	{
		public override string Id => "minecraft:element_37";
	} // class

	public partial class Element38 : ElementBase
	{
		public override string Id => "minecraft:element_38";
	} // class

	public partial class Element39 : ElementBase
	{
		public override string Id => "minecraft:element_39";
	} // class

	public partial class Element4 : ElementBase
	{
		public override string Id => "minecraft:element_4";
	} // class

	public partial class Element40 : ElementBase
	{
		public override string Id => "minecraft:element_40";
	} // class

	public partial class Element41 : ElementBase
	{
		public override string Id => "minecraft:element_41";
	} // class

	public partial class Element42 : ElementBase
	{
		public override string Id => "minecraft:element_42";
	} // class

	public partial class Element43 : ElementBase
	{
		public override string Id => "minecraft:element_43";
	} // class

	public partial class Element44 : ElementBase
	{
		public override string Id => "minecraft:element_44";
	} // class

	public partial class Element45 : ElementBase
	{
		public override string Id => "minecraft:element_45";
	} // class

	public partial class Element46 : ElementBase
	{
		public override string Id => "minecraft:element_46";
	} // class

	public partial class Element47 : ElementBase
	{
		public override string Id => "minecraft:element_47";
	} // class

	public partial class Element48 : ElementBase
	{
		public override string Id => "minecraft:element_48";
	} // class

	public partial class Element49 : ElementBase
	{
		public override string Id => "minecraft:element_49";
	} // class

	public partial class Element5 : ElementBase
	{
		public override string Id => "minecraft:element_5";
	} // class

	public partial class Element50 : ElementBase
	{
		public override string Id => "minecraft:element_50";
	} // class

	public partial class Element51 : ElementBase
	{
		public override string Id => "minecraft:element_51";
	} // class

	public partial class Element52 : ElementBase
	{
		public override string Id => "minecraft:element_52";
	} // class

	public partial class Element53 : ElementBase
	{
		public override string Id => "minecraft:element_53";
	} // class

	public partial class Element54 : ElementBase
	{
		public override string Id => "minecraft:element_54";
	} // class

	public partial class Element55 : ElementBase
	{
		public override string Id => "minecraft:element_55";
	} // class

	public partial class Element56 : ElementBase
	{
		public override string Id => "minecraft:element_56";
	} // class

	public partial class Element57 : ElementBase
	{
		public override string Id => "minecraft:element_57";
	} // class

	public partial class Element58 : ElementBase
	{
		public override string Id => "minecraft:element_58";
	} // class

	public partial class Element59 : ElementBase
	{
		public override string Id => "minecraft:element_59";
	} // class

	public partial class Element6 : ElementBase
	{
		public override string Id => "minecraft:element_6";
	} // class

	public partial class Element60 : ElementBase
	{
		public override string Id => "minecraft:element_60";
	} // class

	public partial class Element61 : ElementBase
	{
		public override string Id => "minecraft:element_61";
	} // class

	public partial class Element62 : ElementBase
	{
		public override string Id => "minecraft:element_62";
	} // class

	public partial class Element63 : ElementBase
	{
		public override string Id => "minecraft:element_63";
	} // class

	public partial class Element64 : ElementBase
	{
		public override string Id => "minecraft:element_64";
	} // class

	public partial class Element65 : ElementBase
	{
		public override string Id => "minecraft:element_65";
	} // class

	public partial class Element66 : ElementBase
	{
		public override string Id => "minecraft:element_66";
	} // class

	public partial class Element67 : ElementBase
	{
		public override string Id => "minecraft:element_67";
	} // class

	public partial class Element68 : ElementBase
	{
		public override string Id => "minecraft:element_68";
	} // class

	public partial class Element69 : ElementBase
	{
		public override string Id => "minecraft:element_69";
	} // class

	public partial class Element7 : ElementBase
	{
		public override string Id => "minecraft:element_7";
	} // class

	public partial class Element70 : ElementBase
	{
		public override string Id => "minecraft:element_70";
	} // class

	public partial class Element71 : ElementBase
	{
		public override string Id => "minecraft:element_71";
	} // class

	public partial class Element72 : ElementBase
	{
		public override string Id => "minecraft:element_72";
	} // class

	public partial class Element73 : ElementBase
	{
		public override string Id => "minecraft:element_73";
	} // class

	public partial class Element74 : ElementBase
	{
		public override string Id => "minecraft:element_74";
	} // class

	public partial class Element75 : ElementBase
	{
		public override string Id => "minecraft:element_75";
	} // class

	public partial class Element76 : ElementBase
	{
		public override string Id => "minecraft:element_76";
	} // class

	public partial class Element77 : ElementBase
	{
		public override string Id => "minecraft:element_77";
	} // class

	public partial class Element78 : ElementBase
	{
		public override string Id => "minecraft:element_78";
	} // class

	public partial class Element79 : ElementBase
	{
		public override string Id => "minecraft:element_79";
	} // class

	public partial class Element8 : ElementBase
	{
		public override string Id => "minecraft:element_8";
	} // class

	public partial class Element80 : ElementBase
	{
		public override string Id => "minecraft:element_80";
	} // class

	public partial class Element81 : ElementBase
	{
		public override string Id => "minecraft:element_81";
	} // class

	public partial class Element82 : ElementBase
	{
		public override string Id => "minecraft:element_82";
	} // class

	public partial class Element83 : ElementBase
	{
		public override string Id => "minecraft:element_83";
	} // class

	public partial class Element84 : ElementBase
	{
		public override string Id => "minecraft:element_84";
	} // class

	public partial class Element85 : ElementBase
	{
		public override string Id => "minecraft:element_85";
	} // class

	public partial class Element86 : ElementBase
	{
		public override string Id => "minecraft:element_86";
	} // class

	public partial class Element87 : ElementBase
	{
		public override string Id => "minecraft:element_87";
	} // class

	public partial class Element88 : ElementBase
	{
		public override string Id => "minecraft:element_88";
	} // class

	public partial class Element89 : ElementBase
	{
		public override string Id => "minecraft:element_89";
	} // class

	public partial class Element9 : ElementBase
	{
		public override string Id => "minecraft:element_9";
	} // class

	public partial class Element90 : ElementBase
	{
		public override string Id => "minecraft:element_90";
	} // class

	public partial class Element91 : ElementBase
	{
		public override string Id => "minecraft:element_91";
	} // class

	public partial class Element92 : ElementBase
	{
		public override string Id => "minecraft:element_92";
	} // class

	public partial class Element93 : ElementBase
	{
		public override string Id => "minecraft:element_93";
	} // class

	public partial class Element94 : ElementBase
	{
		public override string Id => "minecraft:element_94";
	} // class

	public partial class Element95 : ElementBase
	{
		public override string Id => "minecraft:element_95";
	} // class

	public partial class Element96 : ElementBase
	{
		public override string Id => "minecraft:element_96";
	} // class

	public partial class Element97 : ElementBase
	{
		public override string Id => "minecraft:element_97";
	} // class

	public partial class Element98 : ElementBase
	{
		public override string Id => "minecraft:element_98";
	} // class

	public partial class Element99 : ElementBase
	{
		public override string Id => "minecraft:element_99";
	} // class

	public partial class EmeraldBlock : Block
	{
		public override string Id => "minecraft:emerald_block";
	} // class

	public partial class EmeraldOre : Block
	{
		public override string Id => "minecraft:emerald_ore";
	} // class

	public partial class EnchantingTable : Block
	{
		public override string Id => "minecraft:enchanting_table";
	} // class

	public partial class EndBrickStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:end_brick_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class EndBricks : Block
	{
		public override string Id => "minecraft:end_bricks";
	} // class

	public partial class EndGateway : Block
	{
		public override string Id => "minecraft:end_gateway";
	} // class

	public partial class EndPortal : Block
	{
		public override string Id => "minecraft:end_portal";
	} // class

	public partial class EndPortalFrame : Block
	{
		private readonly BlockStateByte _endPortalEyeBit = new BlockStateByte() { Name = "end_portal_eye_bit", Value = 0 };
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:end_portal_frame";

		[StateBit]
		public bool EndPortalEyeBit { get => Convert.ToBoolean(_endPortalEyeBit.Value); set => NotifyStateUpdate(_endPortalEyeBit, value); }

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "end_portal_eye_bit":
						NotifyStateUpdate(_endPortalEyeBit, s.Value);
						break;
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _endPortalEyeBit;
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _endPortalEyeBit, _cardinalDirection);
		} // method
	} // class

	public partial class EndRod : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:end_rod";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class EndStone : Block
	{
		public override string Id => "minecraft:end_stone";
	} // class

	public partial class EnderChest
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "north" };

		public override string Id => "minecraft:ender_chest";

		[StateEnum("east", "north", "south", "west")]
		public override string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class ExposedCopper : Block
	{
		public override string Id => "minecraft:exposed_copper";
	} // class

	public partial class ExposedCutCopper : Block
	{
		public override string Id => "minecraft:exposed_cut_copper";
	} // class

	public partial class ExposedCutCopperSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:exposed_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class ExposedCutCopperStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:exposed_cut_copper_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class ExposedDoubleCutCopperSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:exposed_double_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class Farmland : Block
	{
		private readonly BlockStateInt _moisturizedAmount = new BlockStateInt() { Name = "moisturized_amount", Value = 0 };

		public override string Id => "minecraft:farmland";

		[StateRange(0, 7)]
		public int MoisturizedAmount { get => _moisturizedAmount.Value; set => NotifyStateUpdate(_moisturizedAmount, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "moisturized_amount":
						NotifyStateUpdate(_moisturizedAmount, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _moisturizedAmount;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _moisturizedAmount);
		} // method
	} // class

	public partial class FenceGate
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class Fire : Block
	{
		private readonly BlockStateInt _age = new BlockStateInt() { Name = "age", Value = 0 };

		public override string Id => "minecraft:fire";

		[StateRange(0, 15)]
		public int Age { get => _age.Value; set => NotifyStateUpdate(_age, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "age":
						NotifyStateUpdate(_age, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _age;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _age);
		} // method
	} // class

	public partial class FireCoral : Block
	{
		public override string Id => "minecraft:fire_coral";
	} // class

	public partial class FletchingTable : Block
	{
		public override string Id => "minecraft:fletching_table";
	} // class

	public partial class FlowerPot : Block
	{
		private readonly BlockStateByte _updateBit = new BlockStateByte() { Name = "update_bit", Value = 0 };

		public override string Id => "minecraft:flower_pot";

		[StateBit]
		public bool UpdateBit { get => Convert.ToBoolean(_updateBit.Value); set => NotifyStateUpdate(_updateBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "update_bit":
						NotifyStateUpdate(_updateBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _updateBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _updateBit);
		} // method
	} // class

	public partial class FloweringAzalea : Block
	{
		public override string Id => "minecraft:flowering_azalea";
	} // class

	public partial class FlowingLava
	{
		private readonly BlockStateInt _liquidDepth = new BlockStateInt() { Name = "liquid_depth", Value = 0 };

		public override string Id => "minecraft:flowing_lava";

		[StateRange(0, 15)]
		public override int LiquidDepth { get => _liquidDepth.Value; set => NotifyStateUpdate(_liquidDepth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "liquid_depth":
						NotifyStateUpdate(_liquidDepth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _liquidDepth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _liquidDepth);
		} // method
	} // class

	public partial class FlowingWater
	{
		private readonly BlockStateInt _liquidDepth = new BlockStateInt() { Name = "liquid_depth", Value = 0 };

		public override string Id => "minecraft:flowing_water";

		[StateRange(0, 15)]
		public override int LiquidDepth { get => _liquidDepth.Value; set => NotifyStateUpdate(_liquidDepth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "liquid_depth":
						NotifyStateUpdate(_liquidDepth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _liquidDepth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _liquidDepth);
		} // method
	} // class

	public partial class Frame : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateByte _itemFrameMapBit = new BlockStateByte() { Name = "item_frame_map_bit", Value = 0 };
		private readonly BlockStateByte _itemFramePhotoBit = new BlockStateByte() { Name = "item_frame_photo_bit", Value = 0 };

		public override string Id => "minecraft:frame";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateBit]
		public bool ItemFrameMapBit { get => Convert.ToBoolean(_itemFrameMapBit.Value); set => NotifyStateUpdate(_itemFrameMapBit, value); }

		[StateBit]
		public bool ItemFramePhotoBit { get => Convert.ToBoolean(_itemFramePhotoBit.Value); set => NotifyStateUpdate(_itemFramePhotoBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "item_frame_map_bit":
						NotifyStateUpdate(_itemFrameMapBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "item_frame_photo_bit":
						NotifyStateUpdate(_itemFramePhotoBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
			yield return _itemFrameMapBit;
			yield return _itemFramePhotoBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection, _itemFrameMapBit, _itemFramePhotoBit);
		} // method
	} // class

	public partial class FrogSpawn : Block
	{
		public override string Id => "minecraft:frog_spawn";
	} // class

	public partial class FrostedIce : Block
	{
		private readonly BlockStateInt _age = new BlockStateInt() { Name = "age", Value = 0 };

		public override string Id => "minecraft:frosted_ice";

		[StateRange(0, 3)]
		public int Age { get => _age.Value; set => NotifyStateUpdate(_age, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "age":
						NotifyStateUpdate(_age, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _age;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _age);
		} // method
	} // class

	public partial class Furnace
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:furnace";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class GildedBlackstone : Block
	{
		public override string Id => "minecraft:gilded_blackstone";
	} // class

	public partial class Glass : Block
	{
		public override string Id => "minecraft:glass";
	} // class

	public partial class GlassPane : Block
	{
		public override string Id => "minecraft:glass_pane";
	} // class

	public partial class GlowFrame : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateByte _itemFrameMapBit = new BlockStateByte() { Name = "item_frame_map_bit", Value = 0 };
		private readonly BlockStateByte _itemFramePhotoBit = new BlockStateByte() { Name = "item_frame_photo_bit", Value = 0 };

		public override string Id => "minecraft:glow_frame";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateBit]
		public bool ItemFrameMapBit { get => Convert.ToBoolean(_itemFrameMapBit.Value); set => NotifyStateUpdate(_itemFrameMapBit, value); }

		[StateBit]
		public bool ItemFramePhotoBit { get => Convert.ToBoolean(_itemFramePhotoBit.Value); set => NotifyStateUpdate(_itemFramePhotoBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "item_frame_map_bit":
						NotifyStateUpdate(_itemFrameMapBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "item_frame_photo_bit":
						NotifyStateUpdate(_itemFramePhotoBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
			yield return _itemFrameMapBit;
			yield return _itemFramePhotoBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection, _itemFrameMapBit, _itemFramePhotoBit);
		} // method
	} // class

	public partial class GlowLichen : Block
	{
		private readonly BlockStateInt _multiFaceDirectionBits = new BlockStateInt() { Name = "multi_face_direction_bits", Value = 0 };

		public override string Id => "minecraft:glow_lichen";

		[StateRange(0, 63)]
		public int MultiFaceDirectionBits { get => _multiFaceDirectionBits.Value; set => NotifyStateUpdate(_multiFaceDirectionBits, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "multi_face_direction_bits":
						NotifyStateUpdate(_multiFaceDirectionBits, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _multiFaceDirectionBits;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _multiFaceDirectionBits);
		} // method
	} // class

	public partial class Glowingobsidian : Block
	{
		public override string Id => "minecraft:glowingobsidian";
	} // class

	public partial class Glowstone : Block
	{
		public override string Id => "minecraft:glowstone";
	} // class

	public partial class GoldBlock : Block
	{
		public override string Id => "minecraft:gold_block";
	} // class

	public partial class GoldOre : Block
	{
		public override string Id => "minecraft:gold_ore";
	} // class

	public partial class GoldenRail : Block
	{
		private readonly BlockStateByte _railDataBit = new BlockStateByte() { Name = "rail_data_bit", Value = 0 };
		private readonly BlockStateInt _railDirection = new BlockStateInt() { Name = "rail_direction", Value = 0 };

		public override string Id => "minecraft:golden_rail";

		[StateBit]
		public bool RailDataBit { get => Convert.ToBoolean(_railDataBit.Value); set => NotifyStateUpdate(_railDataBit, value); }

		[StateRange(0, 5)]
		public int RailDirection { get => _railDirection.Value; set => NotifyStateUpdate(_railDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "rail_data_bit":
						NotifyStateUpdate(_railDataBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "rail_direction":
						NotifyStateUpdate(_railDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _railDataBit;
			yield return _railDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _railDataBit, _railDirection);
		} // method
	} // class

	public partial class GraniteStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:granite_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class Grass : Block
	{
		public override string Id => "minecraft:grass";
	} // class

	public partial class GrassPath : Block
	{
		public override string Id => "minecraft:grass_path";
	} // class

	public partial class Gravel : Block
	{
		public override string Id => "minecraft:gravel";
	} // class

	public partial class GrayCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:gray_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class GrayCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:gray_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class GrayCarpet : CarpetBase
	{
		public override string Id => "minecraft:gray_carpet";
	} // class

	public partial class GrayConcrete : ConcreteBase
	{
		public override string Id => "minecraft:gray_concrete";
	} // class

	public partial class GrayConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:gray_concrete_powder";
	} // class

	public partial class GrayGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:gray_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class GrayShulkerBox : Block
	{
		public override string Id => "minecraft:gray_shulker_box";
	} // class

	public partial class GrayStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:gray_stained_glass";
	} // class

	public partial class GrayStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:gray_stained_glass_pane";
	} // class

	public partial class GrayTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:gray_terracotta";
	} // class

	public partial class GrayWool : WoolBase
	{
		public override string Id => "minecraft:gray_wool";
	} // class

	public partial class GreenCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:green_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class GreenCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:green_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class GreenCarpet : CarpetBase
	{
		public override string Id => "minecraft:green_carpet";
	} // class

	public partial class GreenConcrete : ConcreteBase
	{
		public override string Id => "minecraft:green_concrete";
	} // class

	public partial class GreenConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:green_concrete_powder";
	} // class

	public partial class GreenGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:green_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class GreenShulkerBox : Block
	{
		public override string Id => "minecraft:green_shulker_box";
	} // class

	public partial class GreenStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:green_stained_glass";
	} // class

	public partial class GreenStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:green_stained_glass_pane";
	} // class

	public partial class GreenTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:green_terracotta";
	} // class

	public partial class GreenWool : WoolBase
	{
		public override string Id => "minecraft:green_wool";
	} // class

	public partial class Grindstone : Block
	{
		private readonly BlockStateString _attachment = new BlockStateString() { Name = "attachment", Value = "standing" };
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };

		public override string Id => "minecraft:grindstone";

		[StateEnum("hanging", "multiple", "side", "standing")]
		public string Attachment { get => _attachment.Value; set => NotifyStateUpdate(_attachment, value); }

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "attachment":
						NotifyStateUpdate(_attachment, s.Value);
						break;
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachment;
			yield return _direction;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachment, _direction);
		} // method
	} // class

	public partial class HangingRoots : Block
	{
		public override string Id => "minecraft:hanging_roots";
	} // class

	public partial class HardGlass : Block
	{
		public override string Id => "minecraft:hard_glass";
	} // class

	public partial class HardGlassPane : Block
	{
		public override string Id => "minecraft:hard_glass_pane";
	} // class

	public partial class HardStainedGlass : StainedGlassBase
	{
		private readonly BlockStateString _color = new BlockStateString() { Name = "color", Value = "white" };

		public override string Id => "minecraft:hard_stained_glass";

		[StateEnum("black", "blue", "brown", "cyan", "gray", "green", "light_blue", "lime", "magenta", "orange", "pink", "purple", "red", "silver", "white", "yellow")]
		public string Color { get => _color.Value; set => NotifyStateUpdate(_color, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "color":
						NotifyStateUpdate(_color, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _color;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _color);
		} // method
	} // class

	public partial class HardStainedGlassPane : StainedGlassPaneBase
	{
		private readonly BlockStateString _color = new BlockStateString() { Name = "color", Value = "white" };

		public override string Id => "minecraft:hard_stained_glass_pane";

		[StateEnum("black", "blue", "brown", "cyan", "gray", "green", "light_blue", "lime", "magenta", "orange", "pink", "purple", "red", "silver", "white", "yellow")]
		public string Color { get => _color.Value; set => NotifyStateUpdate(_color, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "color":
						NotifyStateUpdate(_color, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _color;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _color);
		} // method
	} // class

	public partial class HardenedClay : Block
	{
		public override string Id => "minecraft:hardened_clay";
	} // class

	public partial class HayBlock : Block
	{
		private readonly BlockStateInt _deprecated = new BlockStateInt() { Name = "deprecated", Value = 0 };
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:hay_block";

		[StateRange(0, 3)]
		public int Deprecated { get => _deprecated.Value; set => NotifyStateUpdate(_deprecated, value); }

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "deprecated":
						NotifyStateUpdate(_deprecated, s.Value);
						break;
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _deprecated;
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _deprecated, _pillarAxis);
		} // method
	} // class

	public partial class HeavyWeightedPressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:heavy_weighted_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class HoneyBlock : Block
	{
		public override string Id => "minecraft:honey_block";
	} // class

	public partial class HoneycombBlock : Block
	{
		public override string Id => "minecraft:honeycomb_block";
	} // class

	public partial class Hopper : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateByte _toggleBit = new BlockStateByte() { Name = "toggle_bit", Value = 0 };

		public override string Id => "minecraft:hopper";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateBit]
		public bool ToggleBit { get => Convert.ToBoolean(_toggleBit.Value); set => NotifyStateUpdate(_toggleBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "toggle_bit":
						NotifyStateUpdate(_toggleBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
			yield return _toggleBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection, _toggleBit);
		} // method
	} // class

	public partial class HornCoral : Block
	{
		public override string Id => "minecraft:horn_coral";
	} // class

	public partial class Ice : Block
	{
		public override string Id => "minecraft:ice";
	} // class

	public partial class InfestedDeepslate : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:infested_deepslate";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class InfoUpdate : Block
	{
		public override string Id => "minecraft:info_update";
	} // class

	public partial class InfoUpdate2 : Block
	{
		public override string Id => "minecraft:info_update2";
	} // class

	public partial class InvisibleBedrock : Block
	{
		public override string Id => "minecraft:invisible_bedrock";
	} // class

	public partial class IronBars : Block
	{
		public override string Id => "minecraft:iron_bars";
	} // class

	public partial class IronBlock : Block
	{
		public override string Id => "minecraft:iron_block";
	} // class

	public partial class IronDoor : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:iron_door";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class IronOre : Block
	{
		public override string Id => "minecraft:iron_ore";
	} // class

	public partial class IronTrapdoor : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:iron_trapdoor";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class Jigsaw : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _rotation = new BlockStateInt() { Name = "rotation", Value = 0 };

		public override string Id => "minecraft:jigsaw";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 3)]
		public int Rotation { get => _rotation.Value; set => NotifyStateUpdate(_rotation, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "rotation":
						NotifyStateUpdate(_rotation, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
			yield return _rotation;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection, _rotation);
		} // method
	} // class

	public partial class Jukebox : Block
	{
		public override string Id => "minecraft:jukebox";
	} // class

	public partial class JungleButton
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:jungle_button";

		[StateBit]
		public override bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class JungleDoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:jungle_door";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class JungleFence : Block
	{
		public override string Id => "minecraft:jungle_fence";
	} // class

	public partial class JungleFenceGate
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:jungle_fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class JungleHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:jungle_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class JungleLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:jungle_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class JunglePressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:jungle_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class JungleStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:jungle_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class JungleStandingSign
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:jungle_standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class JungleTrapdoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:jungle_trapdoor";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class JungleWallSign
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:jungle_wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class Kelp : Block
	{
		private readonly BlockStateInt _kelpAge = new BlockStateInt() { Name = "kelp_age", Value = 0 };

		public override string Id => "minecraft:kelp";

		[StateRange(0, 25)]
		public int KelpAge { get => _kelpAge.Value; set => NotifyStateUpdate(_kelpAge, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "kelp_age":
						NotifyStateUpdate(_kelpAge, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _kelpAge;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _kelpAge);
		} // method
	} // class

	public partial class Ladder : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:ladder";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class Lantern : Block
	{
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:lantern";

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _hanging);
		} // method
	} // class

	public partial class LapisBlock : Block
	{
		public override string Id => "minecraft:lapis_block";
	} // class

	public partial class LapisOre : Block
	{
		public override string Id => "minecraft:lapis_ore";
	} // class

	public partial class LargeAmethystBud : Block
	{
		private readonly BlockStateString _blockFace = new BlockStateString() { Name = "minecraft:block_face", Value = "down" };

		public override string Id => "minecraft:large_amethyst_bud";

		[StateEnum("down", "east", "north", "south", "up", "west")]
		public string BlockFace { get => _blockFace.Value; set => NotifyStateUpdate(_blockFace, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:block_face":
						NotifyStateUpdate(_blockFace, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _blockFace;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _blockFace);
		} // method
	} // class

	public partial class Lava
	{
		private readonly BlockStateInt _liquidDepth = new BlockStateInt() { Name = "liquid_depth", Value = 0 };

		public override string Id => "minecraft:lava";

		[StateRange(0, 15)]
		public override int LiquidDepth { get => _liquidDepth.Value; set => NotifyStateUpdate(_liquidDepth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "liquid_depth":
						NotifyStateUpdate(_liquidDepth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _liquidDepth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _liquidDepth);
		} // method
	} // class

	public partial class Leaves : Block
	{
		private readonly BlockStateString _oldLeafType = new BlockStateString() { Name = "old_leaf_type", Value = "oak" };
		private readonly BlockStateByte _persistentBit = new BlockStateByte() { Name = "persistent_bit", Value = 0 };
		private readonly BlockStateByte _updateBit = new BlockStateByte() { Name = "update_bit", Value = 0 };

		public override string Id => "minecraft:leaves";

		[StateEnum("birch", "jungle", "oak", "spruce")]
		public string OldLeafType { get => _oldLeafType.Value; set => NotifyStateUpdate(_oldLeafType, value); }

		[StateBit]
		public bool PersistentBit { get => Convert.ToBoolean(_persistentBit.Value); set => NotifyStateUpdate(_persistentBit, value); }

		[StateBit]
		public bool UpdateBit { get => Convert.ToBoolean(_updateBit.Value); set => NotifyStateUpdate(_updateBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "old_leaf_type":
						NotifyStateUpdate(_oldLeafType, s.Value);
						break;
					case BlockStateByte s when s.Name == "persistent_bit":
						NotifyStateUpdate(_persistentBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "update_bit":
						NotifyStateUpdate(_updateBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _oldLeafType;
			yield return _persistentBit;
			yield return _updateBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _oldLeafType, _persistentBit, _updateBit);
		} // method
	} // class

	public partial class Leaves2 : Block
	{
		private readonly BlockStateString _newLeafType = new BlockStateString() { Name = "new_leaf_type", Value = "acacia" };
		private readonly BlockStateByte _persistentBit = new BlockStateByte() { Name = "persistent_bit", Value = 0 };
		private readonly BlockStateByte _updateBit = new BlockStateByte() { Name = "update_bit", Value = 0 };

		public override string Id => "minecraft:leaves2";

		[StateEnum("acacia", "dark_oak")]
		public string NewLeafType { get => _newLeafType.Value; set => NotifyStateUpdate(_newLeafType, value); }

		[StateBit]
		public bool PersistentBit { get => Convert.ToBoolean(_persistentBit.Value); set => NotifyStateUpdate(_persistentBit, value); }

		[StateBit]
		public bool UpdateBit { get => Convert.ToBoolean(_updateBit.Value); set => NotifyStateUpdate(_updateBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "new_leaf_type":
						NotifyStateUpdate(_newLeafType, s.Value);
						break;
					case BlockStateByte s when s.Name == "persistent_bit":
						NotifyStateUpdate(_persistentBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "update_bit":
						NotifyStateUpdate(_updateBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _newLeafType;
			yield return _persistentBit;
			yield return _updateBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _newLeafType, _persistentBit, _updateBit);
		} // method
	} // class

	public partial class Lectern : Block
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };
		private readonly BlockStateByte _poweredBit = new BlockStateByte() { Name = "powered_bit", Value = 0 };

		public override string Id => "minecraft:lectern";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		[StateBit]
		public bool PoweredBit { get => Convert.ToBoolean(_poweredBit.Value); set => NotifyStateUpdate(_poweredBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "powered_bit":
						NotifyStateUpdate(_poweredBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
			yield return _poweredBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection, _poweredBit);
		} // method
	} // class

	public partial class Lever : Block
	{
		private readonly BlockStateString _leverDirection = new BlockStateString() { Name = "lever_direction", Value = "down_east_west" };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:lever";

		[StateEnum("down_east_west", "down_north_south", "east", "north", "south", "up_east_west", "up_north_south", "west")]
		public string LeverDirection { get => _leverDirection.Value; set => NotifyStateUpdate(_leverDirection, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "lever_direction":
						NotifyStateUpdate(_leverDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _leverDirection;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _leverDirection, _openBit);
		} // method
	} // class

	public partial class LightBlock : Block
	{
		private readonly BlockStateInt _blockLightLevel = new BlockStateInt() { Name = "block_light_level", Value = 0 };

		public override string Id => "minecraft:light_block";

		[StateRange(0, 15)]
		public int BlockLightLevel { get => _blockLightLevel.Value; set => NotifyStateUpdate(_blockLightLevel, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "block_light_level":
						NotifyStateUpdate(_blockLightLevel, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _blockLightLevel;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _blockLightLevel);
		} // method
	} // class

	public partial class LightBlueCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:light_blue_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class LightBlueCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:light_blue_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class LightBlueCarpet : CarpetBase
	{
		public override string Id => "minecraft:light_blue_carpet";
	} // class

	public partial class LightBlueConcrete : ConcreteBase
	{
		public override string Id => "minecraft:light_blue_concrete";
	} // class

	public partial class LightBlueConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:light_blue_concrete_powder";
	} // class

	public partial class LightBlueGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:light_blue_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class LightBlueShulkerBox : Block
	{
		public override string Id => "minecraft:light_blue_shulker_box";
	} // class

	public partial class LightBlueStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:light_blue_stained_glass";
	} // class

	public partial class LightBlueStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:light_blue_stained_glass_pane";
	} // class

	public partial class LightBlueTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:light_blue_terracotta";
	} // class

	public partial class LightBlueWool : WoolBase
	{
		public override string Id => "minecraft:light_blue_wool";
	} // class

	public partial class LightGrayCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:light_gray_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class LightGrayCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:light_gray_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class LightGrayCarpet : CarpetBase
	{
		public override string Id => "minecraft:light_gray_carpet";
	} // class

	public partial class LightGrayConcrete : ConcreteBase
	{
		public override string Id => "minecraft:light_gray_concrete";
	} // class

	public partial class LightGrayConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:light_gray_concrete_powder";
	} // class

	public partial class LightGrayShulkerBox : Block
	{
		public override string Id => "minecraft:light_gray_shulker_box";
	} // class

	public partial class LightGrayStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:light_gray_stained_glass";
	} // class

	public partial class LightGrayStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:light_gray_stained_glass_pane";
	} // class

	public partial class LightGrayTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:light_gray_terracotta";
	} // class

	public partial class LightGrayWool : WoolBase
	{
		public override string Id => "minecraft:light_gray_wool";
	} // class

	public partial class LightWeightedPressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:light_weighted_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class LightningRod : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:lightning_rod";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class LimeCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:lime_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class LimeCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:lime_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class LimeCarpet : CarpetBase
	{
		public override string Id => "minecraft:lime_carpet";
	} // class

	public partial class LimeConcrete : ConcreteBase
	{
		public override string Id => "minecraft:lime_concrete";
	} // class

	public partial class LimeConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:lime_concrete_powder";
	} // class

	public partial class LimeGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:lime_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class LimeShulkerBox : Block
	{
		public override string Id => "minecraft:lime_shulker_box";
	} // class

	public partial class LimeStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:lime_stained_glass";
	} // class

	public partial class LimeStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:lime_stained_glass_pane";
	} // class

	public partial class LimeTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:lime_terracotta";
	} // class

	public partial class LimeWool : WoolBase
	{
		public override string Id => "minecraft:lime_wool";
	} // class

	public partial class LitBlastFurnace
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:lit_blast_furnace";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class LitDeepslateRedstoneOre : Block
	{
		public override string Id => "minecraft:lit_deepslate_redstone_ore";
	} // class

	public partial class LitFurnace
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:lit_furnace";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class LitPumpkin : Block
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:lit_pumpkin";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class LitRedstoneLamp
	{
		public override string Id => "minecraft:lit_redstone_lamp";
	} // class

	public partial class LitRedstoneOre
	{
		public override string Id => "minecraft:lit_redstone_ore";
	} // class

	public partial class LitSmoker : Block
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:lit_smoker";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class Lodestone : Block
	{
		public override string Id => "minecraft:lodestone";
	} // class

	public partial class Loom : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };

		public override string Id => "minecraft:loom";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction);
		} // method
	} // class

	public partial class MagentaCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:magenta_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class MagentaCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:magenta_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class MagentaCarpet : CarpetBase
	{
		public override string Id => "minecraft:magenta_carpet";
	} // class

	public partial class MagentaConcrete : ConcreteBase
	{
		public override string Id => "minecraft:magenta_concrete";
	} // class

	public partial class MagentaConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:magenta_concrete_powder";
	} // class

	public partial class MagentaGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:magenta_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class MagentaShulkerBox : Block
	{
		public override string Id => "minecraft:magenta_shulker_box";
	} // class

	public partial class MagentaStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:magenta_stained_glass";
	} // class

	public partial class MagentaStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:magenta_stained_glass_pane";
	} // class

	public partial class MagentaTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:magenta_terracotta";
	} // class

	public partial class MagentaWool : WoolBase
	{
		public override string Id => "minecraft:magenta_wool";
	} // class

	public partial class Magma : Block
	{
		public override string Id => "minecraft:magma";
	} // class

	public partial class MangroveButton : Block
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:mangrove_button";

		[StateBit]
		public bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class MangroveDoor : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:mangrove_door";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class MangroveDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:mangrove_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class MangroveFence : Block
	{
		public override string Id => "minecraft:mangrove_fence";
	} // class

	public partial class MangroveFenceGate : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:mangrove_fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class MangroveHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:mangrove_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class MangroveLeaves : Block
	{
		private readonly BlockStateByte _persistentBit = new BlockStateByte() { Name = "persistent_bit", Value = 0 };
		private readonly BlockStateByte _updateBit = new BlockStateByte() { Name = "update_bit", Value = 0 };

		public override string Id => "minecraft:mangrove_leaves";

		[StateBit]
		public bool PersistentBit { get => Convert.ToBoolean(_persistentBit.Value); set => NotifyStateUpdate(_persistentBit, value); }

		[StateBit]
		public bool UpdateBit { get => Convert.ToBoolean(_updateBit.Value); set => NotifyStateUpdate(_updateBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "persistent_bit":
						NotifyStateUpdate(_persistentBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "update_bit":
						NotifyStateUpdate(_updateBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _persistentBit;
			yield return _updateBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _persistentBit, _updateBit);
		} // method
	} // class

	public partial class MangroveLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:mangrove_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class MangrovePlanks : Block
	{
		public override string Id => "minecraft:mangrove_planks";
	} // class

	public partial class MangrovePressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:mangrove_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class MangrovePropagule : Block
	{
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };
		private readonly BlockStateInt _propaguleStage = new BlockStateInt() { Name = "propagule_stage", Value = 0 };

		public override string Id => "minecraft:mangrove_propagule";

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		[StateRange(0, 4)]
		public int PropaguleStage { get => _propaguleStage.Value; set => NotifyStateUpdate(_propaguleStage, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
					case BlockStateInt s when s.Name == "propagule_stage":
						NotifyStateUpdate(_propaguleStage, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _hanging;
			yield return _propaguleStage;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _hanging, _propaguleStage);
		} // method
	} // class

	public partial class MangroveRoots : Block
	{
		public override string Id => "minecraft:mangrove_roots";
	} // class

	public partial class MangroveSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:mangrove_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class MangroveStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:mangrove_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class MangroveStandingSign : Block
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:mangrove_standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class MangroveTrapdoor : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:mangrove_trapdoor";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class MangroveWallSign : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:mangrove_wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class MangroveWood : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };
		private readonly BlockStateByte _strippedBit = new BlockStateByte() { Name = "stripped_bit", Value = 0 };

		public override string Id => "minecraft:mangrove_wood";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		[StateBit]
		public bool StrippedBit { get => Convert.ToBoolean(_strippedBit.Value); set => NotifyStateUpdate(_strippedBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
					case BlockStateByte s when s.Name == "stripped_bit":
						NotifyStateUpdate(_strippedBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
			yield return _strippedBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis, _strippedBit);
		} // method
	} // class

	public partial class MediumAmethystBud : Block
	{
		private readonly BlockStateString _blockFace = new BlockStateString() { Name = "minecraft:block_face", Value = "down" };

		public override string Id => "minecraft:medium_amethyst_bud";

		[StateEnum("down", "east", "north", "south", "up", "west")]
		public string BlockFace { get => _blockFace.Value; set => NotifyStateUpdate(_blockFace, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:block_face":
						NotifyStateUpdate(_blockFace, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _blockFace;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _blockFace);
		} // method
	} // class

	public partial class MelonBlock : Block
	{
		public override string Id => "minecraft:melon_block";
	} // class

	public partial class MelonStem : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _growth = new BlockStateInt() { Name = "growth", Value = 0 };

		public override string Id => "minecraft:melon_stem";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 7)]
		public int Growth { get => _growth.Value; set => NotifyStateUpdate(_growth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "growth":
						NotifyStateUpdate(_growth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
			yield return _growth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection, _growth);
		} // method
	} // class

	public partial class MobSpawner : Block
	{
		public override string Id => "minecraft:mob_spawner";
	} // class

	public partial class MonsterEgg : Block
	{
		private readonly BlockStateString _monsterEggStoneType = new BlockStateString() { Name = "monster_egg_stone_type", Value = "stone" };

		public override string Id => "minecraft:monster_egg";

		[StateEnum("chiseled_stone_brick", "cobblestone", "cracked_stone_brick", "mossy_stone_brick", "stone", "stone_brick")]
		public string MonsterEggStoneType { get => _monsterEggStoneType.Value; set => NotifyStateUpdate(_monsterEggStoneType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "monster_egg_stone_type":
						NotifyStateUpdate(_monsterEggStoneType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _monsterEggStoneType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _monsterEggStoneType);
		} // method
	} // class

	public partial class MossBlock : Block
	{
		public override string Id => "minecraft:moss_block";
	} // class

	public partial class MossCarpet : CarpetBase
	{
		public override string Id => "minecraft:moss_carpet";
	} // class

	public partial class MossyCobblestone : Block
	{
		public override string Id => "minecraft:mossy_cobblestone";
	} // class

	public partial class MossyCobblestoneStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:mossy_cobblestone_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class MossyStoneBrickStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:mossy_stone_brick_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class MovingBlock : Block
	{
		public override string Id => "minecraft:moving_block";
	} // class

	public partial class Mud : Block
	{
		public override string Id => "minecraft:mud";
	} // class

	public partial class MudBrickDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:mud_brick_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class MudBrickSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:mud_brick_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class MudBrickStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:mud_brick_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class MudBrickWall : Block
	{
		private readonly BlockStateString _wallConnectionTypeEast = new BlockStateString() { Name = "wall_connection_type_east", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeNorth = new BlockStateString() { Name = "wall_connection_type_north", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeSouth = new BlockStateString() { Name = "wall_connection_type_south", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeWest = new BlockStateString() { Name = "wall_connection_type_west", Value = "none" };
		private readonly BlockStateByte _wallPostBit = new BlockStateByte() { Name = "wall_post_bit", Value = 0 };

		public override string Id => "minecraft:mud_brick_wall";

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeEast { get => _wallConnectionTypeEast.Value; set => NotifyStateUpdate(_wallConnectionTypeEast, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeNorth { get => _wallConnectionTypeNorth.Value; set => NotifyStateUpdate(_wallConnectionTypeNorth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeSouth { get => _wallConnectionTypeSouth.Value; set => NotifyStateUpdate(_wallConnectionTypeSouth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeWest { get => _wallConnectionTypeWest.Value; set => NotifyStateUpdate(_wallConnectionTypeWest, value); }

		[StateBit]
		public bool WallPostBit { get => Convert.ToBoolean(_wallPostBit.Value); set => NotifyStateUpdate(_wallPostBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wall_connection_type_east":
						NotifyStateUpdate(_wallConnectionTypeEast, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_north":
						NotifyStateUpdate(_wallConnectionTypeNorth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_south":
						NotifyStateUpdate(_wallConnectionTypeSouth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_west":
						NotifyStateUpdate(_wallConnectionTypeWest, s.Value);
						break;
					case BlockStateByte s when s.Name == "wall_post_bit":
						NotifyStateUpdate(_wallPostBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _wallConnectionTypeEast;
			yield return _wallConnectionTypeNorth;
			yield return _wallConnectionTypeSouth;
			yield return _wallConnectionTypeWest;
			yield return _wallPostBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _wallConnectionTypeEast, _wallConnectionTypeNorth, _wallConnectionTypeSouth, _wallConnectionTypeWest, _wallPostBit);
		} // method
	} // class

	public partial class MudBricks : Block
	{
		public override string Id => "minecraft:mud_bricks";
	} // class

	public partial class MuddyMangroveRoots : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:muddy_mangrove_roots";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class Mycelium : Block
	{
		public override string Id => "minecraft:mycelium";
	} // class

	public partial class NetherBrick : Block
	{
		public override string Id => "minecraft:nether_brick";
	} // class

	public partial class NetherBrickFence
	{
		public override string Id => "minecraft:nether_brick_fence";
	} // class

	public partial class NetherBrickStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:nether_brick_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class NetherGoldOre : Block
	{
		public override string Id => "minecraft:nether_gold_ore";
	} // class

	public partial class NetherSprouts : Block
	{
		public override string Id => "minecraft:nether_sprouts";
	} // class

	public partial class NetherWart : Block
	{
		private readonly BlockStateInt _age = new BlockStateInt() { Name = "age", Value = 0 };

		public override string Id => "minecraft:nether_wart";

		[StateRange(0, 3)]
		public int Age { get => _age.Value; set => NotifyStateUpdate(_age, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "age":
						NotifyStateUpdate(_age, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _age;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _age);
		} // method
	} // class

	public partial class NetherWartBlock : Block
	{
		public override string Id => "minecraft:nether_wart_block";
	} // class

	public partial class NetheriteBlock : Block
	{
		public override string Id => "minecraft:netherite_block";
	} // class

	public partial class Netherrack : Block
	{
		public override string Id => "minecraft:netherrack";
	} // class

	public partial class Netherreactor : Block
	{
		public override string Id => "minecraft:netherreactor";
	} // class

	public partial class NormalStoneStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:normal_stone_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class Noteblock : Block
	{
		public override string Id => "minecraft:noteblock";
	} // class

	public partial class OakFence : Block
	{
		public override string Id => "minecraft:oak_fence";
	} // class

	public partial class OakHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:oak_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class OakLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:oak_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class OakStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:oak_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class Observer : Block
	{
		private readonly BlockStateString _facingDirection = new BlockStateString() { Name = "minecraft:facing_direction", Value = "down" };
		private readonly BlockStateByte _poweredBit = new BlockStateByte() { Name = "powered_bit", Value = 0 };

		public override string Id => "minecraft:observer";

		[StateEnum("down", "east", "north", "south", "up", "west")]
		public string FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateBit]
		public bool PoweredBit { get => Convert.ToBoolean(_poweredBit.Value); set => NotifyStateUpdate(_poweredBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "powered_bit":
						NotifyStateUpdate(_poweredBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
			yield return _poweredBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection, _poweredBit);
		} // method
	} // class

	public partial class Obsidian : Block
	{
		public override string Id => "minecraft:obsidian";
	} // class

	public partial class OchreFroglight : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:ochre_froglight";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class OrangeCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:orange_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class OrangeCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:orange_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class OrangeCarpet : CarpetBase
	{
		public override string Id => "minecraft:orange_carpet";
	} // class

	public partial class OrangeConcrete : ConcreteBase
	{
		public override string Id => "minecraft:orange_concrete";
	} // class

	public partial class OrangeConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:orange_concrete_powder";
	} // class

	public partial class OrangeGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:orange_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class OrangeShulkerBox : Block
	{
		public override string Id => "minecraft:orange_shulker_box";
	} // class

	public partial class OrangeStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:orange_stained_glass";
	} // class

	public partial class OrangeStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:orange_stained_glass_pane";
	} // class

	public partial class OrangeTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:orange_terracotta";
	} // class

	public partial class OrangeWool : WoolBase
	{
		public override string Id => "minecraft:orange_wool";
	} // class

	public partial class OxidizedCopper : Block
	{
		public override string Id => "minecraft:oxidized_copper";
	} // class

	public partial class OxidizedCutCopper : Block
	{
		public override string Id => "minecraft:oxidized_cut_copper";
	} // class

	public partial class OxidizedCutCopperSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:oxidized_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class OxidizedCutCopperStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:oxidized_cut_copper_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class OxidizedDoubleCutCopperSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:oxidized_double_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class PackedIce : Block
	{
		public override string Id => "minecraft:packed_ice";
	} // class

	public partial class PackedMud : Block
	{
		public override string Id => "minecraft:packed_mud";
	} // class

	public partial class PearlescentFroglight : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:pearlescent_froglight";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class PinkCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:pink_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class PinkCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:pink_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class PinkCarpet : CarpetBase
	{
		public override string Id => "minecraft:pink_carpet";
	} // class

	public partial class PinkConcrete : ConcreteBase
	{
		public override string Id => "minecraft:pink_concrete";
	} // class

	public partial class PinkConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:pink_concrete_powder";
	} // class

	public partial class PinkGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:pink_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class PinkPetals : Block
	{
		private readonly BlockStateInt _growth = new BlockStateInt() { Name = "growth", Value = 0 };
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:pink_petals";

		[StateRange(0, 7)]
		public int Growth { get => _growth.Value; set => NotifyStateUpdate(_growth, value); }

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growth":
						NotifyStateUpdate(_growth, s.Value);
						break;
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growth;
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growth, _cardinalDirection);
		} // method
	} // class

	public partial class PinkShulkerBox : Block
	{
		public override string Id => "minecraft:pink_shulker_box";
	} // class

	public partial class PinkStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:pink_stained_glass";
	} // class

	public partial class PinkStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:pink_stained_glass_pane";
	} // class

	public partial class PinkTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:pink_terracotta";
	} // class

	public partial class PinkWool : WoolBase
	{
		public override string Id => "minecraft:pink_wool";
	} // class

	public partial class Piston : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:piston";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class PistonArmCollision : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:piston_arm_collision";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class PitcherCrop : Block
	{
		private readonly BlockStateInt _growth = new BlockStateInt() { Name = "growth", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:pitcher_crop";

		[StateRange(0, 7)]
		public int Growth { get => _growth.Value; set => NotifyStateUpdate(_growth, value); }

		[StateBit]
		public bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growth":
						NotifyStateUpdate(_growth, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growth;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growth, _upperBlockBit);
		} // method
	} // class

	public partial class PitcherPlant : Block
	{
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:pitcher_plant";

		[StateBit]
		public bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upperBlockBit);
		} // method
	} // class

	public partial class Planks : Block
	{
		private readonly BlockStateString _woodType = new BlockStateString() { Name = "wood_type", Value = "oak" };

		public override string Id => "minecraft:planks";

		[StateEnum("acacia", "birch", "dark_oak", "jungle", "oak", "spruce")]
		public string WoodType { get => _woodType.Value; set => NotifyStateUpdate(_woodType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wood_type":
						NotifyStateUpdate(_woodType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _woodType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _woodType);
		} // method
	} // class

	public partial class Podzol : Block
	{
		public override string Id => "minecraft:podzol";
	} // class

	public partial class PointedDripstone : Block
	{
		private readonly BlockStateString _dripstoneThickness = new BlockStateString() { Name = "dripstone_thickness", Value = "tip" };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:pointed_dripstone";

		[StateEnum("base", "frustum", "merge", "middle", "tip")]
		public string DripstoneThickness { get => _dripstoneThickness.Value; set => NotifyStateUpdate(_dripstoneThickness, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "dripstone_thickness":
						NotifyStateUpdate(_dripstoneThickness, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _dripstoneThickness;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _dripstoneThickness, _hanging);
		} // method
	} // class

	public partial class PolishedAndesiteStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:polished_andesite_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class PolishedBasalt : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:polished_basalt";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class PolishedBlackstone : Block
	{
		public override string Id => "minecraft:polished_blackstone";
	} // class

	public partial class PolishedBlackstoneBrickDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:polished_blackstone_brick_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class PolishedBlackstoneBrickSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:polished_blackstone_brick_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class PolishedBlackstoneBrickStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:polished_blackstone_brick_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class PolishedBlackstoneBrickWall : Block
	{
		private readonly BlockStateString _wallConnectionTypeEast = new BlockStateString() { Name = "wall_connection_type_east", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeNorth = new BlockStateString() { Name = "wall_connection_type_north", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeSouth = new BlockStateString() { Name = "wall_connection_type_south", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeWest = new BlockStateString() { Name = "wall_connection_type_west", Value = "none" };
		private readonly BlockStateByte _wallPostBit = new BlockStateByte() { Name = "wall_post_bit", Value = 0 };

		public override string Id => "minecraft:polished_blackstone_brick_wall";

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeEast { get => _wallConnectionTypeEast.Value; set => NotifyStateUpdate(_wallConnectionTypeEast, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeNorth { get => _wallConnectionTypeNorth.Value; set => NotifyStateUpdate(_wallConnectionTypeNorth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeSouth { get => _wallConnectionTypeSouth.Value; set => NotifyStateUpdate(_wallConnectionTypeSouth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeWest { get => _wallConnectionTypeWest.Value; set => NotifyStateUpdate(_wallConnectionTypeWest, value); }

		[StateBit]
		public bool WallPostBit { get => Convert.ToBoolean(_wallPostBit.Value); set => NotifyStateUpdate(_wallPostBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wall_connection_type_east":
						NotifyStateUpdate(_wallConnectionTypeEast, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_north":
						NotifyStateUpdate(_wallConnectionTypeNorth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_south":
						NotifyStateUpdate(_wallConnectionTypeSouth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_west":
						NotifyStateUpdate(_wallConnectionTypeWest, s.Value);
						break;
					case BlockStateByte s when s.Name == "wall_post_bit":
						NotifyStateUpdate(_wallPostBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _wallConnectionTypeEast;
			yield return _wallConnectionTypeNorth;
			yield return _wallConnectionTypeSouth;
			yield return _wallConnectionTypeWest;
			yield return _wallPostBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _wallConnectionTypeEast, _wallConnectionTypeNorth, _wallConnectionTypeSouth, _wallConnectionTypeWest, _wallPostBit);
		} // method
	} // class

	public partial class PolishedBlackstoneBricks : Block
	{
		public override string Id => "minecraft:polished_blackstone_bricks";
	} // class

	public partial class PolishedBlackstoneButton : Block
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:polished_blackstone_button";

		[StateBit]
		public bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class PolishedBlackstoneDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:polished_blackstone_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class PolishedBlackstonePressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:polished_blackstone_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class PolishedBlackstoneSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:polished_blackstone_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class PolishedBlackstoneStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:polished_blackstone_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class PolishedBlackstoneWall : Block
	{
		private readonly BlockStateString _wallConnectionTypeEast = new BlockStateString() { Name = "wall_connection_type_east", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeNorth = new BlockStateString() { Name = "wall_connection_type_north", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeSouth = new BlockStateString() { Name = "wall_connection_type_south", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeWest = new BlockStateString() { Name = "wall_connection_type_west", Value = "none" };
		private readonly BlockStateByte _wallPostBit = new BlockStateByte() { Name = "wall_post_bit", Value = 0 };

		public override string Id => "minecraft:polished_blackstone_wall";

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeEast { get => _wallConnectionTypeEast.Value; set => NotifyStateUpdate(_wallConnectionTypeEast, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeNorth { get => _wallConnectionTypeNorth.Value; set => NotifyStateUpdate(_wallConnectionTypeNorth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeSouth { get => _wallConnectionTypeSouth.Value; set => NotifyStateUpdate(_wallConnectionTypeSouth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeWest { get => _wallConnectionTypeWest.Value; set => NotifyStateUpdate(_wallConnectionTypeWest, value); }

		[StateBit]
		public bool WallPostBit { get => Convert.ToBoolean(_wallPostBit.Value); set => NotifyStateUpdate(_wallPostBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wall_connection_type_east":
						NotifyStateUpdate(_wallConnectionTypeEast, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_north":
						NotifyStateUpdate(_wallConnectionTypeNorth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_south":
						NotifyStateUpdate(_wallConnectionTypeSouth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_west":
						NotifyStateUpdate(_wallConnectionTypeWest, s.Value);
						break;
					case BlockStateByte s when s.Name == "wall_post_bit":
						NotifyStateUpdate(_wallPostBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _wallConnectionTypeEast;
			yield return _wallConnectionTypeNorth;
			yield return _wallConnectionTypeSouth;
			yield return _wallConnectionTypeWest;
			yield return _wallPostBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _wallConnectionTypeEast, _wallConnectionTypeNorth, _wallConnectionTypeSouth, _wallConnectionTypeWest, _wallPostBit);
		} // method
	} // class

	public partial class PolishedDeepslate : Block
	{
		public override string Id => "minecraft:polished_deepslate";
	} // class

	public partial class PolishedDeepslateDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:polished_deepslate_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class PolishedDeepslateSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:polished_deepslate_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class PolishedDeepslateStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:polished_deepslate_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class PolishedDeepslateWall : Block
	{
		private readonly BlockStateString _wallConnectionTypeEast = new BlockStateString() { Name = "wall_connection_type_east", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeNorth = new BlockStateString() { Name = "wall_connection_type_north", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeSouth = new BlockStateString() { Name = "wall_connection_type_south", Value = "none" };
		private readonly BlockStateString _wallConnectionTypeWest = new BlockStateString() { Name = "wall_connection_type_west", Value = "none" };
		private readonly BlockStateByte _wallPostBit = new BlockStateByte() { Name = "wall_post_bit", Value = 0 };

		public override string Id => "minecraft:polished_deepslate_wall";

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeEast { get => _wallConnectionTypeEast.Value; set => NotifyStateUpdate(_wallConnectionTypeEast, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeNorth { get => _wallConnectionTypeNorth.Value; set => NotifyStateUpdate(_wallConnectionTypeNorth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeSouth { get => _wallConnectionTypeSouth.Value; set => NotifyStateUpdate(_wallConnectionTypeSouth, value); }

		[StateEnum("none", "short", "tall")]
		public string WallConnectionTypeWest { get => _wallConnectionTypeWest.Value; set => NotifyStateUpdate(_wallConnectionTypeWest, value); }

		[StateBit]
		public bool WallPostBit { get => Convert.ToBoolean(_wallPostBit.Value); set => NotifyStateUpdate(_wallPostBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "wall_connection_type_east":
						NotifyStateUpdate(_wallConnectionTypeEast, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_north":
						NotifyStateUpdate(_wallConnectionTypeNorth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_south":
						NotifyStateUpdate(_wallConnectionTypeSouth, s.Value);
						break;
					case BlockStateString s when s.Name == "wall_connection_type_west":
						NotifyStateUpdate(_wallConnectionTypeWest, s.Value);
						break;
					case BlockStateByte s when s.Name == "wall_post_bit":
						NotifyStateUpdate(_wallPostBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _wallConnectionTypeEast;
			yield return _wallConnectionTypeNorth;
			yield return _wallConnectionTypeSouth;
			yield return _wallConnectionTypeWest;
			yield return _wallPostBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _wallConnectionTypeEast, _wallConnectionTypeNorth, _wallConnectionTypeSouth, _wallConnectionTypeWest, _wallPostBit);
		} // method
	} // class

	public partial class PolishedDioriteStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:polished_diorite_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class PolishedGraniteStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:polished_granite_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class Portal : Block
	{
		private readonly BlockStateString _portalAxis = new BlockStateString() { Name = "portal_axis", Value = "unknown" };

		public override string Id => "minecraft:portal";

		[StateEnum("unknown", "x", "z")]
		public string PortalAxis { get => _portalAxis.Value; set => NotifyStateUpdate(_portalAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "portal_axis":
						NotifyStateUpdate(_portalAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _portalAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _portalAxis);
		} // method
	} // class

	public partial class Potatoes
	{
		private readonly BlockStateInt _growth = new BlockStateInt() { Name = "growth", Value = 0 };

		public override string Id => "minecraft:potatoes";

		[StateRange(0, 7)]
		public override int Growth { get => _growth.Value; set => NotifyStateUpdate(_growth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growth":
						NotifyStateUpdate(_growth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growth);
		} // method
	} // class

	public partial class PowderSnow : Block
	{
		public override string Id => "minecraft:powder_snow";
	} // class

	public partial class PoweredComparator
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };
		private readonly BlockStateByte _outputLitBit = new BlockStateByte() { Name = "output_lit_bit", Value = 0 };
		private readonly BlockStateByte _outputSubtractBit = new BlockStateByte() { Name = "output_subtract_bit", Value = 0 };

		public override string Id => "minecraft:powered_comparator";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		[StateBit]
		public bool OutputLitBit { get => Convert.ToBoolean(_outputLitBit.Value); set => NotifyStateUpdate(_outputLitBit, value); }

		[StateBit]
		public bool OutputSubtractBit { get => Convert.ToBoolean(_outputSubtractBit.Value); set => NotifyStateUpdate(_outputSubtractBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "output_lit_bit":
						NotifyStateUpdate(_outputLitBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "output_subtract_bit":
						NotifyStateUpdate(_outputSubtractBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
			yield return _outputLitBit;
			yield return _outputSubtractBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection, _outputLitBit, _outputSubtractBit);
		} // method
	} // class

	public partial class PoweredRepeater
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };
		private readonly BlockStateInt _repeaterDelay = new BlockStateInt() { Name = "repeater_delay", Value = 0 };

		public override string Id => "minecraft:powered_repeater";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		[StateRange(0, 3)]
		public int RepeaterDelay { get => _repeaterDelay.Value; set => NotifyStateUpdate(_repeaterDelay, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "repeater_delay":
						NotifyStateUpdate(_repeaterDelay, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
			yield return _repeaterDelay;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection, _repeaterDelay);
		} // method
	} // class

	public partial class Prismarine : Block
	{
		private readonly BlockStateString _prismarineBlockType = new BlockStateString() { Name = "prismarine_block_type", Value = "default" };

		public override string Id => "minecraft:prismarine";

		[StateEnum("bricks", "dark", "default")]
		public string PrismarineBlockType { get => _prismarineBlockType.Value; set => NotifyStateUpdate(_prismarineBlockType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "prismarine_block_type":
						NotifyStateUpdate(_prismarineBlockType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _prismarineBlockType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _prismarineBlockType);
		} // method
	} // class

	public partial class PrismarineBricksStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:prismarine_bricks_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class PrismarineStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:prismarine_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class Pumpkin : Block
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:pumpkin";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class PumpkinStem : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _growth = new BlockStateInt() { Name = "growth", Value = 0 };

		public override string Id => "minecraft:pumpkin_stem";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 7)]
		public int Growth { get => _growth.Value; set => NotifyStateUpdate(_growth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "growth":
						NotifyStateUpdate(_growth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
			yield return _growth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection, _growth);
		} // method
	} // class

	public partial class PurpleCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:purple_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class PurpleCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:purple_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class PurpleCarpet : CarpetBase
	{
		public override string Id => "minecraft:purple_carpet";
	} // class

	public partial class PurpleConcrete : ConcreteBase
	{
		public override string Id => "minecraft:purple_concrete";
	} // class

	public partial class PurpleConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:purple_concrete_powder";
	} // class

	public partial class PurpleGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:purple_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class PurpleShulkerBox : Block
	{
		public override string Id => "minecraft:purple_shulker_box";
	} // class

	public partial class PurpleStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:purple_stained_glass";
	} // class

	public partial class PurpleStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:purple_stained_glass_pane";
	} // class

	public partial class PurpleTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:purple_terracotta";
	} // class

	public partial class PurpleWool : WoolBase
	{
		public override string Id => "minecraft:purple_wool";
	} // class

	public partial class PurpurBlock : Block
	{
		private readonly BlockStateString _chiselType = new BlockStateString() { Name = "chisel_type", Value = "default" };
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:purpur_block";

		[StateEnum("chiseled", "default", "lines", "smooth")]
		public string ChiselType { get => _chiselType.Value; set => NotifyStateUpdate(_chiselType, value); }

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "chisel_type":
						NotifyStateUpdate(_chiselType, s.Value);
						break;
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _chiselType;
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _chiselType, _pillarAxis);
		} // method
	} // class

	public partial class PurpurStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:purpur_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class QuartzBlock : Block
	{
		private readonly BlockStateString _chiselType = new BlockStateString() { Name = "chisel_type", Value = "default" };
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:quartz_block";

		[StateEnum("chiseled", "default", "lines", "smooth")]
		public string ChiselType { get => _chiselType.Value; set => NotifyStateUpdate(_chiselType, value); }

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "chisel_type":
						NotifyStateUpdate(_chiselType, s.Value);
						break;
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _chiselType;
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _chiselType, _pillarAxis);
		} // method
	} // class

	public partial class QuartzBricks : Block
	{
		public override string Id => "minecraft:quartz_bricks";
	} // class

	public partial class QuartzOre : Block
	{
		public override string Id => "minecraft:quartz_ore";
	} // class

	public partial class QuartzStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:quartz_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class Rail : Block
	{
		private readonly BlockStateInt _railDirection = new BlockStateInt() { Name = "rail_direction", Value = 0 };

		public override string Id => "minecraft:rail";

		[StateRange(0, 9)]
		public int RailDirection { get => _railDirection.Value; set => NotifyStateUpdate(_railDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "rail_direction":
						NotifyStateUpdate(_railDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _railDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _railDirection);
		} // method
	} // class

	public partial class RawCopperBlock : Block
	{
		public override string Id => "minecraft:raw_copper_block";
	} // class

	public partial class RawGoldBlock : Block
	{
		public override string Id => "minecraft:raw_gold_block";
	} // class

	public partial class RawIronBlock : Block
	{
		public override string Id => "minecraft:raw_iron_block";
	} // class

	public partial class RedCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:red_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class RedCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:red_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class RedCarpet : CarpetBase
	{
		public override string Id => "minecraft:red_carpet";
	} // class

	public partial class RedConcrete : ConcreteBase
	{
		public override string Id => "minecraft:red_concrete";
	} // class

	public partial class RedConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:red_concrete_powder";
	} // class

	public partial class RedFlower : Block
	{
		private readonly BlockStateString _flowerType = new BlockStateString() { Name = "flower_type", Value = "poppy" };

		public override string Id => "minecraft:red_flower";

		[StateEnum("allium", "cornflower", "houstonia", "lily_of_the_valley", "orchid", "oxeye", "poppy", "tulip_orange", "tulip_pink", "tulip_red", "tulip_white")]
		public string FlowerType { get => _flowerType.Value; set => NotifyStateUpdate(_flowerType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "flower_type":
						NotifyStateUpdate(_flowerType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _flowerType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _flowerType);
		} // method
	} // class

	public partial class RedGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:red_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class RedMushroom : Block
	{
		public override string Id => "minecraft:red_mushroom";
	} // class

	public partial class RedMushroomBlock : Block
	{
		private readonly BlockStateInt _hugeMushroomBits = new BlockStateInt() { Name = "huge_mushroom_bits", Value = 0 };

		public override string Id => "minecraft:red_mushroom_block";

		[StateRange(0, 15)]
		public int HugeMushroomBits { get => _hugeMushroomBits.Value; set => NotifyStateUpdate(_hugeMushroomBits, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "huge_mushroom_bits":
						NotifyStateUpdate(_hugeMushroomBits, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _hugeMushroomBits;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _hugeMushroomBits);
		} // method
	} // class

	public partial class RedNetherBrick : Block
	{
		public override string Id => "minecraft:red_nether_brick";
	} // class

	public partial class RedNetherBrickStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:red_nether_brick_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class RedSandstone : Block
	{
		private readonly BlockStateString _sandStoneType = new BlockStateString() { Name = "sand_stone_type", Value = "default" };

		public override string Id => "minecraft:red_sandstone";

		[StateEnum("cut", "default", "heiroglyphs", "smooth")]
		public string SandStoneType { get => _sandStoneType.Value; set => NotifyStateUpdate(_sandStoneType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "sand_stone_type":
						NotifyStateUpdate(_sandStoneType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _sandStoneType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _sandStoneType);
		} // method
	} // class

	public partial class RedSandstoneStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:red_sandstone_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class RedShulkerBox : Block
	{
		public override string Id => "minecraft:red_shulker_box";
	} // class

	public partial class RedStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:red_stained_glass";
	} // class

	public partial class RedStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:red_stained_glass_pane";
	} // class

	public partial class RedTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:red_terracotta";
	} // class

	public partial class RedWool : WoolBase
	{
		public override string Id => "minecraft:red_wool";
	} // class

	public partial class RedstoneBlock : Block
	{
		public override string Id => "minecraft:redstone_block";
	} // class

	public partial class RedstoneLamp : Block
	{
		public override string Id => "minecraft:redstone_lamp";
	} // class

	public partial class RedstoneOre : Block
	{
		public override string Id => "minecraft:redstone_ore";
	} // class

	public partial class RedstoneTorch
	{
		private readonly BlockStateString _torchFacingDirection = new BlockStateString() { Name = "torch_facing_direction", Value = "unknown" };

		public override string Id => "minecraft:redstone_torch";

		[StateEnum("east", "north", "south", "top", "unknown", "west")]
		public override string TorchFacingDirection { get => _torchFacingDirection.Value; set => NotifyStateUpdate(_torchFacingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "torch_facing_direction":
						NotifyStateUpdate(_torchFacingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _torchFacingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _torchFacingDirection);
		} // method
	} // class

	public partial class RedstoneWire : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:redstone_wire";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class Reeds : Block
	{
		private readonly BlockStateInt _age = new BlockStateInt() { Name = "age", Value = 0 };

		public override string Id => "minecraft:reeds";

		[StateRange(0, 15)]
		public int Age { get => _age.Value; set => NotifyStateUpdate(_age, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "age":
						NotifyStateUpdate(_age, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _age;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _age);
		} // method
	} // class

	public partial class ReinforcedDeepslate : Block
	{
		public override string Id => "minecraft:reinforced_deepslate";
	} // class

	public partial class RepeatingCommandBlock : Block
	{
		private readonly BlockStateByte _conditionalBit = new BlockStateByte() { Name = "conditional_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:repeating_command_block";

		[StateBit]
		public bool ConditionalBit { get => Convert.ToBoolean(_conditionalBit.Value); set => NotifyStateUpdate(_conditionalBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "conditional_bit":
						NotifyStateUpdate(_conditionalBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _conditionalBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _conditionalBit, _facingDirection);
		} // method
	} // class

	public partial class Reserved6 : Block
	{
		public override string Id => "minecraft:reserved6";
	} // class

	public partial class RespawnAnchor : Block
	{
		private readonly BlockStateInt _respawnAnchorCharge = new BlockStateInt() { Name = "respawn_anchor_charge", Value = 0 };

		public override string Id => "minecraft:respawn_anchor";

		[StateRange(0, 4)]
		public int RespawnAnchorCharge { get => _respawnAnchorCharge.Value; set => NotifyStateUpdate(_respawnAnchorCharge, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "respawn_anchor_charge":
						NotifyStateUpdate(_respawnAnchorCharge, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _respawnAnchorCharge;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _respawnAnchorCharge);
		} // method
	} // class

	public partial class Sand : Block
	{
		private readonly BlockStateString _sandType = new BlockStateString() { Name = "sand_type", Value = "normal" };

		public override string Id => "minecraft:sand";

		[StateEnum("normal", "red")]
		public string SandType { get => _sandType.Value; set => NotifyStateUpdate(_sandType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "sand_type":
						NotifyStateUpdate(_sandType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _sandType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _sandType);
		} // method
	} // class

	public partial class Sandstone : Block
	{
		private readonly BlockStateString _sandStoneType = new BlockStateString() { Name = "sand_stone_type", Value = "default" };

		public override string Id => "minecraft:sandstone";

		[StateEnum("cut", "default", "heiroglyphs", "smooth")]
		public string SandStoneType { get => _sandStoneType.Value; set => NotifyStateUpdate(_sandStoneType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "sand_stone_type":
						NotifyStateUpdate(_sandStoneType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _sandStoneType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _sandStoneType);
		} // method
	} // class

	public partial class SandstoneStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:sandstone_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class Sapling : Block
	{
		private readonly BlockStateByte _ageBit = new BlockStateByte() { Name = "age_bit", Value = 0 };
		private readonly BlockStateString _saplingType = new BlockStateString() { Name = "sapling_type", Value = "oak" };

		public override string Id => "minecraft:sapling";

		[StateBit]
		public bool AgeBit { get => Convert.ToBoolean(_ageBit.Value); set => NotifyStateUpdate(_ageBit, value); }

		[StateEnum("acacia", "birch", "dark_oak", "jungle", "oak", "spruce")]
		public string SaplingType { get => _saplingType.Value; set => NotifyStateUpdate(_saplingType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "age_bit":
						NotifyStateUpdate(_ageBit, s.Value);
						break;
					case BlockStateString s when s.Name == "sapling_type":
						NotifyStateUpdate(_saplingType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _ageBit;
			yield return _saplingType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _ageBit, _saplingType);
		} // method
	} // class

	public partial class Scaffolding : Block
	{
		private readonly BlockStateInt _stability = new BlockStateInt() { Name = "stability", Value = 0 };
		private readonly BlockStateByte _stabilityCheck = new BlockStateByte() { Name = "stability_check", Value = 0 };

		public override string Id => "minecraft:scaffolding";

		[StateRange(0, 7)]
		public int Stability { get => _stability.Value; set => NotifyStateUpdate(_stability, value); }

		[StateBit]
		public bool StabilityCheck { get => Convert.ToBoolean(_stabilityCheck.Value); set => NotifyStateUpdate(_stabilityCheck, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "stability":
						NotifyStateUpdate(_stability, s.Value);
						break;
					case BlockStateByte s when s.Name == "stability_check":
						NotifyStateUpdate(_stabilityCheck, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _stability;
			yield return _stabilityCheck;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _stability, _stabilityCheck);
		} // method
	} // class

	public partial class Sculk : Block
	{
		public override string Id => "minecraft:sculk";
	} // class

	public partial class SculkCatalyst : Block
	{
		private readonly BlockStateByte _bloom = new BlockStateByte() { Name = "bloom", Value = 0 };

		public override string Id => "minecraft:sculk_catalyst";

		[StateBit]
		public bool Bloom { get => Convert.ToBoolean(_bloom.Value); set => NotifyStateUpdate(_bloom, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "bloom":
						NotifyStateUpdate(_bloom, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _bloom;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _bloom);
		} // method
	} // class

	public partial class SculkSensor : Block
	{
		private readonly BlockStateInt _sculkSensorPhase = new BlockStateInt() { Name = "sculk_sensor_phase", Value = 0 };

		public override string Id => "minecraft:sculk_sensor";

		[StateRange(0, 2)]
		public int SculkSensorPhase { get => _sculkSensorPhase.Value; set => NotifyStateUpdate(_sculkSensorPhase, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "sculk_sensor_phase":
						NotifyStateUpdate(_sculkSensorPhase, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _sculkSensorPhase;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _sculkSensorPhase);
		} // method
	} // class

	public partial class SculkShrieker : Block
	{
		private readonly BlockStateByte _active = new BlockStateByte() { Name = "active", Value = 0 };
		private readonly BlockStateByte _canSummon = new BlockStateByte() { Name = "can_summon", Value = 0 };

		public override string Id => "minecraft:sculk_shrieker";

		[StateBit]
		public bool Active { get => Convert.ToBoolean(_active.Value); set => NotifyStateUpdate(_active, value); }

		[StateBit]
		public bool CanSummon { get => Convert.ToBoolean(_canSummon.Value); set => NotifyStateUpdate(_canSummon, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "active":
						NotifyStateUpdate(_active, s.Value);
						break;
					case BlockStateByte s when s.Name == "can_summon":
						NotifyStateUpdate(_canSummon, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _active;
			yield return _canSummon;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _active, _canSummon);
		} // method
	} // class

	public partial class SculkVein : Block
	{
		private readonly BlockStateInt _multiFaceDirectionBits = new BlockStateInt() { Name = "multi_face_direction_bits", Value = 0 };

		public override string Id => "minecraft:sculk_vein";

		[StateRange(0, 63)]
		public int MultiFaceDirectionBits { get => _multiFaceDirectionBits.Value; set => NotifyStateUpdate(_multiFaceDirectionBits, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "multi_face_direction_bits":
						NotifyStateUpdate(_multiFaceDirectionBits, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _multiFaceDirectionBits;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _multiFaceDirectionBits);
		} // method
	} // class

	public partial class SeaLantern : Block
	{
		public override string Id => "minecraft:sea_lantern";
	} // class

	public partial class SeaPickle : Block
	{
		private readonly BlockStateInt _clusterCount = new BlockStateInt() { Name = "cluster_count", Value = 0 };
		private readonly BlockStateByte _deadBit = new BlockStateByte() { Name = "dead_bit", Value = 0 };

		public override string Id => "minecraft:sea_pickle";

		[StateRange(0, 3)]
		public int ClusterCount { get => _clusterCount.Value; set => NotifyStateUpdate(_clusterCount, value); }

		[StateBit]
		public bool DeadBit { get => Convert.ToBoolean(_deadBit.Value); set => NotifyStateUpdate(_deadBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "cluster_count":
						NotifyStateUpdate(_clusterCount, s.Value);
						break;
					case BlockStateByte s when s.Name == "dead_bit":
						NotifyStateUpdate(_deadBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _clusterCount;
			yield return _deadBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _clusterCount, _deadBit);
		} // method
	} // class

	public partial class Seagrass : Block
	{
		private readonly BlockStateString _seaGrassType = new BlockStateString() { Name = "sea_grass_type", Value = "default" };

		public override string Id => "minecraft:seagrass";

		[StateEnum("default", "double_bot", "double_top")]
		public string SeaGrassType { get => _seaGrassType.Value; set => NotifyStateUpdate(_seaGrassType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "sea_grass_type":
						NotifyStateUpdate(_seaGrassType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _seaGrassType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _seaGrassType);
		} // method
	} // class

	public partial class Shroomlight : Block
	{
		public override string Id => "minecraft:shroomlight";
	} // class

	public partial class SilverGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:silver_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class Skull : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:skull";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class Slime : Block
	{
		public override string Id => "minecraft:slime";
	} // class

	public partial class SmallAmethystBud : Block
	{
		private readonly BlockStateString _blockFace = new BlockStateString() { Name = "minecraft:block_face", Value = "down" };

		public override string Id => "minecraft:small_amethyst_bud";

		[StateEnum("down", "east", "north", "south", "up", "west")]
		public string BlockFace { get => _blockFace.Value; set => NotifyStateUpdate(_blockFace, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:block_face":
						NotifyStateUpdate(_blockFace, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _blockFace;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _blockFace);
		} // method
	} // class

	public partial class SmallDripleafBlock : Block
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:small_dripleaf_block";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		[StateBit]
		public bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection, _upperBlockBit);
		} // method
	} // class

	public partial class SmithingTable : Block
	{
		public override string Id => "minecraft:smithing_table";
	} // class

	public partial class Smoker : Block
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:smoker";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class SmoothBasalt : Block
	{
		public override string Id => "minecraft:smooth_basalt";
	} // class

	public partial class SmoothQuartzStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:smooth_quartz_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class SmoothRedSandstoneStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:smooth_red_sandstone_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class SmoothSandstoneStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:smooth_sandstone_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class SmoothStone : Block
	{
		public override string Id => "minecraft:smooth_stone";
	} // class

	public partial class SnifferEgg : Block
	{
		private readonly BlockStateString _crackedState = new BlockStateString() { Name = "cracked_state", Value = "no_cracks" };

		public override string Id => "minecraft:sniffer_egg";

		[StateEnum("cracked", "max_cracked", "no_cracks")]
		public string CrackedState { get => _crackedState.Value; set => NotifyStateUpdate(_crackedState, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "cracked_state":
						NotifyStateUpdate(_crackedState, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _crackedState;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _crackedState);
		} // method
	} // class

	public partial class Snow : Block
	{
		public override string Id => "minecraft:snow";
	} // class

	public partial class SnowLayer : Block
	{
		private readonly BlockStateByte _coveredBit = new BlockStateByte() { Name = "covered_bit", Value = 0 };
		private readonly BlockStateInt _height = new BlockStateInt() { Name = "height", Value = 0 };

		public override string Id => "minecraft:snow_layer";

		[StateBit]
		public bool CoveredBit { get => Convert.ToBoolean(_coveredBit.Value); set => NotifyStateUpdate(_coveredBit, value); }

		[StateRange(0, 7)]
		public int Height { get => _height.Value; set => NotifyStateUpdate(_height, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "covered_bit":
						NotifyStateUpdate(_coveredBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "height":
						NotifyStateUpdate(_height, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _coveredBit;
			yield return _height;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _coveredBit, _height);
		} // method
	} // class

	public partial class SoulCampfire : Block
	{
		private readonly BlockStateByte _extinguished = new BlockStateByte() { Name = "extinguished", Value = 0 };
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };

		public override string Id => "minecraft:soul_campfire";

		[StateBit]
		public bool Extinguished { get => Convert.ToBoolean(_extinguished.Value); set => NotifyStateUpdate(_extinguished, value); }

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "extinguished":
						NotifyStateUpdate(_extinguished, s.Value);
						break;
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _extinguished;
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _extinguished, _cardinalDirection);
		} // method
	} // class

	public partial class SoulFire : Block
	{
		private readonly BlockStateInt _age = new BlockStateInt() { Name = "age", Value = 0 };

		public override string Id => "minecraft:soul_fire";

		[StateRange(0, 15)]
		public int Age { get => _age.Value; set => NotifyStateUpdate(_age, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "age":
						NotifyStateUpdate(_age, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _age;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _age);
		} // method
	} // class

	public partial class SoulLantern : Block
	{
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:soul_lantern";

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _hanging);
		} // method
	} // class

	public partial class SoulSand : Block
	{
		public override string Id => "minecraft:soul_sand";
	} // class

	public partial class SoulSoil : Block
	{
		public override string Id => "minecraft:soul_soil";
	} // class

	public partial class SoulTorch : Block
	{
		private readonly BlockStateString _torchFacingDirection = new BlockStateString() { Name = "torch_facing_direction", Value = "unknown" };

		public override string Id => "minecraft:soul_torch";

		[StateEnum("east", "north", "south", "top", "unknown", "west")]
		public string TorchFacingDirection { get => _torchFacingDirection.Value; set => NotifyStateUpdate(_torchFacingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "torch_facing_direction":
						NotifyStateUpdate(_torchFacingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _torchFacingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _torchFacingDirection);
		} // method
	} // class

	public partial class Sponge : Block
	{
		private readonly BlockStateString _spongeType = new BlockStateString() { Name = "sponge_type", Value = "dry" };

		public override string Id => "minecraft:sponge";

		[StateEnum("dry", "wet")]
		public string SpongeType { get => _spongeType.Value; set => NotifyStateUpdate(_spongeType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "sponge_type":
						NotifyStateUpdate(_spongeType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _spongeType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _spongeType);
		} // method
	} // class

	public partial class SporeBlossom : Block
	{
		public override string Id => "minecraft:spore_blossom";
	} // class

	public partial class SpruceButton
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:spruce_button";

		[StateBit]
		public override bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class SpruceDoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:spruce_door";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class SpruceFence : Block
	{
		public override string Id => "minecraft:spruce_fence";
	} // class

	public partial class SpruceFenceGate
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:spruce_fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class SpruceHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:spruce_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class SpruceLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:spruce_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class SprucePressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:spruce_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class SpruceStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:spruce_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class SpruceStandingSign
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:spruce_standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class SpruceTrapdoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:spruce_trapdoor";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class SpruceWallSign
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:spruce_wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class StandingBanner : Block
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:standing_banner";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class StandingSign
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class StickyPiston : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:sticky_piston";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class StickyPistonArmCollision : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:sticky_piston_arm_collision";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class Stone : Block
	{
		private readonly BlockStateString _stoneType = new BlockStateString() { Name = "stone_type", Value = "stone" };

		public override string Id => "minecraft:stone";

		[StateEnum("andesite", "andesite_smooth", "diorite", "diorite_smooth", "granite", "granite_smooth", "stone")]
		public string StoneType { get => _stoneType.Value; set => NotifyStateUpdate(_stoneType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "stone_type":
						NotifyStateUpdate(_stoneType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _stoneType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _stoneType);
		} // method
	} // class

	public partial class StoneBlockSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };
		private readonly BlockStateString _stoneSlabType = new BlockStateString() { Name = "stone_slab_type", Value = "smooth_stone" };

		public override string Id => "minecraft:stone_block_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		[StateEnum("brick", "cobblestone", "nether_brick", "quartz", "sandstone", "smooth_stone", "stone_brick", "wood")]
		public string StoneSlabType { get => _stoneSlabType.Value; set => NotifyStateUpdate(_stoneSlabType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
					case BlockStateString s when s.Name == "stone_slab_type":
						NotifyStateUpdate(_stoneSlabType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
			yield return _stoneSlabType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf, _stoneSlabType);
		} // method
	} // class

	public partial class StoneBlockSlab2 : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };
		private readonly BlockStateString _stoneSlabType2 = new BlockStateString() { Name = "stone_slab_type_2", Value = "red_sandstone" };

		public override string Id => "minecraft:stone_block_slab2";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		[StateEnum("mossy_cobblestone", "prismarine_brick", "prismarine_dark", "prismarine_rough", "purpur", "red_nether_brick", "red_sandstone", "smooth_sandstone")]
		public string StoneSlabType2 { get => _stoneSlabType2.Value; set => NotifyStateUpdate(_stoneSlabType2, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
					case BlockStateString s when s.Name == "stone_slab_type_2":
						NotifyStateUpdate(_stoneSlabType2, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
			yield return _stoneSlabType2;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf, _stoneSlabType2);
		} // method
	} // class

	public partial class StoneBlockSlab3 : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };
		private readonly BlockStateString _stoneSlabType3 = new BlockStateString() { Name = "stone_slab_type_3", Value = "end_stone_brick" };

		public override string Id => "minecraft:stone_block_slab3";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		[StateEnum("andesite", "diorite", "end_stone_brick", "granite", "polished_andesite", "polished_diorite", "polished_granite", "smooth_red_sandstone")]
		public string StoneSlabType3 { get => _stoneSlabType3.Value; set => NotifyStateUpdate(_stoneSlabType3, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
					case BlockStateString s when s.Name == "stone_slab_type_3":
						NotifyStateUpdate(_stoneSlabType3, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
			yield return _stoneSlabType3;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf, _stoneSlabType3);
		} // method
	} // class

	public partial class StoneBlockSlab4 : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };
		private readonly BlockStateString _stoneSlabType4 = new BlockStateString() { Name = "stone_slab_type_4", Value = "mossy_stone_brick" };

		public override string Id => "minecraft:stone_block_slab4";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		[StateEnum("cut_red_sandstone", "cut_sandstone", "mossy_stone_brick", "smooth_quartz", "stone")]
		public string StoneSlabType4 { get => _stoneSlabType4.Value; set => NotifyStateUpdate(_stoneSlabType4, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
					case BlockStateString s when s.Name == "stone_slab_type_4":
						NotifyStateUpdate(_stoneSlabType4, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
			yield return _stoneSlabType4;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf, _stoneSlabType4);
		} // method
	} // class

	public partial class StoneBrickStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:stone_brick_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class StoneButton
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:stone_button";

		[StateBit]
		public override bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class StonePressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:stone_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class StoneStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:stone_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class Stonebrick : Block
	{
		private readonly BlockStateString _stoneBrickType = new BlockStateString() { Name = "stone_brick_type", Value = "default" };

		public override string Id => "minecraft:stonebrick";

		[StateEnum("chiseled", "cracked", "default", "mossy", "smooth")]
		public string StoneBrickType { get => _stoneBrickType.Value; set => NotifyStateUpdate(_stoneBrickType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "stone_brick_type":
						NotifyStateUpdate(_stoneBrickType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _stoneBrickType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _stoneBrickType);
		} // method
	} // class

	public partial class Stonecutter : Block
	{
		public override string Id => "minecraft:stonecutter";
	} // class

	public partial class StonecutterBlock : Block
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "north" };

		public override string Id => "minecraft:stonecutter_block";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class StrippedAcaciaLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_acacia_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedBambooBlock : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_bamboo_block";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedBirchLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_birch_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedCherryLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_cherry_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedCherryWood : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_cherry_wood";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedCrimsonHyphae : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_crimson_hyphae";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedCrimsonStem : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_crimson_stem";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedDarkOakLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_dark_oak_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedJungleLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_jungle_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedMangroveLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_mangrove_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedMangroveWood : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_mangrove_wood";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedOakLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_oak_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedSpruceLog : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_spruce_log";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedWarpedHyphae : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_warped_hyphae";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StrippedWarpedStem : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:stripped_warped_stem";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class StructureBlock : Block
	{
		private readonly BlockStateString _structureBlockType = new BlockStateString() { Name = "structure_block_type", Value = "data" };

		public override string Id => "minecraft:structure_block";

		[StateEnum("corner", "data", "export", "invalid", "load", "save")]
		public string StructureBlockType { get => _structureBlockType.Value; set => NotifyStateUpdate(_structureBlockType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "structure_block_type":
						NotifyStateUpdate(_structureBlockType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _structureBlockType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _structureBlockType);
		} // method
	} // class

	public partial class StructureVoid : Block
	{
		private readonly BlockStateString _structureVoidType = new BlockStateString() { Name = "structure_void_type", Value = "void" };

		public override string Id => "minecraft:structure_void";

		[StateEnum("air", "void")]
		public string StructureVoidType { get => _structureVoidType.Value; set => NotifyStateUpdate(_structureVoidType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "structure_void_type":
						NotifyStateUpdate(_structureVoidType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _structureVoidType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _structureVoidType);
		} // method
	} // class

	public partial class SuspiciousGravel : Block
	{
		private readonly BlockStateInt _brushedProgress = new BlockStateInt() { Name = "brushed_progress", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:suspicious_gravel";

		[StateRange(0, 3)]
		public int BrushedProgress { get => _brushedProgress.Value; set => NotifyStateUpdate(_brushedProgress, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "brushed_progress":
						NotifyStateUpdate(_brushedProgress, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _brushedProgress;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _brushedProgress, _hanging);
		} // method
	} // class

	public partial class SuspiciousSand : Block
	{
		private readonly BlockStateInt _brushedProgress = new BlockStateInt() { Name = "brushed_progress", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:suspicious_sand";

		[StateRange(0, 3)]
		public int BrushedProgress { get => _brushedProgress.Value; set => NotifyStateUpdate(_brushedProgress, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "brushed_progress":
						NotifyStateUpdate(_brushedProgress, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _brushedProgress;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _brushedProgress, _hanging);
		} // method
	} // class

	public partial class SweetBerryBush : Block
	{
		private readonly BlockStateInt _growth = new BlockStateInt() { Name = "growth", Value = 0 };

		public override string Id => "minecraft:sweet_berry_bush";

		[StateRange(0, 7)]
		public int Growth { get => _growth.Value; set => NotifyStateUpdate(_growth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growth":
						NotifyStateUpdate(_growth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growth);
		} // method
	} // class

	public partial class Tallgrass : Block
	{
		private readonly BlockStateString _tallGrassType = new BlockStateString() { Name = "tall_grass_type", Value = "default" };

		public override string Id => "minecraft:tallgrass";

		[StateEnum("default", "fern", "snow", "tall")]
		public string TallGrassType { get => _tallGrassType.Value; set => NotifyStateUpdate(_tallGrassType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "tall_grass_type":
						NotifyStateUpdate(_tallGrassType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _tallGrassType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _tallGrassType);
		} // method
	} // class

	public partial class Target : Block
	{
		public override string Id => "minecraft:target";
	} // class

	public partial class TintedGlass : Block
	{
		public override string Id => "minecraft:tinted_glass";
	} // class

	public partial class Tnt : Block
	{
		private readonly BlockStateByte _allowUnderwaterBit = new BlockStateByte() { Name = "allow_underwater_bit", Value = 0 };
		private readonly BlockStateByte _explodeBit = new BlockStateByte() { Name = "explode_bit", Value = 0 };

		public override string Id => "minecraft:tnt";

		[StateBit]
		public bool AllowUnderwaterBit { get => Convert.ToBoolean(_allowUnderwaterBit.Value); set => NotifyStateUpdate(_allowUnderwaterBit, value); }

		[StateBit]
		public bool ExplodeBit { get => Convert.ToBoolean(_explodeBit.Value); set => NotifyStateUpdate(_explodeBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "allow_underwater_bit":
						NotifyStateUpdate(_allowUnderwaterBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "explode_bit":
						NotifyStateUpdate(_explodeBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _allowUnderwaterBit;
			yield return _explodeBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _allowUnderwaterBit, _explodeBit);
		} // method
	} // class

	public partial class Torch : Block
	{
		private readonly BlockStateString _torchFacingDirection = new BlockStateString() { Name = "torch_facing_direction", Value = "unknown" };

		public override string Id => "minecraft:torch";

		[StateEnum("east", "north", "south", "top", "unknown", "west")]
		public string TorchFacingDirection { get => _torchFacingDirection.Value; set => NotifyStateUpdate(_torchFacingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "torch_facing_direction":
						NotifyStateUpdate(_torchFacingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _torchFacingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _torchFacingDirection);
		} // method
	} // class

	public partial class Torchflower : Block
	{
		public override string Id => "minecraft:torchflower";
	} // class

	public partial class TorchflowerCrop : Block
	{
		private readonly BlockStateInt _growth = new BlockStateInt() { Name = "growth", Value = 0 };

		public override string Id => "minecraft:torchflower_crop";

		[StateRange(0, 7)]
		public int Growth { get => _growth.Value; set => NotifyStateUpdate(_growth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growth":
						NotifyStateUpdate(_growth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growth);
		} // method
	} // class

	public partial class Trapdoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:trapdoor";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class TrappedChest
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "north" };

		public override string Id => "minecraft:trapped_chest";

		[StateEnum("east", "north", "south", "west")]
		public override string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection);
		} // method
	} // class

	public partial class TripWire : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateByte _disarmedBit = new BlockStateByte() { Name = "disarmed_bit", Value = 0 };
		private readonly BlockStateByte _poweredBit = new BlockStateByte() { Name = "powered_bit", Value = 0 };
		private readonly BlockStateByte _suspendedBit = new BlockStateByte() { Name = "suspended_bit", Value = 0 };

		public override string Id => "minecraft:trip_wire";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateBit]
		public bool DisarmedBit { get => Convert.ToBoolean(_disarmedBit.Value); set => NotifyStateUpdate(_disarmedBit, value); }

		[StateBit]
		public bool PoweredBit { get => Convert.ToBoolean(_poweredBit.Value); set => NotifyStateUpdate(_poweredBit, value); }

		[StateBit]
		public bool SuspendedBit { get => Convert.ToBoolean(_suspendedBit.Value); set => NotifyStateUpdate(_suspendedBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "disarmed_bit":
						NotifyStateUpdate(_disarmedBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "powered_bit":
						NotifyStateUpdate(_poweredBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "suspended_bit":
						NotifyStateUpdate(_suspendedBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _disarmedBit;
			yield return _poweredBit;
			yield return _suspendedBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _disarmedBit, _poweredBit, _suspendedBit);
		} // method
	} // class

	public partial class TripwireHook : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _poweredBit = new BlockStateByte() { Name = "powered_bit", Value = 0 };

		public override string Id => "minecraft:tripwire_hook";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool PoweredBit { get => Convert.ToBoolean(_poweredBit.Value); set => NotifyStateUpdate(_poweredBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "powered_bit":
						NotifyStateUpdate(_poweredBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _direction;
			yield return _poweredBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _direction, _poweredBit);
		} // method
	} // class

	public partial class TubeCoral : Block
	{
		public override string Id => "minecraft:tube_coral";
	} // class

	public partial class Tuff : Block
	{
		public override string Id => "minecraft:tuff";
	} // class

	public partial class TurtleEgg : Block
	{
		private readonly BlockStateString _crackedState = new BlockStateString() { Name = "cracked_state", Value = "no_cracks" };
		private readonly BlockStateString _turtleEggCount = new BlockStateString() { Name = "turtle_egg_count", Value = "one_egg" };

		public override string Id => "minecraft:turtle_egg";

		[StateEnum("cracked", "max_cracked", "no_cracks")]
		public string CrackedState { get => _crackedState.Value; set => NotifyStateUpdate(_crackedState, value); }

		[StateEnum("four_egg", "one_egg", "three_egg", "two_egg")]
		public string TurtleEggCount { get => _turtleEggCount.Value; set => NotifyStateUpdate(_turtleEggCount, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "cracked_state":
						NotifyStateUpdate(_crackedState, s.Value);
						break;
					case BlockStateString s when s.Name == "turtle_egg_count":
						NotifyStateUpdate(_turtleEggCount, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _crackedState;
			yield return _turtleEggCount;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _crackedState, _turtleEggCount);
		} // method
	} // class

	public partial class TwistingVines : Block
	{
		private readonly BlockStateInt _twistingVinesAge = new BlockStateInt() { Name = "twisting_vines_age", Value = 0 };

		public override string Id => "minecraft:twisting_vines";

		[StateRange(0, 25)]
		public int TwistingVinesAge { get => _twistingVinesAge.Value; set => NotifyStateUpdate(_twistingVinesAge, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "twisting_vines_age":
						NotifyStateUpdate(_twistingVinesAge, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _twistingVinesAge;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _twistingVinesAge);
		} // method
	} // class

	public partial class UnderwaterTorch : Block
	{
		private readonly BlockStateString _torchFacingDirection = new BlockStateString() { Name = "torch_facing_direction", Value = "unknown" };

		public override string Id => "minecraft:underwater_torch";

		[StateEnum("east", "north", "south", "top", "unknown", "west")]
		public string TorchFacingDirection { get => _torchFacingDirection.Value; set => NotifyStateUpdate(_torchFacingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "torch_facing_direction":
						NotifyStateUpdate(_torchFacingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _torchFacingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _torchFacingDirection);
		} // method
	} // class

	public partial class UndyedShulkerBox : Block
	{
		public override string Id => "minecraft:undyed_shulker_box";
	} // class

	public partial class Unknown : Block
	{
		public override string Id => "minecraft:unknown";
	} // class

	public partial class UnlitRedstoneTorch
	{
		private readonly BlockStateString _torchFacingDirection = new BlockStateString() { Name = "torch_facing_direction", Value = "unknown" };

		public override string Id => "minecraft:unlit_redstone_torch";

		[StateEnum("east", "north", "south", "top", "unknown", "west")]
		public override string TorchFacingDirection { get => _torchFacingDirection.Value; set => NotifyStateUpdate(_torchFacingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "torch_facing_direction":
						NotifyStateUpdate(_torchFacingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _torchFacingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _torchFacingDirection);
		} // method
	} // class

	public partial class UnpoweredComparator
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };
		private readonly BlockStateByte _outputLitBit = new BlockStateByte() { Name = "output_lit_bit", Value = 0 };
		private readonly BlockStateByte _outputSubtractBit = new BlockStateByte() { Name = "output_subtract_bit", Value = 0 };

		public override string Id => "minecraft:unpowered_comparator";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		[StateBit]
		public bool OutputLitBit { get => Convert.ToBoolean(_outputLitBit.Value); set => NotifyStateUpdate(_outputLitBit, value); }

		[StateBit]
		public bool OutputSubtractBit { get => Convert.ToBoolean(_outputSubtractBit.Value); set => NotifyStateUpdate(_outputSubtractBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "output_lit_bit":
						NotifyStateUpdate(_outputLitBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "output_subtract_bit":
						NotifyStateUpdate(_outputSubtractBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
			yield return _outputLitBit;
			yield return _outputSubtractBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection, _outputLitBit, _outputSubtractBit);
		} // method
	} // class

	public partial class UnpoweredRepeater
	{
		private readonly BlockStateString _cardinalDirection = new BlockStateString() { Name = "minecraft:cardinal_direction", Value = "south" };
		private readonly BlockStateInt _repeaterDelay = new BlockStateInt() { Name = "repeater_delay", Value = 0 };

		public override string Id => "minecraft:unpowered_repeater";

		[StateEnum("east", "north", "south", "west")]
		public string CardinalDirection { get => _cardinalDirection.Value; set => NotifyStateUpdate(_cardinalDirection, value); }

		[StateRange(0, 3)]
		public int RepeaterDelay { get => _repeaterDelay.Value; set => NotifyStateUpdate(_repeaterDelay, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:cardinal_direction":
						NotifyStateUpdate(_cardinalDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "repeater_delay":
						NotifyStateUpdate(_repeaterDelay, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _cardinalDirection;
			yield return _repeaterDelay;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _cardinalDirection, _repeaterDelay);
		} // method
	} // class

	public partial class VerdantFroglight : Block
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:verdant_froglight";

		[StateEnum("x", "y", "z")]
		public string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class Vine : Block
	{
		private readonly BlockStateInt _vineDirectionBits = new BlockStateInt() { Name = "vine_direction_bits", Value = 0 };

		public override string Id => "minecraft:vine";

		[StateRange(0, 15)]
		public int VineDirectionBits { get => _vineDirectionBits.Value; set => NotifyStateUpdate(_vineDirectionBits, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "vine_direction_bits":
						NotifyStateUpdate(_vineDirectionBits, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _vineDirectionBits;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _vineDirectionBits);
		} // method
	} // class

	public partial class WallBanner : Block
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:wall_banner";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class WallSign
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class WarpedButton : Block
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:warped_button";

		[StateBit]
		public bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class WarpedDoor : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:warped_door";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class WarpedDoubleSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:warped_double_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WarpedFence : Block
	{
		public override string Id => "minecraft:warped_fence";
	} // class

	public partial class WarpedFenceGate : Block
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _inWallBit = new BlockStateByte() { Name = "in_wall_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };

		public override string Id => "minecraft:warped_fence_gate";

		[StateRange(0, 3)]
		public int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public bool InWallBit { get => Convert.ToBoolean(_inWallBit.Value); set => NotifyStateUpdate(_inWallBit, value); }

		[StateBit]
		public bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "in_wall_bit":
						NotifyStateUpdate(_inWallBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _inWallBit;
			yield return _openBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _inWallBit, _openBit);
		} // method
	} // class

	public partial class WarpedFungus : Block
	{
		public override string Id => "minecraft:warped_fungus";
	} // class

	public partial class WarpedHangingSign : Block
	{
		private readonly BlockStateByte _attachedBit = new BlockStateByte() { Name = "attached_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };
		private readonly BlockStateByte _hanging = new BlockStateByte() { Name = "hanging", Value = 0 };

		public override string Id => "minecraft:warped_hanging_sign";

		[StateBit]
		public bool AttachedBit { get => Convert.ToBoolean(_attachedBit.Value); set => NotifyStateUpdate(_attachedBit, value); }

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		[StateBit]
		public bool Hanging { get => Convert.ToBoolean(_hanging.Value); set => NotifyStateUpdate(_hanging, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "attached_bit":
						NotifyStateUpdate(_attachedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
					case BlockStateByte s when s.Name == "hanging":
						NotifyStateUpdate(_hanging, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _attachedBit;
			yield return _facingDirection;
			yield return _groundSignDirection;
			yield return _hanging;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _attachedBit, _facingDirection, _groundSignDirection, _hanging);
		} // method
	} // class

	public partial class WarpedHyphae : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:warped_hyphae";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class WarpedNylium : Block
	{
		public override string Id => "minecraft:warped_nylium";
	} // class

	public partial class WarpedPlanks : Block
	{
		public override string Id => "minecraft:warped_planks";
	} // class

	public partial class WarpedPressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:warped_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class WarpedRoots : Block
	{
		public override string Id => "minecraft:warped_roots";
	} // class

	public partial class WarpedSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:warped_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WarpedStairs
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:warped_stairs";

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public override int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class WarpedStandingSign
	{
		private readonly BlockStateInt _groundSignDirection = new BlockStateInt() { Name = "ground_sign_direction", Value = 0 };

		public override string Id => "minecraft:warped_standing_sign";

		[StateRange(0, 15)]
		public int GroundSignDirection { get => _groundSignDirection.Value; set => NotifyStateUpdate(_groundSignDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "ground_sign_direction":
						NotifyStateUpdate(_groundSignDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _groundSignDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _groundSignDirection);
		} // method
	} // class

	public partial class WarpedStem : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "y" };

		public override string Id => "minecraft:warped_stem";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis);
		} // method
	} // class

	public partial class WarpedTrapdoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };

		public override string Id => "minecraft:warped_trapdoor";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _openBit;
			yield return _upsideDownBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _openBit, _upsideDownBit);
		} // method
	} // class

	public partial class WarpedWallSign
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:warped_wall_sign";

		[StateRange(0, 5)]
		public int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class WarpedWartBlock : Block
	{
		public override string Id => "minecraft:warped_wart_block";
	} // class

	public partial class Water
	{
		private readonly BlockStateInt _liquidDepth = new BlockStateInt() { Name = "liquid_depth", Value = 0 };

		public override string Id => "minecraft:water";

		[StateRange(0, 15)]
		public override int LiquidDepth { get => _liquidDepth.Value; set => NotifyStateUpdate(_liquidDepth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "liquid_depth":
						NotifyStateUpdate(_liquidDepth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _liquidDepth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _liquidDepth);
		} // method
	} // class

	public partial class Waterlily : Block
	{
		public override string Id => "minecraft:waterlily";
	} // class

	public partial class WaxedCopper : Block
	{
		public override string Id => "minecraft:waxed_copper";
	} // class

	public partial class WaxedCutCopper : Block
	{
		public override string Id => "minecraft:waxed_cut_copper";
	} // class

	public partial class WaxedCutCopperSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:waxed_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WaxedCutCopperStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:waxed_cut_copper_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class WaxedDoubleCutCopperSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:waxed_double_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WaxedExposedCopper : Block
	{
		public override string Id => "minecraft:waxed_exposed_copper";
	} // class

	public partial class WaxedExposedCutCopper : Block
	{
		public override string Id => "minecraft:waxed_exposed_cut_copper";
	} // class

	public partial class WaxedExposedCutCopperSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:waxed_exposed_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WaxedExposedCutCopperStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:waxed_exposed_cut_copper_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class WaxedExposedDoubleCutCopperSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:waxed_exposed_double_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WaxedOxidizedCopper : Block
	{
		public override string Id => "minecraft:waxed_oxidized_copper";
	} // class

	public partial class WaxedOxidizedCutCopper : Block
	{
		public override string Id => "minecraft:waxed_oxidized_cut_copper";
	} // class

	public partial class WaxedOxidizedCutCopperSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:waxed_oxidized_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WaxedOxidizedCutCopperStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:waxed_oxidized_cut_copper_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class WaxedOxidizedDoubleCutCopperSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:waxed_oxidized_double_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WaxedWeatheredCopper : Block
	{
		public override string Id => "minecraft:waxed_weathered_copper";
	} // class

	public partial class WaxedWeatheredCutCopper : Block
	{
		public override string Id => "minecraft:waxed_weathered_cut_copper";
	} // class

	public partial class WaxedWeatheredCutCopperSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:waxed_weathered_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WaxedWeatheredCutCopperStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:waxed_weathered_cut_copper_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class WaxedWeatheredDoubleCutCopperSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:waxed_weathered_double_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WeatheredCopper : Block
	{
		public override string Id => "minecraft:weathered_copper";
	} // class

	public partial class WeatheredCutCopper : Block
	{
		public override string Id => "minecraft:weathered_cut_copper";
	} // class

	public partial class WeatheredCutCopperSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:weathered_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class WeatheredCutCopperStairs : Block
	{
		private readonly BlockStateByte _upsideDownBit = new BlockStateByte() { Name = "upside_down_bit", Value = 0 };
		private readonly BlockStateInt _weirdoDirection = new BlockStateInt() { Name = "weirdo_direction", Value = 0 };

		public override string Id => "minecraft:weathered_cut_copper_stairs";

		[StateBit]
		public bool UpsideDownBit { get => Convert.ToBoolean(_upsideDownBit.Value); set => NotifyStateUpdate(_upsideDownBit, value); }

		[StateRange(0, 3)]
		public int WeirdoDirection { get => _weirdoDirection.Value; set => NotifyStateUpdate(_weirdoDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "upside_down_bit":
						NotifyStateUpdate(_upsideDownBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "weirdo_direction":
						NotifyStateUpdate(_weirdoDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _upsideDownBit;
			yield return _weirdoDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _upsideDownBit, _weirdoDirection);
		} // method
	} // class

	public partial class WeatheredDoubleCutCopperSlab : DoubleSlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };

		public override string Id => "minecraft:weathered_double_cut_copper_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf);
		} // method
	} // class

	public partial class Web : Block
	{
		public override string Id => "minecraft:web";
	} // class

	public partial class WeepingVines : Block
	{
		private readonly BlockStateInt _weepingVinesAge = new BlockStateInt() { Name = "weeping_vines_age", Value = 0 };

		public override string Id => "minecraft:weeping_vines";

		[StateRange(0, 25)]
		public int WeepingVinesAge { get => _weepingVinesAge.Value; set => NotifyStateUpdate(_weepingVinesAge, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "weeping_vines_age":
						NotifyStateUpdate(_weepingVinesAge, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _weepingVinesAge;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _weepingVinesAge);
		} // method
	} // class

	public partial class Wheat
	{
		private readonly BlockStateInt _growth = new BlockStateInt() { Name = "growth", Value = 0 };

		public override string Id => "minecraft:wheat";

		[StateRange(0, 7)]
		public override int Growth { get => _growth.Value; set => NotifyStateUpdate(_growth, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "growth":
						NotifyStateUpdate(_growth, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _growth;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _growth);
		} // method
	} // class

	public partial class WhiteCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:white_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class WhiteCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:white_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class WhiteCarpet : CarpetBase
	{
		public override string Id => "minecraft:white_carpet";
	} // class

	public partial class WhiteConcrete : ConcreteBase
	{
		public override string Id => "minecraft:white_concrete";
	} // class

	public partial class WhiteConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:white_concrete_powder";
	} // class

	public partial class WhiteGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:white_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class WhiteShulkerBox : Block
	{
		public override string Id => "minecraft:white_shulker_box";
	} // class

	public partial class WhiteStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:white_stained_glass";
	} // class

	public partial class WhiteStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:white_stained_glass_pane";
	} // class

	public partial class WhiteTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:white_terracotta";
	} // class

	public partial class WhiteWool : WoolBase
	{
		public override string Id => "minecraft:white_wool";
	} // class

	public partial class WitherRose : Block
	{
		public override string Id => "minecraft:wither_rose";
	} // class

	public partial class Wood : LogBase
	{
		private readonly BlockStateString _pillarAxis = new BlockStateString() { Name = "pillar_axis", Value = "x" };
		private readonly BlockStateByte _strippedBit = new BlockStateByte() { Name = "stripped_bit", Value = 0 };
		private readonly BlockStateString _woodType = new BlockStateString() { Name = "wood_type", Value = "oak" };

		public override string Id => "minecraft:wood";

		[StateEnum("x", "y", "z")]
		public override string PillarAxis { get => _pillarAxis.Value; set => NotifyStateUpdate(_pillarAxis, value); }

		[StateBit]
		public bool StrippedBit { get => Convert.ToBoolean(_strippedBit.Value); set => NotifyStateUpdate(_strippedBit, value); }

		[StateEnum("acacia", "birch", "dark_oak", "jungle", "oak", "spruce")]
		public string WoodType { get => _woodType.Value; set => NotifyStateUpdate(_woodType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "pillar_axis":
						NotifyStateUpdate(_pillarAxis, s.Value);
						break;
					case BlockStateByte s when s.Name == "stripped_bit":
						NotifyStateUpdate(_strippedBit, s.Value);
						break;
					case BlockStateString s when s.Name == "wood_type":
						NotifyStateUpdate(_woodType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _pillarAxis;
			yield return _strippedBit;
			yield return _woodType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _pillarAxis, _strippedBit, _woodType);
		} // method
	} // class

	public partial class WoodenButton
	{
		private readonly BlockStateByte _buttonPressedBit = new BlockStateByte() { Name = "button_pressed_bit", Value = 0 };
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:wooden_button";

		[StateBit]
		public override bool ButtonPressedBit { get => Convert.ToBoolean(_buttonPressedBit.Value); set => NotifyStateUpdate(_buttonPressedBit, value); }

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "button_pressed_bit":
						NotifyStateUpdate(_buttonPressedBit, s.Value);
						break;
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _buttonPressedBit;
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _buttonPressedBit, _facingDirection);
		} // method
	} // class

	public partial class WoodenDoor
	{
		private readonly BlockStateInt _direction = new BlockStateInt() { Name = "direction", Value = 0 };
		private readonly BlockStateByte _doorHingeBit = new BlockStateByte() { Name = "door_hinge_bit", Value = 0 };
		private readonly BlockStateByte _openBit = new BlockStateByte() { Name = "open_bit", Value = 0 };
		private readonly BlockStateByte _upperBlockBit = new BlockStateByte() { Name = "upper_block_bit", Value = 0 };

		public override string Id => "minecraft:wooden_door";

		[StateRange(0, 3)]
		public override int Direction { get => _direction.Value; set => NotifyStateUpdate(_direction, value); }

		[StateBit]
		public override bool DoorHingeBit { get => Convert.ToBoolean(_doorHingeBit.Value); set => NotifyStateUpdate(_doorHingeBit, value); }

		[StateBit]
		public override bool OpenBit { get => Convert.ToBoolean(_openBit.Value); set => NotifyStateUpdate(_openBit, value); }

		[StateBit]
		public override bool UpperBlockBit { get => Convert.ToBoolean(_upperBlockBit.Value); set => NotifyStateUpdate(_upperBlockBit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "direction":
						NotifyStateUpdate(_direction, s.Value);
						break;
					case BlockStateByte s when s.Name == "door_hinge_bit":
						NotifyStateUpdate(_doorHingeBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "open_bit":
						NotifyStateUpdate(_openBit, s.Value);
						break;
					case BlockStateByte s when s.Name == "upper_block_bit":
						NotifyStateUpdate(_upperBlockBit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _direction;
			yield return _doorHingeBit;
			yield return _openBit;
			yield return _upperBlockBit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _direction, _doorHingeBit, _openBit, _upperBlockBit);
		} // method
	} // class

	public partial class WoodenPressurePlate : Block
	{
		private readonly BlockStateInt _redstoneSignal = new BlockStateInt() { Name = "redstone_signal", Value = 0 };

		public override string Id => "minecraft:wooden_pressure_plate";

		[StateRange(0, 15)]
		public int RedstoneSignal { get => _redstoneSignal.Value; set => NotifyStateUpdate(_redstoneSignal, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "redstone_signal":
						NotifyStateUpdate(_redstoneSignal, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _redstoneSignal;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _redstoneSignal);
		} // method
	} // class

	public partial class WoodenSlab : SlabBase
	{
		private readonly BlockStateString _verticalHalf = new BlockStateString() { Name = "minecraft:vertical_half", Value = "bottom" };
		private readonly BlockStateString _woodType = new BlockStateString() { Name = "wood_type", Value = "oak" };

		public override string Id => "minecraft:wooden_slab";

		[StateEnum("bottom", "top")]
		public override string VerticalHalf { get => _verticalHalf.Value; set => NotifyStateUpdate(_verticalHalf, value); }

		[StateEnum("acacia", "birch", "dark_oak", "jungle", "oak", "spruce")]
		public string WoodType { get => _woodType.Value; set => NotifyStateUpdate(_woodType, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateString s when s.Name == "minecraft:vertical_half":
						NotifyStateUpdate(_verticalHalf, s.Value);
						break;
					case BlockStateString s when s.Name == "wood_type":
						NotifyStateUpdate(_woodType, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _verticalHalf;
			yield return _woodType;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _verticalHalf, _woodType);
		} // method
	} // class

	public partial class YellowCandle : Block
	{
		private readonly BlockStateInt _candles = new BlockStateInt() { Name = "candles", Value = 0 };
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:yellow_candle";

		[StateRange(0, 3)]
		public int Candles { get => _candles.Value; set => NotifyStateUpdate(_candles, value); }

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "candles":
						NotifyStateUpdate(_candles, s.Value);
						break;
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _candles;
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _candles, _lit);
		} // method
	} // class

	public partial class YellowCandleCake : Block
	{
		private readonly BlockStateByte _lit = new BlockStateByte() { Name = "lit", Value = 0 };

		public override string Id => "minecraft:yellow_candle_cake";

		[StateBit]
		public bool Lit { get => Convert.ToBoolean(_lit.Value); set => NotifyStateUpdate(_lit, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateByte s when s.Name == "lit":
						NotifyStateUpdate(_lit, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _lit;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _lit);
		} // method
	} // class

	public partial class YellowCarpet : CarpetBase
	{
		public override string Id => "minecraft:yellow_carpet";
	} // class

	public partial class YellowConcrete : ConcreteBase
	{
		public override string Id => "minecraft:yellow_concrete";
	} // class

	public partial class YellowConcretePowder : ConcretePowderBase
	{
		public override string Id => "minecraft:yellow_concrete_powder";
	} // class

	public partial class YellowFlower : Block
	{
		public override string Id => "minecraft:yellow_flower";
	} // class

	public partial class YellowGlazedTerracotta : GlazedTerracottaBase
	{
		private readonly BlockStateInt _facingDirection = new BlockStateInt() { Name = "facing_direction", Value = 0 };

		public override string Id => "minecraft:yellow_glazed_terracotta";

		[StateRange(0, 5)]
		public override int FacingDirection { get => _facingDirection.Value; set => NotifyStateUpdate(_facingDirection, value); }

		public override void SetStates(IEnumerable<IBlockState> states)
		{
			foreach (var state in states)
			{
				switch (state)
				{
					case BlockStateInt s when s.Name == "facing_direction":
						NotifyStateUpdate(_facingDirection, s.Value);
						break;
				} // switch
			} // foreach
		} // method

		protected override IEnumerable<IBlockState> GetStates()
		{
			yield return _facingDirection;
		} // method

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, _facingDirection);
		} // method
	} // class

	public partial class YellowShulkerBox : Block
	{
		public override string Id => "minecraft:yellow_shulker_box";
	} // class

	public partial class YellowStainedGlass : StainedGlassBase
	{
		public override string Id => "minecraft:yellow_stained_glass";
	} // class

	public partial class YellowStainedGlassPane : StainedGlassPaneBase
	{
		public override string Id => "minecraft:yellow_stained_glass_pane";
	} // class

	public partial class YellowTerracotta : TerracottaBase
	{
		public override string Id => "minecraft:yellow_terracotta";
	} // class

	public partial class YellowWool : WoolBase
	{
		public override string Id => "minecraft:yellow_wool";
	} // class
}
