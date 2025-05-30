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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2022 Niclas Olofsson.
// All Rights Reserved.
#endregion

using System;
using System.Collections.Generic;

namespace MiNET.Net
{
	public class AbilityLayers : List<AbilityLayer>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.Write((byte) Count);

			foreach (var layer in this)
			{
				layer.Write(packet);
			}
		}

		public static AbilityLayers Read(Packet packet)
		{
			var layers = new AbilityLayers();

			var count = packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				layers.Add(AbilityLayer.Read(packet));
			}

			return layers;
		}
	}

	public class AbilityLayer : IPacketDataObject
	{
		public const int AbilityCount = 20;

		public AbilityLayerType Type { get; set; }

		public Dictionary<PlayerAbility, bool> Abilities { get; set; }

		public float FlySpeed { get; set; }

		public float VerticalFlySpeed { get; set; }

		public float WalkSpeed { get; set; }

		public void Write(Packet packet)
		{
			packet.Write((ushort) Type);

			var values = PlayerAbility.None;
			var abilities = PlayerAbility.None;

			foreach (var ability in Abilities)
			{
				abilities |= ability.Key;
				values |= ability.Value ? ability.Key : 0;
			}

			if (FlySpeed > 0) abilities |= PlayerAbility.FlySpeed;
			if (VerticalFlySpeed > 0) abilities |= PlayerAbility.AbilityVerticalFlySpeed;
			if (WalkSpeed > 0) abilities |= PlayerAbility.WalkSpeed;

			packet.Write((uint) abilities);
			packet.Write((uint) values);
			packet.Write(FlySpeed);
			packet.Write(VerticalFlySpeed);
			packet.Write(WalkSpeed);
		}

		public static AbilityLayer Read(Packet packet)
		{
			var type = (AbilityLayerType) packet.ReadUshort();
			var abilities = (PlayerAbility) packet.ReadUint();
			var values = (PlayerAbility) packet.ReadUint();

			var abilityValues = new Dictionary<PlayerAbility, bool>(AbilityCount);
			for (var i = 0; i < AbilityCount; i++)
			{
				var ability = (PlayerAbility) (1 << i);
				if (ability == PlayerAbility.FlySpeed
					|| ability == PlayerAbility.AbilityVerticalFlySpeed
					|| ability == PlayerAbility.WalkSpeed)
				{
					continue;
				}

				if (abilities.HasFlag(ability))
				{
					abilityValues.Add(ability, values.HasFlag(ability));
				}
			}

			return new AbilityLayer()
			{
				Type = type,
				Abilities = abilityValues,
				FlySpeed = packet.ReadFloat(),
				VerticalFlySpeed = packet.ReadFloat(),
				WalkSpeed = packet.ReadFloat()
			};
		}
	}

	public enum AbilityLayerType
	{
		CustomCache = 0,
		Base = 1,
		Spectator = 2,
		Commands = 3,
		Editor = 4,
		LoadingScreen = 5
	}

	[Flags]
	public enum PlayerAbility : uint
	{
		None = 0,

		Build = 1 << 0,
		Mine = 1 << 1,
		DoorsAndSwitches = 1 << 2,
		OpenContainers = 1 << 3,
		AttackPlayers = 1 << 4,
		AttackMobs = 1 << 5,
		OperatorCommands = 1 << 6,
		Teleport = 1 << 7,
		Invulnerable = 1 << 8,
		Flying = 1 << 9,
		MayFly = 1 << 10,
		InstantBuild = 1 << 11,
		Lightning = 1 << 12,
		FlySpeed = 1 << 13,
		WalkSpeed = 1 << 14,
		Muted = 1 << 15,
		WorldBuilder = 1 << 16,
		NoClip = 1 << 17,
		PrivilegedBuilder = 1 << 18,
		AbilityVerticalFlySpeed = 1 << 19
	}
}