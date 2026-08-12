using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000E0 RID: 224
	internal class slabDupe
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x001CAC73 File Offset: 0x001C8E73
		public slabDupe(int val)
		{
			this.rr = new Random(val);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x001CAC88 File Offset: 0x001C8E88
		public void init(Vector3 startpos, Vector3 normal, int val)
		{
			this.rr = new Random(val);
			this.scaler = (float)this.rr.Next(120, 240) / 100f;
			this.mypos = new Vector3(startpos.X, startpos.Y, startpos.Z);
			this.hits = this.rr.Next(5, 15);
			if (this.scaler < 4f)
			{
				this.hits = this.rr.Next(0, 4);
			}
			this.hits = this.rr.Next(1, 3);
			Matrix matrix = Matrix.CreateRotationY((float)this.rr.Next(0, 1000) / 100f);
			matrix.Up = normal;
			matrix.Right = Vector3.Cross(matrix.Forward, matrix.Up);
			matrix.Right = Vector3.Normalize(matrix.Right);
			matrix.Forward = Vector3.Cross(matrix.Up, matrix.Right);
			matrix.Forward = Vector3.Normalize(matrix.Forward);
			this.myRot = matrix;
			this.Transform = Matrix.CreateTranslation(new Vector3(0f, this.scaler / 2f, 0f)) * this.myRot * Matrix.CreateScale(this.scaler) * Matrix.CreateTranslation(this.mypos);
			this.oldpos = this.mypos;
		}

		// Token: 0x04001FBD RID: 8125
		public Matrix myRot;

		// Token: 0x04001FBE RID: 8126
		public bool move;

		// Token: 0x04001FBF RID: 8127
		public Vector3 mypos;

		// Token: 0x04001FC0 RID: 8128
		private Vector3 normal;

		// Token: 0x04001FC1 RID: 8129
		private Vector3 delta;

		// Token: 0x04001FC2 RID: 8130
		public Vector3 grav;

		// Token: 0x04001FC3 RID: 8131
		public Vector3 oldpos;

		// Token: 0x04001FC4 RID: 8132
		public float scaler;

		// Token: 0x04001FC5 RID: 8133
		private int stateFlag;

		// Token: 0x04001FC6 RID: 8134
		public int locationIndex;

		// Token: 0x04001FC7 RID: 8135
		public Matrix Transform;

		// Token: 0x04001FC8 RID: 8136
		public int hits;

		// Token: 0x04001FC9 RID: 8137
		public Random rr;
	}
}
