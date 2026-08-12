using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000022 RID: 34
	internal class GlobeMaker : GameScreen
	{
		// Token: 0x0600015D RID: 349 RVA: 0x000309AC File Offset: 0x0002EBAC
		public void Load(ContentManager contentx, ScreenManager graphics, string body, float diameter)
		{
			this.scale = diameter;
			this.random = new Random();
			this.bodytype = body;
			this.content = contentx;
			this.device = graphics.GraphicsDevice;
			if (this.content == null)
			{
				this.content = new ContentManager(base.ScreenManager.Game.Services, "Content");
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00030A10 File Offset: 0x0002EC10
		public void LoadVertices()
		{
			this.LoadHeightData();
			VertexGlobe[] array = this.SetUpTv();
			int[] array2 = this.SetUpTerrainIndices();
			array = this.CalculateNormals(array, array2);
			this.CopyToTerrainBuffers(array, array2);
			this.terrainVertexDeclaration = new VertexBuffer(this.device, typeof(VertexGlobe), array.Length, BufferUsage.None);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00030A61 File Offset: 0x0002EC61
		public void LoadHeightData()
		{
			this.Tw = this.poly;
			this.heightData = new int[this.poly, this.poly];
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00030A88 File Offset: 0x0002EC88
		public VertexGlobe[] SetUpTv()
		{
			VertexGlobe[] array = new VertexGlobe[this.Tw * this.Tw];
			for (int i = 0; i < this.Tw; i++)
			{
				for (int j = 0; j < this.Tw; j++)
				{
					float num = (float)j;
					if (num == (float)(this.Tw - 1))
					{
						num = 0f;
					}
					this.radius = 205f * this.scale;
					if (this.bodytype == "moon")
					{
						this.radius = (float)(this.heightData[i, j] + 100) * this.scale;
					}
					float num2 = ((float)i - (float)(this.Tw - 1) / 2f) / ((float)this.Tw / (3.1415927f + 3.1415927f / (float)(this.Tw - 1)));
					float num3 = (num - (float)(this.Tw - 1) / 2f) / ((float)this.Tw / (6.2831855f + 6.2831855f / (float)(this.Tw - 1)));
					float num4 = (float)Math.Sin((double)((num2 + this.spec3 + 1f) * this.spec)) / this.spec2 * ((float)Math.Cos((double)((num3 + 2f) * 1f)) + 1f) / 2f + 1f;
					float num5 = (float)Math.Sin((double)((num2 + this.spec3 + 1f) * this.spec)) / this.spec2 * ((float)Math.Sin((double)((num3 + this.spec3) * 1f)) + 1f) / 2f + 1f;
					float num6 = (float)Math.Sin((double)((num2 + this.specA3 - 17f) * this.specA)) / this.specA2 * ((float)(-(float)Math.Cos((double)((num3 + this.spec3) * 1f))) + 1f) / 3f + 1f;
					float num7 = (float)Math.Sin((double)((num2 + this.specA3 - 1f) * this.specA)) / this.specA2 * ((float)Math.Cos((double)((num3 + 0f) * 1f)) + 1f) / 3f + 1f;
					float num8 = 0f;
					float num9 = 0f;
					float num10 = 0f;
					if (this.bodytype == "moon")
					{
						num8 = (float)Math.Sin((double)num3) * ((float)(-(float)Math.Cos((double)num2)) * this.radius) * num4 * num5 * this.s1;
						num9 = (float)Math.Sin((double)num2) * this.radius * this.tall;
						num10 = -(float)Math.Cos((double)num3) * ((float)(-(float)Math.Cos((double)num2)) * this.radius) * num6 * num7 * this.s1;
					}
					if (this.bodytype == "planet")
					{
						num8 = (float)Math.Sin((double)num3) * ((float)(-(float)Math.Cos((double)num2)) * this.radius);
						num9 = (float)Math.Sin((double)num2) * this.radius;
						num10 = -(float)Math.Cos((double)num3) * ((float)(-(float)Math.Cos((double)num2)) * this.radius);
					}
					array[i + j * this.Tw].Position = new Vector3(num8, num9, num10);
					array[i + j * this.Tw].TextureCoordinate.X = (float)j / (float)(this.Tw - 1);
					array[i + j * this.Tw].TextureCoordinate.Y = (float)i / (float)(this.Tw - 1);
					if (this.bodytype == "moon")
					{
						array[i + j * this.Tw].TexWeights.X = 0f;
						array[i + j * this.Tw].TexWeights.Y = this.blend1;
						array[i + j * this.Tw].TexWeights.Z = this.blend2;
						array[i + j * this.Tw].TexWeights.W = this.blend3;
					}
					if (this.bodytype == "planet")
					{
						array[i + j * this.Tw].TexWeights.X = 0f;
						array[i + j * this.Tw].TexWeights.Y = 0f;
						array[i + j * this.Tw].TexWeights.Z = 1f;
						array[i + j * this.Tw].TexWeights.W = 0f;
					}
					float num11 = array[i + j * this.Tw].TexWeights.X;
					num11 += array[i + j * this.Tw].TexWeights.Y;
					num11 += array[i + j * this.Tw].TexWeights.Z;
					num11 += array[i + j * this.Tw].TexWeights.W;
					VertexGlobe[] array2 = array;
					int num12 = i + j * this.Tw;
					array2[num12].TexWeights.X = array2[num12].TexWeights.X / num11;
					VertexGlobe[] array3 = array;
					int num13 = i + j * this.Tw;
					array3[num13].TexWeights.Y = array3[num13].TexWeights.Y / num11;
					VertexGlobe[] array4 = array;
					int num14 = i + j * this.Tw;
					array4[num14].TexWeights.Z = array4[num14].TexWeights.Z / num11;
					VertexGlobe[] array5 = array;
					int num15 = i + j * this.Tw;
					array5[num15].TexWeights.W = array5[num15].TexWeights.W / num11;
				}
			}
			return array;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00031050 File Offset: 0x0002F250
		public int[] SetUpTerrainIndices()
		{
			int[] array = new int[(this.Tw - 1) * (this.Tw - 1) * 6];
			int num = 0;
			for (int i = 0; i < this.Tw - 1; i++)
			{
				for (int j = 0; j < this.Tw - 1; j++)
				{
					int num2 = i + 1;
					int num3 = j + 1;
					int num4 = i + j * this.Tw;
					int num5 = num2 + j * this.Tw;
					int num6 = i + num3 * this.Tw;
					int num7 = num2 + num3 * this.Tw;
					array[num++] = num6;
					array[num++] = num5;
					array[num++] = num4;
					array[num++] = num6;
					array[num++] = num7;
					array[num++] = num5;
				}
			}
			return array;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0003111C File Offset: 0x0002F31C
		public VertexGlobe[] CalculateNormals(VertexGlobe[] vertices, int[] indices)
		{
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].Normal = new Vector3(0f, 0f, 0f);
			}
			for (int j = 0; j < indices.Length / 3; j++)
			{
				int num = indices[j * 3];
				int num2 = indices[j * 3 + 1];
				int num3 = indices[j * 3 + 2];
				Vector3 vector = vertices[num].Position - vertices[num3].Position;
				Vector3 vector2 = vertices[num].Position - vertices[num2].Position;
				Vector3 vector3 = Vector3.Cross(vector, vector2);
				int num4 = num;
				vertices[num4].Normal = vertices[num4].Normal + vector3;
				int num5 = num2;
				vertices[num5].Normal = vertices[num5].Normal + vector3;
				int num6 = num3;
				vertices[num6].Normal = vertices[num6].Normal + vector3;
			}
			for (int k = 0; k < vertices.Length; k++)
			{
				vertices[k].Normal.Normalize();
			}
			for (int l = 0; l < this.Tw; l++)
			{
				int num7 = l;
				int num8 = this.Tw * this.Tw - this.Tw + l;
				Vector3 vector4 = (vertices[num7].Normal + vertices[num8].Normal) / 2f;
				vertices[num7].Normal = vector4;
				vertices[num8].Normal = vector4;
			}
			return vertices;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000312BC File Offset: 0x0002F4BC
		public void CopyToTerrainBuffers(VertexGlobe[] vertices, int[] indices)
		{
			this.terrainVertexBuffer = new VertexBuffer(this.device, typeof(VertexGlobe), vertices.Length, BufferUsage.WriteOnly);
			this.terrainVertexBuffer.SetData<VertexGlobe>(vertices);
			this.terrainIndexBuffer = new IndexBuffer(this.device, typeof(int), indices.Length, BufferUsage.WriteOnly);
			this.terrainIndexBuffer.SetData<int>(indices);
		}

		// Token: 0x040006CE RID: 1742
		private GraphicsDevice device;

		// Token: 0x040006CF RID: 1743
		private ContentManager content;

		// Token: 0x040006D0 RID: 1744
		private string bodytype = "moon";

		// Token: 0x040006D1 RID: 1745
		public int Tw;

		// Token: 0x040006D2 RID: 1746
		public float scale = 40000f;

		// Token: 0x040006D3 RID: 1747
		public float radius;

		// Token: 0x040006D4 RID: 1748
		public VertexBuffer terrainVertexBuffer;

		// Token: 0x040006D5 RID: 1749
		public IndexBuffer terrainIndexBuffer;

		// Token: 0x040006D6 RID: 1750
		public VertexBuffer terrainVertexDeclaration;

		// Token: 0x040006D7 RID: 1751
		public int craterindex = 1;

		// Token: 0x040006D8 RID: 1752
		public int craterindex2 = 1;

		// Token: 0x040006D9 RID: 1753
		public int craterindex3 = 1;

		// Token: 0x040006DA RID: 1754
		public float blend1 = 1f;

		// Token: 0x040006DB RID: 1755
		public float blend2;

		// Token: 0x040006DC RID: 1756
		public float blend3;

		// Token: 0x040006DD RID: 1757
		public int bumpindex = 1;

		// Token: 0x040006DE RID: 1758
		public int bumpit = 1;

		// Token: 0x040006DF RID: 1759
		public int bumpit2 = 1;

		// Token: 0x040006E0 RID: 1760
		public int startx;

		// Token: 0x040006E1 RID: 1761
		public int starty;

		// Token: 0x040006E2 RID: 1762
		public float bumpreduce = 1f;

		// Token: 0x040006E3 RID: 1763
		public int bumpindex2 = 1;

		// Token: 0x040006E4 RID: 1764
		public int startx2;

		// Token: 0x040006E5 RID: 1765
		public int starty2;

		// Token: 0x040006E6 RID: 1766
		public float bumpreduce2 = 1f;

		// Token: 0x040006E7 RID: 1767
		public float spinspeed;

		// Token: 0x040006E8 RID: 1768
		public float axisangle;

		// Token: 0x040006E9 RID: 1769
		public int poly = 80;

		// Token: 0x040006EA RID: 1770
		public float tall = 1f;

		// Token: 0x040006EB RID: 1771
		public float spec = 1f;

		// Token: 0x040006EC RID: 1772
		public float spec2 = 5f;

		// Token: 0x040006ED RID: 1773
		public float spec3 = 5f;

		// Token: 0x040006EE RID: 1774
		public float specA = 1f;

		// Token: 0x040006EF RID: 1775
		public float specA2 = 5f;

		// Token: 0x040006F0 RID: 1776
		public float specA3 = 5f;

		// Token: 0x040006F1 RID: 1777
		public float clamp1;

		// Token: 0x040006F2 RID: 1778
		public float clamp2;

		// Token: 0x040006F3 RID: 1779
		public float s1 = 1f;

		// Token: 0x040006F4 RID: 1780
		public float spintoggle;

		// Token: 0x040006F5 RID: 1781
		public int[,] heightData;

		// Token: 0x040006F6 RID: 1782
		private Random random;
	}
}
