using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SkinnedModel;
using Steamworks;

namespace Blood
{
	// Token: 0x0200008F RID: 143
	internal class MainMenu : GameScreen
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x0011BD26 File Offset: 0x00119F26
		private void Form1_KeyDown(object sender, FormClosingEventArgs e)
		{
			this.abortThread();
			this.UnloadContent();
			base.ScreenManager.exitmyGame();
			this.exitMyGame = true;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0011BD74 File Offset: 0x00119F74
		public MainMenu(bool showit)
		{
			if (showit)
			{
				base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
				base.TransitionOffTime = TimeSpan.FromSeconds(0.20000000298023224);
			}
			else
			{
				base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
				base.TransitionOffTime = TimeSpan.FromSeconds(0.20000000298023224);
			}
			this.backgroundThread = new Thread(new ThreadStart(this.BackgroundWorkerThread));
			this.backgroundThread.Name = "fuckerThread";
			this.backgroundThreadExit = new ManualResetEvent(false);
			this.showTitle = showit;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0011E3DC File Offset: 0x0011C5DC
		public override void LoadContent()
		{
			this.sc = base.ScreenManager;
			this.sc.inSpace = false;
			this.sc.trophy.checkAll();
			Form form = (Form)Control.FromHandle(this.sc.Game.Window.Handle);
			form.FormClosing += this.Form1_KeyDown;
			try
			{
				if (SteamAPI.IsSteamRunning())
				{
					CSteamID steamID = SteamUser.GetSteamID();
					this.sc.developer = false;
					if (steamID.m_SteamID == 76561198259812257UL)
					{
						this.sc.developer = true;
					}
					this.sc.guestpresent = false;
				}
			}
			catch
			{
			}
			this.sc.cryptHits = this.sc.cryptHitsReset;
			this.sc.chatboxHELP = false;
			this.sc.hostAllowCheats = true;
			this.sc.allWeapons = true;
			this.sc.hostFriendly = false;
			this.sc.hostBobbleheads = false;
			this.sc.hordemode = false;
			this.sc.paintland = false;
			this.sc.tunnelMode = false;
			this.sc.hatindex = 0;
			if (!this.sc.host)
			{
				this.sc.df = this.sc.df_orig;
			}
			this.sc.host = true;
			this.sc.myfov = (float)Math.Cos((double)MathHelper.ToRadians((this.sc.mylens + 30f) / 2f));
			this.sc.lobby.inviteRequest = false;
			this.sc.lobby.acceptRequests();
			this.sc.introCamera = 0f;
			this.content2 = new ContentManager(base.ScreenManager.Game.Services, "ABCDE1");
			this.sc.chatHistory.Clear();
			this.sc.weFailed = 0;
			this.sc.keepShowingWarning = true;
			this.sc.walletHint = false;
			this.sc.walletHintShow = false;
			this.sc.deathcamHint = false;
			this.spriteBatch = new SpriteBatch(this.sc.GraphicsDevice);
			this.sc.easter_skull1 = false;
			this.sc.easter_skull2 = false;
			this.sc.easter_skull3 = false;
			this.sc.easter_skulltalk = false;
			this.speak = this.content2.Load<SoundEffect>("cough");
			this.speaker = this.speak.CreateInstance();
			this.font = this.content2.Load<SpriteFont>("ammo");
			this.font2 = this.content2.Load<SpriteFont>("ammomedium");
			this.font3 = this.sc.font3;
			this.deadfont = this.content2.Load<SpriteFont>("deadfont");
			this.diaryfont = this.sc.font3;
			Princess.cuttyCount = 0;
			Princess4.cuttyCount = 0;
			this.sc.redContrast = 128;
			this.sc.shitContrast = 128;
			this.sc.contrastBU = 128;
			this.rr = new Random();
			this.sc.bc1 = this.content2.Load<Texture2D>("bc1");
			this.sc.bc2 = this.content2.Load<Texture2D>("bc2");
			this.music1 = this.content2.Load<SoundEffect>("ThunderDreams");
			this.menuMusic = this.music1.CreateInstance();
			this.menuMusic.IsLooped = true;
			this.menuMusic.Play();
			this.menuMusic.Volume = 0.5f;
			int num = this.rr.Next(0, 5);
			List<string> list = new List<string>();
			if (num == 0)
			{
				list.Add("titlePigA");
				list.Add("titlePigB");
				list.Add("titlePigC");
			}
			if (num == 1)
			{
				list.Add("titlePigC");
				list.Add("titlePigB");
				list.Add("titlePigA");
			}
			if (num == 2)
			{
				list.Add("titlePigB");
				list.Add("titlePigA");
				list.Add("titlePigC");
			}
			if (num == 3)
			{
				list.Add("titlePigC");
				list.Add("titlePigA");
				list.Add("titlePigB");
			}
			if (num == 4)
			{
				list.Add("titlePigA");
				list.Add("titlePigC");
				list.Add("titlePigB");
			}
			int num2 = this.rr.Next(0, list.Count);
			string text = list[num2];
			this.sc.titlePig0 = this.content2.Load<Texture2D>(text);
			list.RemoveAt(num2);
			num2 = this.rr.Next(0, list.Count);
			text = list[num2];
			this.sc.titlePig2 = this.content2.Load<Texture2D>(text);
			list.RemoveAt(num2);
			num2 = this.rr.Next(0, list.Count);
			text = list[num2];
			this.sc.titlePig3 = this.content2.Load<Texture2D>(text);
			this.sc.titlePig1 = this.content2.Load<Texture2D>("pigsrun2");
			this.myRect = new Rectangle[20];
			this.myRect[0] = new Rectangle(0, 0, 300, 195);
			this.myRect[1] = new Rectangle(0, 199, 300, 195);
			this.myRect[2] = new Rectangle(304, 0, 300, 195);
			this.myRect[3] = new Rectangle(304, 199, 300, 195);
			this.myRect[4] = new Rectangle(608, 0, 300, 195);
			this.myRect[5] = new Rectangle(608, 199, 300, 195);
			this.myRect[6] = new Rectangle(0, 398, 300, 195);
			this.myRect[7] = new Rectangle(0, 597, 300, 195);
			this.myRect[8] = new Rectangle(304, 398, 300, 195);
			this.myRect[9] = new Rectangle(304, 597, 300, 195);
			this.myRect[10] = new Rectangle(608, 398, 300, 195);
			this.myRect[11] = new Rectangle(608, 597, 300, 195);
			this.myRect[12] = new Rectangle(912, 0, 300, 195);
			this.myRect[13] = new Rectangle(912, 199, 300, 195);
			this.myRect[14] = new Rectangle(1216, 0, 300, 195);
			this.myRect[15] = new Rectangle(912, 398, 300, 195);
			this.myRect[16] = new Rectangle(1216, 199, 300, 195);
			this.myRect[17] = new Rectangle(912, 597, 300, 195);
			this.myRect[18] = new Rectangle(1216, 398, 300, 195);
			this.myRect[19] = new Rectangle(1216, 597, 300, 195);
			if (!this.sc.titlesLoaded)
			{
				this.sc.loadTitles();
			}
			if (this.showTitle)
			{
				this.counterStart = 260;
				this.titlecounter = (float)(this.counterStart + 80);
			}
			this.backgroundThread.Start();
			this.mouseState = Mouse.GetState();
			this.sc.mymouse.X = (float)this.mouseState.X;
			this.sc.mymouse.Y = (float)this.mouseState.Y;
			this.sc.Game.IsMouseVisible = false;
			this.sc.tempRifle = 2;
			this.sc.tempPistol = 2;
			this.sc.tempAmmo = 0;
			this.sc.tempMag = 0;
			this.sc.lobby.players = "0";
			this.sc.lobby.lobbyplayers = "0";
			this.Content = new ContentManager(this.sc.Game.Services, "Content");
			this.sc.loadFarmer();
			this.farmerModel = this.sc.farmerModel;
			this.farmerTexture = this.sc.farmerTexture;
			this.farmerSkin = this.Content.Load<Effect>("effects\\farmerSkinDiary");
			this.farmerSkin.Parameters["World"].SetValue(Matrix.CreateTranslation(0f, 0f, 0f));
			this.farmerSkin.Parameters["Texture"].SetValue(this.farmerTexture);
			SkinningData skinningData = this.sc.farmerModel.Tag as SkinningData;
			this.farmer1 = new AnimationPlayer[11];
			this.farmer1[0] = new AnimationPlayer(skinningData);
			AnimationClip animationClip = skinningData.AnimationClips["farmer"];
			this.farmer1[0].StartClip(animationClip);
			this.farmer1[1] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["wave"];
			this.farmer1[1].StartClip(animationClip);
			this.farmer1[2] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["pat"];
			this.farmer1[2].StartClip(animationClip);
			this.farmer1[3] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["nose"];
			this.farmer1[3].StartClip(animationClip);
			this.farmer1[4] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["kick"];
			this.farmer1[4].StartClip(animationClip);
			this.farmer1[5] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["convulse1"];
			this.farmer1[5].StartClip(animationClip);
			this.farmer1[6] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["vomit"];
			this.farmer1[6].StartClip(animationClip);
			this.farmer1[7] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["vomit2"];
			this.farmer1[7].StartClip(animationClip);
			this.farmer1[8] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["point1"];
			this.farmer1[8].StartClip(animationClip);
			this.farmer1[9] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["clap"];
			this.farmer1[9].StartClip(animationClip);
			this.farmer1[10] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["head"];
			this.farmer1[10].StartClip(animationClip);
			this.farmerJaw = new List<float>();
			this.farmerAnim.animCount = -1f;
			this.farmerAnim.animTween = 0f;
			this.farmerAnim.animList = new List<int>();
			this.farmerAnim.animClip = -1;
			this.farmerAnim.animMax = 0;
			this.farmerAnim.animMin = 0;
			this.farmerAnim.animLoop = 0;
			this.im.Clear();
			for (int i = 0; i < 10; i++)
			{
				this.im.Add(new MainMenu.imim());
				this.im[i].spriteIndex = i;
			}
			this.displayList.Clear();
			if (!this.sc.localLoad)
			{
				this.loads.Add(4);
				return;
			}
			this.sc.loadModels();
			this.sc.moremodelsLoaded = true;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0011F158 File Offset: 0x0011D358
		public override void UnloadContent()
		{
			this.content2.Unload();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0011F168 File Offset: 0x0011D368
		private void BackgroundWorkerThread()
		{
			this.backgroundThread.IsBackground = true;
			while (!this.backgroundThreadExit.WaitOne(50))
			{
				if (!this.isBusy && this.loads.Count > 0)
				{
					this.isBusy = true;
					switch (this.loads[0])
					{
					case 4:
						this.sc.loadModels();
						this.sc.loadBigModel();
						this.sc.moremodelsLoaded = true;
						break;
					}
					this.loads.RemoveAt(0);
					this.isBusy = false;
				}
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0011F214 File Offset: 0x0011D414
		private void abortThread()
		{
			try
			{
				if (this.backgroundThread != null)
				{
					this.backgroundThreadExit.Set();
					this.backgroundThread.Join();
				}
			}
			catch
			{
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0011F258 File Offset: 0x0011D458
		private void disposeMovies()
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0011F25C File Offset: 0x0011D45C
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			if (this.pigwalk > 500)
			{
				this.pigwalk++;
			}
			if (this.pigwalk % 3 == 0 && this.pigwalk > 500)
			{
				this.pigcount++;
				this.pigcount %= 20;
			}
			this.sc.workshop.myTimer++;
			this.sc.myTimer += 1f;
			this.moreTimer++;
			this.titlecounter -= 1.5f;
			if ((int)this.titlecounter <= this.counterStart / 2 && this.rampCount == 0)
			{
				this.ramp = 0f;
				this.rampCount = 1;
			}
			if (!this.sc.lobby.ALLClear)
			{
				this.sc.lobby.ALLCount++;
				if (this.sc.lobby.ALLCount > 1800)
				{
					this.sc.lobby.ALLClear = true;
				}
			}
			else
			{
				this.sc.lobby.ALLCount = 0;
			}
			this.covered = otherScreenHasFocus;
			base.Update(gameTime, false, false);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0011F3A8 File Offset: 0x0011D5A8
		public void selectOne(int min, int max, InputState input)
		{
			if (input.IsMenuLeft(base.ControllingPlayer))
			{
				this.whichbutton--;
				if (this.whichbutton < min)
				{
					this.whichbutton = max;
				}
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
			if (input.IsMenuRight(base.ControllingPlayer))
			{
				this.whichbutton++;
				if (this.whichbutton > max)
				{
					this.whichbutton = min;
				}
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0011F45C File Offset: 0x0011D65C
		public void selectOneExclusive(int min, int max, InputState input)
		{
			if (input.IsMenuLeftExclusive(base.ControllingPlayer))
			{
				this.sc.Game.IsMouseVisible = false;
				this.whichbutton--;
				this.whichbutton = (int)MathHelper.Clamp((float)this.whichbutton, (float)min, (float)max);
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
			if (input.IsMenuRightExclusive(base.ControllingPlayer))
			{
				this.sc.Game.IsMouseVisible = false;
				this.whichbutton++;
				this.whichbutton = (int)MathHelper.Clamp((float)this.whichbutton, (float)min, (float)max);
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
			if (input.IsNewButtonPress(Buttons.LeftShoulder, base.ControllingPlayer, out this.playerindex))
			{
				this.sc.Game.IsMouseVisible = false;
				this.whichbutton--;
				this.whichbutton = (int)MathHelper.Clamp((float)this.whichbutton, (float)min, (float)max);
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
			if (input.IsNewButtonPress(Buttons.RightShoulder, base.ControllingPlayer, out this.playerindex))
			{
				this.sc.Game.IsMouseVisible = false;
				this.whichbutton++;
				this.whichbutton = (int)MathHelper.Clamp((float)this.whichbutton, (float)min, (float)max);
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
			if (this.menuState != MainMenu.states.graphics)
			{
				if (input.IsNewButtonPress(Buttons.DPadLeft, base.ControllingPlayer, out this.playerindex))
				{
					this.sc.Game.IsMouseVisible = false;
					this.whichbutton--;
					this.whichbutton = (int)MathHelper.Clamp((float)this.whichbutton, (float)min, (float)max);
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
				if (input.IsNewButtonPress(Buttons.DPadRight, base.ControllingPlayer, out this.playerindex))
				{
					this.sc.Game.IsMouseVisible = false;
					this.whichbutton++;
					this.whichbutton = (int)MathHelper.Clamp((float)this.whichbutton, (float)min, (float)max);
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0011F718 File Offset: 0x0011D918
		public void backtomain(InputState input, int num)
		{
			if ((this.mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && this.prevMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released) || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.whichbutton = num;
				this.menuState = MainMenu.states.main;
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.delayinput = true;
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0011F7B8 File Offset: 0x0011D9B8
		public void forgotsave()
		{
			this.sc.SavePrefs();
			this.sc.setupnum = this.sc.resolution;
			this.sc.aa = this.sc.aliasing;
			this.sc.setgraphics = false;
			this.menuState = this.lastState2;
			this.whichbutton = 1;
			this.delayinput = true;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0011F824 File Offset: 0x0011DA24
		public bool KMtoggle(Microsoft.Xna.Framework.Input.Keys k)
		{
			return this.keyState.IsKeyDown(k) && this.prevKeys.IsKeyUp(k);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0011F850 File Offset: 0x0011DA50
		private void handleDiary(ref InputState input)
		{
			if (this.sc.workshop.myTimer % 250 == 0 || !this.sc.workshop.populateNow)
			{
				this.sc.workshop.populateDiary();
				this.sc.workshop.populateNow = true;
			}
			this.farmerBones(ref this.farmer1[0]);
			this.directNum = -1;
			if (this.sc.developer && this.sc.workshop.testmode)
			{
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.F1))
				{
					this.recording1 = !this.recording1;
					if (this.recording1)
					{
						this.recording2 = false;
						this.recording3 = false;
						this.recording4 = false;
					}
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.F2))
				{
					this.recording2 = !this.recording2;
					if (this.recording2)
					{
						this.recording1 = false;
						this.recording3 = false;
						this.recording4 = false;
					}
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.F3))
				{
					this.recording3 = !this.recording3;
					if (this.recording3)
					{
						this.recording1 = false;
						this.recording2 = false;
						this.recording4 = false;
					}
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.F4))
				{
					this.recording4 = !this.recording4;
					if (this.recording4)
					{
						this.recording1 = false;
						this.recording2 = false;
						this.recording3 = false;
					}
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.F5))
				{
					this.writeFile();
					this.sc.tick.Play(this.sc.ev, 0.8f, 0f);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Home))
				{
					this.removeTrack(100);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.PageUp))
				{
					this.removeTrack(101);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.PageDown))
				{
					this.removeTrack(102);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.NumPad1))
				{
					this.removeTrack(0);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.NumPad2))
				{
					this.removeTrack(1);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.NumPad3))
				{
					this.removeTrack(2);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.NumPad4))
				{
					this.removeTrack(3);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.NumPad5))
				{
					this.removeTrack(4);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.NumPad6))
				{
					this.removeTrack(5);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.NumPad7))
				{
					this.removeTrack(6);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.NumPad8))
				{
					this.removeTrack(7);
				}
				if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.NumPad9))
				{
					this.removeTrack(8);
				}
				if (this.recording1)
				{
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D1))
					{
						this.directNum = 1000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D2))
					{
						this.directNum = 2000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D3))
					{
						this.directNum = 3000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D4))
					{
						this.directNum = 4000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D5))
					{
						this.directNum = 5000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D6))
					{
						this.directNum = 6000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D7))
					{
						this.directNum = 7000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D8))
					{
						this.directNum = 8000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D9))
					{
						this.directNum = 9000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D0))
					{
						this.directNum = 10000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Q))
					{
						this.directNum = 11000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.W))
					{
						this.directNum = 12000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.E))
					{
						this.directNum = 13000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.R))
					{
						this.directNum = 14000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.T))
					{
						this.directNum = 15000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Y))
					{
						this.directNum = 16000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.U))
					{
						this.directNum = 17000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.I))
					{
						this.directNum = 18000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.O))
					{
						this.directNum = 19000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.P))
					{
						this.directNum = 20000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.A))
					{
						this.directNum = 21000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.S))
					{
						this.directNum = 22000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D))
					{
						this.directNum = 23000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.F))
					{
						this.directNum = 24000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.G))
					{
						this.directNum = 25000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.H))
					{
						this.directNum = 26000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.J))
					{
						this.directNum = 27000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.K))
					{
						this.directNum = 28000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.L))
					{
						this.directNum = 29000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.OemSemicolon))
					{
						this.directNum = 30000;
					}
				}
				if (this.recording2)
				{
					this.directRate = 0;
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D1))
					{
						this.directNum = 51000;
						this.directRate = 0;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D2))
					{
						this.directNum = 52000;
						this.directRate = 0;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D3))
					{
						this.directNum = 53000;
						this.directRate = 0;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D4))
					{
						this.directNum = 54000;
						this.directRate = 0;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D5))
					{
						this.directNum = 55000;
						this.directRate = 0;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D6))
					{
						this.directNum = 56000;
						this.directRate = 0;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D7))
					{
						this.directNum = 57000;
						this.directRate = 0;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D8))
					{
						this.directNum = 58000;
						this.directRate = 0;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Q))
					{
						this.directNum = 51000;
						this.directRate = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.W))
					{
						this.directNum = 52000;
						this.directRate = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.E))
					{
						this.directNum = 53000;
						this.directRate = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.R))
					{
						this.directNum = 54000;
						this.directRate = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.T))
					{
						this.directNum = 55000;
						this.directRate = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Y))
					{
						this.directNum = 56000;
						this.directRate = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.U))
					{
						this.directNum = 57000;
						this.directRate = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.I))
					{
						this.directNum = 58000;
						this.directRate = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.O))
					{
						this.directNum = 59000;
						this.directRate = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.A))
					{
						this.directNum = 51000;
						this.directRate = 2;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.S))
					{
						this.directNum = 52000;
						this.directRate = 2;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D))
					{
						this.directNum = 53000;
						this.directRate = 2;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.F))
					{
						this.directNum = 54000;
						this.directRate = 2;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.G))
					{
						this.directNum = 55000;
						this.directRate = 2;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.H))
					{
						this.directNum = 56000;
						this.directRate = 2;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.J))
					{
						this.directNum = 57000;
						this.directRate = 2;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.K))
					{
						this.directNum = 58000;
						this.directRate = 2;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.L))
					{
						this.directNum = 59000;
						this.directRate = 2;
					}
					if (this.directNum >= 51000 && this.directNum <= 60000)
					{
						this.directNumX = (int)this.sc.adjustVector2(this.sc.mymouse).X + 5000;
						this.directNumY = (int)this.sc.adjustVector2(this.sc.mymouse).Y + 5000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Z))
					{
						this.directNum = 81000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.X))
					{
						this.directNum = 82000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.C))
					{
						this.directNum = 83000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.V))
					{
						this.directNum = 84000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.B))
					{
						this.directNum = 85000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.N))
					{
						this.directNum = 86000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.M))
					{
						this.directNum = 87000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.OemComma))
					{
						this.directNum = 88000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.OemPeriod))
					{
						this.directNum = 89000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.OemQuestion))
					{
						this.directNum = 90000;
					}
				}
				if (this.recording3)
				{
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D1))
					{
						this.mediaIndex = 0;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D2))
					{
						this.mediaIndex = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D3))
					{
						this.mediaIndex = 2;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D4))
					{
						this.mediaIndex = 3;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D5))
					{
						this.mediaIndex = 4;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D6))
					{
						this.mediaIndex = 5;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D7))
					{
						this.mediaIndex = 6;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D8))
					{
						this.mediaIndex = 7;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D9))
					{
						this.mediaIndex = 8;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D0))
					{
						this.mediaIndex = 9;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Q))
					{
						for (int i = 0; i < 10; i++)
						{
							if (this.mediaIndex == i)
							{
								this.directNum = 201000 + i * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.W))
					{
						for (int j = 0; j < 10; j++)
						{
							if (this.mediaIndex == j)
							{
								this.directNum = 211000 + j * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.E))
					{
						for (int k = 0; k < 10; k++)
						{
							if (this.mediaIndex == k)
							{
								this.directNum = 221000 + k * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.R))
					{
						for (int l = 0; l < 10; l++)
						{
							if (this.mediaIndex == l)
							{
								this.directNum = 231000 + l * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.T))
					{
						for (int m = 0; m < 10; m++)
						{
							if (this.mediaIndex == m)
							{
								this.directNum = 241000 + m * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Y))
					{
						for (int n = 0; n < 10; n++)
						{
							if (this.mediaIndex == n)
							{
								this.directNum = 251000 + n * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.U))
					{
						for (int num = 0; num < 10; num++)
						{
							if (this.mediaIndex == num)
							{
								this.directNum = 261000 + num * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.I))
					{
						for (int num2 = 0; num2 < 10; num2++)
						{
							if (this.mediaIndex == num2)
							{
								this.directNum = 271000 + num2 * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.O))
					{
						for (int num3 = 0; num3 < 10; num3++)
						{
							if (this.mediaIndex == num3)
							{
								this.directNum = 281000 + num3 * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.P))
					{
						for (int num4 = 0; num4 < 10; num4++)
						{
							if (this.mediaIndex == num4)
							{
								this.directNum = 291000 + num4 * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.A))
					{
						for (int num5 = 0; num5 < 10; num5++)
						{
							if (this.mediaIndex == num5)
							{
								this.directNum = 111000 + num5 * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.S))
					{
						for (int num6 = 0; num6 < 10; num6++)
						{
							if (this.mediaIndex == num6)
							{
								this.directNum = 121000 + num6 * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D))
					{
						for (int num7 = 0; num7 < 10; num7++)
						{
							if (this.mediaIndex == num7)
							{
								this.directNum = 131000 + num7 * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.F))
					{
						for (int num8 = 0; num8 < 10; num8++)
						{
							if (this.mediaIndex == num8)
							{
								this.directNum = 141000 + num8 * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Z))
					{
						for (int num9 = 0; num9 < 10; num9++)
						{
							if (this.mediaIndex == num9)
							{
								this.directNum = 151000 + num9 * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.X))
					{
						for (int num10 = 0; num10 < 10; num10++)
						{
							if (this.mediaIndex == num10)
							{
								this.directNum = 161000 + num10 * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.C))
					{
						for (int num11 = 0; num11 < 10; num11++)
						{
							if (this.mediaIndex == num11)
							{
								this.directNum = 171000 + num11 * 1000;
							}
						}
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.V))
					{
						for (int num12 = 0; num12 < 10; num12++)
						{
							if (this.mediaIndex == num12)
							{
								this.directNum = 181000 + num12 * 1000;
							}
						}
					}
				}
				if (this.recording4)
				{
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D1))
					{
						this.directNum = 301000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D2))
					{
						this.directNum = 302000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D3))
					{
						this.directNum = 303000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D4))
					{
						this.directNum = 304000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D5))
					{
						this.directNum = 305000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D6))
					{
						this.directNum = 306000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D7))
					{
						this.directNum = 307000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D8))
					{
						this.directNum = 308000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D9))
					{
						this.directNum = 309000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Y))
					{
						this.colorXtemp = 0;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.U))
					{
						this.colorXtemp = 1;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.I))
					{
						this.colorXtemp = 2;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.H))
					{
						this.colorXtemp = 3;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.J))
					{
						this.colorXtemp = 4;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.K))
					{
						this.colorXtemp = 5;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.N))
					{
						this.colorXtemp = 6;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.M))
					{
						this.colorXtemp = 7;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.OemComma))
					{
						this.colorXtemp = 8;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Q))
					{
						this.directNum = 311000 + this.colorXtemp * 1000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.W))
					{
						this.directNum = 321000 + this.colorXtemp * 1000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.E))
					{
						this.directNum = 331000 + this.colorXtemp * 1000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.A))
					{
						this.directNum = 341000 + this.colorXtemp * 1000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.S))
					{
						this.directNum = 351000 + this.colorXtemp * 1000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.D))
					{
						this.directNum = 361000 + this.colorXtemp * 1000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.Z))
					{
						this.directNum = 371000 + this.colorXtemp * 1000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.X))
					{
						this.directNum = 381000 + this.colorXtemp * 1000;
					}
					if (this.KMtoggle(Microsoft.Xna.Framework.Input.Keys.C))
					{
						this.directNum = 391000 + this.colorXtemp * 1000;
					}
				}
			}
			if (this.mouseback || input.IsNewButtonPress(Buttons.Back, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.farmerDialog1Loaded = false;
				try
				{
					this.farmerDialog1.sound[0].Dispose();
				}
				catch
				{
					this.farmerDialog1Loaded = false;
				}
				this.resetFarmerDirector();
				this.resetImages();
				this.sc.workshop.populateNow = false;
				this.whichbutton = 0;
				this.menuState = MainMenu.states.more;
				this.menuSelect = 1;
				this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
				this.delayinput = true;
				return;
			}
			if ((this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex)) && this.sc.workshop.entry.Count > 0)
			{
				for (int num13 = 0; num13 < this.sc.workshop.entry.Count; num13++)
				{
					if (this.whichbutton == num13)
					{
						this.resetImages();
						this.entryIndex = num13;
						if (this.sc.workshop.entry[num13].dates == "03.20.21" && (!this.sc.star1 || !this.sc.star2 || !this.sc.star3))
						{
							this.sc.harp2.Play(this.sc.ev, 0.5f, 0f);
							this.sc.star1 = true;
							this.sc.star2 = true;
							this.sc.star3 = true;
						}
						if (this.followPoint)
						{
							this.lastlookpos = this.lookpos[this.lookIndex];
						}
						if (!this.followPoint)
						{
							this.lastlookpos = this.sc.adjustVector2(this.sc.mymouse);
						}
						this.lookIndex = 0;
						this.followTween = 1f;
						this.sc.workshop.entryIndex = num13;
						this.farmerTalk();
						this.sc.workshopNum = this.sc.workshop.fileSize;
						this.sc.SavePrefs();
						this.followTimer = 2f;
						break;
					}
				}
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0012102C File Offset: 0x0011F22C
		private void handleMain(ref InputState input)
		{
			this.pigwalk = 0;
			this.pigcount = 11;
			this.sc.hordemode = false;
			this.sc.paintland = false;
			this.sc.lobby.players = "0";
			this.sc.lobby.lobbyplayers = "0";
			if (this.mouseback || input.IsNewButtonPress(Buttons.Back, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.exitGame();
				return;
			}
			this.selectOneExclusive(0, 3, input);
			if (this.sc.moremodelsLoaded && (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex)))
			{
				if (this.whichbutton == 0)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.playgame;
					this.b.main = false;
					this.menuSelect = 1;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
				}
				if (this.whichbutton == 1)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.coopChoice;
					this.b.main = false;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
				}
				if (this.whichbutton == 2)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.settings;
					this.b.main = false;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
				}
				if (this.whichbutton == 3)
				{
					this.whichbutton = 0;
					this.checkTrophyOnce = false;
					this.menuState = MainMenu.states.more;
					this.b.main = false;
					this.moreTimer = 440;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
				}
				if (this.whichbutton == 7 && !this.sc.lockVideosetup)
				{
					this.sc.setupnum = this.sc.resolution;
					this.sc.aa = this.sc.aliasing;
					this.whichbutton = 0;
					this.sc.harp2.Play(this.sc.ev * 0.6f, 0.4f, 0f);
					this.lastState2 = MainMenu.states.main;
					this.menuState = MainMenu.states.graphics;
					this.b.main = false;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
				}
				if (this.whichbutton == 8)
				{
					this.whichbutton = 0;
					this.sc.harp2.Play(this.sc.ev * 0.6f, -0.2f, 0f);
					this.lastState2 = MainMenu.states.main;
					this.menuState = MainMenu.states.audio;
					this.b.main = false;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
					this.slider1 = (int)Math.Round(Math.Sqrt((double)(this.sc.mv * 100f)));
					this.slider2 = (int)Math.Round(Math.Sqrt((double)(this.sc.ev * 100f)));
					this.slider3 = (int)Math.Round(Math.Sqrt((double)(this.sc.vv * 100f)));
					this.slider1 = (int)MathHelper.Clamp((float)this.slider1, 0f, 10f);
					this.slider2 = (int)MathHelper.Clamp((float)this.slider2, 0f, 10f);
					this.slider3 = (int)MathHelper.Clamp((float)this.slider3, 0f, 10f);
					this.sliderbox1 = (float)this.slider1 * 31.4f - 157f;
					this.sliderbox2 = (float)this.slider2 * 31.4f - 157f;
					this.sliderbox3 = (float)this.slider3 * 31.4f - 157f;
					this.myIndex = -1;
				}
				if (this.whichbutton == 9)
				{
					this.whichbutton = 0;
					this.sc.harp2.Play(this.sc.ev * 0.6f, 0.2f, 0f);
					this.menuState = MainMenu.states.controls;
					this.b.main = false;
					this.lastState2 = MainMenu.states.main;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
				}
				if (this.whichbutton == 11 && this.paintunlocked)
				{
					this.sc.harp2.Play(this.sc.ev * 0.6f, -0.2f, 0f);
					MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("Name a Famous Painter", 4);
					messageBoxScreen.Approved += delegate
					{
						this.sc.SavePrefs();
						this.whichbutton = 0;
						this.menuState = MainMenu.states.multi3player;
						this.b.main = false;
						this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
						this.delayinput = true;
					};
					messageBoxScreen.Failed += delegate
					{
						this.sc.pigDie[0].Play(this.sc.ev, 0f, 0f);
						this.delayinput = true;
					};
					messageBoxScreen.Cancelled += delegate
					{
						this.delayinput = true;
					};
					this.sc.AddScreen(messageBoxScreen, null);
				}
				if (this.whichbutton == 12)
				{
					this.sc.harp2.Play(this.sc.ev * 0.6f, 0.2f, 0f);
					if (this.sc.star1)
					{
						MessageBoxScreen2 messageBoxScreen2 = new MessageBoxScreen2("SECRET ABILITY\nEnable Faster Grenades?\n", 1);
						messageBoxScreen2.Accepted += delegate
						{
							this.sc.fastnades = true;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						messageBoxScreen2.Cancelled += delegate
						{
							this.sc.fastnades = false;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						this.sc.AddScreen(messageBoxScreen2, null);
					}
					if (!this.sc.star1)
					{
						MessageBoxScreen2 messageBoxScreen3 = new MessageBoxScreen2("SECRET#1  LOCKED", 0);
						messageBoxScreen3.Accepted += delegate
						{
							this.delayinput = true;
						};
						messageBoxScreen3.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen3, null);
					}
				}
				if (this.whichbutton == 13)
				{
					this.sc.harp2.Play(this.sc.ev * 0.6f, 0.2f, 0f);
					if (this.sc.star2)
					{
						MessageBoxScreen2 messageBoxScreen4 = new MessageBoxScreen2("SECRET ABILITY\nEnable Extra Gore?\nchatbox :gore\n", 1);
						messageBoxScreen4.Accepted += delegate
						{
							this.sc.gorelevel = 1;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						messageBoxScreen4.Cancelled += delegate
						{
							this.sc.gorelevel = 0;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						this.sc.AddScreen(messageBoxScreen4, null);
					}
					if (!this.sc.star2)
					{
						MessageBoxScreen2 messageBoxScreen5 = new MessageBoxScreen2("SECRET#2  LOCKED", 0);
						messageBoxScreen5.Accepted += delegate
						{
							this.delayinput = true;
						};
						messageBoxScreen5.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen5, null);
					}
				}
				if (this.whichbutton == 14)
				{
					this.sc.harp2.Play(this.sc.ev * 0.6f, 0.2f, 0f);
					if (this.sc.star3)
					{
						MessageBoxScreen2 messageBoxScreen6 = new MessageBoxScreen2("SECRET ABILITY\nEnable Double Ammo?\n", 1);
						messageBoxScreen6.Accepted += delegate
						{
							this.sc.doubleAmmo = true;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						messageBoxScreen6.Cancelled += delegate
						{
							this.sc.doubleAmmo = false;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						this.sc.AddScreen(messageBoxScreen6, null);
					}
					if (!this.sc.star3)
					{
						MessageBoxScreen2 messageBoxScreen7 = new MessageBoxScreen2("SECRET#3  LOCKED", 0);
						messageBoxScreen7.Accepted += delegate
						{
							this.delayinput = true;
						};
						messageBoxScreen7.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen7, null);
					}
				}
			}
			if (this.sc.developer && this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F1) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F1))
			{
				this.sc.resetGame();
				this.sc.harp2.Play(this.sc.ev, 0f, 0f);
				for (int i = 0; i < this.sc.days.Length; i++)
				{
					this.sc.days[i] = 0;
					this.sc.gdata.days[i] = 0;
				}
				int num = 50;
				for (int j = 0; j < num; j++)
				{
					this.sc.days[j] = 1;
				}
				for (int k = 0; k < this.sc.hats.Length; k++)
				{
					this.sc.hats[k] = 0;
				}
				this.sc.hats[1] = 0;
				this.sc.hats[2] = 0;
				this.sc.hats[3] = 0;
				this.sc.hats[4] = 0;
				this.sc.hats[5] = 0;
				this.sc.hats[6] = 0;
				this.sc.hats[7] = 0;
				this.sc.hats[8] = 0;
				this.sc.hats[9] = 0;
				this.sc.hats[10] = 0;
				this.sc.hats[11] = 0;
				this.sc.hats[12] = 0;
				this.sc.hats[13] = 0;
				this.sc.hats[14] = 0;
				this.sc.hats[15] = 0;
				this.sc.man1 = true;
				this.sc.man2 = true;
				this.sc.man3 = true;
				this.sc.man4 = true;
				this.sc.FarmerUnlocked = true;
				this.sc.resetTunnels();
				this.sc.goggles = 1;
				this.sc.exitkey[0] = 0;
				this.sc.exitkey[1] = 0;
				this.sc.exitkey[2] = 0;
				this.sc.exitkey[3] = 0;
				this.sc.exitkey[4] = 0;
				this.sc.exitkey[5] = 0;
				this.sc.flashlight1 = 0;
				this.sc.flashlight2 = 0;
				this.sc.flashlight3 = 0;
				this.sc.SaveGame();
			}
			if (this.sc.developer && this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F3) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F3))
			{
				MessageBoxScreen2 messageBoxScreen8 = new MessageBoxScreen2("Give all Achievements\nAre you sure?\n", 1);
				messageBoxScreen8.Accepted += delegate
				{
					this.sc.trophy.gimme();
					this.delayinput = true;
				};
				messageBoxScreen8.Cancelled += delegate
				{
					this.delayinput = true;
				};
				this.sc.AddScreen(messageBoxScreen8, null);
			}
			if (this.sc.developer && this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F4) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F4))
			{
				MessageBoxScreen2 messageBoxScreen9 = new MessageBoxScreen2("Delete all Achievements\nAre you sure?\n", 1);
				messageBoxScreen9.Accepted += delegate
				{
					this.sc.man4 = false;
					this.sc.star1 = false;
					this.sc.star2 = false;
					this.sc.star3 = false;
					this.sc.trophy.resetStats();
					this.sc.trophy.init();
					this.sc.fanfare.Play(this.sc.ev, 0f, 0f);
					this.delayinput = true;
				};
				messageBoxScreen9.Cancelled += delegate
				{
					this.delayinput = true;
				};
				this.sc.AddScreen(messageBoxScreen9, null);
			}
			if (this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F5) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F5))
			{
				MessageBoxScreen2 messageBoxScreen10 = new MessageBoxScreen2("Delete All Preferences\nAre you sure?\n", 1);
				messageBoxScreen10.Accepted += delegate
				{
					using (IsolatedStorageFile store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
					{
						string text = "saveprefs";
						if (store.FileExists(text))
						{
							store.DeleteFile(text);
						}
					}
					string text2 = "SAVES//saveprefs";
					if (!File.Exists(text2))
					{
						File.Delete(text2);
					}
					this.sc.fanfare.Play(this.sc.ev, 0f, 0f);
					this.delayinput = true;
					this.sc.resetPreferences();
					this.sc.SavePrefs();
				};
				messageBoxScreen10.Cancelled += delegate
				{
					this.delayinput = true;
				};
				this.sc.AddScreen(messageBoxScreen10, null);
			}
			if (this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F6) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F6))
			{
				MessageBoxScreen2 messageBoxScreen11 = new MessageBoxScreen2("Reset Blood and Bacon\nStart Game from Day 1\nAre you sure?\n", 1);
				messageBoxScreen11.Accepted += delegate
				{
					using (IsolatedStorageFile store2 = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
					{
						string text3 = "savegame";
						if (store2.FileExists(text3))
						{
							store2.DeleteFile(text3);
						}
					}
					string text4 = "SAVES//savegame";
					if (!File.Exists(text4))
					{
						File.Delete(text4);
					}
					this.sc.resetGame();
					this.sc.man1 = false;
					this.sc.man2 = false;
					this.sc.man3 = false;
					this.sc.man4 = false;
					this.sc.SaveGame();
					this.sc.fanfare.Play(this.sc.ev, 0f, 0f);
					this.delayinput = true;
				};
				messageBoxScreen11.Cancelled += delegate
				{
					this.delayinput = true;
				};
				this.sc.AddScreen(messageBoxScreen11, null);
			}
			if (this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F7) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F7))
			{
				MessageBoxScreen2 messageBoxScreen12 = new MessageBoxScreen2("Reset Tunnels Update\nAre you sure?\n", 1);
				messageBoxScreen12.Accepted += delegate
				{
					this.sc.resetTunnels();
					this.sc.fanfare.Play(this.sc.ev, 0.3f, 0f);
				};
				messageBoxScreen12.Cancelled += delegate
				{
					this.delayinput = true;
				};
				this.sc.AddScreen(messageBoxScreen12, null);
			}
			if (this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F8) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F8))
			{
				MessageBoxScreen2 messageBoxScreen13 = new MessageBoxScreen2("Delete Space Mission Data\nAre you sure?\n", 1);
				messageBoxScreen13.Accepted += delegate
				{
					string text5 = "SAVES//spaceprefs";
					if (File.Exists(text5))
					{
						try
						{
							File.Delete(text5);
							this.sc.fanfare.Play(this.sc.ev, 0f, 0f);
						}
						catch
						{
							this.sc.abort.Play(this.sc.ev, 0f, 0f);
						}
					}
					this.delayinput = true;
					this.sc.resetSpacePrefs();
					this.sc.SaveSpacePrefs();
					this.sc.man4 = false;
				};
				messageBoxScreen13.Cancelled += delegate
				{
					this.delayinput = true;
				};
				this.sc.AddScreen(messageBoxScreen13, null);
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x001220AC File Offset: 0x001202AC
		private void handleMore(ref InputState input)
		{
			if (this.moreTimer % 450 == 0)
			{
				this.sc.workshop.getSubscribedItems();
				this.sc.workshop.forceDownloadFileId();
			}
			if (this.moreTimer > 300 && !this.b.more && SteamAPI.IsSteamRunning())
			{
				this.sc.trophy.checkAll();
				this.b.more = true;
			}
			if (this.sc.trophy.allcomplete)
			{
				if (!this.sc.allachieves)
				{
					this.sc.achievepop.Play(this.sc.ev, 0f, -1f);
				}
				this.sc.allachieves = true;
			}
			else
			{
				this.sc.allachieves = false;
			}
			if (this.mouseback || input.IsNewButtonPress(Buttons.Back, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.whichbutton = 0;
				this.menuState = MainMenu.states.main;
				this.b.more = false;
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.delayinput = true;
				return;
			}
			this.selectOneExclusive(0, 3, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 4)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.main;
					this.b.more = false;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
					return;
				}
				if (this.whichbutton == 0)
				{
					if (SteamAPI.IsSteamRunning())
					{
						this.sc.workshop.getSubscribedItems();
						this.sc.workshop.forceDownloadFileId();
						if (this.sc.workshop.weSubscribed)
						{
							this.whichbutton = -1;
							this.menuState = MainMenu.states.diary;
							this.b.more = false;
							this.menuSelect = 1;
							this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
						}
						else
						{
							this.sc.cancel.Play(this.sc.ev, 0.5f, 0f);
							MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("Subscribe to the\nDearDiary Workshop", 0);
							messageBoxScreen.Accepted += delegate
							{
								this.delayinput = true;
							};
							messageBoxScreen.Cancelled += delegate
							{
								this.delayinput = true;
							};
							this.sc.AddScreen(messageBoxScreen, null);
							this.delayinput = true;
							this.sc.workshopNum = 0;
							this.sc.SavePrefs();
						}
					}
					else
					{
						this.sc.cancel.Play(this.sc.ev, (float)this.rr.Next(-60, 60) / 100f, 0f);
						MessageBoxScreen2 messageBoxScreen2 = new MessageBoxScreen2("Steam is currently\noffline, try later.", 0);
						messageBoxScreen2.Accepted += delegate
						{
							this.delayinput = true;
						};
						messageBoxScreen2.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen2, null);
						this.delayinput = true;
						this.sc.workshopNum = 0;
						this.sc.SavePrefs();
					}
				}
				if (this.whichbutton == 1)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.workshop;
					this.b.more = false;
					this.subRow = 0;
					this.workshopChosen = 1;
					this.drawsubs = false;
					this.drawcustom = false;
					this.sc.hammer1.Play(this.sc.ev, 0f, 0f);
					this.sc.loadCrosshairC();
					this.delayinput = true;
				}
				if (this.whichbutton == 2)
				{
					this.sc.harp2.Play(this.sc.ev * 0.6f, -0.2f, 0f);
					MessageBoxScreen2 messageBoxScreen3 = new MessageBoxScreen2("Name a Famous Painter", 4);
					messageBoxScreen3.Approved += delegate
					{
						this.sc.SavePrefs();
						this.whichbutton = 0;
						this.menuState = MainMenu.states.multi3player;
						this.b.main = false;
						this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
						this.delayinput = true;
					};
					messageBoxScreen3.Failed += delegate
					{
						this.sc.pigDie[0].Play(this.sc.ev, 0f, 0f);
						this.delayinput = true;
					};
					messageBoxScreen3.Cancelled += delegate
					{
						this.delayinput = true;
					};
					this.sc.AddScreen(messageBoxScreen3, null);
				}
				if (this.whichbutton == 3)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.videos;
					this.b.more = false;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
				}
				if (this.whichbutton == 12)
				{
					this.sc.harp2.Play(this.sc.ev * 0.6f, 0.2f, 0f);
					if (this.sc.star1)
					{
						MessageBoxScreen2 messageBoxScreen4 = new MessageBoxScreen2("SECRET ABILITY\nEnable Faster Grenades?\n", 1);
						messageBoxScreen4.Accepted += delegate
						{
							this.sc.fastnades = true;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						messageBoxScreen4.Cancelled += delegate
						{
							this.sc.fastnades = false;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						this.sc.AddScreen(messageBoxScreen4, null);
					}
					if (!this.sc.star1)
					{
						MessageBoxScreen2 messageBoxScreen5 = new MessageBoxScreen2("SECRET#1  LOCKED", 0);
						messageBoxScreen5.Accepted += delegate
						{
							this.delayinput = true;
						};
						messageBoxScreen5.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen5, null);
					}
				}
				if (this.whichbutton == 13)
				{
					this.sc.harp2.Play(this.sc.ev * 0.6f, 0.2f, 0f);
					if (this.sc.star2)
					{
						MessageBoxScreen2 messageBoxScreen6 = new MessageBoxScreen2("SECRET ABILITY\nEnable Extra Gore?\nchatbox :gore\n", 1);
						messageBoxScreen6.Accepted += delegate
						{
							this.sc.gorelevel = 1;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						messageBoxScreen6.Cancelled += delegate
						{
							this.sc.gorelevel = 0;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						this.sc.AddScreen(messageBoxScreen6, null);
					}
					if (!this.sc.star2)
					{
						MessageBoxScreen2 messageBoxScreen7 = new MessageBoxScreen2("SECRET#2  LOCKED", 0);
						messageBoxScreen7.Accepted += delegate
						{
							this.delayinput = true;
						};
						messageBoxScreen7.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen7, null);
					}
				}
				if (this.whichbutton == 14)
				{
					this.sc.harp2.Play(this.sc.ev * 0.6f, 0.2f, 0f);
					if (this.sc.star3)
					{
						MessageBoxScreen2 messageBoxScreen8 = new MessageBoxScreen2("SECRET ABILITY\nEnable Double Ammo?\n", 1);
						messageBoxScreen8.Accepted += delegate
						{
							this.sc.doubleAmmo = true;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						messageBoxScreen8.Cancelled += delegate
						{
							this.sc.doubleAmmo = false;
							this.delayinput = true;
							this.sc.SavePrefs();
						};
						this.sc.AddScreen(messageBoxScreen8, null);
					}
					if (!this.sc.star3)
					{
						MessageBoxScreen2 messageBoxScreen9 = new MessageBoxScreen2("SECRET#3  LOCKED", 0);
						messageBoxScreen9.Accepted += delegate
						{
							this.delayinput = true;
						};
						messageBoxScreen9.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen9, null);
					}
				}
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00122ABC File Offset: 0x00120CBC
		private void handleWorkshop(ref InputState input)
		{
			this.selectOneExclusive(0, 3, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (!this.sc.workshop.showPublishbox)
				{
					if (this.whichbutton == 0)
					{
						this.drawcustom = true;
						this.drawsubs = false;
						this.subRow = 0;
						this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
						if (this.sc.crosshairC.Count < 1)
						{
							this.techinfoBox("No Custom Crosshairs Found\nPlease Place PNG Files\nIn The Crosshairs Folder\n");
						}
					}
					if (this.whichbutton == 1)
					{
						this.drawsubs = true;
						this.drawcustom = false;
						this.subRow = 0;
						this.sc.workshop.getSubscribedItems();
						if (this.sc.workshop.totalSubscriptions <= 0U)
						{
							MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("You Have No Subscriptions\nPlease Visit The Workshop\nAnd Pick A Few...\n", 18);
							messageBoxScreen.Accepted += delegate
							{
								string text3 = "steam://url/SteamWorkshopPage/" + this.sc.workshop.myappID;
								this.sc.workshop.openWEB(text3);
								this.delayinput = true;
							};
							messageBoxScreen.Cancelled += delegate
							{
								this.delayinput = true;
							};
							this.sc.AddScreen(messageBoxScreen, null);
						}
						if (this.sc.workshop.totalSubscriptions > 0U)
						{
							this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
							this.sc.workshop.getSubbedTextures();
						}
					}
					if (this.whichbutton == 2)
					{
						if (this.sc.workshop.status == "")
						{
							if (this.sc.crosshair1[this.workshopChosen].type == 1 && this.sc.crosshair1[this.workshopChosen].legal)
							{
								this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
								this.sc.workshop.showPublishbox = true;
								this.zoomX = 0;
								this.zoomY = 0;
							}
							else
							{
								if (this.sc.crosshair1[this.workshopChosen].legal)
								{
									this.techinfoBox("Please Select A Filled Loadout\n");
								}
								if (!this.sc.crosshair1[this.workshopChosen].legal)
								{
									this.techinfoBox("You are not allowed\nto republish this item\n");
								}
							}
						}
						if (this.sc.workshop.status == "FAIL" || this.sc.workshop.status == "DONE")
						{
							this.sc.workshop.status = "";
						}
					}
					if (this.whichbutton == 3)
					{
						this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
						this.sc.saveLoadOut();
						this.checkTrophyOnce = false;
						this.menuState = MainMenu.states.more;
						this.delayinput = true;
						this.whichbutton = 1;
					}
					if (this.whichbutton == 20)
					{
						this.workshopChosen = 1;
					}
					if (this.whichbutton == 21)
					{
						this.workshopChosen = 2;
					}
					if (this.whichbutton == 22)
					{
						this.workshopChosen = 3;
					}
					if (this.whichbutton == 28)
					{
						this.sc.tick.Play(this.sc.ev, 0f, 0f);
						this.subRow--;
						if (this.subRow <= 0)
						{
							this.subRow = 0;
						}
					}
					if (this.whichbutton == 29)
					{
						this.sc.tick.Play(this.sc.ev, 0f, 0f);
						this.subRow++;
						if (this.subRow > 3)
						{
							this.subRow = 3;
						}
					}
					if (this.whichbutton == 23)
					{
						int num = this.subRow * 5 + 1;
						if ((!this.drawcustom && !this.drawsubs) || (this.drawcustom && this.sc.crosshairC.Count < num) || (this.drawsubs && this.sc.workshop.crosshairS.Count < num))
						{
							this.techinfoBox("This Slot Is Empty\n");
						}
						else
						{
							this.sc.drip.Play(this.sc.ev, -0.5f, 0f);
							if (this.drawcustom)
							{
								this.sc.crosshair1[this.workshopChosen].texture = this.sc.crosshairC[num - 1];
								this.sc.crosshair1[this.workshopChosen].type = 1;
								this.sc.crosshair1[this.workshopChosen].legal = true;
							}
							if (this.drawsubs)
							{
								this.sc.crosshair1[this.workshopChosen].texture = this.sc.workshop.crosshairS[num - 1];
								this.sc.crosshair1[this.workshopChosen].type = 1;
								this.sc.crosshair1[this.workshopChosen].legal = false;
							}
						}
					}
					if (this.whichbutton == 24)
					{
						int num2 = this.subRow * 5 + 2;
						if ((!this.drawcustom && !this.drawsubs) || (this.drawcustom && this.sc.crosshairC.Count < num2) || (this.drawsubs && this.sc.workshop.crosshairS.Count < num2))
						{
							this.techinfoBox("This Slot Is Empty\n");
						}
						else
						{
							this.sc.drip.Play(this.sc.ev, -0.5f, 0f);
							if (this.drawcustom)
							{
								this.sc.crosshair1[this.workshopChosen].texture = this.sc.crosshairC[num2 - 1];
								this.sc.crosshair1[this.workshopChosen].type = 1;
								this.sc.crosshair1[this.workshopChosen].legal = true;
							}
							if (this.drawsubs)
							{
								this.sc.crosshair1[this.workshopChosen].texture = this.sc.workshop.crosshairS[num2 - 1];
								this.sc.crosshair1[this.workshopChosen].type = 1;
								this.sc.crosshair1[this.workshopChosen].legal = false;
							}
						}
					}
					if (this.whichbutton == 25)
					{
						int num3 = this.subRow * 5 + 3;
						if ((!this.drawcustom && !this.drawsubs) || (this.drawcustom && this.sc.crosshairC.Count < num3) || (this.drawsubs && this.sc.workshop.crosshairS.Count < num3))
						{
							this.techinfoBox("This Slot Is Empty\n");
						}
						else
						{
							this.sc.drip.Play(this.sc.ev, -0.5f, 0f);
							if (this.drawcustom)
							{
								this.sc.crosshair1[this.workshopChosen].texture = this.sc.crosshairC[num3 - 1];
								this.sc.crosshair1[this.workshopChosen].type = 1;
								this.sc.crosshair1[this.workshopChosen].legal = true;
							}
							if (this.drawsubs)
							{
								this.sc.crosshair1[this.workshopChosen].texture = this.sc.workshop.crosshairS[num3 - 1];
								this.sc.crosshair1[this.workshopChosen].type = 1;
								this.sc.crosshair1[this.workshopChosen].legal = false;
							}
						}
					}
					if (this.whichbutton == 26)
					{
						int num4 = this.subRow * 5 + 4;
						if ((!this.drawcustom && !this.drawsubs) || (this.drawcustom && this.sc.crosshairC.Count < num4) || (this.drawsubs && this.sc.workshop.crosshairS.Count < num4))
						{
							this.techinfoBox("This Slot Is Empty\n");
						}
						else
						{
							this.sc.drip.Play(this.sc.ev, -0.5f, 0f);
							if (this.drawcustom)
							{
								this.sc.crosshair1[this.workshopChosen].texture = this.sc.crosshairC[num4 - 1];
								this.sc.crosshair1[this.workshopChosen].type = 1;
								this.sc.crosshair1[this.workshopChosen].legal = true;
							}
							if (this.drawsubs)
							{
								this.sc.crosshair1[this.workshopChosen].texture = this.sc.workshop.crosshairS[num4 - 1];
								this.sc.crosshair1[this.workshopChosen].type = 1;
								this.sc.crosshair1[this.workshopChosen].legal = false;
							}
						}
					}
					if (this.whichbutton == 27)
					{
						int num5 = this.subRow * 5 + 5;
						if ((!this.drawcustom && !this.drawsubs) || (this.drawcustom && this.sc.crosshairC.Count < num5) || (this.drawsubs && this.sc.workshop.crosshairS.Count < num5))
						{
							this.techinfoBox("This Slot Is Empty\n");
						}
						else
						{
							this.sc.drip.Play(this.sc.ev, -0.5f, 0f);
							if (this.drawcustom)
							{
								this.sc.crosshair1[this.workshopChosen].texture = this.sc.crosshairC[num5 - 1];
								this.sc.crosshair1[this.workshopChosen].type = 1;
								this.sc.crosshair1[this.workshopChosen].legal = true;
							}
							if (this.drawsubs)
							{
								this.sc.crosshair1[this.workshopChosen].texture = this.sc.workshop.crosshairS[num5 - 1];
								this.sc.crosshair1[this.workshopChosen].type = 1;
								this.sc.crosshair1[this.workshopChosen].legal = false;
							}
						}
					}
					if (this.whichbutton == 30)
					{
						this.sc.crosshair1[1].type = 0;
					}
					if (this.whichbutton == 31)
					{
						this.sc.crosshair1[2].type = 0;
					}
					if (this.whichbutton == 32)
					{
						this.sc.crosshair1[3].type = 0;
					}
				}
				if (this.sc.workshop.showPublishbox)
				{
					if (this.whichbutton == 4)
					{
						MessageBoxScreen2 messageBoxScreen2 = new MessageBoxScreen2("Title", 14);
						messageBoxScreen2.Approved += delegate
						{
							this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
							this.delayinput = true;
						};
						messageBoxScreen2.Failed += delegate
						{
							this.sc.pigDie[1].Play(this.sc.ev, 0f, 0f);
							this.delayinput = true;
						};
						messageBoxScreen2.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen2, null);
					}
					if (this.whichbutton == 5)
					{
						MessageBoxScreen2 messageBoxScreen3 = new MessageBoxScreen2("Description", 15);
						messageBoxScreen3.Approved += delegate
						{
							this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
							this.delayinput = true;
						};
						messageBoxScreen3.Failed += delegate
						{
							this.sc.pigDie[1].Play(this.sc.ev, 0f, 0f);
							this.delayinput = true;
						};
						messageBoxScreen3.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen3, null);
					}
					if (this.whichbutton == 50)
					{
						MessageBoxScreen2 messageBoxScreen4 = new MessageBoxScreen2("Corner WaterMark", 17);
						messageBoxScreen4.Approved += delegate
						{
							this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
							this.delayinput = true;
						};
						messageBoxScreen4.Failed += delegate
						{
							this.delayinput = true;
						};
						messageBoxScreen4.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen4, null);
					}
					if (this.whichbutton == 6)
					{
						if (this.formDrawBG)
						{
							if (this.formBGColorindex == 0)
							{
								this.formDrawBG = false;
							}
						}
						else
						{
							this.formDrawBG = true;
						}
						this.formBGColorindex = 0;
					}
					if (this.whichbutton == 7)
					{
						if (this.formDrawBG)
						{
							if (this.formBGColorindex == 1)
							{
								this.formDrawBG = false;
							}
						}
						else
						{
							this.formDrawBG = true;
						}
						this.formBGColorindex = 1;
					}
					if (this.whichbutton == 8)
					{
						if (this.formDrawBG)
						{
							if (this.formBGColorindex == 2)
							{
								this.formDrawBG = false;
							}
						}
						else
						{
							this.formDrawBG = true;
						}
						this.formBGColorindex = 2;
					}
					if (this.whichbutton == 9)
					{
						if (this.formDrawBG)
						{
							if (this.formBGColorindex == 3)
							{
								this.formDrawBG = false;
							}
						}
						else
						{
							this.formDrawBG = true;
						}
						this.formBGColorindex = 3;
					}
					if (this.whichbutton == 10)
					{
						if (this.formDrawBG)
						{
							if (this.formBGColorindex == 4)
							{
								this.formDrawBG = false;
							}
						}
						else
						{
							this.formDrawBG = true;
						}
						this.formBGColorindex = 4;
					}
					if (this.whichbutton == 66)
					{
						if (this.formDrawBand)
						{
							if (this.formBandColorIndex == 0)
							{
								this.formDrawBand = false;
							}
						}
						else
						{
							this.formDrawBand = true;
						}
						this.formBandColorIndex = 0;
					}
					if (this.whichbutton == 67)
					{
						if (this.formDrawBand)
						{
							if (this.formBandColorIndex == 1)
							{
								this.formDrawBand = false;
							}
						}
						else
						{
							this.formDrawBand = true;
						}
						this.formBandColorIndex = 1;
					}
					if (this.whichbutton == 68)
					{
						if (this.formDrawBand)
						{
							if (this.formBandColorIndex == 2)
							{
								this.formDrawBand = false;
							}
						}
						else
						{
							this.formDrawBand = true;
						}
						this.formBandColorIndex = 2;
					}
					if (this.whichbutton == 69)
					{
						if (this.formDrawBand)
						{
							if (this.formBandColorIndex == 3)
							{
								this.formDrawBand = false;
							}
						}
						else
						{
							this.formDrawBand = true;
						}
						this.formBandColorIndex = 3;
					}
					if (this.whichbutton == 70)
					{
						if (this.formDrawBand)
						{
							if (this.formBandColorIndex == 4)
							{
								this.formDrawBand = false;
							}
						}
						else
						{
							this.formDrawBand = true;
						}
						this.formBandColorIndex = 4;
					}
					if (this.whichbutton >= 40 && this.whichbutton <= 48)
					{
						this.formtagIndex = this.whichbutton - 40;
						this.sc.workshop.formTAG = this.sc.workshop.availableTAGS[this.formtagIndex];
						this.sc.drip.Play(this.sc.ev, 0f, 0f);
					}
					if (this.whichbutton == 11)
					{
						this.sc.workshop.formVisibility = this.sc.workshop.seePublic;
						this.sc.drip.Play(this.sc.ev, 0f, 0f);
					}
					if (this.whichbutton == 12)
					{
						this.sc.workshop.formVisibility = this.sc.workshop.seePrivate;
						this.sc.drip.Play(this.sc.ev, 0f, 0f);
					}
					if (this.whichbutton == 13)
					{
						this.sc.workshop.formVisibility = this.sc.workshop.seeFriends;
						this.sc.drip.Play(this.sc.ev, 0f, 0f);
					}
					if (this.whichbutton == 15)
					{
						this.sc.workshop.showPublishbox = false;
						this.sc.cancel.Play(this.sc.ev, 0f, 0f);
					}
					if (this.whichbutton == 16)
					{
						this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
						if (this.sc.workshop.status == "")
						{
							string text = "UploadData";
							if (!Directory.Exists(text))
							{
								Directory.CreateDirectory(text);
							}
							string text2 = "UploadData\\dummyB.png";
							using (Stream stream = File.OpenWrite(text2))
							{
								this.sc.crosshair1[this.workshopChosen].texture.SaveAsPng(stream, this.sc.crosshair1[this.workshopChosen].texture.Width, this.sc.crosshair1[this.workshopChosen].texture.Height);
							}
							this.sc.crossIndex = this.workshopChosen;
							this.sc.formDrawBand = this.formDrawBand;
							this.sc.formDrawBG = this.formDrawBG;
							this.sc.formBandColorIndex = this.formBandColorIndex;
							this.sc.formBGColorIndex = this.formBGColorindex;
							this.sc.takepic = 5;
							this.delayinput = true;
						}
						else
						{
							this.sc.abort.Play(this.sc.ev, -0.3f, 0f);
							this.sc.workshop.showPublishbox = false;
						}
					}
				}
			}
			if (this.sc.workshop.showPublishbox && this.mousePressHold && this.workshopChosen != -1)
			{
				if (this.whichbutton == 80)
				{
					this.zoomX = 0;
					this.zoomY = 0;
				}
				if (this.whichbutton == 81)
				{
					this.zoomX -= 2;
				}
				if (this.whichbutton == 82)
				{
					this.zoomX += 2;
				}
				if (this.whichbutton == 83)
				{
					this.zoomY -= 2;
				}
				if (this.whichbutton == 84)
				{
					this.zoomY += 2;
				}
				if (this.whichbutton == 85)
				{
					this.zoomX -= 2;
					this.zoomY -= 2;
				}
				if (this.whichbutton == 86)
				{
					this.zoomX += 2;
					this.zoomY += 2;
				}
				if (this.whichbutton == 87)
				{
					this.zoomX += 2;
					this.zoomY += 2;
				}
				if (this.whichbutton == 88)
				{
					this.zoomX -= 2;
					this.zoomY -= 2;
				}
				if (this.zoomX < 0)
				{
					this.zoomX = 0;
				}
				if (this.zoomY < 0)
				{
					this.zoomY = 0;
				}
				if (this.zoomX > this.sc.crosshair1[this.workshopChosen].texture.Width / 2 - 10)
				{
					this.zoomX = this.sc.crosshair1[this.workshopChosen].texture.Width / 2 - 10;
				}
				if (this.zoomY > this.sc.crosshair1[this.workshopChosen].texture.Height / 2 - 10)
				{
					this.zoomY = this.sc.crosshair1[this.workshopChosen].texture.Height / 2 - 10;
				}
			}
			if (this.mouseback || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				if (this.sc.workshop.showPublishbox)
				{
					this.sc.workshop.showPublishbox = false;
					this.delayinput = true;
					this.sc.cancel.Play(this.sc.ev, 0f, 0f);
				}
				else
				{
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.sc.saveLoadOut();
					this.checkTrophyOnce = false;
					this.menuState = MainMenu.states.more;
					this.delayinput = true;
					this.whichbutton = 1;
				}
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0012409C File Offset: 0x0012229C
		private void handleCreditwall(ref InputState input)
		{
			if (this.mouseback || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.whichbutton = 0;
				this.menuState = MainMenu.states.multi2player;
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.delayinput = true;
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00124108 File Offset: 0x00122308
		private void handleAlphateam(ref InputState input)
		{
			if (this.mouseback || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.whichbutton = 0;
				this.menuState = MainMenu.states.multi4player;
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.delayinput = true;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00124250 File Offset: 0x00122450
		private void handlePlaygame(ref InputState input)
		{
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0 || (this.whichbutton == -1 && this.mousepress))
				{
					if (this.menuSelect == 4 && !this.sc.FarmerUnlocked)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 8 && !this.sc.man1)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 9 && !this.sc.man2)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 10 && !this.sc.man3)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 7 && !this.sc.man4)
					{
						this.sc.abort.Play(this.sc.ev, 0.4f, 0f);
					}
					else if (this.menuSelect == 6)
					{
						this.sc.maxDay();
						if (this.mousepress)
						{
							this.sc.usingMouse = true;
						}
						else
						{
							this.sc.usingMouse = false;
						}
						this.sc.playerindex = this.playerindex;
						if (!this.sc.trophy.spacedeath.trophywon || !this.sc.trophy.spaceevent.trophywon || !this.sc.trophy.walkinghere.trophywon)
						{
							MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("Optional Space Achievements\nUnlock All 3 Now?\n", 1);
							messageBoxScreen.Accepted += delegate
							{
								this.sc.fanfare.Play(this.sc.ev * 0.5f, 0f, 0f);
								this.sc.trophy.gimmeSpace();
								this.sc.trophy.checkAll();
							};
							messageBoxScreen.Cancelled += delegate
							{
								MessageBoxScreen2 messageBoxScreen3 = new MessageBoxScreen2("Visit The Farm in 3019\nPlay Unpopular Space Update?\n", 1);
								messageBoxScreen3.Accepted += delegate
								{
									this.CreateSpace(this.playerindex);
								};
								messageBoxScreen3.Cancelled += delegate
								{
									this.delayinput = true;
								};
								this.sc.AddScreen(messageBoxScreen3, null);
							};
							this.sc.AddScreen(messageBoxScreen, null);
						}
						else
						{
							MessageBoxScreen2 messageBoxScreen2 = new MessageBoxScreen2("Visit The Farm in 3019\nPlay Unpopular Space Update?\n", 1);
							messageBoxScreen2.Accepted += delegate
							{
								this.CreateSpace(this.playerindex);
							};
							messageBoxScreen2.Cancelled += delegate
							{
								this.delayinput = true;
							};
							this.sc.AddScreen(messageBoxScreen2, null);
						}
						if (!this.sc.man4)
						{
							this.sc.errorMessage = "astronaut unlocked !!!";
							this.sc.errorMessageTimer = 360;
							this.sc.fanfare.Play(this.sc.ev, 0f, 0f);
							this.sc.man4 = true;
							this.sc.SaveEquipables();
						}
					}
					else
					{
						if (this.mousepress)
						{
							this.sc.usingMouse = true;
						}
						else
						{
							this.sc.usingMouse = false;
						}
						this.sc.gamestart.Play(this.sc.ev, 0f, 0f);
						this.CreateSession(this.playerindex);
					}
				}
				if (this.whichbutton == 1)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.main;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
				}
			}
			if (this.mouseback || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.whichbutton = 0;
				this.menuState = MainMenu.states.main;
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.delayinput = true;
			}
			if (input.IsMenuUp(base.ControllingPlayer) && this.menuSelect != 6 && this.menuSelect != 7)
			{
				this.lastselect = this.itemSelect;
				if (this.itemSelect % this.menuArray.Length < 4)
				{
					this.itemSelect = 9;
				}
				else
				{
					this.itemSelect = 10;
				}
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				this.whichbutton = 0;
			}
			if (input.IsMenuDown(base.ControllingPlayer) && (this.menuSelect == 6 || this.menuSelect == 7))
			{
				this.itemSelect = this.lastselect;
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				this.whichbutton = 0;
			}
			if (input.IsMenuRight(base.ControllingPlayer) || input.IsNewButtonPress(Buttons.RightShoulder, base.ControllingPlayer, out this.playerindex))
			{
				this.itemSelect++;
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				this.whichbutton = 0;
			}
			if (input.IsMenuLeft(base.ControllingPlayer) || input.IsNewButtonPress(Buttons.LeftShoulder, base.ControllingPlayer, out this.playerindex))
			{
				this.itemSelect--;
				if (this.itemSelect < 0)
				{
					this.itemSelect = this.menuArray.Length;
				}
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, -0.1f, 0f);
				this.whichbutton = 0;
			}
			if (this.mousemoving)
			{
				int num = this.menuSelect;
				for (int i = 0; i <= 10; i++)
				{
					if (i <= 5)
					{
						Rectangle rectangle = new Rectangle((int)this.selectPoint[i].X, (int)this.selectPoint[i].Y, this.glow.Width, this.glow.Height);
						this.queryButton2(rectangle, i);
					}
					else
					{
						Rectangle rectangle2 = new Rectangle((int)this.selectPoint[i].X, (int)this.selectPoint[i].Y, 50, 50);
						this.queryButton2(rectangle2, i);
					}
				}
				if (num != this.menuSelect)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
			if (this.menuSelect == 0)
			{
				this.sc.gameNPC = 4;
			}
			if (this.menuSelect == 1)
			{
				this.sc.gameNPC = 3;
			}
			if (this.menuSelect == 2)
			{
				this.sc.gameNPC = 0;
			}
			if (this.menuSelect == 3)
			{
				this.sc.gameNPC = 1;
			}
			if (this.menuSelect == 4)
			{
				this.sc.gameNPC = 2;
			}
			if (this.menuSelect == 5)
			{
				this.sc.gameNPC = 5;
			}
			if (this.menuSelect == 7)
			{
				this.sc.gameNPC = 10;
			}
			if (this.menuSelect == 8)
			{
				this.sc.gameNPC = 7;
			}
			if (this.menuSelect == 9)
			{
				this.sc.gameNPC = 8;
			}
			if (this.menuSelect == 10)
			{
				this.sc.gameNPC = 9;
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00124C94 File Offset: 0x00122E94
		private void handleMultijoinStart(ref InputState input)
		{
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0 || (this.whichbutton == -1 && this.mousepress))
				{
					if (this.menuSelect == 4 && !this.sc.FarmerUnlocked)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 8 && !this.sc.man1)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 9 && !this.sc.man2)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 10 && !this.sc.man3)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 7 && !this.sc.man4)
					{
						this.sc.abort.Play(this.sc.ev, 0.4f, 0f);
					}
					else if (this.menuSelect == 6)
					{
						if (!this.sc.trophy.spacedeath.trophywon || !this.sc.trophy.spaceevent.trophywon || !this.sc.trophy.walkinghere.trophywon)
						{
							MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("Optional Space Achievements\nUnlock All 3 Now?\n", 1);
							messageBoxScreen.Accepted += delegate
							{
								this.sc.fanfare.Play(this.sc.ev * 0.5f, 0f, 0f);
								this.sc.trophy.gimmeSpace();
								this.sc.trophy.checkAll();
							};
							messageBoxScreen.Cancelled += delegate
							{
								this.delayinput = true;
								this.sc.abort.Play(this.sc.ev, -0.2f, 0f);
							};
							this.sc.AddScreen(messageBoxScreen, null);
						}
						this.sc.errorMessage = "astronaut unlocked !!!";
						this.sc.errorMessageTimer = 360;
						if (!this.sc.man4)
						{
							this.sc.fanfare.Play(this.sc.ev, 0f, 0f);
							this.sc.man4 = true;
							this.sc.SaveEquipables();
						}
					}
					else if (this.sc.lobby.joinedLobby.Count > 0)
					{
						if (this.sc.lobby.lobbyPassword != "")
						{
							PlayerIndex pp = this.playerindex;
							bool usingmouse = false;
							if (this.mousepress)
							{
								usingmouse = true;
							}
							MessageBoxScreen2 messageBoxScreen2 = new MessageBoxScreen2("Password Please", 6, this.sc.lobby.lobbyPassword);
							messageBoxScreen2.Approved += delegate
							{
								int num3 = this.sc.lobby.joinLobby(this.sc.lobby.joinedLobby[0]);
								if (num3 != 0 && num3 != 200 && num3 >= 1 && num3 <= 101)
								{
									if (usingmouse)
									{
										this.sc.usingMouse = true;
									}
									else
									{
										this.sc.usingMouse = false;
									}
									this.sc.gamestart.Play(this.sc.ev, 0f, 0f);
									this.JoinCoopSession(pp, num3);
									return;
								}
								this.sc.abort.Play(this.sc.ev, 0f, 0f);
							};
							messageBoxScreen2.Cancelled += delegate
							{
								this.sc.abort.Play(this.sc.ev, 0f, 0f);
							};
							messageBoxScreen2.Failed += delegate
							{
								this.sc.pigDie[0].Play(this.sc.ev, 0f, 0f);
							};
							this.sc.AddScreen(messageBoxScreen2, null);
						}
						else
						{
							int num = this.sc.lobby.joinLobby(this.sc.lobby.joinedLobby[0]);
							if (num != 0 && num != 200 && num >= 1 && num <= 101)
							{
								if (this.mousepress)
								{
									this.sc.usingMouse = true;
								}
								else
								{
									this.sc.usingMouse = false;
								}
								this.sc.gamestart.Play(this.sc.ev, 0f, 0f);
								this.JoinCoopSession(this.playerindex, num);
							}
							else
							{
								string text = "Cannot Join\nHost Started the Game";
								this.sc.abort.Play(this.sc.ev, 0f, 0f);
								if (this.sc.lobby.lobbyState == "0")
								{
									text = "Cannot Join\nHost is not Ready";
								}
								if (this.sc.lobby.lobbyState == "20")
								{
									text = "Cannot Join\nGame is in Progress";
								}
								if (this.sc.lobby.lobbyState == "6")
								{
									text = "Cannot Join\nlobby is Full";
								}
								MessageBoxScreen2 messageBoxScreen3 = new MessageBoxScreen2(text, 0);
								messageBoxScreen3.Accepted += delegate
								{
									this.delayinput = true;
								};
								messageBoxScreen3.Cancelled += delegate
								{
									this.delayinput = true;
								};
								this.sc.AddScreen(messageBoxScreen3, null);
							}
						}
					}
					else
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
				}
				if (this.whichbutton == 1)
				{
					this.sc.lobby.uncreateLobby();
					this.sc.lobby.leaveLobby();
					this.whichbutton = 0;
					if (this.jumptomain)
					{
						this.menuState = MainMenu.states.main;
					}
					else
					{
						this.menuState = MainMenu.states.cooplobby;
					}
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
				}
			}
			if (this.mouseback || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.sc.lobby.uncreateLobby();
				this.sc.lobby.leaveLobby();
				this.whichbutton = 0;
				if (this.jumptomain)
				{
					this.menuState = MainMenu.states.main;
				}
				else
				{
					this.menuState = MainMenu.states.cooplobby;
				}
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.delayinput = true;
			}
			if (input.IsMenuUp(base.ControllingPlayer) && this.menuSelect != 6 && this.menuSelect != 7)
			{
				this.lastselect = this.itemSelect;
				if (this.itemSelect % this.menuArray.Length < 4)
				{
					this.itemSelect = 9;
				}
				else
				{
					this.itemSelect = 10;
				}
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				this.whichbutton = 0;
			}
			if (input.IsMenuDown(base.ControllingPlayer) && (this.menuSelect == 6 || this.menuSelect == 7))
			{
				this.itemSelect = this.lastselect;
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				this.whichbutton = 0;
			}
			if (input.IsMenuRight(base.ControllingPlayer) || input.IsNewButtonPress(Buttons.RightShoulder, base.ControllingPlayer, out this.playerindex))
			{
				this.itemSelect++;
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				this.whichbutton = 0;
			}
			if (input.IsMenuLeft(base.ControllingPlayer) || input.IsNewButtonPress(Buttons.LeftShoulder, base.ControllingPlayer, out this.playerindex))
			{
				this.itemSelect--;
				if (this.itemSelect < 0)
				{
					this.itemSelect = this.menuArray.Length;
				}
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, -0.1f, 0f);
				this.whichbutton = 0;
			}
			if (this.mousemoving)
			{
				int num2 = this.menuSelect;
				for (int i = 0; i <= 10; i++)
				{
					if (i <= 5)
					{
						Rectangle rectangle = new Rectangle((int)this.selectPoint[i].X, (int)this.selectPoint[i].Y, this.glow.Width, this.glow.Height);
						this.queryButton2(rectangle, i);
					}
					else
					{
						Rectangle rectangle2 = new Rectangle((int)this.selectPoint[i].X, (int)this.selectPoint[i].Y, 50, 50);
						this.queryButton2(rectangle2, i);
					}
				}
				if (num2 != this.menuSelect)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
			if (this.menuSelect == 0)
			{
				this.sc.gameNPC = 4;
			}
			if (this.menuSelect == 1)
			{
				this.sc.gameNPC = 3;
			}
			if (this.menuSelect == 2)
			{
				this.sc.gameNPC = 0;
			}
			if (this.menuSelect == 3)
			{
				this.sc.gameNPC = 1;
			}
			if (this.menuSelect == 4)
			{
				this.sc.gameNPC = 2;
			}
			if (this.menuSelect == 5)
			{
				this.sc.gameNPC = 5;
			}
			if (this.menuSelect == 7)
			{
				this.sc.gameNPC = 10;
			}
			if (this.menuSelect == 8)
			{
				this.sc.gameNPC = 7;
			}
			if (this.menuSelect == 9)
			{
				this.sc.gameNPC = 8;
			}
			if (this.menuSelect == 10)
			{
				this.sc.gameNPC = 9;
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00125814 File Offset: 0x00123A14
		private void handleMultiHostStart(ref InputState input)
		{
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0 || (this.whichbutton == -1 && this.mousepress))
				{
					if (this.menuSelect == 4 && !this.sc.FarmerUnlocked)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 8 && !this.sc.man1)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 9 && !this.sc.man2)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 10 && !this.sc.man3)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 7 && !this.sc.man4)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					else if (this.menuSelect == 6)
					{
						if (!this.sc.trophy.spacedeath.trophywon || !this.sc.trophy.spaceevent.trophywon || !this.sc.trophy.walkinghere.trophywon)
						{
							MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("Optional Space Achievements\nUnlock All 3 Now?\n", 1);
							messageBoxScreen.Accepted += delegate
							{
								this.sc.fanfare.Play(this.sc.ev * 0.5f, 0f, 0f);
								this.sc.trophy.gimmeSpace();
								this.sc.trophy.checkAll();
							};
							messageBoxScreen.Cancelled += delegate
							{
								this.delayinput = true;
								this.sc.abort.Play(this.sc.ev, -0.2f, 0f);
							};
							this.sc.AddScreen(messageBoxScreen, null);
						}
						this.sc.errorMessage = "astronaut unlocked !!!";
						this.sc.errorMessageTimer = 360;
						if (!this.sc.man4)
						{
							this.sc.fanfare.Play(this.sc.ev, 0f, 0f);
							this.sc.man4 = true;
							this.sc.SaveEquipables();
						}
					}
					else if (this.menuSelect == 11)
					{
						MessageBoxScreen2 messageBoxScreen2 = new MessageBoxScreen2("Enter Password", 7);
						messageBoxScreen2.Approved += delegate
						{
							this.sc.harp2.Play(this.sc.ev, 0.2f, 0f);
							if (this.sc.lobby.createdLobby.Count > 0)
							{
								this.sc.lobby.setPassword(this.sc.lobby.createdLobby[0]);
							}
							this.delayinput = true;
						};
						messageBoxScreen2.Cancelled += delegate
						{
							this.delayinput = true;
						};
						this.sc.AddScreen(messageBoxScreen2, null);
					}
					else
					{
						if (this.mousepress)
						{
							this.sc.usingMouse = true;
						}
						else
						{
							this.sc.usingMouse = false;
						}
						this.sc.gamestart.Play(this.sc.ev, 0f, 0f);
						this.CreateCoopSession(this.playerindex);
					}
				}
				if (this.whichbutton == 1)
				{
					this.sc.lobby.uncreateLobby();
					this.sc.lobby.leaveLobby();
					this.whichbutton = 0;
					this.menuState = MainMenu.states.main;
					if (this.lobbynum == "2")
					{
						this.menuState = MainMenu.states.multi2player;
					}
					if (this.lobbynum == "4")
					{
						this.menuState = MainMenu.states.multi4player;
					}
					if (this.lobbynum == "3")
					{
						this.menuState = MainMenu.states.multi3player;
					}
					if (this.lobbynum == "6")
					{
						this.menuState = MainMenu.states.multi6player;
					}
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
				}
			}
			if (this.mouseback || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.sc.lobby.uncreateLobby();
				this.sc.lobby.leaveLobby();
				this.whichbutton = 0;
				this.menuState = MainMenu.states.main;
				if (this.lobbynum == "2")
				{
					this.menuState = MainMenu.states.multi2player;
				}
				if (this.lobbynum == "4")
				{
					this.menuState = MainMenu.states.multi4player;
				}
				if (this.lobbynum == "3")
				{
					this.menuState = MainMenu.states.multi3player;
				}
				if (this.lobbynum == "6")
				{
					this.menuState = MainMenu.states.multi6player;
				}
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.delayinput = true;
			}
			if (input.IsMenuUp(base.ControllingPlayer) && this.menuSelect != 6 && this.menuSelect != 7)
			{
				this.lastselect = this.itemSelect;
				if (this.itemSelect % this.menuArray.Length < 4)
				{
					this.itemSelect = 9;
				}
				else
				{
					this.itemSelect = 10;
				}
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				this.whichbutton = 0;
			}
			if (input.IsMenuDown(base.ControllingPlayer) && (this.menuSelect == 6 || this.menuSelect == 7))
			{
				this.itemSelect = this.lastselect;
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				this.whichbutton = 0;
			}
			if (input.IsMenuRight(base.ControllingPlayer) || input.IsNewButtonPress(Buttons.RightShoulder, base.ControllingPlayer, out this.playerindex))
			{
				this.itemSelect++;
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				this.whichbutton = 0;
			}
			if (input.IsMenuLeft(base.ControllingPlayer) || input.IsNewButtonPress(Buttons.LeftShoulder, base.ControllingPlayer, out this.playerindex))
			{
				this.itemSelect--;
				if (this.itemSelect < 0)
				{
					this.itemSelect = this.menuArray.Length;
				}
				this.menuSelect = this.menuArray[this.itemSelect % this.menuArray.Length];
				this.sc.tick.Play(this.sc.ev, -0.1f, 0f);
				this.whichbutton = 0;
			}
			if (this.mousemoving)
			{
				int num = this.menuSelect;
				for (int i = 0; i <= 11; i++)
				{
					if (i <= 5)
					{
						Rectangle rectangle = new Rectangle((int)this.selectPoint[i].X, (int)this.selectPoint[i].Y, this.glow.Width, this.glow.Height);
						this.queryButton2(rectangle, i);
					}
					else
					{
						Rectangle rectangle2 = new Rectangle((int)this.selectPoint[i].X, (int)this.selectPoint[i].Y, 50, 50);
						if (i == 11)
						{
							rectangle2.Width = 233;
							rectangle2.Height = 40;
						}
						this.queryButton2(rectangle2, i);
					}
				}
				if (num != this.menuSelect)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
			if (this.menuSelect == 0)
			{
				this.sc.gameNPC = 4;
			}
			if (this.menuSelect == 1)
			{
				this.sc.gameNPC = 3;
			}
			if (this.menuSelect == 2)
			{
				this.sc.gameNPC = 0;
			}
			if (this.menuSelect == 3)
			{
				this.sc.gameNPC = 1;
			}
			if (this.menuSelect == 4)
			{
				this.sc.gameNPC = 2;
			}
			if (this.menuSelect == 5)
			{
				this.sc.gameNPC = 5;
			}
			if (this.menuSelect == 7)
			{
				this.sc.gameNPC = 10;
			}
			if (this.menuSelect == 8)
			{
				this.sc.gameNPC = 7;
			}
			if (this.menuSelect == 9)
			{
				this.sc.gameNPC = 8;
			}
			if (this.menuSelect == 10)
			{
				this.sc.gameNPC = 9;
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00126170 File Offset: 0x00124370
		private void handleDisplay(ref InputState input)
		{
			if (this.mouseback || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.menuState = MainMenu.states.controls;
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.delayinput = true;
				this.showHelp = false;
				this.showGamepad = false;
				this.showInstruct = false;
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0012623C File Offset: 0x0012443C
		private void handleSettings(ref InputState input)
		{
			this.selectOneExclusive(0, 2, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.audio;
					this.lastState2 = MainMenu.states.settings;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
					this.slider1 = (int)Math.Round(Math.Sqrt((double)(this.sc.mv * 100f)));
					this.slider2 = (int)Math.Round(Math.Sqrt((double)(this.sc.ev * 100f)));
					this.slider3 = (int)Math.Round(Math.Sqrt((double)(this.sc.vv * 100f)));
					this.slider1 = (int)MathHelper.Clamp((float)this.slider1, 0f, 10f);
					this.slider2 = (int)MathHelper.Clamp((float)this.slider2, 0f, 10f);
					this.slider3 = (int)MathHelper.Clamp((float)this.slider3, 0f, 10f);
					this.sliderbox1 = (float)this.slider1 * 31.4f - 157f;
					this.sliderbox2 = (float)this.slider2 * 31.4f - 157f;
					this.sliderbox3 = (float)this.slider3 * 31.4f - 157f;
					this.myIndex = -1;
				}
				if (this.whichbutton == 1 && !this.sc.lockVideosetup)
				{
					this.whichbutton = 0;
					this.sc.setupnum = this.sc.resolution;
					this.sc.aa = this.sc.aliasing;
					this.menuState = MainMenu.states.graphics;
					this.lastState2 = MainMenu.states.settings;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
				}
				if (this.whichbutton == 2)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.controls;
					this.lastState2 = MainMenu.states.settings;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
				}
				if (this.whichbutton == 3)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.main;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
				}
				if (this.whichbutton == 4)
				{
					this.sc.pigSqueal[this.rr.Next(0, 4)].Play(this.sc.ev, (float)this.rr.Next(-80, 80) / 100f, (float)this.rr.Next(-80, 80) / 100f);
					this.pigcount++;
					this.pigcount %= 20;
					this.pigwalk++;
					if (this.pigwalk > 10)
					{
						this.pigwalk = 505;
						if (!this.sc.star1)
						{
							this.sc.harp2.Play(this.sc.ev, 0f, 0f);
							MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("Secret Star\n", 9);
							messageBoxScreen.Accepted += delegate
							{
								this.delayinput = true;
							};
							messageBoxScreen.Cancelled += delegate
							{
								this.delayinput = true;
							};
							this.sc.AddScreen(messageBoxScreen, null);
							this.delayinput = true;
						}
						this.sc.star1 = true;
						this.sc.SavePrefs();
					}
				}
			}
			this.backtomain(input, 2);
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00126818 File Offset: 0x00124A18
		private void handleCoopChoice(ref InputState input)
		{
			this.selectOneExclusive(0, 2, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0)
				{
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
					this.whichbutton = 0;
					MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("Enter Your Name", 4);
					messageBoxScreen.Approved += delegate
					{
						this.sc.SavePrefs();
						this.whichbutton = 0;
						this.menuState = MainMenu.states.multi2player;
						this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
						this.delayinput = true;
					};
					messageBoxScreen.Failed += delegate
					{
						this.sc.pigDie[0].Play(this.sc.ev, 0f, 0f);
						this.delayinput = true;
					};
					messageBoxScreen.Cancelled += delegate
					{
						this.delayinput = true;
					};
					this.sc.AddScreen(messageBoxScreen, null);
				}
				if (this.whichbutton == 1)
				{
					this.whichbutton = 0;
					this.sc.harp2.Play(this.sc.ev * 0.6f, -0.2f, 0f);
					MessageBoxScreen2 messageBoxScreen2 = new MessageBoxScreen2("Enter Your Name", 4);
					messageBoxScreen2.Approved += delegate
					{
						this.sc.SavePrefs();
						this.whichbutton = 0;
						this.menuState = MainMenu.states.multi4player;
						this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
						this.delayinput = true;
					};
					messageBoxScreen2.Failed += delegate
					{
						this.sc.pigDie[0].Play(this.sc.ev, 0f, 0f);
						this.delayinput = true;
					};
					messageBoxScreen2.Cancelled += delegate
					{
						this.delayinput = true;
					};
					this.sc.AddScreen(messageBoxScreen2, null);
				}
				if (this.whichbutton == 2)
				{
					this.whichbutton = 0;
					this.sc.harp2.Play(this.sc.ev * 0.6f, -0.2f, 0f);
					MessageBoxScreen2 messageBoxScreen3 = new MessageBoxScreen2("Enter Your Name", 4);
					messageBoxScreen3.Approved += delegate
					{
						this.sc.SavePrefs();
						this.whichbutton = 0;
						this.menuState = MainMenu.states.multi6player;
						this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
						this.delayinput = true;
					};
					messageBoxScreen3.Failed += delegate
					{
						this.sc.pigDie[0].Play(this.sc.ev, 0f, 0f);
						this.delayinput = true;
					};
					messageBoxScreen3.Cancelled += delegate
					{
						this.delayinput = true;
					};
					this.sc.AddScreen(messageBoxScreen3, null);
				}
				if (this.whichbutton == 3)
				{
					this.whichbutton = 0;
					this.menuState = MainMenu.states.main;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
				}
			}
			this.backtomain(input, 1);
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00126AE4 File Offset: 0x00124CE4
		private void handleMulti2player(ref InputState input)
		{
			this.selectOneExclusive(0, 2, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0)
				{
					this.whichbutton = 0;
					this.sc.lobby.password = "";
					this.lobbynum = "2";
					this.menuState = MainMenu.states.multiHostStart;
					this.sc.lobby.createLobby(2);
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
				}
				if (this.whichbutton == 1)
				{
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
					this.whichbutton = 0;
					this.menuState = MainMenu.states.cooplobby;
					this.whichKEY = 0;
					this.sc.lobby.lobbys.Clear();
					this.lobbynum = "2";
					this.sc.lobby.requestLobby("2");
				}
				if (this.whichbutton == 2)
				{
					this.whichbutton = 1;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.menuState = MainMenu.states.main;
				}
				if (this.whichbutton == 7)
				{
					this.sc.harp2.Play(this.sc.ev * 0.6f, 0.4f, 0f);
					this.whichbutton = 0;
					this.menuState = MainMenu.states.creditwall;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
				}
			}
			this.backtomain(input, 1);
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00126CF0 File Offset: 0x00124EF0
		private void handleMulti3player(ref InputState input)
		{
			this.selectOneExclusive(0, 2, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0)
				{
					this.whichbutton = 0;
					this.sc.lobby.password = "";
					this.lobbynum = "3";
					this.menuState = MainMenu.states.multiHostStart;
					this.sc.lobby.createLobby(3);
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
				}
				if (this.whichbutton == 1)
				{
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
					this.whichbutton = 0;
					this.menuState = MainMenu.states.cooplobby;
					this.whichKEY = 0;
					this.sc.lobby.lobbys.Clear();
					this.lobbynum = "3";
					this.sc.lobby.requestLobby("3");
				}
				if (this.whichbutton == 2)
				{
					this.whichbutton = 1;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.menuState = MainMenu.states.main;
				}
			}
			this.backtomain(input, 1);
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00126E8C File Offset: 0x0012508C
		private void handleMulti4player(ref InputState input)
		{
			this.selectOneExclusive(0, 2, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0)
				{
					this.whichbutton = 0;
					this.sc.lobby.password = "";
					this.lobbynum = "4";
					this.menuState = MainMenu.states.multiHostStart;
					this.sc.lobby.createLobby(4);
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
				}
				if (this.whichbutton == 1)
				{
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
					this.whichbutton = 0;
					this.menuState = MainMenu.states.cooplobby;
					this.whichKEY = 0;
					this.sc.lobby.lobbys.Clear();
					this.lobbynum = "4";
					this.sc.lobby.requestLobby("4");
				}
				if (this.whichbutton == 2)
				{
					this.whichbutton = 1;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.menuState = MainMenu.states.main;
				}
				if (this.whichbutton == 7)
				{
					if (this.sc.developer)
					{
						this.whichbutton = 0;
						this.sc.lobby.password = "";
						this.lobbynum = "4";
						this.menuState = MainMenu.states.multiHostStart;
						this.sc.lobby.createLobby(44);
					}
					else
					{
						this.sc.harp2.Play(this.sc.ev * 0.6f, 0.4f, 0f);
						this.whichbutton = 0;
						this.menuState = MainMenu.states.alphateam;
						this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
					}
				}
			}
			this.backtomain(input, 1);
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x001270EC File Offset: 0x001252EC
		private void handleMulti6player(ref InputState input)
		{
			this.selectOneExclusive(0, 2, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0)
				{
					this.whichbutton = 0;
					this.sc.lobby.password = "";
					this.lobbynum = "6";
					this.menuState = MainMenu.states.multiHostStart;
					this.sc.lobby.createLobby(6);
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
				}
				if (this.whichbutton == 1)
				{
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.2f, 0f);
					this.whichbutton = 0;
					this.menuState = MainMenu.states.cooplobby;
					this.whichKEY = 0;
					this.sc.lobby.lobbys.Clear();
					this.lobbynum = "6";
					this.sc.lobby.requestLobby("6");
				}
				if (this.whichbutton == 2)
				{
					this.whichbutton = 1;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.menuState = MainMenu.states.main;
				}
			}
			this.backtomain(input, 1);
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00127288 File Offset: 0x00125488
		private void handleControls(ref InputState input)
		{
			this.selectOneExclusive(0, 3, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0)
				{
					this.showHelp = true;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
					this.sc.trophy.win(this.sc.trophy.aptpupil);
					this.menuState = MainMenu.states.displaykeys;
					this.sc.LoadSavedKeys();
					this.thisKEY = 0;
					this.whichKEY = 0;
				}
				if (this.whichbutton == 1)
				{
					this.showInstruct = true;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
					this.sc.trophy.win(this.sc.trophy.aptpupil);
					this.menuState = MainMenu.states.display;
				}
				if (this.whichbutton == 2)
				{
					this.showGamepad = true;
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
					this.sc.trophy.win(this.sc.trophy.aptpupil);
					this.menuState = MainMenu.states.display;
				}
				if (this.whichbutton == 3)
				{
					this.whichbutton = 1;
					this.menuState = MainMenu.states.settings;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
				}
			}
			if (this.mouseback || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.menuState = this.lastState2;
				this.delayinput = true;
				this.whichbutton = 1;
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x001274F0 File Offset: 0x001256F0
		private void handleDisplaykeys(ref InputState input)
		{
			if (this.thisKEY != 0 && (this.mousepress || this.mouseback || this.mousemiddle || this.mousenum1 || this.mousenum2) && this.thisKEY == this.whichKEY)
			{
				Microsoft.Xna.Framework.Input.Keys keys = Microsoft.Xna.Framework.Input.Keys.VolumeUp;
				if (this.mousepress)
				{
					keys = Microsoft.Xna.Framework.Input.Keys.VolumeDown;
				}
				if (this.mouseback)
				{
					keys = Microsoft.Xna.Framework.Input.Keys.VolumeUp;
				}
				if (this.mousemiddle)
				{
					keys = Microsoft.Xna.Framework.Input.Keys.VolumeMute;
				}
				if (this.mousenum1)
				{
					keys = Microsoft.Xna.Framework.Input.Keys.Print;
				}
				if (this.mousenum2)
				{
					keys = Microsoft.Xna.Framework.Input.Keys.PrintScreen;
				}
				if (this.thisKEY == 2)
				{
					this.sc.lmb_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 3)
				{
					this.sc.rmb_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 9)
				{
					this.sc.space_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 5)
				{
					this.sc.w_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 6)
				{
					this.sc.s_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 10)
				{
					this.sc.leftshift_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 11)
				{
					this.sc.r_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 12)
				{
					this.sc.x_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 13)
				{
					this.sc.q_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 14)
				{
					this.sc.e_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 28)
				{
					this.sc.enter_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 27)
				{
					this.sc.t_key = keys;
					this.thisKEY = 0;
				}
				if (this.thisKEY == 29)
				{
					this.sc.plus_key = keys;
					this.thisKEY = 0;
				}
			}
			if (this.thisKEY != 0 && this.pressedKeys.Length > 0)
			{
				if (this.thisKEY == 1)
				{
					this.sc.escape_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 2)
				{
					this.sc.lmb_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 3)
				{
					this.sc.rmb_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 5)
				{
					this.sc.w_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 6)
				{
					this.sc.s_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 7)
				{
					this.sc.a_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 8)
				{
					this.sc.d_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 9)
				{
					this.sc.space_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 10)
				{
					this.sc.leftshift_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 11)
				{
					this.sc.r_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 12)
				{
					this.sc.x_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 13)
				{
					this.sc.q_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 14)
				{
					this.sc.e_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 15)
				{
					this.sc.f_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 16)
				{
					this.sc.one_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 17)
				{
					this.sc.two_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 18)
				{
					this.sc.three_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 19)
				{
					this.sc.four_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 20)
				{
					this.sc.f1_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 21)
				{
					this.sc.f2_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 22)
				{
					this.sc.f3_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 23)
				{
					this.sc.tab_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 24)
				{
					this.sc.up_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 25)
				{
					this.sc.down_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 26)
				{
					this.sc.left_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 27)
				{
					this.sc.t_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 28)
				{
					this.sc.enter_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
				if (this.thisKEY == 29)
				{
					this.sc.plus_key = this.pressedKeys[0];
					this.thisKEY = 0;
				}
			}
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichKEY == 4)
				{
					this.sc.abort.Play(this.sc.ev, 0f, 0f);
					this.thisKEY = 0;
				}
				if (this.whichKEY == 30)
				{
					this.sc.SaveSavedKeys();
					this.sc.getXkey();
					this.sc.harp2.Play(this.sc.ev * 0.4f, 0f, 0f);
					this.thisKEY = 0;
				}
				if (this.whichKEY == 33)
				{
					this.sc.tick.Play(this.sc.ev, 0f, 0f);
					this.thisKEY = 0;
					this.sc.rmb_key = Microsoft.Xna.Framework.Input.Keys.VolumeUp;
					this.sc.lmb_key = Microsoft.Xna.Framework.Input.Keys.VolumeDown;
					this.sc.mmb_key = Microsoft.Xna.Framework.Input.Keys.VolumeMute;
					this.sc.but1_key = Microsoft.Xna.Framework.Input.Keys.Print;
					this.sc.but2_key = Microsoft.Xna.Framework.Input.Keys.PrintScreen;
					this.sc.escape_key = Microsoft.Xna.Framework.Input.Keys.Escape;
					this.sc.w_key = Microsoft.Xna.Framework.Input.Keys.W;
					this.sc.a_key = Microsoft.Xna.Framework.Input.Keys.A;
					this.sc.s_key = Microsoft.Xna.Framework.Input.Keys.S;
					this.sc.d_key = Microsoft.Xna.Framework.Input.Keys.D;
					this.sc.f_key = Microsoft.Xna.Framework.Input.Keys.F;
					this.sc.q_key = Microsoft.Xna.Framework.Input.Keys.Q;
					this.sc.e_key = Microsoft.Xna.Framework.Input.Keys.E;
					this.sc.x_key = Microsoft.Xna.Framework.Input.Keys.X;
					this.sc.r_key = Microsoft.Xna.Framework.Input.Keys.R;
					this.sc.up_key = Microsoft.Xna.Framework.Input.Keys.Up;
					this.sc.down_key = Microsoft.Xna.Framework.Input.Keys.Down;
					this.sc.left_key = Microsoft.Xna.Framework.Input.Keys.Left;
					this.sc.space_key = Microsoft.Xna.Framework.Input.Keys.Space;
					this.sc.one_key = Microsoft.Xna.Framework.Input.Keys.D1;
					this.sc.two_key = Microsoft.Xna.Framework.Input.Keys.D2;
					this.sc.three_key = Microsoft.Xna.Framework.Input.Keys.D3;
					this.sc.four_key = Microsoft.Xna.Framework.Input.Keys.D4;
					this.sc.tab_key = Microsoft.Xna.Framework.Input.Keys.Tab;
					this.sc.f1_key = Microsoft.Xna.Framework.Input.Keys.F1;
					this.sc.f2_key = Microsoft.Xna.Framework.Input.Keys.F2;
					this.sc.f3_key = Microsoft.Xna.Framework.Input.Keys.F3;
					this.sc.leftshift_key = Microsoft.Xna.Framework.Input.Keys.LeftShift;
					this.sc.t_key = Microsoft.Xna.Framework.Input.Keys.T;
					this.sc.enter_key = Microsoft.Xna.Framework.Input.Keys.Enter;
					this.sc.plus_key = Microsoft.Xna.Framework.Input.Keys.OemPlus;
				}
				if (this.whichKEY == 36)
				{
					this.menuState = MainMenu.states.controls;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
					this.showHelp = false;
					this.whichbutton = 0;
				}
				if (this.whichKEY != 2 || this.whichKEY != 3 || this.whichKEY != 4 || this.whichKEY != 30 || this.whichKEY != 33 || this.whichKEY != 36)
				{
					if (this.thisKEY == this.whichKEY)
					{
						this.thisKEY = 0;
					}
					else
					{
						this.thisKEY = this.whichKEY;
					}
				}
				else
				{
					this.thisKEY = 0;
				}
			}
			if ((this.mouseback && this.whichKEY == 0) || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex))
			{
				this.menuState = MainMenu.states.controls;
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.delayinput = true;
				this.showHelp = false;
			}
			if (this.mouseback && this.thisKEY == 0)
			{
				this.whichKEY = 0;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00127EC4 File Offset: 0x001260C4
		private void handleAudio(ref InputState input)
		{
			if (this.whichbutton < 2)
			{
				this.selectOneExclusive(0, 1, input);
			}
			if (input.IsMenuUp(base.ControllingPlayer))
			{
				this.whichbutton--;
				if (this.whichbutton < 2)
				{
					this.whichbutton = 2;
				}
				this.sc.tick.Play(this.sc.ev, 0.2f, 0f);
			}
			if (input.IsMenuDown(base.ControllingPlayer))
			{
				this.whichbutton++;
				if (this.whichbutton < 2)
				{
					this.whichbutton = 2;
				}
				if (this.whichbutton > 4)
				{
					this.whichbutton = 4;
				}
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
			}
			if (input.IsMenuRight(base.ControllingPlayer) || input.IsMenuLeft(base.ControllingPlayer))
			{
				float num = 31.4f;
				if (input.IsMenuLeft(base.ControllingPlayer))
				{
					num = -31.4f;
				}
				if (this.whichbutton == 2)
				{
					this.sliderbox1 += num;
					this.sliderbox1 = MathHelper.Clamp(this.sliderbox1, -157f, 157f);
					if (this.slider1 != (int)((this.sliderbox1 + 157f) / 31.4f))
					{
						this.sc.tick.Play(0.8f, MathHelper.Clamp(-0.5f + (float)this.slider1 / 10f, -1f, 1f), 0f);
					}
					this.slider1 = (int)((this.sliderbox1 + 157f) / 31.4f);
					this.sc.mv = (float)(this.slider1 * this.slider1) / 100f;
					this.menuMusic.Volume = this.sc.mv;
				}
				if (this.whichbutton == 3)
				{
					this.sliderbox2 += num;
					this.sliderbox2 = MathHelper.Clamp(this.sliderbox2, -157f, 157f);
					if (this.slider2 != (int)((this.sliderbox2 + 157f) / 31.4f))
					{
						this.sc.tick.Play(0.8f, MathHelper.Clamp(-0.5f + (float)this.slider2 / 10f, -1f, 1f), 0f);
					}
					this.slider2 = (int)((this.sliderbox2 + 157f) / 31.4f);
					this.sc.ev = (float)(this.slider2 * this.slider2) / 100f;
				}
				if (this.whichbutton == 4)
				{
					this.sliderbox3 += num;
					this.sliderbox3 = MathHelper.Clamp(this.sliderbox3, -157f, 157f);
					if (this.slider3 != (int)((this.sliderbox3 + 157f) / 31.4f))
					{
						this.sc.tick.Play(0.8f, MathHelper.Clamp(-0.5f + (float)this.slider3 / 10f, -1f, 1f), 0f);
					}
					this.slider3 = (int)((this.sliderbox3 + 157f) / 31.4f);
					this.sc.vv = (float)(this.slider3 * this.slider3) / 100f;
				}
			}
			if (this.myIndex == 0)
			{
				if (this.mousepress)
				{
					this.origMouseX = this.adjustMouse().X;
					this.sliderbox1a = this.sliderbox1;
				}
				if (this.mousePressHold)
				{
					this.sliderbox1 = this.sliderbox1a + (this.adjustMouse().X - this.origMouseX);
					this.sliderbox1 = MathHelper.Clamp(this.sliderbox1, -157f, 157f);
					if (this.slider1 != (int)((this.sliderbox1 + 157f) / 31.4f))
					{
						this.sc.tick.Play(0.8f, MathHelper.Clamp(-0.5f + (float)this.slider1 / 10f, -1f, 1f), 0f);
					}
					this.slider1 = (int)((this.sliderbox1 + 157f) / 31.4f);
					this.sc.mv = (float)(this.slider1 * this.slider1) / 100f;
					this.menuMusic.Volume = this.sc.mv;
				}
			}
			if (this.myIndex == 1)
			{
				if (this.mousepress)
				{
					this.origMouseX = this.adjustMouse().X;
					this.sliderbox2a = this.sliderbox2;
				}
				if (this.mousePressHold)
				{
					this.sliderbox2 = this.sliderbox2a + (this.adjustMouse().X - this.origMouseX);
					this.sliderbox2 = MathHelper.Clamp(this.sliderbox2, -157f, 157f);
					if (this.slider2 != (int)((this.sliderbox2 + 157f) / 31.4f))
					{
						this.sc.tick.Play(0.8f, MathHelper.Clamp(-0.5f + (float)this.slider2 / 10f, -1f, 1f), 0f);
					}
					this.slider2 = (int)((this.sliderbox2 + 157f) / 31.4f);
					this.sc.ev = (float)(this.slider2 * this.slider2) / 100f;
				}
			}
			if (this.myIndex == 2)
			{
				if (this.mousepress)
				{
					this.origMouseX = this.adjustMouse().X;
					this.sliderbox3a = this.sliderbox3;
				}
				if (this.mousePressHold)
				{
					this.sliderbox3 = this.sliderbox3a + (this.adjustMouse().X - this.origMouseX);
					this.sliderbox3 = MathHelper.Clamp(this.sliderbox3, -157f, 157f);
					if (this.slider3 != (int)((this.sliderbox3 + 157f) / 31.4f))
					{
						this.sc.tick.Play(0.8f, MathHelper.Clamp(-0.5f + (float)this.slider3 / 10f, -1f, 1f), 0f);
					}
					this.slider3 = (int)((this.sliderbox3 + 157f) / 31.4f);
					this.sc.vv = (float)(this.slider3 * this.slider3) / 100f;
				}
			}
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0)
				{
					this.sc.SavePrefs();
					this.sc.harp2.Play(this.sc.ev, 0.1f, 0f);
					this.menuState = this.lastState2;
					this.delayinput = true;
				}
				if (this.whichbutton == 1)
				{
					this.whichbutton = 0;
					this.menuState = this.lastState2;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
				}
				if (this.whichbutton > 1)
				{
					this.whichbutton = 0;
					this.sc.switch2.Play(this.sc.ev, 0f, 0f);
				}
			}
			if (this.mouseback || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.menuState = this.lastState2;
				this.delayinput = true;
				this.whichbutton = 0;
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00128738 File Offset: 0x00126938
		private void handleVideos(ref InputState input)
		{
			this.selectOneExclusive(0, 2, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0)
				{
					this.menuState = MainMenu.states.credit;
					try
					{
						MediaScreen mediaScreen = new MediaScreen(1);
						mediaScreen.Accepted += delegate
						{
							this.menuState = MainMenu.states.videos;
							this.delayinput = true;
						};
						mediaScreen.Cancelled += delegate
						{
							this.menuState = MainMenu.states.videos;
							this.delayinput = true;
						};
						this.sc.AddScreen(mediaScreen, null);
					}
					catch
					{
						this.menuState = MainMenu.states.videos;
						this.delayinput = true;
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
						this.sc.trophy.win(this.sc.trophy.credits);
					}
				}
				if (this.whichbutton == 1)
				{
					this.menuState = MainMenu.states.trailer;
					try
					{
						MediaScreen mediaScreen2 = new MediaScreen(0);
						mediaScreen2.Accepted += delegate
						{
							this.menuState = MainMenu.states.videos;
							this.delayinput = true;
						};
						mediaScreen2.Cancelled += delegate
						{
							this.menuState = MainMenu.states.videos;
							this.delayinput = true;
						};
						this.sc.AddScreen(mediaScreen2, null);
					}
					catch
					{
						this.menuState = MainMenu.states.videos;
						this.delayinput = true;
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
				}
				if (this.whichbutton == 2)
				{
					this.whichbutton = 3;
					this.menuState = MainMenu.states.more;
					this.checkTrophyOnce = false;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
				}
			}
			if (this.mouseback || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.menuState = MainMenu.states.more;
				this.checkTrophyOnce = false;
				this.delayinput = true;
				this.whichbutton = 0;
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x001289E0 File Offset: 0x00126BE0
		private void handleGraphics(ref InputState input)
		{
			this.selectOneExclusive(0, 1, input);
			if (this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex))
			{
				if (this.whichbutton == 0)
				{
					this.sc.setResolution();
					this.sc.trophy.win(this.sc.trophy.graphics);
					this.delayinput = true;
				}
				if (this.whichbutton == 1)
				{
					if (this.sc.setgraphics)
					{
						this.forgotsave();
						this.whichbutton = 1;
					}
					else
					{
						this.menuState = this.lastState2;
						this.whichbutton = 1;
						this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
						this.delayinput = true;
					}
				}
			}
			bool flag = (this.mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && this.prevMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released) || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex);
			if (flag)
			{
				if (this.sc.setgraphics)
				{
					this.forgotsave();
					this.whichbutton = 1;
				}
				else
				{
					this.whichbutton = 1;
					this.menuState = this.lastState2;
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
				}
			}
			if (this.highlighttimer > 0)
			{
				this.highlighttimer--;
				if (this.highlighttimer == 0)
				{
					this.highlight = 0;
				}
			}
			bool flag2 = input.IsNewButtonPress(Buttons.DPadLeft, base.ControllingPlayer, out this.playerindex);
			bool flag3 = input.IsNewButtonPress(Buttons.DPadRight, base.ControllingPlayer, out this.playerindex);
			bool flag4 = input.IsNewButtonPress(Buttons.DPadUp, base.ControllingPlayer, out this.playerindex);
			bool flag5 = input.IsNewButtonPress(Buttons.DPadDown, base.ControllingPlayer, out this.playerindex);
			bool flag6 = this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.OemPlus) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.OemPlus);
			bool flag7 = this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.OemMinus) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.OemMinus);
			if (flag6 || (this.mousepress && this.whichbutton == 9))
			{
				if (flag6)
				{
					this.highlight = 9;
					this.highlighttimer = 5;
				}
				this.sc.aspectratio += 0.025f;
				if (this.sc.aspectratio > 3f)
				{
					this.sc.aspectratio = 3f;
				}
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
				this.sc.setResolution();
			}
			if (flag7 || (this.mousepress && this.whichbutton == 10))
			{
				if (flag7)
				{
					this.highlight = 10;
					this.highlighttimer = 5;
				}
				this.sc.aspectratio -= 0.025f;
				if (this.sc.aspectratio < 0.3f)
				{
					this.sc.aspectratio = 0.3f;
				}
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
				this.sc.setResolution();
			}
			if (flag2 || (this.mousepress && this.whichbutton == 7))
			{
				if (flag2)
				{
					this.highlight = 7;
					this.highlighttimer = 5;
				}
				this.sc.aa -= 2;
				if (this.sc.aa < 0)
				{
					this.sc.aa = 8;
				}
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
			}
			if (flag3 || (this.mousepress && this.whichbutton == 8))
			{
				if (flag3)
				{
					this.highlight = 8;
					this.highlighttimer = 5;
				}
				this.sc.aa += 2;
				if (this.sc.aa > 8)
				{
					this.sc.aa = 0;
				}
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
			}
			if (flag4 || (this.mousepress && this.whichbutton == 6))
			{
				if (flag4)
				{
					this.highlight = 6;
					this.highlighttimer = 5;
				}
				this.sc.setupnum++;
				if (this.sc.setupnum > this.sc.resnames.Count - 1)
				{
					this.sc.setupnum = 0;
				}
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
			}
			if (flag5 || (this.mousepress && this.whichbutton == 5))
			{
				if (flag5)
				{
					this.highlight = 5;
					this.highlighttimer = 5;
				}
				this.sc.setupnum--;
				if (this.sc.setupnum < 0)
				{
					this.sc.setupnum = this.sc.resnames.Count - 1;
				}
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
			}
			if (this.mousepress && this.whichbutton == 11)
			{
				this.sc.fullmode = !this.sc.fullmode;
				this.sc.setResolution();
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
			}
			if (this.mousepress && this.whichbutton == 12)
			{
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
				this.sc.border = !this.sc.border;
				if (!this.sc.fullmode)
				{
					if (this.sc.border)
					{
						this.sc.addBorder();
					}
					else
					{
						this.sc.removeBorder();
					}
				}
			}
			if (this.whichbutton != -1)
			{
				this.lastbutton = this.whichbutton;
			}
			if (this.mousemoving)
			{
				this.whichbutton = -1;
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0012906C File Offset: 0x0012726C
		private void handleCooplobby(ref InputState input)
		{
			if ((this.mousepress || input.IsMenuSelect(base.ControllingPlayer, out this.playerindex)) && this.whichKEY >= 0 && this.sc.lobby.lobbys.Count > 0 && this.whichKEY < this.sc.lobby.lobbys.Count)
			{
				int num = this.sc.lobby.joinLobby(this.sc.lobby.lobbys[this.whichKEY].id);
				if (num == 200)
				{
					this.sc.lobby.lobbyName = this.sc.lobby.lobbys[this.whichKEY].name;
					this.whichbutton = 0;
					this.jumptomain = false;
					this.menuState = MainMenu.states.multijoinStart;
					if (this.sc.lobby.joinedLobby.Count > 0)
					{
						this.sc.lobby.grabDayCharState(this.sc.lobby.joinedLobby[0]);
					}
					this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
				}
			}
			if (input.IsMenuDown(base.ControllingPlayer) && this.whichKEY + 3 <= 20)
			{
				this.whichKEY += 3;
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
			}
			if (input.IsMenuUp(base.ControllingPlayer) && this.whichKEY - 3 >= 0)
			{
				this.whichKEY -= 3;
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
			}
			if (input.IsMenuLeft(base.ControllingPlayer))
			{
				this.whichKEY--;
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
				if (this.whichKEY < 0)
				{
					this.whichKEY = 0;
				}
			}
			if (input.IsMenuRight(base.ControllingPlayer))
			{
				this.whichKEY++;
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
				if (this.whichKEY > 20)
				{
					this.whichKEY = 20;
				}
			}
			if (this.mouseback || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.sc.lobby.leaveLobby();
				this.sc.lobby.uncreateLobby();
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.menuState = MainMenu.states.main;
				if (this.lobbynum == "2")
				{
					this.menuState = MainMenu.states.multi2player;
				}
				if (this.lobbynum == "4")
				{
					this.menuState = MainMenu.states.multi4player;
				}
				if (this.lobbynum == "3")
				{
					this.menuState = MainMenu.states.multi3player;
				}
				if (this.lobbynum == "6")
				{
					this.menuState = MainMenu.states.multi6player;
				}
				this.delayinput = true;
				this.whichbutton = 1;
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00129414 File Offset: 0x00127614
		private void handleTrailer(ref InputState input)
		{
			if (this.mouseback || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.menuState = MainMenu.states.videos;
				this.delayinput = true;
				this.whichbutton = 1;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0012949C File Offset: 0x0012769C
		private void handleCredit(ref InputState input)
		{
			if (this.mouseback || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerindex) || input.IsMenuCancel(base.ControllingPlayer, out this.playerindex))
			{
				this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
				this.menuState = MainMenu.states.videos;
				this.delayinput = true;
				this.whichbutton = 0;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00129654 File Offset: 0x00127854
		public override void HandleInput(InputState input)
		{
			if (!this.sc.audioLoaded)
			{
				return;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (base.ControllingPlayer != null)
			{
				this.playerindex = base.ControllingPlayer.Value;
				this.gamePadState = input.CurrentGamePadStates[(int)this.playerindex];
				this.prevstate = input.LastGamePadStates[(int)this.playerindex];
				GamePad.SetVibration(this.playerindex, 0f, 0f);
			}
			this.prevKeys = input.lastKeyState;
			this.keyState = input.currentKeyState;
			this.prevMouse = this.mouseState;
			this.mouseState = Mouse.GetState();
			this.pressedKeys = this.keyState.GetPressedKeys();
			if (this.delayinput)
			{
				this.prevMouse = this.mouseState;
				this.prevKeys = this.keyState;
				this.prevstate = this.gamePadState;
			}
			if (this.titlecounter <= 0f && !this.delayinput)
			{
				if (this.mouseState.X == (int)this.sc.mymouse.X && this.mouseState.Y == (int)this.sc.mymouse.Y)
				{
					this.mousemoving = false;
					this.sc.mousefade--;
					if (this.sc.mousefade == 0)
					{
						this.sc.Game.IsMouseVisible = false;
					}
				}
				else
				{
					this.mousemoving = true;
					this.sc.mousefade = 210;
					this.sc.Game.IsMouseVisible = true;
					if (this.sc.justsetgraphics)
					{
						this.sc.justsetgraphics = false;
						this.sc.mousefade = 0;
						this.sc.Game.IsMouseVisible = false;
						this.mousemoving = false;
						this.whichbutton = 0;
					}
				}
			}
			else
			{
				this.sc.Game.IsMouseVisible = false;
			}
			this.sc.mymouse.X = (float)this.mouseState.X;
			this.sc.mymouse.Y = (float)this.mouseState.Y;
			this.mouseback = this.mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && this.prevMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released;
			this.mousepress = (this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Enter)) || (this.mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && this.prevMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released);
			this.mousePressHold = this.mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
			this.mousemiddle = this.mouseState.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && this.prevMouse.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Released;
			this.mousenum1 = this.mouseState.XButton1 == Microsoft.Xna.Framework.Input.ButtonState.Pressed && this.prevMouse.XButton1 == Microsoft.Xna.Framework.Input.ButtonState.Released;
			this.mousenum2 = this.mouseState.XButton2 == Microsoft.Xna.Framework.Input.ButtonState.Pressed && this.prevMouse.XButton2 == Microsoft.Xna.Framework.Input.ButtonState.Released;
			if (this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F10) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F10))
			{
				this.sc.fullmode = !this.sc.fullmode;
				this.sc.setResolution();
			}
			if (this.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F9) && this.prevKeys.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F9))
			{
				this.sc.border = !this.sc.border;
				if (!this.sc.fullmode)
				{
					if (this.sc.border)
					{
						this.sc.addBorder();
					}
					else
					{
						this.sc.removeBorder();
					}
				}
			}
			if (this.titlecounter <= 0f)
			{
				if (this.sc.lobby.inviteRequest)
				{
					this.sc.harp2.Play(this.sc.ev, 0f, 0f);
					string text = "Enter Your Name?\n";
					try
					{
						text = this.sc.lobby.inviteName + " Invites You\nEnter Your Name?";
					}
					catch
					{
					}
					MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2(text, 4);
					messageBoxScreen.Approved += delegate
					{
						this.sc.SavePrefs();
						this.sc.lobby.uncreateLobby();
						this.sc.lobby.leaveLobby();
						int num = this.sc.lobby.joinLobby(this.sc.lobby.inviteLobbyID);
						if (num == 200)
						{
							this.sc.lobby.lobbyName = this.sc.lobby.inviteName;
							this.whichbutton = 0;
							this.jumptomain = true;
							this.menuState = MainMenu.states.multijoinStart;
							if (this.sc.lobby.joinedLobby.Count > 0)
							{
								this.sc.lobby.grabDayCharState(this.sc.lobby.joinedLobby[0]);
							}
							this.sc.switch2.Play(this.sc.ev * 0.2f, 0.1f, 0f);
						}
						this.sc.lobby.inviteRequest = false;
					};
					messageBoxScreen.Cancelled += delegate
					{
						this.delayinput = true;
						this.sc.lobby.inviteRequest = false;
					};
					this.sc.AddScreen(messageBoxScreen, null);
				}
				if (this.menuState == MainMenu.states.main)
				{
					this.handleMain(ref input);
				}
				else if (this.menuState == MainMenu.states.creditwall)
				{
					this.handleCreditwall(ref input);
				}
				else if (this.menuState == MainMenu.states.alphateam)
				{
					this.handleAlphateam(ref input);
				}
				else if (this.menuState == MainMenu.states.playgame)
				{
					this.handlePlaygame(ref input);
				}
				else if (this.menuState == MainMenu.states.multijoinStart)
				{
					this.handleMultijoinStart(ref input);
				}
				else if (this.menuState == MainMenu.states.multiHostStart)
				{
					this.handleMultiHostStart(ref input);
				}
				else if (this.menuState == MainMenu.states.display)
				{
					this.handleDisplay(ref input);
				}
				else if (this.menuState == MainMenu.states.settings)
				{
					this.handleSettings(ref input);
				}
				else if (this.menuState == MainMenu.states.coopChoice)
				{
					this.handleCoopChoice(ref input);
				}
				else if (this.menuState == MainMenu.states.multi2player)
				{
					this.handleMulti2player(ref input);
				}
				else if (this.menuState == MainMenu.states.multi4player)
				{
					this.handleMulti4player(ref input);
				}
				else if (this.menuState == MainMenu.states.multi3player)
				{
					this.handleMulti3player(ref input);
				}
				else if (this.menuState == MainMenu.states.multi6player)
				{
					this.handleMulti6player(ref input);
				}
				else if (this.menuState == MainMenu.states.workshop)
				{
					this.handleWorkshop(ref input);
				}
				else if (this.menuState == MainMenu.states.controls)
				{
					this.handleControls(ref input);
				}
				else if (this.menuState == MainMenu.states.displaykeys)
				{
					this.handleDisplaykeys(ref input);
				}
				else if (this.menuState == MainMenu.states.audio)
				{
					this.handleAudio(ref input);
				}
				else if (this.menuState == MainMenu.states.videos)
				{
					this.handleVideos(ref input);
				}
				else if (this.menuState == MainMenu.states.graphics)
				{
					this.handleGraphics(ref input);
				}
				else if (this.menuState == MainMenu.states.cooplobby)
				{
					this.handleCooplobby(ref input);
				}
				else if (this.menuState == MainMenu.states.trailer)
				{
					this.handleTrailer(ref input);
				}
				else if (this.menuState == MainMenu.states.credit)
				{
					this.handleCredit(ref input);
				}
				else if (this.menuState == MainMenu.states.diary)
				{
					this.handleDiary(ref input);
				}
				else if (this.menuState == MainMenu.states.more)
				{
					this.handleMore(ref input);
				}
			}
			this.delayinput = false;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00129D38 File Offset: 0x00127F38
		public void techinfoBox(string ss)
		{
			MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2(ss, 16);
			messageBoxScreen.Accepted += delegate
			{
				this.delayinput = true;
			};
			messageBoxScreen.Cancelled += delegate
			{
				this.delayinput = true;
			};
			this.sc.AddScreen(messageBoxScreen, null);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00129D87 File Offset: 0x00127F87
		public void backgroundPlay()
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00129D8C File Offset: 0x00127F8C
		public void drawHelper(string mess, int totalbuttons, int butnum, float gap)
		{
			int num = totalbuttons - 1;
			float num2 = 214f;
			float num3 = 60f;
			float num4 = (float)totalbuttons * num2 + (float)num * gap;
			float num5 = 350f - num3 / 2f;
			float num6 = 640f - num4 / 2f;
			num6 += (float)butnum * (num2 + gap);
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				Rectangle rectangle = new Rectangle((int)num6, (int)num5, 214, 70);
				this.queryButton(rectangle, butnum);
			}
			Color color = new Color(180, 180, 180);
			if (this.whichbutton == butnum)
			{
				color = new Color(255, 255, 255);
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(num6 - 4f, num5), new Rectangle?(this.buttonOn), Color.White);
			}
			else
			{
				color = new Color(160, 160, 160);
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(num6, num5), new Rectangle?(this.button), Color.White);
			}
			float num7 = num6 + num2 / 2f - this.deadfont.MeasureString(mess).X / 2f + 3f;
			float num8 = 350f - this.deadfont.MeasureString(mess).Y / 2.5f;
			this.spriteBatch.DrawString(this.deadfont, mess, new Vector2(num7 - 3f, num8 - 3f), Color.Black);
			this.spriteBatch.DrawString(this.deadfont, mess, new Vector2(num7, num8), color);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00129F58 File Offset: 0x00128158
		public void drawHelper2(string mess, int totalbuttons, int butnum, float gap)
		{
			int num = totalbuttons - 1;
			float num2 = 214f;
			float num3 = 60f;
			float num4 = (float)totalbuttons * num2 + (float)num * gap;
			float num5 = 350f - num3 / 2f;
			float num6 = 640f - num4 / 2f;
			num6 += (float)butnum * (num2 + gap);
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				Rectangle rectangle = new Rectangle((int)num6, (int)num5, 214, 70);
				this.queryButton(rectangle, butnum);
			}
			Color color = new Color(180, 180, 180);
			if (this.whichbutton == butnum)
			{
				color = new Color(255, 255, 255);
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(num6 - 4f, num5), new Rectangle?(this.buttonOn2), Color.White);
			}
			else
			{
				color = new Color(160, 160, 160);
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(num6, num5), new Rectangle?(this.button2), Color.White);
			}
			float num7 = num6 + num2 / 2f - this.deadfont.MeasureString(mess).X / 2f + 3f;
			float num8 = 350f - this.deadfont.MeasureString(mess).Y / 2.5f;
			this.spriteBatch.DrawString(this.deadfont, mess, new Vector2(num7 - 3f, num8 - 3f), Color.Black);
			this.spriteBatch.DrawString(this.deadfont, mess, new Vector2(num7, num8), color);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0012A124 File Offset: 0x00128324
		public void drawHelper3(string mess, int totalbuttons, int butnum, float gap)
		{
			int num = totalbuttons - 1;
			float num2 = 214f;
			float num3 = 60f;
			float num4 = (float)totalbuttons * num2 + (float)num * gap;
			float num5 = 350f - num3 / 2f;
			float num6 = 640f - num4 / 2f;
			num6 += (float)butnum * (num2 + gap);
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				Rectangle rectangle = new Rectangle((int)num6, (int)num5, 214, 70);
				this.queryButton(rectangle, butnum);
			}
			Color color = new Color(180, 180, 180);
			if (this.whichbutton == butnum)
			{
				color = new Color(255, 255, 255);
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(num6 - 4f, num5), new Rectangle?(this.buttonOn3), Color.White);
			}
			else
			{
				color = new Color(160, 160, 160);
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(num6, num5), new Rectangle?(this.button3), Color.White);
			}
			float num7 = num6 + num2 / 2f - this.deadfont.MeasureString(mess).X / 2f + 3f;
			float num8 = 350f - this.deadfont.MeasureString(mess).Y / 2.5f;
			this.spriteBatch.DrawString(this.deadfont, mess, new Vector2(num7 - 3f, num8 - 3f), Color.Black);
			this.spriteBatch.DrawString(this.deadfont, mess, new Vector2(num7, num8), color);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0012A2F0 File Offset: 0x001284F0
		public void drawHelper4(string mess, int totalbuttons, int butnum, float gap, bool flash)
		{
			int num = totalbuttons - 1;
			float num2 = 174f;
			float num3 = 60f;
			float num4 = (float)totalbuttons * num2 + (float)num * gap;
			float num5 = 360f - num3 / 2f;
			float num6 = 640f - num4 / 2f;
			num6 += (float)butnum * (num2 + gap);
			int num7 = 15;
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				Rectangle rectangle = new Rectangle((int)num6, (int)num5 + num7, 175, 62);
				this.queryButton(rectangle, butnum);
			}
			Color color = new Color(180, 180, 180);
			if (this.whichbutton == butnum)
			{
				color = new Color(255, 255, 255);
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(num6, (float)num7 + num5), new Rectangle?(this.buttonOn4), Color.White);
			}
			else
			{
				color = new Color(160, 160, 160);
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(num6, (float)num7 + num5), new Rectangle?(this.button4), Color.White);
			}
			if (flash)
			{
				color = this.flashing[this.flashindex];
			}
			float num8 = num6 + num2 / 2f - this.sc.fontsmall.MeasureString(mess).X / 2f;
			float num9 = (float)(num7 + 360) - this.sc.fontsmall.MeasureString(mess).Y / 3f;
			this.spriteBatch.DrawString(this.sc.fontsmall, mess, new Vector2(num8 - 1f, num9 - 1f), new Color(50, 50, 50, 255));
			this.spriteBatch.DrawString(this.sc.fontsmall, mess, new Vector2(num8, num9), color);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0012A500 File Offset: 0x00128700
		public void drawHelper5(string mess, int totalbuttons, int butnum, float gap, bool flash)
		{
			int num = totalbuttons - 1;
			float num2 = 174f;
			float num3 = 60f;
			float num4 = (float)totalbuttons * num2 + (float)num * gap;
			float num5 = 360f - num3 / 2f;
			float num6 = 640f - num4 / 2f;
			num6 += (float)butnum * (num2 + gap);
			int num7 = 15;
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				Rectangle rectangle = new Rectangle((int)num6, (int)num5 + num7, 175, 62);
				this.queryButton(rectangle, butnum);
			}
			Color color = new Color(180, 180, 180);
			if (this.whichbutton == butnum)
			{
				color = new Color(255, 255, 255);
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(num6, (float)num7 + num5), new Rectangle?(this.buttonOn4x), Color.White);
			}
			else
			{
				color = new Color(160, 160, 160);
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(num6, (float)num7 + num5), new Rectangle?(this.button4x), Color.White);
			}
			if (flash)
			{
				color = this.flashing[this.flashindex];
			}
			float num8 = num6 + num2 / 2f - this.sc.fontsmall.MeasureString(mess).X / 2f;
			float num9 = (float)(num7 + 360) - this.sc.fontsmall.MeasureString(mess).Y / 3f;
			this.spriteBatch.DrawString(this.sc.fontsmall, mess, new Vector2(num8 - 1f, num9 - 1f), new Color(50, 50, 50, 255));
			this.spriteBatch.DrawString(this.sc.fontsmall, mess, new Vector2(num8, num9), color);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0012A710 File Offset: 0x00128910
		public void queryButtonX(Rectangle rr, Vector2 vv, int ch)
		{
			rr = new Rectangle((int)vv.X, (int)vv.Y, rr.Width, rr.Height);
			Vector2 vector = this.sc.mymouse;
			if (this.sc.aspectratio <= 1f)
			{
				vector.Y -= ((float)this.sc.screenSize.Height - (float)this.sc.screenSize.Height * this.sc.aspectratio) * (0.5f / this.sc.aspectratio);
			}
			else
			{
				vector.X -= ((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio);
			}
			Vector2 vector2 = 1f * new Vector2((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height);
			vector /= vector2;
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				this.myIndex = ch;
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0012A8C4 File Offset: 0x00128AC4
		public void queryButton(Rectangle rr, int index)
		{
			Vector2 vector = this.sc.adjustVector2(this.sc.mymouse);
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				this.whichbutton = index;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0012A950 File Offset: 0x00128B50
		public void queryButton2(Rectangle rr, int index)
		{
			Vector2 vector = this.sc.adjustVector2(this.sc.mymouse);
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				this.menuSelect = index;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0012A9DC File Offset: 0x00128BDC
		public void queryKeySquare(Rectangle rr, int index)
		{
			Vector2 vector = this.sc.adjustVector2(this.sc.mymouse);
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				this.whichKEY = index;
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0012AA68 File Offset: 0x00128C68
		public Vector2 adjustMouse()
		{
			Vector2 vector = this.sc.mymouse;
			if (this.sc.aspectratio <= 1f)
			{
				vector.Y -= ((float)this.sc.screenSize.Height - (float)this.sc.screenSize.Height * this.sc.aspectratio) * (0.5f / this.sc.aspectratio);
			}
			else
			{
				vector.X -= ((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio);
			}
			Vector2 vector2 = 1f * new Vector2((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height);
			vector /= vector2;
			return vector;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0012AB94 File Offset: 0x00128D94
		private void drawmain()
		{
			this.menuMusic.Volume = this.sc.mv;
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleHeaderBloodBacon, this.sc.origSize, Color.White);
			if (this.newpage)
			{
				this.spriteBatch.Draw(this.sc.titlePig0, new Vector2(475f, 399f), new Rectangle?(new Rectangle(0, 0, 400, 260)), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
			}
			if (!this.newpage)
			{
				if (this.sc.star1)
				{
					if (!this.sc.fastnades)
					{
						this.spriteBatch.Draw(this.sc.staroff, new Vector2(514f, 477f), Color.White);
					}
					if (this.sc.fastnades)
					{
						this.spriteBatch.Draw(this.sc.star, new Vector2(514f, 477f), Color.White);
					}
				}
				if (this.sc.star2)
				{
					if (this.sc.gorelevel < 1)
					{
						this.spriteBatch.Draw(this.sc.staroff, new Vector2(607f, 477f), Color.White);
					}
					if (this.sc.gorelevel > 0)
					{
						this.spriteBatch.Draw(this.sc.star, new Vector2(607f, 477f), Color.White);
					}
				}
				if (this.sc.star3)
				{
					if (!this.sc.doubleAmmo)
					{
						this.spriteBatch.Draw(this.sc.staroff, new Vector2(701f, 477f), Color.White);
					}
					if (this.sc.doubleAmmo)
					{
						this.spriteBatch.Draw(this.sc.star, new Vector2(701f, 477f), Color.White);
					}
				}
				this.spriteBatch.Draw(this.sc.titleHeaderSecrets, this.sc.origSize, Color.White);
			}
			this.drawHelper("single", 4, 0, 20f);
			this.drawHelper("co-op", 4, 1, 20f);
			this.drawHelper("setup", 4, 2, 20f);
			if (!this.newpage)
			{
				this.drawHelper("credits", 4, 3, 20f);
			}
			if (this.newpage)
			{
				this.drawHelper("more", 4, 3, 20f);
			}
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				this.queryButton(new Rectangle(586, 230, 80, 25), 7);
				this.queryButton(new Rectangle(424, 230, 80, 25), 8);
				this.queryButton(new Rectangle(754, 230, 80, 25), 9);
				if (this.paintunlocked)
				{
					this.queryButton(this.bonusPaint, 11);
				}
				if (!this.newpage)
				{
					this.queryButton(this.star1, 12);
					this.queryButton(this.star2, 13);
					this.queryButton(this.star3, 14);
				}
				if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
				{
					this.sc.tick.Play(this.sc.ev, 0.2f, 0f);
				}
			}
			if (this.whichbutton == 7)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(564f, 220f), new Rectangle?(this.bonusGlow), Color.White, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
			}
			else
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(564f, 220f), new Rectangle?(this.bonus), Color.White, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
			}
			if (this.whichbutton == 8)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(414f, 230f), new Rectangle?(this.audioGlow), Color.White, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
			}
			else
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(414f, 230f), new Rectangle?(this.audio), Color.White, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
			}
			if (this.whichbutton == 9)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(744f, 230f), new Rectangle?(this.keymapGlow), Color.White, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
			}
			else
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(744f, 230f), new Rectangle?(this.keymap), Color.White, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
			}
			if (this.whichbutton == 12)
			{
				this.spriteBatch.Draw(this.sc.starB, new Vector2(507f, 472f), Color.White);
			}
			if (this.whichbutton == 13)
			{
				this.spriteBatch.Draw(this.sc.starB, new Vector2(600f, 472f), Color.White);
			}
			if (this.whichbutton == 14)
			{
				this.spriteBatch.Draw(this.sc.starB, new Vector2(694f, 472f), Color.White);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0012B1E4 File Offset: 0x001293E4
		private void drawmore()
		{
			this.menuMusic.Volume = this.sc.mv;
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleHeaderMore, this.sc.origSize, Color.White);
			if (this.newpage)
			{
				if (this.sc.star1)
				{
					if (!this.sc.fastnades)
					{
						this.spriteBatch.Draw(this.sc.staroff, new Vector2(514f, 477f), Color.White);
					}
					if (this.sc.fastnades)
					{
						this.spriteBatch.Draw(this.sc.star, new Vector2(514f, 477f), Color.White);
					}
				}
				if (this.sc.star2)
				{
					if (this.sc.gorelevel < 1)
					{
						this.spriteBatch.Draw(this.sc.staroff, new Vector2(607f, 477f), Color.White);
					}
					if (this.sc.gorelevel > 0)
					{
						this.spriteBatch.Draw(this.sc.star, new Vector2(607f, 477f), Color.White);
					}
				}
				if (this.sc.star3)
				{
					if (!this.sc.doubleAmmo)
					{
						this.spriteBatch.Draw(this.sc.staroff, new Vector2(701f, 477f), Color.White);
					}
					if (this.sc.doubleAmmo)
					{
						this.spriteBatch.Draw(this.sc.star, new Vector2(701f, 477f), Color.White);
					}
				}
				this.spriteBatch.Draw(this.sc.titleHeaderSecrets, this.sc.origSize, Color.White);
			}
			if (this.sc.allachieves)
			{
				this.spriteBatch.Draw(this.sc.certified, new Vector2(250f, 65f), Color.White);
			}
			this.drawHelper("diary", 5, 0, 18f);
			this.drawHelper("workshop", 5, 1, 18f);
			this.drawHelper("paintland", 5, 2, 18f);
			this.drawHelper("credits", 5, 3, 18f);
			this.drawHelper("back", 5, 4, 18f);
			if (this.sc.workshopNum != this.sc.workshop.fileSize)
			{
				int fileSize = this.sc.workshop.fileSize;
			}
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				this.queryButton(this.star1, 12);
				this.queryButton(this.star2, 13);
				this.queryButton(this.star3, 14);
				if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
				{
					this.sc.tick.Play(this.sc.ev, 0f, 0f);
				}
			}
			if (this.whichbutton == 12)
			{
				this.spriteBatch.Draw(this.sc.starB, new Vector2(507f, 472f), Color.White);
			}
			if (this.whichbutton == 13)
			{
				this.spriteBatch.Draw(this.sc.starB, new Vector2(600f, 472f), Color.White);
			}
			if (this.whichbutton == 14)
			{
				this.spriteBatch.Draw(this.sc.starB, new Vector2(694f, 472f), Color.White);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0012B5E0 File Offset: 0x001297E0
		private void drawcoopChoice()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titlePig2, new Vector2(475f, 399f), new Rectangle?(new Rectangle(0, 0, 400, 260)), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.sc.titleHeaderMultiplayer, this.sc.origSize, Color.White);
			this.drawHelper("2player", 4, 0, 20f);
			this.drawHelper2("4player", 4, 1, 20f);
			this.drawHelper3("hordes", 4, 2, 20f);
			this.drawHelper("back", 4, 3, 20f);
			if (!this.covered && this.sc.Game.IsMouseVisible && this.lastbutton != this.whichbutton && this.whichbutton != -1)
			{
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0012B734 File Offset: 0x00129934
		private void drawmulti2player()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titlePig2, new Vector2(475f, 399f), new Rectangle?(new Rectangle(0, 0, 400, 260)), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.sc.titleHeader2player, this.sc.origSize, Color.White);
			this.drawHelper2("host", 3, 0, 35f);
			this.drawHelper2("join", 3, 1, 35f);
			this.drawHelper2("back", 3, 2, 35f);
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				this.queryButton(new Rectangle(580, 230, 100, 54), 7);
				if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
			if (this.whichbutton == 7)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(580f, 230f), new Rectangle?(this.creditBoxGlow), Color.White);
				return;
			}
			this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(580f, 230f), new Rectangle?(this.creditBox), Color.White);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0012B904 File Offset: 0x00129B04
		private void drawmulti3player()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG2, this.sc.origSize, Color.White);
			this.drawHelper2("host", 3, 0, 35f);
			this.drawHelper2("join", 3, 1, 35f);
			this.drawHelper3("back", 3, 2, 35f);
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				this.queryButton(new Rectangle(900, 175, 100, 54), 7);
				if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0012B9E4 File Offset: 0x00129BE4
		private void drawmulti4player()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titlePig2, new Vector2(475f, 399f), new Rectangle?(new Rectangle(0, 0, 400, 260)), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.sc.titleHeader4player, this.sc.origSize, Color.White);
			this.drawHelper2("host", 3, 0, 35f);
			this.drawHelper2("join", 3, 1, 35f);
			this.drawHelper2("back", 3, 2, 35f);
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				this.queryButton(new Rectangle(900, 175, 100, 54), 7);
				if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
			if (this.whichbutton == 7)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(900f, 175f), new Rectangle?(this.creditBoxGlow), Color.White);
				return;
			}
			this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(900f, 175f), new Rectangle?(this.creditBox), Color.White);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0012BBB4 File Offset: 0x00129DB4
		private void drawmulti6player()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titlePig2, new Vector2(475f, 399f), new Rectangle?(new Rectangle(0, 0, 400, 260)), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.sc.titleHeader6player, this.sc.origSize, Color.White);
			this.drawHelper("host", 3, 0, 35f);
			this.drawHelper("join", 3, 1, 35f);
			this.drawHelper("back", 3, 2, 35f);
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				this.queryButton(new Rectangle(900, 175, 100, 54), 7);
				if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0012BD10 File Offset: 0x00129F10
		private void drawplaygame()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleHeaderBloodBaconB, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleHeaderTunnelDays, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleCharsSingle, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleFaces, new Rectangle(0, 220, 1280, 500), new Rectangle?(new Rectangle(0, 220, 1280, 500)), Color.White);
			Rectangle rectangle;
			if (!this.sc.man1)
			{
				rectangle = new Rectangle((int)this.selectPoint[8].X + 1, (int)this.selectPoint[8].Y + -1, 48, 48);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.man2)
			{
				rectangle = new Rectangle((int)this.selectPoint[9].X + 1, (int)this.selectPoint[9].Y + -1, 48, 48);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.man3)
			{
				rectangle = new Rectangle((int)this.selectPoint[10].X + 1, (int)this.selectPoint[10].Y + -1, 48, 48);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.FarmerUnlocked)
			{
				rectangle = new Rectangle((int)this.selectPoint[4].X + 11, (int)this.selectPoint[4].Y + 11, this.locked.Width, this.locked.Height);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.man4)
			{
				rectangle = new Rectangle((int)this.selectPoint[7].X, (int)this.selectPoint[7].Y, this.locked.Width, this.locked.Height);
				rectangle.Width = 48;
				rectangle.Height = 48;
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			rectangle = new Rectangle((int)this.selectPoint[this.menuSelect].X - 1, (int)this.selectPoint[this.menuSelect].Y - 1, this.glow.Width, this.glow.Height);
			if (this.menuSelect >= 6)
			{
				if (this.menuSelect <= 10)
				{
					rectangle.Width = 52;
					rectangle.Height = 52;
				}
				else
				{
					rectangle.X = 551;
					rectangle.Y = 212;
					rectangle.Width = 205;
					rectangle.Height = 53;
				}
			}
			if (this.menuSelect <= 10)
			{
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.glow), Color.White);
			}
			this.drawInfo();
			this.drawHelper("start", 2, 0, 20f);
			this.drawHelper("back", 2, 1, 20f);
			if (!this.covered && this.sc.Game.IsMouseVisible && this.lastbutton != this.whichbutton && this.whichbutton != -1)
			{
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0012C198 File Offset: 0x0012A398
		private void drawmultijoinstart()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleHeaderJoin, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleCharsSingle, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleFaces, new Rectangle(0, 220, 1280, 500), new Rectangle?(new Rectangle(0, 220, 1280, 500)), Color.White);
			Color color = Color.White;
			if (!this.sc.lobby.inLobby)
			{
				color = new Color(255, 105, 105, 255);
			}
			if (this.sc.lobby.lobbyPassword != "")
			{
				color = new Color(105, 105, 255, 255);
			}
			if (this.sc.lobby.lobbyState == "20")
			{
				color = new Color(255, 90, 90, 255);
			}
			if (this.sc.myTimer % 30f == 0f && this.sc.lobby.joinedLobby.Count > 0)
			{
				this.sc.lobby.grabDayCharState(this.sc.lobby.joinedLobby[0]);
			}
			if (this.sc.developer)
			{
				this.spriteBatch.DrawString(this.sc.font2, this.sc.lobby.lobbyPassword, new Vector2(640f - this.sc.font2.MeasureString(this.sc.lobby.lobbyPassword).X / 2f, 30f), Color.White);
			}
			this.spriteBatch.DrawString(this.sc.font2, this.sc.lobby.lobbyName + "'s Game", new Vector2(640f, 130f) - this.sc.font2.MeasureString(this.sc.lobby.lobbyName + "'s Game") / 2f, color);
			string text = "";
			if (this.sc.lobby.lobbyChar == "0")
			{
				text = "Lando Bacon";
			}
			if (this.sc.lobby.lobbyChar == "1")
			{
				text = "Johnny Blood";
			}
			if (this.sc.lobby.lobbyChar == "2")
			{
				text = "Farmer McDigg";
			}
			if (this.sc.lobby.lobbyChar == "3")
			{
				text = "The Skeleton";
			}
			if (this.sc.lobby.lobbyChar == "4")
			{
				text = "Daisy Dee";
			}
			if (this.sc.lobby.lobbyChar == "5")
			{
				text = "A Viking";
			}
			if (this.sc.lobby.lobbyChar == "7")
			{
				text = "A Scarecrow";
			}
			if (this.sc.lobby.lobbyChar == "8")
			{
				text = "Toy Robot";
			}
			if (this.sc.lobby.lobbyChar == "9")
			{
				text = "The Golem";
			}
			if (this.sc.lobby.lobbyChar == "10")
			{
				text = "Astronaut";
			}
			if (text != "")
			{
				this.spriteBatch.DrawString(this.sc.font2, "Playing as " + text, new Vector2(640f, 160f) - this.sc.font2.MeasureString("Playing as " + text) / 2f, color);
			}
			if (this.sc.lobby.lobbyDay != "null" && this.sc.lobby.lobbyDay != "wait")
			{
				if (this.sc.lobby.lobbyDay == "0")
				{
					this.spriteBatch.DrawString(this.sc.font2, "Choosing A Character", new Vector2(640f, 190f) - this.sc.font2.MeasureString("Choosing A Character") / 2f, color);
				}
				else if (this.sc.lobby.lobbyplayers == "6")
				{
					this.spriteBatch.DrawString(this.sc.font2, "Day 666 Hordes", new Vector2(640f, 190f) - this.sc.font2.MeasureString("Day 666 Hordes") / 2f, color);
				}
				else if (this.sc.lobby.lobbyplayers == "3")
				{
					this.spriteBatch.DrawString(this.sc.font2, "Paint The Farm", new Vector2(640f, 190f) - this.sc.font2.MeasureString("Paint The Farm") / 2f, color);
				}
				else
				{
					this.spriteBatch.DrawString(this.sc.font2, "Playing on Day " + this.sc.lobby.lobbyDay, new Vector2(640f, 190f) - this.sc.font2.MeasureString("Playing on Day " + this.sc.lobby.lobbyDay) / 2f, color);
				}
			}
			else
			{
				if (this.sc.lobby.lobbyDay == "null")
				{
					this.spriteBatch.DrawString(this.sc.font2, "Lobby is Full", new Vector2(640f, 190f) - this.sc.font2.MeasureString("Lobby is Full") / 2f, color);
				}
				if (this.sc.lobby.lobbyDay == "wait")
				{
					color = Color.White;
					this.spriteBatch.DrawString(this.sc.font2, "Please Wait", new Vector2(640f, 190f) - this.sc.font2.MeasureString("Please Wait") / 2f, color);
				}
			}
			bool flag = this.sc.lobby.lobbyplayers == "2" && this.sc.lobby.lobbyVersion == this.sc.lobby.versionNumber.ToString();
			bool flag2 = this.sc.lobby.lobbyplayers == "4" && this.sc.lobby.lobbyVersion == this.sc.lobby.version4Player.ToString();
			bool flag3 = this.sc.lobby.lobbyplayers == "6" && this.sc.lobby.lobbyVersion == this.sc.lobby.version6Player.ToString();
			bool flag4 = this.sc.lobby.lobbyplayers == "3" && this.sc.lobby.lobbyDay == this.sc.lobby.paintVersion.ToString();
			bool flag5 = this.sc.lobby.lobbyplayers == "4" && this.sc.lobby.lobbyVersion == this.sc.lobby.version4Tunnel.ToString();
			if (flag || flag2 || flag3 || flag4 || flag5)
			{
				if (this.sc.lobby.lobbyState == "0")
				{
					this.spriteBatch.DrawString(this.sc.font2, "Not Yet Ready", new Vector2(640f, 220f) - this.sc.font2.MeasureString("Not Yet Ready") / 2f, color);
				}
				if (this.sc.lobby.lobbyState == "10")
				{
					this.spriteBatch.DrawString(this.sc.font2, "Waiting in Barn", new Vector2(640f, 220f) - this.sc.font2.MeasureString("Waiting in Barn") / 2f, color);
				}
				if (this.sc.lobby.lobbyState == "20")
				{
					this.spriteBatch.DrawString(this.sc.font2, "Game is in Progress", new Vector2(640f, 220f) - this.sc.font2.MeasureString("Game is in Progress") / 2f, color);
				}
				if (this.sc.lobby.lobbyState == "5")
				{
					this.spriteBatch.DrawString(this.sc.font2, "Lobby Observation", new Vector2(640f, 220f) - this.sc.font2.MeasureString("Lobby Observation") / 2f, color);
				}
				if (this.sc.lobby.lobbyState == "6")
				{
					this.spriteBatch.DrawString(this.sc.font2, "Game Is Full", new Vector2(640f, 220f) - this.sc.font2.MeasureString("Game Is Full") / 2f, color);
				}
			}
			else if (this.sc.lobby.lobbyDay != "wait" && this.sc.lobby.lobbyDay != "null")
			{
				this.spriteBatch.DrawString(this.sc.font2, "** VERSION MISMATCH **", new Vector2(640f, 220f) - this.sc.font2.MeasureString("** VERSION MISMATCH **") / 2f, new Color(255, 0, 255, 255));
			}
			Rectangle rectangle;
			if (!this.sc.man1)
			{
				rectangle = new Rectangle((int)this.selectPoint[8].X + 1, (int)this.selectPoint[8].Y + -1, 48, 48);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.man2)
			{
				rectangle = new Rectangle((int)this.selectPoint[9].X + 1, (int)this.selectPoint[9].Y + -1, 48, 48);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.man3)
			{
				rectangle = new Rectangle((int)this.selectPoint[10].X + 1, (int)this.selectPoint[10].Y + -1, 48, 48);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.FarmerUnlocked)
			{
				rectangle = new Rectangle((int)this.selectPoint[4].X + 11, (int)this.selectPoint[4].Y + 11, this.locked.Width, this.locked.Height);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.man4)
			{
				rectangle = new Rectangle((int)this.selectPoint[7].X, (int)this.selectPoint[7].Y, this.locked.Width, this.locked.Height);
				rectangle.Width = 48;
				rectangle.Height = 48;
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			rectangle = new Rectangle((int)this.selectPoint[this.menuSelect].X - 1, (int)this.selectPoint[this.menuSelect].Y - 1, this.glow.Width, this.glow.Height);
			if (this.menuSelect >= 6)
			{
				if (this.menuSelect <= 10)
				{
					rectangle.Width = 52;
					rectangle.Height = 52;
				}
				else
				{
					rectangle.X = 551;
					rectangle.Y = 212;
					rectangle.Width = 205;
					rectangle.Height = 53;
				}
			}
			if (this.menuSelect <= 10)
			{
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.glow), Color.White);
			}
			this.drawInfo();
			this.drawHelper("start", 2, 0, 20f);
			this.drawHelper("back", 2, 1, 20f);
			if (!this.covered && this.sc.Game.IsMouseVisible && this.lastbutton != this.whichbutton && this.whichbutton != -1)
			{
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0012D0BC File Offset: 0x0012B2BC
		private void drawmultihoststart()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleHeaderBloodBaconC, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleCharsSingle, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleFaces, new Rectangle(0, 220, 1280, 500), new Rectangle?(new Rectangle(0, 220, 1280, 500)), Color.White);
			if (this.lobbynum == "4" || this.lobbynum == "2")
			{
				this.spriteBatch.Draw(this.sc.titleHeaderTunnelDays, this.sc.origSize, Color.White);
			}
			Rectangle rectangle;
			if (!this.sc.man1)
			{
				rectangle = new Rectangle((int)this.selectPoint[8].X + 1, (int)this.selectPoint[8].Y + -1, 48, 48);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.man2)
			{
				rectangle = new Rectangle((int)this.selectPoint[9].X + 1, (int)this.selectPoint[9].Y + -1, 48, 48);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.man3)
			{
				rectangle = new Rectangle((int)this.selectPoint[10].X + 1, (int)this.selectPoint[10].Y + -1, 48, 48);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.FarmerUnlocked)
			{
				rectangle = new Rectangle((int)this.selectPoint[4].X + 11, (int)this.selectPoint[4].Y + 11, this.locked.Width, this.locked.Height);
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			if (!this.sc.man4)
			{
				rectangle = new Rectangle((int)this.selectPoint[7].X, (int)this.selectPoint[7].Y, this.locked.Width, this.locked.Height);
				rectangle.Width = 48;
				rectangle.Height = 48;
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.locked), Color.White);
			}
			rectangle = new Rectangle((int)this.selectPoint[this.menuSelect].X - 1, (int)this.selectPoint[this.menuSelect].Y - 1, this.glow.Width, this.glow.Height);
			if (this.menuSelect >= 6)
			{
				if (this.menuSelect <= 10)
				{
					rectangle.Width = 52;
					rectangle.Height = 52;
				}
				else
				{
					rectangle.X = 551;
					rectangle.Y = 212;
					rectangle.Width = 205;
					rectangle.Height = 53;
				}
			}
			if (this.menuSelect <= 10)
			{
				this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.glow), Color.White);
			}
			this.drawInfo();
			this.drawHelper("start", 2, 0, 20f);
			this.drawHelper("back", 2, 1, 20f);
			if (!this.covered && this.sc.Game.IsMouseVisible && this.lastbutton != this.whichbutton && this.whichbutton != -1)
			{
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0012D568 File Offset: 0x0012B768
		private void drawsettings()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titlePig1, new Vector2(475f, 416f), new Rectangle?(this.myRect[this.pigcount]), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.sc.titleheaderSettings, this.sc.origSize, Color.White);
			this.drawHelper("audio", 4, 0, 20f);
			this.drawHelper("video", 4, 1, 20f);
			this.drawHelper("controls", 4, 2, 20f);
			this.drawHelper("back", 4, 3, 20f);
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				this.queryButton(new Rectangle(493, 463, 280, 120), 4);
				if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0012D6E0 File Offset: 0x0012B8E0
		private void drawcontrols()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titlePig2, new Vector2(475f, 399f), new Rectangle?(new Rectangle(0, 0, 400, 260)), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.sc.titleHeaderControls, this.sc.origSize, Color.White);
			this.drawHelper("keyboard", 3, 0, 35f);
			this.drawHelper("hot-keys", 3, 1, 35f);
			this.drawHelper("gamepad", 3, 2, 35f);
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				this.queryButton(this.secretCoop, 13);
				if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0012D830 File Offset: 0x0012BA30
		private void drawdisplay()
		{
			if (this.showInstruct)
			{
				Vector2 vector = new Vector2((float)(640 - this.sc.instructions.Width / 2), (float)(360 - this.sc.instructions.Height / 2));
				this.spriteBatch.Draw(this.sc.instructions, vector, Color.White);
			}
			if (this.showGamepad)
			{
				Vector2 vector2 = new Vector2((float)(640 - this.sc.controller.Width / 2), (float)(360 - this.sc.controller.Height / 2));
				this.spriteBatch.Draw(this.sc.controller, vector2, Color.White);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0012D8F8 File Offset: 0x0012BAF8
		private void drawcooplobby()
		{
			if ((int)this.sc.myTimer % 250 == 1)
			{
				this.sc.lobby.requestLobby(this.lobbynum);
			}
			if (this.lobbynum == "6")
			{
				this.spriteBatch.Draw(this.sc.titleHeaderLobbies2, this.sc.origSize, Color.White);
			}
			else
			{
				this.spriteBatch.Draw(this.sc.titleHeaderLobbies, this.sc.origSize, Color.White);
			}
			for (int i = 0; i < 21; i++)
			{
				Color color = new Color(58, 181, 51, 255);
				string text;
				if (i < this.sc.lobby.lobbys.Count)
				{
					if (this.whichKEY == i)
					{
						color = Color.White;
					}
					text = this.sc.lobby.lobbys[i].name;
					this.spriteBatch.DrawString(this.sc.font2, this.sc.lobby.lobbys[i].name, this.nameSpot[i] - this.sc.font2.MeasureString(this.sc.lobby.lobbys[i].name) / 2f, color);
				}
				else
				{
					text = "empty-slot";
					color = new Color(190, 90, 90, 255);
					if (this.whichKEY == i)
					{
						color = Color.White;
					}
					this.spriteBatch.DrawString(this.sc.font2, "empty-slot", this.nameSpot[i] - this.sc.font2.MeasureString("empty-slot") / 2f, color);
				}
				if (this.mousemoving)
				{
					Vector2 vector = this.sc.font2.MeasureString(text);
					this.queryKeySquare(new Rectangle((int)(this.nameSpot[i].X - vector.X / 2f), (int)(this.nameSpot[i].Y - vector.Y / 2f), (int)vector.X, (int)vector.Y), i);
				}
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0012DB70 File Offset: 0x0012BD70
		private void drawvideos()
		{
			this.backgroundPlay();
			this.menuMusic.Volume = this.sc.mv;
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titlePig3, new Vector2(475f, 399f), new Rectangle?(new Rectangle(0, 0, 400, 260)), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.sc.titleHeaderVideos, this.sc.origSize, Color.White);
			if (base.IsActive)
			{
				this.drawHelper("credits", 3, 0, 35f);
				this.drawHelper("trailer", 3, 1, 35f);
				this.drawHelper("back", 3, 2, 35f);
			}
			if (!this.covered && this.sc.Game.IsMouseVisible && this.lastbutton != this.whichbutton && this.whichbutton != -1)
			{
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0012DCD0 File Offset: 0x0012BED0
		private void drawgraphics()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titlePig3, new Vector2(475f, 399f), new Rectangle?(new Rectangle(0, 0, 400, 260)), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
			int num = 346;
			int num2 = 640;
			int num3 = 934;
			this.spriteBatch.Draw(this.sc.titleCharsGraphics, this.sc.origSize, Color.White);
			string text = "Aliasing " + this.sc.aa;
			this.spriteBatch.DrawString(this.font2, text, new Vector2((float)num2 - this.font2.MeasureString(text).X / 2f, 250f), Color.White);
			string text2 = this.sc.resnames[this.sc.setupnum];
			this.spriteBatch.DrawString(this.font2, text2, new Vector2((float)num - this.font2.MeasureString(text2).X / 2f, 250f), Color.White);
			this.spriteBatch.DrawString(this.font2, Math.Round((double)this.sc.aspectratio, 1).ToString(), new Vector2((float)num3 - this.font2.MeasureString(Math.Round((double)this.sc.aspectratio, 1).ToString()).X / 2f, 250f), Color.White);
			if (base.IsActive)
			{
				this.drawHelper("apply", 2, 0, 45f);
				this.drawHelper("back", 2, 1, 45f);
			}
			int num4 = num + 5;
			int num5 = num - (this.arrowdown.Width + 5);
			int num6 = num2 + 5;
			int num7 = num2 - (this.arrowdown.Width + 5);
			int num8 = num3 + 5;
			int num9 = num3 - (this.arrowdown.Width + 5);
			int num10 = 140;
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				Rectangle rectangle = new Rectangle(num5, num10, this.arrowfill.Width, this.arrowfill.Height);
				this.queryButton(rectangle, 5);
				rectangle = new Rectangle(num4, num10, this.arrowfill.Width, this.arrowfill.Height);
				this.queryButton(rectangle, 6);
				rectangle = new Rectangle(num7, num10, this.arrowfill.Width, this.arrowfill.Height);
				this.queryButton(rectangle, 7);
				rectangle = new Rectangle(num6, num10, this.arrowfill.Width, this.arrowfill.Height);
				this.queryButton(rectangle, 8);
				rectangle = new Rectangle(num8, num10, this.arrowfill.Width, this.arrowfill.Height);
				this.queryButton(rectangle, 9);
				rectangle = new Rectangle(num9, num10, this.arrowfill.Width, this.arrowfill.Height);
				this.queryButton(rectangle, 10);
				rectangle = new Rectangle(330, 321, 34, 34);
				this.queryButton(rectangle, 11);
				rectangle = new Rectangle(330, 375, 34, 34);
				this.queryButton(rectangle, 12);
				if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
			if (this.whichbutton == 5 || this.highlight == 5)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num5, (float)num10), new Rectangle?(this.arrowfill), Color.White);
			}
			this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num5, (float)num10), new Rectangle?(this.arrowdown), Color.White);
			if (this.whichbutton == 6 || this.highlight == 6)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num4, (float)num10), new Rectangle?(this.arrowfill), Color.White);
			}
			this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num4, (float)num10), new Rectangle?(this.arrowup), Color.White);
			if (this.whichbutton == 7 || this.highlight == 7)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num7, (float)num10), new Rectangle?(this.arrowfill), Color.White);
			}
			this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num7, (float)num10), new Rectangle?(this.arrowleft), Color.White);
			if (this.whichbutton == 8 || this.highlight == 8)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num6, (float)num10), new Rectangle?(this.arrowfill), Color.White);
			}
			this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num6, (float)num10), new Rectangle?(this.arrowright), Color.White);
			if (this.whichbutton == 9 || this.highlight == 9)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num8, (float)num10), new Rectangle?(this.arrowfill), Color.White);
			}
			this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num8, (float)num10), new Rectangle?(this.arrowplus), Color.White);
			if (this.whichbutton == 10 || this.highlight == 10)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num9, (float)num10), new Rectangle?(this.arrowfill), Color.White);
			}
			this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2((float)num9, (float)num10), new Rectangle?(this.arrowminus), Color.White);
			if (!this.sc.fullmode)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(330f, 321f), new Rectangle?(this.redboxfill), Color.White);
			}
			else
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(330f, 321f), new Rectangle?(this.redbox), Color.White);
			}
			if (!this.sc.border)
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(330f, 375f), new Rectangle?(this.redboxfill), Color.White);
			}
			else
			{
				this.spriteBatch.Draw(this.sc.menuBlob2, new Vector2(330f, 375f), new Rectangle?(this.redbox), Color.White);
			}
			string text3 = "F10 WINDOWED  ";
			this.spriteBatch.DrawString(this.font2, text3, new Vector2(330f - this.font2.MeasureString(text3).X, 320f), Color.White);
			text3 = "F9 NO BORDERS ";
			this.spriteBatch.DrawString(this.font2, text3, new Vector2(330f - this.font2.MeasureString(text3).X, 374f), Color.White);
			this.drawData();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0012E4FC File Offset: 0x0012C6FC
		private void drawaudio()
		{
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titlePig3, new Vector2(475f, 399f), new Rectangle?(new Rectangle(0, 0, 400, 260)), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.sc.titleHeaderAudio, this.sc.origSize, Color.White);
			if (base.IsActive)
			{
				this.drawHelper("save", 2, 0, 65f);
				this.drawHelper("back", 2, 1, 65f);
			}
			if (!this.covered && this.sc.Game.IsMouseVisible && this.lastbutton != this.whichbutton && this.whichbutton != -1)
			{
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
			int num = this.myIndex;
			this.myIndex = -1;
			Vector2 vector = new Vector2(640f, 240f);
			Vector2 vector2 = new Vector2(-120f, -100f);
			this.spriteBatch.DrawString(this.sc.scribblefont, this.slider1.ToString(), new Vector2(vector2.X + 350f, vector2.Y + 25f) + vector - this.sc.scribblefont.MeasureString(this.slider1.ToString()) / 2f, Color.White);
			this.queryButtonX(this.sliderBoxRect, vector + vector2 + new Vector2(105f + this.sliderbox1, 4f), 0);
			this.spriteBatch.Draw(this.sc.paper1, vector + vector2 + new Vector2(-40f, 20f), new Rectangle?(this.sliderRect), Color.White);
			if (this.myIndex == 0 || this.whichbutton == 2)
			{
				this.spriteBatch.Draw(this.sc.paper1, vector + vector2 + new Vector2(105f + this.sliderbox1, 4f), new Rectangle?(this.sliderGlowRect), Color.White);
			}
			else
			{
				this.spriteBatch.Draw(this.sc.paper1, vector + vector2 + new Vector2(105f + this.sliderbox1, 4f), new Rectangle?(this.sliderBoxRect), Color.White);
			}
			vector2 = new Vector2(-120f, -50f);
			this.spriteBatch.DrawString(this.sc.scribblefont, this.slider2.ToString(), new Vector2(vector2.X + 350f, vector2.Y + 25f) + vector - this.sc.scribblefont.MeasureString(this.slider2.ToString()) / 2f, Color.White);
			this.queryButtonX(this.sliderBoxRect, vector + vector2 + new Vector2(105f + this.sliderbox2, 4f), 1);
			this.spriteBatch.Draw(this.sc.paper1, vector + vector2 + new Vector2(-40f, 20f), new Rectangle?(this.sliderRect), Color.White);
			if (this.myIndex == 1 || this.whichbutton == 3)
			{
				this.spriteBatch.Draw(this.sc.paper1, vector + vector2 + new Vector2(105f + this.sliderbox2, 4f), new Rectangle?(this.sliderGlowRect), Color.White);
			}
			else
			{
				this.spriteBatch.Draw(this.sc.paper1, vector + vector2 + new Vector2(105f + this.sliderbox2, 4f), new Rectangle?(this.sliderBoxRect), Color.White);
			}
			vector2 = new Vector2(-120f, 0f);
			this.spriteBatch.DrawString(this.sc.scribblefont, this.slider3.ToString(), new Vector2(vector2.X + 350f, vector2.Y + 25f) + vector - this.sc.scribblefont.MeasureString(this.slider3.ToString()) / 2f, Color.White);
			this.queryButtonX(this.sliderBoxRect, vector + vector2 + new Vector2(105f + this.sliderbox3, 4f), 2);
			this.spriteBatch.Draw(this.sc.paper1, vector + vector2 + new Vector2(-40f, 20f), new Rectangle?(this.sliderRect), Color.White);
			if (this.myIndex == 2 || this.whichbutton == 4)
			{
				this.spriteBatch.Draw(this.sc.paper1, vector + vector2 + new Vector2(105f + this.sliderbox3, 4f), new Rectangle?(this.sliderGlowRect), Color.White);
			}
			else
			{
				this.spriteBatch.Draw(this.sc.paper1, vector + vector2 + new Vector2(105f + this.sliderbox3, 4f), new Rectangle?(this.sliderBoxRect), Color.White);
			}
			if (num != this.myIndex && this.myIndex != -1)
			{
				this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0012EB54 File Offset: 0x0012CD54
		private void drawdiary()
		{
			this.spriteBatch.Draw(this.sc.titleHeaderDiary, this.sc.origSize, Color.White);
			if (this.sc.developer)
			{
				if (this.recording1 && this.sc.myTimer % 15f < 7f)
				{
					this.spriteBatch.DrawString(this.deadfont, "Record Body", new Vector2(700f, 20f), Color.White);
				}
				if (this.recording2 && this.sc.myTimer % 15f < 7f)
				{
					this.spriteBatch.DrawString(this.deadfont, "Record Sprites", new Vector2(700f, 20f), Color.Green);
				}
				if (this.recording3 && this.sc.myTimer % 15f < 7f)
				{
					this.spriteBatch.DrawString(this.deadfont, "Record RotScale", new Vector2(700f, 20f), Color.Yellow);
				}
				if (this.recording4 && this.sc.myTimer % 15f < 7f)
				{
					this.spriteBatch.DrawString(this.deadfont, "Record Lights", new Vector2(700f, 20f), Color.Yellow);
				}
			}
			if (this.sc.workshop.entry.Count > 0)
			{
				this.sc.workshop.entryIndex = (int)MathHelper.Clamp((float)this.sc.workshop.entryIndex, 0f, (float)(this.sc.workshop.entry.Count - 1));
				bool flag = false;
				byte b = byte.MaxValue;
				if (this.farmerJawIndex != -1)
				{
					b = 50;
				}
				for (int i = 0; i < this.sc.workshop.entry.Count; i++)
				{
					this.queryButton(this.sc.workshop.entryBox[i], i);
					Vector2 vector = new Vector2((float)this.sc.workshop.entryBox[i].X, (float)(this.sc.workshop.entryBox[i].Y - 10));
					string[] array = this.sc.workshop.entry[i].dates.Split(new char[] { '.' });
					string text = this.months[Convert.ToInt32(array[0])] + "." + array[1];
					string text2 = "";
					Color color = new Color(120, 120, 160, (int)b);
					Color color2 = new Color(255, 255, 255, (int)b);
					if (array[2].Length == 4)
					{
						text = this.months[Convert.ToInt32(array[0])] + "." + array[1];
						text2 = array[2];
						color = new Color(90, 90, 90, (int)b);
					}
					if (array[2].Length == 2 && !flag)
					{
						flag = true;
						if (this.whichbutton == i)
						{
							if (this.farmerJawIndex == -1 && this.followPoint)
							{
								this.followPoint = false;
								this.followTimer = 200f;
								this.lastlookpos = this.lookpos[this.lookIndex];
								this.followTween = 1f;
							}
							this.spriteBatch.DrawString(this.diaryfont, text, vector, color2, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
						}
						else
						{
							this.spriteBatch.DrawString(this.diaryfont, text, vector, color, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
						}
					}
					else if (this.whichbutton == i)
					{
						if (this.farmerJawIndex == -1 && this.followPoint)
						{
							this.followPoint = false;
							this.followTimer = 200f;
							this.lastlookpos = this.lookpos[this.lookIndex];
							this.followTween = 1f;
						}
						this.spriteBatch.DrawString(this.diaryfont, text, vector, color2);
						this.spriteBatch.DrawString(this.diaryfont, text2, vector + new Vector2(75f, 0f), color2);
					}
					else
					{
						this.spriteBatch.DrawString(this.diaryfont, text, vector, color);
						this.spriteBatch.DrawString(this.diaryfont, text2, vector + new Vector2(75f, 0f), color);
					}
				}
				if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
				{
					this.sc.tick.Play(this.sc.ev, 0.2f, 0f);
				}
				if (this.farmerJawIndex != -1 && this.sc.workshop.entry.Count > 0)
				{
					int num = this.sc.workshop.entryIndex;
					for (int j = this.displayList.Count - 1; j >= 0; j--)
					{
						int num2 = this.displayList[j];
						if (this.im[num2].show && this.sc.workshop.entry[num].media.Count > this.im[num2].spriteIndex)
						{
							this.im[num2].tween += this.im[num2].tweenRate;
							Vector2 vector2 = this.im[num2].pos;
							if (this.im[num2].tween > 1f)
							{
								this.im[num2].tween = 1f;
								this.im[num2].oldpos = this.im[num2].pos;
							}
							else
							{
								vector2 = Vector2.Lerp(this.im[num2].oldpos, this.im[num2].pos, this.im[num2].tween);
							}
							Texture2D texture2D = this.sc.workshop.entry[num].media[this.im[num2].spriteIndex];
							Vector2 vector3 = new Vector2((float)(texture2D.Width / 2), (float)(texture2D.Height / 2));
							if (this.im[num2].full)
							{
								this.spriteBatch.Draw(texture2D, new Rectangle(0, 0, 1280, 720), Color.White);
							}
							else
							{
								this.spriteBatch.Draw(texture2D, vector2, null, Color.White, this.im[num2].rot, vector3, this.im[num2].scale, this.im[num2].flip, 0f);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0012F2C8 File Offset: 0x0012D4C8
		private void drawworkshop()
		{
			this.backgroundPlay();
			this.spriteBatch.Draw(this.sc.titleBG, this.sc.origSize, Color.White);
			this.spriteBatch.Draw(this.sc.titleHeaderWorkshop, this.sc.origSize, Color.White);
			if (this.drawsubs)
			{
				this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopBottomSubtitle, new Rectangle?(this.workshopBottomSubtitle), Color.White);
				if (!this.sc.workshop.textureBusy && this.sc.workshop.crosshairS.Count > this.subRow * 5)
				{
					int num = this.subRow * 5;
					while (num < this.subRow * 5 + 5 && num <= this.sc.workshop.crosshairS.Count - 1)
					{
						this.spriteBatch.Draw(this.sc.whiteTexture, new Rectangle(154 + num % 5 * 205, 471, 150, 150), new Color(35, 35, 35, 255));
						this.spriteBatch.Draw(this.sc.workshop.crosshairS[num], new Rectangle(154 + num % 5 * 205, 471, 150, 150), Color.White);
						num++;
					}
				}
			}
			if (this.drawcustom)
			{
				this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopBottomSubtitle, new Rectangle?(this.workshopBottomCustitle), Color.White);
				if (this.sc.crosshairC.Count > this.subRow * 5)
				{
					int num2 = this.subRow * 5;
					while (num2 < this.subRow * 5 + 5 && num2 <= this.sc.crosshairC.Count - 1)
					{
						this.spriteBatch.Draw(this.sc.whiteTexture, new Rectangle(154 + num2 % 5 * 205, 471, 150, 150), new Color(35, 35, 35, 255));
						this.spriteBatch.Draw(this.sc.crosshairC[num2], new Rectangle(154 + num2 % 5 * 205, 471, 150, 150), Color.White);
						num2++;
					}
				}
			}
			if (this.subRow == 0)
			{
				this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopNumsX, new Rectangle?(this.workshopNums1), Color.White);
			}
			else if (this.subRow == 1)
			{
				this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopNumsX, new Rectangle?(this.workshopNums2), Color.White);
			}
			else if (this.subRow == 2)
			{
				this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopNumsX, new Rectangle?(this.workshopNums3), Color.White);
			}
			else if (this.subRow == 3)
			{
				this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopNumsX, new Rectangle?(this.workshopNums4), Color.White);
			}
			for (int i = 1; i < 4; i++)
			{
				if (this.sc.crosshair1[i].type == 1)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, new Rectangle(154 + i * 205, 141, 150, 150), new Rectangle?(this.workshopBG1), Color.White);
					this.spriteBatch.Draw(this.sc.crosshair1[i].texture, new Rectangle(154 + i * 205, 141, 150, 150), Color.White);
					if (i == 1)
					{
						this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopDelbox1, new Rectangle?(this.workshopRedbox), Color.White);
					}
					if (i == 2)
					{
						this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopDelbox2, new Rectangle?(this.workshopRedbox), Color.White);
					}
					if (i == 3)
					{
						this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopDelbox3, new Rectangle?(this.workshopRedbox), Color.White);
					}
				}
			}
			if (!this.sc.workshop.showPublishbox)
			{
				this.drawHelper4("CUSTOM", 4, 0, 15f, false);
				this.drawHelper4("SUBSCRIBE", 4, 1, 15f, false);
				string text = "PUBLISH";
				bool flag = false;
				if (this.sc.workshop.status == "WORK")
				{
					text = this.sc.workshop.workstatus;
					flag = true;
				}
				if (this.sc.workshop.status == "FAIL")
				{
					text = "FAIL";
				}
				if (this.sc.workshop.status == "DONE")
				{
					text = "DONE";
				}
				if (this.sc.crosshair1[this.workshopChosen].legal)
				{
					this.drawHelper4(text, 4, 2, 15f, flag);
				}
				if (!this.sc.crosshair1[this.workshopChosen].legal)
				{
					this.drawHelper5(text, 4, 2, 15f, flag);
				}
				this.drawHelper4("BACK", 4, 3, 15f, false);
			}
			if (!this.covered && this.sc.Game.IsMouseVisible)
			{
				if (this.sc.workshop.showPublishbox)
				{
					this.queryButton(this.formTitle, 4);
					this.queryButton(this.formDescr, 5);
					this.queryButton(this.formBut1, 6);
					this.queryButton(this.formBut2, 7);
					this.queryButton(this.formBut3, 8);
					this.queryButton(this.formBut4, 9);
					this.queryButton(this.formBut5, 10);
					this.queryButton(this.formBut1b, 66);
					this.queryButton(this.formBut2b, 67);
					this.queryButton(this.formBut3b, 68);
					this.queryButton(this.formBut4b, 69);
					this.queryButton(this.formBut5b, 70);
					this.queryButton(this.formPublic, 11);
					this.queryButton(this.formPrivate, 12);
					this.queryButton(this.formFriends, 13);
					this.queryButton(this.formTerms, 14);
					this.queryButton(this.formCancel, 15);
					this.queryButton(this.formPublish, 16);
					this.queryButton(this.formTag[0], 40);
					this.queryButton(this.formTag[1], 41);
					this.queryButton(this.formTag[2], 42);
					this.queryButton(this.formTag[3], 43);
					this.queryButton(this.formTag[4], 44);
					this.queryButton(this.formTag[5], 45);
					this.queryButton(this.formTag[6], 46);
					this.queryButton(this.formTag[7], 47);
					this.queryButton(this.formTag[8], 48);
					this.queryButton(this.formWaterMark, 50);
					if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
					{
						this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
					}
					this.queryButton(this.formCenter, 80);
					this.queryButton(this.formLeft, 81);
					this.queryButton(this.formRight, 82);
					this.queryButton(this.formDown, 83);
					this.queryButton(this.formUp, 84);
					this.queryButton(this.formCorner1, 85);
					this.queryButton(this.formCorner2, 86);
					this.queryButton(this.formCorner3, 87);
					this.queryButton(this.formCorner4, 88);
				}
				else
				{
					this.queryButton(this.workshopt1, 20);
					this.queryButton(this.workshopt2, 21);
					this.queryButton(this.workshopt3, 22);
					this.queryButton(this.workshopb1, 23);
					this.queryButton(this.workshopb2, 24);
					this.queryButton(this.workshopb3, 25);
					this.queryButton(this.workshopb4, 26);
					this.queryButton(this.workshopb5, 27);
					this.queryButton(this.workshopLeftarrow, 28);
					this.queryButton(this.workshopRightarrow, 29);
					if (this.sc.crosshair1[1].type == 1)
					{
						this.queryButton(this.workshopDelbox1, 30);
					}
					if (this.sc.crosshair1[2].type == 1)
					{
						this.queryButton(this.workshopDelbox2, 31);
					}
					if (this.sc.crosshair1[3].type == 1)
					{
						this.queryButton(this.workshopDelbox3, 32);
					}
					if (this.lastbutton != this.whichbutton && this.whichbutton != -1)
					{
						if (this.whichbutton >= 23 && this.whichbutton <= 27)
						{
							if (this.whichbutton == 23)
							{
								this.sc.tick.Play(this.sc.ev * 0.6f, -0.7f, 0f);
							}
							if (this.whichbutton == 24)
							{
								this.sc.tick.Play(this.sc.ev * 0.6f, -0.65f, 0f);
							}
							if (this.whichbutton == 25)
							{
								this.sc.tick.Play(this.sc.ev * 0.6f, -0.55f, 0f);
							}
							if (this.whichbutton == 26)
							{
								this.sc.tick.Play(this.sc.ev * 0.6f, -0.5f, 0f);
							}
							if (this.whichbutton == 27)
							{
								this.sc.tick.Play(this.sc.ev * 0.6f, -0.45f, 0f);
							}
						}
						else if (this.whichbutton >= 20 && this.whichbutton <= 22)
						{
							if (this.whichbutton == 20)
							{
								this.sc.tick.Play(this.sc.ev * 0.6f, -0.3f, 0f);
							}
							if (this.whichbutton == 21)
							{
								this.sc.tick.Play(this.sc.ev * 0.6f, -0.25f, 0f);
							}
							if (this.whichbutton == 22)
							{
								this.sc.tick.Play(this.sc.ev * 0.6f, -0.2f, 0f);
							}
						}
						else
						{
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
					}
				}
			}
			if (!this.sc.workshop.showPublishbox)
			{
				if (this.whichbutton == 20)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopt1, new Rectangle?(this.workshopt1), Color.White);
				}
				if (this.whichbutton == 21)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopt2, new Rectangle?(this.workshopt1), Color.White);
				}
				if (this.whichbutton == 22)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopt3, new Rectangle?(this.workshopt1), Color.White);
				}
				this.flashindex++;
				this.flashindex %= this.flashing.Length;
				if (this.workshopChosen == 1)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopt1B, new Rectangle?(this.workshopt3B), this.flashing[this.flashindex]);
				}
				if (this.workshopChosen == 2)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopt2B, new Rectangle?(this.workshopt3B), this.flashing[this.flashindex]);
				}
				if (this.workshopChosen == 3)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopt3B, new Rectangle?(this.workshopt3B), this.flashing[this.flashindex]);
				}
				if (this.whichbutton == 23)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopb1, new Rectangle?(this.workshopb1), Color.White);
				}
				if (this.whichbutton == 24)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopb2, new Rectangle?(this.workshopb2), Color.White);
				}
				if (this.whichbutton == 25)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopb3, new Rectangle?(this.workshopb3), Color.White);
				}
				if (this.whichbutton == 26)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopb4, new Rectangle?(this.workshopb4), Color.White);
				}
				if (this.whichbutton == 27)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopb5, new Rectangle?(this.workshopb5), Color.White);
				}
				if (this.whichbutton == 28)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopLeftarrow, new Rectangle?(this.workshopLeftarrow), Color.White);
				}
				if (this.whichbutton == 29)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshop2, this.workshopRightarrow, new Rectangle?(this.workshopRightarrow), Color.White);
				}
			}
			if (this.sc.workshop.showPublishbox)
			{
				this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish, this.sc.origSize, Color.White);
				if (this.formDrawBG)
				{
					this.spriteBatch.Draw(this.sc.whiteTexture, this.formPreview, this.formBGColor[this.formBGColorindex]);
				}
				Rectangle rectangle = new Rectangle(this.zoomX, this.zoomY, this.sc.crosshair1[this.workshopChosen].texture.Width - 2 * this.zoomX, this.sc.crosshair1[this.workshopChosen].texture.Height - 2 * this.zoomY);
				this.sc.zoomRectangle = rectangle;
				this.spriteBatch.Draw(this.sc.crosshair1[this.workshopChosen].texture, this.formPreview, new Rectangle?(rectangle), Color.White);
				this.spriteBatch.DrawString(this.sc.fontsmall, this.sc.workshop.formTitle, new Vector2(344f, 181f), Color.White);
				this.spriteBatch.DrawString(this.sc.fontsmall, this.sc.workshop.formMark, new Vector2(344f, 349f), Color.White);
				this.me.Clear();
				string[] array = this.sc.workshop.formDescr.Split(new char[] { ' ' });
				string text2 = "";
				bool flag2 = false;
				for (int j = 0; j < array.Length; j++)
				{
					text2 = text2 + array[j] + " ";
					flag2 = false;
					if (text2.Length > 29)
					{
						this.me.Add(text2 + "\n");
						text2 = "";
						flag2 = true;
					}
				}
				if (!flag2)
				{
					this.me.Add(text2);
				}
				for (int k = 0; k < this.me.Count; k++)
				{
					float num3 = 344f;
					float num4 = (float)(259 + 18 * k);
					this.spriteBatch.DrawString(this.sc.fontsmall, this.me[k], new Vector2(num3, num4), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				}
				if (this.whichbutton == 4)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formTitle, new Rectangle?(this.formTitle), Color.White);
				}
				if (this.whichbutton == 5)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formDescr, new Rectangle?(this.formDescr), Color.White);
				}
				if (this.whichbutton == 50)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formWaterMark, new Rectangle?(this.formWaterMark), Color.White);
				}
				if (this.whichbutton == 6)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBut1, new Rectangle?(this.formBut1), Color.White);
				}
				if (this.whichbutton == 7)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBut2, new Rectangle?(this.formBut2), Color.White);
				}
				if (this.whichbutton == 8)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBut3, new Rectangle?(this.formBut3), Color.White);
				}
				if (this.whichbutton == 9)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBut4, new Rectangle?(this.formBut4), Color.White);
				}
				if (this.whichbutton == 10)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBut5, new Rectangle?(this.formBut5), Color.White);
				}
				if (this.whichbutton == 66)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBut1b, new Rectangle?(this.formBut1b), Color.White);
				}
				if (this.whichbutton == 67)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBut2b, new Rectangle?(this.formBut2b), Color.White);
				}
				if (this.whichbutton == 68)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBut3b, new Rectangle?(this.formBut3b), Color.White);
				}
				if (this.whichbutton == 69)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBut4b, new Rectangle?(this.formBut4b), Color.White);
				}
				if (this.whichbutton == 70)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBut5b, new Rectangle?(this.formBut5b), Color.White);
				}
				if (this.whichbutton == 11)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formPublic, new Rectangle?(this.formPublic), Color.White);
				}
				if (this.whichbutton == 12)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formPrivate, new Rectangle?(this.formPrivate), Color.White);
				}
				if (this.whichbutton == 13)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formFriends, new Rectangle?(this.formFriends), Color.White);
				}
				if (this.whichbutton == 14)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formTerms, new Rectangle?(this.formTerms), Color.White);
				}
				if (this.whichbutton == 15)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formCancel, new Rectangle?(this.formCancel), Color.White);
				}
				if (this.whichbutton == 16)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formPublish, new Rectangle?(this.formPublish), Color.White);
				}
				if (this.sc.workshop.formVisibility == this.sc.workshop.seePublic)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, new Vector2(348f, 413f), new Rectangle?(this.formCheckbox), Color.White);
				}
				if (this.sc.workshop.formVisibility == this.sc.workshop.seePrivate)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, new Vector2(457f, 413f), new Rectangle?(this.formCheckbox), Color.White);
				}
				if (this.sc.workshop.formVisibility == this.sc.workshop.seeFriends)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, new Vector2(562f, 413f), new Rectangle?(this.formCheckbox), Color.White);
				}
				this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formTag[this.formtagIndex], new Rectangle?(this.formTag[this.formtagIndex]), Color.White);
				if (this.formDrawBand)
				{
					this.spriteBatch.Draw(this.sc.titleHeaderWorkshopPublish2, this.formBandL, new Rectangle?(this.formBand), this.formBandColor[this.formBandColorIndex]);
					if (this.sc.workshop.formMark != "")
					{
						this.spriteBatch.DrawString(this.font3, this.sc.workshop.formMark, new Vector2(748f, 113f), Color.Black, -0.78539f, this.font3.MeasureString(this.sc.workshop.formMark) / 2f, 1f, SpriteEffects.None, 0f);
					}
				}
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00130AC8 File Offset: 0x0012ECC8
		private void drawFooter()
		{
			if (this.sc.errorMessageTimer > 0)
			{
				MainMenu.mybuilder.Length = 0;
				MainMenu.mybuilder.Append(this.sc.errorMessage);
				this.spriteBatch.DrawString(this.font, MainMenu.mybuilder, new Vector2(10f, 10f), Color.White);
			}
			this.spriteBatch.End();
			if (this.menuState == MainMenu.states.diary && SteamAPI.IsSteamRunning())
			{
				this.sc.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
				this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
				this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
				this.drawFarmer();
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00130B90 File Offset: 0x0012ED90
		public override void Draw(GameTime gameTime)
		{
			this.sc.GraphicsDevice.Clear(Color.Black);
			if (!this.sc.audioLoaded)
			{
				return;
			}
			if (this.menuState == MainMenu.states.diary)
			{
				this.menuMusic.Volume = 0f;
			}
			Matrix matrix = Matrix.CreateScale((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height, 1f);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, null, null, null, matrix);
			this.sc.errorMessageTimer--;
			if (this.titlecounter <= 0f)
			{
				if (this.menuState == MainMenu.states.main)
				{
					this.drawmain();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.more)
				{
					this.drawmore();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.coopChoice)
				{
					this.drawcoopChoice();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.multi2player)
				{
					this.drawmulti2player();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.multi4player)
				{
					this.drawmulti4player();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.multi6player)
				{
					this.drawmulti6player();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.multi3player)
				{
					this.drawmulti3player();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.playgame)
				{
					this.drawplaygame();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.multijoinStart)
				{
					this.drawmultijoinstart();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.multiHostStart)
				{
					this.drawmultihoststart();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.cooplobby)
				{
					this.drawcooplobby();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.settings)
				{
					this.drawsettings();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.display)
				{
					this.drawdisplay();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.controls)
				{
					this.drawcontrols();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.graphics)
				{
					this.drawgraphics();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.audio)
				{
					this.drawaudio();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.videos)
				{
					this.drawvideos();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.workshop)
				{
					this.drawworkshop();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.diary)
				{
					this.drawdiary();
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.displaykeys)
				{
					this.sc.drawKeyBindings2(this.spriteBatch, ref this.whichKEY, ref this.thisKEY, this.font2);
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.trailer)
				{
					this.menuMusic.Volume = 0f;
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.credit)
				{
					this.menuMusic.Volume = 0f;
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.creditwall)
				{
					this.spriteBatch.Draw(this.sc.titleCreditBlank, this.sc.origSize, Color.White);
					this.spriteBatch.Draw(this.sc.titleCredit1, this.sc.origSize, Color.White);
					this.drawFooter();
					return;
				}
				if (this.menuState == MainMenu.states.alphateam)
				{
					this.spriteBatch.Draw(this.sc.titleCreditBlank, this.sc.origSize, Color.White);
					this.spriteBatch.Draw(this.sc.titleCredit2, this.sc.origSize, Color.White);
					this.drawFooter();
					return;
				}
			}
			else if (this.titlecounter <= (float)this.counterStart)
			{
				Vector2 vector = new Vector2((float)this.sc.bc2.Width / 2f, (float)this.sc.bc2.Height / 2f);
				if (this.ramp < 1f && this.ramp + 0.07f >= 1f)
				{
					this.sc.pileDriverSure.Play(this.sc.ev, -0.2f, 0f);
				}
				this.ramp += 0.07f;
				if (this.ramp > 1f)
				{
					this.ramp = 1f;
				}
				float num = MathHelper.Hermite(18f, 0f, 0.9f, 0f, this.ramp);
				Vector2 vector2 = new Vector2((float)(this.sc.origSize.Width / 2), (float)(this.sc.origSize.Height / 2));
				if (this.titlecounter > (float)(this.counterStart / 2))
				{
					this.spriteBatch.Draw(this.sc.bc1, vector2, null, Color.White, 0f, vector, num, SpriteEffects.None, 0f);
				}
				else
				{
					this.spriteBatch.Draw(this.sc.bc2, vector2, null, Color.White, 0f, vector, num, SpriteEffects.None, 0f);
				}
			}
			this.drawFooter();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x001310D0 File Offset: 0x0012F2D0
		public void drawInfo()
		{
			float num = (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height;
			int num2 = this.sc.curDay;
			this.spriteBatch.DrawString(this.font2, "Start on Day " + num2, new Vector2(23f, 313f), Color.Black);
			this.spriteBatch.DrawString(this.font2, "Start on Day " + num2, new Vector2(20f, 310f), Color.White);
			num2 = (int)(this.sc.maxDay() + 1);
			if (num2 > 101)
			{
				num2 = 101;
			}
			if (num2 > 1)
			{
				this.spriteBatch.DrawString(this.font2, "Best Day " + num2, new Vector2(23f, 343f), Color.Black);
				this.spriteBatch.DrawString(this.font2, "Best Day " + num2, new Vector2(20f, 340f), Color.White);
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x001311FC File Offset: 0x0012F3FC
		public void drawData()
		{
			MainMenu.mybuilder.Length = 0;
			MainMenu.mybuilder.Append("Current Graphics");
			this.spriteBatch.DrawString(this.font2, MainMenu.mybuilder, new Vector2(30f, 50f), Color.White);
			MainMenu.mybuilder.Length = 0;
			MainMenu.mybuilder.Concat(this.sc.width);
			MainMenu.mybuilder.Append(" x ");
			MainMenu.mybuilder.Concat(this.sc.hite);
			this.spriteBatch.DrawString(this.font2, MainMenu.mybuilder, new Vector2(30f, 90f), Color.White);
			MainMenu.mybuilder.Length = 0;
			MainMenu.mybuilder.Append("AntiAlias ");
			MainMenu.mybuilder.Concat(this.sc.aliasing);
			this.spriteBatch.DrawString(this.font2, MainMenu.mybuilder, new Vector2(30f, 130f), Color.White);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00131344 File Offset: 0x0012F544
		private void exitGame()
		{
			MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("You Finished?\n", 1);
			messageBoxScreen.Accepted += delegate
			{
				this.abortThread();
				this.UnloadContent();
				base.ScreenManager.exitmyGame();
				this.exitMyGame = true;
			};
			messageBoxScreen.Cancelled += delegate
			{
				this.delayinput = true;
			};
			this.sc.AddScreen(messageBoxScreen, null);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00131398 File Offset: 0x0012F598
		private void CreateSpace(PlayerIndex playerindex)
		{
			this.sc.LoadAstro();
			this.disconnectData();
			this.menuMusic.Stop();
			this.abortThread();
			this.sc.Game.IsMouseVisible = false;
			this.sc.host = true;
			Form form = (Form)Control.FromHandle(this.sc.Game.Window.Handle);
			form.FormClosing -= this.Form1_KeyDown;
			this.sc.LoadAstroParameters();
			this.sc.inSpace = true;
			this.sc.bgindex = 1;
			this.sc.loadflag = 0;
			LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
			{
				new GameplayScreen(1f)
			});
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00131470 File Offset: 0x0012F670
		private void CreateSession(PlayerIndex playerindex)
		{
			this.sc.errorMessageTimer = 230;
			this.sc.errorMessage = "starting game . . .";
			this.sc.resetGame();
			this.sc.LoadGame(true);
			this.sc.LoadPrefs();
			this.sc.currentDay = this.sc.curDay;
			this.disconnectData();
			this.sc.gameState = 0;
			this.sc.gameSpectate = 1;
			this.sc.showTunnels = true;
			this.sc.gameSeed = this.rr.Next(1, 5000);
			this.menuMusic.Stop();
			this.abortThread();
			this.sc.Game.IsMouseVisible = false;
			this.sc.host = true;
			this.sc.playerindex = playerindex;
			Form form = (Form)Control.FromHandle(this.sc.Game.Window.Handle);
			form.FormClosing -= this.Form1_KeyDown;
			LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
			{
				new BloodnBacon(null)
			});
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x001315B0 File Offset: 0x0012F7B0
		private void CreateCoopSession(PlayerIndex playerindex)
		{
			if (!(this.sc.lobby.players == "4") && !(this.sc.lobby.players == "2") && !(this.sc.lobby.players == "6") && !(this.sc.lobby.players == "3"))
			{
				this.sc.abort.Play(this.sc.ev, 0f, 0f);
				return;
			}
			this.sc.showTunnels = false;
			this.sc.resetGame();
			this.sc.LoadGame(true);
			this.sc.LoadPrefs();
			this.sc.currentDay = this.sc.curDay;
			this.sc.hordemode = false;
			this.sc.paintland = false;
			if (this.sc.lobby.players == "6")
			{
				this.sc.hordemode = true;
			}
			if (this.sc.lobby.players == "3")
			{
				this.sc.paintland = true;
			}
			this.menuMusic.Stop();
			this.abortThread();
			this.sc.tempRifle = 2;
			this.sc.tempPistol = 2;
			this.sc.tempAmmo = 0;
			this.sc.tempMag = 0;
			this.sc.gameState = 0;
			this.sc.gameSpectate = 0;
			this.sc.gameSeed = this.rr.Next(1, 5000);
			this.sc.Game.IsMouseVisible = false;
			this.sc.host = true;
			this.disconnectData();
			Form form = (Form)Control.FromHandle(this.sc.Game.Window.Handle);
			form.FormClosing -= this.Form1_KeyDown;
			if (this.sc.lobby.players == "3")
			{
				this.sc.currentDay = 1;
				LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
				{
					new BloodnBacon3P(null)
				});
			}
			if (this.sc.lobby.players == "6")
			{
				LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
				{
					new BloodnBacon6P(null)
				});
			}
			if (this.sc.lobby.players == "2")
			{
				this.sc.showTunnels = true;
				this.sc.totalPlayers = 2;
				LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
				{
					new BloodnBacon4PT(null)
				});
			}
			if (this.sc.lobby.players == "4")
			{
				if (this.sc.lobby.ver == this.sc.lobby.version4Tunnel.ToString())
				{
					this.sc.tada3.Play(this.sc.ev, 0f, 0f);
					this.sc.showTunnels = true;
					this.sc.totalPlayers = 4;
					LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
					{
						new BloodnBacon4PT(null)
					});
					return;
				}
				this.sc.showTunnels = true;
				this.sc.totalPlayers = 4;
				LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
				{
					new BloodnBacon4PT(null)
				});
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x001319A8 File Offset: 0x0012FBA8
		private void JoinCoopSession(PlayerIndex playerindex, int day)
		{
			if (!(this.sc.lobby.lobbyplayers == "4") && !(this.sc.lobby.lobbyplayers == "2") && !(this.sc.lobby.lobbyplayers == "6") && !(this.sc.lobby.lobbyplayers == "3"))
			{
				this.sc.abort.Play(this.sc.ev, 0f, 0f);
				return;
			}
			this.sc.showTunnels = false;
			this.sc.hordemode = false;
			this.sc.paintland = false;
			if (this.sc.lobby.lobbyplayers == "6")
			{
				this.sc.hordemode = true;
			}
			if (this.sc.lobby.lobbyplayers == "3")
			{
				this.sc.paintland = true;
			}
			this.sc.resetGame();
			this.sc.LoadGame(true);
			this.sc.LoadPrefs();
			this.sc.currentDay = day;
			this.sc.gameState = 0;
			this.sc.gameSpectate = 0;
			this.sc.gameSeed = this.rr.Next(1, 5000);
			this.sc.tempRifle = 2;
			this.sc.tempPistol = 2;
			this.sc.tempAmmo = 0;
			this.sc.tempMag = 0;
			this.menuMusic.Stop();
			this.abortThread();
			this.disconnectData();
			this.sc.Game.IsMouseVisible = false;
			this.sc.host = false;
			Form form = (Form)Control.FromHandle(this.sc.Game.Window.Handle);
			form.FormClosing -= this.Form1_KeyDown;
			if (this.sc.lobby.lobbyplayers == "3")
			{
				this.sc.totalPlayers = 3;
				this.sc.currentDay = 1;
				LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
				{
					new BloodnBacon3P(null)
				});
			}
			if (this.sc.lobby.lobbyplayers == "6")
			{
				this.sc.totalPlayers = 6;
				LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
				{
					new BloodnBacon6P(null)
				});
			}
			if (this.sc.lobby.lobbyplayers == "4")
			{
				if (this.sc.lobby.lobbyVersion == this.sc.lobby.version4Tunnel.ToString())
				{
					this.sc.totalPlayers = 4;
					this.sc.tada3.Play(this.sc.ev, 0f, 0f);
					this.sc.showTunnels = true;
					LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
					{
						new BloodnBacon4PT(null)
					});
				}
				else
				{
					this.sc.totalPlayers = 4;
					this.sc.showTunnels = true;
					LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
					{
						new BloodnBacon4PT(null)
					});
				}
			}
			if (this.sc.lobby.lobbyplayers == "2")
			{
				this.sc.totalPlayers = 2;
				this.sc.showTunnels = true;
				LoadingScreen1.Load(base.ScreenManager, true, new PlayerIndex?(playerindex), null, new GameScreen[]
				{
					new BloodnBacon4PT(null)
				});
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00131DB0 File Offset: 0x0012FFB0
		public void disconnectData()
		{
			try
			{
				if (SteamAPI.IsSteamRunning())
				{
					CSteamID csteamID = default(CSteamID);
					uint num = 0U;
					while (SteamNetworking.IsP2PPacketAvailable(out num, 0))
					{
						byte[] array = new byte[num];
						uint num2;
						SteamNetworking.ReadP2PPacket(array, num, out num2, out csteamID, 0);
					}
					this.sc.lobby.closeSession(csteamID);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00131E18 File Offset: 0x00130018
		private static float WrapAngle(float radians)
		{
			while (radians < -3.1415927f)
			{
				radians += 6.2831855f;
			}
			while (radians > 3.1415927f)
			{
				radians -= 6.2831855f;
			}
			return radians;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00131E44 File Offset: 0x00130044
		private void farmerManager(string type)
		{
			if (type != "")
			{
				if (type == "wave")
				{
					this.farmerAnim.animList.Clear();
					this.farmerAnim.animList.Add(25);
					this.farmerAnim.animList.Add(26);
					this.farmerAnim.animList.Add(27);
					this.farmerAnim.animList.Add(28);
					this.farmerAnim.animClip = 1;
					this.farmerAnim.animCount = 0f;
					this.farmerAnim.animMin = 0;
					this.farmerAnim.animMax = 75;
					this.farmerAnim.animTween = 0f;
					this.farmerAnim.animLoop = 0;
					return;
				}
				if (type == "vomit")
				{
					this.farmerAnim.animList.Clear();
					this.farmerAnim.animList.Add(12);
					this.farmerAnim.animList.Add(13);
					this.farmerAnim.animList.Add(14);
					this.farmerAnim.animList.Add(15);
					this.farmerAnim.animList.Add(16);
					this.farmerAnim.animList.Add(17);
					this.farmerAnim.animList.Add(22);
					this.farmerAnim.animList.Add(23);
					this.farmerAnim.animList.Add(24);
					this.farmerAnim.animList.Add(26);
					this.farmerAnim.animList.Add(27);
					this.farmerAnim.animList.Add(28);
					this.farmerAnim.animClip = 6;
					this.farmerAnim.animCount = 0f;
					this.farmerAnim.animMin = 0;
					this.farmerAnim.animMax = 162;
					this.farmerAnim.animTween = 0f;
					this.farmerAnim.animLoop = 0;
					return;
				}
				if (type == "vomit2")
				{
					this.farmerAnim.animList.Clear();
					this.farmerAnim.animList.Add(12);
					this.farmerAnim.animList.Add(13);
					this.farmerAnim.animList.Add(14);
					this.farmerAnim.animList.Add(15);
					this.farmerAnim.animList.Add(16);
					this.farmerAnim.animList.Add(17);
					this.farmerAnim.animList.Add(22);
					this.farmerAnim.animList.Add(23);
					this.farmerAnim.animList.Add(24);
					this.farmerAnim.animList.Add(26);
					this.farmerAnim.animList.Add(27);
					this.farmerAnim.animList.Add(28);
					this.farmerAnim.animClip = 7;
					this.farmerAnim.animCount = 0f;
					this.farmerAnim.animMin = 0;
					this.farmerAnim.animMax = 87;
					this.farmerAnim.animTween = 0f;
					this.farmerAnim.animLoop = 0;
					return;
				}
				if (type == "convulse1")
				{
					this.farmerAnim.animList.Clear();
					this.farmerAnim.animList.Add(2);
					this.farmerAnim.animList.Add(3);
					this.farmerAnim.animList.Add(4);
					this.farmerAnim.animList.Add(7);
					this.farmerAnim.animList.Add(8);
					this.farmerAnim.animList.Add(9);
					this.farmerAnim.animList.Add(12);
					this.farmerAnim.animList.Add(13);
					this.farmerAnim.animList.Add(14);
					this.farmerAnim.animList.Add(15);
					this.farmerAnim.animList.Add(16);
					this.farmerAnim.animList.Add(22);
					this.farmerAnim.animList.Add(23);
					this.farmerAnim.animList.Add(24);
					this.farmerAnim.animList.Add(26);
					this.farmerAnim.animList.Add(27);
					this.farmerAnim.animList.Add(28);
					this.farmerAnim.animClip = 5;
					this.farmerAnim.animCount = 0f;
					this.farmerAnim.animMin = 0;
					this.farmerAnim.animMax = 210;
					this.farmerAnim.animTween = 0f;
					this.farmerAnim.animLoop = 0;
					return;
				}
				if (type == "kick")
				{
					this.farmerAnim.animList.Clear();
					this.farmerAnim.animList.Add(7);
					this.farmerAnim.animList.Add(8);
					this.farmerAnim.animList.Add(9);
					this.farmerAnim.animClip = 4;
					this.farmerAnim.animCount = 0f;
					this.farmerAnim.animMin = 0;
					this.farmerAnim.animMax = 150;
					this.farmerAnim.animTween = 0f;
					this.farmerAnim.animLoop = 0;
					return;
				}
				if (type == "pat")
				{
					this.farmerAnim.animList.Clear();
					this.farmerAnim.animList.Add(14);
					this.farmerAnim.animList.Add(22);
					this.farmerAnim.animList.Add(23);
					this.farmerAnim.animList.Add(24);
					this.farmerAnim.animList.Add(26);
					this.farmerAnim.animList.Add(27);
					this.farmerAnim.animList.Add(28);
					this.farmerAnim.animClip = 2;
					this.farmerAnim.animCount = 0f;
					this.farmerAnim.animMin = 0;
					this.farmerAnim.animMax = 150;
					this.farmerAnim.animTween = 0f;
					this.farmerAnim.animLoop = 0;
					return;
				}
				if (type == "point1")
				{
					this.farmerAnim.animList.Clear();
					this.farmerAnim.animList.Add(22);
					this.farmerAnim.animList.Add(23);
					this.farmerAnim.animList.Add(24);
					this.farmerAnim.animList.Add(26);
					this.farmerAnim.animList.Add(27);
					this.farmerAnim.animList.Add(28);
					this.farmerAnim.animClip = 8;
					this.farmerAnim.animCount = 0f;
					this.farmerAnim.animMin = 0;
					this.farmerAnim.animMax = 200;
					this.farmerAnim.animTween = 0f;
					this.farmerAnim.animLoop = 0;
					return;
				}
				if (type == "clap")
				{
					this.farmerAnim.animList.Clear();
					this.farmerAnim.animList.Add(21);
					this.farmerAnim.animList.Add(22);
					this.farmerAnim.animList.Add(23);
					this.farmerAnim.animList.Add(24);
					this.farmerAnim.animList.Add(25);
					this.farmerAnim.animList.Add(26);
					this.farmerAnim.animList.Add(27);
					this.farmerAnim.animList.Add(28);
					this.farmerAnim.animClip = 9;
					this.farmerAnim.animCount = 0f;
					this.farmerAnim.animMin = 0;
					this.farmerAnim.animMax = 200;
					this.farmerAnim.animTween = 0f;
					this.farmerAnim.animLoop = 0;
					return;
				}
				if (type == "head")
				{
					this.farmerAnim.animList.Clear();
					this.farmerAnim.animList.Add(21);
					this.farmerAnim.animList.Add(22);
					this.farmerAnim.animList.Add(23);
					this.farmerAnim.animList.Add(24);
					this.farmerAnim.animList.Add(25);
					this.farmerAnim.animList.Add(26);
					this.farmerAnim.animList.Add(27);
					this.farmerAnim.animList.Add(28);
					this.farmerAnim.animClip = 10;
					this.farmerAnim.animCount = 0f;
					this.farmerAnim.animMin = 0;
					this.farmerAnim.animMax = 102;
					this.farmerAnim.animTween = 0f;
					this.farmerAnim.animLoop = 0;
					return;
				}
				if (type == "nose")
				{
					this.farmerAnim.animList.Clear();
					this.farmerAnim.animList.Add(22);
					this.farmerAnim.animList.Add(23);
					this.farmerAnim.animList.Add(24);
					this.farmerAnim.animClip = 3;
					this.farmerAnim.animCount = 0f;
					this.farmerAnim.animMin = 0;
					this.farmerAnim.animMax = 95;
					this.farmerAnim.animTween = 0f;
					this.farmerAnim.animLoop = 0;
				}
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00132884 File Offset: 0x00130A84
		private void farmerBones(ref AnimationPlayer a)
		{
			this.talkSmooth = 0f;
			float num = 5f;
			if (this.farmerJawIndex < 0)
			{
				if (this.farmerAnim.animClip == -1)
				{
					this.farmerGlitchCount--;
				}
				if (this.farmerAnim.animClip == -1 && this.farmerGlitchCount < 0)
				{
					this.farmerGlitchIndex = this.rr.Next(0, 4);
					if (this.farmerGlitchIndex > 4)
					{
						this.farmerGlitchIndex = 0;
					}
					if (this.farmerGlitchIndex == 0)
					{
						this.farmerManager("wave");
					}
					if (this.farmerGlitchIndex == 1)
					{
						this.farmerManager("nose");
					}
					if (this.farmerGlitchIndex == 3)
					{
						this.farmerManager("kick");
					}
					this.farmerGlitchCount = this.rr.Next(100, 500);
				}
			}
			if (this.farmerJawIndex >= 0 && this.farmerDialog1Loaded)
			{
				this.farmerJawIndex++;
				this.directorManager();
				this.talkAverage = this.farmerJaw[this.farmerJawIndex] % 1000f;
				if (this.farmerJawIndex > 1 && this.farmerJawIndex < this.farmerJaw.Count - 2)
				{
					this.talkAverage = (this.farmerJaw[this.farmerJawIndex - 1] % 1000f + this.farmerJaw[this.farmerJawIndex] % 1000f + this.farmerJaw[this.farmerJawIndex + 1] % 1000f) / 3f;
				}
				this.talkAverage = MathHelper.Clamp(this.talkAverage, 0f, 30f);
				this.talkSmooth = this.talkAverage;
				if (this.farmerJaw[this.farmerJawIndex] == -1f)
				{
					this.followPoint = true;
					Vector2.Lerp(ref this.pos1, ref this.pos2, this.followTween, out this.lastlookpos);
					this.lookIndex = 0;
					this.followTween = 1f;
					this.headTween = 1f;
					this.farmerJawIndex = -1;
					this.talkIndex = -1;
					this.talkSmooth = 0f;
					this.resetImages();
				}
			}
			this.followTimer -= 1f;
			if (this.followTimer <= 0f && !this.followPoint)
			{
				this.followTimer = 0f;
				this.followPoint = true;
				this.lastlookpos = this.sc.adjustVector2(this.sc.mymouse);
				this.followTween = 1f;
				this.lookIndex = 0;
			}
			this.adj = this.sc.adjustVector2(this.sc.mymouse);
			if (this.followPoint)
			{
				this.adj = this.lookpos[this.lookIndex];
			}
			this.pos1 = this.adj;
			this.adj.X = ((this.adj.X + 250f) / 1280f - 0.5f) * 2.8f;
			this.adj.X = MathHelper.Clamp(this.adj.X, -1.4f, 0.9f);
			this.adj.Y = ((this.adj.Y + 120f) / 720f - 0.5f) * 2f;
			this.adj.Y = MathHelper.Clamp(this.adj.Y, -0.75f, 0.4f);
			this.basePos = this.lastlookpos;
			this.pos2 = this.basePos;
			this.basePos.X = ((this.basePos.X + 240f) / 1280f - 0.5f) * 2.8f;
			this.basePos.X = MathHelper.Clamp(this.basePos.X, -1.4f, 0.9f);
			this.basePos.Y = ((this.basePos.Y + 100f) / 720f - 0.5f) * 2f;
			this.basePos.Y = MathHelper.Clamp(this.basePos.Y, -0.75f, 0.4f);
			this.followTween -= 0.05f;
			if (this.followTween <= 0f)
			{
				this.followTween = 0f;
			}
			this.headTween -= 0.1f;
			if (this.headTween <= 0f)
			{
				this.headTween = 0f;
			}
			this.desiredAngle = MathHelper.Lerp(this.adj.X, this.basePos.X, this.followTween);
			this.tiltDown = MathHelper.Lerp(this.adj.Y, this.basePos.Y, this.followTween);
			this.farmerFrame1 += 0.4f;
			double num2 = (double)(this.farmerFrame1 * 0.0417f);
			this.currentTimeValue = TimeSpan.FromSeconds(num2 % a.currentClipValue.Duration.TotalSeconds);
			this.currentKeyframe = (int)(this.currentTimeValue.TotalSeconds * 60.0) * 28;
			this.currentTimeValue += TimeSpan.FromSeconds(0.041999999433755875);
			a.UpdateBoneTransforms2(this.currentKeyframe, this.currentTimeValue);
			a.boneTransforms[19] = Matrix.CreateRotationZ(MathHelper.ToRadians(num - this.talkSmooth)) * a.boneTransforms[19];
			this.headMatrix = Matrix.CreateRotationY(this.desiredAngle) * a.boneTransforms[16];
			this.headMatrix = Matrix.CreateRotationX(this.tiltDown) * this.headMatrix;
			a.boneTransforms[16] = Matrix.Lerp(this.headMatrix, this.lastheadMatrix, this.headTween);
			this.lastheadMatrix = a.boneTransforms[16];
			if (this.farmerAnim.animCount > -1f)
			{
				num2 = (double)(this.farmerAnim.animCount * 0.4f * 0.0417f);
				this.currentTimeValue = TimeSpan.FromSeconds(num2 % this.farmer1[this.farmerAnim.animClip].currentClipValue.Duration.TotalSeconds);
				this.currentKeyframe = (int)(this.currentTimeValue.TotalSeconds * 60.0) * 28;
				this.currentTimeValue += TimeSpan.FromSeconds(0.041999999433755875);
				this.farmer1[this.farmerAnim.animClip].UpdateBoneTransforms2(this.currentKeyframe, this.currentTimeValue);
				this.farmerAnim.animTween = MathHelper.Clamp(this.farmerAnim.animTween, 0f, 1f);
				for (int i = 0; i < this.farmerAnim.animList.Count; i++)
				{
					a.boneTransforms[this.farmerAnim.animList[i]] = a.boneTransforms[this.farmerAnim.animList[i]] * (1f - this.farmerAnim.animTween) + this.farmer1[this.farmerAnim.animClip].boneTransforms[this.farmerAnim.animList[i]] * this.farmerAnim.animTween;
				}
				this.farmerAnim.animCount = this.farmerAnim.animCount + 1f;
				if (this.farmerAnim.animCount < (float)(this.farmerAnim.animMin + 7))
				{
					this.farmerAnim.animTween = this.farmerAnim.animTween + 0.16666667f;
				}
				if (this.farmerAnim.animCount > (float)(this.farmerAnim.animMax - 7))
				{
					this.farmerAnim.animTween = this.farmerAnim.animTween - 0.16666667f;
				}
				if (this.farmerAnim.animCount > (float)this.farmerAnim.animMax)
				{
					if (this.farmerAnim.animLoop == 0)
					{
						this.farmerAnim.animCount = -1f;
						this.farmerAnim.animTween = 0f;
						this.farmerAnim.animClip = -1;
					}
					else
					{
						this.farmerAnim.animCount = (float)this.farmerAnim.animMin;
						this.farmerAnim.animTween = 0f;
						this.farmerAnim.animLoop = this.farmerAnim.animLoop - 1;
					}
				}
			}
			a.UpdateWorldTransforms(Matrix.CreateScale(2f) * Matrix.CreateRotationY(1.57f) * Matrix.CreateTranslation(0f, 20f, 30f), a.boneTransforms);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x001331B4 File Offset: 0x001313B4
		private void removeTrack(int t)
		{
			this.sc.tick.Play(this.sc.ev, -1f, 0f);
			this.sc.tick.Play(this.sc.ev, -1f, 0f);
			if (t == 100)
			{
				for (int i = 0; i < this.farmerJaw.Count; i++)
				{
					if (this.farmerJaw[i] >= 1000f && this.farmerJaw[i] < 10999f)
					{
						this.farmerJaw[i] = this.farmerJaw[i] % 1000f;
					}
				}
			}
			if (t == 101)
			{
				for (int j = 0; j < this.farmerJaw.Count; j++)
				{
					if (this.farmerJaw[j] >= 11000f && this.farmerJaw[j] < 20999f)
					{
						this.farmerJaw[j] = this.farmerJaw[j] % 1000f;
					}
				}
			}
			if (t == 102)
			{
				for (int k = 0; k < this.farmerJaw.Count; k++)
				{
					if (this.farmerJaw[k] >= 21000f && this.farmerJaw[k] < 30999f)
					{
						this.farmerJaw[k] = this.farmerJaw[k] % 1000f;
					}
				}
			}
			if (t >= 0 && t <= 8)
			{
				for (int l = 0; l < this.farmerJaw.Count; l++)
				{
					if (this.farmerJaw[l] >= (float)(51000 + t * 1000) && this.farmerJaw[l] < (float)(51999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(61000 + t * 1000) && this.farmerJaw[l] < (float)(61999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(71000 + t * 1000) && this.farmerJaw[l] < (float)(71999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(81000 + t * 1000) && this.farmerJaw[l] < (float)(81999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(201000 + t * 1000) && this.farmerJaw[l] < (float)(201999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(211000 + t * 1000) && this.farmerJaw[l] < (float)(211999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(221000 + t * 1000) && this.farmerJaw[l] < (float)(221999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(231000 + t * 1000) && this.farmerJaw[l] < (float)(231999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(241000 + t * 1000) && this.farmerJaw[l] < (float)(241999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(251000 + t * 1000) && this.farmerJaw[l] < (float)(251999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(261000 + t * 1000) && this.farmerJaw[l] < (float)(261999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(271000 + t * 1000) && this.farmerJaw[l] < (float)(271999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(281000 + t * 1000) && this.farmerJaw[l] < (float)(281999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(291000 + t * 1000) && this.farmerJaw[l] < (float)(291999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(111000 + t * 1000) && this.farmerJaw[l] < (float)(111999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(121000 + t * 1000) && this.farmerJaw[l] < (float)(121999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(131000 + t * 1000) && this.farmerJaw[l] < (float)(131999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(141000 + t * 1000) && this.farmerJaw[l] < (float)(141999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(151000 + t * 1000) && this.farmerJaw[l] < (float)(151999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(161000 + t * 1000) && this.farmerJaw[l] < (float)(161999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(171000 + t * 1000) && this.farmerJaw[l] < (float)(171999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
					if (this.farmerJaw[l] >= (float)(181000 + t * 1000) && this.farmerJaw[l] < (float)(181999 + t * 1000))
					{
						this.farmerJaw[l] = this.farmerJaw[l] % 1000f;
					}
				}
			}
			this.writeFile();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00133AC0 File Offset: 0x00131CC0
		private void writeFile()
		{
			string text = this.sc.workshop.entry[this.entryIndex].path + "\\report.txt";
			using (StreamWriter streamWriter = new StreamWriter(File.Open(text, FileMode.Create)))
			{
				for (int i = 0; i < this.farmerJaw.Count; i++)
				{
					streamWriter.WriteLine(this.farmerJaw[i]);
				}
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00133B4C File Offset: 0x00131D4C
		private void directorManager()
		{
			int num = -1;
			if (this.directNum > 999 && this.farmerJaw[this.farmerJawIndex] < 1000f && this.farmerJawIndex < this.farmerJaw.Count - 2)
			{
				if (this.directNum >= 51000 && this.directNum <= 60000 && (this.farmerJaw[this.farmerJawIndex + 1] > 999f || this.farmerJaw[this.farmerJawIndex + 2] > 999f))
				{
					num = -1;
				}
				else
				{
					if (this.directNum >= 51000 && this.directNum <= 60000)
					{
						if (this.directRate == 1)
						{
							this.directNum += 10000;
							this.sc.tick.Play(this.sc.ev, 1f, 0f);
						}
						if (this.directRate == 2)
						{
							this.directNum += 20000;
							this.sc.tick.Play(this.sc.ev, -1f, 0f);
						}
					}
					this.farmerJaw[this.farmerJawIndex] = this.farmerJaw[this.farmerJawIndex] % 1000f + (float)this.directNum;
					this.sc.workshop.entry[this.entryIndex].motion[this.farmerJawIndex] = this.farmerJaw[this.farmerJawIndex];
					if (this.directNum >= 51000 && this.directNum <= 80000 && this.farmerJawIndex < this.farmerJaw.Count - 2)
					{
						int num2 = this.farmerJawIndex + 1;
						int num3 = this.farmerJawIndex + 2;
						this.farmerJaw[num2] = this.farmerJaw[num2] % 1000f + (float)(this.directNumX * 1000);
						this.sc.workshop.entry[this.entryIndex].motion[num2] = this.farmerJaw[num2];
						this.farmerJaw[num3] = this.farmerJaw[num3] % 1000f + (float)(this.directNumY * 1000);
						this.sc.workshop.entry[this.entryIndex].motion[num3] = this.farmerJaw[num3];
					}
				}
			}
			if (this.farmerJaw[this.farmerJawIndex] > 999f)
			{
				num = (int)this.farmerJaw[this.farmerJawIndex] / 1000;
			}
			if (num >= 1 && num <= 10)
			{
				this.followPoint = true;
				this.headTween = 1f;
				Vector2.Lerp(ref this.pos1, ref this.pos2, this.followTween, out this.lastlookpos);
				if (num == 1)
				{
					this.lookPosRR = new Vector2((float)this.rr.Next(-30, 30), (float)this.rr.Next(-30, 30));
					this.lookpos[0] = this.lookposOrig[0] + this.lookPosRR;
					this.lookIndex = 0;
					this.followTween = 1f;
				}
				if (num == 2)
				{
					this.lookPosRR = new Vector2((float)this.rr.Next(-50, 50), (float)this.rr.Next(10, 250));
					this.lookpos[1] = this.lookposOrig[1] + this.lookPosRR;
					this.lookIndex = 1;
					this.followTween = 1f;
				}
				if (num == 3)
				{
					this.lookPosRR = new Vector2((float)this.rr.Next(-100, 100), (float)this.rr.Next(-100, 100));
					this.lookpos[2] = this.lookposOrig[2] + this.lookPosRR;
					this.lookIndex = 2;
					this.followTween = 1f;
				}
				if (num == 4)
				{
					this.lookPosRR = new Vector2((float)this.rr.Next(-100, 100), (float)this.rr.Next(-50, 50));
					this.lookpos[3] = this.lookposOrig[3] + this.lookPosRR;
					this.lookIndex = 3;
					this.followTween = 1f;
				}
				if (num == 5)
				{
					this.lookPosRR = new Vector2((float)this.rr.Next(-100, 100), (float)this.rr.Next(-100, 100));
					this.lookpos[4] = this.lookposOrig[4] + this.lookPosRR;
					this.lookIndex = 4;
					this.followTween = 1f;
				}
				if (num == 6)
				{
					this.lookPosRR = new Vector2((float)this.rr.Next(-150, 150), (float)this.rr.Next(-100, 100));
					this.lookpos[5] = this.lookposOrig[5] + this.lookPosRR;
					this.lookIndex = 5;
					this.followTween = 1f;
				}
				if (num == 7)
				{
					this.lookPosRR = new Vector2((float)this.rr.Next(-150, 150), (float)this.rr.Next(-150, 150));
					this.lookpos[6] = this.lookposOrig[6] + this.lookPosRR;
					this.lookIndex = 6;
					this.followTween = 1f;
				}
				return;
			}
			if (num >= 11 && num <= 20)
			{
				if (num == 11)
				{
					this.farmerManager("point1");
				}
				if (num == 12)
				{
					this.farmerManager("clap");
				}
				if (num == 13)
				{
					this.farmerManager("wave");
				}
				if (num == 14)
				{
					this.farmerManager("nose");
				}
				if (num == 15)
				{
					this.farmerManager("kick");
				}
				if (num == 16)
				{
					this.farmerManager("head");
				}
				return;
			}
			if (num >= 21 && num <= 30 && this.entryIndex >= 0)
			{
				try
				{
					if (num == 21)
					{
						this.sc.workshop.entry[this.entryIndex].soundhit[0].Play(this.sc.ev, 0f, 0f);
					}
					if (num == 22)
					{
						this.sc.workshop.entry[this.entryIndex].soundhit[1].Play(this.sc.ev, 0f, 0f);
					}
					if (num == 23)
					{
						this.sc.workshop.entry[this.entryIndex].soundhit[2].Play(this.sc.ev, 0f, 0f);
					}
					if (num == 24)
					{
						this.sc.workshop.entry[this.entryIndex].soundhit[3].Play(this.sc.ev, 0f, 0f);
					}
					if (num == 25)
					{
						this.sc.workshop.entry[this.entryIndex].soundhit[4].Play(this.sc.ev, 0f, 0f);
					}
					if (num == 26)
					{
						this.sc.workshop.entry[this.entryIndex].soundhit[5].Play(this.sc.ev, 0f, 0f);
					}
					if (num == 27)
					{
						this.sc.workshop.entry[this.entryIndex].soundhit[6].Play(this.sc.ev, 0f, 0f);
					}
					if (num == 28)
					{
						this.sc.workshop.entry[this.entryIndex].soundhit[7].Play(this.sc.ev, 0f, 0f);
					}
					if (num == 29)
					{
						this.sc.workshop.entry[this.entryIndex].soundhit[8].Play(this.sc.ev, 0f, 0f);
					}
					if (num == 30)
					{
						this.sc.workshop.entry[this.entryIndex].soundhit[9].Play(this.sc.ev, 0f, 0f);
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 51 && num <= 60)
			{
				try
				{
					int i = 51;
					while (i <= 60)
					{
						if (num == i)
						{
							int num4 = i - 51;
							this.im[num4].show = true;
							this.im[num4].tween = 0f;
							this.im[num4].tweenRate = 1f;
							this.im[num4].pos = new Vector2(this.farmerJaw[this.farmerJawIndex + 1] / 1000f - 5000f, this.farmerJaw[this.farmerJawIndex + 2] / 1000f - 5000f);
							if (!this.displayList.Contains(num4))
							{
								this.displayList.Insert(0, num4);
								break;
							}
							break;
						}
						else
						{
							i++;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 61 && num <= 70)
			{
				try
				{
					int j = 61;
					while (j <= 70)
					{
						if (num == j)
						{
							int num5 = j - 61;
							this.im[num5].show = true;
							this.im[num5].oldpos = Vector2.Lerp(this.im[num5].oldpos, this.im[num5].pos, this.im[num5].tween);
							this.im[num5].tween = 0f;
							this.im[num5].tweenRate = 0.07f;
							this.im[num5].pos = new Vector2(this.farmerJaw[this.farmerJawIndex + 1] / 1000f - 5000f, this.farmerJaw[this.farmerJawIndex + 2] / 1000f - 5000f);
							if (!this.displayList.Contains(num5))
							{
								this.displayList.Insert(0, num5);
								break;
							}
							break;
						}
						else
						{
							j++;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 71 && num <= 80)
			{
				try
				{
					int k = 71;
					while (k <= 80)
					{
						if (num == k)
						{
							int num6 = k - 71;
							this.im[num6].show = true;
							this.im[num6].oldpos = Vector2.Lerp(this.im[num6].oldpos, this.im[num6].pos, this.im[num6].tween);
							this.im[num6].tween = 0f;
							this.im[num6].tweenRate = 0.01f;
							this.im[num6].pos = new Vector2(this.farmerJaw[this.farmerJawIndex + 1] / 1000f - 5000f, this.farmerJaw[this.farmerJawIndex + 2] / 1000f - 5000f);
							if (!this.displayList.Contains(num6))
							{
								this.displayList.Insert(0, num6);
								break;
							}
							break;
						}
						else
						{
							k++;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 81 && num <= 90)
			{
				try
				{
					for (int l = 81; l <= 90; l++)
					{
						if (num == l)
						{
							int num7 = l - 81;
							this.im[num7].show = false;
							this.displayList.Remove(num7);
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 201 && num <= 210)
			{
				try
				{
					for (int m = 201; m <= 210; m++)
					{
						if (num == m)
						{
							int num8 = m - 201;
							this.im[num8].spriteIndex = 0;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 211 && num <= 220)
			{
				try
				{
					for (int n = 211; n <= 220; n++)
					{
						if (num == n)
						{
							int num9 = n - 211;
							this.im[num9].spriteIndex = 1;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 221 && num <= 230)
			{
				try
				{
					for (int num10 = 221; num10 <= 230; num10++)
					{
						if (num == num10)
						{
							int num11 = num10 - 221;
							this.im[num11].spriteIndex = 2;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 231 && num <= 240)
			{
				try
				{
					for (int num12 = 231; num12 <= 240; num12++)
					{
						if (num == num12)
						{
							int num13 = num12 - 231;
							this.im[num13].spriteIndex = 3;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 241 && num <= 250)
			{
				try
				{
					for (int num14 = 241; num14 <= 250; num14++)
					{
						if (num == num14)
						{
							int num15 = num14 - 241;
							this.im[num15].spriteIndex = 4;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 251 && num <= 260)
			{
				try
				{
					for (int num16 = 251; num16 <= 260; num16++)
					{
						if (num == num16)
						{
							int num17 = num16 - 251;
							this.im[num17].spriteIndex = 5;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 261 && num <= 270)
			{
				try
				{
					for (int num18 = 261; num18 <= 270; num18++)
					{
						if (num == num18)
						{
							int num19 = num18 - 261;
							this.im[num19].spriteIndex = 6;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 271 && num <= 280)
			{
				try
				{
					for (int num20 = 271; num20 <= 280; num20++)
					{
						if (num == num20)
						{
							int num21 = num20 - 271;
							this.im[num21].spriteIndex = 7;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 281 && num <= 290)
			{
				try
				{
					for (int num22 = 281; num22 <= 290; num22++)
					{
						if (num == num22)
						{
							int num23 = num22 - 281;
							this.im[num23].spriteIndex = 8;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 291 && num <= 300)
			{
				try
				{
					for (int num24 = 291; num24 <= 300; num24++)
					{
						if (num == num24)
						{
							int num25 = num24 - 291;
							this.im[num25].spriteIndex = 9;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 111 && num <= 120)
			{
				try
				{
					for (int num26 = 111; num26 <= 120; num26++)
					{
						if (num == num26)
						{
							int num27 = num26 - 111;
							this.im[num27].rot -= 0.2f;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 121 && num <= 130)
			{
				try
				{
					for (int num28 = 121; num28 <= 130; num28++)
					{
						if (num == num28)
						{
							int num29 = num28 - 121;
							this.im[num29].rot += 0.2f;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 131 && num <= 140)
			{
				try
				{
					for (int num30 = 131; num30 <= 140; num30++)
					{
						if (num == num30)
						{
							int num31 = num30 - 131;
							this.im[num31].scale *= 0.66f;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 141 && num <= 150)
			{
				try
				{
					for (int num32 = 141; num32 <= 150; num32++)
					{
						if (num == num32)
						{
							int num33 = num32 - 141;
							this.im[num33].scale *= 1.33f;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 151 && num <= 160)
			{
				try
				{
					for (int num34 = 151; num34 <= 160; num34++)
					{
						if (num == num34)
						{
							int num35 = num34 - 151;
							this.im[num35].flip = SpriteEffects.FlipHorizontally;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 161 && num <= 170)
			{
				try
				{
					for (int num36 = 161; num36 <= 170; num36++)
					{
						if (num == num36)
						{
							int num37 = num36 - 161;
							this.im[num37].flip = SpriteEffects.None;
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 171 && num <= 180)
			{
				try
				{
					for (int num38 = 171; num38 <= 180; num38++)
					{
						if (num == num38)
						{
							int num39 = num38 - 171;
							this.displayList.Remove(num39);
							this.displayList.Insert(0, num39);
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			if (num >= 181 && num <= 190)
			{
				try
				{
					for (int num40 = 181; num40 <= 190; num40++)
					{
						if (num == num40)
						{
							int num41 = num40 - 181;
							this.displayList.Remove(num41);
							this.displayList.Add(num41);
							break;
						}
					}
				}
				catch
				{
				}
				return;
			}
			try
			{
				for (int num42 = 301; num42 <= 310; num42++)
				{
					if (num == num42)
					{
						int num43 = num42 - 301;
						if (this.im[num43].show)
						{
							this.im[num43].show = false;
							this.im[num43].full = false;
							this.displayList.Remove(num43);
						}
						else
						{
							this.im[num43].show = true;
							this.im[num43].full = true;
							this.displayList.Remove(num43);
							for (int num44 = 0; num44 < this.displayList.Count; num44++)
							{
								if (this.im[this.displayList[num44]].full)
								{
									this.displayList.Insert(num44, num43);
									break;
								}
							}
							if (!this.displayList.Contains(num43))
							{
								this.displayList.Add(num43);
							}
						}
						return;
					}
				}
			}
			catch
			{
			}
			for (int num45 = 311; num45 <= 320; num45++)
			{
				if (num == num45)
				{
					this.briteXold = this.briteX;
					this.colorXold = this.colorX;
					this.liteTween = 0f;
					this.briteX = 0;
					this.colorX = num45 - 311;
					return;
				}
			}
			for (int num46 = 321; num46 <= 330; num46++)
			{
				if (num == num46)
				{
					this.briteXold = this.briteX;
					this.colorXold = this.colorX;
					this.liteTween = 0f;
					this.briteX = 1;
					this.colorX = num46 - 321;
					return;
				}
			}
			for (int num47 = 331; num47 <= 340; num47++)
			{
				if (num == num47)
				{
					this.briteXold = this.briteX;
					this.colorXold = this.colorX;
					this.liteTween = 0f;
					this.briteX = 2;
					this.colorX = num47 - 331;
					return;
				}
			}
			for (int num48 = 341; num48 <= 350; num48++)
			{
				if (num == num48)
				{
					this.briteXold = this.briteX;
					this.colorXold = this.colorX;
					this.liteTween = 0f;
					this.briteX = 3;
					this.colorX = num48 - 341;
					return;
				}
			}
			for (int num49 = 351; num49 <= 360; num49++)
			{
				if (num == num49)
				{
					this.briteXold = this.briteX;
					this.colorXold = this.colorX;
					this.liteTween = 0f;
					this.briteX = 4;
					this.colorX = num49 - 351;
					return;
				}
			}
			for (int num50 = 361; num50 <= 370; num50++)
			{
				if (num == num50)
				{
					this.briteXold = this.briteX;
					this.colorXold = this.colorX;
					this.liteTween = 0f;
					this.briteX = 5;
					this.colorX = num50 - 361;
					return;
				}
			}
			for (int num51 = 371; num51 <= 380; num51++)
			{
				if (num == num51)
				{
					this.briteXold = this.briteX;
					this.colorXold = this.colorX;
					this.liteTween = 0f;
					this.briteX = 6;
					this.colorX = num51 - 371;
					return;
				}
			}
			for (int num52 = 381; num52 <= 390; num52++)
			{
				if (num == num52)
				{
					this.briteXold = this.briteX;
					this.colorXold = this.colorX;
					this.liteTween = 0f;
					this.briteX = 7;
					this.colorX = num52 - 381;
					return;
				}
			}
			for (int num53 = 391; num53 <= 400; num53++)
			{
				if (num == num53)
				{
					this.briteXold = this.briteX;
					this.colorXold = this.colorX;
					this.liteTween = 0f;
					this.briteX = 8;
					this.colorX = num53 - 391;
					return;
				}
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x001354F0 File Offset: 0x001336F0
		private void farmerTalk()
		{
			this.farmerDialog1Loaded = false;
			try
			{
				this.farmerDialog1.sound[0].Dispose();
			}
			catch
			{
				this.farmerDialog1Loaded = false;
			}
			try
			{
				bool flag = this.sc.workshop.populateDiaryContents(this.sc.workshop.entryIndex);
				if (flag)
				{
					this.farmerDialog1 = new SoundEffects(1);
					this.farmerDialog1.sound[0] = this.sc.workshop.entry[this.sc.workshop.entryIndex].voice.CreateInstance();
					this.farmerJaw.Clear();
					for (int i = 0; i < this.sc.workshop.entry[this.sc.workshop.entryIndex].motion.Count - 1; i++)
					{
						this.farmerJaw.Add(this.sc.workshop.entry[this.sc.workshop.entryIndex].motion[i]);
					}
					this.farmerJaw.Add(-1f);
					this.farmerDialog1.count = 0;
					this.farmerDialog1.sound[0].Play();
					this.farmerDialog1.sound[0].Volume = this.sc.vv;
					this.farmerDialog1Loaded = true;
					this.farmerJawIndex = 1;
				}
				else
				{
					this.farmerDialog1Loaded = false;
				}
			}
			catch
			{
				this.farmerDialog1Loaded = false;
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x001356B8 File Offset: 0x001338B8
		private void drawFarmer()
		{
			Vector3 vector = this.LightDirectionX[this.briteX];
			Vector3 vector2 = this.DiffuseLightX[this.colorX];
			Vector3 vector3 = this.AmbientLightX[this.colorX];
			if (this.liteTween <= 1f)
			{
				this.liteTween += 0.02f;
				vector = Vector3.Lerp(this.LightDirectionX[this.briteXold], this.LightDirectionX[this.briteX], this.liteTween);
				vector2 = Vector3.Lerp(this.DiffuseLightX[this.colorXold], this.DiffuseLightX[this.colorX], this.liteTween);
				vector3 = Vector3.Lerp(this.AmbientLightX[this.colorXold], this.AmbientLightX[this.colorX], this.liteTween);
			}
			Matrix matrix = Matrix.CreateLookAt(new Vector3(120f, 90f, 40f), new Vector3(0f, 90f, -10f), Vector3.Up);
			Matrix matrix2 = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(50f), 1.78f, 10f, 15000f);
			this.farmerModel.Meshes[0].MeshParts[0].Effect = this.farmerSkin;
			this.farmerSkin.Parameters["View"].SetValue(matrix);
			this.farmerSkin.Parameters["Projection"].SetValue(matrix2);
			this.farmerSkin.Parameters["Bones"].SetValue(this.farmer1[0].skinTransforms);
			this.farmerSkin.Parameters["LightDirection"].SetValue(Vector3.Normalize(vector));
			this.farmerSkin.Parameters["AmbientLight"].SetValue(vector3);
			this.farmerSkin.Parameters["DiffuseLight"].SetValue(vector2);
			this.farmerSkin.Parameters["flash"].SetValue(Vector3.Zero);
			this.farmerSkin.CurrentTechnique = this.farmerSkin.Techniques["SkinnedEffect"];
			this.farmerModel.Meshes[0].Draw();
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00135957 File Offset: 0x00133B57
		private void resetFarmerDirector()
		{
			this.followPoint = true;
			this.followTimer = 0f;
			this.farmerJawIndex = -1;
			this.talkIndex = -1;
			this.talkSmooth = 0f;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00135984 File Offset: 0x00133B84
		private void resetImages()
		{
			this.im.Clear();
			for (int i = 0; i < 10; i++)
			{
				this.im.Add(new MainMenu.imim());
				this.im[i].spriteIndex = i;
			}
			this.displayList.Clear();
			this.briteXold = this.briteX;
			this.colorXold = this.colorX;
			this.briteX = 4;
			this.colorX = 4;
			this.liteTween = 0f;
		}

		// Token: 0x0400143B RID: 5179
		private bool newpage = true;

		// Token: 0x0400143C RID: 5180
		private bool drawsubs;

		// Token: 0x0400143D RID: 5181
		private bool drawcustom;

		// Token: 0x0400143E RID: 5182
		private int xx = 57;

		// Token: 0x0400143F RID: 5183
		private int yy = 48;

		// Token: 0x04001440 RID: 5184
		private Rectangle[] myRect;

		// Token: 0x04001441 RID: 5185
		private int pigcount;

		// Token: 0x04001442 RID: 5186
		private int pigwalk;

		// Token: 0x04001443 RID: 5187
		private List<string> me = new List<string>();

		// Token: 0x04001444 RID: 5188
		private bool coopAlpha = true;

		// Token: 0x04001445 RID: 5189
		private bool paintunlocked = true;

		// Token: 0x04001446 RID: 5190
		private bool downloadAudio;

		// Token: 0x04001447 RID: 5191
		private bool checkTrophyOnce;

		// Token: 0x04001448 RID: 5192
		private bool jumptomain;

		// Token: 0x04001449 RID: 5193
		private string lobbynum = "";

		// Token: 0x0400144A RID: 5194
		private bool noMovies;

		// Token: 0x0400144B RID: 5195
		private bool showHelp;

		// Token: 0x0400144C RID: 5196
		private bool showGamepad;

		// Token: 0x0400144D RID: 5197
		private bool showInstruct;

		// Token: 0x0400144E RID: 5198
		private bool covered;

		// Token: 0x0400144F RID: 5199
		private int whichKEY;

		// Token: 0x04001450 RID: 5200
		private int thisKEY;

		// Token: 0x04001451 RID: 5201
		private int whichbutton;

		// Token: 0x04001452 RID: 5202
		private int lastbutton;

		// Token: 0x04001453 RID: 5203
		private int highlight;

		// Token: 0x04001454 RID: 5204
		private int highlighttimer;

		// Token: 0x04001455 RID: 5205
		private bool mousemoving;

		// Token: 0x04001456 RID: 5206
		private bool delayinput;

		// Token: 0x04001457 RID: 5207
		private bool mouseback;

		// Token: 0x04001458 RID: 5208
		private bool mousepress;

		// Token: 0x04001459 RID: 5209
		private bool mousePressHold;

		// Token: 0x0400145A RID: 5210
		private bool mousemiddle;

		// Token: 0x0400145B RID: 5211
		private bool mousenum1;

		// Token: 0x0400145C RID: 5212
		private bool mousenum2;

		// Token: 0x0400145D RID: 5213
		private int[] menuArray = new int[]
		{
			0, 6, 1, 8, 2, 9, 3, 10, 4, 7,
			5
		};

		// Token: 0x0400145E RID: 5214
		private int itemSelect;

		// Token: 0x0400145F RID: 5215
		private int lastselect;

		// Token: 0x04001460 RID: 5216
		private int myIndex;

		// Token: 0x04001461 RID: 5217
		private Rectangle sliderBoxRect = new Rectangle(575, 547, 34, 46);

		// Token: 0x04001462 RID: 5218
		private Rectangle sliderGlowRect = new Rectangle(634, 547, 34, 46);

		// Token: 0x04001463 RID: 5219
		private Rectangle sliderRect = new Rectangle(486, 631, 320, 14);

		// Token: 0x04001464 RID: 5220
		private float origMouseX;

		// Token: 0x04001465 RID: 5221
		private Vector2 origMouseXY;

		// Token: 0x04001466 RID: 5222
		private Vector2 origPlaque;

		// Token: 0x04001467 RID: 5223
		private float nowMouseX;

		// Token: 0x04001468 RID: 5224
		private float sliderbox1;

		// Token: 0x04001469 RID: 5225
		private float sliderbox1a;

		// Token: 0x0400146A RID: 5226
		private float sliderbox2;

		// Token: 0x0400146B RID: 5227
		private float sliderbox2a;

		// Token: 0x0400146C RID: 5228
		private float sliderbox3;

		// Token: 0x0400146D RID: 5229
		private float sliderbox3a;

		// Token: 0x0400146E RID: 5230
		private float sliderbox4;

		// Token: 0x0400146F RID: 5231
		private float sliderbox4a;

		// Token: 0x04001470 RID: 5232
		private int updownIndex;

		// Token: 0x04001471 RID: 5233
		private int slider1;

		// Token: 0x04001472 RID: 5234
		private int slider2;

		// Token: 0x04001473 RID: 5235
		private int slider3;

		// Token: 0x04001474 RID: 5236
		private int slider4;

		// Token: 0x04001475 RID: 5237
		private int slider5;

		// Token: 0x04001476 RID: 5238
		public int zoomX;

		// Token: 0x04001477 RID: 5239
		public int zoomY;

		// Token: 0x04001478 RID: 5240
		private Rectangle formCorner1 = new Rectangle(710, 75, 34, 35);

		// Token: 0x04001479 RID: 5241
		private Rectangle formCorner2 = new Rectangle(929, 75, 34, 35);

		// Token: 0x0400147A RID: 5242
		private Rectangle formCorner3 = new Rectangle(929, 291, 34, 35);

		// Token: 0x0400147B RID: 5243
		private Rectangle formCorner4 = new Rectangle(710, 291, 34, 35);

		// Token: 0x0400147C RID: 5244
		private Rectangle formLeft = new Rectangle(710, 143, 38, 110);

		// Token: 0x0400147D RID: 5245
		private Rectangle formRight = new Rectangle(925, 145, 38, 110);

		// Token: 0x0400147E RID: 5246
		private Rectangle formUp = new Rectangle(781, 75, 113, 35);

		// Token: 0x0400147F RID: 5247
		private Rectangle formDown = new Rectangle(783, 291, 113, 35);

		// Token: 0x04001480 RID: 5248
		private Rectangle formCenter = new Rectangle(806, 165, 68, 65);

		// Token: 0x04001481 RID: 5249
		private Rectangle workshopRedbox = new Rectangle(682, 91, 29, 29);

		// Token: 0x04001482 RID: 5250
		private Rectangle workshopDelbox1 = new Rectangle(479, 142, 29, 29);

		// Token: 0x04001483 RID: 5251
		private Rectangle workshopDelbox2 = new Rectangle(684, 142, 29, 29);

		// Token: 0x04001484 RID: 5252
		private Rectangle workshopDelbox3 = new Rectangle(889, 142, 29, 29);

		// Token: 0x04001485 RID: 5253
		private int workshopBGindex;

		// Token: 0x04001486 RID: 5254
		private Rectangle workshopBG1 = new Rectangle(359, 303, 150, 149);

		// Token: 0x04001487 RID: 5255
		private Rectangle workshopBG2 = new Rectangle(564, 303, 150, 149);

		// Token: 0x04001488 RID: 5256
		private Rectangle workshopBG3 = new Rectangle(769, 303, 150, 149);

		// Token: 0x04001489 RID: 5257
		private Rectangle workshopx1 = new Rectangle(261, 144, 44, 44);

		// Token: 0x0400148A RID: 5258
		private Rectangle workshopx2 = new Rectangle(261, 196, 44, 44);

		// Token: 0x0400148B RID: 5259
		private Rectangle workshopx3 = new Rectangle(261, 249, 44, 44);

		// Token: 0x0400148C RID: 5260
		private Rectangle workshopt1 = new Rectangle(355, 138, 158, 157);

		// Token: 0x0400148D RID: 5261
		private Rectangle workshopt2 = new Rectangle(560, 138, 158, 157);

		// Token: 0x0400148E RID: 5262
		private Rectangle workshopt3 = new Rectangle(765, 138, 158, 157);

		// Token: 0x0400148F RID: 5263
		private Rectangle workshopt1B = new Rectangle(352, 135, 164, 162);

		// Token: 0x04001490 RID: 5264
		private Rectangle workshopt2B = new Rectangle(557, 135, 164, 162);

		// Token: 0x04001491 RID: 5265
		private Rectangle workshopt3B = new Rectangle(762, 135, 164, 162);

		// Token: 0x04001492 RID: 5266
		private Rectangle workshopBottomSubtitle = new Rectangle(461, 629, 358, 27);

		// Token: 0x04001493 RID: 5267
		private Rectangle workshopBottomCustitle = new Rectangle(461, 659, 358, 27);

		// Token: 0x04001494 RID: 5268
		private int subRow;

		// Token: 0x04001495 RID: 5269
		private Rectangle workshopLeftarrow = new Rectangle(69, 496, 61, 98);

		// Token: 0x04001496 RID: 5270
		private Rectangle workshopRightarrow = new Rectangle(1146, 496, 61, 98);

		// Token: 0x04001497 RID: 5271
		private Rectangle workshopNums1 = new Rectangle(283, 4, 843, 19);

		// Token: 0x04001498 RID: 5272
		private Rectangle workshopNums2 = new Rectangle(283, 22, 843, 19);

		// Token: 0x04001499 RID: 5273
		private Rectangle workshopNums3 = new Rectangle(283, 39, 843, 19);

		// Token: 0x0400149A RID: 5274
		private Rectangle workshopNums4 = new Rectangle(283, 57, 843, 19);

		// Token: 0x0400149B RID: 5275
		private Rectangle workshopNumsX = new Rectangle(283, 603, 843, 19);

		// Token: 0x0400149C RID: 5276
		private int workshopChosen = 1;

		// Token: 0x0400149D RID: 5277
		private int flashindex;

		// Token: 0x0400149E RID: 5278
		private Color[] flashing = new Color[]
		{
			new Color(255, 255, 0, 255),
			new Color(225, 255, 0, 255),
			new Color(200, 255, 0, 255),
			new Color(175, 255, 0, 255),
			new Color(150, 255, 0, 255),
			new Color(125, 255, 0, 255),
			new Color(100, 255, 0, 255),
			new Color(75, 255, 0, 255),
			new Color(50, 255, 0, 255),
			new Color(0, 255, 0, 255),
			new Color(0, 255, 25, 255),
			new Color(0, 255, 50, 255),
			new Color(0, 255, 70, 255),
			new Color(0, 255, 100, 255),
			new Color(0, 255, 120, 255),
			new Color(0, 255, 150, 255),
			new Color(0, 255, 170, 255),
			new Color(0, 255, 200, 255),
			new Color(0, 255, 225, 255),
			new Color(0, 255, 255, 255),
			new Color(0, 225, 255, 255),
			new Color(0, 200, 255, 255),
			new Color(0, 170, 255, 255),
			new Color(0, 150, 255, 255),
			new Color(0, 120, 255, 255),
			new Color(0, 100, 255, 255),
			new Color(0, 70, 255, 255),
			new Color(0, 55, 255, 255),
			new Color(0, 25, 255, 255),
			new Color(0, 0, 255, 255),
			new Color(25, 0, 255, 255),
			new Color(50, 0, 255, 255),
			new Color(70, 0, 255, 255),
			new Color(100, 0, 255, 255),
			new Color(120, 0, 255, 255),
			new Color(150, 0, 255, 255),
			new Color(170, 0, 255, 255),
			new Color(200, 0, 255, 255),
			new Color(225, 0, 255, 255),
			new Color(255, 0, 255, 255),
			new Color(255, 0, 225, 255),
			new Color(255, 0, 200, 255),
			new Color(255, 0, 170, 255),
			new Color(255, 0, 150, 255),
			new Color(255, 0, 125, 255),
			new Color(255, 0, 100, 255),
			new Color(255, 0, 70, 255),
			new Color(255, 0, 50, 255),
			new Color(255, 0, 25, 255),
			new Color(255, 0, 0, 255),
			new Color(255, 25, 0, 255),
			new Color(255, 50, 0, 255),
			new Color(255, 70, 0, 255),
			new Color(255, 100, 0, 255),
			new Color(255, 120, 0, 255),
			new Color(255, 150, 0, 255),
			new Color(255, 170, 0, 255),
			new Color(255, 200, 0, 255),
			new Color(255, 225, 0, 255),
			new Color(255, 255, 0, 255)
		};

		// Token: 0x0400149F RID: 5279
		private Rectangle workshopb1 = new Rectangle(150, 468, 158, 157);

		// Token: 0x040014A0 RID: 5280
		private Rectangle workshopb2 = new Rectangle(355, 468, 158, 157);

		// Token: 0x040014A1 RID: 5281
		private Rectangle workshopb3 = new Rectangle(560, 468, 158, 157);

		// Token: 0x040014A2 RID: 5282
		private Rectangle workshopb4 = new Rectangle(765, 468, 158, 157);

		// Token: 0x040014A3 RID: 5283
		private Rectangle workshopb5 = new Rectangle(970, 468, 158, 157);

		// Token: 0x040014A4 RID: 5284
		private Rectangle formPreview = new Rectangle(712, 77, 250, 250);

		// Token: 0x040014A5 RID: 5285
		private Rectangle formCheckbox = new Rectangle(204, 415, 52, 50);

		// Token: 0x040014A6 RID: 5286
		private Rectangle formTitle = new Rectangle(333, 180, 315, 39);

		// Token: 0x040014A7 RID: 5287
		private Rectangle formDescr = new Rectangle(334, 257, 367, 73);

		// Token: 0x040014A8 RID: 5288
		private Rectangle formPublic = new Rectangle(351, 432, 30, 30);

		// Token: 0x040014A9 RID: 5289
		private Rectangle formPrivate = new Rectangle(460, 432, 30, 30);

		// Token: 0x040014AA RID: 5290
		private Rectangle formFriends = new Rectangle(565, 432, 30, 30);

		// Token: 0x040014AB RID: 5291
		private Rectangle formBut1 = new Rectangle(747, 336, 28, 28);

		// Token: 0x040014AC RID: 5292
		private Rectangle formBut1b = new Rectangle(747, 369, 28, 28);

		// Token: 0x040014AD RID: 5293
		private Rectangle formBut2 = new Rectangle(782, 336, 28, 28);

		// Token: 0x040014AE RID: 5294
		private Rectangle formBut2b = new Rectangle(782, 369, 28, 28);

		// Token: 0x040014AF RID: 5295
		private Rectangle formBut3 = new Rectangle(818, 336, 28, 28);

		// Token: 0x040014B0 RID: 5296
		private Rectangle formBut3b = new Rectangle(818, 369, 28, 28);

		// Token: 0x040014B1 RID: 5297
		private Rectangle formBut4 = new Rectangle(855, 336, 28, 28);

		// Token: 0x040014B2 RID: 5298
		private Rectangle formBut4b = new Rectangle(855, 369, 28, 28);

		// Token: 0x040014B3 RID: 5299
		private Rectangle formBut5 = new Rectangle(892, 336, 28, 28);

		// Token: 0x040014B4 RID: 5300
		private Rectangle formBut5b = new Rectangle(892, 369, 28, 28);

		// Token: 0x040014B5 RID: 5301
		private Rectangle formTerms = new Rectangle(332, 543, 207, 29);

		// Token: 0x040014B6 RID: 5302
		private Rectangle formCancel = new Rectangle(651, 582, 144, 65);

		// Token: 0x040014B7 RID: 5303
		private Rectangle formPublish = new Rectangle(819, 582, 144, 65);

		// Token: 0x040014B8 RID: 5304
		private Rectangle formWaterMark = new Rectangle(332, 346, 315, 39);

		// Token: 0x040014B9 RID: 5305
		private bool formDrawBG;

		// Token: 0x040014BA RID: 5306
		private int formBGColorindex;

		// Token: 0x040014BB RID: 5307
		private Color[] formBGColor = new Color[]
		{
			new Color(210, 210, 210, 255),
			new Color(180, 180, 180, 255),
			new Color(110, 110, 110, 255),
			new Color(60, 60, 60, 255),
			new Color(15, 15, 15, 255)
		};

		// Token: 0x040014BC RID: 5308
		private bool formDrawBand;

		// Token: 0x040014BD RID: 5309
		private int formBandColorIndex;

		// Token: 0x040014BE RID: 5310
		private Color[] formBandColor = new Color[]
		{
			new Color(225, 178, 30, 255),
			new Color(32, 210, 19, 255),
			new Color(42, 137, 217, 255),
			new Color(166, 86, 178, 255),
			new Color(175, 52, 52, 255)
		};

		// Token: 0x040014BF RID: 5311
		private Rectangle formBand = new Rectangle(1020, 76, 132, 125);

		// Token: 0x040014C0 RID: 5312
		private Rectangle formBandL = new Rectangle(711, 76, 132, 125);

		// Token: 0x040014C1 RID: 5313
		public int formtagIndex;

		// Token: 0x040014C2 RID: 5314
		private Rectangle[] formTag = new Rectangle[]
		{
			new Rectangle(729, 423, 71, 16),
			new Rectangle(810, 423, 56, 16),
			new Rectangle(882, 423, 86, 16),
			new Rectangle(727, 445, 79, 19),
			new Rectangle(816, 448, 47, 17),
			new Rectangle(879, 446, 64, 19),
			new Rectangle(742, 469, 42, 21),
			new Rectangle(810, 470, 50, 20),
			new Rectangle(877, 471, 64, 19)
		};

		// Token: 0x040014C3 RID: 5315
		private Rectangle star1 = new Rectangle(508, 473, 75, 75);

		// Token: 0x040014C4 RID: 5316
		private Rectangle star2 = new Rectangle(602, 473, 75, 75);

		// Token: 0x040014C5 RID: 5317
		private Rectangle star3 = new Rectangle(697, 473, 75, 75);

		// Token: 0x040014C6 RID: 5318
		private Rectangle star4 = new Rectangle(118, 470, 74, 86);

		// Token: 0x040014C7 RID: 5319
		private Rectangle star5 = new Rectangle(299, 107, 60, 60);

		// Token: 0x040014C8 RID: 5320
		private Rectangle bonusPaint = new Rectangle(113, 204, 57, 39);

		// Token: 0x040014C9 RID: 5321
		private Rectangle bonusSpeak = new Rectangle(1065, 163, 100, 90);

		// Token: 0x040014CA RID: 5322
		private Rectangle bonusGrab0 = new Rectangle(346, 220, 143, 76);

		// Token: 0x040014CB RID: 5323
		private Rectangle bonusGrab = new Rectangle(566, 220, 143, 76);

		// Token: 0x040014CC RID: 5324
		private Rectangle bonusGrab2 = new Rectangle(796, 220, 143, 76);

		// Token: 0x040014CD RID: 5325
		private Rectangle secretCoop = new Rectangle(657, 484, 27, 26);

		// Token: 0x040014CE RID: 5326
		private Rectangle creditBox = new Rectangle(20, 274, 104, 54);

		// Token: 0x040014CF RID: 5327
		private Rectangle creditBoxGlow = new Rectangle(20, 341, 104, 54);

		// Token: 0x040014D0 RID: 5328
		private Rectangle creditBox2 = new Rectangle(20, 274, 104, 54);

		// Token: 0x040014D1 RID: 5329
		private Rectangle creditBoxGlow2 = new Rectangle(20, 341, 104, 54);

		// Token: 0x040014D2 RID: 5330
		private Rectangle bonus = new Rectangle(132, 272, 172, 74);

		// Token: 0x040014D3 RID: 5331
		private Rectangle bonusGlow = new Rectangle(132, 350, 172, 74);

		// Token: 0x040014D4 RID: 5332
		private Rectangle audio = new Rectangle(235, 572, 148, 70);

		// Token: 0x040014D5 RID: 5333
		private Rectangle audioGlow = new Rectangle(235, 648, 148, 70);

		// Token: 0x040014D6 RID: 5334
		private Rectangle keymap = new Rectangle(414, 572, 148, 70);

		// Token: 0x040014D7 RID: 5335
		private Rectangle keymapGlow = new Rectangle(414, 648, 148, 70);

		// Token: 0x040014D8 RID: 5336
		private Rectangle button = new Rectangle(132, 446, 218, 83);

		// Token: 0x040014D9 RID: 5337
		private Rectangle buttonOn = new Rectangle(367, 446, 226, 92);

		// Token: 0x040014DA RID: 5338
		private Rectangle button2 = new Rectangle(10, 552, 218, 83);

		// Token: 0x040014DB RID: 5339
		private Rectangle buttonOn2 = new Rectangle(4, 644, 226, 92);

		// Token: 0x040014DC RID: 5340
		private Rectangle button3 = new Rectangle(10, 758, 218, 83);

		// Token: 0x040014DD RID: 5341
		private Rectangle buttonOn3 = new Rectangle(4, 850, 226, 92);

		// Token: 0x040014DE RID: 5342
		private Rectangle button4 = new Rectangle(256, 769, 174, 66);

		// Token: 0x040014DF RID: 5343
		private Rectangle buttonOn4 = new Rectangle(256, 861, 174, 66);

		// Token: 0x040014E0 RID: 5344
		private Rectangle button4x = new Rectangle(470, 769, 174, 66);

		// Token: 0x040014E1 RID: 5345
		private Rectangle buttonOn4x = new Rectangle(470, 861, 174, 66);

		// Token: 0x040014E2 RID: 5346
		private Rectangle arrowup = new Rectangle(480, 280, 43, 38);

		// Token: 0x040014E3 RID: 5347
		private Rectangle arrowdown = new Rectangle(480, 331, 43, 38);

		// Token: 0x040014E4 RID: 5348
		private Rectangle arrowright = new Rectangle(430, 280, 43, 38);

		// Token: 0x040014E5 RID: 5349
		private Rectangle arrowleft = new Rectangle(430, 331, 43, 38);

		// Token: 0x040014E6 RID: 5350
		private Rectangle arrowplus = new Rectangle(380, 280, 43, 38);

		// Token: 0x040014E7 RID: 5351
		private Rectangle arrowminus = new Rectangle(380, 331, 43, 38);

		// Token: 0x040014E8 RID: 5352
		private Rectangle redbox = new Rectangle(4, 181, 34, 34);

		// Token: 0x040014E9 RID: 5353
		private Rectangle redboxfill = new Rectangle(4, 232, 34, 34);

		// Token: 0x040014EA RID: 5354
		private Rectangle arrowfill = new Rectangle(480, 380, 43, 38);

		// Token: 0x040014EB RID: 5355
		private MainMenu.initb b = new MainMenu.initb();

		// Token: 0x040014EC RID: 5356
		private MainMenu.states menuState;

		// Token: 0x040014ED RID: 5357
		private MainMenu.states lastState;

		// Token: 0x040014EE RID: 5358
		private MainMenu.states lastState2;

		// Token: 0x040014EF RID: 5359
		private Vector2 pigVec = new Vector2(475f, 399f);

		// Token: 0x040014F0 RID: 5360
		private Rectangle pigRect = new Rectangle(450, 399, 400, 260);

		// Token: 0x040014F1 RID: 5361
		private bool trailershown;

		// Token: 0x040014F2 RID: 5362
		private bool exitMyGame;

		// Token: 0x040014F3 RID: 5363
		private int randomPhrase;

		// Token: 0x040014F4 RID: 5364
		private Vector2[] nameSpot = new Vector2[]
		{
			new Vector2(300f, 311f),
			new Vector2(640f, 311f),
			new Vector2(980f, 311f),
			new Vector2(300f, 351f),
			new Vector2(640f, 351f),
			new Vector2(980f, 351f),
			new Vector2(300f, 391f),
			new Vector2(640f, 391f),
			new Vector2(980f, 391f),
			new Vector2(300f, 431f),
			new Vector2(640f, 431f),
			new Vector2(980f, 431f),
			new Vector2(300f, 471f),
			new Vector2(640f, 471f),
			new Vector2(980f, 471f),
			new Vector2(300f, 511f),
			new Vector2(640f, 511f),
			new Vector2(980f, 511f),
			new Vector2(300f, 551f),
			new Vector2(640f, 551f),
			new Vector2(980f, 551f)
		};

		// Token: 0x040014F5 RID: 5365
		private string[] fakenames = new string[]
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

		// Token: 0x040014F6 RID: 5366
		private int celselect;

		// Token: 0x040014F7 RID: 5367
		private List<int> randomNames = new List<int>();

		// Token: 0x040014F8 RID: 5368
		private static StringBuilder mybuilder = new StringBuilder(62, 62);

		// Token: 0x040014F9 RID: 5369
		private int moreTimer;

		// Token: 0x040014FA RID: 5370
		private float titlecounter;

		// Token: 0x040014FB RID: 5371
		private float ramp;

		// Token: 0x040014FC RID: 5372
		private int rampCount;

		// Token: 0x040014FD RID: 5373
		private int counterStart;

		// Token: 0x040014FE RID: 5374
		private SoundEffectInstance menuMusic;

		// Token: 0x040014FF RID: 5375
		private SoundEffectInstance speaker;

		// Token: 0x04001500 RID: 5376
		private SoundEffect music1;

		// Token: 0x04001501 RID: 5377
		private SoundEffect speak;

		// Token: 0x04001502 RID: 5378
		private GamePadState prevstate;

		// Token: 0x04001503 RID: 5379
		private GamePadState gamePadState;

		// Token: 0x04001504 RID: 5380
		private KeyboardState prevKeys;

		// Token: 0x04001505 RID: 5381
		private KeyboardState keyState;

		// Token: 0x04001506 RID: 5382
		private MouseState prevMouse;

		// Token: 0x04001507 RID: 5383
		private MouseState mouseState;

		// Token: 0x04001508 RID: 5384
		private Microsoft.Xna.Framework.Input.Keys[] pressedKeys;

		// Token: 0x04001509 RID: 5385
		private ContentManager content2;

		// Token: 0x0400150A RID: 5386
		private ScreenManager sc;

		// Token: 0x0400150B RID: 5387
		private SpriteBatch spriteBatch;

		// Token: 0x0400150C RID: 5388
		private Random rr;

		// Token: 0x0400150D RID: 5389
		public SpriteFont font;

		// Token: 0x0400150E RID: 5390
		public SpriteFont font2;

		// Token: 0x0400150F RID: 5391
		public SpriteFont font3;

		// Token: 0x04001510 RID: 5392
		public SpriteFont deadfont;

		// Token: 0x04001511 RID: 5393
		public SpriteFont diaryfont;

		// Token: 0x04001512 RID: 5394
		private string ss;

		// Token: 0x04001513 RID: 5395
		private PlayerIndex playerindex;

		// Token: 0x04001514 RID: 5396
		private PlayerIndex tempindex;

		// Token: 0x04001515 RID: 5397
		private int menuSelect = 1;

		// Token: 0x04001516 RID: 5398
		private string hostChoice = "";

		// Token: 0x04001517 RID: 5399
		private string highlightname = "";

		// Token: 0x04001518 RID: 5400
		private Rectangle glow = new Rectangle(1244, 477, 123, 120);

		// Token: 0x04001519 RID: 5401
		private Rectangle taken = new Rectangle(957, 725, 100, 98);

		// Token: 0x0400151A RID: 5402
		private Rectangle locked = new Rectangle(842, 725, 100, 98);

		// Token: 0x0400151B RID: 5403
		private Vector2[] selectPoint = new Vector2[]
		{
			new Vector2(126f, 434f),
			new Vector2(307f, 434f),
			new Vector2(490f, 434f),
			new Vector2(670f, 434f),
			new Vector2(858f, 434f),
			new Vector2(1037f, 434f),
			new Vector2(252f, 468f),
			new Vector2(984f, 468f),
			new Vector2(433f, 468f),
			new Vector2(613f, 468f),
			new Vector2(800f, 468f),
			new Vector2(513f, 190f)
		};

		// Token: 0x0400151C RID: 5404
		private Rectangle[] vehicleCard = new Rectangle[]
		{
			new Rectangle(0, 98, 100, 98),
			new Rectangle(100, 98, 100, 98),
			new Rectangle(200, 98, 100, 98),
			new Rectangle(300, 98, 100, 98),
			new Rectangle(400, 98, 100, 98)
		};

		// Token: 0x0400151D RID: 5405
		private Rectangle[] charCard = new Rectangle[]
		{
			new Rectangle(0, 0, 100, 98),
			new Rectangle(100, 0, 100, 98),
			new Rectangle(200, 0, 100, 98),
			new Rectangle(300, 0, 100, 98),
			new Rectangle(400, 0, 100, 98)
		};

		// Token: 0x0400151E RID: 5406
		private float ramper;

		// Token: 0x0400151F RID: 5407
		private int val1;

		// Token: 0x04001520 RID: 5408
		private int val2;

		// Token: 0x04001521 RID: 5409
		private int indieIndex = 1;

		// Token: 0x04001522 RID: 5410
		private Thread backgroundThread;

		// Token: 0x04001523 RID: 5411
		private EventWaitHandle backgroundThreadExit;

		// Token: 0x04001524 RID: 5412
		private bool isBusy;

		// Token: 0x04001525 RID: 5413
		private List<int> loads = new List<int>();

		// Token: 0x04001526 RID: 5414
		private bool showTitle;

		// Token: 0x04001527 RID: 5415
		private int pulseTimer;

		// Token: 0x04001528 RID: 5416
		private Model farmerModel;

		// Token: 0x04001529 RID: 5417
		private Texture2D farmerTexture;

		// Token: 0x0400152A RID: 5418
		private AnimationPlayer[] farmer1;

		// Token: 0x0400152B RID: 5419
		private Effect farmerSkin;

		// Token: 0x0400152C RID: 5420
		private int farmerGlitchCount;

		// Token: 0x0400152D RID: 5421
		private int farmerGlitchIndex;

		// Token: 0x0400152E RID: 5422
		private MainMenu.npcanim farmerAnim;

		// Token: 0x0400152F RID: 5423
		private string[] months = new string[]
		{
			"Ozo", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept",
			"Oct", "Nov", "Dec"
		};

		// Token: 0x04001530 RID: 5424
		private SoundEffects farmerDialog1;

		// Token: 0x04001531 RID: 5425
		private bool farmerDialog1Loaded;

		// Token: 0x04001532 RID: 5426
		private int talkIndex = -1;

		// Token: 0x04001533 RID: 5427
		private List<float> farmerJaw;

		// Token: 0x04001534 RID: 5428
		private int farmerJawIndex = -1;

		// Token: 0x04001535 RID: 5429
		private float talkAverage;

		// Token: 0x04001536 RID: 5430
		private float talkSmooth;

		// Token: 0x04001537 RID: 5431
		private float desiredAngle;

		// Token: 0x04001538 RID: 5432
		private Vector2 adj = Vector2.Zero;

		// Token: 0x04001539 RID: 5433
		private Vector2 basePos = Vector2.Zero;

		// Token: 0x0400153A RID: 5434
		private Vector2 pos1 = Vector2.Zero;

		// Token: 0x0400153B RID: 5435
		private Vector2 pos2 = Vector2.Zero;

		// Token: 0x0400153C RID: 5436
		private float tiltDown;

		// Token: 0x0400153D RID: 5437
		private float farmerlook;

		// Token: 0x0400153E RID: 5438
		private float farmertilt;

		// Token: 0x0400153F RID: 5439
		private float farmerFrame1;

		// Token: 0x04001540 RID: 5440
		private int currentKeyframe;

		// Token: 0x04001541 RID: 5441
		private TimeSpan currentTimeValue;

		// Token: 0x04001542 RID: 5442
		private float followTimer;

		// Token: 0x04001543 RID: 5443
		private float followTween;

		// Token: 0x04001544 RID: 5444
		private float headTween;

		// Token: 0x04001545 RID: 5445
		private Matrix headMatrix = Matrix.Identity;

		// Token: 0x04001546 RID: 5446
		private Matrix lastheadMatrix = Matrix.Identity;

		// Token: 0x04001547 RID: 5447
		private Rectangle diaryRGB = new Rectangle(917, 1229, 129, 126);

		// Token: 0x04001548 RID: 5448
		private Rectangle diaryGlow = new Rectangle(1054, 1229, 129, 126);

		// Token: 0x04001549 RID: 5449
		private Rectangle diaryDark = new Rectangle(1190, 1229, 129, 126);

		// Token: 0x0400154A RID: 5450
		private bool recording1;

		// Token: 0x0400154B RID: 5451
		private bool recording2;

		// Token: 0x0400154C RID: 5452
		private bool recording3;

		// Token: 0x0400154D RID: 5453
		private bool recording4;

		// Token: 0x0400154E RID: 5454
		private Vector3[] LightDirectionX = new Vector3[]
		{
			new Vector3(0.6f, 1f, -0.9f),
			new Vector3(0.6f, 1f, 0.1f),
			new Vector3(0.6f, 1f, 0.9f),
			new Vector3(0.6f, 0.3f, -0.9f),
			new Vector3(0.6f, 0.3f, 0.1f),
			new Vector3(0.6f, 0.3f, 0.9f),
			new Vector3(0.6f, -1f, -0.9f),
			new Vector3(0.6f, -1f, 0.1f),
			new Vector3(0.6f, -1f, 0.9f)
		};

		// Token: 0x0400154F RID: 5455
		private Vector3[] DiffuseLightX = new Vector3[]
		{
			new Vector3(1f, 0.8f, 0.8f),
			new Vector3(1f, 1f, 0.8f),
			new Vector3(0.7f, 0.7f, 1f),
			new Vector3(1f, 0.9f, 0.9f),
			new Vector3(1f, 1f, 0.85f),
			new Vector3(0.7f, 0.7f, 1f),
			new Vector3(1f, 0.9f, 0.9f),
			new Vector3(1f, 1f, 0.9f),
			new Vector3(0.7f, 0.7f, 1f)
		};

		// Token: 0x04001550 RID: 5456
		private Vector3[] AmbientLightX = new Vector3[]
		{
			new Vector3(0.7f, 0.6f, 0.6f),
			new Vector3(0.7f, 0.7f, 0.65f),
			new Vector3(0.6f, 0.6f, 0.7f),
			new Vector3(0.4f, 0.3f, 0.3f),
			new Vector3(0.4f, 0.4f, 0.39f),
			new Vector3(0.3f, 0.3f, 0.4f),
			new Vector3(0.1f, 0.05f, 0.05f),
			new Vector3(0.1f, 0.1f, 0.1f),
			new Vector3(0.1f, 0.1f, 0.2f)
		};

		// Token: 0x04001551 RID: 5457
		private int briteX = 4;

		// Token: 0x04001552 RID: 5458
		private int colorX = 4;

		// Token: 0x04001553 RID: 5459
		private int briteXold;

		// Token: 0x04001554 RID: 5460
		private int colorXold;

		// Token: 0x04001555 RID: 5461
		private int briteXtemp;

		// Token: 0x04001556 RID: 5462
		private int colorXtemp;

		// Token: 0x04001557 RID: 5463
		private float liteTween;

		// Token: 0x04001558 RID: 5464
		private List<MainMenu.imim> im = new List<MainMenu.imim>();

		// Token: 0x04001559 RID: 5465
		private List<int> displayList = new List<int>();

		// Token: 0x0400155A RID: 5466
		private Vector2 lastlookpos = new Vector2(390f, 142f);

		// Token: 0x0400155B RID: 5467
		private Vector2[] lookpos = new Vector2[]
		{
			new Vector2(390f, 195f),
			new Vector2(910f, 128f),
			new Vector2(616f, 390f),
			new Vector2(414f, 537f),
			new Vector2(136f, 567f),
			new Vector2(30f, 296f),
			new Vector2(64f, 28f)
		};

		// Token: 0x0400155C RID: 5468
		private Vector2[] lookposOrig = new Vector2[]
		{
			new Vector2(390f, 195f),
			new Vector2(910f, 128f),
			new Vector2(616f, 390f),
			new Vector2(414f, 537f),
			new Vector2(136f, 567f),
			new Vector2(30f, 296f),
			new Vector2(64f, 28f)
		};

		// Token: 0x0400155D RID: 5469
		private Vector2 lookPosRR = new Vector2(0f, 0f);

		// Token: 0x0400155E RID: 5470
		private int lookIndex;

		// Token: 0x0400155F RID: 5471
		private bool followMouse;

		// Token: 0x04001560 RID: 5472
		private bool followPoint = true;

		// Token: 0x04001561 RID: 5473
		private int directNum = -1;

		// Token: 0x04001562 RID: 5474
		private int directRate;

		// Token: 0x04001563 RID: 5475
		private int directNumX;

		// Token: 0x04001564 RID: 5476
		private int directNumY;

		// Token: 0x04001565 RID: 5477
		private int entryIndex = -1;

		// Token: 0x04001566 RID: 5478
		private int mediaIndex;

		// Token: 0x04001567 RID: 5479
		private ContentManager Content;

		// Token: 0x02000090 RID: 144
		private enum states
		{
			// Token: 0x04001569 RID: 5481
			main,
			// Token: 0x0400156A RID: 5482
			playgame,
			// Token: 0x0400156B RID: 5483
			settings,
			// Token: 0x0400156C RID: 5484
			videos,
			// Token: 0x0400156D RID: 5485
			trailer,
			// Token: 0x0400156E RID: 5486
			credit,
			// Token: 0x0400156F RID: 5487
			graphics,
			// Token: 0x04001570 RID: 5488
			gamesetup,
			// Token: 0x04001571 RID: 5489
			controller,
			// Token: 0x04001572 RID: 5490
			coopChoice,
			// Token: 0x04001573 RID: 5491
			keyboard,
			// Token: 0x04001574 RID: 5492
			display,
			// Token: 0x04001575 RID: 5493
			displaykeys,
			// Token: 0x04001576 RID: 5494
			cooplobby,
			// Token: 0x04001577 RID: 5495
			multiHostStart,
			// Token: 0x04001578 RID: 5496
			multi2player,
			// Token: 0x04001579 RID: 5497
			multijoinStart,
			// Token: 0x0400157A RID: 5498
			multi4player,
			// Token: 0x0400157B RID: 5499
			multi6player,
			// Token: 0x0400157C RID: 5500
			multi3player,
			// Token: 0x0400157D RID: 5501
			controls,
			// Token: 0x0400157E RID: 5502
			creditwall,
			// Token: 0x0400157F RID: 5503
			alphateam,
			// Token: 0x04001580 RID: 5504
			audio,
			// Token: 0x04001581 RID: 5505
			workshop,
			// Token: 0x04001582 RID: 5506
			diary,
			// Token: 0x04001583 RID: 5507
			more
		}

		// Token: 0x02000091 RID: 145
		private class initb
		{
			// Token: 0x04001584 RID: 5508
			public bool main;

			// Token: 0x04001585 RID: 5509
			public bool playgame;

			// Token: 0x04001586 RID: 5510
			public bool settings;

			// Token: 0x04001587 RID: 5511
			public bool videos;

			// Token: 0x04001588 RID: 5512
			public bool trailer;

			// Token: 0x04001589 RID: 5513
			public bool credit;

			// Token: 0x0400158A RID: 5514
			public bool graphics;

			// Token: 0x0400158B RID: 5515
			public bool gamesetup;

			// Token: 0x0400158C RID: 5516
			public bool controller;

			// Token: 0x0400158D RID: 5517
			public bool coopChoice;

			// Token: 0x0400158E RID: 5518
			public bool keyboard;

			// Token: 0x0400158F RID: 5519
			public bool display;

			// Token: 0x04001590 RID: 5520
			public bool displaykeys;

			// Token: 0x04001591 RID: 5521
			public bool cooplobby;

			// Token: 0x04001592 RID: 5522
			public bool multiHostStart;

			// Token: 0x04001593 RID: 5523
			public bool multi2player;

			// Token: 0x04001594 RID: 5524
			public bool multijoinStart;

			// Token: 0x04001595 RID: 5525
			public bool multi4player;

			// Token: 0x04001596 RID: 5526
			public bool multi6player;

			// Token: 0x04001597 RID: 5527
			public bool multi3player;

			// Token: 0x04001598 RID: 5528
			public bool controls;

			// Token: 0x04001599 RID: 5529
			public bool creditwall;

			// Token: 0x0400159A RID: 5530
			public bool alphateam;

			// Token: 0x0400159B RID: 5531
			public bool audio;

			// Token: 0x0400159C RID: 5532
			public bool workshop;

			// Token: 0x0400159D RID: 5533
			public bool diary;

			// Token: 0x0400159E RID: 5534
			public bool more;
		}

		// Token: 0x02000092 RID: 146
		private struct npcanim
		{
			// Token: 0x0400159F RID: 5535
			public float animCount;

			// Token: 0x040015A0 RID: 5536
			public float animTween;

			// Token: 0x040015A1 RID: 5537
			public List<int> animList;

			// Token: 0x040015A2 RID: 5538
			public bool looped;

			// Token: 0x040015A3 RID: 5539
			public int animClip;

			// Token: 0x040015A4 RID: 5540
			public int animMax;

			// Token: 0x040015A5 RID: 5541
			public int animMin;

			// Token: 0x040015A6 RID: 5542
			public int animLoop;

			// Token: 0x040015A7 RID: 5543
			public float tweenspeed;
		}

		// Token: 0x02000093 RID: 147
		public class imim
		{
			// Token: 0x040015A8 RID: 5544
			public bool full;

			// Token: 0x040015A9 RID: 5545
			public bool show;

			// Token: 0x040015AA RID: 5546
			public bool shake;

			// Token: 0x040015AB RID: 5547
			public int shaketimer;

			// Token: 0x040015AC RID: 5548
			public Vector2 pos = Vector2.Zero;

			// Token: 0x040015AD RID: 5549
			public Vector2 oldpos = Vector2.Zero;

			// Token: 0x040015AE RID: 5550
			public float tween;

			// Token: 0x040015AF RID: 5551
			public float tweenRate = 1f;

			// Token: 0x040015B0 RID: 5552
			public int spriteIndex;

			// Token: 0x040015B1 RID: 5553
			public float scale = 1f;

			// Token: 0x040015B2 RID: 5554
			public float rot;

			// Token: 0x040015B3 RID: 5555
			public SpriteEffects flip;
		}
	}
}
