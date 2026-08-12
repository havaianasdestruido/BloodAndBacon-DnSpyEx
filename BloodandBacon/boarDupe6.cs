using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000033 RID: 51
	public class boarDupe6
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0004B2D4 File Offset: 0x000494D4
		public boarDupe6(int group, int variant, Vector3 startpos, int myMove, int seed, int age, ScreenManager sc, string timeofday, int formation, bool noZboars)
		{
			this.nozombies = noZboars;
			boarDupe6.sc = sc;
			boarDupe6.homingCount = 0;
			boarDupe6.homingCount2 = 0;
			this.splatIndex = 0;
			this.random = new Random(seed);
			this.randomDeath = new Random(seed + 1);
			this.randomHead = new Random(seed + 2);
			this.randomPartsFly = new Random(seed + 3);
			this.blood = 0;
			this.mypos = startpos;
			this.move = myMove;
			this.tween = 1f;
			this.age = age;
			this.seed = seed;
			this.boarGroup = group;
			this.variant = variant;
			if (sc.gameSpectate != 0)
			{
				this.freq = (float)this.random.Next(2000, 10000) / 100f;
				this.amp = (float)this.random.Next(200, 700) / 100f;
			}
			else
			{
				this.freq = (float)this.random.Next(6000, 10000) / 100f;
				this.amp = (float)this.random.Next(700, 900) / 100f;
			}
			this.stunLength = this.random.Next(120, 154);
			this.defaultClip = 0;
			this.nightSpeed = 1f;
			if (group == 1 && this.random.Next(1, 100) < sc.run1Percent)
			{
				this.defaultClip = 1;
				this.nightSpeed = (float)this.random.Next(5, 10);
				if (variant == 4 && boarDupe6.currentDay <= 100)
				{
					this.nightSpeed = (float)this.random.Next(10, 15);
				}
			}
			if (group == 2 && this.random.Next(1, 100) < sc.run2Percent)
			{
				this.defaultClip = 1;
				this.nightSpeed = (float)this.random.Next(5, 10);
				if (variant == 4 && boarDupe6.currentDay <= 100)
				{
					this.nightSpeed = (float)this.random.Next(10, 15);
				}
			}
			this.isBlind = -1;
			this.isSpooked = -1;
			if (this.random.Next(1, 100) < 20 && boarDupe6.currentDay != 101)
			{
				this.isSpooked = -1;
			}
			this.resetSpooked = this.isSpooked;
			this.isStun = 0;
			this.resetStun = this.isStun;
			this.isChomp = -1;
			if (variant == 1)
			{
				this.isChomp = 0;
			}
			if (variant == 2 && this.random.Next(1, 100) < 40)
			{
				this.isChomp = 0;
			}
			this.resetChomp = this.isChomp;
			this.isShocked = 0;
			if (variant == 2)
			{
				this.isShocked = -1;
			}
			this.isHead = -1;
			this.exploded = 0;
			this.immunity = 0;
			if (group == 1)
			{
				this.assexplode = sc.boar1explode;
				if (sc.boar1explode == 0)
				{
					this.assexplode = 50000;
				}
				this.borders = false;
				if (sc.boar1Borders == 1)
				{
					this.borders = true;
				}
			}
			if (group == 2)
			{
				this.assexplode = sc.boar2explode;
				if (sc.boar2explode == 0)
				{
					this.assexplode = 50000;
				}
				this.borders = false;
				if (sc.boar2Borders == 1)
				{
					this.borders = true;
				}
			}
			this.shottie = 0;
			if (this.boarGroup == 1)
			{
				this.shottie = sc.boar1shottie;
				if (sc.boar1shottie == 0)
				{
					this.shottie = 50000;
				}
			}
			if (this.boarGroup == 2)
			{
				this.shottie = sc.boar2shottie;
				if (sc.boar2shottie == 0)
				{
					this.shottie = 50000;
				}
			}
			this.homing = 0;
			if (group == 1)
			{
				this.charge = (float)this.random.Next(300, 500) / 10000f * (float)sc.boar1Charge;
			}
			if (group == 2)
			{
				this.charge = (float)this.random.Next(300, 500) / 10000f * (float)sc.boar2Charge;
			}
			this.turnHelper = (float)this.random.Next(130, 200) / 10000f;
			if (group == 1 && (variant == 0 || variant == 3 || variant == 2 || variant == 4) && this.random.Next(1, 100) < sc.headless1Percent)
			{
				this.isHead = 0;
			}
			if (group == 2 && (variant == 0 || variant == 3 || variant == 2 || variant == 4) && this.random.Next(1, 100) < sc.headless2Percent)
			{
				this.isHead = 0;
			}
			this.tint = (float)this.random.Next(1, 6);
			if (variant == 4)
			{
				this.tint = (float)this.random.Next(0, 7);
				if (boarDupe6.currentDay == 101 || boarDupe6.currentDay == 42)
				{
					this.tint = 6f;
				}
			}
			if (variant == 1)
			{
				int num = this.random.Next(0, 4);
				if (num == 0)
				{
					this.tint = 7f;
				}
				if (num == 1)
				{
					this.tint = 8f;
				}
				if (num == 2)
				{
					this.tint = 9f;
				}
				if (num == 3)
				{
					this.tint = 0f;
				}
			}
			if (variant == 2)
			{
				this.tint = 6f;
				if (this.random.Next(1, 100) < 30)
				{
					this.tint = 10f;
				}
			}
			if (variant == 3)
			{
				int num2 = this.random.Next(0, 4);
				if (num2 == 0)
				{
					this.tint = 0f;
				}
				if (num2 == 1)
				{
					this.tint = 5f;
				}
				if (num2 == 2)
				{
					this.tint = 8f;
				}
				if (num2 == 3)
				{
					this.tint = 9f;
				}
			}
			if (group == 1)
			{
				this.scale = (float)this.random.Next(sc.boar1MinSize, sc.boar1MaxSize) / 50f;
			}
			if (group == 2)
			{
				this.scale = (float)this.random.Next(sc.boar2MinSize, sc.boar2MaxSize) / 50f;
			}
			if (group == 1)
			{
				this.health = this.scale * ((float)sc.boar1Health / 10f);
			}
			if (group == 2)
			{
				this.health = this.scale * ((float)sc.boar2Health / 10f);
			}
			if (variant == 1)
			{
				this.health *= 2f;
			}
			if (this.health < 1f)
			{
				this.health = 1f;
			}
			if (sc.boar1Health > 5000)
			{
				this.undead = true;
				this.health = 699f;
			}
			this.startHealth = this.health;
			if ((group == 1 && this.random.Next(0, 1001) < sc.boar1TinyOdds * 10) || (group == 2 && this.random.Next(0, 1001) < sc.boar2TinyOdds * 10))
			{
				this.scale = (float)this.random.Next(3, 6) / 50f;
				this.tint = 0f;
				if (this.random.Next(1, 100) < 50)
				{
					this.tint = 6f;
				}
				this.health = 1f;
				this.isHead = -1;
			}
			if ((group == 1 && this.random.Next(0, 1001) < sc.boar1GiantOdds * 10) || (group == 2 && this.random.Next(0, 1001) < sc.boar2GiantOdds * 10))
			{
				this.scale = (float)this.random.Next(18, 22) / 50f;
				if (this.random.Next(1, 1000) < 150)
				{
					this.scale = (float)this.random.Next(28, 36) / 50f;
				}
				this.immunity = 50000;
				this.shottie = 5;
				this.tint = 6f;
				this.health = 10f;
				this.isHead = -1;
				if (this.random.Next(1, 100) < 60)
				{
					this.isHead = 0;
				}
			}
			this.isGrow = -1;
			if (((group == 1 && sc.run1Percent == 1) || (group == 2 && sc.run2Percent == 1)) && this.immunity < 5000 && this.random.Next(1, 100) < 70)
			{
				this.isGrow = 0;
				this.isGrowAge = this.random.Next(500, 2500);
				this.bluScale = (float)this.random.Next(26, 47) / 100f;
				this.growTrigger = false;
			}
			if (this.undead)
			{
				this.isGrow = 0;
				this.isGrowAge = 250;
				this.bluScale = (float)this.random.Next(35, 38) / 100f;
				this.growTrigger = false;
			}
			this.myRot = (float)this.random.Next(-800, 800) / 100f;
			this.speed = this.scale * (boarDupe6.acc / 1000f) * this.nightSpeed;
			this.turn = (float)this.random.Next(-20, 20) / 1000f;
			this.timer = (float)this.random.Next(20, 300);
			this.clip1 = this.defaultClip;
			this.clip2 = this.defaultClip;
			if (group == 1 && sc.boar1Timer > 0)
			{
				this.timer = (float)sc.boar1Timer;
			}
			if (group == 2 && sc.boar2Timer > 0)
			{
				this.timer = (float)sc.boar2Timer;
			}
			if (group == 1 && sc.boar1Clip > 0)
			{
				this.scaleSpeed = (float)this.random.Next(35, 45) / 10f;
				if (sc.boar1Clip == 1)
				{
					this.speed = 0.2f * (boarDupe6.acc / 1000f) * this.scaleSpeed;
				}
				if (sc.boar1Clip == 2)
				{
					this.speed = 0.2f * (boarDupe6.acc / 1000f) * 7f;
					this.scaleSpeed = 7f;
				}
				if (sc.boar1Clip == 3)
				{
					this.speed = 0.2f * (boarDupe6.acc / 1000f) * 10f;
					this.scaleSpeed = 10f;
				}
				if (sc.boar1Clip == 4)
				{
					this.speed = 0.2f * (boarDupe6.acc / 1000f) * 22f;
					this.scaleSpeed = 22f;
				}
				this.clip1 = this.random.Next(1, 4);
				this.clip2 = this.clip1;
			}
			if (group == 2 && sc.boar2Clip > 0)
			{
				this.scaleSpeed = (float)this.random.Next(35, 45) / 10f;
				if (sc.boar2Clip == 1)
				{
					this.speed = 0.2f * (boarDupe6.acc / 1000f) * this.scaleSpeed;
				}
				if (sc.boar2Clip == 2)
				{
					this.speed = 0.2f * (boarDupe6.acc / 1000f) * 7f;
					this.scaleSpeed = 7f;
				}
				if (sc.boar2Clip == 3)
				{
					this.speed = 0.2f * (boarDupe6.acc / 1000f) * 10f;
					this.scaleSpeed = 10f;
				}
				if (sc.boar2Clip == 4)
				{
					this.speed = 0.2f * (boarDupe6.acc / 1000f) * 22f;
					this.scaleSpeed = 22f;
				}
				this.clip1 = this.random.Next(1, 4);
				this.clip2 = this.clip1;
			}
			if (group == 1 && sc.boar1Rot != 0f)
			{
				this.myRot = sc.boar1Rot;
			}
			if (group == 2 && sc.boar2Rot != 0f)
			{
				this.myRot = sc.boar2Rot;
			}
			if (group == 1 && sc.boar1Turn != 99f)
			{
				this.turn = sc.boar1Turn / 1000f;
			}
			if (group == 2 && sc.boar2Turn != 99f)
			{
				this.turn = sc.boar2Turn / 1000f;
			}
			if (group == 1)
			{
				sc.boarAttack = sc.boar1Attack;
				sc.boarTurnRate = sc.boar1TurnRate;
			}
			if (group == 2)
			{
				sc.boarAttack = sc.boar2Attack;
				sc.boarTurnRate = sc.boar2TurnRate;
			}
			if (group == 1 && ((formation > 19 && formation < 30) || Math.Abs(sc.boar1Rot) >= 50f))
			{
				Vector2 vector = new Vector2(3000f, 3000f);
				if (Math.Abs(sc.boar1Rot) >= 50f)
				{
					vector = new Vector2(sc.boar1Rot, sc.boar1Turn);
				}
				float num3 = -(float)Math.Atan2((double)(startpos.Z - vector.X), (double)(startpos.X - vector.Y));
				float num4 = 6.2831855f * Vector2.Distance(vector, new Vector2(startpos.X, startpos.Z));
				float num5 = num4 / (this.speed * boarDupe6.handicapSpeed);
				if (sc.boar1Turn >= 0f)
				{
					this.myRot = num3;
					this.turn = -6.2831855f / num5;
				}
				else
				{
					this.myRot = num3 + 3.1415927f;
					this.turn = -6.2831855f / num5 * -1f;
				}
			}
			if (group == 2 && ((formation > 19 && formation < 30) || Math.Abs(sc.boar2Rot) >= 50f))
			{
				Vector2 vector2 = new Vector2(3000f, 3000f);
				if (Math.Abs(sc.boar2Rot) >= 50f)
				{
					vector2 = new Vector2(sc.boar2Rot, sc.boar2Turn);
				}
				float num6 = -(float)Math.Atan2((double)(startpos.Z - vector2.X), (double)(startpos.X - vector2.Y));
				float num7 = 6.2831855f * Vector2.Distance(vector2, new Vector2(startpos.X, startpos.Z));
				float num8 = num7 / (this.speed * boarDupe6.handicapSpeed);
				if (sc.boar2Turn >= 0f)
				{
					this.myRot = num6;
					this.turn = -6.2831855f / num8;
				}
				else
				{
					this.myRot = num6 + 3.1415927f;
					this.turn = -6.2831855f / num8 * -1f;
				}
			}
			this.temp1 = this.random.Next(20, 30000);
			this.temp2 = this.random.Next(20, 30000);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0004C254 File Offset: 0x0004A454
		private void calTint()
		{
			float num = this.tint - (float)((int)this.tint);
			int[] array = new int[]
			{
				9, 8, 7, 5, 3, 11, 6, 6, 6, 6,
				6, 6, 6
			};
			int num2 = (int)this.health / 100;
			num2 = (int)MathHelper.Clamp((float)num2, 0f, 7f);
			this.tint = (float)array[num2] + num;
			if (this.health < 100f)
			{
				this.undead = false;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0004C2C4 File Offset: 0x0004A4C4
		public void UpdateNormal(ref float[,] heights)
		{
			this.age++;
			if (this.undead)
			{
				this.calTint();
			}
			if (this.growTrigger)
			{
				this.scale += 0.002f;
				if (this.scale > this.bluScale)
				{
					this.growTrigger = false;
					if (!this.undead)
					{
						this.isGrow = 2;
						if (this.boarGroup == 1)
						{
							this.health = this.scale * ((float)boarDupe6.sc.boar1Health / 10f);
						}
						if (this.boarGroup == 2)
						{
							this.health = this.scale * ((float)boarDupe6.sc.boar2Health / 10f);
						}
						if (this.homing != 0)
						{
							this.speed = this.scale * (boarDupe6.acc / 1000f) * this.scaleSpeed;
						}
					}
					else
					{
						this.isGrow = -1;
					}
				}
			}
			this.mypos.X = this.mypos.X + -(float)Math.Cos((double)(this.myRot + 1.5708f)) * (this.speed * boarDupe6.handicapSpeed);
			this.mypos.Z = this.mypos.Z + (float)Math.Sin((double)(this.myRot + 1.5708f)) * (this.speed * boarDupe6.handicapSpeed);
			if (this.mypos.X > 6900f || this.mypos.X < -900f)
			{
				this.mypos.X = 6000f - this.mypos.X;
			}
			if (this.mypos.Z > 6900f || this.mypos.Z < -900f)
			{
				this.mypos.Z = 6000f - this.mypos.Z;
			}
			boarDupe6.GetHeightFast(ref heights, new Vector2(this.mypos.X, this.mypos.Z), out this.groundHeight);
			this.mypos.Y = this.groundHeight;
			this.myRot += this.turn;
			this.timer -= 1f;
			this.temp1++;
			this.temp2++;
			this.frame1 = this.temp1 % boarDupe6.sics[this.clip1 * 2] + boarDupe6.sics[this.clip1 * 2 + 1];
			this.frame2 = this.temp2 % boarDupe6.sics[this.clip2 * 2] + boarDupe6.sics[this.clip2 * 2 + 1];
			this.tween += 0.125f;
			if (this.timer <= 0f)
			{
				this.makeCalc();
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0004C5A0 File Offset: 0x0004A7A0
		public void UpdateDeath(ref float[,] heights)
		{
			this.age++;
			if (this.undead)
			{
				this.calTint();
			}
			this.mypos.X = this.mypos.X + this.angleX * (this.speed * boarDupe6.handicapSpeed);
			this.mypos.Z = this.mypos.Z + this.angleZ * (this.speed * boarDupe6.handicapSpeed);
			this.mypos.X = MathHelper.Clamp(this.mypos.X, 100f, 5900f);
			this.mypos.Z = MathHelper.Clamp(this.mypos.Z, 100f, 5900f);
			boarDupe6.GetHeightFast(ref heights, new Vector2(this.mypos.X, this.mypos.Z), out this.groundHeight);
			this.mypos.Y = this.groundHeight;
			this.myRot += this.turn;
			this.timer -= 1f;
			this.tween += 0.125f;
			if (this.timer <= 0f)
			{
				this.turn *= 0.98f;
				this.speed *= 0.98f;
				if (this.speed < 0.05f)
				{
					this.makeCalc();
					return;
				}
			}
			else
			{
				this.temp1++;
				this.temp2++;
				this.frame1 = this.temp1 % boarDupe6.sics[this.clip1 * 2] + boarDupe6.sics[this.clip1 * 2 + 1];
				this.frame2 = this.temp2 % boarDupe6.sics[this.clip2 * 2] + boarDupe6.sics[this.clip2 * 2 + 1];
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0004C798 File Offset: 0x0004A998
		private void setClips(int chx)
		{
			int num = this.frame2;
			this.frame2 = this.frame1;
			this.frame1 = num;
			num = this.temp2;
			this.temp2 = this.temp1;
			this.temp1 = num;
			this.tween = 0f;
			this.clip2 = this.clip1;
			this.clip1 = chx;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0004C7F8 File Offset: 0x0004A9F8
		public void makeCalc()
		{
			this.isChomp = this.resetChomp;
			this.isStun = this.resetStun;
			this.isSpooked = this.resetSpooked;
			if (this.interrupt)
			{
				this.interrupt = false;
				this.clip2 = this.clip1;
				this.clip1 = this.oldclip;
				this.temp2 = this.temp1;
				this.temp1 = this.oldtemp;
				this.frame2 = this.frame1;
				this.frame1 = this.oldframe;
				this.turn = this.oldturn;
				this.isturning = this.oldturning;
				this.speed = this.oldspeed;
				this.timer = this.oldtimer;
				this.tween = 0f;
				return;
			}
			if (this.isShocked == 2)
			{
				if (this.undead)
				{
					this.isShocked = 0;
					this.blood = 0;
					this.death = false;
					this.isStun = 0;
					this.undead = true;
					if (this.homing != 0)
					{
						this.isturning = false;
						this.timer = 50000f;
						this.ch = 2;
						if (this.random.Next(1, 100) < 51)
						{
							this.ch = 3;
						}
						this.speed = this.scale * (boarDupe6.acc / 1000f) * this.charge * 1.2f;
						this.scaleSpeed = this.charge;
					}
					else if (this.random.Next(0, 200) < 130)
					{
						this.timer = (float)this.random.Next(250, 800);
						this.turn = 0f;
						if (this.random.Next(1, 1000) < 90)
						{
							this.turn = (float)this.random.Next(-20, 20) / 1000f;
						}
						this.speed = this.scale * (boarDupe6.acc / 1000f) * this.nightSpeed;
						this.isturning = false;
						this.ch = this.defaultClip;
					}
					else
					{
						this.isturning = false;
						this.timer = (float)this.random.Next(120, 280);
						this.speed = 0f;
						this.turn = 0f;
						this.ch = 5;
					}
					this.setClips(this.ch);
					return;
				}
				if (!this.death || this.isHead > 0 || this.nozombies)
				{
					this.move = 0;
					this.isShocked = 3;
					this.tween = 1f;
					this.health = 0f;
					this.blood = 0;
					return;
				}
				this.isGrow = 0;
				this.isGrowAge = this.age + this.random.Next(100, 600);
				this.bluScale = (float)this.random.Next(25, 60) / 100f;
				this.growTrigger = false;
				this.assexplode = 50;
				this.shottie = 500;
				this.death = false;
				this.undead = true;
				this.zombie = true;
				this.blood = 0;
				this.health = 300f;
				this.isShocked = 0;
				if (this.homing != 0)
				{
					this.isturning = false;
					this.timer = 50000f;
					this.ch = 2;
					if (this.random.Next(1, 100) < 51)
					{
						this.ch = 3;
					}
					this.speed = this.scale * (boarDupe6.acc / 1000f) * this.charge * 1.5f;
					this.scaleSpeed = this.charge;
					this.setClips(this.ch);
					return;
				}
				this.isturning = false;
				this.timer = (float)this.random.Next(120, 280);
				this.speed = 0f;
				this.turn = 0f;
				this.setClips(5);
				return;
			}
			else
			{
				if (this.clip1 >= 10)
				{
					if (!this.undead)
					{
						this.tween = 1f;
						this.move = 0;
						return;
					}
					this.death = false;
					this.isStun = 0;
					if (this.health > 100f)
					{
						this.health -= 50f;
					}
					this.isturning = false;
					this.timer = (float)this.random.Next(120, 280);
					this.speed = 0f;
					this.turn = 0f;
					this.ch = 5;
					this.setClips(this.ch);
				}
				if (this.isturning)
				{
					if (this.random.Next(0, 200) < 160)
					{
						this.isturning = false;
						this.timer = (float)this.random.Next(240, 530);
						this.turn = (float)this.random.Next(-7, 7) / 1000f;
						this.speed = this.scale * (boarDupe6.acc / 1000f) * this.nightSpeed;
					}
					else
					{
						this.isturning = false;
						this.timer = (float)this.random.Next(120, 280);
						this.speed = 0f;
						this.turn = 0f;
						this.ch = 5;
					}
					this.setClips(this.ch);
					return;
				}
				if (this.random.Next(0, 200) < 130)
				{
					this.timer = (float)this.random.Next(250, 800);
					this.turn = (float)this.random.Next(-7, 7) / 1000f;
					if (this.random.Next(1, 1000) < 90)
					{
						this.turn = (float)this.random.Next(-20, 20) / 1000f;
					}
					this.speed = this.scale * (boarDupe6.acc / 1000f) * this.nightSpeed;
					this.isturning = true;
					this.ch = this.defaultClip;
				}
				else
				{
					this.isturning = false;
					this.timer = (float)this.random.Next(120, 280);
					this.speed = 0f;
					this.turn = 0f;
					this.ch = 5;
				}
				this.setClips(this.ch);
				return;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0004CE4C File Offset: 0x0004B04C
		private static void GetHeightFast(ref float[,] heights, Vector2 position, out float height)
		{
			int num = (int)(position.X / boarDupe6.unit);
			int num2 = (int)(position.Y / boarDupe6.unit);
			num = (int)MathHelper.Clamp(position.X / boarDupe6.unit, 0f, (float)(boarDupe6.bitmap - 2));
			num2 = (int)MathHelper.Clamp(position.Y / boarDupe6.unit, 0f, (float)(boarDupe6.bitmap - 2));
			float num3 = position.X % boarDupe6.unit / boarDupe6.unit;
			float num4 = position.Y % boarDupe6.unit / boarDupe6.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
		}

		// Token: 0x04000885 RID: 2181
		private int ch;

		// Token: 0x04000886 RID: 2182
		public static int homingCount = 0;

		// Token: 0x04000887 RID: 2183
		public static int homingCount2 = 0;

		// Token: 0x04000888 RID: 2184
		public static int currentDay = 1;

		// Token: 0x04000889 RID: 2185
		public static float acc = 3000f;

		// Token: 0x0400088A RID: 2186
		public static float handicapSpeed = 1f;

		// Token: 0x0400088B RID: 2187
		public static float handicapTurn = 1f;

		// Token: 0x0400088C RID: 2188
		public bool undead;

		// Token: 0x0400088D RID: 2189
		public bool zombie;

		// Token: 0x0400088E RID: 2190
		public bool borders = true;

		// Token: 0x0400088F RID: 2191
		public bool interrupt;

		// Token: 0x04000890 RID: 2192
		public bool oldturning;

		// Token: 0x04000891 RID: 2193
		public float oldturn;

		// Token: 0x04000892 RID: 2194
		public float oldtimer;

		// Token: 0x04000893 RID: 2195
		public float oldspeed;

		// Token: 0x04000894 RID: 2196
		public int oldclip;

		// Token: 0x04000895 RID: 2197
		public int oldtemp;

		// Token: 0x04000896 RID: 2198
		public int oldframe;

		// Token: 0x04000897 RID: 2199
		public bool death;

		// Token: 0x04000898 RID: 2200
		public float amp = 1f;

		// Token: 0x04000899 RID: 2201
		public float freq = 20f;

		// Token: 0x0400089A RID: 2202
		public float charge = 1f;

		// Token: 0x0400089B RID: 2203
		public float turnlimit = 0.07f;

		// Token: 0x0400089C RID: 2204
		public float turnHelper = 0.07f;

		// Token: 0x0400089D RID: 2205
		public int boarGroup = 1;

		// Token: 0x0400089E RID: 2206
		public int variant;

		// Token: 0x0400089F RID: 2207
		public int isStun;

		// Token: 0x040008A0 RID: 2208
		public int resetStun;

		// Token: 0x040008A1 RID: 2209
		public int isChomp;

		// Token: 0x040008A2 RID: 2210
		public int resetChomp;

		// Token: 0x040008A3 RID: 2211
		public int isSpooked;

		// Token: 0x040008A4 RID: 2212
		public int isGrow = -1;

		// Token: 0x040008A5 RID: 2213
		public bool growTrigger;

		// Token: 0x040008A6 RID: 2214
		public int isGrowAge;

		// Token: 0x040008A7 RID: 2215
		private int resetSpooked;

		// Token: 0x040008A8 RID: 2216
		public int isBlind;

		// Token: 0x040008A9 RID: 2217
		public int isHead = -1;

		// Token: 0x040008AA RID: 2218
		public bool nozombies;

		// Token: 0x040008AB RID: 2219
		public int isShocked;

		// Token: 0x040008AC RID: 2220
		public int isZap;

		// Token: 0x040008AD RID: 2221
		public int isCrumbled;

		// Token: 0x040008AE RID: 2222
		public int exploded;

		// Token: 0x040008AF RID: 2223
		public int shottie;

		// Token: 0x040008B0 RID: 2224
		public int timeofDeath;

		// Token: 0x040008B1 RID: 2225
		public static int bitmap;

		// Token: 0x040008B2 RID: 2226
		public static float unit;

		// Token: 0x040008B3 RID: 2227
		public static float Grid;

		// Token: 0x040008B4 RID: 2228
		public int homing;

		// Token: 0x040008B5 RID: 2229
		public int splatIndex;

		// Token: 0x040008B6 RID: 2230
		public int move;

		// Token: 0x040008B7 RID: 2231
		public float myRot;

		// Token: 0x040008B8 RID: 2232
		public float myRotHeld;

		// Token: 0x040008B9 RID: 2233
		public float angleX;

		// Token: 0x040008BA RID: 2234
		public float angleZ;

		// Token: 0x040008BB RID: 2235
		public float scale;

		// Token: 0x040008BC RID: 2236
		public float bluScale;

		// Token: 0x040008BD RID: 2237
		public Vector3 mypos;

		// Token: 0x040008BE RID: 2238
		public bool isturning;

		// Token: 0x040008BF RID: 2239
		public int frame1 = 1;

		// Token: 0x040008C0 RID: 2240
		public int frame2 = 1;

		// Token: 0x040008C1 RID: 2241
		public int temp1;

		// Token: 0x040008C2 RID: 2242
		public int temp2;

		// Token: 0x040008C3 RID: 2243
		public int clip1;

		// Token: 0x040008C4 RID: 2244
		public int clip2;

		// Token: 0x040008C5 RID: 2245
		public float tween;

		// Token: 0x040008C6 RID: 2246
		public int stunLength = 150;

		// Token: 0x040008C7 RID: 2247
		public float speed;

		// Token: 0x040008C8 RID: 2248
		public float turn;

		// Token: 0x040008C9 RID: 2249
		public float timer;

		// Token: 0x040008CA RID: 2250
		public float startHealth = 5f;

		// Token: 0x040008CB RID: 2251
		public float health = 5f;

		// Token: 0x040008CC RID: 2252
		public Matrix transform;

		// Token: 0x040008CD RID: 2253
		public int blood;

		// Token: 0x040008CE RID: 2254
		public static List<int> sics;

		// Token: 0x040008CF RID: 2255
		public int seed;

		// Token: 0x040008D0 RID: 2256
		private float groundHeight;

		// Token: 0x040008D1 RID: 2257
		public int age;

		// Token: 0x040008D2 RID: 2258
		public float tint;

		// Token: 0x040008D3 RID: 2259
		public float oldtint;

		// Token: 0x040008D4 RID: 2260
		public Random random;

		// Token: 0x040008D5 RID: 2261
		public Random randomDeath;

		// Token: 0x040008D6 RID: 2262
		public Random randomHead;

		// Token: 0x040008D7 RID: 2263
		public Random randomPartsFly;

		// Token: 0x040008D8 RID: 2264
		public int immunity;

		// Token: 0x040008D9 RID: 2265
		public int assexplode;

		// Token: 0x040008DA RID: 2266
		public static ScreenManager sc;

		// Token: 0x040008DB RID: 2267
		public float nightSpeed = 1f;

		// Token: 0x040008DC RID: 2268
		public float scaleSpeed = 1f;

		// Token: 0x040008DD RID: 2269
		private int defaultClip;
	}
}
