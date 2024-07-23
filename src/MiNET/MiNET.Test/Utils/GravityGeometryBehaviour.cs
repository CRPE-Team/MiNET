using System;
using System.Numerics;
using log4net;
using MiNET.Entities;
using MiNET.Net;
using MiNET.Utils.Skins;
using MiNET.Utils.Vectors;

namespace MiNET.Test.Utils
{
	public class GravityGeometryBehavior
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(GravityGeometryBehavior));

		public const float Gravity = 0.20f;
		public const float Drag = 0.02f;

		public PlayerMob Mob { get; }
		public GeometryModel CurrentModel { get; private set; }
		public bool ResetOnEnd { get; set; }

		public GravityGeometryBehavior(PlayerMob mob, GeometryModel currentModel)
		{
			Mob = mob;
			CurrentModel = currentModel;
			var geometry = CurrentModel.CollapseToDerived(CurrentModel.FindGeometry(mob.Skin.GeometryName));
			geometry.Subdivide(true, false);

			SetVelocity(geometry, new Random());

			CurrentModel.Geometry.Clear();
			CurrentModel.Geometry.Add(geometry);
		}

		private void SetVelocity(GeometryModel model, Random random)
		{
			foreach (var geometry in model.Geometry)
			{
				SetVelocity(geometry, random);
			}
		}


		private void SetVelocity(Geometry geometry, Random random1)
		{
			var random = new Random();
			foreach (var bone in geometry.Bones)
			{
				SetVelocity(bone, random);
			}
		}

		private void SetVelocity(Bone bone, Random random)
		{
			if (bone.NeverRender) return;
			if (bone.Cubes == null || bone.Cubes.Count == 0) return;

			foreach (var cube in bone.Cubes)
			{
				SetVelocity(cube, random);
			}
		}

		private Vector3 _origin = new Vector3(0, 4, 10);

		private void SetVelocity(Cube cube, Random random)
		{
			var pos = new Vector3(cube.Origin[0] / 16f, cube.Origin[1] / 16f, cube.Origin[2] / 16f) + Mob.KnownPosition;
			var dir = pos - _origin;
			var distance = dir.Length();

			distance = Math.Max(1, distance);
			distance = distance / (distance * distance);
			if (distance < 0.1)
				return;

			var force = new Vector3(distance, distance, distance) * 5;
			cube.Velocity = Vector3.Reflect(dir.Normalize() * force, Vector3.UnitZ);
		}

		public void FakeMeltTicking(object sender, PlayerEventArgs playerEventArgs)
		{
			var mob = (PlayerMob) sender;
			if (CurrentModel == null)
			{
				Log.Warn($"No current model set for mob.");
				return;
			}

			try
			{
				var stillMoving = false;
				foreach (var geometry in CurrentModel.Geometry)
				{
					foreach (var bone in geometry.Bones)
					{
						if (bone.NeverRender) continue;
						if (bone.Cubes == null || bone.Cubes.Count == 0) continue;

						foreach (var cube in bone.Cubes)
						{
							if (cube.Origin[1] <= 0.05f && cube.Velocity.Y <= 0.01)
							{
								cube.Origin[1] = 0f;
								cube.Velocity = Vector3.Zero;
								continue;
							}

							stillMoving = true;

							var x = cube.Origin[0];
							var y = cube.Origin[1];
							var z = cube.Origin[2];

							cube.Origin = new[] { x + cube.Velocity.X, Math.Max(0f, y + cube.Velocity.Y), z + cube.Velocity.Z };
							cube.Velocity -= new Vector3(0, Gravity, 0);
							cube.Velocity *= 1 - Drag;
						}
					}
				}

				if (!stillMoving)
				{
					Log.Warn("Done. De-register tick.");
					mob.Ticking -= FakeMeltTicking;

					if (ResetOnEnd)
					{
						var skin = mob.Skin;

						var updateSkin = McpePlayerSkin.CreateObject();
						updateSkin.NoBatch = true;
						updateSkin.uuid = mob.ClientUuid;
						updateSkin.oldSkinName = mob.Skin.SkinId;
						updateSkin.skinName = mob.Skin.SkinId;
						updateSkin.skin = skin;

						mob.Level.RelayBroadcast(updateSkin);
					}
				}
				else
				{
					var skin = mob.Skin;
					var geometry = CurrentModel.FindGeometry(skin.GeometryName);
					geometry.Description.Identifier = $"geometry.{DateTime.UtcNow.Ticks}.{mob.ClientUuid}";
					mob.Skin.SkinResourcePatch = new SkinResourcePatch() { Geometry = new GeometryIdentifier() { Default = geometry.Description.Identifier } };

					CurrentModel.Geometry.Clear();
					CurrentModel.Geometry.Add(geometry);

					skin.GeometryName = geometry.Description.Identifier;
					skin.GeometryData = Skin.ToJson(CurrentModel);

					var updateSkin = McpePlayerSkin.CreateObject();
					updateSkin.NoBatch = true;
					updateSkin.uuid = mob.ClientUuid;
					updateSkin.oldSkinName = mob.Skin.SkinId;
					updateSkin.skinName = mob.Skin.SkinId;
					updateSkin.skin = skin;

					mob.Level.RelayBroadcast(updateSkin);
				}
			}
			catch (Exception e)
			{
				mob.Ticking -= FakeMeltTicking;
				Log.Error(e);
			}
		}
	}
}