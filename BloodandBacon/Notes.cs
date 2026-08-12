using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x02000180 RID: 384
	internal class Notes : MenuScreen
	{
		// Token: 0x06000E41 RID: 3649 RVA: 0x00402C98 File Offset: 0x00400E98
		public Notes()
			: base("")
		{
			base.TransitionOnTime = TimeSpan.FromSeconds(2.5);
			base.TransitionOffTime = TimeSpan.FromSeconds(2.200000047683716);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00402DA0 File Offset: 0x00400FA0
		public override void LoadContent()
		{
			if (this.content == null)
			{
				this.content = new ContentManager(base.ScreenManager.Game.Services, "Content");
			}
			this.random = new Random(44);
			this.graphicsDevice = base.ScreenManager.GraphicsDevice;
			this.button = this.content.Load<SoundEffect>("Audio\\gong");
			this.gear2 = this.content.Load<Texture2D>("sprites\\icons\\gear1");
			this.random = new Random(3);
			this.num = this.random.Next(1, 14);
			this.horPos = this.random.Next(0, 900);
			this.sizer = (float)this.random.Next(2800, 4800) / 1000f;
			this.buttonOn = this.content.Load<Texture2D>("sprites\\icons\\buttonon" + this.num);
			this.loadfont1 = this.content.Load<SpriteFont>("fonts\\tag");
			this.loadfont2 = this.content.Load<SpriteFont>("fonts\\load2");
			this.L257load = this.content.Load<Texture2D>("menus\\Loading");
			this.allcolors(1);
			this.rays = this.content.Load<Texture2D>("loading\\rays");
			int num = 0;
			int num2 = 1;
			for (int i = 0; i < this.colorArray1.Length; i++)
			{
				num++;
				if (num > 32)
				{
					num = 1;
					num2++;
				}
				int num3 = (int)Convert.ToInt16(this.colorArray1[i].R);
				int num4 = (int)Convert.ToInt16(this.colorArray1[i].G);
				int num5 = (int)Convert.ToInt16(this.colorArray1[i].B);
				if (num5 + num4 + num3 > 20)
				{
					this.colortab.Add(this.colorArray1[i]);
					this.posxtab.Add(num);
					this.posytab.Add(num2);
				}
			}
			this.spriteBatch = new SpriteBatch(base.ScreenManager.GraphicsDevice);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00402FC8 File Offset: 0x004011C8
		private void allcolors(int val)
		{
			this.loadIndex += val;
			this.myimage = this.content.Load<Texture2D>("loading\\image" + this.loadIndex);
			this.dossy = this.content.Load<Texture2D>("loading\\dossy");
			this.colorArray1 = new Color[1024];
			this.myimage.GetData<Color>(this.colorArray1);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00403040 File Offset: 0x00401240
		public override void UnloadContent()
		{
			this.content.Unload();
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00403050 File Offset: 0x00401250
		public override void HandleInput(InputState input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			int value = (int)base.ControllingPlayer.Value;
			GamePadState gamePadState = input.CurrentGamePadStates[value];
			if (gamePadState.Buttons.Back == ButtonState.Pressed)
			{
				this.fadeflag = 3;
			}
			if (gamePadState.Buttons.X == ButtonState.Pressed)
			{
				base.ScreenManager.mess = "";
			}
			this.myRot += gamePadState.ThumbSticks.Left.X / 80f;
			this.zoomFactor += gamePadState.ThumbSticks.Left.Y / 10f;
			this.offsetX += gamePadState.ThumbSticks.Right.X * 10f;
			this.offsetY -= gamePadState.ThumbSticks.Right.Y * 10f;
			this.previousgamepadstate = gamePadState;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0040316F File Offset: 0x0040136F
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, false);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0040317C File Offset: 0x0040137C
		public override void Draw(GameTime gameTime)
		{
			if (base.TransitionAlpha >= 255 || this.fadeflag > 1)
			{
				this.drawSaver();
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

		// Token: 0x06000E48 RID: 3656 RVA: 0x00403230 File Offset: 0x00401430
		private void drawSaver()
		{
			this.rot += 0.8f;
			this.realrot += 0.05f;
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			this.spriteBatch.Draw(this.dossy, new Vector2((float)this.horPos, this.realrot * 20f - 150f), null, new Color(120, 120, 200, 80), 0f, new Vector2(0f, 0f), this.sizer, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.L257load, new Vector2(0f, 0f), null, Color.White, 0f, new Vector2(0f, 0f), 2f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.gear2, new Vector2(160f, 60f), null, Color.White, this.realrot, new Vector2((float)(this.gear2.Width / 2), (float)(this.gear2.Height / 2)), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.gear2, new Vector2(192f, 92f), null, Color.White, -this.realrot + 0.01f, new Vector2((float)(this.gear2.Width / 2), (float)(this.gear2.Height / 2)), 0.66f, SpriteEffects.None, 0f);
			this.spriteBatch.DrawString(this.loadfont1, "Current Notes.....   ", new Vector2(230f, 35f), Color.White);
			this.spriteBatch.DrawString(this.loadfont2, base.ScreenManager.mess, new Vector2(520f + this.offsetX, 330f + this.offsetY), Color.White, this.myRot, new Vector2(300f, 150f), this.zoomFactor, SpriteEffects.None, 0f);
			this.spriteBatch.End();
		}

		// Token: 0x04003A73 RID: 14963
		private GraphicsDevice graphicsDevice;

		// Token: 0x04003A74 RID: 14964
		private ContentManager content;

		// Token: 0x04003A75 RID: 14965
		private Random random;

		// Token: 0x04003A76 RID: 14966
		private Texture2D gear2;

		// Token: 0x04003A77 RID: 14967
		private Texture2D buttonOn;

		// Token: 0x04003A78 RID: 14968
		private Texture2D L257load;

		// Token: 0x04003A79 RID: 14969
		private Texture2D rays;

		// Token: 0x04003A7A RID: 14970
		private float rot;

		// Token: 0x04003A7B RID: 14971
		private float realrot;

		// Token: 0x04003A7C RID: 14972
		private SpriteBatch spriteBatch;

		// Token: 0x04003A7D RID: 14973
		private SpriteFont loadfont1;

		// Token: 0x04003A7E RID: 14974
		private SpriteFont loadfont2;

		// Token: 0x04003A7F RID: 14975
		private List<Color> col = new List<Color>();

		// Token: 0x04003A80 RID: 14976
		private List<float> locX = new List<float>();

		// Token: 0x04003A81 RID: 14977
		private List<float> locY = new List<float>();

		// Token: 0x04003A82 RID: 14978
		private int horPos;

		// Token: 0x04003A83 RID: 14979
		private float sizer = 1f;

		// Token: 0x04003A84 RID: 14980
		private string[] image0 = new string[0];

		// Token: 0x04003A85 RID: 14981
		private Texture2D myimage;

		// Token: 0x04003A86 RID: 14982
		private Texture2D dossy;

		// Token: 0x04003A87 RID: 14983
		private Color[] colorArray1 = new Color[0];

		// Token: 0x04003A88 RID: 14984
		private GamePadState previousgamepadstate;

		// Token: 0x04003A89 RID: 14985
		private int fader = 255;

		// Token: 0x04003A8A RID: 14986
		private int fadeflag = 1;

		// Token: 0x04003A8B RID: 14987
		private string[] loadx = new string[] { "LOADING", "LOADING", "LOADING", "LOADING", "LOADING", "LOADING", " LOADING" };

		// Token: 0x04003A8C RID: 14988
		private List<Color> colortab = new List<Color>();

		// Token: 0x04003A8D RID: 14989
		private List<int> posxtab = new List<int>();

		// Token: 0x04003A8E RID: 14990
		private List<int> posytab = new List<int>();

		// Token: 0x04003A8F RID: 14991
		private int loadIndex;

		// Token: 0x04003A90 RID: 14992
		private SoundEffect button;

		// Token: 0x04003A91 RID: 14993
		private int num;

		// Token: 0x04003A92 RID: 14994
		private float offsetX;

		// Token: 0x04003A93 RID: 14995
		private float offsetY;

		// Token: 0x04003A94 RID: 14996
		private float zoomFactor = 1.3f;

		// Token: 0x04003A95 RID: 14997
		private float myRot;
	}
}
