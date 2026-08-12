using System;
using System.Globalization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x020000B7 RID: 183
	internal class Lander : GameScreen
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x00148F94 File Offset: 0x00147194
		public void LoadContent(ContentManager content, ScreenManager screenManager)
		{
			this.sc = screenManager;
			this.content = content;
			this.device = screenManager.GraphicsDevice;
			this.model = content.Load<Model>("astro\\models\\Lander");
			this.podBone = this.model.Bones["pod"];
			this.doorBone = this.model.Bones["door"];
			this.door2Bone = this.model.Bones["door2"];
			this.flameBone = this.model.Bones["flame"];
			this.flameTransform = this.flameBone.Transform;
			this.podTransform = this.podBone.Transform;
			this.doorTransform = this.doorBone.Transform;
			this.door2Transform = this.door2Bone.Transform;
			this.orientation = Matrix.CreateRotationY(10f);
			this.orientation.Up = Vector3.Up;
			this.orientation.Right = Vector3.Cross(this.orientation.Forward, this.orientation.Up);
			this.orientation.Right = Vector3.Normalize(this.orientation.Right);
			this.orientation.Forward = Vector3.Cross(this.orientation.Up, this.orientation.Right);
			this.orientation.Forward = Vector3.Normalize(this.orientation.Forward);
			this.lastorientation = this.orientation;
			this.drophite = new float[81, 81];
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			StreamReader streamReader = new StreamReader("Content/astro/text/dropHite.txt");
			for (int i = 0; i < 81; i++)
			{
				for (int j = 0; j < 81; j++)
				{
					this.drophite[j, 80 - i] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
					this.drophite[j, 80 - i] *= 5f;
				}
			}
			streamReader.Close();
			streamReader.Dispose();
			this.boneTransforms = new Matrix[this.model.Bones.Count];
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x001491C8 File Offset: 0x001473C8
		public override void UnloadContent()
		{
			this.content.Unload();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x001491D8 File Offset: 0x001473D8
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

		// Token: 0x06000637 RID: 1591 RVA: 0x0014937C File Offset: 0x0014757C
		public void collideDropship(Vector3 newPosition)
		{
			Vector3 vector = new Vector3(-51f, -112f, 237f) * 5f + this.dropship;
			Vector3 vector2 = new Vector3(51f, 96f, 797f) * 5f + this.dropship;
			Vector3 vector3 = new Vector3(-102f, -35f, -673f) * 5f + this.dropship;
			Vector3 vector4 = new Vector3(102f, 136f, -112f) * 5f + this.dropship;
			Vector3 vector5 = new Vector3(-388f, -21f, -209f) * 5f + this.dropship;
			Vector3 vector6 = new Vector3(374f, 76f, -15f) * 5f + this.dropship;
			Vector3 vector7 = new Vector3(-115f, -128f, -387f) * 5f + this.dropship;
			Vector3 vector8 = new Vector3(115f, 92f, 273f) * 5f + this.dropship;
			Vector3 vector9 = new Vector3(-394.79f, -59f, -407.92f) * 5f + this.dropship;
			Vector3 vector10 = new Vector3(-318.86f, 91f, 56.31f) * 5f + this.dropship;
			Vector3 vector11 = new Vector3(311.73f, -59f, -407.92f) * 5f + this.dropship;
			Vector3 vector12 = new Vector3(387.67f, 91f, 56.31f) * 5f + this.dropship;
			if (this.abovedropship(vector, vector2))
			{
				return;
			}
			if (this.abovedropship(vector3, vector4))
			{
				return;
			}
			if (this.abovedropship(vector5, vector6))
			{
				return;
			}
			if (this.abovedropship(vector7, vector8))
			{
				return;
			}
			if (this.abovedropship(vector9, vector10))
			{
				return;
			}
			if (this.abovedropship(vector11, vector12))
			{
				return;
			}
			if (this.onDropship)
			{
				return;
			}
			if (this.boxcollide(vector, vector2))
			{
				return;
			}
			if (this.boxcollide(vector3, vector4))
			{
				return;
			}
			if (this.boxcollide(vector5, vector6))
			{
				return;
			}
			if (this.boxcollide(vector7, vector8))
			{
				return;
			}
			if (this.boxcollide(vector9, vector10))
			{
				return;
			}
			this.boxcollide(vector11, vector12);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00149624 File Offset: 0x00147824
		public bool abovedropship(Vector3 min, Vector3 max)
		{
			if (!this.onDropship && this.newPosition.X > min.X && this.newPosition.X < max.X && this.newPosition.Z > min.Z && this.newPosition.Z < max.Z && this.newPosition.Y > max.Y)
			{
				this.onDropship = true;
				this.impact = 0;
				return true;
			}
			return false;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x001496B0 File Offset: 0x001478B0
		public bool boxcollide(Vector3 min, Vector3 max)
		{
			if (!this.onDropship)
			{
				BoundingBox boundingBox = new BoundingBox(min, max);
				if (boundingBox.Intersects(new BoundingSphere(this.newPosition, 230f)))
				{
					float num = 9000f;
					Vector3 vector = this.velocity;
					if (this.velocity.Length() <= 0.1f)
					{
						vector = this.dropship - this.newPosition;
					}
					vector = Vector3.Normalize(vector);
					Ray ray = new Ray(this.newPosition, vector);
					this.IntersectRayVsBox(boundingBox, ray, out num, out this.reflectnormal);
					if (num < 230f)
					{
						this.impact = 5;
						float num2 = MathHelper.Max(20f, this.velocity.Length() * 0.6f);
						if (num2 == 20f)
						{
							this.reflectnormal *= 20f;
						}
						else
						{
							this.reflectnormal = Vector3.Reflect(num2 * vector, this.reflectnormal);
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x001497B0 File Offset: 0x001479B0
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

		// Token: 0x0600063B RID: 1595 RVA: 0x00149844 File Offset: 0x00147A44
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

		// Token: 0x0600063C RID: 1596 RVA: 0x001498D8 File Offset: 0x00147AD8
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

		// Token: 0x0600063D RID: 1597 RVA: 0x001499D8 File Offset: 0x00147BD8
		public bool Ktoggle(Keys k)
		{
			return this.keyState.IsKeyDown(k) && this.prevkeyState.IsKeyUp(k);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00149A04 File Offset: 0x00147C04
		public void HandleInput(ref MouseState mouse, ref MouseState prevmouse, ref KeyboardState keystate, ref KeyboardState prevkeystate, ref GamePadState currentGamePadState, ref int[,] heightData, ref Vector3[,] normals)
		{
			this.mouseState = mouse;
			this.prevMouse = prevmouse;
			this.keyState = keystate;
			this.prevkeyState = prevkeystate;
			this.rrcount++;
			if (this.dentme == 1)
			{
				this.dentmex();
				this.dentme = 0;
			}
			if (this.dentme2 == 1)
			{
				this.dentmex2();
				this.dentme2 = 0;
			}
			this.allheights = heightData;
			this.gemmax = 1f;
			this.leftTrigger = 0f;
			if (this.door1 == 1)
			{
				if (!this.sc.usingMouse)
				{
					this.rightTrigger = currentGamePadState.Triggers.Right;
					this.leftTrigger = currentGamePadState.Triggers.Left;
					this.rightTrigger = (float)Math.Pow((double)this.rightTrigger, (double)this.curve);
				}
				else
				{
					if (this.KMdown(this.sc.lmb_key))
					{
						this.rightTrigger += 0.01f;
						this.rightTrigger = (float)Math.Pow((double)this.rightTrigger, 0.8);
						if (this.rightTrigger > 1f)
						{
							this.rightTrigger = 1f;
						}
					}
					else
					{
						if (this.diff > 100f)
						{
							this.rightTrigger -= 0.01f;
						}
						else
						{
							this.rightTrigger -= 0.05f;
						}
						if (this.rightTrigger < 0f)
						{
							this.rightTrigger = 0f;
						}
					}
					if (this.KMdown(this.sc.rmb_key))
					{
						if (this.impact == 1 && this.diff <= 40f)
						{
							this.leftTrigger = 1f;
						}
					}
					else
					{
						this.leftTrigger = 0f;
					}
				}
			}
			else
			{
				this.leftTrigger = 0f;
				this.rightTrigger = 0f;
			}
			this.lscale = this.normalizedSpeed * this.rFlame[this.rrcount % this.rFlame.Length] + 0.5f;
			if (this.rightTrigger <= 0f)
			{
				this.lscale = 0f;
			}
			this.movement = Vector3.Zero;
			if (this.rightTrigger > 0f)
			{
				this.speed = this.rightTrigger * this.thrust;
				this.normalizedSpeed = this.speed / this.maxthrust;
				this.flameFlag = 1;
			}
			else
			{
				this.speed = 0f;
				this.normalizedSpeed = 0f;
				this.flameFlag = 0;
			}
			this.movement.Y = this.speed;
			this.newPosition = this.position + this.velocity;
			this.landerBottom = this.newPosition + Vector3.Transform(new Vector3(0f, -155f, 0f), this.orientation);
			this.surface.X = this.landerBottom.X;
			this.surface.Z = this.landerBottom.Z;
			this.onDropship = false;
			if (this.insideDropship)
			{
				this.collideDropship(this.newPosition);
			}
			if (!this.onDropship)
			{
				this.GetHeightAndNormal(ref heightData, ref normals, this.surface, out this.surface.Y, out this.normal);
				this.altitude = (int)(this.position.Y - this.surface.Y);
			}
			else
			{
				this.ShipHeightandNormal(this.surface, out this.surface.Y, out this.normal);
				float num;
				this.GetHeight(ref heightData, this.surface, out num);
				this.altitude = (int)(this.position.Y - num);
			}
			this.shieldhit -= 1f;
			Vector3 vector = new Vector3(Facility.offset.X + 2200f, Facility.offset.Y + 360f, Facility.offset.Z + 4050f);
			float num2 = Vector3.DistanceSquared(vector, this.position);
			float num3 = 620f;
			if (num2 < num3 * num3)
			{
				Vector3 vector2 = Vector3.Zero;
				Vector3 vector3 = this.position - vector;
				if (vector3.LengthSquared() > 0f)
				{
					vector2 = Vector3.Normalize(vector3);
				}
				this.shockhit = 30;
				this.shieldhit = 120f;
				float num4 = this.velocity.Length();
				if (num4 < 10f)
				{
					num4 = 10f;
				}
				this.velocity = vector2 * num4;
			}
			this.velocity += Vector3.Transform(this.movement, this.orientation) + new Vector3(0f, this.gravity, 0f);
			if (this.position.Y > 9000f + this.surface.Y && this.velocity.Y > 0f)
			{
				this.velocity -= new Vector3(0f, this.velocity.Y * (1E-05f * (float)this.altitude), 0f);
			}
			if (this.velocity.Y < this.terminalveloc)
			{
				this.velocity.Y = this.terminalveloc;
			}
			if (this.impact == 0 && (this.altitude < 350 || this.onDropship))
			{
				Vector3 vector4 = this.newPosition + Vector3.Transform(new Vector3(0f, -150f, 135f), this.orientation);
				Vector3 vector5 = this.newPosition + Vector3.Transform(new Vector3(-135f, -150f, 0f), this.orientation);
				Vector3 vector6 = this.newPosition + Vector3.Transform(new Vector3(0f, -150f, -135f), this.orientation);
				Vector3 vector7 = this.newPosition + Vector3.Transform(new Vector3(135f, -150f, 0f), this.orientation);
				Vector3 vector8 = this.newPosition + Vector3.Transform(new Vector3(0f, 150f, 120f), this.orientation);
				Vector3 vector9 = this.newPosition + Vector3.Transform(new Vector3(-120f, 150f, 0f), this.orientation);
				Vector3 vector10 = this.newPosition + Vector3.Transform(new Vector3(0f, 150f, -120f), this.orientation);
				Vector3 vector11 = this.newPosition + Vector3.Transform(new Vector3(120f, 150f, 0f), this.orientation);
				Vector3 vector12 = this.newPosition + Vector3.Transform(new Vector3(0f, 255f, 0f), this.orientation);
				Vector3 vector13 = vector4;
				Vector3 vector14 = vector5;
				Vector3 vector15 = vector6;
				Vector3 vector16 = vector7;
				Vector3 vector17 = vector8;
				Vector3 vector18 = vector9;
				Vector3 vector19 = vector10;
				Vector3 vector20 = vector11;
				Vector3 vector21 = vector12;
				if (this.onDropship)
				{
					this.GetHeightShip(vector4, out vector13.Y);
					this.GetHeightShip(vector5, out vector14.Y);
					this.GetHeightShip(vector6, out vector15.Y);
					this.GetHeightShip(vector7, out vector16.Y);
					this.GetHeightShip(vector8, out vector17.Y);
					this.GetHeightShip(vector9, out vector18.Y);
					this.GetHeightShip(vector10, out vector19.Y);
					this.GetHeightShip(vector11, out vector20.Y);
					this.GetHeightShip(vector12, out vector21.Y);
				}
				else
				{
					this.GetHeight(ref heightData, vector4, out vector13.Y);
					this.GetHeight(ref heightData, vector5, out vector14.Y);
					this.GetHeight(ref heightData, vector6, out vector15.Y);
					this.GetHeight(ref heightData, vector7, out vector16.Y);
					this.GetHeight(ref heightData, vector8, out vector17.Y);
					this.GetHeight(ref heightData, vector9, out vector18.Y);
					this.GetHeight(ref heightData, vector10, out vector19.Y);
					this.GetHeight(ref heightData, vector11, out vector20.Y);
					this.GetHeight(ref heightData, vector12, out vector21.Y);
				}
				float num5 = 0f;
				if (this.onDropship)
				{
					num5 = 10f;
				}
				if (vector4.Y < vector13.Y + num5 || vector5.Y < vector14.Y + num5 || vector6.Y < vector15.Y + num5 || vector7.Y < vector16.Y + num5)
				{
					if (this.rightTrigger == 0f && Vector3.Dot(this.normal, this.orientation.Up) > 0.8f && (this.onDropship || this.velocity.LengthSquared() < 2500f))
					{
						this.impact = 1;
					}
					else
					{
						this.impact = 2;
						if (this.shockhit == 0)
						{
							this.shockhit = 2 + (int)this.velocity.Length() / 2;
						}
					}
				}
				else if (vector8.Y < vector17.Y || vector9.Y < vector18.Y || vector10.Y < vector19.Y || vector11.Y < vector20.Y || vector12.Y < vector21.Y)
				{
					this.impact = 2;
					if (this.shockhit == 0)
					{
						this.shockhit = 2 + (int)this.velocity.Length() / 2;
					}
				}
				if (this.impact == 2)
				{
					float num6 = 0.9f;
					this.movedist1 = Vector3.Distance(this.newPosition, this.position) * 0.0005f;
					this.velocity = Vector3.Reflect(this.velocity, this.normal) * 0.8f;
					this.velocity.Y = Math.Abs(this.velocity.Y + 0f) * num6;
					this.position.Y = MathHelper.Max(this.newPosition.Y, this.position.Y) + 0.5f;
					this.newPosition = this.position + this.velocity;
					float num7 = 0f;
					if (vector4.Y < vector13.Y)
					{
						this.delta1 = new Vector3(vector4.X, 0f, vector4.Z) - new Vector3(this.newPosition.X, 0f, this.newPosition.Z);
						num7 = vector13.Y - vector4.Y;
					}
					if (vector5.Y < vector14.Y)
					{
						this.delta1 = new Vector3(vector5.X, 0f, vector5.Z) - new Vector3(this.newPosition.X, 0f, this.newPosition.Z);
						num7 = vector14.Y - vector5.Y;
					}
					if (vector6.Y < vector15.Y)
					{
						this.delta1 = new Vector3(vector6.X, 0f, vector6.Z) - new Vector3(this.newPosition.X, 0f, this.newPosition.Z);
						num7 = vector15.Y - vector6.Y;
					}
					if (vector7.Y < vector16.Y)
					{
						this.delta1 = new Vector3(vector7.X, 0f, vector7.Z) - new Vector3(this.newPosition.X, 0f, this.newPosition.Z);
						num7 = vector16.Y - vector7.Y;
					}
					if (vector8.Y < vector17.Y)
					{
						this.delta1 = new Vector3(vector8.X, 0f, vector8.Z) - new Vector3(this.newPosition.X, 0f, this.newPosition.Z);
						num7 = vector17.Y - vector8.Y;
					}
					if (vector9.Y < vector18.Y)
					{
						this.delta1 = new Vector3(vector9.X, 0f, vector9.Z) - new Vector3(this.newPosition.X, 0f, this.newPosition.Z);
						num7 = vector18.Y - vector9.Y;
					}
					if (vector10.Y < vector19.Y)
					{
						this.delta1 = new Vector3(vector10.X, 0f, vector10.Z) - new Vector3(this.newPosition.X, 0f, this.newPosition.Z);
						num7 = vector19.Y - vector10.Y;
					}
					if (vector11.Y < vector20.Y)
					{
						this.delta1 = new Vector3(vector11.X, 0f, vector11.Z) - new Vector3(this.newPosition.X, 0f, this.newPosition.Z);
						num7 = vector20.Y - vector11.Y;
					}
					if (vector12.Y < vector21.Y)
					{
						this.delta1 = new Vector3(vector12.X, 0f, vector12.Z) - new Vector3(this.newPosition.X, 0f, this.newPosition.Z);
						num7 = vector21.Y - vector12.Y;
					}
					this.newPosition.Y = this.newPosition.Y + (Math.Abs(num7) + 1f);
				}
			}
			if (this.impact == 5)
			{
				this.movedist1 = Vector3.Distance(this.newPosition, this.position) * 0.0005f;
				this.velocity = this.reflectnormal;
				this.newPosition = this.position + this.velocity;
				this.shockhit = 40;
			}
			if (this.movedist1 > 0f && this.delta1.LengthSquared() > 0f)
			{
				this.orientation *= Matrix.CreateFromAxisAngle(Vector3.Cross(-Vector3.Normalize(this.delta1), Vector3.Up), this.movedist1);
			}
			this.movedist1 *= 0.94f;
			if (this.newPosition.Y - 300f <= this.surface.Y && this.rightTrigger <= 0f && this.impact == 2)
			{
				this.movedistx = Vector3.Distance(this.newPosition, this.position) * 0.55f;
				this.deltax = new Vector3(this.newPosition.X, 0f, this.newPosition.Z) - new Vector3(this.position.X, 0f, this.position.Z);
			}
			if (this.deltax.LengthSquared() > 0f)
			{
				this.orientation *= Matrix.CreateFromAxisAngle(Vector3.Cross(-Vector3.Normalize(this.deltax), Vector3.Up), this.movedistx * 0.0037f);
			}
			this.movedistx *= 0.97f;
			if (this.diff == this.offset)
			{
				this.orientation *= Matrix.CreateRotationX(this.landerDirectionX) * Matrix.CreateRotationZ(this.landerDirectionZ);
				this.directme = (float)Math.Acos(Convert.ToDouble(Vector3.Dot(this.lastorientation.Forward, Vector3.Forward)));
				if (this.lastorientation.Forward.X > 0f)
				{
					this.directme = -this.directme;
				}
				this.lasthite = this.newPosition;
			}
			this.lastorientation = this.orientation;
			if (this.rightTrigger <= 0f && this.impact == 1 && (this.onDropship || this.velocity.Y < 0f) && this.landerBottom.Y <= this.surface.Y + 50f)
			{
				if (this.diff > 0f)
				{
					this.diff -= 1f;
				}
				this.orientationx = Matrix.CreateRotationY(this.directme);
				this.orientationx.Up = this.normal;
				this.orientationx.Right = Vector3.Cross(this.orientationx.Forward, this.orientationx.Up);
				this.orientationx.Right = Vector3.Normalize(this.orientationx.Right);
				this.orientationx.Forward = Vector3.Cross(this.orientationx.Up, this.orientationx.Right);
				this.orientationx.Forward = Vector3.Normalize(this.orientationx.Forward);
				if (this.diff / this.offset > 0f)
				{
					Vector3 vector22;
					Quaternion quaternion;
					Vector3 vector23;
					this.lastorientation.Decompose(out vector22, out quaternion, out vector23);
					Vector3 vector24;
					Quaternion quaternion2;
					Vector3 vector25;
					this.orientationx.Decompose(out vector24, out quaternion2, out vector25);
					this.orientation = Matrix.CreateFromQuaternion(Quaternion.Slerp(quaternion2, quaternion, this.diff / this.offset)) * Matrix.CreateTranslation(Vector3.Lerp(vector25, vector23, this.diff / this.offset));
				}
				if (this.landerBottom.Y <= this.surface.Y || (this.onDropship && this.landerBottom.Y <= this.surface.Y + 20f))
				{
					if (this.velocity.Y < 0f)
					{
						this.velocity.Y = 0f;
					}
					this.velocity /= 1.1f;
					this.newPosition = this.surface + Vector3.Transform(new Vector3(0f, 155f, 0f), this.orientation);
					this.lastorientation = this.orientation;
					this.movedistx = 0f;
					this.movedist1 = 0f;
				}
			}
			else
			{
				this.diff = this.offset;
				this.impact = 0;
			}
			if (this.onDropship || this.insideDropship)
			{
				this.newPosition += this.dropshipVeloc;
			}
			float num8 = 0.93f;
			float num9 = 0.993f;
			float num10 = MathHelper.Lerp(1500f, 500f, MathHelper.Clamp((float)(this.altitude / 500), 0f, 1f));
			if (this.impact == 2)
			{
				num10 = 500f;
			}
			float num11 = MathHelper.Lerp(num10, 260f, this.rightTrigger * this.rightTrigger * this.rightTrigger);
			if (this.sc.usingMouse)
			{
				if (this.KMdown(this.sc.d_key))
				{
					this.ggO.X = this.ggO.X + 0.07f;
					if (this.ggO.X > 1f)
					{
						this.ggO.X = 1f;
					}
				}
				if (this.KMdown(this.sc.a_key))
				{
					this.ggO.X = this.ggO.X - 0.07f;
					if (this.ggO.X < -1f)
					{
						this.ggO.X = -1f;
					}
				}
				if (this.KMreleased(this.sc.a_key) && this.KMreleased(this.sc.d_key))
				{
					if (this.ggO.X < 0f)
					{
						this.ggO.X = this.ggO.X + 0.1f;
						if (this.ggO.X > 0f)
						{
							this.ggO.X = 0f;
						}
					}
					if (this.ggO.X > 0f)
					{
						this.ggO.X = this.ggO.X - 0.1f;
						if (this.ggO.X < 0f)
						{
							this.ggO.X = 0f;
						}
					}
				}
				if (this.KMdown(this.sc.w_key))
				{
					if (this.ggO.Y < 0f)
					{
						this.ggO.Y = 0f;
					}
					this.ggO.Y = this.ggO.Y + 0.07f;
					if (this.ggO.Y > 1f)
					{
						this.ggO.Y = 1f;
					}
				}
				if (this.KMdown(this.sc.s_key))
				{
					if (this.ggO.Y > 0f)
					{
						this.ggO.Y = 0f;
					}
					this.ggO.Y = this.ggO.Y - 0.07f;
					if (this.ggO.Y < -1f)
					{
						this.ggO.Y = -1f;
					}
				}
				if (this.KMreleased(this.sc.s_key) && this.KMreleased(this.sc.w_key))
				{
					if (this.ggO.Y < 0f)
					{
						this.ggO.Y = this.ggO.Y + 0.1f;
						if (this.ggO.Y > 0f)
						{
							this.ggO.Y = 0f;
						}
					}
					if (this.ggO.Y > 0f)
					{
						this.ggO.Y = this.ggO.Y - 0.1f;
						if (this.ggO.Y < 0f)
						{
							this.ggO.Y = 0f;
						}
					}
				}
			}
			else
			{
				this.ggO.X = currentGamePadState.ThumbSticks.Left.X;
				this.ggO.Y = currentGamePadState.ThumbSticks.Left.Y;
			}
			this.turnAmountX = -this.ggO.X / num11;
			this.turnAmountZ = -this.ggO.Y / num11;
			this.landerDirectionX += this.turnAmountX * (float)Math.Sin((double)this.camrot) + this.turnAmountZ * (float)(-(float)Math.Cos((double)this.camrot));
			this.landerDirectionZ += this.turnAmountZ * (float)(-(float)Math.Sin((double)this.camrot)) + this.turnAmountX * (float)(-(float)Math.Cos((double)this.camrot));
			this.landerDirectionX *= num8;
			this.landerDirectionZ *= num8;
			this.velocity.X = this.velocity.X * num9;
			this.velocity.Z = this.velocity.Z * num9;
			this.lander2ground = this.rayDistance;
			this.ground = this.position - this.orientation.Up * this.rayDistance;
			float num12;
			if (!this.onDropship)
			{
				this.GetHeight(ref heightData, this.ground, out num12);
			}
			else
			{
				this.GetHeightShip(this.ground, out num12);
			}
			if (num12 < this.ground.Y)
			{
				this.rayDistance += 90f;
				if (this.rayDistance > 9000f)
				{
					this.rayDistance = 9000f;
				}
			}
			if (num12 > this.ground.Y)
			{
				this.rayDistance -= 90f;
				if (this.rayDistance < 90f)
				{
					this.rayDistance = 90f;
				}
			}
			float num13 = this.position.X - this.newPosition.X;
			float num14 = this.position.Z - this.newPosition.Z;
			if (Math.Abs(num13) > this.maxspeed && !this.onDropship && !this.insideDropship)
			{
				this.position.X = this.position.X - (float)Math.Sign(num13) * this.maxspeed;
			}
			else
			{
				this.position.X = this.newPosition.X;
			}
			if (Math.Abs(num14) > this.maxspeed && !this.onDropship && !this.insideDropship)
			{
				this.position.Z = this.position.Z - (float)Math.Sign(num14) * this.maxspeed;
			}
			else
			{
				this.position.Z = this.newPosition.Z;
			}
			this.position.Y = this.newPosition.Y;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0014B368 File Offset: 0x00149568
		public void Draw(Matrix viewMatrix, Matrix projectionMatrix, Vector3 sundir)
		{
			this.animateparts();
			this.flameMatrix = Matrix.CreateScale(this.lscale);
			this.flameBone.Transform = this.flameMatrix * this.flameTransform;
			this.model.CopyAbsoluteBoneTransformsTo(this.boneTransforms);
			Matrix matrix = this.orientation * Matrix.CreateTranslation(this.position);
			foreach (ModelMesh modelMesh in this.model.Meshes)
			{
				this.mytimer += 1f;
				int num = 1;
				if (modelMesh.Name == "flame")
				{
					if (this.flameFlag == 0)
					{
						num = 0;
					}
					else
					{
						this.reducer = 1f - MathHelper.Clamp((this.lander2ground - 400f) / 2200f, 0f, 1f);
						this.directLight = this.orientation.Up;
					}
				}
				if (modelMesh.Name == "interior" && this.door1 == 1)
				{
					num = 0;
				}
				if (num == 1)
				{
					foreach (Effect effect in modelMesh.Effects)
					{
						BasicEffect basicEffect = (BasicEffect)effect;
						basicEffect.PreferPerPixelLighting = false;
						basicEffect.World = this.boneTransforms[modelMesh.ParentBone.Index] * matrix;
						if (modelMesh.Name == "dish1" && this.radioFixed)
						{
							basicEffect.World = Matrix.CreateRotationY(0.9f * (float)Math.Sin((double)(this.mytimer / 400f))) * this.boneTransforms[modelMesh.ParentBone.Index] * matrix;
						}
						basicEffect.View = viewMatrix;
						basicEffect.Projection = projectionMatrix;
						basicEffect.LightingEnabled = true;
						basicEffect.DirectionalLight0.Enabled = true;
						basicEffect.AmbientLightColor = this.amb * 0.8f;
						if (modelMesh.Name == "interior")
						{
							basicEffect.AmbientLightColor = new Vector3(1f, 1f, 1f);
						}
						if (modelMesh.Name == "flame")
						{
							basicEffect.AmbientLightColor = this.rAmbient[this.rrcount % this.rAmbient.Length];
						}
						if (this.reducer > 0f)
						{
							basicEffect.DirectionalLight1.Enabled = true;
							basicEffect.DirectionalLight1.Direction = this.directLight;
							basicEffect.DirectionalLight1.DiffuseColor = new Vector3(0.8f, 1f, 1f) * this.lscale * this.reducer;
						}
						basicEffect.DirectionalLight0.Direction = sundir;
						basicEffect.DirectionalLight0.DiffuseColor = this.diffu;
					}
					modelMesh.Draw();
				}
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0014B6BC File Offset: 0x001498BC
		private void dentmex()
		{
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0014B6BE File Offset: 0x001498BE
		private void dentmex2()
		{
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0014B6C0 File Offset: 0x001498C0
		private void animateparts()
		{
			if (this.commandFlag == 1)
			{
				this.commandFlag = 0;
				this.gemmax = 0f;
				if (this.door1 == 1)
				{
					this.door1 = 8;
				}
			}
			if (this.commandFlag == 2)
			{
				this.commandFlag = 0;
				if (this.door1 == 10)
				{
					this.door1 = 3;
				}
				float[] array = new float[15];
				this.region1 = array;
				float[] array2 = new float[15];
				this.region2 = array2;
			}
			if (this.door1 == 8)
			{
				this.doorVar1 -= 1.6f;
				this.doorBone.Transform = Matrix.CreateRotationZ(MathHelper.ToRadians(this.doorVar1)) * this.doorTransform;
				Vector3 vector = Vector3.Transform(new Vector3(6f, 282f, 0f), this.doorBone.Transform);
				vector = Vector3.Transform(vector, this.orientation * Matrix.CreateTranslation(this.position));
				float num;
				this.GetHeight(ref this.allheights, vector, out num);
				if (vector.Y <= num)
				{
					this.door1 = 9;
				}
			}
			if (this.door1 == 9)
			{
				this.doorVar2 += 3f;
				this.door2Bone.Transform = Matrix.CreateTranslation(0f, this.doorVar2, 0f) * this.door2Transform;
				if (this.doorVar2 >= 139.5f)
				{
					this.door1 = 10;
					Vector3 vector2 = Vector3.Transform(new Vector3(-3.6f, 288f, -75f), this.doorBone.Transform);
					vector2 = Vector3.Transform(vector2, this.orientation * Matrix.CreateTranslation(this.position));
					this.region1[0] = vector2.X;
					this.region1[1] = vector2.Y;
					this.region1[2] = vector2.Z;
					vector2 = Vector3.Transform(new Vector3(-3.6f, 288f, 75f), this.doorBone.Transform);
					vector2 = Vector3.Transform(vector2, this.orientation * Matrix.CreateTranslation(this.position));
					this.region1[3] = vector2.X;
					this.region1[4] = vector2.Y;
					this.region1[5] = vector2.Z;
					vector2 = Vector3.Transform(new Vector3(96f, -20.34f, -66f), this.orientation * Matrix.CreateTranslation(this.position));
					this.region1[6] = vector2.X;
					this.region1[7] = vector2.Y;
					this.region1[8] = vector2.Z;
					vector2 = Vector3.Transform(new Vector3(66f, -20.34f, -96f), this.orientation * Matrix.CreateTranslation(this.position));
					this.region1[9] = vector2.X;
					this.region1[10] = vector2.Y;
					this.region1[11] = vector2.Z;
					vector2 = Vector3.Transform(new Vector3(300f, 288f, 75f), this.doorBone.Transform);
					vector2 = Vector3.Transform(vector2, this.orientation * Matrix.CreateTranslation(this.position));
					vector2 = Vector3.Normalize(new Vector3(this.region1[3], this.region1[4], this.region1[5]) - new Vector3(vector2.X, vector2.Y, vector2.Z));
					this.region1[12] = vector2.X;
					this.region1[13] = vector2.Y;
					this.region1[14] = vector2.Z;
					vector2 = Vector3.Transform(new Vector3(36.9f, 1.35f, -67.8f), this.orientation * Matrix.CreateTranslation(this.position));
					this.region2[0] = vector2.X;
					this.region2[1] = vector2.Y;
					this.region2[2] = vector2.Z;
					vector2 = Vector3.Transform(new Vector3(67.8f, 1.35f, -37.8f), this.orientation * Matrix.CreateTranslation(this.position));
					this.region2[3] = vector2.X;
					this.region2[4] = vector2.Y;
					this.region2[5] = vector2.Z;
					vector2 = Vector3.Transform(new Vector3(-12.66f, 1.35f, 40.050003f), this.orientation * Matrix.CreateTranslation(this.position));
					this.region2[6] = vector2.X;
					this.region2[7] = vector2.Y;
					this.region2[8] = vector2.Z;
					vector2 = Vector3.Transform(new Vector3(-42.66f, 1.35f, 10.05f), this.orientation * Matrix.CreateTranslation(this.position));
					this.region2[9] = vector2.X;
					this.region2[10] = vector2.Y;
					this.region2[11] = vector2.Z;
					vector2 = Vector3.Transform(new Vector3(67.8f, -300f, -37.8f), this.orientation * Matrix.CreateTranslation(this.position));
					vector2 = Vector3.Normalize(new Vector3(this.region2[3], this.region2[4], this.region2[5]) - new Vector3(vector2.X, vector2.Y, vector2.Z));
					this.region2[12] = vector2.X;
					this.region2[13] = vector2.Y;
					this.region2[14] = vector2.Z;
					this.rampSwitch = 1;
				}
			}
			if (this.door1 == 3)
			{
				this.doorVar2 -= 4f;
				this.door2Bone.Transform = Matrix.CreateTranslation(0f, this.doorVar2, 0f) * this.door2Transform;
				if (this.doorVar2 <= 0f)
				{
					this.door1 = 2;
				}
			}
			if (this.door1 == 2)
			{
				this.doorVar1 += 2f;
				if (this.doorVar1 >= 0f)
				{
					this.door1 = 1;
					this.doorVar1 = 0f;
				}
				this.doorBone.Transform = Matrix.CreateRotationZ(MathHelper.ToRadians(this.doorVar1)) * this.doorTransform;
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0014BD6C File Offset: 0x00149F6C
		public void GetHeightAndNormal(ref int[,] heightData, ref Vector3[,] normals, Vector3 position, out float height, out Vector3 normal)
		{
			if (Lander.nearfarm)
			{
				this.GetHeightFarm(ref heightData, position, out height, out normal, false);
				return;
			}
			this.heightmapWidth = (float)(this.bitmap - 1) * this.gridScale;
			position.X = (position.X % this.heightmapWidth + 1.5f * this.heightmapWidth) % this.heightmapWidth;
			position.Z = (position.Z % this.heightmapWidth + 1.5f * this.heightmapWidth) % this.heightmapWidth;
			Vector3 vector = position;
			int num = (int)vector.X / (int)this.gridScale;
			int num2 = (int)vector.Z / (int)this.gridScale;
			float num3 = vector.X % this.gridScale / this.gridScale;
			float num4 = vector.Z % this.gridScale / this.gridScale;
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
			float num7 = MathHelper.Lerp((float)heightData[num, num2], (float)heightData[num5, num2], num3);
			float num8 = MathHelper.Lerp((float)heightData[num, num6], (float)heightData[num5, num6], num3);
			height = MathHelper.Lerp(num7, num8, num4);
			Vector3 vector2 = Vector3.Lerp(normals[num, num2], normals[num5, num2], num3);
			Vector3 vector3 = Vector3.Lerp(normals[num, num6], normals[num5, num6], num3);
			normal = Vector3.Lerp(vector2, vector3, num4);
			normal = Vector3.Normalize(normal);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0014BF2C File Offset: 0x0014A12C
		public void GetHeight(ref int[,] heightData, Vector3 position, out float height)
		{
			if (Lander.nearfarm)
			{
				Vector3 vector;
				this.GetHeightFarm(ref heightData, position, out height, out vector, true);
				return;
			}
			this.heightmapWidth = (float)(this.bitmap - 1) * this.gridScale;
			position.X = (position.X % this.heightmapWidth + 1.5f * this.heightmapWidth) % this.heightmapWidth;
			position.Z = (position.Z % this.heightmapWidth + 1.5f * this.heightmapWidth) % this.heightmapWidth;
			Vector3 vector2 = position;
			int num = (int)vector2.X / (int)this.gridScale;
			int num2 = (int)vector2.Z / (int)this.gridScale;
			float num3 = vector2.X % this.gridScale / this.gridScale;
			float num4 = vector2.Z % this.gridScale / this.gridScale;
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
			float num7 = MathHelper.Lerp((float)heightData[num, num2], (float)heightData[num5, num2], num3);
			float num8 = MathHelper.Lerp((float)heightData[num, num6], (float)heightData[num5, num6], num3);
			height = MathHelper.Lerp(num7, num8, num4);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0014C07C File Offset: 0x0014A27C
		public void GetHeightFarm(ref int[,] heights, Vector3 position, out float height, out Vector3 normal, bool onlyheight)
		{
			Vector3 vector = new Vector3(3000f - Lander.farmLocation.X, 0f, 3000f - Lander.farmLocation.Z);
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

		// Token: 0x06000646 RID: 1606 RVA: 0x0014C384 File Offset: 0x0014A584
		public void ShipHeightandNormal(Vector3 position, out float height, out Vector3 normal)
		{
			int num = 81;
			float num2 = 102.5f;
			this.heightmapWidth = (float)(num - 1) * num2;
			position.X = ((position.X - this.dropship.X) % this.heightmapWidth + 1.5f * this.heightmapWidth) % this.heightmapWidth;
			position.Z = ((position.Z - this.dropship.Z) % this.heightmapWidth + 1.5f * this.heightmapWidth) % this.heightmapWidth;
			Vector3 vector = position;
			int num3 = (int)vector.X / (int)num2;
			int num4 = (int)vector.Z / (int)num2;
			float num5 = vector.X % num2 / num2;
			float num6 = vector.Z % num2 / num2;
			int num7 = num3 + 1;
			int num8 = num4 + 1;
			if (num7 > num - 2)
			{
				num7 = 0;
			}
			if (num8 > num - 2)
			{
				num8 = 0;
			}
			float num9 = this.dropship.Y + MathHelper.Lerp(this.drophite[num3, num4], this.drophite[num7, num4], num5);
			float num10 = this.dropship.Y + MathHelper.Lerp(this.drophite[num3, num8], this.drophite[num7, num8], num5);
			height = MathHelper.Lerp(num9, num10, num6);
			normal = Vector3.Normalize(new Vector3(-this.drophite[num7, num4] + this.drophite[num3, num4], 102.5f, -this.drophite[num3, num8] + this.drophite[num3, num4]));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0014C528 File Offset: 0x0014A728
		public void GetHeightShip(Vector3 position, out float height)
		{
			int num = 81;
			float num2 = 102.5f;
			this.heightmapWidth = (float)(num - 1) * num2;
			position.X = ((position.X - this.dropship.X) % this.heightmapWidth + 1.5f * this.heightmapWidth) % this.heightmapWidth;
			position.Z = ((position.Z - this.dropship.Z) % this.heightmapWidth + 1.5f * this.heightmapWidth) % this.heightmapWidth;
			Vector3 vector = position;
			int num3 = (int)vector.X / (int)num2;
			int num4 = (int)vector.Z / (int)num2;
			float num5 = vector.X % num2 / num2;
			float num6 = vector.Z % num2 / num2;
			int num7 = num3 + 1;
			int num8 = num4 + 1;
			if (num7 > num - 2)
			{
				num7 = 0;
			}
			if (num8 > num - 2)
			{
				num8 = 0;
			}
			float num9 = this.dropship.Y + MathHelper.Lerp(this.drophite[num3, num4], this.drophite[num7, num4], num5);
			float num10 = this.dropship.Y + MathHelper.Lerp(this.drophite[num3, num8], this.drophite[num7, num8], num5);
			height = MathHelper.Lerp(num9, num10, num6);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0014C6B0 File Offset: 0x0014A8B0
		public Lander()
		{
			float[] array = new float[15];
			this.region1 = array;
			float[] array2 = new float[15];
			this.region2 = array2;
			this.directLight = Vector3.Zero;
			this.amb = new Vector3(0.3f, 0.3f, 0.3f);
			this.diffu = new Vector3(1.2f, 1.1f, 1f);
			this.rFlame = new float[] { 0.6f, 0.82f, 0.63f, 0.79f, 0.62f, 0.82f, 0.65f, 0.82f };
			this.rAmbient = new Vector3[]
			{
				new Vector3(0.2f, 0.2f, 1f),
				new Vector3(0.5f, 1f, 0.7f),
				new Vector3(0.3f, 2f, 0.6f),
				new Vector3(0.5f, 0.9f, 0.2f),
				new Vector3(0.6f, 0.4f, 1.6f),
				new Vector3(1f, 1f, 0.7f)
			};
			base..ctor();
		}

		// Token: 0x0400185F RID: 6239
		private Vector2 ggO = Vector2.Zero;

		// Token: 0x04001860 RID: 6240
		public bool radioFixed;

		// Token: 0x04001861 RID: 6241
		public float mytimer;

		// Token: 0x04001862 RID: 6242
		public static Vector3 farmLocation = Vector3.Zero;

		// Token: 0x04001863 RID: 6243
		public static bool nearfarm = false;

		// Token: 0x04001864 RID: 6244
		private KeyboardState keyState;

		// Token: 0x04001865 RID: 6245
		private KeyboardState prevkeyState;

		// Token: 0x04001866 RID: 6246
		private MouseState mouseState;

		// Token: 0x04001867 RID: 6247
		private MouseState prevMouse;

		// Token: 0x04001868 RID: 6248
		public float curve = 0.5f;

		// Token: 0x04001869 RID: 6249
		public float origthrust = 1.5f;

		// Token: 0x0400186A RID: 6250
		public float thrust = 1.5f;

		// Token: 0x0400186B RID: 6251
		private float maxspeed = 250f;

		// Token: 0x0400186C RID: 6252
		private float gravity = -0.65f;

		// Token: 0x0400186D RID: 6253
		private float terminalveloc = -365f;

		// Token: 0x0400186E RID: 6254
		public bool onDropship;

		// Token: 0x0400186F RID: 6255
		public bool insideDropship;

		// Token: 0x04001870 RID: 6256
		public Vector3 position = new Vector3(4000f, 0f, 2000f);

		// Token: 0x04001871 RID: 6257
		public Vector3 dropshipVeloc;

		// Token: 0x04001872 RID: 6258
		public Vector3 dropship;

		// Token: 0x04001873 RID: 6259
		private Vector3 reflectnormal;

		// Token: 0x04001874 RID: 6260
		public float landerDirectionX;

		// Token: 0x04001875 RID: 6261
		public float landerDirectionZ;

		// Token: 0x04001876 RID: 6262
		public float shieldhit;

		// Token: 0x04001877 RID: 6263
		private Vector3 lasthite;

		// Token: 0x04001878 RID: 6264
		private Vector3 landerBottom;

		// Token: 0x04001879 RID: 6265
		public Vector3 surface;

		// Token: 0x0400187A RID: 6266
		public int bitmap;

		// Token: 0x0400187B RID: 6267
		public float gridScale;

		// Token: 0x0400187C RID: 6268
		private float rayDistance = 5f;

		// Token: 0x0400187D RID: 6269
		private float heightmapWidth;

		// Token: 0x0400187E RID: 6270
		private float turnAmountZ;

		// Token: 0x0400187F RID: 6271
		private float turnAmountX;

		// Token: 0x04001880 RID: 6272
		public Vector3 orbitPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04001881 RID: 6273
		private Model model;

		// Token: 0x04001882 RID: 6274
		private Vector3 newPosition;

		// Token: 0x04001883 RID: 6275
		public float waterlevel;

		// Token: 0x04001884 RID: 6276
		public float camrot;

		// Token: 0x04001885 RID: 6277
		private int flameFlag = 1;

		// Token: 0x04001886 RID: 6278
		public Vector3 ground;

		// Token: 0x04001887 RID: 6279
		public float lscale;

		// Token: 0x04001888 RID: 6280
		public float lander2ground;

		// Token: 0x04001889 RID: 6281
		public Matrix orientation = Matrix.Identity;

		// Token: 0x0400188A RID: 6282
		private Matrix orientationx = Matrix.Identity;

		// Token: 0x0400188B RID: 6283
		private Matrix lastorientation = Matrix.Identity;

		// Token: 0x0400188C RID: 6284
		private Matrix wheelRollMatrix = Matrix.Identity;

		// Token: 0x0400188D RID: 6285
		private Matrix flameMatrix = Matrix.Identity;

		// Token: 0x0400188E RID: 6286
		public float gemmax = 1f;

		// Token: 0x0400188F RID: 6287
		public int gems;

		// Token: 0x04001890 RID: 6288
		public Vector3 movement;

		// Token: 0x04001891 RID: 6289
		public Vector3 velocity = Vector3.Zero;

		// Token: 0x04001892 RID: 6290
		public Vector3 normal;

		// Token: 0x04001893 RID: 6291
		public float maxthrust = 1.6f;

		// Token: 0x04001894 RID: 6292
		public float speed;

		// Token: 0x04001895 RID: 6293
		public float normalizedSpeed;

		// Token: 0x04001896 RID: 6294
		private ModelBone podBone;

		// Token: 0x04001897 RID: 6295
		private ModelBone doorBone;

		// Token: 0x04001898 RID: 6296
		private ModelBone door2Bone;

		// Token: 0x04001899 RID: 6297
		private ModelBone flameBone;

		// Token: 0x0400189A RID: 6298
		private Matrix podTransform;

		// Token: 0x0400189B RID: 6299
		private Matrix doorTransform;

		// Token: 0x0400189C RID: 6300
		private Matrix door2Transform;

		// Token: 0x0400189D RID: 6301
		private Matrix flameTransform;

		// Token: 0x0400189E RID: 6302
		public int commandFlag;

		// Token: 0x0400189F RID: 6303
		public int rampSwitch;

		// Token: 0x040018A0 RID: 6304
		public int door1 = 1;

		// Token: 0x040018A1 RID: 6305
		private float doorVar1;

		// Token: 0x040018A2 RID: 6306
		private float doorVar2;

		// Token: 0x040018A3 RID: 6307
		public int impact;

		// Token: 0x040018A4 RID: 6308
		public float diff = 80f;

		// Token: 0x040018A5 RID: 6309
		private float offset = 60f;

		// Token: 0x040018A6 RID: 6310
		private float movedistx;

		// Token: 0x040018A7 RID: 6311
		private Vector3 deltax;

		// Token: 0x040018A8 RID: 6312
		private float movedist1;

		// Token: 0x040018A9 RID: 6313
		private Vector3 delta1 = new Vector3(1f, 0f, 0f);

		// Token: 0x040018AA RID: 6314
		private Vector3 grav = Vector3.Zero;

		// Token: 0x040018AB RID: 6315
		private GraphicsDevice device;

		// Token: 0x040018AC RID: 6316
		public int vi;

		// Token: 0x040018AD RID: 6317
		public int shockhit;

		// Token: 0x040018AE RID: 6318
		public float directme = 3f;

		// Token: 0x040018AF RID: 6319
		public int[,] allheights;

		// Token: 0x040018B0 RID: 6320
		public int vbi;

		// Token: 0x040018B1 RID: 6321
		public Vector3 goof = Vector3.Zero;

		// Token: 0x040018B2 RID: 6322
		public int dentme;

		// Token: 0x040018B3 RID: 6323
		public int dentme2;

		// Token: 0x040018B4 RID: 6324
		private int dentpercent;

		// Token: 0x040018B5 RID: 6325
		public int dentpercent2;

		// Token: 0x040018B6 RID: 6326
		public float[] region1;

		// Token: 0x040018B7 RID: 6327
		public float[] region2;

		// Token: 0x040018B8 RID: 6328
		public float rightTrigger;

		// Token: 0x040018B9 RID: 6329
		public float leftTrigger;

		// Token: 0x040018BA RID: 6330
		private float reducer;

		// Token: 0x040018BB RID: 6331
		private Vector3 directLight;

		// Token: 0x040018BC RID: 6332
		public Vector3 amb;

		// Token: 0x040018BD RID: 6333
		public Vector3 diffu;

		// Token: 0x040018BE RID: 6334
		private Matrix[] boneTransforms;

		// Token: 0x040018BF RID: 6335
		private float[] rFlame;

		// Token: 0x040018C0 RID: 6336
		private Vector3[] rAmbient;

		// Token: 0x040018C1 RID: 6337
		private int rrcount;

		// Token: 0x040018C2 RID: 6338
		public int altitude;

		// Token: 0x040018C3 RID: 6339
		public float[,] drophite;

		// Token: 0x040018C4 RID: 6340
		private ScreenManager sc;

		// Token: 0x040018C5 RID: 6341
		private ContentManager content;
	}
}
