using System;
using System.Numerics;
using log4net;
using MiNET.Utils;
using MiNET.Utils.Nbt;

namespace MiNET.Net
{
	public class SpawnSettings : IPacketDataObject
	{
		public short BiomeType { get; set; }

		public string BiomeName { get; set; }

		public int Dimension { get; set; }

		public void Write(Packet packet)
		{
			packet.Write(BiomeType);
			packet.Write(BiomeName);
			packet.WriteVarInt(Dimension);
		}

		public static SpawnSettings Read(Packet packet)
		{
			return new SpawnSettings()
			{
				BiomeType = packet.ReadShort(),
				BiomeName = packet.ReadString(),
				Dimension = packet.ReadVarInt()
			};
		}
	}

	public class LevelSettings : IPacketDataObject
	{
		public long Seed { get; set; }

		public SpawnSettings SpawnSettings { get; set; }

		public int Generator { get; set; }

		public int GameMode { get; set; }

		public bool Hardcore { get; set; }

		public int Difficulty { get; set; }

		public int X { get; set; }

		public int Y { get; set; }

		public int Z { get; set; }

		public bool HasAchievementsDisabled { get; set; }

		public int EditorWorldType { get; set; }

		public bool CreatedInEditorMode { get; set; }

		public bool ExportedFromEditorMode { get; set; }

		public int Time { get; set; }

		public int EduOffer { get; set; }

		public bool HasEduFeaturesEnabled { get; set; }

		public string EduProductUuid { get; set; }

		public float RainLevel { get; set; }

		public float LightningLevel { get; set; }

		public bool HasConfirmedPlatformLockedContent { get; set; }

		public bool IsMultiplayer { get; set; }

		public bool BroadcastToLan { get; set; }

		public int XboxLiveBroadcastMode { get; set; }

		public int PlatformBroadcastMode { get; set; }

		public bool EnableCommands { get; set; }

		public bool IsTexturepacksRequired { get; set; }

		public GameRules GameRules { get; set; }

		public Experiments Experiments { get; set; }

		public bool BonusChest { get; set; }

		public bool MapEnabled { get; set; }

		public byte PermissionLevel { get; set; }

		public int ServerChunkTickRange { get; set; }

		public bool HasLockedBehaviorPack { get; set; }

		public bool HasLockedResourcePack { get; set; }

		public bool IsFromLockedWorldTemplate { get; set; }

		public bool UseMsaGamertagsOnly { get; set; }

		public bool IsFromWorldTemplate { get; set; }

		public bool IsWorldTemplateOptionLocked { get; set; }

		public bool OnlySpawnV1Villagers { get; set; }

		public bool IsDisablingPersonas { get; set; }

		public bool IsDisablingCustomSkins { get; set; }

		public bool EmoteChatMuted { get; set; }
		
		public string GameVersion { get; set; }

		public int LimitedWorldWidth { get; set; }

		public int LimitedWorldLength { get; set; }

		public bool IsNewNether { get; set; }

		public EducationUriResource EduSharedUriResource { get; set; }

		public bool ExperimentalGameplayOverride { get; set; }

		public byte ChatRestrictionLevel { get; set; }

		public bool IsDisablePlayerInteractions { get; set; }

		public string ServerIdentifier { get; set; }

		public string WorldIdentifier { get; set; }

		public string ScenarioIdentifier { get; set; }

		public void Write(Packet packet)
		{
			packet.Write(Seed);

			packet.Write(SpawnSettings ?? new SpawnSettings());

			packet.WriteSignedVarInt(Generator);
			packet.WriteSignedVarInt(GameMode);
			packet.Write(Hardcore);
			packet.WriteSignedVarInt(Difficulty);

			packet.WriteSignedVarInt(X);
			packet.WriteVarInt(Y);
			packet.WriteSignedVarInt(Z);

			packet.Write(HasAchievementsDisabled);
			packet.WriteVarInt(EditorWorldType);
			packet.Write(CreatedInEditorMode);
			packet.Write(ExportedFromEditorMode);
			packet.WriteSignedVarInt(Time);
			packet.WriteSignedVarInt(EduOffer);
			packet.Write(HasEduFeaturesEnabled);
			packet.Write(EduProductUuid);
			packet.Write(RainLevel);
			packet.Write(LightningLevel);
			packet.Write(HasConfirmedPlatformLockedContent);
			packet.Write(IsMultiplayer);
			packet.Write(BroadcastToLan);
			packet.WriteVarInt(XboxLiveBroadcastMode);
			packet.WriteVarInt(PlatformBroadcastMode);
			packet.Write(EnableCommands);
			packet.Write(IsTexturepacksRequired);
			packet.Write(GameRules);
			packet.Write(Experiments);
			packet.Write(false); //ExperimentsPreviouslyToggled
			packet.Write(BonusChest);
			packet.Write(MapEnabled);
			packet.Write(PermissionLevel);
			packet.Write(ServerChunkTickRange);
			packet.Write(HasLockedBehaviorPack);
			packet.Write(HasLockedResourcePack);
			packet.Write(IsFromLockedWorldTemplate);
			packet.Write(UseMsaGamertagsOnly);
			packet.Write(IsFromWorldTemplate);
			packet.Write(IsWorldTemplateOptionLocked);
			packet.Write(OnlySpawnV1Villagers);
			packet.Write(IsDisablingPersonas);
			packet.Write(IsDisablingCustomSkins);
			packet.Write(EmoteChatMuted);
			packet.Write(GameVersion);
			packet.Write(LimitedWorldWidth);
			packet.Write(LimitedWorldLength);
			packet.Write(IsNewNether);
			packet.Write(EduSharedUriResource ?? new EducationUriResource("", ""));
			packet.Write(false);
			packet.Write(ChatRestrictionLevel);
			packet.Write(IsDisablePlayerInteractions);
			packet.Write(ServerIdentifier);
			packet.Write(WorldIdentifier);
			packet.Write(ScenarioIdentifier);
		}

