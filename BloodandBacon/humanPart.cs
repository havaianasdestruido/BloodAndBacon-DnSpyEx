using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000056 RID: 86
	public class humanPart
	{
		// Token: 0x06000343 RID: 835 RVA: 0x000D18F0 File Offset: 0x000CFAF0
		public humanPart(int i)
		{
			this.rr[0] = new Random(i);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000D1946 File Offset: 0x000CFB46
		public void assignRandom(int seed)
		{
			this.rr[0] = new Random(seed);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000D1958 File Offset: 0x000CFB58
		public void init(float bounce, float scale, float ratio, Matrix startpos, Vector3 veloc, int mybool, bool localShell, float grav, int rbounce, float ta, float tb, bool landupright, bool itBleeds, bool kickable, int boarSEED, int mult)
		{
			this.partID = boarSEED * mult;
			this.mult = mult;
			this.lastKicked = 0;
			this.emitVec = this.emitVec;
			this.ratio = ratio;
			this.scale = scale;
			this.bounce = bounce;
			this.localShell = localShell;
			this.randBounce = rbounce;
			this.landupright = landupright;
			this.itBleeds = itBleeds;
			this.kickable = kickable;
			this.alive = 0;
			this.kicked = 120;
			this.move = mybool;
			this.firstHit = 0;
			this.secondhit = -1;
			this.gravity = grav;
			this.gash = this.rr[0].Next(50, 250);
			this.freq = this.rr[0].Next(4, 16);
			startpos.Decompose(out this.dump1, out this.qq, out this.dump2);
			this.myRot = Matrix.CreateFromQuaternion(this.qq);
			this.turnRate = (float)this.rr[0].Next((int)ta, (int)tb) / 1000f;
			this.inertRot = Matrix.CreateRotationY(0.002f) * Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-veloc, Vector3.Up)), this.turnRate);
			this.v = Vector3.Transform(Vector3.Up, this.myRot);
			this.result = Vector3.Dot(this.v, Vector3.Up);
			this.scaler = (2f - Math.Abs(this.result) * (2f - ratio * 2f)) * scale;
			if (this.move > 0)
			{
				this.velocity = veloc;
			}
			else
			{
				this.velocity = Vector3.Zero;
			}
			this.mypos = Vector3.Transform(Vector3.Zero, startpos);
			this.transform = Matrix.CreateScale(scale) * this.myRot * Matrix.CreateTranslation(this.mypos);
			this.oldpos = this.mypos;
			this.oldRot = this.myRot;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000D1B68 File Offset: 0x000CFD68
		public void Update(ref float[,] heights)
		{
			this.mypos.X = this.mypos.X + this.velocity.X;
			this.mypos.Y = this.mypos.Y + this.velocity.Y;
			this.mypos.Z = this.mypos.Z + this.velocity.Z;
			this.velocity.Y = this.velocity.Y + this.gravity;
			if (this.velocity.Y < -7f)
			{
				this.velocity.Y = -7f;
			}
			this.velocity += this.vortex;
			this.vortex *= 0.98f;
			Matrix.Multiply(ref this.myRot, ref this.inertRot, out this.myRot);
			humanPart.GetHeightFast(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
			Vector3 vector = Vector3.Normalize(Vector3.Transform(Vector3.Up, this.myRot));
			float num = Vector3.Dot(vector, Vector3.Up);
			this.scaler = (2f - Math.Abs(num) * (2f - this.ratio * 2f)) * (this.scale * 3f);
			if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
			{
				this.firstHit++;
				if (this.velocity.Y < -1f)
				{
					this.secondhit++;
				}
				Vector3 vector2 = new Vector3((float)this.rr[0].Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr[0].Next(-this.randBounce, this.randBounce) / 200f);
				if (Math.Abs(this.velocity.Y) > 1f)
				{
					this.velocity = Vector3.Reflect(this.velocity, this.normal + vector2) * new Vector3(0.98f, this.bounce, 0.98f);
				}
				else
				{
					this.velocity *= new Vector3(0.95f, 0f, 0.95f);
				}
				if (this.normal.Y < 0.95f)
				{
					this.velocity += new Vector3(this.normal.X, 0f, this.normal.Z) * 1f;
				}
				this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-this.velocity, Vector3.Up)), this.turnRate);
				if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Z) < 0.2f || this.sloper > 0f) && this.sloper < 1f)
				{
					this.itBleeds = false;
					if (this.landupright)
					{
						if (this.sloper == 0f)
						{
							this.myRot.Decompose(out this.scale0, out this.rotation0, out this.position0);
							Quaternion.Normalize(this.rotation0);
							Matrix matrix = this.myRot;
							matrix.Up = this.normal;
							if (this.rr[0].Next(1, 100) < 50)
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
						this.sloper += 0.3f;
					}
				}
				if (this.sloper >= 1f)
				{
					this.velocity = Vector3.Zero;
					this.move = 0;
					this.sloper = 0f;
				}
				this.mypos.Y = this.scaler + this.groundHeight;
			}
			Matrix.CreateScale(this.scale, out this.m1);
			Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z, out this.m4);
			Matrix.Multiply(ref this.m1, ref this.myRot, out this.m3);
			Matrix.Multiply(ref this.m3, ref this.m4, out this.transform);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000D208C File Offset: 0x000D028C
		public void UpdateNoHeight()
		{
			this.groundHeight = 0.75f;
			this.normal = Vector3.Up;
			this.mypos.X = this.mypos.X + this.velocity.X;
			this.mypos.Y = this.mypos.Y + this.velocity.Y;
			this.mypos.Z = this.mypos.Z + this.velocity.Z;
			this.velocity.Y = this.velocity.Y + this.gravity;
			if (this.velocity.Y < -6f)
			{
				this.velocity.Y = -7f;
			}
			this.myRot *= this.inertRot;
			Vector3 vector = Vector3.Normalize(Vector3.Transform(Vector3.Up, this.myRot));
			float num = Vector3.Dot(vector, Vector3.Up);
			this.scaler = (2f - Math.Abs(num) * (2f - this.ratio * 2f)) * this.scale;
			if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
			{
				this.firstHit++;
				if (this.velocity.Y < -1f)
				{
					this.secondhit++;
				}
				Vector3 vector2 = new Vector3((float)this.rr[0].Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr[0].Next(-this.randBounce, this.randBounce) / 200f);
				if (Math.Abs(this.velocity.Y) > 2f)
				{
					this.velocity = Vector3.Reflect(this.velocity, this.normal + vector2) * new Vector3(0.94f, this.bounce, 0.94f);
				}
				else
				{
					this.velocity *= new Vector3(0.75f, 0f, 0.75f);
				}
				if (this.normal.Y < 0.95f)
				{
					this.velocity += new Vector3(this.normal.X, 0f, this.normal.Z) * 1f;
				}
				this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-this.velocity, Vector3.Up)), this.turnRate);
				if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Z) < 0.2f || this.sloper > 0f) && this.sloper < 1f)
				{
					if (this.landupright)
					{
						if (this.sloper == 0f)
						{
							this.myRot.Decompose(out this.scale0, out this.rotation0, out this.position0);
							Quaternion.Normalize(this.rotation0);
							Matrix matrix = this.myRot;
							matrix.Up = this.normal;
							if (this.rr[0].Next(1, 100) < 50)
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
						this.sloper += 0.3f;
					}
				}
				if (this.sloper >= 1f)
				{
					this.velocity = Vector3.Zero;
					this.move = 0;
					this.sloper = 0f;
				}
				this.mypos.Y = this.scaler + this.groundHeight;
			}
			this.transform = Matrix.CreateScale(this.scale) * this.myRot * Matrix.CreateTranslation(this.mypos);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000D2538 File Offset: 0x000D0738
		private static void GetHeightFast(ref float[,] heights, ref Vector3 pos, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(pos.X / humanPart.unit, 0f, (float)(humanPart.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(pos.Z / humanPart.unit, 0f, (float)(humanPart.bitmap - 2));
			float num3 = pos.X % humanPart.unit / humanPart.unit;
			float num4 = pos.Z % humanPart.unit / humanPart.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x04000DBA RID: 3514
		private Matrix m1;

		// Token: 0x04000DBB RID: 3515
		private Matrix m2;

		// Token: 0x04000DBC RID: 3516
		private Matrix m3;

		// Token: 0x04000DBD RID: 3517
		private Matrix m4;

		// Token: 0x04000DBE RID: 3518
		private float f1;

		// Token: 0x04000DBF RID: 3519
		private float f2;

		// Token: 0x04000DC0 RID: 3520
		private Vector3 v1;

		// Token: 0x04000DC1 RID: 3521
		private Vector3 v2;

		// Token: 0x04000DC2 RID: 3522
		public bool itBleeds;

		// Token: 0x04000DC3 RID: 3523
		public bool kickable;

		// Token: 0x04000DC4 RID: 3524
		public static int bitmap;

		// Token: 0x04000DC5 RID: 3525
		public static float unit;

		// Token: 0x04000DC6 RID: 3526
		private Vector3 scale0;

		// Token: 0x04000DC7 RID: 3527
		private Quaternion rotation0;

		// Token: 0x04000DC8 RID: 3528
		private Quaternion rotation1;

		// Token: 0x04000DC9 RID: 3529
		private Vector3 position0;

		// Token: 0x04000DCA RID: 3530
		private float gravity;

		// Token: 0x04000DCB RID: 3531
		public float scale;

		// Token: 0x04000DCC RID: 3532
		public float ratio;

		// Token: 0x04000DCD RID: 3533
		public float bounce;

		// Token: 0x04000DCE RID: 3534
		private float groundHeight;

		// Token: 0x04000DCF RID: 3535
		public Matrix myRot;

		// Token: 0x04000DD0 RID: 3536
		public Matrix inertRot;

		// Token: 0x04000DD1 RID: 3537
		public Matrix oldRot;

		// Token: 0x04000DD2 RID: 3538
		public float sloper;

		// Token: 0x04000DD3 RID: 3539
		public float turnRate = 0.3f;

		// Token: 0x04000DD4 RID: 3540
		public int move;

		// Token: 0x04000DD5 RID: 3541
		public Vector3 mypos;

		// Token: 0x04000DD6 RID: 3542
		public Vector3 oldpos;

		// Token: 0x04000DD7 RID: 3543
		public Vector3 normal;

		// Token: 0x04000DD8 RID: 3544
		public float scaler;

		// Token: 0x04000DD9 RID: 3545
		public Vector3 velocity;

		// Token: 0x04000DDA RID: 3546
		public Matrix transform;

		// Token: 0x04000DDB RID: 3547
		public int tint;

		// Token: 0x04000DDC RID: 3548
		public int tint2;

		// Token: 0x04000DDD RID: 3549
		public int firstHit;

		// Token: 0x04000DDE RID: 3550
		public int secondhit;

		// Token: 0x04000DDF RID: 3551
		public bool localShell;

		// Token: 0x04000DE0 RID: 3552
		public bool landupright;

		// Token: 0x04000DE1 RID: 3553
		public int randBounce = 10;

		// Token: 0x04000DE2 RID: 3554
		public int alive;

		// Token: 0x04000DE3 RID: 3555
		public int kicked;

		// Token: 0x04000DE4 RID: 3556
		public int bleed = 260;

		// Token: 0x04000DE5 RID: 3557
		public int gash;

		// Token: 0x04000DE6 RID: 3558
		public int freq;

		// Token: 0x04000DE7 RID: 3559
		public Vector3 vortex = Vector3.Zero;

		// Token: 0x04000DE8 RID: 3560
		public Vector3 emitVec;

		// Token: 0x04000DE9 RID: 3561
		public int partID;

		// Token: 0x04000DEA RID: 3562
		public int mult;

		// Token: 0x04000DEB RID: 3563
		public readonly Random[] rr = new Random[2];

		// Token: 0x04000DEC RID: 3564
		public int lastKicked;

		// Token: 0x04000DED RID: 3565
		private Vector3 dump1;

		// Token: 0x04000DEE RID: 3566
		private Vector3 dump2;

		// Token: 0x04000DEF RID: 3567
		private Quaternion qq;

		// Token: 0x04000DF0 RID: 3568
		private Vector3 v;

		// Token: 0x04000DF1 RID: 3569
		private float result;
	}
}
