using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x0200000C RID: 12
	internal abstract class MenuScreen : GameScreen
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00028173 File Offset: 0x00026373
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0002817B File Offset: 0x0002637B
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00028184 File Offset: 0x00026384
		protected IList<MenuEntry> MenuEntries
		{
			get
			{
				return this.menuEntries;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0002818C File Offset: 0x0002638C
		public MenuScreen(string mm)
		{
			this.menuTitle = mm;
			base.TransitionOnTime = TimeSpan.FromSeconds(1.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(1.0);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00028264 File Offset: 0x00026464
		public override void LoadContent()
		{
			this.rr = new Random();
			if (this.content == null)
			{
				this.content = new ContentManager(base.ScreenManager.Game.Services, "Content");
			}
			this.sc = base.ScreenManager;
			this.spriteBatch = base.ScreenManager.SpriteBatch;
			this.myheight = this.menuEntries[0];
			this.aspectRatio = (float)base.ScreenManager.GraphicsDevice.Viewport.Width / (float)base.ScreenManager.GraphicsDevice.Viewport.Height;
			this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30f), 1.77f, 1f, 490000f);
			this.viewMatrix = Matrix.CreateLookAt(this.campos, this.camlookpos, Vector3.Up);
			this.blue = new Texture2D(this.sc.GraphicsDevice, 1, 1);
			this.red = new Texture2D(this.sc.GraphicsDevice, 1, 1);
			this.blue.SetData<Color>(new Color[]
			{
				new Color(10, 10, 200, 50)
			});
			this.red.SetData<Color>(new Color[]
			{
				new Color(220, 10, 10, 50)
			});
			for (int i = 0; i < this.menuEntries.Count; i++)
			{
				MenuEntry menuEntry = this.menuEntries[i];
				menuEntry.LoadContent(this);
				if (this.menuTitle == "Game Video Settings")
				{
					if (menuEntry.Text == "BRITE")
					{
						menuEntry.Amount = this.sc.gamefadeSetting;
					}
					if (menuEntry.Text == "FILTER")
					{
						menuEntry.Amount = this.sc.gamefilterSetting;
					}
					if (menuEntry.Text == "LEVEL")
					{
						menuEntry.Amount = this.sc.gameintenseSetting;
					}
					if (menuEntry.Text == "ALIAS")
					{
						menuEntry.Amount = (float)this.sc.aliasSetting;
					}
				}
				if (this.menuTitle == "Video Settings")
				{
					this.percX = 1f - (this.sc.siders - 5f) / 640f;
					this.percY = 1f - (this.sc.topper - 5f) / 360f;
					if (menuEntry.Text == "BRIGHTEN")
					{
						menuEntry.Amount = (float)((this.sc.brightness - 28) / 10);
					}
					if (menuEntry.Text == "CONTRAST")
					{
						menuEntry.Amount = (float)((this.sc.contrast - 28) / 10);
					}
					if (menuEntry.Text == "BORDERS")
					{
						menuEntry.Amount = 0f;
					}
				}
				if (menuEntry.Text == "PLANET")
				{
					menuEntry.Amount = (float)base.ScreenManager.bgindex;
					menuEntry.Text = menuEntry.Lists[base.ScreenManager.bgindex];
				}
				if (menuEntry.Text == "VIBRO")
				{
					menuEntry.Amount = (float)this.sc.vibroSetting;
				}
				if (this.menuTitle == "Audio Settings")
				{
					if (menuEntry.Text == "SOUND")
					{
						menuEntry.Amount = (float)((int)Math.Round(Math.Sqrt((double)(this.sc.ev * 100f))));
					}
					if (menuEntry.Text == "MUSIC")
					{
						menuEntry.Amount = (float)((int)Math.Round(Math.Sqrt((double)(this.sc.mv * 100f))));
					}
					if (menuEntry.Text == "RADIO")
					{
						menuEntry.Amount = (float)((int)Math.Round(Math.Sqrt((double)(this.sc.voiceVolume * 100f))));
					}
				}
				if (this.menuTitle == "SetCamera")
				{
					if (menuEntry.Text == "Distance")
					{
						menuEntry.Amount = (float)this.sc.allcamsradius[menuEntry.Myindex];
					}
					if (menuEntry.Text == "Orbiting")
					{
						menuEntry.Amount = (float)this.sc.allcamsorbit[menuEntry.Myindex];
						menuEntry.Amount = MathHelper.Clamp((float)this.sc.roverrotlock, 0f, 1f);
					}
					if (menuEntry.Text == "Altitude")
					{
						menuEntry.Amount = MathHelper.Clamp((float)this.sc.allcamsaltitude[menuEntry.Myindex], 0f, 1f);
						menuEntry.Amount = 1f - MathHelper.Clamp((float)this.sc.roverhitelock, 0f, 1f);
					}
					if (menuEntry.Text == "Lensing")
					{
						menuEntry.Amount = (float)this.sc.allcamslens[menuEntry.Myindex];
					}
				}
				if (this.menuTitle == "SetCamera2")
				{
					if (menuEntry.Text == "Distance")
					{
						menuEntry.Amount = (float)this.sc.allcamsradius[menuEntry.Myindex];
					}
					if (menuEntry.Text == "Orbiting")
					{
						menuEntry.Amount = (float)this.sc.allcamsorbit[menuEntry.Myindex];
						menuEntry.Amount = MathHelper.Clamp((float)this.sc.landerrotlock, 0f, 1f);
					}
					if (menuEntry.Text == "Altitude")
					{
						menuEntry.Amount = MathHelper.Clamp((float)this.sc.allcamsaltitude[menuEntry.Myindex], 0f, 1f);
						menuEntry.Amount = 1f - MathHelper.Clamp((float)this.sc.landerhitelock, 0f, 1f);
					}
					if (menuEntry.Text == "Lensing")
					{
						menuEntry.Amount = (float)this.sc.allcamslens[menuEntry.Myindex];
					}
				}
				if (this.menuTitle == "Game Settings1")
				{
					if (menuEntry.Text == "INVERT X")
					{
						int num = 0;
						if (this.sc.space_rinvertX == -1)
						{
							num = 1;
						}
						menuEntry.Amount = (float)num;
					}
					if (menuEntry.Text == "INVERT Y")
					{
						int num2 = 0;
						if (this.sc.space_rinvertY == -1)
						{
							num2 = 1;
						}
						menuEntry.Amount = (float)num2;
					}
					if (menuEntry.Text == "TURN SPEED")
					{
						int num3 = (int)MathHelper.Lerp(0f, 10f, (this.sc.space_rsentivityX - 0.2f) / 1.8f);
						num3 = (int)MathHelper.Clamp((float)num3, 0f, 10f);
						menuEntry.Amount = (float)num3;
					}
					if (menuEntry.Text == "TILT SPEED")
					{
						int num4 = (int)MathHelper.Lerp(0f, 10f, (this.sc.space_rsentivityY - 0.2f) / 1.8f);
						num4 = (int)MathHelper.Clamp((float)num4, 0f, 10f);
						menuEntry.Amount = (float)num4;
					}
					if (menuEntry.Text == "VIBRATION")
					{
						menuEntry.Amount = (float)this.sc.vibroSetting;
					}
				}
				if (this.menuTitle == "Game Settings2")
				{
					if (menuEntry.Text == "INVERT X")
					{
						int num5 = 0;
						if (this.sc.space_invertX == -1)
						{
							num5 = 1;
						}
						menuEntry.Amount = (float)num5;
					}
					if (menuEntry.Text == "INVERT Y")
					{
						int num6 = 0;
						if (this.sc.space_invertY == -1)
						{
							num6 = 1;
						}
						menuEntry.Amount = (float)num6;
					}
					if (menuEntry.Text == "TURN SPEED")
					{
						int num7 = (int)MathHelper.Lerp(0f, 10f, (this.sc.space_sentivityX - 0.2f) / 1.8f);
						num7 = (int)MathHelper.Clamp((float)num7, 0f, 10f);
						menuEntry.Amount = (float)num7;
					}
					if (menuEntry.Text == "TILT SPEED")
					{
						int num8 = (int)MathHelper.Lerp(0f, 10f, (this.sc.space_sentivityY - 0.2f) / 1.8f);
						num8 = (int)MathHelper.Clamp((float)num8, 0f, 10f);
						menuEntry.Amount = (float)num8;
					}
					if (menuEntry.Text == "VIBRATION")
					{
						menuEntry.Amount = (float)this.sc.vibroSetting;
					}
				}
				if (this.menuTitle == "Game Settings3")
				{
					if (menuEntry.Text == "INVERT X")
					{
						int num9 = 0;
						if (this.sc.space_winvertX == -1)
						{
							num9 = 1;
						}
						menuEntry.Amount = (float)num9;
					}
					if (menuEntry.Text == "INVERT Y")
					{
						int num10 = 0;
						if (this.sc.space_winvertY == -1)
						{
							num10 = 1;
						}
						menuEntry.Amount = (float)num10;
					}
					if (menuEntry.Text == "TURN SPEED")
					{
						int num11 = (int)MathHelper.Lerp(0f, 10f, (this.sc.space_wsentivityX - 0.2f) / 1.8f);
						num11 = (int)MathHelper.Clamp((float)num11, 0f, 10f);
						menuEntry.Amount = (float)num11;
					}
					if (menuEntry.Text == "TILT SPEED")
					{
						int num12 = (int)MathHelper.Lerp(0f, 10f, (this.sc.space_wsentivityY - 0.2f) / 1.8f);
						num12 = (int)MathHelper.Clamp((float)num12, 0f, 10f);
						menuEntry.Amount = (float)num12;
					}
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00028C78 File Offset: 0x00026E78
		public bool KMdown(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = MenuScreen.mouseState.LeftButton == ButtonState.Pressed;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = MenuScreen.mouseState.MiddleButton == ButtonState.Pressed;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = MenuScreen.mouseState.RightButton == ButtonState.Pressed;
			}
			else if (k == Keys.Print)
			{
				flag = MenuScreen.mouseState.XButton1 == ButtonState.Pressed;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = MenuScreen.mouseState.XButton2 == ButtonState.Pressed;
			}
			else
			{
				flag = this.keyState.IsKeyDown(k);
			}
			return flag;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00028D08 File Offset: 0x00026F08
		public bool KMreleased(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = MenuScreen.prevMouse.LeftButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = MenuScreen.prevMouse.MiddleButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = MenuScreen.prevMouse.RightButton == ButtonState.Released;
			}
			else if (k == Keys.Print)
			{
				flag = MenuScreen.prevMouse.XButton1 == ButtonState.Released;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = MenuScreen.prevMouse.XButton2 == ButtonState.Released;
			}
			else
			{
				flag = this.prevkeyState.IsKeyUp(k);
			}
			return flag;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00028D98 File Offset: 0x00026F98
		public bool KMtoggle(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = MenuScreen.mouseState.LeftButton == ButtonState.Pressed && MenuScreen.prevMouse.LeftButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = MenuScreen.mouseState.MiddleButton == ButtonState.Pressed && MenuScreen.prevMouse.MiddleButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = MenuScreen.mouseState.RightButton == ButtonState.Pressed && MenuScreen.prevMouse.RightButton == ButtonState.Released;
			}
			else if (k == Keys.Print)
			{
				flag = MenuScreen.mouseState.XButton1 == ButtonState.Pressed && MenuScreen.prevMouse.XButton1 == ButtonState.Released;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = MenuScreen.mouseState.XButton2 == ButtonState.Pressed && MenuScreen.prevMouse.XButton2 == ButtonState.Released;
			}
			else
			{
				flag = this.keyState.IsKeyDown(k) && this.prevkeyState.IsKeyUp(k);
			}
			return flag;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00028E8C File Offset: 0x0002708C
		public override void HandleInput(InputState input)
		{
			this.prevkeyState = input.lastKeyState;
			this.keyState = input.currentKeyState;
			MenuScreen.prevMouse = MenuScreen.mouseState;
			MenuScreen.mouseState = Mouse.GetState();
			if (this.mytimer % 120f == 0f)
			{
				this.sc.centerWindow();
			}
			if (this.delayinput)
			{
				MenuScreen.prevMouse = MenuScreen.mouseState;
				this.prevkeyState = this.keyState;
			}
			if (MenuScreen.mouseState.X == (int)this.sc.mymouse.X && MenuScreen.mouseState.Y == (int)this.sc.mymouse.Y)
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
			}
			this.sc.mymouse.X = (float)MenuScreen.mouseState.X;
			this.sc.mymouse.Y = (float)MenuScreen.mouseState.Y;
			MenuScreen.title = this.menuTitle;
			float num;
			this.sc.righttrigger = input.IsTrigger("right", base.ControllingPlayer, out num);
			this.sc.lefttrigger = input.IsTrigger("left", base.ControllingPlayer, out num);
			this.sc.rightstickY = input.IsStick("updown", base.ControllingPlayer, out num);
			this.sc.rightstickX = input.IsStick("leftright", base.ControllingPlayer, out num);
			bool flag;
			this.sc.rightbumper = input.IsBumper("right", base.ControllingPlayer, out flag);
			this.sc.leftbumper = input.IsBumper("left", base.ControllingPlayer, out flag);
			if (this.menuTitle == "Video Settings")
			{
				this.percX += this.sc.rightstickX / 100f;
				this.percY += this.sc.rightstickY / 100f;
				this.percX = MathHelper.Clamp(this.percX, 0.6f, 1f);
				this.percY = MathHelper.Clamp(this.percY, 0.6f, 1f);
			}
			if (input.IsMenuUpX(base.ControllingPlayer) && this.mytimer > 0.05f)
			{
				this.sc.gamerindex = MenuScreen.playerIndex;
				this.selectedEntry--;
				this.sc.clickmenu.Play(this.sc.ev, 0f, 0f);
				if (this.selectedEntry < 0)
				{
					this.selectedEntry = this.menuEntries.Count - 1;
				}
				this.mytimer = 0f;
			}
			if (input.IsMenuDownX(base.ControllingPlayer) && this.mytimer > 0.05f)
			{
				this.sc.gamerindex = MenuScreen.playerIndex;
				this.selectedEntry++;
				this.sc.clickmenu.Play(this.sc.ev, 0f, 0f);
				if (this.selectedEntry >= this.menuEntries.Count)
				{
					this.selectedEntry = 0;
				}
				this.mytimer = 0f;
			}
			if (this.sc.usingMouse && this.mousemoving && this.point.Count == this.menuEntries.Count)
			{
				this.whichbutton = -1;
				for (int i = 0; i < this.menuEntries.Count; i++)
				{
					this.queryButton(this.point[i], i);
					if (this.whichbutton != -1 && this.whichbutton != this.selectedEntry)
					{
						this.selectedEntry = this.whichbutton;
						this.sc.clickmenu.Play(this.sc.ev, 0f, 0f);
						break;
					}
					this.queryButton(this.valbox[i], i);
					if (this.whichbutton != -1 && this.whichbutton != this.selectedEntry)
					{
						this.selectedEntry = this.whichbutton;
						this.sc.clickmenu.Play(this.sc.ev, 0f, 0f);
						break;
					}
				}
			}
			this.IncDec = false;
			if (this.sc.usingMouse && this.point.Count == this.menuEntries.Count && this.menuEntries[this.selectedEntry].Type > 0)
			{
				this.whichbutton = -1;
				this.queryButton(this.valbox[this.selectedEntry], this.selectedEntry);
				if (this.whichbutton != -1)
				{
					if (this.KMtoggle(this.sc.rmb_key))
					{
						if (this.menuEntries[this.selectedEntry].Amount < (float)(this.menuEntries[this.selectedEntry].Lists.Length - 1))
						{
							this.menuEntries[this.selectedEntry].Amount += 1f;
							this.sc.clickmenu.Play(this.sc.ev, 0.2f, 0f);
							this.menuEntries[this.selectedEntry].OnSelectEntry(MenuScreen.playerIndex);
						}
						this.IncDec = true;
					}
					if (this.KMtoggle(this.sc.lmb_key))
					{
						if (this.menuEntries[this.selectedEntry].Amount > 0f)
						{
							this.menuEntries[this.selectedEntry].Amount -= 1f;
							this.sc.clickmenu.Play(this.sc.ev, -0.2f, 0f);
							this.menuEntries[this.selectedEntry].OnSelectEntry(MenuScreen.playerIndex);
						}
						this.IncDec = true;
					}
				}
			}
			if (this.menuEntries[this.selectedEntry].Type > 0 && input.IsMenuRightX(base.ControllingPlayer) && this.mytimer > 0.05f)
			{
				this.sc.gamerindex = MenuScreen.playerIndex;
				if (this.menuEntries[this.selectedEntry].Amount < (float)(this.menuEntries[this.selectedEntry].Lists.Length - 1))
				{
					this.menuEntries[this.selectedEntry].Amount += 1f;
					this.sc.clickmenu.Play(this.sc.ev, 0f, 0f);
					this.menuEntries[this.selectedEntry].OnSelectEntry(MenuScreen.playerIndex);
					this.mytimer = 0f;
				}
			}
			if (this.menuEntries[this.selectedEntry].Type > 0 && input.IsMenuLeftX(base.ControllingPlayer) && this.mytimer > 0.05f)
			{
				this.sc.gamerindex = MenuScreen.playerIndex;
				if (this.menuEntries[this.selectedEntry].Amount > 0f)
				{
					this.menuEntries[this.selectedEntry].Amount -= 1f;
					this.sc.clickmenu.Play(this.sc.ev, 0f, 0f);
					this.menuEntries[this.selectedEntry].OnSelectEntry(MenuScreen.playerIndex);
					this.mytimer = 0f;
				}
			}
			if (this.menuEntries[this.selectedEntry].Type == 2 && input.IsMenuSelect(base.ControllingPlayer, out MenuScreen.playerIndex))
			{
				this.sc.gamerindex = MenuScreen.playerIndex;
				base.ScreenManager.typewriterblank = 0f;
				base.ScreenManager.textflag = 0;
				base.ScreenManager.typewriterwait = (float)this.rr.Next(700, 1100);
				base.ScreenManager.typeposition = 0f;
				base.ScreenManager.typevertical = (float)this.rr.Next(420, 510);
				base.ScreenManager.typewriterdelay = this.rr.Next(2, 6);
			}
			if (this.IncDec)
			{
				this.delayinput = false;
				return;
			}
			if (input.IsMenuSelect(base.ControllingPlayer, out MenuScreen.playerIndex) || (this.sc.usingMouse && this.KMtoggle(this.sc.lmb_key)))
			{
				this.sc.gamerindex = MenuScreen.playerIndex;
				if (this.menuEntries[this.selectedEntry].Type == 0)
				{
					this.OnSelectEntry(this.selectedEntry, MenuScreen.playerIndex);
				}
			}
			else if (input.IsMenuCancel(base.ControllingPlayer, out MenuScreen.playerIndex) || (this.sc.usingMouse && this.KMtoggle(this.sc.rmb_key)))
			{
				this.sc.gamerindex = MenuScreen.playerIndex;
				this.OnCancel(MenuScreen.playerIndex);
			}
			this.delayinput = false;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00029864 File Offset: 0x00027A64
		protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
		{
			this.sc.gamerindex = playerIndex;
			this.sc.forward.Play(this.sc.ev, 0f, 0f);
			this.menuEntries[this.selectedEntry].OnSelectEntry(playerIndex);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000298BC File Offset: 0x00027ABC
		protected virtual void OnCancel(PlayerIndex playerIndex)
		{
			this.sc.poppy = 0;
			if (this.menuTitle == "Paused")
			{
				this.sc.horn.Play(this.sc.ev * 0.1f, -0.8f, 0f);
				this.sc.SaveSpacePrefs();
				this.sc.Game.IsMouseVisible = false;
			}
			else
			{
				this.sc.back.Play(this.sc.ev, 0f, 0f);
			}
			base.ExitScreen();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0002995D File Offset: 0x00027B5D
		protected void exitPauseScreen(object sender, PlayerIndexEventArgs e)
		{
			this.sc.poppy = 0;
			base.ExitScreen();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00029971 File Offset: 0x00027B71
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
			this.mytimer += 1f;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00029990 File Offset: 0x00027B90
		public override void Draw(GameTime gameTime)
		{
			Vector2 zero = Vector2.Zero;
			if (this.sc.menutype == 0)
			{
				zero.Y = (float)(600 - (this.sc.halo.LineSpacing + 3) * this.menuEntries.Count / 1);
			}
			else
			{
				this.sc.halo = this.sc.halo2;
				zero.Y = (float)(360 - this.menuEntries.Count * (this.sc.halo.LineSpacing + 3) / 2);
			}
			zero.X = this.sc.halo.MeasureString(this.menuEntries[0].Text).X;
			for (int i = 0; i < this.menuEntries.Count; i++)
			{
				if (this.menuEntries[0].Type != 1)
				{
					zero.X = this.sc.halo.MeasureString(this.menuEntries[i].Text).X;
				}
				MenuEntry menuEntry = this.menuEntries[i];
				bool flag = i == this.selectedEntry;
				menuEntry.Draw(this, zero, flag, gameTime, (float)i, (float)this.menuEntries.Count, 1f);
				if (this.point.Count < this.menuEntries.Count)
				{
					Vector2 vector = this.sc.halo2.MeasureString(this.menuEntries[i].Text);
					float num = (1280f - vector.X) / 2f;
					if (this.menuEntries[i].Type == 1)
					{
						num = 640f - (zero.X + 240f) / 2f;
					}
					Rectangle rectangle = new Rectangle(0, 0, 0, 0);
					rectangle = new Rectangle((int)num, (int)(zero.Y + vector.Y * 0.25f), (int)vector.X, (int)(vector.Y * 0.5f));
					this.point.Add(rectangle);
					rectangle = new Rectangle(0, 0, 0, 0);
					if (this.menuEntries[i].Type > 0)
					{
						float x = this.sc.halo2.MeasureString(this.menuEntries[i].Lists[(int)this.menuEntries[i].Amount]).X;
						rectangle = new Rectangle((int)(640f + (zero.X + 90f) / 2f - x / 2f), (int)(zero.Y + vector.Y * 0.25f), (int)x, (int)(vector.Y * 0.5f));
					}
					this.valbox.Add(rectangle);
				}
				zero.Y += menuEntry.GetHeight(this);
			}
			if (this.onceflag == 0)
			{
				base.ScreenManager.drawflag = 1;
				this.onceflag = 1;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00029CA0 File Offset: 0x00027EA0
		public void queryButton(Rectangle rr, int index)
		{
			Vector2 vector = this.sc.mymouse;
			if (this.sc.fullmode)
			{
				float aspectratio = this.sc.aspectratio;
			}
			else
			{
				float num = (float)this.sc.width / (float)this.sc.hite / 1.7777f;
			}
			Vector2 vector2;
			if (this.sc.aspectratio <= 1f)
			{
				vector.Y -= ((float)this.sc.screenSize.Height - (float)this.sc.screenSize.Height * this.sc.aspectratio) * (0.5f / this.sc.aspectratio);
				vector2 = new Vector2((float)this.sc.screenSize.Width / 1280f, (float)this.sc.screenSize.Height / 720f);
			}
			else
			{
				vector.X -= ((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio);
				vector2 = new Vector2((float)this.sc.screenSize.Width / 1280f, (float)this.sc.screenSize.Height / 720f);
			}
			vector /= vector2;
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				this.whichbutton = index;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00029E84 File Offset: 0x00028084
		public void drawBorders()
		{
			float num = 640f - this.percX * 640f;
			base.ScreenManager.siders = num + 5f;
			Matrix matrix = Matrix.CreateScale(1f, this.percY * 144f, 1f) * Matrix.CreateTranslation(num, 360f - this.percY * 360f, 0f);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, this.sc.ScaleMatrix1);
			this.spriteBatch.Draw(this.sc.messageBlob, new Vector2(0f, 0f), new Rectangle?(new Rectangle(0, 0, 12, 5)), Color.White);
			this.spriteBatch.End();
			matrix = Matrix.CreateScale(1f, this.percY * 144f, 1f) * Matrix.CreateTranslation(1275f - num, 360f - this.percY * 360f, 0f);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, matrix);
			this.spriteBatch.Draw(this.sc.messageBlob, new Vector2(-6f, 0f), new Rectangle?(new Rectangle(0, 0, 12, 5)), Color.White);
			this.spriteBatch.End();
			float num2 = 360f - this.percY * 360f;
			matrix = Matrix.CreateScale(this.percX * 256f, 1f, 1f) * Matrix.CreateTranslation(640f - this.percX * 640f, num2, 0f);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, matrix);
			this.spriteBatch.Draw(this.sc.messageBlob, new Vector2(0f, 0f), new Rectangle?(new Rectangle(0, 0, 5, 12)), Color.White);
			this.spriteBatch.End();
			base.ScreenManager.bottomer = 715f - num2;
			base.ScreenManager.topper = num2 + 5f;
			matrix = Matrix.CreateScale(this.percX * 256f, 1f, 1f) * Matrix.CreateTranslation(640f - this.percX * 640f, 715f - num2, 0f);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, matrix);
			this.spriteBatch.Draw(this.sc.messageBlob, new Vector2(0f, -6f), new Rectangle?(new Rectangle(0, 0, 5, 12)), Color.White);
			this.spriteBatch.End();
		}

		// Token: 0x04000572 RID: 1394
		public static PlayerIndex playerIndex = PlayerIndex.One;

		// Token: 0x04000573 RID: 1395
		private float percX = 90f;

		// Token: 0x04000574 RID: 1396
		private float percY = 90f;

		// Token: 0x04000575 RID: 1397
		private SpriteBatch spriteBatch;

		// Token: 0x04000576 RID: 1398
		private MenuEntry myheight;

		// Token: 0x04000577 RID: 1399
		private List<MenuEntry> menuEntries = new List<MenuEntry>();

		// Token: 0x04000578 RID: 1400
		private int selectedEntry;

		// Token: 0x04000579 RID: 1401
		public string menuTitle;

		// Token: 0x0400057A RID: 1402
		public static string title;

		// Token: 0x0400057B RID: 1403
		private ContentManager content;

		// Token: 0x0400057C RID: 1404
		private Matrix projectionMatrix;

		// Token: 0x0400057D RID: 1405
		private Matrix viewMatrix;

		// Token: 0x0400057E RID: 1406
		private float aspectRatio;

		// Token: 0x0400057F RID: 1407
		private Vector3 camlookpos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000580 RID: 1408
		private Vector3 campos = new Vector3(0f, 0f, 2000f);

		// Token: 0x04000581 RID: 1409
		private float mytimer;

		// Token: 0x04000582 RID: 1410
		private int onceflag;

		// Token: 0x04000583 RID: 1411
		public Random rr;

		// Token: 0x04000584 RID: 1412
		private Vector2 textPosition = new Vector2(256f);

		// Token: 0x04000585 RID: 1413
		private float radians = Convert.ToSingle(0.6283185307179586);

		// Token: 0x04000586 RID: 1414
		private ScreenManager sc;

		// Token: 0x04000587 RID: 1415
		private float timeadjust;

		// Token: 0x04000588 RID: 1416
		private string[] lists;

		// Token: 0x04000589 RID: 1417
		private string text;

		// Token: 0x0400058A RID: 1418
		private Texture2D blue;

		// Token: 0x0400058B RID: 1419
		private Texture2D red;

		// Token: 0x0400058C RID: 1420
		private List<Rectangle> point = new List<Rectangle>();

		// Token: 0x0400058D RID: 1421
		private List<Rectangle> valbox = new List<Rectangle>();

		// Token: 0x0400058E RID: 1422
		private int whichbutton;

		// Token: 0x0400058F RID: 1423
		private bool IncDec;

		// Token: 0x04000590 RID: 1424
		private bool delayinput = true;

		// Token: 0x04000591 RID: 1425
		private bool mousemoving;

		// Token: 0x04000592 RID: 1426
		private GamePadState prevstate;

		// Token: 0x04000593 RID: 1427
		private PlayerIndex myplayer;

		// Token: 0x04000594 RID: 1428
		private GamePadState gamePadState;

		// Token: 0x04000595 RID: 1429
		private KeyboardState keyState;

		// Token: 0x04000596 RID: 1430
		private KeyboardState prevkeyState;

		// Token: 0x04000597 RID: 1431
		private static MouseState mouseState;

		// Token: 0x04000598 RID: 1432
		private static MouseState prevMouse;

		// Token: 0x04000599 RID: 1433
		private float mouseX;

		// Token: 0x0400059A RID: 1434
		private float mouseY;
	}
}
