using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000A2 RID: 162
	internal class Background : GameScreen
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x0013F5F0 File Offset: 0x0013D7F0
		public Background()
		{
			base.TransitionOnTime = TimeSpan.FromSeconds(0.5);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
			this.meshmaker = new GlobeMaker();
			this.random = new Random(5);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0013FB6C File Offset: 0x0013DD6C
		public override void LoadContent()
		{
			if (this.content == null)
			{
				this.content = new ContentManager(base.ScreenManager.Game.Services, "Content");
			}
			this.sc = base.ScreenManager;
			this.sc.bgindex = this.random.Next(1, 10);
			this.sc.bgindex = 2;
			this.font = this.content.Load<SpriteFont>("astro\\fonts\\menufont");
			this.typer = this.content.Load<SpriteFont>("astro\\fonts\\typer");
			this.stardome = this.content.Load<Model>("astro\\models\\dome3");
			this.L257main = this.content.Load<Texture2D>("astro\\menus\\L257main");
			this.ring1 = this.content.Load<Texture2D>("astro\\menus\\ring1");
			this.ring2 = this.content.Load<Texture2D>("astro\\menus\\ring2");
			this.ruler = this.content.Load<Texture2D>("astro\\menus\\ruler");
			this.camradian = base.ScreenManager.camradian;
			this.camradius = base.ScreenManager.camradius;
			this.startwidth2 = (float)this.L257main.Width;
			this.starthite2 = (float)this.L257main.Height;
			this.startaspect2 = this.startwidth2 / this.starthite2;
			this.stretch2 = this.startaspect2 / this.sc.aspectratio2;
			Vector2 vector = new Vector2((float)this.L257main.Width, (float)this.L257main.Height);
			if (vector.X / vector.Y >= this.sc.aspectratio2)
			{
				this.src = new Rectangle(0, 0, this.L257main.Width, this.L257main.Height);
				this.amt = (float)this.L257main.Width / vector.X;
				this.destr = new Rectangle((int)((float)this.sc.width * 0.5f), (int)((float)this.sc.hite * 0.5f), (int)((float)this.sc.width * this.amt), (int)((float)this.sc.hite / this.stretch2 * this.amt));
				this.ctr = new Vector2((float)this.L257main.Width, (float)this.L257main.Height) / 2f;
			}
			else
			{
				this.src = new Rectangle(0, 0, this.L257main.Width, this.L257main.Height);
				this.amt = (float)this.L257main.Height / vector.Y;
				this.destr = new Rectangle((int)((float)this.sc.width * 0.5f), (int)((float)this.sc.hite * 0.5f), (int)((float)this.sc.width * this.stretch2 * this.amt), (int)((float)this.sc.hite * this.amt));
				this.ctr = new Vector2((float)this.L257main.Width, (float)this.L257main.Height) / 2f;
			}
			this.globedataA[0] = "SOLAR SYSTEM\nSize = .0048 LightYears\nVastness of Stars\nMade up of 9 planets";
			this.globedataA[1] = "PLANET MERCURY\nDiameter = 4879 km\nDist = 579,000,000 km\nGravity = 3.7 mss";
			this.globedataA[2] = "PLANET VENUS\nDiameter = 12104 km\nDist = 1,082,000,000 km\nGravity = 8.9 mss";
			this.globedataA[3] = "PLANET EARTH\nDiameter = 12756 km\nDist = 1,496,000,000 km\nGravity = 9.8 mss";
			this.globedataA[4] = "PLANET MARS\nDiameter = 6792 km\nDist = 2,279,000,000 km\nGravity = 3.7 mss";
			this.globedataA[5] = "PLANET JUPITER\nDiameter = 142984 km\nDist = 7,786,000,000 km\nGravity = 23.1 mss";
			this.globedataA[6] = "PLANET SATURN\nDiameter = 120536 km\nDist = 14,335,000,000 km\nGravity = 9.0 mss";
			this.globedataA[7] = "PLANET URANUS\nDiameter = 51118 km\nDist = 28,725,000,000 km\nGravity = 8.7 mss";
			this.globedataA[8] = "PLANET NEPTUNE\nDiameter = 49528 km\nDist = 44,951,000,000 km\nGravity = 11.0 mss";
			this.globedataA[9] = "PLANET PLUTO \nDiameter = 2390 km\nDist = 58,700,000,000 km\nGravity = 0.6 mss\nNo Longer a Planet";
			this.globedataB[0] = "THE OORT CLOUD\nDist= 1 Light Year\nBody of Comets\nBeyond solar System";
			this.globedataB[1] = "PLANET MERCURY\nDay Length = 4222 hours\nTemperature = 332 F\nMoons = 2";
			this.globedataB[2] = "PLANET VENUS\nDay Length = 2802 hours\nTemperature = 867 F\nNumber of Moons = 3";
			this.globedataB[3] = "PLANET EARTH\nDay Length = 24 hours\nTemperature = 59 F\nNumber of Moons = 1";
			this.globedataB[4] = "PLANET MARS\nDay Length = 24.7 hours\nTemperature = -85 F\nNumber of Moons = 2";
			this.globedataB[5] = "PLANET JUPITER\nDay Length = 9.9 hours\nTemperature = -166 F\nNumber of Moons = 63";
			this.globedataB[6] = "PLANET SATURN\nDay Length = 10.7 hours\nTemperature = -220 F\nNumber of Moons = 60";
			this.globedataB[7] = "PLANET URANUS\nDay Length = 17.2 hours\nTemperature = -319 F\nNumber of Moons = 27";
			this.globedataB[8] = "PLANET NEPTUNE\nDay Length = 16.1 hours\nTemperature = -328 F\nNumber of Moons = 13";
			this.globedataB[9] = "PLANET PLUTO \nDay Length = 153 hours\nTemp= -373 F\nNumber of Moons = 3\nNo Longer a Planet";
			this.globedataC[0] = "Kupier Belt\nSize= .025 LightYears\\nBody of Asteroids\nIce Composites";
			this.globedataC[1] = "PLANET MERCURY\nAxial Tilt = .01 degrees\nWeak Magnetic Field\nFaint Ring System";
			this.globedataC[2] = "PLANET VENUS\nAxial Tilt = 177 degrees\nBenial Magnetic Field\nNo Ring System";
			this.globedataC[3] = "PLANET EARTH\nAxial Tilt = 23.5 degrees\nLevel Magnetic Field\nNo Ring System";
			this.globedataC[4] = "PLANET MARS\nAxial Tilt = 25.2 degrees\nNo Magnetic Field\nNo Ring System";
			this.globedataC[5] = "PLANET JUPITER\nAxial Tilt = 3 degrees\nNo Magnetic Field\nHas 3 Ring System";
			this.globedataC[6] = "PLANET SATURN\nAxial Tilt = 26.7 degrees\nStrong Magnetic Field\nHas 800 Ring System";
			this.globedataC[7] = "PLANET URANUS\nAxial Tilt = 97.8 degrees\nViolent Magnetic Field\nHas a 13 Ring System";
			this.globedataC[8] = "PLANET NEPTUNE\nAxial Tilt = 28.3 degrees\nStrong Magnetic Field\nHas a 5 Ring System";
			this.globedataC[9] = "PLANET PLUTO\nAxial Tilt = 122.5 degrees\nMagnetic Field Unknown\nNo Ring System\nNo Longer a Planet";
			this.text = this.globedataA[base.ScreenManager.bgindex];
			this.mytimer = (float)this.random.Next(500, 5000);
			this.spec1inc = (float)this.random.Next(1, 100) / 800f;
			this.spec1dur = (float)this.random.Next(0, 9);
			this.spec2inc = (float)this.random.Next(5, 50) / 1700f;
			this.spec2dur = (float)this.random.Next(60, 155);
			this.spec2dur = (float)this.random.Next(0, 15);
			this.spec3inc = (float)this.random.Next(5, 70) / 100f;
			this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), this.sc.aspectratio2, 1f, 490000f);
			this.campos = new Vector3(0f, 0f, 0f);
			this.viewMatrix = Matrix.CreateLookAt(this.campos, new Vector3(0f, 0f, 0f), Vector3.Up);
			this.asteroid = this.content.Load<Model>("astro\\models\\asteroid");
			this.asteroid2 = this.content.Load<Model>("astro\\models\\asteroid2");
			this.planets = new Texture2D[10];
			this.effect = this.content.Load<Effect>("astro\\shaders\\planetShader");
			this.atmosphere = this.content.Load<Model>("astro\\models\\atmosphere");
			this.atmos1 = this.content.Load<Texture2D>("astro\\sprites\\planets\\atmos2");
			this.rings1 = this.content.Load<Texture2D>("astro\\sprites\\planets\\rings1");
			this.rings2 = this.content.Load<Texture2D>("astro\\sprites\\planets\\rings2");
			this.rings3 = this.content.Load<Texture2D>("astro\\sprites\\planets\\rings3");
			for (int i = 1; i <= 9; i++)
			{
				this.planets[i] = this.content.Load<Texture2D>("astro\\sprites\\planets\\planet" + i);
			}
			this.meshmaker.Load(this.content, base.ScreenManager, "planet", 1f);
			this.meshmaker.LoadVertices();
			this.spriteBatch = new SpriteBatch(this.sc.GraphicsDevice);
			this.pop1 = this.content.Load<SoundEffect>("astro\\Audio\\pop");
			this.menubase = this.content.Load<SoundEffect>("astro\\Audio\\menubase");
			this.menubaseI = this.menubase.CreateInstance();
			this.spacewind = this.content.Load<SoundEffect>("astro\\Audio\\spaceWindlow");
			this.spacewindI = this.spacewind.CreateInstance();
			this.menudots = this.content.Load<SoundEffect>("astro\\Audio\\menudots");
			this.menudotsI = this.menudots.CreateInstance();
			this.tone1 = this.content.Load<SoundEffect>("astro\\Audio\\tone1");
			this.tone1I = this.tone1.CreateInstance();
			this.tone2 = this.content.Load<SoundEffect>("astro\\Audio\\tone2");
			this.tone2I = this.tone2.CreateInstance();
			this.tone3 = this.content.Load<SoundEffect>("astro\\Audio\\tone3");
			this.tone3I = this.tone3.CreateInstance();
			this.tone4 = this.content.Load<SoundEffect>("astro\\Audio\\tone4");
			this.tone4I = this.tone4.CreateInstance();
			this.sc.menutype = 0;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x001403D5 File Offset: 0x0013E5D5
		public override void UnloadContent()
		{
			this.content.Unload();
			this.content.Dispose();
			this.content = null;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x001403F4 File Offset: 0x0013E5F4
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, false);
			float num = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
			this.timer += 0.0166f * num;
			if (this.spacewindI.State == SoundState.Stopped)
			{
				this.spacewindI.Volume = 0.5f * this.sc.mv;
				this.spacewindI.IsLooped = true;
				this.spacewindI.Play();
			}
			else
			{
				this.spacewindI.Resume();
				this.spacewindI.Volume = 0.5f * this.sc.mv;
			}
			if (this.menubaseI == null)
			{
				this.menubaseI.Volume = 0.5f * this.sc.mv;
				this.menubaseI.IsLooped = true;
				this.menubaseI.Play();
			}
			else
			{
				this.menubaseI.Resume();
				this.menubaseI.Volume = 0.5f * this.sc.mv;
			}
			this.tonetimer--;
			this.countdown--;
			if (this.tonetimer <= 0)
			{
				this.tonesilence--;
			}
			if (this.tonesilence <= 0)
			{
				this.tonetimer = this.random.Next(200, 1500);
				this.tonerepeat = this.random.Next(120, 600);
				this.countdown = this.tonerepeat;
				this.tonechoice = this.random.Next(0, 5);
				this.tonesilence = this.random.Next(800, 2100);
				if (this.tonechoice == 2)
				{
					this.tonerepeat = this.random.Next(6, 12);
				}
			}
			if (this.tonechoice == 0 && this.countdown <= 0 && this.tonetimer > 0)
			{
				this.countdown = this.tonerepeat;
				if (this.tone1I.State == SoundState.Stopped)
				{
					this.tone1I.Volume = 0.5f * this.sc.mv;
					this.tone1I.Play();
				}
			}
			if (this.tonechoice == 1 && this.tonerepeat > 0 && this.tonetimer > 0)
			{
				this.countdown = this.tonerepeat;
				if (this.tone2I.State == SoundState.Stopped)
				{
					this.tone2I.Volume = 0.5f * this.sc.mv;
					this.tone2I.Play();
				}
			}
			if (this.tonechoice == 2 && this.tonerepeat > 0 && this.tonetimer > 0)
			{
				this.countdown = this.tonerepeat;
				if (this.menudotsI.State == SoundState.Stopped)
				{
					this.menudotsI.Volume = 0.5f * this.sc.mv;
					this.menudotsI.Play();
				}
			}
			if (this.tonechoice == 3 && this.tonerepeat > 0 && this.tonetimer > 0)
			{
				this.countdown = this.tonerepeat;
				if (this.tone3I.State == SoundState.Stopped)
				{
					this.tone3I.Volume = 0.5f * this.sc.mv;
					this.tone3I.Play();
				}
			}
			if (this.tonechoice == 4 && this.tonerepeat > 0 && this.tonetimer > 0)
			{
				this.countdown = this.tonerepeat;
				if (this.tone4I.State == SoundState.Stopped)
				{
					this.tone4I.Volume = 0.5f * this.sc.mv;
					this.tone4I.Play();
				}
			}
			if (base.ScreenManager.drawflag == 1 && MenuScreen.title != "Video Settings")
			{
				if (this.sc.bgindex < 10)
				{
					this.campos = new Vector3(0f, -40f, 800f);
					this.camradius += base.ScreenManager.rightstickY * 50f * num;
					this.camradian += -base.ScreenManager.rightstickX / 80f * num;
					if (this.camradius < 600f)
					{
						this.camradius = 600f;
					}
					if (this.camradius > 9000f)
					{
						this.camradius = 9000f;
					}
					this.campos.X = (float)Math.Sin((double)this.camradian) * this.camradius;
					this.campos.Z = (float)(-(float)Math.Cos((double)this.camradian)) * this.camradius;
					if (base.ScreenManager.leftbumper)
					{
						this.camlook.X = this.camlook.X - (float)Math.Sin((double)(this.camradian + 1.57f)) * 8f * num;
						this.camlook.Z = this.camlook.Z - (float)(-(float)Math.Cos((double)(this.camradian + 1.57f))) * 8f * num;
					}
					if (base.ScreenManager.rightbumper)
					{
						this.camlook.X = this.camlook.X + (float)Math.Sin((double)(this.camradian + 1.57f)) * 8f * num;
						this.camlook.Z = this.camlook.Z + (float)(-(float)Math.Cos((double)(this.camradian + 1.57f))) * 8f * num;
					}
					if (base.ScreenManager.lefttrigger > 0f)
					{
						this.camlook.Y = this.camlook.Y + 6f * base.ScreenManager.lefttrigger * num;
					}
					if (base.ScreenManager.righttrigger > 0f)
					{
						this.camlook.Y = this.camlook.Y - 6f * base.ScreenManager.righttrigger * num;
					}
					this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(35f), this.sc.aspectratio2, 1f, 5000000f);
					this.viewMatrix = Matrix.CreateLookAt(this.campos, this.camlook, Vector3.Up);
					base.ScreenManager.camradian = this.camradian;
					base.ScreenManager.camradius = this.camradius;
				}
				base.Update(gameTime, otherScreenHasFocus, false);
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00140A48 File Offset: 0x0013EC48
		public override void Draw(GameTime gameTime)
		{
			float num = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (this.fadeflag == 1)
			{
				this.additiveStates();
				this.DrawStars(this.stardome);
				this.DrawAtmos(this.atmosphere);
				this.RestoreRenderStates();
				this.DrawGlobe();
				this.DrawAsteroid(this.asteroid, 3100f, 1f, 2f);
				this.DrawAsteroid(this.asteroid2, 2500f, 88f, 2.2f);
				this.DrawAsteroid(this.asteroid, 2450f, 48f, 3f);
				this.DrawAsteroid(this.asteroid2, 3300f, 12f, 1.5f);
				this.DrawAsteroid(this.asteroid, 2200f, 25f, 2f);
				this.DrawAsteroid(this.asteroid2, 1900f, 34f, 2.3f);
				this.DrawAsteroid(this.asteroid, 3521f, 112f, 2.7f);
				this.DrawAsteroid(this.asteroid2, 700f, 233f, 3.7f);
				this.DrawAsteroid(this.asteroid, 1235f, 134f, 2.7f);
				this.DrawAsteroid(this.asteroid2, 1070f, 72f, 3.1f);
				this.DrawAsteroid(this.asteroid, 600f, 54f, 0.7f);
				this.DrawAsteroid(this.asteroid2, 625f, 43f, 0.9f);
				if (base.ScreenManager.bgindex >= 5 && base.ScreenManager.bgindex <= 8)
				{
					this.DrawRing(this.atmosphere);
				}
				this.DrawTitle(num);
				if (this.fader > 0f)
				{
					base.ScreenManager.FadeBackBufferToBlack((int)this.fader);
				}
			}
			if (base.ScreenManager.menuflag == 0)
			{
				this.fader -= 10f * num;
				if (this.fader < (10f - base.ScreenManager.fadeSetting) * 25f)
				{
					this.fader = (10f - base.ScreenManager.fadeSetting) * 25f * num;
				}
				this.fadeflag = 1;
			}
			if (base.ScreenManager.menuflag == 1 && this.fadeflag == 1)
			{
				this.fader += 15f * num;
			}
			if (this.fader > 255f && this.fadeflag == 1)
			{
				this.fader = 255f;
				this.fadeflag = 0;
			}
			if (base.TransitionAlpha < 255)
			{
				base.ScreenManager.FadeBackBufferToBlack((int)(byte.MaxValue - base.TransitionAlpha));
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00140D10 File Offset: 0x0013EF10
		private void DrawTitle(float timeadjust)
		{
			this.sc.typewriterblank -= 1f * timeadjust;
			this.typewritercount -= 1f * timeadjust;
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, null, null, null, this.sc.ScaleMatrix1);
			if (this.sc.typewriterblank <= 0f)
			{
				if (this.sc.textflag == 0)
				{
					if (this.textcount == 1)
					{
						this.text = this.globedataA[base.ScreenManager.bgindex];
					}
					if (this.textcount == 2)
					{
						this.text = this.globedataB[base.ScreenManager.bgindex];
					}
					if (this.textcount == 3)
					{
						this.text = this.globedataC[base.ScreenManager.bgindex];
					}
					this.textcount++;
					if (this.textcount > 3)
					{
						this.textcount = 1;
					}
					this.sc.textflag = 1;
				}
				if (this.typewritercount <= 0f)
				{
					this.sc.typeposition += 1f * Math.Max(timeadjust, 1f);
				}
				if (this.sc.typeposition < (float)this.text.Length && this.typewritercount <= 0f)
				{
					float num = (float)this.random.Next(-70, 90) / 100f;
					this.pop1.Play(0.8f * this.sc.mv, num, 0f);
					this.typewritercount = (float)this.sc.typewriterdelay;
				}
				if (this.sc.typeposition > (float)this.text.Length)
				{
					this.sc.typeposition = (float)this.text.Length;
					this.sc.typewriterwait -= 1f * timeadjust;
				}
				float num2 = (float)(Math.Sin((double)(this.timer * 30f)) * 15.0) + 240f;
				if (this.sc.typewriterwait < 240f)
				{
					num2 = this.sc.typewriterwait + (float)(Math.Sin((double)(this.timer * 30f)) * 15.0);
				}
				num2 /= 245f;
				string text = "";
				if (this.sc.typeposition < (float)this.text.Length)
				{
					text = "|";
				}
				this.spriteBatch.DrawString(this.typer, this.text.Substring(0, (int)this.sc.typeposition) + text, new Vector2(150f, this.sc.typevertical), new Color(115, 180, 255, 255) * num2);
				if ((int)this.sc.typeposition == this.text.Length)
				{
					this.sc.typevertical -= (0.1f + (float)this.random.Next(1, 200) / 1000f) * timeadjust;
				}
				if (this.sc.typewriterwait <= 0f)
				{
					this.sc.textflag = 0;
					this.sc.typewriterblank = (float)this.random.Next(600, 1600);
					this.sc.typewriterwait = (float)this.random.Next(800, 1100);
					this.sc.typeposition = 0f;
					this.sc.typevertical = (float)this.random.Next(430, 500);
					this.sc.typewriterdelay = this.random.Next(2, 7);
				}
			}
			this.spriteBatch.Draw(this.ring1, new Vector2(490f, 205f), null, new Color(255, 255, 255, 255), -this.camradian * 2f, new Vector2((float)(this.ring1.Width / 2), (float)(this.ring1.Height / 2)), 1f + this.camradius / 35000f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.ring2, new Vector2(490f, 205f), null, new Color(255, 255, 255, 255), this.camradian, new Vector2((float)(this.ring2.Width / 2), (float)(this.ring2.Height / 2)), 1f - this.camradius / 19000f, SpriteEffects.None, 0f);
			float num3 = 720f;
			float num4 = this.camradian * 120f * 2f;
			float num5 = this.camradian * 120f * 2f + num3;
			float num6 = this.camradian * 120f * 2f - num3;
			float num7 = (num4 % (1440f + num3) + 1.5f * (1440f + num3)) % (1440f + num3);
			float num8 = (num5 % (1440f + num3) + 1.5f * (1440f + num3)) % (1440f + num3);
			float num9 = (num6 % (1440f + num3) + 1.5f * (1440f + num3)) % (1440f + num3);
			float num10 = 1150f;
			if (this.sc.startaspect < this.sc.aspectratio2 && this.sc.fullscreen == 0)
			{
				num10 = 1110f + (0f - (1110f * this.sc.stretch - 1110f));
			}
			this.spriteBatch.Draw(this.ruler, new Vector2(num10, num7 - num3 - 250f), null, new Color(255, 255, 255, 220), 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.ruler, new Vector2(num10, num8 - num3 - 250f), null, new Color(255, 255, 255, 220), 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.ruler, new Vector2(num10, num9 - num3 - 250f), null, new Color(255, 255, 255, 220), 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.End();
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, null);
			this.spriteBatch.Draw(this.L257main, this.destr, new Rectangle?(this.src), Color.White, 0f, this.ctr, SpriteEffects.None, 0f);
			this.spriteBatch.End();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00141490 File Offset: 0x0013F690
		private void DrawStars(Model model)
		{
			foreach (ModelMesh modelMesh in model.Meshes)
			{
				foreach (Effect effect in modelMesh.Effects)
				{
					BasicEffect basicEffect = (BasicEffect)effect;
					basicEffect.Texture = this.sc.Globalstarmap.manmadestars;
					basicEffect.LightingEnabled = true;
					basicEffect.AmbientLightColor = new Vector3(0.75f, 0.75f, 1f);
					basicEffect.DiffuseColor = new Vector3(0.75f, 0.75f, 1f);
					basicEffect.View = this.viewMatrix;
					basicEffect.Projection = this.projectionMatrix;
					basicEffect.PreferPerPixelLighting = false;
					basicEffect.Alpha = 1f;
					if (model == this.stardome)
					{
						basicEffect.World = Matrix.CreateScale(90f);
					}
				}
				modelMesh.Draw();
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x001415C4 File Offset: 0x0013F7C4
		private void DrawBlob(Model model)
		{
			foreach (ModelMesh modelMesh in model.Meshes)
			{
				foreach (Effect effect in modelMesh.Effects)
				{
					BasicEffect basicEffect = (BasicEffect)effect;
					basicEffect.Texture = this.sc.starblob;
					basicEffect.LightingEnabled = true;
					basicEffect.AmbientLightColor = new Vector3(1f, 1f, 1f);
					basicEffect.DiffuseColor = new Vector3(1f, 1f, 1f);
					basicEffect.View = this.viewMatrix;
					basicEffect.Projection = this.projectionMatrix;
					basicEffect.PreferPerPixelLighting = false;
					basicEffect.Alpha = 1f;
					if (model == this.stardome)
					{
						basicEffect.World = Matrix.CreateScale(95f);
					}
				}
				modelMesh.Draw();
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x001416F4 File Offset: 0x0013F8F4
		private void DrawAsteroid(Model model, float distance, float timeoffset, float scaleme)
		{
			this.lightdirection = new Vector3(0.45f, -0.7f, (float)Math.Sin((double)(this.timer / 8f)) * 0.8f - 0.6f);
			foreach (ModelMesh modelMesh in model.Meshes)
			{
				foreach (Effect effect in modelMesh.Effects)
				{
					BasicEffect basicEffect = (BasicEffect)effect;
					basicEffect.LightingEnabled = true;
					basicEffect.DiffuseColor = new Vector3(0.9f, 0.9f, 0.9f);
					basicEffect.View = this.viewMatrix;
					basicEffect.Projection = this.projectionMatrix;
					basicEffect.PreferPerPixelLighting = false;
					basicEffect.Alpha = 1f;
					basicEffect.AmbientLightColor = new Vector3(0.15f, 0.15f, 0.15f);
					basicEffect.DirectionalLight0.Enabled = true;
					basicEffect.SpecularPower = 5f;
					basicEffect.DirectionalLight0.Direction = this.lightdirection;
					basicEffect.DirectionalLight0.DiffuseColor = this.planetColor[base.ScreenManager.bgindex] / 2f + new Vector3(0.4f, 0.4f, 0.4f);
					basicEffect.PreferPerPixelLighting = true;
					Matrix matrix = Matrix.CreateTranslation(distance, 0f, 0f);
					float num = this.timer / (this.planetSpeed[base.ScreenManager.bgindex] * 4f) + timeoffset;
					float num2 = MathHelper.ToRadians(this.planetDir[base.ScreenManager.bgindex]);
					Vector3 vector = new Vector3((float)Math.Tan((double)num2), 1f, 0f);
					vector.Normalize();
					Matrix matrix2 = Matrix.CreateRotationZ(-num2) * Matrix.CreateFromAxisAngle(vector, num);
					Quaternion quaternion = Quaternion.CreateFromRotationMatrix(matrix2);
					basicEffect.World = Matrix.CreateScale(scaleme) * Matrix.CreateRotationX(num * 24f + timeoffset * 5f) * Matrix.Transform(matrix, quaternion);
				}
				modelMesh.Draw();
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00141980 File Offset: 0x0013FB80
		private void DrawAtmos(Model model)
		{
			foreach (ModelMesh modelMesh in model.Meshes)
			{
				foreach (Effect effect in modelMesh.Effects)
				{
					BasicEffect basicEffect = (BasicEffect)effect;
					basicEffect.LightingEnabled = true;
					basicEffect.DirectionalLight0.Enabled = true;
					float num = -this.camradian + 3.14f;
					basicEffect.DirectionalLight0.Direction = new Vector3(this.campos.X, -0.6f + (float)Math.Sin((double)(this.timer / 8f)) / 2f, this.campos.Z);
					basicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.19f, 0.19f, 0.19f);
					basicEffect.View = this.viewMatrix;
					basicEffect.Projection = this.projectionMatrix;
					basicEffect.PreferPerPixelLighting = true;
					basicEffect.AmbientLightColor = this.planetColor[base.ScreenManager.bgindex];
					basicEffect.Texture = this.atmos1;
					basicEffect.World = Matrix.CreateScale(this.planetScale[base.ScreenManager.bgindex] * 1.08f) * Matrix.CreateRotationY(num);
				}
				modelMesh.Draw();
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00141B38 File Offset: 0x0013FD38
		private void DrawRing(Model model)
		{
			foreach (ModelMesh modelMesh in model.Meshes)
			{
				foreach (Effect effect in modelMesh.Effects)
				{
					BasicEffect basicEffect = (BasicEffect)effect;
					Vector3 vector = new Vector3(0.45f, 0.5f, (float)Math.Sin((double)(this.timer / 8f)) * 0.5f - 0.6f);
					new Vector3(0.05f, 0.05f, 0.05f);
					Vector3 vector2 = new Vector3(0.6f, 0.6f, 0.6f);
					basicEffect.LightingEnabled = true;
					basicEffect.DirectionalLight0.Enabled = true;
					basicEffect.DirectionalLight0.Direction = vector;
					basicEffect.DirectionalLight0.DiffuseColor = vector2;
					basicEffect.View = this.viewMatrix;
					basicEffect.Projection = this.projectionMatrix;
					basicEffect.PreferPerPixelLighting = true;
					basicEffect.AmbientLightColor = this.planetColor[base.ScreenManager.bgindex] / 3f;
					basicEffect.Texture = this.rings1;
					Matrix identity = Matrix.Identity;
					float num = this.timer / this.planetSpeed[base.ScreenManager.bgindex] * 1f;
					float num2 = MathHelper.ToRadians(this.planetDir[base.ScreenManager.bgindex]);
					Vector3 vector3 = new Vector3((float)Math.Tan((double)num2), 1f, 0f);
					vector3.Normalize();
					float num3 = 1.2f;
					if (base.ScreenManager.bgindex == 5)
					{
						num3 = 2f;
						basicEffect.Alpha = 0.5f;
						basicEffect.Texture = this.rings3;
					}
					if (base.ScreenManager.bgindex == 7)
					{
						num3 = 0.3f;
						basicEffect.Alpha = 0.8f;
						basicEffect.Texture = this.rings3;
					}
					if (base.ScreenManager.bgindex == 8)
					{
						num3 = 2f;
						basicEffect.Alpha = 0.4f;
						basicEffect.Texture = this.rings2;
					}
					basicEffect.Alpha = 1f;
					basicEffect.World = Matrix.CreateScale(this.planetScale[base.ScreenManager.bgindex] + num3) * Matrix.CreateRotationX(1.57f) * Matrix.CreateRotationZ(-num2) * Matrix.CreateFromAxisAngle(vector3, num);
				}
				modelMesh.Draw();
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00141E10 File Offset: 0x00140010
		private void DrawGlobe()
		{
			Vector3 vector = new Vector3(0.45f, -0.7f, (float)Math.Sin((double)(this.timer / 8f)) * 0.8f - 0.6f);
			float num = 0.25f;
			Vector3 vector2 = new Vector3(0.9f, 0.9f, 0.9f);
			Vector3 vector3 = new Vector3(1f, 1f, 1f);
			this.effect.CurrentTechnique = this.effect.Techniques["planet"];
			this.effect.Parameters["Texture0"].SetValue(this.planets[base.ScreenManager.bgindex]);
			Matrix matrix = Matrix.Identity;
			float num2 = this.timer / this.planetSpeed[base.ScreenManager.bgindex] * 2f;
			float num3 = MathHelper.ToRadians(this.planetDir[base.ScreenManager.bgindex]);
			Vector3 vector4 = new Vector3((float)Math.Tan((double)num3), 1f, 0f);
			vector4.Normalize();
			matrix = Matrix.CreateScale(this.planetScale[base.ScreenManager.bgindex]) * Matrix.CreateRotationZ(-num3) * Matrix.CreateFromAxisAngle(vector4, num2);
			this.effect.Parameters["World"].SetValue(matrix);
			this.effect.Parameters["View"].SetValue(this.viewMatrix);
			this.effect.Parameters["Projection"].SetValue(this.projectionMatrix);
			this.effect.Parameters["CamPos"].SetValue(this.campos);
			this.effect.Parameters["Ambient"].SetValue(num);
			this.effect.Parameters["Diffuse"].SetValue(vector2);
			this.effect.Parameters["Specular"].SetValue(vector3);
			this.effect.Parameters["specIntensity"].SetValue(this.planetIntense[base.ScreenManager.bgindex] + (float)Math.Sin((double)(this.timer / 11f)) * 0.5f);
			this.effect.Parameters["specPower"].SetValue(this.planetSpec[base.ScreenManager.bgindex]);
			this.effect.Parameters["LightDirection"].SetValue(Vector3.Normalize(vector));
			this.effect.CurrentTechnique.Passes[0].Apply();
			base.ScreenManager.GraphicsDevice.SetVertexBuffer(this.meshmaker.terrainVertexBuffer);
			base.ScreenManager.GraphicsDevice.Indices = this.meshmaker.terrainIndexBuffer;
			int vertexCount = this.meshmaker.terrainVertexBuffer.VertexCount;
			int indexCount = this.meshmaker.terrainIndexBuffer.IndexCount;
			base.ScreenManager.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexCount, 0, indexCount / 3);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00142150 File Offset: 0x00140350
		private void additiveStates()
		{
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
			this.sc.GraphicsDevice.BlendState = BlendState.Additive;
			this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
			base.ScreenManager.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x001421B8 File Offset: 0x001403B8
		private void RestoreRenderStates()
		{
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			this.sc.GraphicsDevice.BlendState = BlendState.AlphaBlend;
			this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
			base.ScreenManager.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
		}

		// Token: 0x040016B2 RID: 5810
		private List<int> mylist = new List<int>();

		// Token: 0x040016B3 RID: 5811
		private List<float> oldx = new List<float>();

		// Token: 0x040016B4 RID: 5812
		private List<float> oldy = new List<float>();

		// Token: 0x040016B5 RID: 5813
		private List<int> fuel = new List<int>();

		// Token: 0x040016B6 RID: 5814
		private float spec1inc = 0.1f;

		// Token: 0x040016B7 RID: 5815
		private float spec2inc = 0.001f;

		// Token: 0x040016B8 RID: 5816
		private float spec3inc = 0.1f;

		// Token: 0x040016B9 RID: 5817
		private float spec1dur = 35f;

		// Token: 0x040016BA RID: 5818
		private float spec2dur = 25f;

		// Token: 0x040016BB RID: 5819
		private int[] cloud = new int[0];

		// Token: 0x040016BC RID: 5820
		private int[] cloud1 = new int[]
		{
			840, 430, 838, 434, 837, 439, 834, 442, 829, 443,
			825, 445, 822, 448, 819, 451, 814, 452, 808, 452,
			802, 452, 799, 455, 795, 457, 790, 458, 789, 463,
			790, 468, 793, 471, 799, 471, 805, 471, 811, 471,
			817, 471, 820, 468, 826, 468, 829, 471, 834, 472,
			838, 474, 844, 474, 850, 474, 855, 475, 859, 477,
			865, 477, 871, 477, 877, 477, 880, 474, 886, 474,
			889, 477, 892, 480, 898, 480, 903, 481, 907, 483,
			910, 480, 916, 480, 922, 480, 925, 477, 931, 477,
			934, 474, 940, 474, 945, 475, 948, 478, 952, 480,
			958, 480, 964, 480, 970, 480, 975, 481, 979, 483,
			985, 483, 991, 483, 997, 483, 1003, 483, 1009, 483,
			1015, 483, 1021, 483, 1027, 483, 1030, 480, 1033, 477,
			1036, 474, 1042, 474, 1048, 474, 1054, 474, 1060, 474,
			1066, 474, 1072, 474, 1078, 474, 1084, 474, 1089, 475,
			1093, 477, 1099, 477, 1105, 477, 1108, 474, 1111, 471,
			1116, 472, 1120, 474, 1126, 474, 1129, 471, 1135, 471,
			1138, 468, 1138, 462, 1135, 459, 1130, 458, 1124, 458,
			1121, 455, 1118, 452, 1115, 449, 1114, 444, 1111, 441,
			1108, 438, 1103, 437, 1097, 437, 1096, 432, 1099, 429,
			1099, 423, 1096, 420, 1096, 414, 1093, 411, 1091, 407,
			1088, 404, 1084, 402, 1081, 399, 1076, 398, 1070, 398,
			1067, 401, 1062, 402, 1058, 404, 1055, 407, 1052, 410,
			1050, 414, 1047, 417, 1046, 422, 1042, 420, 1042, 414,
			1042, 408, 1040, 404, 1039, 399, 1036, 396, 1034, 392,
			1031, 389, 1028, 386, 1024, 384, 1019, 383, 1013, 383,
			1008, 384, 1004, 386, 1003, 381, 1003, 375, 1003, 369,
			1001, 365, 1000, 360, 997, 357, 994, 354, 991, 351,
			986, 350, 980, 350, 975, 351, 971, 353, 968, 356,
			965, 359, 962, 362, 960, 366, 957, 369, 956, 374,
			954, 378, 953, 383, 950, 380, 946, 378, 941, 377,
			938, 380, 932, 380, 929, 383, 924, 384, 920, 386,
			914, 386, 908, 386, 905, 389, 902, 392, 898, 390,
			893, 389, 887, 389, 884, 392, 879, 393, 876, 396,
			872, 398, 870, 402, 867, 405, 866, 410, 864, 414,
			864, 420, 860, 422, 854, 422, 849, 423, 846, 426,
			842, 428, 840, 430
		};

		// Token: 0x040016BD RID: 5821
		private int[] cloud2 = new int[]
		{
			807, 417, 807, 426, 810, 432, 816, 435, 825, 435,
			831, 438, 840, 438, 846, 441, 852, 444, 858, 447,
			864, 450, 870, 453, 876, 456, 882, 453, 891, 453,
			897, 456, 906, 456, 915, 456, 921, 459, 930, 459,
			936, 456, 942, 453, 948, 450, 957, 450, 960, 456,
			966, 459, 969, 465, 975, 468, 981, 471, 990, 471,
			999, 471, 1005, 468, 1014, 468, 1023, 468, 1032, 468,
			1039, 466, 1047, 465, 1051, 460, 1056, 456, 1060, 451,
			1066, 448, 1071, 444, 1077, 441, 1083, 438, 1089, 435,
			1096, 433, 1102, 430, 1108, 427, 1113, 423, 1114, 415,
			1113, 407, 1108, 403, 1102, 400, 1096, 397, 1089, 395,
			1080, 395, 1074, 392, 1065, 392, 1056, 392, 1047, 392,
			1047, 387, 1048, 379, 1045, 373, 1039, 370, 1035, 365,
			1030, 361, 1024, 358, 1018, 355, 1011, 353, 1002, 353,
			993, 353, 987, 356, 981, 359, 975, 362, 969, 365,
			960, 365, 954, 362, 948, 359, 939, 359, 930, 359,
			924, 362, 918, 365, 912, 368, 906, 371, 903, 377,
			900, 383, 897, 389, 888, 389, 882, 386, 876, 383,
			870, 380, 861, 380, 855, 383, 849, 386, 843, 389,
			837, 392, 831, 395, 825, 398, 819, 401, 813, 404,
			807, 407, 807, 416, 807, 417
		};

		// Token: 0x040016BE RID: 5822
		private int[] cloud3 = new int[]
		{
			795, 451, 795, 456, 797, 459, 799, 462, 804, 462,
			809, 462, 814, 462, 819, 462, 824, 462, 829, 462,
			832, 460, 836, 459, 841, 459, 846, 459, 851, 459,
			856, 459, 861, 459, 863, 462, 867, 463, 870, 465,
			875, 465, 877, 468, 882, 468, 884, 465, 889, 465,
			894, 465, 898, 464, 901, 462, 903, 459, 908, 459,
			912, 460, 915, 462, 920, 462, 925, 462, 928, 460,
			932, 459, 937, 459, 940, 457, 944, 456, 949, 456,
			954, 456, 958, 455, 961, 453, 966, 453, 971, 453,
			976, 453, 981, 453, 986, 453, 990, 454, 993, 456,
			998, 456, 1003, 456, 1005, 459, 1010, 459, 1015, 459,
			1020, 459, 1023, 461, 1027, 462, 1032, 462, 1036, 461,
			1039, 459, 1044, 459, 1049, 459, 1054, 459, 1059, 459,
			1061, 462, 1066, 462, 1071, 462, 1076, 462, 1081, 462,
			1083, 459, 1088, 459, 1090, 462, 1095, 462, 1097, 459,
			1102, 459, 1104, 456, 1109, 456, 1114, 456, 1119, 456,
			1124, 456, 1129, 456, 1134, 456, 1138, 455, 1141, 453,
			1146, 453, 1147, 449, 1150, 447, 1149, 443, 1147, 440,
			1144, 438, 1140, 437, 1135, 437, 1130, 437, 1128, 434,
			1123, 434, 1118, 434, 1113, 434, 1108, 434, 1103, 434,
			1098, 434, 1096, 431, 1093, 429, 1090, 427, 1087, 425,
			1082, 425, 1078, 424, 1075, 422, 1073, 419, 1068, 419,
			1063, 419, 1058, 419, 1053, 419, 1050, 421, 1046, 422,
			1044, 419, 1042, 416, 1039, 414, 1035, 413, 1033, 410,
			1028, 410, 1027, 406, 1027, 401, 1027, 396, 1029, 393,
			1031, 390, 1033, 387, 1035, 384, 1037, 381, 1039, 378,
			1039, 373, 1039, 368, 1037, 365, 1035, 362, 1033, 359,
			1030, 357, 1026, 356, 1024, 353, 1019, 353, 1015, 352,
			1012, 350, 1007, 350, 1003, 349, 1000, 347, 996, 348,
			993, 350, 988, 350, 986, 353, 981, 353, 979, 356,
			976, 354, 972, 353, 967, 353, 962, 353, 957, 353,
			954, 355, 950, 356, 945, 356, 943, 359, 939, 360,
			936, 362, 934, 365, 930, 366, 927, 368, 925, 371,
			924, 375, 921, 377, 921, 382, 918, 384, 918, 389,
			919, 393, 920, 395, 918, 398, 916, 395, 911, 395,
			906, 395, 903, 397, 899, 398, 894, 398, 891, 400,
			888, 402, 885, 404, 882, 406, 879, 408, 876, 410,
			876, 415, 873, 417, 873, 422, 870, 424, 866, 425,
			861, 425, 856, 425, 854, 428, 849, 428, 847, 431,
			845, 434, 843, 437, 840, 439, 836, 440, 831, 440,
			826, 440, 821, 440, 816, 440, 811, 440, 807, 441,
			804, 443, 801, 445, 797, 446, 795, 449, 795, 451
		};

		// Token: 0x040016BF RID: 5823
		private int[] cloud4 = new int[]
		{
			867, 432, 867, 438, 870, 441, 870, 447, 873, 450,
			876, 453, 879, 456, 882, 459, 882, 465, 885, 468,
			888, 471, 891, 474, 894, 477, 897, 480, 903, 480,
			906, 483, 912, 483, 918, 483, 922, 481, 927, 480,
			933, 480, 936, 477, 939, 480, 945, 480, 948, 483,
			951, 486, 957, 486, 963, 486, 969, 486, 975, 486,
			979, 484, 984, 483, 987, 480, 991, 478, 994, 475,
			996, 471, 1002, 471, 1005, 474, 1011, 474, 1017, 474,
			1023, 474, 1029, 474, 1035, 474, 1041, 474, 1037, 476,
			1032, 477, 1029, 480, 1029, 486, 1032, 489, 1035, 492,
			1041, 492, 1047, 492, 1053, 492, 1059, 492, 1065, 492,
			1068, 489, 1072, 487, 1075, 484, 1078, 481, 1081, 478,
			1083, 474, 1086, 471, 1089, 468, 1093, 466, 1096, 463,
			1099, 460, 1102, 457, 1105, 454, 1108, 451, 1111, 448,
			1113, 444, 1116, 441, 1117, 436, 1117, 430, 1114, 427,
			1111, 424, 1108, 421, 1105, 418, 1101, 416, 1098, 413,
			1096, 409, 1099, 406, 1099, 400, 1099, 394, 1098, 389,
			1095, 386, 1090, 385, 1086, 383, 1083, 380, 1077, 380,
			1071, 380, 1068, 377, 1062, 377, 1056, 377, 1053, 380,
			1050, 383, 1044, 383, 1041, 386, 1036, 385, 1039, 382,
			1041, 378, 1042, 373, 1045, 370, 1047, 366, 1048, 361,
			1047, 356, 1045, 352, 1042, 349, 1041, 344, 1038, 341,
			1035, 338, 1030, 337, 1026, 335, 1020, 335, 1014, 335,
			1008, 335, 1005, 338, 1002, 341, 999, 344, 993, 344,
			988, 343, 984, 341, 981, 338, 975, 338, 972, 341,
			966, 341, 963, 344, 960, 347, 957, 350, 954, 353,
			951, 356, 951, 362, 951, 368, 951, 374, 949, 376,
			946, 373, 942, 371, 939, 368, 933, 368, 930, 371,
			927, 374, 924, 377, 918, 377, 915, 380, 912, 383,
			909, 386, 906, 389, 903, 392, 900, 395, 897, 398,
			894, 401, 891, 404, 888, 407, 885, 410, 879, 410,
			876, 413, 873, 416, 873, 422, 870, 425, 867, 428,
			867, 432
		};

		// Token: 0x040016C0 RID: 5824
		private string[] globedataA = new string[]
		{
			"", "", "", "", "", "", "", "", "", "",
			""
		};

		// Token: 0x040016C1 RID: 5825
		private string[] globedataB = new string[]
		{
			"", "", "", "", "", "", "", "", "", "",
			""
		};

		// Token: 0x040016C2 RID: 5826
		private string[] globedataC = new string[]
		{
			"", "", "", "", "", "", "", "", "", "",
			""
		};

		// Token: 0x040016C3 RID: 5827
		private string playername = "NoName";

		// Token: 0x040016C4 RID: 5828
		private string[] phrases = new string[] { "Wow", "Space", "Craters", "SuN", "asteroid", "GAS", "ore" };

		// Token: 0x040016C5 RID: 5829
		private float mytimer = 500f;

		// Token: 0x040016C6 RID: 5830
		private float fader = 255f;

		// Token: 0x040016C7 RID: 5831
		private int fadeflag;

		// Token: 0x040016C8 RID: 5832
		private Vector3 campos;

		// Token: 0x040016C9 RID: 5833
		private Vector3 camlook = new Vector3(-200f, -40f, 0f);

		// Token: 0x040016CA RID: 5834
		private ContentManager content;

		// Token: 0x040016CB RID: 5835
		private Model asteroid;

		// Token: 0x040016CC RID: 5836
		private Model asteroid2;

		// Token: 0x040016CD RID: 5837
		private Matrix projectionMatrix;

		// Token: 0x040016CE RID: 5838
		private Matrix viewMatrix;

		// Token: 0x040016CF RID: 5839
		private Matrix axisMatrix = Matrix.Identity;

		// Token: 0x040016D0 RID: 5840
		private Matrix spinMatrix = Matrix.Identity;

		// Token: 0x040016D1 RID: 5841
		private Vector3 lastpos = Vector3.Zero;

		// Token: 0x040016D2 RID: 5842
		private Vector3 tracepos = new Vector3(-1000f, 0f, 0f);

		// Token: 0x040016D3 RID: 5843
		private Vector3 followpos = Vector3.Zero;

		// Token: 0x040016D4 RID: 5844
		private Vector3 lastfollowpos = Vector3.Zero;

		// Token: 0x040016D5 RID: 5845
		private SpriteBatch spriteBatch;

		// Token: 0x040016D6 RID: 5846
		private SpriteFont font;

		// Token: 0x040016D7 RID: 5847
		private SpriteFont typer;

		// Token: 0x040016D8 RID: 5848
		private Vector3 lightdirection;

		// Token: 0x040016D9 RID: 5849
		private Model atmosphere;

		// Token: 0x040016DA RID: 5850
		private Effect effect;

		// Token: 0x040016DB RID: 5851
		private float timer;

		// Token: 0x040016DC RID: 5852
		private Random random;

		// Token: 0x040016DD RID: 5853
		private GlobeMaker meshmaker;

		// Token: 0x040016DE RID: 5854
		private Texture2D atmos1;

		// Token: 0x040016DF RID: 5855
		private Texture2D rings1;

		// Token: 0x040016E0 RID: 5856
		private Texture2D rings2;

		// Token: 0x040016E1 RID: 5857
		private Texture2D rings3;

		// Token: 0x040016E2 RID: 5858
		private Texture2D[] planets;

		// Token: 0x040016E3 RID: 5859
		private Texture2D L257main;

		// Token: 0x040016E4 RID: 5860
		private Texture2D ring1;

		// Token: 0x040016E5 RID: 5861
		private Texture2D ring2;

		// Token: 0x040016E6 RID: 5862
		private Texture2D ruler;

		// Token: 0x040016E7 RID: 5863
		private float[] planetScale = new float[] { 0f, 0.62f, 0.98f, 0.93f, 0.79f, 1.2f, 1.1f, 1f, 0.97f, 0.72f };

		// Token: 0x040016E8 RID: 5864
		private Vector3[] planetColor = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(0.6f, 0.5f, 0.3f),
			new Vector3(0.4f, 0.52f, 0.3f),
			new Vector3(0.3f, 0.4f, 0.6f),
			new Vector3(0.6f, 0.35f, 0.35f),
			new Vector3(0.8f, 0.5f, 0.3f),
			new Vector3(0.7f, 0.6f, 0.1f),
			new Vector3(0.4f, 0.7f, 0.8f),
			new Vector3(0.2f, 0.2f, 0.8f),
			new Vector3(0.6f, 0.55f, 0.3f)
		};

		// Token: 0x040016E9 RID: 5865
		private float[] planetSpeed = new float[] { 0f, 30f, -49f, 17f, 16f, 8f, 9.5f, -11f, 9.2f, -22f };

		// Token: 0x040016EA RID: 5866
		private float[] planetDir = new float[] { 0f, 0.5f, 44f, -24f, 28f, -6f, 32f, 97f, 30f, 80f };

		// Token: 0x040016EB RID: 5867
		private float[] planetSpec = new float[] { 0f, 8.2f, 7.9f, 6.8f, 12.7f, 26f, 3.6f, 2.6f, 33f, 9.74f };

		// Token: 0x040016EC RID: 5868
		private float[] planetIntense = new float[] { 0f, 3.3f, 1.5f, 1.1f, 0.63f, 1.2f, 0.73f, 0.9f, 0.64f, 1.2f };

		// Token: 0x040016ED RID: 5869
		private Vector3 lastArc = Vector3.Zero;

		// Token: 0x040016EE RID: 5870
		private Vector3 lasttrail = Vector3.Zero;

		// Token: 0x040016EF RID: 5871
		private Vector3 mytrail = Vector3.Zero;

		// Token: 0x040016F0 RID: 5872
		private Vector3 myArc = Vector3.Zero;

		// Token: 0x040016F1 RID: 5873
		private float camradius = 1200f;

		// Token: 0x040016F2 RID: 5874
		private float camradian = 3.14f;

		// Token: 0x040016F3 RID: 5875
		private Model stardome;

		// Token: 0x040016F4 RID: 5876
		private SoundEffect pop1;

		// Token: 0x040016F5 RID: 5877
		private SoundEffect menubase;

		// Token: 0x040016F6 RID: 5878
		private SoundEffect menudots;

		// Token: 0x040016F7 RID: 5879
		private SoundEffect tone1;

		// Token: 0x040016F8 RID: 5880
		private SoundEffect tone2;

		// Token: 0x040016F9 RID: 5881
		private SoundEffect tone3;

		// Token: 0x040016FA RID: 5882
		private SoundEffect tone4;

		// Token: 0x040016FB RID: 5883
		private SoundEffect spacewind;

		// Token: 0x040016FC RID: 5884
		private SoundEffectInstance menubaseI;

		// Token: 0x040016FD RID: 5885
		private SoundEffectInstance spacewindI;

		// Token: 0x040016FE RID: 5886
		private SoundEffectInstance menudotsI;

		// Token: 0x040016FF RID: 5887
		private SoundEffectInstance tone1I;

		// Token: 0x04001700 RID: 5888
		private SoundEffectInstance tone2I;

		// Token: 0x04001701 RID: 5889
		private SoundEffectInstance tone3I;

		// Token: 0x04001702 RID: 5890
		private SoundEffectInstance tone4I;

		// Token: 0x04001703 RID: 5891
		private int tonechoice;

		// Token: 0x04001704 RID: 5892
		private int tonetimer;

		// Token: 0x04001705 RID: 5893
		private int tonerepeat = 3;

		// Token: 0x04001706 RID: 5894
		private int countdown = 3;

		// Token: 0x04001707 RID: 5895
		private int tonesilence = 1200;

		// Token: 0x04001708 RID: 5896
		private float typewritercount;

		// Token: 0x04001709 RID: 5897
		private string text = "";

		// Token: 0x0400170A RID: 5898
		private int textcount = 1;

		// Token: 0x0400170B RID: 5899
		private Effect negEffect;

		// Token: 0x0400170C RID: 5900
		private float startwidth2;

		// Token: 0x0400170D RID: 5901
		private float starthite2;

		// Token: 0x0400170E RID: 5902
		private float startaspect2;

		// Token: 0x0400170F RID: 5903
		private float stretch2;

		// Token: 0x04001710 RID: 5904
		private Rectangle src;

		// Token: 0x04001711 RID: 5905
		private Rectangle destr;

		// Token: 0x04001712 RID: 5906
		private float amt;

		// Token: 0x04001713 RID: 5907
		private Vector2 ctr;

		// Token: 0x04001714 RID: 5908
		private ScreenManager sc;
	}
}
