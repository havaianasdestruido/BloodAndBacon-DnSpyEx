using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000A7 RID: 167
	public class caldDupe
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00142E00 File Offset: 0x00141000
		public caldDupe(int i)
		{
			this.rr = new Random(i);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00142E64 File Offset: 0x00141064
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

		// Token: 0x060005FB RID: 1531 RVA: 0x00142F08 File Offset: 0x00141108
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

		// Token: 0x060005FC RID: 1532 RVA: 0x001430B8 File Offset: 0x001412B8
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
				caldDupe.GetHeightSlow(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
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
					this.turnRate = Math.Max(Math.Abs(this.turnRate), 0.01f);
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
				caldDupe.GetHeightSlow(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
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

		// Token: 0x060005FD RID: 1533 RVA: 0x0014368C File Offset: 0x0014188C
		public void updateCualdron()
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

		// Token: 0x060005FE RID: 1534 RVA: 0x001437A8 File Offset: 0x001419A8
		private static void GetHeightSlow(ref float[,] heights, ref Vector3 pos, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(pos.X / caldDupe.unit, 0f, (float)(caldDupe.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(pos.Z / caldDupe.unit, 0f, (float)(caldDupe.bitmap - 2));
			float num3 = pos.X % caldDupe.unit / caldDupe.unit;
			float num4 = pos.Z % caldDupe.unit / caldDupe.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x04001723 RID: 5923
		private float oldY;

		// Token: 0x04001724 RID: 5924
		private Matrix m1;

		// Token: 0x04001725 RID: 5925
		private Matrix m2;

		// Token: 0x04001726 RID: 5926
		private Matrix m3;

		// Token: 0x04001727 RID: 5927
		private Matrix m4;

		// Token: 0x04001728 RID: 5928
		private float f1;

		// Token: 0x04001729 RID: 5929
		private float f2;

		// Token: 0x0400172A RID: 5930
		private Vector3 v1;

		// Token: 0x0400172B RID: 5931
		private Vector3 v2;

		// Token: 0x0400172C RID: 5932
		public int drop;

		// Token: 0x0400172D RID: 5933
		public int dropnow;

		// Token: 0x0400172E RID: 5934
		public bool itBleeds;

		// Token: 0x0400172F RID: 5935
		public bool kickable;

		// Token: 0x04001730 RID: 5936
		public static int bleed = 260;

		// Token: 0x04001731 RID: 5937
		public static int bitmap;

		// Token: 0x04001732 RID: 5938
		public static float unit;

		// Token: 0x04001733 RID: 5939
		private Vector3 scale0;

		// Token: 0x04001734 RID: 5940
		private Quaternion rotation0;

		// Token: 0x04001735 RID: 5941
		private Quaternion rotation1;

		// Token: 0x04001736 RID: 5942
		private Vector3 position0;

		// Token: 0x04001737 RID: 5943
		public int group = 1;

		// Token: 0x04001738 RID: 5944
		private float gravity;

		// Token: 0x04001739 RID: 5945
		public float ratio;

		// Token: 0x0400173A RID: 5946
		public float bounce;

		// Token: 0x0400173B RID: 5947
		public Vector3 scale;

		// Token: 0x0400173C RID: 5948
		private float groundHeight;

		// Token: 0x0400173D RID: 5949
		public Matrix myRot;

		// Token: 0x0400173E RID: 5950
		public Matrix inertRot;

		// Token: 0x0400173F RID: 5951
		public Matrix oldRot;

		// Token: 0x04001740 RID: 5952
		public float sloper;

		// Token: 0x04001741 RID: 5953
		public float turnRate = 0.3f;

		// Token: 0x04001742 RID: 5954
		public int move;

		// Token: 0x04001743 RID: 5955
		public Vector3 mypos;

		// Token: 0x04001744 RID: 5956
		public Vector3 oldpos;

		// Token: 0x04001745 RID: 5957
		public Vector3 normal;

		// Token: 0x04001746 RID: 5958
		public float scaler;

		// Token: 0x04001747 RID: 5959
		public Vector3 velocity;

		// Token: 0x04001748 RID: 5960
		public Matrix transform;

		// Token: 0x04001749 RID: 5961
		public float tint;

		// Token: 0x0400174A RID: 5962
		public int firstHit;

		// Token: 0x0400174B RID: 5963
		public int secondhit;

		// Token: 0x0400174C RID: 5964
		public bool localShell;

		// Token: 0x0400174D RID: 5965
		public bool landupright;

		// Token: 0x0400174E RID: 5966
		public int randBounce = 10;

		// Token: 0x0400174F RID: 5967
		public int alive;

		// Token: 0x04001750 RID: 5968
		public int kicked;

		// Token: 0x04001751 RID: 5969
		public int gash;

		// Token: 0x04001752 RID: 5970
		public int freq;

		// Token: 0x04001753 RID: 5971
		public Vector3 vortex = Vector3.Zero;

		// Token: 0x04001754 RID: 5972
		public ushort partID;

		// Token: 0x04001755 RID: 5973
		public int hits;

		// Token: 0x04001756 RID: 5974
		public int mult;

		// Token: 0x04001757 RID: 5975
		public int lastKicked;

		// Token: 0x04001758 RID: 5976
		public Vector3 dump1;

		// Token: 0x04001759 RID: 5977
		public Vector3 dump2;

		// Token: 0x0400175A RID: 5978
		private Quaternion qq;

		// Token: 0x0400175B RID: 5979
		private Vector3 v;

		// Token: 0x0400175C RID: 5980
		public Matrix rot;

		// Token: 0x0400175D RID: 5981
		private float result;

		// Token: 0x0400175E RID: 5982
		public int seed;

		// Token: 0x0400175F RID: 5983
		public Random rr;

		// Token: 0x04001760 RID: 5984
		private int count;

		// Token: 0x04001761 RID: 5985
		private int sizer = 1;

		// Token: 0x04001762 RID: 5986
		private int startin;

		// Token: 0x04001763 RID: 5987
		private float lerper = 1f;

		// Token: 0x04001764 RID: 5988
		private float grower = 0.5f;

		// Token: 0x04001765 RID: 5989
		public bool pop;
	}
}
