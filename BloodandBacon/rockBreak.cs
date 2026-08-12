using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200015D RID: 349
	internal class rockBreak
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x0037565C File Offset: 0x0037385C
		public rockBreak(ScreenManager scc, Vector3 startpos, float scale, Matrix rot, Vector3 vel, int rampflag, bool cansplit)
		{
			this.sc = scc;
			this.random = new Random(Math.Abs((int)startpos.X * 57));
			this.gravflag = 1;
			this.mytime = this.random.Next(90, 140);
			this.scaler = scale;
			this.myRot = rot;
			this.mypos = new Vector3(startpos.X, startpos.Y + this.scaler * 18f, startpos.Z);
			this.velocity = vel;
			this.Transform = this.myRot * Matrix.CreateScale(this.scaler) * Matrix.CreateTranslation(this.mypos);
			this.oldpos = this.mypos;
			this.onramp = rampflag;
			this.move = true;
			this.breakodds = 0;
			if (cansplit)
			{
				this.breakodds = this.random.Next(0, 4);
			}
			this.grav = -(float)this.random.Next(310, 500) / 1000f;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00375788 File Offset: 0x00373988
		public void Update(ref int[,] heightData)
		{
			this.mypos.X = this.mypos.X + this.velocity.X;
			this.mypos.Y = this.mypos.Y + this.velocity.Y;
			this.mypos.Z = this.mypos.Z + this.velocity.Z;
			if (this.onramp == 5)
			{
				this.mytime--;
				this.velocity /= 1.009f;
				this.GetHeight(ref heightData, this.mypos, out this.groundHeight);
				if (this.gravflag == 1)
				{
					this.velocity += new Vector3(0f, this.grav, 0f);
				}
				float num = Vector3.Distance(this.mypos, this.oldpos);
				this.delta.X = this.oldpos.X - this.mypos.X;
				this.delta.Y = 0f;
				this.delta.Z = this.oldpos.Z - this.mypos.Z;
				if (this.delta.LengthSquared() > 0f)
				{
					this.myRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), num / (15f + this.scaler * 14f));
				}
				if (this.mypos.Y <= this.groundHeight + this.scaler * 14f || this.velocity.Y == 0f)
				{
					this.velocity.Y = -this.velocity.Y * 0.6f;
					if (Math.Abs(this.velocity.Y) < 1f)
					{
						this.velocity.Y = 0f;
						this.gravflag = 0;
					}
					if (this.mytime <= 0 || Vector2.Distance(Vector2.Zero, new Vector2(this.velocity.X, this.velocity.Z)) < 0.1f)
					{
						this.move = true;
						this.onramp = 7;
						this.mytime = 260;
						this.velocity.Y = -Math.Abs(this.velocity.Y);
					}
					this.mypos.Y = this.groundHeight + this.scaler * 14f;
				}
				if (this.breakodds == 1)
				{
					this.move = false;
					this.onramp = 10;
					this.velocity.Y = -Math.Abs(this.velocity.Y);
				}
			}
			if (this.onramp == 7)
			{
				this.mytime--;
				this.GetHeight(ref heightData, this.mypos, out this.groundHeight);
				this.velocity = new Vector3(this.velocity.X = this.velocity.X / 1.008f, -1f, this.velocity.Z = this.velocity.Z / 1.008f);
				float num2 = Vector3.Distance(this.mypos, this.oldpos);
				this.delta.X = this.oldpos.X - this.mypos.X;
				this.delta.Y = 0f;
				this.delta.Z = this.oldpos.Z - this.mypos.Z;
				if (this.delta.LengthSquared() > 0f)
				{
					this.myRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), num2 / (15f + this.scaler * 14f));
				}
				if (this.mytime <= 0)
				{
					this.velocity = Vector3.Zero;
					this.move = false;
					this.onramp = 0;
				}
			}
			this.oldpos.X = this.mypos.X;
			this.oldpos.Y = this.mypos.Y;
			this.oldpos.Z = this.mypos.Z;
			this.Transform = this.myRot * (Matrix.CreateScale(this.scaler) * Matrix.CreateTranslation(this.mypos));
			this.Transform.M42 = (float)Math.Sign(this.Transform.M42) * ((float)Math.Abs((int)this.Transform.M42) + (float)this.mycolor * 0.001f);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00375C48 File Offset: 0x00373E48
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

		// Token: 0x04003417 RID: 13335
		public Matrix myRot;

		// Token: 0x04003418 RID: 13336
		public bool move;

		// Token: 0x04003419 RID: 13337
		public Vector3 mypos;

		// Token: 0x0400341A RID: 13338
		private float groundHeight;

		// Token: 0x0400341B RID: 13339
		private Vector3 delta;

		// Token: 0x0400341C RID: 13340
		public Vector3 velocity;

		// Token: 0x0400341D RID: 13341
		public Vector3 oldpos;

		// Token: 0x0400341E RID: 13342
		public float scaler;

		// Token: 0x0400341F RID: 13343
		public Matrix Transform;

		// Token: 0x04003420 RID: 13344
		public int hits;

		// Token: 0x04003421 RID: 13345
		public int onramp;

		// Token: 0x04003422 RID: 13346
		public int mycolor;

		// Token: 0x04003423 RID: 13347
		private Random random;

		// Token: 0x04003424 RID: 13348
		public int mytime;

		// Token: 0x04003425 RID: 13349
		private int breakodds;

		// Token: 0x04003426 RID: 13350
		private int gravflag;

		// Token: 0x04003427 RID: 13351
		private float grav = -0.35f;

		// Token: 0x04003428 RID: 13352
		private ScreenManager sc;
	}
}
