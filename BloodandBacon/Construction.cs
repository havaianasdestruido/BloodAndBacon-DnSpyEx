using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x020000E5 RID: 229
	internal class Construction : MenuScreen
	{
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060007D4 RID: 2004 RVA: 0x001CCB2C File Offset: 0x001CAD2C
		// (remove) Token: 0x060007D5 RID: 2005 RVA: 0x001CCB64 File Offset: 0x001CAD64
		public event EventHandler<PlayerIndexEventArgs> Accepted;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060007D6 RID: 2006 RVA: 0x001CCB9C File Offset: 0x001CAD9C
		// (remove) Token: 0x060007D7 RID: 2007 RVA: 0x001CCBD4 File Offset: 0x001CADD4
		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		// Token: 0x060007D8 RID: 2008 RVA: 0x001CCC34 File Offset: 0x001CAE34
		public Construction(ref Facility fac)
			: base("")
		{
			this.facility = fac;
			this.facilityNeedsLoading = false;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x001CCE7C File Offset: 0x001CB07C
		public Construction()
			: base("")
		{
			this.facility = new Facility();
			Facility.inFacility = false;
			this.facilityNeedsLoading = true;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x001CD0A8 File Offset: 0x001CB2A8
		public override void LoadContent()
		{
			if (this.content == null)
			{
				this.content = new ContentManager(base.ScreenManager.Game.Services, "Content//astro//");
			}
			this.spriteBatch = new SpriteBatch(base.ScreenManager.GraphicsDevice);
			this.sc = base.ScreenManager;
			if (this.facilityNeedsLoading)
			{
				this.facility.LoadContent(this.content, this.sc);
			}
			this.conTexture = this.content.Load<Texture2D>("textures//conTexture");
			this.conTextureBG = this.content.Load<Texture2D>("textures//conTexture2");
			this.dirtmap = this.content.Load<Texture2D>("textures//dirt");
			this.fullscreen = new Rectangle(480, 0, 800, 450);
			this.topbar = new Rectangle(0, 0, 1280, 80);
			this.bottombar = new Rectangle(0, 654, 1280, 66);
			this.topRGB = new Rectangle(155, 0, 39, 113);
			this.bottomRGB = new Rectangle(0, 70, 40, 40);
			this.hammer1 = new Rectangle(0, 533, 80, 67);
			this.hammer2 = new Rectangle(88, 533, 80, 67);
			this.dozer = new Rectangle(176, 533, 80, 67);
			this.infoicon = new Rectangle(0, 602, 257, 262);
			this.cursor = new Rectangle(72, 116, 64, 64);
			this.curX = 640f;
			this.curY = 360f;
			this.buttonB = new Rectangle(198, 0, 26, 26);
			this.salvageR = new Rectangle(0, 212, 90, 90);
			this.powerR = new Rectangle(90, 212, 90, 90);
			this.oxygenR = new Rectangle(180, 212, 90, 90);
			this.longR = new Rectangle(0, 302, 90, 90);
			this.deadR = new Rectangle(90, 302, 90, 90);
			this.trihallR = new Rectangle(180, 302, 90, 90);
			this.hallR = new Rectangle(0, 392, 90, 90);
			this.cornerR = new Rectangle(90, 392, 90, 90);
			this.crossR = new Rectangle(180, 392, 90, 90);
			this.chestR = new Rectangle(0, 482, 30, 30);
			this.gateR = new Rectangle(30, 482, 30, 30);
			this.breakerR = new Rectangle(60, 482, 30, 30);
			this.switchR = new Rectangle(90, 482, 30, 30);
			this.dirtR = new Rectangle(565, 1, 235, 235);
			this.loadParts(ref this.facility.powerPos, "power", this.powerR);
			this.loadParts(ref this.facility.oxygenPos, "oxygen", this.oxygenR);
			this.loadParts(ref this.facility.salvagePos, "salvage", this.salvageR);
			this.loadParts(ref this.facility.trihallPos, "tri", this.trihallR);
			this.loadParts(ref this.facility.crossPos, "cross", this.crossR);
			this.loadParts(ref this.facility.cornerPos, "corner", this.cornerR);
			this.loadParts(ref this.facility.deadPos, "dead", this.deadR);
			this.loadParts(ref this.facility.longPos, "long", this.longR);
			this.loadParts(ref this.facility.hallwayPos, "hall", this.hallR);
			this.loadParts(ref this.facility.gatePos, "gate", this.gateR);
			this.loadParts(ref this.facility.switchPos, "switch", this.switchR);
			this.loadParts(ref this.facility.chestPos, "chest", this.chestR);
			this.screenoffset = new Vector2(0f, -400f);
			this.startwidth2 = (float)this.conTextureBG.Width;
			this.starthite2 = (float)this.conTextureBG.Height;
			this.startaspect2 = this.startwidth2 / this.starthite2;
			this.stretch2 = this.startaspect2 / this.sc.aspectratio2;
			Vector2 vector = new Vector2(1280f, 720f) * 1f;
			if (vector.X / vector.Y >= this.sc.aspectratio2)
			{
				this.src = new Rectangle(0, 0, this.conTextureBG.Width, this.conTextureBG.Height);
				this.amt = (float)this.conTextureBG.Width / vector.X;
				this.destn = new Rectangle((int)((float)this.sc.width * 0.5f), (int)((float)this.sc.hite * 0.5f), (int)((float)this.sc.width * this.amt), (int)((float)this.sc.hite / this.stretch2 * this.amt));
				this.ctr = new Vector2((float)this.conTextureBG.Width, (float)this.conTextureBG.Height) / 2f;
				return;
			}
			this.src = new Rectangle(0, 0, this.conTextureBG.Width, this.conTextureBG.Height);
			this.amt = (float)this.conTextureBG.Height / vector.Y;
			this.destn = new Rectangle((int)((float)this.sc.width * 0.5f), (int)((float)this.sc.hite * 0.5f), (int)((float)this.sc.width * this.stretch2 * this.amt), (int)((float)this.sc.hite * this.amt));
			this.ctr = new Vector2((float)this.conTextureBG.Width, (float)this.conTextureBG.Height) / 2f;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x001CD72B File Offset: 0x001CB92B
		public override void UnloadContent()
		{
			this.content.Unload();
			this.content.Dispose();
			this.content = null;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x001CD74C File Offset: 0x001CB94C
		public void loadParts(ref float[] part, string name, Rectangle rect)
		{
			for (int i = 0; i < part.Length; i += 4)
			{
				Construction.parts parts = new Construction.parts();
				parts.angle = part[i + 3];
				parts.pos = new Vector3(part[i], part[i + 1], part[i + 2]);
				parts.type = name;
				parts.rr = rect;
				this.mypart.Add(parts);
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x001CD7B0 File Offset: 0x001CB9B0
		public void copyParts()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			int num10 = 0;
			int num11 = 0;
			int num12 = 0;
			for (int i = 0; i < this.mypart.Count; i++)
			{
				if (this.mypart[i].type == "gate")
				{
					this.replacePart(ref this.facility.gatePos, i, ref num11);
				}
				if (this.mypart[i].type == "switch")
				{
					this.replacePart(ref this.facility.switchPos, i, ref num12);
				}
				if (this.mypart[i].type == "chest")
				{
					this.replacePart(ref this.facility.chestPos, i, ref num10);
				}
				if (this.mypart[i].type == "power")
				{
					this.replacePart(ref this.facility.powerPos, i, ref num);
				}
				if (this.mypart[i].type == "oxygen")
				{
					this.replacePart(ref this.facility.oxygenPos, i, ref num2);
				}
				if (this.mypart[i].type == "salvage")
				{
					this.replacePart(ref this.facility.salvagePos, i, ref num3);
				}
				if (this.mypart[i].type == "tri")
				{
					this.replacePart(ref this.facility.trihallPos, i, ref num4);
				}
				if (this.mypart[i].type == "cross")
				{
					this.replacePart(ref this.facility.crossPos, i, ref num5);
				}
				if (this.mypart[i].type == "corner")
				{
					this.replacePart(ref this.facility.cornerPos, i, ref num6);
				}
				if (this.mypart[i].type == "hall")
				{
					this.replacePart(ref this.facility.hallwayPos, i, ref num7);
				}
				if (this.mypart[i].type == "long")
				{
					this.replacePart(ref this.facility.longPos, i, ref num8);
				}
				if (this.mypart[i].type == "dead")
				{
					this.replacePart(ref this.facility.deadPos, i, ref num9);
				}
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x001CDA64 File Offset: 0x001CBC64
		public void replacePart(ref float[] part, int i, ref int count)
		{
			int num = count;
			if (part.Length < num + 4)
			{
				Array.Resize<float>(ref part, num + 4);
			}
			part[num] = this.mypart[i].pos.X;
			part[num + 1] = this.mypart[i].pos.Y;
			part[num + 2] = this.mypart[i].pos.Z;
			part[num + 3] = this.mypart[i].angle;
			count += 4;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x001CDAF8 File Offset: 0x001CBCF8
		public new bool KMdown(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = this.mouseState.LeftButton == ButtonState.Pressed;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = this.mouseState.MiddleButton == ButtonState.Pressed;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = this.mouseState.RightButton == ButtonState.Pressed;
			}
			else if (k == Keys.Print)
			{
				flag = this.mouseState.XButton1 == ButtonState.Pressed;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = this.mouseState.XButton2 == ButtonState.Pressed;
			}
			else
			{
				flag = this.keyState.IsKeyDown(k);
			}
			return flag;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x001CDB8C File Offset: 0x001CBD8C
		public new bool KMreleased(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = this.prevMouse.LeftButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = this.prevMouse.MiddleButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = this.prevMouse.RightButton == ButtonState.Released;
			}
			else if (k == Keys.Print)
			{
				flag = this.prevMouse.XButton1 == ButtonState.Released;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = this.prevMouse.XButton2 == ButtonState.Released;
			}
			else
			{
				flag = this.prevkeyState.IsKeyUp(k);
			}
			return flag;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x001CDC20 File Offset: 0x001CBE20
		public new bool KMtoggle(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = this.mouseState.MiddleButton == ButtonState.Pressed && this.prevMouse.MiddleButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = this.mouseState.RightButton == ButtonState.Pressed && this.prevMouse.RightButton == ButtonState.Released;
			}
			else if (k == Keys.Print)
			{
				flag = this.mouseState.XButton1 == ButtonState.Pressed && this.prevMouse.XButton1 == ButtonState.Released;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = this.mouseState.XButton2 == ButtonState.Pressed && this.prevMouse.XButton2 == ButtonState.Released;
			}
			else
			{
				flag = this.keyState.IsKeyDown(k) && this.prevkeyState.IsKeyUp(k);
			}
			return flag;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x001CDD20 File Offset: 0x001CBF20
		public bool Ktoggle(Keys k)
		{
			return this.keyState.IsKeyDown(k) && this.prevkeyState.IsKeyUp(k);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x001CDDA0 File Offset: 0x001CBFA0
		public override void HandleInput(InputState input)
		{
			this.sc.Game.IsMouseVisible = false;
			if (base.ControllingPlayer != null)
			{
				int value = (int)base.ControllingPlayer.Value;
				this.gamestate = input.CurrentGamePadStates[value];
				this.prevstate = input.LastGamePadStates[value];
			}
			this.myframe++;
			this.prevkeyState = input.lastKeyState;
			this.keyState = input.currentKeyState;
			this.prevMouse = this.mouseState;
			this.mouseState = Mouse.GetState();
			if (this.myframe % 120 == 0)
			{
				this.sc.centerWindow();
			}
			float num = (float)this.mouseState.X - this.sc.mymouse.X;
			float num2 = this.sc.mymouse.Y - (float)this.mouseState.Y;
			if (this.delayinput)
			{
				this.prevMouse = this.mouseState;
				this.prevkeyState = this.keyState;
				this.delayinput = false;
			}
			if (this.mouseState.X == (int)this.sc.mymouse.X && this.mouseState.Y == (int)this.sc.mymouse.Y)
			{
				this.mousemoving = false;
			}
			else
			{
				this.mousemoving = true;
			}
			this.sc.mymouse.X = (float)this.mouseState.X;
			this.sc.mymouse.Y = (float)this.mouseState.Y;
			PlayerIndex playerIndex;
			if (this.Ktoggle(Keys.Back) || this.Ktoggle(this.sc.escape_key) || input.IsMenuCancel(base.ControllingPlayer, out playerIndex))
			{
				messagebox messagebox = new messagebox("Are You finished Building?", 1, 0);
				messagebox.Accepted += delegate
				{
					this.copyParts();
					this.facility.resetClickables();
					this.facility.loadClickables();
					this.facility.buildFloor(3);
					this.facility.updateGateCollision();
					base.ScreenManager.menuflag = 0;
					base.ExitScreen();
				};
				messagebox.Cancelled += delegate
				{
				};
				base.ScreenManager.AddScreen(messagebox, null);
			}
			if (!this.sc.usingMouse)
			{
				this.screenoffset.X = this.screenoffset.X - this.gamestate.ThumbSticks.Right.X * 5f;
				this.screenoffset.Y = this.screenoffset.Y + this.gamestate.ThumbSticks.Right.Y * 5f;
			}
			else if (this.KMdown(this.sc.rmb_key))
			{
				float num3 = 0.1f;
				if (num < -num3)
				{
					this.screenoffset.X = this.screenoffset.X - Math.Abs(num);
				}
				if (num > num3)
				{
					this.screenoffset.X = this.screenoffset.X + Math.Abs(num);
				}
				if (num2 < -num3)
				{
					this.screenoffset.Y = this.screenoffset.Y + Math.Abs(num2);
				}
				if (num2 > num3)
				{
					this.screenoffset.Y = this.screenoffset.Y - Math.Abs(num2);
				}
			}
			float num4 = 8f;
			if (this.touching)
			{
				num4 = 6f;
			}
			if (!this.snapped)
			{
				if (!this.sc.usingMouse)
				{
					Vector2 vector = this.gamestate.ThumbSticks.Left * (this.gamestate.ThumbSticks.Left.Length() * this.gamestate.ThumbSticks.Left.Length());
					Vector2 vector2 = -Vector2.One;
					Vector2 one = Vector2.One;
					Vector2.Clamp(ref vector, ref vector2, ref one, out vector);
					this.curX += vector.X * num4;
					this.curY -= vector.Y * num4;
					this.curX = MathHelper.Clamp(this.curX, 40f, 1240f);
					this.curY = MathHelper.Clamp(this.curY, 90f, 650f);
				}
				else
				{
					this.curX = this.sc.adjustVector2(this.sc.mymouse).X;
					this.curY = this.sc.adjustVector2(this.sc.mymouse).Y;
				}
			}
			else
			{
				int num5 = 10;
				int num6 = 400;
				if ((double)this.gamestate.Triggers.Right > 0.5)
				{
					num5 = 4;
				}
				if (this.mypart[this.currentNum].type == "chest" || this.mypart[this.currentNum].type == "switch" || this.mypart[this.currentNum].type == "gate")
				{
					num5 = 5;
					num6 = 50;
					if ((double)this.gamestate.Triggers.Right > 0.5)
					{
						num6 = 200;
					}
				}
				Vector2 left = this.gamestate.ThumbSticks.Left;
				if (this.sc.usingMouse)
				{
					if (this.myframe % num5 == 0)
					{
						float num7 = 0f;
						if (num < -num7)
						{
							Construction.parts parts = this.mypart[this.currentNum];
							parts.pos.X = parts.pos.X - (float)num6;
							this.sc.back.Play(this.sc.ev, 0.3f, 0f);
						}
						if (num > num7)
						{
							Construction.parts parts2 = this.mypart[this.currentNum];
							parts2.pos.X = parts2.pos.X + (float)num6;
							this.sc.back.Play(this.sc.ev, 0.25f, 0f);
						}
						if (num2 < -num7)
						{
							Construction.parts parts3 = this.mypart[this.currentNum];
							parts3.pos.Z = parts3.pos.Z + (float)num6;
							this.sc.back.Play(this.sc.ev, 0.25f, 0f);
						}
						if (num2 > num7)
						{
							Construction.parts parts4 = this.mypart[this.currentNum];
							parts4.pos.Z = parts4.pos.Z - (float)num6;
							this.sc.back.Play(this.sc.ev, 0.3f, 0f);
						}
					}
					Vector2 vector3 = this.sc.adjustVector2(this.sc.mymouse);
					this.mypart[this.currentNum].pos.X = (vector3.X - this.screenoffset.X) * 13.333333f;
					this.mypart[this.currentNum].pos.Z = (vector3.Y - this.screenoffset.Y) * 13.333333f;
					this.mypart[this.currentNum].pos.X = (float)Math.Round((double)(this.mypart[this.currentNum].pos.X / 200f), 0) * 200f;
					this.mypart[this.currentNum].pos.Z = (float)Math.Round((double)(this.mypart[this.currentNum].pos.Z / 200f), 0) * 200f;
				}
				else if (this.myframe % num5 == 0)
				{
					float num8 = 0f;
					if (left.X < -num8)
					{
						Construction.parts parts5 = this.mypart[this.currentNum];
						parts5.pos.X = parts5.pos.X - (float)num6;
						this.sc.back.Play(this.sc.ev, 0.3f, 0f);
					}
					if (left.X > num8)
					{
						Construction.parts parts6 = this.mypart[this.currentNum];
						parts6.pos.X = parts6.pos.X + (float)num6;
						this.sc.back.Play(this.sc.ev, 0.25f, 0f);
					}
					if (left.Y < -num8)
					{
						Construction.parts parts7 = this.mypart[this.currentNum];
						parts7.pos.Z = parts7.pos.Z + (float)num6;
						this.sc.back.Play(this.sc.ev, 0.25f, 0f);
					}
					if (left.Y > num8)
					{
						Construction.parts parts8 = this.mypart[this.currentNum];
						parts8.pos.Z = parts8.pos.Z - (float)num6;
						this.sc.back.Play(this.sc.ev, 0.3f, 0f);
					}
				}
				this.curX = this.screenoffset.X + this.mypart[this.currentNum].pos.X / 13.333333f;
				this.curY = this.screenoffset.Y + this.mypart[this.currentNum].pos.Z / 13.333333f;
				this.curScale = 0.7f;
			}
			if (this.KMtoggle(this.sc.lmb_key) || (this.gamestate.Buttons.A == ButtonState.Pressed && this.prevstate.Buttons.A == ButtonState.Released))
			{
				if (!this.snapped)
				{
					if (this.mypart.Count > 0 && this.currentNum < this.mypart.Count && this.currentNum >= 0)
					{
						this.sc.select.Play(this.sc.ev, -0.2f, 0f);
						this.curX = this.mypart[this.currentNum].pos.X / 13.333333f;
						this.curY = this.mypart[this.currentNum].pos.Z / 13.333333f;
						this.snapped = true;
						this.oldangle = this.mypart[this.currentNum].angle;
					}
				}
				else
				{
					this.mypart[this.currentNum].pos.X = (float)Math.Round((double)(this.mypart[this.currentNum].pos.X / 100f), 0) * 100f;
					this.mypart[this.currentNum].pos.Z = (float)Math.Round((double)(this.mypart[this.currentNum].pos.Z / 100f), 0) * 100f;
					this.snapped = false;
					this.sc.drop.Play(this.sc.ev, 0f, 0f);
				}
			}
			if (this.snapped && (this.Ktoggle(this.sc.a_key) || (this.gamestate.Buttons.LeftShoulder == ButtonState.Pressed && this.prevstate.Buttons.LeftShoulder == ButtonState.Released)) && this.mypart.Count > 0 && this.currentNum < this.mypart.Count && this.currentNum >= 0)
			{
				this.oldangle = this.mypart[this.currentNum].angle;
				this.mypart[this.currentNum].angle += 1.5707964f;
				this.sc.forklift.Play(this.sc.ev * 0.7f, -0.2f, 0f);
			}
			if (this.snapped && (this.Ktoggle(this.sc.d_key) || (this.gamestate.Buttons.RightShoulder == ButtonState.Pressed && this.prevstate.Buttons.RightShoulder == ButtonState.Released)) && this.mypart.Count > 0 && this.currentNum < this.mypart.Count && this.currentNum >= 0)
			{
				this.oldangle = this.mypart[this.currentNum].angle;
				this.mypart[this.currentNum].angle -= 1.5707964f;
				this.sc.forklift.Play(this.sc.ev * 0.7f, -0.1f, 0f);
			}
			this.delayinput = false;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x001CEB28 File Offset: 0x001CCD28
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			if (this.flashTimer > 0)
			{
				this.flashTimer--;
				this.flashAnim += 8.4f;
				if (this.flashAnim > 126f)
				{
					this.flashAnim = 0f;
				}
			}
			this.mytimer += 1f;
			this.cursorBox = new Rectangle((int)(this.curX - 10f), (int)(this.curY - 10f), 20, 20);
			if (this.touching)
			{
				float num = 0.06f;
				this.curScale -= num;
				if (this.curScale < 0.9f)
				{
					this.curScale = 0.9f;
				}
				this.curOpacity += num;
				if (this.curOpacity > 1.1f)
				{
					this.curOpacity = 1.1f;
				}
				this.curRot += num;
			}
			else
			{
				float num2 = 0.06f;
				this.curScale += num2;
				if (this.curScale > 1f)
				{
					this.curScale = 1f;
				}
				this.curOpacity -= num2;
				if (this.curOpacity < 0.9f)
				{
					this.curOpacity = 0.9f;
				}
				this.curRot = 0f;
			}
			base.Update(gameTime, otherScreenHasFocus, false);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x001CEC84 File Offset: 0x001CCE84
		public override void Draw(GameTime gameTime)
		{
			this.touching = false;
			if (!this.snapped)
			{
				this.currentNum = -1;
			}
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearWrap, null, null, null, this.sc.ScaleMatrix1);
			this.spriteBatch.Draw(this.sc.blankTexture, new Vector2(640f, 360f), null, new Color(43, 29, 25, 255), 0f, new Vector2(2f, 2f), 3000f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.dirtmap, new Vector2(640f, 760f) + this.screenoffset, null, Color.White, 0f, new Vector2(640f, 360f), 1.3f, SpriteEffects.None, 0f);
			this.collide1.Clear();
			this.drawParts(this.screenoffset);
			this.spriteBatch.Draw(this.conTexture, new Vector2(this.curX, this.curY), new Rectangle?(this.cursor), Color.White * this.curOpacity, this.curRot, new Vector2(32f, 32f), this.curScale, SpriteEffects.None, 0f);
			this.spriteBatch.End();
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
			this.spriteBatch.Draw(this.conTextureBG, this.destn, new Rectangle?(this.src), Color.White, 0f, this.ctr, SpriteEffects.None, 0f);
			this.spriteBatch.End();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x001CEE54 File Offset: 0x001CD054
		public void drawParts(Vector2 offset)
		{
			for (int i = 0; i < this.mypart.Count; i++)
			{
				float num = this.mypart[i].pos.X / 13.333333f;
				float num2 = this.mypart[i].pos.Z / 13.333333f;
				float num3 = this.mypart[i].angle % 6.2831855f;
				if (num3 < 0f)
				{
					num3 += 6.2831855f;
				}
				int num4 = (int)(num + offset.X);
				int num5 = (int)(num2 + offset.Y);
				Rectangle rectangle = new Rectangle(num4 - 20, num5 - 20, 40, 40);
				float num6 = 45f;
				Color color = new Color(255, 255, 255, 255);
				Color color2 = new Color(130, 255, 130, 255);
				if (this.mypart[i].type == "chest" || this.mypart[i].type == "switch" || this.mypart[i].type == "gate")
				{
					num6 = 15f;
					color = new Color(255, 255, 255, 255);
					rectangle = new Rectangle(num4 - 10, num5 - 10, 20, 20);
					color2 = new Color(130, 255, 130, 255);
				}
				if (!this.collide1.Contains(rectangle))
				{
					this.collide1.Add(rectangle);
				}
				if (!this.snapped && rectangle.Intersects(this.cursorBox))
				{
					this.touching = true;
					this.currentNum = i;
					this.spriteBatch.Draw(this.conTexture, new Vector2(num, num2) + offset, new Rectangle?(this.mypart[i].rr), color2, -num3, new Vector2(num6, num6), 1f, SpriteEffects.None, 0f);
				}
				else if (!this.snapped || this.currentNum != i)
				{
					this.spriteBatch.Draw(this.conTexture, new Vector2(num, num2) + offset, new Rectangle?(this.mypart[i].rr), color, -num3, new Vector2(num6, num6), 1f, SpriteEffects.None, 0f);
				}
			}
			if (this.snapped)
			{
				float num7 = this.mypart[this.currentNum].pos.X / 13.333333f;
				float num8 = this.mypart[this.currentNum].pos.Z / 13.333333f;
				float num9;
				if (this.oldangle != this.mypart[this.currentNum].angle)
				{
					this.oldangle = MathHelper.Lerp(this.oldangle, this.mypart[this.currentNum].angle, 0.1f);
					num9 = this.oldangle % 6.2831855f;
					if (num9 < 0f)
					{
						num9 += 6.2831855f;
					}
				}
				else
				{
					num9 = this.mypart[this.currentNum].angle % 6.2831855f;
					if (num9 < 0f)
					{
						num9 += 6.2831855f;
					}
				}
				float num10 = 1.1f;
				float num11 = 45f;
				Color color3 = new Color(130, 130, 255, 255);
				float num12 = (float)Math.Sin((double)(this.mytimer / 10f)) * 0.04f;
				if (this.mypart[this.currentNum].type == "chest" || this.mypart[this.currentNum].type == "switch" || this.mypart[this.currentNum].type == "gate")
				{
					num12 = 0f;
					num10 = 1f;
					num11 = 15f;
					color3 = new Color(130, 130, 255, 255);
				}
				this.spriteBatch.Draw(this.conTexture, new Vector2(num7, num8) + offset, new Rectangle?(this.mypart[this.currentNum].rr), color3, -num9 + num12, new Vector2(num11, num11), num10, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x04002041 RID: 8257
		private bool delayinput = true;

		// Token: 0x04002042 RID: 8258
		private bool mousemoving;

		// Token: 0x04002043 RID: 8259
		private PlayerIndex myplayer;

		// Token: 0x04002044 RID: 8260
		private GamePadState gamePadState;

		// Token: 0x04002045 RID: 8261
		private KeyboardState keyState;

		// Token: 0x04002046 RID: 8262
		private KeyboardState prevkeyState;

		// Token: 0x04002047 RID: 8263
		private MouseState mouseState;

		// Token: 0x04002048 RID: 8264
		private MouseState prevMouse;

		// Token: 0x04002049 RID: 8265
		private float oldangle;

		// Token: 0x0400204A RID: 8266
		private bool snapped;

		// Token: 0x0400204B RID: 8267
		private Vector2 screenoffset;

		// Token: 0x0400204C RID: 8268
		private SpriteBatch spriteBatch;

		// Token: 0x0400204D RID: 8269
		private Rectangle fullscreen;

		// Token: 0x0400204E RID: 8270
		private Rectangle topbar;

		// Token: 0x0400204F RID: 8271
		private Rectangle bottombar;

		// Token: 0x04002050 RID: 8272
		private Rectangle topRGB;

		// Token: 0x04002051 RID: 8273
		private Rectangle bottomRGB;

		// Token: 0x04002052 RID: 8274
		private Rectangle cursor;

		// Token: 0x04002053 RID: 8275
		private Rectangle buttonB;

		// Token: 0x04002054 RID: 8276
		private Rectangle hammer1;

		// Token: 0x04002055 RID: 8277
		private Rectangle hammer2;

		// Token: 0x04002056 RID: 8278
		private Rectangle dozer;

		// Token: 0x04002057 RID: 8279
		private Rectangle infoicon;

		// Token: 0x04002058 RID: 8280
		private Rectangle chestR;

		// Token: 0x04002059 RID: 8281
		private Rectangle switchR;

		// Token: 0x0400205A RID: 8282
		private Rectangle breakerR;

		// Token: 0x0400205B RID: 8283
		private Rectangle gateR;

		// Token: 0x0400205C RID: 8284
		private Rectangle trihallR;

		// Token: 0x0400205D RID: 8285
		private Rectangle longR;

		// Token: 0x0400205E RID: 8286
		private Rectangle hallR;

		// Token: 0x0400205F RID: 8287
		private Rectangle crossR;

		// Token: 0x04002060 RID: 8288
		private Rectangle deadR;

		// Token: 0x04002061 RID: 8289
		private Rectangle cornerR;

		// Token: 0x04002062 RID: 8290
		private Rectangle oxygenR;

		// Token: 0x04002063 RID: 8291
		private Rectangle powerR;

		// Token: 0x04002064 RID: 8292
		private Rectangle salvageR;

		// Token: 0x04002065 RID: 8293
		private Rectangle dirtR;

		// Token: 0x04002066 RID: 8294
		private Rectangle[] icons;

		// Token: 0x04002067 RID: 8295
		private Vector2[] boxGrid = new Vector2[]
		{
			new Vector2(317f, 260f),
			new Vector2(317f, 345f),
			new Vector2(317f, 430f),
			new Vector2(907f, 260f),
			new Vector2(907f, 345f),
			new Vector2(907f, 430f)
		};

		// Token: 0x04002068 RID: 8296
		private List<Rectangle> collide1 = new List<Rectangle>();

		// Token: 0x04002069 RID: 8297
		private Rectangle collideBut1;

		// Token: 0x0400206A RID: 8298
		private Rectangle collideBut2;

		// Token: 0x0400206B RID: 8299
		private Rectangle cursorBox;

		// Token: 0x0400206C RID: 8300
		private int butpress;

		// Token: 0x0400206D RID: 8301
		private float curX;

		// Token: 0x0400206E RID: 8302
		private float curY;

		// Token: 0x0400206F RID: 8303
		private float curRot;

		// Token: 0x04002070 RID: 8304
		private float curScale = 1f;

		// Token: 0x04002071 RID: 8305
		private float curOpacity = 0.6f;

		// Token: 0x04002072 RID: 8306
		private float boxScaleUp = 1f;

		// Token: 0x04002073 RID: 8307
		private int boxInc;

		// Token: 0x04002074 RID: 8308
		private int boxIndexUp = -1;

		// Token: 0x04002075 RID: 8309
		private float[] boxSize = new float[] { 0.95f, 0.98f, 1f, 1.03f, 1.05f, 1.1f, 1.13f, 1.1f, 1.05f };

		// Token: 0x04002076 RID: 8310
		private string[] boxName = new string[] { "Xtra Attachment", "Solar Panels", "Mining System", "Engine Type", "Wheel Type", "Flatbed Type" };

		// Token: 0x04002077 RID: 8311
		private float boxScaleDown = 1f;

		// Token: 0x04002078 RID: 8312
		private int boxDec = 1;

		// Token: 0x04002079 RID: 8313
		private int boxIndexDown = -1;

		// Token: 0x0400207A RID: 8314
		private float mytimer;

		// Token: 0x0400207B RID: 8315
		private int flashTimer;

		// Token: 0x0400207C RID: 8316
		private float flashAnim;

		// Token: 0x0400207D RID: 8317
		private Rectangle glow = new Rectangle(235, 80, 66, 66);

		// Token: 0x0400207E RID: 8318
		private int flash2 = -1;

		// Token: 0x0400207F RID: 8319
		private float upgradeScale = 0.2f;

		// Token: 0x04002080 RID: 8320
		private float boxOpacity = 0.9f;

		// Token: 0x04002081 RID: 8321
		private int myframe;

		// Token: 0x04002082 RID: 8322
		private int max;

		// Token: 0x04002083 RID: 8323
		private int currentNum = -1;

		// Token: 0x04002084 RID: 8324
		private bool touching;

		// Token: 0x04002085 RID: 8325
		private int touchingBox;

		// Token: 0x04002086 RID: 8326
		private GamePadState gamestate;

		// Token: 0x04002087 RID: 8327
		private GamePadState prevstate;

		// Token: 0x0400208A RID: 8330
		private ScreenManager sc;

		// Token: 0x0400208B RID: 8331
		private ContentManager content;

		// Token: 0x0400208C RID: 8332
		private bool facilityNeedsLoading;

		// Token: 0x0400208D RID: 8333
		private Texture2D conTexture;

		// Token: 0x0400208E RID: 8334
		private Texture2D conTextureBG;

		// Token: 0x0400208F RID: 8335
		private Texture2D dirtmap;

		// Token: 0x04002090 RID: 8336
		private float startwidth2;

		// Token: 0x04002091 RID: 8337
		private float starthite2;

		// Token: 0x04002092 RID: 8338
		private float startaspect2;

		// Token: 0x04002093 RID: 8339
		private float stretch2;

		// Token: 0x04002094 RID: 8340
		private Rectangle src;

		// Token: 0x04002095 RID: 8341
		private Rectangle destn;

		// Token: 0x04002096 RID: 8342
		private float amt;

		// Token: 0x04002097 RID: 8343
		private Vector2 ctr;

		// Token: 0x04002098 RID: 8344
		private Matrix projection;

		// Token: 0x04002099 RID: 8345
		private Matrix view;

		// Token: 0x0400209A RID: 8346
		private Matrix orientation = Matrix.CreateRotationY(1f);

		// Token: 0x0400209B RID: 8347
		private float myRot = 1.5f;

		// Token: 0x0400209C RID: 8348
		private Vector3 sunDir = new Vector3(0.5f, 1f, 0.1f);

		// Token: 0x0400209D RID: 8349
		private Facility facility;

		// Token: 0x0400209E RID: 8350
		private List<Construction.parts> mypart = new List<Construction.parts>();

		// Token: 0x020000E6 RID: 230
		public class parts
		{
			// Token: 0x040020A0 RID: 8352
			public Vector3 pos;

			// Token: 0x040020A1 RID: 8353
			public float angle;

			// Token: 0x040020A2 RID: 8354
			public string type;

			// Token: 0x040020A3 RID: 8355
			public Rectangle rr;
		}
	}
}
