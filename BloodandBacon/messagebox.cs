using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x02000072 RID: 114
	internal class messagebox : GameScreen
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060003EB RID: 1003 RVA: 0x000E3DA4 File Offset: 0x000E1FA4
		// (remove) Token: 0x060003EC RID: 1004 RVA: 0x000E3DDC File Offset: 0x000E1FDC
		public event EventHandler<PlayerIndexEventArgs> Accepted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060003ED RID: 1005 RVA: 0x000E3E14 File Offset: 0x000E2014
		// (remove) Token: 0x060003EE RID: 1006 RVA: 0x000E3E4C File Offset: 0x000E204C
		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060003EF RID: 1007 RVA: 0x000E3E84 File Offset: 0x000E2084
		// (remove) Token: 0x060003F0 RID: 1008 RVA: 0x000E3EBC File Offset: 0x000E20BC
		public event EventHandler<PlayerIndexEventArgs> Response1;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060003F1 RID: 1009 RVA: 0x000E3EF4 File Offset: 0x000E20F4
		// (remove) Token: 0x060003F2 RID: 1010 RVA: 0x000E3F2C File Offset: 0x000E212C
		public event EventHandler<PlayerIndexEventArgs> Response2;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060003F3 RID: 1011 RVA: 0x000E3F64 File Offset: 0x000E2164
		// (remove) Token: 0x060003F4 RID: 1012 RVA: 0x000E3F9C File Offset: 0x000E219C
		public event EventHandler<PlayerIndexEventArgs> Response3;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060003F5 RID: 1013 RVA: 0x000E3FD4 File Offset: 0x000E21D4
		// (remove) Token: 0x060003F6 RID: 1014 RVA: 0x000E400C File Offset: 0x000E220C
		public event EventHandler<PlayerIndexEventArgs> Response4;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060003F7 RID: 1015 RVA: 0x000E4044 File Offset: 0x000E2244
		// (remove) Token: 0x060003F8 RID: 1016 RVA: 0x000E407C File Offset: 0x000E227C
		public event EventHandler<PlayerIndexEventArgs> Response5;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060003F9 RID: 1017 RVA: 0x000E40B4 File Offset: 0x000E22B4
		// (remove) Token: 0x060003FA RID: 1018 RVA: 0x000E40EC File Offset: 0x000E22EC
		public event EventHandler<PlayerIndexEventArgs> Response6;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060003FB RID: 1019 RVA: 0x000E4124 File Offset: 0x000E2324
		// (remove) Token: 0x060003FC RID: 1020 RVA: 0x000E415C File Offset: 0x000E235C
		public event EventHandler<PlayerIndexEventArgs> Response7;

		// Token: 0x060003FD RID: 1021 RVA: 0x000E4194 File Offset: 0x000E2394
		public messagebox(string messagex, int flag, int num)
		{
			this.flag = flag;
			this.message = messagex;
			if (flag == 0)
			{
				this.includeYesNo = false;
				this.includeBG = true;
				this.textgap = 10;
			}
			if (flag == 1)
			{
				this.includeYesNo = true;
				this.includeBG = true;
				this.textgap = 10;
				if (!this.showmiles)
				{
					this.message += "\n";
				}
			}
			if (flag == 2)
			{
				this.includeYesNo = false;
				this.includeBG = true;
				this.textgap = 10;
				this.selects = num;
			}
			base.IsPopup = false;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000E43A8 File Offset: 0x000E25A8
		public override void LoadContent()
		{
			ContentManager content = base.ScreenManager.Game.Content;
			this.sc = base.ScreenManager;
			this.sc.myindex = (int)MathHelper.Clamp((float)this.sc.myindex, 1f, (float)this.selects);
			this.font = this.sc.terminal;
			this.squarefont = this.sc.squarefont;
			this.menuSlate = new Rectangle(114, 12, 480, 244);
			this.cornerR = new Rectangle(139, 433, 88, 88);
			this.borderR = new Rectangle(9, 457, 100, 64);
			this.spriteBatch = base.ScreenManager.SpriteBatch;
			char[] array = new char[] { '\r', '\n' };
			string[] array2 = this.message.Split(array);
			int num = array2.Length;
			this.viewportSize = new Vector2(1280f, 720f);
			this.textSize = this.font.MeasureString(this.message);
			this.textPosition = this.viewportSize / 2f - this.textSize / 2f;
			this.textPosition.Y = this.textPosition.Y - (float)(num * this.textgap / 2);
			this.hPad = 80;
			this.vPad = 30;
			this.mywidth = (float)((int)this.textSize.X + this.hPad * 2);
			this.myhite = (float)((int)this.textSize.Y + this.vPad * 2 + num * this.textgap);
			this.left = (float)((int)this.textPosition.X - this.hPad);
			this.top = (float)((int)this.textPosition.Y - this.vPad);
			this.top = this.viewportSize.Y / 2f - this.myhite / 2f;
			this.right = this.left + this.mywidth;
			this.bottom = this.top + this.myhite;
			this.middle = this.viewportSize.X / 2f;
			this.midhite = this.viewportSize.Y / 2f;
			int num2 = 30;
			this.backgroundRectangle = new Rectangle((int)this.left, (int)this.top, (int)this.mywidth, (int)this.myhite);
			this.topRect = new Rectangle((int)this.middle, (int)this.top, (int)this.mywidth, num2);
			this.botRect = new Rectangle((int)this.middle, (int)this.bottom, (int)this.mywidth, num2);
			this.leftRect = new Rectangle((int)this.left, (int)this.midhite, (int)this.myhite, num2);
			this.rightRect = new Rectangle((int)this.right, (int)this.midhite, (int)this.myhite, num2);
			int num3 = 60;
			this.cornA = new Rectangle((int)this.left, (int)this.top, num3, num3);
			this.cornB = new Rectangle((int)this.right, (int)this.top, num3, num3);
			this.cornC = new Rectangle((int)this.left, (int)this.bottom, num3, num3);
			this.cornD = new Rectangle((int)this.right, (int)this.bottom, num3, num3);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000E4738 File Offset: 0x000E2938
		public override void HandleInput(InputState input)
		{
			this.gamestate = input.CurrentGamePadStates[(int)this.sc.playerindex];
			this.prevstate = input.LastGamePadStates[(int)this.sc.playerindex];
			this.prevKeys = input.lastKeyState;
			this.keyState = input.currentKeyState;
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
			if (this.mouseState.RightButton == ButtonState.Pressed)
			{
				ButtonState rightButton = this.prevMouse.RightButton;
			}
			if (this.delayinput)
			{
				this.prevMouse = this.mouseState;
				this.prevKeys = this.keyState;
				this.prevstate = this.gamestate;
			}
			if (this.flag == 0)
			{
				if (this.keyState.IsKeyDown(Keys.Y))
				{
					this.prevKeys.IsKeyUp(Keys.Y);
				}
				if (this.keyState.IsKeyDown(Keys.N))
				{
					this.prevKeys.IsKeyUp(Keys.N);
				}
				bool flag = this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released;
				bool flag2 = this.mouseState.RightButton == ButtonState.Pressed && this.prevMouse.RightButton == ButtonState.Released;
				if (input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex) || input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) || flag || flag2)
				{
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
			}
			if (this.flag == 1)
			{
				bool flag3 = this.keyState.IsKeyDown(Keys.Y) && this.prevKeys.IsKeyUp(Keys.Y);
				bool flag4 = this.keyState.IsKeyDown(Keys.N) && this.prevKeys.IsKeyUp(Keys.N);
				if (this.mouseState.LeftButton == ButtonState.Pressed)
				{
					ButtonState leftButton = this.prevMouse.LeftButton;
				}
				bool flag5 = this.mouseState.RightButton == ButtonState.Pressed && this.prevMouse.RightButton == ButtonState.Released;
				if (flag3 || input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "YES"))
				{
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				else if (flag5 || flag4 || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "NO"))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
			}
			if (this.flag == 2)
			{
				if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.sc.myindex--;
					if (this.sc.myindex < 1)
					{
						this.sc.myindex = this.selects;
					}
					this.sc.click.Play(this.sc.ev, -0.1f, 0f);
				}
				if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.sc.myindex++;
					if (this.sc.myindex > this.selects)
					{
						this.sc.myindex = 1;
					}
					this.sc.click.Play(this.sc.ev, 0.1f, 0f);
				}
				if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.sc.myindex == 1 && this.Response1 != null)
					{
						this.Response1(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					if (this.sc.myindex == 2 && this.Response2 != null)
					{
						this.Response2(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					if (this.sc.myindex == 3 && this.Response3 != null)
					{
						this.Response3(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					if (this.sc.myindex == 4 && this.Response4 != null)
					{
						this.Response4(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					if (this.sc.myindex == 5 && this.Response5 != null)
					{
						this.Response5(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					if (this.sc.myindex == 6 && this.Response6 != null)
					{
						this.Response6(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					if (this.sc.myindex == 7 && this.Response7 != null)
					{
						this.Response6(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					this.sc.select.Play(this.sc.ev, 0f, 0f);
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				if (input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					if (!this.showmiles)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
			}
			this.delayinput = false;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000E4E44 File Offset: 0x000E3044
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			this.myframe++;
			this.whichButton = "none";
			this.queryButton2(this.yesRect, "YES");
			this.queryButton2(this.okRect, "OKAY");
			this.queryButton2(this.noRect, "NO");
			this.queryButton2(this.arrRect, "ARROW");
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000E4EB8 File Offset: 0x000E30B8
		public override void Draw(GameTime gameTime)
		{
			if (!this.showmiles)
			{
				this.sc.FadeBackBufferToBlack((int)(base.TransitionAlpha * 2 / 3));
			}
			Color color = new Color(255, 255, 255, (int)base.TransitionAlpha);
			Color color2 = new Color(30, 30, 30, (int)base.TransitionAlpha);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, this.sc.ScaleMatrix1);
			if (this.includeBG)
			{
				this.spriteBatch.Draw(base.ScreenManager.messageBlob, this.backgroundRectangle, new Rectangle?(this.menuSlate), color);
				float num = 0.4f;
				if (this.myframe % 70 < 33)
				{
					num = 0.2f;
				}
				this.spriteBatch.Draw(base.ScreenManager.messageBlob, this.cornA, new Rectangle?(this.cornerR), color, 0f, new Vector2((float)this.cornerR.Width * num, (float)this.cornerR.Height * num), SpriteEffects.None, 0f);
				this.spriteBatch.Draw(base.ScreenManager.messageBlob, this.cornB, new Rectangle?(this.cornerR), color, 1.57f, new Vector2((float)this.cornerR.Width * num, (float)this.cornerR.Height * num), SpriteEffects.None, 0f);
				this.spriteBatch.Draw(base.ScreenManager.messageBlob, this.cornC, new Rectangle?(this.cornerR), color, -1.57f, new Vector2((float)this.cornerR.Width * num, (float)this.cornerR.Height * num), SpriteEffects.None, 0f);
				this.spriteBatch.Draw(base.ScreenManager.messageBlob, this.cornD, new Rectangle?(this.cornerR), color, 3.14f, new Vector2((float)this.cornerR.Width * num, (float)this.cornerR.Height * num), SpriteEffects.None, 0f);
			}
			float[] array = new float[8];
			char[] array2 = new char[] { '\r', '\n' };
			string[] array3 = this.message.Split(array2);
			for (int i = 0; i < array3.Length; i++)
			{
				float num2 = this.viewportSize.X / 2f - this.font.MeasureString(array3[i]).X / 2f;
				float num3 = 0f + this.textPosition.Y + (this.textSize.Y / (float)array3.Length + (float)this.textgap) * (float)i;
				this.spriteBatch.DrawString(this.font, array3[i], new Vector2(num2 + 3f, num3 + 1f), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				this.spriteBatch.DrawString(this.font, array3[i], new Vector2(num2 + 1f, num3 + 2f), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				this.spriteBatch.DrawString(this.font, array3[i], new Vector2(num2, num3), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				array[i + 1] = num3;
			}
			if (this.includeYesNo && !this.showmiles)
			{
				string text = "A";
				string text2 = "B";
				string text3 = "  YES         NO";
				Vector2 vector = new Vector2(this.textPosition.X + this.textSize.X / 2f - 15f, this.bottom - (float)this.vPad);
				vector.Y -= 30f + this.squarefont.MeasureString(text2).X / 2f;
				vector.X = this.viewportSize.X / 2f - this.font.MeasureString(text3).X / 2f;
				this.yesRect = new Rectangle((int)(vector.X - 1f), (int)vector.Y, 100, 30);
				if (this.whichButton == "YES")
				{
					int num4 = 30;
					this.spriteBatch.Draw(this.sc.whiteTexture, new Rectangle(this.yesRect.X, this.yesRect.Y + 1, num4, num4), Color.White);
					this.spriteBatch.DrawString(this.squarefont, text, 1f * vector, Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				else
				{
					this.spriteBatch.DrawString(this.squarefont, text, 1f * vector, color, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				vector.X = this.viewportSize.X / 2f - this.font.MeasureString(text3).X / 2f + this.font.MeasureString(" ").X * 12f;
				this.noRect = new Rectangle((int)(1f * (vector.X - 1f)), (int)(1f * vector.Y), 100, 30);
				if (this.whichButton == "NO")
				{
					int num5 = 30;
					this.spriteBatch.Draw(this.sc.whiteTexture, new Rectangle(this.noRect.X, this.noRect.Y + 1, num5, num5), Color.White);
					this.spriteBatch.DrawString(this.squarefont, text2, 1f * vector, Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				else
				{
					this.spriteBatch.DrawString(this.squarefont, text2, 1f * vector, color, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				vector.X = this.viewportSize.X / 2f;
				vector.Y += this.squarefont.MeasureString(text2).X / 2f - 2f;
				this.spriteBatch.DrawString(this.font, text3, 1f * vector, color, 0f, new Vector2(this.font.MeasureString(text3).X / 2f, this.font.MeasureString(text3).Y / 2f), 0.8f, SpriteEffects.None, 0f);
			}
			if (this.flag == 2 || this.flag == 4 || this.flag == 5)
			{
				int num6 = this.myframe / 10 % 3;
				this.spriteBatch.Draw(this.sc.messageBlob, new Vector2(this.left + 20f, array[this.sc.myindex]), new Rectangle?(this.dot1[num6]), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				this.spriteBatch.Draw(this.sc.messageBlob, new Vector2(this.right - 25f - (float)this.dot1[num6].Width, array[this.sc.myindex]), new Rectangle?(this.dot1[num6]), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.flag == 3)
			{
				int num7 = this.myframe / 10 % 3;
				this.spriteBatch.Draw(this.sc.messageBlob, new Vector2(this.left + 20f, array[this.sc.myindex]), new Rectangle?(this.dot1[num7]), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				this.spriteBatch.Draw(this.sc.messageBlob, new Vector2(this.right - 25f - (float)this.dot1[num7].Width, array[this.sc.myindex]), new Rectangle?(this.dot1[num7]), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
			}
			this.spriteBatch.End();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000E57D0 File Offset: 0x000E39D0
		public void queryButton(Rectangle rr, string word)
		{
			bool flag = this.mm.Y > (float)rr.Y && this.mm.Y < (float)(rr.Y + rr.Height) && this.mm.X > (float)rr.X && this.mm.X < (float)(rr.X + rr.Width);
			if (flag)
			{
				this.whichButton = word;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000E5850 File Offset: 0x000E3A50
		public void queryButton2(Rectangle rr, string word)
		{
			Vector2 vector = this.mm;
			if (this.sc.aspectratio <= 1f)
			{
				vector.Y -= ((float)this.sc.screenSize.Height - (float)this.sc.screenSize.Height * this.sc.aspectratio) * (0.5f / this.sc.aspectratio);
			}
			else
			{
				vector.X -= ((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio);
			}
			Vector2 vector2 = new Vector2((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height);
			vector /= vector2;
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				this.whichButton = word;
			}
		}

		// Token: 0x04001015 RID: 4117
		private Keys[] pressedKeys = new Keys[0];

		// Token: 0x04001016 RID: 4118
		private int kt;

		// Token: 0x04001017 RID: 4119
		private int alternator;

		// Token: 0x04001018 RID: 4120
		private bool delayinput = true;

		// Token: 0x04001019 RID: 4121
		private PlayerIndex playerIndex;

		// Token: 0x0400101A RID: 4122
		private int myindex = -1;

		// Token: 0x0400101B RID: 4123
		private string whichButton = "none";

		// Token: 0x0400101C RID: 4124
		private Rectangle yesRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x0400101D RID: 4125
		private Rectangle noRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x0400101E RID: 4126
		private Rectangle okRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x0400101F RID: 4127
		private Rectangle arrRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x04001020 RID: 4128
		private Rectangle meter = new Rectangle(164, 289, 362, 48);

		// Token: 0x04001021 RID: 4129
		private Rectangle meterColor = new Rectangle(164, 346, 362, 48);

		// Token: 0x04001022 RID: 4130
		private Rectangle pointer1 = new Rectangle(1095, 32, 20, 30);

		// Token: 0x04001023 RID: 4131
		private Rectangle pointer2 = new Rectangle(1071, 32, 20, 30);

		// Token: 0x04001024 RID: 4132
		private Rectangle arrows = new Rectangle(541, 289, 39, 84);

		// Token: 0x04001025 RID: 4133
		private Rectangle[] dot1 = new Rectangle[]
		{
			new Rectangle(23, 22, 28, 28),
			new Rectangle(23, 62, 28, 28),
			new Rectangle(23, 97, 28, 28)
		};

		// Token: 0x04001026 RID: 4134
		private Vector2 mm = Vector2.Zero;

		// Token: 0x04001027 RID: 4135
		private string message;

		// Token: 0x04001028 RID: 4136
		private bool includeYesNo;

		// Token: 0x04001029 RID: 4137
		private bool includeBG = true;

		// Token: 0x0400102A RID: 4138
		private int flag;

		// Token: 0x0400102B RID: 4139
		private SpriteFont squarefont;

		// Token: 0x0400102C RID: 4140
		private SpriteBatch spriteBatch;

		// Token: 0x0400102D RID: 4141
		private SpriteFont font;

		// Token: 0x0400102E RID: 4142
		private ScreenManager sc;

		// Token: 0x0400102F RID: 4143
		private Vector2 viewportSize;

		// Token: 0x04001030 RID: 4144
		private Vector2 textSize;

		// Token: 0x04001031 RID: 4145
		private Vector2 textPosition;

		// Token: 0x04001032 RID: 4146
		private int hPad;

		// Token: 0x04001033 RID: 4147
		private int vPad;

		// Token: 0x04001034 RID: 4148
		private int textgap = 10;

		// Token: 0x04001035 RID: 4149
		private float mywidth;

		// Token: 0x04001036 RID: 4150
		private float myhite;

		// Token: 0x04001037 RID: 4151
		private float left;

		// Token: 0x04001038 RID: 4152
		private float top;

		// Token: 0x04001039 RID: 4153
		private float right;

		// Token: 0x0400103A RID: 4154
		private float bottom;

		// Token: 0x0400103B RID: 4155
		private float middle;

		// Token: 0x0400103C RID: 4156
		private float midhite;

		// Token: 0x0400103D RID: 4157
		private Rectangle menuSlate;

		// Token: 0x0400103E RID: 4158
		private Rectangle cornerR;

		// Token: 0x0400103F RID: 4159
		private Rectangle borderR;

		// Token: 0x04001040 RID: 4160
		private Rectangle backgroundRectangle;

		// Token: 0x04001041 RID: 4161
		private Rectangle topRect;

		// Token: 0x04001042 RID: 4162
		private Rectangle botRect;

		// Token: 0x04001043 RID: 4163
		private Rectangle leftRect;

		// Token: 0x04001044 RID: 4164
		private Rectangle rightRect;

		// Token: 0x04001045 RID: 4165
		private Rectangle cornA;

		// Token: 0x04001046 RID: 4166
		private Rectangle cornB;

		// Token: 0x04001047 RID: 4167
		private Rectangle cornC;

		// Token: 0x04001048 RID: 4168
		private Rectangle cornD;

		// Token: 0x04001049 RID: 4169
		private int myframe;

		// Token: 0x0400104A RID: 4170
		private int val1;

		// Token: 0x0400104B RID: 4171
		private int selects;

		// Token: 0x0400104C RID: 4172
		private bool showmiles;

		// Token: 0x0400104D RID: 4173
		private GamePadState gamestate;

		// Token: 0x0400104E RID: 4174
		private GamePadState prevstate;

		// Token: 0x0400104F RID: 4175
		private KeyboardState prevKeys;

		// Token: 0x04001050 RID: 4176
		private KeyboardState keyState;

		// Token: 0x04001051 RID: 4177
		private MouseState prevMouse;

		// Token: 0x04001052 RID: 4178
		private MouseState mouseState;
	}
}
