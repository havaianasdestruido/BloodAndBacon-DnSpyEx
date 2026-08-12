using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;

namespace Blood
{
	// Token: 0x0200010F RID: 271
	public class enemynpc
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x00253730 File Offset: 0x00251930
		public enemynpc(ScreenManager sc, ContentManager cc, int mazeid, ref float[,] tunnelheights)
		{
			this.sc = sc;
			this.Content = cc;
			this.mazeid = mazeid;
			this.enemyProxy = this.Content.Load<Model>("models//enemyProxy3");
			this.enemyTexture = this.Content.Load<Texture2D>("texture//enemyTexture2");
			this.enemyDeadTexture = this.Content.Load<Texture2D>("texture//enemyTexture4");
			this.splat2 = this.Content.Load<Texture2D>("texture\\splat2");
			this.seen = this.Content.Load<SoundEffect>("audio//seen3");
			this.seen4 = this.Content.Load<SoundEffect>("audio//seen4");
			this.enemydie = this.Content.Load<SoundEffect>("audio//enemyDie1");
			this.cube = this.Content.Load<Model>("models//cube");
			this.parts = new List<int>(5);
			this.s = new int[5];
			this.enemyInstancing();
			if (mazeid == 0)
			{
				this.enemyHome = this.enemyHome1;
				this.enemyGuard = this.enemyGuard1;
				this.enemyInvade = this.enemyInvade1;
			}
			if (mazeid == 1)
			{
				this.enemyHome = this.enemyHome1;
				this.enemyGuard = this.enemyGuard1;
				this.enemyInvade = this.enemyInvade1;
			}
			if (mazeid == 2)
			{
				this.enemyHome = this.enemyHome2;
				this.enemyGuard = this.enemyGuard2;
				this.enemyInvade = this.enemyInvade2;
			}
			if (mazeid == 3)
			{
				this.enemyHome = this.enemyHome3;
				this.enemyGuard = this.enemyGuard3;
				this.enemyInvade = this.enemyInvade3;
			}
			if (mazeid == 4)
			{
				this.enemyHome = this.enemyHome4;
				this.enemyGuard = this.enemyGuard4;
				this.enemyInvade = this.enemyInvade4;
			}
			this.makeSkulls();
			this.setSkullDifficulty();
			this.readGPSpaths(ref tunnelheights);
			for (int i = 0; i < this.skull.dupe.Count; i++)
			{
				Array.Resize<Vector3>(ref this.skull.dupe[i].skelPath3, 3);
				for (int j = 0; j < 3; j++)
				{
					this.skull.dupe[i].skelPath3[j] = this.skull.dupe[i].enemypos;
				}
				this.skull.dupe[i].skelIndex = 0;
				this.skull.dupe[i].skelInc = 0f;
				int num = this.skull.dupe[i].skelIndex + 1;
				this.skull.dupe[i].skelPos1 = this.skull.dupe[i].skelPath3[this.skull.dupe[i].skelIndex];
				this.skull.dupe[i].skelPos2 = this.skull.dupe[i].skelPath3[num];
				this.skull.dupe[i].skelStep = 0.0028f / (Vector3.Distance(this.skull.dupe[i].skelPos1, this.skull.dupe[i].skelPos2) / this.skull.dupe[i].skelOrigRate);
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x002540E8 File Offset: 0x002522E8
		public void Update1(ref Cursor cursor, ref Cursor genCursor, ref localPlayer myPlayer, ref Vector2 hitVel, ref float camshaker, ref float[,] th, ref float[,] fh, List<BloodnBacon.myDoor> combo, List<BloodnBacon.myDoor> plain)
		{
			this.bitSpray = false;
			this.decalSpray = false;
			this.bloodSpray = false;
			if (this.skull.dupe.Count > 0)
			{
				this.updateEnemySkull1(ref this.skull, ref th, ref fh, ref genCursor, ref myPlayer, combo, plain);
				this.splatEnemySkull(ref this.skull, ref cursor, ref genCursor, ref myPlayer, ref hitVel, ref camshaker);
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00254148 File Offset: 0x00252348
		public void Update2(ref Cursor cursor, ref Cursor genCursor, ref localPlayer myPlayer, ref Vector2 hitVel, ref float camshaker, ref float[,] th, ref float[,] fh, List<BloodnBacon4PT.myDoor> combo, List<BloodnBacon4PT.myDoor> plain)
		{
			this.bitSpray = false;
			this.decalSpray = false;
			this.bloodSpray = false;
			if (this.skull.dupe.Count > 0)
			{
				this.updateEnemySkull2(ref this.skull, ref th, ref fh, ref genCursor, ref myPlayer, combo, plain);
				this.splatEnemySkull(ref this.skull, ref cursor, ref genCursor, ref myPlayer, ref hitVel, ref camshaker);
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x002541A8 File Offset: 0x002523A8
		private void makeImage(Matrix[] m, int width, int hite, ref Texture2D tt)
		{
			tt = new Texture2D(this.sc.GraphicsDevice, width, hite, false, SurfaceFormat.Vector4);
			Vector4[] array = new Vector4[width * hite];
			int num = 0;
			for (int i = 0; i < m.Length; i++)
			{
				array[num] = new Vector4(m[i].M11, m[i].M21, m[i].M31, m[i].M41);
				num++;
				array[num] = new Vector4(m[i].M12, m[i].M22, m[i].M32, m[i].M42);
				num++;
				array[num] = new Vector4(m[i].M13, m[i].M23, m[i].M33, m[i].M43);
				num++;
			}
			tt.SetData<Vector4>(array);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x002542F8 File Offset: 0x002524F8
		public void enemyInstancing()
		{
			this.skull = default(enemynpc.npcEnemy);
			this.skull.hitindex = -1;
			this.skull.alive = 0;
			this.skull.alive2 = 666;
			this.skull.data = this.enemyProxy.Tag as SkinningDataX;
			this.makeImage(this.skull.data.Bones, this.skull.data.Width, this.skull.data.Hite, ref this.skull.bitmap);
			this.skull.Texture1 = this.enemyTexture;
			this.skull.model1 = this.enemyProxy;
			this.skull.max = 5;
			this.skull.dupe = new List<enemyDupe>(this.skull.max);
			this.skull.dupe.Capacity = 5;
			this.skull.display1 = new enemynpc.skinstream[this.skull.max];
			this.skull.buffer1 = new DynamicVertexBuffer(this.sc.GraphicsDevice, enemynpc.vd, this.skull.max, BufferUsage.WriteOnly);
			this.skull.targ = new Matrix[6];
			this.skull.targ[0] = Matrix.CreateTranslation(new Vector3(1.15f, -8.9f, 20.45f));
			this.skull.targ[1] = Matrix.CreateTranslation(new Vector3(0.5f, -21f, -25.6f));
			this.skull.targ[2] = Matrix.CreateTranslation(new Vector3(-6.4f, -17.11f, -6.5f));
			this.skull.targ[3] = Matrix.CreateTranslation(new Vector3(7.2f, -16.8f, -7f));
			this.skull.targ[4] = Matrix.CreateTranslation(new Vector3(-16.46f, 8.33f, 22.675f));
			this.skull.targ[5] = Matrix.CreateTranslation(new Vector3(17.5f, 8.7f, 23.56f));
			this.skull.bone = new int[] { 1, 4, 1, 1, 1, 1 };
			this.enemysphereScale = new float[] { 54.7f, 24.5f, 25.6f, 25.6f, 22f, 24f };
			this.enemyEffect = this.Content.Load<Effect>("effects\\Effectenemy1");
			this.skull.eff = this.enemyEffect;
			this.skull.uv = new Vector3[9];
			this.skull.uv[0] = new Vector3(0.01f, 0.01f, 10f);
			this.skull.uv[1] = new Vector3(0.504f, 0.073000014f, 1f);
			this.skull.uv[2] = new Vector3(0.397f, 0.287f, 1f);
			this.skull.uv[3] = new Vector3(0.611f, 0.287f, 1f);
			this.skull.uv[4] = new Vector3(0.397f, 0.439f, 1f);
			this.skull.uv[5] = new Vector3(0.649f, 0.439f, 1f);
			this.skull.uv[6] = new Vector3(0.343f, 0.856f, 1f);
			this.skull.uv[7] = new Vector3(0.19f, 0.29400003f, 1f);
			this.skull.uv[8] = new Vector3(0.814f, 0.29400003f, 1f);
			for (int i = 0; i < this.skull.uv.Length; i++)
			{
				this.skull.eff.Parameters["v" + i.ToString()].SetValue(this.skull.uv[i]);
			}
			this.skull.eff.Parameters["BoneDelta"].SetValue(1f / (float)this.skull.data.Width);
			this.skull.eff.Parameters["RowDelta"].SetValue(1f / (float)this.skull.data.Hite);
			this.skull.eff.Parameters["AnimationTexture"].SetValue(this.skull.bitmap);
			this.skull.eff.Parameters["hole1"].SetValue(this.splat2);
			this.skull.eff.Parameters["Texture"].SetValue(this.skull.Texture1);
			this.skull.eff.Parameters["Texture2"].SetValue(this.enemyDeadTexture);
			this.skull.npcDist = 20000f;
			enemyDupe.sics = new List<int>(2 * (this.skull.data.Clips.Length + 1));
			enemyDupe.sics.Add(this.skull.data.Clips[0]);
			enemyDupe.sics.Add(0);
			for (int j = 1; j < this.skull.data.Clips.Length; j++)
			{
				enemyDupe.sics.Add(this.skull.data.Clips[j] - this.skull.data.Clips[j - 1]);
				enemyDupe.sics.Add(this.skull.data.Clips[j - 1]);
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00254970 File Offset: 0x00252B70
		public void addEnemyBlood(ref enemynpc.npcEnemy n, int i, int bodypart)
		{
			int blood = n.dupe[i].blood;
			this.parts.Clear();
			this.s[4] = blood / 100000;
			this.s[3] = blood % 100000 / 10000;
			this.s[2] = blood % 10000 / 1000;
			this.s[1] = blood % 1000 / 100;
			this.s[0] = blood % 100 / 10;
			this.parts.Add(this.s[0]);
			this.parts.Add(this.s[1]);
			this.parts.Add(this.s[2]);
			this.parts.Add(this.s[3]);
			this.parts.Add(this.s[4]);
			if (!this.parts.Contains(bodypart))
			{
				this.s[n.dupe[i].splatIndex] = bodypart;
				n.dupe[i].blood = this.s[4] * 100000 + this.s[3] * 10000 + this.s[2] * 1000 + this.s[1] * 100 + this.s[0] * 10 + 6;
				n.dupe[i].splatIndex++;
				if (n.dupe[i].splatIndex > 4)
				{
					n.dupe[i].splatIndex = 0;
				}
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00254B14 File Offset: 0x00252D14
		private Matrix RotateToFaceE(Vector3 O, Vector3 P, Vector3 U, Matrix mm, ref bool turning, Matrix tilt)
		{
			if (Vector3.Distance(O, P) < 5f)
			{
				return mm;
			}
			Vector3 vector = O - P;
			Vector3 vector2 = Vector3.Cross(U, vector);
			Vector3.Normalize(ref vector2, out vector2);
			Vector3 vector3 = Vector3.Cross(vector2, U);
			Vector3.Normalize(ref vector3, out vector3);
			Vector3 vector4 = Vector3.Cross(vector3, vector2);
			Matrix matrix = new Matrix(vector2.X, vector2.Y, vector2.Z, 0f, vector4.X, vector4.Y, vector4.Z, 0f, vector3.X, vector3.Y, vector3.Z, 0f, 0f, 0f, 0f, 1f);
			Quaternion quaternion = Quaternion.CreateFromRotationMatrix(mm);
			Quaternion quaternion2 = Quaternion.CreateFromRotationMatrix(tilt * matrix);
			if (Math.Abs(quaternion2.W - quaternion.W) > 0.2f || quaternion.X != quaternion2.X || quaternion.Y != quaternion2.Y || quaternion.Z != quaternion2.Z)
			{
				matrix = Matrix.CreateFromQuaternion(Quaternion.Lerp(quaternion, quaternion2, 0.12f));
				turning = true;
				return matrix;
			}
			return mm;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00254C50 File Offset: 0x00252E50
		private void readGPSpaths(ref float[,] tunnelheights)
		{
			string text = this.mazeid.ToString();
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			StreamReader streamReader = new StreamReader("Content/gpspath/maze" + text + ".txt");
			int num = 0;
			this.plot3 = new Vector3[2100];
			while (!streamReader.EndOfStream)
			{
				float num2 = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num2 += 3000f;
				float num3 = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num3 += 3000f;
				float num4 = 0f;
				this.GetHeightFast(ref tunnelheights, new Vector3(num2, 0f, num3), ref num4);
				this.plot3[num] = new Vector3(num2, num4, num3);
				num++;
			}
			Array.Resize<Vector3>(ref this.plot3, num);
			streamReader.Close();
			streamReader.Dispose();
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00254D28 File Offset: 0x00252F28
		public void makeSkulls()
		{
			int num = 10;
			int num2 = this.mazeid;
			if (this.mazeid == 1)
			{
				Vector3 vector = new Vector3(this.enemyHome[0].X, -242f, this.enemyHome[0].Y);
				this.skull.dupe.Add(new enemyDupe(0, 0, vector, 1, 1, this.timeFrame, this.sc, 160));
				this.skull.dupe[0].myhome = this.enemyHome[0];
				this.skull.dupe[0].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound3").CreateInstance();
				this.skull.dupe[0].enemySound.IsLooped = true;
				this.skull.dupe[0].enemySound.Volume = 0f;
				this.skull.dupe[0].enemySound.Apply3D(this.skull.dupe[0].audiolistener, this.skull.dupe[0].audioemitter);
				this.skull.dupe[0].enemySound.Play();
				vector = new Vector3(this.enemyHome[2].X, -242f, this.enemyHome[2].Y);
				this.skull.dupe.Add(new enemyDupe(0, 0, vector, 1, 1, this.timeFrame, this.sc, 220));
				this.skull.dupe[1].myhome = this.enemyHome[2];
				this.skull.dupe[1].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound6").CreateInstance();
				this.skull.dupe[1].enemySound.IsLooped = true;
				this.skull.dupe[1].enemySound.Volume = 0f;
				this.skull.dupe[1].enemySound.Apply3D(this.skull.dupe[0].audiolistener, this.skull.dupe[0].audioemitter);
				this.skull.dupe[1].enemySound.Play();
			}
			if (this.mazeid == 2)
			{
				Vector3 vector2 = new Vector3(this.enemyHome[0].X, -242f, this.enemyHome[0].Y);
				this.skull.dupe.Add(new enemyDupe(0, 0, vector2, 1, 1, this.timeFrame, this.sc, 210));
				this.skull.dupe[0].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound3").CreateInstance();
				this.skull.dupe[0].myhome = this.enemyHome[0];
				this.skull.dupe[0].enemySound.IsLooped = true;
				this.skull.dupe[0].enemySound.Volume = 0f;
				this.skull.dupe[0].enemySound.Apply3D(this.skull.dupe[0].audiolistener, this.skull.dupe[0].audioemitter);
				this.skull.dupe[0].enemySound.Play();
				vector2 = new Vector3(this.enemyHome[1].X, -242f, this.enemyHome[1].Y);
				this.skull.dupe.Add(new enemyDupe(0, 0, vector2, 1, 1, this.timeFrame, this.sc, 310));
				this.skull.dupe[1].myhome = this.enemyHome[1];
				this.skull.dupe[1].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound5").CreateInstance();
				this.skull.dupe[1].enemySound.IsLooped = true;
				this.skull.dupe[1].enemySound.Volume = 0f;
				this.skull.dupe[1].enemySound.Apply3D(this.skull.dupe[0].audiolistener, this.skull.dupe[0].audioemitter);
				this.skull.dupe[1].enemySound.Play();
			}
			if (this.mazeid == 3)
			{
				Vector3 vector3 = new Vector3(this.enemyHome[0].X, -242f, this.enemyHome[0].Y);
				this.skull.dupe.Add(new enemyDupe(0, 0, vector3, 1, 1, this.timeFrame, this.sc, 220));
				this.skull.dupe[0].myhome = this.enemyHome[0];
				this.skull.dupe[0].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound2a").CreateInstance();
				this.skull.dupe[0].enemySound.IsLooped = true;
				this.skull.dupe[0].enemySound.Volume = 0f;
				this.skull.dupe[0].enemySound.Apply3D(this.skull.dupe[0].audiolistener, this.skull.dupe[0].audioemitter);
				this.skull.dupe[0].enemySound.Play();
				vector3 = new Vector3(this.enemyHome[2].X, -242f, this.enemyHome[2].Y);
				this.skull.dupe.Add(new enemyDupe(0, 0, vector3, 1, 1, this.timeFrame, this.sc, 270));
				this.skull.dupe[1].myhome = this.enemyHome[2];
				int num3 = this.rr.Next(1, 100);
				if (num3 > 60)
				{
					this.skull.dupe[1].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound2").CreateInstance();
				}
				else
				{
					this.skull.dupe[1].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound2b").CreateInstance();
				}
				this.skull.dupe[1].enemySound.IsLooped = true;
				this.skull.dupe[1].enemySound.Volume = 0f;
				this.skull.dupe[1].enemySound.Apply3D(this.skull.dupe[0].audiolistener, this.skull.dupe[0].audioemitter);
				this.skull.dupe[1].enemySound.Play();
			}
			if (this.mazeid == 4)
			{
				Vector3 vector4 = new Vector3(this.enemyHome[0].X, -242f, this.enemyHome[0].Y);
				this.skull.dupe.Add(new enemyDupe(0, 0, vector4, 1, 1, this.timeFrame, this.sc, num));
				this.skull.dupe[0].myhome = this.enemyHome[0];
				this.skull.dupe[0].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound2a").CreateInstance();
				this.skull.dupe[0].enemySound.IsLooped = true;
				this.skull.dupe[0].enemySound.Volume = 0f;
				this.skull.dupe[0].enemySound.Apply3D(this.skull.dupe[0].audiolistener, this.skull.dupe[0].audioemitter);
				this.skull.dupe[0].enemySound.Play();
				vector4 = new Vector3(this.enemyHome[1].X, -242f, this.enemyHome[1].Y);
				this.skull.dupe.Add(new enemyDupe(0, 0, vector4, 1, 1, this.timeFrame, this.sc, num));
				this.skull.dupe[1].myhome = this.enemyHome[1];
				this.skull.dupe[1].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound5").CreateInstance();
				this.skull.dupe[1].enemySound.IsLooped = true;
				this.skull.dupe[1].enemySound.Volume = 0f;
				this.skull.dupe[1].enemySound.Apply3D(this.skull.dupe[1].audiolistener, this.skull.dupe[1].audioemitter);
				this.skull.dupe[1].enemySound.Play();
				vector4 = new Vector3(this.enemyHome[2].X, -242f, this.enemyHome[2].Y);
				this.skull.dupe.Add(new enemyDupe(0, 0, vector4, 1, 1, this.timeFrame, this.sc, num));
				this.skull.dupe[2].myhome = this.enemyHome[2];
				this.skull.dupe[2].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound6").CreateInstance();
				this.skull.dupe[2].enemySound.IsLooped = true;
				this.skull.dupe[2].enemySound.Volume = 0f;
				this.skull.dupe[2].enemySound.Apply3D(this.skull.dupe[2].audiolistener, this.skull.dupe[2].audioemitter);
				this.skull.dupe[2].enemySound.Play();
				vector4 = new Vector3(this.enemyHome[3].X, -242f, this.enemyHome[3].Y);
				this.skull.dupe.Add(new enemyDupe(0, 0, vector4, 1, 1, this.timeFrame, this.sc, num));
				this.skull.dupe[3].myhome = this.enemyHome[3];
				this.skull.dupe[3].enemySound = this.Content.Load<SoundEffect>("audio\\enemySound2b").CreateInstance();
				this.skull.dupe[3].enemySound.IsLooped = true;
				this.skull.dupe[3].enemySound.Volume = 0f;
				this.skull.dupe[3].enemySound.Apply3D(this.skull.dupe[3].audiolistener, this.skull.dupe[3].audioemitter);
				this.skull.dupe[3].enemySound.Play();
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00255ABC File Offset: 0x00253CBC
		public void setSkullDifficulty()
		{
			int num = 0;
			int num2 = 2;
			int num3 = 5;
			int num4 = 6;
			int num5 = 8;
			int num6 = 300;
			int[] array = new int[] { 10, 90, 160 };
			float[] array2 = new float[] { 1.3f, 2.4f, 3.5f };
			float num7 = array2[this.sc.df];
			if (this.sc.totalPlayers > 2)
			{
				array2 = new float[] { 2f, 3.5f, 5.2f };
				num7 = array2[this.sc.df];
				array = new int[] { 20, 110, 180 };
			}
			int num8 = this.mazeid;
			if (this.mazeid == 1)
			{
				this.skull.dupe[0].health = (float)(120 + array[this.sc.df]);
				this.skull.dupe[0].tint = (float)num4;
				this.skull.dupe[0].scale = 1.25f;
				this.skull.dupe[0].attackAgain = 10;
				this.skull.dupe[0].attackAgainOrig = 10;
				this.skull.dupe[0].skelOrigRate = (float)(650 + num6);
				this.skull.dupe[0].skelFastRate = this.skull.dupe[0].skelOrigRate * num7;
				this.skull.dupe[0].health = (float)(130 + array[this.sc.df]);
				this.skull.dupe[1].tint = (float)num2;
				this.skull.dupe[1].scale = 0.8f;
				this.skull.dupe[1].attackAgain = 10;
				this.skull.dupe[1].attackAgainOrig = 10;
				this.skull.dupe[1].skelOrigRate = (float)(550 + num6);
				this.skull.dupe[1].skelFastRate = this.skull.dupe[1].skelOrigRate * num7;
			}
			if (this.mazeid == 2)
			{
				this.skull.dupe[0].health = (float)(210 + array[this.sc.df]);
				this.skull.dupe[0].tint = (float)num4;
				this.skull.dupe[0].scale = 1.25f;
				this.skull.dupe[0].attackAgain = 10;
				this.skull.dupe[0].attackAgainOrig = 10;
				this.skull.dupe[0].skelOrigRate = (float)(650 + num6);
				this.skull.dupe[0].skelFastRate = this.skull.dupe[0].skelOrigRate * num7;
				this.skull.dupe[1].health = (float)(200 + array[this.sc.df]);
				this.skull.dupe[1].tint = (float)num;
				this.skull.dupe[1].scale = 0.95f;
				this.skull.dupe[1].attackAgain = 10;
				this.skull.dupe[1].attackAgainOrig = 10;
				this.skull.dupe[1].skelOrigRate = (float)(600 + num6);
				this.skull.dupe[1].skelFastRate = this.skull.dupe[1].skelOrigRate * num7;
			}
			if (this.mazeid == 3)
			{
				this.skull.dupe[0].health = (float)(210 + array[this.sc.df]);
				this.skull.dupe[0].tint = (float)num4;
				this.skull.dupe[0].scale = 1f;
				this.skull.dupe[0].attackAgain = 1;
				this.skull.dupe[0].attackAgainOrig = 1;
				this.skull.dupe[0].skelOrigRate = (float)(650 + num6);
				this.skull.dupe[0].skelFastRate = this.skull.dupe[0].skelOrigRate * num7;
				this.skull.dupe[1].health = (float)(220 + array[this.sc.df]);
				this.skull.dupe[1].tint = (float)num3;
				this.skull.dupe[1].scale = 0.95f;
				this.skull.dupe[1].attackAgain = 1;
				this.skull.dupe[1].attackAgainOrig = 1;
				this.skull.dupe[1].skelOrigRate = (float)(600 + num6);
				this.skull.dupe[1].skelFastRate = this.skull.dupe[1].skelOrigRate * num7;
			}
			if (this.mazeid == 4)
			{
				this.skull.dupe[0].health = (float)(130 + array[this.sc.df]);
				this.skull.dupe[0].tint = (float)num4;
				this.skull.dupe[0].scale = 1f;
				this.skull.dupe[0].attackAgain = 0;
				this.skull.dupe[0].attackAgainOrig = 0;
				this.skull.dupe[0].skelOrigRate = (float)(650 + num6);
				this.skull.dupe[0].skelFastRate = this.skull.dupe[0].skelOrigRate * num7;
				this.skull.dupe[1].health = (float)(150 + array[this.sc.df]);
				this.skull.dupe[1].tint = (float)num;
				this.skull.dupe[1].scale = 0.95f;
				this.skull.dupe[1].attackAgain = 0;
				this.skull.dupe[1].attackAgainOrig = 0;
				this.skull.dupe[1].skelOrigRate = (float)(600 + num6);
				this.skull.dupe[1].skelFastRate = this.skull.dupe[1].skelOrigRate * num7;
				this.skull.dupe[2].health = (float)(180 + array[this.sc.df]);
				this.skull.dupe[2].tint = (float)num5;
				this.skull.dupe[2].scale = 0.8f;
				this.skull.dupe[2].attackAgain = 0;
				this.skull.dupe[2].attackAgainOrig = 0;
				this.skull.dupe[2].skelOrigRate = (float)(890 + num6);
				this.skull.dupe[2].skelFastRate = this.skull.dupe[2].skelOrigRate * num7;
				this.skull.dupe[3].health = (float)(200 + array[this.sc.df]);
				this.skull.dupe[3].tint = (float)num3;
				this.skull.dupe[3].scale = 1.4f;
				this.skull.dupe[3].attackAgain = 0;
				this.skull.dupe[3].attackAgainOrig = 0;
				this.skull.dupe[3].skelOrigRate = (float)(1100 + num6);
				this.skull.dupe[3].skelFastRate = this.skull.dupe[3].skelOrigRate * num7;
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00256418 File Offset: 0x00254618
		public void updateEnemySkull1(ref enemynpc.npcEnemy n, ref float[,] tunnelheights, ref float[,] farmheights, ref Cursor genCursor, ref localPlayer myPlayer, List<BloodnBacon.myDoor> combo, List<BloodnBacon.myDoor> plain)
		{
			n.alive = 0;
			n.index1 = 0;
			this.seenCounter--;
			for (int i = 0; i < n.dupe.Count; i++)
			{
				if (n.dupe[i].move > 0 && !n.dupe[i].death)
				{
					n.alive++;
				}
				if (n.dupe[i].death)
				{
					if (n.dupe[i].move > 0)
					{
						n.dupe[i].UpdateDeath(ref tunnelheights);
						n.dupe[i].enemySound.Volume = 0f;
						n.dupe[i].mypos = n.dupe[i].enemypos + new Vector3(0f, n.dupe[i].hoff, 0f);
						n.dupe[i].myRot2 = n.dupe[i].skelRot;
						n.dupe[i].transform = Matrix.CreateScale(n.dupe[i].scale) * n.dupe[i].myRot2 * Matrix.CreateTranslation(n.dupe[i].mypos);
						this.tempySkin.Transformation = n.dupe[i].transform;
						this.tempySkin.tween = n.dupe[i].tween;
						this.tempySkin.frame1 = (float)n.dupe[i].frame1;
						this.tempySkin.frame2 = (float)n.dupe[i].frame2;
						this.tempySkin.blood = (float)n.dupe[i].blood;
						this.tempySkin.tint = n.dupe[i].tint + n.dupe[i].dying;
						n.display1[n.index1] = this.tempySkin;
						n.index1++;
						if (n.alive2 == i && n.dupe[i].enemypos.Y < -250f)
						{
							if (this.sc.goldKeys.keyTusk1 && this.sc.tusk1 == 0 && this.mazeid == 1)
							{
								this.sc.goldKeys.keyTusk1 = false;
							}
							if (this.sc.goldKeys.keyTusk2 && this.sc.tusk2 == 0 && this.mazeid == 2)
							{
								this.sc.goldKeys.keyTusk2 = false;
							}
							if (this.sc.goldKeys.keyTusk3 && this.sc.tusk3 == 0 && this.mazeid == 4)
							{
								this.sc.goldKeys.keyTusk3 = false;
							}
						}
					}
				}
				else
				{
					n.dupe[i].skelInc += n.dupe[i].skelStep;
					if (n.dupe[i].skelInc > 1f)
					{
						n.dupe[i].skelInc = 0f;
						n.dupe[i].skelIndex++;
						bool flag = true;
						int num = n.dupe[i].skelIndex + 1;
						if (num > n.dupe[i].skelPath3.Length - 1)
						{
							num = 0;
							this.findenemyDestination(ref n, i, ref this.enemyGuard, ref this.enemyInvade);
							flag = false;
						}
						if (flag)
						{
							n.dupe[i].skelPos1 = n.dupe[i].skelPath3[n.dupe[i].skelIndex];
							n.dupe[i].skelPos2 = n.dupe[i].skelPath3[num];
							n.dupe[i].skelStepTimer = 0;
						}
					}
					n.dupe[i].skelPos = Vector3.Lerp(n.dupe[i].skelPos1, n.dupe[i].skelPos2, n.dupe[i].skelInc);
					float num2 = (float)Math.Atan((double)((n.dupe[i].skelPos2.Y - n.dupe[i].skelPos1.Y) / 125f));
					Matrix matrix = Matrix.CreateRotationX(num2);
					n.dupe[i].skelRot = this.RotateToFaceE(n.dupe[i].skelPos, n.dupe[i].skelPos2, Vector3.Up, n.dupe[i].skelRot, ref n.dupe[i].enemyisTurning, matrix);
					Vector3 vector = n.dupe[i].enemypos;
					n.dupe[i].enemypos = n.dupe[i].skelPos;
					if (!n.dupe[i].enemySound.IsDisposed)
					{
						float num3 = 1f - MathHelper.Clamp((Vector3.Distance(this.campos, n.dupe[i].enemypos) - 250f) / 1100f, 0f, 1f);
						n.dupe[i].enemySound.Volume = num3 * this.sc.ev;
						n.dupe[i].audioemitter.Position = n.dupe[i].mypos;
						n.dupe[i].audiolistener.Position = this.campos;
						n.dupe[i].audiolistener.Forward = Vector3.Transform(new Vector3(0f, 0f, 1f), Matrix.CreateRotationY(myPlayer.displayState.npcRotation));
						n.dupe[i].enemySound.Apply3D(n.dupe[i].audiolistener, n.dupe[i].audioemitter);
					}
					if (this.sc.myTimer % 3f == 0f)
					{
						Matrix matrix2 = Matrix.CreateLookAt(new Vector3(n.dupe[i].skelPos1.X, n.dupe[i].skelPos1.Y + 45f, n.dupe[i].skelPos1.Z), new Vector3(n.dupe[i].skelPos2.X, n.dupe[i].skelPos2.Y + 45f, n.dupe[i].skelPos2.Z), Vector3.Up);
						this.UpdatePickingEnemyDoors(this.enemyProj, matrix2, ref genCursor, ref n, i, combo, plain);
					}
					if (n.dupe[i].enemyBlocked)
					{
						n.dupe[i].enemypos = vector;
						this.findenemyDestination(ref n, i, ref this.enemyGuard, ref this.enemyInvade);
						n.dupe[i].enemyBlocked = false;
					}
					n.dupe[i].UpdateNormal(ref tunnelheights);
					n.dupe[i].mypos = n.dupe[i].enemypos + new Vector3(0f, n.dupe[i].hoff, 0f);
					n.dupe[i].myRot2 = n.dupe[i].skelRot;
					n.dupe[i].transform = Matrix.CreateScale(n.dupe[i].scale) * n.dupe[i].myRot2 * Matrix.CreateTranslation(n.dupe[i].mypos);
					this.tempySkin.Transformation = n.dupe[i].transform;
					this.tempySkin.tween = n.dupe[i].tween;
					this.tempySkin.frame1 = (float)n.dupe[i].frame1;
					this.tempySkin.frame2 = (float)n.dupe[i].frame2;
					this.tempySkin.blood = (float)n.dupe[i].blood;
					this.tempySkin.tint = n.dupe[i].tint;
					n.display1[n.index1] = this.tempySkin;
					n.index1++;
				}
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00256DDC File Offset: 0x00254FDC
		public void updateEnemySkull2(ref enemynpc.npcEnemy n, ref float[,] tunnelheights, ref float[,] farmheights, ref Cursor genCursor, ref localPlayer myPlayer, List<BloodnBacon4PT.myDoor> combo, List<BloodnBacon4PT.myDoor> plain)
		{
			n.alive = 0;
			n.index1 = 0;
			this.seenCounter--;
			for (int i = 0; i < n.dupe.Count; i++)
			{
				if (n.dupe[i].move > 0 && !n.dupe[i].death)
				{
					n.alive++;
				}
				if (n.dupe[i].death)
				{
					if (n.dupe[i].move > 0)
					{
						n.dupe[i].UpdateDeath(ref tunnelheights);
						n.dupe[i].enemySound.Volume = 0f;
						n.dupe[i].mypos = n.dupe[i].enemypos + new Vector3(0f, n.dupe[i].hoff, 0f);
						n.dupe[i].myRot2 = n.dupe[i].skelRot;
						n.dupe[i].transform = Matrix.CreateScale(n.dupe[i].scale) * n.dupe[i].myRot2 * Matrix.CreateTranslation(n.dupe[i].mypos);
						this.tempySkin.Transformation = n.dupe[i].transform;
						this.tempySkin.tween = n.dupe[i].tween;
						this.tempySkin.frame1 = (float)n.dupe[i].frame1;
						this.tempySkin.frame2 = (float)n.dupe[i].frame2;
						this.tempySkin.blood = (float)n.dupe[i].blood;
						this.tempySkin.tint = n.dupe[i].tint + n.dupe[i].dying;
						n.display1[n.index1] = this.tempySkin;
						n.index1++;
						if (n.alive2 == i && n.dupe[i].enemypos.Y < -250f)
						{
							if (this.sc.goldKeys.keyTusk1 && this.sc.tusk1 == 0 && this.mazeid == 1)
							{
								this.sc.goldKeys.keyTusk1 = false;
							}
							if (this.sc.goldKeys.keyTusk2 && this.sc.tusk2 == 0 && this.mazeid == 2)
							{
								this.sc.goldKeys.keyTusk2 = false;
							}
							if (this.sc.goldKeys.keyTusk3 && this.sc.tusk3 == 0 && this.mazeid == 4)
							{
								this.sc.goldKeys.keyTusk3 = false;
							}
						}
					}
				}
				else
				{
					n.dupe[i].skelInc += n.dupe[i].skelStep;
					if (n.dupe[i].skelInc > 1f)
					{
						n.dupe[i].skelInc = 0f;
						n.dupe[i].skelIndex++;
						bool flag = true;
						int num = n.dupe[i].skelIndex + 1;
						if (num > n.dupe[i].skelPath3.Length - 1)
						{
							if (this.sc.host)
							{
								num = 0;
								this.findenemyDestination(ref n, i, ref this.enemyGuard, ref this.enemyInvade);
								flag = false;
							}
							else
							{
								n.dupe[i].skelIndex = n.dupe[i].skelPath3.Length - 2;
								n.dupe[i].skelInc = 1f;
								flag = false;
							}
						}
						if (flag)
						{
							n.dupe[i].skelPos1 = n.dupe[i].skelPath3[n.dupe[i].skelIndex];
							n.dupe[i].skelPos2 = n.dupe[i].skelPath3[num];
							n.dupe[i].skelStepTimer = 0;
						}
					}
					n.dupe[i].skelPos = Vector3.Lerp(n.dupe[i].skelPos1, n.dupe[i].skelPos2, n.dupe[i].skelInc);
					float num2 = (float)Math.Atan((double)((n.dupe[i].skelPos2.Y - n.dupe[i].skelPos1.Y) / 125f));
					Matrix matrix = Matrix.CreateRotationX(num2);
					n.dupe[i].skelRot = this.RotateToFaceE(n.dupe[i].skelPos, n.dupe[i].skelPos2, Vector3.Up, n.dupe[i].skelRot, ref n.dupe[i].enemyisTurning, matrix);
					Vector3 vector = n.dupe[i].enemypos;
					n.dupe[i].enemypos = n.dupe[i].skelPos;
					if (!n.dupe[i].enemySound.IsDisposed)
					{
						float num3 = 1f - MathHelper.Clamp((Vector3.Distance(this.campos, n.dupe[i].enemypos) - 350f) / 1400f, 0f, 1f);
						n.dupe[i].enemySound.Volume = num3 * this.sc.ev;
						n.dupe[i].audioemitter.Position = n.dupe[i].mypos;
						n.dupe[i].audiolistener.Position = this.campos;
						n.dupe[i].audiolistener.Forward = Vector3.Transform(new Vector3(0f, 0f, 1f), Matrix.CreateRotationY(myPlayer.displayState.npcRotation));
						n.dupe[i].enemySound.Apply3D(n.dupe[i].audiolistener, n.dupe[i].audioemitter);
					}
					if (this.sc.myTimer % 3f == 0f && this.sc.host)
					{
						Matrix matrix2 = Matrix.CreateLookAt(new Vector3(n.dupe[i].skelPos1.X, n.dupe[i].skelPos1.Y + 45f, n.dupe[i].skelPos1.Z), new Vector3(n.dupe[i].skelPos2.X, n.dupe[i].skelPos2.Y + 45f, n.dupe[i].skelPos2.Z), Vector3.Up);
						this.UpdatePickingEnemyDoors(this.enemyProj, matrix2, ref genCursor, ref n, i, combo, plain);
					}
					if (n.dupe[i].enemyBlocked && this.sc.host)
					{
						n.dupe[i].enemypos = vector;
						this.findenemyDestination(ref n, i, ref this.enemyGuard, ref this.enemyInvade);
						n.dupe[i].enemyBlocked = false;
					}
					n.dupe[i].UpdateNormal(ref tunnelheights);
					n.dupe[i].mypos = n.dupe[i].enemypos + new Vector3(0f, n.dupe[i].hoff, 0f);
					n.dupe[i].myRot2 = n.dupe[i].skelRot;
					n.dupe[i].transform = Matrix.CreateScale(n.dupe[i].scale) * n.dupe[i].myRot2 * Matrix.CreateTranslation(n.dupe[i].mypos);
					this.tempySkin.Transformation = n.dupe[i].transform;
					this.tempySkin.tween = n.dupe[i].tween;
					this.tempySkin.frame1 = (float)n.dupe[i].frame1;
					this.tempySkin.frame2 = (float)n.dupe[i].frame2;
					this.tempySkin.blood = (float)n.dupe[i].blood;
					this.tempySkin.tint = n.dupe[i].tint;
					n.display1[n.index1] = this.tempySkin;
					n.index1++;
				}
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00257808 File Offset: 0x00255A08
		private void splatEnemySkull(ref enemynpc.npcEnemy n, ref Cursor cursor, ref Cursor genCursor, ref localPlayer myPlayer, ref Vector2 hitVel, ref float camshaker)
		{
			this.attackWaite--;
			n.hitindex = -1;
			n.npcDist = 20000f;
			float num = cursor.closestIntersection * cursor.closestIntersection;
			Vector3 vector = Vector3.Normalize(this.camlookpos - this.campos);
			Vector3 vector2 = -vector * 100f + this.campos;
			int num2 = 25000000;
			float num3 = 0f;
			Vector2 vector3 = new Vector2(myPlayer.displayState.npcPosition.X, myPlayer.displayState.npcPosition.Z);
			for (int i = 0; i < n.dupe.Count; i++)
			{
				Vector2 vector4 = new Vector2(n.dupe[i].mypos.X, n.dupe[i].mypos.Z) - vector3;
				float num4 = vector4.LengthSquared();
				if (!myPlayer.gunFired && this.attackWaite <= 0 && myPlayer.now.health > 0f && !n.dupe[i].death && myPlayer.displayState.npcPosition.Y < -80f && num4 < 400f * n.dupe[i].scale * (400f * n.dupe[i].scale))
				{
					int num5 = n.dupe[i].frame1 * 19 + 1;
					Matrix.CreateTranslation(5f, 0f, 0f, out this.m1);
					Matrix.Multiply(ref this.m1, ref n.data.Bones[num5], out this.m2);
					Matrix.Multiply(ref this.m2, ref n.dupe[i].transform, out this.m3);
					Vector3 vector5;
					Vector3.Transform(ref this.vZero, ref this.m3, out vector5);
					float num6 = (new Vector2(vector5.X, vector5.Z) - vector3).LengthSquared();
					float num7 = 10000f * n.dupe[i].scale * n.dupe[i].scale;
					if (num6 < num7)
					{
						float num8 = (float)this.rr.Next(900, 1000) / 100f;
						if (this.sc.df == 0)
						{
							num8 = (float)this.rr.Next(900, 1200) / 300f;
						}
						if (!myPlayer.isLiftingOpponent && myPlayer.fallState == 0)
						{
							hitVel = -Vector2.Normalize(vector4) * 2.5f;
							hitVel = Vector2.Transform(new Vector2(-hitVel.X, hitVel.Y), Matrix.CreateRotationZ(-this.headRot));
						}
						if (this.tunnelAvoidDamage)
						{
							num8 = 0f;
							hitVel = Vector2.Zero;
						}
						if (myPlayer.now.health < 150f)
						{
							this.sc.hurt.Play(this.sc.ev, (float)this.rr.Next(-30, 20) / 100f, 0f);
						}
						else
						{
							this.sc.boarbite[this.rr.Next(0, 5)].Play(this.sc.ev, (float)this.rr.Next(-10, 20) / 100f, (float)this.rr.Next(-70, 70) / 100f);
						}
						if (this.rr.Next(1, 200) < 10)
						{
							camshaker = 20f;
						}
						if (this.rr.Next(1, 200) < 45)
						{
							myPlayer.bloodCoil = 50;
						}
						if (myPlayer.isDown)
						{
							myPlayer.damHealth(num8 * 0.5f, this.sc.cheat_Invincible);
							this.attackWaite = 200;
							n.dupe[i].attackAgain = 0;
						}
						else
						{
							if (this.rr.Next(1, 100) < 20)
							{
								this.seen4.Play(this.sc.ev, 0f, 0f);
							}
							myPlayer.damHealth(num8, this.sc.cheat_Invincible);
							this.attackWaite = 2;
							n.dupe[i].attackAgain = 0;
						}
					}
				}
				if (myPlayer.gunChoice != 14 && myPlayer.gunFired && num4 < (float)num2)
				{
					Vector3 vector6 = new Vector3(n.dupe[i].mypos.X, n.dupe[i].mypos.Y, n.dupe[i].mypos.Z) - vector2;
					Vector3 vector5;
					Vector3.Normalize(ref vector6, out vector5);
					Vector3.Dot(ref vector5, ref vector, out num3);
					if (num4 < 22500f || num3 > this.sc.myfov)
					{
						float num9 = 0.98f;
						if (num4 < 90000f)
						{
							num9 = 0.2f;
						}
						if ((!myPlayer.closeCam || num3 > num9) && num4 < n.npcDist * n.npcDist && num4 < num)
						{
							float num10 = 1f;
							if (myPlayer.gunChoice == 8 && num4 > 90000f)
							{
								num10 = 2f;
							}
							int num11 = n.dupe[i].frame1 * 19 + 3;
							Matrix matrix = n.data.Bones[num11] * n.dupe[i].transform;
							Matrix.CreateScale(num10, out this.m1);
							Matrix.Multiply(ref this.m1, ref matrix, out this.m2);
							Vector3.Transform(ref this.bottomCorner, ref this.m2, out this.min);
							Vector3.Transform(ref this.topCornernew, ref this.m2, out this.max);
							this.distCheck = genCursor.hitBox(myPlayer.gunpos, myPlayer.gunlook, this.min, this.max);
							if (this.distCheck != null && this.distCheck.Value < n.npcDist && this.distCheck.Value < cursor.closestIntersection)
							{
								n.npcDist = this.distCheck.Value;
								n.hitindex = i;
							}
						}
					}
				}
			}
			if (this.skull.hitindex == -1)
			{
				this.skull.npcDist = 10000f;
				return;
			}
			if (myPlayer.gunChoice == 8 && this.skull.npcDist > 1500f)
			{
				this.skull.npcDist = 10000f;
				this.skull.hitindex = -1;
				return;
			}
			if (this.skull.npcDist > cursor.closestIntersection)
			{
				this.skull.npcDist = 10000f;
				this.skull.hitindex = -1;
				return;
			}
			float num12 = 9000f;
			this.cubeCenter = Vector3.Zero;
			int num13 = -1;
			for (int j = 0; j < n.targ.Length; j++)
			{
				int num14 = n.dupe[this.skull.hitindex].frame1 * 19 + n.bone[j];
				this.cubeMatrix = n.targ[j] * n.data.Bones[num14] * n.dupe[this.skull.hitindex].transform;
				this.cubeCenter = Vector3.Transform(Vector3.Zero, this.cubeMatrix);
				this.distCheck = genCursor.hitSphere2(myPlayer.gunpos, myPlayer.gunlook, this.cubeCenter, n.dupe[this.skull.hitindex].scale * (0.5f * this.enemysphereScale[j]));
				if (this.distCheck != null && this.distCheck.Value < num12)
				{
					num12 = this.distCheck.Value;
					num13 = j ?? 5;
				}
			}
			if (num13 == -1)
			{
				this.skull.hitindex = -1;
				return;
			}
			if (num12 < 9000f)
			{
				if (this.sc.host)
				{
					n.dupe[this.skull.hitindex].localhitCount++;
					if (n.dupe[this.skull.hitindex].localhitCount > 4 && !n.dupe[this.skull.hitindex].homing)
					{
						this.lastPlayerPos = new Vector2(this.campos.X, this.campos.Z);
						this.findenemyDestination2(ref n, this.skull.hitindex, this.lastPlayerPos);
						n.dupe[this.skull.hitindex].localhitCount = 0;
					}
				}
				myPlayer.now.accuracy = (ushort)this.skull.hitindex;
				bool flag = this.explosiveCount > 0 && this.handtype[myPlayer.gunChoice] == 2;
				this.outside = num12 * genCursor.rayDir + genCursor.rayPos;
				this.scale = n.dupe[this.skull.hitindex].scale;
				if (!flag)
				{
					this.bitSpray = true;
					myPlayer.now.gunfired = 12;
				}
				if (flag)
				{
					this.bloodSpray = true;
					myPlayer.now.gunfired = 13;
					if (this.rr.Next(1, 100) < 20)
					{
						this.addEnemyBlood(ref n, this.skull.hitindex, this.rr.Next(0, 9));
					}
					if (this.rr.Next(1, 100) < 50)
					{
						Vector3 rayDir = genCursor.rayDir;
						Vector3 vector7 = new Vector3(n.dupe[this.skull.hitindex].mypos.X, n.dupe[this.skull.hitindex].mypos.Y + 50f, n.dupe[this.skull.hitindex].mypos.Z);
						this.skullView = Matrix.CreateLookAt(vector7, new Vector3(vector7.X + (float)this.rr.Next(-7000, 7000) / 40f, vector7.Y + (float)this.rr.Next(-7000, 7000) / 40f, vector7.Z + (float)this.rr.Next(-7000, 7000) / 40f), Vector3.Right);
						this.decalSpray = true;
					}
				}
				if (!n.dupe[this.skull.hitindex].death && (n.dupe[this.skull.hitindex].timer < 30f || (n.dupe[this.skull.hitindex].defaultClip >= 0 && n.dupe[this.skull.hitindex].defaultClip <= 2)))
				{
					n.dupe[this.skull.hitindex].temp1 = 1;
					n.dupe[this.skull.hitindex].temp2 = 1;
					n.dupe[this.skull.hitindex].makeCalc(this.rr.Next(5, 8), 35f);
					n.dupe[this.skull.hitindex].defaultClip = 5;
				}
				if (!flag)
				{
					int num15 = this.rr.Next(0, 4);
					this.sc.metalHit[num15].Play(this.sc.ev * 0.5f, (float)this.rr.Next(-30, 40) / 100f, (float)this.rr.Next(-30, 30) / 100f);
				}
				if (n.dupe[this.skull.hitindex].health > 0f && !n.dupe[this.skull.hitindex].death)
				{
					if (flag)
					{
						n.dupe[this.skull.hitindex].health -= this.gunDam[myPlayer.lastWeapon] * 5f;
						if (this.sc.df == 0)
						{
							n.dupe[this.skull.hitindex].health -= this.gunDam[myPlayer.lastWeapon] * 4f;
						}
					}
					if (!flag)
					{
						n.dupe[this.skull.hitindex].health -= this.gunDam[myPlayer.lastWeapon] * 1f;
						if (this.sc.df == 0)
						{
							n.dupe[this.skull.hitindex].health -= this.gunDam[myPlayer.lastWeapon] * 2f;
						}
					}
					if (n.dupe[this.skull.hitindex].health <= 0f)
					{
						this.killSkull(ref n, this.skull.hitindex);
						this.sendDeath = this.skull.hitindex;
					}
				}
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x002586A0 File Offset: 0x002568A0
		public void killSkull(ref enemynpc.npcEnemy n, int i)
		{
			if (enemyDupe.isChasing == i)
			{
				enemyDupe.isChasing = -1;
			}
			n.dupe[i].timeofDeath = -this.timeFrame;
			n.dupe[i].health = 0f;
			n.dupe[i].death = true;
			n.dupe[i].enemySound.Volume = 0f;
			this.enemydie.Play(this.sc.ev, (float)this.rr.Next(-20, 20) / 100f, 0f);
			int num = this.rr.Next(8, 10);
			n.dupe[i].defaultClip = num;
			n.dupe[i].makeCalc(num, 238f);
			n.dupe[i].temp1 = 1;
			if (n.alive == 1)
			{
				bool flag = (this.sc.goldKeys.keyTusk1 && this.sc.tusk1 == 0 && this.mazeid == 1) || (this.sc.goldKeys.keyTusk2 && this.sc.tusk2 == 0 && this.mazeid == 2) || (this.sc.goldKeys.keyTusk3 && this.sc.tusk3 == 0 && this.mazeid == 4);
				if (flag)
				{
					n.alive2 = i;
					this.sc.newtip.Play(this.sc.ev, 0f, 0f);
					this.sc.goldKeys.tuskpos = new Vector3(n.dupe[i].enemypos.X, -265f, n.dupe[i].enemypos.Z);
					this.sc.goldKeys.tuskmatrix = Matrix.CreateScale(5f) * Matrix.CreateRotationY(3f) * Matrix.CreateTranslation(this.sc.goldKeys.tuskpos);
				}
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x002588DC File Offset: 0x00256ADC
		public void findenemyDestination(ref enemynpc.npcEnemy n, int i, ref Vector2[] guard, ref Vector2[] invade)
		{
			if (enemyDupe.isChasing == i)
			{
				enemyDupe.isChasing = -1;
			}
			bool flag = false;
			bool flag2 = false;
			n.dupe[i].enemyCounter++;
			if (n.dupe[i].blockedCount > 3)
			{
				flag2 = true;
			}
			if (!flag2 && (flag || n.dupe[i].enemyCounter % 3 == 0))
			{
				n.dupe[i].destination = guard[this.rr.Next(0, guard.Length)];
				float num = Vector2.Distance(new Vector2(n.dupe[i].destination.X, n.dupe[i].destination.Y), new Vector2(n.dupe[i].enemypos.X, n.dupe[i].enemypos.Z));
				if (num < 250f)
				{
					flag = true;
					for (int j = 0; j < guard.Length; j++)
					{
						n.dupe[i].destination = guard[j];
						num = Vector2.Distance(new Vector2(n.dupe[i].destination.X, n.dupe[i].destination.Y), new Vector2(n.dupe[i].enemypos.X, n.dupe[i].enemypos.Z));
						if (num > 250f)
						{
							flag = false;
							break;
						}
					}
				}
				else
				{
					flag = false;
				}
			}
			if (flag2 || flag || n.dupe[i].enemyCounter % 3 == 1)
			{
				n.dupe[i].destination = n.dupe[i].myhome;
				if (Vector2.Distance(new Vector2(n.dupe[i].destination.X, n.dupe[i].destination.Y), new Vector2(n.dupe[i].enemypos.X, n.dupe[i].enemypos.Z)) < 250f)
				{
					flag = true;
				}
			}
			if (!flag2 && (flag || n.dupe[i].enemyCounter % 3 == 2))
			{
				n.dupe[i].destination = invade[this.rr.Next(0, invade.Length)];
				float num2 = Vector2.Distance(new Vector2(n.dupe[i].destination.X, n.dupe[i].destination.Y), new Vector2(n.dupe[i].enemypos.X, n.dupe[i].enemypos.Z));
				if (num2 < 250f)
				{
					for (int k = 0; k < invade.Length; k++)
					{
						n.dupe[i].destination = invade[k];
						num2 = Vector2.Distance(new Vector2(n.dupe[i].destination.X, n.dupe[i].destination.Y), new Vector2(n.dupe[i].enemypos.X, n.dupe[i].enemypos.Z));
						if (num2 > 250f)
						{
							break;
						}
					}
				}
			}
			int num3 = 1;
			if (n.dupe[i].blockedCount > 3)
			{
				num3 = 3;
			}
			int num4 = this.rr.Next(1, 230);
			string text = this.skeletonPath(num3, ref n, i, n.dupe[i].enemypos, n.dupe[i].destination, num4);
			if (text == "good")
			{
				n.dupe[i].blockedCount = 0;
				n.dupe[i].homing = false;
				this.sendTime += 1;
				this.enemypos = n.dupe[i].enemypos;
				this.destination = n.dupe[i].destination;
				this.sendSeed = num4;
				this.iter = num3;
				this.skullindex = i;
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00258DD8 File Offset: 0x00256FD8
		public string skeletonPath(int ity, ref enemynpc.npcEnemy n, int i, Vector3 enemypos, Vector2 destination, int myseed)
		{
			List<Vector3> list = new List<Vector3>();
			float num = 150f;
			int num2 = -1;
			for (int j = 0; j < this.plot3.Length; j++)
			{
				list.Add(this.plot3[j]);
				float num3 = Vector2.Distance(new Vector2(this.plot3[j].X, this.plot3[j].Z), new Vector2(enemypos.X, enemypos.Z));
				if (num3 < num)
				{
					num = num3;
					num2 = j;
				}
			}
			if (num2 == -1)
			{
				return "lost start not found";
			}
			Vector3 vector = this.plot3[num2];
			bool flag = false;
			List<Vector3> final = new List<Vector3>();
			List<Vector3> search = new List<Vector3>();
			List<Vector3> list2 = new List<Vector3>();
			Random random = new Random(myseed);
			list.ForEach(delegate(Vector3 item)
			{
				search.Add(item);
			});
			for (int k = 0; k < 100; k++)
			{
				int num4 = random.Next(0, search.Count);
				int num5 = random.Next(0, search.Count);
				Vector3 vector2 = search[num4];
				search[num4] = search[num5];
				search[num5] = vector2;
			}
			for (int l = 0; l < ity; l++)
			{
				flag = this.buildPath(ref search, ref list2, vector, destination, myseed);
				if (final.Count == 0 || (list2.Count < final.Count && flag))
				{
					final.Clear();
					list2.ForEach(delegate(Vector3 item)
					{
						final.Add(item);
					});
				}
				search.Clear();
				list2.Clear();
				myseed++;
				list.ForEach(delegate(Vector3 item)
				{
					search.Add(item);
				});
				for (int m = 0; m < 100; m++)
				{
					int num6 = random.Next(0, search.Count);
					int num7 = random.Next(0, search.Count);
					Vector3 vector3 = search[num6];
					search[num6] = search[num7];
					search[num7] = vector3;
				}
			}
			if (final.Count < 3 && flag)
			{
				return "too close";
			}
			if (!flag)
			{
				return "lost destination not found";
			}
			final.Insert(0, enemypos);
			Array.Resize<Vector3>(ref n.dupe[i].skelPath3, final.Count);
			for (int num8 = 0; num8 < final.Count; num8++)
			{
				n.dupe[i].skelPath3[num8] = final[num8];
			}
			n.dupe[i].skelIndex = 0;
			n.dupe[i].skelInc = 0f;
			int num9 = n.dupe[i].skelIndex + 1;
			n.dupe[i].skelPos1 = n.dupe[i].skelPath3[n.dupe[i].skelIndex];
			n.dupe[i].skelPos2 = n.dupe[i].skelPath3[num9];
			n.dupe[i].skelStep = 0.0028f / (Vector3.Distance(n.dupe[i].skelPos1, n.dupe[i].skelPos2) / n.dupe[i].skelOrigRate);
			n.dupe[i].homing = false;
			return "good";
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0025921C File Offset: 0x0025741C
		public bool buildPath(ref List<Vector3> source, ref List<Vector3> result, Vector3 start, Vector2 end, int seed)
		{
			bool flag = true;
			Random random = new Random(seed);
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < source.Count; i++)
			{
				float num = Vector2.Distance(new Vector2(start.X, start.Z), new Vector2(source[i].X, source[i].Z));
				if (num > 50f && num <= 135f)
				{
					list.Add(source[i]);
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			List<Vector3> list2 = new List<Vector3>();
			while (list.Count > 0)
			{
				int num2 = random.Next(0, list.Count);
				list2.Add(list[num2]);
				list.RemoveAt(num2);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				if (Vector2.Distance(end, new Vector2(list2[j].X, list2[j].Z)) < 125f)
				{
					result.Add(list2[j]);
					return true;
				}
				start = list2[j];
				source.Remove(start);
				result.Add(start);
				flag = this.buildPath(ref source, ref result, start, end, seed + 1);
				if (flag)
				{
					return true;
				}
				if (!flag)
				{
					result.Remove(start);
				}
			}
			return flag;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00259384 File Offset: 0x00257584
		public void findenemyDestination2(ref enemynpc.npcEnemy n, int i, Vector2 dest)
		{
			n.dupe[i].destination = dest;
			int num = this.rr.Next(1, 230);
			string text = this.skullChasePath(3, ref n, i, n.dupe[i].enemypos, n.dupe[i].destination, num);
			if (text == "good")
			{
				n.dupe[i].blockedCount = 0;
				this.sendTime += 1;
				this.enemypos = n.dupe[i].enemypos;
				this.destination = n.dupe[i].destination;
				this.sendSeed = num;
				this.iter = 33;
				this.skullindex = i;
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00259488 File Offset: 0x00257688
		public string skullChasePath(int ity, ref enemynpc.npcEnemy n, int i, Vector3 enemypos, Vector2 destination, int myseed)
		{
			List<Vector3> list = new List<Vector3>();
			float num = 130f;
			int num2 = -1;
			int num3 = 0;
			for (int j = 0; j < this.plot3.Length; j++)
			{
				float num4 = Vector2.Distance(destination, new Vector2(enemypos.X, enemypos.Z)) + 400f;
				float num5 = Vector2.Distance(destination, new Vector2(this.plot3[j].X, this.plot3[j].Z));
				float num6 = Vector2.Distance(new Vector2(this.plot3[j].X, this.plot3[j].Z), new Vector2(enemypos.X, enemypos.Z));
				if (num6 <= num4)
				{
					num3++;
					list.Add(this.plot3[j]);
					if (num6 < num && num5 <= num4)
					{
						num = num6;
						num2 = j;
					}
				}
			}
			if (num2 == -1)
			{
				enemyDupe.isChasing = -1;
				return "nostart";
			}
			Vector3 vector = this.plot3[num2];
			bool flag = false;
			List<Vector3> final = new List<Vector3>();
			List<Vector3> search = new List<Vector3>();
			List<Vector3> list2 = new List<Vector3>();
			list.ForEach(delegate(Vector3 item)
			{
				search.Add(item);
			});
			for (int k = 0; k < ity; k++)
			{
				flag = this.buildChasePath(ref search, ref list2, vector, destination, myseed);
				if (final.Count == 0 || (list2.Count < final.Count && flag))
				{
					final.Clear();
					list2.ForEach(delegate(Vector3 item)
					{
						final.Add(item);
					});
				}
				search.Clear();
				list2.Clear();
				list.ForEach(delegate(Vector3 item)
				{
					search.Add(item);
				});
			}
			if (final.Count < 1 && flag)
			{
				enemyDupe.isChasing = -1;
				return "tooclose";
			}
			if (!flag)
			{
				enemyDupe.isChasing = -1;
				return "lost";
			}
			final.Insert(0, new Vector3(enemypos.X, enemypos.Y, enemypos.Z));
			Array.Resize<Vector3>(ref n.dupe[i].skelPath3, final.Count);
			for (int l = 0; l < final.Count; l++)
			{
				n.dupe[i].skelPath3[l] = final[l];
			}
			n.dupe[i].skelIndex = 0;
			n.dupe[i].skelInc = 0f;
			int num7 = n.dupe[i].skelIndex + 1;
			n.dupe[i].skelPos1 = n.dupe[i].skelPath3[n.dupe[i].skelIndex];
			n.dupe[i].skelPos2 = n.dupe[i].skelPath3[num7];
			n.dupe[i].skelStep = 0.0028f / (Vector3.Distance(n.dupe[i].skelPos1, n.dupe[i].skelPos2) / n.dupe[i].skelFastRate);
			int num8 = this.rr.Next(1, 100);
			if (num8 > 30)
			{
				this.seen4.Play(this.sc.ev, 0f, 0f);
			}
			else
			{
				this.seen.Play(this.sc.ev, 0f, 0f);
			}
			if (!n.dupe[i].death)
			{
				int num9 = this.rr.Next(3, 5);
				n.dupe[i].makeCalc(num9, 90f);
				n.dupe[i].defaultClip = num9;
			}
			n.dupe[i].localhitCount = 0;
			n.dupe[i].remotehitCount = 0;
			n.dupe[i].homing = true;
			return "good";
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00259934 File Offset: 0x00257B34
		private bool buildChasePath(ref List<Vector3> source, ref List<Vector3> result, Vector3 start, Vector2 end, int myseed)
		{
			bool flag = true;
			Random random = new Random(myseed);
			List<Vector3> list = new List<Vector3>();
			float num = 10000f;
			for (int i = 0; i < source.Count; i++)
			{
				float num2 = Vector2.Distance(new Vector2(start.X, start.Z), new Vector2(source[i].X, source[i].Z));
				float num3 = Vector2.Distance(end, new Vector2(source[i].X, source[i].Z));
				if (num2 > 50f && num2 <= 125f && num3 < num)
				{
					if (num != 10000f)
					{
						list.RemoveAt(list.Count - 1);
					}
					num = num3;
					list.Add(source[i]);
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			List<Vector3> list2 = new List<Vector3>();
			while (list.Count > 0)
			{
				int num4 = random.Next(0, list.Count);
				list2.Add(list[num4]);
				list.RemoveAt(num4);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				if (Vector2.Distance(end, new Vector2(list2[j].X, list2[j].Z)) <= 125f)
				{
					result.Add(list2[j]);
					return true;
				}
				start = list2[j];
				source.Remove(start);
				result.Add(start);
				flag = this.buildChasePath(ref source, ref result, start, end, myseed + 1);
				if (flag)
				{
					return true;
				}
				if (!flag)
				{
					result.Remove(start);
				}
			}
			return flag;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00259AF4 File Offset: 0x00257CF4
		private void UpdatePickingEnemyDoors(Matrix projection, Matrix view, ref Cursor c, ref enemynpc.npcEnemy n, int p, List<BloodnBacon.myDoor> combo, List<BloodnBacon.myDoor> plain)
		{
			bool flag = false;
			n.dupe[p].blockedTimer--;
			if (n.dupe[p].blockedTimer <= 0)
			{
				n.dupe[p].blockedCount = 0;
			}
			this.cursorRay = c.CalculateCursorRay(projection, view);
			c.rayDir = (c.rayPos = (c.pickedTriangle[0] = (c.pickedTriangle[1] = (c.pickedTriangle[2] = Vector3.Zero))));
			c.rayDir = this.cursorRay.Direction;
			c.rayPos = this.cursorRay.Position;
			c.closestIntersection = 10000f;
			for (int i = 0; i < combo.Count; i++)
			{
				flag = false;
				c.RayInteresectGeneric(this.cursorRay, combo[i].doorMatrix, ref c.door1Vertices, ref flag);
				if (flag && combo[i].doorlock && c.closestIntersection < 200f)
				{
					n.dupe[p].enemyBlocked = true;
					n.dupe[p].blockedTimer = 50;
					n.dupe[p].blockedCount++;
					return;
				}
			}
			for (int j = 0; j < plain.Count; j++)
			{
				flag = false;
				c.RayInteresectGeneric(this.cursorRay, plain[j].doorMatrix, ref c.door1Vertices, ref flag);
				if (flag && plain[j].doorlock && c.closestIntersection < 200f)
				{
					n.dupe[p].enemyBlocked = true;
					n.dupe[p].blockedTimer = 50;
					n.dupe[p].blockedCount++;
					return;
				}
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00259D28 File Offset: 0x00257F28
		private void UpdatePickingEnemyDoors(Matrix projection, Matrix view, ref Cursor c, ref enemynpc.npcEnemy n, int p, List<BloodnBacon4PT.myDoor> combo, List<BloodnBacon4PT.myDoor> plain)
		{
			bool flag = false;
			n.dupe[p].blockedTimer--;
			if (n.dupe[p].blockedTimer <= 0)
			{
				n.dupe[p].blockedCount = 0;
			}
			this.cursorRay = c.CalculateCursorRay(projection, view);
			c.rayDir = (c.rayPos = (c.pickedTriangle[0] = (c.pickedTriangle[1] = (c.pickedTriangle[2] = Vector3.Zero))));
			c.rayDir = this.cursorRay.Direction;
			c.rayPos = this.cursorRay.Position;
			c.closestIntersection = 10000f;
			for (int i = 0; i < combo.Count; i++)
			{
				flag = false;
				c.RayInteresectGeneric(this.cursorRay, combo[i].doorMatrix, ref c.door1Vertices, ref flag);
				if (flag && combo[i].doorlock && c.closestIntersection < 200f)
				{
					n.dupe[p].enemyBlocked = true;
					n.dupe[p].blockedTimer = 50;
					n.dupe[p].blockedCount++;
					return;
				}
			}
			for (int j = 0; j < plain.Count; j++)
			{
				flag = false;
				c.RayInteresectGeneric(this.cursorRay, plain[j].doorMatrix, ref c.door1Vertices, ref flag);
				if (flag && plain[j].doorlock && c.closestIntersection < 200f)
				{
					n.dupe[p].enemyBlocked = true;
					n.dupe[p].blockedTimer = 50;
					n.dupe[p].blockedCount++;
					return;
				}
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00259F5C File Offset: 0x0025815C
		private void UpdatePickingEnemyPlayer(Matrix projection, ref Cursor c, ref enemynpc.npcEnemy n, int p, bool omni)
		{
			Matrix matrix = Matrix.Identity;
			Vector3 vector = new Vector3(0f, 45f, 0f) + n.dupe[p].enemypos;
			if (!omni)
			{
				matrix = Matrix.CreateLookAt(vector, vector + Vector3.Transform(new Vector3(0f, 0f, -50f), n.dupe[p].skelRot), Vector3.Up);
			}
			if (omni)
			{
				matrix = Matrix.CreateLookAt(vector, this.campos, Vector3.Up);
			}
			bool flag = false;
			this.cursorRay = c.CalculateCursorRay(projection, matrix);
			c.rayDir = (c.rayPos = (c.pickedTriangle[0] = (c.pickedTriangle[1] = (c.pickedTriangle[2] = Vector3.Zero))));
			c.rayDir = this.cursorRay.Direction;
			c.rayPos = this.cursorRay.Position;
			c.closestIntersection = 10000f;
			c.ignorebounds = true;
			c.RayInteresectGeneric(this.cursorRay, this.buildingMatrix, ref c.tunnelvertices3, ref flag);
			if ((enemyDupe.isChasing == p || enemyDupe.isChasing == -1) && Vector3.Distance(this.campos, n.dupe[p].enemypos) > 100f)
			{
				BoundingSphere boundingSphere = default(BoundingSphere);
				boundingSphere.Center = this.campos;
				boundingSphere.Radius = 80f;
				float? num = boundingSphere.Intersects(this.cursorRay);
				if (num != null && num.Value < c.closestIntersection && (this.lightON || num.Value < 300f))
				{
					this.lastPlayerPos = new Vector2(this.campos.X, this.campos.Z);
					if (this.seenCounter <= 0)
					{
						this.seen.Play(this.sc.ev, 0f, 0f);
						this.seenCounter = 120;
					}
					enemyDupe.isChasing = p;
					this.findenemyDestination2(ref this.skull, p, this.lastPlayerPos);
				}
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0025A1C2 File Offset: 0x002583C2
		public void Draw(bool tunnelCheats)
		{
			if (this.skull.index1 > 0)
			{
				this.DrawSkull1(ref this.skull, this.skull.index1);
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0025A1EC File Offset: 0x002583EC
		private void DrawSkull1(ref enemynpc.npcEnemy n, int cc)
		{
			ModelMeshPart modelMeshPart = n.model1.Meshes[0].MeshParts[0];
			n.buffer1.SetData<enemynpc.skinstream>(n.display1, 0, cc, SetDataOptions.Discard);
			int num = this.techniWorld;
			if (num == 2)
			{
				num = 1;
			}
			float num2 = 0.2f;
			if (this.tunnelDebug)
			{
				num2 = 1f;
			}
			n.eff.CurrentTechnique = n.eff.Techniques[num];
			n.eff.Parameters["darkness"].SetValue(num2);
			n.eff.Parameters["gDiffuse"].SetValue(this.ttWorld);
			n.eff.Parameters["depth"].SetValue(this.flashlightDepth);
			n.eff.Parameters["View"].SetValue(this.view);
			n.eff.Parameters["Projection"].SetValue(this.proj);
			n.eff.Parameters["projectorView"].SetValue(this.view2World);
			n.eff.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(n.buffer1, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0025A3D0 File Offset: 0x002585D0
		private void DrawModel(Model model, Matrix world, Vector3 cc)
		{
			foreach (Effect effect in model.Meshes[0].Effects)
			{
				BasicEffect basicEffect = (BasicEffect)effect;
				basicEffect.World = world;
				basicEffect.View = this.view;
				basicEffect.Projection = this.proj;
				basicEffect.LightingEnabled = true;
				basicEffect.EmissiveColor = cc;
				basicEffect.DirectionalLight0.Enabled = true;
				basicEffect.DirectionalLight0.Direction = new Vector3(0.2f, -0.5f, 0.1f);
				basicEffect.AmbientLightColor = cc;
				basicEffect.DirectionalLight0.DiffuseColor = cc;
				basicEffect.PreferPerPixelLighting = false;
			}
			model.Meshes[0].Draw();
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0025A4B8 File Offset: 0x002586B8
		public void GetHeightFast(ref float[,] heights, Vector3 position, ref float height)
		{
			int num = (int)MathHelper.Clamp(position.X / enemynpc.unit, 0f, (float)(enemynpc.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(position.Z / enemynpc.unit, 0f, (float)(enemynpc.bitmap - 2));
			float num3 = position.X % enemynpc.unit / enemynpc.unit;
			float num4 = position.Z % enemynpc.unit / enemynpc.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
		}

		// Token: 0x0400278B RID: 10123
		public Vector3 bigcolor = Vector3.One;

		// Token: 0x0400278C RID: 10124
		public Vector3 enemypos = Vector3.Zero;

		// Token: 0x0400278D RID: 10125
		public Vector2 destination = Vector2.Zero;

		// Token: 0x0400278E RID: 10126
		public ushort sendTime;

		// Token: 0x0400278F RID: 10127
		public int sendSeed = -1;

		// Token: 0x04002790 RID: 10128
		public int lastTime;

		// Token: 0x04002791 RID: 10129
		public int iter = -1;

		// Token: 0x04002792 RID: 10130
		public int skullindex = -1;

		// Token: 0x04002793 RID: 10131
		public int sendDeath = -1;

		// Token: 0x04002794 RID: 10132
		public int localhitCount;

		// Token: 0x04002795 RID: 10133
		public int remotehitCount;

		// Token: 0x04002796 RID: 10134
		private enemynpc.conductor tempConduct = default(enemynpc.conductor);

		// Token: 0x04002797 RID: 10135
		public enemynpc.npcEnemy skull;

		// Token: 0x04002798 RID: 10136
		private Vector3 bottomCorner = new Vector3(-25f, -33.77f, -36.16f);

		// Token: 0x04002799 RID: 10137
		private Vector3 topCornernew = new Vector3(28.88f, 26.538f, 38.86f);

		// Token: 0x0400279A RID: 10138
		private SoundEffect seen;

		// Token: 0x0400279B RID: 10139
		private SoundEffect seen4;

		// Token: 0x0400279C RID: 10140
		private SoundEffect enemydie;

		// Token: 0x0400279D RID: 10141
		private Effect enemyEffect;

		// Token: 0x0400279E RID: 10142
		private int attackWaite;

		// Token: 0x0400279F RID: 10143
		private Random rr = new Random();

		// Token: 0x040027A0 RID: 10144
		public float[] gunDam = new float[]
		{
			2f, 0f, 1f, 0f, 1f, 0f, 1.2f, 0f, 3f, 0f,
			1.1f, 0f, 1f, 0f, 1f, 0f, 1f, 0f, 1f, 0f,
			1.1f, 0f, 1.3f
		};

		// Token: 0x040027A1 RID: 10145
		public static int bitmap;

		// Token: 0x040027A2 RID: 10146
		public static float unit;

		// Token: 0x040027A3 RID: 10147
		public static float Grid;

		// Token: 0x040027A4 RID: 10148
		public bool tunnelAvoidDamage;

		// Token: 0x040027A5 RID: 10149
		public float headRot;

		// Token: 0x040027A6 RID: 10150
		public Vector3 campos;

		// Token: 0x040027A7 RID: 10151
		public Vector3 camlookpos;

		// Token: 0x040027A8 RID: 10152
		public Vector2 hitVel;

		// Token: 0x040027A9 RID: 10153
		private int timeFrame;

		// Token: 0x040027AA RID: 10154
		private Ray cursorRay;

		// Token: 0x040027AB RID: 10155
		private Vector3 min;

		// Token: 0x040027AC RID: 10156
		private Vector3 max;

		// Token: 0x040027AD RID: 10157
		private float? distCheck;

		// Token: 0x040027AE RID: 10158
		public Matrix cubeMatrix;

		// Token: 0x040027AF RID: 10159
		public Vector3 cubeCenter = Vector3.Zero;

		// Token: 0x040027B0 RID: 10160
		public Vector3 outside;

		// Token: 0x040027B1 RID: 10161
		public float scale;

		// Token: 0x040027B2 RID: 10162
		public Matrix skullView;

		// Token: 0x040027B3 RID: 10163
		public BoundingBox mybox;

		// Token: 0x040027B4 RID: 10164
		public Vector2 lastPlayerPos = Vector2.Zero;

		// Token: 0x040027B5 RID: 10165
		private int seenCounter;

		// Token: 0x040027B6 RID: 10166
		public bool lightON;

		// Token: 0x040027B7 RID: 10167
		public Matrix buildingMatrix;

		// Token: 0x040027B8 RID: 10168
		public int techniWorld;

		// Token: 0x040027B9 RID: 10169
		public bool tunnelDebug;

		// Token: 0x040027BA RID: 10170
		public Texture2D ttWorld;

		// Token: 0x040027BB RID: 10171
		public Matrix view;

		// Token: 0x040027BC RID: 10172
		public Matrix proj;

		// Token: 0x040027BD RID: 10173
		public Matrix view2World;

		// Token: 0x040027BE RID: 10174
		private Model cube;

		// Token: 0x040027BF RID: 10175
		private Matrix enemyProj = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(80f), 1.78f, 1f, 3000f);

		// Token: 0x040027C0 RID: 10176
		public bool bitSpray;

		// Token: 0x040027C1 RID: 10177
		public bool bloodSpray;

		// Token: 0x040027C2 RID: 10178
		public bool decalSpray;

		// Token: 0x040027C3 RID: 10179
		private readonly VertexBufferBinding[] _vertexBufferBindings = new VertexBufferBinding[2];

		// Token: 0x040027C4 RID: 10180
		public float flashlightDepth = 100f;

		// Token: 0x040027C5 RID: 10181
		private int[] handtype = new int[]
		{
			1, 1, 1, 1, 1, 1, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 1, 1, 2, 2,
			2, 2, 2, 2
		};

		// Token: 0x040027C6 RID: 10182
		public int explosiveCount;

		// Token: 0x040027C7 RID: 10183
		private List<int> parts;

		// Token: 0x040027C8 RID: 10184
		private int[] s;

		// Token: 0x040027C9 RID: 10185
		private bool enemySchedulethisFrame;

		// Token: 0x040027CA RID: 10186
		private Vector3 v1;

		// Token: 0x040027CB RID: 10187
		private Vector3 v2;

		// Token: 0x040027CC RID: 10188
		private Vector3 v3;

		// Token: 0x040027CD RID: 10189
		private Vector3 vZero = Vector3.Zero;

		// Token: 0x040027CE RID: 10190
		private Matrix m1;

		// Token: 0x040027CF RID: 10191
		private Matrix m2;

		// Token: 0x040027D0 RID: 10192
		private Matrix m3;

		// Token: 0x040027D1 RID: 10193
		private Matrix m4;

		// Token: 0x040027D2 RID: 10194
		private float f1;

		// Token: 0x040027D3 RID: 10195
		private float f2;

		// Token: 0x040027D4 RID: 10196
		private float f3;

		// Token: 0x040027D5 RID: 10197
		private static VertexDeclaration vd = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 4),
			new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0),
			new VertexElement(68, VertexElementFormat.Single, VertexElementUsage.Fog, 1),
			new VertexElement(72, VertexElementFormat.Single, VertexElementUsage.Fog, 2),
			new VertexElement(76, VertexElementFormat.Single, VertexElementUsage.Fog, 3),
			new VertexElement(80, VertexElementFormat.Single, VertexElementUsage.Fog, 4)
		});

		// Token: 0x040027D6 RID: 10198
		private enemynpc.skinstream tempySkin = default(enemynpc.skinstream);

		// Token: 0x040027D7 RID: 10199
		private enemynpc.skinstream[] tempySkinArray = new enemynpc.skinstream[1];

		// Token: 0x040027D8 RID: 10200
		public Texture2D enemyTexture;

		// Token: 0x040027D9 RID: 10201
		public Texture2D enemyDeadTexture;

		// Token: 0x040027DA RID: 10202
		public Texture2D splat2;

		// Token: 0x040027DB RID: 10203
		public Model enemyProxy;

		// Token: 0x040027DC RID: 10204
		public Effect eff;

		// Token: 0x040027DD RID: 10205
		public float[] enemysphereScale;

		// Token: 0x040027DE RID: 10206
		public Vector3[] uv;

		// Token: 0x040027DF RID: 10207
		public Matrix[] targ;

		// Token: 0x040027E0 RID: 10208
		public int[] bone;

		// Token: 0x040027E1 RID: 10209
		public int hitindex;

		// Token: 0x040027E2 RID: 10210
		public float npcDist;

		// Token: 0x040027E3 RID: 10211
		private ScreenManager sc;

		// Token: 0x040027E4 RID: 10212
		private ContentManager cc;

		// Token: 0x040027E5 RID: 10213
		private ContentManager Content;

		// Token: 0x040027E6 RID: 10214
		private Vector3[] plot3;

		// Token: 0x040027E7 RID: 10215
		private Vector2[] enemyHome;

		// Token: 0x040027E8 RID: 10216
		private Vector2[] enemyInvade;

		// Token: 0x040027E9 RID: 10217
		private Vector2[] enemyGuard;

		// Token: 0x040027EA RID: 10218
		private Vector2[] enemyHome1 = new Vector2[]
		{
			new Vector2(2007.124f, 1922.5f),
			new Vector2(1632.124f, 1922.5f),
			new Vector2(3382.123f, 2047.5f)
		};

		// Token: 0x040027EB RID: 10219
		private Vector2[] enemyInvade1 = new Vector2[]
		{
			new Vector2(2132.124f, 4297.5f),
			new Vector2(2882.124f, 4297.5f)
		};

		// Token: 0x040027EC RID: 10220
		private Vector2[] enemyGuard1 = new Vector2[]
		{
			new Vector2(2757.124f, 1922.5f),
			new Vector2(2132.124f, 2297.5f),
			new Vector2(1382.124f, 3172.5f)
		};

		// Token: 0x040027ED RID: 10221
		private Vector2[] enemyHome2 = new Vector2[]
		{
			new Vector2(3632.123f, 2672.5f),
			new Vector2(382.12402f, 2797.5f)
		};

		// Token: 0x040027EE RID: 10222
		private Vector2[] enemyInvade2 = new Vector2[]
		{
			new Vector2(2882.124f, 4422.5f),
			new Vector2(2632.124f, 3672.5f),
			new Vector2(2132.124f, 4047.5f)
		};

		// Token: 0x040027EF RID: 10223
		private Vector2[] enemyGuard2 = new Vector2[]
		{
			new Vector2(2132.124f, 2672.5f),
			new Vector2(2257.124f, 1922.5f),
			new Vector2(1882.124f, 2672.5f)
		};

		// Token: 0x040027F0 RID: 10224
		private Vector2[] enemyHome3 = new Vector2[]
		{
			new Vector2(4257.123f, 2047.5f),
			new Vector2(2507.124f, 2047.5f),
			new Vector2(4007.123f, 3672.5f)
		};

		// Token: 0x040027F1 RID: 10225
		private Vector2[] enemyInvade3 = new Vector2[]
		{
			new Vector2(2132.124f, 4422.5f),
			new Vector2(1007.124f, 3172.5f),
			new Vector2(2882.124f, 4422.5f)
		};

		// Token: 0x040027F2 RID: 10226
		private Vector2[] enemyGuard3 = new Vector2[]
		{
			new Vector2(3632.123f, 1047.5f),
			new Vector2(2257.124f, 2172.5f),
			new Vector2(3507.123f, 3172.5f)
		};

		// Token: 0x040027F3 RID: 10227
		private Vector2[] enemyHome4 = new Vector2[]
		{
			new Vector2(2132.124f, 3172.5f),
			new Vector2(1507.124f, 1922.5f),
			new Vector2(4007.123f, 3797.5f),
			new Vector2(3757.123f, 2672.5f)
		};

		// Token: 0x040027F4 RID: 10228
		private Vector2[] enemyGuard4 = new Vector2[]
		{
			new Vector2(1257.124f, 3547.5f),
			new Vector2(1632.124f, 1547.5f),
			new Vector2(3382.123f, 1172.5f),
			new Vector2(4382.123f, 1797.5f),
			new Vector2(882.124f, 2422.5f)
		};

		// Token: 0x040027F5 RID: 10229
		private Vector2[] enemyInvade4 = new Vector2[]
		{
			new Vector2(2507.124f, 4547.5f),
			new Vector2(2632.124f, 3547.5f),
			new Vector2(2882.124f, 2422.5f),
			new Vector2(2882.124f, 4422.5f),
			new Vector2(2882.124f, 2797.5f)
		};

		// Token: 0x040027F6 RID: 10230
		private int mazeid = -1;

		// Token: 0x02000110 RID: 272
		public struct conductor
		{
			// Token: 0x040027F7 RID: 10231
			public byte type;

			// Token: 0x040027F8 RID: 10232
			public int id;

			// Token: 0x040027F9 RID: 10233
			public byte action;

			// Token: 0x040027FA RID: 10234
			public byte bodypart;

			// Token: 0x040027FB RID: 10235
			public int frame;

			// Token: 0x040027FC RID: 10236
			public int time;

			// Token: 0x040027FD RID: 10237
			public bool died;

			// Token: 0x040027FE RID: 10238
			public Vector3 veloc;
		}

		// Token: 0x02000111 RID: 273
		public struct npcEnemy
		{
			// Token: 0x040027FF RID: 10239
			public int alive;

			// Token: 0x04002800 RID: 10240
			public int alive2;

			// Token: 0x04002801 RID: 10241
			public Model model1;

			// Token: 0x04002802 RID: 10242
			public enemynpc.skinstream[] display1;

			// Token: 0x04002803 RID: 10243
			public List<int> explodelist;

			// Token: 0x04002804 RID: 10244
			public List<int> shockList;

			// Token: 0x04002805 RID: 10245
			public List<int> shatterList;

			// Token: 0x04002806 RID: 10246
			public List<enemyDupe> dupe;

			// Token: 0x04002807 RID: 10247
			public List<enemynpc.conductor> conductor;

			// Token: 0x04002808 RID: 10248
			public int max;

			// Token: 0x04002809 RID: 10249
			public int index1;

			// Token: 0x0400280A RID: 10250
			public DynamicVertexBuffer buffer1;

			// Token: 0x0400280B RID: 10251
			public SkinningDataX data;

			// Token: 0x0400280C RID: 10252
			public Texture2D bitmap;

			// Token: 0x0400280D RID: 10253
			public Texture2D Texture1;

			// Token: 0x0400280E RID: 10254
			public Effect eff;

			// Token: 0x0400280F RID: 10255
			public Vector3[] uv;

			// Token: 0x04002810 RID: 10256
			public Matrix[] targ;

			// Token: 0x04002811 RID: 10257
			public int[] bone;

			// Token: 0x04002812 RID: 10258
			public int hitindex;

			// Token: 0x04002813 RID: 10259
			public float npcDist;
		}

		// Token: 0x02000112 RID: 274
		public struct skinstream : IVertexType
		{
			// Token: 0x17000035 RID: 53
			// (get) Token: 0x0600098D RID: 2445 RVA: 0x0025A672 File Offset: 0x00258872
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return enemynpc.skinstream.VertexDeclaration;
				}
			}

			// Token: 0x04002814 RID: 10260
			public Matrix Transformation;

			// Token: 0x04002815 RID: 10261
			public float frame1;

			// Token: 0x04002816 RID: 10262
			public float frame2;

			// Token: 0x04002817 RID: 10263
			public float tween;

			// Token: 0x04002818 RID: 10264
			public float blood;

			// Token: 0x04002819 RID: 10265
			public float tint;

			// Token: 0x0400281A RID: 10266
			private static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 4),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0),
				new VertexElement(68, VertexElementFormat.Single, VertexElementUsage.Fog, 1),
				new VertexElement(72, VertexElementFormat.Single, VertexElementUsage.Fog, 2),
				new VertexElement(76, VertexElementFormat.Single, VertexElementUsage.Fog, 3),
				new VertexElement(80, VertexElementFormat.Single, VertexElementUsage.Fog, 4)
			});
		}
	}
}
