using fNbt.Serialization.NamingStrategy;

namespace MiNET.Utils.Nbt
{
	public class NbtLowerCaseNamingStrategy : NbtNamingStrategy
	{
		public override string ResolveMemberName(string name)
		{
			return name.ToLower();
		}
	}
}
