using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;

namespace Blood
{
	// Token: 0x02000052 RID: 82
	internal class walletScreen : GameScreen
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600032A RID: 810 RVA: 0x000CF9A8 File Offset: 0x000CDBA8
		// (remove) Token: 0x0600032B RID: 811 RVA: 0x000CF9E0 File Offset: 0x000CDBE0
		public event EventHandler<PlayerIndexEventArgs> Restart;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600032C RID: 812 RVA: 0x000CFA18 File Offset: 0x000CDC18
		// (remove) Token: 0x0600032D RID: 813 RVA: 0x000CFA50 File Offset: 0x000CDC50
		public event EventHandler<PlayerIndexEventArgs> KickPlayer;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600032E RID: 814 RVA: 0x000CFA88 File Offset: 0x000CDC88
		// (remove) Token: 0x0600032F RID: 815 RVA: 0x000CFAC0 File Offset: 0x000CDCC0
		public event EventHandler<PlayerIndexEventArgs> ExitGame;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000330 RID: 816 RVA: 0x000CFAF8 File Offset: 0x000CDCF8
		// (remove) Token: 0x06000331 RID: 817 RVA: 0x000CFB30 File Offset: 0x000CDD30
		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		// Token: 0x06000332 RID: 818 RVA: 0x000CFB68 File Offset: 0x000CDD68
		public walletScreen()
		{
			base.IsPopup = true;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000CFC60 File Offset: 0x000CDE60
		public override void LoadContent()
		{
			ContentManager content = base.ScreenManager.Game.Content;
			this.sc = base.ScreenManager;
			this.spriteBatch = new SpriteBatch(base.ScreenManager.GraphicsDevice);
			this.cashout = this.sc.cashout;
			this.click = this.sc.menuclick;
			this.rr = new Random();
			this.cashout.Play(this.sc.ev * 0.8f, (float)this.rr.Next(-10, 10) / 100f, 0f);
			this.sc.showVideoSetup = false;
			if (this.sc.usingMouse)
			{
				this.sc.Game.IsMouseVisible = true;
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000CFE14 File Offset: 0x000CE014
		public override void HandleInput(InputState input)
		{
			if (this.sc.deactivated)
			{
				return;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (base.ControllingPlayer != null)
			{
				int value = (int)base.ControllingPlayer.Value;
				this.gamestate = input.CurrentGamePadStates[value];
				this.prevstate = input.LastGamePadStates[value];
				GamePad.SetVibration(base.ControllingPlayer.Value, 0f, 0f);
			}
			this.prevkeyState = input.lastKeyState;
			this.keyState = input.currentKeyState;
			this.prevMouse = this.mouseState;
			this.mouseState = Mouse.GetState();
			this.mm.X = (float)this.mouseState.X;
			this.mm.Y = (float)this.mouseState.Y;
			bool flag = this.mouseState.X != (int)this.sc.mymouse.X || this.mouseState.Y != (int)this.sc.mymouse.Y;
			this.sc.mymouse.X = (float)this.mouseState.X;
			this.sc.mymouse.Y = (float)this.mouseState.Y;
			bool flag2 = this.mouseState.RightButton == ButtonState.Pressed && this.prevMouse.RightButton == ButtonState.Released;
			bool flag3 = this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released;
			if (this.delayinput)
			{
				this.prevMouse = this.mouseState;
				this.prevkeyState = this.keyState;
				this.prevstate = this.gamestate;
				flag2 = false;
				flag3 = false;
			}
			if (this.exitme)
			{
				if (this.Cancelled != null)
				{
					this.Cancelled(this, new PlayerIndexEventArgs(PlayerIndex.One));
				}
				this.sc.Game.IsMouseVisible = false;
				base.ExitScreen();
				return;
			}
			if (this.keyState.IsKeyDown(this.sc.escape_key) && this.prevkeyState.IsKeyUp(this.sc.escape_key))
			{
				if (this.Cancelled != null)
				{
					this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
				}
				this.sc.Game.IsMouseVisible = false;
				base.ExitScreen();
				return;
			}
			if (flag2 || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
			{
				if (this.Cancelled != null)
				{
					this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
				}
				this.sc.Game.IsMouseVisible = false;
				base.ExitScreen();
				return;
			}
			if (input.IsMenuUp(base.ControllingPlayer))
			{
				this.myIndex--;
				if (this.myIndex < 0)
				{
					this.myIndex = 5;
				}
				this.click.Play(1f, 0f, 0f);
				return;
			}
			if (input.IsMenuDown(base.ControllingPlayer))
			{
				this.myIndex++;
				if (this.myIndex > 5)
				{
					this.myIndex = 0;
				}
				this.click.Play(1f, 0f, 0f);
				return;
			}
			if (flag && this.sc.usingMouse)
			{
				int num = this.myIndex;
				this.queryButton(this.restartRect, 0);
				this.queryButton(this.settingsRect, 1);
				this.queryButton(this.helpRect, 2);
				this.queryButton(this.multiRect, 3);
				this.queryButton(this.exitRect, 5);
				this.queryButton(this.kickRect, 4);
				if (num != this.myIndex)
				{
					this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
				}
			}
			if (flag3 || input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex) || input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
			{
				if (this.myIndex == 0)
				{
					if (this.sc.host)
					{
						if (!this.sc.hordemode && !this.sc.paintland)
						{
							walletBoxBB walletBoxBB = new walletBoxBB("restart");
							walletBoxBB.Accepted += delegate
							{
								if (this.Restart != null)
								{
									this.Restart(this, new PlayerIndexEventArgs(this.playerIndex));
								}
								this.sc.Game.IsMouseVisible = false;
								base.ExitScreen();
							};
							base.ScreenManager.AddScreen(walletBoxBB, new PlayerIndex?(this.playerIndex));
							this.delayinput = true;
							return;
						}
						MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("Lose All Horde Progress\nAnd Restart From Wave 1?\n", 1);
						if (this.sc.paintland)
						{
							messageBoxScreen = new MessageBoxScreen2("Lose All Your Work\nAnd Restart The Day?\n", 1);
						}
						messageBoxScreen.Accepted += delegate
						{
							if (this.Restart != null)
							{
								this.Restart(this, new PlayerIndexEventArgs(this.playerIndex));
							}
							this.sc.Game.IsMouseVisible = false;
							base.ExitScreen();
						};
						messageBoxScreen.Cancelled += delegate
						{
							this.sc.Game.IsMouseVisible = false;
							base.ExitScreen();
						};
						base.ScreenManager.AddScreen(messageBoxScreen, new PlayerIndex?(this.playerIndex));
						this.delayinput = true;
						return;
					}
					else
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
				}
				if (this.myIndex == 1)
				{
					walletBoxBB walletBoxBB2 = new walletBoxBB("options");
					base.ScreenManager.AddScreen(walletBoxBB2, new PlayerIndex?(this.playerIndex));
					this.delayinput = true;
					return;
				}
				if (this.myIndex == 2)
				{
					walletBoxBB walletBoxBB3 = new walletBoxBB("help");
					base.ScreenManager.AddScreen(walletBoxBB3, new PlayerIndex?(this.playerIndex));
					this.delayinput = true;
					return;
				}
				if (this.myIndex == 3)
				{
					if (this.sc.maxDay() + 1 < 31 && !this.sc.cheatsOn)
					{
						string text = "Cheats will Unlock\n After Day 30";
						if (this.sc.hordemode)
						{
							text = "Cheats Not Allowed";
						}
						MessageBoxScreen2 messageBoxScreen2 = new MessageBoxScreen2(text, 0);
						messageBoxScreen2.Cancelled += delegate
						{
							if (this.sc.usingMouse)
							{
								this.sc.Game.IsMouseVisible = true;
							}
						};
						base.ScreenManager.AddScreen(messageBoxScreen2, new PlayerIndex?(this.playerIndex));
						this.delayinput = true;
						return;
					}
					if (((!this.sc.hordemode && this.sc.hostAllowCheats) || this.sc.developer) && !this.sc.tunnelMode)
					{
						walletBoxBB walletBoxBB4 = new walletBoxBB("cheats");
						base.ScreenManager.AddScreen(walletBoxBB4, new PlayerIndex?(this.playerIndex));
						this.delayinput = true;
						return;
					}
					this.sc.buttonDeny.Play(this.sc.ev, 0f, 0f);
					return;
				}
				else
				{
					if (this.myIndex == 4)
					{
						if (!this.sc.host || this.sc.kickplayerID.Count <= 0)
						{
							this.sc.abort.Play(this.sc.ev, 0f, 0f);
							return;
						}
						if (this.sc.kickplayerID.Count != 1)
						{
							walletBoxBB walletBoxBB5 = new walletBoxBB("kicks");
							walletBoxBB5.Accepted += delegate
							{
								this.sc.Game.IsMouseVisible = false;
								if (this.KickPlayer != null)
								{
									this.KickPlayer(this, new PlayerIndexEventArgs(this.playerIndex));
								}
								base.ExitScreen();
							};
							walletBoxBB5.Cancelled += delegate
							{
							};
							base.ScreenManager.AddScreen(walletBoxBB5, new PlayerIndex?(this.playerIndex));
							this.delayinput = true;
							return;
						}
						this.sc.Game.IsMouseVisible = false;
						if (this.KickPlayer != null)
						{
							this.KickPlayer(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						base.ExitScreen();
					}
					if (this.myIndex == 5)
					{
						if (this.ExitGame != null)
						{
							this.ExitGame(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						this.sc.Game.IsMouseVisible = false;
						base.ExitScreen();
						return;
					}
				}
			}
			this.delayinput = false;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000D0644 File Offset: 0x000CE844
		public override void Draw(GameTime gameTime)
		{
			if (this.sc.deactivated)
			{
				return;
			}
			this.rampIN += 0.1f;
			if (this.rampIN > 1f)
			{
				this.rampIN = 1f;
			}
			float num = MathHelper.Hermite(-520f, 0f, 0f, 0f, this.rampIN);
			float num2 = 0f;
			Matrix matrix;
			if (this.sc.introCamera > 0f)
			{
				matrix = Matrix.CreateScale((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height, 1f);
				if (this.sc.introCamera > 0f && this.sc.introCamera < 5f)
				{
					this.exitme = true;
				}
			}
			else
			{
				matrix = Matrix.CreateScale((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height, 1f);
			}
			if (!this.sc.showVideoSetup)
			{
				base.ScreenManager.FadeBackBufferToBlack((int)(base.TransitionAlpha / 2));
				this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, matrix);
				Rectangle rectangle = new Rectangle((int)num, (int)num2, 519, 720);
				Color color = new Color(250, 250, 250, 255);
				this.spriteBatch.Draw(base.ScreenManager.johnnyWallet, rectangle, new Rectangle?(new Rectangle(0, 0, 519, 720)), Color.White);
				int num3 = 467;
				if (this.myIndex == 0)
				{
					rectangle = new Rectangle((int)num + this.restartRect.X - num3, this.restartRect.Y, this.restartRect.Width, this.restartRect.Height);
					this.spriteBatch.Draw(base.ScreenManager.johnnyWallet, rectangle, new Rectangle?(this.restartRect), color);
				}
				if (this.myIndex == 1)
				{
					rectangle = new Rectangle((int)num + this.settingsRect.X - num3, this.settingsRect.Y, this.settingsRect.Width, this.settingsRect.Height);
					this.spriteBatch.Draw(base.ScreenManager.johnnyWallet, rectangle, new Rectangle?(this.settingsRect), color);
				}
				if (this.myIndex == 2)
				{
					rectangle = new Rectangle((int)num + this.helpRect.X - num3, this.helpRect.Y, this.helpRect.Width, this.helpRect.Height);
					this.spriteBatch.Draw(base.ScreenManager.johnnyWallet, rectangle, new Rectangle?(this.helpRect), color);
				}
				if (this.myIndex == 3)
				{
					rectangle = new Rectangle((int)num + this.multiRect.X - num3, this.multiRect.Y, this.multiRect.Width, this.multiRect.Height);
					this.spriteBatch.Draw(base.ScreenManager.johnnyWallet, rectangle, new Rectangle?(this.multiRect), color);
				}
				if (this.myIndex == 4)
				{
					rectangle = new Rectangle((int)num + this.kickRect.X - num3, this.kickRect.Y, this.kickRect.Width, this.kickRect.Height);
					this.spriteBatch.Draw(base.ScreenManager.johnnyWallet, rectangle, new Rectangle?(this.kickRect), color);
				}
				if (this.myIndex == 5)
				{
					rectangle = new Rectangle((int)num + this.exitRect.X - num3, this.exitRect.Y, this.exitRect.Width, this.exitRect.Height);
					this.spriteBatch.Draw(base.ScreenManager.johnnyWallet, rectangle, new Rectangle?(this.exitRect), color);
				}
				this.spriteBatch.End();
				walletScreen.my_stringbuilder.Length = 0;
				walletScreen.my_stringbuilder.Append("L");
				int num4 = (int)(this.sc.maxDay() + 1);
				if (num4 > 101)
				{
					num4 = 101;
				}
				if (num4 < 10)
				{
					walletScreen.my_stringbuilder.Concat(0);
					walletScreen.my_stringbuilder.Concat(0);
				}
				else if (num4 < 100)
				{
					walletScreen.my_stringbuilder.Concat(0);
				}
				walletScreen.my_stringbuilder.Concat(num4);
				this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, matrix);
				this.spriteBatch.DrawString(this.sc.font, walletScreen.my_stringbuilder, new Vector2(num + 213f, num2 + 194f), new Color(180, 180, 160, 120), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				this.spriteBatch.End();
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000D0B90 File Offset: 0x000CED90
		public void queryButton(Rectangle rr, int ch)
		{
			rr = new Rectangle(rr.X - 467, rr.Y, rr.Width, rr.Height);
			Vector2 vector = this.mm;
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
				this.myIndex = ch;
			}
		}

		// Token: 0x04000D73 RID: 3443
		private KeyboardState keyState;

		// Token: 0x04000D74 RID: 3444
		private KeyboardState prevkeyState;

		// Token: 0x04000D75 RID: 3445
		private MouseState prevMouse;

		// Token: 0x04000D76 RID: 3446
		private MouseState mouseState;

		// Token: 0x04000D77 RID: 3447
		private Vector2 mm = Vector2.Zero;

		// Token: 0x04000D78 RID: 3448
		private bool delayinput;

		// Token: 0x04000D79 RID: 3449
		private SoundEffect cashout;

		// Token: 0x04000D7A RID: 3450
		private SoundEffect click;

		// Token: 0x04000D7B RID: 3451
		private SpriteBatch spriteBatch;

		// Token: 0x04000D7C RID: 3452
		private int myIndex;

		// Token: 0x04000D7D RID: 3453
		private float rampIN;

		// Token: 0x04000D7E RID: 3454
		private Rectangle restartRect = new Rectangle(535, 347, 364, 49);

		// Token: 0x04000D7F RID: 3455
		private Rectangle settingsRect = new Rectangle(535, 408, 364, 49);

		// Token: 0x04000D80 RID: 3456
		private Rectangle helpRect = new Rectangle(535, 465, 364, 49);

		// Token: 0x04000D81 RID: 3457
		private Rectangle multiRect = new Rectangle(535, 522, 364, 49);

		// Token: 0x04000D82 RID: 3458
		private Rectangle kickRect = new Rectangle(535, 576, 364, 49);

		// Token: 0x04000D83 RID: 3459
		private Rectangle exitRect = new Rectangle(535, 635, 364, 49);

		// Token: 0x04000D84 RID: 3460
		private Random rr;

		// Token: 0x04000D85 RID: 3461
		private GamePadState gamestate;

		// Token: 0x04000D86 RID: 3462
		private GamePadState prevstate;

		// Token: 0x04000D87 RID: 3463
		private ScreenManager sc;

		// Token: 0x04000D88 RID: 3464
		private NetworkSession networkSession;

		// Token: 0x04000D89 RID: 3465
		private static StringBuilder my_stringbuilder = new StringBuilder(32, 32);

		// Token: 0x04000D8E RID: 3470
		private PlayerIndex playerIndex;

		// Token: 0x04000D8F RID: 3471
		private bool exitme;
	}
}
