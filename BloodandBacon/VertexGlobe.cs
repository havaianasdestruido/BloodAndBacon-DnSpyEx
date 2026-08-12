using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000021 RID: 33
	public struct VertexGlobe : IVertexType
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00030925 File Offset: 0x0002EB25
		public VertexDeclaration VertexDeclaration
		{
			get
			{
				return VertexGlobe.myVertexDeclaration;
			}
		}

		// Token: 0x040006C8 RID: 1736
		public Vector3 Position;

		// Token: 0x040006C9 RID: 1737
		public Vector3 Normal;

		// Token: 0x040006CA RID: 1738
		public Vector4 TextureCoordinate;

		// Token: 0x040006CB RID: 1739
		public Vector4 TexWeights;

		// Token: 0x040006CC RID: 1740
		public static int SizeInBytes = 56;

		// Token: 0x040006CD RID: 1741
		private static readonly VertexDeclaration myVertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
			new VertexElement(24, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 0),
			new VertexElement(40, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 1)
		});
	}
}
