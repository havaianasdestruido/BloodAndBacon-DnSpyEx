using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000081 RID: 129
	internal class tmake : GameScreen
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x000FE1C4 File Offset: 0x000FC3C4
		public void Load(ContentManager content, ScreenManager graphics)
		{
			this.sc = graphics;
			this.Tw = this.sc.bitmap;
			this.gridScale = this.sc.gridScale;
			this.Tv = new VertexMultitextured[this.Tw * this.Tw];
			this.device = graphics.GraphicsDevice;
			if (content == null)
			{
				content = new ContentManager(this.sc.Game.Services, "Content");
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000FE240 File Offset: 0x000FC440
		public override void UnloadContent()
		{
			this.content.Unload();
			this.terrainVertexBuffer.Dispose();
			this.terrainIndexBuffer.Dispose();
			this.terrainVertexDeclaration.Dispose();
			this.Tv = new VertexMultitextured[0];
			this.heightData = new int[0, 0];
			this.normals = new Vector3[0, 0];
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000FE29F File Offset: 0x000FC49F
		public void move(int x, int z)
		{
			this.terrainVertexBuffer.SetData<VertexMultitextured>(0, this.Tv, z * 290, 290, 56);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000FE2C1 File Offset: 0x000FC4C1
		public void moveall()
		{
			this.terrainVertexBuffer.SetData<VertexMultitextured>(this.Tv);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000FE2D4 File Offset: 0x000FC4D4
		public void moveX(int indexer)
		{
			this.terrainVertexBuffer.SetData<VertexMultitextured>(indexer * 14 * 4, this.Tv, indexer, 5, 56);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000FE2F1 File Offset: 0x000FC4F1
		public void moveOne(int indexer)
		{
			this.terrainVertexBuffer.SetData<VertexMultitextured>(indexer * 14 * 4, this.Tv, indexer, 1, 56);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000FE310 File Offset: 0x000FC510
		public void LoadVertices()
		{
			this.LoadHeightData();
			VertexMultitextured[] array = this.SetUpTv();
			this.terrainIndices = this.SetUpTerrainIndices();
			array = this.CalculateNormals(array, this.terrainIndices);
			this.CopyToTerrainBuffers(array, this.terrainIndices);
			this.terrainVertexDeclaration = new VertexBuffer(this.device, typeof(VertexMultitextured), array.Length, BufferUsage.None);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000FE370 File Offset: 0x000FC570
		public void LoadHeightData()
		{
			this.heightmapWidth = (this.Tw - 1) * this.gridScale;
			this.heightData = new int[this.Tw, this.Tw];
			this.heightDatax = new int[this.Tw + 1, this.Tw + 1];
			for (int i = 0; i < this.Tw; i++)
			{
				for (int j = 0; j < this.Tw; j++)
				{
					this.heightDatax[i, j] = 5;
					this.heightData[i, j] = this.heightDatax[i, j];
					if (j == this.Tw - 1)
					{
						this.heightData[i, j] = this.heightDatax[i, 0];
						this.heightDatax[i, j] = this.heightDatax[i, 0];
					}
					if (i == this.Tw - 1)
					{
						this.heightData[i, j] = this.heightDatax[0, j];
						this.heightDatax[i, j] = this.heightDatax[0, j];
					}
				}
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000FE498 File Offset: 0x000FC698
		public VertexMultitextured[] SetUpTv()
		{
			float num = (float)this.gridScale;
			this.heightmapPosition = new Vector3((float)(-(float)((this.Tw - 1) * this.gridScale / 2)), 0f, (float)(-(float)((this.Tw - 1) * this.gridScale / 2)));
			for (int i = 0; i < this.Tw; i++)
			{
				for (int j = 0; j < this.Tw; j++)
				{
					this.Tv[i + j * this.Tw].Position = new Vector3(num * (float)(i - (this.Tw - 1) / 2), (float)this.heightDatax[i, j], num * (float)(j - (this.Tw - 1) / 2));
					this.Tv[i + j * this.Tw].TextureCoordinate.X = (float)i / ((float)(this.Tw - 1) / 10f);
					this.Tv[i + j * this.Tw].TextureCoordinate.Y = (float)j / ((float)(this.Tw - 1) / 10f);
					this.Tv[i + j * this.Tw].TexWeights.X = MathHelper.Clamp(1f - (float)Math.Abs(this.heightDatax[i, j]) / 40f, 0f, 1f);
					this.Tv[i + j * this.Tw].TexWeights.Y = MathHelper.Clamp(1f - (float)Math.Abs(this.heightDatax[i, j] - 50) / 80f, 0f, 1f);
					this.Tv[i + j * this.Tw].TexWeights.Z = MathHelper.Clamp(1f - (float)Math.Abs(this.heightDatax[i, j] - 140) / 120f, 0f, 1f);
					this.Tv[i + j * this.Tw].TexWeights.W = MathHelper.Clamp(1f - (float)Math.Abs(this.heightDatax[i, j] - 410) / 180f, 0f, 1f);
					float num2 = this.Tv[i + j * this.Tw].TexWeights.X;
					num2 += this.Tv[i + j * this.Tw].TexWeights.Y;
					num2 += this.Tv[i + j * this.Tw].TexWeights.Z;
					num2 += this.Tv[i + j * this.Tw].TexWeights.W;
					VertexMultitextured[] tv = this.Tv;
					int num3 = i + j * this.Tw;
					tv[num3].TexWeights.X = tv[num3].TexWeights.X / num2;
					VertexMultitextured[] tv2 = this.Tv;
					int num4 = i + j * this.Tw;
					tv2[num4].TexWeights.Y = tv2[num4].TexWeights.Y / num2;
					VertexMultitextured[] tv3 = this.Tv;
					int num5 = i + j * this.Tw;
					tv3[num5].TexWeights.Z = tv3[num5].TexWeights.Z / num2;
					VertexMultitextured[] tv4 = this.Tv;
					int num6 = i + j * this.Tw;
					tv4[num6].TexWeights.W = tv4[num6].TexWeights.W / num2;
				}
			}
			return this.Tv;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000FE82C File Offset: 0x000FCA2C
		public int[] SetUpTerrainIndices()
		{
			int[] array = new int[(this.Tw - 1) * (this.Tw - 1) * 6];
			int num = 0;
			for (int i = 0; i < this.Tw - 1; i++)
			{
				for (int j = 0; j < this.Tw - 1; j++)
				{
					int num2 = i + j * this.Tw;
					int num3 = i + 1 + j * this.Tw;
					int num4 = i + (j + 1) * this.Tw;
					int num5 = i + 1 + (j + 1) * this.Tw;
					array[num++] = num2;
					array[num++] = num3;
					array[num++] = num4;
					array[num++] = num3;
					array[num++] = num5;
					array[num++] = num4;
				}
			}
			return array;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000FE8F0 File Offset: 0x000FCAF0
		public VertexMultitextured[] CalculateNormals(VertexMultitextured[] vertices, int[] indices)
		{
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].Normal = new Vector3(0f, 0f, 0f);
			}
			int num = 0;
			int num2 = 0;
			int num3 = 1;
			this.normals = new Vector3[this.Tw, this.Tw];
			for (int j = 0; j < indices.Length / 3; j++)
			{
				int num4 = indices[j * 3];
				int num5 = indices[j * 3 + 1];
				int num6 = indices[j * 3 + 2];
				Vector3 vector = vertices[num4].Position - vertices[num6].Position;
				Vector3 vector2 = vertices[num4].Position - vertices[num5].Position;
				Vector3 vector3 = Vector3.Cross(vector, vector2);
				int num7 = num4;
				vertices[num7].Normal = vertices[num7].Normal + vector3;
				int num8 = num5;
				vertices[num8].Normal = vertices[num8].Normal + vector3;
				int num9 = num6;
				vertices[num9].Normal = vertices[num9].Normal + vector3;
				num3++;
				if (num3 > 1)
				{
					this.normals[num, num2] = vector3;
					num3 = 0;
					num2++;
					if (num2 >= this.Tw - 1)
					{
						num2 = 0;
						num++;
					}
				}
			}
			for (int k = 0; k < vertices.Length; k++)
			{
				vertices[k].Normal.Normalize();
			}
			for (int l = 0; l < this.Tw; l++)
			{
				int num10 = l;
				int num11 = this.Tw * this.Tw - this.Tw + l;
				Vector3 vector4 = (vertices[num10].Normal + vertices[num11].Normal) / 2f;
				vertices[num10].Normal = vector4;
				vertices[num11].Normal = vector4;
				num10 = l * this.Tw;
				num11 = l * this.Tw + (this.Tw - 1);
				vector4 = (vertices[num10].Normal + vertices[num11].Normal) / 2f;
				vertices[num10].Normal = vector4;
				vertices[num11].Normal = vector4;
			}
			return vertices;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000FEB5C File Offset: 0x000FCD5C
		public void CopyToTerrainBuffers(VertexMultitextured[] vertices, int[] indices)
		{
			this.terrainVertexBuffer = new VertexBuffer(this.device, typeof(VertexMultitextured), vertices.Length, BufferUsage.WriteOnly);
			this.terrainVertexBuffer.SetData<VertexMultitextured>(vertices);
			this.terrainIndexBuffer = new IndexBuffer(this.device, typeof(int), indices.Length, BufferUsage.WriteOnly);
			this.terrainIndexBuffer.SetData<int>(indices);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000FEBBF File Offset: 0x000FCDBF
		public void updateTerrainBuffers(VertexMultitextured[] vertices)
		{
			this.terrainVertexBuffer.SetData<VertexMultitextured>(vertices);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000FEBD0 File Offset: 0x000FCDD0
		public VertexPositionTexture[] SetUpFullscreenVertices()
		{
			return new VertexPositionTexture[]
			{
				new VertexPositionTexture(new Vector3(-1f, 1f, 0f), new Vector2(0f, 1f)),
				new VertexPositionTexture(new Vector3(1f, 1f, 0f), new Vector2(1f, 1f)),
				new VertexPositionTexture(new Vector3(-1f, -1f, 0f), new Vector2(0f, 0f)),
				new VertexPositionTexture(new Vector3(1f, -1f, 0f), new Vector2(1f, 0f))
			};
		}

		// Token: 0x040011BE RID: 4542
		private GraphicsDevice device;

		// Token: 0x040011BF RID: 4543
		private ContentManager content;

		// Token: 0x040011C0 RID: 4544
		public int Tw;

		// Token: 0x040011C1 RID: 4545
		public int gridScale;

		// Token: 0x040011C2 RID: 4546
		public VertexMultitextured[] Tv;

		// Token: 0x040011C3 RID: 4547
		public int[] terrainIndices;

		// Token: 0x040011C4 RID: 4548
		private int[,] heightDatax;

		// Token: 0x040011C5 RID: 4549
		public VertexBuffer terrainVertexBuffer;

		// Token: 0x040011C6 RID: 4550
		public IndexBuffer terrainIndexBuffer;

		// Token: 0x040011C7 RID: 4551
		public VertexBuffer terrainVertexDeclaration;

		// Token: 0x040011C8 RID: 4552
		public int[,] heightData;

		// Token: 0x040011C9 RID: 4553
		public Vector3[,] normals;

		// Token: 0x040011CA RID: 4554
		public Vector3 heightmapPosition;

		// Token: 0x040011CB RID: 4555
		public int heightmapWidth;

		// Token: 0x040011CC RID: 4556
		private ScreenManager sc;
	}
}
