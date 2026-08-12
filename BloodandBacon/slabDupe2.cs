using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000049 RID: 73
	internal class slabDupe2
	{
		// Token: 0x060002ED RID: 749 RVA: 0x000BB9B4 File Offset: 0x000B9BB4
		public slabDupe2(int val)
		{
			this.rr = new Random(val);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000BB9C8 File Offset: 0x000B9BC8
		public void init(Vector3 startpos, Vector3 normal, float scaleSize, int val)
		{
			this.rr = new Random(val);
			this.scaler = (float)this.rr.Next(150, 250) / 100f;
			this.scaler *= scaleSize;
			this.mypos = new Vector3(startpos.X, startpos.Y, startpos.Z);
			Vector3 vector = new Vector3((float)this.rr.Next(90, 110) / 100f, 1f, (float)this.rr.Next(90, 110) / 100f) * this.scaler;
			Matrix matrix = Matrix.CreateRotationY((float)this.rr.Next(-1000, 1000) / 100f);
			matrix.Up = normal;
			matrix.Right = Vector3.Cross(matrix.Forward, matrix.Up);
			matrix.Right = Vector3.Normalize(matrix.Right);
			matrix.Forward = Vector3.Cross(matrix.Up, matrix.Right);
			matrix.Forward = Vector3.Normalize(matrix.Forward);
			this.myRot = matrix;
			this.Transform = Matrix.CreateScale(vector) * Matrix.CreateTranslation(new Vector3(0f, this.scaler / 2.2f, 0f)) * this.myRot * Matrix.CreateTranslation(this.mypos);
			this.oldpos = this.mypos;
		}

		// Token: 0x04000CE7 RID: 3303
		public Matrix myRot;

		// Token: 0x04000CE8 RID: 3304
		public bool move;

		// Token: 0x04000CE9 RID: 3305
		public Vector3 mypos;

		// Token: 0x04000CEA RID: 3306
		private float groundHeight;

		// Token: 0x04000CEB RID: 3307
		private Vector3 normal;

		// Token: 0x04000CEC RID: 3308
		private Vector3 rot;

		// Token: 0x04000CED RID: 3309
		private Vector3 delta;

		// Token: 0x04000CEE RID: 3310
		public Vector3 grav;

		// Token: 0x04000CEF RID: 3311
		public Vector3 oldpos;

		// Token: 0x04000CF0 RID: 3312
		public float scaler;

		// Token: 0x04000CF1 RID: 3313
		private int stateFlag;

		// Token: 0x04000CF2 RID: 3314
		public int locationIndex;

		// Token: 0x04000CF3 RID: 3315
		public Matrix Transform;

		// Token: 0x04000CF4 RID: 3316
		public int hits;

		// Token: 0x04000CF5 RID: 3317
		public Random rr;
	}
}
