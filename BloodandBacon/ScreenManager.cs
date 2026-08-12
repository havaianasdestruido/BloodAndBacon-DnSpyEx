using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Steamworks;

namespace Blood
{
	// Token: 0x02000002 RID: 2
	public class ScreenManager : DrawableGameComponent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000026E8 File Offset: 0x000008E8
		public ScreenManager(bool sentGraphics, int x, int y, Game game, GraphicsDeviceManager gg)
		{
			int[] array = new int[26];
			array[2] = 1;
			this.weapon_Unlock = array;
			int[] array2 = new int[6];
			array2[0] = 1;
			this.grinder_Unlock = array2;
			this.grinder_Supply = new int[] { 100, 100, 100, 100, 100, 100 };
			this.boar1Health = 15;
			this.boar2Health = 15;
			this.handicapDam = new float[] { 3f, 1f, 0.5f, 0.5f, 0.5f };
			this.handicapBite = new float[] { 0.5f, 1f, 2f, 2f, 2f };
			this.handicapSpeed = new float[] { 0.6f, 1f, 1.5f, 1.5f };
			this.handicapTurn = new float[] { 1f, 1f, 3f, 3f, 3f };
			this.handicapDam2 = new float[] { 3f, 0.7f, 0.25f, 0.25f, 0.25f };
			this.handicapBite2 = new float[] { 0.5f, 1.5f, 3f, 2f, 2f };
			this.handicapSpeed2 = new float[] { 0.6f, 1f, 1.5f, 1.5f };
			this.handicapTurn2 = new float[] { 1f, 1.2f, 3f, 3f, 3f };
			this.handicapDam4 = new float[] { 2f, 0.75f, 0.25f, 0.25f, 0.25f };
			this.handicapBite4 = new float[] { 1f, 2f, 3f, 2f, 2f };
			this.handicapSpeed4 = new float[] { 1f, 1.1f, 1.5f, 1.5f };
			this.handicapTurn4 = new float[] { 1f, 1.2f, 3f, 3f, 3f };
			int[] array3 = new int[3];
			array3[0] = 80;
			array3[1] = 40;
			this.boarPercent = array3;
			this.boarDistance = new int[] { 300, 600, 1100 };
			this.boarHomingLimit = new int[] { 2, 5, 12 };
			this.cocking = new SoundEffect[22];
			this.gunFire = new SoundEffect[22];
			this.gunMuffle = new SoundEffect[22];
			this.gunDry = new SoundEffect[22];
			this.shellSound = new SoundEffect[22];
			this.hatTrans = Vector3.Zero;
			this.hatscale = 1f;
			this.hatMatrix = new Matrix[11, 16];
			this.temphatMatrix = new Matrix[11, 16];
			this.shellPack = new Model[22];
			this.gunTextures = new Texture2D[22];
			this.hatTextures = new Texture2D[16];
			this.paintColor = 5;
			this.paintColorCanvas = 8;
			this.paintRemColor = 5;
			this.paintRemColorCanvas = 8;
			this.setupnum = 1;
			this.screens = new List<GameScreen>();
			this.screensToUpdate = new List<GameScreen>();
			this.input = new InputState();
			this.storageSpacePrefs = new spaceStorage();
			this.spaceprefs = default(SpaceData);
			this.storagePrefs = new PrefStorage();
			this.prefs = default(PrefData);
			this.storageGame = new GameStorage();
			this.gdata = default(GameData);
			this.storeState = "";
			this.days = new int[201];
			this.camradian1 = -1.725f;
			this.camheight1 = 2.95f;
			this.campos3rd1 = new Vector3(-21.53f, 48.7f, 15.2f);
			this.camlookpos3rd1 = new Vector3(172.4f, 10.25f, -14.95f);
			this.camradian2 = -1.55f;
			this.camheight2 = 2.08f;
			this.campos3rd2 = new Vector3(-71f, 303f, 6.3f);
			this.camlookpos3rd2 = new Vector3(-45.6f, 259f, 6.7f);
			this.dayLevel = 1;
			this.mv = 0.5f;
			this.ev = 0.8f;
			this.vv = 0.7f;
			this.df = 1;
			this.df_orig = 1;
			this.pad_invertY = 1f;
			this.pad_sensitivity = 1f;
			this.pad_vibro = true;
			this.pad_reload = true;
			this.pad_togglesprint = true;
			this.playername = "Player";
			this.fullmode = true;
			this.border = true;
			this.hud_enemy = new Vector2(90f, 35f);
			this.hud_clock = new Vector2(490f, 35f);
			this.hud_day = new Vector2(1180f, 35f);
			this.hud_player1 = new Vector2(485f, 620f);
			this.hud_player2 = new Vector2(70f, 620f);
			this.hud_weapons = new Vector2(1130f, 290f);
			this.hud_dpad = new Vector2(1135f, 360f);
			this.color_enemy = new Color(210, 0, 0, 255);
			this.color_day = new Color(210, 0, 0, 255);
			this.color_clock = new Color(255, 255, 255, 255);
			this.color_player1 = new Color(255, 255, 255, 255);
			this.color_player2 = new Color(255, 255, 255, 255);
			this.color_weapons = new Color(255, 255, 255, 255);
			this.color_dpad = new Color(255, 255, 255, 255);
			this.gamername = "noname";
			this.aliasSet = 2;
			this.typewriterblank = 500f;
			this.typewriterwait = 100f;
			this.typevertical = 100f;
			this.typewriterdelay = 3;
			this.siders = 10f;
			this.bottomer = 710f;
			this.topper = 10f;
			this.voiceVolume = 0.5f;
			this.bgindex = 1;
			this.planet = 1;
			this.fadeSetting = 10f;
			this.intenseSetting = 8f;
			this.gamefadeSetting = 7f;
			this.gameintenseSetting = 3f;
			this.aspectratio = 1.78f;
			this.aspectratio2 = 1.78f;
			this.vehicleindex = 3;
			this.space_vibro = true;
			this.space_invertX = 1;
			this.space_invertY = 1;
			this.space_sentivityX = 1f;
			this.space_sentivityY = 1f;
			this.space_winvertX = 1;
			this.space_winvertY = 1;
			this.space_wsentivityX = 1f;
			this.space_wsentivityY = 1f;
			this.space_rinvertX = 1;
			this.space_rinvertY = 1;
			this.space_rsentivityX = 1f;
			this.space_rsentivityY = 1f;
			this.vibroSetting = 1;
			this.camradius = 1000f;
			this.allcamsradius = new int[] { 0, 1, 3, 4 };
			int[] array4 = new int[4];
			this.allcamsorbit = array4;
			int[] array5 = new int[4];
			this.allcamsaltitude = array5;
			int[] array6 = new int[4];
			this.allcamslens = array6;
			this.roverindex = 2;
			this.roverhitelock = 1;
			this.roverdist = new float[] { 200f, 440f, 800f };
			this.roverheight = new float[] { 100f, 400f, 700f };
			this.roverradian = new float[] { 100f, 100f, 100f };
			this.landerindex = 1;
			this.landerhitelock = 1;
			this.landerdist = new float[] { 500f, 1400f, 2500f };
			this.landerheight = new float[] { 300f, 300f, 300f };
			this.landerradian = new float[] { 100f, 100f, 100f };
			this.drawflag = 1;
			this.equip = new int[]
			{
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 2, 1, 3, 3, 2, 1
			};
			this.roverSpeed = 1f;
			this.roverGrip = 1f;
			this.roverTurn = 0.25f;
			this.cheats_test = true;
			this.mess = "";
			this.planetName = new string[] { "none", "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto" };
			this.Globalstarmap = new buildStars();
			this.vehiclelerp = 1f;
			this.loadscreenIndex = 1;
			this.loadScreen = new List<int>();
			this.bitmap = 290;
			this.gridScale = 150;
			this.unit = 289;
			this.roverparts = new List<string>();
			this.wheelRollMatrix = Matrix.Identity;
			this.wheelRollMatrix2 = Matrix.Identity;
			this.rackMatrix = Matrix.Identity;
			this.solar1aMatrix = Matrix.Identity;
			this.solar1bMatrix = Matrix.Identity;
			this.scooperMatrix = Matrix.Identity;
			this.bucketMatrix = Matrix.Identity;
			this.binMatrix = Matrix.Identity;
			this.errorline = "";
			this.startwidth = 1280f;
			this.starthite = 720f;
			this.stat1 = "";
			this.stat2 = "";
			this.stat3 = "";
			this.stat4 = "";
			this.stat5 = "";
			this.stat6 = "";
			this.stat7 = "";
			this.ar1 = 0.95f;
			this.ar2 = 0.7f;
			this.chatHistory = new List<ScreenManager.chatty>();
			base..ctor(game);
			this.graphics = gg;
			if (sentGraphics)
			{
				this.cardx = x;
				this.cardy = y;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000055CC File Offset: 0x000037CC
		public override void Initialize()
		{
			base.Initialize();
			this.isInitialized = true;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000055DB File Offset: 0x000037DB
		private void Form1_KeyDown(object sender, FormClosingEventArgs e)
		{
			SteamAPI.Shutdown();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000055E4 File Offset: 0x000037E4
		protected override void LoadContent()
		{
			this.rr = new Random();
			this.trialStart = Stopwatch.GetTimestamp();
			this.steamStart();
			this.cardx = this.graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
			this.cardy = this.graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
			int num = 1280;
			int num2 = 0;
			bool flag = false;
			this.resnames = new List<string>();
			this.width = this.cardx;
			this.hite = this.cardy;
			foreach (DisplayMode displayMode in this.graphics.GraphicsDevice.Adapter.SupportedDisplayModes)
			{
				if (displayMode.Width == num && !flag)
				{
					this.width = displayMode.Width;
					this.hite = displayMode.Height;
					this.resolution = num2;
					flag = true;
				}
				num2++;
				this.resnames.Add(displayMode.Width + " by " + displayMode.Height);
			}
			this.aspect = (float)this.cardx / (float)this.cardy;
			this.aspectratio = this.aspect / 1.77777f;
			this.chat = new chatbox();
			this.content = base.Game.Content;
			this.content2 = new ContentManager(base.Game.Services, "ABCDE1");
			this.conModel = new ContentManager(base.Game.Services, "ABCDE2");
			this.SpriteBatch = new SpriteBatch(this.graphics.GraphicsDevice);
			this.strangers.Clear();
			this.redsuns.Clear();
			this.kickers.Clear();
			this.pileDriverSure = this.content2.Load<SoundEffect>("piledriver");
			this.scree = this.content.Load<SoundEffect>("audio//scree");
			this.xmas = this.content.Load<SoundEffect>("audio//xmas");
			this.cancel = this.content.Load<SoundEffect>("audio//cancel1");
			this.accept = this.content.Load<SoundEffect>("audio//accept1");
			this.equipx = this.content.Load<SoundEffect>("audio//equipon");
			this.wavecomplete = this.content.Load<SoundEffect>("audio//wavecomplete");
			this.achievepop = this.content.Load<SoundEffect>("audio//achieves");
			this.tick = this.content.Load<SoundEffect>("audio//tick");
			this.wound = this.content.Load<Texture2D>("texture//woundTarget");
			this.font3 = this.content2.Load<SpriteFont>("awatermark");
			this.landerfont = this.content2.Load<SpriteFont>("TextureFont1");
			this.font = this.content2.Load<SpriteFont>("ammomedium3");
			this.grungeFont = this.content2.Load<SpriteFont>("ammoLargest");
			this.font2 = this.content.Load<SpriteFont>("font//Font");
			this.lilyFont = this.content.Load<SpriteFont>("font//lilyUPC");
			this.fontsmall = this.content2.Load<SpriteFont>("ammosmall3");
			this.arialfont = this.content2.Load<SpriteFont>("Arial");
			this.scribblefont = this.content2.Load<SpriteFont>("scribbleFont2");
			this.scribblefont2 = this.content.Load<SpriteFont>("font//afont5");
			this.terminal = this.content2.Load<SpriteFont>("deadfont2");
			this.squarefont = this.content2.Load<SpriteFont>("square1");
			this.trophy = new achieve(this);
			this.lobby = new Lobby(this, this.pileDriverSure);
			this.workshop = new Workshop(this);
			this.goldKeys = new goldenkey(this, this.content);
			this.star = this.content.Load<Texture2D>("texture//star");
			this.staroff = this.content.Load<Texture2D>("texture//staroff");
			this.starB = this.content.Load<Texture2D>("texture//starBorder");
			this.certified = this.content.Load<Texture2D>("texture//certified");
			this.overlay = this.content2.Load<Texture2D>("overlay");
			this.menuBlob2 = this.content.Load<Texture2D>("texture//menuBlob3");
			this.blankpaper = this.content.Load<Texture2D>("texture//controls3");
			this.topcorner = new Vector2((float)(640 - this.blankpaper.Width / 2), (float)(370 - this.blankpaper.Height / 2));
			this.whiteTexture = new Texture2D(this.graphics.GraphicsDevice, 1, 1);
			this.redTexture = new Texture2D(this.graphics.GraphicsDevice, 1, 1);
			this.whiteTexture.SetData<Color>(new Color[] { Color.White });
			this.redTexture.SetData<Color>(new Color[] { Color.DarkRed });
			this.blackTexture = new Texture2D(this.graphics.GraphicsDevice, 1, 1);
			this.blackTexture.SetData<Color>(new Color[] { Color.Black });
			this.wallguns[0] = Matrix.CreateScale(2.2f) * Matrix.CreateRotationX(0f) * Matrix.CreateRotationY(3.1415923f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(291.5962f, 71.947426f, 1804.3456f);
			this.wallguns[2] = Matrix.CreateScale(2.38f) * Matrix.CreateRotationX(0f) * Matrix.CreateRotationY(3.1415923f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(291.8526f, 41.132626f, 1804.2345f);
			this.wallguns[4] = Matrix.CreateScale(2.6f) * Matrix.CreateRotationX(0.15207727f) * Matrix.CreateRotationY(3.1981506f) * Matrix.CreateRotationZ(0.0063756094f) * Matrix.CreateTranslation(237.41576f, 67f, 1803.8033f);
			this.wallguns[6] = Matrix.CreateScale(2.0160143f) * Matrix.CreateRotationX(0.08249152f) * Matrix.CreateRotationY(0.015799092f) * Matrix.CreateRotationZ(0.0013062005f) * Matrix.CreateTranslation(264.23462f, 37.765495f, 1444.6427f);
			this.wallguns[8] = Matrix.CreateScale(2.5079386f) * Matrix.CreateRotationX(0.101869315f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(323.01572f, 39.409885f, 1445.3538f);
			this.wallguns[10] = Matrix.CreateScale(2.3070757f) * Matrix.CreateRotationX(0.13399903f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(266.28754f, 69.95057f, 1444.9357f);
			this.wallguns[12] = Matrix.CreateScale(2.4037678f) * Matrix.CreateRotationX(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(339.7893f, 70.453636f, 1445.0908f);
			this.wallguns[14] = Matrix.CreateScale(2.8350866f) * Matrix.CreateRotationX(0.1f) * Matrix.CreateRotationY(-0.05f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(295.4769f, 113.61871f, 1447.4276f);
			this.wallguns[16] = Matrix.CreateScale(2.3f) * Matrix.CreateRotationX(0.15207727f) * Matrix.CreateRotationY(3.1981506f) * Matrix.CreateRotationZ(0.0063756094f) * Matrix.CreateTranslation(242f, 39f, 1803.8033f);
			this.wallguns[18] = Matrix.CreateScale(2.8f) * Matrix.CreateRotationX(0.08249152f) * Matrix.CreateRotationY(0.015799092f) * Matrix.CreateRotationZ(0.0013062005f) * Matrix.CreateTranslation(487f, 38f, 1444.6427f);
			this.wallguns[20] = Matrix.CreateScale(2.6f) * Matrix.CreateTranslation(550f, 41f, 1445.25f);
			this.shitContrast = 128;
			this.redContrast = 128;
			this.setBlendState();
			this.resetVideo();
			this.resetAudio();
			this.resetGame();
			foreach (GameScreen gameScreen in this.screens)
			{
				gameScreen.LoadContent();
			}
			this.keyState = Keyboard.GetState();
			if (this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift) || this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightAlt) || this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightShift))
			{
				this.fullscreen = 0;
				this.fullmode = false;
				this.drawViewport = false;
				this.skipsettings = true;
			}
			this.resetGame();
			this.LoadGame(false);
			if (!this.skipsettings)
			{
				this.LoadPrefs();
			}
			this.LoadSavedKeys();
			if (this.bonusweek)
			{
				this.man1 = true;
				this.man2 = true;
				this.FarmerUnlocked = true;
				this.hats[9] = 1;
			}
			this.currentDay = this.curDay;
			this.setupnum = this.resolution;
			if (this.setupnum > this.resnames.Count - 1)
			{
				this.setupnum = 0;
			}
			this.aa = this.aliasing;
			if (!this.skipsettings)
			{
				this.setResolution();
			}
			else
			{
				this.setResolutionSafe();
			}
			this.setgraphics = false;
			this.loadAudio();
			this.audioLoaded = true;
			if (!this.walletLoaded)
			{
				this.loadWallet();
			}
			this.mainTheme = this.content.Load<SoundEffect>("audio\\Corncob");
			this.centerWindow();
			this.bossexplodechoice = this.rr.Next(1, 4);
			base.Game.Deactivated += this.DeactivatedEventHandler;
			base.Game.Activated += this.ActivatedEventHandler;
			this.basicCrosshair1 = this.content.Load<Texture2D>("crosshair//crosshair1");
			this.basicCrosshair2 = this.content.Load<Texture2D>("crosshair//crosshair1");
			this.basicCrosshair3 = this.content.Load<Texture2D>("crosshair//crosshair1");
			this.basicCrosshair4 = this.content.Load<Texture2D>("crosshair//crosshair1");
			this.getLoadOut();
			this.woundRect = new Rectangle[17];
			this.woundRect[0] = new Rectangle(0, 0, 64, 64);
			this.woundRect[1] = new Rectangle(0, 0, 64, 64);
			this.woundRect[2] = new Rectangle(64, 0, 64, 64);
			this.woundRect[3] = new Rectangle(128, 0, 64, 64);
			this.woundRect[4] = new Rectangle(192, 0, 64, 64);
			this.woundRect[5] = new Rectangle(0, 64, 64, 64);
			this.woundRect[6] = new Rectangle(64, 64, 64, 64);
			this.woundRect[7] = new Rectangle(128, 64, 64, 64);
			this.woundRect[8] = new Rectangle(192, 64, 64, 64);
			this.woundRect[9] = new Rectangle(0, 128, 64, 64);
			this.woundRect[10] = new Rectangle(64, 128, 64, 64);
			this.woundRect[11] = new Rectangle(128, 128, 64, 64);
			this.woundRect[12] = new Rectangle(192, 128, 64, 64);
			this.woundRect[13] = new Rectangle(0, 192, 64, 64);
			this.woundRect[14] = new Rectangle(64, 192, 64, 64);
			this.woundRect[15] = new Rectangle(128, 192, 64, 64);
			this.woundRect[16] = new Rectangle(192, 192, 64, 64);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00006408 File Offset: 0x00004608
		private void keymaker(ref Microsoft.Xna.Framework.Input.Keys origkey, string line)
		{
			try
			{
				char[] array = new char[] { ' ', '\t' };
				string[] array2 = line.Split(array);
				origkey = (Microsoft.Xna.Framework.Input.Keys)Enum.Parse(typeof(Microsoft.Xna.Framework.Input.Keys), array2[0]);
			}
			catch
			{
				origkey = origkey;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00006464 File Offset: 0x00004664
		public void getLoadOut()
		{
			string text = "Content\\crosshair\\1a.block";
			string text2 = "Content\\crosshair\\2a.block";
			string text3 = "Content\\crosshair\\3a.block";
			this.crosshair1.Clear();
			this.tempcross1.texture = this.basicCrosshair1;
			this.tempcross1.type = 1;
			this.tempcross1.legal = true;
			this.crosshair1.Add(this.tempcross1);
			this.tempcross2.type = 0;
			this.tempcross2.legal = true;
			if (File.Exists(text))
			{
				this.tempcross2.legal = false;
			}
			this.tempcross2.texture = this.basicCrosshair2;
			this.crosshair1.Add(this.tempcross2);
			this.tempcross3.type = 0;
			this.tempcross3.legal = true;
			if (File.Exists(text2))
			{
				this.tempcross3.legal = false;
			}
			this.tempcross3.texture = this.basicCrosshair3;
			this.crosshair1.Add(this.tempcross3);
			this.tempcross4.type = 0;
			this.tempcross4.legal = true;
			if (File.Exists(text3))
			{
				this.tempcross4.legal = false;
			}
			this.tempcross4.texture = this.basicCrosshair4;
			this.crosshair1.Add(this.tempcross4);
			string text4 = "Content\\crosshair";
			for (int i = 1; i < 4; i++)
			{
				string text5 = i.ToString() + "a.png";
				if (File.Exists(text4 + "\\" + text5))
				{
					try
					{
						using (FileStream fileStream = new FileStream(text4 + "\\" + text5, FileMode.Open))
						{
							this.crosshair1[i].type = 1;
							this.crosshair1[i].texture = Texture2D.FromStream(base.GraphicsDevice, fileStream);
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000666C File Offset: 0x0000486C
		public void loadCrosshairC()
		{
			this.crosshairC.Clear();
			string text = "CrossHairs";
			string text2 = "UploadData";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			IEnumerable<string> enumerable = Directory.EnumerateFiles(text, "*.png", SearchOption.TopDirectoryOnly);
			foreach (string text3 in enumerable)
			{
				string text4 = text3.Substring(text.Length + 1);
				if (File.Exists(text + "\\" + text4))
				{
					using (FileStream fileStream = new FileStream(text + "\\" + text4, FileMode.Open))
					{
						this.crosshairC.Add(Texture2D.FromStream(base.GraphicsDevice, fileStream));
					}
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00006760 File Offset: 0x00004960
		public void saveLoadOut()
		{
			string text = "Content\\crosshair\\1a.png";
			string text2 = "Content\\crosshair\\2a.png";
			string text3 = "Content\\crosshair\\3a.png";
			string text4 = "Content\\crosshair\\1a.block";
			string text5 = "Content\\crosshair\\2a.block";
			string text6 = "Content\\crosshair\\3a.block";
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			if (File.Exists(text2))
			{
				File.Delete(text2);
			}
			if (File.Exists(text3))
			{
				File.Delete(text3);
			}
			if (File.Exists(text4))
			{
				File.Delete(text4);
			}
			if (File.Exists(text5))
			{
				File.Delete(text5);
			}
			if (File.Exists(text6))
			{
				File.Delete(text6);
			}
			if (this.crosshair1[1].type == 1)
			{
				using (Stream stream = File.OpenWrite(text))
				{
					this.crosshair1[1].texture.SaveAsPng(stream, this.crosshair1[1].texture.Width, this.crosshair1[1].texture.Height);
				}
				if (!this.crosshair1[1].legal)
				{
					File.Create(text4);
				}
			}
			if (this.crosshair1[2].type == 1)
			{
				using (Stream stream2 = File.OpenWrite(text2))
				{
					this.crosshair1[2].texture.SaveAsPng(stream2, this.crosshair1[2].texture.Width, this.crosshair1[2].texture.Height);
				}
				if (!this.crosshair1[2].legal)
				{
					File.Create(text5);
				}
			}
			if (this.crosshair1[3].type == 1)
			{
				using (Stream stream3 = File.OpenWrite(text3))
				{
					this.crosshair1[3].texture.SaveAsPng(stream3, this.crosshair1[3].texture.Width, this.crosshair1[3].texture.Height);
				}
				if (!this.crosshair1[3].legal)
				{
					File.Create(text6);
				}
			}
			this.crossIndex = 0;
			this.nextLoadout();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000069BC File Offset: 0x00004BBC
		public void nextLoadout()
		{
			this.crossIndex++;
			if (this.crossIndex > 3)
			{
				this.crossIndex = 0;
				return;
			}
			if (this.crossIndex == 1 && this.crosshair1[1].type == 0)
			{
				this.crossIndex = 2;
			}
			if (this.crossIndex == 2 && this.crosshair1[2].type == 0)
			{
				this.crossIndex = 3;
			}
			if (this.crossIndex == 3 && this.crosshair1[3].type == 0)
			{
				this.crossIndex = 0;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00006A54 File Offset: 0x00004C54
		public string LoadSavedKeys()
		{
			string text = "SAVES//savekeys";
			if (File.Exists(text))
			{
				try
				{
					using (StreamReader streamReader = new StreamReader(File.Open(text, FileMode.Open)))
					{
						this.keymaker(ref this.escape_key, streamReader.ReadLine());
						this.keymaker(ref this.w_key, streamReader.ReadLine());
						this.keymaker(ref this.s_key, streamReader.ReadLine());
						this.keymaker(ref this.a_key, streamReader.ReadLine());
						this.keymaker(ref this.d_key, streamReader.ReadLine());
						this.keymaker(ref this.space_key, streamReader.ReadLine());
						this.keymaker(ref this.leftshift_key, streamReader.ReadLine());
						this.keymaker(ref this.r_key, streamReader.ReadLine());
						this.keymaker(ref this.x_key, streamReader.ReadLine());
						this.keymaker(ref this.q_key, streamReader.ReadLine());
						this.keymaker(ref this.e_key, streamReader.ReadLine());
						this.keymaker(ref this.f_key, streamReader.ReadLine());
						this.keymaker(ref this.one_key, streamReader.ReadLine());
						this.keymaker(ref this.two_key, streamReader.ReadLine());
						this.keymaker(ref this.three_key, streamReader.ReadLine());
						this.keymaker(ref this.four_key, streamReader.ReadLine());
						this.keymaker(ref this.f1_key, streamReader.ReadLine());
						this.keymaker(ref this.f2_key, streamReader.ReadLine());
						this.keymaker(ref this.f3_key, streamReader.ReadLine());
						this.keymaker(ref this.tab_key, streamReader.ReadLine());
						this.keymaker(ref this.up_key, streamReader.ReadLine());
						this.keymaker(ref this.down_key, streamReader.ReadLine());
						this.keymaker(ref this.left_key, streamReader.ReadLine());
						this.keymaker(ref this.lmb_key, streamReader.ReadLine());
						this.keymaker(ref this.rmb_key, streamReader.ReadLine());
						this.keymaker(ref this.mmb_key, streamReader.ReadLine());
						this.keymaker(ref this.but1_key, streamReader.ReadLine());
						this.keymaker(ref this.but2_key, streamReader.ReadLine());
						this.keymaker(ref this.t_key, streamReader.ReadLine());
						this.keymaker(ref this.enter_key, streamReader.ReadLine());
						this.keymaker(ref this.plus_key, streamReader.ReadLine());
						streamReader.Close();
						streamReader.Dispose();
						return "OKAY";
					}
				}
				catch
				{
					return "FAIL";
				}
			}
			string text2;
			using (IsolatedStorageFile store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
			{
				text = "savekeys";
				if (!store.FileExists(text))
				{
					text2 = "nofile";
				}
				else
				{
					try
					{
						using (StreamReader streamReader2 = new StreamReader(store.OpenFile(text, FileMode.Open)))
						{
							this.keymaker(ref this.escape_key, streamReader2.ReadLine());
							this.keymaker(ref this.w_key, streamReader2.ReadLine());
							this.keymaker(ref this.s_key, streamReader2.ReadLine());
							this.keymaker(ref this.a_key, streamReader2.ReadLine());
							this.keymaker(ref this.d_key, streamReader2.ReadLine());
							this.keymaker(ref this.space_key, streamReader2.ReadLine());
							this.keymaker(ref this.leftshift_key, streamReader2.ReadLine());
							this.keymaker(ref this.r_key, streamReader2.ReadLine());
							this.keymaker(ref this.x_key, streamReader2.ReadLine());
							this.keymaker(ref this.q_key, streamReader2.ReadLine());
							this.keymaker(ref this.e_key, streamReader2.ReadLine());
							this.keymaker(ref this.f_key, streamReader2.ReadLine());
							this.keymaker(ref this.one_key, streamReader2.ReadLine());
							this.keymaker(ref this.two_key, streamReader2.ReadLine());
							this.keymaker(ref this.three_key, streamReader2.ReadLine());
							this.keymaker(ref this.four_key, streamReader2.ReadLine());
							this.keymaker(ref this.f1_key, streamReader2.ReadLine());
							this.keymaker(ref this.f2_key, streamReader2.ReadLine());
							this.keymaker(ref this.f3_key, streamReader2.ReadLine());
							this.keymaker(ref this.tab_key, streamReader2.ReadLine());
							this.keymaker(ref this.up_key, streamReader2.ReadLine());
							this.keymaker(ref this.down_key, streamReader2.ReadLine());
							this.keymaker(ref this.left_key, streamReader2.ReadLine());
							this.keymaker(ref this.lmb_key, streamReader2.ReadLine());
							this.keymaker(ref this.rmb_key, streamReader2.ReadLine());
							this.keymaker(ref this.mmb_key, streamReader2.ReadLine());
							this.keymaker(ref this.but1_key, streamReader2.ReadLine());
							this.keymaker(ref this.but2_key, streamReader2.ReadLine());
							this.keymaker(ref this.t_key, streamReader2.ReadLine());
							this.keymaker(ref this.enter_key, streamReader2.ReadLine());
							streamReader2.Close();
							streamReader2.Dispose();
							text2 = "OKAY";
						}
					}
					catch
					{
						text2 = "FAIL";
					}
				}
			}
			return text2;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00006FDC File Offset: 0x000051DC
		public bool SaveSavedKeys()
		{
			string text = "SAVES//savekeys";
			if (!Directory.Exists("SAVES"))
			{
				Directory.CreateDirectory("SAVES");
			}
			bool flag;
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(File.Open(text, FileMode.Create)))
				{
					streamWriter.WriteLine(this.escape_key.ToString());
					streamWriter.WriteLine(this.w_key.ToString());
					streamWriter.WriteLine(this.s_key.ToString());
					streamWriter.WriteLine(this.a_key.ToString());
					streamWriter.WriteLine(this.d_key.ToString());
					streamWriter.WriteLine(this.space_key.ToString());
					streamWriter.WriteLine(this.leftshift_key.ToString());
					streamWriter.WriteLine(this.r_key.ToString());
					streamWriter.WriteLine(this.x_key.ToString());
					streamWriter.WriteLine(this.q_key.ToString());
					streamWriter.WriteLine(this.e_key.ToString());
					streamWriter.WriteLine(this.f_key.ToString());
					streamWriter.WriteLine(this.one_key.ToString());
					streamWriter.WriteLine(this.two_key.ToString());
					streamWriter.WriteLine(this.three_key.ToString());
					streamWriter.WriteLine(this.four_key.ToString());
					streamWriter.WriteLine(this.f1_key.ToString());
					streamWriter.WriteLine(this.f2_key.ToString());
					streamWriter.WriteLine(this.f3_key.ToString());
					streamWriter.WriteLine(this.tab_key.ToString());
					streamWriter.WriteLine(this.up_key.ToString());
					streamWriter.WriteLine(this.down_key.ToString());
					streamWriter.WriteLine(this.left_key.ToString());
					streamWriter.WriteLine(this.lmb_key.ToString());
					streamWriter.WriteLine(this.rmb_key.ToString());
					streamWriter.WriteLine(this.mmb_key.ToString());
					streamWriter.WriteLine(this.but1_key.ToString());
					streamWriter.WriteLine(this.but2_key.ToString());
					streamWriter.WriteLine(this.t_key.ToString());
					streamWriter.WriteLine(this.enter_key.ToString());
					streamWriter.WriteLine(this.plus_key.ToString());
					streamWriter.Close();
					streamWriter.Dispose();
					flag = true;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00007314 File Offset: 0x00005514
		public void steamStart()
		{
			SteamAPI.Init();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000731C File Offset: 0x0000551C
		public void exitmyGame()
		{
			SteamAPI.Shutdown();
			base.Game.Exit();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00007330 File Offset: 0x00005530
		public void loadSpaceFarm()
		{
			this.farmspacecollide = this.conModel.Load<Model>("farm01spaceCollide");
			this.farmBuildingspace = this.conModel.Load<Model>("farm01space");
			this.buildingRGB = this.content.Load<Texture2D>("texture//buildingRGB");
			this.buildingshadspace = this.content.Load<Texture2D>("texture//buildingShadowSpace");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00007398 File Offset: 0x00005598
		public void mainModels()
		{
			this.water = this.conModel.Load<Model>("water");
			this.boar1basicModel = this.conModel.Load<Model>("boarWalk_reduct");
			this.boarSkel = this.content.Load<Model>("npc\\boarSkel");
			this.charModel = this.conModel.Load<Model>("boarCharred");
			this.zombieModel = this.conModel.Load<Model>("boarCharred");
			this.cube = this.conModel.Load<Model>("cube");
			this.decal = this.conModel.Load<Model>("outline2");
			this.decalb = this.conModel.Load<Model>("outline2b");
			this.decal2 = this.conModel.Load<Model>("outline3");
			this.biteDecal = this.conModel.Load<Model>("outlineBites");
			this.explosionDecal = this.conModel.Load<Model>("outlineExplode");
			this.fireballDecal = this.conModel.Load<Model>("outlineFireball");
			this.fireballDecal2 = this.conModel.Load<Model>("outlineFireballblue");
			this.buttonModel = this.conModel.Load<Model>("button");
			this.mountain = this.conModel.Load<Model>("mountain");
			this.gunMuzzle = this.conModel.Load<Model>("gunMuzzle");
			this.gunBlast = this.conModel.Load<Model>("gunblast");
			this.blueLaser = this.conModel.Load<Model>("tracer");
			this.shellPack[0] = this.conModel.Load<Model>("indieShell");
			this.shellPack[2] = this.conModel.Load<Model>("45shell2");
			this.shellPack[4] = this.conModel.Load<Model>("Wshell");
			this.shellPack[6] = this.conModel.Load<Model>("AKshell2");
			this.shellPack[8] = this.conModel.Load<Model>("shotgunShell");
			this.shellPack[10] = this.conModel.Load<Model>("m16shell");
			this.shellPack[12] = this.conModel.Load<Model>("uziShell");
			this.shellPack[14] = this.conModel.Load<Model>("AKshell2");
			this.shellPack[16] = this.conModel.Load<Model>("45shell2");
			this.shellPack[18] = this.conModel.Load<Model>("m16shell");
			this.shellPack[20] = this.conModel.Load<Model>("SCARshell");
			this.gunPack = this.conModel.Load<Model>("gunPack");
			this.hatPack = this.conModel.Load<Model>("hatPack");
			this.grass = this.conModel.Load<Model>("farmGrass");
			this.trees = this.conModel.Load<Model>("farmTrees");
			this.farmTriangles = this.conModel.Load<Model>("farm01triangle");
			this.farmTriangles2 = this.conModel.Load<Model>("farm02triangle");
			this.farmTriangles3 = this.conModel.Load<Model>("farm03triangle");
			this.barnTriangles = this.conModel.Load<Model>("barn01triangle");
			this.doorTriangles = this.conModel.Load<Model>("door01triangle");
			this.farmBuilding = this.conModel.Load<Model>("farm01");
			this.farmBuilding2 = this.conModel.Load<Model>("farm02");
			this.farmBuilding3 = this.conModel.Load<Model>("farm03");
			this.barnBuilding = this.conModel.Load<Model>("barn01");
			this.heightmodel = this.conModel.Load<Model>("farm01collide");
			this.heightmodel2 = this.conModel.Load<Model>("farm01collide2");
			this.tunnel1 = this.conModel.Load<Model>("tunnelA");
			this.tunnelTriangle = this.conModel.Load<Model>("tunnelAtriangle");
			this.tunnelheights = this.conModel.Load<Model>("tunnelAcollide");
			this.blasts = this.content.Load<Texture2D>("texture//blasts");
			this.burster = this.content.Load<Texture2D>("texture//burster");
			this.button = this.content.Load<Texture2D>("texture//button");
			this.crosshair = this.content.Load<Texture2D>("texture//crosshair");
			this.electrify = this.content.Load<Texture2D>("texture//electrify");
			this.muzzles = this.content.Load<Texture2D>("texture//muzzles");
			this.overlayStats = this.content.Load<Texture2D>("texture//overlayStats");
			this.overlayStats2 = this.content.Load<Texture2D>("texture//overlayStats2");
			this.reflectionMap = this.content.Load<Texture2D>("texture//reflectionMap");
			this.spotTexture = this.content.Load<Texture2D>("texture//spotTexture");
			this.waterTexture = this.content.Load<Texture2D>("texture//waterTexture");
			this.loadMainModels = true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000078C4 File Offset: 0x00005AC4
		public void loadBoss()
		{
			this.bossAll = this.conModel.Load<Model>("bossAll");
			this.bossLoaded = true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000078E4 File Offset: 0x00005AE4
		public void loadWallet()
		{
			this.landoWallet = this.content.Load<Texture2D>("texture\\landoWallet");
			this.johnWallet = this.content.Load<Texture2D>("texture\\johnnyWallet");
			this.farmerWallet = this.content.Load<Texture2D>("texture\\farmerWallet");
			this.skellyWallet = this.content.Load<Texture2D>("texture\\skelWallet");
			this.daisyWallet = this.content.Load<Texture2D>("texture\\daisyWallet");
			this.vikingWallet = this.content.Load<Texture2D>("texture\\vikingWallet");
			this.deadWallet = this.content.Load<Texture2D>("texture\\deadWallet");
			this.robotWallet = this.content.Load<Texture2D>("texture\\robotWallet");
			this.golemWallet = this.content.Load<Texture2D>("texture\\golemWallet");
			this.astroWallet = this.content.Load<Texture2D>("texture\\astroWallet");
			this.johnnyWallet = this.johnWallet;
			this.controller = this.content.Load<Texture2D>("texture//controls");
			this.instructions = this.content.Load<Texture2D>("texture//instructions");
			this.page1 = this.content.Load<Texture2D>("texture//page1");
			this.page2 = this.content.Load<Texture2D>("texture//page2");
			this.page3 = this.content.Load<Texture2D>("texture//page3");
			this.page4 = this.content.Load<Texture2D>("texture//page4");
			this.page5 = this.content.Load<Texture2D>("texture//page5");
			this.page6 = this.content.Load<Texture2D>("texture//page6");
			this.page7 = this.content.Load<Texture2D>("texture//page7");
			this.paper1 = this.content.Load<Texture2D>("texture//paper1");
			this.walletLoaded = true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00007ABC File Offset: 0x00005CBC
		public void loadTitles()
		{
			this.titleBG2 = this.content.Load<Texture2D>("texture//titleBG3");
			this.titleBG = this.content.Load<Texture2D>("texture//titleBG");
			this.titleFaces = this.content.Load<Texture2D>("texture//titleFaces");
			this.titleCharsSingle = this.content.Load<Texture2D>("texture//titleCharsSingle");
			this.titleCharsGraphics = this.content.Load<Texture2D>("texture//titleCharsGraphics");
			this.titleHeaderTunnelDays = this.content.Load<Texture2D>("texture//titleHeaderTunnelDays");
			this.titleHeaderBloodBacon = this.content.Load<Texture2D>("texture//titleHeaderBloodBacon");
			this.titleHeaderBloodBaconB = this.content.Load<Texture2D>("texture//titleHeaderBloodBaconB");
			this.titleHeaderBloodBaconC = this.content.Load<Texture2D>("texture//titleHeaderBloodBaconC");
			this.titleHeaderWorkshop = this.content.Load<Texture2D>("texture//titleHeaderWorkshop");
			this.titleHeaderWorkshop2 = this.content.Load<Texture2D>("texture//titleHeaderWorkshop2");
			this.titleHeaderWorkshopPublish = this.content.Load<Texture2D>("texture//titleHeaderWorkshopPublish");
			this.titleHeaderWorkshopPublish2 = this.content.Load<Texture2D>("texture//titleHeaderWorkshopPublish2");
			this.titleHeaderDiary = this.content.Load<Texture2D>("texture//titleHeaderDiary");
			this.titleHeaderSecrets = this.content.Load<Texture2D>("texture//titleHeaderSecrets");
			this.titleHeaderMore = this.content.Load<Texture2D>("texture//titleHeaderMore");
			this.titleheaderSettings = this.content.Load<Texture2D>("texture//titleHeaderSettings");
			this.titleHeaderLobbies = this.content.Load<Texture2D>("texture//titleHeaderLobbies");
			this.titleHeaderLobbies2 = this.content.Load<Texture2D>("texture//titleHeaderLobbies2");
			this.titleHeaderMultiplayer = this.content.Load<Texture2D>("texture//titleHeaderMultiplayer");
			this.titleHeader6player = this.content.Load<Texture2D>("texture//titleHeader6Multiplayer");
			this.titleHeader4player = this.content.Load<Texture2D>("texture//titleHeader4Multiplayer");
			this.titleHeader2player = this.content.Load<Texture2D>("texture//titleHeader2player");
			this.titleHeaderJoin = this.content.Load<Texture2D>("texture//titleHeaderJoin");
			this.titleCreditBlank = this.content.Load<Texture2D>("texture//titleCreditBlank");
			this.titleCredit1 = this.content.Load<Texture2D>("texture//titleCredit1");
			this.titleCredit2 = this.content.Load<Texture2D>("texture//titleCredit2");
			this.titleHeaderAudio = this.content.Load<Texture2D>("texture//titleHeaderAudio");
			this.titleHeaderVideos = this.content.Load<Texture2D>("texture//titleHeaderVideos");
			this.titleHeaderControls = this.content.Load<Texture2D>("texture//titleHeaderControls");
			this.titleFrame = this.content.Load<Texture2D>("texture//titleFrame");
			this.titleTrailer = this.content.Load<Texture2D>("texture//titleTrailer");
			this.titlesLoaded = true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00007D90 File Offset: 0x00005F90
		public void loadFarmer()
		{
			this.farmerModel = this.conModel.Load<Model>("farmerWait1");
			this.farmerModelskull = this.conModel.Load<Model>("farmerWait1skull");
			this.farmerTexture = this.content.Load<Texture2D>("texture//Farmer01");
			this.whiteNPCModel = this.content.Load<Model>("boss//jonWalk2");
			this.whiteNPCnoarms = this.conModel.Load<Model>("jonWalknoarms");
			this.whiteNPCTexture = this.content.Load<Texture2D>("texture//jon6");
			this.whiteNPCTextureOrig = this.content.Load<Texture2D>("texture//jon6");
			this.whiteNPCdead = this.content.Load<Texture2D>("texture//jonDead2");
			this.whiteNPCTextureGreen2 = this.content.Load<Texture2D>("texture//jon6green2");
			this.blackNPCModel = this.conModel.Load<Model>("blackWalk2");
			this.blackNPCnoarms = this.conModel.Load<Model>("blackWalknoarms");
			this.blackNPCTexture = this.content.Load<Texture2D>("texture//Black2");
			this.blackNPCTextureOrig = this.content.Load<Texture2D>("texture//Black2");
			this.blackNPCdead = this.content.Load<Texture2D>("texture//jonDead2");
			this.blackNPCTextureGreen = this.content.Load<Texture2D>("texture//jon6green3");
			this.farmerNPCModel = this.conModel.Load<Model>("farmerWalk2");
			this.farmerNPCnoarms = this.conModel.Load<Model>("farmerWalknoarms");
			this.farmerNPCTexture = this.content.Load<Texture2D>("texture//farmer6");
			this.farmerNPCTextureOrig = this.content.Load<Texture2D>("texture//farmer6");
			this.farmerGreen = this.content.Load<Texture2D>("texture//FarmerGreen");
			this.farmerDead = this.content.Load<Texture2D>("texture//jonDead");
			this.skelNPCmodel = this.conModel.Load<Model>("crackWalk1");
			this.skelNPCnoarms = this.conModel.Load<Model>("crackWalk1noarms");
			this.skelNPCTexture = this.content.Load<Texture2D>("texture//skel1");
			this.skelNPCTextureOrig = this.content.Load<Texture2D>("texture//skel1");
			this.skelGreen = this.content.Load<Texture2D>("texture//skelGreen");
			this.skelDead = this.content.Load<Texture2D>("texture//skelDead");
			this.daisyNPCmodel = this.conModel.Load<Model>("girlWalk1");
			this.daisyNPCnoarms = this.conModel.Load<Model>("girlWalk1noarms");
			this.daisyNPCTexture = this.content.Load<Texture2D>("texture//daisy7orig");
			this.daisyNPCTextureOrig = this.content.Load<Texture2D>("texture//daisy7orig");
			this.daisyGreen = this.content.Load<Texture2D>("texture//daisyBlue");
			this.daisyDead = this.content.Load<Texture2D>("texture//jonDead");
			this.vikingNPCmodel = this.conModel.Load<Model>("vikingWalk1");
			this.vikingNPCnoarms = this.conModel.Load<Model>("vikingWalknoarms");
			this.vikingNPCTexture = this.content.Load<Texture2D>("texture//viking");
			this.vikingNPCTextureOrig = this.content.Load<Texture2D>("texture//viking");
			this.vikingGreen = this.content.Load<Texture2D>("texture//vikingBlue");
			this.vikingDead = this.content.Load<Texture2D>("texture//vikingdead");
			this.ghostNPCmodel = this.conModel.Load<Model>("ghostWalk1");
			this.ghostNPCTexture = this.content.Load<Texture2D>("texture//deather");
			this.strawNPCModel = this.conModel.Load<Model>("strawWalk1");
			this.strawNPCnoarms = this.conModel.Load<Model>("strawWalk1noarms");
			this.strawNPCTexture = this.content.Load<Texture2D>("texture//straw2");
			this.strawNPCTextureOrig = this.content.Load<Texture2D>("texture//straw2");
			this.strawGreen = this.content.Load<Texture2D>("texture//strawGreen");
			this.strawDead = this.content.Load<Texture2D>("texture//strawDead");
			this.robotNPCModel = this.conModel.Load<Model>("robotWalk2");
			this.robotNPCnoarms = this.conModel.Load<Model>("robotNoarms");
			this.robotNPCTexture = this.content.Load<Texture2D>("texture//Robot2");
			this.robotNPCTextureOrig = this.content.Load<Texture2D>("texture//Robot2");
			this.robotGreen = this.content.Load<Texture2D>("texture//Robot2Green");
			this.robotDead = this.content.Load<Texture2D>("texture//Robot2Dead");
			this.golemNPCModel = this.conModel.Load<Model>("golemWalk1");
			this.golemNPCnoarms = this.conModel.Load<Model>("golemWalk1noarms");
			this.golemNPCTexture = this.content.Load<Texture2D>("texture//golem1");
			this.golemNPCTextureOrig = this.content.Load<Texture2D>("texture//golem1");
			this.golemGreen = this.content.Load<Texture2D>("texture//golemGreen");
			this.golemDead = this.content.Load<Texture2D>("texture//golemDead");
			this.astroNPCModel = this.conModel.Load<Model>("astroWalk2");
			this.astroNPCnoarms = this.conModel.Load<Model>("astroWalknoarms");
			this.astroNPCTexture = this.content.Load<Texture2D>("texture//Astro2");
			this.astroNPCTextureOrig = this.content.Load<Texture2D>("texture//Astro2");
			this.astroGreen = this.content.Load<Texture2D>("texture//AstroGreen");
			this.astroDead = this.content.Load<Texture2D>("texture//AstroDead");
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00008333 File Offset: 0x00006533
		public void loadBigModel()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00008335 File Offset: 0x00006535
		public void loadModels()
		{
			this.pigAll = this.conModel.Load<Model>("cuttyAll");
			this.pigModel = this.conModel.Load<Model>("cuttyWalk");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00008364 File Offset: 0x00006564
		public void loadAudio()
		{
			this.tada3 = this.content.Load<SoundEffect>("astro\\Audio\\tadar3");
			this.grow = this.content.Load<SoundEffect>("astro\\Audio\\grow");
			this.jot1 = this.content.Load<SoundEffect>("audio\\pageturn");
			this.jot2 = this.content.Load<SoundEffect>("audio\\jot");
			this.drip = this.content.Load<SoundEffect>("audio\\drip");
			this.spinpics = this.content.Load<SoundEffect>("audio\\spinpics");
			this.menuclick = this.content.Load<SoundEffect>("audio\\menuClick");
			this.cashout = this.content.Load<SoundEffect>("audio\\cashout2");
			this.scribble = this.content.Load<SoundEffect>("audio\\scrib1");
			this.piledriver = this.content.Load<SoundEffect>("audio\\piledriver");
			this.report = this.content.Load<SoundEffect>("audio\\report2");
			this.gamestart = this.content.Load<SoundEffect>("audio\\gamestart3");
			this.ding = this.content.Load<SoundEffect>("audio\\ding");
			this.fireworks = this.content.Load<SoundEffect>("audio\\fireworks");
			this.fireworks2 = this.content.Load<SoundEffect>("audio\\fireworks2");
			this.cocking[0] = this.content.Load<SoundEffect>("audio\\gunCock");
			this.cocking[2] = this.content.Load<SoundEffect>("audio\\gunCock2");
			this.cocking[4] = this.content.Load<SoundEffect>("audio\\gunCock2");
			this.cocking[6] = this.content.Load<SoundEffect>("audio\\akCock");
			this.cocking[8] = this.content.Load<SoundEffect>("audio\\shotgunReload1");
			this.cocking[10] = this.content.Load<SoundEffect>("audio\\m16Cock");
			this.cocking[12] = this.content.Load<SoundEffect>("audio\\smgCock");
			this.cocking[14] = this.content.Load<SoundEffect>("audio\\rocketReload");
			this.cocking[16] = this.content.Load<SoundEffect>("audio\\paintballCock");
			this.cocking[18] = this.content.Load<SoundEffect>("audio\\p90Cock");
			this.cocking[20] = this.content.Load<SoundEffect>("audio\\scarCock");
			this.gunFire[0] = this.content.Load<SoundEffect>("audio\\indiePistol2");
			this.gunFire[2] = this.content.Load<SoundEffect>("audio\\pistol4");
			this.gunFire[4] = this.content.Load<SoundEffect>("audio\\pistolsilent");
			this.gunFire[6] = this.content.Load<SoundEffect>("audio\\ak7");
			this.gunFire[8] = this.content.Load<SoundEffect>("audio\\shotgunFire1");
			this.gunFire[10] = this.content.Load<SoundEffect>("audio\\m16a3");
			this.gunFire[12] = this.content.Load<SoundEffect>("audio\\smg5");
			this.gunFire[14] = this.content.Load<SoundEffect>("audio\\mirvfire");
			this.gunFire[16] = this.content.Load<SoundEffect>("audio\\paintballFire");
			this.gunFire[18] = this.content.Load<SoundEffect>("audio\\p90double");
			this.gunFire[20] = this.content.Load<SoundEffect>("audio\\scar1");
			this.gunMuffle[0] = this.content.Load<SoundEffect>("audio\\indiePistolmuffle");
			this.gunMuffle[2] = this.content.Load<SoundEffect>("audio\\pistolMuffle2");
			this.gunMuffle[4] = this.content.Load<SoundEffect>("audio\\pistolsilent2");
			this.gunMuffle[6] = this.content.Load<SoundEffect>("audio\\ak1");
			this.gunMuffle[8] = this.content.Load<SoundEffect>("audio\\shotgunFire1");
			this.gunMuffle[10] = this.content.Load<SoundEffect>("audio\\m16b");
			this.gunMuffle[12] = this.content.Load<SoundEffect>("audio\\smgfire1");
			this.gunMuffle[14] = this.content.Load<SoundEffect>("audio\\m16b");
			this.gunMuffle[16] = this.content.Load<SoundEffect>("audio\\paintballFire2");
			this.gunMuffle[18] = this.content.Load<SoundEffect>("audio\\p90bang");
			this.gunMuffle[20] = this.content.Load<SoundEffect>("audio\\scar7");
			this.gunDry[0] = this.content.Load<SoundEffect>("audio\\indieEmpty");
			this.gunDry[2] = this.content.Load<SoundEffect>("audio\\coltdry2");
			this.gunDry[4] = this.content.Load<SoundEffect>("audio\\coltdry2");
			this.gunDry[6] = this.content.Load<SoundEffect>("audio\\ak_dry");
			this.gunDry[8] = this.content.Load<SoundEffect>("audio\\shotgundry");
			this.gunDry[10] = this.content.Load<SoundEffect>("audio\\m16dry");
			this.gunDry[12] = this.content.Load<SoundEffect>("audio\\shotgundry");
			this.gunDry[14] = this.content.Load<SoundEffect>("audio\\mirvDry");
			this.gunDry[16] = this.content.Load<SoundEffect>("audio\\m16dry");
			this.gunDry[18] = this.content.Load<SoundEffect>("audio\\ak_dry");
			this.gunDry[20] = this.content.Load<SoundEffect>("audio\\scarDry");
			this.shellSound[0] = this.content.Load<SoundEffect>("audio\\shellHit");
			this.shellSound[2] = this.content.Load<SoundEffect>("audio\\shellHit");
			this.shellSound[4] = this.content.Load<SoundEffect>("audio\\shellhit4");
			this.shellSound[6] = this.content.Load<SoundEffect>("audio\\shell2Hit");
			this.shellSound[8] = this.content.Load<SoundEffect>("audio\\shellhit3");
			this.shellSound[10] = this.content.Load<SoundEffect>("audio\\shell2Hit");
			this.shellSound[12] = this.content.Load<SoundEffect>("audio\\shell2Hit");
			this.shellSound[14] = this.content.Load<SoundEffect>("audio\\shell2Hit");
			this.shellSound[16] = this.content.Load<SoundEffect>("audio\\shell2Hit");
			this.shellSound[18] = this.content.Load<SoundEffect>("audio\\shell2Hit");
			this.shellSound[20] = this.content.Load<SoundEffect>("audio\\shellHit5");
			this.shotgunPump = this.content.Load<SoundEffect>("audio\\shotgunPump");
			this.bonepop = this.content.Load<SoundEffect>("audio\\bonepop");
			this.chomp2 = this.content.Load<SoundEffect>("audio\\chomp1");
			this.dieyell = this.content.Load<SoundEffect>("audio\\death");
			this.cuttygouge = this.content.Load<SoundEffect>("audio\\cuttyGouge2");
			this.cuttyWave = this.content.Load<SoundEffect>("audio\\cuttyGouge4");
			this.buzz = this.content.Load<SoundEffect>("audio\\buzz");
			this.steps = new SoundEffect[6];
			this.steps[0] = this.content.Load<SoundEffect>("audio\\dirtStep3");
			this.steps[1] = this.content.Load<SoundEffect>("audio\\dirtStep4");
			this.steps[2] = this.content.Load<SoundEffect>("audio\\dirtStep5");
			this.steps[3] = this.content.Load<SoundEffect>("audio\\dirtStep6");
			this.steps[4] = this.content.Load<SoundEffect>("audio\\dirtStep7");
			this.steps[5] = this.content.Load<SoundEffect>("audio\\dirtStep8");
			this.crickets = this.content.Load<SoundEffect>("audio\\crickets").CreateInstance();
			this.crickets.IsLooped = true;
			this.pigExplode = new SoundEffect[4];
			this.pigExplode[0] = this.content.Load<SoundEffect>("audio\\pigExplode1");
			this.pigExplode[1] = this.content.Load<SoundEffect>("audio\\pigExplode2");
			this.pigExplode[2] = this.content.Load<SoundEffect>("audio\\pigExplode3");
			this.pigExplode[3] = this.content.Load<SoundEffect>("audio\\pigExplode4");
			this.xmas1 = this.content.Load<SoundEffect>("audio\\xmas1");
			this.xmas2 = this.content.Load<SoundEffect>("audio\\xmas2");
			this.hammer1 = this.content.Load<SoundEffect>("audio\\hammer1");
			this.doorRattle = this.content.Load<SoundEffect>("audio\\doorRattle");
			this.doorUnlock = this.content.Load<SoundEffect>("audio\\unlock");
			this.pillswallow = this.content.Load<SoundEffect>("audio\\pillswallow");
			this.pillRattler = this.content.Load<SoundEffect>("audio\\pillrattle3");
			this.pillselect = this.content.Load<SoundEffect>("audio\\pillselect");
			this.newtip = this.content.Load<SoundEffect>("audio\\message5");
			this.gusher = this.content.Load<SoundEffect>("audio\\gusher");
			this.pump = new SoundEffect[2];
			this.pump[0] = this.content.Load<SoundEffect>("audio\\pump2");
			this.pump[1] = this.content.Load<SoundEffect>("audio\\pump3");
			this.fanfare = this.content.Load<SoundEffect>("audio\\fanfare");
			this.achieve1 = this.content.Load<SoundEffect>("audio\\achv");
			this.hulkRoar2 = this.content.Load<SoundEffect>("audio\\hulkRoar2");
			this.hulkRoar = this.content.Load<SoundEffect>("audio\\hulkRoar");
			this.roar = this.content.Load<SoundEffect>("audio\\roar");
			this.crunch = this.content.Load<SoundEffect>("audio\\crunch");
			this.fence = this.content.Load<SoundEffect>("audio\\shock");
			this.crumble = new SoundEffect[3];
			this.crumble[0] = this.content.Load<SoundEffect>("audio\\crumble3");
			this.crumble[1] = this.content.Load<SoundEffect>("audio\\crumble4");
			this.crumble[2] = this.content.Load<SoundEffect>("audio\\crumble");
			this.switchweapon = this.content.Load<SoundEffect>("audio\\sWeapon");
			this.melee = this.content.Load<SoundEffect>("audio\\melee");
			this.metalHit = new SoundEffect[4];
			this.metalHit[0] = this.content.Load<SoundEffect>("audio\\metalHit1");
			this.metalHit[1] = this.content.Load<SoundEffect>("audio\\metalHit2");
			this.metalHit[2] = this.content.Load<SoundEffect>("audio\\metalHit3");
			this.metalHit[3] = this.content.Load<SoundEffect>("audio\\metalHit4");
			this.pigSqueal = new SoundEffect[4];
			this.pigSqueal[0] = this.content.Load<SoundEffect>("audio\\piggy");
			this.pigSqueal[1] = this.content.Load<SoundEffect>("audio\\piggy2");
			this.pigSqueal[2] = this.content.Load<SoundEffect>("audio\\piggy3");
			this.pigSqueal[3] = this.content.Load<SoundEffect>("audio\\piggy4");
			this.pigDie = new SoundEffect[5];
			this.pigDie[0] = this.content.Load<SoundEffect>("audio//pigDie1");
			this.pigDie[1] = this.content.Load<SoundEffect>("audio//pigDie2");
			this.pigDie[2] = this.content.Load<SoundEffect>("audio//pigDie3");
			this.pigDie[3] = this.content.Load<SoundEffect>("audio//pigDie4");
			this.pigDie[4] = this.content.Load<SoundEffect>("audio//pigDie5");
			this.allpigs = this.content.Load<SoundEffect>("audio\\pigDie15");
			this.barndoor = this.content.Load<SoundEffect>("audio\\barndoor");
			this.grenadePop1 = new SoundEffect[2];
			this.grenadePop1[0] = this.content.Load<SoundEffect>("audio\\gren6");
			this.grenadePop1[1] = this.content.Load<SoundEffect>("audio\\grenadeExplode");
			this.ring = this.content.Load<SoundEffect>("audio\\completed");
			this.sproing = new SoundEffect[2];
			this.sproing[0] = this.content.Load<SoundEffect>("audio\\sproing3");
			this.sproing[1] = this.content.Load<SoundEffect>("audio\\sproing2");
			this.chomp = this.content.Load<SoundEffect>("audio\\chomp2");
			this.ruffles = this.content.Load<SoundEffect>("audio\\ruffles");
			this.falldown = this.content.Load<SoundEffect>("audio\\falldown2");
			this.falldown2 = this.content.Load<SoundEffect>("audio\\falldown3");
			this.toneer = this.content.Load<SoundEffect>("audio\\tonewtf");
			this.dying = this.content.Load<SoundEffect>("audio\\dying2");
			this.chunk1 = this.content.Load<SoundEffect>("audio\\chunkhit1");
			this.chunk2 = this.content.Load<SoundEffect>("audio\\chunkhit2");
			this.meaty = this.content.Load<SoundEffect>("audio\\meat");
			this.manyell = this.content.Load<SoundEffect>("audio\\manyell");
			this.hurt = this.content.Load<SoundEffect>("audio\\hurt");
			this.grabby = this.content.Load<SoundEffect>("audio\\grabWeaponZ");
			this.chain = new SoundEffects(2, true);
			this.chain.sound[0] = this.content.Load<SoundEffect>("audio\\chainRattle").CreateInstance();
			this.chain.sound[1] = this.content.Load<SoundEffect>("audio\\chainRattle").CreateInstance();
			this.ricochete = new SoundEffect[3];
			this.ricochete[0] = this.content.Load<SoundEffect>("audio\\ric6");
			this.ricochete[1] = this.content.Load<SoundEffect>("audio\\ric7");
			this.ricochete[2] = this.content.Load<SoundEffect>("audio\\ric8");
			this.abort = this.content.Load<SoundEffect>("audio\\abort");
			this.harp2 = this.content.Load<SoundEffect>("audio\\harp2");
			this.humm = this.content.Load<SoundEffect>("audio\\humm");
			this.switch2 = this.content.Load<SoundEffect>("audio\\switch2");
			this.lightClick = this.content.Load<SoundEffect>("audio\\switch");
			this.grinder = this.content.Load<SoundEffect>("audio\\grind");
			this.grinderMotor = new SoundEffects(1);
			this.grinderMotor.sound[0] = this.content.Load<SoundEffect>("audio\\grinderMotor").CreateInstance();
			this.grinderMotor.sound[0].IsLooped = true;
			this.pickup1 = this.content.Load<SoundEffect>("audio\\pickup1");
			this.pickup2 = this.content.Load<SoundEffect>("audio\\pickup2");
			this.pickupGrenade = this.content.Load<SoundEffect>("audio\\pickupMain");
			this.drinkMilk = this.content.Load<SoundEffect>("audio\\drinkmilk");
			this.mirvDrop = this.content.Load<SoundEffect>("audio\\build4");
			this.buttonPress = this.content.Load<SoundEffect>("audio\\buttonPress");
			this.buttonDeny = this.content.Load<SoundEffect>("audio\\buttonDeny");
			this.buttonPackage = this.content.Load<SoundEffect>("audio\\buttonPackage");
			this.boarbite = new SoundEffect[5];
			this.boarbite[0] = this.content.Load<SoundEffect>("audio\\bite6");
			this.boarbite[1] = this.content.Load<SoundEffect>("audio\\bite7");
			this.boarbite[2] = this.content.Load<SoundEffect>("audio\\bite8");
			this.boarbite[3] = this.content.Load<SoundEffect>("audio\\bite1");
			this.boarbite[4] = this.content.Load<SoundEffect>("audio\\bite2");
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000093B8 File Offset: 0x000075B8
		public void loadBarns()
		{
			this.barnRGB = this.content.Load<Texture2D>("texture//barnRGB");
			this.barnShadow = this.content.Load<Texture2D>("texture//barnShadow");
			this.barnsLoaded = true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000093F0 File Offset: 0x000075F0
		public void loadMountBuilding()
		{
			this.grassTexture = this.content.Load<Texture2D>("texture//grassTexture");
			this.buildingRGB = this.content.Load<Texture2D>("texture//buildingRGB");
			this.buildingShadow = this.content.Load<Texture2D>("texture//buildingShadow");
			this.buildingRGBNight = this.content.Load<Texture2D>("texture//buildingRGBpm");
			this.buildingShadowNight = this.content.Load<Texture2D>("texture//buildingShadowpm");
			this.mountainBuildingLoaded = true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00009474 File Offset: 0x00007674
		public void loadGUNS()
		{
			this.gunTextures[0] = this.content.Load<Texture2D>("texture//indiepistol2");
			this.gunTextures[2] = this.content.Load<Texture2D>("texture//Colt2");
			this.gunTextures[4] = this.content.Load<Texture2D>("texture//Silentp");
			this.gunTextures[6] = this.content.Load<Texture2D>("texture//AK47");
			this.gunTextures[8] = this.content.Load<Texture2D>("texture//shotgun3");
			this.gunTextures[10] = this.content.Load<Texture2D>("texture//m16map2");
			this.gunTextures[12] = this.content.Load<Texture2D>("texture//uzi2");
			this.gunTextures[14] = this.content.Load<Texture2D>("texture//bfg1");
			this.gunTextures[16] = this.content.Load<Texture2D>("texture//paintballgun");
			this.gunTextures[18] = this.content.Load<Texture2D>("texture//abc4");
			this.gunTextures[20] = this.content.Load<Texture2D>("texture//scarTexture");
			this.hatTextures[0] = this.content.Load<Texture2D>("texture//hat1");
			this.hatTextures[1] = this.content.Load<Texture2D>("texture//hat2");
			this.hatTextures[2] = this.content.Load<Texture2D>("texture//hat3");
			this.hatTextures[3] = this.content.Load<Texture2D>("texture//hat4");
			this.hatTextures[4] = this.content.Load<Texture2D>("texture//hat5");
			this.hatTextures[5] = this.content.Load<Texture2D>("texture//hat6");
			this.hatTextures[6] = this.content.Load<Texture2D>("texture//hat7");
			this.hatTextures[7] = this.content.Load<Texture2D>("texture//hat8");
			this.hatTextures[8] = this.content.Load<Texture2D>("texture//hat9");
			this.hatTextures[9] = this.content.Load<Texture2D>("texture//hat10");
			this.hatTextures[10] = this.content.Load<Texture2D>("texture//hat11");
			this.hatTextures[11] = this.content.Load<Texture2D>("texture//hat12");
			this.hatTextures[12] = this.content.Load<Texture2D>("texture//hat13");
			this.hatTextures[13] = this.content.Load<Texture2D>("texture//hat14");
			this.hatTextures[14] = this.content.Load<Texture2D>("texture//hat15");
			this.hatTextures[15] = this.content.Load<Texture2D>("texture//hat15");
			this.goldkeyTexture = this.content.Load<Texture2D>("texture//goldkey");
			this.blimpTexture = this.content.Load<Texture2D>("texture//blimp");
			this.moonTexture = this.content.Load<Texture2D>("texture//moon");
			this.hatEffect = this.content.Load<Effect>("effects//hateffect");
			this.hatTransX = new Vector3[11, 16];
			this.hatRotX = new Vector3[11, 16];
			this.hatScaleX = new float[11, 16];
			this.setHatMatrix();
			if (this.debug_show)
			{
				this.setTempHatMatrix(ref this.temphatMatrix);
			}
			this.loadGuns = true;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000097BC File Offset: 0x000079BC
		public void setHatMatrix()
		{
			this.hatMatrix[0, 0] = Matrix.CreateRotationZ(-0.06f) * Matrix.CreateRotationY(-0.05f) * Matrix.CreateRotationX(-0.1f) * Matrix.CreateScale(0.9f) * Matrix.CreateTranslation(0.447f, 52.717f, 1.22f);
			this.hatMatrix[0, 1] = Matrix.CreateRotationZ(-0.118f) * Matrix.CreateRotationY(0.158f) * Matrix.CreateRotationX(-0.117f) * Matrix.CreateScale(1.07f) * Matrix.CreateTranslation(0.455f, 51.295f, 1.425f);
			this.hatMatrix[0, 2] = Matrix.CreateRotationZ(-0.119f) * Matrix.CreateRotationY(0.022f) * Matrix.CreateRotationX(0.053f) * Matrix.CreateScale(1.13f) * Matrix.CreateTranslation(0.56f, 50.96f, 1.03f);
			this.hatMatrix[0, 3] = Matrix.CreateRotationZ(-0.156f) * Matrix.CreateRotationY(0.177f) * Matrix.CreateRotationX(-0.149f) * Matrix.CreateScale(1.034f) * Matrix.CreateTranslation(0.531f, 52.27f, 1.055f);
			this.hatMatrix[0, 4] = Matrix.CreateRotationZ(-0.123f) * Matrix.CreateRotationY(0.176f) * Matrix.CreateRotationX(-0.216f) * Matrix.CreateScale(1.18f) * Matrix.CreateTranslation(0.64f, 51.279f, 2.141f);
			this.hatMatrix[0, 5] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.7f, 50.6f, 2.2f);
			this.hatMatrix[0, 6] = Matrix.CreateRotationZ(-0.197f) * Matrix.CreateRotationY(0.121f) * Matrix.CreateRotationX(0.35f) * Matrix.CreateScale(1.01f) * Matrix.CreateTranslation(0.006f, 48.432f, 2.28f);
			this.hatMatrix[0, 7] = Matrix.CreateRotationZ(-0.11f) * Matrix.CreateRotationY(0.074f) * Matrix.CreateRotationX(-0.05f) * Matrix.CreateScale(0.85f) * Matrix.CreateTranslation(0.96f, 48.29f, 3.713f);
			this.hatMatrix[0, 8] = Matrix.CreateRotationZ(-0.08f) * Matrix.CreateRotationY(0.02f) * Matrix.CreateRotationX(-0.09f) * Matrix.CreateScale(0.9f) * Matrix.CreateTranslation(0.65f, 52.49f, 1.28f);
			this.hatMatrix[0, 9] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.17f, 50.52f, 2.2f);
			this.hatMatrix[0, 10] = Matrix.CreateRotationZ(-0.08f) * Matrix.CreateRotationY(0.21f) * Matrix.CreateRotationX(-0.16f) * Matrix.CreateScale(1.01f) * Matrix.CreateTranslation(0.67f, 52.19f, 1.27f);
			this.hatMatrix[0, 11] = Matrix.CreateRotationZ(-0.08f) * Matrix.CreateRotationY(0.02f) * Matrix.CreateRotationX(-0.09f) * Matrix.CreateScale(0.9f) * Matrix.CreateTranslation(0.65f, 52.49f, 1.28f);
			this.hatMatrix[0, 12] = this.hatMatrix[0, 2];
			this.hatMatrix[0, 13] = this.hatMatrix[0, 8];
			this.hatMatrix[0, 14] = this.hatMatrix[0, 0];
			this.hatMatrix[1, 0] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(0.4f, 51.1f, 1.5f);
			this.hatMatrix[1, 1] = Matrix.CreateRotationZ(-0.15f) * Matrix.CreateRotationY(0.08f) * Matrix.CreateRotationX(0.02f) * Matrix.CreateScale(0.98f) * Matrix.CreateTranslation(0.49f, 50.8f, 1.4f);
			this.hatMatrix[1, 2] = Matrix.CreateRotationZ(-0.078f) * Matrix.CreateRotationY(0.001f) * Matrix.CreateRotationX(-0.01f) * Matrix.CreateScale(0.96f) * Matrix.CreateTranslation(0.5f, 50.6f, 1.5f);
			this.hatMatrix[1, 3] = Matrix.CreateRotationZ(-0.133f) * Matrix.CreateRotationY(0.11f) * Matrix.CreateRotationX(-0.043f) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(0.4f, 51.1f, 1.5f);
			this.hatMatrix[1, 4] = Matrix.CreateRotationZ(-0.066f) * Matrix.CreateRotationY(0.12f) * Matrix.CreateRotationX(-0.036f) * Matrix.CreateScale(1.102f) * Matrix.CreateTranslation(0.381f, 50.635f, 1.874f);
			this.hatMatrix[1, 5] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(0.7f, 50.7f, 2.09f);
			this.hatMatrix[1, 6] = Matrix.CreateRotationZ(-0.179f) * Matrix.CreateRotationY(0.093f) * Matrix.CreateRotationX(0.19f) * Matrix.CreateScale(0.93f) * Matrix.CreateTranslation(0.03f, 48.19f, 2.457f);
			this.hatMatrix[1, 7] = Matrix.CreateRotationZ(-0.145f) * Matrix.CreateRotationY(0.074f) * Matrix.CreateRotationX(-0.093f) * Matrix.CreateScale(0.92f) * Matrix.CreateTranslation(0.1f, 48.2f, 3.618f);
			this.hatMatrix[1, 8] = Matrix.CreateRotationZ(-0.13f) * Matrix.CreateRotationY(0.03f) * Matrix.CreateRotationX(-0.03f) * Matrix.CreateScale(0.89f) * Matrix.CreateTranslation(0.4f, 51.1f, 1.5f);
			this.hatMatrix[1, 9] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(-0.02f) * Matrix.CreateRotationX(-0.08f) * Matrix.CreateScale(0.9f) * Matrix.CreateTranslation(0.422f, 50.533f, 2.278f);
			this.hatMatrix[1, 10] = Matrix.CreateRotationZ(-0.14f) * Matrix.CreateRotationY(0.13f) * Matrix.CreateRotationX(-0.13f) * Matrix.CreateScale(0.98f) * Matrix.CreateTranslation(0.56f, 51.63f, 1.5f);
			this.hatMatrix[1, 11] = Matrix.CreateRotationZ(-0.13f) * Matrix.CreateRotationY(0.03f) * Matrix.CreateRotationX(-0.03f) * Matrix.CreateScale(0.89f) * Matrix.CreateTranslation(0.4f, 51.1f, 1.5f);
			this.hatMatrix[1, 12] = this.hatMatrix[1, 2];
			this.hatMatrix[1, 13] = this.hatMatrix[1, 8];
			this.hatMatrix[1, 14] = this.hatMatrix[1, 0];
			this.hatMatrix[2, 0] = Matrix.CreateRotationZ(-0.073f) * Matrix.CreateRotationY(0.015f) * Matrix.CreateRotationX(-0.101f) * Matrix.CreateScale(1.02f) * Matrix.CreateTranslation(-0.015f, 51.327f, 1.335f);
			this.hatMatrix[2, 1] = Matrix.CreateRotationZ(-0.016f) * Matrix.CreateRotationY(0.004f) * Matrix.CreateRotationX(-0.177f) * Matrix.CreateScale(0.92f) * Matrix.CreateTranslation(0.1f, 51.3f, 1.2f);
			this.hatMatrix[2, 2] = Matrix.CreateRotationZ(-0.052f) * Matrix.CreateRotationY(0.071f) * Matrix.CreateRotationX(-0.01f) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(0f, 50.9f, 1.2f);
			this.hatMatrix[2, 3] = Matrix.CreateRotationZ(-0.14f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.02f) * Matrix.CreateTranslation(0.1f, 51.4f, 1.5f);
			this.hatMatrix[2, 4] = Matrix.CreateRotationZ(-0.025f) * Matrix.CreateRotationY(0.087f) * Matrix.CreateRotationX(-0.092f) * Matrix.CreateScale(1.02f) * Matrix.CreateTranslation(-0.005f, 51.2f, 1.613f);
			this.hatMatrix[2, 5] = Matrix.CreateRotationZ(-0.14f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.9f) * Matrix.CreateTranslation(0.2f, 50.7f, 2f);
			this.hatMatrix[2, 6] = Matrix.CreateRotationZ(-0.004f) * Matrix.CreateRotationY(0.075f) * Matrix.CreateRotationX(0.11f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(0f, 48.3f, 2.4f);
			this.hatMatrix[2, 7] = Matrix.CreateRotationZ(-0.14f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(0f, 48.3f, 3.613f);
			this.hatMatrix[2, 8] = Matrix.CreateRotationZ(0.01f) * Matrix.CreateRotationY(-0.15f) * Matrix.CreateRotationX(-0.09f) * Matrix.CreateScale(0.86f) * Matrix.CreateTranslation(0.1f, 51.4f, 1.31f);
			this.hatMatrix[2, 9] = Matrix.CreateRotationZ(-0.14f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.9f) * Matrix.CreateTranslation(-0.111f, 50.152f, 2.238f);
			this.hatMatrix[2, 10] = Matrix.CreateRotationZ(-0.02f) * Matrix.CreateRotationY(0.07f) * Matrix.CreateRotationX(-0.14f) * Matrix.CreateScale(1.02f) * Matrix.CreateTranslation(-0.09f, 51.18f, 1.243f);
			this.hatMatrix[2, 11] = Matrix.CreateRotationZ(0.01f) * Matrix.CreateRotationY(-0.15f) * Matrix.CreateRotationX(-0.09f) * Matrix.CreateScale(0.86f) * Matrix.CreateTranslation(0.1f, 51.4f, 1.31f);
			this.hatMatrix[2, 12] = this.hatMatrix[2, 2];
			this.hatMatrix[2, 13] = this.hatMatrix[2, 8];
			this.hatMatrix[2, 14] = this.hatMatrix[2, 0];
			this.hatMatrix[3, 0] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.02f) * Matrix.CreateTranslation(0.6f, 52.5f, 1f);
			this.hatMatrix[3, 1] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(0.6f, 51.8f, 0.7f);
			this.hatMatrix[3, 2] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.02f) * Matrix.CreateTranslation(0.6f, 51.6f, 0.9f);
			this.hatMatrix[3, 3] = Matrix.CreateRotationZ(-0.08f) * Matrix.CreateRotationY(0.03f) * Matrix.CreateRotationX(-0.22f) * Matrix.CreateScale(1.07f) * Matrix.CreateTranslation(0.6f, 52f, 1.05f);
			this.hatMatrix[3, 4] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.1f) * Matrix.CreateTranslation(0.6f, 51.9f, 1.8f);
			this.hatMatrix[3, 5] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.9f) * Matrix.CreateTranslation(0.6f, 52f, 1.4f);
			this.hatMatrix[3, 6] = Matrix.CreateRotationZ(-0.129f) * Matrix.CreateRotationY(-0.011f) * Matrix.CreateRotationX(0.111f) * Matrix.CreateScale(0.96f) * Matrix.CreateTranslation(0.2f, 48.8f, 2.2f);
			this.hatMatrix[3, 7] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.96f) * Matrix.CreateTranslation(0.2f, 48.8f, 2.2f);
			this.hatMatrix[3, 8] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.892f) * Matrix.CreateTranslation(0.553f, 52.556f, 0.966f);
			this.hatMatrix[3, 9] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(-0.13f) * Matrix.CreateRotationX(-0.3f) * Matrix.CreateScale(0.97f) * Matrix.CreateTranslation(0.46f, 51f, 1.95f);
			this.hatMatrix[3, 10] = Matrix.CreateRotationZ(-0.1f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.97f) * Matrix.CreateTranslation(0.6f, 52.33f, 1.13f);
			this.hatMatrix[3, 11] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.892f) * Matrix.CreateTranslation(0.553f, 52.556f, 0.966f);
			this.hatMatrix[3, 12] = this.hatMatrix[3, 2];
			this.hatMatrix[3, 13] = this.hatMatrix[3, 8];
			this.hatMatrix[3, 14] = this.hatMatrix[3, 0];
			this.hatMatrix[4, 0] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.514f, 51.891f, 2.25f);
			this.hatMatrix[4, 1] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.7f, 51.3f, 1.5f);
			this.hatMatrix[4, 2] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.7f, 51.1f, 2f);
			this.hatMatrix[4, 3] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.7f, 52f, 2.2f);
			this.hatMatrix[4, 4] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.1f) * Matrix.CreateTranslation(0.6f, 51.5f, 2.6f);
			this.hatMatrix[4, 5] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(-0.279f) * Matrix.CreateScale(1.026f) * Matrix.CreateTranslation(0.812f, 50.808f, 2.632f);
			this.hatMatrix[4, 6] = Matrix.CreateRotationZ(-0.13f) * Matrix.CreateRotationY(0.03f) * Matrix.CreateRotationX(0.1f) * Matrix.CreateScale(1.01f) * Matrix.CreateTranslation(0.4f, 48.34f, 3.1f);
			this.hatMatrix[4, 7] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.98f) * Matrix.CreateTranslation(0.4f, 48.4f, 3.1f);
			this.hatMatrix[4, 8] = Matrix.CreateRotationZ(-0.092f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0.129f) * Matrix.CreateScale(0.885f) * Matrix.CreateTranslation(0.788f, 52.403f, 2.157f);
			this.hatMatrix[4, 9] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.68f, 50.43f, 2.7f);
			this.hatMatrix[4, 10] = Matrix.CreateRotationZ(-0.08f) * Matrix.CreateRotationY(0.02f) * Matrix.CreateRotationX(-0.02f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.8f, 52.05f, 1.67f);
			this.hatMatrix[4, 11] = Matrix.CreateRotationZ(-0.092f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0.129f) * Matrix.CreateScale(0.885f) * Matrix.CreateTranslation(0.788f, 52.403f, 2.157f);
			this.hatMatrix[4, 12] = this.hatMatrix[4, 2];
			this.hatMatrix[4, 13] = this.hatMatrix[4, 8];
			this.hatMatrix[4, 14] = this.hatMatrix[4, 0];
			this.hatMatrix[5, 0] = Matrix.CreateRotationZ(-0.056f) * Matrix.CreateRotationY(0.012f) * Matrix.CreateRotationX(0.093f) * Matrix.CreateScale(1.1f) * Matrix.CreateTranslation(0.1f, 52.8f, 1.4f);
			this.hatMatrix[5, 1] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(-0.03f) * Matrix.CreateRotationX(-0.07f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(0f, 52.9f, 1.27f);
			this.hatMatrix[5, 2] = Matrix.CreateRotationZ(0.01f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(-0.06f) * Matrix.CreateScale(1.07f) * Matrix.CreateTranslation(0.19f, 52.61f, 1.53f);
			this.hatMatrix[5, 3] = Matrix.CreateRotationZ(-0.05f) * Matrix.CreateRotationY(0.04f) * Matrix.CreateRotationX(-0.29f) * Matrix.CreateScale(1.1f) * Matrix.CreateTranslation(-0.05f, 52.54f, 1.47f);
			this.hatMatrix[5, 4] = Matrix.CreateRotationZ(-0.04f) * Matrix.CreateRotationY(0.06f) * Matrix.CreateRotationX(0.07f) * Matrix.CreateScale(1.15f) * Matrix.CreateTranslation(-0.04f, 52.1f, 2f);
			this.hatMatrix[5, 5] = Matrix.CreateRotationZ(-0.14f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.02f) * Matrix.CreateTranslation(0.2f, 51.9f, 1.9f);
			this.hatMatrix[5, 6] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0.06f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(-0.1f, 49.4f, 2.38f);
			this.hatMatrix[5, 7] = Matrix.CreateRotationZ(-0.14f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(-0.1f, 49.4f, 2.6f);
			this.hatMatrix[5, 8] = Matrix.CreateRotationZ(-0.01f) * Matrix.CreateRotationY(0.19f) * Matrix.CreateRotationX(0.05f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(0.1f, 52.8f, 1.4f);
			this.hatMatrix[5, 9] = Matrix.CreateRotationZ(0.01f) * Matrix.CreateRotationY(-0.07f) * Matrix.CreateRotationX(0.04f) * Matrix.CreateScale(1.02f) * Matrix.CreateTranslation(-0.17f, 51.55f, 1.9f);
			this.hatMatrix[5, 10] = Matrix.CreateRotationZ(0.05f) * Matrix.CreateRotationY(0.06f) * Matrix.CreateRotationX(-0.1f) * Matrix.CreateScale(1.15f) * Matrix.CreateTranslation(0.01f, 53.11f, 1.7f);
			this.hatMatrix[5, 11] = Matrix.CreateRotationZ(-0.01f) * Matrix.CreateRotationY(0.19f) * Matrix.CreateRotationX(0.05f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(0.1f, 52.8f, 1.4f);
			this.hatMatrix[5, 12] = this.hatMatrix[5, 2];
			this.hatMatrix[5, 13] = this.hatMatrix[5, 8];
			this.hatMatrix[5, 14] = this.hatMatrix[5, 0];
			this.hatMatrix[6, 0] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.84f) * Matrix.CreateTranslation(0.6f, 51.4f, 2.2f);
			this.hatMatrix[6, 1] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.7f, 51.3f, 1.5f);
			this.hatMatrix[6, 2] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.7f, 51.1f, 2f);
			this.hatMatrix[6, 3] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.7f, 52f, 2.2f);
			this.hatMatrix[6, 4] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.1f) * Matrix.CreateTranslation(0.6f, 51.5f, 2.6f);
			this.hatMatrix[6, 5] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.7f, 50.6f, 2.7f);
			this.hatMatrix[6, 6] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.98f) * Matrix.CreateTranslation(0.4f, 48.4f, 3.1f);
			this.hatMatrix[6, 7] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(0.2f, 48.5f, 3.1f);
			this.hatMatrix[6, 8] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.84f) * Matrix.CreateTranslation(0.6f, 51.4f, 2.2f);
			this.hatMatrix[6, 9] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.7f, 50.6f, 2.7f);
			this.hatMatrix[6, 10] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.06f) * Matrix.CreateTranslation(0.7f, 52f, 2.2f);
			this.hatMatrix[6, 11] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.84f) * Matrix.CreateTranslation(0.6f, 51.4f, 2.2f);
			this.hatMatrix[6, 12] = this.hatMatrix[6, 2];
			this.hatMatrix[6, 13] = this.hatMatrix[6, 8];
			this.hatMatrix[6, 14] = this.hatMatrix[6, 0];
			this.hatMatrix[7, 0] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.1f) * Matrix.CreateTranslation(0.1f, 52.8f, 1.4f);
			this.hatMatrix[7, 1] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(0f, 52.9f, 1f);
			this.hatMatrix[7, 2] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(0f, 52.9f, 1f);
			this.hatMatrix[7, 3] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.1f) * Matrix.CreateTranslation(0.1f, 52.8f, 1.4f);
			this.hatMatrix[7, 4] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.1f) * Matrix.CreateTranslation(0.1f, 52.1f, 2f);
			this.hatMatrix[7, 5] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.02f) * Matrix.CreateTranslation(0.2f, 51.9f, 1.9f);
			this.hatMatrix[7, 6] = Matrix.CreateRotationZ(-0.05f) * Matrix.CreateRotationY(0.02f) * Matrix.CreateRotationX(0.13f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(-0.1f, 49.4f, 2.6f);
			this.hatMatrix[7, 7] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.94f) * Matrix.CreateTranslation(-0.1f, 49.4f, 2.6f);
			this.hatMatrix[7, 8] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0.217f) * Matrix.CreateScale(0.588f) * Matrix.CreateTranslation(0.1f, 53.567f, 1.635f);
			this.hatMatrix[7, 9] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.02f) * Matrix.CreateTranslation(-0.21f, 50.87f, 2.28f);
			this.hatMatrix[7, 10] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(0.97f) * Matrix.CreateTranslation(-0.05f, 52.6f, 1.4f);
			this.hatMatrix[7, 11] = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationX(0.217f) * Matrix.CreateScale(0.588f) * Matrix.CreateTranslation(0.1f, 53.567f, 1.635f);
			this.hatMatrix[7, 12] = this.hatMatrix[7, 2];
			this.hatMatrix[7, 13] = this.hatMatrix[7, 8];
			this.hatMatrix[7, 14] = this.hatMatrix[7, 0];
			this.hatMatrix[8, 0] = Matrix.CreateRotationZ(-0.06f) * Matrix.CreateRotationY(0.21f) * Matrix.CreateRotationX(0.11f) * Matrix.CreateScale(1.15f) * Matrix.CreateTranslation(0.62f, 54.41f, 0.39f);
			this.hatMatrix[8, 1] = Matrix.CreateRotationZ(-0.04f) * Matrix.CreateRotationY(0.19f) * Matrix.CreateRotationX(0.03f) * Matrix.CreateScale(1.22f) * Matrix.CreateTranslation(0.59f, 54.45f, -0.44f);
			this.hatMatrix[8, 2] = Matrix.CreateRotationZ(-0.08f) * Matrix.CreateRotationY(0.22f) * Matrix.CreateRotationX(-0.08f) * Matrix.CreateScale(1.82f) * Matrix.CreateTranslation(0.55f, 51.63f, 0.25f);
			this.hatMatrix[8, 3] = Matrix.CreateRotationZ(-0.08f) * Matrix.CreateRotationY(0.25f) * Matrix.CreateRotationX(0.2f) * Matrix.CreateScale(1.3f) * Matrix.CreateTranslation(0.99f, 55.06f, 1.2f);
			this.hatMatrix[8, 4] = Matrix.CreateRotationZ(-0.1f) * Matrix.CreateRotationY(0.15f) * Matrix.CreateRotationX(0.23f) * Matrix.CreateScale(1.66f) * Matrix.CreateTranslation(0.92f, 54.35f, 0.24f);
			this.hatMatrix[8, 5] = Matrix.CreateRotationZ(0.05f) * Matrix.CreateRotationY(6.49f) * Matrix.CreateRotationX(-0.19f) * Matrix.CreateScale(1.52f) * Matrix.CreateTranslation(0.71f, 52.19f, 0.33f);
			this.hatMatrix[8, 6] = Matrix.CreateRotationZ(-0.07f) * Matrix.CreateRotationY(0.22f) * Matrix.CreateRotationX(0.06f) * Matrix.CreateScale(1.92f) * Matrix.CreateTranslation(0.98f, 46.53f, 2.19f);
			this.hatMatrix[8, 7] = Matrix.CreateRotationZ(-0.05f) * Matrix.CreateRotationY(0.19f) * Matrix.CreateRotationX(-0.18f) * Matrix.CreateScale(1.2f) * Matrix.CreateTranslation(0.49f, 48.13f, 2.92f);
			this.hatMatrix[8, 8] = Matrix.CreateRotationZ(-0.118f) * Matrix.CreateRotationY(0.555f) * Matrix.CreateRotationX(0.09f) * Matrix.CreateScale(0.588f) * Matrix.CreateTranslation(0.615f, 54.355f, 0.733f);
			this.hatMatrix[8, 9] = Matrix.CreateRotationZ(-0.04f) * Matrix.CreateRotationY(6.41f) * Matrix.CreateRotationX(-0.13f) * Matrix.CreateScale(1.52f) * Matrix.CreateTranslation(0.64f, 51.33f, 1.79f);
			this.hatMatrix[8, 10] = Matrix.CreateRotationZ(-0.06f) * Matrix.CreateRotationY(0.22f) * Matrix.CreateRotationX(0f) * Matrix.CreateScale(1.3f) * Matrix.CreateTranslation(0.93f, 54.83f, 1.04f);
			this.hatMatrix[8, 11] = Matrix.CreateRotationZ(-0.118f) * Matrix.CreateRotationY(0.555f) * Matrix.CreateRotationX(0.09f) * Matrix.CreateScale(0.588f) * Matrix.CreateTranslation(0.615f, 54.355f, 0.733f);
			this.hatMatrix[8, 12] = this.hatMatrix[8, 2];
			this.hatMatrix[8, 13] = this.hatMatrix[8, 8];
			this.hatMatrix[8, 14] = this.hatMatrix[8, 0];
			this.hatMatrix[9, 0] = Matrix.CreateRotationZ(-0.06f) * Matrix.CreateRotationY(0.037f) * Matrix.CreateRotationX(-0.007f) * Matrix.CreateScale(0.351f) * Matrix.CreateTranslation(0.207f, 53.021f, 1.758f);
			this.hatMatrix[9, 1] = Matrix.CreateRotationZ(-0.04f) * Matrix.CreateRotationY(0.092f) * Matrix.CreateRotationX(-0.177f) * Matrix.CreateScale(0.388f) * Matrix.CreateTranslation(0.138f, 52.637f, 1.519f);
			this.hatMatrix[9, 2] = Matrix.CreateRotationZ(-0.011f) * Matrix.CreateRotationY(0.033f) * Matrix.CreateRotationX(-0.371f) * Matrix.CreateScale(0.805f) * Matrix.CreateTranslation(0.192f, 51.125f, 0.674f);
			this.hatMatrix[9, 3] = Matrix.CreateRotationZ(-0.108f) * Matrix.CreateRotationY(0.063f) * Matrix.CreateRotationX(-0.001f) * Matrix.CreateScale(0.543f) * Matrix.CreateTranslation(0.241f, 52.212f, 1.986f);
			this.hatMatrix[9, 4] = Matrix.CreateRotationZ(-0.064f) * Matrix.CreateRotationY(0.031f) * Matrix.CreateRotationX(0.112f) * Matrix.CreateScale(0.612f) * Matrix.CreateTranslation(0.152f, 51.922f, 2.251f);
			this.hatMatrix[9, 5] = Matrix.CreateRotationZ(0.05f) * Matrix.CreateRotationY(6.26f) * Matrix.CreateRotationX(0.035f) * Matrix.CreateScale(0.473f) * Matrix.CreateTranslation(0.296f, 51.88f, 2.275f);
			this.hatMatrix[9, 6] = Matrix.CreateRotationZ(-0.055f) * Matrix.CreateRotationY(-0.053f) * Matrix.CreateRotationX(-0.211f) * Matrix.CreateScale(1.048f) * Matrix.CreateTranslation(0.158f, 47.864f, 2.599f);
			this.hatMatrix[9, 7] = Matrix.CreateRotationZ(-0.167f) * Matrix.CreateRotationY(0.01f) * Matrix.CreateRotationX(-0.01f) * Matrix.CreateScale(0.75f) * Matrix.CreateTranslation(0.007f, 47.829f, 4.321f);
			this.hatMatrix[9, 8] = Matrix.CreateRotationZ(-0.56f) * Matrix.CreateRotationY(-0.191f) * Matrix.CreateRotationX(-0.037f) * Matrix.CreateScale(0.247f) * Matrix.CreateTranslation(0.849f, 52.831f, 2.174f);
			this.hatMatrix[9, 9] = Matrix.CreateRotationZ(-0.04f) * Matrix.CreateRotationY(6.41f) * Matrix.CreateRotationX(-0.032f) * Matrix.CreateScale(0.512f) * Matrix.CreateTranslation(0.154f, 51.673f, 2.431f);
			this.hatMatrix[9, 10] = Matrix.CreateRotationZ(-0.06f) * Matrix.CreateRotationY(-0.04f) * Matrix.CreateRotationX(-0.096f) * Matrix.CreateScale(0.732f) * Matrix.CreateTranslation(0.173f, 52.535f, 1.964f);
			this.hatMatrix[9, 11] = Matrix.CreateRotationZ(-0.56f) * Matrix.CreateRotationY(-0.191f) * Matrix.CreateRotationX(-0.037f) * Matrix.CreateScale(0.247f) * Matrix.CreateTranslation(0.849f, 52.831f, 2.174f);
			this.hatMatrix[9, 12] = this.hatMatrix[9, 2];
			this.hatMatrix[9, 13] = this.hatMatrix[9, 8];
			this.hatMatrix[9, 14] = this.hatMatrix[9, 0];
			this.hatMatrix[10, 0] = Matrix.CreateRotationZ(-0.06f) * Matrix.CreateRotationY(-0.003f) * Matrix.CreateRotationX(-0.018f) * Matrix.CreateScale(0.364f) * Matrix.CreateTranslation(0.603f, 53.857f, 1.474f);
			this.hatMatrix[10, 1] = Matrix.CreateRotationZ(-0.006f) * Matrix.CreateRotationY(-0.019f) * Matrix.CreateRotationX(-0.565f) * Matrix.CreateScale(0.477f) * Matrix.CreateTranslation(0.274f, 53.233f, -0.257f);
			this.hatMatrix[10, 2] = Matrix.CreateRotationZ(-0.08f) * Matrix.CreateRotationY(0.02f) * Matrix.CreateRotationX(-0.08f) * Matrix.CreateScale(1.244f) * Matrix.CreateTranslation(0.188f, 51.73f, 1.03f);
			this.hatMatrix[10, 3] = Matrix.CreateRotationZ(-0.08f) * Matrix.CreateRotationY(0.121f) * Matrix.CreateRotationX(0.134f) * Matrix.CreateScale(0.855f) * Matrix.CreateTranslation(0.098f, 53.298f, 1.645f);
			this.hatMatrix[10, 4] = Matrix.CreateRotationZ(-0.091f) * Matrix.CreateRotationY(-0.406f) * Matrix.CreateRotationX(0.287f) * Matrix.CreateScale(0.494f) * Matrix.CreateTranslation(0.834f, 53.76f, 1.535f);
			this.hatMatrix[10, 5] = Matrix.CreateRotationZ(0.256f) * Matrix.CreateRotationY(6.307f) * Matrix.CreateRotationX(-0.304f) * Matrix.CreateScale(0.916f) * Matrix.CreateTranslation(0.101f, 51.522f, 0.847f);
			this.hatMatrix[10, 6] = Matrix.CreateRotationZ(-0.064f) * Matrix.CreateRotationY(-0.016f) * Matrix.CreateRotationX(0.178f) * Matrix.CreateScale(1.187f) * Matrix.CreateTranslation(0.017f, 46.788f, 2.261f);
			this.hatMatrix[10, 7] = Matrix.CreateRotationZ(-0.05f) * Matrix.CreateRotationY(0.105f) * Matrix.CreateRotationX(0.037f) * Matrix.CreateScale(0.927f) * Matrix.CreateTranslation(0.417f, 48.328f, 3.541f);
			this.hatMatrix[10, 8] = Matrix.CreateRotationZ(-0.118f) * Matrix.CreateRotationY(-0.168f) * Matrix.CreateRotationX(0.09f) * Matrix.CreateScale(0.742f) * Matrix.CreateTranslation(0.817f, 53.551f, 1.557f);
			this.hatMatrix[10, 9] = Matrix.CreateRotationZ(-0.04f) * Matrix.CreateRotationY(6.41f) * Matrix.CreateRotationX(-0.13f) * Matrix.CreateScale(0.946f) * Matrix.CreateTranslation(0.189f, 51.33f, 1.79f);
			this.hatMatrix[10, 10] = Matrix.CreateRotationZ(-0.06f) * Matrix.CreateRotationY(0.056f) * Matrix.CreateRotationX(-0.203f) * Matrix.CreateScale(1.108f) * Matrix.CreateTranslation(0.408f, 52.712f, 1.04f);
			this.hatMatrix[10, 11] = Matrix.CreateRotationZ(-0.118f) * Matrix.CreateRotationY(-0.168f) * Matrix.CreateRotationX(0.09f) * Matrix.CreateScale(0.742f) * Matrix.CreateTranslation(0.817f, 53.551f, 1.557f);
			this.hatMatrix[10, 12] = this.hatMatrix[10, 2];
			this.hatMatrix[10, 13] = this.hatMatrix[10, 8];
			this.hatMatrix[10, 14] = this.hatMatrix[10, 0];
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000CF5C File Offset: 0x0000B15C
		public void setTempHatMatrix(ref Matrix[,] hatMatrix)
		{
			int num = this.gameNPC;
			this.gameNPC = 0;
			this.hatindex = 1;
			hatMatrix[0, 0] = this.CreateRotationZ(-0.06f) * this.CreateRotationY(-0.05f) * this.CreateRotationX(-0.1f) * this.CreateScale(0.9f) * this.CreateTranslation(0.447f, 52.717f, 1.22f);
			hatMatrix[0, 1] = this.CreateRotationZ(-0.118f) * this.CreateRotationY(0.158f) * this.CreateRotationX(-0.117f) * this.CreateScale(1.07f) * this.CreateTranslation(0.455f, 51.295f, 1.425f);
			hatMatrix[0, 2] = this.CreateRotationZ(-0.119f) * this.CreateRotationY(0.022f) * this.CreateRotationX(0.053f) * this.CreateScale(1.13f) * this.CreateTranslation(0.56f, 50.96f, 1.03f);
			hatMatrix[0, 3] = this.CreateRotationZ(-0.156f) * this.CreateRotationY(0.177f) * this.CreateRotationX(-0.149f) * this.CreateScale(1.034f) * this.CreateTranslation(0.531f, 52.27f, 1.055f);
			hatMatrix[0, 4] = this.CreateRotationZ(-0.123f) * this.CreateRotationY(0.176f) * this.CreateRotationX(-0.216f) * this.CreateScale(1.18f) * this.CreateTranslation(0.64f, 51.279f, 2.141f);
			hatMatrix[0, 5] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.7f, 50.6f, 2.2f);
			hatMatrix[0, 6] = this.CreateRotationZ(-0.197f) * this.CreateRotationY(0.121f) * this.CreateRotationX(0.35f) * this.CreateScale(1.01f) * this.CreateTranslation(0.006f, 48.432f, 2.28f);
			hatMatrix[0, 7] = this.CreateRotationZ(-0.11f) * this.CreateRotationY(0.074f) * this.CreateRotationX(-0.05f) * this.CreateScale(0.85f) * this.CreateTranslation(0.96f, 48.29f, 3.713f);
			hatMatrix[0, 8] = this.CreateRotationZ(-0.08f) * this.CreateRotationY(0.02f) * this.CreateRotationX(-0.09f) * this.CreateScale(0.9f) * this.CreateTranslation(0.65f, 52.49f, 1.28f);
			hatMatrix[0, 9] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.17f, 50.52f, 2.2f);
			hatMatrix[0, 10] = this.CreateRotationZ(-0.08f) * this.CreateRotationY(0.21f) * this.CreateRotationX(-0.16f) * this.CreateScale(1.01f) * this.CreateTranslation(0.67f, 52.19f, 1.27f);
			hatMatrix[0, 11] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(1f, 52.9f, 1.5f);
			hatMatrix[0, 12] = hatMatrix[0, 2];
			hatMatrix[0, 13] = hatMatrix[0, 8];
			hatMatrix[0, 14] = hatMatrix[0, 0];
			this.gameNPC = 1;
			this.hatindex = 1;
			hatMatrix[1, 0] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1f) * this.CreateTranslation(0.4f, 51.1f, 1.5f);
			hatMatrix[1, 1] = this.CreateRotationZ(-0.15f) * this.CreateRotationY(0.08f) * this.CreateRotationX(0.02f) * this.CreateScale(0.98f) * this.CreateTranslation(0.49f, 50.8f, 1.4f);
			hatMatrix[1, 2] = this.CreateRotationZ(-0.078f) * this.CreateRotationY(0.001f) * this.CreateRotationX(-0.01f) * this.CreateScale(0.96f) * this.CreateTranslation(0.5f, 50.6f, 1.5f);
			hatMatrix[1, 3] = this.CreateRotationZ(-0.133f) * this.CreateRotationY(0.11f) * this.CreateRotationX(-0.043f) * this.CreateScale(1f) * this.CreateTranslation(0.4f, 51.1f, 1.5f);
			hatMatrix[1, 4] = this.CreateRotationZ(-0.066f) * this.CreateRotationY(0.12f) * this.CreateRotationX(-0.036f) * this.CreateScale(1.102f) * this.CreateTranslation(0.381f, 50.635f, 1.874f);
			hatMatrix[1, 5] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.94f) * this.CreateTranslation(0.7f, 50.7f, 2.09f);
			hatMatrix[1, 6] = this.CreateRotationZ(-0.179f) * this.CreateRotationY(0.093f) * this.CreateRotationX(0.19f) * this.CreateScale(0.93f) * this.CreateTranslation(0.03f, 48.19f, 2.457f);
			hatMatrix[1, 7] = this.CreateRotationZ(-0.145f) * this.CreateRotationY(0.074f) * this.CreateRotationX(-0.093f) * this.CreateScale(0.92f) * this.CreateTranslation(0.1f, 48.2f, 3.618f);
			hatMatrix[1, 8] = this.CreateRotationZ(-0.13f) * this.CreateRotationY(0.03f) * this.CreateRotationX(-0.03f) * this.CreateScale(0.89f) * this.CreateTranslation(0.4f, 51.1f, 1.5f);
			hatMatrix[1, 9] = this.CreateRotationZ(0f) * this.CreateRotationY(-0.02f) * this.CreateRotationX(-0.08f) * this.CreateScale(0.9f) * this.CreateTranslation(0.422f, 50.533f, 2.278f);
			hatMatrix[1, 10] = this.CreateRotationZ(-0.14f) * this.CreateRotationY(0.13f) * this.CreateRotationX(-0.13f) * this.CreateScale(0.98f) * this.CreateTranslation(0.56f, 51.63f, 1.5f);
			hatMatrix[1, 11] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(1f, 52.9f, 1.5f);
			hatMatrix[1, 12] = hatMatrix[1, 2];
			hatMatrix[1, 13] = hatMatrix[1, 8];
			hatMatrix[1, 14] = hatMatrix[1, 0];
			this.gameNPC = 2;
			this.hatindex = 1;
			hatMatrix[2, 0] = this.CreateRotationZ(-0.073f) * this.CreateRotationY(0.015f) * this.CreateRotationX(-0.101f) * this.CreateScale(1.02f) * this.CreateTranslation(-0.015f, 51.327f, 1.335f);
			hatMatrix[2, 1] = this.CreateRotationZ(-0.016f) * this.CreateRotationY(0.004f) * this.CreateRotationX(-0.177f) * this.CreateScale(0.92f) * this.CreateTranslation(0.1f, 51.3f, 1.2f);
			hatMatrix[2, 2] = this.CreateRotationZ(-0.052f) * this.CreateRotationY(0.071f) * this.CreateRotationX(-0.01f) * this.CreateScale(1f) * this.CreateTranslation(0f, 50.9f, 1.2f);
			hatMatrix[2, 3] = this.CreateRotationZ(-0.14f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.02f) * this.CreateTranslation(0.1f, 51.4f, 1.5f);
			hatMatrix[2, 4] = this.CreateRotationZ(-0.025f) * this.CreateRotationY(0.087f) * this.CreateRotationX(-0.092f) * this.CreateScale(1.02f) * this.CreateTranslation(-0.005f, 51.2f, 1.613f);
			hatMatrix[2, 5] = this.CreateRotationZ(-0.14f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(0.2f, 50.7f, 2f);
			hatMatrix[2, 6] = this.CreateRotationZ(-0.004f) * this.CreateRotationY(0.075f) * this.CreateRotationX(0.11f) * this.CreateScale(0.94f) * this.CreateTranslation(0f, 48.3f, 2.4f);
			hatMatrix[2, 7] = this.CreateRotationZ(-0.14f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.94f) * this.CreateTranslation(0f, 48.3f, 3.613f);
			hatMatrix[2, 8] = this.CreateRotationZ(0.01f) * this.CreateRotationY(-0.15f) * this.CreateRotationX(-0.09f) * this.CreateScale(0.86f) * this.CreateTranslation(0.1f, 51.4f, 1.31f);
			hatMatrix[2, 9] = this.CreateRotationZ(-0.14f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(-0.111f, 50.152f, 2.238f);
			hatMatrix[2, 10] = this.CreateRotationZ(-0.02f) * this.CreateRotationY(0.07f) * this.CreateRotationX(-0.14f) * this.CreateScale(1.02f) * this.CreateTranslation(-0.09f, 51.18f, 1.243f);
			hatMatrix[2, 11] = this.CreateRotationZ(-0.14f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(1f, 52.9f, 1.5f);
			hatMatrix[2, 12] = hatMatrix[2, 2];
			hatMatrix[2, 13] = hatMatrix[2, 8];
			hatMatrix[2, 14] = hatMatrix[2, 0];
			this.gameNPC = 3;
			this.hatindex = 1;
			hatMatrix[3, 0] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.02f) * this.CreateTranslation(0.6f, 52.5f, 1f);
			hatMatrix[3, 1] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1f) * this.CreateTranslation(0.6f, 51.8f, 0.7f);
			hatMatrix[3, 2] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.02f) * this.CreateTranslation(0.6f, 51.6f, 0.9f);
			hatMatrix[3, 3] = this.CreateRotationZ(-0.08f) * this.CreateRotationY(0.03f) * this.CreateRotationX(-0.22f) * this.CreateScale(1.07f) * this.CreateTranslation(0.6f, 52f, 1.05f);
			hatMatrix[3, 4] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.1f) * this.CreateTranslation(0.6f, 51.9f, 1.8f);
			hatMatrix[3, 5] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(0.6f, 52f, 1.4f);
			hatMatrix[3, 6] = this.CreateRotationZ(-0.129f) * this.CreateRotationY(-0.011f) * this.CreateRotationX(0.111f) * this.CreateScale(0.96f) * this.CreateTranslation(0.2f, 48.8f, 2.2f);
			hatMatrix[3, 7] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.96f) * this.CreateTranslation(0.2f, 48.8f, 2.2f);
			hatMatrix[3, 8] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.892f) * this.CreateTranslation(0.553f, 52.556f, 0.966f);
			hatMatrix[3, 9] = this.CreateRotationZ(0f) * this.CreateRotationY(-0.13f) * this.CreateRotationX(-0.3f) * this.CreateScale(0.97f) * this.CreateTranslation(0.46f, 51f, 1.95f);
			hatMatrix[3, 10] = this.CreateRotationZ(-0.1f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.97f) * this.CreateTranslation(0.6f, 52.33f, 1.13f);
			hatMatrix[3, 11] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(1f, 52.9f, 1.5f);
			hatMatrix[3, 12] = hatMatrix[3, 2];
			hatMatrix[3, 13] = hatMatrix[3, 8];
			hatMatrix[3, 14] = hatMatrix[3, 0];
			this.gameNPC = 4;
			this.hatindex = 1;
			hatMatrix[4, 0] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.514f, 51.891f, 2.25f);
			hatMatrix[4, 1] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.7f, 51.3f, 1.5f);
			hatMatrix[4, 2] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.7f, 51.1f, 2f);
			hatMatrix[4, 3] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.7f, 52f, 2.2f);
			hatMatrix[4, 4] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.1f) * this.CreateTranslation(0.6f, 51.5f, 2.6f);
			hatMatrix[4, 5] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(-0.279f) * this.CreateScale(1.026f) * this.CreateTranslation(0.812f, 50.808f, 2.632f);
			hatMatrix[4, 6] = this.CreateRotationZ(-0.13f) * this.CreateRotationY(0.03f) * this.CreateRotationX(0.1f) * this.CreateScale(1.01f) * this.CreateTranslation(0.4f, 48.34f, 3.1f);
			hatMatrix[4, 7] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.98f) * this.CreateTranslation(0.4f, 48.4f, 3.1f);
			hatMatrix[4, 8] = this.CreateRotationZ(-0.092f) * this.CreateRotationY(0f) * this.CreateRotationX(0.129f) * this.CreateScale(0.885f) * this.CreateTranslation(0.788f, 52.403f, 2.157f);
			hatMatrix[4, 9] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.68f, 50.43f, 2.7f);
			hatMatrix[4, 10] = this.CreateRotationZ(-0.08f) * this.CreateRotationY(0.02f) * this.CreateRotationX(-0.02f) * this.CreateScale(1.06f) * this.CreateTranslation(0.8f, 52.05f, 1.67f);
			hatMatrix[4, 11] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(1f, 52.9f, 1.5f);
			hatMatrix[4, 12] = hatMatrix[4, 2];
			hatMatrix[4, 13] = hatMatrix[4, 8];
			hatMatrix[4, 14] = hatMatrix[4, 0];
			this.gameNPC = 5;
			this.hatindex = 1;
			hatMatrix[5, 0] = this.CreateRotationZ(-0.056f) * this.CreateRotationY(0.012f) * this.CreateRotationX(0.093f) * this.CreateScale(1.1f) * this.CreateTranslation(0.1f, 52.8f, 1.4f);
			hatMatrix[5, 1] = this.CreateRotationZ(0f) * this.CreateRotationY(-0.03f) * this.CreateRotationX(-0.07f) * this.CreateScale(0.94f) * this.CreateTranslation(0f, 52.9f, 1.27f);
			hatMatrix[5, 2] = this.CreateRotationZ(0.01f) * this.CreateRotationY(0f) * this.CreateRotationX(-0.06f) * this.CreateScale(1.07f) * this.CreateTranslation(0.19f, 52.61f, 1.53f);
			hatMatrix[5, 3] = this.CreateRotationZ(-0.05f) * this.CreateRotationY(0.04f) * this.CreateRotationX(-0.29f) * this.CreateScale(1.1f) * this.CreateTranslation(-0.05f, 52.54f, 1.47f);
			hatMatrix[5, 4] = this.CreateRotationZ(-0.04f) * this.CreateRotationY(0.06f) * this.CreateRotationX(0.07f) * this.CreateScale(1.15f) * this.CreateTranslation(-0.04f, 52.1f, 2f);
			hatMatrix[5, 5] = this.CreateRotationZ(-0.14f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.02f) * this.CreateTranslation(0.2f, 51.9f, 1.9f);
			hatMatrix[5, 6] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0.06f) * this.CreateScale(0.94f) * this.CreateTranslation(-0.1f, 49.4f, 2.38f);
			hatMatrix[5, 7] = this.CreateRotationZ(-0.14f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.94f) * this.CreateTranslation(-0.1f, 49.4f, 2.6f);
			hatMatrix[5, 8] = this.CreateRotationZ(-0.01f) * this.CreateRotationY(0.19f) * this.CreateRotationX(0.05f) * this.CreateScale(0.94f) * this.CreateTranslation(0.1f, 52.8f, 1.4f);
			hatMatrix[5, 9] = this.CreateRotationZ(0.01f) * this.CreateRotationY(-0.07f) * this.CreateRotationX(0.04f) * this.CreateScale(1.02f) * this.CreateTranslation(-0.17f, 51.55f, 1.9f);
			hatMatrix[5, 10] = this.CreateRotationZ(0.05f) * this.CreateRotationY(0.06f) * this.CreateRotationX(-0.1f) * this.CreateScale(1.15f) * this.CreateTranslation(0.01f, 53.11f, 1.7f);
			hatMatrix[5, 11] = this.CreateRotationZ(-0.14f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(1f, 52.9f, 1.5f);
			hatMatrix[5, 12] = hatMatrix[5, 2];
			hatMatrix[5, 13] = hatMatrix[5, 8];
			hatMatrix[5, 14] = hatMatrix[5, 0];
			this.gameNPC = 6;
			this.hatindex = 1;
			hatMatrix[6, 0] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.84f) * this.CreateTranslation(0.6f, 51.4f, 2.2f);
			hatMatrix[6, 1] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.7f, 51.3f, 1.5f);
			hatMatrix[6, 2] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.7f, 51.1f, 2f);
			hatMatrix[6, 3] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.7f, 52f, 2.2f);
			hatMatrix[6, 4] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.1f) * this.CreateTranslation(0.6f, 51.5f, 2.6f);
			hatMatrix[6, 5] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.7f, 50.6f, 2.7f);
			hatMatrix[6, 6] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.98f) * this.CreateTranslation(0.4f, 48.4f, 3.1f);
			hatMatrix[6, 7] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.94f) * this.CreateTranslation(0.2f, 48.5f, 3.1f);
			hatMatrix[6, 8] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.84f) * this.CreateTranslation(0.6f, 51.4f, 2.2f);
			hatMatrix[6, 9] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.7f, 50.6f, 2.7f);
			hatMatrix[6, 10] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.06f) * this.CreateTranslation(0.7f, 52f, 2.2f);
			hatMatrix[6, 11] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(1f, 52.9f, 1.5f);
			hatMatrix[6, 12] = hatMatrix[6, 2];
			hatMatrix[6, 13] = hatMatrix[6, 8];
			hatMatrix[6, 14] = hatMatrix[6, 0];
			this.gameNPC = 7;
			this.hatindex = 1;
			hatMatrix[7, 0] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.1f) * this.CreateTranslation(0.1f, 52.8f, 1.4f);
			hatMatrix[7, 1] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.94f) * this.CreateTranslation(0f, 52.9f, 1f);
			hatMatrix[7, 2] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1f) * this.CreateTranslation(0f, 52.9f, 1f);
			hatMatrix[7, 3] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.1f) * this.CreateTranslation(0.1f, 52.8f, 1.4f);
			hatMatrix[7, 4] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.1f) * this.CreateTranslation(0.1f, 52.1f, 2f);
			hatMatrix[7, 5] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.02f) * this.CreateTranslation(0.2f, 51.9f, 1.9f);
			hatMatrix[7, 6] = this.CreateRotationZ(-0.05f) * this.CreateRotationY(0.02f) * this.CreateRotationX(0.13f) * this.CreateScale(0.94f) * this.CreateTranslation(-0.1f, 49.4f, 2.6f);
			hatMatrix[7, 7] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.94f) * this.CreateTranslation(-0.1f, 49.4f, 2.6f);
			hatMatrix[7, 8] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0.217f) * this.CreateScale(0.588f) * this.CreateTranslation(0.1f, 53.567f, 1.635f);
			hatMatrix[7, 9] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1.02f) * this.CreateTranslation(-0.21f, 50.87f, 2.28f);
			hatMatrix[7, 10] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.97f) * this.CreateTranslation(-0.05f, 52.6f, 1.4f);
			hatMatrix[7, 11] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(0.9f) * this.CreateTranslation(1f, 52.9f, 1.5f);
			hatMatrix[7, 12] = hatMatrix[7, 2];
			hatMatrix[7, 13] = hatMatrix[7, 8];
			hatMatrix[7, 14] = hatMatrix[7, 0];
			this.gameNPC = 8;
			this.hatindex = 1;
			hatMatrix[8, 0] = this.CreateRotationZ(-0.06f) * this.CreateRotationY(0.21f) * this.CreateRotationX(0.11f) * this.CreateScale(1.15f) * this.CreateTranslation(0.62f, 54.41f, 0.39f);
			hatMatrix[8, 1] = this.CreateRotationZ(-0.04f) * this.CreateRotationY(0.19f) * this.CreateRotationX(0.03f) * this.CreateScale(1.22f) * this.CreateTranslation(0.59f, 54.45f, -0.44f);
			hatMatrix[8, 2] = this.CreateRotationZ(-0.08f) * this.CreateRotationY(0.22f) * this.CreateRotationX(-0.08f) * this.CreateScale(1.82f) * this.CreateTranslation(0.55f, 51.63f, 0.25f);
			hatMatrix[8, 3] = this.CreateRotationZ(-0.08f) * this.CreateRotationY(0.25f) * this.CreateRotationX(0.2f) * this.CreateScale(1.3f) * this.CreateTranslation(0.99f, 55.06f, 1.2f);
			hatMatrix[8, 4] = this.CreateRotationZ(-0.1f) * this.CreateRotationY(0.15f) * this.CreateRotationX(0.23f) * this.CreateScale(1.66f) * this.CreateTranslation(0.92f, 54.35f, 0.24f);
			hatMatrix[8, 5] = this.CreateRotationZ(0.05f) * this.CreateRotationY(6.49f) * this.CreateRotationX(-0.19f) * this.CreateScale(1.52f) * this.CreateTranslation(0.71f, 52.19f, 0.33f);
			hatMatrix[8, 6] = this.CreateRotationZ(-0.07f) * this.CreateRotationY(0.22f) * this.CreateRotationX(0.06f) * this.CreateScale(1.92f) * this.CreateTranslation(0.98f, 46.53f, 2.19f);
			hatMatrix[8, 7] = this.CreateRotationZ(-0.05f) * this.CreateRotationY(0.19f) * this.CreateRotationX(-0.18f) * this.CreateScale(1.2f) * this.CreateTranslation(0.49f, 48.13f, 2.92f);
			hatMatrix[8, 8] = this.CreateRotationZ(-0.118f) * this.CreateRotationY(0.555f) * this.CreateRotationX(0.09f) * this.CreateScale(0.588f) * this.CreateTranslation(0.615f, 54.355f, 0.733f);
			hatMatrix[8, 9] = this.CreateRotationZ(-0.04f) * this.CreateRotationY(6.41f) * this.CreateRotationX(-0.13f) * this.CreateScale(1.52f) * this.CreateTranslation(0.64f, 51.33f, 1.79f);
			hatMatrix[8, 10] = this.CreateRotationZ(-0.06f) * this.CreateRotationY(0.22f) * this.CreateRotationX(0f) * this.CreateScale(1.3f) * this.CreateTranslation(0.93f, 54.83f, 1.04f);
			hatMatrix[8, 11] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1f) * this.CreateTranslation(1f, 52.77f, 1f);
			hatMatrix[8, 12] = hatMatrix[8, 2];
			hatMatrix[8, 13] = hatMatrix[8, 8];
			hatMatrix[8, 14] = hatMatrix[8, 0];
			this.gameNPC = 9;
			this.hatindex = 1;
			hatMatrix[9, 0] = this.CreateRotationZ(-0.06f) * this.CreateRotationY(0.037f) * this.CreateRotationX(-0.007f) * this.CreateScale(0.351f) * this.CreateTranslation(0.207f, 53.021f, 1.758f);
			hatMatrix[9, 1] = this.CreateRotationZ(-0.04f) * this.CreateRotationY(0.092f) * this.CreateRotationX(-0.177f) * this.CreateScale(0.388f) * this.CreateTranslation(0.138f, 52.637f, 1.519f);
			hatMatrix[9, 2] = this.CreateRotationZ(-0.011f) * this.CreateRotationY(0.033f) * this.CreateRotationX(-0.371f) * this.CreateScale(0.805f) * this.CreateTranslation(0.192f, 51.125f, 0.674f);
			hatMatrix[9, 3] = this.CreateRotationZ(-0.108f) * this.CreateRotationY(0.063f) * this.CreateRotationX(-0.001f) * this.CreateScale(0.543f) * this.CreateTranslation(0.241f, 52.212f, 1.986f);
			hatMatrix[9, 4] = this.CreateRotationZ(-0.064f) * this.CreateRotationY(0.031f) * this.CreateRotationX(0.112f) * this.CreateScale(0.612f) * this.CreateTranslation(0.152f, 51.922f, 2.251f);
			hatMatrix[9, 5] = this.CreateRotationZ(0.05f) * this.CreateRotationY(6.26f) * this.CreateRotationX(0.035f) * this.CreateScale(0.473f) * this.CreateTranslation(0.296f, 51.88f, 2.275f);
			hatMatrix[9, 6] = this.CreateRotationZ(-0.055f) * this.CreateRotationY(-0.053f) * this.CreateRotationX(-0.211f) * this.CreateScale(1.048f) * this.CreateTranslation(0.158f, 47.864f, 2.599f);
			hatMatrix[9, 7] = this.CreateRotationZ(-0.167f) * this.CreateRotationY(0.01f) * this.CreateRotationX(-0.01f) * this.CreateScale(0.75f) * this.CreateTranslation(0.007f, 47.829f, 4.321f);
			hatMatrix[9, 8] = this.CreateRotationZ(-0.56f) * this.CreateRotationY(-0.191f) * this.CreateRotationX(-0.037f) * this.CreateScale(0.247f) * this.CreateTranslation(0.849f, 52.831f, 2.174f);
			hatMatrix[9, 9] = this.CreateRotationZ(-0.04f) * this.CreateRotationY(6.41f) * this.CreateRotationX(-0.032f) * this.CreateScale(0.512f) * this.CreateTranslation(0.154f, 51.673f, 2.431f);
			hatMatrix[9, 10] = this.CreateRotationZ(-0.06f) * this.CreateRotationY(-0.04f) * this.CreateRotationX(-0.096f) * this.CreateScale(0.732f) * this.CreateTranslation(0.173f, 52.535f, 1.964f);
			hatMatrix[9, 11] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1f) * this.CreateTranslation(1f, 52.77f, 1f);
			hatMatrix[9, 12] = hatMatrix[9, 2];
			hatMatrix[9, 13] = hatMatrix[9, 8];
			hatMatrix[9, 14] = hatMatrix[9, 0];
			this.gameNPC = 10;
			this.hatindex = 1;
			hatMatrix[10, 0] = this.CreateRotationZ(-0.06f) * this.CreateRotationY(-0.003f) * this.CreateRotationX(-0.018f) * this.CreateScale(0.364f) * this.CreateTranslation(0.603f, 53.857f, 1.474f);
			hatMatrix[10, 1] = this.CreateRotationZ(-0.006f) * this.CreateRotationY(-0.019f) * this.CreateRotationX(-0.565f) * this.CreateScale(0.477f) * this.CreateTranslation(0.274f, 53.233f, -0.257f);
			hatMatrix[10, 2] = this.CreateRotationZ(-0.08f) * this.CreateRotationY(0.02f) * this.CreateRotationX(-0.08f) * this.CreateScale(1.244f) * this.CreateTranslation(0.188f, 51.73f, 1.03f);
			hatMatrix[10, 3] = this.CreateRotationZ(-0.08f) * this.CreateRotationY(0.121f) * this.CreateRotationX(0.134f) * this.CreateScale(0.855f) * this.CreateTranslation(0.098f, 53.298f, 1.645f);
			hatMatrix[10, 4] = this.CreateRotationZ(-0.091f) * this.CreateRotationY(-0.406f) * this.CreateRotationX(0.287f) * this.CreateScale(0.494f) * this.CreateTranslation(0.834f, 53.76f, 1.535f);
			hatMatrix[10, 5] = this.CreateRotationZ(0.256f) * this.CreateRotationY(6.307f) * this.CreateRotationX(-0.304f) * this.CreateScale(0.916f) * this.CreateTranslation(0.101f, 51.522f, 0.847f);
			hatMatrix[10, 6] = this.CreateRotationZ(-0.064f) * this.CreateRotationY(-0.016f) * this.CreateRotationX(0.178f) * this.CreateScale(1.187f) * this.CreateTranslation(0.017f, 46.788f, 2.261f);
			hatMatrix[10, 7] = this.CreateRotationZ(-0.05f) * this.CreateRotationY(0.105f) * this.CreateRotationX(0.037f) * this.CreateScale(0.927f) * this.CreateTranslation(0.417f, 48.328f, 3.541f);
			hatMatrix[10, 8] = this.CreateRotationZ(-0.118f) * this.CreateRotationY(-0.168f) * this.CreateRotationX(0.09f) * this.CreateScale(0.742f) * this.CreateTranslation(0.817f, 53.551f, 1.557f);
			hatMatrix[10, 9] = this.CreateRotationZ(-0.04f) * this.CreateRotationY(6.41f) * this.CreateRotationX(-0.13f) * this.CreateScale(0.946f) * this.CreateTranslation(0.189f, 51.33f, 1.79f);
			hatMatrix[10, 10] = this.CreateRotationZ(-0.06f) * this.CreateRotationY(0.056f) * this.CreateRotationX(-0.203f) * this.CreateScale(1.108f) * this.CreateTranslation(0.408f, 52.712f, 1.04f);
			hatMatrix[10, 11] = this.CreateRotationZ(0f) * this.CreateRotationY(0f) * this.CreateRotationX(0f) * this.CreateScale(1f) * this.CreateTranslation(1f, 52.77f, 1f);
			hatMatrix[10, 12] = hatMatrix[10, 2];
			hatMatrix[10, 13] = hatMatrix[10, 8];
			hatMatrix[10, 14] = this.CreateRotationZ(0f) * this.CreateRotationY(0.18f) * this.CreateRotationX(0f) * this.CreateScale(1f) * this.CreateTranslation(1f, 52.94f, 1f);
			this.gameNPC = num;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00010768 File Offset: 0x0000E968
		public Matrix CreateRotationZ(float z)
		{
			if (this.hatTrigger.X != -1f && (int)this.hatTrigger.X != this.gameNPC && (int)this.hatTrigger.Y != this.hatindex)
			{
				return Matrix.CreateRotationZ(z);
			}
			this.hatRotX[this.gameNPC, this.hatindex - 1].Z = z;
			return Matrix.CreateRotationZ(z);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000107DC File Offset: 0x0000E9DC
		public Matrix CreateRotationY(float y)
		{
			if (this.hatTrigger.X != -1f && (int)this.hatTrigger.X != this.gameNPC && (int)this.hatTrigger.Y != this.hatindex)
			{
				return Matrix.CreateRotationY(y);
			}
			this.hatRotX[this.gameNPC, this.hatindex - 1].Y = y;
			return Matrix.CreateRotationY(y);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00010850 File Offset: 0x0000EA50
		public Matrix CreateRotationX(float x)
		{
			if (this.hatTrigger.X != -1f && (int)this.hatTrigger.X != this.gameNPC && (int)this.hatTrigger.Y != this.hatindex)
			{
				return Matrix.CreateRotationX(x);
			}
			this.hatRotX[this.gameNPC, this.hatindex - 1].X = x;
			return Matrix.CreateRotationX(x);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000108C4 File Offset: 0x0000EAC4
		public Matrix CreateScale(float a)
		{
			if (this.hatTrigger.X != -1f && (int)this.hatTrigger.X != this.gameNPC && (int)this.hatTrigger.Y != this.hatindex)
			{
				return Matrix.CreateScale(a);
			}
			this.hatScaleX[this.gameNPC, this.hatindex - 1] = a;
			return Matrix.CreateScale(a);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00010934 File Offset: 0x0000EB34
		public Matrix CreateTranslation(float x, float y, float z)
		{
			if (this.hatTrigger.X != -1f && (int)this.hatTrigger.X != this.gameNPC && (int)this.hatTrigger.Y != this.hatindex)
			{
				this.hatindex++;
				return Matrix.CreateTranslation(new Vector3(x, y, z));
			}
			this.hatTransX[this.gameNPC, this.hatindex - 1] = new Vector3(x, y, z);
			this.hatindex++;
			if (this.hatindex > 15)
			{
				this.hatindex = 0;
			}
			return Matrix.CreateTranslation(new Vector3(x, y, z));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000109EC File Offset: 0x0000EBEC
		public void setBlendState()
		{
			this.brightness = 128;
			this.brightBU = 128;
			this.contrast = 128;
			this.contrastBU = 128;
			this.brightnessUP = new BlendState();
			this.brightnessUP.ColorSourceBlend = (this.brightnessUP.AlphaSourceBlend = Blend.One);
			this.brightnessUP.ColorDestinationBlend = (this.brightnessUP.AlphaDestinationBlend = Blend.One);
			this.brightnessDOWN = new BlendState();
			this.brightnessDOWN.ColorSourceBlend = (this.brightnessDOWN.AlphaSourceBlend = Blend.Zero);
			this.brightnessDOWN.ColorDestinationBlend = (this.brightnessDOWN.AlphaDestinationBlend = Blend.SourceColor);
			this.contrastBlend = new BlendState();
			this.contrastBlend.ColorSourceBlend = (this.contrastBlend.AlphaSourceBlend = Blend.DestinationColor);
			this.contrastBlend.ColorDestinationBlend = (this.contrastBlend.AlphaDestinationBlend = Blend.SourceColor);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00010AE8 File Offset: 0x0000ECE8
		public static RenderTarget2D CopyTexture2D(GraphicsDevice gr, Texture2D image, Rectangle source, SpriteBatch sb)
		{
			Rectangle rectangle = new Rectangle(0, 0, source.Width, source.Height);
			RenderTarget2D renderTarget2D = new RenderTarget2D(gr, source.Width, source.Height, true, SurfaceFormat.Color, DepthFormat.None);
			gr.SetRenderTarget(renderTarget2D);
			gr.Clear(Color.Transparent);
			sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			sb.Draw(image, rectangle, new Rectangle?(source), Color.White);
			sb.End();
			return renderTarget2D;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00010B5C File Offset: 0x0000ED5C
		protected override void UnloadContent()
		{
			foreach (GameScreen gameScreen in this.screens)
			{
				gameScreen.UnloadContent();
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		private void DeactivatedEventHandler(object sender, EventArgs e)
		{
			this.awayIndex = this.rr.Next(0, this.away.Length);
			GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
			GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
			GamePad.SetVibration(PlayerIndex.Three, 0f, 0f);
			GamePad.SetVibration(PlayerIndex.Four, 0f, 0f);
			this.deactivated = true;
			this.rebuildFog = true;
			this.rebuildTwinSplat = true;
			this.graphics.GraphicsDevice.VertexSamplerStates[0] = SamplerState.PointClamp;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00010C4B File Offset: 0x0000EE4B
		private void ActivatedEventHandler(object sender, EventArgs e)
		{
			this.graphics.GraphicsDevice.VertexSamplerStates[0] = SamplerState.PointClamp;
			this.deactivated = false;
			this.rebuildTargets = true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00010C78 File Offset: 0x0000EE78
		public override void Update(GameTime gameTime)
		{
			if (SteamAPI.IsSteamRunning())
			{
				SteamAPI.RunCallbacks();
			}
			if (!this.workshop.textureBusy)
			{
				this.trophy.UpdateState();
				if (this.gameState > 0)
				{
					this.leaderboardCounter++;
					if (this.leaderboardCounter > 1800)
					{
						if (this.leaderboardSelect == 1)
						{
							this.trophy.updateLeaderboards();
						}
						if (this.leaderboardSelect == 2)
						{
							this.trophy.updateLeaderboards2();
						}
						if (this.leaderboardSelect == 3)
						{
							this.trophy.updateLeaderboards3();
						}
						if (this.leaderboardSelect == 4)
						{
							this.trophy.updateLeaderboards4();
						}
						this.leaderboardSelect++;
						if (this.leaderboardSelect > 4)
						{
							this.leaderboardSelect = 1;
						}
						this.leaderboardCounter = 0;
					}
				}
			}
			this.input.Update();
			this.screensToUpdate.Clear();
			foreach (GameScreen gameScreen in this.screens)
			{
				this.screensToUpdate.Add(gameScreen);
			}
			bool flag = !base.Game.IsActive;
			bool flag2 = false;
			while (this.screensToUpdate.Count > 0)
			{
				GameScreen gameScreen2 = this.screensToUpdate[this.screensToUpdate.Count - 1];
				this.screensToUpdate.RemoveAt(this.screensToUpdate.Count - 1);
				gameScreen2.Update(gameTime, flag, flag2);
				if (gameScreen2.ScreenState == ScreenState.TransitionOn || gameScreen2.ScreenState == ScreenState.Active)
				{
					if (!flag)
					{
						gameScreen2.HandleInput(this.input);
						flag = true;
					}
					if (!gameScreen2.IsPopup)
					{
						flag2 = true;
					}
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00010E34 File Offset: 0x0000F034
		public override void Draw(GameTime gameTime)
		{
			if (!this.deactivated)
			{
				this.devicex = this.width;
				this.devicey = this.hite;
				if (this.drawViewport && !this.inSpace)
				{
					this.graphics.GraphicsDevice.Viewport = this.myviewport;
				}
				this.viewportx = this.myviewport.Width;
				this.viewporty = this.myviewport.Height;
				foreach (GameScreen gameScreen in this.screens)
				{
					if (gameScreen.ScreenState != ScreenState.Hidden)
					{
						gameScreen.Draw(gameTime);
					}
				}
				this.drawBrightness();
				if (Princess.cuttyCount > 0)
				{
					this.drawRedness();
				}
				if (this.shitContrast != 128)
				{
					this.drawShitness();
				}
				if (this.contrastBU != 128)
				{
					this.drawNuke();
				}
				base.Draw(gameTime);
			}
			else
			{
				this.SpriteBatch.Begin();
				this.SpriteBatch.DrawString(this.grungeFont, this.away[this.awayIndex], new Vector2((float)(this.myviewport.Width / 2), (float)(this.myviewport.Height / 2)) - this.grungeFont.MeasureString(this.away[this.awayIndex]) / 2f, Color.White);
				this.SpriteBatch.End();
			}
			if (this.takepic > 0)
			{
				this.takepic--;
			}
			if (this.takepic == 1)
			{
				try
				{
					int backBufferWidth = base.GraphicsDevice.PresentationParameters.BackBufferWidth;
					int backBufferHeight = base.GraphicsDevice.PresentationParameters.BackBufferHeight;
					int[] array = new int[backBufferWidth * backBufferHeight];
					this.graphics.GraphicsDevice.GetBackBufferData<int>(array);
					Texture2D texture2D = new Texture2D(this.graphics.GraphicsDevice, backBufferWidth, backBufferHeight, false, this.graphics.GraphicsDevice.PresentationParameters.BackBufferFormat);
					texture2D.SetData<int>(array);
					if (!Directory.Exists("World"))
					{
						Directory.CreateDirectory("World");
					}
					Stream stream = File.OpenWrite(this.scrnfilename);
					texture2D.SaveAsJpeg(stream, 478, 277);
					stream.Dispose();
					texture2D.Dispose();
					this.takepic = 0;
				}
				catch
				{
					this.takepic = 0;
				}
				this.takepic = 0;
			}
			if (this.takepic == 4)
			{
				try
				{
					RenderTarget2D renderTarget2D = new RenderTarget2D(base.GraphicsDevice, 250, 250, true, SurfaceFormat.Color, DepthFormat.None, 2, RenderTargetUsage.DiscardContents);
					base.GraphicsDevice.SetRenderTarget(renderTarget2D);
					base.GraphicsDevice.Clear(new Color(32, 46, 95, 255));
					this.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearWrap, null, null);
					if (this.formDrawBG)
					{
						this.SpriteBatch.Draw(this.whiteTexture, new Rectangle(0, 0, 250, 250), this.formBGColor[this.formBGColorIndex]);
					}
					this.SpriteBatch.Draw(this.crosshair1[this.crossIndex].texture, new Rectangle(0, 0, 250, 250), new Rectangle?(this.zoomRectangle), Color.White);
					if (this.formDrawBand)
					{
						this.SpriteBatch.Draw(this.titleHeaderWorkshopPublish2, this.formBandL, new Rectangle?(this.formBand), this.formBandColor[this.formBandColorIndex]);
						if (this.workshop.formMark != "")
						{
							this.SpriteBatch.DrawString(this.font3, this.workshop.formMark, new Vector2(37f, 37f), Color.Black, -0.78539f, this.font3.MeasureString(this.workshop.formMark) / 2f, 1f, SpriteEffects.None, 0f);
						}
					}
					this.SpriteBatch.End();
					Texture2D texture2D2 = renderTarget2D;
					base.GraphicsDevice.SetRenderTarget(null);
					using (Stream stream2 = File.OpenWrite("image.jpg"))
					{
						texture2D2.SaveAsJpeg(stream2, 250, 250);
					}
					texture2D2.Dispose();
					this.takepic = 0;
					this.workshop.createNewItem();
				}
				catch
				{
					this.takepic = 0;
				}
				this.takepic = 0;
				this.workshop.showPublishbox = false;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0001132C File Offset: 0x0000F52C
		public void drawBrightness()
		{
			int num = 0;
			int num2 = 0;
			if (this.protectScreen)
			{
				num = this.myviewport.X;
				num2 = this.myviewport.Y;
			}
			if (!this.inSpace)
			{
				if (this.brightness != 128 || this.contrast != 128)
				{
					if (this.brightness > 128)
					{
						this.SpriteBatch.Begin(SpriteSortMode.Deferred, this.brightnessUP);
						this.SpriteBatch.Draw(this.whiteTexture, new Rectangle(num, num2, this.myviewport.Width, this.myviewport.Height), new Color(this.brightness - 128, this.brightness - 128, this.brightness - 128, 255));
						this.SpriteBatch.End();
					}
					else
					{
						this.SpriteBatch.Begin(SpriteSortMode.Deferred, this.brightnessDOWN);
						int num3 = (int)((float)this.brightness * 1.5f);
						this.SpriteBatch.Draw(this.whiteTexture, new Rectangle(num, num2, this.myviewport.Width, this.myviewport.Height), new Color(64 + num3, 64 + num3, 64 + num3, 255));
						this.SpriteBatch.End();
					}
					this.SpriteBatch.Begin(SpriteSortMode.Deferred, this.contrastBlend);
					this.SpriteBatch.Draw(this.whiteTexture, new Rectangle(num, num2, this.myviewport.Width, this.myviewport.Height), new Color(this.contrast, this.contrast, this.contrast, 255));
					this.SpriteBatch.End();
					return;
				}
			}
			else if (this.brightness != 128 || this.contrast != 128)
			{
				if (this.brightness > 128)
				{
					this.SpriteBatch.Begin(SpriteSortMode.Immediate, this.brightnessUP);
					this.SpriteBatch.Draw(this.whiteTexture, new Rectangle(0, 0, base.GraphicsDevice.Viewport.Width, base.GraphicsDevice.Viewport.Height), new Color(this.brightness - 128, this.brightness - 128, this.brightness - 128, 255));
					this.SpriteBatch.End();
				}
				else
				{
					this.SpriteBatch.Begin(SpriteSortMode.Immediate, this.brightnessDOWN);
					int num4 = (int)((float)this.brightness * 1.5f);
					this.SpriteBatch.Draw(this.whiteTexture, new Rectangle(0, 0, base.GraphicsDevice.Viewport.Width, base.GraphicsDevice.Viewport.Height), new Color(64 + num4, 64 + num4, 64 + num4, 255));
					this.SpriteBatch.End();
				}
				this.SpriteBatch.Begin(SpriteSortMode.Immediate, this.contrastBlend);
				this.SpriteBatch.Draw(this.whiteTexture, new Rectangle(0, 0, base.GraphicsDevice.Viewport.Width, base.GraphicsDevice.Viewport.Height), new Color(this.contrast, this.contrast, this.contrast, 255));
				this.SpriteBatch.End();
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000116A0 File Offset: 0x0000F8A0
		public void drawRedness()
		{
			int num = 0;
			int num2 = 0;
			if (this.protectScreen)
			{
				num = this.myviewport.X;
				num2 = this.myviewport.Y;
			}
			if (this.redContrast != 128)
			{
				this.SpriteBatch.Begin(SpriteSortMode.Deferred, this.contrastBlend);
				this.SpriteBatch.Draw(this.whiteTexture, new Rectangle(num, num2, this.myviewport.Width, this.myviewport.Height), new Color(128, this.redContrast, this.redContrast, 255));
				this.SpriteBatch.End();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00011744 File Offset: 0x0000F944
		public void drawShitness()
		{
			int num = 0;
			int num2 = 0;
			if (this.protectScreen)
			{
				num = this.myviewport.X;
				num2 = this.myviewport.Y;
			}
			if (this.shitContrast != 128)
			{
				this.SpriteBatch.Begin(SpriteSortMode.Deferred, this.contrastBlend);
				int num3 = (int)((float)(128 - this.shitContrast) * 0.3f);
				this.SpriteBatch.Draw(this.whiteTexture, new Rectangle(num, num2, this.myviewport.Width, this.myviewport.Height), new Color(128 - num3, this.shitContrast, 128 - num3, 255));
				this.SpriteBatch.End();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00011800 File Offset: 0x0000FA00
		public void drawNuke()
		{
			int num = 0;
			int num2 = 0;
			if (this.protectScreen)
			{
				num = this.myviewport.X;
				num2 = this.myviewport.Y;
			}
			if (this.contrastBU != 128)
			{
				this.SpriteBatch.Begin(SpriteSortMode.Deferred, this.contrastBlend);
				this.SpriteBatch.Draw(this.whiteTexture, new Rectangle(num, num2, this.myviewport.Width, this.myviewport.Height), new Color(this.contrastBU, this.contrastBU, 158, 255));
				this.SpriteBatch.End();
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000118A4 File Offset: 0x0000FAA4
		public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
		{
			screen.ControllingPlayer = controllingPlayer;
			screen.ScreenManager = this;
			screen.IsExiting = false;
			if (this.isInitialized)
			{
				screen.LoadContent();
			}
			this.screens.Add(screen);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000118D5 File Offset: 0x0000FAD5
		public void RemoveScreen(GameScreen screen)
		{
			if (this.isInitialized)
			{
				screen.UnloadContent();
			}
			this.screens.Remove(screen);
			this.screensToUpdate.Remove(screen);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000118FF File Offset: 0x0000FAFF
		public GameScreen[] GetScreens()
		{
			return this.screens.ToArray();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0001190C File Offset: 0x0000FB0C
		public bool LoadPrefs()
		{
			this.prefs = this.storagePrefs.LoadPreferences();
			if (this.prefs.fileExists)
			{
				this.fileVersion = this.prefs.fileVersion;
				this.mv = this.prefs.mv;
				this.ev = this.prefs.ev;
				this.vv = this.prefs.vv;
				this.df = this.prefs.df;
				this.df_orig = this.df;
				this.brightness = this.prefs.brightness;
				this.brightBU = this.brightness;
				this.contrast = this.prefs.contrast;
				this.pad_invertY = this.prefs.pad_invertY;
				this.pad_sensitivity = this.prefs.pad_sensitivity;
				this.pad_vibro = this.prefs.pad_vibro;
				this.hud_enemy = this.prefs.hud_enemy;
				this.hud_clock = this.prefs.hud_clock;
				this.hud_day = this.prefs.hud_day;
				this.hud_player1 = this.prefs.hud_player1;
				this.hud_player2 = this.prefs.hud_player2;
				this.hud_weapons = this.prefs.hud_weapons;
				this.hud_dpad = this.prefs.hud_dpad;
				this.camradian1 = this.prefs.camradianA;
				this.camheight1 = this.prefs.camheightA;
				this.campos3rd1 = this.prefs.campos3rdA;
				this.camlookpos3rd1 = this.prefs.camlookpos3rdA;
				this.camradian2 = this.prefs.camradianB;
				this.camheight2 = this.prefs.camheightB;
				this.campos3rd2 = this.prefs.campos3rdB;
				this.camlookpos3rd2 = this.prefs.camlookpos3rdB;
				this.curDay = (int)MathHelper.Clamp((float)this.prefs.curDay, 1f, (float)(this.maxDay() + 1));
				this.FarmerUnlocked = this.prefs.FarmerUnlocked;
				this.pad_reload = true;
				this.pad_togglesprint = this.prefs.pad_togglesprint;
				this.aliasing = this.prefs.aliasing;
				this.resolution = this.prefs.resolution;
				this.fullscreen = this.prefs.fullscreen;
				this.aspectratio = this.prefs.aspectratio;
				if (this.prefs.playername != "")
				{
					this.playername = this.prefs.playername;
				}
				this.mylens = this.prefs.lens;
				if (this.mylens < 20f || this.mylens > 150f)
				{
					this.mylens = 58f;
				}
				this.myfov = (float)Math.Cos((double)MathHelper.ToRadians((this.mylens + 30f) / 2f));
				this.gorelevel = this.prefs.gore;
				this.fastnades = this.prefs.fastnades;
				this.star1 = this.prefs.star1;
				this.star2 = this.prefs.star2;
				this.star3 = this.prefs.star3;
				this.doubleAmmo = this.prefs.doubleAmmo;
				if (!this.star1)
				{
					this.fastnades = false;
				}
				if (!this.star2)
				{
					this.gorelevel = 0;
				}
				if (!this.star3)
				{
					this.doubleAmmo = false;
				}
				this.workshopNum = (int)this.prefs.workshopNum;
				if (this.fileVersion != this.versionNumber)
				{
					this.resetPreferences();
					this.SavePrefs();
				}
			}
			return this.prefs.fileExists;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00011CDC File Offset: 0x0000FEDC
		public void SavePrefs()
		{
			this.prefs.fileVersion = this.versionNumber;
			this.prefs.mv = this.mv;
			this.prefs.ev = this.ev;
			this.prefs.vv = this.vv;
			if (this.host)
			{
				this.prefs.df = this.df;
			}
			else
			{
				this.prefs.df = this.df_orig;
			}
			this.prefs.brightness = this.brightness;
			this.brightBU = this.brightness;
			this.prefs.contrast = this.contrast;
			this.prefs.pad_invertY = this.pad_invertY;
			this.prefs.pad_sensitivity = this.pad_sensitivity;
			this.prefs.pad_vibro = this.pad_vibro;
			this.prefs.hud_enemy = this.hud_enemy;
			this.prefs.hud_clock = this.hud_clock;
			this.prefs.hud_day = this.hud_day;
			this.prefs.hud_player1 = this.hud_player1;
			this.prefs.hud_player2 = this.hud_player2;
			this.prefs.hud_weapons = this.hud_weapons;
			this.prefs.hud_dpad = this.hud_dpad;
			this.prefs.camradianA = this.camradian1;
			this.prefs.camheightA = this.camheight1;
			this.prefs.campos3rdA = this.campos3rd1;
			this.prefs.camlookpos3rdA = this.camlookpos3rd1;
			this.prefs.camradianB = this.camradian2;
			this.prefs.camheightB = this.camheight2;
			this.prefs.campos3rdB = this.campos3rd2;
			this.prefs.camlookpos3rdB = this.camlookpos3rd2;
			if (this.currentDay >= 1 && this.currentDay <= (int)(this.maxDay() + 1))
			{
				this.curDay = this.currentDay;
			}
			this.prefs.curDay = (ushort)this.curDay;
			this.prefs.FarmerUnlocked = this.FarmerUnlocked;
			this.prefs.pad_reload = this.pad_reload;
			this.prefs.pad_togglesprint = this.pad_togglesprint;
			this.prefs.aliasing = this.aliasing;
			this.prefs.resolution = this.resolution;
			this.prefs.fullscreen = this.fullscreen;
			this.prefs.aspectratio = this.aspectratio;
			this.prefs.playername = this.playername;
			this.prefs.lens = this.mylens;
			this.prefs.gore = this.gorelevel;
			this.prefs.fastnades = this.fastnades;
			this.prefs.star1 = this.star1;
			this.prefs.star2 = this.star2;
			this.prefs.star3 = this.star3;
			this.prefs.doubleAmmo = this.doubleAmmo;
			this.prefs.workshopNum = (byte)this.workshopNum;
			this.storagePrefs.SavePreferences(this.prefs);
			this.storeState = this.storagePrefs.status;
			if (this.storeState != "")
			{
				MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2(this.storeState.ToString(), 0);
				this.AddScreen(messageBoxScreen, null);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00012058 File Offset: 0x00010258
		public bool LoadGame(bool resetthis)
		{
			this.gdata = this.storageGame.LoadGame();
			if (this.gdata.fileExists)
			{
				this.fileVersion = this.gdata.fileVersion;
				for (int i = 0; i < this.days.Length; i++)
				{
					this.days[i] = (int)this.gdata.days[i];
				}
				this.scarh_Unlock = this.gdata.scarh;
				this.weaponUnlock_bits(this.gdata.weaponsunlocked);
				this.grinderUnlock_bits(this.gdata.grinderunlocked);
				this.grenades = (int)this.gdata.grenades;
				this.milks = (int)this.gdata.milks;
				this.hulks = (int)this.gdata.bulkify;
				this.pills = (int)this.gdata.pills;
				this.rockets = (int)this.gdata.mirv;
				int mats = (int)this.gdata.hats;
				this.hats[7] = this.ReadFromBitfield(ref mats, 1);
				this.hats[6] = this.ReadFromBitfield(ref mats, 1);
				this.hats[5] = this.ReadFromBitfield(ref mats, 1);
				this.hats[4] = this.ReadFromBitfield(ref mats, 1);
				this.hats[3] = this.ReadFromBitfield(ref mats, 1);
				this.hats[2] = this.ReadFromBitfield(ref mats, 1);
				this.hats[1] = this.ReadFromBitfield(ref mats, 1);
				this.hats[0] = 0;
				mats = (int)this.gdata.mats;
				this.hats[15] = this.ReadFromBitfield(ref mats, 1);
				this.hats[14] = this.ReadFromBitfield(ref mats, 1);
				this.hats[13] = this.ReadFromBitfield(ref mats, 1);
				this.hats[12] = this.ReadFromBitfield(ref mats, 1);
				this.hats[11] = this.ReadFromBitfield(ref mats, 1);
				this.hats[10] = this.ReadFromBitfield(ref mats, 1);
				this.hats[9] = this.ReadFromBitfield(ref mats, 1);
				this.hats[8] = this.ReadFromBitfield(ref mats, 1);
				this.man1 = this.gdata.man1;
				this.man2 = this.gdata.man2;
				this.man3 = this.gdata.man3;
				this.man4 = this.gdata.man4;
				this.goggles = (int)this.gdata.goggles;
				this.flashlight1 = (int)this.gdata.flashlight1;
				this.flashlight2 = (int)this.gdata.flashlight2;
				this.flashlight3 = (int)this.gdata.flashlight3;
				this.ammoboxCount = 0;
				for (int j = 0; j < this.map.Length; j++)
				{
					this.map[j] = (int)this.gdata.map[j];
				}
				for (int k = 0; k < this.ammobox1.Length; k++)
				{
					this.ammobox1[k] = (int)this.gdata.ammobox1[k];
					this.ammobox2[k] = (int)this.gdata.ammobox2[k];
					this.ammobox3[k] = (int)this.gdata.ammobox3[k];
					this.ammoboxCount += this.ammobox1[k] + this.ammobox2[k] + this.ammobox3[k];
					if (this.ammoboxCount > 10)
					{
						this.ammoboxCount = 10;
					}
				}
				for (int l = 0; l < this.cog1.Length; l++)
				{
					this.cog1[l] = (int)this.gdata.cog1[l];
					this.cog2[l] = (int)this.gdata.cog2[l];
					this.cog3[l] = (int)this.gdata.cog3[l];
				}
				for (int m = 0; m < this.exitkey.Length; m++)
				{
					this.exitkey[m] = (int)this.gdata.exitkey[m];
				}
				for (int n = 0; n < 20; n++)
				{
					this.code1[n, 0] = (int)this.gdata.code1[n, 0];
					this.code1[n, 1] = (int)this.gdata.code1[n, 1];
					this.code1[n, 2] = (int)this.gdata.code1[n, 2];
					this.code2[n, 0] = (int)this.gdata.code2[n, 0];
					this.code2[n, 1] = (int)this.gdata.code2[n, 1];
					this.code2[n, 2] = (int)this.gdata.code2[n, 2];
					this.code3[n, 0] = (int)this.gdata.code3[n, 0];
					this.code3[n, 1] = (int)this.gdata.code3[n, 1];
					this.code3[n, 2] = (int)this.gdata.code3[n, 2];
				}
				this.redskull1 = (int)this.gdata.redskull1;
				this.redskull2 = (int)this.gdata.redskull2;
				this.redskull3 = (int)this.gdata.redskull3;
				this.tusk1 = (int)this.gdata.tusk1;
				this.tusk2 = (int)this.gdata.tusk2;
				this.tusk3 = (int)this.gdata.tusk3;
				this.heirlooms[0] = (int)this.gdata.heirloom[0];
				this.heirlooms[1] = (int)this.gdata.heirloom[1];
				this.heirlooms[2] = (int)this.gdata.heirloom[2];
				this.heirlooms[3] = (int)this.gdata.heirloom[3];
				this.heirlooms[4] = (int)this.gdata.heirloom[4];
				this.heirlooms[5] = (int)this.gdata.heirloom[5];
				this.heirlooms[6] = (int)this.gdata.heirloom[6];
				if (this.bonusweek)
				{
					this.man1 = true;
					this.man2 = true;
					this.FarmerUnlocked = true;
					this.hats[9] = 1;
				}
				if (this.grenades > 10)
				{
					this.grenades = 10;
				}
				if (this.milks > 10)
				{
					this.milks = 10;
				}
				if (this.hulks > 5)
				{
					this.hulks = 5;
				}
				if (this.pills > 5)
				{
					this.pills = 5;
				}
				if (this.rockets > 5)
				{
					this.rockets = 5;
				}
				if (this.fileVersion != this.versionNumber)
				{
					this.fileVersion = this.versionNumber;
				}
			}
			else if (resetthis)
			{
				this.resetGame();
				this.SaveGame();
				this.fanfare.Play(1f, 1f, 0f);
			}
			return this.gdata.fileExists;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00012760 File Offset: 0x00010960
		public void SaveGame()
		{
			this.gdata.fileVersion = this.versionNumber;
			for (int i = 0; i < this.days.Length; i++)
			{
				this.gdata.days[i] = Math.Max((byte)this.days[i], this.gdata.days[i]);
			}
			this.gdata.weaponsunlocked = this.weapon_Unlock[0] + this.weapon_Unlock[2] * 2 + this.weapon_Unlock[4] * 4 + this.weapon_Unlock[6] * 8 + this.weapon_Unlock[8] * 16 + this.weapon_Unlock[10] * 32 + this.weapon_Unlock[12] * 64 + this.weapon_Unlock[14] * 128 + this.weapon_Unlock[16] * 256 + this.weapon_Unlock[18] * 512 + this.weapon_Unlock[20] * 1024 + this.weapon_Unlock[22] * 2048;
			this.gdata.scarh = this.scarh_Unlock;
			this.gdata.grinderunlocked = this.grinder_Unlock[1] + this.grinder_Unlock[2] * 2 + this.grinder_Unlock[3] * 4 + this.grinder_Unlock[4] * 8 + this.grinder_Unlock[5] * 16;
			this.gdata.grenades = (byte)MathHelper.Clamp((float)this.grenades, 0f, 10f);
			this.gdata.milks = (byte)MathHelper.Clamp((float)this.milks, 0f, 10f);
			this.gdata.bulkify = (byte)MathHelper.Clamp((float)this.hulks, 0f, 5f);
			this.gdata.pills = (byte)MathHelper.Clamp((float)this.pills, 0f, 5f);
			this.gdata.mirv = (byte)MathHelper.Clamp((float)this.rockets, 0f, 2f);
			int num = 0;
			this.AddToBitfield(ref num, 1, this.hats[1]);
			this.AddToBitfield(ref num, 1, this.hats[2]);
			this.AddToBitfield(ref num, 1, this.hats[3]);
			this.AddToBitfield(ref num, 1, this.hats[4]);
			this.AddToBitfield(ref num, 1, this.hats[5]);
			this.AddToBitfield(ref num, 1, this.hats[6]);
			this.AddToBitfield(ref num, 1, this.hats[7]);
			this.gdata.hats = (byte)num;
			num = 0;
			this.AddToBitfield(ref num, 1, this.hats[8]);
			this.AddToBitfield(ref num, 1, this.hats[9]);
			this.AddToBitfield(ref num, 1, this.hats[10]);
			this.AddToBitfield(ref num, 1, this.hats[11]);
			this.AddToBitfield(ref num, 1, this.hats[12]);
			this.AddToBitfield(ref num, 1, this.hats[13]);
			this.AddToBitfield(ref num, 1, this.hats[14]);
			this.AddToBitfield(ref num, 1, this.hats[15]);
			this.gdata.mats = (byte)num;
			this.gdata.man1 = this.man1;
			this.gdata.man2 = this.man2;
			this.gdata.man3 = this.man3;
			this.gdata.man4 = this.man4;
			this.gdata.goggles = (byte)this.goggles;
			this.gdata.flashlight1 = (byte)this.flashlight1;
			this.gdata.flashlight2 = (byte)this.flashlight2;
			this.gdata.flashlight3 = (byte)this.flashlight3;
			for (int j = 0; j < this.map.Length; j++)
			{
				this.gdata.map[j] = (byte)this.map[j];
			}
			for (int k = 0; k < this.ammobox1.Length; k++)
			{
				this.gdata.ammobox1[k] = (byte)this.ammobox1[k];
				this.gdata.ammobox2[k] = (byte)this.ammobox2[k];
				this.gdata.ammobox3[k] = (byte)this.ammobox3[k];
			}
			for (int l = 0; l < this.cog1.Length; l++)
			{
				this.gdata.cog1[l] = (byte)this.cog1[l];
				this.gdata.cog2[l] = (byte)this.cog2[l];
				this.gdata.cog3[l] = (byte)this.cog3[l];
			}
			for (int m = 0; m < this.exitkey.Length; m++)
			{
				this.gdata.exitkey[m] = (byte)this.exitkey[m];
			}
			for (int n = 0; n < 20; n++)
			{
				this.gdata.code1[n, 0] = (byte)this.code1[n, 0];
				this.gdata.code1[n, 1] = (byte)this.code1[n, 1];
				this.gdata.code1[n, 2] = (byte)this.code1[n, 2];
				this.gdata.code2[n, 0] = (byte)this.code2[n, 0];
				this.gdata.code2[n, 1] = (byte)this.code2[n, 1];
				this.gdata.code2[n, 2] = (byte)this.code2[n, 2];
				this.gdata.code3[n, 0] = (byte)this.code3[n, 0];
				this.gdata.code3[n, 1] = (byte)this.code3[n, 1];
				this.gdata.code3[n, 2] = (byte)this.code3[n, 2];
			}
			this.gdata.redskull1 = (byte)this.redskull1;
			this.gdata.redskull2 = (byte)this.redskull2;
			this.gdata.redskull3 = (byte)this.redskull3;
			this.gdata.tusk1 = (byte)this.tusk1;
			this.gdata.tusk2 = (byte)this.tusk2;
			this.gdata.tusk3 = (byte)this.tusk3;
			this.gdata.heirloom[0] = (byte)this.heirlooms[0];
			this.gdata.heirloom[1] = (byte)this.heirlooms[1];
			this.gdata.heirloom[2] = (byte)this.heirlooms[2];
			this.gdata.heirloom[3] = (byte)this.heirlooms[3];
			this.gdata.heirloom[4] = (byte)this.heirlooms[4];
			this.gdata.heirloom[5] = (byte)this.heirlooms[5];
			this.gdata.heirloom[6] = (byte)this.heirlooms[6];
			this.storageGame.SaveGame(this.gdata);
			this.storeState = this.storageGame.status;
			if (this.storeState != "")
			{
				MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2(this.storeState.ToString(), 0);
				this.AddScreen(messageBoxScreen, null);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00012EE0 File Offset: 0x000110E0
		public void savePickups()
		{
			this.gdata = this.storageGame.LoadGame();
			if (this.gdata.fileExists)
			{
				this.gdata.pills = (byte)this.pills;
				this.gdata.grenades = (byte)this.grenades;
				this.gdata.milks = (byte)this.milks;
				this.gdata.bulkify = (byte)this.hulks;
				this.storageGame.SaveGame(this.gdata);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00012F64 File Offset: 0x00011164
		public void resetTunnels()
		{
			this.ammoboxCount = 0;
			this.flashlight1 = 0;
			this.flashlight2 = 0;
			this.flashlight3 = 0;
			this.goggles = 0;
			this.redskull1 = 0;
			this.redskull2 = 0;
			this.redskull3 = 0;
			this.tusk1 = 0;
			this.tusk2 = 0;
			this.tusk3 = 0;
			this.map = new int[20];
			this.ammobox1 = new int[20];
			this.ammobox2 = new int[20];
			this.ammobox3 = new int[20];
			this.cog1 = new int[20];
			this.cog2 = new int[20];
			this.cog3 = new int[20];
			this.exitkey = new int[20];
			this.code1 = new int[20, 3];
			this.code2 = new int[20, 3];
			this.code3 = new int[20, 3];
			this.heirlooms = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
			this.exitkey = new int[20];
			string text = "World\\minimap0";
			string text2 = "World\\minimap1";
			string text3 = "World\\minimap2";
			string text4 = "World\\minimap3";
			string text5 = "World\\minimap4";
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			if (File.Exists(text2))
			{
				File.Delete(text2);
			}
			if (File.Exists(text3))
			{
				File.Delete(text3);
			}
			if (File.Exists(text4))
			{
				File.Delete(text4);
			}
			if (File.Exists(text5))
			{
				File.Delete(text5);
			}
			List<Vector2> list = new List<Vector2>();
			this.saveTunnelItems(true, ref list, 0);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0001311C File Offset: 0x0001131C
		public void saveTunnelItems(bool includeAll, ref List<Vector2> blob, int mazeid)
		{
			this.gdata = this.storageGame.LoadGame();
			if (this.gdata.fileExists)
			{
				this.gdata.scarh = this.scarh_Unlock;
				if (this.scarh_Unlock)
				{
					this.weapon_Unlock[20] = 1;
				}
				this.gdata.flashlight1 = (byte)this.flashlight1;
				this.gdata.flashlight2 = (byte)this.flashlight2;
				this.gdata.flashlight3 = (byte)this.flashlight3;
				if (this.redskull1 == 2)
				{
					this.gdata.redskull1 = (byte)this.redskull1;
				}
				if (this.redskull2 == 2)
				{
					this.gdata.redskull2 = (byte)this.redskull2;
				}
				if (this.redskull3 == 2)
				{
					this.gdata.redskull3 = (byte)this.redskull3;
				}
				if (this.tusk1 == 2)
				{
					this.gdata.tusk1 = (byte)this.tusk1;
				}
				if (this.tusk2 == 2)
				{
					this.gdata.tusk2 = (byte)this.tusk2;
				}
				if (this.tusk3 == 2)
				{
					this.gdata.tusk3 = (byte)this.tusk3;
				}
				for (int i = 0; i < this.map.Length; i++)
				{
					this.gdata.map[i] = (byte)this.map[i];
				}
				int num = 0;
				for (int j = 0; j < this.ammobox1.Length; j++)
				{
					this.gdata.ammobox1[j] = (byte)this.ammobox1[j];
					this.gdata.ammobox2[j] = (byte)this.ammobox2[j];
					this.gdata.ammobox3[j] = (byte)this.ammobox3[j];
					num += this.ammobox1[j] + this.ammobox2[j] + this.ammobox3[j];
					if (num > 10)
					{
						num = 10;
					}
				}
				for (int k = 0; k < this.cog1.Length; k++)
				{
					this.gdata.cog1[k] = (byte)this.cog1[k];
					this.gdata.cog2[k] = (byte)this.cog2[k];
					this.gdata.cog3[k] = (byte)this.cog3[k];
				}
				for (int l = 0; l < 20; l++)
				{
					this.gdata.code1[l, 0] = (byte)this.code1[l, 0];
					this.gdata.code1[l, 1] = (byte)this.code1[l, 1];
					this.gdata.code1[l, 2] = (byte)this.code1[l, 2];
					this.gdata.code2[l, 0] = (byte)this.code2[l, 0];
					this.gdata.code2[l, 1] = (byte)this.code2[l, 1];
					this.gdata.code2[l, 2] = (byte)this.code2[l, 2];
					this.gdata.code3[l, 0] = (byte)this.code3[l, 0];
					this.gdata.code3[l, 1] = (byte)this.code3[l, 1];
					this.gdata.code3[l, 2] = (byte)this.code3[l, 2];
				}
				this.gdata.heirloom[0] = (byte)this.heirlooms[0];
				this.gdata.heirloom[1] = (byte)this.heirlooms[1];
				this.gdata.heirloom[2] = (byte)this.heirlooms[2];
				this.gdata.heirloom[3] = (byte)this.heirlooms[3];
				this.gdata.heirloom[4] = (byte)this.heirlooms[4];
				this.gdata.heirloom[5] = (byte)this.heirlooms[5];
				this.gdata.heirloom[6] = (byte)this.heirlooms[6];
				if (includeAll)
				{
					this.ammoboxCount = num;
					int num2 = 0;
					this.AddToBitfield(ref num2, 1, this.hats[8]);
					this.AddToBitfield(ref num2, 1, this.hats[9]);
					this.AddToBitfield(ref num2, 1, this.hats[10]);
					this.AddToBitfield(ref num2, 1, this.hats[11]);
					this.AddToBitfield(ref num2, 1, this.hats[12]);
					this.AddToBitfield(ref num2, 1, this.hats[13]);
					this.AddToBitfield(ref num2, 1, this.hats[14]);
					this.AddToBitfield(ref num2, 1, this.hats[15]);
					this.gdata.mats = (byte)num2;
					for (int m = 0; m < this.exitkey.Length; m++)
					{
						this.gdata.exitkey[m] = (byte)this.exitkey[m];
					}
					this.gdata.redskull1 = (byte)this.redskull1;
					this.gdata.redskull2 = (byte)this.redskull2;
					this.gdata.redskull3 = (byte)this.redskull3;
					this.gdata.tusk1 = (byte)this.tusk1;
					this.gdata.tusk2 = (byte)this.tusk2;
					this.gdata.tusk3 = (byte)this.tusk3;
					this.gdata.goggles = (byte)this.goggles;
				}
				this.storageGame.SaveGame(this.gdata);
			}
			blob = blob.Distinct<Vector2>().ToList<Vector2>();
			string text = "World//minimap" + mazeid.ToString();
			if (!Directory.Exists("World"))
			{
				Directory.CreateDirectory("World");
			}
			try
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(text, FileMode.Create)))
				{
					for (int n = 0; n < blob.Count; n++)
					{
						binaryWriter.Write((byte)blob[n].X);
						binaryWriter.Write((byte)blob[n].Y);
					}
					binaryWriter.Close();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00013768 File Offset: 0x00011968
		public void saveTunnelCodes()
		{
			this.gdata = this.storageGame.LoadGame();
			if (this.gdata.fileExists)
			{
				for (int i = 0; i < this.cog1.Length; i++)
				{
					this.gdata.cog1[i] = (byte)this.cog1[i];
					this.gdata.cog2[i] = (byte)this.cog2[i];
					this.gdata.cog3[i] = (byte)this.cog3[i];
				}
				for (int j = 0; j < 20; j++)
				{
					this.gdata.code1[j, 0] = (byte)this.code1[j, 0];
					this.gdata.code1[j, 1] = (byte)this.code1[j, 1];
					this.gdata.code1[j, 2] = (byte)this.code1[j, 2];
					this.gdata.code2[j, 0] = (byte)this.code2[j, 0];
					this.gdata.code2[j, 1] = (byte)this.code2[j, 1];
					this.gdata.code2[j, 2] = (byte)this.code2[j, 2];
					this.gdata.code3[j, 0] = (byte)this.code3[j, 0];
					this.gdata.code3[j, 1] = (byte)this.code3[j, 1];
					this.gdata.code3[j, 2] = (byte)this.code3[j, 2];
				}
				this.storageGame.SaveGame(this.gdata);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00013930 File Offset: 0x00011B30
		public void SaveEquipables()
		{
			this.gdata = this.storageGame.LoadGame();
			if (this.gdata.fileExists)
			{
				int num = 0;
				this.AddToBitfield(ref num, 1, this.hats[1]);
				this.AddToBitfield(ref num, 1, this.hats[2]);
				this.AddToBitfield(ref num, 1, this.hats[3]);
				this.AddToBitfield(ref num, 1, this.hats[4]);
				this.AddToBitfield(ref num, 1, this.hats[5]);
				this.AddToBitfield(ref num, 1, this.hats[6]);
				this.AddToBitfield(ref num, 1, this.hats[7]);
				this.gdata.hats = (byte)num;
				num = 0;
				this.AddToBitfield(ref num, 1, this.hats[8]);
				this.AddToBitfield(ref num, 1, this.hats[9]);
				this.AddToBitfield(ref num, 1, this.hats[10]);
				this.AddToBitfield(ref num, 1, this.hats[11]);
				this.AddToBitfield(ref num, 1, this.hats[12]);
				this.AddToBitfield(ref num, 1, this.hats[13]);
				this.AddToBitfield(ref num, 1, this.hats[14]);
				this.AddToBitfield(ref num, 1, this.hats[15]);
				this.gdata.mats = (byte)num;
				this.gdata.man1 = this.man1;
				this.gdata.man2 = this.man2;
				this.gdata.man3 = this.man3;
				this.gdata.man4 = this.man4;
				this.storageGame.SaveGame(this.gdata);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00013AD8 File Offset: 0x00011CD8
		private int ReadFromBitfield(ref int bitfield, int bitCount)
		{
			int num = bitfield & ((1 << bitCount) - 1);
			bitfield >>= bitCount;
			return num;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00013AFB File Offset: 0x00011CFB
		private void AddToBitfield(ref int bitfield, int bitCount, int value)
		{
			bitfield <<= bitCount;
			bitfield |= value;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00013B0C File Offset: 0x00011D0C
		public ushort maxDay()
		{
			ushort num = 0;
			for (int i = 0; i < this.days.Length; i++)
			{
				if (this.days[i] > 0)
				{
					num = (ushort)i;
				}
				if (num > 101)
				{
					num = 101;
				}
			}
			return num;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00013B60 File Offset: 0x00011D60
		public void resetGame()
		{
			this.days = new int[201];
			for (int i = 0; i < this.days.Length; i++)
			{
				this.days[i] = 0;
			}
			this.days[0] = 1;
			this.gdata.days = new byte[201];
			for (int j = 0; j < this.gdata.days.Length; j++)
			{
				this.gdata.days[j] = 0;
			}
			this.weaponResetMode("reset");
			int[] array = new int[6];
			array[0] = 1;
			this.grinder_Unlock = array;
			this.grinder_Supply = new int[] { 100, 50, 50, 2, 1, 1 };
			this.grenades = 0;
			this.milks = 0;
			this.hulks = 0;
			this.pills = 0;
			this.rockets = 0;
			for (int k = 0; k < this.hats.Length; k++)
			{
				this.hats[k] = 0;
			}
			this.goggles = 0;
			this.flashlight1 = 0;
			this.flashlight2 = 0;
			this.flashlight3 = 0;
			this.map = new int[20];
			this.ammobox1 = new int[20];
			this.ammobox2 = new int[20];
			this.ammobox3 = new int[20];
			this.cog1 = new int[20];
			this.cog2 = new int[20];
			this.cog3 = new int[20];
			this.exitkey = new int[20];
			this.code1 = new int[20, 3];
			this.code2 = new int[20, 3];
			this.code3 = new int[20, 3];
			this.redskull1 = 0;
			this.redskull2 = 0;
			this.redskull3 = 0;
			this.tusk1 = 0;
			this.tusk2 = 0;
			this.tusk3 = 0;
			this.heirlooms = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00013D6C File Offset: 0x00011F6C
		public void weaponResetMode(string flag)
		{
			this.weapon_Unlock = new int[25];
			for (int i = 0; i < this.weapon_Unlock.Length - 1; i++)
			{
				this.weapon_Unlock[i] = 0;
			}
			if (flag == "pistol")
			{
				this.weapon_Unlock[2] = 1;
				if (this.scarh_Unlock)
				{
					this.weapon_Unlock[20] = 1;
				}
			}
			if (flag == "reset")
			{
				this.weapon_Unlock[2] = 1;
			}
			if (flag == "all")
			{
				this.weapon_Unlock[0] = 1;
				this.weapon_Unlock[2] = 1;
				this.weapon_Unlock[4] = 1;
				this.weapon_Unlock[6] = 1;
				this.weapon_Unlock[8] = 1;
				this.weapon_Unlock[10] = 1;
				this.weapon_Unlock[12] = 1;
				this.weapon_Unlock[14] = 1;
				this.weapon_Unlock[16] = 1;
				this.weapon_Unlock[18] = 1;
				if (this.scarh_Unlock)
				{
					this.weapon_Unlock[20] = 1;
				}
			}
			if (flag == "horde")
			{
				this.weapon_Unlock[0] = 1;
				this.weapon_Unlock[2] = 1;
				this.weapon_Unlock[4] = 1;
				this.weapon_Unlock[6] = 1;
				this.weapon_Unlock[8] = 1;
				this.weapon_Unlock[10] = 1;
				this.weapon_Unlock[12] = 1;
				this.weapon_Unlock[14] = 0;
				this.weapon_Unlock[16] = 0;
				this.weapon_Unlock[18] = 1;
				this.weapon_Unlock[20] = 0;
				if (this.scarh_Unlock)
				{
					this.weapon_Unlock[20] = 1;
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00013EE8 File Offset: 0x000120E8
		public void weaponUnlock_bits(int bit)
		{
			if ((bit & 1) != 0)
			{
				this.weapon_Unlock[0] = 1;
			}
			if ((bit & 2) != 0)
			{
				this.weapon_Unlock[2] = 1;
			}
			if ((bit & 4) != 0)
			{
				this.weapon_Unlock[4] = 1;
			}
			if ((bit & 8) != 0)
			{
				this.weapon_Unlock[6] = 1;
			}
			if ((bit & 16) != 0)
			{
				this.weapon_Unlock[8] = 1;
			}
			if ((bit & 32) != 0)
			{
				this.weapon_Unlock[10] = 1;
			}
			if ((bit & 64) != 0)
			{
				this.weapon_Unlock[12] = 1;
			}
			if ((bit & 128) != 0)
			{
				this.weapon_Unlock[14] = 1;
			}
			if ((bit & 256) != 0)
			{
				this.weapon_Unlock[16] = 1;
			}
			if ((bit & 512) != 0)
			{
				this.weapon_Unlock[18] = 1;
			}
			if ((bit & 1024) != 0)
			{
				this.weapon_Unlock[20] = 1;
			}
			if (this.scarh_Unlock && this.currentDay >= 12)
			{
				this.weapon_Unlock[20] = 1;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00013FC4 File Offset: 0x000121C4
		public void grinderUnlock_bits(int bit)
		{
			if ((bit & 1) != 0)
			{
				this.grinder_Unlock[1] = 1;
			}
			if ((bit & 2) != 0)
			{
				this.grinder_Unlock[2] = 1;
			}
			if ((bit & 4) != 0)
			{
				this.grinder_Unlock[3] = 1;
			}
			if ((bit & 8) != 0)
			{
				this.grinder_Unlock[4] = 1;
			}
			if ((bit & 16) != 0)
			{
				this.grinder_Unlock[5] = 1;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00014018 File Offset: 0x00012218
		public void weaponDayEnd(ref int[] x)
		{
			if ((this.weaponEndofDay & 1) != 0)
			{
				x[0] = 1;
			}
			if ((this.weaponEndofDay & 2) != 0)
			{
				x[2] = 1;
			}
			if ((this.weaponEndofDay & 4) != 0)
			{
				x[4] = 1;
			}
			if ((this.weaponEndofDay & 8) != 0)
			{
				x[6] = 1;
			}
			if ((this.weaponEndofDay & 16) != 0)
			{
				x[8] = 1;
			}
			if ((this.weaponEndofDay & 32) != 0)
			{
				x[10] = 1;
			}
			if ((this.weaponEndofDay & 64) != 0)
			{
				x[12] = 1;
			}
			if ((this.weaponEndofDay & 128) != 0)
			{
				x[14] = 1;
			}
			if ((this.weaponEndofDay & 256) != 0)
			{
				x[16] = 1;
			}
			if ((this.weaponEndofDay & 512) != 0)
			{
				x[18] = 1;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000140CF File Offset: 0x000122CF
		public void resetAudio()
		{
			this.mv = 0.5f;
			this.ev = 0.8f;
			this.vv = 0.6f;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000140F4 File Offset: 0x000122F4
		public void resetVideo()
		{
			this.brightness = 128;
			this.brightBU = 128;
			this.contrast = 128;
			this.hud_enemy = new Vector2(80f, 35f);
			this.hud_clock = new Vector2(490f, 35f);
			this.hud_day = new Vector2(1200f, 35f);
			this.hud_player1 = new Vector2(495f, 620f);
			this.hud_player2 = new Vector2(50f, 640f);
			this.hud_weapons = new Vector2(1090f, 250f);
			this.hud_dpad = new Vector2(1115f, 300f);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000141B8 File Offset: 0x000123B8
		public void resetPreferences()
		{
			this.mv = 0.9f;
			this.ev = 1f;
			this.vv = 1f;
			this.df = 1;
			this.pad_invertY = 1f;
			this.pad_sensitivity = 1f;
			this.pad_vibro = true;
			this.pad_reload = true;
			this.brightness = 128;
			this.contrast = 128;
			this.hud_enemy = new Vector2(80f, 35f);
			this.hud_clock = new Vector2(490f, 35f);
			this.hud_day = new Vector2(1200f, 35f);
			this.hud_player1 = new Vector2(495f, 620f);
			this.hud_player2 = new Vector2(50f, 640f);
			this.hud_weapons = new Vector2(1090f, 250f);
			this.hud_dpad = new Vector2(1115f, 300f);
			this.camradian1 = -1.725f;
			this.camheight1 = 2.95f;
			this.campos3rd1 = new Vector3(-21.53f, 48.7f, 15.2f);
			this.camlookpos3rd1 = new Vector3(172.4f, 10.25f, -14.95f);
			this.camradian2 = -1.55f;
			this.camheight2 = 2.08f;
			this.campos3rd2 = new Vector3(-71f, 303f, 6.3f);
			this.camlookpos3rd2 = new Vector3(-45.6f, 259f, 6.7f);
			this.resolution = 0;
			this.aliasing = 0;
			this.fullscreen = 0;
			this.gorelevel = 0;
			this.fastnades = false;
			this.star1 = false;
			this.star2 = false;
			this.star3 = false;
			this.workshopNum = 0;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00014390 File Offset: 0x00012590
		public void resetColors()
		{
			this.color_enemy = new Color(210, 0, 0, 255);
			this.color_day = new Color(210, 0, 0, 255);
			this.color_clock = new Color(255, 255, 255, 255);
			this.color_player1 = new Color(255, 255, 255, 255);
			this.color_player2 = new Color(255, 255, 255, 255);
			this.color_weapons = new Color(255, 255, 255, 255);
			this.color_dpad = new Color(255, 255, 255, 255);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00014468 File Offset: 0x00012668
		public void FadeBackBufferToBlack(int alpha)
		{
			this.SpriteBatch.Begin();
			this.SpriteBatch.Draw(this.blackTexture, new Rectangle(0, 0, this.graphics.GraphicsDevice.Viewport.Width, this.graphics.GraphicsDevice.Viewport.Height), new Color(0, 0, 0, (int)((byte)alpha)));
			this.SpriteBatch.End();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000144DD File Offset: 0x000126DD
		public Rectangle adjustRect(Rectangle ss)
		{
			return ss;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000144E0 File Offset: 0x000126E0
		public Rectangle adjustRect2(Rectangle ss)
		{
			Vector2 vector = new Vector2((float)this.screenSize.Width / (float)this.origSize.Width, (float)this.screenSize.Height / (float)this.origSize.Height);
			return new Rectangle(ss.X, ss.Y, (int)((float)ss.Width * vector.X), (int)((float)ss.Height * vector.Y))
			{
				X = (int)((float)(ss.X - this.myviewport.X + 1) * vector.X),
				Y = (int)((float)(ss.Y - this.myviewport.Y + 1) * vector.Y)
			};
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000145AC File Offset: 0x000127AC
		public Vector2 adjustVector(Vector2 ss)
		{
			new Vector2((float)this.origSize.Width / (float)this.screenSize.Width, (float)this.origSize.Height / (float)this.screenSize.Height);
			Vector2 vector = new Vector2(ss.X - (float)this.myviewport.X + 1f, ss.Y - (float)this.myviewport.Y + 1f);
			return vector;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00014630 File Offset: 0x00012830
		public Vector2 adjustVector2(Vector2 ss)
		{
			Vector2 vector = new Vector2((float)this.origSize.Width / (float)this.screenSize.Width, (float)this.origSize.Height / (float)this.screenSize.Height);
			Vector2 vector2 = new Vector2(ss.X - (float)this.myviewport.X + 1f, ss.Y - (float)this.myviewport.Y + 1f);
			vector2.X *= vector.X;
			vector2.Y *= vector.Y;
			return vector2;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000146DC File Offset: 0x000128DC
		public Vector2 adjustVector3(Vector2 ss)
		{
			Vector2 vector = new Vector2((float)this.width / this.startwidth, (float)this.hite / this.starthite);
			Vector2 vector2 = new Vector2(ss.X - (float)this.myviewport.X + 1f, ss.Y - (float)this.myviewport.Y + 1f);
			vector2.X *= vector.X;
			vector2.Y *= vector.Y;
			return vector2;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00014774 File Offset: 0x00012974
		public float adjustScale()
		{
			return 1f;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0001477B File Offset: 0x0001297B
		public void windowMax(float inc, int horiz, int vert)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00014780 File Offset: 0x00012980
		public void setAntiAliasing(int aa)
		{
			if (aa > 0)
			{
				this.graphics.PreferMultiSampling = true;
				this.graphics.PreparingDeviceSettings += this.graphics_PreparingDeviceSettings;
			}
			else
			{
				this.graphics.PreferMultiSampling = false;
			}
			this.graphics.ApplyChanges();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000147D0 File Offset: 0x000129D0
		public void setResolutionSafe()
		{
			this.graphics.GraphicsDevice.VertexSamplerStates[0] = SamplerState.PointClamp;
			this.width = this.graphics.GraphicsDevice.Viewport.Width;
			this.hite = this.graphics.GraphicsDevice.Viewport.Height;
			this.origSize = new Rectangle(0, 0, 1280, 720);
			this.winCenter = new Vector2((float)(this.width / 2), (float)(this.hite / 2));
			this.myviewport = this.graphics.GraphicsDevice.Viewport;
			this.startResX = (float)this.width;
			this.startResY = (float)this.hite;
			if (this.startResX > (float)this.width)
			{
				this.startResX = (float)(this.width - 4);
			}
			if (this.startResY > (float)this.hite)
			{
				this.startResY = (float)(this.hite - 4);
			}
			this.myviewport.Width = (int)this.startResX;
			this.myviewport.Height = (int)this.startResY;
			this.myviewport.X = this.width / 2 - this.myviewport.Width / 2;
			this.myviewport.Y = this.hite / 2 - this.myviewport.Height / 2;
			this.screenSize = new Rectangle(this.myviewport.X, this.myviewport.Y, this.myviewport.Width, this.myviewport.Height);
			this.graphics.GraphicsDevice.VertexSamplerStates[0] = SamplerState.PointClamp;
			this.setgraphics = true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00014994 File Offset: 0x00012B94
		public void setResolution()
		{
			try
			{
				this.graphics.GraphicsDevice.VertexSamplerStates[0] = SamplerState.PointClamp;
				int num = 0;
				foreach (DisplayMode displayMode in this.graphics.GraphicsDevice.Adapter.SupportedDisplayModes)
				{
					if (num == this.setupnum)
					{
						this.width = displayMode.Width;
						this.hite = displayMode.Height;
						break;
					}
					num++;
				}
				this.graphics.PreferredBackBufferWidth = this.width;
				this.graphics.PreferredBackBufferHeight = this.hite;
				this.graphics.IsFullScreen = this.fullmode;
				if (this.aa > 0)
				{
					this.graphics.PreferMultiSampling = true;
					this.graphics.PreparingDeviceSettings += this.graphics_PreparingDeviceSettings;
				}
				else
				{
					this.graphics.PreferMultiSampling = false;
				}
				this.graphics.ApplyChanges();
				this.resolution = this.setupnum;
				this.aliasing = this.aa;
				this.origSize = new Rectangle(0, 0, 1280, 720);
				this.winCenter = new Vector2((float)(this.width / 2), (float)(this.hite / 2));
				this.myviewport = this.graphics.GraphicsDevice.Viewport;
				float num2 = this.aspectratio;
				if (this.drawViewport)
				{
					if (num2 <= 1f)
					{
						this.startResX = (float)this.width;
						this.startResY = (float)this.hite * num2;
					}
					else
					{
						this.startResX = (float)this.width / num2;
						this.startResY = (float)this.hite;
					}
				}
				else
				{
					this.startResX = (float)this.width;
					this.startResY = (float)this.hite;
				}
				if (this.startResX > (float)this.width)
				{
					this.startResX = (float)(this.width - 4);
				}
				if (this.startResY > (float)this.hite)
				{
					this.startResY = (float)(this.hite - 4);
				}
				this.myviewport.Width = (int)this.startResX;
				this.myviewport.Height = (int)this.startResY;
				this.myviewport.X = this.width / 2 - this.myviewport.Width / 2;
				this.myviewport.Y = this.hite / 2 - this.myviewport.Height / 2;
				this.screenSize = new Rectangle(this.myviewport.X, this.myviewport.Y, this.myviewport.Width, this.myviewport.Height);
				this.graphics.GraphicsDevice.VertexSamplerStates[0] = SamplerState.PointClamp;
				this.setgraphics = true;
				this.justsetgraphics = true;
			}
			catch
			{
				this.setResolutionSafe();
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00014CA8 File Offset: 0x00012EA8
		public void removeBorder()
		{
			Form form = (Form)Control.FromHandle(base.Game.Window.Handle);
			form.FormBorderStyle = FormBorderStyle.None;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00014CD8 File Offset: 0x00012ED8
		public void addBorder()
		{
			Form form = (Form)Control.FromHandle(base.Game.Window.Handle);
			form.FormBorderStyle = FormBorderStyle.Fixed3D;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00014D07 File Offset: 0x00012F07
		public void centerWindow()
		{
			Mouse.WindowHandle = base.Game.Window.Handle;
			this.winCorner = new Vector2((float)(this.devicex / 2), (float)(this.devicey / 2));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00014D3B File Offset: 0x00012F3B
		private void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
		{
			e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = this.aa;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00014D54 File Offset: 0x00012F54
		public void addChatMsg(string name, Color nameColor, string message, Color messColor, int gap, ulong ID)
		{
			ScreenManager.chatty chatty = new ScreenManager.chatty();
			chatty.idlong = ID;
			chatty.name = name;
			chatty.nameColor = nameColor;
			chatty.message = message;
			chatty.messColor = messColor;
			chatty.gap = gap;
			this.chatHistory.Add(chatty);
			if (this.chatHistory.Count > 500)
			{
				this.chatHistory.RemoveAt(0);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00014DC0 File Offset: 0x00012FC0
		public void reColorChatBox(ref List<dummyOwner> remplayer)
		{
			for (int i = 0; i < this.chatHistory.Count; i++)
			{
				if (this.chatHistory[i].idlong != 5UL)
				{
					bool flag = false;
					for (int j = 0; j < remplayer.Count; j++)
					{
						if (this.chatHistory[i].idlong == this.hostowner.m_SteamID)
						{
							this.chatHistory[i].nameColor = this.hostblue;
							this.chatHistory[i].messColor = Color.White;
							flag = true;
							break;
						}
						if (this.chatHistory[i].idlong == remplayer[j].id.m_SteamID)
						{
							this.chatHistory[i].nameColor = this.colors[j];
							this.chatHistory[i].messColor = Color.LightSteelBlue;
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.chatHistory[i].nameColor = Color.Gray;
						this.chatHistory[i].messColor = Color.Gray;
					}
				}
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00014EFC File Offset: 0x000130FC
		public void echoCOMMANDS()
		{
			ulong num = 5UL;
			int num2 = 160;
			this.addChatMsg("*****************************************************", Color.Yellow, "", Color.Black, num2, num);
			this.addChatMsg("*", Color.Yellow, "", Color.White, num2, num);
			this.addChatMsg("*   :info         ", Color.Yellow, " LIST ALL AVAILABLE COMMANDS", Color.White, num2, num);
			this.addChatMsg("*   :invite         ", Color.Yellow, " INVITE FRIEND", Color.White, num2, num);
			this.addChatMsg("*   :cheats         ", Color.Yellow, " TOGGLE ALLOWED CHEATS", Color.White, num2, num);
			this.addChatMsg("*   :friendly   ", Color.Yellow, " TOGGLE FRIENDLY FIRE ON/OFF ", Color.White, num2, num);
			this.addChatMsg("*   :password ", Color.Yellow, " MAKE LOBBY PASSWORD REQUIRED ", Color.White, num2, num);
			this.addChatMsg("*   :restart     ", Color.Yellow, " RESTART THE LEVEL  ", Color.White, num2, num);
			this.addChatMsg("*   :more ", Color.Yellow, " LIST MORE COMMANDS", Color.White, num2, num);
			this.addChatMsg("*", Color.Yellow, "", Color.White, num2, num);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0001502C File Offset: 0x0001322C
		public void echoCOMMANDS2()
		{
			ulong num = 5UL;
			int num2 = 160;
			this.addChatMsg("*****************************************************", Color.Yellow, "", Color.Black, num2, num);
			this.addChatMsg("*", Color.Yellow, "", Color.White, num2, num);
			this.addChatMsg("*   :skin        ", Color.Yellow, " CHANGE CHARACTER SKIN  :skin daisy", Color.White, num2, num);
			this.addChatMsg("*   :names         ", Color.Yellow, " TOGGLE OVERHEAD NAMES", Color.White, num2, num);
			this.addChatMsg("*   :glow     ", Color.Yellow, " CHANGE GLOW TYPE", Color.White, num2, num);
			this.addChatMsg("*   :bobble        ", Color.Yellow, " TOGGLE BIG HEADS", Color.White, num2, num);
			this.addChatMsg("*   :kick      ", Color.Yellow, " KICK PLAYER FROM GAME ", Color.White, num2, num);
			this.addChatMsg("*   :push      ", Color.Yellow, " PUSH PLAYER FROM BARN", Color.White, num2, num);
			this.addChatMsg("*", Color.Yellow, "", Color.White, num2, num);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00015140 File Offset: 0x00013340
		public void refreshList()
		{
			this.predayList = new List<int>[]
			{
				new List<int> { 1 },
				new List<int> { 28, 24 },
				new List<int> { 29, 34 },
				new List<int> { 26, 25 },
				new List<int> { 27 },
				new List<int> { 30, 9 },
				new List<int> { 32 }
			};
			this.dayList = new List<int>[]
			{
				new List<int> { 1 },
				new List<int> { 2, 13 },
				new List<int> { 18, 35 },
				new List<int> { 7, 3, 22 },
				new List<int> { 8, 4, 22 },
				new List<int> { 9, 14 },
				new List<int> { 19, 21 },
				new List<int> { 30, 31 },
				new List<int> { 33 }
			};
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00015330 File Offset: 0x00013530
		public string getTip(int index)
		{
			if (!this.usingMouse)
			{
				this.dayTips = new string[]
				{
					"", "run around the farm, explore", "use LeftBumper to highlight enemies", "find the water pump to heal", "Electric Fence has been repaired", "check the mailbox for packages", "learn to use the grinder", "use LeftTrigger to melee", "press A to jump over enemies", "Dpad-Up to use flashlight",
					"Click R3 to change cameras", "Press Start to edit Camera", "customize your camera views", "good job...", "almost there...", "only a few remaining...", "now is a good time to explore", "use the flashlight...", "use RightBumper to clean yourself", "This is Revenge-Day !!",
					"Revenge-Days have infinite ammo", "Revenge-Day is once a week", "stats LeftBumper", "Shoot the Grinder Buttons", "Back button to open Wallet Menu", "Press Start to Edit Cameras", "Click R3 to change cameras", "Pistols have infinite Ammo", "Talk to the Farmer", "Y Button to switch Weapons",
					"LeftTrigger to shove whole bodies", "shoot or kick small parts", "Tutorial is almost over", "end of Tutorial", "there is a Boss Fight Day 10", "things are going to get tough", "Enter Key open Chatbox"
				};
			}
			else
			{
				this.dayTips = new string[]
				{
					"",
					"run around the farm, explore",
					"use " + this.getNameForTip(this.e_key) + " to highlight enemies",
					"find the water pump to heal",
					"Electric Fence has been repaired",
					"check the mailbox for packages",
					"learn to use the grinder",
					"use " + this.getNameForTip(this.rmb_key) + " to melee",
					"press " + this.getNameForTip(this.space_key) + " to jump over enemies",
					this.getNameForTip(this.f_key) + " to use flashlight",
					"F1 F2 F3 to change camera",
					"Press F10 to edit Camera",
					"customize your camera views",
					"good job...",
					"almost there...",
					"only a few remaining...",
					"now is a good time to explore",
					"use the flashlight...",
					"press " + this.getNameForTip(this.t_key) + " to clean yourself",
					"This is Revenge-Day !!",
					"Revenge-Days have infinite ammo",
					"Revenge-Day is once a week",
					"stats press " + this.getNameForTip(this.tab_key),
					"Shoot the Grinder Buttons",
					"ESC Key to open Wallet Menu",
					"Press F10 to Edit Cameras",
					"F1 F2 F3 to change cameras",
					"Pistols have infinite Ammo",
					"Talk to the Farmer",
					"press " + this.getNameForTip(this.q_key) + " to switch Weapons",
					"press " + this.getNameForTip(this.rmb_key) + " to shove whole bodies",
					"shoot or kick small parts",
					"Tutorial is almost over",
					"end of Tutorial",
					"there is a Boss Fight Day 10",
					"things are going to get tough",
					"Enter Key for Chatbox"
				};
			}
			return this.dayTips[index];
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000156A4 File Offset: 0x000138A4
		public string getNameForTip(Microsoft.Xna.Framework.Input.Keys k)
		{
			string text = k.ToString() + " Key";
			if (k == Microsoft.Xna.Framework.Input.Keys.VolumeUp)
			{
				text = "RMB";
			}
			if (k == Microsoft.Xna.Framework.Input.Keys.VolumeDown)
			{
				text = "LMB";
			}
			if (k == Microsoft.Xna.Framework.Input.Keys.VolumeMute)
			{
				text = "MMB";
			}
			if (k == Microsoft.Xna.Framework.Input.Keys.Print)
			{
				text = "BUT1";
			}
			if (k == Microsoft.Xna.Framework.Input.Keys.PrintScreen)
			{
				text = "BUT2";
			}
			return text;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00015708 File Offset: 0x00013908
		public void queryKeySquare(Rectangle rr, int index, ref int whichKEY)
		{
			Vector2 vector = this.adjustVector2(this.mymouse);
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				whichKEY = index;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00015784 File Offset: 0x00013984
		public void getXkey()
		{
			this.myXkey = this.x_key.ToString();
			if (this.x_key == Microsoft.Xna.Framework.Input.Keys.VolumeUp)
			{
				this.myXkey = "RMB";
			}
			if (this.x_key == Microsoft.Xna.Framework.Input.Keys.VolumeDown)
			{
				this.myXkey = "LMB";
			}
			if (this.x_key == Microsoft.Xna.Framework.Input.Keys.VolumeMute)
			{
				this.myXkey = "MMB";
			}
			if (this.x_key == Microsoft.Xna.Framework.Input.Keys.Print)
			{
				this.myXkey = "BUT1";
			}
			if (this.x_key == Microsoft.Xna.Framework.Input.Keys.PrintScreen)
			{
				this.myXkey = "BUT2";
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0001581C File Offset: 0x00013A1C
		public string getKeyName(Microsoft.Xna.Framework.Input.Keys k)
		{
			string text = k.ToString();
			if (k == Microsoft.Xna.Framework.Input.Keys.VolumeUp)
			{
				text = "RMB";
			}
			if (k == Microsoft.Xna.Framework.Input.Keys.VolumeDown)
			{
				text = "LMB";
			}
			if (k == Microsoft.Xna.Framework.Input.Keys.VolumeMute)
			{
				text = "MMB";
			}
			if (k == Microsoft.Xna.Framework.Input.Keys.Print)
			{
				text = "BUT1";
			}
			if (k == Microsoft.Xna.Framework.Input.Keys.PrintScreen)
			{
				text = "BUT2";
			}
			return text;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00015878 File Offset: 0x00013A78
		public void drawInteractive(SpriteBatch spriteBatch, ref int whichKEY, ref int thisKEY, SpriteFont font2, ref int val, ref float yy, string thekey, string descr)
		{
			float num = 23f;
			float num2 = 100f;
			float num3 = 400f;
			yy += num;
			val++;
			if (whichKEY == val)
			{
				this.bluePen = this.grnPen;
			}
			else
			{
				this.bluePen = this.bluePen2;
			}
			if (thisKEY == val)
			{
				this.bluePen = this.redPen;
			}
			spriteBatch.DrawString(this.scribblefont, descr + " :", this.topcorner + new Vector2(num2, yy), this.bluePen);
			spriteBatch.DrawString(this.scribblefont, thekey, this.topcorner + new Vector2(num3, yy), this.bluePen);
			Vector2 vector = this.scribblefont.MeasureString(thekey);
			vector.Y /= 3f;
			this.queryKeySquare(new Rectangle((int)(this.topcorner.X + num3), (int)(this.topcorner.Y + yy + vector.Y), (int)vector.X, (int)vector.Y), val, ref whichKEY);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0001599C File Offset: 0x00013B9C
		public void drawKeyBindings2(SpriteBatch spriteBatch, ref int whichKEY, ref int thisKEY, SpriteFont font2)
		{
			this.topcorner = new Vector2((float)(640 - this.blankpaper.Width / 2), (float)(370 - this.blankpaper.Height / 2));
			float num = 100f;
			float num2 = 22f;
			float num3 = 100f;
			float num4 = 400f;
			int num5 = 0;
			num -= num2;
			num -= num2;
			num -= num2;
			spriteBatch.Draw(this.blankpaper, this.topcorner, Color.White);
			int num6 = 28;
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num6, ref num, this.getKeyName(this.plus_key), "change crosshair");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.escape_key), "open wallet");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.lmb_key), "fire gun/use");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.rmb_key), "melee");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, "mouse", "aim/look");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.w_key), "forward");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.s_key), "backward");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.a_key), "strafe left");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.d_key), "strafe right");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.space_key), "jump");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.leftshift_key), "sprint");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.r_key), "reload");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.x_key), "interact");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.q_key), "switch weapons");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.e_key), "highlight enemies");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.f_key), "flashlight");
			num += num2;
			num5++;
			if (whichKEY == num5)
			{
				this.bluePen = this.grnPen;
			}
			else
			{
				this.bluePen = this.bluePen2;
			}
			if (thisKEY == num5)
			{
				this.bluePen = this.redPen;
			}
			spriteBatch.DrawString(this.scribblefont, "pickups :", this.topcorner + new Vector2(num3, (float)((int)num)), this.bluePen);
			Vector2 vector = this.scribblefont.MeasureString(this.one_key.ToString() + " ");
			vector.Y /= 3f;
			spriteBatch.DrawString(this.scribblefont, this.one_key.ToString() + " ", this.topcorner + new Vector2(num4, num), this.bluePen);
			this.queryKeySquare(new Rectangle((int)(this.topcorner.X + num4), (int)(this.topcorner.Y + num + vector.Y), (int)vector.X, (int)vector.Y), num5, ref whichKEY);
			num5++;
			if (whichKEY == num5)
			{
				this.bluePen = this.grnPen;
			}
			else
			{
				this.bluePen = this.bluePen2;
			}
			if (thisKEY == num5)
			{
				this.bluePen = this.redPen;
			}
			Vector2 vector2 = this.scribblefont.MeasureString(this.two_key.ToString() + " ");
			vector2.Y /= 3f;
			spriteBatch.DrawString(this.scribblefont, this.two_key.ToString() + " ", this.topcorner + new Vector2(vector.X + num4, num), this.bluePen);
			this.queryKeySquare(new Rectangle((int)(this.topcorner.X + vector.X + num4), (int)(this.topcorner.Y + num + vector2.Y), (int)vector2.X, (int)vector2.Y), num5, ref whichKEY);
			num5++;
			if (whichKEY == num5)
			{
				this.bluePen = this.grnPen;
			}
			else
			{
				this.bluePen = this.bluePen2;
			}
			if (thisKEY == num5)
			{
				this.bluePen = this.redPen;
			}
			Vector2 vector3 = this.scribblefont.MeasureString(this.three_key.ToString() + " ");
			vector3.Y /= 3f;
			spriteBatch.DrawString(this.scribblefont, this.three_key.ToString() + " ", this.topcorner + new Vector2(vector2.X + vector.X + num4, num), this.bluePen);
			this.queryKeySquare(new Rectangle((int)(this.topcorner.X + vector2.X + vector.X + num4), (int)(this.topcorner.Y + num + vector3.Y), (int)vector3.X, (int)vector3.Y), num5, ref whichKEY);
			num5++;
			if (whichKEY == num5)
			{
				this.bluePen = this.grnPen;
			}
			else
			{
				this.bluePen = this.bluePen2;
			}
			if (thisKEY == num5)
			{
				this.bluePen = this.redPen;
			}
			Vector2 vector4 = this.scribblefont.MeasureString(this.four_key.ToString() + " ");
			vector4.Y /= 3f;
			spriteBatch.DrawString(this.scribblefont, this.four_key.ToString() + " ", this.topcorner + new Vector2(vector3.X + vector2.X + vector.X + num4, num), this.bluePen);
			this.queryKeySquare(new Rectangle((int)(this.topcorner.X + vector3.X + vector2.X + vector.X + num4), (int)(this.topcorner.Y + num + vector4.Y), (int)vector4.X, (int)vector4.Y), num5, ref whichKEY);
			num += num2;
			num5++;
			if (whichKEY == num5)
			{
				this.bluePen = this.grnPen;
			}
			else
			{
				this.bluePen = this.bluePen2;
			}
			if (thisKEY == num5)
			{
				this.bluePen = this.redPen;
			}
			spriteBatch.DrawString(this.scribblefont, "camera views :", this.topcorner + new Vector2(num3, (float)((int)num)), this.bluePen);
			vector = this.scribblefont.MeasureString(this.f1_key.ToString() + " ");
			vector.Y /= 3f;
			spriteBatch.DrawString(this.scribblefont, this.f1_key.ToString() + " ", this.topcorner + new Vector2(num4, num), this.bluePen);
			this.queryKeySquare(new Rectangle((int)(this.topcorner.X + num4), (int)(this.topcorner.Y + num + vector.Y), (int)vector.X, (int)vector.Y), num5, ref whichKEY);
			num5++;
			if (whichKEY == num5)
			{
				this.bluePen = this.grnPen;
			}
			else
			{
				this.bluePen = this.bluePen2;
			}
			if (thisKEY == num5)
			{
				this.bluePen = this.redPen;
			}
			vector2 = this.scribblefont.MeasureString(this.f2_key.ToString() + " ");
			vector2.Y /= 3f;
			spriteBatch.DrawString(this.scribblefont, this.f2_key.ToString() + " ", this.topcorner + new Vector2(vector.X + num4, num), this.bluePen);
			this.queryKeySquare(new Rectangle((int)(this.topcorner.X + vector.X + num4), (int)(this.topcorner.Y + num + vector2.Y), (int)vector2.X, (int)vector2.Y), num5, ref whichKEY);
			num5++;
			if (whichKEY == num5)
			{
				this.bluePen = this.grnPen;
			}
			else
			{
				this.bluePen = this.bluePen2;
			}
			if (thisKEY == num5)
			{
				this.bluePen = this.redPen;
			}
			vector3 = this.scribblefont.MeasureString(this.f3_key.ToString() + " ");
			vector3.Y /= 3f;
			spriteBatch.DrawString(this.scribblefont, this.f3_key.ToString(), this.topcorner + new Vector2(vector2.X + vector.X + num4, num), this.bluePen);
			this.queryKeySquare(new Rectangle((int)(this.topcorner.X + vector2.X + vector.X + num4), (int)(this.topcorner.Y + num + vector3.Y), (int)vector3.X, (int)vector3.Y), num5, ref whichKEY);
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.tab_key), "round stats / minimap");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.up_key), "fov adjust");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.down_key), "fov adjust");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.left_key), "fov reset");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.t_key), "wiping face");
			this.drawInteractive(spriteBatch, ref whichKEY, ref thisKEY, font2, ref num5, ref num, this.getKeyName(this.enter_key), "open chatbox");
			if (whichKEY == 36 || whichKEY == 30 || whichKEY == 33)
			{
				whichKEY = 0;
				thisKEY = 0;
			}
			this.queryKeySquare(new Rectangle((int)this.topcorner.X + 520, (int)this.topcorner.Y + 20, 50, 26), 36, ref whichKEY);
			this.queryKeySquare(new Rectangle((int)this.topcorner.X + 221, (int)this.topcorner.Y + 629, 71, 34), 30, ref whichKEY);
			this.queryKeySquare(new Rectangle((int)this.topcorner.X + 341, (int)this.topcorner.Y + 629, 71, 34), 33, ref whichKEY);
			spriteBatch.Draw(this.paper1, this.topcorner + new Vector2(520f, 20f), new Rectangle?(this.cancelButtonBlue), Color.White);
			spriteBatch.Draw(this.paper1, this.topcorner + new Vector2(221f, 629f), new Rectangle?(this.saveButtonBlue), Color.White);
			spriteBatch.Draw(this.paper1, this.topcorner + new Vector2(341f, 629f), new Rectangle?(this.resetButtonBlue), Color.White);
			if (whichKEY == 36)
			{
				spriteBatch.Draw(this.paper1, this.topcorner + new Vector2(520f, 20f), new Rectangle?(this.cancelButtonRed), Color.White);
			}
			if (whichKEY == 30)
			{
				spriteBatch.Draw(this.paper1, this.topcorner + new Vector2(221f, 629f), new Rectangle?(this.saveButtonRed), Color.White);
			}
			if (whichKEY == 33)
			{
				spriteBatch.Draw(this.paper1, this.topcorner + new Vector2(341f, 629f), new Rectangle?(this.resetButtonRed), Color.White);
			}
			if (thisKEY != 0 && thisKEY != 33 && thisKEY != 30 && thisKEY != 36)
			{
				string text = "Press Any Key\nTo Remap Red Key";
				spriteBatch.DrawString(font2, text, new Vector2(980f, 300f), this.redPen);
				return;
			}
			if (whichKEY != 0 && whichKEY != 33 && whichKEY != 30 && whichKEY != 36)
			{
				string text2 = "Left Mouse Click\nTo Choose Key";
				spriteBatch.DrawString(font2, text2, new Vector2(980f, 300f), this.grnPen);
				return;
			}
			if (whichKEY == 0 || whichKEY == 33 || whichKEY == 30 || whichKEY == 36)
			{
				string text3 = "Remap Keys Use\nMouse to Highlight";
				spriteBatch.DrawString(font2, text3, new Vector2(980f, 300f), Color.White);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0001671C File Offset: 0x0001491C
		public void LoadAstroParameters()
		{
			this.inSpace = true;
			this.bgindex = 1;
			Randoms.initRandom(7);
			this.rr = new Random();
			if (this.fullmode)
			{
				this.aspectratio2 = this.aspectratio * 1.77777f;
			}
			else
			{
				this.aspectratio2 = (float)this.width / (float)this.hite;
			}
			this.startwidth = 1280f;
			this.starthite = 720f;
			this.startaspect = (float)Math.Round((double)(this.startwidth / this.starthite), 2);
			this.stretch = this.startaspect / this.aspectratio2;
			this.xoffset = 0f - ((float)this.width * this.stretch - (float)this.width) * 0.5f;
			float num = ((float)this.hite - (float)this.hite / this.stretch) * 0.5f;
			if (this.startaspect >= this.aspectratio2)
			{
				this.ScaleMatrix1 = Matrix.CreateScale((float)this.width / this.startwidth, (float)this.hite / this.starthite / this.stretch, 1f) * Matrix.CreateTranslation(0f, num, 0f);
				this.ScaleRect1 = new Rectangle(0, (int)num, this.width, (int)((float)this.hite / this.stretch));
				this.diffscaler = new Vector2(this.startwidth / (float)this.width, this.starthite / (float)this.hite * this.stretch);
				return;
			}
			this.ScaleMatrix1 = Matrix.CreateScale(this.stretch * ((float)this.width / this.startwidth), (float)this.hite / this.starthite, 1f) * Matrix.CreateTranslation(this.xoffset, 0f, 0f);
			this.ScaleRect1 = new Rectangle((int)this.xoffset, 0, (int)((float)this.width * this.stretch), this.hite);
			this.diffscaler = new Vector2(this.startwidth / (float)this.width, this.starthite / (float)this.hite * this.stretch);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00016954 File Offset: 0x00014B54
		public void LoadAstro()
		{
			if (this.astroloaded)
			{
				return;
			}
			astroDupe.sics = new List<int>();
			astroDupe.acc = 650f;
			astroDupe.farmLocation = new Vector3(0f, 5f, 0f);
			astroDupe.constantRoverPosition = Vector3.Zero;
			astroDupe.facility = Vector4.Zero;
			astro.teamCount = 0;
			astro.oldteamCount = 0;
			astro.someonedied = false;
			astro.inDumper = Matrix.Identity;
			astro.rovpos = Vector3.Zero;
			astro.rovveloc = Vector3.Zero;
			Lander.farmLocation = Vector3.Zero;
			Lander.nearfarm = false;
			Rover.turnSpeed = 0.11f;
			Rover.max = -65f;
			Rover.realGrav = -0.8f;
			Rover.fric = 0.9f;
			Rover.fricOrig = 0.9f;
			Rover.rockhitCount = 0;
			Rover.farmLocation = Vector3.Zero;
			Rover.nearfarm = false;
			Overlay.manbouy = Vector3.Zero;
			Facility.atMain = false;
			Facility.offset = new Vector3(0f, 0f, 0f);
			Facility.outsideCastle = true;
			Facility.inFacility = false;
			Facility.atSwitch = false;
			Facility.atSwitch2 = false;
			Facility.atMain = false;
			Facility.createWorkerLoc = Vector4.Zero;
			Facility.openConstruction = false;
			Facility.mypath.Clear();
			Facility.facilityPlot.Clear();
			Facility.reachPlot.Clear();
			Facility.dummyPlot.Clear();
			gemstruct.totalGems = 0;
			gemstruct.setitRight = Matrix.Identity;
			gemstruct.setVacuum = Matrix.Identity;
			gemstruct.inDumper = Matrix.Identity;
			objDupe.objectData = new int[500, 500];
			objDupe.flowersaved = 0;
			this.controllerX = this.content.Load<Model>("astro\\models\\controller");
			this.astroModel = this.content.Load<Model>("astro\\npc\\astroWalk1");
			this.astronaut = new astro();
			this.astronaut.LoadContent(this.content, this);
			this.Globalstarmap.LoadContent(this, this.content, base.GraphicsDevice, 1600, 25000, false, 0);
			this.blankTexture = this.content.Load<Texture2D>("astro\\menus\\blank");
			this.tahoma1 = this.content.Load<SpriteFont>("astro\\fonts\\tahoma1");
			this.tahoma2 = this.content.Load<SpriteFont>("astro\\fonts\\tahoma2");
			this.whiteTexture = new Texture2D(base.GraphicsDevice, 1, 1);
			this.whiteTexture.SetData<Color>(new Color[] { Color.White });
			this.camedit = this.content.Load<Texture2D>("astro\\textures\\camedit");
			this.entryrgb2 = this.content.Load<Texture2D>("astro\\textures\\entryRGB3");
			this.entryShad = this.content.Load<Texture2D>("astro\\textures\\entryShad2");
			this.messageBlob = this.content.Load<Texture2D>("astro\\textures//messageblob");
			this.iconbar = this.content.Load<Texture2D>("astro\\textures//iconbar");
			this.hudbuttons = this.content.Load<Texture2D>("astro\\textures//hudbuttons");
			this.helmet1 = this.content.Load<Texture2D>("astro\\textures//helmet1");
			this.helmet2 = this.content.Load<Texture2D>("astro\\textures//helmet2");
			this.interfaceBlob = this.content.Load<Texture2D>("astro\\textures\\interfaceBlob3");
			this.rooms = this.content.Load<Texture2D>("astro\\textures//rooms");
			this.codeBG = this.content.Load<Texture2D>("astro\\loading//dossy");
			this.buttonGong = this.content.Load<SoundEffect>("astro\\Audio//gong");
			this.gear2 = this.content.Load<Texture2D>("astro\\sprites//icons//gear1");
			this.loadBG = this.content.Load<Texture2D>("astro\\menus//Loading");
			this.loadfont1 = this.content.Load<SpriteFont>("astro\\fonts//tag");
			this.roverModel = this.content.Load<Model>("astro\\models\\tank3");
			this.origTransforms = new Matrix[this.roverModel.Bones.Count];
			this.roverModel.CopyBoneTransformsTo(this.origTransforms);
			this.halo = this.content.Load<SpriteFont>("astro\\fonts\\halo");
			this.halo2 = this.content.Load<SpriteFont>("astro\\fonts\\halo2");
			this.glow1 = this.content.Load<Texture2D>("astro\\menus\\glow1");
			this.opensolar = this.content.Load<SoundEffect>("astro\\Audio\\solarOpen");
			this.fuellow = this.content.Load<SoundEffect>("astro\\Audio\\fuelMore");
			this.fuelfull = this.content.Load<SoundEffect>("astro\\Audio\\fuelFull");
			this.grow = this.content.Load<SoundEffect>("astro\\Audio\\grow");
			this.landerEngine = this.content.Load<SoundEffect>("astro\\Audio\\thrust");
			this.roverEngine = this.content.Load<SoundEffect>("astro\\Audio\\engine");
			this.dropEngine = this.content.Load<SoundEffect>("astro\\Audio\\engine2");
			this.gravel = this.content.Load<SoundEffect>("astro\\Audio\\dirt2");
			this.overture = this.content.Load<SoundEffect>("astro\\Audio\\score");
			this.breath = this.content.Load<SoundEffect>("astro\\Audio\\breath2");
			this.boom = this.content.Load<SoundEffect>("astro\\Audio\\boom1");
			this.clickmenu = this.content.Load<SoundEffect>("astro\\Audio\\clickmenu");
			this.forward = this.content.Load<SoundEffect>("astro\\Audio\\forward");
			this.back = this.content.Load<SoundEffect>("astro\\Audio\\back");
			this.click = this.content.Load<SoundEffect>("astro\\audio\\switch2");
			this.drop = this.content.Load<SoundEffect>("astro\\audio\\drop");
			this.horn = this.content.Load<SoundEffect>("astro\\Audio\\horn1");
			this.select = this.content.Load<SoundEffect>("astro\\audio\\select1");
			this.step = this.content.Load<SoundEffect>("astro\\audio\\dirtStep3");
			this.crack = this.content.Load<SoundEffect>("astro\\Audio\\crack");
			this.cablesnap = this.content.Load<SoundEffect>("astro\\Audio\\cable");
			this.release = this.content.Load<SoundEffect>("astro\\Audio\\release");
			this.rejected = this.content.Load<SoundEffect>("astro\\Audio\\rejected");
			this.radio1 = this.content.Load<SoundEffect>("astro\\Audio\\radio1");
			this.radio2 = this.content.Load<SoundEffect>("astro\\Audio\\radioTalk2");
			this.radio3 = this.content.Load<SoundEffect>("astro\\Audio\\radioTalk3");
			this.eraseBeacon = this.content.Load<SoundEffect>("astro\\Audio\\pickBeacon");
			this.breakage = this.content.Load<SoundEffect>("astro\\Audio\\breakage");
			this.breakage2 = this.content.Load<SoundEffect>("astro\\Audio\\breakage2");
			this.cheer = this.content.Load<SoundEffect>("astro\\Audio\\cheer-01");
			this.dropBeacon = this.content.Load<SoundEffect>("astro\\Audio\\dropBeacon");
			this.warning = this.content.Load<SoundEffect>("astro\\Audio\\0527");
			this.boulderhit = this.content.Load<SoundEffect>("astro\\Audio\\boulderhit");
			this.boulderhit2 = this.content.Load<SoundEffect>("astro\\Audio\\boulderhit2");
			this.boulderhit3 = this.content.Load<SoundEffect>("astro\\Audio\\boulderhit3");
			this.boulderhit4 = this.content.Load<SoundEffect>("astro\\Audio\\boulderhit4");
			this.shalehit1 = this.content.Load<SoundEffect>("astro\\Audio\\shale");
			this.steeldrum1 = this.content.Load<SoundEffect>("astro\\Audio\\steel");
			this.steeldrum2 = this.content.Load<SoundEffect>("astro\\Audio\\synth");
			this.confirm = this.content.Load<SoundEffect>("astro\\Audio\\confirm1");
			this.forklift = this.content.Load<SoundEffect>("astro\\Audio\\forklift");
			this.enginex = this.content.Load<SoundEffect>("astro\\Audio\\enginex");
			this.scoopx = this.content.Load<SoundEffect>("astro\\Audio\\scoopx");
			this.shocks = this.content.Load<SoundEffect>("astro\\Audio\\shocks");
			this.groundhit = this.content.Load<SoundEffect>("astro\\Audio\\groundhit");
			this.ramp = this.content.Load<SoundEffect>("astro\\Audio\\ramp2");
			this.tada = this.content.Load<SoundEffect>("astro\\Audio\\tada");
			this.tada2 = this.content.Load<SoundEffect>("astro\\Audio\\tada2");
			this.tada3 = this.content.Load<SoundEffect>("astro\\Audio\\tadar3");
			this.tada4 = this.content.Load<SoundEffect>("astro\\Audio\\tadar4");
			this.tada5 = this.content.Load<SoundEffect>("astro\\Audio\\tadar5");
			this.tada6 = this.content.Load<SoundEffect>("astro\\Audio\\tadar6");
			this.tada7 = this.content.Load<SoundEffect>("astro\\Audio\\tadar7");
			this.tada10 = this.content.Load<SoundEffect>("astro\\Audio\\tadar10");
			this.gemfound = this.content.Load<SoundEffect>("astro\\Audio\\gemfound");
			this.land = this.content.Load<SoundEffect>("astro\\Audio\\land");
			this.jump = this.content.Load<SoundEffect>("astro\\Audio\\jump");
			this.bone = this.content.Load<SoundEffect>("astro\\Audio\\bone1");
			this.alarm1 = this.content.Load<SoundEffect>("astro\\Audio\\alarm1");
			this.lever = this.content.Load<SoundEffect>("astro\\Audio\\lever1");
			this.door = this.content.Load<SoundEffect>("astro\\Audio\\door");
			this.typewriterblank = 450f;
			this.textflag = 0;
			this.typewriterwait = (float)this.rr.Next(700, 1100);
			this.typeposition = 0f;
			this.typevertical = (float)this.rr.Next(430, 500);
			this.typewriterdelay = this.rr.Next(2, 6);
			this.astroloaded = true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00017354 File Offset: 0x00015554
		public void LoadSpacePrefs()
		{
			this.spaceprefs = this.storageSpacePrefs.LoadPreferences();
			if (this.storageSpacePrefs.status == "fail")
			{
				this.horn.Play(this.ev, 1f, 0f);
				this.horn.Play(this.ev, 1f, 0f);
				return;
			}
			if (this.spaceprefs.fileExists)
			{
				this.spaceVersion = this.spaceprefs.fileVersion;
				this.mv = this.spaceprefs.mv;
				this.ev = this.spaceprefs.ev;
				this.voiceVolume = this.spaceprefs.vv;
				this.df = this.spaceprefs.df;
				this.brightness = this.spaceprefs.brightness;
				this.contrast = this.spaceprefs.contrast;
				this.space_invertX = this.spaceprefs.pad_invertX;
				this.space_sentivityX = this.spaceprefs.pad_sensitivityX;
				this.space_invertY = this.spaceprefs.pad_invertY;
				this.space_sentivityY = this.spaceprefs.pad_sensitivityY;
				this.space_winvertX = this.spaceprefs.pad_winvertX;
				this.space_wsentivityX = this.spaceprefs.pad_wsensitivityX;
				this.space_winvertY = this.spaceprefs.pad_winvertY;
				this.space_wsentivityY = this.spaceprefs.pad_wsensitivityY;
				this.space_rinvertX = this.spaceprefs.pad_rinvertX;
				this.space_rsentivityX = this.spaceprefs.pad_rsensitivityX;
				this.space_rinvertY = this.spaceprefs.pad_rinvertY;
				this.space_rsentivityY = this.spaceprefs.pad_rsensitivityY;
				this.vibroSetting = this.spaceprefs.pad_vibro;
				try
				{
					Array.Copy(this.spaceprefs.allcamsradius, this.allcamsradius, 4);
					Array.Copy(this.spaceprefs.allcamsorbit, this.allcamsorbit, 4);
					Array.Copy(this.spaceprefs.allcamslens, this.allcamslens, 4);
					Array.Copy(this.spaceprefs.allcamsaltitude, this.allcamsaltitude, 4);
				}
				catch
				{
				}
				this.roverindex = this.spaceprefs.roverindex;
				this.roverrotlock = this.spaceprefs.roverrotlock;
				this.roverhitelock = this.spaceprefs.roverhitelock;
				this.landerindex = this.spaceprefs.landerindex;
				this.landerrotlock = this.spaceprefs.landerrotlock;
				this.landerhitelock = this.spaceprefs.landerhitelock;
				try
				{
					Array.Copy(this.spaceprefs.roverdist, this.roverdist, 3);
					Array.Copy(this.spaceprefs.roverheight, this.roverheight, 3);
					Array.Copy(this.spaceprefs.roverradian, this.roverradian, 3);
					Array.Copy(this.spaceprefs.landerdist, this.landerdist, 3);
					Array.Copy(this.spaceprefs.landerheight, this.landerheight, 3);
					Array.Copy(this.spaceprefs.landerradian, this.landerradian, 3);
					return;
				}
				catch
				{
					return;
				}
			}
			this.horn.Play(this.ev, -1f, 0f);
			this.horn.Play(this.ev, -1f, 0f);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000176D0 File Offset: 0x000158D0
		public void SaveSpacePrefs()
		{
			this.spaceprefs.fileVersion = this.spaceVersion;
			this.spaceprefs.mv = this.mv;
			this.spaceprefs.ev = this.ev;
			this.spaceprefs.vv = this.voiceVolume;
			this.spaceprefs.df = this.df;
			this.spaceprefs.brightness = this.brightness;
			this.spaceprefs.contrast = this.contrast;
			this.spaceprefs.pad_invertX = this.space_invertX;
			this.spaceprefs.pad_sensitivityX = this.space_sentivityX;
			this.spaceprefs.pad_invertY = this.space_invertY;
			this.spaceprefs.pad_sensitivityY = this.space_sentivityY;
			this.spaceprefs.pad_vibro = this.vibroSetting;
			this.spaceprefs.pad_winvertX = this.space_winvertX;
			this.spaceprefs.pad_wsensitivityX = this.space_wsentivityX;
			this.spaceprefs.pad_winvertY = this.space_winvertY;
			this.spaceprefs.pad_wsensitivityY = this.space_wsentivityY;
			this.spaceprefs.pad_rinvertX = this.space_rinvertX;
			this.spaceprefs.pad_rsensitivityX = this.space_rsentivityX;
			this.spaceprefs.pad_rinvertY = this.space_rinvertY;
			this.spaceprefs.pad_rsensitivityY = this.space_rsentivityY;
			this.spaceprefs.allcamsradius = new int[4];
			Array.Copy(this.allcamsradius, this.spaceprefs.allcamsradius, 4);
			this.spaceprefs.allcamsorbit = new int[4];
			Array.Copy(this.allcamsorbit, this.spaceprefs.allcamsorbit, 4);
			this.spaceprefs.allcamslens = new int[4];
			Array.Copy(this.allcamslens, this.spaceprefs.allcamslens, 4);
			this.spaceprefs.allcamsaltitude = new int[4];
			Array.Copy(this.allcamsaltitude, this.spaceprefs.allcamsaltitude, 4);
			this.spaceprefs.roverindex = this.roverindex;
			this.spaceprefs.roverrotlock = this.roverrotlock;
			this.spaceprefs.roverhitelock = this.roverhitelock;
			this.spaceprefs.roverdist = new float[3];
			Array.Copy(this.roverdist, this.spaceprefs.roverdist, 3);
			this.spaceprefs.roverheight = new float[3];
			Array.Copy(this.roverheight, this.spaceprefs.roverheight, 3);
			this.spaceprefs.roverradian = new float[3];
			Array.Copy(this.roverradian, this.spaceprefs.roverradian, 3);
			this.spaceprefs.landerindex = this.landerindex;
			this.spaceprefs.landerrotlock = this.landerrotlock;
			this.spaceprefs.landerhitelock = this.landerhitelock;
			this.spaceprefs.landerdist = new float[3];
			Array.Copy(this.landerdist, this.spaceprefs.landerdist, 3);
			this.spaceprefs.landerheight = new float[3];
			Array.Copy(this.landerheight, this.spaceprefs.landerheight, 3);
			this.spaceprefs.landerradian = new float[3];
			Array.Copy(this.landerradian, this.spaceprefs.landerradian, 3);
			this.storageSpacePrefs.SaveSpacePreferences(this.spaceprefs);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00017AA4 File Offset: 0x00015CA4
		public void resetSpacePrefs()
		{
			this.brightness = 128;
			this.contrast = 128;
			this.mv = 0.9f;
			this.ev = 0.9f;
			this.voiceVolume = 0.9f;
			this.df = 1;
			this.vibroSetting = 1;
			this.allcamsradius = new int[] { 0, 1, 3, 4 };
			int[] array = new int[4];
			this.allcamsorbit = array;
			int[] array2 = new int[4];
			this.allcamsaltitude = array2;
			int[] array3 = new int[4];
			this.allcamslens = array3;
			this.space_vibro = true;
			this.space_invertX = 1;
			this.space_invertY = 1;
			this.space_sentivityX = 1f;
			this.space_sentivityY = 1f;
			this.space_winvertX = 1;
			this.space_winvertY = 1;
			this.space_wsentivityX = 1f;
			this.space_wsentivityY = 1f;
			this.space_rinvertX = 1;
			this.space_rinvertY = 1;
			this.space_rsentivityX = 1f;
			this.space_rsentivityY = 1f;
			this.roverindex = 2;
			this.roverrotlock = 0;
			this.roverhitelock = 1;
			this.roverdist = new float[] { 200f, 440f, 800f };
			this.roverheight = new float[] { 100f, 400f, 700f };
			this.roverradian = new float[] { 100f, 100f, 100f };
			this.landerindex = 1;
			this.landerrotlock = 0;
			this.landerhitelock = 1;
			this.landerdist = new float[] { 500f, 1400f, 2500f };
			this.landerheight = new float[] { 300f, 300f, 300f };
			this.landerradian = new float[] { 100f, 100f, 100f };
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00017C60 File Offset: 0x00015E60
		public bool SaveSpaceKeys()
		{
			string text = "SAVES//spacekeys";
			if (!Directory.Exists("SAVES"))
			{
				Directory.CreateDirectory("SAVES");
			}
			bool flag;
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(File.Open(text, FileMode.Create)))
				{
					streamWriter.WriteLine(this.escape_key.ToString());
					streamWriter.WriteLine(this.w_key.ToString());
					streamWriter.WriteLine(this.s_key.ToString());
					streamWriter.WriteLine(this.a_key.ToString());
					streamWriter.WriteLine(this.d_key.ToString());
					streamWriter.WriteLine(this.space_key.ToString());
					streamWriter.WriteLine(this.leftshift_key.ToString());
					streamWriter.WriteLine(this.r_key.ToString());
					streamWriter.WriteLine(this.x_key.ToString());
					streamWriter.WriteLine(this.q_key.ToString());
					streamWriter.WriteLine(this.e_key.ToString());
					streamWriter.WriteLine(this.f_key.ToString());
					streamWriter.WriteLine(this.one_key.ToString());
					streamWriter.WriteLine(this.two_key.ToString());
					streamWriter.WriteLine(this.three_key.ToString());
					streamWriter.WriteLine(this.four_key.ToString());
					streamWriter.WriteLine(this.f1_key.ToString());
					streamWriter.WriteLine(this.f2_key.ToString());
					streamWriter.WriteLine(this.f3_key.ToString());
					streamWriter.WriteLine(this.tab_key.ToString());
					streamWriter.WriteLine(this.up_key.ToString());
					streamWriter.WriteLine(this.down_key.ToString());
					streamWriter.WriteLine(this.left_key.ToString());
					streamWriter.WriteLine(this.lmb_key.ToString());
					streamWriter.WriteLine(this.rmb_key.ToString());
					streamWriter.WriteLine(this.mmb_key.ToString());
					streamWriter.WriteLine(this.but1_key.ToString());
					streamWriter.WriteLine(this.but2_key.ToString());
					streamWriter.WriteLine(this.t_key.ToString());
					streamWriter.WriteLine(this.enter_key.ToString());
					streamWriter.WriteLine(this.u_key.ToString());
					streamWriter.Close();
					streamWriter.Dispose();
					flag = true;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00017F98 File Offset: 0x00016198
		public string LoadSpaceKeys()
		{
			string text = "SAVES//spacekeys";
			if (!File.Exists(text))
			{
				return "nofile";
			}
			string text2;
			try
			{
				using (StreamReader streamReader = new StreamReader(File.Open(text, FileMode.Open)))
				{
					this.keymaker(ref this.escape_key, streamReader.ReadLine());
					this.keymaker(ref this.w_key, streamReader.ReadLine());
					this.keymaker(ref this.s_key, streamReader.ReadLine());
					this.keymaker(ref this.a_key, streamReader.ReadLine());
					this.keymaker(ref this.d_key, streamReader.ReadLine());
					this.keymaker(ref this.space_key, streamReader.ReadLine());
					this.keymaker(ref this.leftshift_key, streamReader.ReadLine());
					this.keymaker(ref this.r_key, streamReader.ReadLine());
					this.keymaker(ref this.x_key, streamReader.ReadLine());
					this.keymaker(ref this.q_key, streamReader.ReadLine());
					this.keymaker(ref this.e_key, streamReader.ReadLine());
					this.keymaker(ref this.f_key, streamReader.ReadLine());
					this.keymaker(ref this.one_key, streamReader.ReadLine());
					this.keymaker(ref this.two_key, streamReader.ReadLine());
					this.keymaker(ref this.three_key, streamReader.ReadLine());
					this.keymaker(ref this.four_key, streamReader.ReadLine());
					this.keymaker(ref this.f1_key, streamReader.ReadLine());
					this.keymaker(ref this.f2_key, streamReader.ReadLine());
					this.keymaker(ref this.f3_key, streamReader.ReadLine());
					this.keymaker(ref this.tab_key, streamReader.ReadLine());
					this.keymaker(ref this.up_key, streamReader.ReadLine());
					this.keymaker(ref this.down_key, streamReader.ReadLine());
					this.keymaker(ref this.left_key, streamReader.ReadLine());
					this.keymaker(ref this.lmb_key, streamReader.ReadLine());
					this.keymaker(ref this.rmb_key, streamReader.ReadLine());
					this.keymaker(ref this.mmb_key, streamReader.ReadLine());
					this.keymaker(ref this.but1_key, streamReader.ReadLine());
					this.keymaker(ref this.but2_key, streamReader.ReadLine());
					this.keymaker(ref this.t_key, streamReader.ReadLine());
					this.keymaker(ref this.enter_key, streamReader.ReadLine());
					this.keymaker(ref this.u_key, streamReader.ReadLine());
					streamReader.Close();
					streamReader.Dispose();
					text2 = "OKAY";
				}
			}
			catch
			{
				text2 = "FAIL";
			}
			return text2;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00018250 File Offset: 0x00016450
		public void queryKeySquare2(Rectangle rr, int index, ref int whichKEY)
		{
			Vector2 vector = this.mymouse;
			float num = (float)this.screenSize.Height / 720f;
			Vector2 vector2 = new Vector2((float)this.origSize.Width / (float)this.width, (float)this.origSize.Height / (float)this.hite);
			vector.X *= vector2.X;
			vector.Y *= vector2.Y;
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				whichKEY = index;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00018334 File Offset: 0x00016534
		public void drawInteractive3(Vector2 topcorner, SpriteBatch spriteBatch, ref int whichKEY, ref int thisKEY, SpriteFont font2, ref int val, ref float yy, string thekey, string descr)
		{
			SpriteFont spriteFont = this.scribblefont2;
			float num = 23.3f;
			float num2 = 100f;
			float num3 = 400f;
			yy += num;
			val++;
			if (whichKEY == val)
			{
				this.bluePen = this.grnPen;
			}
			else
			{
				this.bluePen = Color.White;
			}
			if (thisKEY == val)
			{
				this.bluePen = this.redPen;
			}
			spriteBatch.DrawString(spriteFont, descr + " :", topcorner + new Vector2(num2, yy), this.bluePen);
			if (descr == "na")
			{
				spriteBatch.DrawString(spriteFont, "------", topcorner + new Vector2(num3, yy), this.bluePen);
			}
			else
			{
				spriteBatch.DrawString(spriteFont, thekey, topcorner + new Vector2(num3, yy), this.bluePen);
			}
			Vector2 vector = spriteFont.MeasureString(thekey);
			Rectangle rectangle = new Rectangle((int)(topcorner.X + num3), (int)(topcorner.Y + yy - vector.Y / 25f), (int)vector.X, (int)(vector.Y / 1.2f));
			this.queryKeySquare2(rectangle, val, ref whichKEY);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00018470 File Offset: 0x00016670
		public void drawKeyBindings3(SpriteBatch spriteBatch, ref int whichKEY, ref int thisKEY, SpriteFont font2)
		{
			SpriteFont spriteFont = this.scribblefont2;
			float y = spriteFont.MeasureString("XXX").Y;
			float num = (float)this.screenSize.Height / 720f;
			Vector2 vector = new Vector2((float)(640 - this.blankpaper.Width / 2), 20f);
			float num2 = -25f;
			float num3 = 1.2f;
			float num4 = 125f;
			float num5 = 22f;
			float num6 = 100f;
			float num7 = 400f;
			int num8 = 0;
			num4 -= num5;
			num4 -= num5;
			num4 -= num5;
			num4 -= num5;
			Color color = Color.White;
			Color white = Color.White;
			spriteBatch.Draw(this.paper1, new Rectangle(0, 0, 1280, 720), new Rectangle?(this.blackX), Color.White);
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.escape_key), "menus");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.lmb_key), "gas/thrust");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.rmb_key), "brake/ramp");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, "mouse", "camera look");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.w_key), "forward/pitch");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.s_key), "backward/pitch");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.a_key), "left/turn");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.d_key), "right/turn");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.space_key), "jump");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.leftshift_key), "sprint");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.r_key), "na");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.x_key), "activate");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.t_key), "callout/honk");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.enter_key), "objectives");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.f_key), "na");
			num4 += num5;
			num8++;
			if (whichKEY == num8)
			{
				color = this.grnPen;
			}
			else
			{
				color = white;
			}
			if (thisKEY == num8)
			{
				color = this.redPen;
			}
			spriteBatch.DrawString(spriteFont, "selection bar", vector + new Vector2(num6, (float)((int)num4)), color);
			Vector2 vector2 = spriteFont.MeasureString(this.one_key.ToString() + " ");
			spriteBatch.DrawString(spriteFont, this.stripD(this.one_key.ToString()) + " ", vector + new Vector2(num7, num4), color);
			this.queryKeySquare2(new Rectangle((int)(vector.X + num7), (int)(vector.Y + num4 + vector2.Y / num2), (int)vector2.X, (int)(vector2.Y / num3)), num8, ref whichKEY);
			num8++;
			if (whichKEY == num8)
			{
				color = this.grnPen;
			}
			else
			{
				color = white;
			}
			if (thisKEY == num8)
			{
				color = this.redPen;
			}
			Vector2 vector3 = spriteFont.MeasureString(this.two_key.ToString() + " ");
			spriteBatch.DrawString(spriteFont, this.stripD(this.two_key.ToString()) + " ", vector + new Vector2(vector2.X + num7, num4), color);
			this.queryKeySquare2(new Rectangle((int)(vector.X + vector2.X + num7), (int)(vector.Y + num4 + vector3.Y / num2), (int)vector3.X, (int)(vector3.Y / num3)), num8, ref whichKEY);
			num8++;
			if (whichKEY == num8)
			{
				color = this.grnPen;
			}
			else
			{
				color = white;
			}
			if (thisKEY == num8)
			{
				color = this.redPen;
			}
			Vector2 vector4 = spriteFont.MeasureString(this.three_key.ToString() + " ");
			spriteBatch.DrawString(spriteFont, this.stripD(this.three_key.ToString()) + " ", vector + new Vector2(vector3.X + vector2.X + num7, num4), color);
			this.queryKeySquare2(new Rectangle((int)(vector.X + vector3.X + vector2.X + num7), (int)(vector.Y + num4 + vector4.Y / num2), (int)vector4.X, (int)(vector4.Y / num3)), num8, ref whichKEY);
			num8++;
			if (whichKEY == num8)
			{
				color = this.grnPen;
			}
			else
			{
				color = white;
			}
			if (thisKEY == num8)
			{
				color = this.redPen;
			}
			Vector2 vector5 = spriteFont.MeasureString(this.four_key.ToString() + " ");
			spriteBatch.DrawString(spriteFont, this.stripD(this.four_key.ToString()) + " ", vector + new Vector2(vector4.X + vector3.X + vector2.X + num7, num4), color);
			this.queryKeySquare2(new Rectangle((int)(vector.X + vector4.X + vector3.X + vector2.X + num7), (int)(vector.Y + num4 + vector5.Y / num2), (int)vector5.X, (int)(vector5.Y / num3)), num8, ref whichKEY);
			num4 += num5;
			num8++;
			if (whichKEY == num8)
			{
				color = this.grnPen;
			}
			else
			{
				color = white;
			}
			if (thisKEY == num8)
			{
				color = this.redPen;
			}
			spriteBatch.DrawString(spriteFont, "camera edit :", vector + new Vector2(num6, (float)((int)num4)), color);
			vector2 = spriteFont.MeasureString(this.f1_key.ToString() + " ");
			spriteBatch.DrawString(spriteFont, this.f1_key.ToString() + " ", vector + new Vector2(num7, num4), color);
			this.queryKeySquare2(new Rectangle((int)(vector.X + num7), (int)(vector.Y + num4 + vector2.Y / num2), (int)vector2.X, (int)(vector2.Y / num3)), num8, ref whichKEY);
			num8++;
			num8++;
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.tab_key), "camera views");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.u_key), "camera unlock");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.left_key), "select bar left");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.right_key), "select bar right");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.up_key), "activate selection");
			this.drawInteractive3(vector, spriteBatch, ref whichKEY, ref thisKEY, font2, ref num8, ref num4, this.getKeyName(this.e_key), "na");
			if (whichKEY == 36 || whichKEY == 30 || whichKEY == 33)
			{
				whichKEY = 0;
				thisKEY = 0;
			}
			this.queryKeySquare2(new Rectangle((int)vector.X + 520, (int)vector.Y + 40, 50, 26), 36, ref whichKEY);
			this.queryKeySquare2(new Rectangle((int)vector.X + 221, (int)vector.Y + 629, 71, 34), 30, ref whichKEY);
			this.queryKeySquare2(new Rectangle((int)vector.X + 341, (int)vector.Y + 629, 71, 34), 33, ref whichKEY);
			spriteBatch.Draw(this.paper1, vector + new Vector2(520f, 40f), new Rectangle?(this.cancelButtonX), Color.White);
			spriteBatch.Draw(this.paper1, vector + new Vector2(200f, 629f), new Rectangle?(this.saveButtonX), Color.White);
			spriteBatch.Draw(this.paper1, vector + new Vector2(341f, 629f), new Rectangle?(this.resetButtonX), Color.White);
			if (whichKEY == 36)
			{
				spriteBatch.Draw(this.paper1, vector + new Vector2(520f, 40f), new Rectangle?(this.cancelButtonX), new Color(0, 90, 245));
			}
			if (whichKEY == 30)
			{
				spriteBatch.Draw(this.paper1, vector + new Vector2(200f, 629f), new Rectangle?(this.saveButtonX), new Color(0, 90, 245));
			}
			if (whichKEY == 33)
			{
				spriteBatch.Draw(this.paper1, vector + new Vector2(341f, 629f), new Rectangle?(this.resetButtonX), new Color(0, 90, 245));
			}
			if (thisKEY != 0 && thisKEY != 33 && thisKEY != 30 && thisKEY != 36)
			{
				string text = "Press Any Key\nTo Remap Red Key";
				spriteBatch.DrawString(spriteFont, text, new Vector2(980f, 340f), this.redPen);
				return;
			}
			if (whichKEY != 0 && whichKEY != 33 && whichKEY != 30 && whichKEY != 36)
			{
				string text2 = "Left Mouse Click\nTo Select Key";
				spriteBatch.DrawString(spriteFont, text2, new Vector2(980f, 340f), this.grnPen);
				return;
			}
			if (whichKEY == 0 || whichKEY == 33 || whichKEY == 30 || whichKEY == 36)
			{
				string text3 = "Remap Keys Use\nMouse to Highlight";
				spriteBatch.DrawString(spriteFont, text3, new Vector2(980f, 340f), Color.White);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00018F18 File Offset: 0x00017118
		public string stripD(string thing)
		{
			return thing.Replace("D", "");
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00018F38 File Offset: 0x00017138
		public void Roverbones()
		{
			this.bodyBone = this.roverModel.Bones["body"];
			this.haulerBone = this.roverModel.Bones["hauler1"];
			this.weaponBone = this.roverModel.Bones["weapon1"];
			this.rackBone = this.roverModel.Bones["rack"];
			this.scooperBone = this.roverModel.Bones["scooper1"];
			this.BackWheelBone = this.roverModel.Bones["backwheel1"];
			this.leftFrontWheelBone = this.roverModel.Bones["LF1"];
			this.rightFrontWheelBone = this.roverModel.Bones["RF1"];
			this.leftFrontjointBone = this.roverModel.Bones["LFjoint"];
			this.rightFrontjointBone = this.roverModel.Bones["RFjoint"];
			this.solar1aBone = this.roverModel.Bones["solar1"];
			this.solar1bBone = this.roverModel.Bones["solar1b"];
			this.rackTrans = this.rackBone.Transform;
			this.bodyTrans = this.bodyBone.Transform;
			this.haulerTrans = this.haulerBone.Transform;
			this.weaponTrans = this.weaponBone.Transform;
			this.scooperTrans = this.scooperBone.Transform;
			this.BackWheelTrans = this.BackWheelBone.Transform;
			this.leftFrontWheelTrans = this.leftFrontWheelBone.Transform;
			this.rightFrontWheelTrans = this.rightFrontWheelBone.Transform;
			this.leftFrontjointTrans = this.leftFrontjointBone.Transform;
			this.rightFrontjointTrans = this.rightFrontjointBone.Transform;
			this.solar1aTrans = this.solar1aBone.Transform;
			this.solar1bTrans = this.solar1bBone.Transform;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00019158 File Offset: 0x00017358
		public void RoverEquip()
		{
			this.roverModel.CopyBoneTransformsFrom(this.origTransforms);
			this.roverparts.Clear();
			this.roverparts.Add("body");
			this.roverparts.Add("rack");
			this.rackBone = this.roverModel.Bones["rack"];
			this.bodyBone = this.roverModel.Bones["body"];
			this.rackTrans = this.rackBone.Transform;
			this.bodyTrans = this.bodyBone.Transform;
			this.roverparts.Add("RFjoint");
			this.roverparts.Add("LFjoint");
			this.leftFrontjointBone = this.roverModel.Bones["LFjoint"];
			this.rightFrontjointBone = this.roverModel.Bones["RFjoint"];
			this.leftFrontjointTrans = this.leftFrontjointBone.Transform;
			this.rightFrontjointTrans = this.rightFrontjointBone.Transform;
			int num = this.equip[0];
			int num2 = this.equip[3];
			int num3 = this.equip[6];
			int num4 = this.equip[9];
			int num5 = this.equip[12];
			int num6 = this.equip[15];
			this.roverparts.Add("weapon" + num6);
			this.weaponBone = this.roverModel.Bones["weapon" + num6];
			this.weaponTrans = this.weaponBone.Transform;
			this.roverparts.Add("solar" + num3);
			this.solar1aBone = this.roverModel.Bones["solar" + num3];
			this.solar1aTrans = this.solar1aBone.Transform;
			this.solar1bTrans = Matrix.Identity;
			if (num3 == 1)
			{
				this.roverparts.Add("solar1b");
				this.solar1bBone = this.roverModel.Bones["solar1b"];
				this.solar1bTrans = this.solar1bBone.Transform;
			}
			this.roverparts.Add("scooper" + num2);
			this.scooperBone = this.roverModel.Bones["scooper" + num2];
			this.scooperTrans = this.scooperBone.Transform;
			this.scooperMatrix = Matrix.CreateRotationX(0f) * Matrix.CreateTranslation(0f, 21.825f, 8.95f);
			this.scooperBone.Transform = this.scooperMatrix * this.scooperTrans;
			if (num4 == 1)
			{
				Rover.realGrav = -0.6f;
				this.roverGrip = 0.6f;
				Rover.fric = this.roverGrip;
				Rover.fricOrig = this.roverGrip;
				this.roverSpeed = -50f;
				Rover.max = this.roverSpeed;
				this.roverTurn = 0.11f;
				Rover.turnSpeed = this.roverTurn;
			}
			if (num4 == 2)
			{
				Rover.realGrav = -1.8f;
				this.roverGrip = 0.2f;
				Rover.fric = this.roverGrip;
				Rover.fricOrig = this.roverGrip;
				this.roverSpeed = -25f;
				Rover.max = this.roverSpeed;
				this.roverTurn = 0.35f;
				Rover.turnSpeed = this.roverTurn;
			}
			if (num4 == 3)
			{
				Rover.realGrav = -1f;
				this.roverGrip = 0.92f;
				Rover.fric = this.roverGrip;
				Rover.fricOrig = this.roverGrip;
				this.roverSpeed = -40f;
				Rover.max = this.roverSpeed;
				this.roverTurn = 0.11f;
				Rover.turnSpeed = this.roverTurn;
			}
			this.roverparts.Add("RF" + num4);
			this.roverparts.Add("LF" + num4);
			this.roverparts.Add("backwheel" + num4);
			this.leftFrontWheelBone = this.roverModel.Bones["LF" + num4];
			this.rightFrontWheelBone = this.roverModel.Bones["RF" + num4];
			this.leftFrontWheelTrans = this.leftFrontWheelBone.Transform;
			this.rightFrontWheelTrans = this.rightFrontWheelBone.Transform;
			this.BackWheelBone = this.roverModel.Bones["backwheel" + num4];
			this.BackWheelTrans = this.BackWheelBone.Transform;
			this.roverparts.Add("hauler" + num5);
			this.haulerBone = this.roverModel.Bones["hauler" + num5];
			this.haulerTrans = this.haulerBone.Transform;
			this.wheelRollMatrix = Matrix.CreateRotationX(0f);
			this.wheelRollMatrix2 = Matrix.CreateRotationY(0f);
			this.rackMatrix = Matrix.CreateTranslation((float)Math.Sin(0.0) * 4f, 0f, (float)(-(float)Math.Cos(0.0)) * 120f + 114f);
		}

		// Token: 0x04000001 RID: 1
		public int tunnelUppy = 10;

		// Token: 0x04000002 RID: 2
		public bool showTunnels;

		// Token: 0x04000003 RID: 3
		public int totalPlayers = 2;

		// Token: 0x04000004 RID: 4
		public List<int> tunnelDay = new List<int> { 2, 7, 12, 18, 23 };

		// Token: 0x04000005 RID: 5
		public List<int> heirlooms = new List<int> { 0, 0, 0, 0, 0, 0, 0 };

		// Token: 0x04000006 RID: 6
		public int ammoboxCount;

		// Token: 0x04000007 RID: 7
		public int cogCount;

		// Token: 0x04000008 RID: 8
		public int flashlight1;

		// Token: 0x04000009 RID: 9
		public int flashlight2;

		// Token: 0x0400000A RID: 10
		public int flashlight3;

		// Token: 0x0400000B RID: 11
		public int goggles;

		// Token: 0x0400000C RID: 12
		public int redskull1;

		// Token: 0x0400000D RID: 13
		public int redskull2;

		// Token: 0x0400000E RID: 14
		public int redskull3;

		// Token: 0x0400000F RID: 15
		public int tusk1;

		// Token: 0x04000010 RID: 16
		public int tusk2;

		// Token: 0x04000011 RID: 17
		public int tusk3;

		// Token: 0x04000012 RID: 18
		public int[] map = new int[20];

		// Token: 0x04000013 RID: 19
		public int[] ammobox1 = new int[20];

		// Token: 0x04000014 RID: 20
		public int[] ammobox2 = new int[20];

		// Token: 0x04000015 RID: 21
		public int[] ammobox3 = new int[20];

		// Token: 0x04000016 RID: 22
		public int[] cog1 = new int[20];

		// Token: 0x04000017 RID: 23
		public int[] cog2 = new int[20];

		// Token: 0x04000018 RID: 24
		public int[] cog3 = new int[20];

		// Token: 0x04000019 RID: 25
		public int[] exitkey = new int[20];

		// Token: 0x0400001A RID: 26
		public int[,] code1 = new int[20, 3];

		// Token: 0x0400001B RID: 27
		public int[,] code2 = new int[20, 3];

		// Token: 0x0400001C RID: 28
		public int[,] code3 = new int[20, 3];

		// Token: 0x0400001D RID: 29
		public bool localLoad = true;

		// Token: 0x0400001E RID: 30
		public float litex;

		// Token: 0x0400001F RID: 31
		public float litey;

		// Token: 0x04000020 RID: 32
		public float litez;

		// Token: 0x04000021 RID: 33
		public Color hostblue = new Color(0, 78, 255, 255);

		// Token: 0x04000022 RID: 34
		public float notch;

		// Token: 0x04000023 RID: 35
		public bool formDrawBand;

		// Token: 0x04000024 RID: 36
		public bool formDrawBG;

		// Token: 0x04000025 RID: 37
		public int formBandColorIndex;

		// Token: 0x04000026 RID: 38
		public Color[] formBandColor = new Color[]
		{
			new Color(225, 178, 30, 255),
			new Color(32, 210, 19, 255),
			new Color(42, 137, 217, 255),
			new Color(166, 86, 178, 255),
			new Color(175, 52, 52, 255)
		};

		// Token: 0x04000027 RID: 39
		public int formBGColorIndex;

		// Token: 0x04000028 RID: 40
		public Color[] formBGColor = new Color[]
		{
			new Color(210, 210, 210, 255),
			new Color(180, 180, 180, 255),
			new Color(110, 110, 110, 255),
			new Color(60, 60, 60, 255),
			new Color(15, 15, 15, 255)
		};

		// Token: 0x04000029 RID: 41
		public Rectangle formBand = new Rectangle(1020, 77, 132, 125);

		// Token: 0x0400002A RID: 42
		public Rectangle formBandL = new Rectangle(0, 0, 132, 125);

		// Token: 0x0400002B RID: 43
		public Rectangle zoomRectangle = new Rectangle(0, 0, 0, 0);

		// Token: 0x0400002C RID: 44
		public Matrix[] wallguns = new Matrix[21];

		// Token: 0x0400002D RID: 45
		public float[] recoilTime = new float[]
		{
			0.5f, 0f, 0.5f, 0f, 0.5f, 0f, 1.1f, 0f, 1f, 0f,
			1.2f, 0f, 1.2f, 0f, 0.5f, 0f, 0.5f, 0f, 1.1f, 0f,
			1.2f
		};

		// Token: 0x0400002E RID: 46
		public int[] recoilA = new int[]
		{
			45, 0, 25, 0, 25, 0, 35, 0, 50, 0,
			20, 0, 38, 0, 10, 0, 10, 0, 20, 0,
			35
		};

		// Token: 0x0400002F RID: 47
		public int[] recoilB = new int[]
		{
			110, 0, 60, 0, 40, 0, 54, 0, 130, 0,
			30, 0, 60, 0, 10, 0, 10, 0, 50, 0,
			45
		};

		// Token: 0x04000030 RID: 48
		public float[] gunDam = new float[]
		{
			2f, 0f, 1f, 0f, 1f, 0f, 1.3f, 0f, 3f, 0f,
			1.15f, 0f, 0.9f, 0f, 1f, 0f, 0.5f, 0f, 0.8f, 0f,
			1.4f
		};

		// Token: 0x04000031 RID: 49
		public int[] gunVibro = new int[]
		{
			18, 0, 10, 0, 10, 0, 12, 0, 25, 0,
			15, 0, 11, 0, 80, 0, 10, 0, 10, 0,
			35
		};

		// Token: 0x04000032 RID: 50
		public float[] gunVibroAmt = new float[]
		{
			80f, 0f, 120f, 0f, 80f, 0f, 110f, 0f, 80f, 0f,
			100f, 0f, 100f, 0f, 80f, 0f, 12f, 0f, 20f, 0f,
			150f
		};

		// Token: 0x04000033 RID: 51
		public float[] gunDelay = new float[]
		{
			9f, 0f, 8f, 0f, 8f, 0f, 7f, 0f, 16f, 0f,
			7f, 0f, 6f, 0f, 29f, 0f, 9f, 0f, 6f, 0f,
			5f
		};

		// Token: 0x04000034 RID: 52
		public float[] gunRadius = new float[]
		{
			700f, 0f, 600f, 0f, 100f, 0f, 1000f, 0f, 1500f, 0f,
			1000f, 0f, 3500f, 0f, 10f, 0f, 600f, 0f, 1100f, 0f,
			1200f
		};

		// Token: 0x04000035 RID: 53
		public Vector2[] sf = new Vector2[]
		{
			new Vector2(50f, 110f),
			new Vector2(0f, 0f),
			new Vector2(50f, 110f),
			new Vector2(0f, 0f),
			new Vector2(10f, 30f),
			new Vector2(0f, 0f),
			new Vector2(120f, 150f),
			new Vector2(0f, 0f),
			new Vector2(160f, 200f),
			new Vector2(0f, 0f),
			new Vector2(130f, 210f),
			new Vector2(0f, 0f),
			new Vector2(50f, 110f),
			new Vector2(0f, 0f),
			new Vector2(180f, 230f),
			new Vector2(0f, 0f),
			new Vector2(50f, 110f),
			new Vector2(0f, 0f),
			new Vector2(80f, 120f),
			new Vector2(0f, 0f),
			new Vector2(180f, 190f),
			new Vector2(0f, 0f),
			new Vector2(0f, 0f),
			new Vector2(0f, 0f)
		};

		// Token: 0x04000036 RID: 54
		public Vector2[] ff = new Vector2[]
		{
			new Vector2(35f, 55f),
			new Vector2(0f, 0f),
			new Vector2(35f, 55f),
			new Vector2(0f, 0f),
			new Vector2(10f, 30f),
			new Vector2(0f, 0f),
			new Vector2(65f, 85f),
			new Vector2(0f, 0f),
			new Vector2(90f, 100f),
			new Vector2(0f, 0f),
			new Vector2(60f, 90f),
			new Vector2(0f, 0f),
			new Vector2(35f, 55f),
			new Vector2(0f, 0f),
			new Vector2(150f, 195f),
			new Vector2(0f, 0f),
			new Vector2(35f, 55f),
			new Vector2(0f, 0f),
			new Vector2(35f, 55f),
			new Vector2(0f, 0f),
			new Vector2(75f, 85f),
			new Vector2(0f, 0f),
			new Vector2(0f, 0f),
			new Vector2(0f, 0f)
		};

		// Token: 0x04000037 RID: 55
		public Matrix[] flashOffset = new Matrix[]
		{
			Matrix.CreateTranslation(5.85f, 2.77f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateTranslation(4.6f, 1.93f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateTranslation(7.8f, 2f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateTranslation(15.47f, 2.25f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateTranslation(15.45f, 1.831f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateTranslation(17.7f, 1.831f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateTranslation(9.5f, 1.35f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateTranslation(16.4f, 3f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateTranslation(5.85f, 2.77f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateTranslation(4.6f, 1.6f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateTranslation(14f, 2f, 0f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(187f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity
		};

		// Token: 0x04000038 RID: 56
		public Matrix[] shellExit = new Matrix[]
		{
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(1f, 1.6f, 1f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(-1f, 1.8f, 0f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(-1f, 1.8f, 0f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(2.7f, 1.8f, 0.3f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(2.7f, 1.8f, 0.3f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(1.2f, 1.8f, 0.3f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(-1.5f, 1.8f, 0.6f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(1.2f, 1.8f, 0.3f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(1f, 1.6f, 1f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(-4f, 1.8f, 0.4f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity,
			Matrix.CreateRotationY(-1.57f) * Matrix.CreateTranslation(1f, 2f, 1f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(11f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f),
			Matrix.Identity
		};

		// Token: 0x04000039 RID: 57
		public Matrix flashlightOffset = Matrix.CreateTranslation(0f, -5f, 0f) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(8f), MathHelper.ToRadians(-86f), MathHelper.ToRadians(-180f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f);

		// Token: 0x0400003A RID: 58
		public Matrix gunOffset = Matrix.CreateRotationX(MathHelper.ToRadians(-90f)) * Matrix.CreateRotationY(MathHelper.ToRadians(186.8f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1f)) * Matrix.CreateTranslation(-24.784f, 42.639f, 2.188f);

		// Token: 0x0400003B RID: 59
		public float[] axGun = new float[]
		{
			0f, 0f, 0f, 0f, 0f, 0f, 0.011f, 0f, 0.011f, 0f,
			0.002f, 0f, 0.011f, 0f, 0.002f, 0f, 0f, 0f, 0f, 0f,
			0.011f
		};

		// Token: 0x0400003C RID: 60
		public float[] ayGun = new float[]
		{
			0f, 0f, 0f, 0f, 0f, 0f, -0.038f, 0f, -0.038f, 0f,
			-0.091f, 0f, -0.038f, 0f, -0.091f, 0f, 0f, 0f, 0f, 0f,
			-0.038f
		};

		// Token: 0x0400003D RID: 61
		public float[] bxGun = new float[]
		{
			0f, 0f, 0f, 0f, 0f, 0f, -0.531f, 0f, -0.531f, 0f,
			-0.722f, 0f, -0.531f, 0f, -0.722f, 0f, 0f, 0f, 0f, 0f,
			-0.531f
		};

		// Token: 0x0400003E RID: 62
		public float[] byGun = new float[]
		{
			0f, 0f, 0f, 0f, 0f, 0f, -0.193f, 0f, -0.193f, 0f,
			-0.214f, 0f, -0.193f, 0f, -0.214f, 0f, 0f, 0f, 0f, 0f,
			-0.193f
		};

		// Token: 0x0400003F RID: 63
		private ScreenManager.xcross tempcross1 = new ScreenManager.xcross();

		// Token: 0x04000040 RID: 64
		private ScreenManager.xcross tempcross2 = new ScreenManager.xcross();

		// Token: 0x04000041 RID: 65
		private ScreenManager.xcross tempcross3 = new ScreenManager.xcross();

		// Token: 0x04000042 RID: 66
		private ScreenManager.xcross tempcross4 = new ScreenManager.xcross();

		// Token: 0x04000043 RID: 67
		public List<ScreenManager.xcross> crosshair1 = new List<ScreenManager.xcross>();

		// Token: 0x04000044 RID: 68
		public List<Texture2D> crosshairC = new List<Texture2D>();

		// Token: 0x04000045 RID: 69
		public Texture2D basicCrosshair1;

		// Token: 0x04000046 RID: 70
		public Texture2D basicCrosshair2;

		// Token: 0x04000047 RID: 71
		public Texture2D basicCrosshair3;

		// Token: 0x04000048 RID: 72
		public Texture2D basicCrosshair4;

		// Token: 0x04000049 RID: 73
		public int crossIndex;

		// Token: 0x0400004A RID: 74
		public bool allachieves;

		// Token: 0x0400004B RID: 75
		public bool lockVideosetup;

		// Token: 0x0400004C RID: 76
		public bool dlcreleased = true;

		// Token: 0x0400004D RID: 77
		public int cryptHits = 666;

		// Token: 0x0400004E RID: 78
		public int cryptHitsReset = 666;

		// Token: 0x0400004F RID: 79
		public int takepic;

		// Token: 0x04000050 RID: 80
		public bool bonusweek;

		// Token: 0x04000051 RID: 81
		public bool developer;

		// Token: 0x04000052 RID: 82
		public bool guestpresent;

		// Token: 0x04000053 RID: 83
		public bool guest;

		// Token: 0x04000054 RID: 84
		public bool hordemode;

		// Token: 0x04000055 RID: 85
		public bool tunnelMode;

		// Token: 0x04000056 RID: 86
		public bool paintland;

		// Token: 0x04000057 RID: 87
		public int stats_pumpkins;

		// Token: 0x04000058 RID: 88
		public int stats_total;

		// Token: 0x04000059 RID: 89
		public bool forcedout;

		// Token: 0x0400005A RID: 90
		public bool nomovies;

		// Token: 0x0400005B RID: 91
		public string scrnfilename = "";

		// Token: 0x0400005C RID: 92
		public Viewport viewportX = new Viewport(0, 0, 1280, 720);

		// Token: 0x0400005D RID: 93
		public Vector2 textPosition;

		// Token: 0x0400005E RID: 94
		public List<string> kickplayerName = new List<string>();

		// Token: 0x0400005F RID: 95
		public List<CSteamID> kickplayerID = new List<CSteamID>();

		// Token: 0x04000060 RID: 96
		public CSteamID kickID = default(CSteamID);

		// Token: 0x04000061 RID: 97
		public string[] fakenames = new string[]
		{
			"DeadlyPelican", "K9Nine619", "Kacil", "Kaeede", "Ticalisoul", "SinfulDarkRain", "kramberry69", "Kain173", "UnearnedOrphan", "RainbowDashOps",
			"LilERome", "kalekemo", "tishy19", "SHINO BAZ", "Honeybeezy", "Lopez Ya Dig", "Vash Yudai", "Furiousfreb", "nytemystress", "kase2254",
			"HarlotSanctuary", "Nintendo", "Ca1iso", "KatanOmega", "katj93", "Hobag00782", "GNR Nightfall", "CNUT21", "KazerN4", "colonelcheru",
			"Soshii", "AsianAvery", "KcTheGinger", "Krennar", "Reek Gz", "Ken Is So Wavy", "BeheldKerreK", "BumbleBee13035", "Avatar Zadkiel", "keving813",
			"w1cked Dragon", "I DUNNO", "kfnnapa", "snub killer75", "KoA xBIGxBOSSx", "munkee21", "Still Element", "Boston96", "Aiea Boy", "kienenator1",
			"Kiggy429", "SamplingKILLA", "IDOCOLOMBIANS69", "KillaEyez1121", "Killerblonde41", "blackpantha2", "Fred The Spy", "wraith2345", "KillzoneX5", "x1scorpio1x",
			"Kimogila", "elitesplicer247", "King Killa T", "PhilliesPwnYou", "Malice 731", "aMp FuZioN", "JackxBauerxCTU", "Sktchd Drknss", "king mat 93", "Kingofnot7",
			"Dragon King", "Kickakapow", "kirakomrade", "elksdb19", "c0uch tr0ll", "SuicideSpartan", "SirDragonKitty", "Kisom", "xEmbrac3", "animefan1",
			"A2K Kitta", "kittenx2008", "Kizsurian", "Kjgmusic", "Krack Dojo", "Posui", "KayMomo", "knb", "Kneepad", "DrzChamp777",
			"Knux7654", "KO M0NKEY", "KoA xBIGxBOSSx", "DoomShadow24", "KOF SHOPPA", "kogosamaru", "SSG KOHJEX", "Daislynn", "koolaid252", "GameTriforce4"
		};

		// Token: 0x04000062 RID: 98
		public string[] dumAss = new string[]
		{
			"Use BounceBack when Bleeding Out", "Can't take BounceBack while standing", "No, you can't use that now", "You're standing now, you idiot", "No please, ..stop pressing that button", "Why are doing this?", "Stop trying to use BounceBack", "Shit you're Dumb", "Stop or you will be booted", "I don't think you understand Bounceback",
			"BounceBack is for Later silly", "Hey dumbass get a life", "I'm going to boot you right now", "it's time to tell you a story", "I grew up in canada on my dad's pig farm", "he was mental and would kidnap people", "I was afraid of him he had a temper", "he built a huge industrial meat grinder", "a horrible smell came from our backyard", "One day I went to see what it was",
			"dead bodies everywhere, and rotting", "I woke up one night to quiet screaming", "my father was staring at me", "I finally ran away from home", "and I told the police about that farm", "my twin brother vanished too", "he stole my last pair of green overalls", "but that story is for another day", "I built this farm with my 2 bare hands", "I learned a lot from my father",
			"Would you like to see my meat grinder?", "yeah life is hard for me", "Sometimes I cry uncontrollably", "I get so angry I want to smash everytyhing", "But I would never hurt you...."
		};

		// Token: 0x04000063 RID: 99
		public bool hover;

		// Token: 0x04000064 RID: 100
		public bool myplayerCheats;

		// Token: 0x04000065 RID: 101
		public List<ulong> strangers = new List<ulong>();

		// Token: 0x04000066 RID: 102
		public int drwhoCount;

		// Token: 0x04000067 RID: 103
		public bool friended;

		// Token: 0x04000068 RID: 104
		public List<ulong> kickers = new List<ulong>();

		// Token: 0x04000069 RID: 105
		public List<int> redsuns = new List<int>();

		// Token: 0x0400006A RID: 106
		public bool hostAllowCheats = true;

		// Token: 0x0400006B RID: 107
		public bool allWeapons = true;

		// Token: 0x0400006C RID: 108
		public bool hostFriendly;

		// Token: 0x0400006D RID: 109
		public bool hostBobbleheads;

		// Token: 0x0400006E RID: 110
		public bool shownames;

		// Token: 0x0400006F RID: 111
		public string glowtype = "EdgeDetect";

		// Token: 0x04000070 RID: 112
		public CSteamID hostowner = default(CSteamID);

		// Token: 0x04000071 RID: 113
		public int myplayerindex;

		// Token: 0x04000072 RID: 114
		public Color[] colors = new Color[]
		{
			new Color(237, 28, 36, 255),
			new Color(242, 212, 0, 255),
			new Color(0, 171, 0, 255),
			new Color(255, 140, 0, 255),
			Color.Gold,
			Color.Cyan,
			Color.PaleGreen,
			Color.PeachPuff
		};

		// Token: 0x04000073 RID: 115
		public bool blackandwhite = true;

		// Token: 0x04000074 RID: 116
		private string[] dayTips;

		// Token: 0x04000075 RID: 117
		public bool usingMouse;

		// Token: 0x04000076 RID: 118
		public bool host = true;

		// Token: 0x04000077 RID: 119
		public bool rebuildFog;

		// Token: 0x04000078 RID: 120
		public bool rebuildTwinSplat;

		// Token: 0x04000079 RID: 121
		public bool deactivated;

		// Token: 0x0400007A RID: 122
		public bool rebuildTargets;

		// Token: 0x0400007B RID: 123
		public int tempRifle = 2;

		// Token: 0x0400007C RID: 124
		public int tempPistol = 2;

		// Token: 0x0400007D RID: 125
		public int tempAmmo;

		// Token: 0x0400007E RID: 126
		public int tempMag;

		// Token: 0x0400007F RID: 127
		public int bossexplodechoice = 1;

		// Token: 0x04000080 RID: 128
		public bool setgraphics;

		// Token: 0x04000081 RID: 129
		public bool justsetgraphics;

		// Token: 0x04000082 RID: 130
		public bool drawme = true;

		// Token: 0x04000083 RID: 131
		public int mousefade;

		// Token: 0x04000084 RID: 132
		public Vector2 mymouse = Vector2.Zero;

		// Token: 0x04000085 RID: 133
		public float mylens = 58f;

		// Token: 0x04000086 RID: 134
		public float myfov = 0.75f;

		// Token: 0x04000087 RID: 135
		public int leaderboardCounter;

		// Token: 0x04000088 RID: 136
		public int leaderboardSelect = 1;

		// Token: 0x04000089 RID: 137
		public achieve trophy;

		// Token: 0x0400008A RID: 138
		public Lobby lobby;

		// Token: 0x0400008B RID: 139
		public Workshop workshop;

		// Token: 0x0400008C RID: 140
		public goldenkey goldKeys;

		// Token: 0x0400008D RID: 141
		public Microsoft.Xna.Framework.Input.Keys lmb_key = Microsoft.Xna.Framework.Input.Keys.VolumeDown;

		// Token: 0x0400008E RID: 142
		public Microsoft.Xna.Framework.Input.Keys mmb_key = Microsoft.Xna.Framework.Input.Keys.VolumeMute;

		// Token: 0x0400008F RID: 143
		public Microsoft.Xna.Framework.Input.Keys rmb_key = Microsoft.Xna.Framework.Input.Keys.VolumeUp;

		// Token: 0x04000090 RID: 144
		public Microsoft.Xna.Framework.Input.Keys but1_key = Microsoft.Xna.Framework.Input.Keys.Print;

		// Token: 0x04000091 RID: 145
		public Microsoft.Xna.Framework.Input.Keys but2_key = Microsoft.Xna.Framework.Input.Keys.PrintScreen;

		// Token: 0x04000092 RID: 146
		public Microsoft.Xna.Framework.Input.Keys escape_key = Microsoft.Xna.Framework.Input.Keys.Escape;

		// Token: 0x04000093 RID: 147
		public Microsoft.Xna.Framework.Input.Keys w_key = Microsoft.Xna.Framework.Input.Keys.W;

		// Token: 0x04000094 RID: 148
		public Microsoft.Xna.Framework.Input.Keys a_key = Microsoft.Xna.Framework.Input.Keys.A;

		// Token: 0x04000095 RID: 149
		public Microsoft.Xna.Framework.Input.Keys s_key = Microsoft.Xna.Framework.Input.Keys.S;

		// Token: 0x04000096 RID: 150
		public Microsoft.Xna.Framework.Input.Keys d_key = Microsoft.Xna.Framework.Input.Keys.D;

		// Token: 0x04000097 RID: 151
		public Microsoft.Xna.Framework.Input.Keys f_key = Microsoft.Xna.Framework.Input.Keys.F;

		// Token: 0x04000098 RID: 152
		public Microsoft.Xna.Framework.Input.Keys q_key = Microsoft.Xna.Framework.Input.Keys.Q;

		// Token: 0x04000099 RID: 153
		public Microsoft.Xna.Framework.Input.Keys e_key = Microsoft.Xna.Framework.Input.Keys.E;

		// Token: 0x0400009A RID: 154
		public Microsoft.Xna.Framework.Input.Keys x_key = Microsoft.Xna.Framework.Input.Keys.X;

		// Token: 0x0400009B RID: 155
		public string myXkey = "x_key";

		// Token: 0x0400009C RID: 156
		public Microsoft.Xna.Framework.Input.Keys r_key = Microsoft.Xna.Framework.Input.Keys.R;

		// Token: 0x0400009D RID: 157
		public Microsoft.Xna.Framework.Input.Keys b_key = Microsoft.Xna.Framework.Input.Keys.B;

		// Token: 0x0400009E RID: 158
		public Microsoft.Xna.Framework.Input.Keys t_key = Microsoft.Xna.Framework.Input.Keys.T;

		// Token: 0x0400009F RID: 159
		public Microsoft.Xna.Framework.Input.Keys u_key = Microsoft.Xna.Framework.Input.Keys.Space;

		// Token: 0x040000A0 RID: 160
		public Microsoft.Xna.Framework.Input.Keys enter_key = Microsoft.Xna.Framework.Input.Keys.Enter;

		// Token: 0x040000A1 RID: 161
		public Microsoft.Xna.Framework.Input.Keys up_key = Microsoft.Xna.Framework.Input.Keys.Up;

		// Token: 0x040000A2 RID: 162
		public Microsoft.Xna.Framework.Input.Keys down_key = Microsoft.Xna.Framework.Input.Keys.Down;

		// Token: 0x040000A3 RID: 163
		public Microsoft.Xna.Framework.Input.Keys right_key = Microsoft.Xna.Framework.Input.Keys.Right;

		// Token: 0x040000A4 RID: 164
		public Microsoft.Xna.Framework.Input.Keys left_key = Microsoft.Xna.Framework.Input.Keys.Left;

		// Token: 0x040000A5 RID: 165
		public Microsoft.Xna.Framework.Input.Keys dead_key = Microsoft.Xna.Framework.Input.Keys.Left;

		// Token: 0x040000A6 RID: 166
		public Microsoft.Xna.Framework.Input.Keys f10_key = Microsoft.Xna.Framework.Input.Keys.F10;

		// Token: 0x040000A7 RID: 167
		public Microsoft.Xna.Framework.Input.Keys plus_key = Microsoft.Xna.Framework.Input.Keys.OemPlus;

		// Token: 0x040000A8 RID: 168
		public Microsoft.Xna.Framework.Input.Keys space_key = Microsoft.Xna.Framework.Input.Keys.Space;

		// Token: 0x040000A9 RID: 169
		public chatbox chat;

		// Token: 0x040000AA RID: 170
		public Microsoft.Xna.Framework.Input.Keys one_key = Microsoft.Xna.Framework.Input.Keys.D1;

		// Token: 0x040000AB RID: 171
		public Microsoft.Xna.Framework.Input.Keys two_key = Microsoft.Xna.Framework.Input.Keys.D2;

		// Token: 0x040000AC RID: 172
		public Microsoft.Xna.Framework.Input.Keys three_key = Microsoft.Xna.Framework.Input.Keys.D3;

		// Token: 0x040000AD RID: 173
		public Microsoft.Xna.Framework.Input.Keys four_key = Microsoft.Xna.Framework.Input.Keys.D4;

		// Token: 0x040000AE RID: 174
		public Microsoft.Xna.Framework.Input.Keys tab_key = Microsoft.Xna.Framework.Input.Keys.Tab;

		// Token: 0x040000AF RID: 175
		public Microsoft.Xna.Framework.Input.Keys f1_key = Microsoft.Xna.Framework.Input.Keys.F1;

		// Token: 0x040000B0 RID: 176
		public Microsoft.Xna.Framework.Input.Keys f2_key = Microsoft.Xna.Framework.Input.Keys.F2;

		// Token: 0x040000B1 RID: 177
		public Microsoft.Xna.Framework.Input.Keys f3_key = Microsoft.Xna.Framework.Input.Keys.F3;

		// Token: 0x040000B2 RID: 178
		public Microsoft.Xna.Framework.Input.Keys leftshift_key = Microsoft.Xna.Framework.Input.Keys.LeftShift;

		// Token: 0x040000B3 RID: 179
		private KeyboardState keyState;

		// Token: 0x040000B4 RID: 180
		private bool skipsettings;

		// Token: 0x040000B5 RID: 181
		public bool drawViewport = true;

		// Token: 0x040000B6 RID: 182
		public Rectangle screenSize;

		// Token: 0x040000B7 RID: 183
		public Rectangle origSize;

		// Token: 0x040000B8 RID: 184
		public float startResX;

		// Token: 0x040000B9 RID: 185
		public float startResY;

		// Token: 0x040000BA RID: 186
		public int width;

		// Token: 0x040000BB RID: 187
		public int hite;

		// Token: 0x040000BC RID: 188
		public float aspect;

		// Token: 0x040000BD RID: 189
		public float orighite;

		// Token: 0x040000BE RID: 190
		public bool easter_skulltalk;

		// Token: 0x040000BF RID: 191
		public bool easter_skull1;

		// Token: 0x040000C0 RID: 192
		public bool easter_skull2;

		// Token: 0x040000C1 RID: 193
		public bool easter_skull3;

		// Token: 0x040000C2 RID: 194
		public Vector2 winCenter;

		// Token: 0x040000C3 RID: 195
		public int screenoffsetX;

		// Token: 0x040000C4 RID: 196
		public int screenoffsetY;

		// Token: 0x040000C5 RID: 197
		public Vector2 winCorner;

		// Token: 0x040000C6 RID: 198
		public float isMaximized = 1f;

		// Token: 0x040000C7 RID: 199
		public bool screenAction;

		// Token: 0x040000C8 RID: 200
		public int myval;

		// Token: 0x040000C9 RID: 201
		public int thisVersion = 4;

		// Token: 0x040000CA RID: 202
		public string loadMoment = "null";

		// Token: 0x040000CB RID: 203
		public bool removeNicely;

		// Token: 0x040000CC RID: 204
		public bool walletHint;

		// Token: 0x040000CD RID: 205
		public bool walletHintShow;

		// Token: 0x040000CE RID: 206
		public bool deathcamHint;

		// Token: 0x040000CF RID: 207
		public bool viewGarbage;

		// Token: 0x040000D0 RID: 208
		public bool kilroyExists;

		// Token: 0x040000D1 RID: 209
		public bool cheat_mousetest;

		// Token: 0x040000D2 RID: 210
		public bool cheat_astro;

		// Token: 0x040000D3 RID: 211
		public bool cheat_avoidhoming;

		// Token: 0x040000D4 RID: 212
		public bool cheatsOn;

		// Token: 0x040000D5 RID: 213
		public bool cheat_skipday;

		// Token: 0x040000D6 RID: 214
		public bool cheat_allweapons;

		// Token: 0x040000D7 RID: 215
		public bool cheat_noenemies;

		// Token: 0x040000D8 RID: 216
		public bool cheat_killboss;

		// Token: 0x040000D9 RID: 217
		public bool networkLag;

		// Token: 0x040000DA RID: 218
		public byte networkQuality;

		// Token: 0x040000DB RID: 219
		public bool debug_showGarbage;

		// Token: 0x040000DC RID: 220
		public bool debug_showSeed;

		// Token: 0x040000DD RID: 221
		public bool debug_showHoming;

		// Token: 0x040000DE RID: 222
		public bool debug_showAccuracy;

		// Token: 0x040000DF RID: 223
		public bool debug_showNetwork;

		// Token: 0x040000E0 RID: 224
		public bool debug_show;

		// Token: 0x040000E1 RID: 225
		public bool debug_cauldron;

		// Token: 0x040000E2 RID: 226
		public bool cheat_SendPackage;

		// Token: 0x040000E3 RID: 227
		public bool cheat_Invincible;

		// Token: 0x040000E4 RID: 228
		public bool cheat_FastFiring;

		// Token: 0x040000E5 RID: 229
		public bool cheat_InfiniteAmmo;

		// Token: 0x040000E6 RID: 230
		public bool cheat_AllExplode;

		// Token: 0x040000E7 RID: 231
		public bool cheat_PickupPack;

		// Token: 0x040000E8 RID: 232
		public bool cheat_unlockAll;

		// Token: 0x040000E9 RID: 233
		public bool cheat_nokick;

		// Token: 0x040000EA RID: 234
		private Vector2 topcorner;

		// Token: 0x040000EB RID: 235
		private Color bluePen = new Color(7, 6, 107, 255);

		// Token: 0x040000EC RID: 236
		private Color bluePen2 = new Color(7, 6, 107, 255);

		// Token: 0x040000ED RID: 237
		private Color redPen = new Color(230, 9, 12, 255);

		// Token: 0x040000EE RID: 238
		private Color grnPen = new Color(20, 220, 10, 255);

		// Token: 0x040000EF RID: 239
		public bool chatboxHELP;

		// Token: 0x040000F0 RID: 240
		public int[] lag = new int[]
		{
			0, 10, 50, 55, 70, 75, 110, 115, 130, 135,
			180, 185, 210, 310, 525, 700
		};

		// Token: 0x040000F1 RID: 241
		public float[] loss = new float[]
		{
			0f, 0.05f, 0.05f, 0.1f, 0.05f, 0.1f, 0.05f, 0.13f, 0.05f, 0.15f,
			0.1f, 0.2f, 0.15f, 0.3f, 0.51f, 0.65f
		};

		// Token: 0x040000F2 RID: 242
		public List<int>[] predayList = new List<int>[]
		{
			new List<int> { 1 },
			new List<int> { 28, 24 },
			new List<int> { 29, 34 },
			new List<int> { 26, 25 },
			new List<int> { 27 },
			new List<int> { 30, 9 },
			new List<int> { 32 }
		};

		// Token: 0x040000F3 RID: 243
		public List<int>[] dayList = new List<int>[]
		{
			new List<int> { 1 },
			new List<int> { 2, 13 },
			new List<int> { 18, 35 },
			new List<int> { 7, 3, 22 },
			new List<int> { 8, 4, 22 },
			new List<int> { 9, 14 },
			new List<int> { 19, 21 },
			new List<int> { 30, 31 },
			new List<int> { 33 }
		};

		// Token: 0x040000F4 RID: 244
		public int weFailed;

		// Token: 0x040000F5 RID: 245
		public bool updatedTrialFile;

		// Token: 0x040000F6 RID: 246
		public int trialattempt;

		// Token: 0x040000F7 RID: 247
		public int trialTimer;

		// Token: 0x040000F8 RID: 248
		public long trialStart;

		// Token: 0x040000F9 RID: 249
		private Rectangle cancelButtonBlue = new Rectangle(365, 546, 50, 26);

		// Token: 0x040000FA RID: 250
		private Rectangle cancelButtonRed = new Rectangle(313, 546, 50, 26);

		// Token: 0x040000FB RID: 251
		private Rectangle saveButtonBlue = new Rectangle(313, 580, 71, 34);

		// Token: 0x040000FC RID: 252
		private Rectangle saveButtonRed = new Rectangle(313, 614, 71, 34);

		// Token: 0x040000FD RID: 253
		private Rectangle resetButtonBlue = new Rectangle(393, 580, 71, 34);

		// Token: 0x040000FE RID: 254
		private Rectangle resetButtonRed = new Rectangle(393, 614, 71, 34);

		// Token: 0x040000FF RID: 255
		private Rectangle cancelButtonX = new Rectangle(700, 700, 50, 26);

		// Token: 0x04000100 RID: 256
		private Rectangle saveButtonX = new Rectangle(700, 730, 71, 34);

		// Token: 0x04000101 RID: 257
		private Rectangle resetButtonX = new Rectangle(700, 770, 71, 34);

		// Token: 0x04000102 RID: 258
		private Rectangle blackX = new Rectangle(700, 820, 20, 20);

		// Token: 0x04000103 RID: 259
		private TrialStorage storageTrial = new TrialStorage();

		// Token: 0x04000104 RID: 260
		private TrialData trials = default(TrialData);

		// Token: 0x04000105 RID: 261
		private string[] away = new string[]
		{
			"AWAY", "OINK", "PIGS", "WHY", "WAIT", "OOHH", "OUCH", "SURE", "FARM", "DONT",
			"PORK", "HAM", "SHIT", "DANG", "KISS", "HOLE", "GRIN", "PINK", "DUNG", "POOP",
			"FART", "BOAR", "SPAM", "PUBG", "WINK", "LICK", "PUBG"
		};

		// Token: 0x04000106 RID: 262
		private int awayIndex;

		// Token: 0x04000107 RID: 263
		public bool keepShowingWarning = true;

		// Token: 0x04000108 RID: 264
		private float versionNumber = 48f;

		// Token: 0x04000109 RID: 265
		public SoundEffect crash;

		// Token: 0x0400010A RID: 266
		public bool barnsLoaded;

		// Token: 0x0400010B RID: 267
		public bool mountainBuildingLoaded;

		// Token: 0x0400010C RID: 268
		public bool loadGuns;

		// Token: 0x0400010D RID: 269
		public bool loadMainModels;

		// Token: 0x0400010E RID: 270
		public bool showVideoSetup;

		// Token: 0x0400010F RID: 271
		public float fileVersion;

		// Token: 0x04000110 RID: 272
		public float spaceVersion = 2.1f;

		// Token: 0x04000111 RID: 273
		public int errorMessageTimer;

		// Token: 0x04000112 RID: 274
		public string errorMessage = "";

		// Token: 0x04000113 RID: 275
		public bool leavingGame;

		// Token: 0x04000114 RID: 276
		public bool isLoading = true;

		// Token: 0x04000115 RID: 277
		public float blackdayFader;

		// Token: 0x04000116 RID: 278
		public float introCamera;

		// Token: 0x04000117 RID: 279
		public bool fenceDemo;

		// Token: 0x04000118 RID: 280
		public bool musicDemo;

		// Token: 0x04000119 RID: 281
		public int dataPause;

		// Token: 0x0400011A RID: 282
		public float myTimer;

		// Token: 0x0400011B RID: 283
		public bool titlesLoaded;

		// Token: 0x0400011C RID: 284
		public int grenades;

		// Token: 0x0400011D RID: 285
		public int milks;

		// Token: 0x0400011E RID: 286
		public int hulks;

		// Token: 0x0400011F RID: 287
		public int pills;

		// Token: 0x04000120 RID: 288
		public int rockets;

		// Token: 0x04000121 RID: 289
		public int[] hats = new int[16];

		// Token: 0x04000122 RID: 290
		public string[] hatnames = new string[]
		{
			"nothing", "top-hat", "cowboy hat", "russian bol", "tracktor cap", "german helmut", "wizard hat", "gas mask", "pumpkin", "elfinhat",
			"redsun hat", "macarthur", "snowcap", "peruvian pom", "irish tweed", "bowler hat"
		};

		// Token: 0x04000123 RID: 291
		public bool man1;

		// Token: 0x04000124 RID: 292
		public bool man2;

		// Token: 0x04000125 RID: 293
		public bool man3;

		// Token: 0x04000126 RID: 294
		public bool man4;

		// Token: 0x04000127 RID: 295
		public bool star1;

		// Token: 0x04000128 RID: 296
		public bool star2;

		// Token: 0x04000129 RID: 297
		public bool star3;

		// Token: 0x0400012A RID: 298
		public int workshopNum;

		// Token: 0x0400012B RID: 299
		public bool bossLoaded;

		// Token: 0x0400012C RID: 300
		public bool cuttyLoaded;

		// Token: 0x0400012D RID: 301
		public bool bigmodelLoaded;

		// Token: 0x0400012E RID: 302
		public Vector3[,] hatTransX;

		// Token: 0x0400012F RID: 303
		public Vector3[,] hatRotX;

		// Token: 0x04000130 RID: 304
		public float[,] hatScaleX;

		// Token: 0x04000131 RID: 305
		public Vector2 hatTrigger = new Vector2(-1f, -1f);

		// Token: 0x04000132 RID: 306
		public int privateMode;

		// Token: 0x04000133 RID: 307
		public int gameState;

		// Token: 0x04000134 RID: 308
		public int gameSpectate;

		// Token: 0x04000135 RID: 309
		public int gameNPC = 1;

		// Token: 0x04000136 RID: 310
		public int gameSeed;

		// Token: 0x04000137 RID: 311
		public bool levelChange;

		// Token: 0x04000138 RID: 312
		public bool sendlevelChange;

		// Token: 0x04000139 RID: 313
		public string newDayTime = "am";

		// Token: 0x0400013A RID: 314
		public float darkness = 1f;

		// Token: 0x0400013B RID: 315
		public float olderdarkness = 1f;

		// Token: 0x0400013C RID: 316
		public float oldFenceDarkness = 1f;

		// Token: 0x0400013D RID: 317
		public float oldMirvDarkness = 1f;

		// Token: 0x0400013E RID: 318
		public float realDarkness = 1f;

		// Token: 0x0400013F RID: 319
		public int realMoon;

		// Token: 0x04000140 RID: 320
		public Vector3 moontype = new Vector3(0f, 0.1f, -0.3f) * 2f;

		// Token: 0x04000141 RID: 321
		public int previousDay = 1;

		// Token: 0x04000142 RID: 322
		public int currentDay = 1;

		// Token: 0x04000143 RID: 323
		public int tempcurrentDay = 1;

		// Token: 0x04000144 RID: 324
		public int curDay = 1;

		// Token: 0x04000145 RID: 325
		public bool FarmerUnlocked;

		// Token: 0x04000146 RID: 326
		public string dayTime = "am";

		// Token: 0x04000147 RID: 327
		public bool mustarDay;

		// Token: 0x04000148 RID: 328
		public int boar1Timer;

		// Token: 0x04000149 RID: 329
		public int boar2Timer;

		// Token: 0x0400014A RID: 330
		public int boar1Clip;

		// Token: 0x0400014B RID: 331
		public int boar2Clip;

		// Token: 0x0400014C RID: 332
		public float boar1Rot;

		// Token: 0x0400014D RID: 333
		public float boar2Rot;

		// Token: 0x0400014E RID: 334
		public float boar1Turn;

		// Token: 0x0400014F RID: 335
		public float boar2Turn;

		// Token: 0x04000150 RID: 336
		public int boar1Borders = 1;

		// Token: 0x04000151 RID: 337
		public int boar2Borders = 1;

		// Token: 0x04000152 RID: 338
		public int boar1Spawn;

		// Token: 0x04000153 RID: 339
		public int boar2Spawn;

		// Token: 0x04000154 RID: 340
		public int revengeDay;

		// Token: 0x04000155 RID: 341
		public int grinderYesterday;

		// Token: 0x04000156 RID: 342
		public int grinderToday;

		// Token: 0x04000157 RID: 343
		public float bloodLevel;

		// Token: 0x04000158 RID: 344
		public int fencecharge;

		// Token: 0x04000159 RID: 345
		public int weaponEndofDay;

		// Token: 0x0400015A RID: 346
		public int previousWeapons;

		// Token: 0x0400015B RID: 347
		public int boar1Variant;

		// Token: 0x0400015C RID: 348
		public int boar2Variant = 1;

		// Token: 0x0400015D RID: 349
		public int boar2Count;

		// Token: 0x0400015E RID: 350
		public int boarCount = 100;

		// Token: 0x0400015F RID: 351
		public int run1Percent = 80;

		// Token: 0x04000160 RID: 352
		public int run2Percent = 80;

		// Token: 0x04000161 RID: 353
		public int headless1Percent = 30;

		// Token: 0x04000162 RID: 354
		public int headless2Percent = 30;

		// Token: 0x04000163 RID: 355
		public int boar1explode = 1;

		// Token: 0x04000164 RID: 356
		public int boar1shottie = 1;

		// Token: 0x04000165 RID: 357
		public int boar2explode = 1;

		// Token: 0x04000166 RID: 358
		public int boar2shottie = 1;

		// Token: 0x04000167 RID: 359
		public int[] weapon_Unlock;

		// Token: 0x04000168 RID: 360
		public bool scarh_Unlock;

		// Token: 0x04000169 RID: 361
		public int[] grinder_Unlock;

		// Token: 0x0400016A RID: 362
		public int[] grinder_Supply;

		// Token: 0x0400016B RID: 363
		public int boar1Health;

		// Token: 0x0400016C RID: 364
		public int boar1Attack;

		// Token: 0x0400016D RID: 365
		public int boar1MinSize;

		// Token: 0x0400016E RID: 366
		public int boar1MaxSize;

		// Token: 0x0400016F RID: 367
		public int boar1GiantOdds;

		// Token: 0x04000170 RID: 368
		public int boar1TinyOdds;

		// Token: 0x04000171 RID: 369
		public int boar1Charge;

		// Token: 0x04000172 RID: 370
		public int boar1TurnRate;

		// Token: 0x04000173 RID: 371
		public int boar2Health;

		// Token: 0x04000174 RID: 372
		public int boar2Attack;

		// Token: 0x04000175 RID: 373
		public int boar2MinSize;

		// Token: 0x04000176 RID: 374
		public int boar2MaxSize;

		// Token: 0x04000177 RID: 375
		public int boar2GiantOdds;

		// Token: 0x04000178 RID: 376
		public int boar2TinyOdds;

		// Token: 0x04000179 RID: 377
		public int boar2Charge;

		// Token: 0x0400017A RID: 378
		public int boar2TurnRate;

		// Token: 0x0400017B RID: 379
		public int boarAttack;

		// Token: 0x0400017C RID: 380
		public int boarTurnRate;

		// Token: 0x0400017D RID: 381
		public float[] handicapDam;

		// Token: 0x0400017E RID: 382
		public float[] handicapBite;

		// Token: 0x0400017F RID: 383
		public float[] handicapSpeed;

		// Token: 0x04000180 RID: 384
		public float[] handicapTurn;

		// Token: 0x04000181 RID: 385
		public float[] handicapDam2;

		// Token: 0x04000182 RID: 386
		public float[] handicapBite2;

		// Token: 0x04000183 RID: 387
		public float[] handicapSpeed2;

		// Token: 0x04000184 RID: 388
		public float[] handicapTurn2;

		// Token: 0x04000185 RID: 389
		public float[] handicapDam4;

		// Token: 0x04000186 RID: 390
		public float[] handicapBite4;

		// Token: 0x04000187 RID: 391
		public float[] handicapSpeed4;

		// Token: 0x04000188 RID: 392
		public float[] handicapTurn4;

		// Token: 0x04000189 RID: 393
		public int[] boarPercent;

		// Token: 0x0400018A RID: 394
		public int[] boarDistance;

		// Token: 0x0400018B RID: 395
		public int[] boarHomingLimit;

		// Token: 0x0400018C RID: 396
		public SoundEffectInstance corncobMusic;

		// Token: 0x0400018D RID: 397
		public SoundEffectInstance crickets;

		// Token: 0x0400018E RID: 398
		public bool gameMusicPlaying;

		// Token: 0x0400018F RID: 399
		public SoundEffect mainTheme;

		// Token: 0x04000190 RID: 400
		public SoundEffect xmas1;

		// Token: 0x04000191 RID: 401
		public SoundEffect xmas2;

		// Token: 0x04000192 RID: 402
		public SoundEffect[] pigExplode;

		// Token: 0x04000193 RID: 403
		public SoundEffect[] steps;

		// Token: 0x04000194 RID: 404
		public SoundEffect[] crumble;

		// Token: 0x04000195 RID: 405
		public SoundEffect[] pump;

		// Token: 0x04000196 RID: 406
		public SoundEffect[] boarbite;

		// Token: 0x04000197 RID: 407
		public SoundEffect[] ricochete;

		// Token: 0x04000198 RID: 408
		public SoundEffect[] sproing;

		// Token: 0x04000199 RID: 409
		public SoundEffect[] grenadePop1;

		// Token: 0x0400019A RID: 410
		public SoundEffect[] pigSqueal;

		// Token: 0x0400019B RID: 411
		public SoundEffect[] pigDie;

		// Token: 0x0400019C RID: 412
		public SoundEffect hammer1;

		// Token: 0x0400019D RID: 413
		public SoundEffect doorRattle;

		// Token: 0x0400019E RID: 414
		public SoundEffect buttonPress;

		// Token: 0x0400019F RID: 415
		public SoundEffect buttonDeny;

		// Token: 0x040001A0 RID: 416
		public SoundEffect achieve1;

		// Token: 0x040001A1 RID: 417
		public SoundEffect buttonPackage;

		// Token: 0x040001A2 RID: 418
		public SoundEffect mirvDrop;

		// Token: 0x040001A3 RID: 419
		public SoundEffect doorUnlock;

		// Token: 0x040001A4 RID: 420
		public SoundEffect pickup1;

		// Token: 0x040001A5 RID: 421
		public SoundEffect pickup2;

		// Token: 0x040001A6 RID: 422
		public SoundEffect pickupGrenade;

		// Token: 0x040001A7 RID: 423
		public SoundEffect drinkMilk;

		// Token: 0x040001A8 RID: 424
		public SoundEffect piledriver;

		// Token: 0x040001A9 RID: 425
		public SoundEffect fireworks;

		// Token: 0x040001AA RID: 426
		public SoundEffect fireworks2;

		// Token: 0x040001AB RID: 427
		public SoundEffect barndoor;

		// Token: 0x040001AC RID: 428
		public SoundEffect fence;

		// Token: 0x040001AD RID: 429
		public SoundEffect crunch;

		// Token: 0x040001AE RID: 430
		public SoundEffect shotgunPump;

		// Token: 0x040001AF RID: 431
		public SoundEffect gusher;

		// Token: 0x040001B0 RID: 432
		public SoundEffect hulkRoar;

		// Token: 0x040001B1 RID: 433
		public SoundEffect hulkRoar2;

		// Token: 0x040001B2 RID: 434
		public SoundEffect roar;

		// Token: 0x040001B3 RID: 435
		public SoundEffect gamestart;

		// Token: 0x040001B4 RID: 436
		public SoundEffect abort;

		// Token: 0x040001B5 RID: 437
		public SoundEffect harp2;

		// Token: 0x040001B6 RID: 438
		public SoundEffect humm;

		// Token: 0x040001B7 RID: 439
		public SoundEffect chunk1;

		// Token: 0x040001B8 RID: 440
		public SoundEffect chunk2;

		// Token: 0x040001B9 RID: 441
		public SoundEffect meaty;

		// Token: 0x040001BA RID: 442
		public SoundEffect manyell;

		// Token: 0x040001BB RID: 443
		public SoundEffect hurt;

		// Token: 0x040001BC RID: 444
		public SoundEffect grabby;

		// Token: 0x040001BD RID: 445
		public SoundEffect chomp2;

		// Token: 0x040001BE RID: 446
		public SoundEffect bonepop;

		// Token: 0x040001BF RID: 447
		public SoundEffect report;

		// Token: 0x040001C0 RID: 448
		public SoundEffect dieyell;

		// Token: 0x040001C1 RID: 449
		public SoundEffect cuttygouge;

		// Token: 0x040001C2 RID: 450
		public SoundEffect cuttyWave;

		// Token: 0x040001C3 RID: 451
		public SoundEffect buzz;

		// Token: 0x040001C4 RID: 452
		public SoundEffect newtip;

		// Token: 0x040001C5 RID: 453
		public SoundEffect pillRattler;

		// Token: 0x040001C6 RID: 454
		public SoundEffect pillswallow;

		// Token: 0x040001C7 RID: 455
		public SoundEffect pillselect;

		// Token: 0x040001C8 RID: 456
		public SoundEffect jot1;

		// Token: 0x040001C9 RID: 457
		public SoundEffect jot2;

		// Token: 0x040001CA RID: 458
		public SoundEffect cashout;

		// Token: 0x040001CB RID: 459
		public SoundEffect scribble;

		// Token: 0x040001CC RID: 460
		public SoundEffect menuclick;

		// Token: 0x040001CD RID: 461
		public SoundEffect switchweapon;

		// Token: 0x040001CE RID: 462
		public SoundEffect melee;

		// Token: 0x040001CF RID: 463
		public SoundEffect lightClick;

		// Token: 0x040001D0 RID: 464
		public SoundEffect grinder;

		// Token: 0x040001D1 RID: 465
		public SoundEffects grinderMotor;

		// Token: 0x040001D2 RID: 466
		public SoundEffects chain;

		// Token: 0x040001D3 RID: 467
		public SoundEffect ring;

		// Token: 0x040001D4 RID: 468
		public SoundEffect chomp;

		// Token: 0x040001D5 RID: 469
		public SoundEffect ruffles;

		// Token: 0x040001D6 RID: 470
		public SoundEffect falldown;

		// Token: 0x040001D7 RID: 471
		public SoundEffect falldown2;

		// Token: 0x040001D8 RID: 472
		public SoundEffect toneer;

		// Token: 0x040001D9 RID: 473
		public SoundEffect dying;

		// Token: 0x040001DA RID: 474
		public SoundEffect drip;

		// Token: 0x040001DB RID: 475
		public SoundEffect ding;

		// Token: 0x040001DC RID: 476
		public SoundEffect achievepop;

		// Token: 0x040001DD RID: 477
		public SoundEffect pileDriverSure;

		// Token: 0x040001DE RID: 478
		public SoundEffect scree;

		// Token: 0x040001DF RID: 479
		public SoundEffect xmas;

		// Token: 0x040001E0 RID: 480
		public SoundEffect startMusic;

		// Token: 0x040001E1 RID: 481
		public SoundEffect accept;

		// Token: 0x040001E2 RID: 482
		public SoundEffect cancel;

		// Token: 0x040001E3 RID: 483
		public SoundEffect equipx;

		// Token: 0x040001E4 RID: 484
		public SoundEffect wavecomplete;

		// Token: 0x040001E5 RID: 485
		public SoundEffect allpigs;

		// Token: 0x040001E6 RID: 486
		public SoundEffect fanfare;

		// Token: 0x040001E7 RID: 487
		public SoundEffect[] metalHit;

		// Token: 0x040001E8 RID: 488
		public SoundEffect[] cocking;

		// Token: 0x040001E9 RID: 489
		public SoundEffect[] gunFire;

		// Token: 0x040001EA RID: 490
		public SoundEffect[] gunMuffle;

		// Token: 0x040001EB RID: 491
		public SoundEffect[] gunDry;

		// Token: 0x040001EC RID: 492
		public SoundEffect[] shellSound;

		// Token: 0x040001ED RID: 493
		public SoundEffect tick;

		// Token: 0x040001EE RID: 494
		public SoundEffect switch2;

		// Token: 0x040001EF RID: 495
		public SoundEffect grow;

		// Token: 0x040001F0 RID: 496
		public Vector3 hatTrans;

		// Token: 0x040001F1 RID: 497
		public float hatscale;

		// Token: 0x040001F2 RID: 498
		public int hatindex;

		// Token: 0x040001F3 RID: 499
		public Effect hatEffect;

		// Token: 0x040001F4 RID: 500
		public Matrix[,] hatMatrix;

		// Token: 0x040001F5 RID: 501
		public Matrix[,] temphatMatrix;

		// Token: 0x040001F6 RID: 502
		public Model gunPack;

		// Token: 0x040001F7 RID: 503
		public Model hatPack;

		// Token: 0x040001F8 RID: 504
		public Model[] shellPack;

		// Token: 0x040001F9 RID: 505
		public Texture2D[] gunTextures;

		// Token: 0x040001FA RID: 506
		public Texture2D[] hatTextures;

		// Token: 0x040001FB RID: 507
		public Texture2D goldkeyTexture;

		// Token: 0x040001FC RID: 508
		public Texture2D blimpTexture;

		// Token: 0x040001FD RID: 509
		public Texture2D moonTexture;

		// Token: 0x040001FE RID: 510
		public Model pigAll;

		// Token: 0x040001FF RID: 511
		public Model pigModel;

		// Token: 0x04000200 RID: 512
		public Model pigModelPrincess;

		// Token: 0x04000201 RID: 513
		public Model cowModel;

		// Token: 0x04000202 RID: 514
		public Model bossAll;

		// Token: 0x04000203 RID: 515
		public int bossIndex;

		// Token: 0x04000204 RID: 516
		public Model water;

		// Token: 0x04000205 RID: 517
		public Model boar1basicModel;

		// Token: 0x04000206 RID: 518
		public Model boar1headlessModel;

		// Token: 0x04000207 RID: 519
		public Model boarbruteModel;

		// Token: 0x04000208 RID: 520
		public Model bruteheadlessModel;

		// Token: 0x04000209 RID: 521
		public Model boarSkel;

		// Token: 0x0400020A RID: 522
		public Model boarSkelHeadless;

		// Token: 0x0400020B RID: 523
		public Model charModel;

		// Token: 0x0400020C RID: 524
		public Model boarArmorModel;

		// Token: 0x0400020D RID: 525
		public Model boarArmorModelHeadless;

		// Token: 0x0400020E RID: 526
		public Model boarmasterModel;

		// Token: 0x0400020F RID: 527
		public Model piggyModel;

		// Token: 0x04000210 RID: 528
		public Model boarmasterModelHeadless;

		// Token: 0x04000211 RID: 529
		public Model zombieModel;

		// Token: 0x04000212 RID: 530
		public Model cube;

		// Token: 0x04000213 RID: 531
		public Model decal;

		// Token: 0x04000214 RID: 532
		public Model decalb;

		// Token: 0x04000215 RID: 533
		public Model decal2;

		// Token: 0x04000216 RID: 534
		public Model biteDecal;

		// Token: 0x04000217 RID: 535
		public Model explosionDecal;

		// Token: 0x04000218 RID: 536
		public Model fireballDecal;

		// Token: 0x04000219 RID: 537
		public Model fireballDecal2;

		// Token: 0x0400021A RID: 538
		public Model buttonModel;

		// Token: 0x0400021B RID: 539
		public Model grass;

		// Token: 0x0400021C RID: 540
		public Model trees;

		// Token: 0x0400021D RID: 541
		public Model farmTriangles;

		// Token: 0x0400021E RID: 542
		public Model farmTriangles2;

		// Token: 0x0400021F RID: 543
		public Model farmTriangles3;

		// Token: 0x04000220 RID: 544
		public Model barnTriangles;

		// Token: 0x04000221 RID: 545
		public Model doorTriangles;

		// Token: 0x04000222 RID: 546
		public Model farmBuilding;

		// Token: 0x04000223 RID: 547
		public Model farmBuilding2;

		// Token: 0x04000224 RID: 548
		public Model farmBuilding3;

		// Token: 0x04000225 RID: 549
		public Model farmBuildingspace;

		// Token: 0x04000226 RID: 550
		public Model tunnel1;

		// Token: 0x04000227 RID: 551
		public Model tunnelTriangle;

		// Token: 0x04000228 RID: 552
		public Model tunnelheights;

		// Token: 0x04000229 RID: 553
		public Model barnBuilding;

		// Token: 0x0400022A RID: 554
		public Model mountain;

		// Token: 0x0400022B RID: 555
		public Texture2D waterTexture;

		// Token: 0x0400022C RID: 556
		public Texture2D johnnyWallet;

		// Token: 0x0400022D RID: 557
		public Texture2D landoWallet;

		// Token: 0x0400022E RID: 558
		public Texture2D farmerWallet;

		// Token: 0x0400022F RID: 559
		public Texture2D skellyWallet;

		// Token: 0x04000230 RID: 560
		public Texture2D johnWallet;

		// Token: 0x04000231 RID: 561
		public Texture2D daisyWallet;

		// Token: 0x04000232 RID: 562
		public Texture2D vikingWallet;

		// Token: 0x04000233 RID: 563
		public Texture2D deadWallet;

		// Token: 0x04000234 RID: 564
		public Texture2D robotWallet;

		// Token: 0x04000235 RID: 565
		public Texture2D golemWallet;

		// Token: 0x04000236 RID: 566
		public Texture2D astroWallet;

		// Token: 0x04000237 RID: 567
		public Texture2D paper1;

		// Token: 0x04000238 RID: 568
		public Texture2D controller;

		// Token: 0x04000239 RID: 569
		public Texture2D controller2;

		// Token: 0x0400023A RID: 570
		public Texture2D blankpaper;

		// Token: 0x0400023B RID: 571
		public Texture2D blankpaper2;

		// Token: 0x0400023C RID: 572
		public Texture2D instructions;

		// Token: 0x0400023D RID: 573
		public Texture2D page1;

		// Token: 0x0400023E RID: 574
		public Texture2D page2;

		// Token: 0x0400023F RID: 575
		public Texture2D page3;

		// Token: 0x04000240 RID: 576
		public Texture2D page4;

		// Token: 0x04000241 RID: 577
		public Texture2D page5;

		// Token: 0x04000242 RID: 578
		public Texture2D page6;

		// Token: 0x04000243 RID: 579
		public Texture2D page7;

		// Token: 0x04000244 RID: 580
		public Texture2D reflectionMap;

		// Token: 0x04000245 RID: 581
		public Texture2D spotTexture;

		// Token: 0x04000246 RID: 582
		public Texture2D crosshair;

		// Token: 0x04000247 RID: 583
		public Texture2D burster;

		// Token: 0x04000248 RID: 584
		public Texture2D electrify;

		// Token: 0x04000249 RID: 585
		public Texture2D muzzles;

		// Token: 0x0400024A RID: 586
		public Texture2D blasts;

		// Token: 0x0400024B RID: 587
		public Model heightmodel;

		// Token: 0x0400024C RID: 588
		public Model heightmodel2;

		// Token: 0x0400024D RID: 589
		public Model farmspacecollide;

		// Token: 0x0400024E RID: 590
		public Texture2D grassTexture;

		// Token: 0x0400024F RID: 591
		public Texture2D mountTexture;

		// Token: 0x04000250 RID: 592
		public Texture2D grassTextureNight;

		// Token: 0x04000251 RID: 593
		public Texture2D mountTextureNight;

		// Token: 0x04000252 RID: 594
		public Texture2D buildingRGB;

		// Token: 0x04000253 RID: 595
		public Texture2D buildingRGBNight;

		// Token: 0x04000254 RID: 596
		public Texture2D buildingShadow;

		// Token: 0x04000255 RID: 597
		public Texture2D buildingShadowNight;

		// Token: 0x04000256 RID: 598
		public Texture2D buildingshadspace;

		// Token: 0x04000257 RID: 599
		public Texture2D barnRGB;

		// Token: 0x04000258 RID: 600
		public Texture2D barnShadow;

		// Token: 0x04000259 RID: 601
		public Model gunMuzzle;

		// Token: 0x0400025A RID: 602
		public Model gunBlast;

		// Token: 0x0400025B RID: 603
		public Model blueLaser;

		// Token: 0x0400025C RID: 604
		public Texture2D overlay;

		// Token: 0x0400025D RID: 605
		public Texture2D overlayStats;

		// Token: 0x0400025E RID: 606
		public Texture2D overlayStats2;

		// Token: 0x0400025F RID: 607
		public Texture2D bc1;

		// Token: 0x04000260 RID: 608
		public Texture2D bc2;

		// Token: 0x04000261 RID: 609
		public Texture2D titleTrailer;

		// Token: 0x04000262 RID: 610
		public Texture2D titleTrailerDemo;

		// Token: 0x04000263 RID: 611
		public Texture2D titleFrame;

		// Token: 0x04000264 RID: 612
		public Texture2D titleBG;

		// Token: 0x04000265 RID: 613
		public Texture2D titleBG2;

		// Token: 0x04000266 RID: 614
		public Texture2D titlePig0;

		// Token: 0x04000267 RID: 615
		public Texture2D titlePig1;

		// Token: 0x04000268 RID: 616
		public Texture2D titlePig2;

		// Token: 0x04000269 RID: 617
		public Texture2D titlePig3;

		// Token: 0x0400026A RID: 618
		public Texture2D titleFaces;

		// Token: 0x0400026B RID: 619
		public Texture2D titleCharsJoin;

		// Token: 0x0400026C RID: 620
		public Texture2D titleCharsMain;

		// Token: 0x0400026D RID: 621
		public Texture2D titleCharsSingle;

		// Token: 0x0400026E RID: 622
		public Texture2D titleCharsVideo;

		// Token: 0x0400026F RID: 623
		public Texture2D titleCharsDemo;

		// Token: 0x04000270 RID: 624
		public Texture2D titleHeaderBloodBacon;

		// Token: 0x04000271 RID: 625
		public Texture2D titleHeaderBloodBaconB;

		// Token: 0x04000272 RID: 626
		public Texture2D titleHeaderBloodBaconC;

		// Token: 0x04000273 RID: 627
		public Texture2D titleHeaderLobby;

		// Token: 0x04000274 RID: 628
		public Texture2D titleHeaderVideos;

		// Token: 0x04000275 RID: 629
		public Texture2D titleHeaderControls;

		// Token: 0x04000276 RID: 630
		public Texture2D titleHeaderDiary;

		// Token: 0x04000277 RID: 631
		public Texture2D titleHeaderSecrets;

		// Token: 0x04000278 RID: 632
		public Texture2D titleHeaderMore;

		// Token: 0x04000279 RID: 633
		public Texture2D titleheaderSettings;

		// Token: 0x0400027A RID: 634
		public Texture2D titleHeader2player;

		// Token: 0x0400027B RID: 635
		public Texture2D titleHeaderMultiplayer;

		// Token: 0x0400027C RID: 636
		public Texture2D titleHeader4player;

		// Token: 0x0400027D RID: 637
		public Texture2D titleHeader6player;

		// Token: 0x0400027E RID: 638
		public Texture2D titleHeaderLobbies;

		// Token: 0x0400027F RID: 639
		public Texture2D titleHeaderLobbies2;

		// Token: 0x04000280 RID: 640
		public Texture2D titleHeaderAudio;

		// Token: 0x04000281 RID: 641
		public Texture2D titleCharsGraphics;

		// Token: 0x04000282 RID: 642
		public Texture2D titleHeaderJoin;

		// Token: 0x04000283 RID: 643
		public Texture2D titleCreditWall;

		// Token: 0x04000284 RID: 644
		public Texture2D titleCreditBlank;

		// Token: 0x04000285 RID: 645
		public Texture2D titleCredit1;

		// Token: 0x04000286 RID: 646
		public Texture2D titleCredit2;

		// Token: 0x04000287 RID: 647
		public Texture2D titleHeaderWorkshop;

		// Token: 0x04000288 RID: 648
		public Texture2D titleHeaderWorkshop2;

		// Token: 0x04000289 RID: 649
		public Texture2D titleHeaderWorkshopPublish;

		// Token: 0x0400028A RID: 650
		public Texture2D titleHeaderWorkshopPublish2;

		// Token: 0x0400028B RID: 651
		public Texture2D button;

		// Token: 0x0400028C RID: 652
		public Texture2D titleHeaderTunnelDays;

		// Token: 0x0400028D RID: 653
		public Model farmerModel;

		// Token: 0x0400028E RID: 654
		public Model twinModel;

		// Token: 0x0400028F RID: 655
		public Model farmerModelskull;

		// Token: 0x04000290 RID: 656
		public Texture2D farmerTexture;

		// Token: 0x04000291 RID: 657
		public Texture2D twinTexture;

		// Token: 0x04000292 RID: 658
		public Texture2D pigTexture;

		// Token: 0x04000293 RID: 659
		public Model player1Model;

		// Token: 0x04000294 RID: 660
		public Model player1Modelnoarms;

		// Token: 0x04000295 RID: 661
		public Model player2Model;

		// Token: 0x04000296 RID: 662
		public Model player2Modelnoarms;

		// Token: 0x04000297 RID: 663
		public Model whiteNPCModel;

		// Token: 0x04000298 RID: 664
		public Model whiteNPCnoarms;

		// Token: 0x04000299 RID: 665
		public Model blackNPCModel;

		// Token: 0x0400029A RID: 666
		public Model farmerNPCModel;

		// Token: 0x0400029B RID: 667
		public Model skelNPCmodel;

		// Token: 0x0400029C RID: 668
		public Model daisyNPCmodel;

		// Token: 0x0400029D RID: 669
		public Model vikingNPCmodel;

		// Token: 0x0400029E RID: 670
		public Model strawNPCModel;

		// Token: 0x0400029F RID: 671
		public Model robotNPCModel;

		// Token: 0x040002A0 RID: 672
		public Model golemNPCModel;

		// Token: 0x040002A1 RID: 673
		public Model astroNPCModel;

		// Token: 0x040002A2 RID: 674
		public Model blackNPCnoarms;

		// Token: 0x040002A3 RID: 675
		public Model farmerNPCnoarms;

		// Token: 0x040002A4 RID: 676
		public Model skelNPCnoarms;

		// Token: 0x040002A5 RID: 677
		public Model daisyNPCnoarms;

		// Token: 0x040002A6 RID: 678
		public Model vikingNPCnoarms;

		// Token: 0x040002A7 RID: 679
		public Model strawNPCnoarms;

		// Token: 0x040002A8 RID: 680
		public Model robotNPCnoarms;

		// Token: 0x040002A9 RID: 681
		public Model golemNPCnoarms;

		// Token: 0x040002AA RID: 682
		public Model astroNPCnoarms;

		// Token: 0x040002AB RID: 683
		public Model ghostNPCmodel;

		// Token: 0x040002AC RID: 684
		public Texture2D blackNPCTexture;

		// Token: 0x040002AD RID: 685
		public Texture2D farmerNPCTexture;

		// Token: 0x040002AE RID: 686
		public Texture2D skelNPCTexture;

		// Token: 0x040002AF RID: 687
		public Texture2D daisyNPCTexture;

		// Token: 0x040002B0 RID: 688
		public Texture2D vikingNPCTexture;

		// Token: 0x040002B1 RID: 689
		public Texture2D strawNPCTexture;

		// Token: 0x040002B2 RID: 690
		public Texture2D robotNPCTexture;

		// Token: 0x040002B3 RID: 691
		public Texture2D golemNPCTexture;

		// Token: 0x040002B4 RID: 692
		public Texture2D astroNPCTexture;

		// Token: 0x040002B5 RID: 693
		public Texture2D ghostNPCTexture;

		// Token: 0x040002B6 RID: 694
		public Texture2D blackNPCTextureOrig;

		// Token: 0x040002B7 RID: 695
		public Texture2D farmerNPCTextureOrig;

		// Token: 0x040002B8 RID: 696
		public Texture2D skelNPCTextureOrig;

		// Token: 0x040002B9 RID: 697
		public Texture2D daisyNPCTextureOrig;

		// Token: 0x040002BA RID: 698
		public Texture2D vikingNPCTextureOrig;

		// Token: 0x040002BB RID: 699
		public Texture2D strawNPCTextureOrig;

		// Token: 0x040002BC RID: 700
		public Texture2D robotNPCTextureOrig;

		// Token: 0x040002BD RID: 701
		public Texture2D golemNPCTextureOrig;

		// Token: 0x040002BE RID: 702
		public Texture2D astroNPCTextureOrig;

		// Token: 0x040002BF RID: 703
		public Texture2D[] badges;

		// Token: 0x040002C0 RID: 704
		public Texture2D whiteNPCTexture;

		// Token: 0x040002C1 RID: 705
		public Texture2D whiteNPCTextureOrig;

		// Token: 0x040002C2 RID: 706
		public Texture2D whiteNPCTextureGreen2;

		// Token: 0x040002C3 RID: 707
		public Texture2D blackNPCTextureGreen;

		// Token: 0x040002C4 RID: 708
		public Texture2D farmerGreen;

		// Token: 0x040002C5 RID: 709
		public Texture2D daisyGreen;

		// Token: 0x040002C6 RID: 710
		public Texture2D vikingGreen;

		// Token: 0x040002C7 RID: 711
		public Texture2D skelGreen;

		// Token: 0x040002C8 RID: 712
		public Texture2D strawGreen;

		// Token: 0x040002C9 RID: 713
		public Texture2D robotGreen;

		// Token: 0x040002CA RID: 714
		public Texture2D golemGreen;

		// Token: 0x040002CB RID: 715
		public Texture2D astroGreen;

		// Token: 0x040002CC RID: 716
		public Texture2D whiteNPCdead;

		// Token: 0x040002CD RID: 717
		public Texture2D blackNPCdead;

		// Token: 0x040002CE RID: 718
		public Texture2D farmerDead;

		// Token: 0x040002CF RID: 719
		public Texture2D skelDead;

		// Token: 0x040002D0 RID: 720
		public Texture2D daisyDead;

		// Token: 0x040002D1 RID: 721
		public Texture2D vikingDead;

		// Token: 0x040002D2 RID: 722
		public Texture2D strawDead;

		// Token: 0x040002D3 RID: 723
		public Texture2D robotDead;

		// Token: 0x040002D4 RID: 724
		public Texture2D golemDead;

		// Token: 0x040002D5 RID: 725
		public Texture2D astroDead;

		// Token: 0x040002D6 RID: 726
		public Texture2D wound;

		// Token: 0x040002D7 RID: 727
		public Rectangle[] woundRect;

		// Token: 0x040002D8 RID: 728
		public Texture2D star;

		// Token: 0x040002D9 RID: 729
		public Texture2D starB;

		// Token: 0x040002DA RID: 730
		public Texture2D staroff;

		// Token: 0x040002DB RID: 731
		public Texture2D certified;

		// Token: 0x040002DC RID: 732
		public int paintColor;

		// Token: 0x040002DD RID: 733
		public int paintColorCanvas;

		// Token: 0x040002DE RID: 734
		public int paintRemColor;

		// Token: 0x040002DF RID: 735
		public int paintRemColorCanvas;

		// Token: 0x040002E0 RID: 736
		public bool walletLoaded;

		// Token: 0x040002E1 RID: 737
		public bool moremodelsLoaded;

		// Token: 0x040002E2 RID: 738
		public bool audioLoaded;

		// Token: 0x040002E3 RID: 739
		public SpriteFont landerfont;

		// Token: 0x040002E4 RID: 740
		public SpriteFont font;

		// Token: 0x040002E5 RID: 741
		public SpriteFont font3;

		// Token: 0x040002E6 RID: 742
		public SpriteFont grungeFont;

		// Token: 0x040002E7 RID: 743
		public SpriteFont font2;

		// Token: 0x040002E8 RID: 744
		public SpriteFont fontsmall;

		// Token: 0x040002E9 RID: 745
		public SpriteFont lilyFont;

		// Token: 0x040002EA RID: 746
		public SpriteFont arialfont;

		// Token: 0x040002EB RID: 747
		public SpriteFont scribblefont;

		// Token: 0x040002EC RID: 748
		public SpriteFont scribblefont2;

		// Token: 0x040002ED RID: 749
		public SpriteFont terminal;

		// Token: 0x040002EE RID: 750
		public SpriteFont squarefont;

		// Token: 0x040002EF RID: 751
		public SpriteBatch SpriteBatch;

		// Token: 0x040002F0 RID: 752
		private bool isInitialized;

		// Token: 0x040002F1 RID: 753
		public bool tryPurchase;

		// Token: 0x040002F2 RID: 754
		public bool rtLock;

		// Token: 0x040002F3 RID: 755
		public int setupnum;

		// Token: 0x040002F4 RID: 756
		public int aa;

		// Token: 0x040002F5 RID: 757
		public List<GameScreen> screens;

		// Token: 0x040002F6 RID: 758
		private List<GameScreen> screensToUpdate;

		// Token: 0x040002F7 RID: 759
		private InputState input;

		// Token: 0x040002F8 RID: 760
		private spaceStorage storageSpacePrefs;

		// Token: 0x040002F9 RID: 761
		private SpaceData spaceprefs;

		// Token: 0x040002FA RID: 762
		private PrefStorage storagePrefs;

		// Token: 0x040002FB RID: 763
		private PrefData prefs;

		// Token: 0x040002FC RID: 764
		private GameStorage storageGame;

		// Token: 0x040002FD RID: 765
		public GameData gdata;

		// Token: 0x040002FE RID: 766
		public string storeState;

		// Token: 0x040002FF RID: 767
		public bool protectScreen;

		// Token: 0x04000300 RID: 768
		public int[] days;

		// Token: 0x04000301 RID: 769
		public float camradian1;

		// Token: 0x04000302 RID: 770
		public float camheight1;

		// Token: 0x04000303 RID: 771
		public Vector3 campos3rd1;

		// Token: 0x04000304 RID: 772
		public Vector3 camlookpos3rd1;

		// Token: 0x04000305 RID: 773
		public float camradian2;

		// Token: 0x04000306 RID: 774
		public float camheight2;

		// Token: 0x04000307 RID: 775
		public Vector3 campos3rd2;

		// Token: 0x04000308 RID: 776
		public Vector3 camlookpos3rd2;

		// Token: 0x04000309 RID: 777
		public Texture2D whiteTexture;

		// Token: 0x0400030A RID: 778
		public Texture2D blackTexture;

		// Token: 0x0400030B RID: 779
		public Texture2D menuBlob;

		// Token: 0x0400030C RID: 780
		public Texture2D menuBlob2;

		// Token: 0x0400030D RID: 781
		public Texture2D redTexture;

		// Token: 0x0400030E RID: 782
		public int brightness;

		// Token: 0x0400030F RID: 783
		public int brightBU;

		// Token: 0x04000310 RID: 784
		public int contrast;

		// Token: 0x04000311 RID: 785
		public int contrastBU;

		// Token: 0x04000312 RID: 786
		public int redContrast;

		// Token: 0x04000313 RID: 787
		public int shitContrast;

		// Token: 0x04000314 RID: 788
		public BlendState brightnessUP;

		// Token: 0x04000315 RID: 789
		public BlendState brightnessDOWN;

		// Token: 0x04000316 RID: 790
		public BlendState contrastBlend;

		// Token: 0x04000317 RID: 791
		public ushort dayLevel;

		// Token: 0x04000318 RID: 792
		public float mv;

		// Token: 0x04000319 RID: 793
		public float ev;

		// Token: 0x0400031A RID: 794
		public float vv;

		// Token: 0x0400031B RID: 795
		public int df;

		// Token: 0x0400031C RID: 796
		public int df_orig;

		// Token: 0x0400031D RID: 797
		public float pad_invertY;

		// Token: 0x0400031E RID: 798
		public float pad_sensitivity;

		// Token: 0x0400031F RID: 799
		public bool pad_vibro;

		// Token: 0x04000320 RID: 800
		public bool pad_reload;

		// Token: 0x04000321 RID: 801
		public bool pad_togglesprint;

		// Token: 0x04000322 RID: 802
		public int aliasing;

		// Token: 0x04000323 RID: 803
		public int resolution;

		// Token: 0x04000324 RID: 804
		public int fullscreen;

		// Token: 0x04000325 RID: 805
		public string playername;

		// Token: 0x04000326 RID: 806
		public int gorelevel;

		// Token: 0x04000327 RID: 807
		public bool doubleAmmo;

		// Token: 0x04000328 RID: 808
		public bool fastnades;

		// Token: 0x04000329 RID: 809
		public bool fullmode;

		// Token: 0x0400032A RID: 810
		public bool border;

		// Token: 0x0400032B RID: 811
		public List<string> resnames;

		// Token: 0x0400032C RID: 812
		public Vector2 hud_enemy;

		// Token: 0x0400032D RID: 813
		public Vector2 hud_clock;

		// Token: 0x0400032E RID: 814
		public Vector2 hud_day;

		// Token: 0x0400032F RID: 815
		public Vector2 hud_player1;

		// Token: 0x04000330 RID: 816
		public Vector2 hud_player2;

		// Token: 0x04000331 RID: 817
		public Vector2 hud_weapons;

		// Token: 0x04000332 RID: 818
		public Vector2 hud_dpad;

		// Token: 0x04000333 RID: 819
		public Color color_enemy;

		// Token: 0x04000334 RID: 820
		public Color color_day;

		// Token: 0x04000335 RID: 821
		public Color color_clock;

		// Token: 0x04000336 RID: 822
		public Color color_player1;

		// Token: 0x04000337 RID: 823
		public Color color_player2;

		// Token: 0x04000338 RID: 824
		public Color color_weapons;

		// Token: 0x04000339 RID: 825
		public Color color_dpad;

		// Token: 0x0400033A RID: 826
		private SoundEffect opening;

		// Token: 0x0400033B RID: 827
		public ContentManager content;

		// Token: 0x0400033C RID: 828
		public ContentManager content2;

		// Token: 0x0400033D RID: 829
		public ContentManager conModel;

		// Token: 0x0400033E RID: 830
		public string gamername;

		// Token: 0x0400033F RID: 831
		public Viewport myviewport;

		// Token: 0x04000340 RID: 832
		public GraphicsDeviceManager graphics;

		// Token: 0x04000341 RID: 833
		public int cardx;

		// Token: 0x04000342 RID: 834
		public int cardy;

		// Token: 0x04000343 RID: 835
		public int devicex;

		// Token: 0x04000344 RID: 836
		public int devicey;

		// Token: 0x04000345 RID: 837
		public int viewportx;

		// Token: 0x04000346 RID: 838
		public int viewporty;

		// Token: 0x04000347 RID: 839
		public astro astronaut;

		// Token: 0x04000348 RID: 840
		public int frontDoor;

		// Token: 0x04000349 RID: 841
		public bool debug;

		// Token: 0x0400034A RID: 842
		public int aliasSet;

		// Token: 0x0400034B RID: 843
		public PlayerIndex gamerindex;

		// Token: 0x0400034C RID: 844
		public int myindex;

		// Token: 0x0400034D RID: 845
		public float typewriterblank;

		// Token: 0x0400034E RID: 846
		public int textflag;

		// Token: 0x0400034F RID: 847
		public float typewriterwait;

		// Token: 0x04000350 RID: 848
		public float typeposition;

		// Token: 0x04000351 RID: 849
		public float typevertical;

		// Token: 0x04000352 RID: 850
		public int typewriterdelay;

		// Token: 0x04000353 RID: 851
		public float siders;

		// Token: 0x04000354 RID: 852
		public float bottomer;

		// Token: 0x04000355 RID: 853
		public float topper;

		// Token: 0x04000356 RID: 854
		public float voiceVolume;

		// Token: 0x04000357 RID: 855
		public int bgindex;

		// Token: 0x04000358 RID: 856
		public int planet;

		// Token: 0x04000359 RID: 857
		public float fadeSetting;

		// Token: 0x0400035A RID: 858
		public float filterSetting;

		// Token: 0x0400035B RID: 859
		public float intenseSetting;

		// Token: 0x0400035C RID: 860
		public float gamefadeSetting;

		// Token: 0x0400035D RID: 861
		public float gamefilterSetting;

		// Token: 0x0400035E RID: 862
		public float gameintenseSetting;

		// Token: 0x0400035F RID: 863
		public int aliasSetting;

		// Token: 0x04000360 RID: 864
		public float aspectratio;

		// Token: 0x04000361 RID: 865
		public float aspectratio2;

		// Token: 0x04000362 RID: 866
		public int vehicleindex;

		// Token: 0x04000363 RID: 867
		public bool space_vibro;

		// Token: 0x04000364 RID: 868
		public int space_invertX;

		// Token: 0x04000365 RID: 869
		public int space_invertY;

		// Token: 0x04000366 RID: 870
		public float space_sentivityX;

		// Token: 0x04000367 RID: 871
		public float space_sentivityY;

		// Token: 0x04000368 RID: 872
		public int space_winvertX;

		// Token: 0x04000369 RID: 873
		public int space_winvertY;

		// Token: 0x0400036A RID: 874
		public float space_wsentivityX;

		// Token: 0x0400036B RID: 875
		public float space_wsentivityY;

		// Token: 0x0400036C RID: 876
		public int space_rinvertX;

		// Token: 0x0400036D RID: 877
		public int space_rinvertY;

		// Token: 0x0400036E RID: 878
		public float space_rsentivityX;

		// Token: 0x0400036F RID: 879
		public float space_rsentivityY;

		// Token: 0x04000370 RID: 880
		public int vibroSetting;

		// Token: 0x04000371 RID: 881
		public float camradian;

		// Token: 0x04000372 RID: 882
		public float camradius;

		// Token: 0x04000373 RID: 883
		public int[] allcamsradius;

		// Token: 0x04000374 RID: 884
		public int[] allcamsorbit;

		// Token: 0x04000375 RID: 885
		public int[] allcamsaltitude;

		// Token: 0x04000376 RID: 886
		public int[] allcamslens;

		// Token: 0x04000377 RID: 887
		public int roverindex;

		// Token: 0x04000378 RID: 888
		public int roverrotlock;

		// Token: 0x04000379 RID: 889
		public int roverhitelock;

		// Token: 0x0400037A RID: 890
		public float[] roverdist;

		// Token: 0x0400037B RID: 891
		public float[] roverheight;

		// Token: 0x0400037C RID: 892
		public float[] roverradian;

		// Token: 0x0400037D RID: 893
		public int landerindex;

		// Token: 0x0400037E RID: 894
		public int landerrotlock;

		// Token: 0x0400037F RID: 895
		public int landerhitelock;

		// Token: 0x04000380 RID: 896
		public float[] landerdist;

		// Token: 0x04000381 RID: 897
		public float[] landerheight;

		// Token: 0x04000382 RID: 898
		public float[] landerradian;

		// Token: 0x04000383 RID: 899
		public int loadflag;

		// Token: 0x04000384 RID: 900
		public int drawflag;

		// Token: 0x04000385 RID: 901
		public int menuflag;

		// Token: 0x04000386 RID: 902
		public SpriteFont halo;

		// Token: 0x04000387 RID: 903
		public SpriteFont halo2;

		// Token: 0x04000388 RID: 904
		public Texture2D glow1;

		// Token: 0x04000389 RID: 905
		public SoundEffect landerEngine;

		// Token: 0x0400038A RID: 906
		public SoundEffect fuellow;

		// Token: 0x0400038B RID: 907
		public SoundEffect fuelfull;

		// Token: 0x0400038C RID: 908
		public SoundEffect opensolar;

		// Token: 0x0400038D RID: 909
		public SoundEffect refine1;

		// Token: 0x0400038E RID: 910
		public SoundEffect refine2;

		// Token: 0x0400038F RID: 911
		public SoundEffect roverEngine;

		// Token: 0x04000390 RID: 912
		public SoundEffect dropEngine;

		// Token: 0x04000391 RID: 913
		public SoundEffect gravel;

		// Token: 0x04000392 RID: 914
		public SoundEffect overture;

		// Token: 0x04000393 RID: 915
		public SoundEffect breath;

		// Token: 0x04000394 RID: 916
		public SoundEffect boom;

		// Token: 0x04000395 RID: 917
		public SoundEffect clickmenu;

		// Token: 0x04000396 RID: 918
		public SoundEffect click;

		// Token: 0x04000397 RID: 919
		public SoundEffect step;

		// Token: 0x04000398 RID: 920
		public SoundEffect forward;

		// Token: 0x04000399 RID: 921
		public SoundEffect back;

		// Token: 0x0400039A RID: 922
		public SoundEffect rejected;

		// Token: 0x0400039B RID: 923
		public SoundEffect radio1;

		// Token: 0x0400039C RID: 924
		public SoundEffect radio2;

		// Token: 0x0400039D RID: 925
		public SoundEffect radio3;

		// Token: 0x0400039E RID: 926
		public SoundEffect release;

		// Token: 0x0400039F RID: 927
		public SoundEffect cablesnap;

		// Token: 0x040003A0 RID: 928
		public SoundEffect crack;

		// Token: 0x040003A1 RID: 929
		public SoundEffect cheer;

		// Token: 0x040003A2 RID: 930
		public SoundEffect breakage;

		// Token: 0x040003A3 RID: 931
		public SoundEffect breakage2;

		// Token: 0x040003A4 RID: 932
		public SoundEffect dropBeacon;

		// Token: 0x040003A5 RID: 933
		public SoundEffect eraseBeacon;

		// Token: 0x040003A6 RID: 934
		public SoundEffect warning;

		// Token: 0x040003A7 RID: 935
		public SoundEffect boulderhit;

		// Token: 0x040003A8 RID: 936
		public SoundEffect boulderhit2;

		// Token: 0x040003A9 RID: 937
		public SoundEffect boulderhit3;

		// Token: 0x040003AA RID: 938
		public SoundEffect boulderhit4;

		// Token: 0x040003AB RID: 939
		public SoundEffect shalehit1;

		// Token: 0x040003AC RID: 940
		public SoundEffect steeldrum1;

		// Token: 0x040003AD RID: 941
		public SoundEffect steeldrum2;

		// Token: 0x040003AE RID: 942
		public SoundEffect forklift;

		// Token: 0x040003AF RID: 943
		public SoundEffect confirm;

		// Token: 0x040003B0 RID: 944
		public SoundEffect shocks;

		// Token: 0x040003B1 RID: 945
		public SoundEffect shocks2;

		// Token: 0x040003B2 RID: 946
		public SoundEffect shocks3;

		// Token: 0x040003B3 RID: 947
		public SoundEffect shocks4;

		// Token: 0x040003B4 RID: 948
		public SoundEffect enginex;

		// Token: 0x040003B5 RID: 949
		public SoundEffect scoopx;

		// Token: 0x040003B6 RID: 950
		public SoundEffect groundhit;

		// Token: 0x040003B7 RID: 951
		public SoundEffect ramp;

		// Token: 0x040003B8 RID: 952
		public SoundEffect tada;

		// Token: 0x040003B9 RID: 953
		public SoundEffect tada2;

		// Token: 0x040003BA RID: 954
		public SoundEffect tada3;

		// Token: 0x040003BB RID: 955
		public SoundEffect tada4;

		// Token: 0x040003BC RID: 956
		public SoundEffect tada5;

		// Token: 0x040003BD RID: 957
		public SoundEffect tada6;

		// Token: 0x040003BE RID: 958
		public SoundEffect tada7;

		// Token: 0x040003BF RID: 959
		public SoundEffect tada10;

		// Token: 0x040003C0 RID: 960
		public SoundEffect gemfound;

		// Token: 0x040003C1 RID: 961
		public SoundEffect select;

		// Token: 0x040003C2 RID: 962
		public SoundEffect land;

		// Token: 0x040003C3 RID: 963
		public SoundEffect jump;

		// Token: 0x040003C4 RID: 964
		public SoundEffect bone;

		// Token: 0x040003C5 RID: 965
		public SoundEffect alarm1;

		// Token: 0x040003C6 RID: 966
		public SoundEffect lever;

		// Token: 0x040003C7 RID: 967
		public SoundEffect door;

		// Token: 0x040003C8 RID: 968
		public SoundEffect drop;

		// Token: 0x040003C9 RID: 969
		public SoundEffect horn;

		// Token: 0x040003CA RID: 970
		public SoundEffect hum;

		// Token: 0x040003CB RID: 971
		public int[] equip;

		// Token: 0x040003CC RID: 972
		public float roverSpeed;

		// Token: 0x040003CD RID: 973
		public float roverGrip;

		// Token: 0x040003CE RID: 974
		public float roverTurn;

		// Token: 0x040003CF RID: 975
		public bool cheats_test;

		// Token: 0x040003D0 RID: 976
		public Vector3 gemDropPosition;

		// Token: 0x040003D1 RID: 977
		public Vector3 gemDropVelocity;

		// Token: 0x040003D2 RID: 978
		public float gemDropScale;

		// Token: 0x040003D3 RID: 979
		public int gemDropType;

		// Token: 0x040003D4 RID: 980
		public string mess;

		// Token: 0x040003D5 RID: 981
		public int oldgamercount;

		// Token: 0x040003D6 RID: 982
		public int oldgamer0;

		// Token: 0x040003D7 RID: 983
		public int oldgamer1;

		// Token: 0x040003D8 RID: 984
		public int oldgamer2;

		// Token: 0x040003D9 RID: 985
		public int oldgamer3;

		// Token: 0x040003DA RID: 986
		public PlayerIndex playnum;

		// Token: 0x040003DB RID: 987
		public StorageDevice device;

		// Token: 0x040003DC RID: 988
		public float rightstickX;

		// Token: 0x040003DD RID: 989
		public float rightstickY;

		// Token: 0x040003DE RID: 990
		public float lefttrigger;

		// Token: 0x040003DF RID: 991
		public float righttrigger;

		// Token: 0x040003E0 RID: 992
		public bool rightbumper;

		// Token: 0x040003E1 RID: 993
		public bool leftbumper;

		// Token: 0x040003E2 RID: 994
		public string[] planetName;

		// Token: 0x040003E3 RID: 995
		public buildStars Globalstarmap;

		// Token: 0x040003E4 RID: 996
		public float vehiclelerp;

		// Token: 0x040003E5 RID: 997
		public Texture2D blankTexture;

		// Token: 0x040003E6 RID: 998
		public int poppy;

		// Token: 0x040003E7 RID: 999
		public int menutype;

		// Token: 0x040003E8 RID: 1000
		public Texture2D manmadestars;

		// Token: 0x040003E9 RID: 1001
		public GraphicsDeviceManager gg;

		// Token: 0x040003EA RID: 1002
		public int loadscreenIndex;

		// Token: 0x040003EB RID: 1003
		public List<int> loadScreen;

		// Token: 0x040003EC RID: 1004
		public Texture2D camedit;

		// Token: 0x040003ED RID: 1005
		public Texture2D interfaceBlob;

		// Token: 0x040003EE RID: 1006
		public Texture2D starblob;

		// Token: 0x040003EF RID: 1007
		public Texture2D messageBlob;

		// Token: 0x040003F0 RID: 1008
		public Texture2D iconbar;

		// Token: 0x040003F1 RID: 1009
		public Texture2D hudbuttons;

		// Token: 0x040003F2 RID: 1010
		public Texture2D helmet1;

		// Token: 0x040003F3 RID: 1011
		public Texture2D helmet2;

		// Token: 0x040003F4 RID: 1012
		public Texture2D rooms;

		// Token: 0x040003F5 RID: 1013
		public Texture2D codeBG;

		// Token: 0x040003F6 RID: 1014
		public Texture2D gear2;

		// Token: 0x040003F7 RID: 1015
		public Texture2D buttonOn;

		// Token: 0x040003F8 RID: 1016
		public Texture2D loadBG;

		// Token: 0x040003F9 RID: 1017
		public Texture2D entryrgb2;

		// Token: 0x040003FA RID: 1018
		public Texture2D entryShad;

		// Token: 0x040003FB RID: 1019
		public SoundEffect buttonGong;

		// Token: 0x040003FC RID: 1020
		public SoundEffect spinpics;

		// Token: 0x040003FD RID: 1021
		public SpriteFont tahoma1;

		// Token: 0x040003FE RID: 1022
		public SpriteFont tahoma2;

		// Token: 0x040003FF RID: 1023
		public SpriteFont loadfont1;

		// Token: 0x04000400 RID: 1024
		public int bitmap;

		// Token: 0x04000401 RID: 1025
		public int gridScale;

		// Token: 0x04000402 RID: 1026
		public int unit;

		// Token: 0x04000403 RID: 1027
		public string body;

		// Token: 0x04000404 RID: 1028
		public string scooper;

		// Token: 0x04000405 RID: 1029
		public string hauler;

		// Token: 0x04000406 RID: 1030
		public string solar;

		// Token: 0x04000407 RID: 1031
		public string solarB;

		// Token: 0x04000408 RID: 1032
		public string weapon;

		// Token: 0x04000409 RID: 1033
		public string rack;

		// Token: 0x0400040A RID: 1034
		public string backwheel;

		// Token: 0x0400040B RID: 1035
		public string rfjoint;

		// Token: 0x0400040C RID: 1036
		public string lfjoint;

		// Token: 0x0400040D RID: 1037
		public string rf;

		// Token: 0x0400040E RID: 1038
		public string lf;

		// Token: 0x0400040F RID: 1039
		public List<string> roverparts;

		// Token: 0x04000410 RID: 1040
		public Model roverModel;

		// Token: 0x04000411 RID: 1041
		public Model astroModel;

		// Token: 0x04000412 RID: 1042
		public Model controllerX;

		// Token: 0x04000413 RID: 1043
		public Matrix wheelRollMatrix;

		// Token: 0x04000414 RID: 1044
		public Matrix wheelRollMatrix2;

		// Token: 0x04000415 RID: 1045
		public Matrix rackMatrix;

		// Token: 0x04000416 RID: 1046
		public Matrix solar1aMatrix;

		// Token: 0x04000417 RID: 1047
		public Matrix solar1bMatrix;

		// Token: 0x04000418 RID: 1048
		public Matrix scooperMatrix;

		// Token: 0x04000419 RID: 1049
		public Matrix bucketMatrix;

		// Token: 0x0400041A RID: 1050
		public Matrix binMatrix;

		// Token: 0x0400041B RID: 1051
		public ModelBone BackWheelBone;

		// Token: 0x0400041C RID: 1052
		public ModelBone leftFrontWheelBone;

		// Token: 0x0400041D RID: 1053
		public ModelBone rightFrontWheelBone;

		// Token: 0x0400041E RID: 1054
		public ModelBone leftFrontjointBone;

		// Token: 0x0400041F RID: 1055
		public ModelBone rightFrontjointBone;

		// Token: 0x04000420 RID: 1056
		public ModelBone rackBone;

		// Token: 0x04000421 RID: 1057
		public ModelBone haulerBone;

		// Token: 0x04000422 RID: 1058
		public ModelBone bodyBone;

		// Token: 0x04000423 RID: 1059
		public ModelBone weaponBone;

		// Token: 0x04000424 RID: 1060
		public ModelBone scooperBone;

		// Token: 0x04000425 RID: 1061
		public ModelBone solar1aBone;

		// Token: 0x04000426 RID: 1062
		public ModelBone solar1bBone;

		// Token: 0x04000427 RID: 1063
		public Matrix BackWheelTrans;

		// Token: 0x04000428 RID: 1064
		public Matrix leftFrontWheelTrans;

		// Token: 0x04000429 RID: 1065
		public Matrix rightFrontWheelTrans;

		// Token: 0x0400042A RID: 1066
		public Matrix leftFrontjointTrans;

		// Token: 0x0400042B RID: 1067
		public Matrix rightFrontjointTrans;

		// Token: 0x0400042C RID: 1068
		public Matrix rackTrans;

		// Token: 0x0400042D RID: 1069
		public Matrix bodyTrans;

		// Token: 0x0400042E RID: 1070
		public Matrix haulerTrans;

		// Token: 0x0400042F RID: 1071
		public Matrix weaponTrans;

		// Token: 0x04000430 RID: 1072
		public Matrix scooperTrans;

		// Token: 0x04000431 RID: 1073
		public Matrix solar1aTrans;

		// Token: 0x04000432 RID: 1074
		public Matrix solar1bTrans;

		// Token: 0x04000433 RID: 1075
		private Matrix[] origTransforms;

		// Token: 0x04000434 RID: 1076
		public string errorline;

		// Token: 0x04000435 RID: 1077
		public Matrix ScaleMatrix1;

		// Token: 0x04000436 RID: 1078
		public Rectangle ScaleRect1;

		// Token: 0x04000437 RID: 1079
		public Vector2 diffscaler;

		// Token: 0x04000438 RID: 1080
		public float startaspect;

		// Token: 0x04000439 RID: 1081
		public float stretch;

		// Token: 0x0400043A RID: 1082
		public float xoffset;

		// Token: 0x0400043B RID: 1083
		public bool inSpace;

		// Token: 0x0400043C RID: 1084
		public bool astroloaded;

		// Token: 0x0400043D RID: 1085
		public float startwidth;

		// Token: 0x0400043E RID: 1086
		public float starthite;

		// Token: 0x0400043F RID: 1087
		public PlayerIndex playerindex;

		// Token: 0x04000440 RID: 1088
		private int callback;

		// Token: 0x04000441 RID: 1089
		public string stat1;

		// Token: 0x04000442 RID: 1090
		public string stat2;

		// Token: 0x04000443 RID: 1091
		public string stat3;

		// Token: 0x04000444 RID: 1092
		public string stat4;

		// Token: 0x04000445 RID: 1093
		public string stat5;

		// Token: 0x04000446 RID: 1094
		public string stat6;

		// Token: 0x04000447 RID: 1095
		public string stat7;

		// Token: 0x04000448 RID: 1096
		public float ar1;

		// Token: 0x04000449 RID: 1097
		public float ar2;

		// Token: 0x0400044A RID: 1098
		private Random rr;

		// Token: 0x0400044B RID: 1099
		public int chatIndex;

		// Token: 0x0400044C RID: 1100
		public float chatFade;

		// Token: 0x0400044D RID: 1101
		public List<ScreenManager.chatty> chatHistory;

		// Token: 0x02000003 RID: 3
		public class xcross
		{
			// Token: 0x0400044E RID: 1102
			public Texture2D texture;

			// Token: 0x0400044F RID: 1103
			public int type;

			// Token: 0x04000450 RID: 1104
			public bool legal;
		}

		// Token: 0x02000004 RID: 4
		public class chatty
		{
			// Token: 0x04000451 RID: 1105
			public string message;

			// Token: 0x04000452 RID: 1106
			public string name;

			// Token: 0x04000453 RID: 1107
			public Color messColor;

			// Token: 0x04000454 RID: 1108
			public Color nameColor;

			// Token: 0x04000455 RID: 1109
			public int gap;

			// Token: 0x04000456 RID: 1110
			public ulong idlong;
		}
	}
}
