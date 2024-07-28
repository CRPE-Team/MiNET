namespace MiNET.Plugins
{
	public enum CommandParameterType
	{
		Bool = -3,
		Enum = -1,

		Unknown = 0,

		Int = 1,
		Float = 3,
		Value = 4,
		WildcardInt = 5,
		Operator = 6,
		CompareOperator = 7,
		Target = 8,

		WildcardTarget = 10,

		Filepath = 17,

		FullIntegerRange = 23,

		EquipmentSlot = 47,
		String = 48,

		IntPosition = 64,
		Position = 65,

		Message = 67,

		Rawtext = 70,

		Json = 74,

		BlockStates = 84,

		Command = 87,


		EnumFlag = 0x200000,
		PostfixFlag = 0x1000000,
		SoftEnumFlag = 0x4000000
	}
}
