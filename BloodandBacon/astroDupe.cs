using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000E2 RID: 226
	public class astroDupe
	{
		// Token: 0x060007C7 RID: 1991 RVA: 0x001CB138 File Offset: 0x001C9338
		public astroDupe(Vector3 startpos, int myMove, int seed, int age, ScreenManager sc, astroDupe.emotion emo, float myrot)
		{
			this.terrainCh = new int[] { this.walkClip, this.waitClip, this.hammerClip, this.lookClip };
			this.strandedCh = new int[] { this.waitClip, this.lookClip, this.waveClip, this.wave2Clip };
			this.savedCh = new int[] { this.dance1Clip };
			this.sc = sc;
			this.emo = emo;
			this.loc = astroDupe.where.outofTruck;
			this.random = new Random(seed);
			this.botStart = 0f;
			this.bodytype = 1;
			if (this.random.Next(1, 500) < 200)
			{
				this.bodytype = 2;
			}
			this.headType = 1;
			this.packType = this.random.Next(1, 3);
			this.blood = 0;
			this.mypos = startpos;
			this.move = myMove;
			this.tween = 1f;
			this.age = age;
			this.seed = seed;
			this.tint = this.random.Next(0, 7);
			this.scale = 1f;
			this.startHealth = this.health;
			this.myRot = myrot;
			this.speed = this.scale * (astroDupe.acc / 1000f);
			this.turn = (float)this.random.Next(-20, 20) / 1000f;
			this.timer = (float)this.random.Next(20, 300);
			this.clip1 = 0;
			this.clip2 = 0;
			this.timer = 300f;
			this.temp1 = this.random.Next(20, 30000);
			this.temp2 = this.temp1;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x001CB434 File Offset: 0x001C9634
		public astroDupe(Vector3 startpos, int myMove, int seed, int age, ScreenManager sc, astroDupe.emotion emo)
		{
			this.terrainCh = new int[] { this.walkClip, this.waitClip, this.hammerClip, this.lookClip };
			this.strandedCh = new int[] { this.waitClip, this.lookClip, this.waveClip, this.wave2Clip };
			this.savedCh = new int[] { this.dance1Clip };
			this.sc = sc;
			this.emo = emo;
			this.loc = astroDupe.where.outofTruck;
			this.random = new Random(seed);
			this.botStart = 0f;
			this.bodytype = 1;
			if (this.random.Next(1, 500) < 200)
			{
				this.bodytype = 2;
			}
			this.headType = 1;
			if (this.emo == astroDupe.emotion.stranded)
			{
				this.headType = 2;
			}
			if (this.emo == astroDupe.emotion.stranded2)
			{
				this.headType = 1;
				this.emo = astroDupe.emotion.stranded;
			}
			this.packType = this.random.Next(1, 3);
			this.blood = 0;
			this.mypos = startpos;
			this.move = myMove;
			this.tween = 1f;
			this.age = age;
			this.seed = seed;
			this.tint = this.random.Next(0, 7);
			this.scale = 1f;
			this.startHealth = this.health;
			this.myRot = (float)this.random.Next(-800, 800) / 100f;
			this.speed = this.scale * (astroDupe.acc / 1000f);
			this.turn = (float)this.random.Next(-20, 20) / 1000f;
			this.timer = (float)this.random.Next(20, 300);
			this.clip1 = 0;
			this.clip2 = 0;
			this.timer = 300f;
			this.temp1 = this.random.Next(20, 30000);
			this.temp2 = this.temp1;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x001CB770 File Offset: 0x001C9970
		public void updateFacilityAI(ref int[,] heights, ref Vector3[,] normalData)
		{
			if (this.botPath.Count <= 0)
			{
				return;
			}
			if (this.move == 2)
			{
				this.temp1++;
				this.temp2++;
				this.frame1 = this.temp1 % astroDupe.sics[this.clip1 * 2] + astroDupe.sics[this.clip1 * 2 + 1];
				this.frame2 = this.temp2 % astroDupe.sics[this.clip2 * 2] + astroDupe.sics[this.clip2 * 2 + 1];
				this.tween += 0.05f;
				this.GetHeight2(ref heights, this.mypos, out this.groundHeight);
				this.mypos.Y = this.groundHeight;
				float num = (float)((this.botPath.Count - 1) * 400);
				float num2 = 1f / num;
				if (this.speed < 2f)
				{
					this.botStart += num2 * (this.speed * 4f);
				}
				else
				{
					this.botStart += num2 * (this.speed * 5f);
				}
				if (this.botStart < 1f)
				{
					float num3 = (float)(this.botPath.Count - 1) * this.botStart;
					int num4 = (int)num3;
					int num5 = num4 + 1;
					float num6 = MathHelper.Lerp(this.botPath[num4].X, this.botPath[num5].X, num3 - (float)num4);
					float num7 = MathHelper.Lerp(this.botPath[num4].Y, this.botPath[num5].Y, num3 - (float)num4);
					num6 /= 4f;
					num6 += Facility.offset.X;
					num7 /= 4f;
					num7 += Facility.offset.Z;
					float num8 = num6 - this.mypos.X;
					float num9 = num7 - this.mypos.Z;
					if (num6 - this.mypos.X > 0.2f)
					{
						num9 += 15f;
					}
					if (num6 - this.mypos.X < -0.2f)
					{
						num9 -= 15f;
					}
					if (num7 - this.mypos.Z > 0.2f)
					{
						num8 -= 15f;
					}
					if (num7 - this.mypos.Z < -0.2f)
					{
						num8 += 15f;
					}
					float num10 = (float)Math.Atan2((double)num8, (double)num9) + 0f;
					float num11 = 0.04f;
					if (this.speed == 3f)
					{
						num11 = 0.13f;
					}
					float num12 = astroDupe.WrapAngle(num10 - this.myRot);
					num12 = MathHelper.Clamp(num12, -num11, num11);
					this.myRot = astroDupe.WrapAngle(this.myRot + num12);
					float num13 = Vector2.Distance(new Vector2(num6, num7), new Vector2(this.mypos.X, this.mypos.Z));
					num13 = MathHelper.Clamp(num13 / 50f, 0.5f, 1f);
					if (num13 > 200f)
					{
						num13 = 1.2f;
					}
					this.mypos += Vector3.Transform(new Vector3(0f, 0f, 1f) * this.speed * num13, Matrix.CreateRotationY(this.myRot));
				}
				else
				{
					this.botStart = 0f;
					this.move = 3;
				}
			}
			if (this.move == 5)
			{
				this.temp1++;
				this.temp2++;
				this.frame1 = this.temp1 % astroDupe.sics[this.clip1 * 2] + astroDupe.sics[this.clip1 * 2 + 1];
				this.frame2 = this.temp2 % astroDupe.sics[this.clip2 * 2] + astroDupe.sics[this.clip2 * 2 + 1];
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x001CBBB0 File Offset: 0x001C9DB0
		public void UpdateNormally(ref int[,] heightData, ref Vector3[,] normalData)
		{
			this.age++;
			if (this.move == 1)
			{
				if (this.clip1 != 2)
				{
					if (this.loc != astroDupe.where.enteringTruck)
					{
						this.mypos.X = this.mypos.X + -(float)Math.Cos((double)(this.myRot + 1.5708f)) * this.speed;
						this.mypos.Z = this.mypos.Z + (float)Math.Sin((double)(this.myRot + 1.5708f)) * this.speed;
						this.GetHeight(ref heightData, this.mypos, out this.groundHeight);
						this.mypos.Y = this.groundHeight;
						this.myRot += this.turn;
						this.timer -= 1f;
						this.tween += this.tweenspeed;
						if ((double)this.tween >= 0.9)
						{
							this.holdlastclip = false;
						}
						if (this.timer <= 0f)
						{
							if (this.loc == astroDupe.where.outofTruck)
							{
								this.terrainChooseClip(0);
							}
							if (this.loc == astroDupe.where.leavingTruck)
							{
								this.loc = astroDupe.where.outofTruck;
								this.holdlastclip = true;
								this.terrainChooseClip(this.turnfastClip);
							}
						}
					}
					if (this.loc == astroDupe.where.enteringTruck)
					{
						this.tween += 0.05f;
						float x = this.destination.X;
						float z = this.destination.Z;
						float num = x - this.mypos.X;
						float num2 = z - this.mypos.Z;
						if (x - this.mypos.X > 0.2f)
						{
							num2 += 15f;
						}
						if (x - this.mypos.X < -0.2f)
						{
							num2 -= 15f;
						}
						if (z - this.mypos.Z > 0.2f)
						{
							num -= 15f;
						}
						if (z - this.mypos.Z < -0.2f)
						{
							num += 15f;
						}
						float num3 = (float)Math.Atan2((double)num, (double)num2) + 0f;
						float num4 = astroDupe.WrapAngle(num3 - this.myRot);
						num4 = MathHelper.Clamp(num4, -0.04f, 0.04f);
						this.myRot = astroDupe.WrapAngle(this.myRot + num4);
						float num5 = Vector2.Distance(new Vector2(x, z), new Vector2(this.mypos.X, this.mypos.Z));
						float num6 = MathHelper.Clamp(num5 / 200f, 0.8f, 1f);
						if (num5 > 40f)
						{
							this.mypos += Vector3.Transform(new Vector3(0f, 0f, 1f) * this.speed * num6, Matrix.CreateRotationY(this.myRot));
							this.GetHeight(ref heightData, this.mypos, out this.groundHeight);
							this.mypos.Y = this.groundHeight;
						}
						if (num5 <= 40f)
						{
							this.isturning = false;
							this.speed = 0f;
							this.turn = 0f;
							this.timer = 234f;
							this.temp1 = 0;
							this.temp2 = 0;
							this.holdlastframe = true;
							this.loc = astroDupe.where.inTruck;
							this.tweenspeed = 0.1f;
							this.switchClip(this.climbinClip);
						}
					}
				}
				else if (this.clip1 == 2)
				{
					this.flipTimer += 0.03f;
					this.mypos += this.flipVeloc;
					this.tween += 0.05f;
					this.GetHeight(ref heightData, this.mypos, out this.groundHeight);
					if (this.mypos.Y < this.groundHeight)
					{
						this.flipVeloc.Y = this.flipVeloc.Y + 0.5f;
						this.mypos.Y = this.groundHeight;
					}
				}
				if (!this.holdlastframe || this.timer > 0f)
				{
					this.temp1++;
					if (!this.holdlastclip)
					{
						this.temp2++;
					}
					this.frame1 = this.temp1 % astroDupe.sics[this.clip1 * 2] + astroDupe.sics[this.clip1 * 2 + 1];
					this.frame2 = this.temp2 % astroDupe.sics[this.clip2 * 2] + astroDupe.sics[this.clip2 * 2 + 1];
				}
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x001CC064 File Offset: 0x001CA264
		public void terrainChooseClip(int myact)
		{
			if (!this.flipinspace)
			{
				if (this.isturning)
				{
					this.isturning = false;
					this.timer = (float)this.random.Next(240, 530);
					this.turn = (float)this.random.Next(-7, 7) / 1000f;
					this.speed = this.scale * (astroDupe.acc / 1000f);
				}
				else
				{
					int num = this.terrainCh[this.random.Next(0, this.terrainCh.Length)];
					if (this.emo == astroDupe.emotion.stranded)
					{
						num = this.strandedCh[this.random.Next(0, this.strandedCh.Length)];
					}
					if (myact != 0)
					{
						num = myact;
					}
					if (this.nextact != 0)
					{
						num = this.nextact;
						this.nextact = 0;
					}
					if (num == this.walkClip)
					{
						this.timer = (float)this.random.Next(250, 800);
						this.turn = (float)this.random.Next(-9, 9) / 1000f;
						if (this.random.Next(1, 1000) < 90)
						{
							this.turn = (float)this.random.Next(-20, 20) / 1000f;
						}
						this.speed = this.scale * (astroDupe.acc / 1000f);
						this.tweenspeed = 0.05f;
						this.switchClip(this.walkClip);
					}
					if (num == this.walkawayfromtruckClip)
					{
						this.timer = (float)this.random.Next(300, 440);
						this.turn = 0f;
						this.speed = this.scale * (astroDupe.acc / 1000f);
						this.isturning = false;
						this.tweenspeed = 0.05f;
						this.holdlastclip = true;
						this.loc = astroDupe.where.outofTruck;
						this.switchClip(this.walkClip);
						if (this.emo == astroDupe.emotion.stranded)
						{
							if (Vector3.Distance(this.mypos, astroDupe.farmLocation) < 3000f)
							{
								this.emo = astroDupe.emotion.justsaved;
								Overlay.manbouy = Vector3.Zero;
								this.nextact = this.dance1Clip;
							}
							else
							{
								Overlay.manbouy = this.mypos;
							}
						}
					}
					if (num == this.turnfastClip)
					{
						this.timer = 40f;
						this.turn = 0.08f;
						if (this.random.Next(1, 1000) < 500)
						{
							this.turn = -0.08f;
						}
						this.speed = 0.4f;
						this.holdlastclip = true;
						this.isturning = false;
						this.tweenspeed = 0.1f;
						this.nextact = this.walkawayfromtruckClip;
						this.switchClip(this.walkClip);
					}
					if (num == this.waitClip)
					{
						this.isturning = false;
						this.holdlastclip = true;
						this.timer = (float)this.random.Next(80, 280);
						this.tweenspeed = 0.05f;
						this.speed = 0f;
						this.turn = 0f;
						this.switchClip(this.waitClip);
					}
					if (num == this.hammerClip)
					{
						this.isturning = false;
						this.holdlastclip = true;
						this.timer = (float)this.random.Next(130, 360);
						this.tweenspeed = 0.05f;
						this.speed = 0f;
						this.turn = 0f;
						this.switchClip(this.hammerClip);
					}
					if (num == this.lookClip)
					{
						this.temp2 = 0;
						this.holdlastclip = true;
						this.isturning = false;
						this.tweenspeed = 0.05f;
						this.timer = 200f;
						this.speed = 0f;
						this.turn = 0f;
						this.switchClip(this.lookClip);
					}
					if (num == this.waveClip)
					{
						this.temp2 = 0;
						this.holdlastclip = true;
						this.isturning = false;
						this.timer = (float)this.random.Next(135, 290);
						this.tweenspeed = 0.05f;
						this.speed = 0f;
						this.turn = 0f;
						this.switchClip(this.waveClip);
					}
					if (num == this.wave2Clip)
					{
						this.temp2 = 0;
						this.holdlastclip = false;
						this.isturning = false;
						this.timer = 490f;
						this.tweenspeed = 0.05f;
						this.speed = 0f;
						this.turn = 0f;
						this.switchClip(this.wave2Clip);
					}
					if (num == this.dance1Clip)
					{
						this.temp2 = 0;
						this.holdlastclip = false;
						this.isturning = false;
						this.timer = 755f;
						this.tweenspeed = 0.05f;
						this.speed = 0f;
						this.turn = 0f;
						this.switchClip(this.dance1Clip);
					}
				}
			}
			if (this.flipinspace)
			{
				this.switchClip(this.flipClip);
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x001CC55C File Offset: 0x001CA75C
		public void switchClip(int ch)
		{
			int num = this.frame2;
			this.frame2 = this.frame1;
			this.frame1 = num;
			num = this.temp2;
			this.temp2 = this.temp1;
			this.temp1 = num;
			this.tween = 0f;
			this.clip2 = this.clip1;
			this.clip1 = ch;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x001CC5BC File Offset: 0x001CA7BC
		public void leaveTruck()
		{
			this.temp2 = 0;
			this.timer = 186f;
			this.turn = 0f;
			this.speed = 0f;
			this.tweenspeed = 0.1f;
			this.isturning = false;
			this.loc = astroDupe.where.leavingTruck;
			this.holdlastframe = false;
			this.holdlastclip = true;
			this.switchClip(this.climboutClip);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x001CC624 File Offset: 0x001CA824
		public void makeSalute(bool commandtoenter)
		{
			this.isturning = false;
			this.speed = 0f;
			this.turn = 0f;
			this.timer = 80f;
			this.temp2 = 0;
			if (!commandtoenter)
			{
				this.tweenspeed = 0.05f;
				this.switchClip(this.saluteClip);
			}
			if (commandtoenter)
			{
				this.timer = 235f;
				this.turn = 0f;
				this.speed = 3f;
				this.isturning = false;
				if (this.emo == astroDupe.emotion.underground)
				{
					this.speed = 3f;
					this.temp2 = this.random.Next(10, 300);
					this.switchClip(this.runClip);
					return;
				}
				if (this.emo == astroDupe.emotion.stranded)
				{
					Overlay.manbouy = Vector3.Zero;
					this.loc = astroDupe.where.enteringTruck;
				}
				this.loc = astroDupe.where.enteringTruck;
				this.temp2 = this.random.Next(10, 300);
				this.switchClip(this.runClip);
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x001CC726 File Offset: 0x001CA926
		private static float WrapAngle(float radians)
		{
			while (radians < -3.1415927f)
			{
				radians += 6.2831855f;
			}
			while (radians > 3.1415927f)
			{
				radians -= 6.2831855f;
			}
			return radians;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x001CC750 File Offset: 0x001CA950
		private void GetHeight(ref int[,] heightData, Vector3 position, out float height)
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
			float num8 = (1f - num4) * (float)heightData[num2, num3] + num4 * (float)heightData[num6, num3];
			float num9 = (1f - num4) * (float)heightData[num2, num7] + num4 * (float)heightData[num6, num7];
			height = (1f - num5) * num8 + num5 * num9;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x001CC870 File Offset: 0x001CAA70
		private void GetHeightNormal(ref int[,] heightData, ref Vector3[,] normals, Vector3 position, out float height, out Vector3 normal)
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

		// Token: 0x060007D2 RID: 2002 RVA: 0x001CCA04 File Offset: 0x001CAC04
		public void GetHeight2(ref int[,] heightData, Vector3 position, out float height)
		{
			float y = position.Y;
			position -= Facility.offset;
			position *= 4f;
			float num = 100f;
			int num2 = (int)MathHelper.Clamp(position.X / num, 0f, 175f);
			int num3 = (int)MathHelper.Clamp(position.Z / num, 0f, 175f);
			float num4 = position.X % num / num;
			float num5 = position.Z % num / num;
			int num6 = num2 + 1;
			int num7 = num3 + 1;
			float num8 = MathHelper.Lerp((float)heightData[num2, num3], (float)heightData[num6, num3], num4);
			float num9 = MathHelper.Lerp((float)heightData[num2, num7], (float)heightData[num6, num7], num4);
			height = MathHelper.Lerp(num8, num9, num5);
			if (height < Facility.offset.Y - 500f)
			{
				height = y;
			}
		}

		// Token: 0x04001FDB RID: 8155
		public static Vector3 farmLocation = new Vector3(0f, 5f, 0f);

		// Token: 0x04001FDC RID: 8156
		public static Vector3 constantRoverPosition = Vector3.Zero;

		// Token: 0x04001FDD RID: 8157
		public static Vector4 facility = Vector4.Zero;

		// Token: 0x04001FDE RID: 8158
		public static float acc = 650f;

		// Token: 0x04001FDF RID: 8159
		public static List<int> sics;

		// Token: 0x04001FE0 RID: 8160
		public astroDupe.emotion emo;

		// Token: 0x04001FE1 RID: 8161
		public astroDupe.where loc;

		// Token: 0x04001FE2 RID: 8162
		public bool isturning;

		// Token: 0x04001FE3 RID: 8163
		public bool holdlastframe;

		// Token: 0x04001FE4 RID: 8164
		private bool holdlastclip;

		// Token: 0x04001FE5 RID: 8165
		public bool flipinspace;

		// Token: 0x04001FE6 RID: 8166
		private int nextact;

		// Token: 0x04001FE7 RID: 8167
		public Vector3 destination = Vector3.Zero;

		// Token: 0x04001FE8 RID: 8168
		public Vector3 normal = Vector3.Zero;

		// Token: 0x04001FE9 RID: 8169
		public Vector3 posOffset;

		// Token: 0x04001FEA RID: 8170
		public float rotOffset;

		// Token: 0x04001FEB RID: 8171
		public int variant;

		// Token: 0x04001FEC RID: 8172
		public float flipTimer;

		// Token: 0x04001FED RID: 8173
		public Matrix flipRot = Matrix.Identity;

		// Token: 0x04001FEE RID: 8174
		public Vector3 flipDir = Vector3.One;

		// Token: 0x04001FEF RID: 8175
		public Vector3 flipVeloc = Vector3.One;

		// Token: 0x04001FF0 RID: 8176
		public float flipSpeed;

		// Token: 0x04001FF1 RID: 8177
		public bool employed;

		// Token: 0x04001FF2 RID: 8178
		public List<Vector2> botPath = new List<Vector2>();

		// Token: 0x04001FF3 RID: 8179
		public float botStart;

		// Token: 0x04001FF4 RID: 8180
		public int isShocked;

		// Token: 0x04001FF5 RID: 8181
		public int bodytype;

		// Token: 0x04001FF6 RID: 8182
		public int headType;

		// Token: 0x04001FF7 RID: 8183
		public int packType;

		// Token: 0x04001FF8 RID: 8184
		public int splatIndex;

		// Token: 0x04001FF9 RID: 8185
		public int move;

		// Token: 0x04001FFA RID: 8186
		public float myRot;

		// Token: 0x04001FFB RID: 8187
		public float myRotHeld;

		// Token: 0x04001FFC RID: 8188
		public float angleX;

		// Token: 0x04001FFD RID: 8189
		public float angleZ;

		// Token: 0x04001FFE RID: 8190
		public float scale;

		// Token: 0x04001FFF RID: 8191
		public float bluScale;

		// Token: 0x04002000 RID: 8192
		public Vector3 mypos;

		// Token: 0x04002001 RID: 8193
		public int frame1 = 1;

		// Token: 0x04002002 RID: 8194
		public int frame2 = 1;

		// Token: 0x04002003 RID: 8195
		public int temp1;

		// Token: 0x04002004 RID: 8196
		public int temp2;

		// Token: 0x04002005 RID: 8197
		public int clip1;

		// Token: 0x04002006 RID: 8198
		public int clip2;

		// Token: 0x04002007 RID: 8199
		public float tween;

		// Token: 0x04002008 RID: 8200
		public float tweenspeed = 0.05f;

		// Token: 0x04002009 RID: 8201
		public int stunLength = 150;

		// Token: 0x0400200A RID: 8202
		public float speed;

		// Token: 0x0400200B RID: 8203
		public float turn;

		// Token: 0x0400200C RID: 8204
		public float timer;

		// Token: 0x0400200D RID: 8205
		public float startHealth = 5f;

		// Token: 0x0400200E RID: 8206
		public float health = 5f;

		// Token: 0x0400200F RID: 8207
		public Matrix transform;

		// Token: 0x04002010 RID: 8208
		public int blood;

		// Token: 0x04002011 RID: 8209
		public int seed;

		// Token: 0x04002012 RID: 8210
		private float groundHeight;

		// Token: 0x04002013 RID: 8211
		public int age;

		// Token: 0x04002014 RID: 8212
		public int tint;

		// Token: 0x04002015 RID: 8213
		public int oldtint;

		// Token: 0x04002016 RID: 8214
		public Random random;

		// Token: 0x04002017 RID: 8215
		public int immunity;

		// Token: 0x04002018 RID: 8216
		public ScreenManager sc;

		// Token: 0x04002019 RID: 8217
		public float scaleSpeed = 1f;

		// Token: 0x0400201A RID: 8218
		public int walkClip;

		// Token: 0x0400201B RID: 8219
		public int waitClip = 1;

		// Token: 0x0400201C RID: 8220
		public int flipClip = 2;

		// Token: 0x0400201D RID: 8221
		public int saluteClip = 3;

		// Token: 0x0400201E RID: 8222
		public int hammerClip = 4;

		// Token: 0x0400201F RID: 8223
		public int dieClip = 5;

		// Token: 0x04002020 RID: 8224
		public int climbinClip = 6;

		// Token: 0x04002021 RID: 8225
		public int runClip = 7;

		// Token: 0x04002022 RID: 8226
		public int climboutClip = 8;

		// Token: 0x04002023 RID: 8227
		public int lookClip = 9;

		// Token: 0x04002024 RID: 8228
		public int waveClip = 10;

		// Token: 0x04002025 RID: 8229
		public int wave2Clip = 11;

		// Token: 0x04002026 RID: 8230
		public int dance1Clip = 12;

		// Token: 0x04002027 RID: 8231
		public int turnfastClip = 50;

		// Token: 0x04002028 RID: 8232
		public int walkawayfromtruckClip = 45;

		// Token: 0x04002029 RID: 8233
		private int[] terrainCh = new int[0];

		// Token: 0x0400202A RID: 8234
		private int[] strandedCh = new int[0];

		// Token: 0x0400202B RID: 8235
		private int[] savedCh = new int[0];

		// Token: 0x020000E3 RID: 227
		public enum emotion
		{
			// Token: 0x0400202D RID: 8237
			flipping,
			// Token: 0x0400202E RID: 8238
			hungry,
			// Token: 0x0400202F RID: 8239
			thirsty,
			// Token: 0x04002030 RID: 8240
			stranded,
			// Token: 0x04002031 RID: 8241
			stranded2,
			// Token: 0x04002032 RID: 8242
			safe,
			// Token: 0x04002033 RID: 8243
			working,
			// Token: 0x04002034 RID: 8244
			underground,
			// Token: 0x04002035 RID: 8245
			sweeping,
			// Token: 0x04002036 RID: 8246
			digging,
			// Token: 0x04002037 RID: 8247
			angry,
			// Token: 0x04002038 RID: 8248
			trucking,
			// Token: 0x04002039 RID: 8249
			justsaved,
			// Token: 0x0400203A RID: 8250
			scaredintruck
		}

		// Token: 0x020000E4 RID: 228
		public enum where
		{
			// Token: 0x0400203C RID: 8252
			outofTruck,
			// Token: 0x0400203D RID: 8253
			inTruck,
			// Token: 0x0400203E RID: 8254
			enteringTruck,
			// Token: 0x0400203F RID: 8255
			leavingTruck,
			// Token: 0x04002040 RID: 8256
			inspace
		}
	}
}
