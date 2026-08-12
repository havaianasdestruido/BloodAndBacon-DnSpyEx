using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000187 RID: 391
	internal class MoonSurface : GameScreen
	{
		// Token: 0x06000E5B RID: 3675 RVA: 0x00403E60 File Offset: 0x00402060
		public void LoadContent(ContentManager content, ScreenManager sc, ref int[,] heightData, ref Vector3[,] normalData, ref int[,] objectData, ref MoonSurface.tex[] texData)
		{
			this.content = content;
			this.sc = sc;
			this.device = sc.GraphicsDevice;
			this.spriteBatch = new SpriteBatch(sc.GraphicsDevice);
			this.displayLegend = new Texture2D(this.device, 500, 500);
			if (sc.planetName[sc.planet] == "Earth")
			{
				this.mount1 = content.Load<Texture2D>("astro\\brushes\\mount4");
				this.valley1 = content.Load<Texture2D>("astro\\brushes\\valley4");
				this.object1 = content.Load<Texture2D>("astro\\brushes\\object4");
				this.brush2 = content.Load<Texture2D>("astro\\brushes\\brush8");
			}
			else if (sc.planetName[sc.planet] == "Mercury")
			{
				this.object1 = content.Load<Texture2D>("astro\\brushes\\object3");
			}
			else if (sc.planetName[sc.planet] == "Venus")
			{
				this.mount1 = content.Load<Texture2D>("astro\\brushes\\mount2");
				this.valley1 = content.Load<Texture2D>("astro\\brushes\\valley2");
				this.object1 = content.Load<Texture2D>("astro\\brushes\\object2");
			}
			else if (sc.planetName[sc.planet] == "Mars")
			{
				this.mount1 = content.Load<Texture2D>("astro\\brushes\\mount5");
				this.valley1 = content.Load<Texture2D>("astro\\brushes\\valley5");
				this.object1 = content.Load<Texture2D>("astro\\brushes\\object5");
			}
			else if (sc.planetName[sc.planet] == "Neptune")
			{
				this.mount1 = content.Load<Texture2D>("astro\\brushes\\mount8");
				this.valley1 = content.Load<Texture2D>("astro\\brushes\\valley8");
				this.object1 = content.Load<Texture2D>("astro\\brushes\\object8");
			}
			else
			{
				this.mount1 = content.Load<Texture2D>("astro\\brushes\\mount1");
				this.valley1 = content.Load<Texture2D>("astro\\brushes\\valley1");
				this.object1 = content.Load<Texture2D>("astro\\brushes\\object1");
			}
			this.high = new List<ushort>();
			this.low = new List<ushort>();
			string currentDirectory = Directory.GetCurrentDirectory();
			using (Stream stream = File.OpenRead(currentDirectory + "\\Content\\astro\\brushes\\earth2.raw"))
			{
				for (;;)
				{
					int num = stream.ReadByte();
					int num2 = stream.ReadByte();
					if (num2 == -1 || num == -1)
					{
						break;
					}
					ushort num3 = (ushort)((num << 8) | num2);
					this.high.Add(num3);
				}
			}
			this.buildTerrain(this.randomField, sc, ref heightData, ref normalData, ref objectData, ref texData);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x004040F4 File Offset: 0x004022F4
		public override void UnloadContent()
		{
			this.content.Unload();
			this.high.Clear();
			this.low.Clear();
			this.brush1.Dispose();
			this.tt.Dispose();
			this.colorArray1 = new Color[0];
			this.colorArray2 = new Color[0];
			this.dataArray = new Color[0];
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0040415C File Offset: 0x0040235C
		public void buildTerrain(int val, ScreenManager sc, ref int[,] heightData, ref Vector3[,] normalData, ref int[,] objectData, ref MoonSurface.tex[] texData)
		{
			this.random = new Random(val);
			Color color = new Color(255, 255, 255);
			Color color2 = new Color(0, 234, 255);
			Color color3 = new Color(255, 0, 0);
			Color color4 = new Color(0, 255, 0);
			this.dataArray = new Color[250000];
			this.object1.GetData<Color>(this.dataArray);
			for (int i = 0; i < 500; i++)
			{
				for (int j = 0; j < 500; j++)
				{
					if (j < 500 && i < 500)
					{
						if (this.dataArray[j + i * 500] == color)
						{
							objectData[j, i] = 27;
						}
						else if (this.dataArray[j + i * 500] == new Color(150, 255, 255))
						{
							objectData[j, i] = 28;
						}
						else if (this.dataArray[j + i * 500] == new Color(50, 50, 50))
						{
							objectData[j, i] = 29;
						}
						else if (this.dataArray[j + i * 500] == new Color(100, 100, 255))
						{
							objectData[j, i] = 30;
						}
						else if (this.dataArray[j + i * 500] == new Color(155, 0, 255))
						{
							objectData[j, i] = 7;
						}
						else if (this.dataArray[j + i * 500] == color2)
						{
							objectData[j, i] = 5;
						}
						else if (this.dataArray[j + i * 500] == color3)
						{
							this.astroLoc.Add(new MoonSurface.astroState(new Vector2((float)((j - 250) * 4 * 150), (float)((i - 250) * 4 * 150)), astroDupe.emotion.safe));
							objectData[j, i] = 0;
						}
						else if (this.dataArray[j + i * 500] == new Color(255, 255, 0))
						{
							this.astroLoc.Add(new MoonSurface.astroState(new Vector2((float)((j - 250) * 4 * 150), (float)((i - 250) * 4 * 150)), astroDupe.emotion.stranded));
							objectData[j, i] = 0;
						}
						else if (this.dataArray[j + i * 500] == new Color(255, 200, 0))
						{
							this.astroLoc.Add(new MoonSurface.astroState(new Vector2((float)((j - 250) * 4 * 150), (float)((i - 250) * 4 * 150)), astroDupe.emotion.stranded2));
							objectData[j, i] = 0;
						}
						else if (this.dataArray[j + i * 500] == new Color(255, 100, 0))
						{
							this.astroLoc.Add(new MoonSurface.astroState(new Vector2((float)((j - 250) * 4 * 150), (float)((i - 250) * 4 * 150)), astroDupe.emotion.underground));
							objectData[j, i] = 0;
						}
						else if (this.dataArray[j + i * 500] == color4)
						{
							this.facility1 = new Vector2((float)((j - 250) * 4 * 150), (float)((i - 250) * 4 * 150));
							this.facilitySmooth = new Vector2((float)(j * 4), (float)(i * 4));
							objectData[j, i] = 0;
						}
						else
						{
							objectData[j, i] = 0;
						}
					}
				}
			}
			this.object1.Dispose();
			if (sc.planetName[sc.planet] == "Mercury")
			{
				this.maketerrain(0, 0, 6, 25, 4, 2, 0, 1, ref heightData, ref normalData, ref texData);
			}
			else if (sc.planetName[sc.planet] == "Venus")
			{
				this.maketerrain(3730, 0, 26, 40, 2, 4, 3, 1, ref heightData, ref normalData, ref texData);
			}
			else if (sc.planetName[sc.planet] == "Earth")
			{
				this.maketerrain(1200, 1, 8, 22, 1, 1, 1, 0, ref heightData, ref normalData, ref texData);
			}
			else if (sc.planetName[sc.planet] == "Mars")
			{
				this.maketerrain(1730, 0, 24, 7, 2, 1, 3, 1, ref heightData, ref normalData, ref texData);
			}
			else if (sc.planetName[sc.planet] == "Neptune")
			{
				this.maketerrain(3730, 0, 12, 12, 1, 1, 1, 1, ref heightData, ref normalData, ref texData);
			}
			else
			{
				this.maketerrain(3000, 2, 10, 25, 2, 6, 3, 1, ref heightData, ref normalData, ref texData);
			}
			this.fixNormals(ref heightData, ref normalData);
			this.facility1hite = (float)heightData[(int)this.facilitySmooth.X, (int)this.facilitySmooth.Y];
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0040475C File Offset: 0x0040295C
		private void coverBrush(Texture2D brush, string rgb, float oddspercent, int gapmin, int gapmax, int rotmin, int rotmax, int scalemin, int scalemax, int depthmin, int depthmax)
		{
			int num = 2000;
			int num2 = brush.Width / 2;
			Color white = Color.White;
			int num3 = num - num2;
			for (int i = num2; i < num; i += this.random.Next(num2 + gapmin, num2 + gapmax))
			{
				this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone);
				for (int j = num2; j < num; j += this.random.Next(num2 + gapmin, num2 + gapmax))
				{
					if ((float)this.random.Next(0, 100) <= oddspercent)
					{
						float num4 = (float)this.random.Next(rotmin, rotmax) / 1000f;
						float num5 = (float)this.random.Next(scalemin, scalemax) / 1000f;
						if (rgb == "red")
						{
							white = new Color((int)((byte)this.random.Next(depthmin, depthmax)), 0, 0, 255);
						}
						if (rgb == "grn")
						{
							white = new Color(0, (int)((byte)this.random.Next(depthmin, depthmax)), 0, 255);
						}
						if (rgb == "blu")
						{
							white = new Color(0, 0, (int)((byte)this.random.Next(depthmin, depthmax)), 255);
						}
						this.spriteBatch.Draw(brush, new Vector2((float)i, (float)j), null, white, num4, new Vector2((float)num2, (float)num2), num5, SpriteEffects.None, 0f);
						int num6 = i;
						int num7 = j;
						if (j > num3)
						{
							num7 = -(num - j);
						}
						if (i > num3)
						{
							num6 = -(num - i);
						}
						if (j > num3 || i > num3)
						{
							this.spriteBatch.Draw(brush, new Vector2((float)num6, (float)num7), null, white, num4, new Vector2((float)num2, (float)num2), num5, SpriteEffects.None, 0f);
						}
						if (j > num3 && i > num3)
						{
							this.spriteBatch.Draw(brush, new Vector2((float)i, (float)num7), null, white, num4, new Vector2((float)num2, (float)num2), num5, SpriteEffects.None, 0f);
							this.spriteBatch.Draw(brush, new Vector2((float)num6, (float)j), null, white, num4, new Vector2((float)num2, (float)num2), num5, SpriteEffects.None, 0f);
						}
					}
				}
				this.spriteBatch.End();
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x004049CC File Offset: 0x00402BCC
		public void maketerrain(int a, int b, int c, int d, int e, int f, int g, int div, ref int[,] heightData, ref Vector3[,] normalData, ref MoonSurface.tex[] texData)
		{
			this.baseHite = a;
			int num = 2000;
			for (int i = 0; i < 2001; i++)
			{
				int num2 = i - 1;
				int num3 = num2 + 1;
				if (num3 > 1999)
				{
					num3 = 0;
				}
				for (int j = 0; j < 2001; j++)
				{
					if (j < 2000 && i < 2000)
					{
						int num4 = (int)Vector2.Distance(new Vector2((float)j, (float)i), this.facilitySmooth);
						if (num4 <= 3)
						{
							num4 = 0;
						}
						if (num4 > 30)
						{
							num4 = 30;
						}
						num4 = 31 - num4;
						if (num4 < 1)
						{
						}
						heightData[j, i] = a;
						heightData[j, i] += (int)(this.high[j + i * num] / 10);
						this.buildtexture(ref heightData, ref texData, j, i);
					}
					if (j > 0 && i > 0)
					{
						int num5 = j - 1;
						int num6 = num5 + 1;
						if (num6 > 1999)
						{
							num6 = 0;
						}
						Vector3 vector = new Vector3((float)(-(float)heightData[num6, num2] + heightData[num5, num2]), 150f, (float)(-(float)heightData[num5, num3] + heightData[num5, num2]));
						normalData[num5, num3] += vector;
						normalData[num5, num2] += vector;
						normalData[num6, num2] += vector;
						normalData[num5, num2].Normalize();
					}
				}
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00404B8C File Offset: 0x00402D8C
		public void buildtexture(ref int[,] heightData, ref MoonSurface.tex[] texData, int xx, int yy)
		{
			int num = heightData[xx, yy] + 2000;
			num = heightData[xx, yy];
			texData[xx + yy * 2000].TexWeights.X = MathHelper.Clamp((float)(1 - Math.Abs(num) / 2000), 0f, 1f);
			texData[xx + yy * 2000].TexWeights.Y = MathHelper.Clamp((float)(1 - Math.Abs(num - 4000) / 3980), 0f, 1f);
			texData[xx + yy * 2000].TexWeights.Z = MathHelper.Clamp((float)(1 - Math.Abs(num - 8000) / 6000), 0f, 1f);
			if (num >= 7000)
			{
				texData[xx + yy * 2000].TexWeights.Z = 1f;
			}
			if (num <= 20)
			{
				texData[xx + yy * 2000].TexWeights.Y = 0.01f;
				texData[xx + yy * 2000].TexWeights.X = 1f;
			}
			float num2 = texData[xx + yy * 2000].TexWeights.X;
			num2 += texData[xx + yy * 2000].TexWeights.Y;
			num2 += texData[xx + yy * 2000].TexWeights.Z;
			MathHelper.Clamp(num2, 1f, 10f);
			float num3 = texData[xx + yy * 2000].TexWeights.X;
			float num4 = texData[xx + yy * 2000].TexWeights.Y;
			float num5 = texData[xx + yy * 2000].TexWeights.Z;
			num3 /= num2;
			num4 /= num2;
			num5 /= num2;
			texData[xx + yy * 2000].TexWeights.X = num3;
			texData[xx + yy * 2000].TexWeights.Y = num4;
			texData[xx + yy * 2000].TexWeights.Z = num5;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00404DFC File Offset: 0x00402FFC
		public void fixNormals(ref int[,] heightData, ref Vector3[,] normalData)
		{
			for (int i = 0; i < 2000; i++)
			{
				Vector3 vector = normalData[i, 0];
				Vector3 vector2 = normalData[i, 1999];
				Vector3 vector3 = Vector3.Normalize((vector + vector2) / 2f);
				normalData[i, 0] = vector3;
				normalData[i, 1999] = vector3;
				vector = normalData[0, i];
				vector2 = normalData[1999, i];
				vector3 = Vector3.Normalize((vector + vector2) / 2f);
				normalData[0, i] = vector3;
				normalData[1999, i] = vector3;
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00404ED8 File Offset: 0x004030D8
		public void buildObjectMap(int myposx, int myposz, ref int[,] data)
		{
			Color[] array = new Color[250000];
			int num = 500;
			int num2 = 0;
			int num3 = 500;
			int num4 = 0;
			int num5 = 500;
			for (int i = num4; i < num5; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					if (data[j, i] == 5)
					{
						array[j + i * num] = new Color(0, 234, 255);
					}
					if (data[j, i] == 27)
					{
						array[j + i * num] = new Color(255, 255, 255);
					}
					if (data[j, i] == 99)
					{
						array[j + i * num] = new Color(255, 0, 0);
					}
					if (myposx / 4 == j && myposz / 4 == i)
					{
						array[j + i * num] = new Color(0, 255, 0);
					}
				}
			}
			try
			{
				this.displayLegend.SetData<Color>(array);
			}
			catch
			{
			}
		}

		// Token: 0x04003A9B RID: 15003
		private ContentManager content;

		// Token: 0x04003A9C RID: 15004
		public int randomField = 2;

		// Token: 0x04003A9D RID: 15005
		private Texture2D tt;

		// Token: 0x04003A9E RID: 15006
		private Color[] colorArray1 = new Color[0];

		// Token: 0x04003A9F RID: 15007
		private Color[] colorArray2 = new Color[0];

		// Token: 0x04003AA0 RID: 15008
		private Color[] dataArray = new Color[0];

		// Token: 0x04003AA1 RID: 15009
		private GraphicsDevice device;

		// Token: 0x04003AA2 RID: 15010
		private PresentationParameters pp;

		// Token: 0x04003AA3 RID: 15011
		private SpriteBatch spriteBatch;

		// Token: 0x04003AA4 RID: 15012
		public int baseHite = 2000;

		// Token: 0x04003AA5 RID: 15013
		public List<MoonSurface.astroState> astroLoc = new List<MoonSurface.astroState>();

		// Token: 0x04003AA6 RID: 15014
		public Vector2 facility1 = Vector2.Zero;

		// Token: 0x04003AA7 RID: 15015
		public float facility1hite;

		// Token: 0x04003AA8 RID: 15016
		public Vector2 facilitySmooth = Vector2.Zero;

		// Token: 0x04003AA9 RID: 15017
		private Random random;

		// Token: 0x04003AAA RID: 15018
		private Texture2D brush1;

		// Token: 0x04003AAB RID: 15019
		private Texture2D brush2;

		// Token: 0x04003AAC RID: 15020
		private Texture2D brush3;

		// Token: 0x04003AAD RID: 15021
		private Texture2D brush9;

		// Token: 0x04003AAE RID: 15022
		private Texture2D mount1;

		// Token: 0x04003AAF RID: 15023
		private Texture2D valley1;

		// Token: 0x04003AB0 RID: 15024
		private Texture2D object1;

		// Token: 0x04003AB1 RID: 15025
		public Texture2D displayLegend;

		// Token: 0x04003AB2 RID: 15026
		private ScreenManager sc;

		// Token: 0x04003AB3 RID: 15027
		public List<ushort> high;

		// Token: 0x04003AB4 RID: 15028
		public List<ushort> low;

		// Token: 0x02000188 RID: 392
		public class astroState
		{
			// Token: 0x06000E64 RID: 3684 RVA: 0x00405082 File Offset: 0x00403282
			public astroState(Vector2 p1, astroDupe.emotion p2)
			{
				this.Loc = p1;
				this.emo = p2;
			}

			// Token: 0x04003AB5 RID: 15029
			public Vector2 Loc = default(Vector2);

			// Token: 0x04003AB6 RID: 15030
			public astroDupe.emotion emo;
		}

		// Token: 0x02000189 RID: 393
		public struct tex
		{
			// Token: 0x04003AB7 RID: 15031
			public Vector3 TexWeights;
		}
	}
}
