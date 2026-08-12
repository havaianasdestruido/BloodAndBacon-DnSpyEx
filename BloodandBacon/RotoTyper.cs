using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x02000158 RID: 344
	internal class RotoTyper : MenuScreen
	{
		// Token: 0x06000C98 RID: 3224 RVA: 0x00373A54 File Offset: 0x00371C54
		public RotoTyper()
			: base("")
		{
			base.TransitionOnTime = TimeSpan.FromSeconds(2.5);
			base.TransitionOffTime = TimeSpan.FromSeconds(2.200000047683716);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00373B1C File Offset: 0x00371D1C
		public override void LoadContent()
		{
			if (this.content == null)
			{
				this.content = new ContentManager(base.ScreenManager.Game.Services, "Content");
			}
			this.random = new Random();
			this.blankTexture = this.content.Load<Texture2D>("menus\\blank");
			this.back = this.content.Load<Texture2D>("menus\\back");
			this.thumb = this.content.Load<Texture2D>("menus\\thumb");
			this.rotobox = this.content.Load<Texture2D>("menus\\rotobox");
			this.solidbox = this.content.Load<Texture2D>("menus\\solidbox");
			this.pointer = this.content.Load<Texture2D>("menus\\pointer");
			this.databox = this.content.Load<Texture2D>("menus\\databox");
			this.rotoMain = this.content.Load<Texture2D>("menus\\rotoMain");
			this.scanline = this.content.Load<Texture2D>("menus\\scanline");
			this.r1 = this.content.Load<Texture2D>("menus\\r1");
			this.r2 = this.content.Load<Texture2D>("menus\\r2");
			this.r3 = this.content.Load<Texture2D>("menus\\r3");
			this.r4 = this.content.Load<Texture2D>("menus\\r4");
			this.r5 = this.content.Load<Texture2D>("menus\\r5");
			this.r6 = this.content.Load<Texture2D>("menus\\r6");
			this.pop1 = this.content.Load<SoundEffect>("Audio\\pop");
			this.click = this.content.Load<SoundEffect>("Audio\\clickmenu");
			this.beep = this.content.Load<SoundEffect>("Audio\\0527");
			this.tag = this.content.Load<SpriteFont>("menus\\consola");
			this.bigtag = this.content.Load<SpriteFont>("fonts\\bigtag");
			this.spriteBatch = new SpriteBatch(base.ScreenManager.GraphicsDevice);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00373D2A File Offset: 0x00371F2A
		public override void UnloadContent()
		{
			this.content.Unload();
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00373D38 File Offset: 0x00371F38
		public override void HandleInput(InputState input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			int value = (int)base.ControllingPlayer.Value;
			GamePadState gamePadState = input.CurrentGamePadStates[value];
			if (gamePadState.Buttons.Back == ButtonState.Pressed || gamePadState.Buttons.B == ButtonState.Pressed)
			{
				this.fadeflag = 3;
			}
			float x = gamePadState.ThumbSticks.Left.X;
			float y = gamePadState.ThumbSticks.Left.Y;
			float num = MathHelper.ToDegrees((float)Math.Atan((double)(x / y)));
			if (y < 0f)
			{
				num += 180f;
			}
			if (num < 0f)
			{
				num = 360f + num;
			}
			this.degrees = MathHelper.ToRadians(num);
			this.pointerscale = Math.Abs(x) + Math.Abs(y);
			if (this.pointerscale > 0.7f)
			{
				this.myletter = (int)Math.Round((double)(num / 13.86f + 1f));
				if (this.myletter > 26)
				{
					this.myletter = 1;
				}
			}
			if (this.pointerscale > 1f)
			{
				this.pointerscale = 1f;
			}
			if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed && this.previousgamepadstate.Buttons.RightShoulder == ButtonState.Released)
			{
				this.highlightflag = 4;
				this.cursor++;
				if (this.cursor > this.enteredtext.Length)
				{
					this.cursor = this.enteredtext.Length;
				}
				if (this.cursor > 15)
				{
					this.cursor = 15;
					this.beep.Play(0.2f * base.ScreenManager.ev, 0f, 0f);
					this.highlightflag = 7;
				}
				if (this.cursor < 0)
				{
					this.cursor = 0;
				}
			}
			if (gamePadState.Buttons.LeftShoulder == ButtonState.Pressed && this.previousgamepadstate.Buttons.LeftShoulder == ButtonState.Released)
			{
				this.cursor--;
				if (this.cursor < 0)
				{
					this.cursor = 0;
				}
				this.highlightflag = 3;
			}
			if (gamePadState.Buttons.RightStick == ButtonState.Pressed && this.previousgamepadstate.Buttons.RightStick == ButtonState.Released)
			{
				this.highlightflag = 5;
				if (this.style == 3)
				{
					this.style = this.oldstyle;
				}
				else
				{
					this.style = 3;
				}
			}
			if (gamePadState.Buttons.LeftStick == ButtonState.Pressed && this.previousgamepadstate.Buttons.LeftStick == ButtonState.Released)
			{
				this.highlightflag = 5;
				if (this.style == 1)
				{
					this.style = 2;
				}
				else
				{
					this.style = 1;
				}
				this.oldstyle = this.style;
			}
			if (gamePadState.Triggers.Right > 0.8f && this.previousgamepadstate.Triggers.Right <= 0.8f)
			{
				this.highlightflag = 5;
				if (this.style == 3)
				{
					this.style = this.oldstyle;
				}
				else
				{
					this.style = 3;
				}
			}
			if (gamePadState.Triggers.Left > 0.8f && this.previousgamepadstate.Triggers.Left <= 0.8f)
			{
				this.highlightflag = 5;
				if (this.style == 1)
				{
					this.style = 2;
				}
				else
				{
					this.style = 1;
				}
				this.oldstyle = this.style;
			}
			if (gamePadState.Buttons.A == ButtonState.Pressed && this.previousgamepadstate.Buttons.A == ButtonState.Released)
			{
				this.click.Play(base.ScreenManager.ev, 0f, 0f);
				if (this.style == 1)
				{
					this.letters = "abcdefghijklmnopqrstuvwxyz*";
				}
				if (this.style == 2)
				{
					this.letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ*";
				}
				if (this.style == 3)
				{
					this.letters = "0123456789!?@#$%^&*().[]:-*";
				}
				if (this.myletter > 0)
				{
					if (this.enteredtext.Length < 16)
					{
						string text = this.letters.Substring(this.myletter - 1, 1);
						if (this.cursor > 0 || this.enteredtext.Length > 0)
						{
							this.enteredtext = this.enteredtext.Insert(this.cursor, text);
						}
						else
						{
							this.enteredtext = text;
						}
						this.cursor++;
						if (this.cursor > this.enteredtext.Length)
						{
							this.cursor = this.enteredtext.Length;
						}
						if (this.cursor > 15)
						{
							this.cursor = 15;
						}
					}
					else
					{
						string text2 = this.letters.Substring(this.myletter - 1, 1);
						this.enteredtext = this.enteredtext.Remove(this.cursor, 1);
						this.enteredtext = this.enteredtext.Insert(this.cursor, text2);
						this.beep.Play(0.2f * base.ScreenManager.ev, 0f, 0f);
						this.highlightflag = 7;
					}
				}
			}
			if (gamePadState.Buttons.X == ButtonState.Pressed && this.previousgamepadstate.Buttons.X == ButtonState.Released)
			{
				this.highlightflag = 2;
				if (this.enteredtext.Length > 0)
				{
					if (this.cursor > 0 && (this.enteredtext.Length != 16 || this.cursor != 15))
					{
						this.enteredtext = this.enteredtext.Remove(this.cursor - 1, 1);
						this.cursor--;
						if (this.cursor < 0)
						{
							this.cursor = 0;
						}
					}
					else
					{
						this.enteredtext = this.enteredtext.Remove(this.cursor, 1);
					}
				}
				else
				{
					this.enteredtext = "";
				}
				this.click.Play(base.ScreenManager.ev, -1f, 0f);
			}
			if (gamePadState.Buttons.Y == ButtonState.Pressed && this.previousgamepadstate.Buttons.Y == ButtonState.Released)
			{
				this.highlightflag = 1;
				if (this.enteredtext.Length < 16)
				{
					this.enteredtext = this.enteredtext.Insert(this.cursor, " ");
					this.cursor++;
				}
				else
				{
					this.beep.Play(0.2f * base.ScreenManager.ev, 0f, 0f);
					this.highlightflag = 7;
				}
				if (this.cursor > this.enteredtext.Length)
				{
					this.cursor = this.enteredtext.Length;
				}
				if (this.cursor > 15)
				{
					this.cursor = 15;
				}
			}
			this.previousgamepadstate = gamePadState;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0037444C File Offset: 0x0037264C
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, false);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00374458 File Offset: 0x00372658
		public override void Draw(GameTime gameTime)
		{
			if (base.TransitionAlpha >= 255 || this.fadeflag > 1)
			{
				this.DrawText(this.enteredtext);
				if (this.fadeflag == 1)
				{
					this.fader -= 3;
					if (this.fader <= 0)
					{
						this.fader = 0;
						this.fadeflag = 2;
					}
				}
				if (this.fadeflag == 3)
				{
					this.fader += 11;
				}
				if (this.fader > 255)
				{
					this.fader = 255;
					this.fadeflag = 0;
					base.ScreenManager.menuflag = 0;
					base.ExitScreen();
				}
				base.ScreenManager.FadeBackBufferToBlack(this.fader);
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00374514 File Offset: 0x00372714
		private void DrawText(string text)
		{
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			this.spriteBatch.Draw(this.scanline, new Vector2(this.scanx, 572f), null, new Color(255, 255, 255, 150), MathHelper.ToRadians(90f), new Vector2((float)(this.scanline.Width / 2), (float)(this.scanline.Height / 2)), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.scanline, new Vector2(0f, this.scany), new Color(255, 255, 255, 150));
			this.spriteBatch.Draw(this.scanline, new Vector2(0f, this.scany + 6f), new Color(255, 255, 255, 150));
			this.spriteBatch.Draw(this.rotoMain, new Vector2(this.offx, this.offy), Color.White);
			string text2 = "";
			if (this.style == 1)
			{
				text2 = "abcdefghijklmnopqrstuvwxyz";
			}
			if (this.style == 2)
			{
				text2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			}
			if (this.style == 3)
			{
				text2 = "0123456789!?@#$%^&*().[]:-";
			}
			for (int i = 0; i < text2.Length; i++)
			{
				float num = this.tag.MeasureString(text2.Substring(i, 1)).X / 2f;
				float num2 = this.tag.MeasureString(text2.Substring(i, 1)).Y / 2f;
				float num3 = MathHelper.ToRadians((float)i * 13.86f);
				float num4 = (float)Math.Sin((double)num3) * 180f + this.rx - num;
				float num5 = (float)(-(float)Math.Cos((double)num3)) * 180f + this.ry - num2;
				Color color = new Color(56, 76, 120, 255);
				if (this.highlightflag == 7)
				{
					color = new Color(199, 199, 76, 255);
				}
				if (i == this.myletter - 1)
				{
					if (this.lastletter != this.myletter - 1)
					{
						float num6 = 1f - (float)this.random.Next(1, 30) / 100f;
						this.pop1.Play(0.3f * base.ScreenManager.ev, num6, 0f);
					}
					this.lastletter = this.myletter - 1;
					this.spriteBatch.DrawString(this.tag, text2.Substring(i, 1), new Vector2(num4, num5), Color.White);
				}
				else
				{
					this.spriteBatch.Draw(this.solidbox, new Vector2((float)Math.Sin((double)num3) * 180f + this.rx, (float)(-(float)Math.Cos((double)num3)) * 180f + this.ry), null, color, num3, new Vector2((float)(this.rotobox.Width / 2), (float)(this.rotobox.Height / 2)), 1f, SpriteEffects.None, 0f);
					this.spriteBatch.DrawString(this.tag, text2.Substring(i, 1), new Vector2(num4, num5), Color.Black);
				}
			}
			this.spriteBatch.Draw(this.rotobox, new Vector2((float)Math.Sin((double)MathHelper.ToRadians((float)this.lastletter * 13.86f)) * 180f + this.rx, (float)(-(float)Math.Cos((double)MathHelper.ToRadians((float)this.lastletter * 13.86f))) * 180f + this.ry), null, Color.White, MathHelper.ToRadians((float)this.lastletter * 13.86f), new Vector2((float)(this.rotobox.Width / 2), (float)(this.rotobox.Height / 2)), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.pointer, new Vector2(this.rx, this.ry), null, Color.White, this.degrees, new Vector2((float)(this.pointer.Width / 2), (float)this.pointer.Height), this.pointerscale, SpriteEffects.None, 0f);
			this.spriteBatch.DrawString(this.bigtag, text2.Substring(this.lastletter, 1), new Vector2(this.rx - this.bigtag.MeasureString(text2.Substring(this.lastletter, 1)).X / 2f, this.ry - this.bigtag.MeasureString(text2.Substring(this.lastletter, 1)).Y / 2f), Color.White);
			float num7 = (float)(base.ScreenManager.GraphicsDevice.Viewport.Width / 2) - this.tag.MeasureString("OOOOOOOOOOOOOOOO").X / 2f;
			for (float num8 = 0f; num8 < 16f; num8 += 1f)
			{
				float num9 = num8 * (float)(this.databox.Width + 1) + num7 + 2f;
				this.spriteBatch.Draw(this.databox, new Vector2(num9, 57f + this.offy), new Color(56, 76, 120, 255));
			}
			this.spriteBatch.DrawString(this.tag, text, new Vector2(num7, 58f + this.offy), Color.White);
			this.blink--;
			if (this.blink <= 0)
			{
				this.blink = 6;
				if (this.onoff == 0)
				{
					this.onoff = 1;
				}
				else
				{
					this.onoff = 0;
				}
			}
			float num10 = 0f;
			if (text.Length > 0)
			{
				num10 = this.tag.MeasureString(text.Substring(0, this.cursor)).X;
			}
			if (this.cursor > 15)
			{
				this.cursor = 15;
			}
			if (this.onoff == 1)
			{
				this.spriteBatch.DrawString(this.tag, "_", new Vector2(num7 + num10, 61f + this.offy), Color.White);
			}
			if (this.highlightflag > 0)
			{
				this.highlight--;
				if (this.highlight <= 0)
				{
					this.highlightflag = 0;
					this.highlight = 5;
				}
				if (this.highlightflag == 1)
				{
					this.spriteBatch.Draw(this.r1, new Vector2(144f + this.offx, 187f + this.offy), Color.White);
				}
				if (this.highlightflag == 2)
				{
					this.spriteBatch.Draw(this.r2, new Vector2(800f + this.offx, 187f + this.offy), Color.White);
				}
				if (this.highlightflag == 3)
				{
					this.spriteBatch.Draw(this.r3, new Vector2(144f + this.offx, 307f + this.offy), Color.White);
				}
				if (this.highlightflag == 4)
				{
					this.spriteBatch.Draw(this.r4, new Vector2(800f + this.offx, 307f + this.offy), Color.White);
				}
				if (this.highlightflag == 5)
				{
					this.spriteBatch.Draw(this.r5, new Vector2(144f + this.offx, 432f + this.offy), Color.White);
				}
				if (this.highlightflag == 6)
				{
					this.spriteBatch.Draw(this.r6, new Vector2(800f + this.offx, 432f + this.offy), Color.White);
				}
			}
			this.spriteBatch.DrawString(this.tag, "Space", new Vector2(174f + this.offx, 192f + this.offy), Color.White);
			this.spriteBatch.DrawString(this.tag, "Delete", new Vector2(840f + this.offx, 192f + this.offy), Color.White);
			this.spriteBatch.DrawString(this.tag, "Cursor", new Vector2(154f + this.offx, 312f + this.offy), Color.White);
			this.spriteBatch.DrawString(this.tag, "Cursor", new Vector2(850f + this.offx, 312f + this.offy), Color.White);
			this.spriteBatch.DrawString(this.tag, "Caps", new Vector2(184f + this.offx, 437f + this.offy), Color.White);
			this.spriteBatch.DrawString(this.tag, "Done", new Vector2(855f + this.offx, 437f + this.offy), Color.White);
			this.spriteBatch.Draw(base.ScreenManager.interfaceBlob, new Vector2(0f, 0f), Color.White);
			this.spriteBatch.End();
		}

		// Token: 0x040033E5 RID: 13285
		private int myletter;

		// Token: 0x040033E6 RID: 13286
		private string letters = "";

		// Token: 0x040033E7 RID: 13287
		private string enteredtext = "";

		// Token: 0x040033E8 RID: 13288
		private int onoff;

		// Token: 0x040033E9 RID: 13289
		private int blink = 45;

		// Token: 0x040033EA RID: 13290
		private int cursor;

		// Token: 0x040033EB RID: 13291
		private float rx = 640f;

		// Token: 0x040033EC RID: 13292
		private float ry = 410f;

		// Token: 0x040033ED RID: 13293
		private float offx = 68f;

		// Token: 0x040033EE RID: 13294
		private float offy = 50f;

		// Token: 0x040033EF RID: 13295
		private Texture2D blankTexture;

		// Token: 0x040033F0 RID: 13296
		private Texture2D back;

		// Token: 0x040033F1 RID: 13297
		private Texture2D thumb;

		// Token: 0x040033F2 RID: 13298
		private Texture2D rotobox;

		// Token: 0x040033F3 RID: 13299
		private Texture2D solidbox;

		// Token: 0x040033F4 RID: 13300
		private Texture2D pointer;

		// Token: 0x040033F5 RID: 13301
		private Texture2D databox;

		// Token: 0x040033F6 RID: 13302
		private Texture2D rotoMain;

		// Token: 0x040033F7 RID: 13303
		private Texture2D r1;

		// Token: 0x040033F8 RID: 13304
		private Texture2D r2;

		// Token: 0x040033F9 RID: 13305
		private Texture2D r3;

		// Token: 0x040033FA RID: 13306
		private Texture2D r4;

		// Token: 0x040033FB RID: 13307
		private Texture2D r5;

		// Token: 0x040033FC RID: 13308
		private Texture2D r6;

		// Token: 0x040033FD RID: 13309
		private Texture2D scanline;

		// Token: 0x040033FE RID: 13310
		private float scanx = 240f;

		// Token: 0x040033FF RID: 13311
		private float scany = 120f;

		// Token: 0x04003400 RID: 13312
		private int highlightflag;

		// Token: 0x04003401 RID: 13313
		private int highlight = 10;

		// Token: 0x04003402 RID: 13314
		private int style = 1;

		// Token: 0x04003403 RID: 13315
		private int oldstyle = 1;

		// Token: 0x04003404 RID: 13316
		private int lastletter;

		// Token: 0x04003405 RID: 13317
		private float degrees;

		// Token: 0x04003406 RID: 13318
		private float pointerscale;

		// Token: 0x04003407 RID: 13319
		private SoundEffect pop1;

		// Token: 0x04003408 RID: 13320
		private SoundEffect beep;

		// Token: 0x04003409 RID: 13321
		private SoundEffect click;

		// Token: 0x0400340A RID: 13322
		private int fader = 255;

		// Token: 0x0400340B RID: 13323
		private int fadeflag = 1;

		// Token: 0x0400340C RID: 13324
		private ContentManager content;

		// Token: 0x0400340D RID: 13325
		private Random random;

		// Token: 0x0400340E RID: 13326
		private GamePadState previousgamepadstate;

		// Token: 0x0400340F RID: 13327
		private SpriteBatch spriteBatch;

		// Token: 0x04003410 RID: 13328
		private SpriteFont tag;

		// Token: 0x04003411 RID: 13329
		private SpriteFont bigtag;
	}
}
