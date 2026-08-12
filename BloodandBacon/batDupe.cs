using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000B1 RID: 177
	public class batDupe
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x001484F4 File Offset: 0x001466F4
		public batDupe(Vector3 startpos, int ground, float amt, int flag, int seed, float st, int tint)
		{
			this.clip1 = 0;
			this.move = flag;
			this.ground = ground;
			this.pathStart = st;
			this.amt = amt;
			this.rr = new Random(seed);
			this.speed = this.rr.Next(2, 4);
			this.temp1 = this.rr.Next(1, 100);
			this.tint = tint;
			this.batStart = this.pathStart + (float)this.rr.Next(0, 100) / 5000f;
			if (this.batStart > 1f)
			{
				this.batStart -= 1f;
			}
			this.scale = (float)this.rr.Next(40, 130) / 200f;
			this.dist = (float)this.rr.Next(-800, 800) / 100f;
			this.ph = (float)this.rr.Next(4, 12) / 100f;
			this.startpos = startpos;
			this.offset = startpos;
			this.myRot = Matrix.Identity;
			this.transform = this.myRot * Matrix.CreateTranslation(this.mypos);
			this.freq = (float)this.rr.Next(200, 700) / 100f;
			this.amp = (float)this.rr.Next(300, 800) / 250f;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00148690 File Offset: 0x00146890
		public void Update(ref Vector3[] batpath)
		{
			this.temp1 += this.speed;
			this.frame1 = this.temp1 % batDupe.sics[this.clip1 * 2] + batDupe.sics[this.clip1 * 2 + 1];
			if (this.move == 0)
			{
				float num = (float)(batpath.Length - 1) * this.batStart;
				int num2 = (int)num;
				float num3 = Vector3.Distance(batpath[num2], batpath[num2 + 1]) / 10f;
				this.batStart += this.amt / num3;
				if (this.batStart < 1f)
				{
					this.dist += this.ph;
					this.mypos.X = MathHelper.Lerp(batpath[num2].X, batpath[num2 + 1].X, num - (float)num2);
					this.mypos.Z = MathHelper.Lerp(batpath[num2].Z, batpath[num2 + 1].Z, num - (float)num2);
					this.mypos.X = this.mypos.X + -(float)Math.Cos((double)this.dist) * 10f;
					this.mypos.Z = this.mypos.Z + -(float)Math.Cos((double)this.dist) * 10f;
					this.mypos.X = this.mypos.X + 3000f;
					this.mypos.Z = this.mypos.Z + 3000f;
					this.mypos.Y = MathHelper.Lerp(batpath[num2].Y, batpath[num2 + 1].Y, num - (float)num2) + (float)this.ground + (float)Math.Sin((double)(this.dist * (this.freq / 3f))) * (this.amp * 2f);
					this.mypos += this.startpos;
				}
				else
				{
					this.batStart = 0f;
					this.startpos = this.offset;
				}
				this.velocity = this.mypos - this.oldpos;
				float num4 = 0f;
				if (this.velocity.Z > 0f)
				{
					num4 = 3.14f;
				}
				float num5 = this.velocity.Y / Vector2.Distance(new Vector2(this.velocity.X, this.velocity.Z), new Vector2(0f, 0f));
				float num6 = this.velocity.X / this.velocity.Z;
				this.myRot = Matrix.CreateRotationX((float)Math.Atan((double)num5)) * Matrix.CreateRotationY((float)Math.Atan((double)num6) + num4);
				this.oldpos = this.mypos;
			}
			this.transform = Matrix.CreateScale(this.scale) * this.myRot * Matrix.CreateTranslation(this.mypos);
		}

		// Token: 0x04001841 RID: 6209
		public static List<int> sics;

		// Token: 0x04001842 RID: 6210
		public int frame1;

		// Token: 0x04001843 RID: 6211
		public int tint;

		// Token: 0x04001844 RID: 6212
		private int temp1;

		// Token: 0x04001845 RID: 6213
		private int clip1;

		// Token: 0x04001846 RID: 6214
		private float gravity;

		// Token: 0x04001847 RID: 6215
		private float rotz;

		// Token: 0x04001848 RID: 6216
		private Random rr;

		// Token: 0x04001849 RID: 6217
		private float scale = 1f;

		// Token: 0x0400184A RID: 6218
		private Vector3 myaxis;

		// Token: 0x0400184B RID: 6219
		public float deathTimer;

		// Token: 0x0400184C RID: 6220
		private float dist;

		// Token: 0x0400184D RID: 6221
		private float ph;

		// Token: 0x0400184E RID: 6222
		public int request;

		// Token: 0x0400184F RID: 6223
		public int move;

		// Token: 0x04001850 RID: 6224
		public Matrix myRot;

		// Token: 0x04001851 RID: 6225
		public Vector3 mypos;

		// Token: 0x04001852 RID: 6226
		public Vector3 startpos;

		// Token: 0x04001853 RID: 6227
		public Vector3 offset;

		// Token: 0x04001854 RID: 6228
		public Vector3 oldpos;

		// Token: 0x04001855 RID: 6229
		public int ground;

		// Token: 0x04001856 RID: 6230
		public Vector3 velocity;

		// Token: 0x04001857 RID: 6231
		public Matrix transform;

		// Token: 0x04001858 RID: 6232
		private float batStart;

		// Token: 0x04001859 RID: 6233
		private float amp;

		// Token: 0x0400185A RID: 6234
		private float freq;

		// Token: 0x0400185B RID: 6235
		private float amt;

		// Token: 0x0400185C RID: 6236
		private float pathStart;

		// Token: 0x0400185D RID: 6237
		public int alreadyBit;

		// Token: 0x0400185E RID: 6238
		private int speed = 1;
	}
}
