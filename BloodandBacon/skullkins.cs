using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200013B RID: 315
	public class skullkins
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x002E4758 File Offset: 0x002E2958
		public skullkins(ScreenManager ss, ContentManager cc)
		{
			this.rz = new Random();
			this.content = cc;
			this.sc = ss;
			this.pump = default(skullkins.shell);
			this.pump.max = 0;
			this.pump.type = 0;
			this.pump.maxCapacity = 290;
			this.pump.index = 0;
			this.pump.model1 = this.content.Load<Model>("models\\skull1");
			this.pump.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, skullkins.instanceDec, this.pump.maxCapacity, BufferUsage.WriteOnly);
			this.pump.displayList = new skullkins.instancedObject[this.pump.maxCapacity];
			this.pump.dupe = new skullDupe[this.pump.maxCapacity];
			for (int i = 0; i < this.pump.maxCapacity; i++)
			{
				this.pump.dupe[i] = new skullDupe(i);
			}
			this.pump.stream = new skullkins.instancedObject[this.pump.maxCapacity];
			this.p_stem = default(skullkins.shell);
			this.p_stem.max = 0;
			this.p_stem.type = 1;
			this.p_stem.maxCapacity = 30;
			this.p_stem.index = 0;
			this.p_stem.model1 = this.content.Load<Model>("models\\s_stem");
			int num = this.p_stem.maxCapacity;
			this.p_stem.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, skullkins.instanceDec, num, BufferUsage.WriteOnly);
			this.p_stem.displayList = new skullkins.instancedObject[num];
			this.p_stem.dupe = new skullDupe[num];
			for (int j = 0; j < num; j++)
			{
				this.p_stem.dupe[j] = new skullDupe(j);
			}
			this.p_stem.stream = new skullkins.instancedObject[num];
			this.p_base = default(skullkins.shell);
			this.p_base.max = 0;
			this.p_base.type = 2;
			this.p_base.maxCapacity = 40;
			this.p_base.index = 0;
			this.p_base.model1 = this.content.Load<Model>("models\\s_base");
			num = this.p_base.maxCapacity;
			this.p_base.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, skullkins.instanceDec, num, BufferUsage.WriteOnly);
			this.p_base.displayList = new skullkins.instancedObject[num];
			this.p_base.dupe = new skullDupe[num];
			for (int k = 0; k < num; k++)
			{
				this.p_base.dupe[k] = new skullDupe(k);
			}
			this.p_base.stream = new skullkins.instancedObject[num];
			this.p_rind = default(skullkins.shell);
			this.p_rind.max = 0;
			this.p_rind.type = 3;
			this.p_rind.maxCapacity = 40;
			this.p_rind.index = 0;
			this.p_rind.model1 = this.content.Load<Model>("models\\s_rind");
			num = this.p_rind.maxCapacity;
			this.p_rind.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, skullkins.instanceDec, num, BufferUsage.WriteOnly);
			this.p_rind.displayList = new skullkins.instancedObject[num];
			this.p_rind.dupe = new skullDupe[num];
			for (int l = 0; l < num; l++)
			{
				this.p_rind.dupe[l] = new skullDupe(l);
			}
			this.p_rind.stream = new skullkins.instancedObject[num];
			this.p_chunk = default(skullkins.shell);
			this.p_chunk.max = 0;
			this.p_chunk.type = 4;
			this.p_chunk.maxCapacity = 40;
			this.p_chunk.index = 0;
			this.p_chunk.model1 = this.content.Load<Model>("models\\s_chunk");
			num = this.p_chunk.maxCapacity;
			this.p_chunk.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, skullkins.instanceDec, num, BufferUsage.WriteOnly);
			this.p_chunk.displayList = new skullkins.instancedObject[num];
			this.p_chunk.dupe = new skullDupe[num];
			for (int m = 0; m < num; m++)
			{
				this.p_chunk.dupe[m] = new skullDupe(m);
			}
			this.p_chunk.stream = new skullkins.instancedObject[num];
			this.p_bitty = default(skullkins.shell);
			this.p_bitty.max = 0;
			this.p_bitty.type = 4;
			this.p_bitty.maxCapacity = 40;
			this.p_bitty.index = 0;
			this.p_bitty.model1 = this.content.Load<Model>("models\\s_bitty");
			num = this.p_bitty.maxCapacity;
			this.p_bitty.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, skullkins.instanceDec, num, BufferUsage.WriteOnly);
			this.p_bitty.displayList = new skullkins.instancedObject[num];
			this.p_bitty.dupe = new skullDupe[num];
			for (int n = 0; n < num; n++)
			{
				this.p_bitty.dupe[n] = new skullDupe(n);
			}
			this.p_bitty.stream = new skullkins.instancedObject[num];
			this.sound1 = this.content.Load<SoundEffect>("audio\\squash1");
			this.sound2 = this.content.Load<SoundEffect>("audio\\squash2");
			this.sound3 = this.content.Load<SoundEffect>("audio\\squash3");
			this.sound4 = this.content.Load<SoundEffect>("audio\\squash4");
			this.pop = this.content.Load<SoundEffect>("audio\\pop");
			this.pop2 = this.content.Load<SoundEffect>("audio\\pop2");
			this.ss1 = this.content.Load<SoundEffect>("audio\\bonehit");
			this.ss2 = this.content.Load<SoundEffect>("audio\\bonehit2");
			this.skulltexture = this.content.Load<Texture2D>("texture\\skull2");
			this.effect = this.content.Load<Effect>("effects\\instancerDeep2");
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x002E4DD1 File Offset: 0x002E2FD1
		public void emitPumpkins(int seeder, Vector3 pos, Matrix rotter, ref float[,] heights, bool sendpos, bool placeit, float floor)
		{
			this.pump.type = 0;
			this.dropPumpkin(seeder, ref this.pump, pos, rotter, ref heights, placeit, floor);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x002E4DF4 File Offset: 0x002E2FF4
		private void dropPumpkin(int seeder, ref skullkins.shell sh, Vector3 pos, Matrix rotter, ref float[,] heights, bool placeit, float floor)
		{
			sh.dupe[sh.index].rr = new Random(seeder);
			float num = (float)sh.dupe[sh.index].rr.Next(9, 23) / 100f;
			Vector3 vector = new Vector3(num, num, num);
			Vector3 vector2 = pos;
			sh.dupe[sh.index].drop = 0;
			Vector3 vector4;
			int num3;
			if (placeit)
			{
				vector2 = pos;
				float num2;
				Vector3 vector3;
				skullkins.GetHeightFast(ref heights, ref vector2, out num2, out vector3);
				vector4 = Vector3.Transform(new Vector3((float)sh.dupe[sh.index].rr.Next(-20, 20) / 100f, (float)sh.dupe[sh.index].rr.Next(-20, 20) / 100f, -0.5f), rotter);
				num3 = 3;
			}
			else
			{
				num3 = 3;
				vector2 = pos;
				float num2;
				Vector3 vector3;
				skullkins.GetHeightFast(ref heights, ref vector2, out num2, out vector3);
				vector4 = Vector3.Transform(new Vector3((float)sh.dupe[sh.index].rr.Next(-120, 120) / 100f, (float)sh.dupe[sh.index].rr.Next(-120, 120) / 100f, (float)(-(float)sh.dupe[sh.index].rr.Next(8, 25))), rotter);
			}
			Matrix matrix = Matrix.CreateRotationX((float)sh.dupe[sh.index].rr.Next(0, 8000) / 100f) * Matrix.CreateRotationY((float)sh.dupe[sh.index].rr.Next(0, 8000) / 100f);
			int num4 = sh.dupe[sh.index].rr.Next(0, 11);
			sh.dupe[sh.index].tint = (float)sh.dupe[sh.index].rr.Next(0, 11);
			int num5 = 4;
			if (this.sc.df == 0)
			{
				num5 = 12;
			}
			if (sh.dupe[sh.index].rr.Next(1, 1000) < num5)
			{
				num4 = 11;
				num = (float)sh.dupe[sh.index].rr.Next(16, 22) / 100f;
				vector = new Vector3(num, num, num);
			}
			if (placeit)
			{
				num4 = 11;
				num = (float)sh.dupe[sh.index].rr.Next(16, 22) / 100f;
				vector = new Vector3(num, num, num);
			}
			sh.dupe[sh.index].init(seeder, vector, vector2, matrix, vector4, num3, floor, num4);
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

		// Token: 0x06000B4C RID: 2892 RVA: 0x002E514C File Offset: 0x002E334C
		public void dropParts(ref skullkins.shell sh, int i, Matrix m, int size)
		{
			float num = (float)this.rz.Next(30, 140) / 100f;
			float num2 = 120f;
			float num3 = 220f;
			float tint = this.pump.dupe[i].tint;
			int num4 = 110;
			int num5 = 350;
			float num6 = 60f;
			int num7 = -200;
			int num8 = -130;
			float num9 = 1000f;
			if (this.sc.revengeDay > 0)
			{
				num = (float)this.rz.Next(50, 350) / 100f;
				num4 = 100;
				num5 = 500;
			}
			int num10 = 1;
			Matrix matrix = Matrix.CreateScale(this.pump.dupe[i].scale) * this.pump.dupe[i].rot * Matrix.CreateTranslation(this.pump.dupe[i].mypos);
			Matrix matrix2 = m * matrix;
			sh.dupe[sh.index].tint = this.pump.dupe[i].tint;
			Vector3 vector = new Vector3((float)this.rz.Next(-300, 300) / 120f, (float)this.rz.Next(num4, num5) / num6, (float)this.rz.Next(-300, 300) / 120f);
			Vector3 vector2 = Vector3.Transform(new Vector3(0f, 0f, 0f), m);
			Vector3 vector3 = (float)this.rz.Next(100, 200) / 30f * Vector3.Normalize(vector2);
			float num11 = (float)this.rz.Next(300, 700) / 1000f;
			float num12 = (float)this.rz.Next(num7, num8) / num9;
			sh.dupe[sh.index].init2(size, num10, num11, this.pump.dupe[i].scale, 0.3f, matrix2, new Vector3(vector3.X + vector.X, vector.Y, vector3.Z + vector.Z) * num, true, num12, 20, num2, num3, false, false, false);
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

		// Token: 0x06000B4D RID: 2893 RVA: 0x002E543C File Offset: 0x002E363C
		public void updateSkullkin(ref skullkins.shell sh, float range, ref float[,] heights, Vector3 campos, Vector3 camlookpos, ref localPlayer myPlayer, ref Cursor genCursor, ref Vector2 hitVel, float headRot, Vector3 remVec, Vector3 remplayerPOS)
		{
			this.lightpos = campos;
			this.lightlookpos = camlookpos;
			this.lighton = myPlayer.now.flashlight;
			this.gunfired = myPlayer.gunFired;
			this.lastpumpkin = -1;
			Vector3 vector = Vector3.Normalize(camlookpos - campos);
			Vector3 vector2 = -vector * 100f + campos;
			range *= range;
			this.pumpkinIndex = -1;
			float num = 0f;
			bool flag = false;
			sh.tempindex = 0;
			this.alive = 0;
			skullkins.pumpkindistance = 10000f;
			float num2 = myPlayer.vec.Length();
			float num3 = remVec.Length();
			for (int i = 0; i < sh.max; i++)
			{
				if (sh.dupe[i].move != -1)
				{
					float num4;
					Vector3.DistanceSquared(ref myPlayer.displayState.npcPosition, ref sh.dupe[i].mypos, out num4);
					if (sh.dupe[i].firstHit == 1)
					{
						sh.dupe[i].firstHit = 2;
						num = MathHelper.Clamp(1f - ((float)Math.Sqrt((double)num4) - 200f) / 2000f, 0.1f, 1f);
						if (sh.dupe[sh.index].rr.Next(1, 100) < 80)
						{
							this.ss1.Play(this.sc.ev * num, (float)sh.dupe[sh.index].rr.Next(-20, 60) / 100f, (float)sh.dupe[sh.index].rr.Next(-100, 100) / 100f);
						}
					}
					if (sh.dupe[i].firstHit == 4)
					{
						sh.dupe[i].firstHit = 5;
						num = MathHelper.Clamp(1f - ((float)Math.Sqrt((double)num4) - 200f) / 2000f, 0.1f, 1f);
						if (sh.dupe[sh.index].rr.Next(1, 100) < 80)
						{
							this.ss2.Play(this.sc.ev * num, (float)sh.dupe[sh.index].rr.Next(-20, 60) / 100f, (float)sh.dupe[sh.index].rr.Next(-100, 100) / 100f);
						}
					}
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					this.alive++;
					if (num4 < range)
					{
						Vector3 vector3 = new Vector3(sh.dupe[i].mypos.X, sh.dupe[i].mypos.Y, sh.dupe[i].mypos.Z) - vector2;
						Vector3 vector4;
						Vector3.Normalize(ref vector3, out vector4);
						float num5 = 0f;
						Vector3.Dot(ref vector4, ref vector, out num5);
						if (num5 > this.sc.myfov)
						{
							if (sh.dupe[i].move > 0)
							{
								sh.dupe[i].updateSkullHead(ref heights);
							}
							sh.stream[i].Trans = sh.dupe[i].transform;
							sh.stream[i].tint = sh.dupe[i].tint;
							sh.displayList[sh.tempindex] = sh.stream[i];
							sh.tempindex++;
							flag2 = true;
							if (sh.dupe[i].kickable && !flag)
							{
								if (myPlayer.gunFired && num5 > 0.95f)
								{
									float num6 = 32f;
									if (genCursor.hitSphere(myPlayer.gunpos, myPlayer.gunlook, sh.dupe[i].mypos, sh.dupe[i].scale.Y * num6) != null && (float)Math.Sqrt((double)num4) <= skullkins.pumpkindistance)
									{
										skullkins.pumpkindistance = (float)Math.Sqrt((double)num4);
										num = MathHelper.Clamp(1f - ((float)Math.Sqrt((double)num4) - 200f) / 4000f, 0.2f, 1f);
										this.pumpkinIndex = i;
									}
								}
								if (num3 > 0f && !flag4)
								{
									float num7;
									Vector3.DistanceSquared(ref remplayerPOS, ref sh.dupe[i].mypos, out num7);
									if (num7 < 6400f * sh.dupe[i].scale.X)
									{
										sh.dupe[i].move = 3;
										sh.dupe[i].firstHit = 0;
										sh.dupe[i].velocity = remVec * (float)sh.dupe[sh.index].rr.Next(100, 200) / 100f;
										sh.dupe[i].velocity.Y = (float)sh.dupe[sh.index].rr.Next(5, 15) / 10f;
										skullDupe skullDupe = sh.dupe[i];
										skullDupe.velocity.X = skullDupe.velocity.X + (float)sh.dupe[sh.index].rr.Next(-100, 100) / 100f;
										skullDupe skullDupe2 = sh.dupe[i];
										skullDupe2.velocity.Z = skullDupe2.velocity.Z + (float)sh.dupe[sh.index].rr.Next(-100, 100) / 100f;
										if (sh.dupe[sh.index].rr.Next(1, 100) < 30)
										{
											this.ss2.Play(this.sc.ev * 0.5f, (float)sh.dupe[sh.index].rr.Next(-40, 40) / 100f, (float)sh.dupe[sh.index].rr.Next(-100, 100) / 100f);
										}
									}
								}
								if (!myPlayer.isDown && !myPlayer.gunFired && num4 < 6400f * sh.dupe[i].scale.X)
								{
									if (num2 > 1f && !flag3)
									{
										sh.dupe[i].move = 3;
										sh.dupe[i].firstHit = 0;
										sh.dupe[i].velocity = myPlayer.vec * (float)sh.dupe[sh.index].rr.Next(50, 100) / 100f;
										sh.dupe[i].velocity.Y = (float)sh.dupe[sh.index].rr.Next(5, 15) / 10f;
										skullDupe skullDupe3 = sh.dupe[i];
										skullDupe3.velocity.X = skullDupe3.velocity.X + (float)sh.dupe[sh.index].rr.Next(-100, 100) / 100f;
										skullDupe skullDupe4 = sh.dupe[i];
										skullDupe4.velocity.Z = skullDupe4.velocity.Z + (float)sh.dupe[sh.index].rr.Next(-100, 100) / 100f;
										if (sh.dupe[sh.index].rr.Next(1, 100) < 40)
										{
											this.ss2.Play(this.sc.ev * 0.5f, (float)sh.dupe[sh.index].rr.Next(-40, 40) / 100f, (float)sh.dupe[sh.index].rr.Next(-100, 100) / 100f);
										}
									}
									if (sh.dupe[i].move == 3 && sh.dupe[i].canhurt)
									{
										sh.dupe[i].canhurt = false;
										this.sc.falldown2.Play(this.sc.ev, (float)sh.dupe[sh.index].rr.Next(-20, 20) / 100f, 0f);
										Vector2 vector5 = new Vector2(sh.dupe[i].mypos.X, sh.dupe[i].mypos.Z) - new Vector2(myPlayer.displayState.npcPosition.X, myPlayer.displayState.npcPosition.Z);
										if (vector5.Length() > 0f)
										{
											hitVel = -Vector2.Normalize(vector5) * 2.5f;
											hitVel = Vector2.Transform(new Vector2(-hitVel.X, hitVel.Y), Matrix.CreateRotationZ(-headRot));
										}
										if (this.sc.df == 0)
										{
											myPlayer.damHealth((float)sh.dupe[sh.index].rr.Next(3, 6), false);
										}
										else
										{
											myPlayer.damHealth((float)sh.dupe[sh.index].rr.Next(7, 11), false);
										}
									}
								}
							}
						}
					}
					if (sh.dupe[i].move > 0 && !flag2)
					{
						sh.dupe[i].updateSkullHead(ref heights);
					}
					this.lastpumpkin = i;
					sh.dupe[i].pop = false;
				}
			}
			if (this.pumpkinIndex != -1)
			{
				this.explode(this.pumpkinIndex, ref myPlayer, ref heights, num);
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x002E5E50 File Offset: 0x002E4050
		public void updateSkullParts(ref skullkins.shell sh, float range, ref float[,] heights, Vector3 campos, Vector3 camlookpos, ref localPlayer myPlayer, ref Cursor genCursor)
		{
			this.lightpos = campos;
			this.lightlookpos = camlookpos;
			this.lighton = myPlayer.now.flashlight;
			this.gunfired = myPlayer.gunFired;
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

		// Token: 0x06000B4F RID: 2895 RVA: 0x002E6064 File Offset: 0x002E4264
		public void explode(int i, ref localPlayer myPlayer, ref float[,] heights, float vol)
		{
			this.pump.dupe[i].move = -1;
			myPlayer.pumpkinID = (byte)i;
			if (this.pump.dupe[i].tint != 11f)
			{
				int num = this.rz.Next(0, 4);
				if (num == 0)
				{
					this.sound1.Play(this.sc.ev * vol, (float)this.rz.Next(-50, 0) / 100f, 0f);
				}
				if (num == 1)
				{
					this.sound2.Play(this.sc.ev * vol, (float)this.rz.Next(-50, 0) / 100f, 0f);
				}
				if (num == 2)
				{
					this.sound3.Play(this.sc.ev * vol, (float)this.rz.Next(-50, 0) / 100f, 0f);
				}
				if (num == 3)
				{
					this.sound4.Play(this.sc.ev * vol, (float)this.rz.Next(-50, 0) / 100f, 0f);
				}
			}
			else if (!myPlayer.tunnelHeal)
			{
				this.sc.toneer.Play(this.sc.ev, 0f, 0f);
				myPlayer.tunnelHeal = true;
			}
			Matrix matrix = Matrix.CreateRotationX(MathHelper.ToRadians(7.26f)) * Matrix.CreateTranslation(1.63f, 40.52f, 2.89f);
			this.dropParts(ref this.p_stem, i, matrix, 5);
			matrix = Matrix.CreateRotationY(MathHelper.ToRadians(-5.13f)) * Matrix.CreateTranslation(-15.9f, -1.29f, -14.283f);
			this.dropParts(ref this.p_rind, i, matrix, 18);
			matrix = Matrix.CreateRotationY(MathHelper.ToRadians(-20.3f)) * Matrix.CreateTranslation(23.55f, 0.155f, 5.59f);
			this.dropParts(ref this.p_base, i, matrix, 18);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(0f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-46.59f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(181f)) * Matrix.CreateTranslation(-19.07f, -0.92f, 15.73f);
			this.dropParts(ref this.p_base, i, matrix, 18);
			matrix = Matrix.CreateTranslation(0.76f, 17.35f, 29f);
			this.dropParts(ref this.p_chunk, i, matrix, 10);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-166f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-41f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(9f)) * Matrix.CreateTranslation(21.21f, -11.5f, -26.53f);
			this.dropParts(ref this.p_chunk, i, matrix, 10);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-165.9f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-3.14f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(186.15f)) * Matrix.CreateTranslation(-2f, 14.7f, -31.3f);
			this.dropParts(ref this.p_chunk, i, matrix, 10);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-14.36f)) * Matrix.CreateRotationY(MathHelper.ToRadians(9.5f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(5.5f)) * Matrix.CreateTranslation(-3.26f, -11.96f, -33.03f);
			this.dropParts(ref this.p_bitty, i, matrix, 5);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-171.5f)) * Matrix.CreateRotationY(MathHelper.ToRadians(20f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(7.84f)) * Matrix.CreateTranslation(11.7f, -8.2f, 32.9f);
			this.dropParts(ref this.p_bitty, i, matrix, 5);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(69f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-42.3f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(3.6f)) * Matrix.CreateTranslation(12.43f, 27f, -11.72f);
			this.dropParts(ref this.p_bitty, i, matrix, 5);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x002E64E4 File Offset: 0x002E46E4
		public void draw(Matrix view, Matrix proj)
		{
			this.view = view;
			this.proj = proj;
			this.DrawInstance(ref this.pump, "fastShader2");
			this.DrawInstance(ref this.p_stem, "fastShader2");
			this.DrawInstance(ref this.p_base, "fastShader2");
			this.DrawInstance(ref this.p_rind, "fastShader2");
			this.DrawInstance(ref this.p_chunk, "fastShader2");
			this.DrawInstance(ref this.p_bitty, "fastShader2");
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x002E6568 File Offset: 0x002E4768
		private void DrawInstance(ref skullkins.shell shell, string tech)
		{
			int tempindex = shell.tempindex;
			if (tempindex < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = shell.model1.Meshes[0].MeshParts[0];
			shell.buffer.SetData<skullkins.instancedObject>(shell.displayList, 0, tempindex, SetDataOptions.Discard);
			this.effect.CurrentTechnique = this.effect.Techniques[tech];
			Vector3 vector = Vector3.Normalize(this.lightpos - this.lightlookpos);
			vector.Y *= -1f;
			vector.X *= -1f;
			float num = 1.1f;
			float num2 = 700f;
			if (!this.lighton)
			{
				num = 0.3f;
				num2 = 350f;
			}
			if (this.gunfired)
			{
				num2 = Math.Max(450f, num2);
				num = 1.3f;
			}
			this.effect.Parameters["depth"].SetValue(5000);
			this.effect.Parameters["lightPos1"].SetValue(this.lightpos);
			this.effect.Parameters["Texture"].SetValue(this.skulltexture);
			this.effect.Parameters["LightDirection2"].SetValue(vector);
			this.effect.Parameters["diff2"].SetValue(new Vector3(0.8f, 0.8f, 0.8f) * num);
			this.effect.Parameters["amb2"].SetValue(new Vector3(0.2f, 0.2f, 0.2f));
			this.effect.Parameters["View"].SetValue(this.view);
			this.effect.Parameters["Projection"].SetValue(this.proj);
			this.effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(shell.buffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, tempindex);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x002E6814 File Offset: 0x002E4A14
		private static void GetHeightFast(ref float[,] heights, ref Vector3 pos, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(pos.X / skullkins.unit, 0f, (float)(skullkins.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(pos.Z / skullkins.unit, 0f, (float)(skullkins.bitmap - 2));
			float num3 = pos.X % skullkins.unit / skullkins.unit;
			float num4 = pos.Z % skullkins.unit / skullkins.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x04002E4C RID: 11852
		public static float pumpkindistance = 10000f;

		// Token: 0x04002E4D RID: 11853
		private Effect effect;

		// Token: 0x04002E4E RID: 11854
		private ScreenManager sc;

		// Token: 0x04002E4F RID: 11855
		private ContentManager content;

		// Token: 0x04002E50 RID: 11856
		public static int bitmap;

		// Token: 0x04002E51 RID: 11857
		public static float unit;

		// Token: 0x04002E52 RID: 11858
		public int pumpkinIndex = -1;

		// Token: 0x04002E53 RID: 11859
		public int lastpumpkin = -1;

		// Token: 0x04002E54 RID: 11860
		private Vector3 lightpos;

		// Token: 0x04002E55 RID: 11861
		private Vector3 lightlookpos;

		// Token: 0x04002E56 RID: 11862
		private bool lighton;

		// Token: 0x04002E57 RID: 11863
		private bool gunfired;

		// Token: 0x04002E58 RID: 11864
		private readonly VertexBufferBinding[] _vertexBufferBindings = new VertexBufferBinding[2];

		// Token: 0x04002E59 RID: 11865
		private static VertexDeclaration instanceDec = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
		});

		// Token: 0x04002E5A RID: 11866
		private skullkins.instancedObject[] tempInstance = new skullkins.instancedObject[1];

		// Token: 0x04002E5B RID: 11867
		public skullkins.shell pump;

		// Token: 0x04002E5C RID: 11868
		public skullkins.shell p_stem;

		// Token: 0x04002E5D RID: 11869
		public skullkins.shell p_base;

		// Token: 0x04002E5E RID: 11870
		public skullkins.shell p_rind;

		// Token: 0x04002E5F RID: 11871
		public skullkins.shell p_chunk;

		// Token: 0x04002E60 RID: 11872
		public skullkins.shell p_bitty;

		// Token: 0x04002E61 RID: 11873
		private SoundEffect sound1;

		// Token: 0x04002E62 RID: 11874
		private SoundEffect sound2;

		// Token: 0x04002E63 RID: 11875
		private SoundEffect sound3;

		// Token: 0x04002E64 RID: 11876
		private SoundEffect sound4;

		// Token: 0x04002E65 RID: 11877
		private SoundEffect pop;

		// Token: 0x04002E66 RID: 11878
		private SoundEffect pop2;

		// Token: 0x04002E67 RID: 11879
		private SoundEffect ss1;

		// Token: 0x04002E68 RID: 11880
		private SoundEffect ss2;

		// Token: 0x04002E69 RID: 11881
		private Texture2D skulltexture;

		// Token: 0x04002E6A RID: 11882
		public int alive;

		// Token: 0x04002E6B RID: 11883
		private Matrix view;

		// Token: 0x04002E6C RID: 11884
		private Matrix proj;

		// Token: 0x04002E6D RID: 11885
		private Random rz;

		// Token: 0x0200013C RID: 316
		public struct instancedObject : IVertexType
		{
			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000B54 RID: 2900 RVA: 0x002E69B7 File Offset: 0x002E4BB7
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return skullkins.instancedObject.InstanceVertexDeclaration;
				}
			}

			// Token: 0x04002E6E RID: 11886
			public Matrix Trans;

			// Token: 0x04002E6F RID: 11887
			public float tint;

			// Token: 0x04002E70 RID: 11888
			private static readonly VertexDeclaration InstanceVertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
			});
		}

		// Token: 0x0200013D RID: 317
		public struct shell
		{
			// Token: 0x04002E71 RID: 11889
			public int type;

			// Token: 0x04002E72 RID: 11890
			public int drop;

			// Token: 0x04002E73 RID: 11891
			public int max;

			// Token: 0x04002E74 RID: 11892
			public int tempindex;

			// Token: 0x04002E75 RID: 11893
			public int index;

			// Token: 0x04002E76 RID: 11894
			public int maxCapacity;

			// Token: 0x04002E77 RID: 11895
			public skullkins.instancedObject[] stream;

			// Token: 0x04002E78 RID: 11896
			public DynamicVertexBuffer buffer;

			// Token: 0x04002E79 RID: 11897
			public skullkins.instancedObject[] displayList;

			// Token: 0x04002E7A RID: 11898
			public skullDupe[] dupe;

			// Token: 0x04002E7B RID: 11899
			public Model model1;
		}
	}
}
