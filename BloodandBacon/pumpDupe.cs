using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000061 RID: 97
	public class pumpDupe
	{
		// Token: 0x06000387 RID: 903 RVA: 0x000D7C44 File Offset: 0x000D5E44
		public pumpDupe(int i)
		{
			this.rr = new Random(i);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000D7CA8 File Offset: 0x000D5EA8
		public void init(Vector3 scal, Vector3 pos, Matrix rot, int sed)
		{
			this.rr = new Random(sed);
			this.seed = sed;
			this.kickable = true;
			this.move = 1;
			this.startin = this.rr.Next(30, 460);
			this.grower = (float)this.rr.Next(50, 82) / 100f;
			this.lerper = 1f;
			this.mypos = pos;
			this.scale = scal;
			this.rot = rot;
			this.transform = Matrix.CreateScale(scal) * rot * Matrix.CreateTranslation(pos);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000D7D4C File Offset: 0x000D5F4C
		public void init2(int size, int group, float bounce, Vector3 scale, float ratio, Matrix startpos, Vector3 veloc, bool localShell, float grav, int rbounce, float ta, float tb, bool landupright, bool itBleeds, bool kickable, int boarSEED)
		{
			this.rr = new Random(boarSEED);
			this.sizer = size;
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

		// Token: 0x0600038A RID: 906 RVA: 0x000D7EFC File Offset: 0x000D60FC
		public void Update(ref float[,] heights)
		{
			if (this.move == 1)
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
				Matrix.Multiply(ref this.myRot, ref this.inertRot, out this.myRot);
				pumpDupe.GetHeightSlow(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
				this.scaler = this.scale.X * (float)this.sizer;
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
					this.turnRate = Math.Max(Math.Abs(this.turnRate), 0.05f);
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
							this.sloper += 0.1f;
						}
					}
					if (this.sloper >= 1f)
					{
						this.velocity = Vector3.Zero;
						this.move = 2;
						this.sloper = 0f;
					}
					if (this.scaler + this.groundHeight <= this.mypos.Y + 5f)
					{
						this.mypos.Y = this.scaler + this.groundHeight;
					}
				}
			}
			if (this.move == 2)
			{
				this.scaler = this.scale.X * (float)this.sizer;
				this.scale *= 0.99f;
				pumpDupe.GetHeightSlow(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
				if (this.scaler + this.groundHeight <= this.mypos.Y + 5f)
				{
					this.mypos.Y = this.scaler + this.groundHeight;
				}
				if (this.scale.Y < 0.05f)
				{
					this.move = -1;
				}
			}
			Matrix.CreateScale(this.scale.Y, out this.m1);
			Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z, out this.m4);
			Matrix.Multiply(ref this.m1, ref this.myRot, out this.m3);
			Matrix.Multiply(ref this.m3, ref this.m4, out this.transform);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000D84D0 File Offset: 0x000D66D0
		public void updatePumpkin()
		{
			if (this.move > 0)
			{
				this.startin--;
				if (this.startin <= 0)
				{
					this.lerper *= this.grower;
					Vector3 vector = (1f - this.lerper) * this.scale;
					float num = 34f * vector.Y * this.lerper;
					this.transform = Matrix.CreateScale(vector) * this.rot * Matrix.CreateTranslation(this.mypos.X, this.mypos.Y - num, this.mypos.Z);
					if (this.lerper < 0.001f)
					{
						this.move = 0;
						this.pop = true;
						return;
					}
				}
				else
				{
					this.transform = Matrix.CreateScale(0.006f) * this.rot * Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z);
				}
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000D85EC File Offset: 0x000D67EC
		private static void GetHeightSlow(ref float[,] heights, ref Vector3 pos, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(pos.X / pumpDupe.unit, 0f, (float)(pumpDupe.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(pos.Z / pumpDupe.unit, 0f, (float)(pumpDupe.bitmap - 2));
			float num3 = pos.X % pumpDupe.unit / pumpDupe.unit;
			float num4 = pos.Z % pumpDupe.unit / pumpDupe.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x04000EAD RID: 3757
		private float oldY;

		// Token: 0x04000EAE RID: 3758
		private Matrix m1;

		// Token: 0x04000EAF RID: 3759
		private Matrix m2;

		// Token: 0x04000EB0 RID: 3760
		private Matrix m3;

		// Token: 0x04000EB1 RID: 3761
		private Matrix m4;

		// Token: 0x04000EB2 RID: 3762
		private float f1;

		// Token: 0x04000EB3 RID: 3763
		private float f2;

		// Token: 0x04000EB4 RID: 3764
		private Vector3 v1;

		// Token: 0x04000EB5 RID: 3765
		private Vector3 v2;

		// Token: 0x04000EB6 RID: 3766
		public int drop;

		// Token: 0x04000EB7 RID: 3767
		public int dropnow;

		// Token: 0x04000EB8 RID: 3768
		public bool itBleeds;

		// Token: 0x04000EB9 RID: 3769
		public bool kickable;

		// Token: 0x04000EBA RID: 3770
		public static int bleed = 260;

		// Token: 0x04000EBB RID: 3771
		public static int bitmap;

		// Token: 0x04000EBC RID: 3772
		public static float unit;

		// Token: 0x04000EBD RID: 3773
		private Vector3 scale0;

		// Token: 0x04000EBE RID: 3774
		private Quaternion rotation0;

		// Token: 0x04000EBF RID: 3775
		private Quaternion rotation1;

		// Token: 0x04000EC0 RID: 3776
		private Vector3 position0;

		// Token: 0x04000EC1 RID: 3777
		public int group = 1;

		// Token: 0x04000EC2 RID: 3778
		private float gravity;

		// Token: 0x04000EC3 RID: 3779
		public float ratio;

		// Token: 0x04000EC4 RID: 3780
		public float bounce;

		// Token: 0x04000EC5 RID: 3781
		public Vector3 scale;

		// Token: 0x04000EC6 RID: 3782
		private float groundHeight;

		// Token: 0x04000EC7 RID: 3783
		public Matrix myRot;

		// Token: 0x04000EC8 RID: 3784
		public Matrix inertRot;

		// Token: 0x04000EC9 RID: 3785
		public Matrix oldRot;

		// Token: 0x04000ECA RID: 3786
		public float sloper;

		// Token: 0x04000ECB RID: 3787
		public float turnRate = 0.3f;

		// Token: 0x04000ECC RID: 3788
		public int move;

		// Token: 0x04000ECD RID: 3789
		public Vector3 mypos;

		// Token: 0x04000ECE RID: 3790
		public Vector3 oldpos;

		// Token: 0x04000ECF RID: 3791
		public Vector3 normal;

		// Token: 0x04000ED0 RID: 3792
		public float scaler;

		// Token: 0x04000ED1 RID: 3793
		public Vector3 velocity;

		// Token: 0x04000ED2 RID: 3794
		public Matrix transform;

		// Token: 0x04000ED3 RID: 3795
		public float tint;

		// Token: 0x04000ED4 RID: 3796
		public int firstHit;

		// Token: 0x04000ED5 RID: 3797
		public int secondhit;

		// Token: 0x04000ED6 RID: 3798
		public bool localShell;

		// Token: 0x04000ED7 RID: 3799
		public bool landupright;

		// Token: 0x04000ED8 RID: 3800
		public int randBounce = 10;

		// Token: 0x04000ED9 RID: 3801
		public int alive;

		// Token: 0x04000EDA RID: 3802
		public int kicked;

		// Token: 0x04000EDB RID: 3803
		public int gash;

		// Token: 0x04000EDC RID: 3804
		public int freq;

		// Token: 0x04000EDD RID: 3805
		public Vector3 vortex = Vector3.Zero;

		// Token: 0x04000EDE RID: 3806
		public ushort partID;

		// Token: 0x04000EDF RID: 3807
		public int hits;

		// Token: 0x04000EE0 RID: 3808
		public int mult;

		// Token: 0x04000EE1 RID: 3809
		public int lastKicked;

		// Token: 0x04000EE2 RID: 3810
		public Vector3 dump1;

		// Token: 0x04000EE3 RID: 3811
		public Vector3 dump2;

		// Token: 0x04000EE4 RID: 3812
		private Quaternion qq;

		// Token: 0x04000EE5 RID: 3813
		private Vector3 v;

		// Token: 0x04000EE6 RID: 3814
		public Matrix rot;

		// Token: 0x04000EE7 RID: 3815
		private float result;

		// Token: 0x04000EE8 RID: 3816
		public int seed;

		// Token: 0x04000EE9 RID: 3817
		public Random rr;

		// Token: 0x04000EEA RID: 3818
		private int count;

		// Token: 0x04000EEB RID: 3819
		private int sizer = 1;

		// Token: 0x04000EEC RID: 3820
		private int startin;

		// Token: 0x04000EED RID: 3821
		private float lerper = 1f;

		// Token: 0x04000EEE RID: 3822
		private float grower = 0.5f;

		// Token: 0x04000EEF RID: 3823
		public bool pop;
	}
}
