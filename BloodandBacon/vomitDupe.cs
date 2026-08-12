using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200010C RID: 268
	public class vomitDupe
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x00252787 File Offset: 0x00250987
		public vomitDupe(int i)
		{
			this.rr[0] = new Random(i);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x002527B4 File Offset: 0x002509B4
		public void init(float bounce, Matrix mm, Vector3 veloc, float grav, int rbounce, float ta, float tb)
		{
			this.groundHeight = -500f;
			this.bounce = bounce;
			this.randBounce = rbounce;
			this.gravity = grav;
			this.age = 0f;
			this.velocity = veloc;
			mm.Decompose(out this.scalePart, out this.qq, out this.mypos);
			this.myRot = Matrix.CreateFromQuaternion(this.qq);
			this.transform = mm;
			this.scale = (this.scalePart.X + this.scalePart.Y + this.scalePart.Z) / 2f;
			this.v = Vector3.Transform(Vector3.Up, this.myRot);
			this.result = Vector3.Dot(this.v, Vector3.Up);
			this.move = 1;
			this.splat = false;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00252890 File Offset: 0x00250A90
		public void Update(ref float[,] heights, Vector3 inherit)
		{
			this.age += 1f;
			if (this.move == 1)
			{
				this.mypos += inherit;
				this.mypos.X = this.mypos.X + this.velocity.X;
				this.mypos.Y = this.mypos.Y + this.velocity.Y;
				this.mypos.Z = this.mypos.Z + this.velocity.Z;
				this.velocity.Y = this.velocity.Y + this.gravity;
				float num = 0f;
				if (this.velocity.Z > 0f)
				{
					num = 3.14f;
				}
				float num2 = this.velocity.Y / Vector2.Distance(new Vector2(this.velocity.X, this.velocity.Z), new Vector2(0f, 0f));
				float num3 = this.velocity.X / this.velocity.Z;
				this.rotz += 0.8f;
				Matrix.CreateRotationZ(this.rotz, out this.m1);
				Matrix.CreateRotationX((float)Math.Atan((double)num2), out this.m2);
				Matrix.CreateRotationY((float)Math.Atan((double)num3) + num, out this.m4);
				Matrix.Multiply(ref this.m1, ref this.m2, out this.m3);
				Matrix.Multiply(ref this.m3, ref this.m4, out this.myRot);
				if (this.age > 8f)
				{
					vomitDupe.GetHeightFastest(ref heights, new Vector2(this.mypos.X, this.mypos.Z), out this.groundHeight, out this.normal);
				}
				this.scalePart.X = this.scalePart.X * 1.02f;
				this.scalePart.Y = this.scalePart.Y * 1.02f;
				if (this.mypos.Y < this.groundHeight)
				{
					Vector3 vector = new Vector3((float)this.rr[0].Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr[0].Next(-this.randBounce, this.randBounce) / 200f);
					this.velocity = Vector3.Reflect(this.velocity, this.normal + vector) * new Vector3(0.52f, this.bounce, 0.52f);
					this.move = 2;
					if (this.rr[0].Next(1, 100) < 70)
					{
						this.move = 3;
					}
					this.splat = true;
					this.scalePart.Z = this.scalePart.Z * 0.7f;
					this.scalePart.X = this.scalePart.X * 0.92f;
					this.scalePart.Y = this.scalePart.Y * 0.92f;
					num = 0f;
					if (this.velocity.Z > 0f)
					{
						num = 3.14f;
					}
					num2 = this.velocity.Y / Vector2.Distance(new Vector2(this.velocity.X, this.velocity.Z), new Vector2(0f, 0f));
					num3 = this.velocity.X / this.velocity.Z;
					this.myRot = Matrix.CreateRotationZ(0f) * Matrix.CreateRotationX((float)Math.Atan((double)num2)) * Matrix.CreateRotationY((float)Math.Atan((double)num3) + num);
					this.mypos.Y = this.scale + this.groundHeight;
				}
			}
			if (this.move == 2)
			{
				this.mypos.X = this.mypos.X + this.velocity.X;
				this.mypos.Y = this.mypos.Y + this.velocity.Y;
				this.mypos.Z = this.mypos.Z + this.velocity.Z;
				this.velocity.Y = this.velocity.Y + this.gravity / 2.5f;
				this.velocity *= new Vector3(0.93f, 0.93f, 0.93f);
				this.scalePart.Z = this.scalePart.Z * 0.94f;
				this.scalePart.X = this.scalePart.X * 0.95f;
				this.scalePart.Y = this.scalePart.Y * 0.95f;
				if (this.scalePart.X < 0.4f)
				{
					this.move = 3;
				}
			}
			Matrix.CreateScale(this.scalePart.X, this.scalePart.Y, this.scalePart.Z, out this.m1);
			Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z, out this.m4);
			Matrix.Multiply(ref this.m1, ref this.myRot, out this.m3);
			Matrix.Multiply(ref this.m3, ref this.m4, out this.transform);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00252DE4 File Offset: 0x00250FE4
		private static void GetHeightFast(ref float[,] heights, Vector2 position, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(position.X / vomitDupe.unit, 0f, (float)(vomitDupe.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(position.Y / vomitDupe.unit, 0f, (float)(vomitDupe.bitmap - 2));
			float num3 = position.X % vomitDupe.unit / vomitDupe.unit;
			float num4 = position.Y % vomitDupe.unit / vomitDupe.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = new Vector3(0f, 1f, 0f);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00252ECC File Offset: 0x002510CC
		private static void GetHeightFastest(ref float[,] heights, Vector2 position, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(position.X / vomitDupe.unit, 0f, (float)(vomitDupe.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(position.Y / vomitDupe.unit, 0f, (float)(vomitDupe.bitmap - 2));
			height = heights[num, num2];
			normal = new Vector3(0f, 1f, 0f);
		}

		// Token: 0x04002739 RID: 10041
		private Matrix m1;

		// Token: 0x0400273A RID: 10042
		private Matrix m2;

		// Token: 0x0400273B RID: 10043
		private Matrix m3;

		// Token: 0x0400273C RID: 10044
		private Matrix m4;

		// Token: 0x0400273D RID: 10045
		private float rotz;

		// Token: 0x0400273E RID: 10046
		public static int bitmap;

		// Token: 0x0400273F RID: 10047
		public static float unit;

		// Token: 0x04002740 RID: 10048
		public float age;

		// Token: 0x04002741 RID: 10049
		private Vector3 scalePart;

		// Token: 0x04002742 RID: 10050
		public Vector3 mycolor;

		// Token: 0x04002743 RID: 10051
		private float gravity;

		// Token: 0x04002744 RID: 10052
		public float scale;

		// Token: 0x04002745 RID: 10053
		public float ratio;

		// Token: 0x04002746 RID: 10054
		public float bounce;

		// Token: 0x04002747 RID: 10055
		private float groundHeight;

		// Token: 0x04002748 RID: 10056
		public Matrix myRot;

		// Token: 0x04002749 RID: 10057
		public float sloper;

		// Token: 0x0400274A RID: 10058
		public int move;

		// Token: 0x0400274B RID: 10059
		public bool splat;

		// Token: 0x0400274C RID: 10060
		public Vector3 mypos;

		// Token: 0x0400274D RID: 10061
		public Vector3 normal;

		// Token: 0x0400274E RID: 10062
		public Vector3 velocity;

		// Token: 0x0400274F RID: 10063
		public Matrix transform;

		// Token: 0x04002750 RID: 10064
		public int randBounce = 10;

		// Token: 0x04002751 RID: 10065
		public readonly Random[] rr = new Random[2];

		// Token: 0x04002752 RID: 10066
		public int lastKicked;

		// Token: 0x04002753 RID: 10067
		private Quaternion qq;

		// Token: 0x04002754 RID: 10068
		private Vector3 v;

		// Token: 0x04002755 RID: 10069
		private float result;
	}
}
