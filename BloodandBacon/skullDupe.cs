using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000027 RID: 39
	public class skullDupe
	{
		// Token: 0x06000178 RID: 376 RVA: 0x000319A8 File Offset: 0x0002FBA8
		public skullDupe(int i)
		{
			this.rr = new Random(i);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00031A20 File Offset: 0x0002FC20
		public void init(int seeder, Vector3 scal, Vector3 pos, Matrix rot, Vector3 veloc, int move, float floor, int tinter)
		{
			this.rr = new Random(seeder);
			this.seed = 0;
			this.gravity = -0.2f;
			this.kickable = true;
			this.alive = 0;
			this.firstHit = 0;
			this.move = move;
			if (move == 3)
			{
				this.canhurt = true;
			}
			this.startin = this.rr.Next(30, 460);
			this.grower = (float)this.rr.Next(50, 82) / 100f;
			this.lerper = 1f;
			this.turnRate = (float)this.rr.Next(320, 500) / 1000f * this.scale.X;
			this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-veloc, Vector3.Up)), this.turnRate);
			this.floor = floor;
			this.ratio = 1f;
			this.bounce = (float)this.rr.Next(25, 60) / 100f;
			this.mypos = pos;
			this.scale = scal;
			this.rot = rot;
			this.myRot = rot;
			this.tint = (float)tinter;
			this.velocity = veloc;
			this.oldY = this.mypos.Y;
			this.oldpos = this.mypos;
			this.oldRot = this.myRot;
			this.transform = Matrix.CreateScale(scal) * rot * Matrix.CreateTranslation(pos);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00031BB0 File Offset: 0x0002FDB0
		public void init2(int size, int group, float bounce, Vector3 scale, float ratio, Matrix startpos, Vector3 veloc, bool localShell, float grav, int rbounce, float ta, float tb, bool landupright, bool itBleeds, bool kickable)
		{
			this.sizer = size;
			this.lastKicked = 0;
			this.ratio = ratio;
			this.scale = scale;
			this.bounce = bounce;
			this.localShell = localShell;
			this.randBounce = rbounce;
			this.landupright = false;
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

		// Token: 0x0600017B RID: 379 RVA: 0x00031D54 File Offset: 0x0002FF54
		public void Update(ref float[,] heights)
		{
			if (this.move == 1)
			{
				this.oldY = this.mypos.Y;
				this.mypos.X = this.mypos.X + this.velocity.X;
				this.mypos.Y = this.mypos.Y + this.velocity.Y;
				this.mypos.Z = this.mypos.Z + this.velocity.Z;
				this.velocity.Y = this.velocity.Y + this.gravity;
				if (this.velocity.Y < -8f)
				{
					this.velocity.Y = -8f;
				}
				Matrix.Multiply(ref this.myRot, ref this.inertRot, out this.myRot);
				skullDupe.GetHeightSlow(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
				this.scaler = this.scale.X * (float)this.sizer;
				if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
				{
					this.firstHit++;
					Vector3 vector = new Vector3((float)this.rr.Next(-this.randBounce, this.randBounce) / 500f, 0f, (float)this.rr.Next(-this.randBounce, this.randBounce) / 500f);
					if (Math.Abs(this.velocity.Y) > 2f || Math.Abs(this.scaler + this.groundHeight - this.mypos.Y) > 5f)
					{
						this.velocity = Vector3.Reflect(this.velocity, this.normal + vector) * new Vector3(0.91f, this.bounce, 0.91f);
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
				skullDupe.GetHeightSlow(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
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

		// Token: 0x0600017C RID: 380 RVA: 0x00032308 File Offset: 0x00030508
		public void updateSkullHead(ref float[,] heights)
		{
			if (this.move == 1)
			{
				this.alive++;
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
					}
				}
				else
				{
					this.transform = Matrix.CreateScale(0.006f) * this.rot * Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z);
				}
			}
			if (this.move == 33)
			{
				this.alive++;
				this.oldY = this.mypos.Y;
				this.mypos.X = this.mypos.X + this.velocity.X;
				this.mypos.Y = this.mypos.Y + this.velocity.Y;
				this.mypos.Z = this.mypos.Z + this.velocity.Z;
				this.velocity.Y = this.velocity.Y + this.gravity;
				if (this.velocity.Y < -9f)
				{
					this.velocity.Y = -9f;
				}
				skullDupe.GetHeightSlow(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
				if (this.alive < 14)
				{
					this.groundHeight = -2000f;
				}
				float num2 = Vector2.Distance(new Vector2(this.mypos.X, this.mypos.Z), new Vector2(this.oldpos.X, this.oldpos.Z));
				this.delta.X = this.oldpos.X - this.mypos.X;
				this.delta.Y = 0f;
				this.delta.Z = this.oldpos.Z - this.mypos.Z;
				if (this.alive > 25 && this.delta.Length() > 0.1f)
				{
					this.myRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), num2 / (34f * this.scale.X));
				}
				this.scaler = this.scale.X * 31f;
				if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
				{
					this.firstHit++;
					Vector3 vector2 = new Vector3((float)this.rr.Next(-10, 10) / 300f, 0f, (float)this.rr.Next(-10, 10) / 300f);
					if (Math.Abs(this.velocity.Y) > 2f || Math.Abs(this.scaler + this.groundHeight - this.mypos.Y) > 5f)
					{
						this.velocity = Vector3.Reflect(this.velocity, this.normal + vector2) * new Vector3(0.81f, this.bounce, 0.81f);
					}
					else
					{
						this.velocity *= new Vector3(0.96f, 0f, 0.96f);
					}
					if (this.normal.Y < 0.95f)
					{
						this.velocity += new Vector3(this.normal.X, 0f, this.normal.Z) * 0.8f;
					}
					if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Z) < 0.2f || this.sloper > 0f) && this.sloper < 1f)
					{
						this.itBleeds = false;
						this.sloper += 0.2f;
					}
					if (this.sloper >= 1f)
					{
						this.velocity = Vector3.Zero;
						this.move = 0;
						this.canhurt = false;
						this.sloper = 0f;
					}
					if (this.floor > -320f)
					{
						if (this.mypos.Y < this.floor + this.scaler)
						{
							this.mypos.Y = this.floor + this.scaler;
						}
					}
					else if (this.scaler + this.groundHeight <= this.mypos.Y + 5f)
					{
						this.mypos.Y = this.scaler + this.groundHeight;
					}
				}
				this.oldpos.X = this.mypos.X;
				this.oldpos.Y = this.mypos.Y;
				this.oldpos.Z = this.mypos.Z;
				this.transform = Matrix.CreateScale(this.scale) * this.myRot * Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z);
			}
			if (this.move == 3)
			{
				this.alive++;
				this.oldY = this.mypos.Y;
				this.mypos.X = this.mypos.X + this.velocity.X;
				this.mypos.Y = this.mypos.Y + this.velocity.Y;
				this.mypos.Z = this.mypos.Z + this.velocity.Z;
				this.velocity.Y = this.velocity.Y + this.gravity;
				if (this.velocity.Y < -17f)
				{
					this.velocity.Y = -17f;
				}
				float num3 = Vector2.Distance(new Vector2(this.mypos.X, this.mypos.Z), new Vector2(this.oldpos.X, this.oldpos.Z));
				this.delta.X = this.oldpos.X - this.mypos.X;
				this.delta.Y = 0f;
				this.delta.Z = this.oldpos.Z - this.mypos.Z;
				if (this.alive > 25 && this.delta.Length() > 0.1f)
				{
					this.turnRate = Math.Min(num3 / (40f * this.scale.X), 0.35f);
					this.myRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), this.turnRate);
				}
				else if (this.alive <= 25)
				{
					this.turnRate = Math.Max(Math.Abs(this.turnRate), 0.35f);
					this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-this.velocity, Vector3.Up)), this.turnRate);
					Matrix.Multiply(ref this.myRot, ref this.inertRot, out this.myRot);
				}
				skullDupe.GetHeightSlow(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
				if (this.alive < 14)
				{
					this.groundHeight = -2000f;
				}
				this.scaler = this.scale.X * 31f;
				if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
				{
					this.firstHit++;
					Vector3 vector3 = new Vector3((float)this.rr.Next(-this.randBounce, this.randBounce) / 400f, 0f, (float)this.rr.Next(-this.randBounce, this.randBounce) / 400f);
					if (this.normal.Y < 0.9f)
					{
						this.velocity = Vector3.Reflect(this.velocity, this.normal + vector3) * new Vector3(0.75f, this.bounce, 0.75f);
					}
					else if (this.normal.Y >= 0.9f)
					{
						this.velocity = Vector3.Reflect(this.velocity, this.normal + vector3) * new Vector3(0.97f, this.bounce, 0.97f);
					}
					if (Math.Abs(this.velocity.Y) < 1f)
					{
						this.velocity *= new Vector3(0.98f, 0f, 0.98f);
					}
					if (this.mypos.Y - this.groundHeight > 200f && this.velocity.Length() > 10f)
					{
						this.velocity *= new Vector3(0.8f, 1f, 0.8f);
						this.sloper = 0f;
					}
					if (this.normal.Y < 0.9f && this.velocity.Length() <= 2f)
					{
						this.velocity += new Vector3(this.normal.X, 0f, this.normal.Z) * 1f;
						this.sloper = 0f;
					}
					if ((this.velocity.Length() <= 1f || this.sloper > 0f) && this.sloper < 1f && this.normal.Y > 0.95f)
					{
						this.sloper += 0.1f;
					}
					if (this.sloper >= 1f)
					{
						this.velocity = Vector3.Zero;
						this.move = 0;
						this.canhurt = false;
						this.sloper = 0f;
					}
					if (this.scaler + this.groundHeight <= this.mypos.Y + 5f)
					{
						this.mypos.Y = this.scaler + this.groundHeight;
					}
				}
				this.oldpos.X = this.mypos.X;
				this.oldpos.Y = this.mypos.Y;
				this.oldpos.Z = this.mypos.Z;
				Matrix.CreateScale(this.scale.X, out this.m1);
				Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z, out this.m4);
				Matrix.Multiply(ref this.m1, ref this.myRot, out this.m3);
				Matrix.Multiply(ref this.m3, ref this.m4, out this.transform);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00032EF0 File Offset: 0x000310F0
		private static void GetHeightSlow(ref float[,] heights, ref Vector3 pos, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(pos.X / skullDupe.unit, 0f, (float)(skullDupe.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(pos.Z / skullDupe.unit, 0f, (float)(skullDupe.bitmap - 2));
			float num3 = pos.X % skullDupe.unit / skullDupe.unit;
			float num4 = pos.Z % skullDupe.unit / skullDupe.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x040006FD RID: 1789
		private float oldY;

		// Token: 0x040006FE RID: 1790
		private Matrix m1;

		// Token: 0x040006FF RID: 1791
		private Matrix m2;

		// Token: 0x04000700 RID: 1792
		private Matrix m3;

		// Token: 0x04000701 RID: 1793
		private Matrix m4;

		// Token: 0x04000702 RID: 1794
		private float f1;

		// Token: 0x04000703 RID: 1795
		private float f2;

		// Token: 0x04000704 RID: 1796
		private Vector3 v1;

		// Token: 0x04000705 RID: 1797
		private Vector3 v2;

		// Token: 0x04000706 RID: 1798
		public int drop;

		// Token: 0x04000707 RID: 1799
		public int dropnow;

		// Token: 0x04000708 RID: 1800
		public bool itBleeds;

		// Token: 0x04000709 RID: 1801
		public bool kickable;

		// Token: 0x0400070A RID: 1802
		private Vector3 delta;

		// Token: 0x0400070B RID: 1803
		public static int bleed = 260;

		// Token: 0x0400070C RID: 1804
		public static int bitmap;

		// Token: 0x0400070D RID: 1805
		public static float unit;

		// Token: 0x0400070E RID: 1806
		private Vector3 scale0;

		// Token: 0x0400070F RID: 1807
		private Quaternion rotation0;

		// Token: 0x04000710 RID: 1808
		private Quaternion rotation1;

		// Token: 0x04000711 RID: 1809
		private Vector3 position0;

		// Token: 0x04000712 RID: 1810
		public int group = 1;

		// Token: 0x04000713 RID: 1811
		private float gravity;

		// Token: 0x04000714 RID: 1812
		public float ratio;

		// Token: 0x04000715 RID: 1813
		public float bounce;

		// Token: 0x04000716 RID: 1814
		public Vector3 scale;

		// Token: 0x04000717 RID: 1815
		private float groundHeight;

		// Token: 0x04000718 RID: 1816
		public Matrix myRot;

		// Token: 0x04000719 RID: 1817
		public Matrix inertRot;

		// Token: 0x0400071A RID: 1818
		public Matrix oldRot;

		// Token: 0x0400071B RID: 1819
		public float sloper;

		// Token: 0x0400071C RID: 1820
		public float turnRate = 0.3f;

		// Token: 0x0400071D RID: 1821
		public int move;

		// Token: 0x0400071E RID: 1822
		public bool canhurt;

		// Token: 0x0400071F RID: 1823
		public Vector3 mypos;

		// Token: 0x04000720 RID: 1824
		public Vector3 oldpos;

		// Token: 0x04000721 RID: 1825
		public Vector3 normal;

		// Token: 0x04000722 RID: 1826
		public float scaler;

		// Token: 0x04000723 RID: 1827
		public Vector3 velocity;

		// Token: 0x04000724 RID: 1828
		public Matrix transform;

		// Token: 0x04000725 RID: 1829
		public float tint;

		// Token: 0x04000726 RID: 1830
		public int firstHit;

		// Token: 0x04000727 RID: 1831
		public int secondhit;

		// Token: 0x04000728 RID: 1832
		public bool localShell;

		// Token: 0x04000729 RID: 1833
		public bool landupright;

		// Token: 0x0400072A RID: 1834
		public int randBounce = 10;

		// Token: 0x0400072B RID: 1835
		public int alive;

		// Token: 0x0400072C RID: 1836
		public int kicked;

		// Token: 0x0400072D RID: 1837
		public int gash;

		// Token: 0x0400072E RID: 1838
		public int freq;

		// Token: 0x0400072F RID: 1839
		public Vector3 vortex = Vector3.Zero;

		// Token: 0x04000730 RID: 1840
		public ushort partID;

		// Token: 0x04000731 RID: 1841
		public int hits;

		// Token: 0x04000732 RID: 1842
		public int mult;

		// Token: 0x04000733 RID: 1843
		public int lastKicked;

		// Token: 0x04000734 RID: 1844
		public Vector3 dump1;

		// Token: 0x04000735 RID: 1845
		public Vector3 dump2;

		// Token: 0x04000736 RID: 1846
		private Quaternion qq;

		// Token: 0x04000737 RID: 1847
		private Vector3 v;

		// Token: 0x04000738 RID: 1848
		public Matrix rot;

		// Token: 0x04000739 RID: 1849
		private float result;

		// Token: 0x0400073A RID: 1850
		public int seed;

		// Token: 0x0400073B RID: 1851
		public Random rr = new Random();

		// Token: 0x0400073C RID: 1852
		private int count;

		// Token: 0x0400073D RID: 1853
		private int sizer = 1;

		// Token: 0x0400073E RID: 1854
		private int startin;

		// Token: 0x0400073F RID: 1855
		private float lerper = 1f;

		// Token: 0x04000740 RID: 1856
		private float grower = 0.5f;

		// Token: 0x04000741 RID: 1857
		public float floor = -305f;

		// Token: 0x04000742 RID: 1858
		public bool pop;
	}
}
