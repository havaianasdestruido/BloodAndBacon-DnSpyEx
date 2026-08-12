using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000088 RID: 136
	internal class rockstruct : GameScreen
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x00115F28 File Offset: 0x00114128
		public void LoadContent(ContentManager content, ScreenManager sc, int grid, int bit, int count)
		{
			this.content = content;
			this.random = new Random();
			this.sc = sc;
			this.gr = sc.GraphicsDevice;
			this.gridscale = grid;
			this.bitmap = bit;
			this.thisframe = 0f;
			this.rr = new Random();
			objDupe.objectData = new int[500, 500];
			this.maxrockCount = count;
			this.maxflowerCount = 200;
			this.rockInst = new objDupe[this.maxrockCount];
			for (int i = 0; i < this.maxrockCount; i++)
			{
				this.rockInst[i] = new objDupe(i);
			}
			this.rockTrans = new rockstruct.transcolor[this.maxrockCount];
			this.rockBuffer = new DynamicVertexBuffer(this.gr, rockstruct.vd, this.maxrockCount, BufferUsage.WriteOnly);
			this.flowerInst = new objDupe[this.maxflowerCount];
			for (int j = 0; j < this.maxflowerCount; j++)
			{
				this.flowerInst[j] = new objDupe(j);
			}
			this.flowerTrans = new rockstruct.transcolor[this.maxflowerCount];
			this.flowerBuffer = new DynamicVertexBuffer(this.gr, rockstruct.vd, this.maxflowerCount, BufferUsage.WriteOnly);
			this.slabmax = 80;
			this.slabTrans = new rockstruct.transcolor[this.slabmax];
			this.slabInst = new slabDupe[this.slabmax];
			for (int k = 0; k < this.slabmax; k++)
			{
				this.slabInst[k] = new slabDupe(k);
			}
			this.slabBuffer = new DynamicVertexBuffer(this.gr, rockstruct.vd, this.slabmax, BufferUsage.WriteOnly);
			this.minAngle = 0.3f;
			this.maxAngle = 0.85f;
			this.minAngle = 0f;
			this.maxAngle = 0.99f;
			this.slab2max = 200;
			this.slab2Trans = new rockstruct.transcolor[this.slab2max];
			this.slab2Inst = new slabDupe2[this.slab2max];
			this.slab2Inst = new slabDupe2[this.slab2max];
			for (int l = 0; l < this.slab2max; l++)
			{
				this.slab2Inst[l] = new slabDupe2(l);
			}
			this.slab2Buffer = new DynamicVertexBuffer(this.gr, rockstruct.vd, this.slab2max, BufferUsage.WriteOnly);
			this.rockCount = 0;
			this.rock1Count = 0;
			this.rock2Count = 0;
			this.slabCount = 0;
			this.slab2Count = 0;
			this.rock1Inst = new rockBreak[this.maxcount2];
			this.rock1Trans = new rockstruct.transcolor[this.maxcount2];
			this.rock1Buffer = new DynamicVertexBuffer(this.gr, rockstruct.vd, this.maxcount2, BufferUsage.WriteOnly);
			this.rock2Inst = new rockBreak[this.maxcount2];
			this.rock2Trans = new rockstruct.transcolor[this.maxcount2];
			this.rock2Buffer = new DynamicVertexBuffer(this.gr, rockstruct.vd, this.maxcount2, BufferUsage.WriteOnly);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00116210 File Offset: 0x00114410
		public override void UnloadContent()
		{
			this.content.Unload();
			this.rockInst.Initialize();
			this.rockBuffer.Dispose();
			this.rockBuffer.Dispose();
			this.rock1Buffer.Dispose();
			this.rock2Buffer.Dispose();
			this.slabBuffer.Dispose();
			this.slab2Buffer.Dispose();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00116278 File Offset: 0x00114478
		public void placeOBJECTS(int vehicleindex, ref Rover rover, ref Lander lander, ref int[,] heightData, ref Vector3[,] normalData, ref int[,] objectData, float color)
		{
			Vector3 vector;
			if (vehicleindex == 1)
			{
				vector = rover.position;
			}
			else
			{
				vector = lander.position;
			}
			this.rockCount = 0;
			this.rockInst[0].init(false, new Vector3(0f, -100000f, 0f), this.sc, 1, 0, 0);
			this.rockTrans[0].Trans = this.rockInst[this.rockCount].Transform;
			this.flowerCount = 0;
			this.flowerInst[0].initflower(new Vector3(0f, -100000f, 0f), this.sc, 1, 0, 0);
			this.flowerTrans[0].Trans = this.flowerInst[this.flowerCount].Transform;
			int num = (int)vector.X - 21000;
			while ((float)num < vector.X + 21000f)
			{
				int num2 = (int)vector.Z - 21000;
				while ((float)num2 < vector.Z + 21000f)
				{
					int num3 = 1999 * this.gridscale;
					float num4 = ((float)(num % num3) + 1.5f * (float)num3) % (float)num3;
					float num5 = ((float)(num2 % num3) + 1.5f * (float)num3) % (float)num3;
					num4 = (float)((int)num4 / this.gridscale);
					num5 = (float)((int)num5 / this.gridscale);
					num4 = (float)((int)(num4 / 4f));
					num5 = (float)((int)(num5 / 4f));
					int num6 = objectData[(int)num4, (int)num5];
					if (objDupe.objectData[(int)num4, (int)num5] == 0 && num6 >= 27 && num6 <= 30)
					{
						int num7 = ((int)num4 + (int)num5 + 1) * ((int)num4 + (int)num5) / 2 + (int)num5;
						Random random = new Random(num7);
						float num8;
						Vector3 vector2;
						this.GetHeightNormal(ref heightData, ref normalData, new Vector3((float)(num + 300), 0f, (float)num2), out num8, out vector2);
						if (vector2.Y >= 0.7f)
						{
							this.rockCount++;
							int num9 = random.Next(1, 100);
							if (num9 < 20)
							{
								this.rockInst[this.rockCount].gemtype = 2f;
							}
							else if (num9 >= 20 && num9 < 95)
							{
								this.rockInst[this.rockCount].gemtype = 1f;
							}
							else
							{
								this.rockInst[this.rockCount].gemtype = 0f;
							}
							if (num6 == 28 || num6 == 30)
							{
								this.rockInst[this.rockCount].gemtype = 3f;
							}
							bool flag = false;
							if (num6 == 29 || num6 == 30)
							{
								flag = true;
							}
							this.rockInst[this.rockCount].init(flag, new Vector3((float)(num + 300), num8, (float)num2), this.sc, num7, (int)num4, (int)num5);
							this.rockInst[this.rockCount].addObject(27);
							objDupe.objectData[(int)num4, (int)num5] = 27;
							this.rockTrans[this.rockCount].Trans = this.rockInst[this.rockCount].Transform;
							this.rockTrans[this.rockCount].Color = this.rockInst[this.rockCount].gemtype;
						}
					}
					if (objDupe.objectData[(int)num4, (int)num5] == 0 && num6 == 7)
					{
						int num10 = ((int)num4 + (int)num5 + 1) * ((int)num4 + (int)num5) / 2 + (int)num5;
						float num8;
						Vector3 vector2;
						this.GetHeightNormal(ref heightData, ref normalData, new Vector3((float)num, 0f, (float)num2), out num8, out vector2);
						if (this.flowerCount < this.maxflowerCount)
						{
							this.flowerCount++;
							this.flowerInst[this.flowerCount].initflower(new Vector3((float)num, num8, (float)num2), this.sc, num10, (int)num4, (int)num5);
							this.flowerInst[this.flowerCount].addObject(7);
							this.flowerInst[this.flowerCount].gemtype = 1f;
							objDupe.objectData[(int)num4, (int)num5] = 7;
							this.flowerTrans[this.flowerCount].Trans = this.flowerInst[this.flowerCount].Transform;
							this.flowerTrans[this.flowerCount].Color = 1f;
						}
					}
					if (this.rockCount >= this.maxrockCount - 1)
					{
						break;
					}
					num2 += 150;
				}
				if (this.rockCount >= this.maxrockCount - 1)
				{
					return;
				}
				num += 150;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00116730 File Offset: 0x00114930
		public void removeOBJECTS(Vector3 campos, Vector3 camlookpos, int vehicleindex, Vector3 grnd, ref int[,] heightData, ref Vector3[,] normalData, ref int[,] objectData)
		{
			this.thisframe += 1f;
			this.chosen = -1;
			this.chosenfar = -1;
			float num = 35000f;
			this.flowerDistance = 35000f;
			for (int i = 1; i <= this.flowerCount; i++)
			{
				this.flowerTrans[i].Color = (float)(Math.Sin((double)(this.thisframe / this.flowerInst[i].div)) / 5.0 + 0.8199999928474426);
				if (this.chosen == -1)
				{
					float num2 = Vector2.Distance(new Vector2(this.flowerInst[i].mypos.X, this.flowerInst[i].mypos.Z), new Vector2(campos.X, campos.Z));
					if (this.flowerInst[i].stateFlag == 15 && num2 < num)
					{
						this.flowerDistance = num2;
						num = num2;
						this.chosenfar = i;
					}
					Vector3 vector = Vector3.Normalize(camlookpos - campos);
					Vector3 vector2 = Vector3.Normalize(new Vector3(this.flowerInst[i].mypos.X, this.flowerInst[i].mypos.Y - 20f, this.flowerInst[i].mypos.Z) - campos);
					float num3 = Vector3.Dot(vector2, vector);
					if (num2 < 150f && num3 > 0.8f)
					{
						this.chosen = i;
					}
				}
				if (this.flowerInst[i].stateFlag == 5)
				{
					this.flowerInst[i].Update(ref heightData, ref normalData, ref objectData);
					this.flowerTrans[i].Trans = this.flowerInst[i].Transform;
				}
				float num4 = Vector2.DistanceSquared(new Vector2(grnd.X, grnd.Z), new Vector2(this.flowerInst[i].mypos.X, this.flowerInst[i].mypos.Z));
				if (num4 > (float)((this.acre + 9000) * (this.acre + 9000)))
				{
					if (i != this.flowerCount)
					{
						this.flowerInst[i].move = this.flowerInst[this.flowerCount].move;
						if (this.flowerInst[this.flowerCount].onMap)
						{
							this.flowerInst[i].onMap = true;
							this.flowerInst[i].removeObject();
							this.flowerInst[i].objx = this.flowerInst[this.flowerCount].objx;
							this.flowerInst[i].objz = this.flowerInst[this.flowerCount].objz;
							this.flowerInst[i].addObject(7);
							if (this.flowerInst[this.flowerCount].stateFlag == 10)
							{
								this.flowerInst[i].stateFlag = 10;
								this.flowerInst[this.flowerCount].stateFlag = 0;
								this.flowerInst[i].addObject(8);
							}
							this.flowerInst[this.flowerCount].onMap = false;
						}
						else
						{
							this.flowerInst[i].onMap = false;
							this.flowerInst[i].removeObject();
							this.flowerInst[i].objx = this.rockInst[this.flowerCount].objx;
							this.flowerInst[i].objz = this.rockInst[this.flowerCount].objz;
							this.flowerInst[this.flowerCount].onMap = false;
							this.flowerInst[this.flowerCount].removeObject();
						}
						this.flowerInst[i].mypos = this.flowerInst[this.flowerCount].mypos;
						this.flowerInst[i].scaler = this.flowerInst[this.flowerCount].scaler;
						this.flowerInst[i].myRot = this.flowerInst[this.flowerCount].myRot;
						this.flowerInst[i].Transform = this.flowerInst[this.flowerCount].Transform;
						this.flowerInst[i].hits = this.flowerInst[this.flowerCount].hits;
						this.flowerInst[i].grav = this.flowerInst[this.flowerCount].grav;
						this.flowerTrans[i].Trans = this.flowerInst[this.flowerCount].Transform;
						this.flowerTrans[i].Color = this.flowerTrans[this.flowerCount].Color;
					}
					else
					{
						this.flowerInst[i].removeObject();
						this.flowerInst[i].move = false;
					}
					this.flowerCount--;
					if (this.flowerCount < 0)
					{
						this.flowerCount = 0;
					}
				}
			}
			int num5 = (int)(this.thisframe % 50f);
			for (int j = 1 + num5; j <= this.rockCount; j += 50)
			{
				if (j > this.rockCount)
				{
					return;
				}
				float num6 = Vector2.DistanceSquared(new Vector2(grnd.X, grnd.Z), new Vector2(this.rockInst[j].mypos.X, this.rockInst[j].mypos.Z));
				if (num6 > (float)((this.acre + 9000) * (this.acre + 9000)))
				{
					if (j != this.rockCount)
					{
						this.rockMoving.Remove(j);
						this.rockClose.Remove(j);
						if (this.rockClose.Contains(this.rockCount))
						{
							this.rockClose.Remove(this.rockCount);
							if (!this.rockClose.Contains(j))
							{
								this.rockClose.Add(j);
							}
						}
						if (this.rockInst[this.rockCount].move)
						{
							this.rockMoving.Remove(this.rockCount);
							if (!this.rockMoving.Contains(j))
							{
								this.rockMoving.Add(j);
							}
						}
						this.rockInst[j].move = this.rockInst[this.rockCount].move;
						if (this.rockInst[this.rockCount].onMap)
						{
							this.rockInst[j].removeObject();
							this.rockInst[j].objx = this.rockInst[this.rockCount].objx;
							this.rockInst[j].objz = this.rockInst[this.rockCount].objz;
							this.rockInst[j].addObject(27);
							this.rockInst[j].onMap = true;
							this.rockInst[this.rockCount].onMap = false;
						}
						else
						{
							this.rockInst[j].onMap = false;
							this.rockInst[j].removeObject();
							this.rockInst[j].objx = this.rockInst[this.rockCount].objx;
							this.rockInst[j].objz = this.rockInst[this.rockCount].objz;
							this.rockInst[this.rockCount].onMap = false;
							this.rockInst[this.rockCount].removeObject();
						}
						this.rockInst[j].mypos = this.rockInst[this.rockCount].mypos;
						this.rockInst[j].scaler = this.rockInst[this.rockCount].scaler;
						this.rockInst[j].myRot = this.rockInst[this.rockCount].myRot;
						this.rockInst[j].Transform = this.rockInst[this.rockCount].Transform;
						this.rockInst[j].hits = this.rockInst[this.rockCount].hits;
						this.rockInst[j].grav = this.rockInst[this.rockCount].grav;
						this.rockInst[j].gemtype = this.rockInst[this.rockCount].gemtype;
						this.rockTrans[j].Trans = this.rockInst[this.rockCount].Transform;
						this.rockTrans[j].Color = this.rockTrans[this.rockCount].Color;
					}
					else
					{
						this.rockInst[j].removeObject();
						this.rockMoving.Remove(j);
						this.rockClose.Remove(j);
						this.rockInst[j].move = false;
					}
					this.rockCount--;
					if (this.rockCount < 0)
					{
						this.rockCount = 0;
					}
				}
				else if (num6 < 16000000f)
				{
					if (!this.rockClose.Contains(j))
					{
						this.rockClose.Add(j);
					}
				}
				else if (this.rockClose.Contains(j))
				{
					this.rockClose.Remove(j);
				}
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00117070 File Offset: 0x00115270
		public void simpleObjectAdd(float x, float z, ref int[,] heightData, ref Vector3[,] normalData, ref int[,] objectData)
		{
			if (this.rockCount >= this.maxrockCount)
			{
				return;
			}
			int num = 1999 * this.gridscale;
			float num2 = ((x + 0f) % (float)num + 1.5f * (float)num) % (float)num;
			float num3 = ((z + 0f) % (float)num + 1.5f * (float)num) % (float)num;
			num2 = (float)((int)num2 / this.gridscale);
			num3 = (float)((int)num3 / this.gridscale);
			num2 = (float)((int)(num2 / 4f));
			num3 = (float)((int)(num3 / 4f));
			int num4 = objectData[(int)num2, (int)num3];
			if (this.rockCount < this.maxrockCount && num4 >= 27 && num4 <= 30 && objDupe.objectData[(int)num2, (int)num3] == 0)
			{
				int num5 = ((int)num2 + (int)num3 + 1) * ((int)num2 + (int)num3) / 2 + (int)num3;
				Random random = new Random(num5);
				float num6;
				Vector3 vector;
				this.GetHeightNormal(ref heightData, ref normalData, new Vector3(x, 0f, z), out num6, out vector);
				if (vector.Y >= 0.7f)
				{
					this.rockCount++;
					int num7 = random.Next(1, 100);
					if (num7 < 20)
					{
						this.rockInst[this.rockCount].gemtype = 2f;
					}
					else if (num7 >= 20 && num7 < 95)
					{
						this.rockInst[this.rockCount].gemtype = 1f;
					}
					else
					{
						this.rockInst[this.rockCount].gemtype = 0f;
					}
					if (num4 == 28 || num4 == 30)
					{
						this.rockInst[this.rockCount].gemtype = 3f;
					}
					bool flag = false;
					if (num4 == 29 || num4 == 30)
					{
						flag = true;
					}
					this.rockInst[this.rockCount].init(flag, new Vector3(x, num6, z), this.sc, num5, (int)num2, (int)num3);
					this.rockInst[this.rockCount].addObject(27);
					objDupe.objectData[(int)num2, (int)num3] = 27;
					this.rockTrans[this.rockCount].Trans = this.rockInst[this.rockCount].Transform;
					this.rockTrans[this.rockCount].Color = this.rockInst[this.rockCount].gemtype;
				}
			}
			if ((num4 == 7 || num4 == 8) && objDupe.objectData[(int)num2, (int)num3] == 0)
			{
				int num8 = ((int)num2 + (int)num3 + 1) * ((int)num2 + (int)num3) / 2 + (int)num3;
				float num6;
				Vector3 vector;
				this.GetHeightNormal(ref heightData, ref normalData, new Vector3(x, 0f, z), out num6, out vector);
				if (this.flowerCount < this.maxflowerCount - 1)
				{
					this.flowerCount++;
					if (num4 == 8)
					{
						this.flowerInst[this.flowerCount].stateFlag = 10;
					}
					if (num4 == 7)
					{
						this.flowerInst[this.flowerCount].stateFlag = 0;
					}
					this.flowerInst[this.flowerCount].initflower(new Vector3(x, num6, z), this.sc, num8, (int)num2, (int)num3);
					this.flowerInst[this.flowerCount].addObject(num4);
					this.flowerInst[this.flowerCount].gemtype = 1f;
					objDupe.objectData[(int)num2, (int)num3] = num4;
					this.flowerTrans[this.flowerCount].Trans = this.flowerInst[this.flowerCount].Transform;
					this.flowerTrans[this.flowerCount].Color = this.flowerInst[this.flowerCount].gemtype;
				}
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00117424 File Offset: 0x00115624
		public void collideROCKS(int vehicleindex, ref Lander lander, ref Rover rover, Vector3 campos, ref SoundEffect boulderhit, ref SoundEffect crack, ref int[,] heightData, ref Vector3[,] normals, ref int[,] objectData, ref Vector3 vec, ref Vector3 hitVec)
		{
			this.sc.gemDropType = 0;
			Vector3 vector;
			if (vehicleindex == 1)
			{
				vector = rover.position;
			}
			else
			{
				vector = lander.position;
			}
			if (vehicleindex == 1)
			{
				for (int i = 0; i < this.rockClose.Count; i++)
				{
					int num = this.rockClose[i];
					float num2 = Vector3.DistanceSquared(this.rockInst[num].mypos, rover.position);
					float num3 = 40f + this.rockInst[num].scaler * 30f;
					if (num2 < num3 * num3)
					{
						boulderhit.Play(this.sc.ev, 0f, 0f);
						Vector2 vector2 = Vector2.Zero;
						Vector2 vector3 = new Vector2(rover.position.X - this.rockInst[num].mypos.X, rover.position.Z - this.rockInst[num].mypos.Z);
						if (vector3.LengthSquared() > 0f)
						{
							vector2 = Vector2.Normalize(vector3);
						}
						if (this.rockInst[num].hits > 1)
						{
							rover.grav.X = vector2.X * 3f * this.rockInst[num].mass;
							rover.grav.Z = vector2.Y * 3f * this.rockInst[num].mass;
							Rover.fric = 0.99f;
							Rover.rockhitCount = 150;
							rover.movement *= 0f;
						}
						rover.shockhit = 15;
						bool flag = true;
						this.rockInst[num].move = true;
						if (!this.rockMoving.Contains(num))
						{
							this.rockMoving.Add(num);
						}
						objDupe objDupe = this.rockInst[num];
						objDupe.grav.X = objDupe.grav.X + -vector2.X * 10f / this.rockInst[num].mass;
						objDupe objDupe2 = this.rockInst[num];
						objDupe2.grav.Z = objDupe2.grav.Z + -vector2.Y * 10f / this.rockInst[num].mass;
						this.rockInst[num].hits--;
						if (this.rockInst[num].hits <= 0)
						{
							this.sc.gemDropPosition = this.rockInst[num].mypos;
							this.sc.gemDropVelocity = this.rockInst[num].grav;
							this.sc.gemDropScale = this.rockInst[num].scaler;
							this.sc.gemDropType = (int)this.rockInst[num].gemtype;
							if (this.sc.gemDropType > 0)
							{
								this.sc.breakage.Play(this.sc.ev, (float)this.rr.Next(-20, 20) / 100f, 0f);
							}
							else
							{
								this.sc.breakage2.Play(this.sc.ev, (float)this.rr.Next(-10, 30) / 100f, 0f);
							}
							this.splitBoulder(num, ref rover, ref boulderhit, ref crack, ref objectData);
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			else if (vehicleindex == 2)
			{
				float num4;
				this.GetHeightOnly(ref heightData, lander.position, out num4);
				if (lander.position.Y - num4 < 500f)
				{
					for (int j = 0; j < this.rockClose.Count; j++)
					{
						int num5 = this.rockClose[j];
						float num6 = Vector3.DistanceSquared(this.rockInst[num5].mypos, lander.position);
						float num7 = 220f + this.rockInst[num5].scaler * 30f;
						if (num6 < num7 * num7)
						{
							boulderhit.Play(this.sc.ev, 0f, 0f);
							Vector3 vector4 = Vector3.Zero;
							Vector3 vector5 = lander.position - this.rockInst[num5].mypos;
							if (vector5.LengthSquared() > 0f)
							{
								vector4 = Vector3.Normalize(vector5);
							}
							lander.shockhit = 30;
							bool flag2 = true;
							lander.velocity = vector4 * lander.velocity.Length();
							if (flag2)
							{
								break;
							}
						}
					}
				}
			}
			else if (vehicleindex == 3)
			{
				float num8;
				this.GetHeightOnly(ref heightData, campos, out num8);
				if (campos.Y - num8 < 500f)
				{
					for (int k = 0; k < this.rockClose.Count; k++)
					{
						int num9 = this.rockClose[k];
						float num10 = Vector3.DistanceSquared(this.rockInst[num9].mypos, campos);
						float num11 = 50f + this.rockInst[num9].scaler * 30f;
						if (num10 < num11 * num11)
						{
							boulderhit.Play(this.sc.ev, 0f, 0f);
							Vector3 vector6 = Vector3.Zero;
							Vector3 vector7 = campos - this.rockInst[num9].mypos;
							if (vector7.LengthSquared() > 0f)
							{
								vector6 = Vector3.Normalize(vector7);
							}
							lander.shockhit = 20;
							bool flag3 = true;
							hitVec = vector6 * vec.Length() * 4f;
							if (flag3)
							{
								break;
							}
						}
					}
				}
			}
			for (int l = 0; l < this.rockMoving.Count; l++)
			{
				int num12 = this.rockMoving[l];
				this.rockInst[num12].Update(ref heightData, ref normals, ref objectData);
				this.rockTrans[num12].Trans = this.rockInst[num12].Transform;
				int num13 = 0;
				for (int m = 0; m < this.rockClose.Count; m++)
				{
					int num14 = this.rockClose[m];
					if (num14 != num12)
					{
						float num15 = Vector3.Distance(this.rockInst[num14].mypos, this.rockInst[num12].mypos);
						if (num15 < this.rockInst[num12].scaler * 27f + this.rockInst[num14].scaler * 27f)
						{
							Vector3 vector8 = Vector3.Zero;
							Vector3 vector9 = this.rockInst[num12].mypos - this.rockInst[num14].mypos;
							if (vector9.LengthSquared() > 0f)
							{
								vector8 = Vector3.Normalize(vector9);
							}
							this.rockInst[num12].grav = Vector3.Reflect(this.rockInst[num12].grav, vector8) * 0.5f;
							this.rockInst[num14].move = true;
							if (!this.rockMoving.Contains(num14))
							{
								this.rockMoving.Add(num14);
							}
							objDupe objDupe3 = this.rockInst[num14];
							objDupe3.grav.X = objDupe3.grav.X + -vector8.X * this.rockInst[num12].mass * 4f;
							objDupe objDupe4 = this.rockInst[num14];
							objDupe4.grav.Z = objDupe4.grav.Z + -vector8.Z * this.rockInst[num12].mass * 4f;
							num13++;
							if (num13 > 0)
							{
								break;
							}
						}
					}
				}
				if (!this.rockInst[num12].move || Vector2.Distance(new Vector2(vector.X, vector.Z), new Vector2(this.rockInst[num12].mypos.X, this.rockInst[num12].mypos.Z)) > 13000f)
				{
					this.rockInst[num12].move = false;
					this.rockMoving.Remove(num12);
				}
			}
			for (int n = 0; n < this.rock1Count; n++)
			{
				if (!this.rock1Inst[n].move)
				{
					this.rock1Inst[n] = this.rock1Inst[this.rock1Count - 1];
					this.rock1Inst[n].move = this.rock1Inst[this.rock1Count - 1].move;
					this.rock1Inst[n].mytime = this.rock1Inst[this.rock1Count - 1].mytime;
					this.rock1Inst[n].onramp = this.rock1Inst[this.rock1Count - 1].onramp;
					this.rock1Inst[n].mypos = this.rock1Inst[this.rock1Count - 1].mypos;
					this.rock1Inst[n].scaler = this.rock1Inst[this.rock1Count - 1].scaler;
					this.rock1Inst[n].myRot = this.rock1Inst[this.rock1Count - 1].myRot;
					this.rock1Inst[n].Transform = this.rock1Inst[this.rock1Count - 1].Transform;
					this.rock1Inst[n].velocity = this.rock1Inst[this.rock1Count - 1].velocity;
					this.rock1Inst[n].Transform = this.rock1Inst[this.rock1Count - 1].Transform;
					this.rock1Trans[n].Trans = this.rock1Inst[this.rock1Count - 1].Transform;
					this.rock1Trans[n].Color = this.rock1Trans[this.rock1Count - 1].Color;
					this.rock1Count--;
					break;
				}
			}
			for (int num16 = 0; num16 < this.rock1Count; num16++)
			{
				if (this.rock1Inst[num16].move)
				{
					this.rock1Inst[num16].Update(ref heightData);
					this.rock1Trans[num16].Trans = this.rock1Inst[num16].Transform;
				}
			}
			for (int num17 = 0; num17 < this.rock2Count; num17++)
			{
				if (!this.rock2Inst[num17].move)
				{
					if (this.rock2Inst[num17].onramp == 10 && this.rock2Inst[num17].scaler > 0.5f && this.rock2Count < this.rock2Inst.Length - 2)
					{
						this.rr = new Random((int)this.rock2Inst[num17].mypos.X * 12);
						Vector3 vector10 = new Vector3((float)this.rr.Next(-1000, 1000) / 120f, (float)this.rr.Next(-1000, 1000) / 410f, (float)this.rr.Next(-1000, 1000) / 110f) * this.rock2Inst[num17].scaler;
						Vector3 vector11 = new Vector3((float)this.rr.Next(-1000, 1000) / 320f, (float)this.rr.Next(-1000, 1000) / 500f, (float)this.rr.Next(-1000, 1000) / 320f);
						Matrix matrix = Matrix.CreateFromYawPitchRoll(vector11.X + (float)this.random.Next(-1000, 1000), vector11.X + (float)this.random.Next(-1000, 1000), vector11.X);
						this.rock2Inst[this.rock2Count] = new rockBreak(this.sc, this.rock2Inst[num17].mypos + vector10, this.rock2Inst[num17].scaler / 2f, matrix, this.rock2Inst[num17].velocity + vector11, 5, true);
						this.rock2Trans[this.rock2Count].Trans = this.rock2Inst[this.rock2Count].Transform;
						this.rock2Trans[this.rock2Count].Color = this.rock2Trans[num17].Color;
						this.rock2Count++;
						this.rr = new Random((int)this.rock2Inst[num17].mypos.Z * 76);
						vector10 = new Vector3((float)this.rr.Next(-1000, 1000) / 90f, (float)this.rr.Next(-1000, 1000) / 395f, (float)this.rr.Next(-1000, 1000) / 90f) * this.rock2Inst[num17].scaler;
						vector11 = new Vector3((float)this.rr.Next(-1000, 1000) / 330f, (float)this.rr.Next(-1000, 1000) / 500f, (float)this.rr.Next(-1000, 1000) / 330f);
						matrix = Matrix.CreateFromYawPitchRoll(vector11.X + (float)this.random.Next(-1000, 1000), vector11.X + (float)this.random.Next(-1000, 1000), vector11.X);
						this.rock2Inst[this.rock2Count] = new rockBreak(this.sc, this.rock2Inst[num17].mypos + vector10, this.rock2Inst[num17].scaler / 2f, matrix, this.rock2Inst[num17].velocity + vector11, 5, true);
						this.rock2Trans[this.rock2Count].Trans = this.rock2Inst[this.rock2Count].Transform;
						this.rock2Trans[this.rock2Count].Color = this.rock2Trans[num17].Color;
						this.rock2Count++;
					}
					this.rock2Inst[num17] = this.rock2Inst[this.rock2Count - 1];
					this.rock2Inst[num17].move = this.rock2Inst[this.rock2Count - 1].move;
					this.rock2Inst[num17].mytime = this.rock2Inst[this.rock2Count - 1].mytime;
					this.rock2Inst[num17].onramp = this.rock2Inst[this.rock2Count - 1].onramp;
					this.rock2Inst[num17].mypos = this.rock2Inst[this.rock2Count - 1].mypos;
					this.rock2Inst[num17].scaler = this.rock2Inst[this.rock2Count - 1].scaler;
					this.rock2Inst[num17].myRot = this.rock2Inst[this.rock2Count - 1].myRot;
					this.rock2Inst[num17].Transform = this.rock2Inst[this.rock2Count - 1].Transform;
					this.rock2Inst[num17].velocity = this.rock2Inst[this.rock2Count - 1].velocity;
					this.rock2Trans[num17].Trans = this.rock2Inst[this.rock2Count - 1].Transform;
					this.rock2Trans[num17].Color = this.rock2Trans[this.rock2Count - 1].Color;
					this.rock2Count--;
					break;
				}
			}
			for (int num18 = 0; num18 < this.rock2Count; num18++)
			{
				if (this.rock2Inst[num18].move)
				{
					this.rock2Inst[num18].Update(ref heightData);
					this.rock2Trans[num18].Trans = this.rock2Inst[num18].Transform;
				}
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x001184A8 File Offset: 0x001166A8
		public void splitBoulder(int k, ref Rover rover, ref SoundEffect boulderhit, ref SoundEffect crack, ref int[,] objectData)
		{
			bool flag = this.rockInst[k].gemtype == 0f;
			float scaler = this.rockInst[k].scaler;
			Vector3 vector = this.rockInst[k].mypos + new Vector3(0f, -scaler * 10f, 0f);
			float num = this.rockTrans[k].Color;
			num = 0f;
			Vector3 grav = this.rockInst[k].grav;
			objectData[this.rockInst[k].objx, this.rockInst[k].objz] = 0;
			if (k != this.rockCount)
			{
				this.rockMoving.Remove(k);
				this.rockClose.Remove(k);
				if (this.rockClose.Contains(this.rockCount))
				{
					this.rockClose.Remove(this.rockCount);
					if (!this.rockClose.Contains(k))
					{
						this.rockClose.Add(k);
					}
				}
				if (this.rockInst[this.rockCount].move)
				{
					this.rockMoving.Remove(this.rockCount);
					this.rockMoving.Add(k);
				}
				if (this.rockInst[this.rockCount].onMap)
				{
					this.rockInst[k].onMap = true;
					this.rockInst[k].removeObject();
					this.rockInst[k].objx = this.rockInst[this.rockCount].objx;
					this.rockInst[k].objz = this.rockInst[this.rockCount].objz;
					this.rockInst[k].addObject(27);
					this.rockInst[this.rockCount].onMap = false;
				}
				else
				{
					this.rockInst[k].onMap = false;
					this.rockInst[k].removeObject();
					this.rockInst[k].objx = this.rockInst[this.rockCount].objx;
					this.rockInst[k].objz = this.rockInst[this.rockCount].objz;
					this.rockInst[this.rockCount].removeObject();
					this.rockInst[this.rockCount].onMap = false;
				}
				this.rockInst[k].move = this.rockInst[this.rockCount].move;
				this.rockInst[k].mypos = this.rockInst[this.rockCount].mypos;
				this.rockInst[k].scaler = this.rockInst[this.rockCount].scaler;
				this.rockInst[k].myRot = this.rockInst[this.rockCount].myRot;
				this.rockInst[k].Transform = this.rockInst[this.rockCount].Transform;
				this.rockInst[k].hits = this.rockInst[this.rockCount].hits;
				this.rockInst[k].grav = this.rockInst[this.rockCount].grav;
				this.rockInst[k].gemtype = this.rockInst[this.rockCount].gemtype;
				this.rockTrans[k].Trans = this.rockInst[this.rockCount].Transform;
				this.rockTrans[k].Color = this.rockTrans[this.rockCount].Color;
			}
			else
			{
				this.rockInst[k].removeObject();
				this.rockMoving.Remove(k);
				this.rockClose.Remove(k);
				this.rockInst[k].move = false;
			}
			this.rockCount--;
			if (this.rockCount < 0)
			{
				this.rockCount = 0;
			}
			if (this.rock1Count < this.rock1Inst.Length - 2)
			{
				this.rock1Inst[this.rock1Count] = new rockBreak(this.sc, vector + rover.orientation.Left * (scaler * 12f), scaler, Matrix.CreateFromYawPitchRoll(rover.facingDirection, 0f, 0f), grav * (float)this.random.Next(60, 150) / 100f + rover.orientation.Left * scaler, 5, false);
				this.rock1Trans[this.rock1Count].Trans = this.rock1Inst[this.rock1Count].Transform;
				this.rock1Trans[this.rock1Count].Color = num;
				this.rock1Count++;
				this.rock1Inst[this.rock1Count] = new rockBreak(this.sc, vector + rover.orientation.Right * (scaler * 12f), scaler, Matrix.CreateFromYawPitchRoll(rover.facingDirection + 3.14f, 0f, 0f), grav * (float)this.random.Next(60, 150) / 100f + rover.orientation.Right * scaler, 5, false);
				this.rock1Trans[this.rock1Count].Trans = this.rock1Inst[this.rock1Count].Transform;
				this.rock1Trans[this.rock1Count].Color = num;
				this.rock1Count++;
			}
			if (this.rock2Count < this.rock2Inst.Length - 2)
			{
				Vector3 vector2 = new Vector3((float)(this.random.Next(-1000, 1000) / 200), (float)(this.random.Next(2000, 6000) / 200), (float)(this.random.Next(-1000, 1000) / 200)) * scaler;
				Vector3 vector3 = new Vector3((float)(this.random.Next(-2000, 2000) / 200), (float)(this.random.Next(-2000, 4000) / 200), (float)(this.random.Next(-2000, 2000) / 200));
				Matrix matrix = Matrix.CreateFromYawPitchRoll(vector2.X + (float)this.random.Next(-1000, 1000), vector2.X + (float)this.random.Next(-1000, 1000), vector2.X);
				this.rock2Inst[this.rock2Count] = new rockBreak(this.sc, vector + vector2, scaler / ((float)this.random.Next(120, 250) / 100f), matrix, -grav + vector3, 5, true);
				this.rock2Trans[this.rock2Count].Trans = this.rock2Inst[this.rock2Count].Transform;
				this.rock2Count++;
				vector2 = new Vector3((float)(this.random.Next(-1000, 1000) / 200), (float)(this.random.Next(-1000, 3000) / 200), (float)(this.random.Next(-1000, 1000) / 200)) * scaler;
				vector3 = new Vector3((float)(this.random.Next(-2000, 2000) / 200), (float)(this.random.Next(-2000, 4000) / 200), (float)(this.random.Next(-2000, 2000) / 200));
				matrix = Matrix.CreateFromYawPitchRoll(vector2.X + (float)this.random.Next(-1000, 1000), vector2.X + (float)this.random.Next(-1000, 1000), vector2.X);
				this.rock2Inst[this.rock2Count] = new rockBreak(this.sc, vector + vector2, scaler / ((float)this.random.Next(130, 280) / 100f), matrix, grav + vector3, 5, true);
				this.rock2Trans[this.rock2Count].Trans = this.rock2Inst[this.rock2Count].Transform;
				this.rock2Count++;
				if (flag)
				{
					vector2 = new Vector3((float)(this.random.Next(-1000, 1000) / 200), (float)(this.random.Next(-1000, 3000) / 200), (float)(this.random.Next(-1000, 1000) / 200)) * scaler;
					vector3 = new Vector3((float)(this.random.Next(-2000, 2000) / 200), (float)(this.random.Next(-2000, 4000) / 200), (float)(this.random.Next(-2000, 2000) / 200));
					matrix = Matrix.CreateFromYawPitchRoll(vector2.X + (float)this.random.Next(-1000, 1000), vector2.X + (float)this.random.Next(-1000, 1000), vector2.X);
					this.rock2Inst[this.rock2Count] = new rockBreak(this.sc, vector + vector2, scaler / ((float)this.random.Next(120, 250) / 100f), matrix, grav + vector3, 5, false);
					this.rock2Trans[this.rock2Count].Trans = this.rock2Inst[this.rock2Count].Transform;
					this.rock2Count++;
				}
			}
			if (scaler > 3f)
			{
				crack.Play(this.sc.ev, 0f, 0f);
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00118F2C File Offset: 0x0011712C
		public void placeSLABLET(Vector3 grnd, ref int[,] heightData, ref Vector3[,] normalData, float ratio)
		{
			ratio *= 0.43f;
			float num = 0f;
			float num2 = (float)this.random.Next(50, 800) / 100f;
			int num3 = this.random.Next(2, 5);
			for (int i = 0; i < num3; i++)
			{
				if (this.slab2Count < this.slab2max - 1)
				{
					num += (float)this.random.Next(50, 150) / 100f;
					if (num > 6.28f + num2)
					{
						break;
					}
					Vector3 vector = Vector3.Transform(ratio * Vector3.Right * (float)this.random.Next(1300, 1600) / 10f, Matrix.CreateRotationY(num));
					float num4;
					Vector3 vector2;
					this.GetHeightNormal(ref heightData, ref normalData, grnd + vector, out num4, out vector2);
					this.slab2Count++;
					int num5 = this.slab2Count;
					this.slab2Inst[num5].init(new Vector3(grnd.X + vector.X, num4, grnd.Z + vector.Z), vector2, 1f * ratio, this.random.Next(2, 4000));
					this.slab2Trans[num5].Trans = this.slab2Inst[num5].Transform;
				}
			}
			num = 0f;
			num2 = (float)this.random.Next(50, 800) / 100f;
			num3 = this.random.Next(2, 6);
			for (int j = 0; j < num3; j++)
			{
				if (this.slab2Count < this.slab2max - 1)
				{
					num += (float)this.random.Next(50, 150) / 100f;
					if (num > 6.28f + num2)
					{
						break;
					}
					Vector3 vector3 = Vector3.Transform(ratio * Vector3.Right * (float)this.random.Next(1700, 2300) / 10f, Matrix.CreateRotationY(num));
					float num4;
					Vector3 vector2;
					this.GetHeightNormal(ref heightData, ref normalData, grnd + vector3, out num4, out vector2);
					this.slab2Count++;
					int num6 = this.slab2Count;
					this.slab2Inst[num6].init(new Vector3(grnd.X + vector3.X, num4, grnd.Z + vector3.Z), vector2, 0.8f * ratio, this.random.Next(2, 4000));
					this.slab2Trans[num6].Trans = this.slab2Inst[num6].Transform;
				}
			}
			num = 0f;
			num2 = (float)this.random.Next(50, 800) / 100f;
			num3 = this.random.Next(2, 7);
			for (int k = 0; k < num3; k++)
			{
				if (this.slab2Count < this.slab2max - 1)
				{
					num += (float)this.random.Next(50, 150) / 100f;
					if (num > 6.28f + num2)
					{
						return;
					}
					Vector3 vector4 = Vector3.Transform(ratio * Vector3.Right * (float)this.random.Next(2450, 2700) / 10f, Matrix.CreateRotationY(num));
					float num4;
					Vector3 vector2;
					this.GetHeightNormal(ref heightData, ref normalData, grnd + vector4, out num4, out vector2);
					this.slab2Count++;
					int num7 = this.slab2Count;
					this.slab2Inst[num7].init(new Vector3(grnd.X + vector4.X, num4, grnd.Z + vector4.Z), vector2, 0.5f * ratio, this.random.Next(2, 4000));
					this.slab2Trans[num7].Trans = this.slab2Inst[num7].Transform;
				}
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00119344 File Offset: 0x00117544
		public void placeSLAB(int vehicleindex, ref Rover rover, ref Lander lander, ref int[,] heightData, ref Vector3[,] normalData, ref int[,] objectData)
		{
			Vector3 vector;
			if (vehicleindex == 1)
			{
				vector = rover.position;
			}
			else
			{
				vector = lander.position;
			}
			this.slabInst[0].init(new Vector3(0f, -100000f, 0f), Vector3.Up, this.random.Next(2, 4000));
			this.slabTrans[0].Trans = this.slabInst[0].Transform;
			for (int i = 1; i < this.slabmax; i++)
			{
				float num = vector.X + (float)this.random.Next(-this.acre, this.acre);
				float num2 = vector.Z + (float)this.random.Next(-this.acre, this.acre);
				float num3;
				Vector3 vector2;
				this.GetHeightNormal(ref heightData, ref normalData, new Vector3(num, 0f, num2), out num3, out vector2);
				int num4 = 2000 * this.gridscale;
				float num5 = ((num + 200f) % (float)num4 + 1.5f * (float)num4) % (float)num4;
				float num6 = ((num2 + 200f) % (float)num4 + 1.5f * (float)num4) % (float)num4;
				num5 = (float)((int)num5 / this.gridscale);
				num6 = (float)((int)num6 / this.gridscale);
				int num7 = objectData[(int)num5 / 4, (int)num6 / 4];
				if (vector2.Y <= this.maxAngle && vector2.Y >= this.minAngle && num7 == 5)
				{
					this.slabCount++;
					this.slabInst[this.slabCount].init(new Vector3(num, num3, num2), vector2, this.random.Next(2, 4000));
					this.slabTrans[this.slabCount].Trans = this.slabInst[this.slabCount].Transform;
					if (this.slab2Count < this.slab2max - 1)
					{
						this.placeSLABLET(new Vector3(num, num3, num2), ref heightData, ref normalData, this.slabInst[this.slabCount].scaler);
					}
				}
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0011956C File Offset: 0x0011776C
		public void updateSLAB(int frame, int vehicleindex, Vector3 grnd, ref int[,] heightData, ref Vector3[,] normalData, ref int[,] objectData)
		{
			int num = frame % 50;
			int num2 = 1 + num;
			while (num2 <= this.slabCount && num2 <= this.slabCount)
			{
				float num3 = Vector2.DistanceSquared(new Vector2(grnd.X, grnd.Z), new Vector2(this.slabInst[num2].mypos.X, this.slabInst[num2].mypos.Z));
				if (num3 > (float)((this.acre + 9000) * (this.acre + 9000)))
				{
					if (num2 != this.slabCount)
					{
						this.slabInst[num2].move = this.slabInst[this.slabCount].move;
						this.slabInst[num2].mypos = this.slabInst[this.slabCount].mypos;
						this.slabInst[num2].scaler = this.slabInst[this.slabCount].scaler;
						this.slabInst[num2].myRot = this.slabInst[this.slabCount].myRot;
						this.slabInst[num2].Transform = this.slabInst[this.slabCount].Transform;
						this.slabInst[num2].hits = this.slabInst[this.slabCount].hits;
						this.slabInst[num2].grav = this.slabInst[this.slabCount].grav;
						this.slabTrans[num2].Trans = this.slabInst[this.slabCount].Transform;
					}
					this.slabCount--;
					if (this.slabCount < 0)
					{
						this.slabCount = 0;
					}
				}
				num2 += 50;
			}
			int num4 = 1 + num;
			while (num4 <= this.slab2Count && num4 <= this.slab2Count)
			{
				float num5 = Vector2.DistanceSquared(new Vector2(grnd.X, grnd.Z), new Vector2(this.slab2Inst[num4].mypos.X, this.slab2Inst[num4].mypos.Z));
				if (num5 > (float)((this.acre + 5000) * (this.acre + 5000)))
				{
					if (num4 != this.slab2Count)
					{
						this.slab2Inst[num4].move = this.slab2Inst[this.slab2Count].move;
						this.slab2Inst[num4].mypos = this.slab2Inst[this.slab2Count].mypos;
						this.slab2Inst[num4].scaler = this.slab2Inst[this.slab2Count].scaler;
						this.slab2Inst[num4].myRot = this.slab2Inst[this.slab2Count].myRot;
						this.slab2Inst[num4].Transform = this.slab2Inst[this.slab2Count].Transform;
						this.slab2Inst[num4].hits = this.slab2Inst[this.slab2Count].hits;
						this.slab2Inst[num4].grav = this.slab2Inst[this.slab2Count].grav;
						this.slab2Trans[num4].Trans = this.slab2Inst[this.slab2Count].Transform;
					}
					this.slab2Count--;
					if (this.slab2Count < 0)
					{
						this.slab2Count = 0;
					}
				}
				num4 += 50;
			}
			this.slabPOS.X = grnd.X;
			this.slabPOS.Y = grnd.Z;
			float num6 = Math.Abs(this.slabPOS.X - this.oldslabPOS.X);
			float num7 = Math.Abs(this.slabPOS.Y - this.oldslabPOS.Y);
			if (num6 > 300f || num7 > 300f)
			{
				if (this.slabCount < this.slabmax - 1)
				{
					float num8;
					float num9;
					if (num6 >= num7)
					{
						if (this.slabPOS.X > this.oldslabPOS.X)
						{
							num8 = grnd.X + (float)this.acre;
							num9 = grnd.Z + (float)this.random.Next(-this.acre, this.acre);
						}
						else
						{
							num8 = grnd.X - (float)this.acre;
							num9 = grnd.Z + (float)this.random.Next(-this.acre, this.acre);
						}
					}
					else if (this.slabPOS.Y > this.oldslabPOS.Y)
					{
						num8 = grnd.X + (float)this.random.Next(-this.acre, this.acre);
						num9 = grnd.Z + (float)this.acre;
					}
					else
					{
						num8 = grnd.X + (float)this.random.Next(-this.acre, this.acre);
						num9 = grnd.Z - (float)this.acre;
					}
					float num10;
					Vector3 vector;
					this.GetHeightNormal(ref heightData, ref normalData, new Vector3(num8, 0f, num9), out num10, out vector);
					int num11 = 2000 * this.gridscale;
					float num12 = ((num8 + 200f) % (float)num11 + 1.5f * (float)num11) % (float)num11;
					float num13 = ((num9 + 200f) % (float)num11 + 1.5f * (float)num11) % (float)num11;
					num12 = (float)((int)num12 / this.gridscale);
					num13 = (float)((int)num13 / this.gridscale);
					int num14 = objectData[(int)num12 / 4, (int)num13 / 4];
					if (vector.Y <= this.maxAngle && vector.Y >= this.minAngle && num14 == 5)
					{
						this.slabCount++;
						int num15 = this.slabCount;
						this.slabInst[num15].init(new Vector3(num8, num10, num9), vector, this.random.Next(2, 4000));
						this.slabTrans[num15].Trans = this.slabInst[num15].Transform;
						if (this.slab2Count < this.slab2max - 1)
						{
							this.placeSLABLET(new Vector3(num8, num10, num9), ref heightData, ref normalData, this.slabInst[this.slabCount].scaler);
						}
					}
				}
				this.oldslabPOS = this.slabPOS;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00119BDC File Offset: 0x00117DDC
		public void drawObjects(Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj)
		{
			this.DrawInstance(this.rock, this.rockTrans, this.rockCount, this.rockBuffer, light, amb, diff, view, proj);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00119C10 File Offset: 0x00117E10
		public void drawFlowers(Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj)
		{
			this.DrawInstanceFlower(this.flower, this.flowerTrans, this.flowerCount, this.flowerBuffer, light, amb, diff, view, proj);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00119C44 File Offset: 0x00117E44
		public void drawSlabs(Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj, Vector3 campos)
		{
			this.DrawInstanceSlab(this.slab, this.slabTrans, this.slabCount, this.slabBuffer, light, amb, diff, view, proj, campos);
			this.DrawInstanceSlab(this.slablet, this.slab2Trans, this.slab2Count, this.slab2Buffer, light, amb, diff, view, proj, campos);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00119CA0 File Offset: 0x00117EA0
		public void drawRockRubble(Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj)
		{
			this.DrawInstance2(this.rock1, this.rock1Trans, this.rock1Count, this.rock1Buffer, light, amb, diff, view, proj);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00119CD4 File Offset: 0x00117ED4
		public void drawBrokenSides(Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj)
		{
			this.DrawInstance2(this.rock2, this.rock2Trans, this.rock2Count, this.rock2Buffer, light, amb, diff, view, proj);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00119D08 File Offset: 0x00117F08
		public void DrawInstanceSlab(Model model, rockstruct.transcolor[] minstances, int cc, DynamicVertexBuffer dd, Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj, Vector3 campos)
		{
			if (cc == 0)
			{
				return;
			}
			ModelMeshPart modelMeshPart = model.Meshes[0].MeshParts[0];
			dd.SetData<rockstruct.transcolor>(minstances, 1, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(dd, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.gr.Indices = modelMeshPart.IndexBuffer;
			Effect effect = modelMeshPart.Effect;
			this.frame++;
			effect.CurrentTechnique = effect.Techniques["Reflection1"];
			effect.Parameters["ReflectionMap"].SetValue(this.reflection);
			effect.Parameters["slide"].SetValue((float)this.frame / 150f);
			effect.Parameters["CameraPosition"].SetValue(campos);
			effect.Parameters["View"].SetValue(view);
			effect.Parameters["Projection"].SetValue(proj);
			effect.Parameters["LightDirection"].SetValue(light);
			effect.Parameters["diff"].SetValue(diff);
			effect.Parameters["amb"].SetValue(amb);
			effect.CurrentTechnique.Passes[0].Apply();
			this.gr.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00119ECC File Offset: 0x001180CC
		public void DrawInstanceFlower(Model model, rockstruct.transcolor[] minstances, int cc, DynamicVertexBuffer dd, Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj)
		{
			if (cc == 0)
			{
				return;
			}
			ModelMeshPart modelMeshPart = model.Meshes[0].MeshParts[0];
			dd.SetData<rockstruct.transcolor>(minstances, 1, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(dd, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.gr.Indices = modelMeshPart.IndexBuffer;
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques["fastShaderflower"];
			effect.Parameters["View"].SetValue(view);
			effect.Parameters["Projection"].SetValue(proj);
			effect.Parameters["LightDirection"].SetValue(light);
			effect.Parameters["diff"].SetValue(diff);
			effect.Parameters["amb"].SetValue(amb);
			effect.CurrentTechnique.Passes[0].Apply();
			this.gr.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0011A02C File Offset: 0x0011822C
		public void DrawInstance(Model model, rockstruct.transcolor[] minstances, int cc, DynamicVertexBuffer dd, Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj)
		{
			if (cc == 0)
			{
				return;
			}
			ModelMeshPart modelMeshPart = model.Meshes[0].MeshParts[0];
			dd.SetData<rockstruct.transcolor>(minstances, 1, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(dd, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.gr.Indices = modelMeshPart.IndexBuffer;
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques["fastShader"];
			effect.Parameters["View"].SetValue(view);
			effect.Parameters["Projection"].SetValue(proj);
			effect.Parameters["LightDirection"].SetValue(light);
			effect.Parameters["diff"].SetValue(diff);
			effect.Parameters["amb"].SetValue(amb);
			effect.CurrentTechnique.Passes[0].Apply();
			this.gr.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0011A18C File Offset: 0x0011838C
		public void DrawInstance2(Model model, rockstruct.transcolor[] minstances, int cc, DynamicVertexBuffer dd, Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj)
		{
			if (cc == 0)
			{
				return;
			}
			ModelMeshPart modelMeshPart = model.Meshes[0].MeshParts[0];
			dd.SetData<rockstruct.transcolor>(minstances, 0, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(dd, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.gr.Indices = modelMeshPart.IndexBuffer;
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques["fastShader"];
			effect.Parameters["View"].SetValue(view);
			effect.Parameters["Projection"].SetValue(proj);
			effect.Parameters["LightDirection"].SetValue(light);
			effect.Parameters["diff"].SetValue(diff);
			effect.Parameters["amb"].SetValue(amb);
			effect.CurrentTechnique.Passes[0].Apply();
			this.gr.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0011A2EC File Offset: 0x001184EC
		public void GetHeightOnly(ref int[,] heightData, Vector3 position, out float height)
		{
			int gridScale = this.sc.gridScale;
			int num = this.sc.bitmap;
			int num2 = (num - 1) * gridScale;
			position.X = (position.X % (float)num2 + 1.5f * (float)num2) % (float)num2;
			position.Z = (position.Z % (float)num2 + 1.5f * (float)num2) % (float)num2;
			Vector3 vector = position;
			int num3 = (int)vector.X / gridScale;
			int num4 = (int)vector.Z / gridScale;
			float num5 = vector.X % (float)gridScale / (float)gridScale;
			float num6 = vector.Z % (float)gridScale / (float)gridScale;
			int num7 = num3 + 1;
			int num8 = num4 + 1;
			if (num7 > num - 2)
			{
				num7 = 0;
			}
			if (num8 > num - 2)
			{
				num8 = 0;
			}
			float num9 = MathHelper.Lerp((float)heightData[num3, num4], (float)heightData[num7, num4], num5);
			float num10 = MathHelper.Lerp((float)heightData[num3, num8], (float)heightData[num7, num8], num5);
			height = MathHelper.Lerp(num9, num10, num6);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0011A3F8 File Offset: 0x001185F8
		private void GetHeightNormalX(ref int[,] heightData, ref Vector3[,] normals, Vector3 position, out float height, out Vector3 normal)
		{
			int gridScale = this.sc.gridScale;
			int num = this.sc.bitmap;
			int num2 = (num - 1) * gridScale;
			position.X = (position.X % (float)num2 + 1.5f * (float)num2) % (float)num2;
			position.Z = (position.Z % (float)num2 + 1.5f * (float)num2) % (float)num2;
			Vector3 vector = position;
			int num3 = (int)vector.X / gridScale;
			int num4 = (int)vector.Z / gridScale;
			float num5 = vector.X % (float)gridScale / (float)gridScale;
			float num6 = vector.Z % (float)gridScale / (float)gridScale;
			int num7 = num3 + 1;
			int num8 = num4 + 1;
			if (num7 > num - 2)
			{
				num7 = 0;
			}
			if (num8 > num - 2)
			{
				num8 = 0;
			}
			float num9 = MathHelper.Lerp((float)heightData[num3, num4], (float)heightData[num7, num4], num5);
			float num10 = MathHelper.Lerp((float)heightData[num3, num8], (float)heightData[num7, num8], num5);
			height = MathHelper.Lerp(num9, num10, num6);
			Vector3 vector2 = Vector3.Lerp(normals[num3, num4], normals[num7, num4], num5);
			Vector3 vector3 = Vector3.Lerp(normals[num3, num8], normals[num7, num8], num5);
			normal = Vector3.Lerp(vector2, vector3, num6);
			if (normal.LengthSquared() > 0f)
			{
				normal = Vector3.Normalize(normal);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0011A58C File Offset: 0x0011878C
		public void GetHeightNormal(ref int[,] heightData, ref Vector3[,] normals, Vector3 position, out float height, out Vector3 normal)
		{
			int gridScale = this.sc.gridScale;
			int num = this.sc.bitmap;
			int num2 = (num - 1) * gridScale;
			position.X = (position.X % (float)num2 + 1.5f * (float)num2) % (float)num2;
			position.Z = (position.Z % (float)num2 + 1.5f * (float)num2) % (float)num2;
			Vector3 vector = position;
			int num3 = (int)vector.X / gridScale;
			int num4 = (int)vector.Z / gridScale;
			float num5 = vector.X % (float)gridScale / (float)gridScale;
			float num6 = vector.Z % (float)gridScale / (float)gridScale;
			int num7 = num3 + 1;
			int num8 = num4 + 1;
			if (num7 > num - 2)
			{
				num7 = 0;
			}
			if (num8 > num - 2)
			{
				num8 = 0;
			}
			Vector3 vector2 = new Vector3((float)num3, (float)heightData[num3, num4], (float)num4);
			if (num5 + num6 >= 1f)
			{
				vector2 = new Vector3((float)num7, (float)heightData[num7, num8], (float)num8);
			}
			Vector3 vector3 = new Vector3((float)num3, (float)heightData[num3, num8], (float)num8);
			Vector3 vector4 = new Vector3((float)num7, (float)heightData[num7, num4], (float)num4);
			Vector2 vector5 = new Vector2(vector.X / (float)gridScale, vector.Z / (float)gridScale);
			float num9 = (vector3.Z - vector4.Z) * (vector2.X - vector4.X) + (vector4.X - vector3.X) * (vector2.Z - vector4.Z);
			float num10 = ((vector3.Z - vector4.Z) * (vector5.X - vector4.X) + (vector4.X - vector3.X) * (vector5.Y - vector4.Z)) / num9;
			float num11 = ((vector4.Z - vector2.Z) * (vector5.X - vector4.X) + (vector2.X - vector4.X) * (vector5.Y - vector4.Z)) / num9;
			float num12 = 1f - num10 - num11;
			height = num10 * vector2.Y + num11 * vector3.Y + num12 * vector4.Y;
			if (num5 + num6 > 1f)
			{
				vector2 = new Vector3((float)num7, (float)heightData[num7, num8], (float)num8);
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

		// Token: 0x060004C7 RID: 1223 RVA: 0x0011A870 File Offset: 0x00118A70
		public void GetHeightNormalReturns(ref int[,] heightData, ref Vector3[,] normals, Vector3 position, out float height, out int objx, out int objz)
		{
			int gridScale = this.sc.gridScale;
			int num = this.sc.bitmap;
			int num2 = (num - 1) * gridScale;
			position.X = (position.X % (float)num2 + 1.5f * (float)num2) % (float)num2;
			position.Z = (position.Z % (float)num2 + 1.5f * (float)num2) % (float)num2;
			Vector3 vector = position;
			int num3 = (int)vector.X / gridScale;
			int num4 = (int)vector.Z / gridScale;
			objx = num3;
			objz = num4;
			float num5 = vector.X % (float)gridScale / (float)gridScale;
			float num6 = vector.Z % (float)gridScale / (float)gridScale;
			int num7 = num3 + 1;
			int num8 = num4 + 1;
			if (num7 > num - 2)
			{
				num7 = 0;
			}
			if (num8 > num - 2)
			{
				num8 = 0;
			}
			Vector3 vector2 = new Vector3((float)num3, (float)heightData[num3, num4], (float)num4);
			if (num5 + num6 >= 1f)
			{
				vector2 = new Vector3((float)num7, (float)heightData[num7, num8], (float)num8);
			}
			Vector3 vector3 = new Vector3((float)num3, (float)heightData[num3, num8], (float)num8);
			Vector3 vector4 = new Vector3((float)num7, (float)heightData[num7, num4], (float)num4);
			Vector2 vector5 = new Vector2(vector.X / (float)gridScale, vector.Z / (float)gridScale);
			float num9 = (vector3.Z - vector4.Z) * (vector2.X - vector4.X) + (vector4.X - vector3.X) * (vector2.Z - vector4.Z);
			float num10 = ((vector3.Z - vector4.Z) * (vector5.X - vector4.X) + (vector4.X - vector3.X) * (vector5.Y - vector4.Z)) / num9;
			float num11 = ((vector4.Z - vector2.Z) * (vector5.X - vector4.X) + (vector2.X - vector4.X) * (vector5.Y - vector4.Z)) / num9;
			float num12 = 1f - num10 - num11;
			height = num10 * vector2.Y + num11 * vector3.Y + num12 * vector4.Y;
		}

		// Token: 0x040012FD RID: 4861
		public float flowerDistance;

		// Token: 0x040012FE RID: 4862
		private int frame;

		// Token: 0x040012FF RID: 4863
		private int acre = 21000;

		// Token: 0x04001300 RID: 4864
		public Model rock;

		// Token: 0x04001301 RID: 4865
		public Model rockB;

		// Token: 0x04001302 RID: 4866
		public Model flower;

		// Token: 0x04001303 RID: 4867
		public int maxrockCount;

		// Token: 0x04001304 RID: 4868
		public int maxflowerCount;

		// Token: 0x04001305 RID: 4869
		public objDupe[] rockInst;

		// Token: 0x04001306 RID: 4870
		public objDupe[] flowerInst;

		// Token: 0x04001307 RID: 4871
		public int chosen = -1;

		// Token: 0x04001308 RID: 4872
		public int chosenfar = -1;

		// Token: 0x04001309 RID: 4873
		public int rockCount;

		// Token: 0x0400130A RID: 4874
		public int flowerCount;

		// Token: 0x0400130B RID: 4875
		public List<int> rockMoving = new List<int>();

		// Token: 0x0400130C RID: 4876
		public List<int> rockClose = new List<int>();

		// Token: 0x0400130D RID: 4877
		public Vector2 myPOS = new Vector2(0f, 0f);

		// Token: 0x0400130E RID: 4878
		public Vector2 oldmyPOS = new Vector2(0f, 0f);

		// Token: 0x0400130F RID: 4879
		public Model slab;

		// Token: 0x04001310 RID: 4880
		public Model slablet;

		// Token: 0x04001311 RID: 4881
		public int slabmax;

		// Token: 0x04001312 RID: 4882
		public int slab2max;

		// Token: 0x04001313 RID: 4883
		public Texture2D reflection;

		// Token: 0x04001314 RID: 4884
		public slabDupe[] slabInst;

		// Token: 0x04001315 RID: 4885
		public slabDupe2[] slab2Inst;

		// Token: 0x04001316 RID: 4886
		public int slabCount;

		// Token: 0x04001317 RID: 4887
		public int slab2Count;

		// Token: 0x04001318 RID: 4888
		public Vector2 slabPOS = new Vector2(0f, 0f);

		// Token: 0x04001319 RID: 4889
		public Vector2 oldslabPOS = new Vector2(0f, 0f);

		// Token: 0x0400131A RID: 4890
		private float minAngle;

		// Token: 0x0400131B RID: 4891
		private float maxAngle;

		// Token: 0x0400131C RID: 4892
		private Random rr;

		// Token: 0x0400131D RID: 4893
		public int maxcount2 = 20;

		// Token: 0x0400131E RID: 4894
		public Model rock1;

		// Token: 0x0400131F RID: 4895
		public Model rockB1;

		// Token: 0x04001320 RID: 4896
		public Model rockB2;

		// Token: 0x04001321 RID: 4897
		public rockBreak[] rock1Inst = new rockBreak[40];

		// Token: 0x04001322 RID: 4898
		public int rock1Count;

		// Token: 0x04001323 RID: 4899
		public Model rock2;

		// Token: 0x04001324 RID: 4900
		public rockBreak[] rock2Inst = new rockBreak[40];

		// Token: 0x04001325 RID: 4901
		public int rock2Count;

		// Token: 0x04001326 RID: 4902
		private Random random;

		// Token: 0x04001327 RID: 4903
		private int gridscale;

		// Token: 0x04001328 RID: 4904
		private int bitmap;

		// Token: 0x04001329 RID: 4905
		private int hectare = 2000;

		// Token: 0x0400132A RID: 4906
		private int grid = 150;

		// Token: 0x0400132B RID: 4907
		private readonly VertexBufferBinding[] _vertexBufferBindings = new VertexBufferBinding[2];

		// Token: 0x0400132C RID: 4908
		private DynamicVertexBuffer rockBuffer;

		// Token: 0x0400132D RID: 4909
		private DynamicVertexBuffer rock1Buffer;

		// Token: 0x0400132E RID: 4910
		private DynamicVertexBuffer rock2Buffer;

		// Token: 0x0400132F RID: 4911
		private DynamicVertexBuffer slabBuffer;

		// Token: 0x04001330 RID: 4912
		private DynamicVertexBuffer slab2Buffer;

		// Token: 0x04001331 RID: 4913
		private DynamicVertexBuffer flowerBuffer;

		// Token: 0x04001332 RID: 4914
		private static VertexDeclaration vd = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
		});

		// Token: 0x04001333 RID: 4915
		private rockstruct.transcolor[] rockTrans;

		// Token: 0x04001334 RID: 4916
		private rockstruct.transcolor[] rock1Trans;

		// Token: 0x04001335 RID: 4917
		private rockstruct.transcolor[] rock2Trans;

		// Token: 0x04001336 RID: 4918
		private rockstruct.transcolor[] slabTrans;

		// Token: 0x04001337 RID: 4919
		private rockstruct.transcolor[] slab2Trans;

		// Token: 0x04001338 RID: 4920
		public rockstruct.transcolor[] flowerTrans;

		// Token: 0x04001339 RID: 4921
		private ScreenManager sc;

		// Token: 0x0400133A RID: 4922
		private GraphicsDevice gr;

		// Token: 0x0400133B RID: 4923
		private ContentManager content;

		// Token: 0x0400133C RID: 4924
		private float thisframe;

		// Token: 0x02000089 RID: 137
		public struct transcolor : IVertexType
		{
			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060004CA RID: 1226 RVA: 0x0011AC26 File Offset: 0x00118E26
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return rockstruct.transcolor.VertexDeclaration;
				}
			}

			// Token: 0x0400133D RID: 4925
			public Matrix Trans;

			// Token: 0x0400133E RID: 4926
			public float Color;

			// Token: 0x0400133F RID: 4927
			private static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
			});
		}
	}
}
