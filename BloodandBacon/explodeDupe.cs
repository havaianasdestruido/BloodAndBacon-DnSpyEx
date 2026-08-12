using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000D7 RID: 215
	public class explodeDupe
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x001C26EC File Offset: 0x001C08EC
		public explodeDupe(int i)
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
			if (this.rr.Next(1, 100) < 20)
			{
				this.spinner = true;
			}
			this.velocity = Vector3.Zero;
			this.transform = Matrix.Identity;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x001C2854 File Offset: 0x001C0A54
		public void createState(Matrix mm, Vector3 veloc, float cuttyScale, float reality)
		{
			mm.Decompose(out this.scalePart, out this.qq, out this.mypos);
			this.myRot = Matrix.CreateRotationX((float)this.rr.Next(-1800, 1800) / 100f) * Matrix.CreateRotationY((float)this.rr.Next(-1800, 1800) / 100f) * Matrix.CreateRotationZ((float)this.rr.Next(-1800, 1800) / 100f);
			if (this.mypos.Y < 50f)
			{
				this.velocity = veloc * (float)this.rr.Next(70, 99) / 100f;
			}
			else
			{
				this.velocity = veloc * (float)this.rr.Next(20, 80) / 100f;
			}
			this.velocity *= reality;
			this.gravity *= reality;
			this.turnRate *= reality;
			this.myscale = (this.scalePart.X + this.scalePart.Y + this.scalePart.Z) / 3.5f;
			this.scale = this.myscale / cuttyScale;
			this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-veloc, Vector3.Up)), this.turnRate);
			this.scalePart /= cuttyScale;
			this.poofy = false;
			if (this.rr.Next(1, 1000) < 70)
			{
				this.poofy = true;
			}
			explodeDupe.vortex = Vector2.Zero;
			this.age = 0;
			this.move = 1;
			this.sloper = 0f;
			this.oldpos = this.mypos;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x001C2A48 File Offset: 0x001C0C48
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
					this.offset = Vector3.Transform(new Vector3(0f, 0f, 0.3f) * this.scale, this.myRot) + this.mypos;
				}
				else
				{
					this.offset = this.mypos;
				}
				if (this.age > 100)
				{
					explodeDupe.GetHeightFast(heights, new Vector2(this.mypos.X, this.mypos.Z), out this.groundHeight, out this.normal);
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
					if (this.sloper >= 1f)
					{
						this.velocity = Vector3.Zero;
						this.move = 3;
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

		// Token: 0x06000786 RID: 1926 RVA: 0x001C2DDC File Offset: 0x001C0FDC
		private static void GetHeightFast(float[,] heights, Vector2 position, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(position.X / explodeDupe.unit, 0f, (float)(explodeDupe.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(position.Y / explodeDupe.unit, 0f, (float)(explodeDupe.bitmap - 2));
			float num3 = position.X % explodeDupe.unit / explodeDupe.unit;
			float num4 = position.Y % explodeDupe.unit / explodeDupe.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x04001EFB RID: 7931
		private Matrix m1;

		// Token: 0x04001EFC RID: 7932
		private Matrix m2;

		// Token: 0x04001EFD RID: 7933
		private Matrix m3;

		// Token: 0x04001EFE RID: 7934
		private Matrix m4;

		// Token: 0x04001EFF RID: 7935
		public bool vortexOn;

		// Token: 0x04001F00 RID: 7936
		public bool poofy;

		// Token: 0x04001F01 RID: 7937
		public static Vector2 vortex;

		// Token: 0x04001F02 RID: 7938
		public float myscale = 1f;

		// Token: 0x04001F03 RID: 7939
		private float cuttyScale = 1f;

		// Token: 0x04001F04 RID: 7940
		public static int bitmap;

		// Token: 0x04001F05 RID: 7941
		public static float unit;

		// Token: 0x04001F06 RID: 7942
		public Vector3 scalePart;

		// Token: 0x04001F07 RID: 7943
		private Quaternion rotation0;

		// Token: 0x04001F08 RID: 7944
		private Quaternion rotation1;

		// Token: 0x04001F09 RID: 7945
		private Vector3 position0;

		// Token: 0x04001F0A RID: 7946
		public Matrix tempTrans;

		// Token: 0x04001F0B RID: 7947
		private float gravity;

		// Token: 0x04001F0C RID: 7948
		public float scale;

		// Token: 0x04001F0D RID: 7949
		public float ratio;

		// Token: 0x04001F0E RID: 7950
		public float bounce;

		// Token: 0x04001F0F RID: 7951
		private float groundHeight;

		// Token: 0x04001F10 RID: 7952
		public Matrix myRot;

		// Token: 0x04001F11 RID: 7953
		public Matrix inertRot;

		// Token: 0x04001F12 RID: 7954
		public Matrix oldRot;

		// Token: 0x04001F13 RID: 7955
		public float sloper;

		// Token: 0x04001F14 RID: 7956
		public float turnRate = 0.3f;

		// Token: 0x04001F15 RID: 7957
		public int move;

		// Token: 0x04001F16 RID: 7958
		public Vector3 mypos;

		// Token: 0x04001F17 RID: 7959
		public Vector3 oldpos;

		// Token: 0x04001F18 RID: 7960
		public Vector3 offset;

		// Token: 0x04001F19 RID: 7961
		public Vector3 normal;

		// Token: 0x04001F1A RID: 7962
		public float scaler;

		// Token: 0x04001F1B RID: 7963
		public Vector3 velocity;

		// Token: 0x04001F1C RID: 7964
		public Matrix transform;

		// Token: 0x04001F1D RID: 7965
		public int tint;

		// Token: 0x04001F1E RID: 7966
		public int tint2;

		// Token: 0x04001F1F RID: 7967
		public int firstHit;

		// Token: 0x04001F20 RID: 7968
		public bool localShell;

		// Token: 0x04001F21 RID: 7969
		public bool landupright;

		// Token: 0x04001F22 RID: 7970
		public int randBounce = 10;

		// Token: 0x04001F23 RID: 7971
		public int age;

		// Token: 0x04001F24 RID: 7972
		public int bleed = 260;

		// Token: 0x04001F25 RID: 7973
		public Vector3 emitVec;

		// Token: 0x04001F26 RID: 7974
		public int partID;

		// Token: 0x04001F27 RID: 7975
		public int mult;

		// Token: 0x04001F28 RID: 7976
		public readonly Random rr;

		// Token: 0x04001F29 RID: 7977
		public int lastKicked;

		// Token: 0x04001F2A RID: 7978
		public int cChoice;

		// Token: 0x04001F2B RID: 7979
		private Quaternion qq;

		// Token: 0x04001F2C RID: 7980
		public bool spinner;
	}
}
