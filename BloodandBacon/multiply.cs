using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000059 RID: 89
	public class multiply
	{
		// Token: 0x0600035C RID: 860 RVA: 0x000D506A File Offset: 0x000D326A
		public Matrix rotX(float x, ref Matrix m)
		{
			Matrix.CreateRotationX(x, out this.m1);
			Matrix.Multiply(ref this.m1, ref m, out this.result);
			return this.result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000D5090 File Offset: 0x000D3290
		public Matrix rotY(float y, ref Matrix m)
		{
			Matrix.CreateRotationY(y, out this.m1);
			Matrix.Multiply(ref this.m1, ref m, out this.result);
			return this.result;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000D50B6 File Offset: 0x000D32B6
		public Matrix rotZ(float z, ref Matrix m)
		{
			Matrix.CreateRotationY(z, out this.m1);
			Matrix.Multiply(ref this.m1, ref m, out this.result);
			return this.result;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000D50DC File Offset: 0x000D32DC
		public Matrix rotXY(float x, float y, ref Matrix m)
		{
			Matrix.CreateRotationX(x, out this.m1);
			Matrix.CreateRotationY(y, out this.m2);
			Matrix.Multiply(ref this.m1, ref this.m2, out this.m3);
			Matrix.Multiply(ref this.m3, ref m, out this.result);
			return this.result;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000D5130 File Offset: 0x000D3330
		public Matrix rotXZ(float x, float z, ref Matrix m)
		{
			Matrix.CreateRotationX(x, out this.m1);
			Matrix.CreateRotationZ(z, out this.m2);
			Matrix.Multiply(ref this.m1, ref this.m2, out this.m3);
			Matrix.Multiply(ref this.m3, ref m, out this.result);
			return this.result;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000D5184 File Offset: 0x000D3384
		public Matrix rotYX(float y, float x, ref Matrix m)
		{
			Matrix.CreateRotationY(y, out this.m1);
			Matrix.CreateRotationX(x, out this.m2);
			Matrix.Multiply(ref this.m1, ref this.m2, out this.m3);
			Matrix.Multiply(ref this.m3, ref m, out this.result);
			return this.result;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000D51D8 File Offset: 0x000D33D8
		public Matrix rotYZ(float y, float z, ref Matrix m)
		{
			Matrix.CreateRotationY(y, out this.m1);
			Matrix.CreateRotationZ(z, out this.m2);
			Matrix.Multiply(ref this.m1, ref this.m2, out this.m3);
			Matrix.Multiply(ref this.m3, ref m, out this.result);
			return this.result;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000D522C File Offset: 0x000D342C
		public Matrix rotZX(float z, float x, ref Matrix m)
		{
			Matrix.CreateRotationZ(z, out this.m1);
			Matrix.CreateRotationX(x, out this.m2);
			Matrix.Multiply(ref this.m1, ref this.m2, out this.m3);
			Matrix.Multiply(ref this.m3, ref m, out this.result);
			return this.result;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000D5280 File Offset: 0x000D3480
		public Matrix rotZY(float z, float y, ref Matrix m)
		{
			Matrix.CreateRotationZ(z, out this.m1);
			Matrix.CreateRotationY(y, out this.m2);
			Matrix.Multiply(ref this.m1, ref this.m2, out this.m3);
			Matrix.Multiply(ref this.m3, ref m, out this.result);
			return this.result;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000D52D4 File Offset: 0x000D34D4
		public Matrix rotXYZ(float x, float y, float z, ref Matrix m)
		{
			Matrix.CreateRotationX(x, out this.m1);
			Matrix.CreateRotationY(y, out this.m2);
			Matrix.CreateRotationZ(z, out this.m3);
			Matrix.Multiply(ref this.m1, ref this.m2, out this.r1);
			Matrix.Multiply(ref this.r1, ref this.m3, out this.r2);
			Matrix.Multiply(ref this.r2, ref m, out this.result);
			return this.result;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000D534C File Offset: 0x000D354C
		public Matrix rotZXY(float z, float x, float y, ref Matrix m)
		{
			Matrix.CreateRotationZ(z, out this.m1);
			Matrix.CreateRotationX(x, out this.m2);
			Matrix.CreateRotationY(y, out this.m3);
			Matrix.Multiply(ref this.m1, ref this.m2, out this.r1);
			Matrix.Multiply(ref this.r1, ref this.m3, out this.r2);
			Matrix.Multiply(ref this.r2, ref m, out this.result);
			return this.result;
		}

		// Token: 0x04000E67 RID: 3687
		private Matrix m1;

		// Token: 0x04000E68 RID: 3688
		private Matrix m2;

		// Token: 0x04000E69 RID: 3689
		private Matrix m3;

		// Token: 0x04000E6A RID: 3690
		private Matrix m4;

		// Token: 0x04000E6B RID: 3691
		private Matrix r1;

		// Token: 0x04000E6C RID: 3692
		private Matrix r2;

		// Token: 0x04000E6D RID: 3693
		private Matrix r3;

		// Token: 0x04000E6E RID: 3694
		private Matrix result;
	}
}
