using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000131 RID: 305
	internal class LoadingScreen2 : GameScreen
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x002DEAF8 File Offset: 0x002DCCF8
		private LoadingScreen2(ScreenManager screenManager, bool loadingIsSlow, GameScreen[] screensToLoad)
		{
			this.loadingIsSlow = loadingIsSlow;
			this.screensToLoad = screensToLoad;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.5);
			this.sc = screenManager;
			this.spriteBatch = new SpriteBatch(this.sc.GraphicsDevice);
			this.scalematrix = this.sc.ScaleMatrix1;
			if (loadingIsSlow)
			{
				this.backgroundThread = new Thread(new ThreadStart(this.BackgroundWorkerThread));
				this.backgroundThreadExit = new ManualResetEvent(false);
				this.graphicsDevice = screenManager.GraphicsDevice;
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x002DEDFC File Offset: 0x002DCFFC
		public static void Load(ScreenManager screenManager, bool loadingIsSlow, PlayerIndex? controllingPlayer, params GameScreen[] screensToLoad)
		{
			foreach (GameScreen gameScreen in screenManager.GetScreens())
			{
				gameScreen.ExitScreen();
			}
			LoadingScreen2 loadingScreen = new LoadingScreen2(screenManager, loadingIsSlow, screensToLoad);
			LoadingScreen2.done = false;
			screenManager.AddScreen(loadingScreen, controllingPlayer);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x002DEE40 File Offset: 0x002DD040
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
			this.mytime++;
			if (this.otherScreensAreGone)
			{
				if (this.backgroundThread != null)
				{
					this.loadStartTime = gameTime;
					this.backgroundThread.Start();
				}
				base.ScreenManager.RemoveScreen(this);
				foreach (GameScreen gameScreen in this.screensToLoad)
				{
					if (gameScreen != null)
					{
						base.ScreenManager.AddScreen(gameScreen, base.ControllingPlayer);
					}
				}
				if (this.backgroundThread != null)
				{
					this.backgroundThreadExit.Set();
					this.backgroundThread.Join();
				}
				base.ScreenManager.Game.ResetElapsedTime();
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x002DEEF4 File Offset: 0x002DD0F4
		private void allcolors()
		{
			if (base.ScreenManager.loadScreen.Count == 0)
			{
				for (int i = 1; i < this.messages.Length + 1; i++)
				{
					base.ScreenManager.loadScreen.Add(i);
				}
			}
			int num = this.random.Next(0, base.ScreenManager.loadScreen.Count - 1);
			this.loadIndex = base.ScreenManager.loadScreen[num];
			base.ScreenManager.loadScreen.RemoveAt(num);
			int num2 = this.random.Next(0, 5);
			this.mess = this.header[num2] + this.loadx[base.ScreenManager.bgindex - 1];
			this.myimage = this.content.Load<Texture2D>("astro\\loading\\image0");
			this.codeBG = base.ScreenManager.codeBG;
			this.colorArray1 = new Color[1024];
			this.myimage.GetData<Color>(this.colorArray1);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x002DF000 File Offset: 0x002DD200
		public override void Draw(GameTime gameTime)
		{
			if (base.ScreenState == ScreenState.Active && base.ScreenManager.GetScreens().Length == 1)
			{
				this.otherScreensAreGone = true;
			}
			if (this.loadonce == 0)
			{
				if (this.content == null)
				{
					this.content = new ContentManager(base.ScreenManager.Game.Services, "Content");
				}
				this.random = new Random();
				int num = this.random.Next(1, 13);
				this.horPos = this.random.Next(0, 300);
				this.blue = this.random.Next(120, 200);
				this.sizer = (float)this.random.Next(1200, 1600) / 1000f;
				this.button = this.sc.buttonGong;
				this.gear2 = this.sc.gear2;
				this.loadfont1 = this.sc.loadfont1;
				this.loadBG = this.sc.loadBG;
				this.buttonOn = this.content.Load<Texture2D>("astro//sprites\\icons\\buttonon" + num);
				this.loadonce = 1;
			}
			if (this.loadingIsSlow)
			{
				if (!this.loaded)
				{
					this.loaded = true;
					this.allcolors();
					this.messagedisplay = this.messages[this.loadIndex - 1].ToLower();
					int num2 = 0;
					int num3 = 1;
					for (int i = 0; i < this.colorArray1.Length; i++)
					{
						num2++;
						if (num2 > 32)
						{
							num2 = 1;
							num3++;
						}
						int num4 = (int)Convert.ToInt16(this.colorArray1[i].R);
						int num5 = (int)Convert.ToInt16(this.colorArray1[i].G);
						int num6 = (int)Convert.ToInt16(this.colorArray1[i].B);
						if (num6 + num5 + num4 > 20)
						{
							this.colortab.Add(this.colorArray1[i]);
							this.posxtab.Add(num2);
							this.posytab.Add(num3);
						}
					}
				}
				if (base.ScreenManager.loadflag != 2)
				{
					this.rot += 0.04f;
					if ((double)this.rot > 0.15)
					{
						this.rot = 0f;
						this.realrot += 0.1f;
						this.pulse--;
						if (this.pulse <= 0)
						{
							this.pulse = 10;
						}
						if (this.colortab.Count > 0)
						{
							int num7 = this.random.Next(0, this.colortab.Count);
							int num8 = this.posxtab[num7] - 16;
							int num9 = this.posytab[num7] - 16;
							this.locX.Add((float)(num8 * 12 + 640) + (float)this.random.Next(-1000, 1000) / 1000f);
							this.locY.Add((float)(num9 * 12 + 330) + (float)this.random.Next(-1000, 1000) / 1000f);
							this.col.Add(this.colortab[num7]);
							this.colortab.RemoveAt(num7);
							this.posxtab.RemoveAt(num7);
							this.posytab.RemoveAt(num7);
							if (this.colortab.Count > 0)
							{
								num7 = this.random.Next(0, this.colortab.Count);
								num8 = this.posxtab[num7] - 16;
								num9 = this.posytab[num7] - 16;
								this.locX.Add((float)(num8 * 12 + 640) + (float)this.random.Next(-1000, 1000) / 1000f);
								this.locY.Add((float)(num9 * 12 + 330) + (float)this.random.Next(-1000, 1000) / 1000f);
								this.col.Add(this.colortab[num7]);
								this.colortab.RemoveAt(num7);
								this.posxtab.RemoveAt(num7);
								this.posytab.RemoveAt(num7);
							}
							if (this.colortab.Count > 0)
							{
								num7 = this.random.Next(0, this.colortab.Count);
								num8 = this.posxtab[num7] - 16;
								num9 = this.posytab[num7] - 16;
								this.locX.Add((float)(num8 * 12 + 640) + (float)this.random.Next(-1000, 1000) / 1000f);
								this.locY.Add((float)(num9 * 12 + 330) + (float)this.random.Next(-1000, 1000) / 1000f);
								this.col.Add(this.colortab[num7]);
								this.colortab.RemoveAt(num7);
								this.posxtab.RemoveAt(num7);
								this.posytab.RemoveAt(num7);
							}
							if (this.colortab.Count > 0)
							{
								num7 = this.random.Next(0, this.colortab.Count);
								num8 = this.posxtab[num7] - 16;
								num9 = this.posytab[num7] - 16;
								this.locX.Add((float)(num8 * 12 + 640) + (float)this.random.Next(-1000, 1000) / 1000f);
								this.locY.Add((float)(num9 * 12 + 330) + (float)this.random.Next(-1000, 1000) / 1000f);
								this.col.Add(this.colortab[num7]);
								this.colortab.RemoveAt(num7);
								this.posxtab.RemoveAt(num7);
								this.posytab.RemoveAt(num7);
							}
							if (this.colortab.Count > 0)
							{
								num7 = this.random.Next(0, this.colortab.Count);
								num8 = this.posxtab[num7] - 16;
								num9 = this.posytab[num7] - 16;
								this.locX.Add((float)(num8 * 12 + 640) + (float)this.random.Next(-1000, 1000) / 1000f);
								this.locY.Add((float)(num9 * 12 + 330) + (float)this.random.Next(-1000, 1000) / 1000f);
								this.col.Add(this.colortab[num7]);
								this.colortab.RemoveAt(num7);
								this.posxtab.RemoveAt(num7);
								this.posytab.RemoveAt(num7);
							}
							this.button.Play(0.3f * base.ScreenManager.ev, (float)this.random.Next(-10000, 10000) / 10000f, 0f);
						}
					}
					this.mytime++;
					if (this.colortab.Count <= 0 && this.mytime > 70)
					{
						this.countdown--;
						if (this.countdown <= 0)
						{
							LoadingScreen2.done = true;
						}
					}
					this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, this.scalematrix);
					this.spriteBatch.Draw(this.codeBG, new Rectangle(this.horPos + 50, (int)(this.realrot * 20f - 50f), (int)(this.sizer * 720f), (int)(this.sizer * 720f)), new Color(120, 120, this.blue, 80));
					this.spriteBatch.Draw(this.loadBG, new Rectangle(0, 0, 1280, 720), Color.White);
					for (int j = 0; j < this.locX.Count; j++)
					{
						this.spriteBatch.Draw(this.buttonOn, new Vector2(this.locX[j], this.locY[j]), this.col[j]);
					}
					this.spriteBatch.Draw(this.gear2, new Vector2(160f, 60f), null, Color.White, this.realrot, new Vector2((float)(this.gear2.Width / 2), (float)(this.gear2.Height / 2)), 1f, SpriteEffects.None, 0f);
					this.spriteBatch.Draw(this.gear2, new Vector2(192f, 92f), null, Color.White, -this.realrot + 0.01f, new Vector2((float)(this.gear2.Width / 2), (float)(this.gear2.Height / 2)), 0.66f, SpriteEffects.None, 0f);
					this.spriteBatch.DrawString(this.loadfont1, this.mess, new Vector2(235f, 45f), Color.White);
					this.spriteBatch.DrawString(this.loadfont1, this.messagedisplay, new Vector2(640f - this.loadfont1.MeasureString(this.messagedisplay).X / 2f, 600f), new Color(200, 200, 230));
					this.spriteBatch.End();
				}
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x002DFA34 File Offset: 0x002DDC34
		private void BackgroundWorkerThread()
		{
			long timestamp = Stopwatch.GetTimestamp();
			while (!this.backgroundThreadExit.WaitOne(160))
			{
				GameTime gameTime = this.GetGameTime(ref timestamp);
				if (base.ScreenManager.loadflag < 2)
				{
					this.rot += 0.15f;
					this.DrawLoadAnimation(gameTime);
				}
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x002DFA8C File Offset: 0x002DDC8C
		private GameTime GetGameTime(ref long lastTime)
		{
			long timestamp = Stopwatch.GetTimestamp();
			long num = timestamp - lastTime;
			lastTime = timestamp;
			TimeSpan timeSpan = TimeSpan.FromTicks(num * 10000000L / Stopwatch.Frequency);
			return new GameTime(this.loadStartTime.TotalGameTime + timeSpan, timeSpan);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x002DFAD4 File Offset: 0x002DDCD4
		private void DrawLoadAnimation(GameTime gameTime)
		{
			if (this.graphicsDevice == null || this.graphicsDevice.IsDisposed)
			{
				return;
			}
			try
			{
				this.graphicsDevice.Clear(Color.Black);
				this.Draw(gameTime);
				this.graphicsDevice.Present();
			}
			catch
			{
				this.graphicsDevice = null;
			}
		}

		// Token: 0x04002D64 RID: 11620
		public static bool done;

		// Token: 0x04002D65 RID: 11621
		private int mytime;

		// Token: 0x04002D66 RID: 11622
		private int countdown = 10;

		// Token: 0x04002D67 RID: 11623
		private bool loadingIsSlow;

		// Token: 0x04002D68 RID: 11624
		private bool otherScreensAreGone;

		// Token: 0x04002D69 RID: 11625
		private GameScreen[] screensToLoad;

		// Token: 0x04002D6A RID: 11626
		private Thread backgroundThread;

		// Token: 0x04002D6B RID: 11627
		private EventWaitHandle backgroundThreadExit;

		// Token: 0x04002D6C RID: 11628
		private GraphicsDevice graphicsDevice;

		// Token: 0x04002D6D RID: 11629
		private GameTime loadStartTime;

		// Token: 0x04002D6E RID: 11630
		private ContentManager content;

		// Token: 0x04002D6F RID: 11631
		private Random random;

		// Token: 0x04002D70 RID: 11632
		private Texture2D gear2;

		// Token: 0x04002D71 RID: 11633
		private Texture2D buttonOn;

		// Token: 0x04002D72 RID: 11634
		private Texture2D loadBG;

		// Token: 0x04002D73 RID: 11635
		private Texture2D rays;

		// Token: 0x04002D74 RID: 11636
		private bool loaded;

		// Token: 0x04002D75 RID: 11637
		private float rot;

		// Token: 0x04002D76 RID: 11638
		private float realrot;

		// Token: 0x04002D77 RID: 11639
		private SpriteBatch spriteBatch;

		// Token: 0x04002D78 RID: 11640
		private SpriteFont loadfont1;

		// Token: 0x04002D79 RID: 11641
		private SpriteFont loadfont2;

		// Token: 0x04002D7A RID: 11642
		private List<Color> col = new List<Color>();

		// Token: 0x04002D7B RID: 11643
		private List<float> locX = new List<float>();

		// Token: 0x04002D7C RID: 11644
		private List<float> locY = new List<float>();

		// Token: 0x04002D7D RID: 11645
		private int horPos;

		// Token: 0x04002D7E RID: 11646
		private float sizer = 1f;

		// Token: 0x04002D7F RID: 11647
		private int blue = 200;

		// Token: 0x04002D80 RID: 11648
		private string[] image0 = new string[0];

		// Token: 0x04002D81 RID: 11649
		private Texture2D myimage;

		// Token: 0x04002D82 RID: 11650
		private Texture2D codeBG;

		// Token: 0x04002D83 RID: 11651
		private Color[] colorArray1 = new Color[0];

		// Token: 0x04002D84 RID: 11652
		private string messagedisplay = "";

		// Token: 0x04002D85 RID: 11653
		private string[] messages = new string[]
		{
			"call the dropship", "activate the monolith", "upgraded solar cell", "missions increase rep", "mine crystals", "stay alert", "mine crystals", "don't monkey around", "refine ore for gems", "refine ore for gems",
			"Go Get Promoted", "gold Is worthless", "repair from the lander", "this is rocket science", "Know Your Friends", "the L257 lander", "launch another rocket", "listen to your radio", "refine shale into fuel", "red stones for fuel",
			"return home", "upgrades cost money", "remember to recharge", "the blue dot", "save your progress", "increase your ranking", "rocks contain crystals", "bag oh money", "save yourgame", "watching you",
			"recordings of the dead", "signs of life", "remember to refuel", "are you human"
		};

		// Token: 0x04002D86 RID: 11654
		private string[] loadx = new string[] { "moon of mercury", "moon of venus", "earth's moon", "moon of mars", "moon of saturn", "moon of jupiter", "moon of neptune", "moon of uranus", "moon of pluto" };

		// Token: 0x04002D87 RID: 11655
		private string[] header = new string[] { "destination : ", "orbiting : ", "approaching : ", "nearing : ", "navigating : " };

		// Token: 0x04002D88 RID: 11656
		private string mess = "";

		// Token: 0x04002D89 RID: 11657
		private int loadspot;

		// Token: 0x04002D8A RID: 11658
		private List<Color> colortab = new List<Color>();

		// Token: 0x04002D8B RID: 11659
		private List<int> posxtab = new List<int>();

		// Token: 0x04002D8C RID: 11660
		private List<int> posytab = new List<int>();

		// Token: 0x04002D8D RID: 11661
		private int pulse = 10;

		// Token: 0x04002D8E RID: 11662
		private int loadIndex = 1;

		// Token: 0x04002D8F RID: 11663
		private int loadonce;

		// Token: 0x04002D90 RID: 11664
		private SoundEffect button;

		// Token: 0x04002D91 RID: 11665
		private ScreenManager sc;

		// Token: 0x04002D92 RID: 11666
		private Matrix scalematrix = Matrix.Identity;
	}
}
