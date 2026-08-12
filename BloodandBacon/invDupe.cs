using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200010B RID: 267
	public class invDupe
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x002516EC File Offset: 0x0024F8EC
		public invDupe(int i)
		{
			this.rr = new Randoms(i);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00251754 File Offset: 0x0024F954
		public void init(float bounce, float scale, float ratio, Matrix startpos, Vector3 veloc, int move, float grav, int rbounce, float ta, float tb, int landupright, bool oblong)
		{
			this.isLocal = true;
			this.age = 0;
			this.ratio = ratio;
			this.scale = new Vector3(scale, scale, scale);
			this.bounce = bounce;
			this.randBounce = rbounce;
			this.landupright = landupright;
			this.move = move;
			this.sloper = 0f;
			if (oblong)
			{
				this.scale = new Vector3(scale * ((float)this.rr.Next(80, 150) / 100f), scale * ((float)this.rr.Next(80, 120) / 100f), scale * ((float)this.rr.Next(80, 150) / 100f));
			}
			this.firstHit = 0;
			this.gravity = grav;
			startpos.Decompose(out this.dump1, out this.qq, out this.dump2);
			this.myRot = Matrix.CreateFromQuaternion(this.qq);
			this.turnRate = (float)this.rr.Next((int)ta, (int)tb) / 1000f;
			this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-veloc, Vector3.Up)), this.turnRate / 3f);
			this.v = Vector3.Transform(Vector3.Up, this.myRot);
			this.result = Vector3.Dot(this.v, Vector3.Up);
			this.scaler = (2f - Math.Abs(this.result) * (2f - ratio * 2f)) * scale;
			if (move > 0)
			{
				this.velocity = veloc;
			}
			else
			{
				this.velocity = Vector3.Zero;
			}
			this.mypos = Vector3.Transform(Vector3.Zero, startpos);
			this.transform = Matrix.CreateScale(scale) * this.myRot * Matrix.CreateTranslation(this.mypos);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00251938 File Offset: 0x0024FB38
		public void initGrenade(Vector3 startpos, Vector3 veloc, byte bounce, int seed, bool isLocal, int age, int small)
		{
			this.rr.changeSeed(seed);
			this.isLocal = isLocal;
			this.age = age;
			this.ratio = 0.5f;
			this.scale = new Vector3(2f, 2f, 2f);
			this.farmerowned = false;
			if (small == 2)
			{
				this.scale = new Vector3(2f, 2f, 2f);
				this.farmerowned = true;
			}
			if (small == 0)
			{
				this.scale = new Vector3(1f, 1f, 1f);
			}
			this.bounce = (float)bounce / 255f;
			this.randBounce = 35;
			this.landupright = 0;
			this.move = 1;
			this.firstHit = 0;
			this.gravity = -0.09f;
			this.sloper = 0f;
			this.myRot = Matrix.CreateFromYawPitchRoll((float)this.rr.Next(-860, 890) / 100f, (float)this.rr.Next(-860, 890) / 100f, (float)this.rr.Next(-860, 890) / 100f);
			this.turnRate = (float)this.rr.Next(260, 390) / 1000f;
			this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-veloc, Vector3.Up)), this.turnRate / 2f);
			this.v = Vector3.Transform(Vector3.Up, this.myRot);
			this.result = Vector3.Dot(this.v, Vector3.Up);
			this.scaler = (2f - Math.Abs(this.result) * (2f - this.ratio * 2f)) * this.scale.X;
			this.velocity = veloc;
			this.mypos = startpos;
			this.transform = Matrix.CreateScale(this.scale) * this.myRot * Matrix.CreateTranslation(this.mypos);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00251B5C File Offset: 0x0024FD5C
		public void Update(ref float[,] heights)
		{
			this.mypos.X = this.mypos.X + this.velocity.X;
			this.mypos.Y = this.mypos.Y + this.velocity.Y;
			this.mypos.Z = this.mypos.Z + this.velocity.Z;
			this.velocity.Y = this.velocity.Y + this.gravity;
			this.myRot *= this.inertRot;
			if (this.velocity.Y < -6f)
			{
				this.velocity.Y = -7f;
			}
			invDupe.GetHeightFast(heights, new Vector2(this.mypos.X, this.mypos.Z), out this.groundHeight, out this.normal);
			Vector3 vector = Vector3.Normalize(Vector3.Transform(Vector3.Up, this.myRot));
			float num = Vector3.Dot(vector, Vector3.Up);
			this.scaler = (2f - Math.Abs(num) * (2f - this.ratio * 2f)) * this.scale.X;
			if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
			{
				if (this.velocity.LengthSquared() > 4f)
				{
					this.firstHit++;
				}
				Vector3 vector2 = new Vector3((float)this.rr.Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr.Next(-this.randBounce, this.randBounce) / 200f);
				if (Math.Abs(this.velocity.Y) > 2f || Math.Abs(this.scaler + this.groundHeight - this.mypos.Y) > 5f)
				{
					this.velocity = Vector3.Reflect(this.velocity, this.normal + vector2) * new Vector3(0.94f, this.bounce, 0.94f);
				}
				else
				{
					this.velocity *= new Vector3(0.87f, 0f, 0.87f);
				}
				if (this.normal.Y < 0.95f)
				{
					this.velocity += new Vector3(this.normal.X, 0f, this.normal.Z);
				}
				this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-this.velocity, Vector3.Up)), this.turnRate);
				if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Z) < 0.2f || this.sloper > 0f) && this.sloper < 1f)
				{
					if (this.landupright > 0)
					{
						if (this.sloper == 0f)
						{
							this.myRot.Decompose(out this.scale0, out this.rotation0, out this.position0);
							Quaternion.Normalize(this.rotation0);
							Matrix matrix = this.myRot;
							matrix.Up = this.normal;
							if (this.rr.Next(1, 100) < 50 && this.landupright == 2)
							{
								matrix.Up = -this.normal;
							}
							matrix.Right = Vector3.Cross(matrix.Forward, matrix.Up);
							matrix.Right = Vector3.Normalize(matrix.Right);
							matrix.Forward = Vector3.Cross(matrix.Up, matrix.Right);
							matrix.Forward = Vector3.Normalize(matrix.Forward);
							matrix.Decompose(out this.scale0, out this.rotation1, out this.position0);
							Quaternion.Normalize(this.rotation1);
						}
						this.sloper += 0.1f;
						this.myRot = Matrix.CreateFromQuaternion(Quaternion.Slerp(this.rotation0, this.rotation1, this.sloper));
					}
					else
					{
						this.sloper += 0.1f;
					}
				}
				if (this.sloper >= 1f)
				{
					this.velocity = Vector3.Zero;
					this.move = 0;
					this.sloper = 0f;
				}
				if (this.scaler + this.groundHeight <= this.mypos.Y + 5f)
				{
					this.mypos.Y = this.scaler + this.groundHeight;
				}
			}
			this.transform = Matrix.CreateScale(this.scale.X, this.scale.Y, this.scale.Z) * this.myRot * Matrix.CreateTranslation(this.mypos);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00252070 File Offset: 0x00250270
		public void UpdateInBarn(ref float[,] heights)
		{
			this.mypos.X = this.mypos.X + this.velocity.X;
			this.mypos.Y = this.mypos.Y + this.velocity.Y;
			this.mypos.Z = this.mypos.Z + this.velocity.Z;
			this.velocity.Y = this.velocity.Y + this.gravity;
			this.myRot *= this.inertRot;
			if (this.velocity.Y < -6f)
			{
				this.velocity.Y = -7f;
			}
			bool flag = this.x1 < this.mypos.X && this.mypos.X < this.x2 && this.z1 < this.mypos.Z && this.mypos.Z < this.z2;
			this.groundHeight = 0f;
			this.normal = Vector3.Up;
			if (!flag && !this.farmerowned)
			{
				invDupe.GetHeightFast(heights, new Vector2(this.mypos.X, this.mypos.Z), out this.groundHeight, out this.normal);
				if (this.groundHeight != 0f)
				{
					this.normal = Vector3.Zero;
					if (this.mypos.X > this.x2)
					{
						this.normal.X = -1f;
					}
					else if (this.mypos.X < this.x1)
					{
						this.normal.X = 1f;
					}
					if (this.mypos.Z < this.z1)
					{
						this.normal.Z = 1f;
					}
					else if (this.mypos.Z > this.z2)
					{
						this.normal.Z = -1f;
					}
				}
			}
			Vector3 vector = Vector3.Normalize(Vector3.Transform(Vector3.Up, this.myRot));
			float num = Vector3.Dot(vector, Vector3.Up);
			this.scaler = (2f - Math.Abs(num) * (2f - this.ratio * 2f)) * this.scale.X;
			if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
			{
				if (this.velocity.LengthSquared() > 4f)
				{
					this.firstHit++;
				}
				Vector3 vector2 = new Vector3((float)this.rr.Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr.Next(-this.randBounce, this.randBounce) / 200f);
				if (Math.Abs(this.velocity.Y) > 2f)
				{
					this.velocity = Vector3.Reflect(this.velocity, this.normal + vector2) * new Vector3(0.94f, this.bounce, 0.94f);
				}
				else
				{
					this.velocity *= new Vector3(0.87f, 0f, 0.87f);
				}
				if (this.normal.Y < 0.95f)
				{
					this.velocity += new Vector3(this.normal.X, 0f, this.normal.Z);
				}
				this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-this.velocity, Vector3.Up)), this.turnRate);
				if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Z) < 0.2f || this.sloper > 0f) && this.sloper < 1f)
				{
					if (this.landupright > 0)
					{
						if (this.sloper == 0f)
						{
							this.myRot.Decompose(out this.scale0, out this.rotation0, out this.position0);
							Quaternion.Normalize(this.rotation0);
							Matrix matrix = this.myRot;
							matrix.Up = this.normal;
							if (this.rr.Next(1, 100) < 50 && this.landupright == 2)
							{
								matrix.Up = -this.normal;
							}
							matrix.Right = Vector3.Cross(matrix.Forward, matrix.Up);
							matrix.Right = Vector3.Normalize(matrix.Right);
							matrix.Forward = Vector3.Cross(matrix.Up, matrix.Right);
							matrix.Forward = Vector3.Normalize(matrix.Forward);
							matrix.Decompose(out this.scale0, out this.rotation1, out this.position0);
							Quaternion.Normalize(this.rotation1);
						}
						this.sloper += 0.1f;
						this.myRot = Matrix.CreateFromQuaternion(Quaternion.Slerp(this.rotation0, this.rotation1, this.sloper));
					}
					else
					{
						this.sloper += 0.1f;
					}
				}
				if (this.sloper >= 1f)
				{
					this.velocity = Vector3.Zero;
					this.move = 0;
					this.sloper = 0f;
				}
				if (this.normal.Y > 0.6f)
				{
					this.mypos.Y = this.scaler + this.groundHeight;
				}
			}
			this.transform = Matrix.CreateScale(this.scale.X, this.scale.Y, this.scale.Z) * this.myRot * Matrix.CreateTranslation(this.mypos);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00252678 File Offset: 0x00250878
		private static void GetHeightFast(float[,] heights, Vector2 position, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(position.X / invDupe.unit, 0f, (float)(invDupe.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(position.Y / invDupe.unit, 0f, (float)(invDupe.bitmap - 2));
			float num3 = position.X % invDupe.unit / invDupe.unit;
			float num4 = position.Y % invDupe.unit / invDupe.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x0400270D RID: 9997
		public static int glowIndex = -1;

		// Token: 0x0400270E RID: 9998
		public static int bitmap;

		// Token: 0x0400270F RID: 9999
		public static float unit;

		// Token: 0x04002710 RID: 10000
		private float x1 = 3130f;

		// Token: 0x04002711 RID: 10001
		private float x2 = 3660f;

		// Token: 0x04002712 RID: 10002
		private float z1 = 4450f;

		// Token: 0x04002713 RID: 10003
		private float z2 = 4800f;

		// Token: 0x04002714 RID: 10004
		private Vector3 scale0;

		// Token: 0x04002715 RID: 10005
		private Quaternion rotation0;

		// Token: 0x04002716 RID: 10006
		private Quaternion rotation1;

		// Token: 0x04002717 RID: 10007
		private Vector3 position0;

		// Token: 0x04002718 RID: 10008
		public float gravity;

		// Token: 0x04002719 RID: 10009
		public Vector3 scale;

		// Token: 0x0400271A RID: 10010
		public float ratio;

		// Token: 0x0400271B RID: 10011
		public float bounce;

		// Token: 0x0400271C RID: 10012
		public float groundHeight;

		// Token: 0x0400271D RID: 10013
		public Matrix myRot;

		// Token: 0x0400271E RID: 10014
		public Matrix inertRot;

		// Token: 0x0400271F RID: 10015
		public Matrix oldRot;

		// Token: 0x04002720 RID: 10016
		public float sloper;

		// Token: 0x04002721 RID: 10017
		public float turnRate = 0.3f;

		// Token: 0x04002722 RID: 10018
		public int move;

		// Token: 0x04002723 RID: 10019
		public Vector3 mypos;

		// Token: 0x04002724 RID: 10020
		public Vector3 oldpos;

		// Token: 0x04002725 RID: 10021
		public Vector3 normal;

		// Token: 0x04002726 RID: 10022
		public float scaler;

		// Token: 0x04002727 RID: 10023
		public Vector3 velocity;

		// Token: 0x04002728 RID: 10024
		public Matrix transform;

		// Token: 0x04002729 RID: 10025
		public int tint;

		// Token: 0x0400272A RID: 10026
		public int tint2;

		// Token: 0x0400272B RID: 10027
		public int firstHit;

		// Token: 0x0400272C RID: 10028
		public bool localShell;

		// Token: 0x0400272D RID: 10029
		public bool barnok;

		// Token: 0x0400272E RID: 10030
		public int landupright;

		// Token: 0x0400272F RID: 10031
		public int randBounce = 10;

		// Token: 0x04002730 RID: 10032
		public int age;

		// Token: 0x04002731 RID: 10033
		public bool isLocal = true;

		// Token: 0x04002732 RID: 10034
		private bool farmerowned;

		// Token: 0x04002733 RID: 10035
		private Vector3 dump1;

		// Token: 0x04002734 RID: 10036
		private Vector3 dump2;

		// Token: 0x04002735 RID: 10037
		private Quaternion qq;

		// Token: 0x04002736 RID: 10038
		private Vector3 v;

		// Token: 0x04002737 RID: 10039
		private float result;

		// Token: 0x04002738 RID: 10040
		public Randoms rr;
	}
}
