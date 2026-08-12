using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000139 RID: 313
	public class dupeItem
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x002E30CE File Offset: 0x002E12CE
		public dupeItem(int i)
		{
			this.rr = new Randoms(i);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x002E3107 File Offset: 0x002E1307
		public void assignRandom(int seed)
		{
			this.rr.changeSeed(seed);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x002E3118 File Offset: 0x002E1318
		public void init(int group, float bounce, float scale, float ratio, Matrix startpos, Vector3 veloc, bool localShell, float grav, int rbounce, float ta, float tb, bool landupright, bool itBleeds, bool kickable, int boarSEED, int mult)
		{
			this.group = group;
			this.partID = (ushort)(boarSEED * mult);
			this.mult = mult;
			this.lastKicked = 0;
			this.ratio = ratio;
			this.scale = scale;
			this.bounce = bounce;
			this.localShell = localShell;
			this.randBounce = rbounce;
			this.landupright = landupright;
			this.itBleeds = itBleeds;
			this.kickable = kickable;
			this.alive = 0;
			this.kicked = 60;
			this.move = 1;
			this.firstHit = 0;
			this.secondhit = -1;
			this.gravity = grav;
			this.gash = this.rr.Next(50, 250);
			this.freq = this.rr.Next(4, 16);
			startpos.Decompose(out this.dump1, out this.qq, out this.dump2);
			this.myRot = Matrix.CreateFromQuaternion(this.qq);
			this.turnRate = (float)this.rr.Next((int)ta, (int)tb) / 1000f;
			this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-veloc, Vector3.Up)), this.turnRate);
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
			this.oldY = this.mypos.Y;
			this.oldpos = this.mypos;
			this.oldRot = this.myRot;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x002E3324 File Offset: 0x002E1524
		public void Update(ref float[,] heights)
		{
			this.oldY = this.mypos.Y;
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
			dupeItem.GetHeightSlow(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
			this.scaler = (2f - Math.Abs(Vector3.Transform(Vector3.Up, this.myRot).Y) * (2f - this.ratio * 2f)) * this.scale;
			if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
			{
				this.firstHit++;
				if (this.velocity.Y < -1f)
				{
					this.secondhit++;
				}
				Vector3 vector = new Vector3((float)this.rr.Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr.Next(-this.randBounce, this.randBounce) / 200f);
				if (Math.Abs(this.velocity.Y) > 2f || Math.Abs(this.scaler + this.groundHeight - this.mypos.Y) > 5f)
				{
					this.velocity = Vector3.Reflect(this.velocity, this.normal + vector) * new Vector3(0.94f, this.bounce, 0.94f);
				}
				else
				{
					this.velocity *= new Vector3(0.75f, 0f, 0.75f);
				}
				if (this.normal.Y < 0.95f)
				{
					this.velocity += new Vector3(this.normal.X, 0f, this.normal.Z);
				}
				this.turnRate = Math.Max(Math.Abs(this.turnRate), 0.25f);
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
							if (this.rr.Next(1, 100) < 50)
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
				if (this.scaler + this.groundHeight <= this.mypos.Y + 5f)
				{
					this.mypos.Y = this.scaler + this.groundHeight;
				}
			}
			Matrix.CreateScale(this.scale, out this.m1);
			Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z, out this.m4);
			Matrix.Multiply(ref this.m1, ref this.myRot, out this.m3);
			Matrix.Multiply(ref this.m3, ref this.m4, out this.transform);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x002E3898 File Offset: 0x002E1A98
		public void Update2(ref float[,] heights)
		{
			this.mypos.X = this.mypos.X + this.velocity.X;
			this.mypos.Y = this.mypos.Y + this.velocity.Y;
			this.mypos.Z = this.mypos.Z + this.velocity.Z;
			this.velocity.Y = this.velocity.Y + this.gravity;
			if (this.velocity.Y < -5f)
			{
				this.velocity.Y = -5f;
			}
			Matrix.Multiply(ref this.myRot, ref this.inertRot, out this.myRot);
			dupeItem.GetHeightFast(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
			this.scaler = (2f - Math.Abs(Vector3.Transform(Vector3.Up, this.myRot).Y) * (2f - this.ratio * 2f)) * this.scale;
			if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
			{
				this.firstHit++;
				if (this.velocity.Y < -1f)
				{
					this.secondhit++;
				}
				Vector3 vector = new Vector3((float)this.rr.Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr.Next(-this.randBounce, this.randBounce) / 200f);
				if (Math.Abs(this.velocity.Y) > 2f)
				{
					this.velocity = Vector3.Reflect(this.velocity, this.normal + vector) * new Vector3(0.94f, this.bounce, 0.94f);
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
					this.itBleeds = false;
					if (this.landupright)
					{
						if (this.sloper == 0f)
						{
							this.myRot.Decompose(out this.scale0, out this.rotation0, out this.position0);
							Quaternion.Normalize(this.rotation0);
							Matrix matrix = this.myRot;
							matrix.Up = this.normal;
							if (this.rr.Next(1, 100) < 50)
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

		// Token: 0x06000B40 RID: 2880 RVA: 0x002E3D78 File Offset: 0x002E1F78
		public void Update3(ref float[,] heights)
		{
			this.mypos.X = this.mypos.X + this.velocity.X;
			this.mypos.Y = this.mypos.Y + this.velocity.Y;
			this.mypos.Z = this.mypos.Z + this.velocity.Z;
			this.velocity.Y = this.velocity.Y + this.gravity;
			Matrix.Multiply(ref this.myRot, ref this.inertRot, out this.myRot);
			dupeItem.GetHeightSlow(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
			this.scaler = this.scale;
			if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
			{
				Vector3 vector = new Vector3((float)this.rr.Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr.Next(-this.randBounce, this.randBounce) / 200f);
				this.velocity = Vector3.Reflect(this.velocity, this.normal + vector) * new Vector3(0.98f, this.bounce, 0.98f);
				this.turnRate = Math.Max(Math.Abs(this.turnRate), 0.05f);
				this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-this.velocity, Vector3.Up)), this.turnRate);
				if (this.scaler + this.groundHeight <= this.mypos.Y + 5f)
				{
					this.mypos.Y = this.scaler + this.groundHeight;
				}
				this.mypos.Y = this.scaler + this.groundHeight;
			}
			Matrix.CreateScale(this.scale, out this.m1);
			Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z, out this.m4);
			Matrix.Multiply(ref this.m1, ref this.myRot, out this.m3);
			Matrix.Multiply(ref this.m3, ref this.m4, out this.transform);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x002E3FDC File Offset: 0x002E21DC
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
			this.scaler = (2f - Math.Abs(Vector3.Transform(Vector3.Up, this.myRot).Y) * (2f - this.ratio * 2f)) * this.scale;
			if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
			{
				this.firstHit++;
				if (this.velocity.Y < -1f)
				{
					this.secondhit++;
				}
				Vector3 vector = new Vector3((float)this.rr.Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr.Next(-this.randBounce, this.randBounce) / 200f);
				if (Math.Abs(this.velocity.Y) > 2f)
				{
					this.velocity = Vector3.Reflect(this.velocity, this.normal + vector) * new Vector3(0.94f, this.bounce, 0.94f);
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
							if (this.rr.Next(1, 100) < 50)
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

		// Token: 0x06000B42 RID: 2882 RVA: 0x002E4474 File Offset: 0x002E2674
		private static void GetHeightFast(ref float[,] heights, ref Vector3 pos, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(pos.X / dupeItem.unit, 0f, (float)(dupeItem.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(pos.Z / dupeItem.unit, 0f, (float)(dupeItem.bitmap - 2));
			float num3 = pos.X % dupeItem.unit / dupeItem.unit;
			float num4 = pos.Z % dupeItem.unit / dupeItem.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x002E4580 File Offset: 0x002E2780
		private static void GetHeightSlow(ref float[,] heights, ref Vector3 pos, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(pos.X / dupeItem.unit, 0f, (float)(dupeItem.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(pos.Z / dupeItem.unit, 0f, (float)(dupeItem.bitmap - 2));
			float num3 = pos.X % dupeItem.unit / dupeItem.unit;
			float num4 = pos.Z % dupeItem.unit / dupeItem.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x04002E10 RID: 11792
		private float oldY;

		// Token: 0x04002E11 RID: 11793
		private Matrix m1;

		// Token: 0x04002E12 RID: 11794
		private Matrix m2;

		// Token: 0x04002E13 RID: 11795
		private Matrix m3;

		// Token: 0x04002E14 RID: 11796
		private Matrix m4;

		// Token: 0x04002E15 RID: 11797
		private float f1;

		// Token: 0x04002E16 RID: 11798
		private float f2;

		// Token: 0x04002E17 RID: 11799
		private Vector3 v1;

		// Token: 0x04002E18 RID: 11800
		private Vector3 v2;

		// Token: 0x04002E19 RID: 11801
		public bool itBleeds;

		// Token: 0x04002E1A RID: 11802
		public bool kickable;

		// Token: 0x04002E1B RID: 11803
		public static int bleed = 260;

		// Token: 0x04002E1C RID: 11804
		public static int bitmap;

		// Token: 0x04002E1D RID: 11805
		public static float unit;

		// Token: 0x04002E1E RID: 11806
		private Vector3 scale0;

		// Token: 0x04002E1F RID: 11807
		private Quaternion rotation0;

		// Token: 0x04002E20 RID: 11808
		private Quaternion rotation1;

		// Token: 0x04002E21 RID: 11809
		private Vector3 position0;

		// Token: 0x04002E22 RID: 11810
		public int group = 1;

		// Token: 0x04002E23 RID: 11811
		private float gravity;

		// Token: 0x04002E24 RID: 11812
		public float scale;

		// Token: 0x04002E25 RID: 11813
		public float ratio;

		// Token: 0x04002E26 RID: 11814
		public float bounce;

		// Token: 0x04002E27 RID: 11815
		private float groundHeight;

		// Token: 0x04002E28 RID: 11816
		public Matrix myRot;

		// Token: 0x04002E29 RID: 11817
		public Matrix inertRot;

		// Token: 0x04002E2A RID: 11818
		public Matrix oldRot;

		// Token: 0x04002E2B RID: 11819
		public float sloper;

		// Token: 0x04002E2C RID: 11820
		public float turnRate = 0.3f;

		// Token: 0x04002E2D RID: 11821
		public int move;

		// Token: 0x04002E2E RID: 11822
		public Vector3 mypos;

		// Token: 0x04002E2F RID: 11823
		public Vector3 oldpos;

		// Token: 0x04002E30 RID: 11824
		public Vector3 normal;

		// Token: 0x04002E31 RID: 11825
		public float scaler;

		// Token: 0x04002E32 RID: 11826
		public Vector3 velocity;

		// Token: 0x04002E33 RID: 11827
		public Matrix transform;

		// Token: 0x04002E34 RID: 11828
		public float tint;

		// Token: 0x04002E35 RID: 11829
		public int firstHit;

		// Token: 0x04002E36 RID: 11830
		public int secondhit;

		// Token: 0x04002E37 RID: 11831
		public bool localShell;

		// Token: 0x04002E38 RID: 11832
		public bool landupright;

		// Token: 0x04002E39 RID: 11833
		public int randBounce = 10;

		// Token: 0x04002E3A RID: 11834
		public int alive;

		// Token: 0x04002E3B RID: 11835
		public int kicked;

		// Token: 0x04002E3C RID: 11836
		public int gash;

		// Token: 0x04002E3D RID: 11837
		public int freq;

		// Token: 0x04002E3E RID: 11838
		public Vector3 vortex = Vector3.Zero;

		// Token: 0x04002E3F RID: 11839
		public ushort partID;

		// Token: 0x04002E40 RID: 11840
		public int hits;

		// Token: 0x04002E41 RID: 11841
		public int mult;

		// Token: 0x04002E42 RID: 11842
		public int lastKicked;

		// Token: 0x04002E43 RID: 11843
		public Vector3 dump1;

		// Token: 0x04002E44 RID: 11844
		public Vector3 dump2;

		// Token: 0x04002E45 RID: 11845
		private Quaternion qq;

		// Token: 0x04002E46 RID: 11846
		private Vector3 v;

		// Token: 0x04002E47 RID: 11847
		private float result;

		// Token: 0x04002E48 RID: 11848
		public Randoms rr;
	}
}
