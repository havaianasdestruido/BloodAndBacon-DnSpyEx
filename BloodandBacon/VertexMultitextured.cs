using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000080 RID: 128
	public struct VertexMultitextured : IVertexType
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x000FE13C File Offset: 0x000FC33C
		public VertexDeclaration VertexDeclaration
		{
			get
			{
				return VertexMultitextured.myVertexDeclaration;
			}
		}

		// Token: 0x040011B8 RID: 4536
		public Vector3 Position;

		// Token: 0x040011B9 RID: 4537
		public Vector3 Normal;

		// Token: 0x040011BA RID: 4538
		public Vector4 TextureCoordinate;

		// Token: 0x040011BB RID: 4539
		public Vector4 TexWeights;

		// Token: 0x040011BC RID: 4540
		public static int SizeInBytes = 56;

		// Token: 0x040011BD RID: 4541
		private static readonly VertexDeclaration myVertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
			new VertexElement(24, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 0),
			new VertexElement(40, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 1)
		});
	}
}
