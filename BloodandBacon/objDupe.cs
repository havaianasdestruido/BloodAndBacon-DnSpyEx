using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200010A RID: 266
	internal class objDupe
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x00250B81 File Offset: 0x0024ED81
		public objDupe(int var)
		{
			this.rr = new Random(var);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00250BB8 File Offset: 0x0024EDB8
		public void initflower(Vector3 startpos, ScreenManager scc, int pair, int x, int z)
		{
			this.objx = x;
			this.objz = z;
			this.sc = scc;
			if (this.stateFlag == 0)
			{
				this.stateFlag = 15;
			}
			this.rr = new Random(pair);
			this.div = (float)this.rr.Next(300, 500) / 100f;
			this.move = false;
			this.rot = new Vector3((float)this.rr.Next(-1600, 1600) / 100f, 0f, 0f);
			this.scaler = (float)this.rr.Next(150, 250) / 100f;
			this.finalscale = (float)this.rr.Next(1100, 2100) / 100f;
			if (this.stateFlag == 10)
			{
				this.scaler = this.finalscale;
			}
			this.hits = 10000;
			this.mass = this.scaler + 300f;
			this.myRot = Matrix.CreateFromYawPitchRoll(this.rot.X, this.rot.Y, this.rot.Z);
			this.mypos = new Vector3(startpos.X, startpos.Y, startpos.Z);
			this.Transform = this.myRot * Matrix.CreateScale(this.scaler) * Matrix.CreateTranslation(this.mypos);
			this.oldpos = this.mypos;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00250D50 File Offset: 0x0024EF50
		public void init(bool large, Vector3 startpos, ScreenManager scc, int pair, int x, int z)
		{
			this.objx = x;
			this.objz = z;
			this.sc = scc;
			this.stateFlag = 0;
			this.rr = new Random(pair);
			this.move = false;
			this.rot = new Vector3((float)this.rr.Next(100, 1600) / 100f, (float)this.rr.Next(100, 1600) / 100f, (float)this.rr.Next(100, 1600) / 100f);
			this.scaler = (float)this.rr.Next(110, 260) / 100f;
			this.hits = this.rr.Next(3, 7);
			this.mass = this.scaler + 3f;
			if (large)
			{
				this.scaler = (float)this.rr.Next(110, 130) / 10f;
				this.hits = 50;
				this.mass = this.scaler;
			}
			this.myRot = Matrix.CreateFromYawPitchRoll(this.rot.X, this.rot.Y, this.rot.Z);
			this.mypos = new Vector3(startpos.X, startpos.Y + this.scaler * 25f, startpos.Z);
			this.Transform = this.myRot * Matrix.CreateScale(this.scaler) * Matrix.CreateTranslation(this.mypos);
			this.oldpos = this.mypos;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00250EF0 File Offset: 0x0024F0F0
		public void addObject(int c)
		{
			objDupe.objectData[this.objx, this.objz] = c;
			this.onMap = true;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00250F10 File Offset: 0x0024F110
		public void removeObject()
		{
			objDupe.objectData[this.objx, this.objz] = 0;
			this.onMap = false;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00250F30 File Offset: 0x0024F130
		public void Update(ref int[,] heightData, ref Vector3[,] normals, ref int[,] objectData)
		{
			if (this.stateFlag >= 10)
			{
				return;
			}
			if (this.stateFlag == 0)
			{
				float num = 1.3f - this.normal.Y;
				this.grav.Z = this.grav.Z + this.normal.Z * num * 0.3f;
				this.grav.X = this.grav.X + this.normal.X * num * 0.3f;
				this.grav.Y = this.grav.Y - this.normal.Y * num * 0.3f;
				this.mypos += this.grav;
				this.grav /= 1.005f;
				this.GetHeight(ref heightData, ref normals, this.mypos, out this.groundHeight, out this.normal);
				this.mypos.Y = this.groundHeight + this.scaler * 25f;
				float num2 = Vector3.Distance(this.mypos, this.oldpos);
				this.delta.X = this.oldpos.X - this.mypos.X;
				this.delta.Y = 0.001f;
				this.delta.Z = this.oldpos.Z - this.mypos.Z;
				if (this.delta.LengthSquared() > 0f)
				{
					this.myRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), num2 / (25f + this.scaler * 18f));
				}
				if (num2 < 1.2f)
				{
					this.grav.X = 0f;
					this.grav.Y = 0f;
					this.grav.Z = 0f;
					this.move = false;
				}
			}
			if (this.stateFlag == 5)
			{
				this.scaler += 0.4f;
				if (this.scaler >= this.finalscale)
				{
					this.stateFlag = 10;
					objDupe.flowersaved++;
					this.addObject(8);
					if (objDupe.flowersaved == 3)
					{
						this.sc.tada10.Play(this.sc.voiceVolume, 0f, 0f);
					}
					if (objDupe.flowersaved > 0)
					{
						this.sc.trophy.win(this.sc.trophy.spaceevent);
					}
					objectData[this.objx, this.objz] = 8;
				}
				this.myRot *= Matrix.CreateRotationY(0.04f);
			}
			this.Transform = this.myRot * Matrix.CreateScale(this.scaler) * Matrix.CreateTranslation(this.mypos);
			this.oldpos.X = this.mypos.X;
			this.oldpos.Y = this.mypos.Y;
			this.oldpos.Z = this.mypos.Z;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0025126C File Offset: 0x0024F46C
		private void GetHeightX(ref int[,] heightData, ref Vector3[,] normals, Vector3 position, out float height, out Vector3 normal)
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
			Vector3 vector2 = Vector3.Lerp(normals[num2, num3], normals[num6, num3], num4);
			Vector3 vector3 = Vector3.Lerp(normals[num2, num7], normals[num6, num7], num4);
			normal = Vector3.Lerp(vector2, vector3, num5);
			if (normal.LengthSquared() > 0f)
			{
				normal = Vector3.Normalize(normal);
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00251400 File Offset: 0x0024F600
		public void GetHeight(ref int[,] heightData, ref Vector3[,] normals, Vector3 position, out float height, out Vector3 normal)
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
			Vector3 vector2 = new Vector3((float)num2, (float)heightData[num2, num3], (float)num3);
			if (num4 + num5 >= 1f)
			{
				vector2 = new Vector3((float)num6, (float)heightData[num6, num7], (float)num7);
			}
			Vector3 vector3 = new Vector3((float)num2, (float)heightData[num2, num7], (float)num7);
			Vector3 vector4 = new Vector3((float)num6, (float)heightData[num6, num3], (float)num3);
			Vector2 vector5 = new Vector2(vector.X / (float)gridScale, vector.Z / (float)gridScale);
			float num8 = (vector3.Z - vector4.Z) * (vector2.X - vector4.X) + (vector4.X - vector3.X) * (vector2.Z - vector4.Z);
			float num9 = ((vector3.Z - vector4.Z) * (vector5.X - vector4.X) + (vector4.X - vector3.X) * (vector5.Y - vector4.Z)) / num8;
			float num10 = ((vector4.Z - vector2.Z) * (vector5.X - vector4.X) + (vector2.X - vector4.X) * (vector5.Y - vector4.Z)) / num8;
			float num11 = 1f - num9 - num10;
			height = num9 * vector2.Y + num10 * vector3.Y + num11 * vector4.Y;
			if (num4 + num5 > 1f)
			{
				vector2 = new Vector3((float)num6, (float)heightData[num6, num7], (float)num7);
			}
			vector2.Y /= (float)gridScale;
			vector3.Y /= (float)gridScale;
			vector4.Y /= (float)gridScale;
			Vector3 vector6 = vector2;
			Vector3 vector7 = vector3;
			Vector3 vector8 = vector4;
			Vector3 vector9 = Vector3.Cross(vector8 - vector7, vector6 - vector7);
			Vector3 vector10 = Vector3.Normalize(vector9);
			if (vector10.Y < 0f)
			{
				vector10 = -vector10;
			}
			normal = vector10;
		}

		// Token: 0x040026F5 RID: 9973
		public static int flowersaved = 0;

		// Token: 0x040026F6 RID: 9974
		public static int[,] objectData;

		// Token: 0x040026F7 RID: 9975
		public int objx;

		// Token: 0x040026F8 RID: 9976
		public int objz;

		// Token: 0x040026F9 RID: 9977
		public bool onMap;

		// Token: 0x040026FA RID: 9978
		public float div = 4f;

		// Token: 0x040026FB RID: 9979
		public Matrix myRot;

		// Token: 0x040026FC RID: 9980
		public bool move;

		// Token: 0x040026FD RID: 9981
		public Vector3 mypos;

		// Token: 0x040026FE RID: 9982
		public float gemtype;

		// Token: 0x040026FF RID: 9983
		private float groundHeight;

		// Token: 0x04002700 RID: 9984
		private Vector3 normal;

		// Token: 0x04002701 RID: 9985
		private Vector3 rot;

		// Token: 0x04002702 RID: 9986
		private Vector3 delta;

		// Token: 0x04002703 RID: 9987
		public Vector3 grav;

		// Token: 0x04002704 RID: 9988
		public Vector3 oldpos;

		// Token: 0x04002705 RID: 9989
		public float scaler;

		// Token: 0x04002706 RID: 9990
		public float finalscale = 1f;

		// Token: 0x04002707 RID: 9991
		public int stateFlag;

		// Token: 0x04002708 RID: 9992
		private ScreenManager sc;

		// Token: 0x04002709 RID: 9993
		public Matrix Transform;

		// Token: 0x0400270A RID: 9994
		public int hits;

		// Token: 0x0400270B RID: 9995
		public Random rr;

		// Token: 0x0400270C RID: 9996
		public float mass = 1f;
	}
}
