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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2018 Niclas Olofsson. 
// All Rights Reserved.

#endregion

namespace MiNET.Net
{
	public enum PlayerAction
	{
		StartBreak = 0,
		AbortBreak = 1,
		StopBreak = 2,
		GetUpdatedBlock = 3,
		DropItem = 4,
		StartSleeping = 5,
		StopSleeping = 6,
		Respawn = 7,
		Jump = 8,
		StartSprint = 9,
		StopSprint = 10,
		StartSneak = 11,
		StopSneak = 12,
		CreativeDestroy = 13,
		DimensionChangeAck = 14, //sent when spawning in a different dimension to tell the server we spawned
		StartGlide = 15,
		StopGlide = 16,
		WorldImmutable = 17,
		Breaking = 18,
		ChangeSkin = 19,
		SetEnchantmentSeed = 20, //no longer used
		StartSwimming = 21,
		StopSwimming = 22,
		StartSpinAttack = 23,
		StopSpinAttack = 24,
		InteractBlock = 25,
		PredictDestroyBlock = 26,
		ContinueDestroyBlock = 27,
		StartItemUse = 28,
		StopItemUse = 29,
		HandledTeleport = 30,
		MissedSwing = 31,
		StartCrawling = 32,
		StopCrawling = 33,
		StartFlying = 34,
		StopFlying = 35,
		AckEntityData = 36,
		StartUsingItem = 37
	}
}