		public static LevelSettings Read(Packet packet)
		{
			var settings = new LevelSettings();

			settings.Seed = packet.ReadLong();

			settings.SpawnSettings = SpawnSettings.Read(packet);

			settings.Generator = packet.ReadSignedVarInt();
			settings.GameMode = packet.ReadSignedVarInt();
			settings.Hardcore = packet.ReadBool();
			settings.Difficulty = packet.ReadSignedVarInt();

			settings.X = packet.ReadSignedVarInt();
			settings.Y = packet.ReadVarInt();
			settings.Z = packet.ReadSignedVarInt();

			settings.HasAchievementsDisabled = packet.ReadBool();
			settings.EditorWorldType = packet.ReadVarInt();
			settings.CreatedInEditorMode = packet.ReadBool();
			settings.ExportedFromEditorMode= packet.ReadBool();
			settings.Time = packet.ReadSignedVarInt();
			settings.EduOffer = packet.ReadSignedVarInt();
			settings.HasEduFeaturesEnabled = packet.ReadBool();
			settings.EduProductUuid = packet.ReadString();
			settings.RainLevel = packet.ReadFloat();
			settings.LightningLevel = packet.ReadFloat();
			settings.HasConfirmedPlatformLockedContent = packet.ReadBool();
			settings.IsMultiplayer = packet.ReadBool();
			settings.BroadcastToLan = packet.ReadBool();
			settings.XboxLiveBroadcastMode = packet.ReadVarInt();
			settings.PlatformBroadcastMode = packet.ReadVarInt();
			settings.EnableCommands = packet.ReadBool();
			settings.IsTexturepacksRequired = packet.ReadBool();
			settings.GameRules = packet.ReadGameRules();
			settings.Experiments = packet.ReadExperiments();
			packet.ReadBool();
			settings.BonusChest = packet.ReadBool();
			settings.MapEnabled = packet.ReadBool();
			settings.PermissionLevel = packet.ReadByte();
			settings.ServerChunkTickRange = packet.ReadInt();
			settings.HasLockedBehaviorPack = packet.ReadBool();
			settings.HasLockedResourcePack = packet.ReadBool();
			settings.IsFromLockedWorldTemplate = packet.ReadBool();
			settings.UseMsaGamertagsOnly = packet.ReadBool();
			settings.IsFromWorldTemplate = packet.ReadBool();
			settings.IsWorldTemplateOptionLocked = packet.ReadBool();
			settings.OnlySpawnV1Villagers = packet.ReadBool();
			settings.IsDisablingPersonas = packet.ReadBool();
			settings.IsDisablingCustomSkins = packet.ReadBool();
			settings.EmoteChatMuted = packet.ReadBool();
			settings.GameVersion = packet.ReadString();

			settings.LimitedWorldWidth = packet.ReadInt();
			settings.LimitedWorldLength = packet.ReadInt();
			settings.IsNewNether = packet.ReadBool();
			settings.EduSharedUriResource = packet.ReadEducationUriResource();

			if (packet.ReadBool())
			{
				settings.ExperimentalGameplayOverride = packet.ReadBool();
			}
			else
			{
				settings.ExperimentalGameplayOverride = false;
			}

			settings.ChatRestrictionLevel = packet.ReadByte();
			settings.IsDisablePlayerInteractions = packet.ReadBool();
			settings.ServerIdentifier = packet.ReadString();
			settings.WorldIdentifier = packet.ReadString();
			settings.ScenarioIdentifier = packet.ReadString();

			return settings;
		}
	}

