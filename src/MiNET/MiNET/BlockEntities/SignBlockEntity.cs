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

using System.Drawing;
using fNbt.Serialization;

namespace MiNET.BlockEntities
{
	public class SignBlockEntity : BlockEntity
	{
		/// <summary>
		/// A compound which discribes back text. 
		/// </summary>
		public SignText BackText { get; set; } = new();

		/// <summary>
		/// A compound which discribes front text.
		/// </summary>
		public SignText FrontText { get; set; } = new();

		/// <summary>
		///  true if the text is locked with honeycomb.
		/// </summary>
		public bool IsWaxed { get; set; }

		public long LockedForEditingBy { get; set; }


		public SignBlockEntity() : base(BlockEntityIds.Sign)
		{
			
		}

		protected SignBlockEntity(string name) : base(name)
		{

		}
	}

	[NbtObject]
	public class SignText
	{
		/// <summary>
		/// true if the outer glow of a sign with glowing text does not show.
		/// </summary>
		public bool HideGlowOutline { get; set; }

		/// <summary>
		/// true if the sign has been dyed with a glow ink sac.
		/// </summary>
		public bool IgnoreLighting { get; set; }

		/// <summary>
		/// Unknown. Defaults to true.
		/// </summary>
		public bool PersistFormatting { get; set; } = true;

		/// <summary>
		/// The color that has been used to dye the sign. Default is Black.
		/// </summary>
		public Color SignTextColor { get; set; } = SignColor.Black;

		/// <summary>
		/// The text on it.
		/// </summary>
		public string Text { get; set; } = string.Empty;

		/// <summary>
		/// Unknown.
		/// </summary>
		public string TextOwner { get; set; } = string.Empty;
	}

	public static class SignColor
	{
		public static Color Black { get; } = Color.FromArgb(0, 0, 0);

		public static Color White { get; } = Color.FromArgb(240, 240, 240);

		public static Color Orange { get; } = Color.FromArgb(249, 128, 29);

		public static Color Magenta { get; } = Color.FromArgb(199, 78, 189);

		public static Color LightBlue  { get; } = Color.FromArgb(58, 179, 218);

		public static Color Yellow { get; } = Color.FromArgb(254, 216, 61);

		public static Color Lime { get; } = Color.FromArgb(128, 199, 31);

		public static Color Pink { get; } = Color.FromArgb(243, 139, 170);

		public static Color Gray { get; } = Color.FromArgb(71, 79, 82);

		public static Color LightGray { get; } = Color.FromArgb(157, 157, 151);

		public static Color Cyan { get; } = Color.FromArgb(22, 156, 156);

		public static Color Purple { get; } = Color.FromArgb(137, 50, 184);

		public static Color Blue { get; } = Color.FromArgb(60, 68, 170);

		public static Color Brown { get; } = Color.FromArgb(131, 84, 50);

		public static Color Green { get; } = Color.FromArgb(94, 124, 22);

		public static Color Red { get; } = Color.FromArgb(176, 46, 38);
	}
}