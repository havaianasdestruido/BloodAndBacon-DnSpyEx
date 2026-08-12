using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000057 RID: 87
	internal class ropeDupe
	{
		// Token: 0x06000349 RID: 841 RVA: 0x000D2644 File Offset: 0x000D0844
		public ropeDupe(int num)
		{
			this.myRot = Matrix.CreateFromYawPitchRoll(1.57f, 0f, 0f);
			this.scaler = 2f;
			this.tranforms = new Matrix[num];
			this.div = num;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000D2698 File Offset: 0x000D0898
		public void initialize(Vector3 lander)
		{
			this.random = new Random();
			this.lengthDiv = 6.8f;
			this.dt = 0.3f;
			this.avg = 0.8f;
			this.start = 0;
			this.pX = new float[this.div];
			this.pY = new float[this.div];
			this.pZ = new float[this.div];
			this.oX = new float[this.div];
			this.oY = new float[this.div];
			this.oZ = new float[this.div];
			this.aX = new float[this.div];
			this.aY = new float[this.div];
			this.aZ = new float[this.div];
			this.fric = new float[this.div];
			this.grav = new float[this.div];
			float num = 0.015f;
			for (int i = 0; i < this.div; i++)
			{
				this.pX[i] = lander.X;
				this.pY[i] = lander.Y - 6.8f * (float)i;
				this.pZ[i] = lander.Z;
				this.oX[i] = lander.X;
				this.oY[i] = lander.Y - 6.8f * (float)i;
				this.oZ[i] = lander.Z;
				this.aX[i] = 0f;
				this.aY[i] = 0f;
				this.aZ[i] = 0f;
				this.fric[i] = 0.999f;
				this.grav[i] = -num;
				num *= 0.99f;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000D2864 File Offset: 0x000D0A64
		public void Update(Vector3 pos, Vector3 pos2, Matrix orient, Vector3 vel, ref float[,] heights)
		{
			this.player1 = pos;
			this.player2 = pos2;
			this.chainBump = false;
			this.calcGround(ref heights);
			for (int i = 0; i < 3; i++)
			{
				this.calcVelocity();
				this.calcSprings();
				this.calcSprings();
			}
			this.myRot = Matrix.CreateRotationY(-1.57f) * orient;
			this.mypos = new Vector3(this.pX[this.start], this.pY[this.start], this.pZ[this.start]);
			this.Transform = this.myRot * (Matrix.CreateScale(this.scaler) * Matrix.CreateTranslation(this.mypos));
			this.tranforms[this.start] = this.Transform;
			float num = 0f;
			for (int j = 1; j < this.div; j++)
			{
				num += 1.57f;
				this.myRot = Matrix.CreateRotationZ(num) * this.RotateToFace(new Vector3(this.pX[j], this.pY[j], this.pZ[j]), new Vector3(this.pX[j - 1], this.pY[j - 1], this.pZ[j - 1]));
				this.mypos = new Vector3(this.pX[j], this.pY[j], this.pZ[j]);
				this.Transform = Matrix.CreateScale(this.scaler) * this.myRot * Matrix.CreateTranslation(this.mypos);
				this.tranforms[j] = this.Transform;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000D2A20 File Offset: 0x000D0C20
		public void UpdateMore(ref List<Vector3> pos, Matrix orient, Vector3 vel, ref float[,] heights)
		{
			this.chainBump = false;
			this.calcGroundMax(ref pos, ref heights);
			for (int i = 0; i < 3; i++)
			{
				this.calcVelocity();
				this.calcSprings();
				this.calcSprings();
			}
			this.myRot = Matrix.CreateRotationY(-1.57f) * orient;
			this.mypos = new Vector3(this.pX[this.start], this.pY[this.start], this.pZ[this.start]);
			this.Transform = this.myRot * (Matrix.CreateScale(this.scaler) * Matrix.CreateTranslation(this.mypos));
			this.tranforms[this.start] = this.Transform;
			float num = 0f;
			for (int j = 1; j < this.div; j++)
			{
				num += 1.57f;
				this.myRot = Matrix.CreateRotationZ(num) * this.RotateToFace(new Vector3(this.pX[j], this.pY[j], this.pZ[j]), new Vector3(this.pX[j - 1], this.pY[j - 1], this.pZ[j - 1]));
				this.mypos = new Vector3(this.pX[j], this.pY[j], this.pZ[j]);
				this.Transform = Matrix.CreateScale(this.scaler) * this.myRot * Matrix.CreateTranslation(this.mypos);
				this.tranforms[j] = this.Transform;
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000D2BD0 File Offset: 0x000D0DD0
		public void calcVelocity()
		{
			for (int i = 1; i < this.div; i++)
			{
				float num = this.pX[i];
				float num2 = this.pY[i];
				float num3 = this.pZ[i];
				this.pX[i] += this.fric[i] * this.pX[i] - this.fric[i] * this.oX[i] + this.aX[i] * this.dt * this.dt;
				this.pY[i] += this.fric[i] * this.pY[i] - this.fric[i] * this.oY[i] + this.aY[i] * this.dt * this.dt + this.grav[i];
				this.pZ[i] += this.fric[i] * this.pZ[i] - this.fric[i] * this.oZ[i] + this.aZ[i] * this.dt * this.dt;
				this.oX[i] = num;
				this.oY[i] = num2;
				this.oZ[i] = num3;
				this.aX[i] *= 0.87f;
				this.aY[i] *= 0.87f;
				this.aZ[i] *= 0.87f;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000D2D80 File Offset: 0x000D0F80
		public void calcGround(ref float[,] heights)
		{
			for (int i = 1; i < this.div; i++)
			{
				Vector3 vector = new Vector3(this.pX[i], this.pY[i], this.pZ[i]);
				float num = 1f;
				Vector3 up = Vector3.Up;
				if (num > this.pY[i] - 1f)
				{
					this.pY[i] = num + 1f;
					this.fric[i] = 0.98f;
				}
				else
				{
					this.fric[i] = 0.999f;
				}
				if (!this.chainBump && Vector3.Distance(vector, this.player1) < 15f && i > this.start + 2)
				{
					Vector3 vector2 = Vector3.Normalize(vector - this.player1) * 4f;
					this.aX[i] = vector2.X;
					this.aY[i] = vector2.Y;
					this.aZ[i] = vector2.Z;
					this.chainBump = true;
				}
				if (!this.chainBump && Vector3.Distance(vector, this.player2) < 15f && i > this.start + 2)
				{
					Vector3 vector3 = Vector3.Normalize(vector - this.player2) * 4f;
					this.aX[i] = vector3.X;
					this.aY[i] = vector3.Y;
					this.aZ[i] = vector3.Z;
					this.chainBump = true;
				}
				if (vector.Z > 4803f)
				{
					this.pZ[i] = 4803f;
				}
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000D2F14 File Offset: 0x000D1114
		public void calcGroundMax(ref List<Vector3> pos, ref float[,] heights)
		{
			for (int i = 1; i < this.div; i++)
			{
				Vector3 vector = new Vector3(this.pX[i], this.pY[i], this.pZ[i]);
				float num = 1f;
				Vector3 up = Vector3.Up;
				if (num > this.pY[i] - 1f)
				{
					this.pY[i] = num + 1f;
					this.fric[i] = 0.98f;
				}
				else
				{
					this.fric[i] = 0.999f;
				}
				for (int j = 0; j < pos.Count; j++)
				{
					if (!this.chainBump && Vector3.Distance(vector, pos[j]) < 15f && i > this.start + 2)
					{
						Vector3 vector2 = Vector3.Normalize(vector - pos[j]) * 4f;
						this.aX[i] = vector2.X;
						this.aY[i] = vector2.Y;
						this.aZ[i] = vector2.Z;
						this.chainBump = true;
						break;
					}
				}
				if (vector.Z > 4803f)
				{
					this.pZ[i] = 4803f;
				}
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000D3050 File Offset: 0x000D1250
		public void calcSprings()
		{
			for (int i = this.div - 1; i >= this.start + 1; i--)
			{
				float num = this.pX[i] - this.pX[i - 1];
				float num2 = this.pY[i] - this.pY[i - 1];
				float num3 = this.pZ[i] - this.pZ[i - 1];
				float num4 = Vector3.Distance(new Vector3(this.pX[i], this.pY[i], this.pZ[i]), new Vector3(this.pX[i - 1], this.pY[i - 1], this.pZ[i - 1]));
				float num5 = num4 - this.lengthDiv;
				this.pX[i] -= num / num4 * this.avg * num5;
				this.pY[i] -= num2 / num4 * this.avg * num5;
				this.pZ[i] -= num3 / num4 * this.avg * num5;
				if (i > this.start + 1)
				{
					this.pX[i - 1] += num / num4 * this.avg * num5;
					this.pY[i - 1] += num2 / num4 * this.avg * num5;
					this.pZ[i - 1] += num3 / num4 * this.avg * num5;
				}
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000D3200 File Offset: 0x000D1400
		public Matrix RotateToFace(Vector3 O, Vector3 P)
		{
			Vector3 vector = Vector3.Cross(O, P);
			Vector3 vector2 = O - P;
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			Vector3.Normalize(ref vector3, out vector3);
			Vector3 vector4 = Vector3.Cross(vector3, vector);
			Vector3.Normalize(ref vector4, out vector4);
			Vector3 vector5 = Vector3.Cross(vector4, vector3);
			Matrix matrix = new Matrix(vector3.X, vector3.Y, vector3.Z, 0f, vector5.X, vector5.Y, vector5.Z, 0f, vector4.X, vector4.Y, vector4.Z, 0f, 0f, 0f, 0f, 1f);
			return matrix;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000D32B4 File Offset: 0x000D14B4
		private static void GetHeight(ref float[,] heights, Vector3 position, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(position.X / ropeDupe.unit, 0f, (float)(ropeDupe.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(position.Z / ropeDupe.unit, 0f, (float)(ropeDupe.bitmap - 2));
			float num3 = position.X % ropeDupe.unit / ropeDupe.unit;
			float num4 = position.Z % ropeDupe.unit / ropeDupe.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x04000DF2 RID: 3570
		public static int bitmap;

		// Token: 0x04000DF3 RID: 3571
		public static float unit;

		// Token: 0x04000DF4 RID: 3572
		public float rockScale;

		// Token: 0x04000DF5 RID: 3573
		public Matrix[] tranforms;

		// Token: 0x04000DF6 RID: 3574
		public float dt;

		// Token: 0x04000DF7 RID: 3575
		public float avg;

		// Token: 0x04000DF8 RID: 3576
		public int start;

		// Token: 0x04000DF9 RID: 3577
		public int div = 20;

		// Token: 0x04000DFA RID: 3578
		private float lengthDiv;

		// Token: 0x04000DFB RID: 3579
		private Vector3 player1;

		// Token: 0x04000DFC RID: 3580
		private Vector3 player2;

		// Token: 0x04000DFD RID: 3581
		public float[] pX;

		// Token: 0x04000DFE RID: 3582
		public float[] pY;

		// Token: 0x04000DFF RID: 3583
		public float[] pZ;

		// Token: 0x04000E00 RID: 3584
		public float[] oX;

		// Token: 0x04000E01 RID: 3585
		public float[] oY;

		// Token: 0x04000E02 RID: 3586
		public float[] oZ;

		// Token: 0x04000E03 RID: 3587
		public float[] aX;

		// Token: 0x04000E04 RID: 3588
		public float[] aY;

		// Token: 0x04000E05 RID: 3589
		public float[] aZ;

		// Token: 0x04000E06 RID: 3590
		private float[] grav;

		// Token: 0x04000E07 RID: 3591
		private float[] fric;

		// Token: 0x04000E08 RID: 3592
		private float scaler;

		// Token: 0x04000E09 RID: 3593
		public bool chainBump;

		// Token: 0x04000E0A RID: 3594
		private Matrix myRot;

		// Token: 0x04000E0B RID: 3595
		private Vector3 mypos;

		// Token: 0x04000E0C RID: 3596
		private Matrix Transform;

		// Token: 0x04000E0D RID: 3597
		private Random random;
	}
}
