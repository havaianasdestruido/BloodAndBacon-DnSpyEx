using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Blood
{
	// Token: 0x02000164 RID: 356
	internal class MediaScreen : GameScreen
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000CD7 RID: 3287 RVA: 0x0037721C File Offset: 0x0037541C
		// (remove) Token: 0x06000CD8 RID: 3288 RVA: 0x00377254 File Offset: 0x00375454
		public event EventHandler<PlayerIndexEventArgs> Accepted;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000CD9 RID: 3289 RVA: 0x0037728C File Offset: 0x0037548C
		// (remove) Token: 0x06000CDA RID: 3290 RVA: 0x003772C4 File Offset: 0x003754C4
		public event EventHandler<PlayerIndexEventArgs> Approved;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000CDB RID: 3291 RVA: 0x003772FC File Offset: 0x003754FC
		// (remove) Token: 0x06000CDC RID: 3292 RVA: 0x00377334 File Offset: 0x00375534
		public event EventHandler<PlayerIndexEventArgs> Launch;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000CDD RID: 3293 RVA: 0x0037736C File Offset: 0x0037556C
		// (remove) Token: 0x06000CDE RID: 3294 RVA: 0x003773A4 File Offset: 0x003755A4
		public event EventHandler<PlayerIndexEventArgs> Failed;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000CDF RID: 3295 RVA: 0x003773DC File Offset: 0x003755DC
		// (remove) Token: 0x06000CE0 RID: 3296 RVA: 0x00377414 File Offset: 0x00375614
		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000CE1 RID: 3297 RVA: 0x0037744C File Offset: 0x0037564C
		// (remove) Token: 0x06000CE2 RID: 3298 RVA: 0x00377484 File Offset: 0x00375684
		public event EventHandler<PlayerIndexEventArgs> downloadCancel;

		// Token: 0x06000CE3 RID: 3299 RVA: 0x003774BC File Offset: 0x003756BC
		public MediaScreen(int flag)
		{
			this.password = "nada";
			this.flag = flag;
			this.message = "nuoll";
			this.longmessage = "";
			base.IsPopup = true;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x003776C0 File Offset: 0x003758C0
		public override void LoadContent()
		{
			ContentManager content = base.ScreenManager.Game.Content;
			this.sc = base.ScreenManager;
			this.sc.forcedout = false;
			this.rr = new Random();
			this.content2 = new ContentManager(base.ScreenManager.Game.Services, "ABCDE1");
			if (this.flag == 0)
			{
				this.trailerVid = this.content2.Load<Video>("trailer");
			}
			if (this.flag == 1)
			{
				this.trailerVid = this.content2.Load<Video>("creditNEW");
			}
			this.myPlayer = new VideoPlayer();
			this.myPlayer.IsLooped = true;
			this.myPlayer.Volume = this.sc.ev;
			this.myPlayer.Stop();
			this.myPlayer.Play(this.trailerVid);
			this.font2 = this.sc.lilyFont;
			this.font = this.sc.terminal;
			this.squarefont = this.sc.squarefont;
			if (this.useArial)
			{
				this.font = this.sc.squarefont;
			}
			this.menuSlate = new Rectangle(114, 12, 480, 244);
			this.menuSlate = new Rectangle(76, 0, 520, 267);
			this.cornerR = new Rectangle(16, 13, 88, 88);
			this.borderR = new Rectangle(9, 457, 100, 64);
			this.spriteBatch = base.ScreenManager.SpriteBatch;
			this.viewport = new Viewport(0, 0, 1280, 720);
			this.viewportSize = new Vector2((float)this.viewport.Width, (float)this.viewport.Height);
			this.textSize = this.font.MeasureString(this.message);
			this.hPad = 40;
			this.vPad = 40;
			this.textPosition = this.viewportSize / 2f - this.textSize / 2f;
			this.mywidth = (float)((int)this.textSize.X + this.hPad * 2);
			this.myhite = (float)((int)this.textSize.Y + this.vPad * 2);
			if (this.sc.introCamera <= 0f)
			{
				this.textPosition.X = this.viewportSize.X / 2f;
				this.left = (float)((int)this.textPosition.X - this.hPad) - this.textSize.X / 2f;
				this.top = (float)((int)this.textPosition.Y - this.vPad);
			}
			else
			{
				this.textPosition.Y = this.textPosition.Y + (float)this.viewport.Y;
				this.textPosition.X = this.viewportSize.X / 2f + (float)this.viewport.X;
				this.left = (float)((int)this.textPosition.X - this.hPad) - this.textSize.X / 2f;
				this.top = (float)((int)this.textPosition.Y - this.vPad);
			}
			this.right = this.left + this.mywidth;
			this.bottom = this.top + this.myhite;
			this.backgroundRectangle = new Rectangle((int)this.left - 10, (int)this.top - 10, (int)this.mywidth + 20, (int)this.myhite + 20);
			int num = 60;
			this.cornA = new Rectangle((int)this.left, (int)this.top, num, num);
			this.cornB = new Rectangle((int)this.right, (int)this.top, num, num);
			this.cornC = new Rectangle((int)this.left, (int)this.bottom, num, num);
			this.cornD = new Rectangle((int)this.right, (int)this.bottom, num, num);
			this.mouseState = Mouse.GetState();
			this.sc.mymouse.X = (float)this.mouseState.X;
			this.sc.mymouse.Y = (float)this.mouseState.Y;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00377B34 File Offset: 0x00375D34
		public override void HandleInput(InputState input)
		{
			if (base.ControllingPlayer != null)
			{
				int value = (int)base.ControllingPlayer.Value;
				this.gamestate = input.CurrentGamePadStates[value];
				this.prevstate = input.LastGamePadStates[value];
			}
			this.prevKeys = input.lastKeyState;
			this.keyState = input.currentKeyState;
			if ((int)this.sc.myTimer % 120 == 0)
			{
				this.sc.centerWindow();
			}
			this.prevMouse = this.mouseState;
			this.mouseState = Mouse.GetState();
			this.mm.X = (float)this.mouseState.X;
			this.mm.Y = (float)this.mouseState.Y;
			if (this.mouseState.X == (int)this.sc.mymouse.X && this.mouseState.Y == (int)this.sc.mymouse.Y)
			{
				this.sc.mousefade--;
				if (this.sc.mousefade == 0)
				{
					this.sc.Game.IsMouseVisible = false;
				}
			}
			else
			{
				this.sc.mousefade = 210;
				this.sc.Game.IsMouseVisible = true;
			}
			this.sc.mymouse.X = (float)this.mouseState.X;
			this.sc.mymouse.Y = (float)this.mouseState.Y;
			bool flag = this.mouseState.RightButton == ButtonState.Pressed && this.prevMouse.RightButton == ButtonState.Released;
			bool flag2 = this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released;
			if (this.delayinput)
			{
				this.prevMouse = this.mouseState;
				this.prevKeys = this.keyState;
				this.prevstate = this.gamestate;
			}
			if (this.flag == 0 || this.flag == 1)
			{
				if (flag2 || flag || input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					try
					{
						this.myPlayer.Dispose();
					}
					catch (Exception ex)
					{
						throw new Exception("MoviePlayer Stop() Failed! : " + ex.Message, ex);
					}
					base.ExitScreen();
				}
				else if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					try
					{
						this.myPlayer.Dispose();
					}
					catch (Exception ex2)
					{
						throw new Exception("MoviePlayer Stop() Failed! : " + ex2.Message, ex2);
					}
					base.ExitScreen();
				}
			}
			this.delayinput = false;
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00377E40 File Offset: 0x00376040
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			this.myframe++;
			this.whichButton = "none";
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00377E64 File Offset: 0x00376064
		public override void Draw(GameTime gameTime)
		{
			this.playTimer--;
			this.watchtimer++;
			Matrix matrix = Matrix.CreateScale((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height, 1f);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, matrix);
			if (this.flag == 0)
			{
				this.spriteBatch.Draw(this.sc.titleTrailer, this.sc.origSize, Color.White);
				if (!this.myPlayer.IsDisposed)
				{
					if (this.myPlayer.State != MediaState.Playing)
					{
						this.myPlayer.Stop();
						this.myPlayer.Play(this.trailerVid);
						this.playTimer = 2;
					}
					if (this.playTimer <= 0)
					{
						this.spriteBatch.Draw(this.myPlayer.GetTexture(), new Vector2(848f, 338f), new Rectangle?(new Rectangle(0, 35, 720, 410)), Color.White, -0.07f, new Vector2(360f, 240f), 0.9f, SpriteEffects.None, 0f);
					}
				}
				this.spriteBatch.Draw(this.sc.titleFrame, this.sc.origSize, Color.White);
			}
			if (this.flag == 1 && !this.myPlayer.IsDisposed)
			{
				if (this.myPlayer.State != MediaState.Playing)
				{
					this.myPlayer.Stop();
					this.myPlayer.Play(this.trailerVid);
					this.playTimer = 2;
				}
				if (this.playTimer <= 0)
				{
					this.spriteBatch.Draw(this.myPlayer.GetTexture(), new Vector2(640f, 360f), null, Color.White, 0f, new Vector2(360f, 240f), 1f, SpriteEffects.None, 0f);
					if (this.watchtimer > 2580)
					{
						this.sc.trophy.win(this.sc.trophy.credits);
						this.watchtimer = 0;
					}
				}
			}
			this.spriteBatch.End();
		}

		// Token: 0x040034B0 RID: 13488
		private Video trailerVid;

		// Token: 0x040034B1 RID: 13489
		private VideoPlayer myPlayer;

		// Token: 0x040034B2 RID: 13490
		private ContentManager content2;

		// Token: 0x040034B3 RID: 13491
		private int playTimer = 60;

		// Token: 0x040034B4 RID: 13492
		private int watchtimer;

		// Token: 0x040034B5 RID: 13493
		private List<string> me = new List<string>();

		// Token: 0x040034B6 RID: 13494
		private int alternator;

		// Token: 0x040034B7 RID: 13495
		private bool delayinput = true;

		// Token: 0x040034B8 RID: 13496
		private string whichButton = "none";

		// Token: 0x040034B9 RID: 13497
		private Rectangle bgBlue = new Rectangle(644, 0, 510, 270);

		// Token: 0x040034BA RID: 13498
		private Rectangle yesRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x040034BB RID: 13499
		private Rectangle noRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x040034BC RID: 13500
		private Rectangle okRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x040034BD RID: 13501
		private Rectangle arrRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x040034BE RID: 13502
		private Rectangle meter = new Rectangle(164, 289, 362, 48);

		// Token: 0x040034BF RID: 13503
		private Rectangle meterColor = new Rectangle(164, 346, 362, 48);

		// Token: 0x040034C0 RID: 13504
		private Rectangle whitebox = new Rectangle(867, 74, 35, 35);

		// Token: 0x040034C1 RID: 13505
		private Rectangle arrows = new Rectangle(541, 289, 39, 84);

		// Token: 0x040034C2 RID: 13506
		private Vector2 mm = Vector2.Zero;

		// Token: 0x040034C3 RID: 13507
		private Matrix hudMatrix;

		// Token: 0x040034C4 RID: 13508
		private Keys[] pressedKeys = new Keys[0];

		// Token: 0x040034C5 RID: 13509
		private int kt;

		// Token: 0x040034C6 RID: 13510
		private int totaldots;

		// Token: 0x040034C7 RID: 13511
		private bool lastwasdot = true;

		// Token: 0x040034C8 RID: 13512
		private Rectangle goldRod;

		// Token: 0x040034C9 RID: 13513
		private Rectangle goldButton;

		// Token: 0x040034CA RID: 13514
		private string message;

		// Token: 0x040034CB RID: 13515
		private string messone;

		// Token: 0x040034CC RID: 13516
		private string ipaddress;

		// Token: 0x040034CD RID: 13517
		private string myname;

		// Token: 0x040034CE RID: 13518
		private string chatentry;

		// Token: 0x040034CF RID: 13519
		private bool includeYesNo;

		// Token: 0x040034D0 RID: 13520
		private bool techYesNo;

		// Token: 0x040034D1 RID: 13521
		private bool includeOkay;

		// Token: 0x040034D2 RID: 13522
		private int flag;

		// Token: 0x040034D3 RID: 13523
		private SpriteFont squarefont;

		// Token: 0x040034D4 RID: 13524
		private SpriteBatch spriteBatch;

		// Token: 0x040034D5 RID: 13525
		private SpriteFont font;

		// Token: 0x040034D6 RID: 13526
		private SpriteFont font2;

		// Token: 0x040034D7 RID: 13527
		private ScreenManager sc;

		// Token: 0x040034D8 RID: 13528
		private Viewport viewport;

		// Token: 0x040034D9 RID: 13529
		private Vector2 viewportSize;

		// Token: 0x040034DA RID: 13530
		private Vector2 textSize;

		// Token: 0x040034DB RID: 13531
		private Vector2 textPosition;

		// Token: 0x040034DC RID: 13532
		private int hPad;

		// Token: 0x040034DD RID: 13533
		private int vPad;

		// Token: 0x040034DE RID: 13534
		private float widthset = 300f;

		// Token: 0x040034DF RID: 13535
		private float mywidth;

		// Token: 0x040034E0 RID: 13536
		private float myhite;

		// Token: 0x040034E1 RID: 13537
		private float left;

		// Token: 0x040034E2 RID: 13538
		private float top;

		// Token: 0x040034E3 RID: 13539
		private float right;

		// Token: 0x040034E4 RID: 13540
		private float bottom;

		// Token: 0x040034E5 RID: 13541
		private float middle;

		// Token: 0x040034E6 RID: 13542
		private float midhite;

		// Token: 0x040034E7 RID: 13543
		private Rectangle menuSlate;

		// Token: 0x040034E8 RID: 13544
		private Rectangle cornerR;

		// Token: 0x040034E9 RID: 13545
		private Rectangle borderR;

		// Token: 0x040034EA RID: 13546
		private Rectangle backgroundRectangle;

		// Token: 0x040034EB RID: 13547
		private Rectangle topRect;

		// Token: 0x040034EC RID: 13548
		private Rectangle botRect;

		// Token: 0x040034ED RID: 13549
		private Rectangle leftRect;

		// Token: 0x040034EE RID: 13550
		private Rectangle rightRect;

		// Token: 0x040034EF RID: 13551
		private Rectangle cornA;

		// Token: 0x040034F0 RID: 13552
		private Rectangle cornB;

		// Token: 0x040034F1 RID: 13553
		private Rectangle cornC;

		// Token: 0x040034F2 RID: 13554
		private Rectangle cornD;

		// Token: 0x040034F3 RID: 13555
		private int max;

		// Token: 0x040034F4 RID: 13556
		private float currentNum;

		// Token: 0x040034F5 RID: 13557
		private int myframe;

		// Token: 0x040034F6 RID: 13558
		private bool useArial;

		// Token: 0x040034F7 RID: 13559
		private PlayerIndex playerIndex;

		// Token: 0x040034F8 RID: 13560
		private int myindex = -1;

		// Token: 0x040034F9 RID: 13561
		private GamePadState gamestate;

		// Token: 0x040034FA RID: 13562
		private GamePadState prevstate;

		// Token: 0x040034FB RID: 13563
		private KeyboardState prevKeys;

		// Token: 0x040034FC RID: 13564
		private KeyboardState keyState;

		// Token: 0x040034FD RID: 13565
		private MouseState prevMouse;

		// Token: 0x040034FE RID: 13566
		private MouseState mouseState;

		// Token: 0x040034FF RID: 13567
		public string longmessage = "";

		// Token: 0x04003506 RID: 13574
		private bool includeMeter;

		// Token: 0x04003507 RID: 13575
		private Random rr;

		// Token: 0x04003508 RID: 13576
		private string[] dummy = new string[]
		{
			"Dumb Ass", "Dumb-Ass", "El Dumbo", "Fart Smeller", "Lazy Goat", "The Noob", "Greenhorn", "Bed Wetter", "John Smith", "Pee Wee",
			"Thundar"
		};

		// Token: 0x04003509 RID: 13577
		public string password = "";

		// Token: 0x0400350A RID: 13578
		private ulong id = 5UL;
	}
}
