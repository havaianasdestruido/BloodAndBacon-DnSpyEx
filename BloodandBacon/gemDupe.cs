using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000030 RID: 48
	internal class gemDupe
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x0004A8E0 File Offset: 0x00048AE0
		public gemDupe(ScreenManager scc, Vector3 startpos, Vector3 veloc, bool mybool, int j)
		{
			this.sc = scc;
			this.move = mybool;
			this.random = new Random(j * 5);
			if (this.move)
			{
				this.onramp = 5;
			}
			this.myRot = Matrix.CreateFromYawPitchRoll((float)this.random.Next(0, 880000) / 900f, (float)this.random.Next(0, 880000) / 900f, (float)this.random.Next(0, 880000) / 900f);
			this.scaler = (float)this.random.Next(300, 700) / 100f;
			if (this.move)
			{
				this.velocity = veloc + new Vector3((float)this.random.Next(-10000, 10000) / 8000f, 0f, (float)this.random.Next(-10000, 10000) / 8000f);
			}
			else
			{
				this.velocity = Vector3.Zero;
			}
			this.normal = new Vector3((float)this.random.Next(-100, 100) / 700f, 1f, (float)this.random.Next(-100, 100) / 700f);
			this.mypos = new Vector3(startpos.X, startpos.Y, startpos.Z);
			this.transform = Matrix.CreateScale(this.scaler) * this.myRot * Matrix.CreateTranslation(this.mypos) * this.rampy;
			this.oldpos = this.mypos;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0004AAB0 File Offset: 0x00048CB0
		public void Update(ref int[,] heightData)
		{
			if (this.onramp != 3)
			{
				this.mypos.X = this.mypos.X + this.velocity.X;
				this.mypos.Y = this.mypos.Y + this.velocity.Y;
				this.mypos.Z = this.mypos.Z + this.velocity.Z;
				if (this.onramp == 5)
				{
					this.GetHeight(ref heightData, this.mypos, out this.groundHeight);
					this.velocity += new Vector3(0f, -0.35f, 0f);
					float num = Vector3.Distance(this.mypos, this.oldpos);
					this.delta.X = this.oldpos.X - this.mypos.X;
					this.delta.Y = 0f;
					this.delta.Z = this.oldpos.Z - this.mypos.Z;
					if (this.delta.LengthSquared() > 0f)
					{
						this.myRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), num / (2f + this.scaler * 2f));
					}
					if (this.mypos.Y < this.groundHeight + this.scaler)
					{
						this.velocity = Vector3.Reflect(this.velocity, this.normal) * 0.3f;
						if (this.groundHeight + this.scaler - this.mypos.Y < 0.6f)
						{
							this.velocity = Vector3.Zero;
							this.onramp = 0;
						}
						this.mypos.Y = this.groundHeight + this.scaler;
					}
				}
				if (this.onramp == 1)
				{
					float num2 = Vector3.Distance(this.mypos, this.oldpos);
					this.delta.X = this.oldpos.X - this.mypos.X;
					this.delta.Y = 0f;
					this.delta.Z = this.oldpos.Z - this.mypos.Z;
					if (this.delta.LengthSquared() > 0f)
					{
						this.myRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), num2 / (2f + this.scaler * 2f));
					}
					if (this.mypos.Y < this.groundHeight + this.scaler)
					{
						this.velocity = Vector3.Reflect(this.velocity, this.normal);
						this.mypos.Y = this.groundHeight + this.scaler;
					}
				}
				if (this.onramp == 2)
				{
					float num3 = Vector3.Distance(this.mypos, this.oldpos);
					this.delta.X = this.oldpos.X - this.mypos.X;
					this.delta.Y = 0f;
					this.delta.Z = this.oldpos.Z - this.mypos.Z;
					if (this.delta.LengthSquared() > 0f)
					{
						this.myRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), num3 / (1f + this.scaler * 2f));
					}
					if (this.mypos.Y < this.groundHeight + this.scaler)
					{
						this.velocity = Vector3.Reflect(this.velocity, this.normal) / 1.5f;
						if (this.groundHeight + this.scaler - this.mypos.Y < 0.2f)
						{
							this.velocity.Y = 0f;
							this.onramp = 3;
						}
						this.mypos.Y = this.groundHeight + this.scaler;
					}
				}
				this.oldpos.X = this.mypos.X;
				this.oldpos.Y = this.mypos.Y;
				this.oldpos.Z = this.mypos.Z;
			}
			this.transform = Matrix.CreateScale(this.scaler) * this.myRot * Matrix.CreateTranslation(this.mypos) * this.rampy;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0004AF74 File Offset: 0x00049174
		private void GetHeight(ref int[,] heightData, Vector3 position, out float height)
		{
			int gridScale = this.sc.gridScale;
			int bitmap = this.sc.bitmap;
			int num = (bitmap - 1) * gridScale;
			position.X = (position.X % (float)num + 1.5f * (float)num) % (float)num;
			position.Z = (position.Z % (float)num + 1.5f * (float)num) % (float)num;
			Vector3 vector = position;
			int num2 = (int)vector.X / gridScale;
			int num3 = (int)vector.Z / gridScale;
			float num4 = vector.X % (float)gridScale / (float)gridScale;
			float num5 = vector.Z % (float)gridScale / (float)gridScale;
			int num6 = num2 + 1;
			int num7 = num3 + 1;
			if (num6 > bitmap - 2)
			{
				num6 = 0;
			}
			if (num7 > bitmap - 2)
			{
				num7 = 0;
			}
			float num8 = MathHelper.Lerp((float)heightData[num2, num3], (float)heightData[num6, num3], num4);
			float num9 = MathHelper.Lerp((float)heightData[num2, num7], (float)heightData[num6, num7], num4);
			height = MathHelper.Lerp(num8, num9, num5);
		}

		// Token: 0x04000875 RID: 2165
		public Matrix myRot;

		// Token: 0x04000876 RID: 2166
		public bool move;

		// Token: 0x04000877 RID: 2167
		public Vector3 mypos;

		// Token: 0x04000878 RID: 2168
		private Random random;

		// Token: 0x04000879 RID: 2169
		public float groundHeight;

		// Token: 0x0400087A RID: 2170
		public Vector3 normal;

		// Token: 0x0400087B RID: 2171
		private Vector3 delta;

		// Token: 0x0400087C RID: 2172
		public Vector3 oldpos;

		// Token: 0x0400087D RID: 2173
		public float scaler;

		// Token: 0x0400087E RID: 2174
		public Vector3 velocity = Vector3.Zero;

		// Token: 0x0400087F RID: 2175
		public int onramp;

		// Token: 0x04000880 RID: 2176
		public Matrix rampy = Matrix.Identity;

		// Token: 0x04000881 RID: 2177
		public Matrix transform;

		// Token: 0x04000882 RID: 2178
		public int locationIndex;

		// Token: 0x04000883 RID: 2179
		public int transIndex;

		// Token: 0x04000884 RID: 2180
		private ScreenManager sc;
	}
}
