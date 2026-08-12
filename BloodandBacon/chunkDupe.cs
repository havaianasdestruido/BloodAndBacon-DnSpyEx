using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200010D RID: 269
	public class chunkDupe
	{
		// Token: 0x0600096A RID: 2410 RVA: 0x00252F40 File Offset: 0x00251140
		public chunkDupe(int i)
		{
			this.rr[0] = new Random(i);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00252F98 File Offset: 0x00251198
		public void init(float bounce, float scale, float ratio, Matrix startpos, Vector3 veloc, float grav, int rbounce, float ta, float tb)
		{
			this.ratio = ratio;
			this.scale = scale;
			this.bounce = bounce;
			this.randBounce = rbounce;
			this.alive = 0;
			this.move = 0;
			this.gravity = grav;
			startpos.Decompose(out this.dump1, out this.qq, out this.dump2);
			this.myRot = Matrix.CreateFromQuaternion(this.qq);
			this.turnRate = (float)this.rr[0].Next((int)ta, (int)tb) / 1000f;
			this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(new Vector3((float)this.rr[0].Next(-800, 800) / 100f, (float)this.rr[0].Next(-800, 800) / 100f, (float)this.rr[0].Next(-800, 800) / 100f)), this.turnRate);
			this.v = Vector3.Transform(Vector3.Up, this.myRot);
			this.result = Vector3.Dot(this.v, Vector3.Up);
			this.scaler = (2f - Math.Abs(this.result) * (2f - ratio * 2f)) * scale;
			this.velocity = Vector3.Zero;
			this.mypos = Vector3.Transform(Vector3.Zero, startpos);
			this.transform = startpos;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00253110 File Offset: 0x00251310
		public void createState(Matrix mm)
		{
			mm.Decompose(out this.scalePart, out this.qq, out this.mypos);
			this.myRot = Matrix.CreateFromQuaternion(this.qq);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00253140 File Offset: 0x00251340
		public void Update(ref float[,] heights)
		{
			if (this.move == 1)
			{
				this.mypos.X = this.mypos.X + this.velocity.X;
				this.mypos.Y = this.mypos.Y + this.velocity.Y;
				this.mypos.Z = this.mypos.Z + this.velocity.Z;
				this.velocity.Y = this.velocity.Y + this.gravity;
				this.myRot *= this.inertRot;
				chunkDupe.GetHeightFast(heights, new Vector2(this.mypos.X, this.mypos.Z), out this.groundHeight, out this.normal);
				this.scaler = this.scale * 4f;
				if (this.mypos.Y < this.scaler + this.groundHeight || this.sloper > 0f)
				{
					Vector3 vector = new Vector3((float)this.rr[0].Next(-this.randBounce, this.randBounce) / 200f, 0f, (float)this.rr[0].Next(-this.randBounce, this.randBounce) / 200f);
					if (Math.Abs(this.velocity.Y) > 2f)
					{
						this.velocity = Vector3.Reflect(this.velocity, this.normal + vector) * new Vector3(0.94f, this.bounce, 0.94f);
					}
					else
					{
						this.velocity *= new Vector3(0.85f, 0f, 0.85f);
					}
					if (this.normal.Y < 0.95f)
					{
						this.velocity += new Vector3(this.normal.X, 0f, this.normal.Z);
					}
					this.inertRot = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(-this.velocity, Vector3.Up)), this.turnRate);
					if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Z) < 0.2f || this.sloper > 0f) && this.sloper < 1f)
					{
						this.sloper += 0.1f;
					}
					if (this.sloper >= 1f)
					{
						this.velocity = Vector3.Zero;
						this.move = 2;
						this.sloper = 0f;
					}
					this.mypos.Y = this.scaler + this.groundHeight;
				}
			}
			if (this.move == 2)
			{
				this.scalePart *= 0.98f;
				if (this.scalePart.X < 0.01f)
				{
					this.move = 3;
				}
			}
			Matrix.CreateScale(this.scalePart.X, this.scalePart.Y, this.scalePart.Z, out this.m1);
			Matrix.CreateTranslation(this.mypos.X, this.mypos.Y, this.mypos.Z, out this.m4);
			Matrix.Multiply(ref this.m1, ref this.myRot, out this.m3);
			Matrix.Multiply(ref this.m3, ref this.m4, out this.transform);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x002534D0 File Offset: 0x002516D0
		private static void GetHeightFast(float[,] heights, Vector2 position, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(position.X / chunkDupe.unit, 0f, (float)(chunkDupe.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(position.Y / chunkDupe.unit, 0f, (float)(chunkDupe.bitmap - 2));
			height = heights[num, num2];
			normal = new Vector3(0f, 1f, 0f);
		}

		// Token: 0x04002756 RID: 10070
		private Matrix m1;

		// Token: 0x04002757 RID: 10071
		private Matrix m2;

		// Token: 0x04002758 RID: 10072
		private Matrix m3;

		// Token: 0x04002759 RID: 10073
		private Matrix m4;

		// Token: 0x0400275A RID: 10074
		public bool itBleeds;

		// Token: 0x0400275B RID: 10075
		public bool kickable;

		// Token: 0x0400275C RID: 10076
		public static int bitmap;

		// Token: 0x0400275D RID: 10077
		public static float unit;

		// Token: 0x0400275E RID: 10078
		private Vector3 scalePart;

		// Token: 0x0400275F RID: 10079
		private Quaternion rotation0;

		// Token: 0x04002760 RID: 10080
		private Quaternion rotation1;

		// Token: 0x04002761 RID: 10081
		private Vector3 position0;

		// Token: 0x04002762 RID: 10082
		public Matrix tempTrans;

		// Token: 0x04002763 RID: 10083
		private float gravity;

		// Token: 0x04002764 RID: 10084
		public float scale;

		// Token: 0x04002765 RID: 10085
		public float ratio;

		// Token: 0x04002766 RID: 10086
		public float bounce;

		// Token: 0x04002767 RID: 10087
		private float groundHeight;

		// Token: 0x04002768 RID: 10088
		public Matrix myRot;

		// Token: 0x04002769 RID: 10089
		public Matrix inertRot;

		// Token: 0x0400276A RID: 10090
		public Matrix oldRot;

		// Token: 0x0400276B RID: 10091
		public float sloper;

		// Token: 0x0400276C RID: 10092
		public float turnRate = 0.3f;

		// Token: 0x0400276D RID: 10093
		public int move;

		// Token: 0x0400276E RID: 10094
		public Vector3 mypos;

		// Token: 0x0400276F RID: 10095
		public Vector3 oldpos;

		// Token: 0x04002770 RID: 10096
		public Vector3 normal;

		// Token: 0x04002771 RID: 10097
		public float scaler;

		// Token: 0x04002772 RID: 10098
		public Vector3 velocity;

		// Token: 0x04002773 RID: 10099
		public Matrix transform;

		// Token: 0x04002774 RID: 10100
		public int tint;

		// Token: 0x04002775 RID: 10101
		public int tint2;

		// Token: 0x04002776 RID: 10102
		public int firstHit;

		// Token: 0x04002777 RID: 10103
		public int secondhit;

		// Token: 0x04002778 RID: 10104
		public bool localShell;

		// Token: 0x04002779 RID: 10105
		public bool landupright;

		// Token: 0x0400277A RID: 10106
		public int randBounce = 10;

		// Token: 0x0400277B RID: 10107
		public int alive;

		// Token: 0x0400277C RID: 10108
		public int kicked;

		// Token: 0x0400277D RID: 10109
		public int bleed = 260;

		// Token: 0x0400277E RID: 10110
		public int gash;

		// Token: 0x0400277F RID: 10111
		public int freq;

		// Token: 0x04002780 RID: 10112
		public Vector3 vortex = Vector3.Zero;

		// Token: 0x04002781 RID: 10113
		public Vector3 emitVec;

		// Token: 0x04002782 RID: 10114
		public int partID;

		// Token: 0x04002783 RID: 10115
		public int mult;

		// Token: 0x04002784 RID: 10116
		public readonly Random[] rr = new Random[2];

		// Token: 0x04002785 RID: 10117
		public int lastKicked;

		// Token: 0x04002786 RID: 10118
		private Vector3 dump1;

		// Token: 0x04002787 RID: 10119
		private Vector3 dump2;

		// Token: 0x04002788 RID: 10120
		private Quaternion qq;

		// Token: 0x04002789 RID: 10121
		private Vector3 v;

		// Token: 0x0400278A RID: 10122
		private float result;
	}
}
