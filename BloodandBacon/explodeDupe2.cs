using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000097 RID: 151
	public class explodeDupe2
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x001367C8 File Offset: 0x001349C8
		public explodeDupe2(int i)
		{
			this.rr = new Random(i);
			this.ratio = 1f;
			this.scale = 1f;
			this.bounce = 0.65f;
			this.randBounce = 15;
			this.cChoice = this.rr.Next(1, 7);
			this.move = 1;
			this.gravity = -0.15f;
			this.gravity = -0.002f;
			this.turnRate = (float)this.rr.Next(10, 48) / 1000f;
			this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(new Vector3((float)this.rr.Next(-1800, 1800) / 100f, (float)this.rr.Next(-1800, 1800) / 100f, (float)this.rr.Next(-1800, 1800) / 100f)), this.turnRate);
			if (this.rr.Next(1, 100) < 30)
			{
				this.spinner = true;
			}
			this.velocity = Vector3.Zero;
			this.transform = Matrix.Identity;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00136930 File Offset: 0x00134B30
		public void createState(Matrix mm, Vector3 veloc, float cuttyScale, float reality)
		{
			mm.Decompose(out this.scalePart, out this.qq, out this.mypos);
			this.myRot = Matrix.CreateRotationX((float)this.rr.Next(-1800, 1800) / 100f) * Matrix.CreateRotationY((float)this.rr.Next(-1800, 1800) / 100f) * Matrix.CreateRotationZ((float)this.rr.Next(-1800, 1800) / 100f);
			this.velocity = veloc * (float)this.rr.Next(20, 70) / 100f;
			this.velocity *= reality;
			this.gravity *= reality;
			this.turnRate *= reality;
			this.myscale = (this.scalePart.X + this.scalePart.Y + this.scalePart.Z) / 3f;
			this.scale = this.myscale / cuttyScale;
			this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-veloc, Vector3.Up)), this.turnRate);
			this.scalePart /= cuttyScale;
			this.poofy = false;
			explodeDupe2.vortex = Vector2.Zero;
			this.age = 0;
			this.move = 1;
			this.sloper = 0f;
			this.oldpos = this.mypos;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00136ACC File Offset: 0x00134CCC
		public void Update(ref float[,] heights)
		{
			this.age++;
			if (this.move == 1)
			{
				this.mypos.X = this.mypos.X + this.velocity.X;
				this.mypos.Y = this.mypos.Y + this.velocity.Y;
				this.mypos.Z = this.mypos.Z + this.velocity.Z;
				this.velocity.Y = this.velocity.Y + this.gravity;
				this.myRot *= this.inertRot;
				this.groundHeight = -1000f;
				if (this.spinner)
				{
					this.offset = Vector3.Transform(new Vector3(0f, 0f, 0.1f) * this.scale, this.myRot) + this.mypos;
				}
				else
				{
					this.offset = this.mypos;
				}
				if (this.age > 100)
				{
					explodeDupe2.GetHeightFast(heights, new Vector2(this.mypos.X, this.mypos.Z), out this.groundHeight, out this.normal);
				}
				this.scaler = this.scale * 0.3f;
				if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
				{
					if (this.firstHit < 5)
					{
						this.firstHit = 1;
					}
					Vector3 vector = new Vector3((float)this.rr.Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr.Next(-this.randBounce, this.randBounce) / 200f);
					if (Math.Abs(this.velocity.Y) > 1f)
					{
						this.velocity = Vector3.Reflect(this.velocity, this.normal + vector) * new Vector3(0.99f, this.bounce, 0.99f);
					}
					else
					{
						this.velocity *= new Vector3(0.99f, 0f, 0.99f);
					}
					this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-this.velocity, Vector3.Up)), this.turnRate);
					if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Z) < 0.05f || this.sloper > 0f) && this.sloper < 1f)
					{
						this.sloper += 0.1f;
					}
					else
					{
						this.sloper += 0.01f;
					}
					if (this.sloper >= 1f)
					{
						this.velocity = Vector3.Zero;
						this.move = 0;
						this.sloper = 0f;
					}
					this.mypos.Y = this.scaler + this.groundHeight;
				}
			}
			Matrix.CreateScale(this.scalePart.X, this.scalePart.Y, this.scalePart.Z, out this.m1);
			Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z, out this.m4);
			Matrix.Multiply(ref this.m1, ref this.myRot, out this.m3);
			Matrix.Multiply(ref this.m3, ref this.m4, out this.transform);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00136E74 File Offset: 0x00135074
		private static void GetHeightFast(float[,] heights, Vector2 position, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(position.X / explodeDupe2.unit, 0f, (float)(explodeDupe2.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(position.Y / explodeDupe2.unit, 0f, (float)(explodeDupe2.bitmap - 2));
			float num3 = position.X % explodeDupe2.unit / explodeDupe2.unit;
			float num4 = position.Y % explodeDupe2.unit / explodeDupe2.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x040015D0 RID: 5584
		private Matrix m1;

		// Token: 0x040015D1 RID: 5585
		private Matrix m2;

		// Token: 0x040015D2 RID: 5586
		private Matrix m3;

		// Token: 0x040015D3 RID: 5587
		private Matrix m4;

		// Token: 0x040015D4 RID: 5588
		public bool vortexOn;

		// Token: 0x040015D5 RID: 5589
		public bool poofy;

		// Token: 0x040015D6 RID: 5590
		public static Vector2 vortex;

		// Token: 0x040015D7 RID: 5591
		public float myscale = 1f;

		// Token: 0x040015D8 RID: 5592
		private float cuttyScale = 1f;

		// Token: 0x040015D9 RID: 5593
		public static int bitmap;

		// Token: 0x040015DA RID: 5594
		public static float unit;

		// Token: 0x040015DB RID: 5595
		public Vector3 scalePart;

		// Token: 0x040015DC RID: 5596
		private Quaternion rotation0;

		// Token: 0x040015DD RID: 5597
		private Quaternion rotation1;

		// Token: 0x040015DE RID: 5598
		private Vector3 position0;

		// Token: 0x040015DF RID: 5599
		public Matrix tempTrans;

		// Token: 0x040015E0 RID: 5600
		private float gravity;

		// Token: 0x040015E1 RID: 5601
		public float scale;

		// Token: 0x040015E2 RID: 5602
		public float ratio;

		// Token: 0x040015E3 RID: 5603
		public float bounce;

		// Token: 0x040015E4 RID: 5604
		private float groundHeight;

		// Token: 0x040015E5 RID: 5605
		public Matrix myRot;

		// Token: 0x040015E6 RID: 5606
		public Matrix inertRot;

		// Token: 0x040015E7 RID: 5607
		public Matrix oldRot;

		// Token: 0x040015E8 RID: 5608
		public float sloper;

		// Token: 0x040015E9 RID: 5609
		public float turnRate = 0.3f;

		// Token: 0x040015EA RID: 5610
		public int move;

		// Token: 0x040015EB RID: 5611
		public Vector3 mypos;

		// Token: 0x040015EC RID: 5612
		public Vector3 oldpos;

		// Token: 0x040015ED RID: 5613
		public Vector3 offset;

		// Token: 0x040015EE RID: 5614
		public Vector3 normal;

		// Token: 0x040015EF RID: 5615
		public float scaler;

		// Token: 0x040015F0 RID: 5616
		public Vector3 velocity;

		// Token: 0x040015F1 RID: 5617
		public Matrix transform;

		// Token: 0x040015F2 RID: 5618
		public int tint;

		// Token: 0x040015F3 RID: 5619
		public int tint2;

		// Token: 0x040015F4 RID: 5620
		public int firstHit;

		// Token: 0x040015F5 RID: 5621
		public bool localShell;

		// Token: 0x040015F6 RID: 5622
		public bool landupright;

		// Token: 0x040015F7 RID: 5623
		public int randBounce = 10;

		// Token: 0x040015F8 RID: 5624
		public int age;

		// Token: 0x040015F9 RID: 5625
		public int bleed = 260;

		// Token: 0x040015FA RID: 5626
		public Vector3 emitVec;

		// Token: 0x040015FB RID: 5627
		public int partID;

		// Token: 0x040015FC RID: 5628
		public int mult;

		// Token: 0x040015FD RID: 5629
		public readonly Random rr;

		// Token: 0x040015FE RID: 5630
		public int lastKicked;

		// Token: 0x040015FF RID: 5631
		public int cChoice;

		// Token: 0x04001600 RID: 5632
		private Quaternion qq;

		// Token: 0x04001601 RID: 5633
		public bool spinner;
	}
}
