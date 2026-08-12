using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x020000A1 RID: 161
	internal class MiniGame : MenuScreen
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x0013CCE4 File Offset: 0x0013AEE4
		public MiniGame()
			: base("")
		{
			base.TransitionOnTime = TimeSpan.FromSeconds(2.5);
			base.TransitionOffTime = TimeSpan.FromSeconds(2.200000047683716);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0013CFBC File Offset: 0x0013B1BC
		public override void LoadContent()
		{
			if (this.content == null)
			{
				this.content = new ContentManager(base.ScreenManager.Game.Services, "Content");
			}
			this.random = new Random(77);
			this.graphicsDevice = base.ScreenManager.GraphicsDevice;
			this.button = this.content.Load<SoundEffect>("Audio\\gong");
			this.gear2 = this.content.Load<Texture2D>("sprites\\icons\\gear1");
			this.random = new Random(57);
			this.num = this.random.Next(1, 14);
			this.horPos = this.random.Next(0, 900);
			this.sizer = (float)this.random.Next(2800, 4800) / 1000f;
			this.buttonOn = this.content.Load<Texture2D>("sprites\\icons\\buttonon" + this.num);
			this.loadfont1 = this.content.Load<SpriteFont>("fonts\\tag");
			this.loadfont2 = this.content.Load<SpriteFont>("fonts\\load");
			this.L257load = this.content.Load<Texture2D>("menus\\Loading");
			this.allcolors(1);
			this.rays = this.content.Load<Texture2D>("loading\\rays");
			this.messagedisplay = "\"" + this.messages[this.loadIndex - 1] + "\"";
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

		// Token: 0x060005D9 RID: 1497 RVA: 0x0013D208 File Offset: 0x0013B408
		private void allcolors(int val)
		{
			this.loadIndex += val;
			if (this.loadIndex > this.messages.Length)
			{
				this.loadIndex = 1;
			}
			if (this.loadIndex < 1)
			{
				this.loadIndex = this.messages.Length;
			}
			this.myimage = this.content.Load<Texture2D>("loading\\image" + this.loadIndex);
			this.dossy = this.content.Load<Texture2D>("loading\\dossy");
			this.colorArray1 = new Color[1024];
			this.myimage.GetData<Color>(this.colorArray1);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0013D2AE File Offset: 0x0013B4AE
		public override void UnloadContent()
		{
			this.content.Unload();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0013D2BC File Offset: 0x0013B4BC
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
			if (gamePadState.Buttons.A == ButtonState.Pressed && this.previousgamepadstate.Buttons.A == ButtonState.Released)
			{
				this.allcolors(1);
				this.colortab.Clear();
				this.posxtab.Clear();
				this.posytab.Clear();
				this.messagedisplay = "\"" + this.messages[this.loadIndex - 1] + "\"";
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
				this.realrot = 0f;
				this.horPos = this.random.Next(0, 900);
				this.sizer = (float)this.random.Next(1800, 5800) / 1000f;
				this.realrot = 0f;
				this.locX.Clear();
				this.locY.Clear();
				this.col.Clear();
			}
			if (gamePadState.Buttons.B == ButtonState.Pressed && this.previousgamepadstate.Buttons.B == ButtonState.Released)
			{
				this.allcolors(-1);
				this.colortab.Clear();
				this.posxtab.Clear();
				this.posytab.Clear();
				this.messagedisplay = "\"" + this.messages[this.loadIndex - 1] + "\"";
				int num6 = 0;
				int num7 = 1;
				for (int j = 0; j < this.colorArray1.Length; j++)
				{
					num6++;
					if (num6 > 32)
					{
						num6 = 1;
						num7++;
					}
					int num8 = (int)Convert.ToInt16(this.colorArray1[j].R);
					int num9 = (int)Convert.ToInt16(this.colorArray1[j].G);
					int num10 = (int)Convert.ToInt16(this.colorArray1[j].B);
					if (num10 + num9 + num8 > 20)
					{
						this.colortab.Add(this.colorArray1[j]);
						this.posxtab.Add(num6);
						this.posytab.Add(num7);
					}
				}
				this.realrot = 0f;
				this.horPos = this.random.Next(0, 900);
				this.sizer = (float)this.random.Next(1800, 5800) / 1000f;
				this.realrot = 0f;
				this.locX.Clear();
				this.locY.Clear();
				this.col.Clear();
			}
			if (gamePadState.Buttons.X == ButtonState.Pressed && this.previousgamepadstate.Buttons.X == ButtonState.Released)
			{
				this.num--;
				if (this.num < 1)
				{
					this.num = 14;
				}
				this.buttonOn = this.content.Load<Texture2D>("sprites\\icons\\buttonon" + this.num);
				this.horPos = this.random.Next(0, 900);
				this.sizer = (float)this.random.Next(1800, 5800) / 1000f;
				this.realrot = 0f;
			}
			if (gamePadState.Buttons.Y == ButtonState.Pressed && this.previousgamepadstate.Buttons.Y == ButtonState.Released)
			{
				this.num++;
				if (this.num > 13)
				{
					this.num = 1;
				}
				this.buttonOn = this.content.Load<Texture2D>("sprites\\icons\\buttonon" + this.num);
				this.horPos = this.random.Next(0, 900);
				this.sizer = (float)this.random.Next(1800, 5800) / 1000f;
				this.realrot = 0f;
			}
			this.previousgamepadstate = gamePadState;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0013D7E2 File Offset: 0x0013B9E2
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, false);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0013D7F0 File Offset: 0x0013B9F0
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

		// Token: 0x060005DE RID: 1502 RVA: 0x0013D8A4 File Offset: 0x0013BAA4
		private void drawSaver()
		{
			this.rot += 0.8f;
			if ((double)this.rot > 0.15)
			{
				this.rot = 0f;
				this.realrot += 0.05f;
				this.pulse--;
				if (this.pulse <= 0)
				{
					this.pulse = 12;
				}
				if (this.colortab.Count > 0)
				{
					int num = this.random.Next(0, this.colortab.Count);
					int num2 = this.posxtab[num] - 16;
					int num3 = this.posytab[num] - 16;
					this.locX.Add((float)(num2 * 12 + 640) + (float)this.random.Next(-1000, 1000) / 1000f);
					this.locY.Add((float)(num3 * 12 + 330) + (float)this.random.Next(-1000, 1000) / 1000f);
					this.col.Add(this.colortab[num]);
					this.colortab.RemoveAt(num);
					this.posxtab.RemoveAt(num);
					this.posytab.RemoveAt(num);
					if (this.colortab.Count > 0)
					{
						num = this.random.Next(0, this.colortab.Count);
						num2 = this.posxtab[num] - 16;
						num3 = this.posytab[num] - 16;
						this.locX.Add((float)(num2 * 12 + 640) + (float)this.random.Next(-1000, 1000) / 1000f);
						this.locY.Add((float)(num3 * 12 + 330) + (float)this.random.Next(-1000, 1000) / 1000f);
						this.col.Add(this.colortab[num]);
						this.colortab.RemoveAt(num);
						this.posxtab.RemoveAt(num);
						this.posytab.RemoveAt(num);
					}
					if (this.colortab.Count > 0)
					{
						num = this.random.Next(0, this.colortab.Count);
						num2 = this.posxtab[num] - 16;
						num3 = this.posytab[num] - 16;
						this.locX.Add((float)(num2 * 12 + 640) + (float)this.random.Next(-1000, 1000) / 1000f);
						this.locY.Add((float)(num3 * 12 + 330) + (float)this.random.Next(-1000, 1000) / 1000f);
						this.col.Add(this.colortab[num]);
						this.colortab.RemoveAt(num);
						this.posxtab.RemoveAt(num);
						this.posytab.RemoveAt(num);
					}
					if (this.colortab.Count > 0)
					{
						num = this.random.Next(0, this.colortab.Count);
						num2 = this.posxtab[num] - 16;
						num3 = this.posytab[num] - 16;
						this.locX.Add((float)(num2 * 12 + 640) + (float)this.random.Next(-1000, 1000) / 1000f);
						this.locY.Add((float)(num3 * 12 + 330) + (float)this.random.Next(-1000, 1000) / 1000f);
						this.col.Add(this.colortab[num]);
						this.colortab.RemoveAt(num);
						this.posxtab.RemoveAt(num);
						this.posytab.RemoveAt(num);
					}
					if (this.pulse == 3)
					{
						this.button.Play(0.3f * base.ScreenManager.ev, (float)this.random.Next(-1000, 1000) / 1000f, 0f);
					}
				}
			}
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			this.spriteBatch.Draw(this.dossy, new Vector2((float)this.horPos, this.realrot * 20f - 150f), null, new Color(120, 120, 200, 80), 0f, new Vector2(0f, 0f), this.sizer, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.L257load, new Vector2(0f, 0f), null, Color.White, 0f, new Vector2(0f, 0f), 2f, SpriteEffects.None, 0f);
			for (int i = 0; i < this.locX.Count; i++)
			{
				this.spriteBatch.Draw(this.buttonOn, new Vector2(this.locX[i], this.locY[i]), this.col[i]);
			}
			this.spriteBatch.Draw(this.gear2, new Vector2(160f, 60f), null, Color.White, this.realrot, new Vector2((float)(this.gear2.Width / 2), (float)(this.gear2.Height / 2)), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.gear2, new Vector2(192f, 92f), null, Color.White, -this.realrot + 0.01f, new Vector2((float)(this.gear2.Width / 2), (float)(this.gear2.Height / 2)), 0.66f, SpriteEffects.None, 0f);
			this.spriteBatch.DrawString(this.loadfont1, "Simulated Loading.....   " + this.loadIndex.ToString(), new Vector2(230f, 35f), Color.White);
			this.spriteBatch.DrawString(this.loadfont1, this.messagedisplay, new Vector2((float)(this.graphicsDevice.Viewport.Width / 2) - this.loadfont1.MeasureString(this.messagedisplay).X / 2f, (float)(this.graphicsDevice.Viewport.Height - 130)), new Color(200, 200, 230));
			if (this.colortab.Count <= 0)
			{
				this.loadspot += 2;
				if (this.loadspot > 80)
				{
					this.loadspot = 0;
				}
				this.spriteBatch.Draw(this.rays, new Vector2((float)(this.graphicsDevice.Viewport.Width / 2), (float)(this.graphicsDevice.Viewport.Height / 2)), null, new Color((int)((byte)(255 - this.loadspot * 3)), (int)((byte)(255 - this.loadspot * 3)), (int)((byte)(255 - this.loadspot * 3)), 190), 0f, new Vector2((float)(this.rays.Width / 2), (float)(this.rays.Height / 2)), ((float)this.loadspot / 70f + 1.5f) * 4f, SpriteEffects.None, 0f);
			}
			this.spriteBatch.End();
		}

		// Token: 0x0400168F RID: 5775
		private GraphicsDevice graphicsDevice;

		// Token: 0x04001690 RID: 5776
		private ContentManager content;

		// Token: 0x04001691 RID: 5777
		private Random random;

		// Token: 0x04001692 RID: 5778
		private Texture2D gear2;

		// Token: 0x04001693 RID: 5779
		private Texture2D buttonOn;

		// Token: 0x04001694 RID: 5780
		private Texture2D L257load;

		// Token: 0x04001695 RID: 5781
		private Texture2D rays;

		// Token: 0x04001696 RID: 5782
		private float rot;

		// Token: 0x04001697 RID: 5783
		private float realrot;

		// Token: 0x04001698 RID: 5784
		private SpriteBatch spriteBatch;

		// Token: 0x04001699 RID: 5785
		private SpriteFont loadfont1;

		// Token: 0x0400169A RID: 5786
		private SpriteFont loadfont2;

		// Token: 0x0400169B RID: 5787
		private List<Color> col = new List<Color>();

		// Token: 0x0400169C RID: 5788
		private List<float> locX = new List<float>();

		// Token: 0x0400169D RID: 5789
		private List<float> locY = new List<float>();

		// Token: 0x0400169E RID: 5790
		private int horPos;

		// Token: 0x0400169F RID: 5791
		private float sizer = 1f;

		// Token: 0x040016A0 RID: 5792
		private string[] image0 = new string[0];

		// Token: 0x040016A1 RID: 5793
		private Texture2D myimage;

		// Token: 0x040016A2 RID: 5794
		private Texture2D dossy;

		// Token: 0x040016A3 RID: 5795
		private Color[] colorArray1 = new Color[0];

		// Token: 0x040016A4 RID: 5796
		private string messagedisplay = "";

		// Token: 0x040016A5 RID: 5797
		private GamePadState previousgamepadstate;

		// Token: 0x040016A6 RID: 5798
		private int fader = 255;

		// Token: 0x040016A7 RID: 5799
		private int fadeflag = 1;

		// Token: 0x040016A8 RID: 5800
		private string[] messages = new string[]
		{
			"A Little Bird Told Me", "Alien Life", "Ancient Artifacts", "Buy Better Batteries", "Caution", "Certain Skills Increase Your Rank", "Crystals Can Be Refined", "Do Anything To Stay Alert", "Don't Drop Your Crystals", "Don't Monkey Around In Space",
			"Don't Run With Scissors", "Don't Talk To Strangers", "Eat Later", "Gems Can Be Sold At A Profit", "Gems Can Be Used As Tools", "Go Get Promoted", "Gold Is Worthless In Space", "Great Treasures Wait For You", "In Space Repairs Are Done Yourself", "It's Not Rocket Science",
			"Know Your Friends", "L257 Is Ready For Launch", "Launch Another Rocket For Cash", "Listen To Your Radio", "No Time For Romance", "Ore Becomes Fuel", "Ore Means Life", "Rank Means Increased Salary", "Rare Gems Are Hard To Find", "Recycling Saves Money",
			"Refine Lunar Ore", "Remember To Recharge", "Remember Your Home", "Repair It Yourself", "Repair Your Ship", "Save Your Progress", "Some Actions Increase Your Rank", "Some Repairs Take Longer", "Some Rocks Contain Crystals", "Some Things Cost More Than Money",
			"Take Good Notes", "The Eyes See Everything", "This Is A Treasure Chest", "This Is Not Food", "Use Ore To Build Explosives", "Use Your Radio From Afar", "Watch For Other Life Forms", "Watch For The Signs", "Watch Your Fuel Levels", "You Are Only Human"
		};

		// Token: 0x040016A9 RID: 5801
		private string[] loadx = new string[] { "LOADING", "LOADING", "LOADING", "LOADING", "LOADING", "LOADING", " LOADING" };

		// Token: 0x040016AA RID: 5802
		private int loadspot;

		// Token: 0x040016AB RID: 5803
		private List<Color> colortab = new List<Color>();

		// Token: 0x040016AC RID: 5804
		private List<int> posxtab = new List<int>();

		// Token: 0x040016AD RID: 5805
		private List<int> posytab = new List<int>();

		// Token: 0x040016AE RID: 5806
		private int pulse = 10;

		// Token: 0x040016AF RID: 5807
		private int loadIndex;

		// Token: 0x040016B0 RID: 5808
		private SoundEffect button;

		// Token: 0x040016B1 RID: 5809
		private int num;
	}
}
