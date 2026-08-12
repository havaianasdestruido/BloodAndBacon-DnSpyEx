using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x020000E7 RID: 231
	internal class MessageBoxScreen2 : GameScreen
	{
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060007EA RID: 2026 RVA: 0x001CF30C File Offset: 0x001CD50C
		// (remove) Token: 0x060007EB RID: 2027 RVA: 0x001CF344 File Offset: 0x001CD544
		public event EventHandler<PlayerIndexEventArgs> Accepted;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060007EC RID: 2028 RVA: 0x001CF37C File Offset: 0x001CD57C
		// (remove) Token: 0x060007ED RID: 2029 RVA: 0x001CF3B4 File Offset: 0x001CD5B4
		public event EventHandler<PlayerIndexEventArgs> Approved;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060007EE RID: 2030 RVA: 0x001CF3EC File Offset: 0x001CD5EC
		// (remove) Token: 0x060007EF RID: 2031 RVA: 0x001CF424 File Offset: 0x001CD624
		public event EventHandler<PlayerIndexEventArgs> Launch;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060007F0 RID: 2032 RVA: 0x001CF45C File Offset: 0x001CD65C
		// (remove) Token: 0x060007F1 RID: 2033 RVA: 0x001CF494 File Offset: 0x001CD694
		public event EventHandler<PlayerIndexEventArgs> Failed;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060007F2 RID: 2034 RVA: 0x001CF4CC File Offset: 0x001CD6CC
		// (remove) Token: 0x060007F3 RID: 2035 RVA: 0x001CF504 File Offset: 0x001CD704
		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060007F4 RID: 2036 RVA: 0x001CF53C File Offset: 0x001CD73C
		// (remove) Token: 0x060007F5 RID: 2037 RVA: 0x001CF574 File Offset: 0x001CD774
		public event EventHandler<PlayerIndexEventArgs> downloadCancel;

		// Token: 0x060007F6 RID: 2038 RVA: 0x001CF5AC File Offset: 0x001CD7AC
		public MessageBoxScreen2(string messagex, int flag, string password)
		{
			this.password = password;
			this.flag = flag;
			this.message = messagex;
			this.longmessage = "";
			base.IsPopup = true;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x001CF7A0 File Offset: 0x001CD9A0
		public MessageBoxScreen2(string messagex, int flag)
		{
			this.flag = flag;
			this.message = messagex;
			if (flag == 1)
			{
				this.includeYesNo = true;
				this.message = this.message;
			}
			if (flag == 9)
			{
				this.includeYesNo = false;
				this.includeOkay = true;
				this.message = this.message;
			}
			if (flag == 2)
			{
				this.includeYesNo = true;
				this.ipaddress = "";
				this.messone = this.message;
				this.message = this.messone + "\n" + this.ipaddress + "\n";
			}
			if (flag == 3)
			{
				this.includeYesNo = false;
				this.includeMeter = true;
				this.message += "\n";
			}
			this.longmessage = "";
			base.IsPopup = true;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x001CFA30 File Offset: 0x001CDC30
		public MessageBoxScreen2(string messagex, int flag, ulong id)
		{
			this.id = id;
			this.flag = flag;
			this.message = messagex;
			if (flag == 1)
			{
				this.includeYesNo = true;
				this.message = this.message;
			}
			if (flag == 2)
			{
				this.includeYesNo = true;
				this.ipaddress = "";
				this.messone = this.message;
				this.message = this.messone + "\n" + this.ipaddress + "\n";
			}
			if (flag == 3)
			{
				this.includeYesNo = false;
				this.includeMeter = true;
				this.message += "\n";
			}
			this.longmessage = "";
			base.IsPopup = true;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x001CFCA8 File Offset: 0x001CDEA8
		public override void LoadContent()
		{
			ContentManager content = base.ScreenManager.Game.Content;
			this.sc = base.ScreenManager;
			this.sc.forcedout = false;
			this.rr = new Random();
			this.hudMatrix = Matrix.CreateScale((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height, 1f);
			if (this.sc.introCamera > 0f && this.flag != 0)
			{
				this.hudMatrix *= Matrix.CreateTranslation((float)this.sc.myviewport.X, (float)this.sc.myviewport.Y, 0f);
			}
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
			if (this.flag == 4)
			{
				this.includeYesNo = true;
				this.myname = this.sc.lobby.nickname;
				this.myname = this.sc.playername;
				this.messone = this.message;
				this.message = this.messone + "\n" + this.myname + "\n";
			}
			if (this.flag == 55)
			{
				this.includeYesNo = true;
				this.myname = "Log Update 001";
				this.messone = this.message;
				this.message = this.messone + "\n" + this.myname + "\n";
			}
			if (this.flag == 17)
			{
				this.includeYesNo = false;
				this.myname = this.sc.workshop.formMark;
				this.messone = this.message;
				this.message = this.myname;
			}
			if (this.flag == 14)
			{
				this.includeYesNo = false;
				this.myname = this.sc.workshop.formTitle;
				this.messone = this.message;
				this.message = this.myname;
			}
			if (this.flag == 15)
			{
				this.includeYesNo = false;
				this.myname = this.sc.workshop.formDescr;
				this.messone = this.message;
				this.message = this.myname;
			}
			if (this.flag == 16 || this.flag == 18)
			{
				this.includeYesNo = false;
				this.techYesNo = false;
				if (this.flag == 18)
				{
					this.flag = 16;
					this.techYesNo = true;
				}
				this.message = this.message;
			}
			if (this.flag == 6)
			{
				this.includeYesNo = true;
				this.myname = "";
				this.messone = this.message;
				this.message = this.messone + "\n" + this.myname + "\n";
			}
			if (this.flag == 5)
			{
				this.includeYesNo = false;
				this.chatentry = "";
				this.message = "";
			}
			if (this.flag == 7)
			{
				this.includeYesNo = true;
				this.myname = this.sc.lobby.password;
				this.messone = this.message;
				this.message = this.messone + "\n" + this.myname + "\n";
			}
			this.textSize = this.font.MeasureString(this.message);
			this.hPad = 40;
			this.vPad = 40;
			if (this.flag == 15)
			{
				this.textSize.Y = 120f;
				this.textSize.X = 300f;
			}
			if (this.flag == 14 || this.flag == 17)
			{
				this.textSize.X = this.textSize.X + 100f;
				this.textSize.Y = 50f;
			}
			if (this.flag == 16)
			{
				this.textSize = this.sc.fontsmall.MeasureString(this.message);
				char[] array = new char[] { '\r', '\n' };
				string[] array2 = this.message.Split(array);
				if (array2.Length <= 2)
				{
					this.textSize.Y = this.textSize.Y + 15f;
				}
				this.hPad = 15;
				this.vPad = 0;
			}
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

		// Token: 0x060007FA RID: 2042 RVA: 0x001D046C File Offset: 0x001CE66C
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
			if (this.sc.introCamera > 0f && this.sc.introCamera < 5f)
			{
				if (this.Cancelled != null)
				{
					this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
				}
				this.sc.Game.IsMouseVisible = false;
				base.ExitScreen();
			}
			if (this.flag == 0)
			{
				if (flag2 || flag || input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
			}
			if (this.flag == 1)
			{
				bool flag3 = this.keyState.IsKeyDown(Keys.Y) && this.prevKeys.IsKeyUp(Keys.Y);
				bool flag4 = this.keyState.IsKeyDown(Keys.N) && this.prevKeys.IsKeyUp(Keys.N);
				if (flag3 || input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "YES"))
				{
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				else if (flag4 || flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "NO"))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
			}
			if (this.flag == 9)
			{
				bool flag5 = this.keyState.IsKeyDown(Keys.Y) && this.prevKeys.IsKeyUp(Keys.Y);
				bool flag6 = this.keyState.IsKeyDown(Keys.N) && this.prevKeys.IsKeyUp(Keys.N);
				if (flag5 || input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "OKAY"))
				{
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				else if (flag6 || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex) || (this.mouseState.RightButton == ButtonState.Pressed && this.prevMouse.RightButton == ButtonState.Released && this.whichButton == "OKAY"))
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
				if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "YES"))
				{
					if (this.myindex == -1)
					{
						if (this.totaldots == 3 && !this.lastwasdot)
						{
							if (this.Accepted != null)
							{
								this.sc.switch2.Play(this.sc.ev, 1f, 0f);
								char[] array = new char[] { '.' };
								string[] array2 = this.ipaddress.Split(array);
								string text = "";
								for (int i = 0; i < array2.Length; i++)
								{
									if (i > 0)
									{
										text += ".";
									}
									text += Convert.ToInt32(array2[i]).ToString();
								}
								this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
							}
							this.sc.Game.IsMouseVisible = false;
							base.ExitScreen();
						}
						else
						{
							this.sc.abort.Play(this.sc.ev, 0f, 0f);
						}
					}
					else
					{
						if (this.Accepted != null)
						{
							this.sc.switch2.Play(this.sc.ev, 1f, 0f);
							this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						this.sc.Game.IsMouseVisible = false;
						base.ExitScreen();
					}
				}
				else if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "NO"))
				{
					if (this.Cancelled != null)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				this.lastwasdot = true;
				bool flag7 = false;
				this.totaldots = 0;
				int length = this.ipaddress.Length;
				if (length > 0)
				{
					this.lastwasdot = this.ipaddress[length - 1].ToString() == ".";
					for (int j = 0; j < length; j++)
					{
						if (this.ipaddress[j].ToString() == ".")
						{
							this.totaldots++;
						}
					}
				}
				if (length > 2)
				{
					int num = length - 1;
					flag7 = this.ipaddress[num].ToString() != "." && this.ipaddress[num - 1].ToString() != "." && this.ipaddress[num - 2].ToString() != ".";
				}
				if (length < 15)
				{
					if (!flag7)
					{
						if ((this.keyState.IsKeyDown(Keys.D0) && this.prevKeys.IsKeyUp(Keys.D0)) || (this.keyState.IsKeyDown(Keys.NumPad0) && this.prevKeys.IsKeyUp(Keys.NumPad0)))
						{
							this.ipaddress += "0";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D1) && this.prevKeys.IsKeyUp(Keys.D1)) || (this.keyState.IsKeyDown(Keys.NumPad1) && this.prevKeys.IsKeyUp(Keys.NumPad1)))
						{
							this.ipaddress += "1";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D2) && this.prevKeys.IsKeyUp(Keys.D2)) || (this.keyState.IsKeyDown(Keys.NumPad2) && this.prevKeys.IsKeyUp(Keys.NumPad2)))
						{
							this.ipaddress += "2";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D3) && this.prevKeys.IsKeyUp(Keys.D3)) || (this.keyState.IsKeyDown(Keys.NumPad3) && this.prevKeys.IsKeyUp(Keys.NumPad3)))
						{
							this.ipaddress += "3";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D4) && this.prevKeys.IsKeyUp(Keys.D4)) || (this.keyState.IsKeyDown(Keys.NumPad4) && this.prevKeys.IsKeyUp(Keys.NumPad4)))
						{
							this.ipaddress += "4";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D5) && this.prevKeys.IsKeyUp(Keys.D5)) || (this.keyState.IsKeyDown(Keys.NumPad5) && this.prevKeys.IsKeyUp(Keys.NumPad5)))
						{
							this.ipaddress += "5";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D6) && this.prevKeys.IsKeyUp(Keys.D6)) || (this.keyState.IsKeyDown(Keys.NumPad6) && this.prevKeys.IsKeyUp(Keys.NumPad6)))
						{
							this.ipaddress += "6";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D7) && this.prevKeys.IsKeyUp(Keys.D7)) || (this.keyState.IsKeyDown(Keys.NumPad7) && this.prevKeys.IsKeyUp(Keys.NumPad7)))
						{
							this.ipaddress += "7";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D8) && this.prevKeys.IsKeyUp(Keys.D8)) || (this.keyState.IsKeyDown(Keys.NumPad8) && this.prevKeys.IsKeyUp(Keys.NumPad8)))
						{
							this.ipaddress += "8";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D9) && this.prevKeys.IsKeyUp(Keys.D9)) || (this.keyState.IsKeyDown(Keys.NumPad9) && this.prevKeys.IsKeyUp(Keys.NumPad9)))
						{
							this.ipaddress += "9";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.totaldots < 3 && !this.lastwasdot && this.keyState.IsKeyDown(Keys.OemPeriod) && this.prevKeys.IsKeyUp(Keys.OemPeriod))
						{
							this.ipaddress += ".";
							this.sc.tick.Play(this.sc.ev, -0.3f, 0f);
						}
						if (this.totaldots < 3 && !this.lastwasdot && this.keyState.IsKeyDown(Keys.Decimal) && this.prevKeys.IsKeyUp(Keys.Decimal))
						{
							this.ipaddress += ".";
							this.sc.tick.Play(this.sc.ev, -0.3f, 0f);
						}
					}
					else if (flag7 && this.totaldots < 3)
					{
						if ((this.keyState.IsKeyDown(Keys.D0) && this.prevKeys.IsKeyUp(Keys.D0)) || (this.keyState.IsKeyDown(Keys.NumPad0) && this.prevKeys.IsKeyUp(Keys.NumPad0)))
						{
							this.ipaddress += ".0";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D1) && this.prevKeys.IsKeyUp(Keys.D1)) || (this.keyState.IsKeyDown(Keys.NumPad1) && this.prevKeys.IsKeyUp(Keys.NumPad1)))
						{
							this.ipaddress += ".1";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D2) && this.prevKeys.IsKeyUp(Keys.D2)) || (this.keyState.IsKeyDown(Keys.NumPad2) && this.prevKeys.IsKeyUp(Keys.NumPad2)))
						{
							this.ipaddress += ".2";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D3) && this.prevKeys.IsKeyUp(Keys.D3)) || (this.keyState.IsKeyDown(Keys.NumPad3) && this.prevKeys.IsKeyUp(Keys.NumPad3)))
						{
							this.ipaddress += ".3";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D4) && this.prevKeys.IsKeyUp(Keys.D4)) || (this.keyState.IsKeyDown(Keys.NumPad4) && this.prevKeys.IsKeyUp(Keys.NumPad4)))
						{
							this.ipaddress += ".4";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D5) && this.prevKeys.IsKeyUp(Keys.D5)) || (this.keyState.IsKeyDown(Keys.NumPad5) && this.prevKeys.IsKeyUp(Keys.NumPad5)))
						{
							this.ipaddress += ".5";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D6) && this.prevKeys.IsKeyUp(Keys.D6)) || (this.keyState.IsKeyDown(Keys.NumPad6) && this.prevKeys.IsKeyUp(Keys.NumPad6)))
						{
							this.ipaddress += ".6";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D7) && this.prevKeys.IsKeyUp(Keys.D7)) || (this.keyState.IsKeyDown(Keys.NumPad7) && this.prevKeys.IsKeyUp(Keys.NumPad7)))
						{
							this.ipaddress += ".7";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D8) && this.prevKeys.IsKeyUp(Keys.D8)) || (this.keyState.IsKeyDown(Keys.NumPad8) && this.prevKeys.IsKeyUp(Keys.NumPad8)))
						{
							this.ipaddress += ".8";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if ((this.keyState.IsKeyDown(Keys.D9) && this.prevKeys.IsKeyUp(Keys.D9)) || (this.keyState.IsKeyDown(Keys.NumPad9) && this.prevKeys.IsKeyUp(Keys.NumPad9)))
						{
							this.ipaddress += ".9";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.totaldots < 3 && !this.lastwasdot && this.keyState.IsKeyDown(Keys.OemPeriod) && this.prevKeys.IsKeyUp(Keys.OemPeriod))
						{
							this.ipaddress += ".";
							this.sc.tick.Play(this.sc.ev, -0.3f, 0f);
						}
						if (this.totaldots < 3 && !this.lastwasdot && this.keyState.IsKeyDown(Keys.Decimal) && this.prevKeys.IsKeyUp(Keys.Decimal))
						{
							this.ipaddress += ".";
							this.sc.tick.Play(this.sc.ev, -0.3f, 0f);
						}
					}
				}
				if (length > 0 && ((this.keyState.IsKeyDown(Keys.Back) && this.prevKeys.IsKeyUp(Keys.Back)) || (this.keyState.IsKeyDown(Keys.Delete) && this.prevKeys.IsKeyUp(Keys.Delete))))
				{
					string text2 = "";
					for (int k = 0; k < this.ipaddress.Length - 1; k++)
					{
						text2 += this.ipaddress[k].ToString();
					}
					this.ipaddress = text2;
					this.sc.tick.Play(this.sc.ev, -0.6f, 0f);
				}
				string text3 = "_";
				if (this.myframe % 30 < 15)
				{
					text3 = "  ";
				}
				this.message = string.Concat(new string[] { this.messone, "\n", this.ipaddress, text3, "\n" });
				if ((this.keyState.IsKeyDown(Keys.Up) && this.prevKeys.IsKeyUp(Keys.Up)) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "ARROW"))
				{
					this.myindex--;
					if (this.myindex < -1)
					{
						this.myindex = -1;
					}
					this.sc.tick.Play(this.sc.ev, 0f, 0f);
				}
				if (this.keyState.IsKeyDown(Keys.Down) && this.prevKeys.IsKeyUp(Keys.Down))
				{
					this.myindex++;
					this.sc.tick.Play(this.sc.ev, -0.3f, 0f);
				}
			}
			if (this.flag == 3 && input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex) && this.downloadCancel != null)
			{
				this.downloadCancel(this, new PlayerIndexEventArgs(this.playerIndex));
				this.sc.Game.IsMouseVisible = false;
				base.ExitScreen();
			}
			if (this.flag == 4)
			{
				if (input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "YES"))
				{
					this.myname = this.myname.Trim();
					if (this.Approved != null && this.myname != "")
					{
						if (this.myname.ToUpper() == "PLAYER")
						{
							this.myname = this.dummy[this.rr.Next(0, this.dummy.Length)];
						}
						this.sc.lobby.nickname = this.myname;
						this.sc.playername = this.myname;
						this.Approved(this, null);
					}
					else if (this.Failed != null)
					{
						this.Failed(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				else if (input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerIndex) || flag || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "NO"))
				{
					if (this.Cancelled != null)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
						this.Cancelled(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				int length2 = this.myname.Length;
				this.inputKeys3(ref this.myname, length2, 16);
				string text4 = "_";
				if (this.myframe % 30 < 15)
				{
					text4 = "  ";
				}
				this.message = string.Concat(new string[] { this.messone, "\n", this.myname, text4, "\n" });
			}
			if (this.flag == 55)
			{
				if (input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "YES"))
				{
					this.myname = this.myname.Trim();
					if (this.Approved != null && this.myname != "")
					{
						this.sc.workshop.changenotes = this.myname;
						this.Approved(this, null);
					}
					else if (this.Failed != null)
					{
						this.Failed(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				else if (input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerIndex) || flag || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "NO"))
				{
					if (this.Cancelled != null)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
						this.Cancelled(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				int length3 = this.myname.Length;
				this.inputKeys3(ref this.myname, length3, 16);
				string text5 = "_";
				if (this.myframe % 30 < 15)
				{
					text5 = "  ";
				}
				this.message = string.Concat(new string[] { this.messone, "\n", this.myname, text5, "\n" });
			}
			if (this.flag == 14)
			{
				if (input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "YES"))
				{
					this.myname = this.myname.Trim();
					if (this.Approved != null && this.myname != "")
					{
						this.sc.workshop.formTitle = this.myname;
						this.Approved(this, null);
					}
					else if (this.Failed != null)
					{
						this.Failed(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				else if (input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerIndex) || flag || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "NO"))
				{
					if (this.Cancelled != null)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
						this.Cancelled(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				int length4 = this.myname.Length;
				this.inputKeys4(ref this.myname, length4, 21);
				string text6 = "_";
				if (this.myframe % 60 < 30)
				{
					text6 = " ";
				}
				this.message = this.myname + text6 + "\n";
			}
			if (this.flag == 17)
			{
				if (input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "YES"))
				{
					this.myname = this.myname.Trim();
					if (this.Approved != null)
					{
						this.sc.workshop.formMark = this.myname.ToUpper();
						this.Approved(this, null);
					}
					else if (this.Failed != null)
					{
						this.Failed(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				else if (input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerIndex) || flag || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "NO"))
				{
					if (this.Cancelled != null)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
						this.Cancelled(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				int length5 = this.myname.Length;
				this.inputKeys4(ref this.myname, length5, 10);
				string text7 = "_";
				if (this.myframe % 60 < 30)
				{
					text7 = " ";
				}
				this.message = this.myname + text7 + "\n";
			}
			if (this.flag == 15)
			{
				if (input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "YES"))
				{
					this.myname = this.myname.Trim();
					if (this.Approved != null && this.myname != "")
					{
						this.sc.workshop.formDescr = this.myname;
						this.Approved(this, null);
					}
					else if (this.Failed != null)
					{
						this.Failed(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				else if (input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerIndex) || flag || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "NO"))
				{
					if (this.Cancelled != null)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
						this.Cancelled(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				int length6 = this.myname.Length;
				this.inputKeys4(ref this.myname, length6, 90);
				string text8 = "_";
				if (this.myframe % 60 < 30)
				{
					text8 = " ";
				}
				this.message = this.myname + text8;
			}
			if (this.flag == 16)
			{
				if (this.whichButton == "YES" && flag2)
				{
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if ((this.whichButton == "NO" && flag2) || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
			}
			if (this.flag == 5)
			{
				if (this.sc.forcedout)
				{
					this.sc.forcedout = false;
					if (this.Failed != null)
					{
						this.longmessage = "";
						this.Failed(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				if (this.keyState.IsKeyDown(Keys.Enter) && this.prevKeys.IsKeyUp(Keys.Enter))
				{
					if (this.chatentry.Length > 0)
					{
						Color color = Color.White;
						this.sc.chat.message2send = true;
						this.sc.chat.message = this.chatentry;
						if (this.sc.host)
						{
							color = this.sc.hostblue;
						}
						else
						{
							color = this.sc.colors[this.sc.myplayerindex];
						}
						this.sc.addChatMsg(this.sc.lobby.nickname + " : ", color, this.chatentry, Color.LightSteelBlue, 0, this.id);
						this.chatentry = "";
					}
					else
					{
						if (this.Failed != null)
						{
							this.longmessage = "";
							this.Failed(this, null);
						}
						this.sc.Game.IsMouseVisible = false;
						base.ExitScreen();
					}
				}
				else if (this.keyState.IsKeyDown(Keys.Escape) || input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerIndex) || flag)
				{
					if (this.Cancelled != null)
					{
						this.longmessage = "";
						this.Cancelled(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				int length7 = this.chatentry.Length;
				if (this.keyState.IsKeyDown(Keys.Down) && this.prevKeys.IsKeyUp(Keys.Down))
				{
					this.sc.chatIndex--;
					if (this.sc.chatIndex <= 0)
					{
						this.sc.chatIndex = 0;
					}
				}
				if (this.keyState.IsKeyDown(Keys.Up) && this.prevKeys.IsKeyUp(Keys.Up))
				{
					this.sc.chatIndex++;
					if (this.sc.chatIndex > this.sc.chatHistory.Count)
					{
						this.sc.chatIndex = this.sc.chatHistory.Count;
					}
				}
				if (this.keyState.IsKeyDown(Keys.Left) && this.prevKeys.IsKeyUp(Keys.Left))
				{
					this.sc.chatIndex = 0;
				}
				this.inputKeys3(ref this.chatentry, length7, 45);
				string text9 = "|";
				if (this.myframe % 30 < 15)
				{
					text9 = "  ";
				}
				this.message = this.chatentry + text9;
			}
			if (this.flag == 6)
			{
				if (input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "YES"))
				{
					if (this.Approved != null && this.myname == this.password)
					{
						this.Approved(this, null);
					}
					else if (this.Failed != null)
					{
						this.Failed(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				else if (input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerIndex) || flag || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "NO"))
				{
					if (this.Cancelled != null)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
						this.Cancelled(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				int length8 = this.myname.Length;
				this.inputKeys3(ref this.myname, length8, 16);
				string text10 = "_";
				if (this.myframe % 30 < 15)
				{
					text10 = "  ";
				}
				this.message = string.Concat(new string[] { this.messone, "\n", this.myname, text10, "\n" });
			}
			if (this.flag == 7)
			{
				if (input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex) || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "YES"))
				{
					this.myname = this.myname.Trim();
					if (this.Approved != null)
					{
						this.sc.lobby.password = this.myname;
						this.Approved(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				else if (input.IsNewButtonPress(Buttons.B, base.ControllingPlayer, out this.playerIndex) || flag || (this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released && this.whichButton == "NO"))
				{
					if (this.Cancelled != null)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
						this.Cancelled(this, null);
					}
					this.sc.Game.IsMouseVisible = false;
					base.ExitScreen();
				}
				int length9 = this.myname.Length;
				this.inputKeys3(ref this.myname, length9, 16);
				string text11 = "_";
				if (this.myframe % 30 < 15)
				{
					text11 = "  ";
				}
				this.message = string.Concat(new string[] { this.messone, "\n", this.myname, text11, "\n" });
			}
			this.delayinput = false;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x001D2B80 File Offset: 0x001D0D80
		private void inputKeys3(ref string ans, int count, int lim)
		{
			string text = "";
			int num = 40;
			Keys[] array = this.keyState.GetPressedKeys();
			if (array.Length > 0)
			{
				this.kt++;
			}
			bool flag = this.keyState.IsKeyDown(Keys.LeftShift) || this.keyState.IsKeyDown(Keys.RightShift);
			if (count < lim)
			{
				bool flag2 = false;
				if (count == 0 && this.keyState.IsKeyDown(Keys.OemPeriod) && this.prevKeys.IsKeyUp(Keys.OemPeriod))
				{
					text = ":";
					this.sc.tick.Play(this.sc.ev, -0.3f, 0f);
					this.sc.tick.Play(this.sc.ev, -0.8f, 0f);
					flag2 = true;
				}
				if (!flag2)
				{
					if (!flag)
					{
						if (this.keyState.IsKeyDown(Keys.OemComma) && this.prevKeys.IsKeyUp(Keys.OemComma))
						{
							text = ",";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPeriod) && this.prevKeys.IsKeyUp(Keys.OemPeriod))
						{
							text = ".";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemQuestion) && this.prevKeys.IsKeyUp(Keys.OemQuestion))
						{
							text = "/";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemSemicolon) && this.prevKeys.IsKeyUp(Keys.OemSemicolon))
						{
							text = ";";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemOpenBrackets) && this.prevKeys.IsKeyUp(Keys.OemOpenBrackets))
						{
							text = "[";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemCloseBrackets) && this.prevKeys.IsKeyUp(Keys.OemCloseBrackets))
						{
							text = "]";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemMinus) && this.prevKeys.IsKeyUp(Keys.OemMinus))
						{
							text = "-";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPlus) && this.prevKeys.IsKeyUp(Keys.OemPlus))
						{
							text = "=";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemQuotes) && this.prevKeys.IsKeyUp(Keys.OemQuotes))
						{
							text = "'";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPipe) && this.prevKeys.IsKeyUp(Keys.OemPipe))
						{
							text = "\\";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D0) && this.prevKeys.IsKeyUp(Keys.D0))
						{
							text = "0";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D1) && this.prevKeys.IsKeyUp(Keys.D1))
						{
							text = "1";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D2) && this.prevKeys.IsKeyUp(Keys.D2))
						{
							text = "2";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D3) && this.prevKeys.IsKeyUp(Keys.D3))
						{
							text = "3";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D4) && this.prevKeys.IsKeyUp(Keys.D4))
						{
							text = "4";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D5) && this.prevKeys.IsKeyUp(Keys.D5))
						{
							text = "5";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D6) && this.prevKeys.IsKeyUp(Keys.D6))
						{
							text = "6";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D7) && this.prevKeys.IsKeyUp(Keys.D7))
						{
							text = "7";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D8) && this.prevKeys.IsKeyUp(Keys.D8))
						{
							text = "8";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D9) && this.prevKeys.IsKeyUp(Keys.D9))
						{
							text = "9";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.A) && this.prevKeys.IsKeyUp(Keys.A))
						{
							text = "a";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.B) && this.prevKeys.IsKeyUp(Keys.B))
						{
							text = "b";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.C) && this.prevKeys.IsKeyUp(Keys.C))
						{
							text = "c";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D) && this.prevKeys.IsKeyUp(Keys.D))
						{
							text = "d";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.E) && this.prevKeys.IsKeyUp(Keys.E))
						{
							text = "e";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.F) && this.prevKeys.IsKeyUp(Keys.F))
						{
							text = "f";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.G) && this.prevKeys.IsKeyUp(Keys.G))
						{
							text = "g";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.H) && this.prevKeys.IsKeyUp(Keys.H))
						{
							text = "h";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.I) && this.prevKeys.IsKeyUp(Keys.I))
						{
							text = "i";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.J) && this.prevKeys.IsKeyUp(Keys.J))
						{
							text = "j";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.K) && this.prevKeys.IsKeyUp(Keys.K))
						{
							text = "k";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.L) && this.prevKeys.IsKeyUp(Keys.L))
						{
							text = "l";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.M) && this.prevKeys.IsKeyUp(Keys.M))
						{
							text = "m";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.N) && this.prevKeys.IsKeyUp(Keys.N))
						{
							text = "n";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.O) && this.prevKeys.IsKeyUp(Keys.O))
						{
							text = "o";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.P) && this.prevKeys.IsKeyUp(Keys.P))
						{
							text = "p";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Q) && this.prevKeys.IsKeyUp(Keys.Q))
						{
							text = "q";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.R) && this.prevKeys.IsKeyUp(Keys.R))
						{
							text = "r";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.S) && this.prevKeys.IsKeyUp(Keys.S))
						{
							text = "s";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.T) && this.prevKeys.IsKeyUp(Keys.T))
						{
							text = "t";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.U) && this.prevKeys.IsKeyUp(Keys.U))
						{
							text = "u";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.V) && this.prevKeys.IsKeyUp(Keys.V))
						{
							text = "v";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.W) && this.prevKeys.IsKeyUp(Keys.W))
						{
							text = "w";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.X) && this.prevKeys.IsKeyUp(Keys.X))
						{
							text = "x";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Y) && this.prevKeys.IsKeyUp(Keys.Y))
						{
							text = "y";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Z) && this.prevKeys.IsKeyUp(Keys.Z))
						{
							text = "z";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
					}
					else
					{
						if (this.keyState.IsKeyDown(Keys.OemComma) && this.prevKeys.IsKeyUp(Keys.OemComma))
						{
							text = "<";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPeriod) && this.prevKeys.IsKeyUp(Keys.OemPeriod))
						{
							text = ">";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemQuestion) && this.prevKeys.IsKeyUp(Keys.OemQuestion))
						{
							text = "?";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemSemicolon) && this.prevKeys.IsKeyUp(Keys.OemSemicolon))
						{
							text = ":";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemOpenBrackets) && this.prevKeys.IsKeyUp(Keys.OemOpenBrackets))
						{
							text = "{";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemCloseBrackets) && this.prevKeys.IsKeyUp(Keys.OemCloseBrackets))
						{
							text = "}";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemMinus) && this.prevKeys.IsKeyUp(Keys.OemMinus))
						{
							text = "_";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPlus) && this.prevKeys.IsKeyUp(Keys.OemPlus))
						{
							text = "+";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemQuotes) && this.prevKeys.IsKeyUp(Keys.OemQuotes))
						{
							text = "\"";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPipe) && this.prevKeys.IsKeyUp(Keys.OemPipe))
						{
							text = "|";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D0) && this.prevKeys.IsKeyUp(Keys.D0))
						{
							text = ")";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D1) && this.prevKeys.IsKeyUp(Keys.D1))
						{
							text = "!";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D2) && this.prevKeys.IsKeyUp(Keys.D2))
						{
							text = "@";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D3) && this.prevKeys.IsKeyUp(Keys.D3))
						{
							text = "#";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D4) && this.prevKeys.IsKeyUp(Keys.D4))
						{
							text = "$";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D5) && this.prevKeys.IsKeyUp(Keys.D5))
						{
							text = "%";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D6) && this.prevKeys.IsKeyUp(Keys.D6))
						{
							text = "^";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D7) && this.prevKeys.IsKeyUp(Keys.D7))
						{
							text = "&";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D8) && this.prevKeys.IsKeyUp(Keys.D8))
						{
							text = "*";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D9) && this.prevKeys.IsKeyUp(Keys.D9))
						{
							text = "(";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.A) && this.prevKeys.IsKeyUp(Keys.A))
						{
							text = "A";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.B) && this.prevKeys.IsKeyUp(Keys.B))
						{
							text = "B";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.C) && this.prevKeys.IsKeyUp(Keys.C))
						{
							text = "C";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D) && this.prevKeys.IsKeyUp(Keys.D))
						{
							text = "D";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.E) && this.prevKeys.IsKeyUp(Keys.E))
						{
							text = "E";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.F) && this.prevKeys.IsKeyUp(Keys.F))
						{
							text = "F";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.G) && this.prevKeys.IsKeyUp(Keys.G))
						{
							text = "G";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.H) && this.prevKeys.IsKeyUp(Keys.H))
						{
							text = "H";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.I) && this.prevKeys.IsKeyUp(Keys.I))
						{
							text = "I";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.J) && this.prevKeys.IsKeyUp(Keys.J))
						{
							text = "J";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.K) && this.prevKeys.IsKeyUp(Keys.K))
						{
							text = "K";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.L) && this.prevKeys.IsKeyUp(Keys.L))
						{
							text = "L";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.M) && this.prevKeys.IsKeyUp(Keys.M))
						{
							text = "M";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.N) && this.prevKeys.IsKeyUp(Keys.N))
						{
							text = "N";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.O) && this.prevKeys.IsKeyUp(Keys.O))
						{
							text = "O";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.P) && this.prevKeys.IsKeyUp(Keys.P))
						{
							text = "P";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Q) && this.prevKeys.IsKeyUp(Keys.Q))
						{
							text = "Q";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.R) && this.prevKeys.IsKeyUp(Keys.R))
						{
							text = "R";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.S) && this.prevKeys.IsKeyUp(Keys.S))
						{
							text = "S";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.T) && this.prevKeys.IsKeyUp(Keys.T))
						{
							text = "T";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.U) && this.prevKeys.IsKeyUp(Keys.U))
						{
							text = "U";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.V) && this.prevKeys.IsKeyUp(Keys.V))
						{
							text = "V";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.W) && this.prevKeys.IsKeyUp(Keys.W))
						{
							text = "W";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.X) && this.prevKeys.IsKeyUp(Keys.X))
						{
							text = "X";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Y) && this.prevKeys.IsKeyUp(Keys.Y))
						{
							text = "Y";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Z) && this.prevKeys.IsKeyUp(Keys.Z))
						{
							text = "Z";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
					}
				}
				ans += text;
			}
			if (count > 0 && count < lim && this.keyState.IsKeyDown(Keys.Space) && this.prevKeys.IsKeyUp(Keys.Space))
			{
				ans += " ";
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
			}
			if (count > 0 && ((this.keyState.IsKeyDown(Keys.Back) && (this.kt > num || this.prevKeys.IsKeyUp(Keys.Back))) || (this.keyState.IsKeyDown(Keys.Delete) && (this.kt > num || this.prevKeys.IsKeyUp(Keys.Delete)))))
			{
				string text2 = "";
				for (int i = 0; i < ans.Length - 1; i++)
				{
					text2 += ans[i].ToString();
				}
				ans = text2;
				this.sc.tick.Play(this.sc.ev, -0.6f, 0f);
			}
			this.pressedKeys = this.prevKeys.GetPressedKeys();
			if (array.Length > 0)
			{
				if (this.pressedKeys.Length > 0)
				{
					if (array[0] != this.pressedKeys[0])
					{
						this.kt = 0;
						return;
					}
					if (this.kt > num)
					{
						this.kt = num - 4;
						return;
					}
				}
			}
			else
			{
				this.kt = 0;
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x001D48F8 File Offset: 0x001D2AF8
		private void inputKeys4(ref string ans, int count, int lim)
		{
			string text = "";
			int num = 40;
			Keys[] array = this.keyState.GetPressedKeys();
			if (array.Length > 0)
			{
				this.kt++;
			}
			bool flag = this.keyState.IsKeyDown(Keys.LeftShift) || this.keyState.IsKeyDown(Keys.RightShift);
			if (count < lim)
			{
				bool flag2 = false;
				if (count == 0 && this.keyState.IsKeyDown(Keys.OemPeriod) && this.prevKeys.IsKeyUp(Keys.OemPeriod))
				{
					text = ":";
					this.sc.tick.Play(this.sc.ev, -0.3f, 0f);
					this.sc.tick.Play(this.sc.ev, -0.8f, 0f);
					flag2 = true;
				}
				if (!flag2)
				{
					if (!flag)
					{
						if (this.keyState.IsKeyDown(Keys.OemComma) && this.prevKeys.IsKeyUp(Keys.OemComma))
						{
							text = ",";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPeriod) && this.prevKeys.IsKeyUp(Keys.OemPeriod))
						{
							text = ".";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemQuestion) && this.prevKeys.IsKeyUp(Keys.OemQuestion))
						{
							text = "/";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemSemicolon) && this.prevKeys.IsKeyUp(Keys.OemSemicolon))
						{
							text = ";";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemOpenBrackets) && this.prevKeys.IsKeyUp(Keys.OemOpenBrackets))
						{
							text = "[";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemCloseBrackets) && this.prevKeys.IsKeyUp(Keys.OemCloseBrackets))
						{
							text = "]";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemMinus) && this.prevKeys.IsKeyUp(Keys.OemMinus))
						{
							text = "-";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPlus) && this.prevKeys.IsKeyUp(Keys.OemPlus))
						{
							text = "=";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemQuotes) && this.prevKeys.IsKeyUp(Keys.OemQuotes))
						{
							text = "'";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPipe) && this.prevKeys.IsKeyUp(Keys.OemPipe))
						{
							text = "\\";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D0) && this.prevKeys.IsKeyUp(Keys.D0))
						{
							text = "0";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D1) && this.prevKeys.IsKeyUp(Keys.D1))
						{
							text = "1";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D2) && this.prevKeys.IsKeyUp(Keys.D2))
						{
							text = "2";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D3) && this.prevKeys.IsKeyUp(Keys.D3))
						{
							text = "3";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D4) && this.prevKeys.IsKeyUp(Keys.D4))
						{
							text = "4";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D5) && this.prevKeys.IsKeyUp(Keys.D5))
						{
							text = "5";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D6) && this.prevKeys.IsKeyUp(Keys.D6))
						{
							text = "6";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D7) && this.prevKeys.IsKeyUp(Keys.D7))
						{
							text = "7";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D8) && this.prevKeys.IsKeyUp(Keys.D8))
						{
							text = "8";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D9) && this.prevKeys.IsKeyUp(Keys.D9))
						{
							text = "9";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.A) && this.prevKeys.IsKeyUp(Keys.A))
						{
							text = "a";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.B) && this.prevKeys.IsKeyUp(Keys.B))
						{
							text = "b";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.C) && this.prevKeys.IsKeyUp(Keys.C))
						{
							text = "c";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D) && this.prevKeys.IsKeyUp(Keys.D))
						{
							text = "d";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.E) && this.prevKeys.IsKeyUp(Keys.E))
						{
							text = "e";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.F) && this.prevKeys.IsKeyUp(Keys.F))
						{
							text = "f";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.G) && this.prevKeys.IsKeyUp(Keys.G))
						{
							text = "g";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.H) && this.prevKeys.IsKeyUp(Keys.H))
						{
							text = "h";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.I) && this.prevKeys.IsKeyUp(Keys.I))
						{
							text = "i";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.J) && this.prevKeys.IsKeyUp(Keys.J))
						{
							text = "j";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.K) && this.prevKeys.IsKeyUp(Keys.K))
						{
							text = "k";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.L) && this.prevKeys.IsKeyUp(Keys.L))
						{
							text = "l";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.M) && this.prevKeys.IsKeyUp(Keys.M))
						{
							text = "m";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.N) && this.prevKeys.IsKeyUp(Keys.N))
						{
							text = "n";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.O) && this.prevKeys.IsKeyUp(Keys.O))
						{
							text = "o";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.P) && this.prevKeys.IsKeyUp(Keys.P))
						{
							text = "p";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Q) && this.prevKeys.IsKeyUp(Keys.Q))
						{
							text = "q";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.R) && this.prevKeys.IsKeyUp(Keys.R))
						{
							text = "r";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.S) && this.prevKeys.IsKeyUp(Keys.S))
						{
							text = "s";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.T) && this.prevKeys.IsKeyUp(Keys.T))
						{
							text = "t";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.U) && this.prevKeys.IsKeyUp(Keys.U))
						{
							text = "u";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.V) && this.prevKeys.IsKeyUp(Keys.V))
						{
							text = "v";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.W) && this.prevKeys.IsKeyUp(Keys.W))
						{
							text = "w";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.X) && this.prevKeys.IsKeyUp(Keys.X))
						{
							text = "x";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Y) && this.prevKeys.IsKeyUp(Keys.Y))
						{
							text = "y";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Z) && this.prevKeys.IsKeyUp(Keys.Z))
						{
							text = "z";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
					}
					else
					{
						if (this.keyState.IsKeyDown(Keys.OemComma) && this.prevKeys.IsKeyUp(Keys.OemComma))
						{
							text = "<";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPeriod) && this.prevKeys.IsKeyUp(Keys.OemPeriod))
						{
							text = ">";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemQuestion) && this.prevKeys.IsKeyUp(Keys.OemQuestion))
						{
							text = "?";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemSemicolon) && this.prevKeys.IsKeyUp(Keys.OemSemicolon))
						{
							text = ":";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemOpenBrackets) && this.prevKeys.IsKeyUp(Keys.OemOpenBrackets))
						{
							text = "{";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemCloseBrackets) && this.prevKeys.IsKeyUp(Keys.OemCloseBrackets))
						{
							text = "}";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemMinus) && this.prevKeys.IsKeyUp(Keys.OemMinus))
						{
							text = "_";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPlus) && this.prevKeys.IsKeyUp(Keys.OemPlus))
						{
							text = "+";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemQuotes) && this.prevKeys.IsKeyUp(Keys.OemQuotes))
						{
							text = "\"";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.OemPipe) && this.prevKeys.IsKeyUp(Keys.OemPipe))
						{
							text = "|";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D0) && this.prevKeys.IsKeyUp(Keys.D0))
						{
							text = ")";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D1) && this.prevKeys.IsKeyUp(Keys.D1))
						{
							text = "!";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D2) && this.prevKeys.IsKeyUp(Keys.D2))
						{
							text = "@";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D3) && this.prevKeys.IsKeyUp(Keys.D3))
						{
							text = "#";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D4) && this.prevKeys.IsKeyUp(Keys.D4))
						{
							text = "$";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D5) && this.prevKeys.IsKeyUp(Keys.D5))
						{
							text = "%";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D6) && this.prevKeys.IsKeyUp(Keys.D6))
						{
							text = "^";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D7) && this.prevKeys.IsKeyUp(Keys.D7))
						{
							text = "&";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D8) && this.prevKeys.IsKeyUp(Keys.D8))
						{
							text = "*";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D9) && this.prevKeys.IsKeyUp(Keys.D9))
						{
							text = "(";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.A) && this.prevKeys.IsKeyUp(Keys.A))
						{
							text = "A";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.B) && this.prevKeys.IsKeyUp(Keys.B))
						{
							text = "B";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.C) && this.prevKeys.IsKeyUp(Keys.C))
						{
							text = "C";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.D) && this.prevKeys.IsKeyUp(Keys.D))
						{
							text = "D";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.E) && this.prevKeys.IsKeyUp(Keys.E))
						{
							text = "E";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.F) && this.prevKeys.IsKeyUp(Keys.F))
						{
							text = "F";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.G) && this.prevKeys.IsKeyUp(Keys.G))
						{
							text = "G";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.H) && this.prevKeys.IsKeyUp(Keys.H))
						{
							text = "H";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.I) && this.prevKeys.IsKeyUp(Keys.I))
						{
							text = "I";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.J) && this.prevKeys.IsKeyUp(Keys.J))
						{
							text = "J";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.K) && this.prevKeys.IsKeyUp(Keys.K))
						{
							text = "K";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.L) && this.prevKeys.IsKeyUp(Keys.L))
						{
							text = "L";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.M) && this.prevKeys.IsKeyUp(Keys.M))
						{
							text = "M";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.N) && this.prevKeys.IsKeyUp(Keys.N))
						{
							text = "N";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.O) && this.prevKeys.IsKeyUp(Keys.O))
						{
							text = "O";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.P) && this.prevKeys.IsKeyUp(Keys.P))
						{
							text = "P";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Q) && this.prevKeys.IsKeyUp(Keys.Q))
						{
							text = "Q";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.R) && this.prevKeys.IsKeyUp(Keys.R))
						{
							text = "R";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.S) && this.prevKeys.IsKeyUp(Keys.S))
						{
							text = "S";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.T) && this.prevKeys.IsKeyUp(Keys.T))
						{
							text = "T";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.U) && this.prevKeys.IsKeyUp(Keys.U))
						{
							text = "U";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.V) && this.prevKeys.IsKeyUp(Keys.V))
						{
							text = "V";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.W) && this.prevKeys.IsKeyUp(Keys.W))
						{
							text = "W";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.X) && this.prevKeys.IsKeyUp(Keys.X))
						{
							text = "X";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Y) && this.prevKeys.IsKeyUp(Keys.Y))
						{
							text = "Y";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						if (this.keyState.IsKeyDown(Keys.Z) && this.prevKeys.IsKeyUp(Keys.Z))
						{
							text = "Z";
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
					}
				}
				ans += text;
			}
			if (count > 0 && count < lim && this.keyState.IsKeyDown(Keys.Space) && this.prevKeys.IsKeyUp(Keys.Space))
			{
				ans += " ";
				this.sc.tick.Play(this.sc.ev, 0f, 0f);
			}
			if (count > 0 && ((this.keyState.IsKeyDown(Keys.Back) && (this.kt > num || this.prevKeys.IsKeyUp(Keys.Back))) || (this.keyState.IsKeyDown(Keys.Delete) && (this.kt > num || this.prevKeys.IsKeyUp(Keys.Delete)))))
			{
				string text2 = "";
				for (int i = 0; i < ans.Length - 1; i++)
				{
					text2 += ans[i].ToString();
				}
				ans = text2;
				this.sc.tick.Play(this.sc.ev, -0.6f, 0f);
			}
			this.pressedKeys = this.prevKeys.GetPressedKeys();
			if (array.Length > 0)
			{
				if (this.pressedKeys.Length > 0)
				{
					if (array[0] != this.pressedKeys[0])
					{
						this.kt = 0;
						return;
					}
					if (this.kt > num)
					{
						this.kt = num - 8;
						return;
					}
				}
			}
			else
			{
				this.kt = 0;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x001D6670 File Offset: 0x001D4870
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			this.myframe++;
			this.whichButton = "none";
			this.queryButton(this.yesRect, "YES");
			this.queryButton(this.okRect, "OKAY");
			this.queryButton(this.noRect, "NO");
			this.queryButton(this.arrRect, "ARROW");
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x001D66E4 File Offset: 0x001D48E4
		public override void Draw(GameTime gameTime)
		{
			if (this.flag != 5)
			{
				base.ScreenManager.FadeBackBufferToBlack((int)(base.TransitionAlpha * 2 / 3));
			}
			Color color = new Color(255, 255, 255, (int)base.TransitionAlpha);
			Color color2 = new Color(30, 30, 30, (int)base.TransitionAlpha);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, this.hudMatrix);
			if (this.flag != 5 && this.flag != 15 && this.flag != 14 && this.flag != 16)
			{
				this.spriteBatch.Draw(base.ScreenManager.menuBlob2, this.backgroundRectangle, new Rectangle?(this.menuSlate), color);
				char[] array = new char[] { '\r', '\n' };
				string[] array2 = this.message.Split(array);
				for (int i = 0; i < array2.Length; i++)
				{
					float num = this.textPosition.X - this.font.MeasureString(array2[i]).X / 2f;
					float num2 = this.textPosition.Y + this.textSize.Y / (float)array2.Length * (float)i;
					this.spriteBatch.DrawString(this.font, array2[i], new Vector2(num + 3f, num2 + 1f), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
					this.spriteBatch.DrawString(this.font, array2[i], new Vector2(num + 1f, num2 + 2f), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
					this.spriteBatch.DrawString(this.font, array2[i], new Vector2(num, num2), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				}
			}
			if (this.flag == 16)
			{
				this.spriteBatch.Draw(base.ScreenManager.menuBlob2, this.backgroundRectangle, new Rectangle?(this.bgBlue), color);
				char[] array3 = new char[] { '\r', '\n' };
				float num3 = 0.65f;
				string[] array4 = this.message.Split(array3);
				int num4 = 17;
				if (array4.Length > 2)
				{
					num4 = 30;
				}
				for (int j = 0; j < array4.Length; j++)
				{
					float num5 = this.textPosition.X - this.sc.fontsmall.MeasureString(array4[j]).X / 2f;
					float num6 = (float)num4 + this.textPosition.Y + this.textSize.Y * num3 / (float)array4.Length * (float)j;
					this.spriteBatch.DrawString(this.sc.fontsmall, array4[j], new Vector2(num5 + 3f, num6 + 1f), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
					this.spriteBatch.DrawString(this.sc.fontsmall, array4[j], new Vector2(num5 + 1f, num6 + 2f), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
					this.spriteBatch.DrawString(this.sc.fontsmall, array4[j], new Vector2(num5, num6), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				}
				if (!this.techYesNo)
				{
					string text = "X";
					string text2 = "  OKAY";
					string text3 = "OKAY  ";
					Vector2 vector = new Vector2(0f, this.bottom - 25f);
					vector.Y -= this.squarefont.MeasureString(text).X / 3f;
					vector.X = this.textPosition.X - this.sc.fontsmall.MeasureString(text3).X / 2f;
					this.yesRect = new Rectangle((int)(vector.X - 1f), (int)vector.Y, 60, 30);
					if (this.whichButton == "YES")
					{
						this.spriteBatch.Draw(this.sc.overlay, new Rectangle(this.yesRect.X, this.yesRect.Y + 1, 30, 30), new Rectangle?(this.whitebox), Color.White);
						this.spriteBatch.DrawString(this.squarefont, text, vector, Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
					}
					else
					{
						this.spriteBatch.DrawString(this.squarefont, text, vector, color, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
					}
					vector.X = this.textPosition.X + 22f;
					vector.Y += 20f;
					this.spriteBatch.DrawString(this.sc.fontsmall, text2, vector, color, 0f, new Vector2(this.sc.fontsmall.MeasureString(text2).X / 2f, this.sc.fontsmall.MeasureString(text2).Y / 2f), 0.8f, SpriteEffects.None, 0f);
				}
				else
				{
					string text4 = "Y";
					string text5 = "N";
					string text6 = "  YES              NO";
					string text7 = "YES              NO";
					Vector2 vector2 = new Vector2(0f, this.bottom - 25f);
					vector2.Y -= this.squarefont.MeasureString(text5).X / 3f;
					vector2.X = this.textPosition.X - this.sc.fontsmall.MeasureString(text7).X / 2f;
					this.yesRect = new Rectangle((int)(vector2.X - 1f), (int)vector2.Y, 60, 30);
					if (this.whichButton == "YES")
					{
						this.spriteBatch.Draw(this.sc.overlay, new Rectangle(this.yesRect.X, this.yesRect.Y + 1, 30, 30), new Rectangle?(this.whitebox), Color.White);
						this.spriteBatch.DrawString(this.squarefont, text4, vector2, Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
					}
					else
					{
						this.spriteBatch.DrawString(this.squarefont, text4, vector2, color, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
					}
					vector2.X = 5f + (this.textPosition.X - this.sc.fontsmall.MeasureString(text7).X / 2f) + this.sc.fontsmall.MeasureString(" ").X * 17f;
					this.noRect = new Rectangle((int)(vector2.X - 1f), (int)vector2.Y, 60, 30);
					if (this.whichButton == "NO")
					{
						this.spriteBatch.Draw(this.sc.overlay, new Rectangle(this.noRect.X, this.noRect.Y + 1, 30, 30), new Rectangle?(this.whitebox), Color.White);
						this.spriteBatch.DrawString(this.squarefont, text5, vector2, Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
					}
					else
					{
						this.spriteBatch.DrawString(this.squarefont, text5, vector2, color, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
					}
					vector2.X = this.textPosition.X + 17f;
					vector2.Y += 20f;
					this.spriteBatch.DrawString(this.sc.fontsmall, text6, vector2, color, 0f, new Vector2(this.sc.fontsmall.MeasureString(text6).X / 2f, this.sc.fontsmall.MeasureString(text6).Y / 2f), 0.8f, SpriteEffects.None, 0f);
				}
			}
			if (this.flag == 15 || this.flag == 14 || this.flag == 17)
			{
				this.spriteBatch.Draw(base.ScreenManager.menuBlob2, this.backgroundRectangle, new Rectangle?(this.bgBlue), color);
				string text8 = "DESCRIPTION";
				if (this.flag == 14)
				{
					text8 = "TITLE";
				}
				if (this.flag == 17)
				{
					text8 = "WaterMark";
				}
				Vector2 vector3 = new Vector2(640f - this.sc.fontsmall.MeasureString(text8).X / 2f, this.top);
				this.spriteBatch.DrawString(this.sc.fontsmall, text8, new Vector2(vector3.X + 3f, vector3.Y + 1f), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				this.spriteBatch.DrawString(this.sc.fontsmall, text8, new Vector2(vector3.X + 1f, vector3.Y + 2f), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				this.spriteBatch.DrawString(this.sc.fontsmall, text8, vector3, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				this.me.Clear();
				string[] array5 = this.message.Split(new char[] { ' ' });
				string text9 = "";
				bool flag = false;
				for (int k = 0; k < array5.Length; k++)
				{
					text9 = text9 + array5[k] + " ";
					flag = false;
					if (text9.Length > 22)
					{
						this.me.Add(text9 + "\n");
						text9 = "";
						flag = true;
					}
				}
				if (!flag)
				{
					this.me.Add(text9);
				}
				for (int l = 0; l < this.me.Count; l++)
				{
					float num7 = this.textPosition.X - this.sc.fontsmall.MeasureString(this.me[l]).X / 2f;
					float num8 = 5f + this.textPosition.Y + (float)(26 * l);
					this.spriteBatch.DrawString(this.sc.fontsmall, this.me[l], new Vector2(num7 + 3f, num8 + 1f), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
					this.spriteBatch.DrawString(this.sc.fontsmall, this.me[l], new Vector2(num7 + 1f, num8 + 2f), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
					this.spriteBatch.DrawString(this.sc.fontsmall, this.me[l], new Vector2(num7, num8), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				}
				string text10 = "Y";
				string text11 = "N";
				string text12 = "  YES              NO";
				string text13 = "YES              NO";
				Vector2 vector4 = new Vector2(0f, this.bottom - 25f);
				vector4.Y -= this.squarefont.MeasureString(text11).X / 3f;
				vector4.X = this.textPosition.X - this.sc.fontsmall.MeasureString(text13).X / 2f;
				this.yesRect = new Rectangle((int)(vector4.X - 1f), (int)vector4.Y, 60, 30);
				if (this.whichButton == "YES")
				{
					this.spriteBatch.Draw(this.sc.overlay, new Rectangle(this.yesRect.X, this.yesRect.Y + 1, 30, 30), new Rectangle?(this.whitebox), Color.White);
					this.spriteBatch.DrawString(this.squarefont, text10, vector4, Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				else
				{
					this.spriteBatch.DrawString(this.squarefont, text10, vector4, color, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				vector4.X = 5f + (this.textPosition.X - this.sc.fontsmall.MeasureString(text13).X / 2f) + this.sc.fontsmall.MeasureString(" ").X * 17f;
				this.noRect = new Rectangle((int)(vector4.X - 1f), (int)vector4.Y, 60, 30);
				if (this.whichButton == "NO")
				{
					this.spriteBatch.Draw(this.sc.overlay, new Rectangle(this.noRect.X, this.noRect.Y + 1, 30, 30), new Rectangle?(this.whitebox), Color.White);
					this.spriteBatch.DrawString(this.squarefont, text11, vector4, Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				else
				{
					this.spriteBatch.DrawString(this.squarefont, text11, vector4, color, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				vector4.X = this.textPosition.X + 17f;
				vector4.Y += 20f;
				this.spriteBatch.DrawString(this.sc.fontsmall, text12, vector4, color, 0f, new Vector2(this.sc.fontsmall.MeasureString(text12).X / 2f, this.sc.fontsmall.MeasureString(text12).Y / 2f), 0.8f, SpriteEffects.None, 0f);
			}
			if (this.flag == 5)
			{
				int num9 = this.viewport.X + 20;
				int num10 = this.viewport.Height - 40;
				int num11 = Math.Max((int)this.font2.MeasureString("X").X * 32, (int)this.font2.MeasureString(this.message).X);
				this.spriteBatch.Draw(this.sc.blackTexture, new Rectangle(num9 + 5, num10 - 20, num11, (int)(0.6f * this.font2.MeasureString("X").Y)), new Color(1, 1, 1, 255));
				char[] array6 = new char[] { '\r', '\n' };
				string[] array7 = this.message.Split(array6);
				for (int m = 0; m < array7.Length; m++)
				{
					this.spriteBatch.DrawString(this.font2, array7[m], new Vector2((float)(num9 + 15), (float)(num10 - 29)), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				}
				if (this.sc.chatHistory.Count > 0)
				{
					int num12 = 0;
					int num13 = this.sc.chatHistory.Count - 1 - this.sc.chatIndex;
					num13 = Math.Max(0, num13);
					int num14 = Math.Max(num13 - 13, 0);
					float num15 = 1f;
					for (int n = num13; n >= num14; n--)
					{
						num12 += 20;
						if (num12 >= 60)
						{
							num15 -= 0.055f;
						}
						this.spriteBatch.DrawString(this.font2, this.sc.chatHistory[n].name, new Vector2((float)(this.viewport.X + 25), (float)(num10 - 50 - num12)), this.sc.chatHistory[n].nameColor * num15, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
						if (this.sc.chatHistory[n].gap == 0)
						{
							this.spriteBatch.DrawString(this.font2, this.sc.chatHistory[n].message, new Vector2(this.font2.MeasureString(this.sc.chatHistory[n].name).X * 0.8f + (float)this.viewport.X + 25f, (float)(num10 - 50 - num12)), this.sc.chatHistory[n].messColor * num15, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
						}
						else
						{
							this.spriteBatch.DrawString(this.font2, this.sc.chatHistory[n].message, new Vector2((float)(this.sc.chatHistory[n].gap + this.viewport.X + 25), (float)(num10 - 50 - num12)), this.sc.chatHistory[n].messColor * num15, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
						}
					}
				}
			}
			if (this.includeOkay)
			{
				string text14 = "X O";
				Vector2 vector5 = new Vector2(0f, this.bottom - (float)this.vPad - this.squarefont.MeasureString(text14).Y / 2f);
				vector5.X = this.textPosition.X - this.squarefont.MeasureString(text14).X / 2f;
				this.okRect = new Rectangle((int)vector5.X - 1, (int)vector5.Y, 60, 30);
				if (this.whichButton == "OKAY")
				{
					this.spriteBatch.Draw(this.sc.overlay, new Rectangle(this.okRect.X, this.okRect.Y, 30, 30), new Rectangle?(this.whitebox), Color.White);
					this.spriteBatch.DrawString(this.squarefont, "X", vector5, Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				else
				{
					this.spriteBatch.DrawString(this.squarefont, "X", vector5, color, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				vector5.X = this.textPosition.X;
				vector5.Y += this.squarefont.MeasureString("okay").Y / 3f + 2f;
				this.spriteBatch.DrawString(this.font, "okay", vector5, color, 0f, new Vector2(this.font.MeasureString("x").X / 2f, this.font.MeasureString("x okay").Y / 2f), 0.8f, SpriteEffects.None, 0f);
			}
			if (this.includeYesNo)
			{
				string text15 = "Y    N";
				string text16 = "Y";
				string text17 = "N";
				string text18 = "  YES         NO";
				string text19 = "YES         NO";
				Vector2 vector6 = new Vector2(0f, this.bottom - (float)this.vPad - this.squarefont.MeasureString(text15).Y / 2f);
				vector6.Y -= this.squarefont.MeasureString(text17).X / 3f;
				vector6.X = this.textPosition.X - this.font.MeasureString(text19).X / 2f;
				this.yesRect = new Rectangle((int)(vector6.X - 1f), (int)(vector6.Y + 1f), 60, 30);
				if (this.whichButton == "YES")
				{
					this.spriteBatch.Draw(this.sc.overlay, new Rectangle(this.yesRect.X, this.yesRect.Y, 30, 30), new Rectangle?(this.whitebox), Color.White);
					this.spriteBatch.DrawString(this.squarefont, text16, vector6, Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				else
				{
					this.spriteBatch.DrawString(this.squarefont, text16, vector6, color, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				vector6.X = this.textPosition.X - this.font.MeasureString(text19).X / 2f + this.font.MeasureString(" ").X * 13f;
				this.noRect = new Rectangle((int)(vector6.X - 1f), (int)vector6.Y, 60, 30);
				if (this.whichButton == "NO")
				{
					this.spriteBatch.Draw(this.sc.overlay, new Rectangle(this.noRect.X, this.noRect.Y + 1, 30, 30), new Rectangle?(this.whitebox), Color.White);
					this.spriteBatch.DrawString(this.squarefont, text17, vector6, Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				else
				{
					this.spriteBatch.DrawString(this.squarefont, text17, vector6, color, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
				}
				vector6.X = this.textPosition.X;
				vector6.Y += this.squarefont.MeasureString(text17).X / 2f - 0f;
				this.spriteBatch.DrawString(this.font, text18, vector6, color, 0f, new Vector2(this.font.MeasureString(text18).X / 2f, this.font.MeasureString(text18).Y / 2f), 0.8f, SpriteEffects.None, 0f);
				if (this.flag == 2)
				{
					Vector2 vector7 = new Vector2(this.right - (float)this.arrows.Width - (float)this.hPad, this.viewportSize.Y / 2f - 25f);
					this.spriteBatch.Draw(this.sc.menuBlob2, vector7, new Rectangle?(this.arrows), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
					this.arrRect = new Rectangle((int)vector7.X, (int)vector7.Y, this.arrows.Width, this.arrows.Height);
				}
			}
			if (this.flag == 3)
			{
				float num16 = 4f;
				Vector2 vector8 = new Vector2(this.textPosition.X - (float)(this.meter.Width / 2), this.bottom - (float)this.vPad - (float)this.meter.Height);
				this.spriteBatch.Draw(this.sc.menuBlob2, vector8, new Rectangle?(new Rectangle(this.meterColor.X, this.meterColor.Y, (int)(num16 * 3.6f), this.meterColor.Height)), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				this.spriteBatch.Draw(this.sc.menuBlob2, vector8, new Rectangle?(this.meter), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
			this.spriteBatch.End();
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x001D80B0 File Offset: 0x001D62B0
		public void queryButton(Rectangle rr, string word)
		{
			Vector2 vector = this.sc.adjustVector(this.mm);
			vector = this.mm;
			Vector2 one = Vector2.One;
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

		// Token: 0x040020A4 RID: 8356
		private List<string> me = new List<string>();

		// Token: 0x040020A5 RID: 8357
		private int alternator;

		// Token: 0x040020A6 RID: 8358
		private bool delayinput = true;

		// Token: 0x040020A7 RID: 8359
		private string whichButton = "none";

		// Token: 0x040020A8 RID: 8360
		private Rectangle bgBlue = new Rectangle(644, 0, 510, 270);

		// Token: 0x040020A9 RID: 8361
		private Rectangle yesRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x040020AA RID: 8362
		private Rectangle noRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x040020AB RID: 8363
		private Rectangle okRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x040020AC RID: 8364
		private Rectangle arrRect = new Rectangle(0, 0, 0, 0);

		// Token: 0x040020AD RID: 8365
		private Rectangle meter = new Rectangle(164, 289, 362, 48);

		// Token: 0x040020AE RID: 8366
		private Rectangle meterColor = new Rectangle(164, 346, 362, 48);

		// Token: 0x040020AF RID: 8367
		private Rectangle whitebox = new Rectangle(867, 74, 35, 35);

		// Token: 0x040020B0 RID: 8368
		private Rectangle arrows = new Rectangle(541, 289, 39, 84);

		// Token: 0x040020B1 RID: 8369
		private Vector2 mm = Vector2.Zero;

		// Token: 0x040020B2 RID: 8370
		private Matrix hudMatrix;

		// Token: 0x040020B3 RID: 8371
		private Keys[] pressedKeys = new Keys[0];

		// Token: 0x040020B4 RID: 8372
		private int kt;

		// Token: 0x040020B5 RID: 8373
		private int totaldots;

		// Token: 0x040020B6 RID: 8374
		private bool lastwasdot = true;

		// Token: 0x040020B7 RID: 8375
		private Rectangle goldRod;

		// Token: 0x040020B8 RID: 8376
		private Rectangle goldButton;

		// Token: 0x040020B9 RID: 8377
		private string message;

		// Token: 0x040020BA RID: 8378
		private string messone;

		// Token: 0x040020BB RID: 8379
		private string ipaddress;

		// Token: 0x040020BC RID: 8380
		private string myname;

		// Token: 0x040020BD RID: 8381
		private string chatentry;

		// Token: 0x040020BE RID: 8382
		private bool includeYesNo;

		// Token: 0x040020BF RID: 8383
		private bool techYesNo;

		// Token: 0x040020C0 RID: 8384
		private bool includeOkay;

		// Token: 0x040020C1 RID: 8385
		private int flag;

		// Token: 0x040020C2 RID: 8386
		private SpriteFont squarefont;

		// Token: 0x040020C3 RID: 8387
		private SpriteBatch spriteBatch;

		// Token: 0x040020C4 RID: 8388
		private SpriteFont font;

		// Token: 0x040020C5 RID: 8389
		private SpriteFont font2;

		// Token: 0x040020C6 RID: 8390
		private ScreenManager sc;

		// Token: 0x040020C7 RID: 8391
		private Viewport viewport;

		// Token: 0x040020C8 RID: 8392
		private Vector2 viewportSize;

		// Token: 0x040020C9 RID: 8393
		private Vector2 textSize;

		// Token: 0x040020CA RID: 8394
		private Vector2 textPosition;

		// Token: 0x040020CB RID: 8395
		private int hPad;

		// Token: 0x040020CC RID: 8396
		private int vPad;

		// Token: 0x040020CD RID: 8397
		private float widthset = 300f;

		// Token: 0x040020CE RID: 8398
		private float mywidth;

		// Token: 0x040020CF RID: 8399
		private float myhite;

		// Token: 0x040020D0 RID: 8400
		private float left;

		// Token: 0x040020D1 RID: 8401
		private float top;

		// Token: 0x040020D2 RID: 8402
		private float right;

		// Token: 0x040020D3 RID: 8403
		private float bottom;

		// Token: 0x040020D4 RID: 8404
		private float middle;

		// Token: 0x040020D5 RID: 8405
		private float midhite;

		// Token: 0x040020D6 RID: 8406
		private Rectangle menuSlate;

		// Token: 0x040020D7 RID: 8407
		private Rectangle cornerR;

		// Token: 0x040020D8 RID: 8408
		private Rectangle borderR;

		// Token: 0x040020D9 RID: 8409
		private Rectangle backgroundRectangle;

		// Token: 0x040020DA RID: 8410
		private Rectangle topRect;

		// Token: 0x040020DB RID: 8411
		private Rectangle botRect;

		// Token: 0x040020DC RID: 8412
		private Rectangle leftRect;

		// Token: 0x040020DD RID: 8413
		private Rectangle rightRect;

		// Token: 0x040020DE RID: 8414
		private Rectangle cornA;

		// Token: 0x040020DF RID: 8415
		private Rectangle cornB;

		// Token: 0x040020E0 RID: 8416
		private Rectangle cornC;

		// Token: 0x040020E1 RID: 8417
		private Rectangle cornD;

		// Token: 0x040020E2 RID: 8418
		private int max;

		// Token: 0x040020E3 RID: 8419
		private float currentNum;

		// Token: 0x040020E4 RID: 8420
		private int myframe;

		// Token: 0x040020E5 RID: 8421
		private bool useArial;

		// Token: 0x040020E6 RID: 8422
		private PlayerIndex playerIndex;

		// Token: 0x040020E7 RID: 8423
		private int myindex = -1;

		// Token: 0x040020E8 RID: 8424
		private GamePadState gamestate;

		// Token: 0x040020E9 RID: 8425
		private GamePadState prevstate;

		// Token: 0x040020EA RID: 8426
		private KeyboardState prevKeys;

		// Token: 0x040020EB RID: 8427
		private KeyboardState keyState;

		// Token: 0x040020EC RID: 8428
		private MouseState prevMouse;

		// Token: 0x040020ED RID: 8429
		private MouseState mouseState;

		// Token: 0x040020EE RID: 8430
		public string longmessage = "";

		// Token: 0x040020F5 RID: 8437
		private bool includeMeter;

		// Token: 0x040020F6 RID: 8438
		private Random rr;

		// Token: 0x040020F7 RID: 8439
		private string[] dummy = new string[]
		{
			"Dumb Ass", "Dumb-Ass", "El Dumbo", "Fart Smeller", "Lazy Goat", "The Noob", "Greenhorn", "Bed Wetter", "John Smith", "Pee Wee",
			"Thundar"
		};

		// Token: 0x040020F8 RID: 8440
		public string password = "";

		// Token: 0x040020F9 RID: 8441
		private ulong id = 5UL;
	}
}
