using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000B0 RID: 176
	public class debrisDupe
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x00147EE8 File Offset: 0x001460E8
		public debrisDupe(int i, float bounce, float scale, Matrix startpos, Vector3 veloc, float grav, bool itBleeds)
		{
			this.rr = new Random(i);
			this.lastKicked = 0;
			this.scale = scale;
			this.bounce = bounce;
			this.randBounce = 20;
			this.itBleeds = itBleeds;
			this.move = 0;
			this.gravity = grav;
			this.gash = this.rr.Next(50, 250);
			this.freq = this.rr.Next(4, 16);
			startpos.Decompose(out this.dump1, out this.qq, out this.dump2);
			this.myRot = Matrix.CreateFromQuaternion(this.qq);
			this.turnRate = (float)this.rr.Next(25, 45) / 1000f;
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
			this.oldpos = this.mypos;
			this.oldRot = this.myRot;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00148070 File Offset: 0x00146270
		public void Update(ref float[,] heights)
		{
			this.mypos.X = this.mypos.X + this.velocity.X;
			this.mypos.Y = this.mypos.Y + this.velocity.Y;
			this.mypos.Z = this.mypos.Z + this.velocity.Z;
			this.velocity.Y = this.velocity.Y + this.gravity;
			if (this.velocity.Y < -7f)
			{
				this.velocity.Y = -7f;
			}
			Matrix.Multiply(ref this.myRot, ref this.inertRot, out this.myRot);
			debrisDupe.GetHeightFast(ref heights, ref this.mypos, out this.groundHeight, out this.normal);
			this.scaler = this.scale;
			if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
			{
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
					this.sloper += 0.3f;
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

		// Token: 0x06000627 RID: 1575 RVA: 0x001483B4 File Offset: 0x001465B4
		private static void GetHeightFast(ref float[,] heights, ref Vector3 pos, out float height, out Vector3 normal)
		{
			normal = new Vector3(0f, 1f, 0f);
			int num = (int)MathHelper.Clamp(pos.X / debrisDupe.unit, 0f, (float)(debrisDupe.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(pos.Z / debrisDupe.unit, 0f, (float)(debrisDupe.bitmap - 2));
			height = heights[num, num2];
			if (height >= pos.Y)
			{
				float num3 = pos.X % debrisDupe.unit / debrisDupe.unit;
				float num4 = pos.Z % debrisDupe.unit / debrisDupe.unit;
				float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
				float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
				height = (1f - num4) * num5 + num4 * num6;
				normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
			}
		}

		// Token: 0x04001815 RID: 6165
		private Matrix m1;

		// Token: 0x04001816 RID: 6166
		private Matrix m2;

		// Token: 0x04001817 RID: 6167
		private Matrix m3;

		// Token: 0x04001818 RID: 6168
		private Matrix m4;

		// Token: 0x04001819 RID: 6169
		public bool itBleeds;

		// Token: 0x0400181A RID: 6170
		public bool kickable;

		// Token: 0x0400181B RID: 6171
		public static int bitmap;

		// Token: 0x0400181C RID: 6172
		public static float unit;

		// Token: 0x0400181D RID: 6173
		private float gravity;

		// Token: 0x0400181E RID: 6174
		public float scale;

		// Token: 0x0400181F RID: 6175
		public float ratio;

		// Token: 0x04001820 RID: 6176
		public float bounce;

		// Token: 0x04001821 RID: 6177
		private float groundHeight;

		// Token: 0x04001822 RID: 6178
		public Matrix myRot;

		// Token: 0x04001823 RID: 6179
		public Matrix inertRot;

		// Token: 0x04001824 RID: 6180
		public Matrix oldRot;

		// Token: 0x04001825 RID: 6181
		public float sloper;

		// Token: 0x04001826 RID: 6182
		public float turnRate = 0.3f;

		// Token: 0x04001827 RID: 6183
		public int move;

		// Token: 0x04001828 RID: 6184
		public Vector3 mypos;

		// Token: 0x04001829 RID: 6185
		public Vector3 oldpos;

		// Token: 0x0400182A RID: 6186
		public Vector3 normal;

		// Token: 0x0400182B RID: 6187
		public float scaler;

		// Token: 0x0400182C RID: 6188
		public Vector3 velocity;

		// Token: 0x0400182D RID: 6189
		public Matrix transform;

		// Token: 0x0400182E RID: 6190
		public int tint;

		// Token: 0x0400182F RID: 6191
		public int tint2;

		// Token: 0x04001830 RID: 6192
		public bool localShell;

		// Token: 0x04001831 RID: 6193
		public bool landupright;

		// Token: 0x04001832 RID: 6194
		public int randBounce = 10;

		// Token: 0x04001833 RID: 6195
		public int alive;

		// Token: 0x04001834 RID: 6196
		public int kicked;

		// Token: 0x04001835 RID: 6197
		public int bleed = 260;

		// Token: 0x04001836 RID: 6198
		public int gash;

		// Token: 0x04001837 RID: 6199
		public int freq;

		// Token: 0x04001838 RID: 6200
		public Vector3 vortex = Vector3.Zero;

		// Token: 0x04001839 RID: 6201
		public Vector3 emitVec;

		// Token: 0x0400183A RID: 6202
		public int partID;

		// Token: 0x0400183B RID: 6203
		public int mult;

		// Token: 0x0400183C RID: 6204
		public readonly Random rr;

		// Token: 0x0400183D RID: 6205
		public int lastKicked;

		// Token: 0x0400183E RID: 6206
		private Vector3 dump1;

		// Token: 0x0400183F RID: 6207
		private Vector3 dump2;

		// Token: 0x04001840 RID: 6208
		private Quaternion qq;
	}
}
