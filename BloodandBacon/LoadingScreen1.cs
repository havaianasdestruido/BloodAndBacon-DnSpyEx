using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;

namespace Blood
{
	// Token: 0x02000019 RID: 25
	internal class LoadingScreen1 : GameScreen
	{
		// Token: 0x06000122 RID: 290 RVA: 0x0002C1B0 File Offset: 0x0002A3B0
		private LoadingScreen1(ScreenManager screenManager, bool loadingIsSlow, NetworkSession networker, GameScreen[] screensToLoad)
		{
			this.sc = screenManager;
			this.loadingIsSlow = loadingIsSlow;
			this.screensToLoad = screensToLoad;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.5);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
			this.networkSession = networker;
			GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
			GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
			GamePad.SetVibration(PlayerIndex.Three, 0f, 0f);
			GamePad.SetVibration(PlayerIndex.Four, 0f, 0f);
			if (loadingIsSlow)
			{
				this.waitTime2 = Stopwatch.Frequency * 7L;
				this.startTime = Stopwatch.GetTimestamp();
				this.backgroundThread = new Thread(new ThreadStart(this.BackgroundWorkerThread));
				this.backgroundThreadExit = new ManualResetEvent(false);
				this.rr = new Random();
				if (this.sc.hordemode)
				{
					this.sc.scree.Play(this.sc.ev, (float)this.rr.Next(0, 10) / 100f, 0f);
					this.shake = 120f;
					this.scaryday = true;
					this.day = "666";
					this.shrink = 0.7f;
					return;
				}
				if (this.sc.paintland)
				{
					this.sc.xmas.Play(this.sc.ev, 0f, 0f);
					this.shake = 10000f;
					this.scaryday = false;
					this.day = "PAINTLAND";
					this.shrink = 0.6f;
					return;
				}
				if (this.sc.inSpace)
				{
					this.shake = 100f;
					this.sc.pileDriverSure.Play(this.sc.ev, (float)this.rr.Next(-20, 0) / 100f, 0f);
					this.scaryday = false;
					this.day = "DAY 00";
					this.shrink = 0.7f;
					return;
				}
				this.shake = 150f;
				this.sc.pileDriverSure.Play(this.sc.ev, (float)this.rr.Next(-20, 0) / 100f, 0f);
				this.day = this.sc.currentDay.ToString();
				this.shrink = 1f;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0002C45C File Offset: 0x0002A65C
		public static void Load(ScreenManager screenManager, bool loadingIsSlow, PlayerIndex? controllingPlayer, NetworkSession networker, params GameScreen[] screensToLoad)
		{
			foreach (GameScreen gameScreen in screenManager.GetScreens())
			{
				gameScreen.ExitScreen();
			}
			LoadingScreen1 loadingScreen = new LoadingScreen1(screenManager, loadingIsSlow, networker, screensToLoad);
			screenManager.AddScreen(loadingScreen, controllingPlayer);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0002C49C File Offset: 0x0002A69C
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			LoadingScreen2.done = true;
			this.lastTime = Stopwatch.GetTimestamp();
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
			if (this.otherScreensAreGone)
			{
				if (this.backgroundThread != null)
				{
					this.loadStartTime = gameTime;
					this.backgroundThread.Start();
				}
				this.sc.RemoveScreen(this);
				foreach (GameScreen gameScreen in this.screensToLoad)
				{
					if (gameScreen != null)
					{
						this.sc.AddScreen(gameScreen, base.ControllingPlayer);
					}
				}
				if (this.backgroundThread != null)
				{
					this.backgroundThreadExit.Set();
					this.backgroundThread.Join();
				}
				this.sc.Game.ResetElapsedTime();
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0002C554 File Offset: 0x0002A754
		public override void Draw(GameTime gameTime)
		{
			if (base.ScreenState == ScreenState.Active && this.sc.GetScreens().Length == 1)
			{
				this.otherScreensAreGone = true;
			}
			this.graphicsDevice = this.sc.GraphicsDevice;
			if (this.loadingIsSlow)
			{
				this.sc.isLoading = true;
				this.font = this.sc.grungeFont;
				this.font2 = this.sc.font;
				this.graphicsDevice = this.sc.GraphicsDevice;
				this.sc.contrastBU = 128;
				this.sc.redContrast = 128;
				SpriteBatch spriteBatch = this.sc.SpriteBatch;
				this.graphicsDevice.Clear(Color.Black);
				Vector2 vector = new Vector2((float)this.rr.Next(-100, 100) / this.shake, (float)this.rr.Next(-100, 100) / this.shake);
				if (this.scaryday || this.sc.inSpace)
				{
					this.shake -= 1.4f;
				}
				if (this.shake < 30f)
				{
					this.shake = 25f;
				}
				string text = "DAY " + this.day;
				if (this.sc.paintland)
				{
					text = this.day;
				}
				if (this.sc.inSpace)
				{
					text = "YEAR 3019";
				}
				this.textPosition = new Vector2((float)this.graphicsDevice.Viewport.Width, (float)this.graphicsDevice.Viewport.Height) / 2f;
				float num = (float)this.sc.screenSize.Width / (float)this.sc.origSize.Width;
				Vector2 vector2 = this.font.MeasureString(text) / 2f;
				spriteBatch.Begin();
				if (this.sc.inSpace)
				{
					string text2 = "32.1  EARLY RELEASE BETA  : EXPECT CRASHES";
					spriteBatch.DrawString(this.font2, text2, new Vector2(50f, 50f), new Color(255, 255, 255, 255));
					float num2 = (float)((int)(this.textPosition.X + vector.X * 4f));
					float num3 = (float)((int)(this.textPosition.Y + vector.Y * 4f));
					spriteBatch.DrawString(this.font, text, new Vector2(num2, num3), new Color(90, 90, 90, 210), 0f, vector2, num * this.shrink, SpriteEffects.None, 0f);
					num2 = (float)((int)(this.textPosition.X + vector.X * 7f));
					num3 = (float)((int)(this.textPosition.Y + vector.Y * 7f));
					spriteBatch.DrawString(this.font, text, new Vector2(num2, num3), new Color(60, 60, 60, 255), 0f, vector2, num * this.shrink, SpriteEffects.None, 0f);
				}
				this.textPosition.X = (float)((int)(this.textPosition.X + vector.X));
				this.textPosition.Y = (float)((int)(this.textPosition.Y + vector.Y));
				spriteBatch.DrawString(this.font, text, this.textPosition, new Color(255, 255, 255, 210), 0f, vector2, num * this.shrink, SpriteEffects.None, 0f);
				if (this.sc.tunnelDay.Contains(this.sc.currentDay) && this.sc.showTunnels)
				{
					spriteBatch.DrawString(this.font, "explore the tunnels", new Vector2(0f, vector2.Y + 35f) + this.textPosition, new Color(255, 255, 255, 210), 0f, vector2, 0.2f * num * this.shrink, SpriteEffects.None, 0f);
					Vector2 vector3 = vector2 + new Vector2(-490f, -166f);
					Rectangle rectangle = new Rectangle(2, 1326, 94, 48);
					int num4 = this.sc.tunnelDay.IndexOf(this.sc.currentDay);
					if (num4 == 1 && this.sc.tusk1 == 0)
					{
						rectangle = new Rectangle(108, 1326, 94, 48);
					}
					if (num4 == 2 && this.sc.tusk2 == 0)
					{
						rectangle = new Rectangle(108, 1326, 94, 48);
					}
					if (num4 == 4 && this.sc.tusk3 == 0)
					{
						rectangle = new Rectangle(108, 1326, 94, 48);
					}
					spriteBatch.Draw(this.sc.overlay, new Vector2(this.textPosition.X, this.textPosition.Y), new Rectangle?(rectangle), new Color(255, 255, 255, 210), 0f, vector3, num * this.shrink, SpriteEffects.None, 0f);
				}
				if (this.sc.viewGarbage)
				{
					spriteBatch.DrawString(this.font2, this.sc.loadMoment, new Vector2(150f, 130f), Color.White);
					int num5 = (int)GC.GetTotalMemory(false) / 1024;
					spriteBatch.DrawString(this.font2, num5.ToString(), new Vector2(150f, 100f), Color.White);
				}
				if (this.lastTime - this.startTime > this.waitTime2)
				{
					this.counter += 1f;
					string text3 = "loading";
					int num6 = 0;
					while ((float)num6 < this.counter)
					{
						text3 += " .";
						num6 += 10;
					}
					if (this.counter > 80f)
					{
						this.counter = 0f;
					}
					spriteBatch.DrawString(this.font2, text3, vector + new Vector2(720f - this.font2.MeasureString("loading").X / 2f, (float)this.graphicsDevice.Viewport.Height * 0.85f), new Color(200, 200, 200, 200));
				}
				spriteBatch.End();
				this.sc.blackdayFader = 150f;
				this.sc.introCamera = 600f;
				this.sc.fenceDemo = false;
				this.sc.musicDemo = false;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0002CC30 File Offset: 0x0002AE30
		private void BackgroundWorkerThread()
		{
			this.lastTime = Stopwatch.GetTimestamp();
			while (!this.backgroundThreadExit.WaitOne(30))
			{
				GameTime gameTime = this.GetGameTime(ref this.lastTime);
				this.lastTime = Stopwatch.GetTimestamp();
				this.DrawLoadAnimation(gameTime);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0002CC78 File Offset: 0x0002AE78
		private GameTime GetGameTime(ref long lastTime)
		{
			long timestamp = Stopwatch.GetTimestamp();
			long num = timestamp - lastTime;
			lastTime = timestamp;
			TimeSpan timeSpan = TimeSpan.FromTicks(num * 10000000L / Stopwatch.Frequency);
			return new GameTime(this.loadStartTime.TotalGameTime + timeSpan, timeSpan);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0002CCC0 File Offset: 0x0002AEC0
		private void DrawLoadAnimation(GameTime gameTime)
		{
			if (this.graphicsDevice == null || this.graphicsDevice.IsDisposed)
			{
				return;
			}
			try
			{
				if ((this.sc.isLoading && !this.sc.inSpace) || (this.sc.inSpace && this.sc.loadflag == 0))
				{
					this.Draw(gameTime);
					this.graphicsDevice.Present();
				}
			}
			catch
			{
				this.graphicsDevice = null;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0002CD48 File Offset: 0x0002AF48
		private void UpdateNetworkSession()
		{
			if (this.networkSession == null || this.networkSession.SessionState == NetworkSessionState.Ended)
			{
				return;
			}
			try
			{
				this.networkSession.Update();
			}
			catch
			{
				this.networkSession = null;
			}
		}

		// Token: 0x040005E3 RID: 1507
		private Random rr;

		// Token: 0x040005E4 RID: 1508
		private float counter;

		// Token: 0x040005E5 RID: 1509
		private int tick;

		// Token: 0x040005E6 RID: 1510
		private float shrink = 1f;

		// Token: 0x040005E7 RID: 1511
		private bool loadingIsSlow;

		// Token: 0x040005E8 RID: 1512
		private bool otherScreensAreGone;

		// Token: 0x040005E9 RID: 1513
		private long startTime;

		// Token: 0x040005EA RID: 1514
		private long lastTime;

		// Token: 0x040005EB RID: 1515
		private long waitTime;

		// Token: 0x040005EC RID: 1516
		private long waitTime2;

		// Token: 0x040005ED RID: 1517
		private string day = "1";

		// Token: 0x040005EE RID: 1518
		private float shake = 150f;

		// Token: 0x040005EF RID: 1519
		private GameScreen[] screensToLoad;

		// Token: 0x040005F0 RID: 1520
		private Thread backgroundThread;

		// Token: 0x040005F1 RID: 1521
		private EventWaitHandle backgroundThreadExit;

		// Token: 0x040005F2 RID: 1522
		private GraphicsDevice graphicsDevice;

		// Token: 0x040005F3 RID: 1523
		private NetworkSession networkSession;

		// Token: 0x040005F4 RID: 1524
		private ScreenManager sc;

		// Token: 0x040005F5 RID: 1525
		private GameTime loadStartTime;

		// Token: 0x040005F6 RID: 1526
		private Vector2 textPosition;

		// Token: 0x040005F7 RID: 1527
		private SpriteFont font;

		// Token: 0x040005F8 RID: 1528
		private SpriteFont font2;

		// Token: 0x040005F9 RID: 1529
		private GameTime gg = new GameTime();

		// Token: 0x040005FA RID: 1530
		private bool scaryday;
	}
}
