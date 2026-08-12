using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000D8 RID: 216
	public class pumpkins
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x001C2EE4 File Offset: 0x001C10E4
		public pumpkins(ScreenManager ss, ContentManager cc)
		{
			this.rr = new Random();
			this.content = cc;
			this.sc = ss;
			this.pump = default(pumpkins.shell);
			this.pump.max = 0;
			this.pump.type = 0;
			this.pump.maxCapacity = 90;
			this.pump.index = 0;
			this.pump.model1 = this.content.Load<Model>("models\\pumpkin");
			this.pump.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, pumpkins.instanceDec, this.pump.maxCapacity, BufferUsage.WriteOnly);
			this.pump.displayList = new pumpkins.instancedObject[this.pump.maxCapacity];
			this.pump.dupe = new pumpDupe[this.pump.maxCapacity];
			for (int i = 0; i < this.pump.maxCapacity; i++)
			{
				this.pump.dupe[i] = new pumpDupe(i);
			}
			this.pump.stream = new pumpkins.instancedObject[this.pump.maxCapacity];
			this.p_stem = default(pumpkins.shell);
			this.p_stem.max = 0;
			this.p_stem.type = 1;
			this.p_stem.maxCapacity = 10;
			this.p_stem.index = 0;
			this.p_stem.model1 = this.content.Load<Model>("models\\p_stem");
			int num = this.p_stem.maxCapacity;
			this.p_stem.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, pumpkins.instanceDec, num, BufferUsage.WriteOnly);
			this.p_stem.displayList = new pumpkins.instancedObject[num];
			this.p_stem.dupe = new pumpDupe[num];
			for (int j = 0; j < num; j++)
			{
				this.p_stem.dupe[j] = new pumpDupe(j);
			}
			this.p_stem.stream = new pumpkins.instancedObject[num];
			this.p_base = default(pumpkins.shell);
			this.p_base.max = 0;
			this.p_base.type = 2;
			this.p_base.maxCapacity = 20;
			this.p_base.index = 0;
			this.p_base.model1 = this.content.Load<Model>("models\\p_base");
			num = this.p_base.maxCapacity;
			this.p_base.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, pumpkins.instanceDec, num, BufferUsage.WriteOnly);
			this.p_base.displayList = new pumpkins.instancedObject[num];
			this.p_base.dupe = new pumpDupe[num];
			for (int k = 0; k < num; k++)
			{
				this.p_base.dupe[k] = new pumpDupe(k);
			}
			this.p_base.stream = new pumpkins.instancedObject[num];
			this.p_rind = default(pumpkins.shell);
			this.p_rind.max = 0;
			this.p_rind.type = 3;
			this.p_rind.maxCapacity = 20;
			this.p_rind.index = 0;
			this.p_rind.model1 = this.content.Load<Model>("models\\p_rind");
			num = this.p_rind.maxCapacity;
			this.p_rind.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, pumpkins.instanceDec, num, BufferUsage.WriteOnly);
			this.p_rind.displayList = new pumpkins.instancedObject[num];
			this.p_rind.dupe = new pumpDupe[num];
			for (int l = 0; l < num; l++)
			{
				this.p_rind.dupe[l] = new pumpDupe(l);
			}
			this.p_rind.stream = new pumpkins.instancedObject[num];
			this.p_chunk = default(pumpkins.shell);
			this.p_chunk.max = 0;
			this.p_chunk.type = 4;
			this.p_chunk.maxCapacity = 20;
			this.p_chunk.index = 0;
			this.p_chunk.model1 = this.content.Load<Model>("models\\p_chunk");
			num = this.p_chunk.maxCapacity;
			this.p_chunk.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, pumpkins.instanceDec, num, BufferUsage.WriteOnly);
			this.p_chunk.displayList = new pumpkins.instancedObject[num];
			this.p_chunk.dupe = new pumpDupe[num];
			for (int m = 0; m < num; m++)
			{
				this.p_chunk.dupe[m] = new pumpDupe(m);
			}
			this.p_chunk.stream = new pumpkins.instancedObject[num];
			this.p_bitty = default(pumpkins.shell);
			this.p_bitty.max = 0;
			this.p_bitty.type = 4;
			this.p_bitty.maxCapacity = 20;
			this.p_bitty.index = 0;
			this.p_bitty.model1 = this.content.Load<Model>("models\\p_bitty");
			num = this.p_bitty.maxCapacity;
			this.p_bitty.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, pumpkins.instanceDec, num, BufferUsage.WriteOnly);
			this.p_bitty.displayList = new pumpkins.instancedObject[num];
			this.p_bitty.dupe = new pumpDupe[num];
			for (int n = 0; n < num; n++)
			{
				this.p_bitty.dupe[n] = new pumpDupe(n);
			}
			this.p_bitty.stream = new pumpkins.instancedObject[num];
			this.sound1 = this.content.Load<SoundEffect>("audio\\squash1");
			this.sound2 = this.content.Load<SoundEffect>("audio\\squash2");
			this.sound3 = this.content.Load<SoundEffect>("audio\\squash3");
			this.sound4 = this.content.Load<SoundEffect>("audio\\squash4");
			this.pop = this.content.Load<SoundEffect>("audio\\pop");
			this.pop2 = this.content.Load<SoundEffect>("audio\\pop2");
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x001C3504 File Offset: 0x001C1704
		public void initPumpkins(int seed, ref float[,] heights)
		{
			this.p_bitty.max = 0;
			this.p_bitty.index = 0;
			this.p_chunk.max = 0;
			this.p_chunk.index = 0;
			this.p_base.max = 0;
			this.p_stem.index = 0;
			this.p_rind.max = 0;
			this.p_rind.index = 0;
			this.p_stem.max = 0;
			this.p_stem.index = 0;
			this.pump.max = 0;
			this.pump.index = 0;
			this.pump.type = 0;
			this.rx = new Random(seed);
			int num = this.rx.Next(9000, 22000);
			for (int i = 0; i < this.pump.maxCapacity; i++)
			{
				bool flag = true;
				if ((float)i > (float)this.pump.maxCapacity * 0.15f)
				{
					flag = false;
				}
				this.dropPumpkin(ref this.pump, num + i * 5, ref heights, flag);
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x001C3614 File Offset: 0x001C1814
		private void dropPumpkin(ref pumpkins.shell sh, int seed, ref float[,] heights, bool inner)
		{
			float num = (float)this.rx.Next(35, 80) / 100f;
			if (this.rx.Next(1, 100) < 6)
			{
				num = 1.1f;
			}
			Vector3 vector = new Vector3(num, num * (float)this.rx.Next(70, 110) / 100f, num);
			Vector3 vector2 = new Vector3((float)this.rx.Next(500, 5500), 0f, (float)this.rx.Next(500, 5500));
			sh.dupe[sh.index].drop = 0;
			int num2 = this.rx.Next(0, 220);
			if (num2 < 100)
			{
				if (num2 >= 70 && num2 < 100)
				{
					sh.dupe[sh.index].drop = 1;
				}
				else if (num2 >= 45 && num2 < 70)
				{
					sh.dupe[sh.index].drop = 3;
				}
				else if (num2 >= 34 && num2 < 45)
				{
					sh.dupe[sh.index].drop = 2;
				}
				else if (num2 >= 0 && num2 < 34)
				{
					sh.dupe[sh.index].drop = 4;
				}
			}
			if (inner)
			{
				vector2 = new Vector3((float)this.rx.Next(-1000, 1000), 0f, (float)this.rx.Next(-1000, 1000)) + new Vector3(3000f, 0f, 3000f);
			}
			else
			{
				int num3 = this.rx.Next(1, 5);
				if (num3 == 1)
				{
					vector2 = new Vector3((float)this.rx.Next(-2300, 1690), 0f, (float)this.rx.Next(-2700, -1560)) + new Vector3(3000f, 0f, 3000f);
				}
				if (num3 == 2)
				{
					vector2 = new Vector3((float)this.rx.Next(1805, 2700), 0f, (float)this.rx.Next(-1095, 2300)) + new Vector3(3000f, 0f, 3000f);
				}
				if (num3 == 3)
				{
					vector2 = new Vector3((float)this.rx.Next(-2240, 2300), 0f, (float)this.rx.Next(1970, 2700)) + new Vector3(3000f, 0f, 3000f);
				}
				if (num3 == 4)
				{
					vector2 = new Vector3((float)this.rx.Next(-2700, -1844), 0f, (float)this.rx.Next(-2376, 1630)) + new Vector3(3000f, 0f, 3000f);
				}
			}
			float num4;
			Vector3 vector3;
			pumpkins.GetHeightFast(ref heights, ref vector2, out num4, out vector3);
			vector2.Y = num4 + 34f * vector.Y;
			Matrix matrix = Matrix.CreateRotationY((float)this.rx.Next(0, 8000) / 100f);
			Matrix matrix2 = matrix;
			matrix2.Up = vector3;
			matrix2.Right = Vector3.Cross(matrix2.Forward, matrix2.Up);
			matrix2.Right = Vector3.Normalize(matrix2.Right);
			matrix2.Forward = Vector3.Cross(matrix2.Up, matrix2.Right);
			matrix2.Forward = Vector3.Normalize(matrix2.Forward);
			Vector3 vector4;
			Quaternion quaternion;
			Vector3 vector5;
			matrix2.Decompose(out vector4, out quaternion, out vector5);
			Quaternion.Normalize(quaternion);
			matrix = Matrix.CreateFromQuaternion(quaternion);
			sh.dupe[sh.index].init(vector, vector2, matrix, seed);
			sh.dupe[sh.index].tint = (float)this.rx.Next(60, 100);
			sh.stream[sh.index].Trans = sh.dupe[sh.index].transform;
			sh.stream[sh.index].tint = sh.dupe[sh.index].tint;
			sh.index++;
			if (sh.index > sh.maxCapacity - 1)
			{
				sh.index = 0;
			}
			sh.max++;
			if (sh.max > sh.maxCapacity - 1)
			{
				sh.max = sh.maxCapacity;
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x001C3AAC File Offset: 0x001C1CAC
		public void dropParts(ref pumpkins.shell sh, int i, Matrix m, int seed, int size)
		{
			Random random = new Random(seed);
			float num = (float)random.Next(30, 140) / 100f;
			float num2 = 80f;
			float num3 = 290f;
			float tint = this.pump.dupe[i].tint;
			int num4 = 110;
			int num5 = 330;
			float num6 = 60f;
			int num7 = -240;
			int num8 = -90;
			float num9 = 1000f;
			if (this.sc.revengeDay > 0)
			{
				num = (float)random.Next(50, 350) / 100f;
				num4 = 100;
				num5 = 500;
			}
			int num10 = 1;
			Matrix matrix = Matrix.CreateScale(this.pump.dupe[i].scale) * this.pump.dupe[i].rot * Matrix.CreateTranslation(this.pump.dupe[i].mypos);
			Matrix matrix2 = m * matrix;
			sh.dupe[sh.index].tint = this.pump.dupe[i].tint;
			Vector3 vector = new Vector3((float)random.Next(-300, 300) / 120f, (float)random.Next(num4, num5) / num6, (float)random.Next(-300, 300) / 120f);
			Vector3 vector2 = Vector3.Transform(new Vector3(0f, 0f, 0f), m);
			Vector3 vector3 = (float)random.Next(100, 300) / 30f * Vector3.Normalize(vector2);
			float num11 = (float)random.Next(300, 700) / 1000f;
			float num12 = (float)random.Next(num7, num8) / num9;
			sh.dupe[sh.index].init2(size, num10, num11, this.pump.dupe[i].scale, 0.3f, matrix2, new Vector3(vector3.X + vector.X, vector.Y, vector3.Z + vector.Z) * num, true, num12, 20, num2, num3, false, false, false, seed);
			sh.stream[sh.index].Trans = sh.dupe[sh.index].transform;
			sh.stream[sh.index].tint = sh.dupe[sh.index].tint;
			sh.index++;
			if (sh.index > sh.maxCapacity - 1)
			{
				sh.index = 0;
			}
			sh.max++;
			if (sh.max > sh.maxCapacity - 1)
			{
				sh.max = sh.maxCapacity;
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x001C3D80 File Offset: 0x001C1F80
		public void updatePumpkin(ref pumpkins.shell sh, float range, ref float[,] heights, Vector3 campos, Vector3 camlookpos, ref localPlayer myPlayer, ref Cursor genCursor, ref Vector2 hitVel, float headRot)
		{
			this.lastpumpkin = -1;
			Vector3 vector = Vector3.Normalize(camlookpos - campos);
			Vector3 vector2 = -vector * 100f + campos;
			range *= range;
			this.pumpkinIndex = -1;
			bool flag = false;
			sh.tempindex = 0;
			this.alive = 0;
			for (int i = 0; i < sh.max; i++)
			{
				if (sh.dupe[i].move != -1)
				{
					bool flag2 = false;
					this.alive++;
					float num;
					Vector3.DistanceSquared(ref myPlayer.displayState.npcPosition, ref sh.dupe[i].mypos, out num);
					if (num < range)
					{
						Vector3 vector3 = new Vector3(sh.dupe[i].mypos.X, sh.dupe[i].mypos.Y, sh.dupe[i].mypos.Z) - vector2;
						Vector3 vector4;
						Vector3.Normalize(ref vector3, out vector4);
						float num2 = 0f;
						Vector3.Dot(ref vector4, ref vector, out num2);
						if (num2 > this.sc.myfov)
						{
							if (sh.dupe[i].move > 0)
							{
								sh.dupe[i].updatePumpkin();
							}
							sh.stream[i].Trans = sh.dupe[i].transform;
							sh.stream[i].tint = sh.dupe[i].tint;
							sh.displayList[sh.tempindex] = sh.stream[i];
							sh.tempindex++;
							flag2 = true;
							if (sh.dupe[i].pop)
							{
								if (this.sc.introCamera <= 0f)
								{
									float num3 = MathHelper.Clamp(1f - ((float)Math.Sqrt((double)num) - 200f) / 4000f, 0.2f, 0.8f);
									this.pop.Play(this.sc.ev * num3, 0f, 0f);
								}
								sh.dupe[i].pop = false;
							}
							if (sh.dupe[i].kickable && !flag)
							{
								if (myPlayer.gunFired && num2 > 0.95f)
								{
									float num4 = 33f;
									if (genCursor.hitSphere(myPlayer.gunpos, myPlayer.gunlook, sh.dupe[i].mypos, sh.dupe[i].scale.Y * num4) != null)
									{
										float num5 = MathHelper.Clamp(1f - ((float)Math.Sqrt((double)num) - 200f) / 4000f, 0.2f, 1f);
										flag = true;
										this.explode(i, ref myPlayer, ref heights, num5);
										if (this.pump.dupe[i].drop > 0)
										{
											this.pumpkinIndex = i;
										}
										this.sc.stats_pumpkins++;
									}
								}
								if (!myPlayer.isDown && !myPlayer.gunFired && num < 4900f * sh.dupe[i].scale.Y)
								{
									flag = true;
									if (sh.dupe[i].scale.X < 0.65f)
									{
										this.explode(i, ref myPlayer, ref heights, 1f);
										if (this.pump.dupe[i].drop > 0)
										{
											this.pumpkinIndex = i;
										}
										this.sc.stats_pumpkins++;
									}
									else
									{
										this.sc.sproing[this.rr.Next(0, 2)].Play(this.sc.ev * (float)this.rr.Next(50, 90) / 100f, 0f, 0f);
										Vector2 vector5 = new Vector2(sh.dupe[i].mypos.X, sh.dupe[i].mypos.Z) - new Vector2(myPlayer.displayState.npcPosition.X, myPlayer.displayState.npcPosition.Z);
										if (vector5.Length() > 0f)
										{
											hitVel = -Vector2.Normalize(vector5) * 2.5f;
											hitVel = Vector2.Transform(new Vector2(-hitVel.X, hitVel.Y), Matrix.CreateRotationZ(-headRot));
										}
									}
								}
							}
						}
					}
					if (sh.dupe[i].move > 0 && !flag2)
					{
						sh.dupe[i].updatePumpkin();
					}
					this.lastpumpkin = i;
					sh.dupe[i].pop = false;
				}
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x001C428C File Offset: 0x001C248C
		public void updatePumpkinParts(ref pumpkins.shell sh, float range, ref float[,] heights, Vector3 campos, Vector3 camlookpos, ref localPlayer myPlayer, ref Cursor genCursor)
		{
			Vector3 vector = Vector3.Normalize(camlookpos - campos);
			Vector3 vector2 = -vector * 100f + campos;
			range *= range;
			sh.tempindex = 0;
			int num = 0;
			for (int i = 0; i < sh.max; i++)
			{
				if (sh.dupe[i].move != -1)
				{
					num++;
					bool flag = false;
					float num2;
					Vector3.DistanceSquared(ref myPlayer.displayState.npcPosition, ref sh.dupe[i].mypos, out num2);
					if (num2 < range)
					{
						Vector3 vector3 = new Vector3(sh.dupe[i].mypos.X, sh.dupe[i].mypos.Y, sh.dupe[i].mypos.Z) - vector2;
						Vector3 vector4;
						Vector3.Normalize(ref vector3, out vector4);
						float num3 = 0f;
						Vector3.Dot(ref vector4, ref vector, out num3);
						if (num3 > this.sc.myfov)
						{
							if (sh.dupe[i].move > 0)
							{
								sh.dupe[i].Update(ref heights);
							}
							sh.stream[i].Trans = sh.dupe[i].transform;
							sh.stream[i].tint = sh.dupe[i].tint;
							sh.displayList[sh.tempindex] = sh.stream[i];
							sh.tempindex++;
							flag = true;
						}
					}
					if (sh.dupe[i].move > 0 && !flag)
					{
						sh.dupe[i].Update(ref heights);
					}
				}
			}
			if (num == 0)
			{
				sh.max = 0;
				sh.index = 0;
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x001C4470 File Offset: 0x001C2670
		public void explode(int i, ref localPlayer myPlayer, ref float[,] heights, float vol)
		{
			this.pump.dupe[i].move = -1;
			myPlayer.pumpkinID = (byte)i;
			if (this.pump.dupe[i].drop > 0)
			{
				this.pump.dupe[i].dropnow = this.pump.dupe[i].drop;
				this.pop2.Play(this.sc.ev, (float)this.rr.Next(-15, 15) / 100f, 0f);
			}
			int num = this.rr.Next(0, 4);
			if (num == 0)
			{
				this.sound1.Play(this.sc.ev * vol, (float)this.rr.Next(-50, 0) / 100f, 0f);
			}
			if (num == 1)
			{
				this.sound2.Play(this.sc.ev * vol, (float)this.rr.Next(-50, 0) / 100f, 0f);
			}
			if (num == 2)
			{
				this.sound3.Play(this.sc.ev * vol, (float)this.rr.Next(-50, 0) / 100f, 0f);
			}
			if (num == 3)
			{
				this.sound4.Play(this.sc.ev * vol, (float)this.rr.Next(-50, 0) / 100f, 0f);
			}
			Matrix matrix = Matrix.CreateRotationX(MathHelper.ToRadians(7.26f)) * Matrix.CreateTranslation(1.63f, 40.52f, 2.89f);
			this.dropParts(ref this.p_stem, i, matrix, this.pump.dupe[i].seed, 5);
			matrix = Matrix.CreateRotationY(MathHelper.ToRadians(-5.13f)) * Matrix.CreateTranslation(-15.9f, -1.29f, -14.283f);
			this.dropParts(ref this.p_rind, i, matrix, this.pump.dupe[i].seed + 1, 18);
			matrix = Matrix.CreateRotationY(MathHelper.ToRadians(-20.3f)) * Matrix.CreateTranslation(23.55f, 0.155f, 5.59f);
			this.dropParts(ref this.p_base, i, matrix, this.pump.dupe[i].seed + 2, 18);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(0f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-46.59f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(181f)) * Matrix.CreateTranslation(-19.07f, -0.92f, 15.73f);
			this.dropParts(ref this.p_base, i, matrix, this.pump.dupe[i].seed + 3, 18);
			matrix = Matrix.CreateTranslation(0.76f, 17.35f, 29f);
			this.dropParts(ref this.p_chunk, i, matrix, this.pump.dupe[i].seed + 4, 10);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-166f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-41f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(9f)) * Matrix.CreateTranslation(21.21f, -11.5f, -26.53f);
			this.dropParts(ref this.p_chunk, i, matrix, this.pump.dupe[i].seed + 5, 10);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-165.9f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-3.14f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(186.15f)) * Matrix.CreateTranslation(-2f, 14.7f, -31.3f);
			this.dropParts(ref this.p_chunk, i, matrix, this.pump.dupe[i].seed + 6, 10);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-14.36f)) * Matrix.CreateRotationY(MathHelper.ToRadians(9.5f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(5.5f)) * Matrix.CreateTranslation(-3.26f, -11.96f, -33.03f);
			this.dropParts(ref this.p_bitty, i, matrix, this.pump.dupe[i].seed + 7, 5);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-171.5f)) * Matrix.CreateRotationY(MathHelper.ToRadians(20f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(7.84f)) * Matrix.CreateTranslation(11.7f, -8.2f, 32.9f);
			this.dropParts(ref this.p_bitty, i, matrix, this.pump.dupe[i].seed + 8, 5);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(69f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-42.3f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(3.6f)) * Matrix.CreateTranslation(12.43f, 27f, -11.72f);
			this.dropParts(ref this.p_bitty, i, matrix, this.pump.dupe[i].seed + 9, 5);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x001C49CC File Offset: 0x001C2BCC
		public void draw(Matrix view, Matrix proj)
		{
			this.view = view;
			this.proj = proj;
			this.DrawInstance(ref this.pump, "fastShader2", 1f);
			this.DrawInstance(ref this.p_stem, "fastShader2", 1f);
			this.DrawInstance(ref this.p_base, "fastShader2", 1f);
			this.DrawInstance(ref this.p_rind, "fastShader2", 1f);
			this.DrawInstance(ref this.p_chunk, "fastShader2", 1f);
			this.DrawInstance(ref this.p_bitty, "fastShader2", 1f);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x001C4A6C File Offset: 0x001C2C6C
		private void DrawInstance(ref pumpkins.shell shell, string tech, float light)
		{
			int tempindex = shell.tempindex;
			if (tempindex < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = shell.model1.Meshes[0].MeshParts[0];
			shell.buffer.SetData<pumpkins.instancedObject>(shell.displayList, 0, tempindex, SetDataOptions.Discard);
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques[tech];
			Vector3 vector = Vector3.Normalize(this.sc.moontype);
			vector.Y = -0.8f;
			effect.Parameters["LightDirection2"].SetValue(vector);
			effect.Parameters["diff2"].SetValue(new Vector3(0.8f, 0.8f, 0.8f));
			effect.Parameters["amb2"].SetValue(new Vector3(0.2f, 0.2f, 0.2f));
			effect.Parameters["View"].SetValue(this.view);
			effect.Parameters["Projection"].SetValue(this.proj);
			effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(shell.buffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, tempindex);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x001C4C34 File Offset: 0x001C2E34
		private static void GetHeightFast(ref float[,] heights, ref Vector3 pos, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(pos.X / pumpkins.unit, 0f, (float)(pumpkins.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(pos.Z / pumpkins.unit, 0f, (float)(pumpkins.bitmap - 2));
			float num3 = pos.X % pumpkins.unit / pumpkins.unit;
			float num4 = pos.Z % pumpkins.unit / pumpkins.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x04001F2D RID: 7981
		private ScreenManager sc;

		// Token: 0x04001F2E RID: 7982
		private ContentManager content;

		// Token: 0x04001F2F RID: 7983
		public static int bitmap;

		// Token: 0x04001F30 RID: 7984
		public static float unit;

		// Token: 0x04001F31 RID: 7985
		public int pumpkinIndex = -1;

		// Token: 0x04001F32 RID: 7986
		public int lastpumpkin = -1;

		// Token: 0x04001F33 RID: 7987
		private readonly VertexBufferBinding[] _vertexBufferBindings = new VertexBufferBinding[2];

		// Token: 0x04001F34 RID: 7988
		private static VertexDeclaration instanceDec = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
		});

		// Token: 0x04001F35 RID: 7989
		private pumpkins.instancedObject[] tempInstance = new pumpkins.instancedObject[1];

		// Token: 0x04001F36 RID: 7990
		public pumpkins.shell pump;

		// Token: 0x04001F37 RID: 7991
		public pumpkins.shell p_stem;

		// Token: 0x04001F38 RID: 7992
		public pumpkins.shell p_base;

		// Token: 0x04001F39 RID: 7993
		public pumpkins.shell p_rind;

		// Token: 0x04001F3A RID: 7994
		public pumpkins.shell p_chunk;

		// Token: 0x04001F3B RID: 7995
		public pumpkins.shell p_bitty;

		// Token: 0x04001F3C RID: 7996
		private SoundEffect sound1;

		// Token: 0x04001F3D RID: 7997
		private SoundEffect sound2;

		// Token: 0x04001F3E RID: 7998
		private SoundEffect sound3;

		// Token: 0x04001F3F RID: 7999
		private SoundEffect sound4;

		// Token: 0x04001F40 RID: 8000
		private SoundEffect pop;

		// Token: 0x04001F41 RID: 8001
		private SoundEffect pop2;

		// Token: 0x04001F42 RID: 8002
		public int alive;

		// Token: 0x04001F43 RID: 8003
		private Matrix view;

		// Token: 0x04001F44 RID: 8004
		private Matrix proj;

		// Token: 0x04001F45 RID: 8005
		private Random rr;

		// Token: 0x04001F46 RID: 8006
		private Random rx;

		// Token: 0x020000D9 RID: 217
		public struct instancedObject : IVertexType
		{
			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000792 RID: 1938 RVA: 0x001C4DCD File Offset: 0x001C2FCD
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return pumpkins.instancedObject.InstanceVertexDeclaration;
				}
			}

			// Token: 0x04001F47 RID: 8007
			public Matrix Trans;

			// Token: 0x04001F48 RID: 8008
			public float tint;

			// Token: 0x04001F49 RID: 8009
			private static readonly VertexDeclaration InstanceVertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
			});
		}

		// Token: 0x020000DA RID: 218
		public struct shell
		{
			// Token: 0x04001F4A RID: 8010
			public int type;

			// Token: 0x04001F4B RID: 8011
			public int drop;

			// Token: 0x04001F4C RID: 8012
			public int max;

			// Token: 0x04001F4D RID: 8013
			public int tempindex;

			// Token: 0x04001F4E RID: 8014
			public int index;

			// Token: 0x04001F4F RID: 8015
			public int maxCapacity;

			// Token: 0x04001F50 RID: 8016
			public pumpkins.instancedObject[] stream;

			// Token: 0x04001F51 RID: 8017
			public DynamicVertexBuffer buffer;

			// Token: 0x04001F52 RID: 8018
			public pumpkins.instancedObject[] displayList;

			// Token: 0x04001F53 RID: 8019
			public pumpDupe[] dupe;

			// Token: 0x04001F54 RID: 8020
			public Model model1;
		}
	}
}