	public partial class McpeStartGame : Packet<McpeStartGame>
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(McpeStartGame));
		
		public long entityIdSelf; // = null;
		public long runtimeEntityId; // = null;
		public int playerGamemode; // = null;
		public Vector3 spawn; // = null;
		public Vector2 rotation; // = null;
		
		public string levelId; // = null;
		public string worldName; // = null;
		public string premiumWorldTemplateId; // = null;
		public bool isTrial; // = null;
		public int movementType; // = null;
		public int movementRewindHistorySize; // = null;
		public bool enableNewBlockBreakSystem; // = null;
		public long currentTick; // = null;
		public int enchantmentSeed; // = null;
		public BlockPalette blockPalette; // = null;
		public ulong blockPaletteChecksum;
		public ItemStates itemstates; // = null;
		public string multiplayerCorrelationId; // = null;
		public bool enableNewInventorySystem; // = null;
		public string serverVersion; // = null;
		public Nbt propertyData; // = null;
		public UUID worldTemplateId; // = null;
		public bool clientSideGenerationEnabled; // = null;
		public bool blockNetworkIdsAreHashes; // = null;
		public bool disableClientSounds; // = null;

		public LevelSettings levelSettings = new LevelSettings();
		
		partial void AfterEncode()
		{
			WriteSignedVarLong(entityIdSelf);
			WriteUnsignedVarLong(runtimeEntityId);
			WriteSignedVarInt(playerGamemode);
			Write(spawn);
			Write(rotation);
			
			LevelSettings s = levelSettings ?? new LevelSettings();
			s.Write(this);
			
			Write(levelId);
			Write(worldName);
			Write(premiumWorldTemplateId);
			Write(isTrial);
			
			//Player movement settings
			WriteSignedVarInt(movementType);
			WriteSignedVarInt(movementRewindHistorySize);
			Write(enableNewBlockBreakSystem);
			
			Write(currentTick);
			WriteSignedVarInt(enchantmentSeed);
			
			Write(blockPalette);

			Write(itemstates);
			
			Write(multiplayerCorrelationId);
			Write(enableNewInventorySystem);
			Write(serverVersion);
			Write(propertyData);
			Write(blockPaletteChecksum);
			Write(worldTemplateId);
			Write(clientSideGenerationEnabled);
			Write(blockNetworkIdsAreHashes);
			Write(disableClientSounds);
		}
		
		partial void AfterDecode()
		{
			entityIdSelf = ReadSignedVarLong();
			runtimeEntityId = ReadUnsignedVarLong();
			playerGamemode = ReadSignedVarInt();
			spawn = ReadVector3();
			rotation = ReadVector2();

			levelSettings = LevelSettings.Read(this);
			
			levelId = ReadString();
			worldName = ReadString();
			premiumWorldTemplateId = ReadString();
			isTrial = ReadBool();
			
			//Player movement settings
			movementType = ReadSignedVarInt();
			movementRewindHistorySize = ReadSignedVarInt();
			enableNewBlockBreakSystem = ReadBool();
			
			currentTick = ReadLong();
			enchantmentSeed = ReadSignedVarInt();

			try
			{
				blockPalette = ReadBlockPalette();
			}
			catch (Exception ex)
			{
				Log.Warn($"Failed to read complete blockpallete", ex);
				return;
			}
			
			itemstates = ReadItemStates();
			
			multiplayerCorrelationId = ReadString();
			enableNewInventorySystem = ReadBool();
			serverVersion = ReadString();
			propertyData = ReadNbt();
			blockPaletteChecksum = ReadUlong();
			worldTemplateId = ReadUUID();
			clientSideGenerationEnabled = ReadBool();
			blockNetworkIdsAreHashes = ReadBool();
			disableClientSounds = ReadBool();
		}

		/// <inheritdoc />
		public override void Reset()
		{
			entityIdSelf=default(long);
			runtimeEntityId=default(long);
			playerGamemode=default(int);
			spawn=default(Vector3);
			rotation=default(Vector2);
			levelSettings = default;
			levelId=default(string);
			worldName=default(string);
			premiumWorldTemplateId=default(string);
			isTrial=default(bool);
			movementType=default(int);
			movementRewindHistorySize=default(int);
			enableNewBlockBreakSystem=default(bool);
			currentTick=default(long);
			enchantmentSeed=default(int);
			blockPalette=default(BlockPalette);
			itemstates=default(ItemStates);
			multiplayerCorrelationId=default(string);
			enableNewInventorySystem=default(bool);
			serverVersion=default(string);
			propertyData=default;
			worldTemplateId=default;
			clientSideGenerationEnabled=default(bool);
			blockNetworkIdsAreHashes=default(bool);
			disableClientSounds=default(bool);
			base.Reset();
		}
	}
}