using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000051 RID: 81
	public class buildStars : GameScreen
	{
		// Token: 0x06000326 RID: 806 RVA: 0x000CF038 File Offset: 0x000CD238
		public void LoadContent(ScreenManager sc, ContentManager content, GraphicsDevice gr, int max, int odds, bool addplanet, int planet)
		{
			this.content = content;
			this.starSheet = content.Load<Texture2D>("astro\\sprites\\stars\\starSheet");
			this.random = new Random();
			PresentationParameters presentationParameters = gr.PresentationParameters;
			this.starBatch = new SpriteBatch(gr);
			int num = max / 2;
			if (addplanet)
			{
				num = max / 4;
			}
			this.colorArray1 = new Color[num * num];
			RenderTarget2D renderTarget2D = new RenderTarget2D(gr, num, num, false, SurfaceFormat.Color, DepthFormat.None);
			this.manmadestars = new Texture2D(gr, max, max);
			int num2 = this.random.Next(1, 3);
			num2 = 1;
			string[] array = new string[]
			{
				"miniMercury2", "miniVenus2", "miniEarth2", "miniMars2", "miniJupiter2", "miniSaturn2", "miniUranus2", "miniNeptune2", "miniPluto2", "miniMercury",
				"miniVenus", "miniEarth", "miniMars", "miniJupiter", "miniSaturn", "miniUranus", "miniNeptune", "miniPluto"
			};
			float num3 = (float)this.random.Next(100, 270) / 100f;
			num3 = 1f;
			this.miniSun = content.Load<Texture2D>("astro\\sprites\\planets\\sun");
			for (int i = 0; i < max; i += num)
			{
				for (int j = 0; j < max; j += num)
				{
					gr.SetRenderTarget(renderTarget2D);
					gr.Clear(Color.Black);
					this.makestars(num, odds, addplanet);
					if (addplanet)
					{
						if (j == 600 && i == 300 && num2 == 1)
						{
							string text = array[planet];
							this.miniPlanet = content.Load<Texture2D>("astro\\sprites\\planets\\" + text);
							this.starBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
							this.starBatch.Draw(this.miniPlanet, new Rectangle((int)((300f - 240f * num3) / 2f), (int)((300f - 96f * num3) / 2f), (int)(240f * num3), (int)(96f * num3)), Color.White);
							this.starBatch.End();
						}
						if (j == 300 && i == 300 && num2 == 2)
						{
							string text2 = array[planet + 9];
							this.miniPlanet = content.Load<Texture2D>("astro\\sprites\\planets\\" + text2);
							this.starBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
							this.starBatch.Draw(this.miniPlanet, new Rectangle((int)((300f - 240f * num3) / 2f), (int)((300f - 96f * num3) / 2f), (int)(240f * num3), (int)(96f * num3)), Color.White);
							this.starBatch.End();
						}
						if (j == 300 && i == 600)
						{
							this.starBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
							this.starBatch.Draw(this.miniSun, new Rectangle(100, 100, 150, 150), Color.White);
							this.starBatch.End();
						}
					}
					gr.SetRenderTarget(null);
					this.tt = renderTarget2D;
					this.tt.GetData<Color>(this.colorArray1);
					this.manmadestars.SetData<Color>(0, new Rectangle?(new Rectangle(j, i, num, num)), this.colorArray1, 0, this.colorArray1.Length);
				}
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000CF403 File Offset: 0x000CD603
		public new void UnloadContent()
		{
			this.manmadestars.Dispose();
			this.starBatch.Dispose();
			this.starSheet.Dispose();
			this.content.Unload();
			this.content.Dispose();
			this.content = null;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000CF444 File Offset: 0x000CD644
		public void makestars(int min, int odds, bool addplanet)
		{
			int num = 1;
			int[] array = new int[]
			{
				0,
				0,
				4 * num,
				0,
				8 * num,
				0,
				12 * num,
				0,
				16 * num,
				0,
				0,
				4 * num,
				4 * num,
				4 * num,
				8 * num,
				4 * num,
				12 * num,
				4 * num
			};
			int[] array2 = new int[]
			{
				0,
				8 * num,
				8 * num,
				8 * num,
				16 * num,
				8 * num,
				24 * num,
				8 * num,
				32 * num,
				8 * num,
				40 * num,
				8 * num,
				48 * num,
				8 * num,
				56 * num,
				8 * num,
				32 * num,
				8 * num
			};
			int[] array3 = new int[]
			{
				0,
				16 * num,
				12 * num,
				16 * num,
				24 * num,
				16 * num,
				36 * num,
				16 * num,
				0,
				28 * num,
				12 * num,
				28 * num,
				24 * num,
				28 * num,
				36 * num,
				28 * num
			};
			int[] array4 = new int[]
			{
				0,
				40 * num,
				20 * num,
				40 * num,
				40 * num,
				40 * num,
				44 * num,
				16 * num
			};
			int num2 = 6;
			this.starBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
			for (int i = 0; i <= min; i++)
			{
				for (int j = 0; j <= min; j++)
				{
					int num3 = this.random.Next(1, odds);
					if (num3 < 32)
					{
						num3 = this.random.Next(1, 1000);
						int num4 = 0;
						int num5 = this.random.Next(0, 9);
						int num6 = array[num5 * 2];
						int num7 = array[num5 * 2 + 1];
						int num8 = 4;
						int num9 = 4;
						if (num3 > 45 && num3 < 115)
						{
							num6 = array2[num5 * 2];
							num7 = array2[num5 * 2 + 1];
							num8 = 8;
							num9 = 8;
						}
						if (num3 < 10)
						{
							num5 = this.random.Next(0, 8);
							num6 = array3[num5 * 2];
							num7 = array3[num5 * 2 + 1];
							num8 = 12;
							num9 = 12;
						}
						num5 = this.random.Next(0, 4);
						if (num3 > 165 && num3 < 170 && num2 > 0)
						{
							num4 = 1;
							num6 = array4[num5 * 2];
							num7 = array4[num5 * 2 + 1];
							num9 = 20;
							num8 = 20;
							num2--;
						}
						if (num3 > 790)
						{
							num6 = 24;
							num7 = 0;
							num8 = 8;
							num9 = 8;
						}
						if (num3 > 860)
						{
							num6 = 32;
							num7 = 0;
							num8 = 8;
							num9 = 8;
						}
						if (num3 > 930)
						{
							num6 = 40;
							num7 = 0;
							num8 = 8;
							num9 = 8;
						}
						float num10 = (float)this.random.Next(0, 760000) / 1000f;
						float num11 = (float)this.random.Next(4000, 10000) / 10000f;
						byte b = (byte)this.random.Next(200, 255);
						if (addplanet)
						{
							num11 = (float)this.random.Next(15000, 19000) / 10000f;
						}
						Color color = new Color((int)b, (int)b, (int)b, 255);
						if (num4 == 1)
						{
							b = (byte)this.random.Next(210, 255);
							color = new Color((int)b, (int)b, (int)b, 255);
							if (this.random.Next(1, 5000) < 1200)
							{
								color = new Color((int)((byte)this.random.Next(140, 240)), (int)((byte)this.random.Next(140, 240)), (int)((byte)this.random.Next(140, 240)), 255);
							}
							num11 = (float)this.random.Next(8000, 15000) / 10000f;
						}
						num8 *= num;
						num9 *= num;
						if ((float)j + (float)num8 * num11 / 2f <= (float)min && (float)j - (float)num8 * num11 / 2f >= 0f && (float)i + (float)num9 * num11 / 2f <= (float)min && (float)i - (float)num9 * num11 / 2f >= 0f)
						{
							this.starBatch.Draw(this.starSheet, new Vector2((float)j, (float)i), new Rectangle?(new Rectangle(num6, num7, num8, num9)), color, num10, new Vector2((float)(num6 + num8 / 2), (float)(num7 + num9 / 2)), num11, SpriteEffects.None, 0f);
						}
					}
				}
			}
			this.starBatch.End();
		}

		// Token: 0x04000D6A RID: 3434
		private SpriteBatch starBatch;

		// Token: 0x04000D6B RID: 3435
		public Texture2D starSheet;

		// Token: 0x04000D6C RID: 3436
		public Texture2D tt;

		// Token: 0x04000D6D RID: 3437
		public Texture2D manmadestars;

		// Token: 0x04000D6E RID: 3438
		public Color[] colorArray1 = new Color[0];

		// Token: 0x04000D6F RID: 3439
		private Texture2D miniPlanet;

		// Token: 0x04000D70 RID: 3440
		private Texture2D miniSun;

		// Token: 0x04000D71 RID: 3441
		private Random random;

		// Token: 0x04000D72 RID: 3442
		private ContentManager content;
	}
}
