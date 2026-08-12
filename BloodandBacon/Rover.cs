using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x020000DD RID: 221
	internal class Rover : GameScreen
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x001C5178 File Offset: 0x001C3378
		public void LoadContent(ContentManager content, ScreenManager sc)
		{
			this.content = content;
			this.rr = new Random();
			this.sc = sc;
			this.gr = sc.GraphicsDevice;
			this.jumpit = content.Load<SoundEffect>("astro\\Audio\\horn1");
			Rover.realGrav = -0.6f;
			sc.RoverEquip();
			this.boneTransforms = new Matrix[sc.roverModel.Bones.Count];
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x001C51E6 File Offset: 0x001C33E6
		public override void UnloadContent()
		{
			this.content.Unload();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x001C51F4 File Offset: 0x001C33F4
		public bool boxcollide(Vector3 min, Vector3 max, Vector3 foffset)
		{
			BoundingBox boundingBox = new BoundingBox(min, max);
			if (boundingBox.Intersects(new BoundingSphere(this.position, 50f)))
			{
				float num = 9000f;
				Vector3 vector = this.velocity;
				if (this.velocity.Length() <= 0.1f)
				{
					vector = foffset - this.position;
				}
				vector = Vector3.Normalize(vector);
				Ray ray = new Ray(this.newPosition, vector);
				this.IntersectRayVsBox(boundingBox, ray, out num, out this.reflectnormal);
				if (num < 50f)
				{
					float num2 = MathHelper.Max(2f, this.velocity.Length() * 0.2f);
					if (num2 == 2f)
					{
						this.reflectnormal *= 2f;
					}
					else
					{
						this.reflectnormal = Vector3.Reflect(num2 * vector, this.reflectnormal);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x001C52DC File Offset: 0x001C34DC
		private bool IntersectRayVsBox(BoundingBox a_kBox, Ray a_kRay, out float a_fDist, out Vector3 normal)
		{
			normal = Vector3.Down;
			a_fDist = float.MaxValue;
			float? num = a_kRay.Intersects(a_kBox);
			if (num == null)
			{
				return false;
			}
			a_fDist = num.Value;
			Vector3 vector = a_kRay.Position + a_kRay.Direction * a_fDist;
			Vector3 vector2 = vector - a_kBox.Min;
			Vector3 vector3 = vector - a_kBox.Max;
			vector2.X = Math.Abs(vector2.X);
			vector2.Y = Math.Abs(vector2.Y);
			vector2.Z = Math.Abs(vector2.Z);
			vector3.X = Math.Abs(vector3.X);
			vector3.Y = Math.Abs(vector3.Y);
			vector3.Z = Math.Abs(vector3.Z);
			normal = Vector3.Left;
			float num2 = vector2.X;
			if (vector3.X < num2)
			{
				num2 = vector3.X;
				normal = Vector3.Right;
			}
			if (vector2.Y < num2)
			{
				num2 = vector2.Y;
				normal = Vector3.Down;
			}
			if (vector3.Y < num2)
			{
				num2 = vector3.Y;
				normal = Vector3.Up;
			}
			if (vector2.Z < num2)
			{
				num2 = vector2.Z;
				normal = Vector3.Forward;
			}
			if (vector3.Z < num2)
			{
				num2 = vector2.Z;
				normal = Vector3.Backward;
			}
			return true;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x001C5480 File Offset: 0x001C3680
		public bool KMdown(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = this.mouseState.LeftButton == ButtonState.Pressed;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = this.mouseState.MiddleButton == ButtonState.Pressed;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = this.mouseState.RightButton == ButtonState.Pressed;
			}
			else if (k == Keys.Print)
			{
				flag = this.mouseState.XButton1 == ButtonState.Pressed;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = this.mouseState.XButton2 == ButtonState.Pressed;
			}
			else
			{
				flag = this.keyState.IsKeyDown(k);
			}
			return flag;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x001C5514 File Offset: 0x001C3714
		public bool KMreleased(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = this.prevMouse.LeftButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = this.prevMouse.MiddleButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = this.prevMouse.RightButton == ButtonState.Released;
			}
			else if (k == Keys.Print)
			{
				flag = this.prevMouse.XButton1 == ButtonState.Released;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = this.prevMouse.XButton2 == ButtonState.Released;
			}
			else
			{
				flag = this.prevkeyState.IsKeyUp(k);
			}
			return flag;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x001C55A8 File Offset: 0x001C37A8
		public bool KMtoggle(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = this.mouseState.MiddleButton == ButtonState.Pressed && this.prevMouse.MiddleButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = this.mouseState.RightButton == ButtonState.Pressed && this.prevMouse.RightButton == ButtonState.Released;
			}
			else if (k == Keys.Print)
			{
				flag = this.mouseState.XButton1 == ButtonState.Pressed && this.prevMouse.XButton1 == ButtonState.Released;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = this.mouseState.XButton2 == ButtonState.Pressed && this.prevMouse.XButton2 == ButtonState.Released;
			}
			else
			{
				flag = this.keyState.IsKeyDown(k) && this.prevkeyState.IsKeyUp(k);
			}
			return flag;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x001C56A8 File Offset: 0x001C38A8
		public bool Ktoggle(Keys k)
		{
			return this.keyState.IsKeyDown(k) && this.prevkeyState.IsKeyUp(k);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x001C56D4 File Offset: 0x001C38D4
		public void HandleInput(ref MouseState mouse, ref MouseState prevmouse, ref KeyboardState keystate, ref KeyboardState prevkeystate, ref GamePadState currentGamePadState, ref int[,] heights, ref Vector3[,] normals)
		{
			this.mouseState = mouse;
			this.prevMouse = prevmouse;
			this.keyState = keystate;
			this.prevkeyState = prevkeystate;
			if (this.sc.usingMouse)
			{
				this.leftTrig = 0f;
				if (this.KMdown(this.sc.a_key))
				{
					this.thumb += 0.04f;
					if (this.thumb > 1f)
					{
						this.thumb = 1f;
					}
				}
				if (this.KMdown(this.sc.d_key))
				{
					this.thumb -= 0.04f;
					if (this.thumb < -1f)
					{
						this.thumb = -1f;
					}
				}
				if (this.KMreleased(this.sc.a_key) && this.KMreleased(this.sc.d_key))
				{
					if (this.thumb < 0f)
					{
						this.thumb += 0.1f;
						if (this.thumb > 0f)
						{
							this.thumb = 0f;
						}
					}
					if (this.thumb > 0f)
					{
						this.thumb -= 0.1f;
						if (this.thumb < 0f)
						{
							this.thumb = 0f;
						}
					}
				}
				if (this.KMdown(this.sc.lmb_key) || this.KMdown(this.sc.w_key))
				{
					this.righttrig -= 0.07f;
					if (this.righttrig < -1f)
					{
						this.righttrig = -1f;
					}
				}
				else
				{
					this.righttrig += 0.07f;
					if (this.righttrig > 0f)
					{
						this.righttrig = 0f;
					}
				}
				if (this.KMdown(this.sc.rmb_key) || this.KMdown(this.sc.s_key))
				{
					this.leftTrig = -1f;
				}
				else
				{
					this.leftTrig = 0f;
				}
			}
			else
			{
				this.thumb = -currentGamePadState.ThumbSticks.Left.X;
				this.righttrig = -currentGamePadState.Triggers.Right;
				this.leftTrig = -currentGamePadState.Triggers.Left;
			}
			Vector3 vector = new Vector3(Facility.offset.X + 2200f, Facility.offset.Y + 360f, Facility.offset.Z + 4080f);
			if (Vector3.DistanceSquared(vector, this.position) < 640000f)
			{
				Vector3 vector2 = new Vector3(-350f, -700f, -400f) * 1f + vector;
				Vector3 vector3 = new Vector3(350f, 700f, 400f) * 1f + vector;
				if (this.boxcollide(vector2, vector3, vector))
				{
					this.velocity = this.reflectnormal;
					this.grav = this.reflectnormal;
					this.movement /= 4f;
				}
			}
			Rover.rockhitCount--;
			bool flag = true;
			if (this.groundflag == 1)
			{
				this.speed = this.righttrig / 1f - this.leftTrig / 1.5f;
				if (this.righttrig > -0.01f && this.leftTrig > -0.01f)
				{
					this.movement.Z = this.movement.Z / 1.03f;
				}
				if (this.leftTrig < 0f && this.righttrig < 0f)
				{
					flag = false;
					this.movement *= 0.97f;
					this.speed = 0f;
					this.righttrig = 0f;
					this.leftTrig = 0f;
				}
				if (this.movement.Z > this.min)
				{
					this.movement.Z = this.min;
				}
				if (this.movement.Z > Rover.max || this.leftTrig < -0.4f)
				{
					this.movement.Z = this.movement.Z + this.speed * 1.1f;
				}
				if (this.movement.Z < Rover.max)
				{
					this.movement.Z = Rover.max;
				}
				if (this.scooperON)
				{
					Rover.fric = 0f;
				}
				Vector3 vector4 = Vector3.Transform(this.movement, this.orientation);
				if (Rover.fric > 0.01f)
				{
					float num = 1f - Rover.fric;
					float y = vector4.Y;
					vector4 = vector4 * num + Rover.fric * this.velocity;
					if (vector4.Length() > 0f)
					{
						vector4 = Vector3.Normalize(vector4) * vector4.Length();
						vector4.Y = y;
					}
				}
				this.velocity = vector4;
				float num2 = 1.3f - this.normal.Y;
				if (this.onramp > 0 || (this.normal.Y > 0.9f && this.righttrig == 0f && this.leftTrig == 0f))
				{
					num2 = 0f;
					this.grav *= 0.985f;
					this.movement *= 0.985f;
					this.velocity *= 0.985f;
				}
				if (this.hithard == 0)
				{
					this.grav.Z = this.grav.Z + this.normal.Z * num2 * 0.5f * Rover.fric;
					this.grav.X = this.grav.X + this.normal.X * num2 * 0.5f * Rover.fric;
				}
				this.grav /= 1.02f;
			}
			else
			{
				this.speed = this.righttrig;
				this.velocity.Y = this.velocity.Y + Rover.realGrav;
			}
			this.newPosition = this.position + this.velocity + this.grav;
			this.gndPosition.Y = this.newPosition.Y;
			this.oldnormal = this.normal;
			this.hitramp(heights, normals, ref this.onramp, ref this.position, ref this.newPosition, ref this.gndPosition, ref this.normal);
			if (this.groundflag == 0)
			{
				this.speedx *= 1f - MathHelper.Clamp(Math.Abs(this.newPosition.Y - this.gndPosition.Y) / 500f, 0f, 0.1f);
			}
			else
			{
				this.speedx = this.movement.Z;
			}
			if (this.hangtime < 0)
			{
				this.hangtime++;
			}
			if (!this.scopemode && this.groundflag == 1 && this.hangtime >= 0)
			{
				this.hillalign = Vector3.Dot(Vector3.Down, Vector3.Normalize(this.orientation.Forward));
				float num3 = 10f;
				float num4 = Vector2.Distance(new Vector2(this.newPosition.X, this.newPosition.Z), new Vector2(this.position.X, this.position.Z));
				bool flag2 = num4 > num3 && this.hillalign >= 0f;
				bool flag3 = num4 > num3 && this.hillalign < 0f;
				bool flag4 = this.newPosition.Y < this.position.Y && this.hillalign < 0f;
				if ((flag2 || flag3) && !flag4)
				{
					float num5 = 150f;
					Vector3 vector5 = this.velocity + this.grav;
					this.futureposition = this.newPosition + num5 * Vector3.Normalize(new Vector3(vector5.X, 0f, vector5.Z));
					this.GetHeightAndNormalX(ref heights, ref normals, this.futureposition, out this.futureposition.Y, out this.futurenormal);
					this.futureDot = Vector3.Dot(this.futurenormal, this.normal);
					bool flag5 = this.futureDot <= 0.99f;
					if (this.onramp == 0 && flag5)
					{
						this.groundflag = 0;
						this.zspin = this.movement.Z;
						this.rotAlign = 1f;
						this.incRot = 0.06f;
						this.grav = Vector3.Zero;
						if (this.hillalign < -0.7f)
						{
							this.velocity = new Vector3(this.newPosition.X, this.gndPosition.Y, this.newPosition.Z) - this.position;
						}
						else
						{
							this.velocity = this.newPosition - this.position;
						}
						this.jumpnow = true;
					}
				}
			}
			if (this.groundflag == 0)
			{
				Vector3 vector6 = Vector3.Transform(new Vector3(0f, 33f, 0f), this.orientation);
				if (this.hithard == 0)
				{
					if (this.orientation.Left.Y < 0.8f && this.thumb < 0f)
					{
						this.lean = Matrix.CreateFromAxisAngle(this.orientation.Forward, -this.thumb / 55f);
						this.orientation *= this.lean;
					}
					else if (this.orientation.Left.Y > -0.8f && this.thumb > 0f)
					{
						this.lean = Matrix.CreateFromAxisAngle(this.orientation.Backward, this.thumb / 55f);
						this.orientation *= this.lean;
					}
					if (this.orientation.Backward.Y < 0.85f && this.speed == 0f)
					{
						this.pitch = Matrix.CreateFromAxisAngle(this.orientation.Left, 0.015f);
						this.orientation *= this.pitch;
					}
					else if (this.orientation.Backward.Y > -0.85f && this.speed <= -0.01f)
					{
						this.pitch = Matrix.CreateFromAxisAngle(this.orientation.Right, -(this.speed / 45f));
						this.orientation *= this.pitch;
					}
				}
				else if (this.hithard == 1)
				{
					this.carRot = Matrix.CreateFromAxisAngle(this.axisdelta * (float)this.turnDir, Rover.turnSpeed);
					this.orientation *= this.carRot;
				}
				else if (this.hithard == 11)
				{
					if (this.newPosition.Y <= this.gndPosition.Y)
					{
						this.shockhit = 20;
						this.velocity = Vector3.Reflect(this.velocity, this.normal);
						this.velocity.Y = this.velocity.Y * ((float)this.rr.Next(60, 100) / 100f);
					}
					float num6 = Vector3.Distance(this.newPosition, this.position);
					num6 = MathHelper.Clamp(num6, 5f, 50f);
					this.delta.X = this.position.X - this.newPosition.X;
					this.delta.Y = 0.001f;
					this.delta.Z = this.position.Z - this.newPosition.Z;
					this.carRot = Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), num6 / 60f);
					this.orientation *= this.carRot;
				}
				else if (this.hithard == 2)
				{
					this.carRot = Matrix.CreateFromAxisAngle(this.axisdelta * (float)this.turnDir, Rover.turnSpeed);
					this.orientation *= this.carRot;
				}
				else if (this.hithard == 22)
				{
					if (this.newPosition.Y <= this.gndPosition.Y)
					{
						this.shockhit = 20;
						this.velocity = Vector3.Reflect(this.velocity, this.normal);
						this.velocity.Y = this.velocity.Y * ((float)this.rr.Next(80, 100) / 100f);
					}
					float num7 = Vector3.Distance(this.newPosition, this.position);
					num7 = MathHelper.Clamp(num7, 5f, 50f);
					this.delta.X = this.position.X - this.newPosition.X;
					this.delta.Y = 0.001f;
					this.delta.Z = this.position.Z - this.newPosition.Z;
					this.carRot = Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), num7 / 150f);
					this.orientation *= this.carRot;
				}
				Vector3 vector7 = Vector3.Transform(new Vector3(0f, 33f, 0f), this.orientation);
				this.orientation *= Matrix.CreateTranslation(vector6 - vector7);
				this.hangtime++;
				if (this.hangtime > 30)
				{
					this.incRot = 0.06f;
				}
				this.lastorientation = this.orientation;
			}
			if (this.flipflag > 1)
			{
				this.flipflag--;
				this.facingDirection += Rover.turnSpeed * (float)this.turnDir;
				if (this.facingDirection > 6.283185f)
				{
					this.facingDirection = 0f;
				}
				if (this.facingDirection < 0f)
				{
					this.facingDirection = 6.283185f;
				}
				this.orientationx = Matrix.CreateRotationY(this.facingDirection);
				this.orientationx.Up = this.normal;
				this.orientationx.Right = Vector3.Cross(this.orientationx.Forward, this.orientationx.Up);
				this.orientationx.Right = Vector3.Normalize(this.orientationx.Right);
				this.orientationx.Forward = Vector3.Cross(this.orientationx.Up, this.orientationx.Right);
				this.orientationx.Forward = Vector3.Normalize(this.orientationx.Forward);
				this.orientation = this.orientationx;
			}
			if (this.newPosition.Y <= this.gndPosition.Y + 10f || this.groundflag == 1)
			{
				if ((this.newPosition.Y - 20f <= this.gndPosition.Y || this.groundflag == 1) && this.hithard == 0)
				{
					this.rotAlign -= this.incRot;
					if (this.rotAlign < 0f)
					{
						this.rotAlign = 0f;
					}
				}
				float num8 = Vector3.Distance(this.position, this.newPosition);
				float num9 = MathHelper.Lerp(0f, 0.05f, num8 / 8f);
				float num10 = MathHelper.Lerp(0.05f, 0.01f + MathHelper.Lerp(0.06f, 0f, Rover.fric), (num8 - 8f) / 70f);
				this.spinny = 1f;
				if (this.movement.Z > 0f)
				{
					this.spinny = -1f;
				}
				if (this.flipflag == 0)
				{
					if (!this.sc.usingMouse)
					{
						if (num8 <= 8f)
						{
							this.facingDirection += this.thumb * num9 * this.spinny;
						}
						else
						{
							this.facingDirection += this.thumb * num10 * this.spinny;
						}
					}
					else if (num8 <= 8f)
					{
						this.facingDirection += this.thumb * num9 * this.spinny;
					}
					else
					{
						this.facingDirection += this.thumb * num10 * this.spinny;
					}
					if ((double)this.facingDirection > 6.283185)
					{
						this.facingDirection = 0f;
					}
					if (this.facingDirection < 0f)
					{
						this.facingDirection = 6.283185f;
					}
				}
				this.orientationx = Matrix.CreateRotationY(this.facingDirection);
				this.orientationx.Up = this.normal;
				this.orientationx.Right = Vector3.Cross(this.orientationx.Forward, this.orientationx.Up);
				this.orientationx.Right = Vector3.Normalize(this.orientationx.Right);
				this.orientationx.Forward = Vector3.Cross(this.orientationx.Up, this.orientationx.Right);
				this.orientationx.Forward = Vector3.Normalize(this.orientationx.Forward);
				if (this.rotAlign > 0f)
				{
					Vector3 vector8;
					Quaternion quaternion;
					Vector3 vector9;
					this.lastorientation.Decompose(out vector8, out quaternion, out vector9);
					Vector3 vector10;
					Quaternion quaternion2;
					Vector3 vector11;
					this.orientationx.Decompose(out vector10, out quaternion2, out vector11);
					this.orientation = Matrix.CreateFromQuaternion(Quaternion.Slerp(quaternion2, quaternion, this.rotAlign)) * Matrix.CreateTranslation(Vector3.Lerp(vector11, vector9, this.rotAlign));
				}
				else
				{
					this.orientation = this.orientationx;
				}
				if (this.movement.Z <= 0f)
				{
					if (this.movement.Z < -20f)
					{
						float num11 = MathHelper.Clamp(Math.Abs(this.movement.Z + 15f) / 30f, 0f, 1f);
						this.leanAmt += this.thumb / 25f * num11 * MathHelper.Lerp(0f, 1.2f, Rover.fric);
						this.leanAmt = MathHelper.Clamp(this.leanAmt, -0.6f, 0.6f);
						Rover.fric += Math.Abs(this.leanAmt / 30f) * num11;
						if (Rover.fric > Rover.fricOrig)
						{
							Rover.fric = Rover.fricOrig;
						}
					}
					else
					{
						Rover.fric += 0.05f;
						if (Rover.fric > Rover.fricOrig)
						{
							Rover.fric = Rover.fricOrig;
						}
					}
				}
			}
			this.fliptimer--;
			if (this.newPosition.Y <= this.gndPosition.Y || this.groundflag == 1)
			{
				if (this.fliptimer > 0)
				{
					this.newPosition.Y = this.gndPosition.Y;
					this.groundflag = 0;
					this.zspin = this.movement.Z;
				}
				else
				{
					this.jumpnow = false;
					this.groundflag = 1;
					float num12 = Vector3.Distance(this.position, this.newPosition);
					if (this.hithard > 0)
					{
						this.hithard = 0;
						this.grav = Vector3.Zero;
						this.movement.Z = 0f;
						this.velocity = new Vector3(this.velocity.X, 0f, this.velocity.Z);
						this.flipflag = 0;
						this.hangtime = -30;
						this.rotAlign = 1f;
					}
					else if (this.hangtime > 1 || this.flipflag == 1)
					{
						float num13 = Vector3.Dot(Vector3.Normalize(new Vector3(this.normal.X, this.normal.Y, this.normal.Z)), Vector3.Normalize(new Vector3(this.lastorientation.Up.X, this.lastorientation.Up.Y, this.lastorientation.Up.Z)));
						float num14 = Vector3.Dot(Vector3.Normalize(this.normal), Vector3.Normalize(this.lastorientation.Right));
						float num15 = Vector3.Dot(Vector3.Normalize(this.normal), Vector3.Normalize(this.lastorientation.Forward));
						if (num13 < 0.8f || this.flipflag == 1)
						{
							int num16;
							if (Math.Abs(num14) > Math.Abs(num15))
							{
								num16 = 1;
							}
							else
							{
								num16 = 2;
							}
							if (num16 == 1 || this.flipflag == 1)
							{
								if (this.flipflag == 0)
								{
									this.flipflag = 20;
									this.oldhangtime = this.hangtime;
									this.turnDir = 1;
									Rover.turnSpeed = (float)this.rr.Next(10, 14) / 100f;
									if (num14 > 0f)
									{
										this.turnDir = -1;
									}
									this.shockhit = this.hangtime + 10;
									this.hangtime = 0;
									this.hithard = 0;
								}
								else
								{
									this.flipflag = 0;
									this.orientation = this.orientationx;
									this.axisdelta = this.orientation.Forward;
									Rover.turnSpeed = (float)this.rr.Next(6, 18) / 100f + num12 / 350f;
									this.hithard = 1;
									this.velocity = Vector3.Reflect(this.velocity, this.normal);
									float num17 = (float)this.rr.Next(3, 7) / 10f;
									this.velocity.X = this.velocity.X * num17;
									this.velocity.Z = this.velocity.Z * num17;
									this.velocity.Y = MathHelper.Clamp(Math.Abs(this.velocity.Y), 15f, 35f);
									this.fliptimer = this.rr.Next(5, 30);
									this.grav = Vector3.Zero;
									this.groundflag = 0;
									this.zspin = this.movement.Z;
									this.hangtime = 0;
									this.oldhangtime = 0;
									this.rotAlign = 1f;
									this.incRot = 0.06f;
								}
							}
							else
							{
								this.flipflag = 0;
								this.shockhit = this.hangtime + 10;
								this.orientation = this.orientationx;
								this.axisdelta = this.orientation.Left;
								this.turnDir = -1;
								if (this.rr.Next(1, 1000) > 920)
								{
									this.turnDir = 1;
								}
								Rover.turnSpeed = (float)this.rr.Next(5, 11) / 100f + num12 / 350f;
								if (num15 < 0f)
								{
									this.turnDir = 1;
								}
								if (this.velocity.Length() < Math.Abs(Rover.max) * 0.95f)
								{
									this.hithard = 2;
									this.velocity = Vector3.Reflect(this.velocity, this.normal);
									float num18 = (float)this.rr.Next(3, 7) / 10f;
									this.velocity.X = this.velocity.X * num18;
									this.velocity.Z = this.velocity.Z * num18;
									this.velocity.Y = MathHelper.Clamp(Math.Abs(this.velocity.Y), 15f, 25f);
									this.fliptimer = this.rr.Next(5, 30);
								}
								else
								{
									this.hithard = 22;
									this.velocity = Vector3.Reflect(this.velocity, this.normal);
									float num19 = (float)this.rr.Next(3, 7) / 10f;
									this.velocity.X = this.velocity.X * num19;
									this.velocity.Z = this.velocity.Z * num19;
									this.velocity.Y = MathHelper.Clamp(Math.Abs(this.velocity.Y), 15f, 25f);
									this.fliptimer = this.rr.Next(10, 130);
								}
								this.grav = Vector3.Zero;
								this.groundflag = 0;
								this.zspin = this.movement.Z;
								this.hangtime = 0;
								this.oldhangtime = 0;
								this.rotAlign = 1f;
								this.incRot = 0.06f;
							}
						}
					}
					if (this.hithard == 0)
					{
						this.hangtime = (int)MathHelper.Min((float)this.hangtime, 0f);
						this.newPosition.Y = this.gndPosition.Y;
					}
				}
			}
			this.leanAmt *= 0.92f;
			if (Rover.fric <= Rover.fricOrig)
			{
				Rover.fric = Rover.fricOrig;
			}
			if (flag)
			{
				if (this.groundflag == 1)
				{
					this.sc.wheelRollMatrix *= Matrix.CreateRotationX(this.movement.Z / 25f);
				}
				else
				{
					this.zspin *= 0.986f;
					this.sc.wheelRollMatrix *= Matrix.CreateRotationX(this.zspin / 25f);
				}
			}
			this.sc.wheelRollMatrix2 = Matrix.CreateRotationY(this.thumb / 2.5f);
			this.sc.rackMatrix = Matrix.CreateTranslation((float)Math.Sin((double)(-(double)this.thumb / 1.8f)) * 4f, 0f, (float)(-(float)Math.Cos((double)(-(double)this.thumb / 1.8f))) * 120f + 114f);
			this.animateparts();
			float num20 = this.position.X - this.newPosition.X;
			float num21 = this.position.Z - this.newPosition.Z;
			if (Math.Abs(num20) > 148f)
			{
				this.position.X = this.position.X - (float)(Math.Sign(num20) * 148);
			}
			else
			{
				this.position.X = this.newPosition.X;
			}
			if (Math.Abs(num21) > 148f)
			{
				this.position.Z = this.position.Z - (float)(Math.Sign(num21) * 148);
			}
			else
			{
				this.position.Z = this.newPosition.Z;
			}
			this.position.Y = this.newPosition.Y;
			this.pp = new Vector3(this.position.X, this.position.Y + MathHelper.Lerp(0f, 20f, Math.Abs(this.leanAmt)), this.position.Z);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x001C7284 File Offset: 0x001C5484
		public void Draw(Matrix viewMatrix, Matrix projectionMatrix, Vector3 sundir)
		{
			this.sc.BackWheelBone.Transform = this.sc.wheelRollMatrix * this.sc.BackWheelTrans;
			this.sc.leftFrontjointBone.Transform = this.sc.wheelRollMatrix2 * this.sc.leftFrontjointTrans;
			this.sc.rightFrontjointBone.Transform = this.sc.wheelRollMatrix2 * this.sc.rightFrontjointTrans;
			this.sc.leftFrontWheelBone.Transform = this.sc.wheelRollMatrix * this.sc.leftFrontWheelTrans;
			this.sc.rightFrontWheelBone.Transform = this.sc.wheelRollMatrix * this.sc.rightFrontWheelTrans;
			this.sc.rackBone.Transform = this.sc.rackMatrix * this.sc.rackTrans;
			this.sc.roverModel.CopyAbsoluteBoneTransformsTo(this.boneTransforms);
			this.pp = new Vector3(this.position.X, this.position.Y + MathHelper.Lerp(0f, 20f, Math.Abs(this.leanAmt)), this.position.Z);
			this.smallWorld = this.orientation * Matrix.CreateFromAxisAngle(this.orientation.Forward, this.leanAmt) * Matrix.CreateTranslation(this.pp);
			Matrix matrix = Matrix.CreateScale(1.5f) * this.smallWorld;
			for (int i = 0; i < this.sc.roverparts.Count; i++)
			{
				ModelMesh modelMesh = this.sc.roverModel.Meshes[this.sc.roverparts[i]];
				int num = 1;
				if (this.dashcam == 1 && (modelMesh.Name == "body" || this.scoop1 == 1 || modelMesh.Name.Contains("weapon")))
				{
					num = 0;
				}
				if (num == 1)
				{
					foreach (Effect effect in modelMesh.Effects)
					{
						BasicEffect basicEffect = (BasicEffect)effect;
						basicEffect.World = this.boneTransforms[modelMesh.ParentBone.Index] * matrix;
						basicEffect.Alpha = 1f;
						basicEffect.View = viewMatrix;
						basicEffect.Projection = projectionMatrix;
						basicEffect.LightingEnabled = true;
						basicEffect.PreferPerPixelLighting = false;
						basicEffect.DirectionalLight0.Enabled = true;
						basicEffect.AmbientLightColor = this.amb;
						basicEffect.DirectionalLight0.Direction = sundir;
						basicEffect.DirectionalLight0.DiffuseColor = this.diffu;
					}
					modelMesh.Draw();
				}
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x001C75A8 File Offset: 0x001C57A8
		private void animateparts()
		{
			if (this.scoopflag == 1)
			{
				this.scoopflag = 0;
				if (this.scoop1 == 1)
				{
					this.scoop1 = 2;
					this.scoopx = 1f;
				}
				return;
			}
			if (this.scoopflag == 2)
			{
				this.scoopflag = 0;
				if (this.scoop1 == 100)
				{
					this.scoop1 = 4;
					this.scoopx = 0f;
				}
				return;
			}
			if (this.scoop1 == 2)
			{
				this.scoopx -= 0.05f;
				if (this.scoopx <= 0f)
				{
					this.scoopx = 0f;
					this.scoop1 = 100;
					this.scooperON = true;
				}
				this.sc.scooperMatrix = Matrix.CreateRotationX(MathHelper.Lerp(-0.44f, 0f, this.scoopx)) * Matrix.CreateTranslation(0f, MathHelper.Lerp(25.98f, 21.825f, this.scoopx), MathHelper.Lerp(-26.1f, 8.95f, this.scoopx));
				this.sc.scooperBone.Transform = this.sc.scooperMatrix * this.sc.scooperTrans;
				return;
			}
			if (this.scoop1 == 4)
			{
				this.scoopx += 0.05f;
				if (this.scoopx >= 1f)
				{
					this.scoopx = 1f;
					this.scoop1 = 1;
					this.scooperON = false;
				}
				this.sc.scooperMatrix = Matrix.CreateRotationX(MathHelper.Lerp(-0.44f, 0f, this.scoopx)) * Matrix.CreateTranslation(0f, MathHelper.Lerp(25.98f, 21.825f, this.scoopx), MathHelper.Lerp(-26.1f, 8.95f, this.scoopx));
				this.sc.scooperBone.Transform = this.sc.scooperMatrix * this.sc.scooperTrans;
				return;
			}
			if (this.solarflag == 1)
			{
				this.solarflag = 0;
				if (this.solar1 == 1)
				{
					this.solar1 = 2;
					this.solarx = 0f;
					this.solary = 0f;
				}
				return;
			}
			if (this.solarflag == 2)
			{
				this.solarflag = 0;
				if (this.solar1 == 100)
				{
					this.solar1 = 4;
					this.solarx = 4f;
					this.solary = -160f;
				}
				return;
			}
			if (this.solar1 == 2)
			{
				this.solarx += 0.2f;
				this.sc.solar1aMatrix = Matrix.CreateTranslation(this.solarx, 0f, 0f);
				this.sc.solar1aBone.Transform = this.sc.solar1aMatrix * this.sc.solar1aTrans;
				if (this.solarx > 4f)
				{
					this.solar1 = 3;
					this.solary = 0f;
				}
				return;
			}
			if (this.solar1 == 3)
			{
				this.solary -= 2f;
				this.sc.solar1bMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(this.solary));
				this.sc.solar1bBone.Transform = this.sc.solar1bMatrix * this.sc.solar1bTrans;
				if (this.solary < -160f)
				{
					this.solar1 = 100;
				}
				return;
			}
			if (this.solar1 == 4)
			{
				this.solary += 4f;
				this.sc.solar1bMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(this.solary));
				this.sc.solar1bBone.Transform = this.sc.solar1bMatrix * this.sc.solar1bTrans;
				if (this.solary >= 0f)
				{
					this.solar1 = 5;
					this.solarx = 4f;
				}
				return;
			}
			if (this.solar1 == 5)
			{
				this.solarx -= 0.6f;
				this.sc.solar1aMatrix = Matrix.CreateTranslation(this.solarx, 0f, 0f);
				this.sc.solar1aBone.Transform = this.sc.solar1aMatrix * this.sc.solar1aTrans;
				if (this.solarx <= 0f)
				{
					this.solar1 = 1;
				}
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x001C7A14 File Offset: 0x001C5C14
		public void hitramp(int[,] heights, Vector3[,] normals, ref int onramp, ref Vector3 position, ref Vector3 newPosition, ref Vector3 gndPosition, ref Vector3 normal)
		{
			if (onramp == 0 && Vector3.Distance(position, new Vector3(this.region1[0], this.region1[1], this.region1[2])) > 200f)
			{
				this.GetHeightAndNormalX(ref heights, ref normals, newPosition, out gndPosition.Y, out normal);
				return;
			}
			this.rampSpline = 0f;
			this.myside = 0;
			if (onramp < 2)
			{
				float num = 0f;
				Vector2 vector = new Vector2(this.region1[9], this.region1[11]);
				Vector2 vector2 = Vector2.Zero;
				Vector2 vector3 = new Vector2(position.X + this.velocity.X, position.Z + this.velocity.Z);
				this.alfa = 0f;
				for (int i = 0; i < 12; i += 3)
				{
					vector2 = vector;
					vector = new Vector2(this.region1[i], this.region1[i + 2]);
					Vector2 vector4 = vector3 - vector2;
					Vector2 vector5 = vector3 - vector;
					float num2 = vector4.X * vector5.Y - vector5.X * vector4.Y;
					float num3 = (float)Math.Acos((double)(Vector2.Dot(vector4, vector5) / (vector4.Length() * vector5.Length())));
					num3 = ((num2 > 0f) ? num3 : (-num3));
					if (Math.Abs(num3) > num)
					{
						num = Math.Abs(num3);
						this.myside = i / 3;
					}
					if (i > 0)
					{
						this.alfa += num3;
					}
				}
				if (Math.Abs(this.alfa) >= 3.1415927f && onramp == 0)
				{
					if (this.myside != 1)
					{
						newPosition.X = position.X - this.velocity.X;
						newPosition.Y = position.Y - this.velocity.Y;
						newPosition.Z = position.Z - this.velocity.Z;
						this.velocity.X = -this.velocity.X;
						this.velocity.Z = -this.velocity.Z;
						this.velocity.Y = -this.velocity.Y;
						this.movement /= 2f;
						this.grav = this.velocity;
					}
					else
					{
						onramp = 1;
					}
				}
				if (Math.Abs(this.alfa) < 3.1415927f && onramp == 1)
				{
					if (this.myside != 1)
					{
						Vector3 vector6 = Vector3.Zero;
						if (this.myside == 2)
						{
							vector6 = new Vector3(this.region1[3], this.region1[4], this.region1[5]) - new Vector3(this.region1[0], this.region1[1], this.region1[2]);
						}
						if (this.myside == 0)
						{
							vector6 = new Vector3(this.region1[0], this.region1[1], this.region1[2]) - new Vector3(this.region1[3], this.region1[4], this.region1[5]);
						}
						if (this.myside == 3)
						{
							onramp = 2;
						}
						else
						{
							this.movement /= 2f;
							newPosition.X = position.X - this.velocity.X;
							newPosition.Y = position.Y - this.velocity.Y;
							newPosition.Z = position.Z - this.velocity.Z;
							vector6 = Vector3.Normalize(vector6);
							this.velocity = Vector3.Reflect(this.velocity, vector6) / 1f;
							this.grav = this.velocity;
						}
						this.grav.Y = 0f;
					}
					else
					{
						onramp = 0;
					}
				}
				this.rampNormal = new Vector3(this.region1[12], this.region1[13], this.region1[14]);
				Vector3 vector7 = new Vector3(this.region1[0], this.region1[1], this.region1[2]);
				Vector3 vector8 = new Vector3(this.region1[3], this.region1[4], this.region1[5]);
				Vector3 vector9 = new Vector3(this.region1[6], this.region1[7], this.region1[8]);
				Vector3 vector10 = new Vector3(this.region1[9], this.region1[10], this.region1[11]);
				Vector3 vector11 = new Vector3(position.X + this.velocity.X, position.Y + this.velocity.Y, position.Z + this.velocity.Z);
				float num4 = Vector3.Distance(vector7, vector8);
				float num5 = Vector3.Distance(vector8, vector11);
				float num6 = Vector3.Distance(vector7, vector11);
				float num7 = Vector3.Distance(vector8, vector9);
				float num8 = Vector3.Distance(vector7, vector10);
				float num9 = Vector3.Distance(vector9, vector11);
				float num10 = Vector3.Distance(vector10, vector11);
				float num11 = Vector3.Distance(vector9, vector10);
				float num12 = (num6 * num6 + num5 * num5 - num4 * num4) / (2f * num6 * num5);
				float num13 = Math.Abs((float)Math.Acos((double)num12));
				if ((double)num13 >= 1.5)
				{
					this.rampSpline = (num13 - 1.5f) / 1.6415927f;
				}
				num12 = (num9 * num9 + num10 * num10 - num11 * num11) / (2f * num9 * num10);
				float num14 = Math.Abs((float)Math.Acos((double)num12));
				if (num14 >= 2f)
				{
					this.rampSpline = (num14 - 2f) / 1.1415927f;
				}
				num12 = (num4 * num4 + num5 * num5 - num6 * num6) / (2f * num4 * num5);
				num13 = (float)Math.Acos((double)num12);
				float num15 = (float)Math.Sin((double)num13) * num5;
				float num16 = num15 / num7;
				float num17 = num15 / num8;
				Vector3 vector12 = Vector3.Lerp(vector8, vector9, num16);
				Vector3 vector13 = Vector3.Lerp(vector7, vector10, num17);
				num12 = (num7 * num7 + num9 * num9 - num5 * num5) / (2f * num7 * num9);
				float num18 = (float)Math.Acos((double)num12);
				float num19 = (float)Math.Sin((double)num18) * num9;
				float num20 = num19 / Vector3.Distance(vector12, vector13);
				this.rampHeight = MathHelper.Lerp(vector12.Y, vector13.Y, num20);
				if (onramp == 0)
				{
					this.rampHeight = (vector7.Y + vector8.Y) / 2f;
				}
				if (this.rampSpline > 0f && onramp == 1)
				{
					this.GetHeightAndNormalX(ref heights, ref normals, newPosition, out gndPosition.Y, out normal);
					gndPosition.Y = (vector7.Y + vector8.Y) / 2f;
				}
				if (onramp == 1 && num14 >= 2f)
				{
					normal = new Vector3(this.region2[12], this.region2[13], this.region2[14]);
					gndPosition.Y = MathHelper.Lerp(vector9.Y, vector10.Y, num20);
				}
				if (onramp == 1)
				{
					this.grav = (Vector3.Lerp(vector12, vector13, 0.5f) - vector11) / 30f;
				}
			}
			if (onramp == 2)
			{
				float num21 = 0f;
				Vector2 vector14 = new Vector2(this.region2[9], this.region2[11]);
				Vector2 vector15 = Vector2.Zero;
				Vector2 vector16 = new Vector2(position.X + this.velocity.X, position.Z + this.velocity.Z);
				this.alfa = 0f;
				for (int j = 0; j < 12; j += 3)
				{
					vector15 = vector14;
					vector14 = new Vector2(this.region2[j], this.region2[j + 2]);
					Vector2 vector17 = vector16 - vector15;
					Vector2 vector18 = vector16 - vector14;
					float num22 = vector17.X * vector18.Y - vector18.X * vector17.Y;
					float num23 = (float)Math.Acos((double)(Vector2.Dot(vector17, vector18) / (vector17.Length() * vector18.Length())));
					num23 = ((num22 > 0f) ? num23 : (-num23));
					if (Math.Abs(num23) > num21)
					{
						num21 = Math.Abs(num23);
						this.myside = j / 3;
					}
					if (j > 0)
					{
						this.alfa += num23;
					}
				}
				if (Math.Abs(this.alfa) < 3.1415927f)
				{
					onramp = this.myside;
					Vector3 vector19 = Vector3.Zero;
					if (this.myside == 2)
					{
						vector19 = new Vector3(this.region2[3], this.region2[4], this.region2[5]) - new Vector3(this.region2[0], this.region2[1], this.region2[2]);
					}
					if (this.myside == 0)
					{
						vector19 = new Vector3(this.region2[0], this.region2[1], this.region2[2]) - new Vector3(this.region2[3], this.region2[4], this.region2[5]);
					}
					if (this.myside == 2 || this.myside == 0)
					{
						newPosition.X = position.X - this.velocity.X;
						newPosition.Y = position.Y - this.velocity.Y;
						newPosition.Z = position.Z - this.velocity.Z;
						this.movement = Vector3.Zero;
						vector19 = Vector3.Normalize(vector19);
						this.velocity = Vector3.Reflect(this.velocity, vector19) / 1f;
						this.grav = this.velocity;
						onramp = 2;
					}
				}
				this.grav.Y = 0f;
				this.rampNormal = new Vector3(this.region2[12], this.region2[13], this.region2[14]);
				Vector3 vector20 = new Vector3(this.region2[0], this.region2[1], this.region2[2]);
				Vector3 vector21 = new Vector3(this.region2[3], this.region2[4], this.region2[5]);
				Vector3 vector22 = new Vector3(this.region2[6], this.region2[7], this.region2[8]);
				Vector3 vector23 = new Vector3(this.region2[9], this.region2[10], this.region2[11]);
				Vector3 vector24 = new Vector3(position.X + this.velocity.X, position.Y + this.velocity.Y, position.Z + this.velocity.Z);
				float num24 = Vector3.Distance(vector20, vector21);
				float num25 = Vector3.Distance(vector21, vector24);
				float num26 = Vector3.Distance(vector20, vector24);
				float num27 = Vector3.Distance(vector21, vector22);
				float num28 = Vector3.Distance(vector20, vector23);
				float num29 = Vector3.Distance(vector22, vector24);
				float num30 = Vector3.Distance(vector23, vector24);
				float num31 = Vector3.Distance(vector22, vector23);
				float num32 = (num26 * num26 + num25 * num25 - num24 * num24) / (2f * num26 * num25);
				float num33 = Math.Abs((float)Math.Acos((double)num32));
				if (num33 >= 2f)
				{
					this.rampSpline = (num33 - 2f) / 1.1415927f;
				}
				num32 = (num29 * num29 + num30 * num30 - num31 * num31) / (2f * num29 * num30);
				float num34 = Math.Abs((float)Math.Acos((double)num32));
				if ((double)num34 >= 2.5)
				{
					this.rampSpline = (num34 - 2.5f) / 0.64159274f;
				}
				num32 = (num24 * num24 + num25 * num25 - num26 * num26) / (2f * num24 * num25);
				num33 = (float)Math.Acos((double)num32);
				float num35 = (float)Math.Sin((double)num33) * num25;
				float num36 = num35 / num27;
				float num37 = num35 / num28;
				Vector3 vector25 = Vector3.Lerp(vector21, vector22, num36);
				Vector3 vector26 = Vector3.Lerp(vector20, vector23, num37);
				num32 = (num27 * num27 + num29 * num29 - num25 * num25) / (2f * num27 * num29);
				float num38 = (float)Math.Acos((double)num32);
				float num39 = (float)Math.Sin((double)num38) * num29;
				float num40 = num39 / Vector3.Distance(vector25, vector26);
				this.rampHeight = MathHelper.Lerp(vector25.Y, vector26.Y, num40);
				if (onramp == 2)
				{
					normal = new Vector3(this.region1[12], this.region1[13], this.region1[14]);
					gndPosition.Y = MathHelper.Lerp(vector21.Y, vector20.Y, num40);
				}
				if (onramp == 2 && (double)num34 >= 2.5)
				{
					normal = new Vector3(this.region3[12], this.region3[13], this.region3[14]);
					gndPosition.Y = MathHelper.Lerp(vector22.Y, vector23.Y, num40);
				}
				if (onramp == 1)
				{
					this.rampNormal = new Vector3(this.region1[12], this.region1[13], this.region1[14]);
					normal = new Vector3(this.region2[12], this.region2[13], this.region2[14]);
					gndPosition.Y = MathHelper.Lerp(vector21.Y, vector20.Y, num40);
				}
				this.grav = (Vector3.Lerp(vector25, vector26, 0.5f) - vector24) / 20f;
			}
			if (onramp == 3)
			{
				float num41 = 0f;
				Vector2 vector27 = new Vector2(this.region3[9], this.region3[11]);
				Vector2 vector28 = Vector2.Zero;
				Vector2 vector29 = new Vector2(position.X + this.velocity.X, position.Z + this.velocity.Z);
				this.alfa = 0f;
				for (int k = 0; k < 12; k += 3)
				{
					vector28 = vector27;
					vector27 = new Vector2(this.region3[k], this.region3[k + 2]);
					Vector2 vector30 = vector29 - vector28;
					Vector2 vector31 = vector29 - vector27;
					float num42 = vector30.X * vector31.Y - vector31.X * vector30.Y;
					float num43 = (float)Math.Acos((double)(Vector2.Dot(vector30, vector31) / (vector30.Length() * vector31.Length())));
					num43 = ((num42 > 0f) ? num43 : (-num43));
					if (Math.Abs(num43) > num41)
					{
						num41 = Math.Abs(num43);
						this.myside = k / 3;
					}
					if (k > 0)
					{
						this.alfa += num43;
					}
				}
				if (Math.Abs(this.alfa) < 3.1415927f)
				{
					if (this.myside != 1)
					{
						this.movement = Vector3.Zero;
						Vector3 vector32 = Vector3.Zero;
						if (this.myside == 2)
						{
							vector32 = new Vector3(this.region3[3], this.region3[4], this.region3[5]) - new Vector3(this.region3[0], this.region3[1], this.region3[2]);
						}
						if (this.myside == 0)
						{
							vector32 = new Vector3(this.region3[0], this.region3[1], this.region3[2]) - new Vector3(this.region3[3], this.region3[4], this.region3[5]);
						}
						if (this.myside == 3)
						{
							newPosition.X = position.X - this.velocity.X;
							newPosition.Y = position.Y - this.velocity.Y;
							newPosition.Z = position.Z - this.velocity.Z;
							this.velocity = Vector3.Zero;
							this.grav = this.velocity;
							this.camSwitch = 1;
						}
						else
						{
							newPosition.X = position.X - this.velocity.X;
							newPosition.Y = position.Y - this.velocity.Y;
							newPosition.Z = position.Z - this.velocity.Z;
							vector32 = Vector3.Normalize(vector32);
							this.velocity = Vector3.Reflect(this.velocity, vector32) / 1f;
							this.grav = this.velocity;
						}
					}
					else
					{
						onramp = 2;
					}
				}
				this.grav.Y = 0f;
				this.rampNormal = new Vector3(this.region3[12], this.region3[13], this.region3[14]);
				Vector3 vector33 = new Vector3(this.region3[0], this.region3[1], this.region3[2]);
				Vector3 vector34 = new Vector3(this.region3[3], this.region3[4], this.region3[5]);
				Vector3 vector35 = new Vector3(this.region3[6], this.region3[7], this.region3[8]);
				Vector3 vector36 = new Vector3(this.region3[9], this.region3[10], this.region3[11]);
				Vector3 vector37 = new Vector3(position.X + this.velocity.X, position.Y + this.velocity.Y, position.Z + this.velocity.Z);
				float num44 = Vector3.Distance(vector33, vector34);
				float num45 = Vector3.Distance(vector34, vector37);
				float num46 = Vector3.Distance(vector33, vector37);
				float num47 = Vector3.Distance(vector34, vector35);
				float num48 = Vector3.Distance(vector33, vector36);
				float num49 = Vector3.Distance(vector35, vector37);
				float num50 = (num46 * num46 + num45 * num45 - num44 * num44) / (2f * num46 * num45);
				float num51 = Math.Abs((float)Math.Acos((double)num50));
				if ((double)num51 >= 2.5)
				{
					this.rampSpline = (num51 - 2.5f) / 0.64159274f;
				}
				num50 = (num44 * num44 + num45 * num45 - num46 * num46) / (2f * num44 * num45);
				num51 = (float)Math.Acos((double)num50);
				float num52 = (float)Math.Sin((double)num51) * num45;
				float num53 = num52 / num47;
				float num54 = num52 / num48;
				Vector3 vector38 = Vector3.Lerp(vector34, vector35, num53);
				Vector3 vector39 = Vector3.Lerp(vector33, vector36, num54);
				num50 = (num47 * num47 + num49 * num49 - num45 * num45) / (2f * num47 * num49);
				float num55 = (float)Math.Acos((double)num50);
				float num56 = (float)Math.Sin((double)num55) * num49;
				float num57 = num56 / Vector3.Distance(vector38, vector39);
				this.rampHeight = MathHelper.Lerp(vector38.Y, vector39.Y, num57);
				normal = new Vector3(this.region2[12], this.region2[13], this.region2[14]);
				gndPosition.Y = MathHelper.Lerp(vector34.Y, vector33.Y, num57);
				this.grav = (Vector3.Lerp(vector38, vector39, 0.5f) - vector37) / 20f;
				if (onramp == 2)
				{
					this.rampNormal = new Vector3(this.region2[12], this.region2[13], this.region2[14]);
					normal = new Vector3(this.region3[12], this.region3[13], this.region3[14]);
					gndPosition.Y = MathHelper.Lerp(vector34.Y, vector33.Y, num57);
				}
			}
			if (onramp > 0)
			{
				normal = Vector3.Lerp(this.rampNormal, normal, this.rampSpline * 0.5f);
				normal.Normalize();
				gndPosition.Y = MathHelper.Lerp(this.rampHeight, gndPosition.Y + 0f, this.rampSpline / 2f);
				float num58 = 1f;
				if (Math.Abs(this.facingDirection - this.directme) > 3.14f)
				{
					num58 = -1f;
				}
				if (this.facingDirection < this.directme)
				{
					this.facingDirection += 0.03f * num58;
				}
				if (this.facingDirection > this.directme)
				{
					this.facingDirection -= 0.03f * num58;
				}
				this.grav.Y = 0f;
			}
			if (onramp == 0)
			{
				this.GetHeightAndNormalX(ref heights, ref normals, newPosition, out gndPosition.Y, out normal);
				if (this.rampSpline > 0f)
				{
					normal = Vector3.Lerp(normal, this.rampNormal, this.rampSpline * 0.5f);
					gndPosition.Y = MathHelper.Lerp(gndPosition.Y, this.rampHeight + 0f, this.rampSpline / 2f);
					normal.Normalize();
					this.grav.Y = 0f;
				}
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x001C8F90 File Offset: 0x001C7190
		public void hitramp2(int[,] heights, Vector3[,] normals, ref int onramp, ref Vector3 position, ref Vector3 newPosition, ref Vector3 gndPosition, ref Vector3 normal, ref Vector3 velocity)
		{
			if (onramp == 0 && Vector3.Distance(position, new Vector3(this.region1[0], this.region1[1], this.region1[2])) > 200f)
			{
				this.GetHeightAndNormalX(ref heights, ref normals, newPosition, out gndPosition.Y, out normal);
				return;
			}
			this.rampSpline = 0f;
			this.myside = 0;
			if (onramp < 2)
			{
				float num = 0f;
				Vector2 vector = new Vector2(this.region1[9], this.region1[11]);
				Vector2 vector2 = Vector2.Zero;
				Vector2 vector3 = new Vector2(position.X + velocity.X, position.Z + velocity.Z);
				this.alfa = 0f;
				for (int i = 0; i < 12; i += 3)
				{
					vector2 = vector;
					vector = new Vector2(this.region1[i], this.region1[i + 2]);
					Vector2 vector4 = vector3 - vector2;
					Vector2 vector5 = vector3 - vector;
					float num2 = vector4.X * vector5.Y - vector5.X * vector4.Y;
					float num3 = (float)Math.Acos((double)(Vector2.Dot(vector4, vector5) / (vector4.Length() * vector5.Length())));
					num3 = ((num2 > 0f) ? num3 : (-num3));
					if (Math.Abs(num3) > num)
					{
						num = Math.Abs(num3);
						this.myside = i / 3;
					}
					if (i > 0)
					{
						this.alfa += num3;
					}
				}
				if (Math.Abs(this.alfa) >= 3.1415927f && onramp == 0)
				{
					if (this.myside != 1)
					{
						newPosition.X = position.X - velocity.X;
						newPosition.Y = position.Y - velocity.Y;
						newPosition.Z = position.Z - velocity.Z;
						velocity.X = -velocity.X;
						velocity.Z = -velocity.Z;
						velocity.Y = -velocity.Y;
					}
					else
					{
						onramp = 1;
					}
				}
				if (Math.Abs(this.alfa) < 3.1415927f && onramp == 1)
				{
					if (this.myside != 1)
					{
						Vector3 vector6 = Vector3.Zero;
						if (this.myside == 2)
						{
							vector6 = new Vector3(this.region1[3], this.region1[4], this.region1[5]) - new Vector3(this.region1[0], this.region1[1], this.region1[2]);
						}
						if (this.myside == 0)
						{
							vector6 = new Vector3(this.region1[0], this.region1[1], this.region1[2]) - new Vector3(this.region1[3], this.region1[4], this.region1[5]);
						}
						if (this.myside == 3)
						{
							onramp = 2;
						}
						else
						{
							this.movement /= 2f;
							newPosition.X = position.X - velocity.X;
							newPosition.Y = position.Y - velocity.Y;
							newPosition.Z = position.Z - velocity.Z;
							vector6 = Vector3.Normalize(vector6);
							velocity = Vector3.Reflect(velocity, vector6) / 1.5f;
						}
					}
					else
					{
						onramp = 0;
					}
				}
				this.rampNormal = new Vector3(this.region1[12], this.region1[13], this.region1[14]);
				Vector3 vector7 = new Vector3(this.region1[0], this.region1[1], this.region1[2]);
				Vector3 vector8 = new Vector3(this.region1[3], this.region1[4], this.region1[5]);
				Vector3 vector9 = new Vector3(this.region1[6], this.region1[7], this.region1[8]);
				Vector3 vector10 = new Vector3(this.region1[9], this.region1[10], this.region1[11]);
				Vector3 vector11 = new Vector3(position.X + velocity.X, position.Y + velocity.Y, position.Z + velocity.Z);
				float num4 = Vector3.Distance(vector7, vector8);
				float num5 = Vector3.Distance(vector8, vector11);
				float num6 = Vector3.Distance(vector7, vector11);
				float num7 = Vector3.Distance(vector8, vector9);
				float num8 = Vector3.Distance(vector7, vector10);
				float num9 = Vector3.Distance(vector9, vector11);
				float num10 = Vector3.Distance(vector10, vector11);
				float num11 = Vector3.Distance(vector9, vector10);
				float num12 = (num6 * num6 + num5 * num5 - num4 * num4) / (2f * num6 * num5);
				float num13 = Math.Abs((float)Math.Acos((double)num12));
				if ((double)num13 >= 1.5)
				{
					this.rampSpline = (num13 - 1.5f) / 1.6415927f;
				}
				num12 = (num9 * num9 + num10 * num10 - num11 * num11) / (2f * num9 * num10);
				float num14 = Math.Abs((float)Math.Acos((double)num12));
				if (num14 >= 2f)
				{
					this.rampSpline = (num14 - 2f) / 1.1415927f;
				}
				num12 = (num4 * num4 + num5 * num5 - num6 * num6) / (2f * num4 * num5);
				num13 = (float)Math.Acos((double)num12);
				float num15 = (float)Math.Sin((double)num13) * num5;
				float num16 = num15 / num7;
				float num17 = num15 / num8;
				Vector3 vector12 = Vector3.Lerp(vector8, vector9, num16);
				Vector3 vector13 = Vector3.Lerp(vector7, vector10, num17);
				num12 = (num7 * num7 + num9 * num9 - num5 * num5) / (2f * num7 * num9);
				float num18 = (float)Math.Acos((double)num12);
				float num19 = (float)Math.Sin((double)num18) * num9;
				float num20 = num19 / Vector3.Distance(vector12, vector13);
				this.rampHeight = MathHelper.Lerp(vector12.Y, vector13.Y, num20);
				if (onramp == 0)
				{
					this.rampHeight = (vector7.Y + vector8.Y) / 2f;
				}
				if (this.rampSpline > 0f && onramp == 1)
				{
					this.GetHeightAndNormalX(ref heights, ref normals, newPosition, out gndPosition.Y, out normal);
					gndPosition.Y = (vector7.Y + vector8.Y) / 2f;
				}
				if (onramp == 1 && num14 >= 2f)
				{
					normal = new Vector3(this.region2[12], this.region2[13], this.region2[14]);
					gndPosition.Y = MathHelper.Lerp(vector9.Y, vector10.Y, num20);
				}
				if (onramp == 1)
				{
					this.grav = (Vector3.Lerp(vector12, vector13, 0.5f) - vector11) / 30f;
				}
			}
			if (onramp == 2)
			{
				float num21 = 0f;
				Vector2 vector14 = new Vector2(this.region2[9], this.region2[11]);
				Vector2 vector15 = Vector2.Zero;
				Vector2 vector16 = new Vector2(position.X + velocity.X, position.Z + velocity.Z);
				this.alfa = 0f;
				for (int j = 0; j < 12; j += 3)
				{
					vector15 = vector14;
					vector14 = new Vector2(this.region2[j], this.region2[j + 2]);
					Vector2 vector17 = vector16 - vector15;
					Vector2 vector18 = vector16 - vector14;
					float num22 = vector17.X * vector18.Y - vector18.X * vector17.Y;
					float num23 = (float)Math.Acos((double)(Vector2.Dot(vector17, vector18) / (vector17.Length() * vector18.Length())));
					num23 = ((num22 > 0f) ? num23 : (-num23));
					if (Math.Abs(num23) > num21)
					{
						num21 = Math.Abs(num23);
						this.myside = j / 3;
					}
					if (j > 0)
					{
						this.alfa += num23;
					}
				}
				if (Math.Abs(this.alfa) < 3.1415927f)
				{
					onramp = this.myside;
					Vector3 vector19 = Vector3.Zero;
					if (this.myside == 2)
					{
						vector19 = new Vector3(this.region2[3], this.region2[4], this.region2[5]) - new Vector3(this.region2[0], this.region2[1], this.region2[2]);
					}
					if (this.myside == 0)
					{
						vector19 = new Vector3(this.region2[0], this.region2[1], this.region2[2]) - new Vector3(this.region2[3], this.region2[4], this.region2[5]);
					}
					if (this.myside == 2 || this.myside == 0)
					{
						newPosition.X = position.X - velocity.X;
						newPosition.Y = position.Y - velocity.Y;
						newPosition.Z = position.Z - velocity.Z;
						vector19 = Vector3.Normalize(vector19);
						velocity = Vector3.Reflect(velocity, vector19) / 2f;
						onramp = 2;
					}
				}
				this.rampNormal = new Vector3(this.region2[12], this.region2[13], this.region2[14]);
				Vector3 vector20 = new Vector3(this.region2[0], this.region2[1], this.region2[2]);
				Vector3 vector21 = new Vector3(this.region2[3], this.region2[4], this.region2[5]);
				Vector3 vector22 = new Vector3(this.region2[6], this.region2[7], this.region2[8]);
				Vector3 vector23 = new Vector3(this.region2[9], this.region2[10], this.region2[11]);
				Vector3 vector24 = new Vector3(position.X + velocity.X, position.Y + velocity.Y, position.Z + velocity.Z);
				float num24 = Vector3.Distance(vector20, vector21);
				float num25 = Vector3.Distance(vector21, vector24);
				float num26 = Vector3.Distance(vector20, vector24);
				float num27 = Vector3.Distance(vector21, vector22);
				float num28 = Vector3.Distance(vector20, vector23);
				float num29 = Vector3.Distance(vector22, vector24);
				float num30 = Vector3.Distance(vector23, vector24);
				float num31 = Vector3.Distance(vector22, vector23);
				float num32 = (num26 * num26 + num25 * num25 - num24 * num24) / (2f * num26 * num25);
				float num33 = Math.Abs((float)Math.Acos((double)num32));
				if (num33 >= 2f)
				{
					this.rampSpline = (num33 - 2f) / 1.1415927f;
				}
				num32 = (num29 * num29 + num30 * num30 - num31 * num31) / (2f * num29 * num30);
				float num34 = Math.Abs((float)Math.Acos((double)num32));
				if ((double)num34 >= 2.5)
				{
					this.rampSpline = (num34 - 2.5f) / 0.64159274f;
				}
				num32 = (num24 * num24 + num25 * num25 - num26 * num26) / (2f * num24 * num25);
				num33 = (float)Math.Acos((double)num32);
				float num35 = (float)Math.Sin((double)num33) * num25;
				float num36 = num35 / num27;
				float num37 = num35 / num28;
				Vector3 vector25 = Vector3.Lerp(vector21, vector22, num36);
				Vector3 vector26 = Vector3.Lerp(vector20, vector23, num37);
				num32 = (num27 * num27 + num29 * num29 - num25 * num25) / (2f * num27 * num29);
				float num38 = (float)Math.Acos((double)num32);
				float num39 = (float)Math.Sin((double)num38) * num29;
				float num40 = num39 / Vector3.Distance(vector25, vector26);
				this.rampHeight = MathHelper.Lerp(vector25.Y, vector26.Y, num40);
				if (onramp == 2)
				{
					normal = new Vector3(this.region1[12], this.region1[13], this.region1[14]);
					gndPosition.Y = MathHelper.Lerp(vector21.Y, vector20.Y, num40);
				}
				if (onramp == 2 && (double)num34 >= 2.5)
				{
					normal = new Vector3(this.region3[12], this.region3[13], this.region3[14]);
					gndPosition.Y = MathHelper.Lerp(vector22.Y, vector23.Y, num40);
				}
				if (onramp == 1)
				{
					this.rampNormal = new Vector3(this.region1[12], this.region1[13], this.region1[14]);
					normal = new Vector3(this.region2[12], this.region2[13], this.region2[14]);
					gndPosition.Y = MathHelper.Lerp(vector21.Y, vector20.Y, num40);
				}
			}
			if (onramp == 3)
			{
				float num41 = 0f;
				Vector2 vector27 = new Vector2(this.region3[9], this.region3[11]);
				Vector2 vector28 = Vector2.Zero;
				Vector2 vector29 = new Vector2(position.X + velocity.X, position.Z + velocity.Z);
				this.alfa = 0f;
				for (int k = 0; k < 12; k += 3)
				{
					vector28 = vector27;
					vector27 = new Vector2(this.region3[k], this.region3[k + 2]);
					Vector2 vector30 = vector29 - vector28;
					Vector2 vector31 = vector29 - vector27;
					float num42 = vector30.X * vector31.Y - vector31.X * vector30.Y;
					float num43 = (float)Math.Acos((double)(Vector2.Dot(vector30, vector31) / (vector30.Length() * vector31.Length())));
					num43 = ((num42 > 0f) ? num43 : (-num43));
					if (Math.Abs(num43) > num41)
					{
						num41 = Math.Abs(num43);
						this.myside = k / 3;
					}
					if (k > 0)
					{
						this.alfa += num43;
					}
				}
				if (Math.Abs(this.alfa) < 3.1415927f)
				{
					if (this.myside != 1)
					{
						this.movement = Vector3.Zero;
						Vector3 vector32 = Vector3.Zero;
						if (this.myside == 2)
						{
							vector32 = new Vector3(this.region3[3], this.region3[4], this.region3[5]) - new Vector3(this.region3[0], this.region3[1], this.region3[2]);
						}
						if (this.myside == 0)
						{
							vector32 = new Vector3(this.region3[0], this.region3[1], this.region3[2]) - new Vector3(this.region3[3], this.region3[4], this.region3[5]);
						}
						if (this.myside == 3)
						{
							newPosition.X = position.X - velocity.X;
							newPosition.Y = position.Y - velocity.Y;
							newPosition.Z = position.Z - velocity.Z;
							velocity = Vector3.Zero;
						}
						else
						{
							newPosition.X = position.X - velocity.X;
							newPosition.Y = position.Y - velocity.Y;
							newPosition.Z = position.Z - velocity.Z;
							vector32 = Vector3.Normalize(vector32);
							velocity = Vector3.Reflect(velocity, vector32) / 2f;
						}
					}
					else
					{
						onramp = 2;
					}
				}
				this.rampNormal = new Vector3(this.region3[12], this.region3[13], this.region3[14]);
				Vector3 vector33 = new Vector3(this.region3[0], this.region3[1], this.region3[2]);
				Vector3 vector34 = new Vector3(this.region3[3], this.region3[4], this.region3[5]);
				Vector3 vector35 = new Vector3(this.region3[6], this.region3[7], this.region3[8]);
				Vector3 vector36 = new Vector3(this.region3[9], this.region3[10], this.region3[11]);
				Vector3 vector37 = new Vector3(position.X + velocity.X, position.Y + velocity.Y, position.Z + velocity.Z);
				float num44 = Vector3.Distance(vector33, vector34);
				float num45 = Vector3.Distance(vector34, vector37);
				float num46 = Vector3.Distance(vector33, vector37);
				float num47 = Vector3.Distance(vector34, vector35);
				float num48 = Vector3.Distance(vector33, vector36);
				float num49 = Vector3.Distance(vector35, vector37);
				float num50 = (num46 * num46 + num45 * num45 - num44 * num44) / (2f * num46 * num45);
				float num51 = Math.Abs((float)Math.Acos((double)num50));
				if ((double)num51 >= 2.5)
				{
					this.rampSpline = (num51 - 2.5f) / 0.64159274f;
				}
				num50 = (num44 * num44 + num45 * num45 - num46 * num46) / (2f * num44 * num45);
				num51 = (float)Math.Acos((double)num50);
				float num52 = (float)Math.Sin((double)num51) * num45;
				float num53 = num52 / num47;
				float num54 = num52 / num48;
				Vector3 vector38 = Vector3.Lerp(vector34, vector35, num53);
				Vector3 vector39 = Vector3.Lerp(vector33, vector36, num54);
				num50 = (num47 * num47 + num49 * num49 - num45 * num45) / (2f * num47 * num49);
				float num55 = (float)Math.Acos((double)num50);
				float num56 = (float)Math.Sin((double)num55) * num49;
				float num57 = num56 / Vector3.Distance(vector38, vector39);
				this.rampHeight = MathHelper.Lerp(vector38.Y, vector39.Y, num57);
				normal = new Vector3(this.region2[12], this.region2[13], this.region2[14]);
				gndPosition.Y = MathHelper.Lerp(vector34.Y, vector33.Y, num57);
				if (onramp == 2)
				{
					this.rampNormal = new Vector3(this.region2[12], this.region2[13], this.region2[14]);
					normal = new Vector3(this.region3[12], this.region3[13], this.region3[14]);
					gndPosition.Y = MathHelper.Lerp(vector34.Y, vector33.Y, num57);
				}
			}
			if (onramp > 0)
			{
				normal = Vector3.Lerp(this.rampNormal, normal, this.rampSpline * 0.5f);
				normal.Normalize();
				gndPosition.Y = MathHelper.Lerp(this.rampHeight, gndPosition.Y + 0f, this.rampSpline / 2f);
			}
			if (onramp == 0)
			{
				this.GetHeightAndNormalX(ref heights, ref normals, newPosition, out gndPosition.Y, out normal);
				if (this.rampSpline > 0f)
				{
					normal = Vector3.Lerp(normal, this.rampNormal, this.rampSpline * 0.5f);
					gndPosition.Y = MathHelper.Lerp(gndPosition.Y, this.rampHeight + 0f, this.rampSpline / 2f);
					normal.Normalize();
				}
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x001CA304 File Offset: 0x001C8504
		public void GetHeightAndNormalX(ref int[,] heights, ref Vector3[,] normals, Vector3 position, out float height, out Vector3 normal)
		{
			if (Rover.nearfarm)
			{
				this.GetHeightFarmOldschool(ref heights, position, out height, out normal, false);
				return;
			}
			this.heightmapWidth = (float)((this.bitmap - 1) * this.gridscale);
			position.X = (position.X % this.heightmapWidth + 1.5f * this.heightmapWidth) % this.heightmapWidth;
			position.Z = (position.Z % this.heightmapWidth + 1.5f * this.heightmapWidth) % this.heightmapWidth;
			Vector3 vector = position;
			int num = (int)vector.X / this.gridscale;
			int num2 = (int)vector.Z / this.gridscale;
			float num3 = vector.X % (float)this.gridscale / (float)this.gridscale;
			float num4 = vector.Z % (float)this.gridscale / (float)this.gridscale;
			int num5 = num + 1;
			int num6 = num2 + 1;
			if (num5 > this.bitmap - 2)
			{
				num5 = 0;
			}
			if (num6 > this.bitmap - 2)
			{
				num6 = 0;
			}
			float num7 = MathHelper.Lerp((float)heights[num, num2], (float)heights[num5, num2], num3);
			float num8 = MathHelper.Lerp((float)heights[num, num6], (float)heights[num5, num6], num3);
			height = MathHelper.Lerp(num7, num8, num4);
			Vector3 vector2 = Vector3.Lerp(normals[num, num2], normals[num5, num2], num3);
			Vector3 vector3 = Vector3.Lerp(normals[num, num6], normals[num5, num6], num3);
			normal = Vector3.Lerp(vector2, vector3, num4);
			normal.Normalize();
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x001CA4B8 File Offset: 0x001C86B8
		public void GetHeightFarmOldschool(ref int[,] heights, Vector3 position, out float height, out Vector3 normal, bool onlyheight)
		{
			height = 1f;
			normal = Vector3.Up;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x001CA4D0 File Offset: 0x001C86D0
		public void GetHeightFarm(ref int[,] heights, Vector3 position, out float height, out Vector3 normal, bool onlyheight)
		{
			Vector3 vector = new Vector3(3000f - Rover.farmLocation.X, 0f, 3000f - Rover.farmLocation.Z);
			position += vector;
			int num = (int)MathHelper.Clamp(position.X / 30f, 0f, 198f);
			int num2 = (int)MathHelper.Clamp(position.Z / 30f, 0f, 198f);
			float num3 = position.X % 30f / 30f;
			float num4 = position.Z % 30f / 30f;
			int num5 = num + 1;
			int num6 = num2 + 1;
			if (num5 > 198)
			{
				num5 = 0;
			}
			if (num6 > 198)
			{
				num6 = 0;
			}
			Vector3 vector2 = new Vector3((float)num, (float)heights[num, num2], (float)num2);
			if (num3 + num4 >= 1f)
			{
				vector2 = new Vector3((float)num5, (float)heights[num5, num6], (float)num6);
			}
			Vector3 vector3 = new Vector3((float)num, (float)heights[num, num6], (float)num6);
			Vector3 vector4 = new Vector3((float)num5, (float)heights[num5, num2], (float)num2);
			Vector2 vector5 = new Vector2(position.X / 30f, position.Z / 30f);
			float num7 = (vector3.Z - vector4.Z) * (vector2.X - vector4.X) + (vector4.X - vector3.X) * (vector2.Z - vector4.Z);
			float num8 = ((vector3.Z - vector4.Z) * (vector5.X - vector4.X) + (vector4.X - vector3.X) * (vector5.Y - vector4.Z)) / num7;
			float num9 = ((vector4.Z - vector2.Z) * (vector5.X - vector4.X) + (vector2.X - vector4.X) * (vector5.Y - vector4.Z)) / num7;
			float num10 = 1f - num8 - num9;
			height = num8 * vector2.Y + num9 * vector3.Y + num10 * vector4.Y;
			normal = Vector3.Zero;
			if (onlyheight)
			{
				return;
			}
			if (num3 + num4 > 1f)
			{
				vector2 = new Vector3((float)num5, (float)heights[num5, num6], (float)num6);
			}
			vector2.Y /= 30f;
			vector3.Y /= 30f;
			vector4.Y /= 30f;
			Vector3 vector6 = vector2;
			Vector3 vector7 = vector3;
			Vector3 vector8 = vector4;
			Vector3 vector9 = Vector3.Cross(vector8 - vector7, vector6 - vector7);
			Vector3 vector10 = Vector3.Normalize(vector9);
			if (vector10.Y < 0f)
			{
				vector10 = -vector10;
			}
			normal = vector10;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x001CA830 File Offset: 0x001C8A30
		public Rover()
		{
			float[] array = new float[15];
			this.region1 = array;
			float[] array2 = new float[15];
			this.region2 = array2;
			float[] array3 = new float[15];
			this.region3 = array3;
			this.rampNormal = Vector3.Zero;
			this.amb = new Vector3(0.3f, 0.3f, 0.3f);
			this.diffu = new Vector3(1.2f, 1.1f, 1f);
			this.horn = 10f;
			base..ctor();
		}

		// Token: 0x04001F55 RID: 8021
		private KeyboardState keyState;

		// Token: 0x04001F56 RID: 8022
		private KeyboardState prevkeyState;

		// Token: 0x04001F57 RID: 8023
		private MouseState mouseState;

		// Token: 0x04001F58 RID: 8024
		private MouseState prevMouse;

		// Token: 0x04001F59 RID: 8025
		public float thumb;

		// Token: 0x04001F5A RID: 8026
		public float righttrig;

		// Token: 0x04001F5B RID: 8027
		public float leftTrig;

		// Token: 0x04001F5C RID: 8028
		public Matrix smallWorld;

		// Token: 0x04001F5D RID: 8029
		private int fliptimer;

		// Token: 0x04001F5E RID: 8030
		private Vector3 reflectnormal;

		// Token: 0x04001F5F RID: 8031
		public bool brakeson;

		// Token: 0x04001F60 RID: 8032
		public bool scopemode;

		// Token: 0x04001F61 RID: 8033
		public Vector3 position = new Vector3(1400f, 0f, 700f);

		// Token: 0x04001F62 RID: 8034
		public float facingDirection = 3.14f;

		// Token: 0x04001F63 RID: 8035
		private Vector3 delta;

		// Token: 0x04001F64 RID: 8036
		private bool jumpnow;

		// Token: 0x04001F65 RID: 8037
		private float spinny = 1f;

		// Token: 0x04001F66 RID: 8038
		private Vector3 lastmovement = Vector3.Zero;

		// Token: 0x04001F67 RID: 8039
		private Matrix lastOrient = Matrix.Identity;

		// Token: 0x04001F68 RID: 8040
		public float dotty;

		// Token: 0x04001F69 RID: 8041
		public float mydotty;

		// Token: 0x04001F6A RID: 8042
		public float hillalign;

		// Token: 0x04001F6B RID: 8043
		public float futureDot;

		// Token: 0x04001F6C RID: 8044
		public int shockhit;

		// Token: 0x04001F6D RID: 8045
		public float leanAmt;

		// Token: 0x04001F6E RID: 8046
		public Vector3 pp;

		// Token: 0x04001F6F RID: 8047
		public int indexMatrix = 1;

		// Token: 0x04001F70 RID: 8048
		public float indexValue;

		// Token: 0x04001F71 RID: 8049
		private Matrix lean;

		// Token: 0x04001F72 RID: 8050
		private Matrix pitch;

		// Token: 0x04001F73 RID: 8051
		public Vector3 aVector = Vector3.Zero;

		// Token: 0x04001F74 RID: 8052
		public Vector3 bVector = Vector3.Zero;

		// Token: 0x04001F75 RID: 8053
		public int flipflag;

		// Token: 0x04001F76 RID: 8054
		private int oldhangtime;

		// Token: 0x04001F77 RID: 8055
		private Vector3 oldgrav = Vector3.Zero;

		// Token: 0x04001F78 RID: 8056
		public int turnDir = 1;

		// Token: 0x04001F79 RID: 8057
		public int spinflag;

		// Token: 0x04001F7A RID: 8058
		public float rotAlign;

		// Token: 0x04001F7B RID: 8059
		private float incRot;

		// Token: 0x04001F7C RID: 8060
		private Vector3 axisdelta = Vector3.Zero;

		// Token: 0x04001F7D RID: 8061
		private Matrix carRot = Matrix.CreateFromYawPitchRoll(0f, 0f, 0f);

		// Token: 0x04001F7E RID: 8062
		private int hithard;

		// Token: 0x04001F7F RID: 8063
		public Vector3 normal = Vector3.Zero;

		// Token: 0x04001F80 RID: 8064
		private Vector3 jumpstart = Vector3.Zero;

		// Token: 0x04001F81 RID: 8065
		private Vector3 lastNormal = Vector3.Zero;

		// Token: 0x04001F82 RID: 8066
		private Vector3 workingNormal = Vector3.Zero;

		// Token: 0x04001F83 RID: 8067
		private Vector3 oldnormal = Vector3.Zero;

		// Token: 0x04001F84 RID: 8068
		private Vector3 futurenormal = Vector3.Zero;

		// Token: 0x04001F85 RID: 8069
		public Vector3 futureposition = Vector3.Zero;

		// Token: 0x04001F86 RID: 8070
		private int hangtime;

		// Token: 0x04001F87 RID: 8071
		public int gridscale;

		// Token: 0x04001F88 RID: 8072
		public int bitmap;

		// Token: 0x04001F89 RID: 8073
		private float heightmapWidth;

		// Token: 0x04001F8A RID: 8074
		public Vector3 newPosition;

		// Token: 0x04001F8B RID: 8075
		public Vector3 gndPosition;

		// Token: 0x04001F8C RID: 8076
		private float solarx;

		// Token: 0x04001F8D RID: 8077
		private float solary;

		// Token: 0x04001F8E RID: 8078
		public int solarflag;

		// Token: 0x04001F8F RID: 8079
		public int solar1 = 1;

		// Token: 0x04001F90 RID: 8080
		public float scoopx = 1f;

		// Token: 0x04001F91 RID: 8081
		public int scoopflag;

		// Token: 0x04001F92 RID: 8082
		public int scoop1 = 1;

		// Token: 0x04001F93 RID: 8083
		public bool scooperON;

		// Token: 0x04001F94 RID: 8084
		public float speedx;

		// Token: 0x04001F95 RID: 8085
		public Matrix orientation = Matrix.Identity;

		// Token: 0x04001F96 RID: 8086
		private Matrix orientationx = Matrix.Identity;

		// Token: 0x04001F97 RID: 8087
		private Matrix lastorientation = Matrix.Identity;

		// Token: 0x04001F98 RID: 8088
		public int myside;

		// Token: 0x04001F99 RID: 8089
		public int groundflag;

		// Token: 0x04001F9A RID: 8090
		public Vector3 movement = Vector3.Zero;

		// Token: 0x04001F9B RID: 8091
		public Vector3 inertial = Vector3.Zero;

		// Token: 0x04001F9C RID: 8092
		public Vector3 velocity = Vector3.Zero;

		// Token: 0x04001F9D RID: 8093
		public Vector3 grav = Vector3.Zero;

		// Token: 0x04001F9E RID: 8094
		private float zspin;

		// Token: 0x04001F9F RID: 8095
		public float min = 7f;

		// Token: 0x04001FA0 RID: 8096
		public static float turnSpeed = 0.11f;

		// Token: 0x04001FA1 RID: 8097
		public static float max = -65f;

		// Token: 0x04001FA2 RID: 8098
		public static float realGrav = -0.8f;

		// Token: 0x04001FA3 RID: 8099
		public static float fric = 0.9f;

		// Token: 0x04001FA4 RID: 8100
		public static float fricOrig = 0.9f;

		// Token: 0x04001FA5 RID: 8101
		public static int rockhitCount = 0;

		// Token: 0x04001FA6 RID: 8102
		public static Vector3 farmLocation = Vector3.Zero;

		// Token: 0x04001FA7 RID: 8103
		public static bool nearfarm = false;

		// Token: 0x04001FA8 RID: 8104
		private float speed;

		// Token: 0x04001FA9 RID: 8105
		public float[] region1;

		// Token: 0x04001FAA RID: 8106
		public float[] region2;

		// Token: 0x04001FAB RID: 8107
		public float[] region3;

		// Token: 0x04001FAC RID: 8108
		public float alfa;

		// Token: 0x04001FAD RID: 8109
		public int onramp;

		// Token: 0x04001FAE RID: 8110
		private Vector3 rampNormal;

		// Token: 0x04001FAF RID: 8111
		private float rampHeight;

		// Token: 0x04001FB0 RID: 8112
		private float rampSpline;

		// Token: 0x04001FB1 RID: 8113
		public float directme;

		// Token: 0x04001FB2 RID: 8114
		public int camSwitch;

		// Token: 0x04001FB3 RID: 8115
		public int dashcam;

		// Token: 0x04001FB4 RID: 8116
		public Vector3 amb;

		// Token: 0x04001FB5 RID: 8117
		public Vector3 diffu;

		// Token: 0x04001FB6 RID: 8118
		private Matrix[] boneTransforms;

		// Token: 0x04001FB7 RID: 8119
		private Random rr;

		// Token: 0x04001FB8 RID: 8120
		private GraphicsDevice gr;

		// Token: 0x04001FB9 RID: 8121
		private ScreenManager sc;

		// Token: 0x04001FBA RID: 8122
		private SoundEffect jumpit;

		// Token: 0x04001FBB RID: 8123
		private float horn;

		// Token: 0x04001FBC RID: 8124
		private ContentManager content;
	}
}
