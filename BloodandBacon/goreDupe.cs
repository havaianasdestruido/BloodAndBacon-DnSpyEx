using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000095 RID: 149
	public class goreDupe
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x00135B83 File Offset: 0x00133D83
		public goreDupe(int i)
		{
			this.rr[0] = new Random(i);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00135BBC File Offset: 0x00133DBC
		public void init(float bounce, float scale, Vector3 startpos, Vector3 veloc, float ta, float tb, bool oblong)
		{
			this.age = 0;
			this.scale = new Vector3(scale, scale, scale);
			this.move = 1;
			if (oblong)
			{
				this.scale = new Vector3(this.scale.X * ((float)this.rr[0].Next(70, 130) / 100f), this.scale.Y, this.scale.Z * ((float)this.rr[0].Next(70, 130) / 100f));
			}
			Matrix.CreateFromYawPitchRoll((float)this.rr[0].Next(-1900, 1800) / 100f, (float)this.rr[0].Next(-1900, 1800) / 100f, (float)this.rr[0].Next(-1900, 1800) / 100f, out this.myRot);
			this.turnRate = (float)this.rr[0].Next((int)ta, (int)tb) / 2000f;
			Matrix.CreateFromYawPitchRoll((float)this.rr[0].NextDouble() * this.turnRate, (float)this.rr[0].NextDouble() * this.turnRate, (float)this.rr[0].NextDouble() * this.turnRate, out this.inertRot);
			this.velocity = veloc;
			this.mypos = startpos;
			Matrix.CreateScale(scale, out this.m1);
			Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z, out this.m4);
			Matrix.Multiply(ref this.m1, ref this.myRot, out this.m3);
			Matrix.Multiply(ref this.m3, ref this.m4, out this.transform);
			if (bounce == 0f)
			{
				this.grav = (float)this.rr[0].Next(90, 380) / 100f;
			}
			else
			{
				this.grav = bounce;
			}
			this.spiral = (float)this.rr[0].Next(120, 160) / 100f;
			this.sucker = (float)this.rr[0].Next(400, 500) / 10f;
			this.speed = (float)this.rr[0].Next(10, 140) / 1000f;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00135E2C File Offset: 0x0013402C
		public void Update(ref float[,] heights)
		{
			this.mypos.X = this.mypos.X + this.velocity.X;
			this.mypos.Y = this.mypos.Y + this.velocity.Y;
			this.mypos.Z = this.mypos.Z + this.velocity.Z;
			Matrix.Multiply(ref this.myRot, ref this.inertRot, out this.m1);
			this.myRot = this.m1;
			this.velocity.X = -this.grav;
			Vector3 vector = new Vector3(0f, 70f, 1338f) - new Vector3(0f, this.mypos.Y, this.mypos.Z);
			Vector3.Normalize(ref vector, out this.v1);
			Matrix.CreateRotationX(-this.spiral, out this.m1);
			Vector3.Transform(ref this.v1, ref this.m1, out this.v2);
			this.velocity += this.v2 * this.speed + vector / this.sucker;
			this.velocity *= 0.98f;
			if (this.mypos.X < 1700f)
			{
				this.age = 600;
			}
			Matrix.CreateScale(this.scale.X, this.scale.Y, this.scale.Z, out this.m1);
			Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z, out this.m4);
			Matrix.Multiply(ref this.m1, ref this.myRot, out this.m3);
			Matrix.Multiply(ref this.m3, ref this.m4, out this.transform);
		}

		// Token: 0x040015B4 RID: 5556
		public Vector3 scale;

		// Token: 0x040015B5 RID: 5557
		private Matrix m1;

		// Token: 0x040015B6 RID: 5558
		private Matrix m2;

		// Token: 0x040015B7 RID: 5559
		private Matrix m3;

		// Token: 0x040015B8 RID: 5560
		private Matrix m4;

		// Token: 0x040015B9 RID: 5561
		private Vector3 v1;

		// Token: 0x040015BA RID: 5562
		private Vector3 v2;

		// Token: 0x040015BB RID: 5563
		public Matrix myRot;

		// Token: 0x040015BC RID: 5564
		public Matrix inertRot;

		// Token: 0x040015BD RID: 5565
		public float turnRate = 0.3f;

		// Token: 0x040015BE RID: 5566
		public int move;

		// Token: 0x040015BF RID: 5567
		public Vector3 mypos;

		// Token: 0x040015C0 RID: 5568
		public Vector3 oldpos;

		// Token: 0x040015C1 RID: 5569
		public Vector3 normal;

		// Token: 0x040015C2 RID: 5570
		public Vector3 velocity;

		// Token: 0x040015C3 RID: 5571
		public Matrix transform;

		// Token: 0x040015C4 RID: 5572
		public int tint;

		// Token: 0x040015C5 RID: 5573
		public int age;

		// Token: 0x040015C6 RID: 5574
		public readonly Random[] rr = new Random[2];

		// Token: 0x040015C7 RID: 5575
		public float grav = 0.04f;

		// Token: 0x040015C8 RID: 5576
		private float spiral;

		// Token: 0x040015C9 RID: 5577
		private float speed;

		// Token: 0x040015CA RID: 5578
		private float sucker;

		// Token: 0x040015CB RID: 5579
		private Vector3 dump1;

		// Token: 0x040015CC RID: 5580
		private Vector3 dump2;

		// Token: 0x040015CD RID: 5581
		private Quaternion qq;
	}
}
