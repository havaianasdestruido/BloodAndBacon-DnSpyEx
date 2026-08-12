using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x0200001B RID: 27
	internal class LanderScreen : MenuScreen
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600013E RID: 318 RVA: 0x0002D444 File Offset: 0x0002B644
		// (remove) Token: 0x0600013F RID: 319 RVA: 0x0002D47C File Offset: 0x0002B67C
		public event EventHandler<PlayerIndexEventArgs> Accepted;

		// Token: 0x06000140 RID: 320 RVA: 0x0002D4B4 File Offset: 0x0002B6B4
		public LanderScreen(int pick, ScreenManager sc)
			: base("")
		{
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
			base.ScreenManager = sc;
			this.defined = new string[this.lander.Length];
			if (pick == 1)
			{
				Array.Copy(this.lander, this.defined, this.lander.Length);
				this.header = this.header1[pick];
				this.diffuse = this.diffuse1;
			}
			if (pick == 2)
			{
				Array.Copy(this.rover, this.defined, this.lander.Length);
				this.header = this.header1[pick];
				this.diffuse = this.diffuse2;
			}
			if (pick == 3)
			{
				Array.Copy(this.walk, this.defined, this.lander.Length);
				this.header = this.header1[pick];
				this.diffuse = this.diffuse3;
			}
			this.pick = pick;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0002DE10 File Offset: 0x0002C010
		public override void LoadContent()
		{
			if (this.content == null)
			{
				this.content = new ContentManager(base.ScreenManager.Game.Services, "Content");
			}
			this.random = new Random();
			this.sc = base.ScreenManager;
			this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30f), this.sc.aspectratio2, 1f, 490000f);
			this.viewMatrix = Matrix.CreateLookAt(this.campos, this.camlookpos, Vector3.Up);
			this.basiceffect = new BasicEffect(this.sc.GraphicsDevice)
			{
				TextureEnabled = true,
				VertexColorEnabled = true
			};
			this.controller = this.sc.controllerX;
			this.dpadBone = this.controller.Bones["DPAD"];
			this.dpadTransform = this.dpadBone.Transform;
			this.rstickBone = this.controller.Bones["RSTICK"];
			this.rstickTransform = this.rstickBone.Transform;
			this.lstickBone = this.controller.Bones["LSTICK"];
			this.lstickTransform = this.lstickBone.Transform;
			this.ltBone = this.controller.Bones["LT"];
			this.ltTransform = this.ltBone.Transform;
			this.rtBone = this.controller.Bones["RT"];
			this.rtTransform = this.rtBone.Transform;
			this.lbBone = this.controller.Bones["LB"];
			this.lbTransform = this.lbBone.Transform;
			this.rbBone = this.controller.Bones["RB"];
			this.rbTransform = this.rbBone.Transform;
			this.aBone = this.controller.Bones["A"];
			this.bBone = this.controller.Bones["B"];
			this.xBone = this.controller.Bones["X"];
			this.yBone = this.controller.Bones["Y"];
			this.aTransform = this.aBone.Transform;
			this.bTransform = this.bBone.Transform;
			this.xTransform = this.xBone.Transform;
			this.yTransform = this.yBone.Transform;
			this.effect = new BasicEffect(base.ScreenManager.GraphicsDevice);
			this.spriteBatch = new SpriteBatch(base.ScreenManager.GraphicsDevice);
			this.world = Matrix.Identity;
			this.tracerx = new lineSystem(this.sc.Game, this.content);
			this.tracerx.Initialize();
			this.tracerx.LoadContent(this.sc.GraphicsDevice);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0002E131 File Offset: 0x0002C331
		public override void UnloadContent()
		{
			this.content.Unload();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0002E140 File Offset: 0x0002C340
		public override void HandleInput(InputState input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			int value = (int)base.ControllingPlayer.Value;
			GamePadState gamePadState = input.CurrentGamePadStates[value];
			if (gamePadState.Buttons.Back == ButtonState.Pressed || input.IsMenuCancel(base.ControllingPlayer, out this.sc.playerindex))
			{
				this.fadeflag = 3;
			}
			if (this.fadeflag == 3)
			{
				if (this.Accepted != null)
				{
					this.Accepted(this, new PlayerIndexEventArgs(this.sc.playerindex));
				}
				base.ScreenManager.menuflag = 0;
				base.ExitScreen();
			}
			this.btimer += 0.02f;
			if (this.btimer > 6f)
			{
				this.automove = 1;
			}
			if (this.automove == 1)
			{
				this.camradian -= 0.01f;
			}
			if (this.previousgamepadstate != gamePadState)
			{
				this.automove = 0;
				this.btimer = 0f;
			}
			this.camradius -= gamePadState.ThumbSticks.Left.Y * 50f;
			if (this.camradius < 1000f)
			{
				this.camradius = 1000f;
			}
			if (this.camradius > 5000f)
			{
				this.camradius = 5000f;
			}
			this.camradian -= gamePadState.ThumbSticks.Right.X / 16f;
			this.camheight -= gamePadState.ThumbSticks.Right.Y / 25f;
			if (this.camheight > 4f)
			{
				this.camheight = 4f;
			}
			if (this.camheight < 2f)
			{
				this.camheight = 2f;
			}
			if ((double)this.camradian > 6.28)
			{
				this.camradian = 0f;
			}
			if (this.camradian < 0f)
			{
				this.camradian = 6.28f;
			}
			this.campos.X = (float)(-(float)Math.Cos((double)this.camheight)) * (float)Math.Sin((double)this.camradian) * this.camradius;
			this.campos.Z = (float)(-(float)Math.Cos((double)this.camheight)) * (float)(-(float)Math.Cos((double)this.camradian)) * this.camradius;
			this.campos.Y = (float)Math.Sin((double)this.camheight) * this.camradius;
			if (gamePadState.DPad.Right == ButtonState.Released && gamePadState.DPad.Left == ButtonState.Released)
			{
				this.zrot = Matrix.CreateRotationZ(0f);
				this.dpadincz = 0f;
			}
			if (gamePadState.DPad.Up == ButtonState.Released && gamePadState.DPad.Down == ButtonState.Released)
			{
				this.dpadincx = 0f;
				this.xrot = Matrix.CreateRotationX(0f);
			}
			if (gamePadState.DPad.Right == ButtonState.Pressed)
			{
				this.dpadincz -= 0.06f;
				this.zrot = Matrix.CreateRotationZ(this.dpadincz);
			}
			if (gamePadState.DPad.Left == ButtonState.Pressed)
			{
				this.dpadincz += 0.06f;
				this.zrot = Matrix.CreateRotationZ(this.dpadincz);
			}
			if (gamePadState.DPad.Up == ButtonState.Pressed)
			{
				this.dpadincx -= 0.06f;
				this.xrot = Matrix.CreateRotationX(this.dpadincx);
			}
			if (gamePadState.DPad.Down == ButtonState.Pressed)
			{
				this.dpadincx += 0.06f;
				this.xrot = Matrix.CreateRotationX(this.dpadincx);
			}
			if ((double)this.dpadincz < -0.11)
			{
				this.dpadincz = -0.11f;
			}
			if ((double)this.dpadincz > 0.11)
			{
				this.dpadincz = 0.11f;
			}
			if ((double)this.dpadincx < -0.11)
			{
				this.dpadincx = -0.11f;
			}
			if ((double)this.dpadincx > 0.11)
			{
				this.dpadincx = 0.11f;
			}
			this.dpadMatrix = this.xrot * this.zrot;
			this.rstickMatrix = Matrix.CreateRotationX(-gamePadState.ThumbSticks.Right.Y / 3f) * Matrix.CreateRotationZ(-gamePadState.ThumbSticks.Right.X / 3f);
			this.rightclick = false;
			this.leftclick = false;
			if (gamePadState.Buttons.RightStick == ButtonState.Pressed)
			{
				this.rightclick = true;
				this.rstickMatrix *= Matrix.CreateTranslation(0f, -5f, 0f);
			}
			else
			{
				this.rstickMatrix *= Matrix.CreateTranslation(0f, 0f, 0f);
			}
			this.lstickMatrix = Matrix.CreateRotationX(-gamePadState.ThumbSticks.Left.Y / 3f) * Matrix.CreateRotationZ(-gamePadState.ThumbSticks.Left.X / 3f);
			if (gamePadState.Buttons.LeftStick == ButtonState.Pressed)
			{
				this.leftclick = true;
				this.lstickMatrix *= Matrix.CreateTranslation(0f, -5f, 0f);
			}
			else
			{
				this.lstickMatrix *= Matrix.CreateTranslation(0f, 0f, 0f);
			}
			this.previousgamepadstate = gamePadState;
			this.rbumperClick = false;
			this.lbumperClick = false;
			this.rtMatrix = Matrix.CreateRotationX(-gamePadState.Triggers.Right / 3.2f);
			this.ltMatrix = Matrix.CreateRotationX(-gamePadState.Triggers.Left / 3.2f);
			if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed)
			{
				this.rbMatrix = Matrix.CreateTranslation(0f, 0f, 8f);
				this.rbumperClick = true;
			}
			else
			{
				this.rbMatrix = Matrix.CreateTranslation(0f, 0f, 0f);
			}
			if (gamePadState.Buttons.LeftShoulder == ButtonState.Pressed)
			{
				this.lbMatrix = Matrix.CreateTranslation(0f, 0f, 8f);
				this.lbumperClick = true;
			}
			else
			{
				this.lbMatrix = Matrix.CreateTranslation(0f, 0f, 0f);
			}
			this.aMatrix = Matrix.CreateTranslation(0f, 0f, 0f);
			this.buttonflagA = 0;
			this.buttonflagB = 0;
			this.buttonflagX = 0;
			this.buttonflagY = 0;
			if (gamePadState.Buttons.A == ButtonState.Pressed)
			{
				this.aMatrix = Matrix.CreateTranslation(0f, -6f, 0f);
				this.buttonflagA = 1;
			}
			this.bMatrix = Matrix.CreateTranslation(0f, 0f, 0f);
			if (gamePadState.Buttons.B == ButtonState.Pressed)
			{
				this.bMatrix = Matrix.CreateTranslation(0f, -6f, 0f);
				this.buttonflagB = 1;
			}
			this.xMatrix = Matrix.CreateTranslation(0f, 0f, 0f);
			if (gamePadState.Buttons.X == ButtonState.Pressed)
			{
				this.xMatrix = Matrix.CreateTranslation(0f, -6f, 0f);
				this.buttonflagX = 1;
			}
			this.yMatrix = Matrix.CreateTranslation(0f, 0f, 0f);
			if (gamePadState.Buttons.Y == ButtonState.Pressed)
			{
				this.yMatrix = Matrix.CreateTranslation(0f, -6f, 0f);
				this.buttonflagY = 1;
			}
			this.previousgamepadstate = gamePadState;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0002E980 File Offset: 0x0002CB80
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			this.mytimer += 0.06f;
			if (this.drawonce == 0)
			{
				this.drawonce = 1;
				int num = 0;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.leftstick0, this.leftstick1);
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.rightstick0, this.rightstick1);
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.xbutton0, this.xbutton1);
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.bbutton0, this.bbutton1);
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.abutton0, this.abutton1);
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.ybutton0, this.ybutton1);
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.rtrigger0, new Vector3(this.rtrigger1.X, this.rtrigger1.Y + 50f, this.rtrigger1.Z));
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.rbumper0, new Vector3(this.rbumper1.X, this.rbumper1.Y + 50f, this.rbumper1.Z));
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.ltrigger0, new Vector3(this.ltrigger1.X, this.ltrigger1.Y + 50f, this.ltrigger1.Z));
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.lbumper0, new Vector3(this.lbumper1.X, this.lbumper1.Y + 50f, this.lbumper1.Z));
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.dpadLR0, this.dpadLR1);
				}
				num++;
				if (this.defined[num] != "Unknown" && this.defined[num] != "NA")
				{
					this.tracerZone(this.dpadUD0, this.dpadUD1);
				}
				num++;
				this.tracerx.Update(gameTime);
			}
			this.viewMatrix = Matrix.CreateLookAt(this.campos, this.camlookpos, Vector3.Up);
			base.Update(gameTime, otherScreenHasFocus, false);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0002ED64 File Offset: 0x0002CF64
		public override void Draw(GameTime gameTime)
		{
			this.RenderStates();
			this.DrawModel(this.controller);
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
			this.tracerx.SetCamera(this.viewMatrix, this.projectionMatrix);
			this.tracerx.Draw(0);
			this.RenderStates();
			if (this.pick == 1)
			{
				this.defined[0] = this.lander[0];
				if (this.leftclick)
				{
					this.defined[0] = this.lander2[0];
				}
				this.defined[1] = this.lander[1];
				if (this.rightclick)
				{
					this.defined[1] = this.lander2[1];
				}
				this.defined[2] = this.lander[2];
				if (this.buttonflagX == 1)
				{
					this.defined[2] = this.lander2[2];
				}
				this.defined[3] = this.lander[3];
				if (this.buttonflagB == 1)
				{
					this.defined[3] = this.lander2[3];
				}
				this.defined[4] = this.lander[4];
				if (this.buttonflagA == 1)
				{
					this.defined[4] = this.lander2[4];
				}
				this.defined[5] = this.lander[5];
				if (this.buttonflagY == 1)
				{
					this.defined[5] = this.lander2[5];
				}
			}
			if (this.pick == 2)
			{
				this.defined[0] = this.rover[0];
				if (this.leftclick)
				{
					this.defined[0] = this.rover2[0];
				}
				this.defined[1] = this.rover[1];
				if (this.rightclick)
				{
					this.defined[1] = this.rover2[1];
				}
				this.defined[9] = this.rover[9];
				if (this.lbumperClick)
				{
					this.defined[9] = this.rover2[9];
				}
				this.defined[7] = this.rover[7];
				if (this.rbumperClick)
				{
					this.defined[7] = this.rover2[7];
				}
				this.defined[2] = this.rover[2];
				if (this.buttonflagX == 1)
				{
					this.defined[2] = this.rover2[2];
				}
				this.defined[3] = this.rover[3];
				if (this.buttonflagB == 1)
				{
					this.defined[3] = this.rover2[3];
				}
				this.defined[4] = this.rover[4];
				if (this.buttonflagA == 1)
				{
					this.defined[4] = this.rover2[4];
				}
				this.defined[5] = this.rover[5];
				if (this.buttonflagY == 1)
				{
					this.defined[5] = this.rover2[5];
				}
			}
			if (this.pick == 3)
			{
				this.defined[0] = this.walk[0];
				if (this.leftclick)
				{
					this.defined[0] = this.walk2[0];
				}
				this.defined[1] = this.walk[1];
				if (this.rightclick)
				{
					this.defined[1] = this.walk2[1];
				}
				this.defined[2] = this.walk[2];
				if (this.buttonflagX == 1)
				{
					this.defined[2] = this.walk2[2];
				}
				this.defined[3] = this.walk[3];
				if (this.buttonflagB == 1)
				{
					this.defined[3] = this.walk2[3];
				}
				this.defined[4] = this.walk[4];
				if (this.buttonflagA == 1)
				{
					this.defined[4] = this.walk2[4];
				}
				this.defined[5] = this.walk[5];
				if (this.buttonflagY == 1)
				{
					this.defined[5] = this.walk2[5];
				}
			}
			this.DrawQuad(this.defined[0], this.leftstick1);
			this.DrawQuad(this.defined[1], this.rightstick1);
			this.DrawQuad(this.defined[2], this.xbutton1);
			this.DrawQuad(this.defined[3], this.bbutton1);
			this.DrawQuad(this.defined[4], this.abutton1);
			this.DrawQuad(this.defined[5], this.ybutton1);
			this.DrawQuad(this.defined[6], this.rtrigger1);
			this.DrawQuad(this.defined[7], this.rbumper1);
			this.DrawQuad(this.defined[8], this.ltrigger1);
			this.DrawQuad(this.defined[9], this.lbumper1);
			this.DrawQuad(this.defined[10], this.dpadUD1);
			this.DrawQuad(this.defined[11], this.dpadLR1);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, this.sc.ScaleMatrix1);
			this.spriteBatch.DrawString(this.sc.landerfont, this.titlehead[this.pick], Vector2.Zero, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			this.spriteBatch.End();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0002F278 File Offset: 0x0002D478
		private void DrawModel(Model model)
		{
			this.dpadBone.Transform = this.dpadMatrix * this.dpadTransform;
			this.rstickBone.Transform = this.rstickMatrix * this.rstickTransform;
			this.lstickBone.Transform = this.lstickMatrix * this.lstickTransform;
			this.rtBone.Transform = this.rtMatrix * this.rtTransform;
			this.ltBone.Transform = this.ltMatrix * this.ltTransform;
			this.rbBone.Transform = this.rbMatrix * this.rbTransform;
			this.lbBone.Transform = this.lbMatrix * this.lbTransform;
			this.bBone.Transform = this.bMatrix * this.bTransform;
			this.aBone.Transform = this.aMatrix * this.aTransform;
			this.xBone.Transform = this.xMatrix * this.xTransform;
			this.yBone.Transform = this.yMatrix * this.yTransform;
			Matrix[] array = new Matrix[model.Bones.Count];
			model.CopyAbsoluteBoneTransformsTo(array);
			Matrix matrix = Matrix.CreateRotationX(0.2f) * Matrix.CreateTranslation(this.controllerpos);
			foreach (ModelMesh modelMesh in model.Meshes)
			{
				int num = 1;
				if (modelMesh.Name == "A" && this.buttonflagA == 1)
				{
					num = 0;
				}
				if (modelMesh.Name == "B" && this.buttonflagB == 1)
				{
					num = 0;
				}
				if (modelMesh.Name == "X" && this.buttonflagX == 1)
				{
					num = 0;
				}
				if (modelMesh.Name == "Y" && this.buttonflagY == 1)
				{
					num = 0;
				}
				foreach (Effect effect in modelMesh.Effects)
				{
					BasicEffect basicEffect = (BasicEffect)effect;
					basicEffect.World = array[modelMesh.ParentBone.Index] * matrix;
					basicEffect.View = this.viewMatrix;
					basicEffect.Projection = this.projectionMatrix;
					basicEffect.LightingEnabled = true;
					basicEffect.DirectionalLight0.Enabled = true;
					basicEffect.DirectionalLight1.Enabled = true;
					basicEffect.DirectionalLight2.Enabled = true;
					basicEffect.DirectionalLight0.Direction = new Vector3(0.3f, -0.7f, (float)Math.Sin((double)(this.mytimer / 8f)) / 1.2f);
					basicEffect.AmbientLightColor = new Vector3(0.24f, 0.24f, 0.24f);
					basicEffect.DirectionalLight0.DiffuseColor = this.diffuse * 0.8f;
					basicEffect.DirectionalLight0.SpecularColor = new Vector3(0.51f, 0.51f, 0.51f);
					basicEffect.DirectionalLight1.Direction = new Vector3((float)Math.Sin((double)(this.mytimer / 4f)) / 1.2f, -0.7f, 0.2f);
					basicEffect.DirectionalLight1.DiffuseColor = this.diffuse * 0.4f;
					basicEffect.DirectionalLight1.SpecularColor = new Vector3(0.45f, 0.45f, 0.45f);
					basicEffect.DirectionalLight2.Direction = new Vector3(0f, 0.7f, 0.2f);
					basicEffect.DirectionalLight2.DiffuseColor = new Vector3(0.1f, 0.1f, 0.1f);
					basicEffect.DirectionalLight2.SpecularColor = new Vector3(0.15f, 0.15f, 0.15f);
					basicEffect.SpecularPower = 35f;
					if (num == 0)
					{
						basicEffect.SpecularPower = 1f;
					}
					basicEffect.PreferPerPixelLighting = true;
				}
				modelMesh.Draw();
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0002F6F8 File Offset: 0x0002D8F8
		private void DrawQuad(string ss, Vector3 pos)
		{
			if (ss == "Unknown" || ss == "NA")
			{
				return;
			}
			ss = ss.ToLower();
			this.basiceffect.View = this.viewMatrix;
			this.basiceffect.Projection = this.projectionMatrix;
			Vector2 vector = this.sc.landerfont.MeasureString(ss) / 2f;
			this.basiceffect.World = Matrix.CreateScale(1f, -1f, 1f) * Matrix.CreateRotationY(-this.camradian + 3.14f) * Matrix.CreateTranslation(pos.X, pos.Y + 10f, pos.Z);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, DepthStencilState.Default, RasterizerState.CullNone, this.basiceffect);
			this.spriteBatch.DrawString(this.sc.landerfont, ss, Vector2.Zero, Color.White, 0f, vector, 1f, SpriteEffects.None, 0f);
			this.spriteBatch.End();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0002F820 File Offset: 0x0002DA20
		private void DrawText(string text, RenderTarget2D target)
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0002F824 File Offset: 0x0002DA24
		private void DrawText2(string text, ref Texture2D tex)
		{
			RenderTarget2D renderTarget2D = new RenderTarget2D(this.sc.GraphicsDevice, 600, 200, true, SurfaceFormat.Color, DepthFormat.None);
			this.sc.GraphicsDevice.SetRenderTarget(renderTarget2D);
			this.sc.GraphicsDevice.Clear(Color.Black);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			this.spriteBatch.DrawString(this.tag, text, new Vector2((float)(300 - text.Length * 5) + 1.5f, 71.5f), Color.Black);
			this.spriteBatch.DrawString(this.tag, text, new Vector2((float)(300 - text.Length * 5) - 1.5f, 71.5f), Color.Black);
			this.spriteBatch.DrawString(this.tag, text, new Vector2((float)(300 - text.Length * 5), 71.5f), Color.Black);
			this.spriteBatch.DrawString(this.tag, text, new Vector2((float)(300 - text.Length * 5), 70f), Color.LightGray);
			this.spriteBatch.End();
			tex = renderTarget2D;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0002F960 File Offset: 0x0002DB60
		private void Header()
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0002F964 File Offset: 0x0002DB64
		private void tracerZone(Vector3 pos1, Vector3 pos2)
		{
			float num = Vector3.Distance(pos1, pos2);
			Vector3 vector = Vector3.Normalize(pos2 - pos1);
			int num2 = 0;
			while ((float)num2 < num)
			{
				this.tracerx.AddParticle(pos1 + (float)num2 * vector, new Vector3(0f, 0f, 0f));
				num2 += 4;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0002F9C0 File Offset: 0x0002DBC0
		private void RestoreRenderStates()
		{
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0002F9C4 File Offset: 0x0002DBC4
		private void RenderStates()
		{
			base.ScreenManager.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		}

		// Token: 0x0400060D RID: 1549
		private BasicEffect basiceffect;

		// Token: 0x0400060E RID: 1550
		private Effect writeEffect;

		// Token: 0x0400060F RID: 1551
		private bool leftclick;

		// Token: 0x04000610 RID: 1552
		private bool rightclick;

		// Token: 0x04000611 RID: 1553
		private bool lbumperClick;

		// Token: 0x04000612 RID: 1554
		private bool rbumperClick;

		// Token: 0x04000613 RID: 1555
		private Texture2D blankTexture;

		// Token: 0x04000614 RID: 1556
		private Texture2D back;

		// Token: 0x04000615 RID: 1557
		private Texture2D thumb;

		// Token: 0x04000616 RID: 1558
		private float mytimer;

		// Token: 0x04000617 RID: 1559
		private float btimer;

		// Token: 0x04000618 RID: 1560
		private int automove;

		// Token: 0x04000619 RID: 1561
		private Vector3 controllerpos = new Vector3(0f, 0f, 0f);

		// Token: 0x0400061A RID: 1562
		private int fader = 255;

		// Token: 0x0400061B RID: 1563
		private int fadeflag = 1;

		// Token: 0x0400061C RID: 1564
		private ContentManager content;

		// Token: 0x0400061D RID: 1565
		private Model controller;

		// Token: 0x0400061E RID: 1566
		private Matrix projectionMatrix;

		// Token: 0x0400061F RID: 1567
		private Matrix viewMatrix;

		// Token: 0x04000620 RID: 1568
		private float aspectRatio;

		// Token: 0x04000621 RID: 1569
		private Matrix axisMatrix = Matrix.Identity;

		// Token: 0x04000622 RID: 1570
		private Matrix spinMatrix = Matrix.Identity;

		// Token: 0x04000623 RID: 1571
		private Random random;

		// Token: 0x04000624 RID: 1572
		private Vector3 lastpos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000625 RID: 1573
		private Vector3 tracepos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000626 RID: 1574
		private Vector3 followpos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000627 RID: 1575
		private Vector3 lastfollowpos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000628 RID: 1576
		private GamePadState previousgamepadstate;

		// Token: 0x04000629 RID: 1577
		private Vector3 camlookpos = new Vector3(0f, 0f, -100f);

		// Token: 0x0400062A RID: 1578
		private Vector3 campos = Vector3.Zero;

		// Token: 0x0400062B RID: 1579
		private float camradius = 2900f;

		// Token: 0x0400062C RID: 1580
		private float camradian = 2.8f;

		// Token: 0x0400062D RID: 1581
		private float camheight = 2.8f;

		// Token: 0x0400062E RID: 1582
		private Matrix xrot = Matrix.CreateRotationX(0f);

		// Token: 0x0400062F RID: 1583
		private Matrix zrot = Matrix.CreateRotationX(0f);

		// Token: 0x04000630 RID: 1584
		private float dpadincx;

		// Token: 0x04000631 RID: 1585
		private float dpadincz;

		// Token: 0x04000632 RID: 1586
		private ModelBone dpadBone;

		// Token: 0x04000633 RID: 1587
		private Matrix dpadTransform;

		// Token: 0x04000634 RID: 1588
		private Matrix dpadMatrix = Matrix.Identity;

		// Token: 0x04000635 RID: 1589
		private ModelBone rstickBone;

		// Token: 0x04000636 RID: 1590
		private Matrix rstickTransform;

		// Token: 0x04000637 RID: 1591
		private Matrix rstickMatrix = Matrix.Identity;

		// Token: 0x04000638 RID: 1592
		private ModelBone lstickBone;

		// Token: 0x04000639 RID: 1593
		private Matrix lstickTransform;

		// Token: 0x0400063A RID: 1594
		private Matrix lstickMatrix = Matrix.Identity;

		// Token: 0x0400063B RID: 1595
		private ModelBone ltBone;

		// Token: 0x0400063C RID: 1596
		private Matrix ltTransform;

		// Token: 0x0400063D RID: 1597
		private Matrix ltMatrix = Matrix.Identity;

		// Token: 0x0400063E RID: 1598
		private ModelBone rtBone;

		// Token: 0x0400063F RID: 1599
		private Matrix rtTransform;

		// Token: 0x04000640 RID: 1600
		private Matrix rtMatrix = Matrix.Identity;

		// Token: 0x04000641 RID: 1601
		private ModelBone lbBone;

		// Token: 0x04000642 RID: 1602
		private Matrix lbTransform;

		// Token: 0x04000643 RID: 1603
		private Matrix lbMatrix = Matrix.Identity;

		// Token: 0x04000644 RID: 1604
		private ModelBone rbBone;

		// Token: 0x04000645 RID: 1605
		private Matrix rbTransform;

		// Token: 0x04000646 RID: 1606
		private Matrix rbMatrix = Matrix.Identity;

		// Token: 0x04000647 RID: 1607
		private ModelBone bBone;

		// Token: 0x04000648 RID: 1608
		private Matrix bTransform;

		// Token: 0x04000649 RID: 1609
		private Matrix bMatrix = Matrix.Identity;

		// Token: 0x0400064A RID: 1610
		private ModelBone aBone;

		// Token: 0x0400064B RID: 1611
		private Matrix aTransform;

		// Token: 0x0400064C RID: 1612
		private Matrix aMatrix = Matrix.Identity;

		// Token: 0x0400064D RID: 1613
		private ModelBone xBone;

		// Token: 0x0400064E RID: 1614
		private Matrix xTransform;

		// Token: 0x0400064F RID: 1615
		private Matrix xMatrix = Matrix.Identity;

		// Token: 0x04000650 RID: 1616
		private ModelBone yBone;

		// Token: 0x04000651 RID: 1617
		private Matrix yTransform;

		// Token: 0x04000652 RID: 1618
		private Matrix yMatrix = Matrix.Identity;

		// Token: 0x04000653 RID: 1619
		private int buttonflagA;

		// Token: 0x04000654 RID: 1620
		private int buttonflagB;

		// Token: 0x04000655 RID: 1621
		private int buttonflagX;

		// Token: 0x04000656 RID: 1622
		private int buttonflagY;

		// Token: 0x04000657 RID: 1623
		private SpriteBatch spriteBatch;

		// Token: 0x04000658 RID: 1624
		private SpriteFont tag;

		// Token: 0x04000659 RID: 1625
		private SpriteFont menufont;

		// Token: 0x0400065A RID: 1626
		private Quad quad;

		// Token: 0x0400065B RID: 1627
		private VertexDeclaration quadVertexDec;

		// Token: 0x0400065C RID: 1628
		private Matrix world;

		// Token: 0x0400065D RID: 1629
		private BasicEffect effect;

		// Token: 0x0400065E RID: 1630
		private Vector2 textPosition = new Vector2(256f);

		// Token: 0x0400065F RID: 1631
		private float radians = Convert.ToSingle(0.6283185307179586);

		// Token: 0x04000660 RID: 1632
		private ParticleSystem tracerx;

		// Token: 0x04000661 RID: 1633
		private ParticleSystem dotx;

		// Token: 0x04000662 RID: 1634
		private ParticleSystem buttonburstA;

		// Token: 0x04000663 RID: 1635
		private ParticleSystem buttonburstB;

		// Token: 0x04000664 RID: 1636
		private ParticleSystem buttonburstX;

		// Token: 0x04000665 RID: 1637
		private ParticleSystem buttonburstY;

		// Token: 0x04000666 RID: 1638
		private Texture2D leftstick;

		// Token: 0x04000667 RID: 1639
		private Texture2D rightstick;

		// Token: 0x04000668 RID: 1640
		private Texture2D xbutton;

		// Token: 0x04000669 RID: 1641
		private Texture2D bbutton;

		// Token: 0x0400066A RID: 1642
		private Texture2D abutton;

		// Token: 0x0400066B RID: 1643
		private Texture2D ybutton;

		// Token: 0x0400066C RID: 1644
		private Texture2D rtrigger;

		// Token: 0x0400066D RID: 1645
		private Texture2D rbumper;

		// Token: 0x0400066E RID: 1646
		private Texture2D ltrigger;

		// Token: 0x0400066F RID: 1647
		private Texture2D lbumper;

		// Token: 0x04000670 RID: 1648
		private Texture2D dpadLR;

		// Token: 0x04000671 RID: 1649
		private Texture2D dpadUD;

		// Token: 0x04000672 RID: 1650
		private Vector3 leftstick0 = new Vector3(-350f, 275f, -180f);

		// Token: 0x04000673 RID: 1651
		private Vector3 leftstick1 = new Vector3(-480f, 450f, -190f);

		// Token: 0x04000674 RID: 1652
		private Vector3 rightstick0 = new Vector3(180f, 230f, 30f);

		// Token: 0x04000675 RID: 1653
		private Vector3 rightstick1 = new Vector3(300f, 450f, 30f);

		// Token: 0x04000676 RID: 1654
		private Vector3 xbutton0 = new Vector3(190f, 190f, -190f);

		// Token: 0x04000677 RID: 1655
		private Vector3 xbutton1 = new Vector3(80f, 250f, -190f);

		// Token: 0x04000678 RID: 1656
		private Vector3 bbutton0 = new Vector3(420f, 190f, -190f);

		// Token: 0x04000679 RID: 1657
		private Vector3 bbutton1 = new Vector3(570f, 250f, -190f);

		// Token: 0x0400067A RID: 1658
		private Vector3 ybutton0 = new Vector3(305f, 220f, -300f);

		// Token: 0x0400067B RID: 1659
		private Vector3 ybutton1 = new Vector3(330f, 280f, -420f);

		// Token: 0x0400067C RID: 1660
		private Vector3 abutton0 = new Vector3(305f, 150f, -90f);

		// Token: 0x0400067D RID: 1661
		private Vector3 abutton1 = new Vector3(390f, 160f, 60f);

		// Token: 0x0400067E RID: 1662
		private Vector3 rtrigger0 = new Vector3(330f, 90f, -360f);

		// Token: 0x0400067F RID: 1663
		private Vector3 rtrigger1 = new Vector3(400f, -80f, -530f);

		// Token: 0x04000680 RID: 1664
		private Vector3 rbumper0 = new Vector3(250f, 180f, -370f);

		// Token: 0x04000681 RID: 1665
		private Vector3 rbumper1 = new Vector3(150f, 100f, -460f);

		// Token: 0x04000682 RID: 1666
		private Vector3 ltrigger0 = new Vector3(-320f, 90f, -360f);

		// Token: 0x04000683 RID: 1667
		private Vector3 ltrigger1 = new Vector3(-400f, -80f, -530f);

		// Token: 0x04000684 RID: 1668
		private Vector3 lbumper0 = new Vector3(-250f, 180f, -370f);

		// Token: 0x04000685 RID: 1669
		private Vector3 lbumper1 = new Vector3(-150f, 100f, -460f);

		// Token: 0x04000686 RID: 1670
		private Vector3 dpadLR0 = new Vector3(-270f, 150f, 10f);

		// Token: 0x04000687 RID: 1671
		private Vector3 dpadLR1 = new Vector3(-450f, 200f, 0f);

		// Token: 0x04000688 RID: 1672
		private Vector3 dpadUD0 = new Vector3(-180f, 135f, 100f);

		// Token: 0x04000689 RID: 1673
		private Vector3 dpadUD1 = new Vector3(-280f, 100f, 300f);

		// Token: 0x0400068A RID: 1674
		private int drawonce;

		// Token: 0x0400068B RID: 1675
		private string[] defined = new string[0];

		// Token: 0x0400068C RID: 1676
		private string[] lander = new string[]
		{
			"Steering", "camera", "NA", "objectives", "  Activate", "beacon", "Thrust", "Unknown", "reverse", "Unknown",
			"On/Off", "Select"
		};

		// Token: 0x0400068D RID: 1677
		private string[] lander2 = new string[]
		{
			"Steering", "zoomout", "NA", "displays", "  Activate", "drop", "Thrust", "Unknown", "reverse", "Unknown",
			"On/Off", "Select"
		};

		// Token: 0x0400068E RID: 1678
		private string[] rover = new string[]
		{
			"Steering", "Camera", "interact", "objective", "  Activate", "honk", "Forward", "scooper", "Reverse", "scooper",
			"On/Off", "Select"
		};

		// Token: 0x0400068F RID: 1679
		private string[] rover2 = new string[]
		{
			"Steering", "zoomout", "interact", "display", "  Activate", "getout", "Forward", "open", "Reverse", "close",
			"On/Off", "Select"
		};

		// Token: 0x04000690 RID: 1680
		private string[] walk = new string[]
		{
			"walk", "look", "interact", "objective", "  jump", "callout", "NA", "Unknown", "NA", "Unknown",
			"On/Off", "Select"
		};

		// Token: 0x04000691 RID: 1681
		private string[] walk2 = new string[]
		{
			"run", "look", "interact", "display", " doublejump", "disembark", "NA", "Unknown", "NA", "Unknown",
			"On/Off", "Select"
		};

		// Token: 0x04000692 RID: 1682
		private string[] xxx = new string[]
		{
			"Lstick", "Rstickk", "xbutton", "bbutton", "abutton", "ybutton", "rtrigger", "rbumper", "ltrigger", "lbumper",
			"dpadUD", "dpadLR"
		};

		// Token: 0x04000693 RID: 1683
		private string header;

		// Token: 0x04000694 RID: 1684
		private string[] header1 = new string[] { "", "Lander Controls", "Rover Controls", "V127 Controls", "Menu Controls" };

		// Token: 0x04000695 RID: 1685
		private Vector3 diffuse;

		// Token: 0x04000696 RID: 1686
		private Vector3 diffuse1 = new Vector3(0.7f, 0.7f, 0.7f);

		// Token: 0x04000697 RID: 1687
		private Vector3 diffuse2 = new Vector3(0.7f, 0.7f, 0.7f);

		// Token: 0x04000698 RID: 1688
		private Vector3 diffuse3 = new Vector3(0.3f, 0.3f, 0.3f);

		// Token: 0x04000699 RID: 1689
		private Vector3 diffuse4 = new Vector3(0.35f, 0.35f, 0.35f);

		// Token: 0x0400069A RID: 1690
		private string[] titlehead = new string[] { "Shit Happens", " lander", " rover", " walking", "Shit Happens" };

		// Token: 0x0400069B RID: 1691
		private int pick;

		// Token: 0x0400069C RID: 1692
		private ScreenManager sc;
	}
}
