using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x020000A9 RID: 169
	internal class titles : GameScreen
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x001454D8 File Offset: 0x001436D8
		public titles()
		{
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00145620 File Offset: 0x00143820
		public override void LoadContent()
		{
			this.sc = base.ScreenManager;
			if (this.content == null)
			{
				this.content = new ContentManager(base.ScreenManager.Game.Services, "Content\\astro");
			}
			GraphicsDevice graphicsDevice = base.ScreenManager.GraphicsDevice;
			this.blurEffect = this.content.Load<Effect>("shaders\\postShader");
			PresentationParameters presentationParameters = base.ScreenManager.GraphicsDevice.PresentationParameters;
			this.resolveTarget = new RenderTarget2D(this.sc.GraphicsDevice, 1280, 720, false, presentationParameters.BackBufferFormat, presentationParameters.DepthStencilFormat, presentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
			this.spriteBatch = new SpriteBatch(this.sc.GraphicsDevice);
			this.font = this.content.Load<SpriteFont>("fonts\\Opening");
			this.font2 = this.content.Load<SpriteFont>("fonts\\tag");
			this.blankTexture = this.content.Load<Texture2D>("menus\\blank");
			this.bigcorp = this.content.Load<Texture2D>("textures\\bigcorp");
			this.glower = this.content.Load<Texture2D>("sprites\\glower");
			this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(75f), this.sc.aspectratio2, 1f, 5690000f);
			this.viewMatrix = Matrix.CreateLookAt(new Vector3(0f, 0f, 70f), new Vector3(0f, 0f, 0f), Vector3.Up);
			this.logo = this.content.Load<Model>("models\\L257c");
			this.stars = this.content.Load<Model>("models\\dome3");
			this.radiotalk1 = this.content.Load<SoundEffect>("Audio\\radioTalk1");
			this.radiotalk1I = this.radiotalk1.CreateInstance();
			this.spacewind = this.content.Load<SoundEffect>("Audio\\spaceWindlow");
			this.spacewindI = this.spacewind.CreateInstance();
			this.trumpet = this.content.Load<SoundEffect>("Audio\\trumpet01");
			this.sparks = new bcSystem(this.sc.Game, this.content);
			this.sparks.Initialize();
			this.sparks.LoadContent(this.sc.GraphicsDevice);
			this.random = new Random(8);
			this.ellBone = this.logo.Bones["ell"];
			this.twoBone = this.logo.Bones["two"];
			this.fiveBone = this.logo.Bones["five"];
			this.sevenBone = this.logo.Bones["seven"];
			this.ellTransform = this.ellBone.Transform;
			this.twoTransform = this.twoBone.Transform;
			this.fiveTransform = this.fiveBone.Transform;
			this.sevenTransform = this.sevenBone.Transform;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00145937 File Offset: 0x00143B37
		public override void UnloadContent()
		{
			this.content.Unload();
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00145944 File Offset: 0x00143B44
		public override void HandleInput(InputState input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			PlayerIndex playerIndex = PlayerIndex.One;
			for (PlayerIndex playerIndex2 = PlayerIndex.One; playerIndex2 <= PlayerIndex.Four; playerIndex2++)
			{
				if (GamePad.GetState(playerIndex2).Buttons.Start == ButtonState.Pressed || GamePad.GetState(playerIndex2).Buttons.Back == ButtonState.Pressed)
				{
					playerIndex = playerIndex2;
					break;
				}
			}
			GamePadState state = GamePad.GetState(playerIndex);
			if ((this.timer > 3f && state.Buttons.Start == ButtonState.Pressed) || state.Buttons.Back == ButtonState.Pressed)
			{
				this.finished = 1;
			}
			if (this.finished == 2)
			{
				if (playerIndex == PlayerIndex.One)
				{
					base.ScreenManager.oldgamer0 = 0;
					base.ScreenManager.oldgamercount = 0;
				}
				else if (playerIndex == PlayerIndex.Two)
				{
					base.ScreenManager.oldgamer1 = 0;
					base.ScreenManager.oldgamercount = 0;
				}
				else if (playerIndex == PlayerIndex.Three)
				{
					base.ScreenManager.oldgamer2 = 0;
					base.ScreenManager.oldgamercount = 0;
				}
				else if (playerIndex == PlayerIndex.Four)
				{
					base.ScreenManager.oldgamer3 = 0;
					base.ScreenManager.oldgamercount = 0;
				}
				if (this.spacewindI.State == SoundState.Playing)
				{
					this.spacewindI.Stop();
				}
				if (this.radiotalk1I.State == SoundState.Playing)
				{
					this.radiotalk1I.Stop();
				}
				LoadingScreen2.Load(base.ScreenManager, false, null, new GameScreen[]
				{
					new Background(),
					new MainMenuScreen()
				});
			}
			this.previousgamepadstate = state;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00145AD4 File Offset: 0x00143CD4
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
			float num = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (base.IsActive)
			{
				this.timer += 0.04f * num;
				if (this.spacewindI.State == SoundState.Stopped)
				{
					this.spacewindI.Volume = 1f;
					this.spacewindI.IsLooped = true;
					this.spacewindI.Play();
				}
				else
				{
					this.spacewindI.Resume();
				}
				this.sparks.Update(gameTime);
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00145B6C File Offset: 0x00143D6C
		public void calcL257(float timeadjust)
		{
			this.x = -(float)Math.Sin((double)this.timer / 1.5);
			this.z = (float)Math.Cos((double)this.timer / 1.5);
			if (this.z < 0f)
			{
				this.moveflag = 0;
			}
			if (this.z >= 0.9999f && this.moveflag == 0)
			{
				switch ((int)this.counter)
				{
				case 1:
					this.where = new Vector3(-68f, -12f, 59f);
					this.hover = new Vector3(0f, 0f, 0f);
					this.hx = 0.0006f;
					this.hy = 0.0006f;
					this.hz = -0.0009f;
					break;
				case 2:
					this.where = new Vector3(23f, 10f, 58f);
					this.hover = new Vector3(0f, 0f, 0f);
					this.hx = 0.0004f;
					this.hy = -0.0022f;
					this.hz = -0.0003f;
					this.horntimer = 8f;
					break;
				case 3:
					this.where = new Vector3(-38f, 0f, 39f);
					this.hover = new Vector3(0f, 0f, 0f);
					this.hx = 0.0003f;
					this.hy = 0.0008f;
					this.hz = 0.0018f;
					break;
				case 4:
					this.where = new Vector3(0f, 0f, -30f);
					this.hover = new Vector3(0f, 0f, 0f);
					this.hx = 0f;
					this.hy = 0f;
					this.hz = -0.018f;
					this.fadeup2 = 0f;
					this.fadeup1 = 0f;
					this.horntimer = 0.01f * timeadjust;
					break;
				}
				this.moveflag = 1;
				this.counter += 1f;
			}
			if (this.counter > 4f)
			{
				this.lightson = true;
				this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40f), this.sc.aspectratio2, 1f, 5490000f);
			}
			if (this.horntimer > 0f)
			{
				this.horntimer -= 0.01666f * timeadjust;
			}
			if (this.horntimer < 0f)
			{
				this.horntimer = 0f;
				this.trumpet.Play(0.5f, 0f, 0f);
			}
			this.hover.X = this.hover.X + this.hx * timeadjust;
			this.hover.Y = this.hover.Y + this.hy * timeadjust;
			this.hover.Z = this.hover.Z + this.hz * timeadjust;
			if (this.lightson)
			{
				this.ramper1 += 0.00040000002f * timeadjust;
				this.ramper2 += 0.00040000002f * timeadjust;
				this.ramper3 += 0.00040000002f * timeadjust;
				this.ramper4 += 0.00040000002f * timeadjust;
				float num = MathHelper.Clamp(this.ramper1, 0f, 1f);
				float num2 = MathHelper.Clamp(this.ramper2, 0f, 1f);
				float num3 = MathHelper.Clamp(this.ramper3, 0f, 1f);
				float num4 = MathHelper.Clamp(this.ramper4, 0f, 1f);
				float num5 = MathHelper.Hermite(2.1f, 0f, 0f, 0f, num);
				float num6 = MathHelper.Hermite(1.75f, 0f, 0f, 0f, num2);
				float num7 = MathHelper.Hermite(1.375f, 0f, 0f, 0f, num3);
				float num8 = MathHelper.Hermite(1.05f, 0f, 0f, 0f, num4);
				this.ellMatrix = Matrix.CreateRotationY(num5);
				this.twoMatrix = Matrix.CreateRotationY(num6);
				this.fiveMatrix = Matrix.CreateRotationY(num7);
				this.sevenMatrix = Matrix.CreateRotationY(num8);
				if (this.ramper1 > 0f)
				{
					this.ellTransform *= Matrix.CreateTranslation(0.001f * timeadjust, 0f, 0f);
					this.twoTransform *= Matrix.CreateTranslation(0.0003f * timeadjust, 0f, 0f);
					this.fiveTransform *= Matrix.CreateTranslation(-0.0003f * timeadjust, 0f, 0f);
					this.sevenTransform *= Matrix.CreateTranslation(-0.001f * timeadjust, 0f, 0f);
				}
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00146094 File Offset: 0x00144294
		public override void Draw(GameTime gameTime)
		{
			float num = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (this.timer > this.titlecutoff)
			{
				this.sc.GraphicsDevice.SetRenderTarget(this.resolveTarget);
				base.ScreenManager.GraphicsDevice.Clear(Color.Transparent);
				this.RestoreRenderStates();
				foreach (ModelMesh modelMesh in this.logo.Meshes)
				{
					foreach (Effect effect in modelMesh.Effects)
					{
						BasicEffect basicEffect = (BasicEffect)effect;
						this.ellBone.Transform = this.ellMatrix * this.ellTransform;
						this.twoBone.Transform = this.twoMatrix * this.twoTransform;
						this.fiveBone.Transform = this.fiveMatrix * this.fiveTransform;
						this.sevenBone.Transform = this.sevenMatrix * this.sevenTransform;
						Matrix[] array = new Matrix[this.logo.Bones.Count];
						this.logo.CopyAbsoluteBoneTransformsTo(array);
						basicEffect.World = array[modelMesh.ParentBone.Index] * Matrix.CreateTranslation(this.where + this.hover);
						basicEffect.View = this.viewMatrix;
						basicEffect.Projection = this.projectionMatrix;
						basicEffect.LightingEnabled = true;
						basicEffect.DirectionalLight0.Enabled = true;
						basicEffect.DirectionalLight1.Enabled = true;
						basicEffect.DirectionalLight2.Enabled = true;
						basicEffect.PreferPerPixelLighting = true;
						basicEffect.SpecularPower = 45f;
						basicEffect.DirectionalLight2.Direction = new Vector3(-0.7f, -0.2f, 0.7f);
						basicEffect.DirectionalLight2.DiffuseColor = new Vector3((float)this.brite, (float)this.brite, (float)this.brite) / 8f;
						basicEffect.DirectionalLight2.SpecularColor = new Vector3((float)this.brite, (float)this.brite, (float)this.brite) / 8f;
						if (this.lightson)
						{
							basicEffect.AmbientLightColor = new Vector3(0.18f, 0.18f, 0.18f) * 0.6f * this.fadeup2;
							basicEffect.DirectionalLight0.Direction = new Vector3(0f, 0f, -1f);
							basicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.8f, 0.8f, 0.8f) * (this.z / 4f + 0.8f) * this.fadeup2;
							basicEffect.DirectionalLight0.SpecularColor = new Vector3(0.05f, 0.05f, 0.05f) * (this.z / 4f + 0.8f) * this.fadeup2;
							basicEffect.DirectionalLight1.Direction = new Vector3(0.2f, 1f, 0f);
							basicEffect.DirectionalLight1.DiffuseColor = new Vector3(0.6f, 0.6f, 0.6f) * (this.z / 4f + 0.8f) * this.fadeup2;
							basicEffect.DirectionalLight1.SpecularColor = new Vector3(0.15f, 0.15f, 0.15f) * this.fadeup2;
							this.fadeup2 += 0.001f * num;
							if (this.fadeup2 > 0.7f)
							{
								this.fadeup2 = 0.7f;
							}
							this.fadeup1 += 0.001f * num;
							if (this.fadeup1 > 1f)
							{
								this.fadeup1 = 1f;
							}
						}
						else
						{
							basicEffect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f) * -(this.z + 1f);
							basicEffect.DirectionalLight0.Direction = new Vector3(this.x, 0f, this.z);
							basicEffect.DirectionalLight0.DiffuseColor = new Vector3(1f, 1f, 1f);
							basicEffect.DirectionalLight0.SpecularColor = new Vector3(1.3f, 1.3f, 1.3f);
							if (this.z >= 0.97f)
							{
								basicEffect.DirectionalLight0.SpecularColor = new Vector3(0f, 0f, 0f);
							}
						}
					}
					modelMesh.Draw();
				}
				this.sc.GraphicsDevice.SetRenderTarget(null);
			}
			this.sc.GraphicsDevice.Clear(Color.Black);
			if (this.timer > this.titlecutoff && this.radiotalk1I.State == SoundState.Stopped)
			{
				this.radiotalk1I.Play();
			}
			if (base.IsActive && this.radiotalk1I.State == SoundState.Paused)
			{
				this.radiotalk1I.Resume();
			}
			if (!base.IsActive && this.radiotalk1I.State == SoundState.Playing)
			{
				this.radiotalk1I.Pause();
			}
			this.RestoreRenderStates();
			if (this.counter > 4f)
			{
				foreach (ModelMesh modelMesh2 in this.stars.Meshes)
				{
					foreach (Effect effect2 in modelMesh2.Effects)
					{
						BasicEffect basicEffect2 = (BasicEffect)effect2;
						basicEffect2.Texture = this.sc.Globalstarmap.manmadestars;
						basicEffect2.View = this.viewMatrix;
						basicEffect2.LightingEnabled = true;
						basicEffect2.Projection = this.projectionMatrix;
						basicEffect2.World = Matrix.CreateScale(30f) * Matrix.CreateRotationX(-1.95f - this.hover.Z / 400f);
						basicEffect2.AmbientLightColor = new Vector3(1f, 1f, 1f) * this.fadeup1;
						basicEffect2.DiffuseColor = new Vector3(1f, 1f, 1f) * this.fadeup1;
						basicEffect2.PreferPerPixelLighting = false;
					}
					modelMesh2.Draw();
				}
			}
			if (this.timer > this.titlecutoff)
			{
				this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(75f), this.sc.aspectratio2, 1f, 5690000f);
				this.viewMatrix = Matrix.CreateLookAt(new Vector3(0f, 0f, 70f), new Vector3(0f, 0f, 0f), Vector3.Up);
			}
			if (this.timer > this.titlecutoff)
			{
				foreach (ModelMesh modelMesh3 in this.logo.Meshes)
				{
					foreach (Effect effect3 in modelMesh3.Effects)
					{
						BasicEffect basicEffect3 = (BasicEffect)effect3;
						this.calcL257(num);
						this.ellBone.Transform = this.ellMatrix * this.ellTransform;
						this.twoBone.Transform = this.twoMatrix * this.twoTransform;
						this.fiveBone.Transform = this.fiveMatrix * this.fiveTransform;
						this.sevenBone.Transform = this.sevenMatrix * this.sevenTransform;
						Matrix[] array2 = new Matrix[this.logo.Bones.Count];
						this.logo.CopyAbsoluteBoneTransformsTo(array2);
						basicEffect3.World = array2[modelMesh3.ParentBone.Index] * Matrix.CreateTranslation(this.where + this.hover);
						basicEffect3.View = this.viewMatrix;
						basicEffect3.Projection = this.projectionMatrix;
						basicEffect3.LightingEnabled = true;
						basicEffect3.DirectionalLight0.Enabled = true;
						basicEffect3.DirectionalLight1.Enabled = true;
						basicEffect3.DirectionalLight2.Enabled = true;
						basicEffect3.PreferPerPixelLighting = true;
						basicEffect3.SpecularPower = 45f;
						basicEffect3.DirectionalLight2.Direction = new Vector3(-0.7f, -0.2f, 0.7f);
						basicEffect3.DirectionalLight2.DiffuseColor = new Vector3((float)this.brite, (float)this.brite, (float)this.brite) / 8f;
						basicEffect3.DirectionalLight2.SpecularColor = new Vector3((float)this.brite, (float)this.brite, (float)this.brite) / 8f;
						if (this.lightson)
						{
							basicEffect3.AmbientLightColor = new Vector3(0.18f, 0.18f, 0.18f) * 0.6f * this.fadeup2;
							basicEffect3.DirectionalLight0.Direction = new Vector3(0f, 0f, -1f);
							basicEffect3.DirectionalLight0.DiffuseColor = new Vector3(0.8f, 0.8f, 0.8f) * (this.z / 4f + 0.8f) * this.fadeup2;
							basicEffect3.DirectionalLight0.SpecularColor = new Vector3(0.05f, 0.05f, 0.05f) * (this.z / 4f + 0.8f) * this.fadeup2;
							basicEffect3.DirectionalLight1.Direction = new Vector3(0.2f, 1f, 0f);
							basicEffect3.DirectionalLight1.DiffuseColor = new Vector3(0.6f, 0.6f, 0.6f) * (this.z / 4f + 0.8f) * this.fadeup2;
							basicEffect3.DirectionalLight1.SpecularColor = new Vector3(0.15f, 0.15f, 0.15f) * this.fadeup2;
						}
						else
						{
							basicEffect3.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f) * -(this.z + 1f);
							basicEffect3.DirectionalLight0.Direction = new Vector3(this.x, 0f, this.z);
							basicEffect3.DirectionalLight0.DiffuseColor = new Vector3(1f, 1f, 1f);
							basicEffect3.DirectionalLight0.SpecularColor = new Vector3(1.3f, 1.3f, 1.3f);
							if (this.z >= 0.97f)
							{
								basicEffect3.DirectionalLight0.SpecularColor = new Vector3(0f, 0f, 0f);
							}
						}
					}
					modelMesh3.Draw();
				}
			}
			if (this.timer <= this.titlecutoff)
			{
				if (this.timer >= 0f && this.moveflag == 0)
				{
					this.moveflag = 1;
					this.trumpet.Play(0.5f, 0f, 0f);
				}
				this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
				float num2 = (float)this.bigcorp.Width;
				float num3 = (float)this.bigcorp.Height;
				float num4 = num2 / num3;
				float num5 = num4 / this.sc.aspectratio2;
				float num6 = 0f - ((float)this.sc.width * num5 - (float)this.sc.width) * 0.5f;
				int hite = this.sc.hite;
				float num7 = (float)this.sc.hite / num5;
				this.spriteBatch.Draw(this.bigcorp, new Rectangle((int)num6, 0, (int)((float)this.sc.width * num5), this.sc.hite), Color.White);
				this.spriteBatch.End();
				if (base.IsActive)
				{
					float num8 = 640f;
					float num9 = -360f;
					float num10 = 1343.5f;
					this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30f), this.sc.aspectratio2, 1f, 15000f);
					this.viewMatrix = Matrix.CreateLookAt(new Vector3(num8, num9 + 20f, num10), new Vector3(num8, num9, 0f), Vector3.Up);
					int num11 = this.random.Next(4, 20);
					int num12 = num11;
					float num13 = 0f;
					float num14 = 0f;
					int num15 = num11;
					float num16 = 0f;
					float num17 = 0f;
					this.partyDelay -= 1f * num;
					if (this.partyDelay <= 0f && this.finished == 0)
					{
						for (int i = 0; i < this.shakeX.Length; i += 2)
						{
							Vector3 vector = new Vector3((float)this.random.Next(-100, 100) / 60f, (float)this.random.Next(-100, 100) / 60f, (float)this.random.Next(-100, 100) / 80f);
							if (num12 == num11)
							{
								num13 = (float)this.random.Next(2, 39);
								num14 = (3900f - (float)i * 2f) / 1000f;
								num12 = 0;
							}
							num12++;
							if (i + 1 > this.shakeX.Length - 1)
							{
								break;
							}
							float num18 = (float)(this.shakeX[i] - 200);
							float num19 = (float)(-(float)(-(float)this.shakeX[i + 1] + 165));
							this.sparks.AddParticle(new Vector3(num18, -num19, 0f), vector + new Vector3((float)Math.Sin((double)num14) * num13, (float)(-(float)Math.Cos((double)num14)) * num13, ((float)Math.Sin((double)(this.timer * 4f)) + 0.2f) * 30f));
							this.sparks.AddParticle(new Vector3(num18 + 4f, -num19, 0f), vector + new Vector3((float)Math.Sin((double)num14) * num13, (float)(-(float)Math.Cos((double)num14)) * num13, ((float)Math.Sin((double)(this.timer * 4f)) + 0.2f) * 35f));
						}
						for (int j = 0; j < this.shakeX2.Length; j += 2)
						{
							if (num15 == num11)
							{
								num16 = (float)this.random.Next(2, 39);
								num17 = (-800f + (float)j * 3f) / 1000f;
								num15 = 0;
							}
							Vector3 vector2 = new Vector3((float)this.random.Next(-100, 100) / 60f, (float)this.random.Next(-100, 100) / 60f, (float)this.random.Next(-100, 100) / 80f);
							num15++;
							if (j + 1 > this.shakeX2.Length - 1)
							{
								break;
							}
							float num20 = (float)(this.shakeX2[j] - 200);
							float num21 = (float)(-(float)(-(float)this.shakeX2[j + 1] + 165));
							this.sparks.AddParticle(new Vector3(num20, -num21, 0f), vector2 + new Vector3((float)Math.Sin((double)num17) * num16, (float)(-(float)Math.Cos((double)num17)) * num16, ((float)Math.Sin((double)(this.timer * 4f)) + 0.2f) * 30f));
							this.sparks.AddParticle(new Vector3(num20 - 3f, -num21, 0f), vector2 + new Vector3((float)Math.Sin((double)num17) * num16, (float)(-(float)Math.Cos((double)num17)) * num16, ((float)Math.Sin((double)(this.timer * 4f)) + 0.2f) * 35f));
						}
						this.partyDelay = (float)this.random.Next(6, 25);
					}
				}
			}
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
			this.britecount += 1f * num;
			this.howlong -= 1f * num;
			if (this.howlong <= 0f && this.random.Next(1, 5000) < 60)
			{
				this.howlong = (float)this.random.Next(40, 70);
				this.freq = this.random.Next(15, 50);
			}
			if (this.britecount > (float)this.freq && this.howlong > 0f && this.z < 0.4f && this.timer > 8f)
			{
				this.britecount = 0f;
				this.brite = (byte)this.random.Next(50, 255);
			}
			else
			{
				this.brite = 0;
			}
			Color color = new Color((int)this.brite, (int)this.brite, (int)this.brite, 255);
			this.spriteBatch.Draw(this.glower, new Rectangle(0, 0, 1280, 720), color);
			this.spriteBatch.End();
			if (this.timer > 80f)
			{
				this.finished = 1;
			}
			if (this.finished == 1)
			{
				this.mytrans -= 4f * num;
				if (this.mytrans < 0f)
				{
					this.mytrans = 0f;
				}
				float num22 = this.mytrans / 255f;
				if (this.radiotalk1I.State == SoundState.Playing)
				{
					this.radiotalk1I.Volume = num22;
				}
				if (this.spacewindI.State == SoundState.Playing)
				{
					this.spacewindI.Volume = num22;
				}
				if (this.mytrans <= 0f)
				{
					this.finished = 2;
					this.mytrans = 0f;
					if (this.timer < 6f)
					{
						this.timer = 7f;
					}
				}
				this.sc.FadeBackBufferToBlack(255 - (int)this.mytrans);
			}
			if (this.timer <= this.titlecutoff)
			{
				this.sparks.SetCamera(this.viewMatrix, this.projectionMatrix);
				this.sparks.Draw(0);
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00147468 File Offset: 0x00145668
		private void RestoreRenderStates()
		{
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			this.sc.GraphicsDevice.BlendState = BlendState.Opaque;
			this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
			base.ScreenManager.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x001474D0 File Offset: 0x001456D0
		private void RenderStates()
		{
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			this.sc.GraphicsDevice.BlendState = BlendState.AlphaBlend;
			this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
			base.ScreenManager.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
		}

		// Token: 0x040017D4 RID: 6100
		private int[] shakeX = new int[]
		{
			526, 494, 526, 492, 526, 490, 526, 488, 526, 486,
			526, 484, 526, 482, 528, 482, 530, 482, 532, 482,
			534, 482, 536, 482, 538, 482, 540, 482, 542, 482,
			544, 482, 546, 482, 548, 482, 550, 482, 552, 482,
			554, 482, 555, 483, 556, 484, 557, 485, 557, 487,
			557, 489, 557, 491, 558, 492, 558, 494, 558, 496,
			557, 497, 556, 498, 555, 499, 574, 498, 574, 496,
			574, 494, 574, 492, 573, 491, 573, 489, 573, 487,
			571, 487, 569, 487, 569, 485, 569, 483, 568, 482,
			570, 482, 572, 482, 574, 482, 576, 482, 578, 482,
			580, 482, 582, 482, 584, 482, 586, 482, 588, 482,
			588, 484, 587, 485, 586, 486, 585, 487, 583, 487,
			581, 487, 581, 489, 581, 491, 581, 493, 581, 495,
			581, 497, 598, 493, 599, 492, 600, 491, 600, 489,
			601, 488, 601, 486, 603, 486, 604, 485, 605, 484,
			607, 483, 609, 483, 610, 482, 612, 482, 614, 482,
			616, 481, 618, 480, 620, 480, 622, 480, 624, 480,
			626, 480, 628, 480, 630, 480, 631, 481, 633, 481,
			634, 482, 636, 483, 637, 484, 637, 486, 637, 488,
			637, 490, 636, 491, 636, 493, 676, 494, 677, 492,
			678, 491, 679, 489, 680, 488, 682, 488, 683, 487,
			684, 486, 685, 485, 687, 484, 688, 483, 690, 483,
			692, 483, 693, 482, 695, 482, 697, 482, 699, 482,
			701, 482, 703, 482, 704, 483, 706, 483, 707, 484,
			708, 485, 710, 486, 711, 488, 720, 490, 721, 489,
			722, 488, 724, 486, 725, 485, 727, 483, 729, 483,
			731, 482, 733, 482, 734, 481, 736, 481, 738, 481,
			740, 481, 742, 481, 744, 482, 746, 483, 747, 484,
			749, 484, 751, 484, 752, 486, 754, 487, 755, 489,
			773, 489, 774, 488, 774, 486, 775, 484, 776, 483,
			778, 483, 780, 483, 782, 483, 784, 483, 786, 483,
			788, 483, 790, 483, 792, 483, 794, 483, 796, 483,
			798, 483, 800, 483, 801, 484, 803, 485, 803, 487,
			804, 489, 804, 491, 821, 490, 821, 488, 821, 486,
			821, 484, 822, 483, 824, 483, 827, 483, 829, 483,
			832, 483, 835, 483, 837, 483, 839, 483, 841, 483,
			843, 484, 845, 485, 846, 486, 847, 488, 848, 489,
			848, 491, 862, 490, 863, 489, 865, 488, 866, 487,
			868, 486, 869, 484, 871, 484, 872, 483, 874, 482,
			876, 482, 879, 481, 881, 481, 883, 481, 885, 481,
			886, 482, 887, 483, 888, 484, 890, 485, 892, 486,
			894, 486, 896, 486, 898, 485, 900, 485, 899, 486,
			916, 494, 916, 492, 917, 490, 918, 488, 918, 486,
			917, 485, 916, 484, 918, 484, 921, 484, 924, 484,
			926, 484, 929, 484, 932, 484, 933, 483, 936, 483,
			938, 483, 940, 483, 942, 484, 943, 486, 944, 487,
			944, 489, 944, 491, 945, 492, 970, 494, 970, 492,
			971, 491, 972, 489, 972, 486, 973, 485, 974, 484,
			975, 482, 977, 482, 979, 483, 979, 485, 980, 486,
			980, 488, 980, 490, 980, 492, 982, 492, 983, 493,
			1001, 487, 1001, 485, 1001, 483, 1003, 483, 1006, 483,
			1009, 483, 1012, 483, 1015, 483, 1020, 483, 1022, 483,
			1024, 483, 1026, 483, 1028, 484, 1030, 484, 1032, 484,
			1034, 484, 1036, 484, 1037, 483, 1039, 483, 1040, 484,
			1039, 485, 1048, 486, 1048, 484, 1049, 483, 1050, 482,
			1051, 481, 1053, 481, 1055, 481, 1057, 481, 1059, 481,
			1061, 481, 1062, 480, 1064, 480, 1066, 480, 1067, 481,
			1067, 483, 1068, 484, 1079, 492, 1080, 491, 1082, 490,
			1084, 488, 1086, 487, 1088, 486, 1090, 486, 1091, 485,
			1093, 485, 1095, 484, 1097, 483, 1099, 483, 1102, 483,
			1104, 483, 1106, 483, 1108, 483, 1109, 484, 1111, 485,
			1112, 487, 1114, 489, 1114, 491, 1116, 492, 1117, 493,
			1132, 492, 1132, 490, 1132, 488, 1132, 486, 1132, 484,
			1133, 483, 1134, 482, 1135, 481, 1137, 481, 1139, 481,
			1141, 481, 1142, 482, 1143, 483, 1144, 485, 1144, 487,
			1145, 489, 1148, 491, 1150, 494, 1151, 495, 1151, 497,
			1152, 499, 1153, 500, 1154, 501, 1155, 502, 1156, 503,
			1158, 503, 1160, 503, 1161, 502, 1162, 501, 1163, 499,
			1163, 497, 1163, 495, 1163, 493, 1163, 491, 1163, 489,
			1163, 487, 1163, 484, 1163, 482, 1164, 481, 1166, 481,
			1167, 482, 1167, 484, 1167, 486, 1166, 487, 1166, 489,
			1166, 491, 1166, 492
		};

		// Token: 0x040017D5 RID: 6101
		private int[] shakeX2 = new int[]
		{
			526, 517, 526, 519, 526, 521, 526, 523, 526, 525,
			526, 527, 526, 529, 527, 531, 529, 531, 531, 531,
			533, 531, 535, 531, 537, 531, 539, 531, 541, 531,
			543, 531, 545, 531, 547, 531, 549, 531, 551, 530,
			552, 529, 553, 528, 555, 528, 556, 527, 557, 525,
			558, 524, 558, 522, 559, 521, 560, 519, 569, 525,
			569, 527, 569, 529, 570, 530, 572, 530, 574, 530,
			576, 530, 578, 530, 580, 530, 582, 530, 583, 531,
			585, 531, 587, 531, 587, 529, 587, 527, 585, 527,
			601, 522, 602, 524, 604, 526, 605, 527, 606, 528,
			608, 528, 609, 529, 610, 530, 611, 531, 612, 532,
			614, 532, 616, 532, 618, 532, 620, 532, 622, 532,
			624, 532, 626, 532, 627, 531, 629, 530, 631, 530,
			633, 530, 634, 529, 636, 529, 637, 528, 637, 526,
			637, 524, 637, 522, 675, 519, 676, 520, 677, 521,
			679, 523, 680, 524, 681, 526, 682, 527, 683, 528,
			685, 528, 687, 529, 688, 530, 689, 531, 691, 531,
			692, 532, 694, 532, 696, 532, 698, 532, 700, 532,
			702, 532, 703, 531, 705, 530, 706, 529, 708, 528,
			709, 527, 710, 525, 710, 523, 720, 520, 721, 521,
			722, 522, 722, 524, 724, 524, 725, 525, 727, 525,
			728, 526, 728, 528, 729, 529, 730, 530, 732, 530,
			733, 531, 735, 531, 736, 532, 738, 533, 739, 532,
			741, 532, 744, 531, 745, 530, 747, 530, 749, 530,
			750, 529, 752, 529, 753, 528, 754, 527, 755, 526,
			756, 525, 757, 524, 757, 522, 758, 521, 774, 522,
			774, 524, 774, 526, 774, 528, 775, 529, 777, 529,
			778, 530, 780, 529, 780, 527, 780, 525, 779, 524,
			778, 523, 778, 521, 778, 519, 778, 517, 779, 516,
			794, 521, 795, 523, 796, 525, 797, 527, 798, 528,
			799, 529, 800, 530, 802, 530, 804, 530, 806, 530,
			808, 530, 808, 528, 807, 527, 806, 526, 805, 525,
			804, 523, 821, 523, 822, 524, 823, 526, 824, 528,
			823, 529, 821, 529, 821, 531, 823, 531, 825, 532,
			826, 531, 826, 529, 826, 527, 827, 526, 827, 524,
			861, 520, 862, 521, 863, 522, 863, 524, 865, 524,
			865, 526, 866, 527, 867, 528, 869, 528, 870, 529,
			871, 530, 873, 530, 875, 530, 877, 530, 879, 531,
			881, 531, 883, 531, 884, 530, 886, 529, 888, 529,
			890, 528, 892, 528, 893, 527, 894, 526, 895, 525,
			896, 524, 897, 522, 915, 521, 916, 523, 916, 525,
			916, 527, 916, 529, 916, 531, 918, 531, 920, 531,
			921, 530, 921, 528, 921, 526, 921, 524, 921, 522,
			934, 521, 935, 523, 936, 524, 937, 526, 938, 527,
			940, 527, 941, 528, 943, 528, 944, 529, 945, 530,
			947, 530, 949, 531, 951, 531, 951, 529, 949, 529,
			948, 528, 947, 527, 947, 525, 945, 525, 945, 523,
			959, 523, 958, 524, 957, 525, 957, 527, 956, 528,
			956, 530, 958, 530, 960, 530, 961, 529, 962, 528,
			963, 527, 964, 526, 964, 524, 965, 523, 966, 521,
			967, 520, 968, 519, 969, 518, 971, 518, 973, 518,
			975, 518, 977, 518, 986, 523, 987, 524, 988, 525,
			988, 527, 989, 528, 990, 529, 991, 530, 993, 530,
			995, 531, 997, 531, 998, 529, 998, 527, 997, 526,
			996, 525, 996, 523, 995, 522, 994, 521, 1017, 521,
			1017, 523, 1016, 524, 1016, 526, 1016, 528, 1017, 529,
			1018, 530, 1020, 530, 1022, 530, 1023, 529, 1023, 527,
			1024, 526, 1024, 524, 1024, 522, 1024, 520, 1054, 524,
			1053, 525, 1052, 526, 1051, 525, 1049, 525, 1047, 525,
			1047, 527, 1047, 529, 1048, 530, 1050, 531, 1052, 531,
			1054, 531, 1056, 531, 1058, 530, 1060, 530, 1062, 530,
			1064, 530, 1066, 530, 1066, 528, 1067, 527, 1080, 522,
			1081, 523, 1082, 524, 1084, 526, 1086, 527, 1088, 527,
			1089, 528, 1089, 530, 1091, 531, 1093, 532, 1095, 532,
			1097, 532, 1099, 532, 1101, 532, 1102, 531, 1104, 531,
			1105, 530, 1107, 530, 1108, 529, 1110, 529, 1112, 529,
			1113, 528, 1114, 527, 1115, 526, 1116, 524, 1117, 523,
			1117, 521, 1132, 522, 1131, 523, 1130, 525, 1130, 527,
			1131, 529, 1132, 530, 1133, 531, 1135, 531, 1137, 531,
			1137, 529, 1138, 528, 1139, 527, 1139, 525, 1139, 523,
			1139, 521, 1139, 519, 1139, 517, 1153, 519, 1154, 520,
			1155, 522, 1156, 523, 1156, 525, 1157, 526, 1158, 527,
			1159, 528, 1159, 530, 1161, 530, 1163, 529, 1165, 529,
			1165, 527, 1165, 525, 1165, 523, 1166, 522, 1166, 520,
			1167, 519, 1168, 518, 1168, 517
		};

		// Token: 0x040017D6 RID: 6102
		private float partyDelay;

		// Token: 0x040017D7 RID: 6103
		private float x;

		// Token: 0x040017D8 RID: 6104
		private float z;

		// Token: 0x040017D9 RID: 6105
		private Effect blurEffect;

		// Token: 0x040017DA RID: 6106
		private RenderTarget2D resolveTarget;

		// Token: 0x040017DB RID: 6107
		private int doneonce;

		// Token: 0x040017DC RID: 6108
		private ContentManager content;

		// Token: 0x040017DD RID: 6109
		private SpriteBatch spriteBatch;

		// Token: 0x040017DE RID: 6110
		private SpriteFont font;

		// Token: 0x040017DF RID: 6111
		private SpriteFont font2;

		// Token: 0x040017E0 RID: 6112
		private Texture2D blankTexture;

		// Token: 0x040017E1 RID: 6113
		private Model logo;

		// Token: 0x040017E2 RID: 6114
		private Model stars;

		// Token: 0x040017E3 RID: 6115
		private float fadeup2;

		// Token: 0x040017E4 RID: 6116
		private float fadeup1;

		// Token: 0x040017E5 RID: 6117
		public float timer;

		// Token: 0x040017E6 RID: 6118
		private float titlecutoff = 8f;

		// Token: 0x040017E7 RID: 6119
		public int horn;

		// Token: 0x040017E8 RID: 6120
		private float horntimer;

		// Token: 0x040017E9 RID: 6121
		private float mytrans = 255f;

		// Token: 0x040017EA RID: 6122
		private bool lightson;

		// Token: 0x040017EB RID: 6123
		private float counter = 1f;

		// Token: 0x040017EC RID: 6124
		private int moveflag;

		// Token: 0x040017ED RID: 6125
		private int finished;

		// Token: 0x040017EE RID: 6126
		private Vector3 where = new Vector3(62f, 10f, 55f);

		// Token: 0x040017EF RID: 6127
		private Vector3 hover = new Vector3(0f, 0f, 0f);

		// Token: 0x040017F0 RID: 6128
		private float hx = 0.0009f;

		// Token: 0x040017F1 RID: 6129
		private float hy = -0.001f;

		// Token: 0x040017F2 RID: 6130
		private float hz = -0.0003f;

		// Token: 0x040017F3 RID: 6131
		private Matrix projectionMatrix;

		// Token: 0x040017F4 RID: 6132
		private Matrix viewMatrix;

		// Token: 0x040017F5 RID: 6133
		private float britecount;

		// Token: 0x040017F6 RID: 6134
		private float howlong;

		// Token: 0x040017F7 RID: 6135
		private int freq = 3;

		// Token: 0x040017F8 RID: 6136
		private byte brite;

		// Token: 0x040017F9 RID: 6137
		private GamePadState previousgamepadstate;

		// Token: 0x040017FA RID: 6138
		private Random random;

		// Token: 0x040017FB RID: 6139
		private Texture2D bigcorp;

		// Token: 0x040017FC RID: 6140
		private Texture2D glower;

		// Token: 0x040017FD RID: 6141
		private SoundEffect radiotalk1;

		// Token: 0x040017FE RID: 6142
		private SoundEffect spacewind;

		// Token: 0x040017FF RID: 6143
		private SoundEffectInstance radiotalk1I;

		// Token: 0x04001800 RID: 6144
		private SoundEffectInstance spacewindI;

		// Token: 0x04001801 RID: 6145
		private SoundEffect trumpet;

		// Token: 0x04001802 RID: 6146
		private ParticleSystem sparks;

		// Token: 0x04001803 RID: 6147
		private Matrix ellMatrix = Matrix.Identity;

		// Token: 0x04001804 RID: 6148
		private Matrix twoMatrix = Matrix.Identity;

		// Token: 0x04001805 RID: 6149
		private Matrix fiveMatrix = Matrix.Identity;

		// Token: 0x04001806 RID: 6150
		private Matrix sevenMatrix = Matrix.Identity;

		// Token: 0x04001807 RID: 6151
		private ModelBone ellBone;

		// Token: 0x04001808 RID: 6152
		private ModelBone twoBone;

		// Token: 0x04001809 RID: 6153
		private ModelBone fiveBone;

		// Token: 0x0400180A RID: 6154
		private ModelBone sevenBone;

		// Token: 0x0400180B RID: 6155
		private Matrix ellTransform;

		// Token: 0x0400180C RID: 6156
		private Matrix twoTransform;

		// Token: 0x0400180D RID: 6157
		private Matrix fiveTransform;

		// Token: 0x0400180E RID: 6158
		private Matrix sevenTransform;

		// Token: 0x0400180F RID: 6159
		private float ramper1 = -0.8f;

		// Token: 0x04001810 RID: 6160
		private float ramper2 = -0.8f;

		// Token: 0x04001811 RID: 6161
		private float ramper3 = -0.8f;

		// Token: 0x04001812 RID: 6162
		private float ramper4 = -0.8f;

		// Token: 0x04001813 RID: 6163
		private ScreenManager sc;
	}
}
