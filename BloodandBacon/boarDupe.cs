using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000058 RID: 88
	public class boarDupe
	{
		// Token: 0x06000353 RID: 851 RVA: 0x000D33C4 File Offset: 0x000D15C4
		public boarDupe(int group, int variant, Vector3 startpos, int myMove, int seed, int age, ScreenManager sc, string timeofday, int formation, bool noZboars)
		{
			this.nozombies = noZboars;
			boarDupe.sc = sc;
			boarDupe.homingCount = 0;
			boarDupe.homingCount2 = 0;
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
				if (variant == 4 && boarDupe.currentDay <= 100)
				{
					this.nightSpeed = (float)this.random.Next(10, 15);
				}
			}
			if (group == 2 && this.random.Next(1, 100) < sc.run2Percent)
			{
				this.defaultClip = 1;
				this.nightSpeed = (float)this.random.Next(5, 10);
				if (variant == 4 && boarDupe.currentDay <= 100)
				{
					this.nightSpeed = (float)this.random.Next(10, 15);
				}
			}
			this.isBlind = -1;
			this.isSpooked = -1;
			if (this.random.Next(1, 100) < 20 && boarDupe.currentDay != 101)
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
				if (boarDupe.currentDay == 101 || boarDupe.currentDay == 42 || boarDupe.currentDay == 3)
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
				if (this.random.Next(1, 100) < 50 || variant == 4)
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
			if (((group == 1 && sc.run1Percent == 1) || (group == 2 && sc.run2Percent == 1)) && this.immunity < 5000 && this.random.Next(1, 100) < 90)
			{
				this.isGrow = 0;
				this.isGrowAge = this.random.Next(100, 1200);
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
			this.speed = this.scale * (boarDupe.acc / 1000f) * this.nightSpeed;
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
					this.speed = 0.2f * (boarDupe.acc / 1000f) * this.scaleSpeed;
				}
				if (sc.boar1Clip == 2)
				{
					this.speed = 0.2f * (boarDupe.acc / 1000f) * 7f;
					this.scaleSpeed = 7f;
				}
				if (sc.boar1Clip == 3)
				{
					this.speed = 0.2f * (boarDupe.acc / 1000f) * 10f;
					this.scaleSpeed = 10f;
				}
				if (sc.boar1Clip == 4)
				{
					this.speed = 0.2f * (boarDupe.acc / 1000f) * 22f;
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
					this.speed = 0.2f * (boarDupe.acc / 1000f) * this.scaleSpeed;
				}
				if (sc.boar2Clip == 2)
				{
					this.speed = 0.2f * (boarDupe.acc / 1000f) * 7f;
					this.scaleSpeed = 7f;
				}
				if (sc.boar2Clip == 3)
				{
					this.speed = 0.2f * (boarDupe.acc / 1000f) * 10f;
					this.scaleSpeed = 10f;
				}
				if (sc.boar2Clip == 4)
				{
					this.speed = 0.2f * (boarDupe.acc / 1000f) * 22f;
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
				float num5 = num4 / (this.speed * boarDupe.handicapSpeed);
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
				float num8 = num7 / (this.speed * boarDupe.handicapSpeed);
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

		// Token: 0x06000354 RID: 852 RVA: 0x000D434C File Offset: 0x000D254C
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

		// Token: 0x06000355 RID: 853 RVA: 0x000D43BC File Offset: 0x000D25BC
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
							this.health = this.scale * ((float)boarDupe.sc.boar1Health / 10f);
						}
						if (this.boarGroup == 2)
						{
							this.health = this.scale * ((float)boarDupe.sc.boar2Health / 10f);
						}
						if (this.homing != 0)
						{
							this.speed = this.scale * (boarDupe.acc / 1000f) * this.scaleSpeed;
						}
					}
					else
					{
						this.isGrow = -1;
					}
				}
			}
			this.mypos.X = this.mypos.X + -(float)Math.Cos((double)(this.myRot + 1.5708f)) * (this.speed * boarDupe.handicapSpeed);
			this.mypos.Z = this.mypos.Z + (float)Math.Sin((double)(this.myRot + 1.5708f)) * (this.speed * boarDupe.handicapSpeed);
			if (this.mypos.X > 5900f || this.mypos.X < 100f)
			{
				this.mypos.X = 6000f - this.mypos.X;
			}
			if (this.mypos.Z > 5900f || this.mypos.Z < 100f)
			{
				this.mypos.Z = 6000f - this.mypos.Z;
			}
			boarDupe.GetHeightFast(ref heights, new Vector2(this.mypos.X, this.mypos.Z), out this.groundHeight);
			this.mypos.Y = this.groundHeight;
			this.myRot += this.turn;
			this.timer -= 1f;
			this.temp1++;
			this.temp2++;
			this.frame1 = this.temp1 % boarDupe.sics[this.clip1 * 2] + boarDupe.sics[this.clip1 * 2 + 1];
			this.frame2 = this.temp2 % boarDupe.sics[this.clip2 * 2] + boarDupe.sics[this.clip2 * 2 + 1];
			this.tween += 0.125f;
			if (this.timer <= 0f)
			{
				this.makeCalc();
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000D4698 File Offset: 0x000D2898
		public void UpdateDeath(ref float[,] heights)
		{
			this.age++;
			if (this.undead)
			{
				this.calTint();
			}
			this.mypos.X = this.mypos.X + this.angleX * (this.speed * boarDupe.handicapSpeed);
			this.mypos.Z = this.mypos.Z + this.angleZ * (this.speed * boarDupe.handicapSpeed);
			this.mypos.X = MathHelper.Clamp(this.mypos.X, 100f, 5900f);
			this.mypos.Z = MathHelper.Clamp(this.mypos.Z, 100f, 5900f);
			boarDupe.GetHeightFast(ref heights, new Vector2(this.mypos.X, this.mypos.Z), out this.groundHeight);
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
				this.frame1 = this.temp1 % boarDupe.sics[this.clip1 * 2] + boarDupe.sics[this.clip1 * 2 + 1];
				this.frame2 = this.temp2 % boarDupe.sics[this.clip2 * 2] + boarDupe.sics[this.clip2 * 2 + 1];
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000D4890 File Offset: 0x000D2A90
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

		// Token: 0x06000358 RID: 856 RVA: 0x000D48F0 File Offset: 0x000D2AF0
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
						this.speed = this.scale * (boarDupe.acc / 1000f) * this.charge * 1.2f;
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
						this.speed = this.scale * (boarDupe.acc / 1000f) * this.nightSpeed;
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
					this.speed = this.scale * (boarDupe.acc / 1000f) * this.charge * 1.5f;
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
						this.speed = this.scale * (boarDupe.acc / 1000f) * this.nightSpeed;
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
					this.speed = this.scale * (boarDupe.acc / 1000f) * this.nightSpeed;
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

		// Token: 0x06000359 RID: 857 RVA: 0x000D4F44 File Offset: 0x000D3144
		private static void GetHeightFast(ref float[,] heights, Vector2 position, out float height)
		{
			int num = (int)(position.X / boarDupe.unit);
			int num2 = (int)(position.Y / boarDupe.unit);
			num = (int)MathHelper.Clamp(position.X / boarDupe.unit, 0f, (float)(boarDupe.bitmap - 2));
			num2 = (int)MathHelper.Clamp(position.Y / boarDupe.unit, 0f, (float)(boarDupe.bitmap - 2));
			float num3 = position.X % boarDupe.unit / boarDupe.unit;
			float num4 = position.Y % boarDupe.unit / boarDupe.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
		}

		// Token: 0x04000E0E RID: 3598
		private int ch;

		// Token: 0x04000E0F RID: 3599
		public static int homingCount = 0;

		// Token: 0x04000E10 RID: 3600
		public static int homingCount2 = 0;

		// Token: 0x04000E11 RID: 3601
		public static int currentDay = 1;

		// Token: 0x04000E12 RID: 3602
		public static float acc = 3000f;

		// Token: 0x04000E13 RID: 3603
		public static float handicapSpeed = 1f;

		// Token: 0x04000E14 RID: 3604
		public static float handicapTurn = 1f;

		// Token: 0x04000E15 RID: 3605
		public bool undead;

		// Token: 0x04000E16 RID: 3606
		public bool zombie;

		// Token: 0x04000E17 RID: 3607
		public bool borders = true;

		// Token: 0x04000E18 RID: 3608
		public bool interrupt;

		// Token: 0x04000E19 RID: 3609
		public bool oldturning;

		// Token: 0x04000E1A RID: 3610
		public float oldturn;

		// Token: 0x04000E1B RID: 3611
		public float oldtimer;

		// Token: 0x04000E1C RID: 3612
		public float oldspeed;

		// Token: 0x04000E1D RID: 3613
		public int oldclip;

		// Token: 0x04000E1E RID: 3614
		public int oldtemp;

		// Token: 0x04000E1F RID: 3615
		public int oldframe;

		// Token: 0x04000E20 RID: 3616
		public bool death;

		// Token: 0x04000E21 RID: 3617
		public float amp = 1f;

		// Token: 0x04000E22 RID: 3618
		public float freq = 20f;

		// Token: 0x04000E23 RID: 3619
		public float charge = 1f;

		// Token: 0x04000E24 RID: 3620
		public float turnlimit = 0.07f;

		// Token: 0x04000E25 RID: 3621
		public float turnHelper = 0.07f;

		// Token: 0x04000E26 RID: 3622
		public int boarGroup = 1;

		// Token: 0x04000E27 RID: 3623
		public int variant;

		// Token: 0x04000E28 RID: 3624
		public int isStun;

		// Token: 0x04000E29 RID: 3625
		public int resetStun;

		// Token: 0x04000E2A RID: 3626
		public int isChomp;

		// Token: 0x04000E2B RID: 3627
		public int resetChomp;

		// Token: 0x04000E2C RID: 3628
		public int isSpooked;

		// Token: 0x04000E2D RID: 3629
		public int isGrow = -1;

		// Token: 0x04000E2E RID: 3630
		public bool growTrigger;

		// Token: 0x04000E2F RID: 3631
		public int isGrowAge;

		// Token: 0x04000E30 RID: 3632
		private int resetSpooked;

		// Token: 0x04000E31 RID: 3633
		public int isBlind;

		// Token: 0x04000E32 RID: 3634
		public int isHead = -1;

		// Token: 0x04000E33 RID: 3635
		public bool nozombies;

		// Token: 0x04000E34 RID: 3636
		public int isShocked;

		// Token: 0x04000E35 RID: 3637
		public int isZap;

		// Token: 0x04000E36 RID: 3638
		public int isCrumbled;

		// Token: 0x04000E37 RID: 3639
		public int exploded;

		// Token: 0x04000E38 RID: 3640
		public int shottie;

		// Token: 0x04000E39 RID: 3641
		public int timeofDeath;

		// Token: 0x04000E3A RID: 3642
		public static int bitmap;

		// Token: 0x04000E3B RID: 3643
		public static float unit;

		// Token: 0x04000E3C RID: 3644
		public static float Grid;

		// Token: 0x04000E3D RID: 3645
		public int homing;

		// Token: 0x04000E3E RID: 3646
		public int splatIndex;

		// Token: 0x04000E3F RID: 3647
		public int move;

		// Token: 0x04000E40 RID: 3648
		public float myRot;

		// Token: 0x04000E41 RID: 3649
		public float myRotHeld;

		// Token: 0x04000E42 RID: 3650
		public float angleX;

		// Token: 0x04000E43 RID: 3651
		public float angleZ;

		// Token: 0x04000E44 RID: 3652
		public float scale;

		// Token: 0x04000E45 RID: 3653
		public float bluScale;

		// Token: 0x04000E46 RID: 3654
		public Vector3 mypos;

		// Token: 0x04000E47 RID: 3655
		public bool isturning;

		// Token: 0x04000E48 RID: 3656
		public int frame1 = 1;

		// Token: 0x04000E49 RID: 3657
		public int frame2 = 1;

		// Token: 0x04000E4A RID: 3658
		public int temp1;

		// Token: 0x04000E4B RID: 3659
		public int temp2;

		// Token: 0x04000E4C RID: 3660
		public int clip1;

		// Token: 0x04000E4D RID: 3661
		public int clip2;

		// Token: 0x04000E4E RID: 3662
		public float tween;

		// Token: 0x04000E4F RID: 3663
		public int stunLength = 150;

		// Token: 0x04000E50 RID: 3664
		public float speed;

		// Token: 0x04000E51 RID: 3665
		public float turn;

		// Token: 0x04000E52 RID: 3666
		public float timer;

		// Token: 0x04000E53 RID: 3667
		public float startHealth = 5f;

		// Token: 0x04000E54 RID: 3668
		public float health = 5f;

		// Token: 0x04000E55 RID: 3669
		public Matrix transform;

		// Token: 0x04000E56 RID: 3670
		public int blood;

		// Token: 0x04000E57 RID: 3671
		public static List<int> sics;

		// Token: 0x04000E58 RID: 3672
		public int seed;

		// Token: 0x04000E59 RID: 3673
		private float groundHeight;

		// Token: 0x04000E5A RID: 3674
		public int age;

		// Token: 0x04000E5B RID: 3675
		public float tint;

		// Token: 0x04000E5C RID: 3676
		public float oldtint;

		// Token: 0x04000E5D RID: 3677
		public Random random;

		// Token: 0x04000E5E RID: 3678
		public Random randomDeath;

		// Token: 0x04000E5F RID: 3679
		public Random randomHead;

		// Token: 0x04000E60 RID: 3680
		public Random randomPartsFly;

		// Token: 0x04000E61 RID: 3681
		public int immunity;

		// Token: 0x04000E62 RID: 3682
		public int assexplode;

		// Token: 0x04000E63 RID: 3683
		public static ScreenManager sc;

		// Token: 0x04000E64 RID: 3684
		public float nightSpeed = 1f;

		// Token: 0x04000E65 RID: 3685
		public float scaleSpeed = 1f;

		// Token: 0x04000E66 RID: 3686
		private int defaultClip;
	}
}
