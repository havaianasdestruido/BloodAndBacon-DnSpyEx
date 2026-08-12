using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x02000085 RID: 133
	internal class Interface : MenuScreen
	{
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600049C RID: 1180 RVA: 0x00111C58 File Offset: 0x0010FE58
		// (remove) Token: 0x0600049D RID: 1181 RVA: 0x00111C90 File Offset: 0x0010FE90
		public event EventHandler<PlayerIndexEventArgs> Accepted;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600049E RID: 1182 RVA: 0x00111CC8 File Offset: 0x0010FEC8
		// (remove) Token: 0x0600049F RID: 1183 RVA: 0x00111D00 File Offset: 0x0010FF00
		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		// Token: 0x060004A0 RID: 1184 RVA: 0x00111D5C File Offset: 0x0010FF5C
		public Interface()
			: base("")
		{
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00111F4C File Offset: 0x0011014C
		public override void LoadContent()
		{
			if (this.content == null)
			{
				this.content = new ContentManager(base.ScreenManager.Game.Services, "Content");
			}
			this.spriteBatch = new SpriteBatch(base.ScreenManager.GraphicsDevice);
			this.sc = base.ScreenManager;
			this.fullscreen = new Rectangle(0, 74, 1280, 586);
			this.topbar = new Rectangle(0, 0, 1280, 80);
			this.bottombar = new Rectangle(0, 654, 1280, 66);
			this.fullscreenRGB = new Rectangle(0, 0, 150, 68);
			this.topRGB = new Rectangle(155, 0, 39, 113);
			this.bottomRGB = new Rectangle(0, 70, 40, 40);
			this.cursor = new Rectangle(72, 116, 64, 64);
			this.curX = 640f;
			this.curY = 360f;
			this.buttonB = new Rectangle(198, 0, 26, 26);
			this.emptybox = new Rectangle(250, 0, 66, 66);
			this.boxes = new Rectangle(323, 0, 69, 22);
			this.collide1 = new Rectangle[6];
			this.upgradeslots = new Rectangle(0, 200, 206, 97);
			this.icons = new Rectangle[19];
			this.icons[0] = new Rectangle(0, 310, 66, 66);
			this.icons[1] = new Rectangle(66, 310, 66, 66);
			this.icons[2] = new Rectangle(132, 310, 66, 66);
			this.icons[3] = new Rectangle(0, 376, 66, 66);
			this.icons[4] = new Rectangle(66, 376, 66, 66);
			this.icons[5] = new Rectangle(132, 376, 66, 66);
			this.icons[6] = new Rectangle(0, 442, 66, 66);
			this.icons[7] = new Rectangle(66, 442, 66, 66);
			this.icons[8] = new Rectangle(132, 442, 66, 66);
			this.icons[9] = new Rectangle(0, 508, 66, 66);
			this.icons[10] = new Rectangle(66, 508, 66, 66);
			this.icons[11] = new Rectangle(132, 508, 66, 66);
			this.icons[12] = new Rectangle(0, 574, 66, 66);
			this.icons[13] = new Rectangle(66, 574, 66, 66);
			this.icons[14] = new Rectangle(132, 574, 66, 66);
			this.icons[15] = new Rectangle(0, 640, 66, 66);
			this.icons[16] = new Rectangle(66, 640, 66, 66);
			this.icons[17] = new Rectangle(132, 640, 66, 66);
			this.sc.RoverEquip();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00112330 File Offset: 0x00110530
		public override void HandleInput(InputState input)
		{
			if (base.ControllingPlayer != null)
			{
				int value = (int)base.ControllingPlayer.Value;
				this.gamestate = input.CurrentGamePadStates[value];
				this.prevstate = input.LastGamePadStates[value];
			}
			PlayerIndex playerIndex;
			if (input.IsMenuCancel(base.ControllingPlayer, out playerIndex))
			{
				this.sc.scooperMatrix = Matrix.CreateRotationX(0f) * Matrix.CreateTranslation(0f, 21.825f, 8.95f);
				this.sc.scooperBone.Transform = this.sc.scooperMatrix * this.sc.scooperTrans;
				if (this.Cancelled != null)
				{
					this.Cancelled(this, new PlayerIndexEventArgs(playerIndex));
				}
				base.ScreenManager.menuflag = 0;
				base.ExitScreen();
			}
			if (input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out playerIndex) && this.touchingBox > -1 && this.butpress > 0 && this.flashTimer <= 0)
			{
				int num = this.sc.equip[this.touchingBox * 3];
				this.sc.equip[this.touchingBox * 3] = this.sc.equip[this.touchingBox * 3 + this.butpress];
				this.sc.equip[this.touchingBox * 3 + this.butpress] = num;
				this.sc.select.Play(this.sc.ev, 0f, 0f);
				this.flashAnim = 0f;
				this.flashTimer = 30;
				this.flash2 = this.butpress;
				this.sc.RoverEquip();
			}
			if (this.gamestate.Buttons.RightStick == ButtonState.Pressed)
			{
				ButtonState rightStick = this.prevstate.Buttons.RightStick;
			}
			if (this.gamestate.Buttons.LeftStick == ButtonState.Pressed)
			{
				ButtonState leftStick = this.prevstate.Buttons.LeftStick;
			}
			float num2 = 12f;
			if (this.touching)
			{
				num2 = 6f;
			}
			Vector2 vector = this.gamestate.ThumbSticks.Left * this.gamestate.ThumbSticks.Left.Length();
			this.curX += vector.X * num2;
			this.curY -= vector.Y * num2;
			this.curX = MathHelper.Clamp(this.curX, 40f, 1240f);
			this.curY = MathHelper.Clamp(this.curY, 90f, 650f);
			if (this.gamestate.DPad.Right == ButtonState.Pressed && this.prevstate.DPad.Right == ButtonState.Released)
			{
				this.currentNum += 1f;
				if (this.currentNum > (float)this.max)
				{
					this.currentNum = (float)this.max;
				}
			}
			if (this.gamestate.DPad.Left == ButtonState.Pressed && this.prevstate.DPad.Left == ButtonState.Released)
			{
				this.currentNum -= 1f;
				if (this.currentNum < 1f)
				{
					this.currentNum = 1f;
				}
			}
			if (this.gamestate.Buttons.RightShoulder == ButtonState.Pressed && this.prevstate.Buttons.RightShoulder == ButtonState.Released)
			{
				this.currentNum += (float)(this.max / 4);
				if (this.currentNum > (float)this.max)
				{
					this.currentNum = (float)this.max;
				}
			}
			if (this.gamestate.Buttons.LeftShoulder == ButtonState.Pressed && this.prevstate.Buttons.LeftShoulder == ButtonState.Released)
			{
				this.currentNum -= (float)(this.max / 4);
				if (this.currentNum < 1f)
				{
					this.currentNum = 1f;
				}
			}
			if (this.gamestate.ThumbSticks.Left.X >= 0.15f)
			{
				this.currentNum += this.gamestate.ThumbSticks.Left.X * 2f;
				if (this.currentNum > (float)this.max)
				{
					this.currentNum = (float)this.max;
				}
			}
			if (this.gamestate.ThumbSticks.Left.X <= -0.15f)
			{
				this.currentNum += this.gamestate.ThumbSticks.Left.X * 2f;
				if (this.currentNum < 1f)
				{
					this.currentNum = 1f;
				}
			}
			float num3 = -this.gamestate.ThumbSticks.Right.X;
			this.sc.wheelRollMatrix *= Matrix.CreateRotationX(-0.04f);
			this.sc.wheelRollMatrix2 = Matrix.CreateRotationY(num3 / 2.5f);
			this.sc.rackMatrix = Matrix.CreateTranslation((float)Math.Sin((double)(-(double)num3 / 1.8f)) * 4f, 0f, (float)(-(float)Math.Cos((double)(-(double)num3 / 1.8f))) * 120f + 114f);
			this.myRot += num3 / 15f;
			this.orientation = Matrix.CreateRotationY(this.myRot);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00112910 File Offset: 0x00110B10
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
			this.touching = false;
			for (int i = 0; i < this.collide1.Length; i++)
			{
				Rectangle rectangle = new Rectangle((int)(this.curX - 10f), (int)(this.curY - 10f), 20, 20);
				if (this.collide1[i].Intersects(rectangle))
				{
					this.touching = true;
					this.touchingBox = i;
					this.butpress = 0;
					if (this.boxIndexUp != i)
					{
						if (this.boxIndexUp > -1)
						{
							this.boxIndexDown = this.boxIndexUp;
							this.boxDec = this.boxSize.Length - 1;
						}
						this.boxIndexUp = i;
						this.boxInc = 0;
					}
				}
			}
			if (!this.touching)
			{
				Rectangle rectangle2 = new Rectangle((int)(this.curX - 10f), (int)(this.curY - 10f), 20, 20);
				if (this.collideBut1.Intersects(rectangle2))
				{
					this.touching = true;
					this.butpress = 1;
				}
				else if (this.collideBut2.Intersects(rectangle2))
				{
					this.touching = true;
					this.butpress = 2;
				}
				else
				{
					this.butpress = 0;
				}
			}
			this.collideBut1 = new Rectangle(0, 0, 0, 0);
			this.collideBut2 = new Rectangle(0, 0, 0, 0);
			if (this.touching)
			{
				float num = 0.06f;
				this.curScale -= num;
				if (this.curScale < 0.7f)
				{
					this.curScale = 0.7f;
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
				if (this.curOpacity < 0.6f)
				{
					this.curOpacity = 0.6f;
				}
				this.curRot = 0f;
			}
			if (!this.touching && this.boxIndexUp > -1)
			{
				this.touchingBox = -1;
				this.boxIndexDown = this.boxIndexUp;
				this.boxDec = this.boxSize.Length - 1;
				this.boxIndexUp = -1;
				this.boxInc = 0;
				this.boxScaleUp = this.boxSize[this.boxInc];
			}
			if (this.boxIndexUp > -1)
			{
				this.boxInc++;
				if (this.boxInc > this.boxSize.Length - 1)
				{
					this.boxInc = this.boxSize.Length - 1;
				}
				this.boxScaleUp = this.boxSize[this.boxInc];
			}
			if (this.boxIndexDown > -1)
			{
				this.boxDec--;
				if (this.boxDec < 0)
				{
					this.boxDec = 0;
					this.boxIndexDown = -1;
				}
				this.boxScaleDown = this.boxSize[this.boxDec];
			}
			base.Update(gameTime, otherScreenHasFocus, false);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00112C4C File Offset: 0x00110E4C
		public override void Draw(GameTime gameTime)
		{
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			this.spriteBatch.Draw(this.sc.interfaceBlob, this.fullscreen, new Rectangle?(this.fullscreenRGB), Color.White);
			this.spriteBatch.End();
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			this.sc.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
			this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
			base.ScreenManager.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
			Vector2 vector = new Vector2(this.curX - 640f, this.curY - 320f) * -0.1f;
			this.view = Matrix.CreateLookAt(new Vector3(-200f, 120f, -vector.X), new Vector3(0f, 30f + vector.Y, -vector.X), Vector3.Up);
			this.projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60f), 1.778f, 1f, 95000f);
			this.DrawRover(this.view, this.projection, Vector3.Normalize(new Vector3(1f, -0.8f, 0.25f)));
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			Vector2 vector2 = new Vector2(this.curX - 640f, this.curY - 320f) * -0.3f;
			for (int i = 0; i < this.boxGrid.Length; i++)
			{
				Vector2 vector3 = vector2 + this.boxGrid[i];
				this.collide1[i] = new Rectangle((int)vector3.X, (int)vector3.Y, 66, 66);
				bool flag = true;
				if (this.touchingBox == i)
				{
					if (this.sc.equip[i * 3] != 0)
					{
						this.spriteBatch.Draw(this.sc.interfaceBlob, vector3 + new Vector2(33f, 33f), new Rectangle?(this.icons[i * 3 + this.sc.equip[i * 3] - 1]), Color.White * 1f, 0f, new Vector2(33f, 33f), this.boxScaleUp, SpriteEffects.None, 0f);
					}
					else
					{
						this.spriteBatch.Draw(this.sc.interfaceBlob, vector3 + new Vector2(33f, 33f), new Rectangle?(this.emptybox), Color.White * 1f, 0f, new Vector2(33f, 33f), this.boxScaleUp, SpriteEffects.None, 0f);
					}
					if (this.flashTimer > 0)
					{
						this.spriteBatch.Draw(this.sc.interfaceBlob, vector3 + new Vector2(33f, 33f), new Rectangle?(new Rectangle(this.glow.X, 85 + (int)this.flashAnim, this.glow.Width, this.glow.Height)), Color.White, 0f, new Vector2(33f, 33f), this.boxScaleUp, SpriteEffects.None, 0f);
					}
					if (this.touching)
					{
						if (i < 3)
						{
							int num = i * 3;
							this.spriteBatch.Draw(this.sc.interfaceBlob, vector3 + new Vector2(-221f, -31f), new Rectangle?(this.upgradeslots), Color.White);
							this.spriteBatch.DrawString(this.sc.tahoma2, this.boxName[i], vector3 + new Vector2(-221f, -28f), new Color(0, 0, 0, 255));
							if (this.sc.equip[num + 1] != 0)
							{
								float num2 = vector3.X - 81f;
								this.collideBut1 = new Rectangle((int)num2, (int)vector3.Y, 66, 66);
								this.spriteBatch.Draw(this.sc.interfaceBlob, new Vector2(num2, vector3.Y), new Rectangle?(this.icons[num + this.sc.equip[num + 1] - 1]), Color.White * 1f);
								if (this.flashTimer > 0 && this.flash2 == 1)
								{
									this.spriteBatch.Draw(this.sc.interfaceBlob, new Vector2(num2, vector3.Y), new Rectangle?(new Rectangle(this.glow.X, 85 + (int)this.flashAnim, this.glow.Width, this.glow.Height)), Color.White);
								}
							}
							if (this.sc.equip[num + 2] != 0)
							{
								float num3 = vector3.X - 151f;
								this.collideBut2 = new Rectangle((int)num3, (int)vector3.Y, 66, 66);
								this.spriteBatch.Draw(this.sc.interfaceBlob, new Vector2(num3, vector3.Y), new Rectangle?(this.icons[num + this.sc.equip[num + 2] - 1]), Color.White * 1f);
								if (this.flashTimer > 0 && this.flash2 == 2)
								{
									this.spriteBatch.Draw(this.sc.interfaceBlob, new Vector2(num3, vector3.Y), new Rectangle?(new Rectangle(this.glow.X, 85 + (int)this.flashAnim, this.glow.Width, this.glow.Height)), Color.White);
								}
							}
						}
						else
						{
							int num4 = i * 3;
							this.spriteBatch.Draw(this.sc.interfaceBlob, vector3 + new Vector2(81f, -31f), new Rectangle?(this.upgradeslots), Color.White * 1f);
							this.spriteBatch.DrawString(this.sc.tahoma2, this.boxName[i], vector3 + new Vector2(81f, -28f), new Color(0, 0, 0, 255));
							if (this.sc.equip[num4 + 1] != 0)
							{
								float num5 = vector3.X + 81f;
								this.collideBut1 = new Rectangle((int)num5, (int)vector3.Y, 66, 66);
								this.spriteBatch.Draw(this.sc.interfaceBlob, new Vector2(num5, vector3.Y), new Rectangle?(this.icons[num4 + this.sc.equip[num4 + 1] - 1]), Color.White * 1f);
								if (this.flashTimer > 0 && this.flash2 == 1)
								{
									this.spriteBatch.Draw(this.sc.interfaceBlob, new Vector2(num5, vector3.Y), new Rectangle?(new Rectangle(this.glow.X, 85 + (int)this.flashAnim, this.glow.Width, this.glow.Height)), Color.White);
								}
							}
							if (this.sc.equip[num4 + 2] != 0)
							{
								float num6 = vector3.X + 151f;
								this.collideBut2 = new Rectangle((int)num6, (int)vector3.Y, 66, 66);
								this.spriteBatch.Draw(this.sc.interfaceBlob, new Vector2(num6, vector3.Y), new Rectangle?(this.icons[num4 + this.sc.equip[num4 + 2] - 1]), Color.White * 1f);
								if (this.flashTimer > 0 && this.flash2 == 2)
								{
									this.spriteBatch.Draw(this.sc.interfaceBlob, new Vector2(num6, vector3.Y), new Rectangle?(new Rectangle(this.glow.X, 85 + (int)this.flashAnim, this.glow.Width, this.glow.Height)), Color.White);
								}
							}
						}
						flag = false;
					}
				}
				else
				{
					float num7 = 1f;
					if (this.boxIndexDown == i)
					{
						num7 = this.boxScaleDown;
					}
					if (this.sc.equip[i * 3] != 0)
					{
						this.spriteBatch.Draw(this.sc.interfaceBlob, vector3 + new Vector2(33f, 33f), new Rectangle?(this.icons[i * 3 + this.sc.equip[i * 3] - 1]), Color.White * 1f, 0f, new Vector2(33f, 33f), num7, SpriteEffects.None, 0f);
					}
					else
					{
						this.spriteBatch.Draw(this.sc.interfaceBlob, vector3 + new Vector2(33f, 33f), new Rectangle?(this.emptybox), Color.White * 1f, 0f, new Vector2(33f, 33f), num7, SpriteEffects.None, 0f);
					}
				}
				if (flag)
				{
					if (i > 2)
					{
						vector3 = vector2 + this.boxGrid[i] + new Vector2(86f, 10f);
						this.spriteBatch.Draw(this.sc.interfaceBlob, vector3, new Rectangle?(this.boxes), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
					}
					else
					{
						vector3 = vector2 + this.boxGrid[i] + new Vector2(-86f, 10f);
						this.spriteBatch.Draw(this.sc.interfaceBlob, vector3, new Rectangle?(this.boxes), Color.White);
					}
				}
			}
			this.spriteBatch.Draw(this.sc.interfaceBlob, new Vector2(this.curX, this.curY), new Rectangle?(this.cursor), Color.White * this.curOpacity, this.curRot, new Vector2(32f, 32f), this.curScale, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.sc.interfaceBlob, this.topbar, new Rectangle?(this.topRGB), Color.White);
			this.spriteBatch.Draw(this.sc.interfaceBlob, this.bottombar, new Rectangle?(this.bottomRGB), Color.White);
			float num8 = 115f;
			float num9 = 35f;
			this.spriteBatch.DrawString(this.sc.tahoma1, "Settings", new Vector2(num8, 40f), Color.White * 0.4f);
			num8 += this.sc.tahoma1.MeasureString("Settings").X + num9;
			this.spriteBatch.DrawString(this.sc.tahoma1, "Rover257", new Vector2(num8, 40f), Color.White * 1f);
			num8 += this.sc.tahoma1.MeasureString("Rover257").X + num9;
			this.spriteBatch.DrawString(this.sc.tahoma1, "Lander257", new Vector2(num8, 40f), Color.White * 0.4f);
			this.spriteBatch.End();
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x001138D8 File Offset: 0x00111AD8
		public void DrawRover(Matrix viewMatrix, Matrix projectionMatrix, Vector3 sundir)
		{
			this.sc.BackWheelBone.Transform = this.sc.wheelRollMatrix * this.sc.BackWheelTrans;
			this.sc.leftFrontjointBone.Transform = this.sc.wheelRollMatrix2 * this.sc.leftFrontjointTrans;
			this.sc.rightFrontjointBone.Transform = this.sc.wheelRollMatrix2 * this.sc.rightFrontjointTrans;
			this.sc.leftFrontWheelBone.Transform = this.sc.wheelRollMatrix * this.sc.leftFrontWheelTrans;
			this.sc.rightFrontWheelBone.Transform = this.sc.wheelRollMatrix * this.sc.rightFrontWheelTrans;
			this.sc.rackBone.Transform = this.sc.rackMatrix * this.sc.rackTrans;
			if (this.sc.equip[3] == 1)
			{
				this.sc.scooperMatrix = Matrix.CreateRotationX(-0.44f) * Matrix.CreateTranslation(0f, 25.98f, -26.1f);
			}
			if (this.sc.equip[3] > 1)
			{
				this.sc.scooperMatrix = Matrix.CreateRotationX(0f) * Matrix.CreateTranslation(0f, 21f, -44f);
			}
			this.sc.scooperBone.Transform = this.sc.scooperMatrix * this.sc.scooperTrans;
			Matrix[] array = new Matrix[this.sc.roverModel.Bones.Count];
			this.sc.roverModel.CopyAbsoluteBoneTransformsTo(array);
			Vector3 vector = new Vector3(0f, 0f, 0f);
			float num = 0f;
			Vector3 vector2 = new Vector3(vector.X, vector.Y + MathHelper.Lerp(0f, 20f, Math.Abs(num)), vector.Z);
			Matrix matrix = this.orientation * Matrix.CreateFromAxisAngle(this.orientation.Forward, num) * Matrix.CreateTranslation(vector2);
			Vector3 vector3 = new Vector3(0.5f, 0.47f, 0.35f);
			Vector3 vector4 = new Vector3(1f, 1f, 1f);
			for (int i = 0; i < this.sc.roverparts.Count; i++)
			{
				ModelMesh modelMesh = this.sc.roverModel.Meshes[this.sc.roverparts[i]];
				int num2 = 1;
				if (num2 == 1)
				{
					foreach (Effect effect in modelMesh.Effects)
					{
						BasicEffect basicEffect = (BasicEffect)effect;
						basicEffect.World = array[modelMesh.ParentBone.Index] * matrix;
						basicEffect.Alpha = 1f;
						basicEffect.View = viewMatrix;
						basicEffect.Projection = projectionMatrix;
						basicEffect.LightingEnabled = true;
						basicEffect.PreferPerPixelLighting = false;
						basicEffect.DirectionalLight0.Enabled = true;
						basicEffect.AmbientLightColor = vector3;
						basicEffect.DirectionalLight0.Direction = sundir;
						basicEffect.DirectionalLight0.DiffuseColor = vector4;
					}
					modelMesh.Draw();
				}
			}
		}

		// Token: 0x04001241 RID: 4673
		private SpriteBatch spriteBatch;

		// Token: 0x04001242 RID: 4674
		private Rectangle fullscreen;

		// Token: 0x04001243 RID: 4675
		private Rectangle topbar;

		// Token: 0x04001244 RID: 4676
		private Rectangle bottombar;

		// Token: 0x04001245 RID: 4677
		private Rectangle fullscreenRGB;

		// Token: 0x04001246 RID: 4678
		private Rectangle topRGB;

		// Token: 0x04001247 RID: 4679
		private Rectangle bottomRGB;

		// Token: 0x04001248 RID: 4680
		private Rectangle cursor;

		// Token: 0x04001249 RID: 4681
		private Rectangle buttonB;

		// Token: 0x0400124A RID: 4682
		private Rectangle upgradeslots;

		// Token: 0x0400124B RID: 4683
		private Rectangle emptybox;

		// Token: 0x0400124C RID: 4684
		private Rectangle boxes;

		// Token: 0x0400124D RID: 4685
		private Rectangle[] icons;

		// Token: 0x0400124E RID: 4686
		private Vector2[] boxGrid = new Vector2[]
		{
			new Vector2(317f, 260f),
			new Vector2(317f, 345f),
			new Vector2(317f, 430f),
			new Vector2(907f, 260f),
			new Vector2(907f, 345f),
			new Vector2(907f, 430f)
		};

		// Token: 0x0400124F RID: 4687
		private Rectangle[] collide1;

		// Token: 0x04001250 RID: 4688
		private Rectangle collideBut1;

		// Token: 0x04001251 RID: 4689
		private Rectangle collideBut2;

		// Token: 0x04001252 RID: 4690
		private int butpress;

		// Token: 0x04001253 RID: 4691
		private float curX;

		// Token: 0x04001254 RID: 4692
		private float curY;

		// Token: 0x04001255 RID: 4693
		private float curRot;

		// Token: 0x04001256 RID: 4694
		private float curScale = 1f;

		// Token: 0x04001257 RID: 4695
		private float curOpacity = 0.6f;

		// Token: 0x04001258 RID: 4696
		private float boxScaleUp = 1f;

		// Token: 0x04001259 RID: 4697
		private int boxInc;

		// Token: 0x0400125A RID: 4698
		private int boxIndexUp = -1;

		// Token: 0x0400125B RID: 4699
		private float[] boxSize = new float[] { 0.95f, 0.98f, 1f, 1.03f, 1.05f, 1.1f, 1.13f, 1.1f, 1.05f };

		// Token: 0x0400125C RID: 4700
		private string[] boxName = new string[] { "Engine Type", "Extractor Type", "Solar Panel", "Traction Type", "Flatbed Depth", "Scoping Sys" };

		// Token: 0x0400125D RID: 4701
		private float boxScaleDown = 1f;

		// Token: 0x0400125E RID: 4702
		private int boxDec = 1;

		// Token: 0x0400125F RID: 4703
		private int boxIndexDown = -1;

		// Token: 0x04001260 RID: 4704
		private int flashTimer;

		// Token: 0x04001261 RID: 4705
		private float flashAnim;

		// Token: 0x04001262 RID: 4706
		private Rectangle glow = new Rectangle(235, 80, 66, 66);

		// Token: 0x04001263 RID: 4707
		private int flash2 = -1;

		// Token: 0x04001264 RID: 4708
		private float upgradeScale = 0.2f;

		// Token: 0x04001265 RID: 4709
		private float boxOpacity = 0.9f;

		// Token: 0x04001266 RID: 4710
		private int max;

		// Token: 0x04001267 RID: 4711
		private float currentNum;

		// Token: 0x04001268 RID: 4712
		private bool touching;

		// Token: 0x04001269 RID: 4713
		private int touchingBox;

		// Token: 0x0400126A RID: 4714
		private GamePadState gamestate;

		// Token: 0x0400126B RID: 4715
		private GamePadState prevstate;

		// Token: 0x0400126E RID: 4718
		private ScreenManager sc;

		// Token: 0x0400126F RID: 4719
		private ContentManager content;

		// Token: 0x04001270 RID: 4720
		private Matrix projection;

		// Token: 0x04001271 RID: 4721
		private Matrix view;

		// Token: 0x04001272 RID: 4722
		private Matrix orientation = Matrix.CreateRotationY(1f);

		// Token: 0x04001273 RID: 4723
		private float myRot = 1.5f;

		// Token: 0x04001274 RID: 4724
		private Vector3 sunDir = new Vector3(0.5f, 1f, 0.1f);
	}
}
