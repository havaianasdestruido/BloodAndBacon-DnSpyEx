using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Steamworks;

namespace Blood
{
	// Token: 0x020000D3 RID: 211
	public class localPlayer
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x001C0720 File Offset: 0x001BE920
		public localPlayer(ContentManager content, Vector3 vv, float rot, int xtend)
		{
			this.simulationState.npcPosition = vv;
			this.simulationState.npcRotation = -1.57f;
			this.feetRot = -1.57f;
			this.lastFeetRot = -1.57f;
			this.simulationState.npcTilt = 0f;
			this.previousState = this.simulationState;
			this.displayState = this.simulationState;
			this.displayState.npcPosition = vv;
			this.feetRot = this.displayState.npcRotation;
			this.lastFeetRot = this.displayState.npcRotation;
			this.headRot = this.displayState.npcRotation;
			this.currentSmoothing = 1f;
			this.smoothingDecay = 0.1f;
			this.now = new localPlayer.nowState();
			this.boneTransforms = new Matrix[29];
			this.creature.id = 65000;
			this.creaturemulti.id = 65000;
			this.creaturemulti.id2 = 65000;
			this.creaturemulti.id3 = 65000;
			this.creaturemulti.id4 = 65000;
			this.creaturemulti.id5 = 65000;
			this.creatureShock.id = 65000;
			this.creatureShock.id2 = 65000;
			this.creatureShock.id3 = 65000;
			this.creatureShock.id4 = 65000;
			this.creatureShock.id5 = 65000;
			this.creatureShock.id6 = 65000;
			this.creatureShock.id7 = 65000;
			this.creatureShock.id8 = 65000;
			this.shatter.id = 65000;
			this.shatter.id2 = 65000;
			this.shatter.id3 = 65000;
			this.shatter.id4 = 65000;
			this.shatter.id5 = 65000;
			this.shatter.id6 = 65000;
			this.shatter.id7 = 65000;
			this.shatter.id8 = 65000;
			this.pumpkinID = byte.MaxValue;
			this.partID = 0;
			this.frame1 = 100000f;
			this.step1 = this.frame1;
			this.animList.Capacity = 20;
			this.x1 = 150f + localPlayer.Grid / 2f;
			this.x2 = 640f + localPlayer.Grid / 2f;
			this.z1 = 1470f + localPlayer.Grid / 2f;
			this.z2 = 1780f + localPlayer.Grid / 2f;
			this.u1 = 590f + localPlayer.Grid / 2f;
			this.u2 = 730f + localPlayer.Grid / 2f;
			this.w1 = 1570f + localPlayer.Grid / 2f;
			this.w2 = 1630f + localPlayer.Grid / 2f;
			this.b0 = new Vector4(3145f, 3730f, 4465f, 4785f);
			this.b1 = new Vector4(this.x1, this.x2, this.z1, this.z2);
			this.b2 = new Vector4(this.u1, this.u2, this.w1, this.w2);
			this.limitX = new Vector2(650f, 5350f);
			this.limitZ = new Vector2(650f, 5350f);
			if (xtend == 1)
			{
				this.limitX = new Vector2(650f, 11000f);
				this.limitZ = new Vector2(650f, 5350f);
				return;
			}
			if (xtend == 2)
			{
				this.limitX = new Vector2(200f, 5800f);
				this.limitZ = new Vector2(200f, 5800f);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x001C0CB4 File Offset: 0x001BEEB4
		public bool insideBarn(Vector3 pos)
		{
			return pos.Y <= 200f && this.b0.X < pos.X && pos.X < this.b0.Y && this.b0.Z < pos.Z && pos.Z < this.b0.W && this.simulationState.npcPosition.Y > -200f;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x001C0D3C File Offset: 0x001BEF3C
		public void UpdateLocal(ref float[,] heights, Vector2 Pos, float Tilt, float Rot, float barndoor, int gamestate, bool flying)
		{
			float num = 2.6f;
			this.vec = Vector3.Transform(new Vector3(-Pos.X, 0f, Pos.Y), Matrix.CreateRotationY(Rot)) * (Math.Abs(Pos.X) * num * 0.9f + Math.Abs(Pos.Y) * num);
			if (this.jumping)
			{
				this.fallVec += this.vec * 0.1f;
				float num2 = this.vec.Length();
				if (this.milkEffects > 0 && !this.inBarn)
				{
					num2 = Math.Max(16f, num2);
				}
				if (this.fallVec.Length() > num2)
				{
					this.fallVec = Vector3.Normalize(this.fallVec) * num2;
				}
				this.vec = this.fallVec + this.jump;
				this.fallVec *= 0.94f;
			}
			if (this.futureDamage == 0f)
			{
				this.jump *= 0.98f;
			}
			this.vec += this.motionVec;
			this.motionVec *= 0.9f;
			this.stickLen = this.vec.LengthSquared();
			this.lastHite = this.simulationState.npcPosition.Y;
			this.simulationState.npcRotation = Rot;
			this.simulationState.npcTilt = Tilt;
			this.inSquare = false;
			if (gamestate != 1)
			{
				bool flag = this.simulationState.npcPosition.Y <= 200f && this.simulationState.npcPosition.Y > -200f;
				bool flag2 = barndoor >= 120f;
				Vector2 vector = new Vector2(this.simulationState.npcPosition.X, this.simulationState.npcPosition.Z);
				bool flag3 = this.inbox(vector, this.b1);
				bool flag4 = false;
				if (flag2)
				{
					flag4 = this.inbox(vector, this.b2);
				}
				if ((flag3 || flag4) && flag)
				{
					this.inSquare = true;
					flag3 = this.inbox(vector, this.b1);
					flag4 = false;
					if (flag2)
					{
						flag4 = this.inbox(vector, this.b2);
					}
					vector += new Vector2(this.vec.X, this.vec.Z);
					if (this.breachx(vector, this.b1, this.b2, flag3, flag4, 2))
					{
						this.vec.X = 0f;
					}
					if (this.breachz(vector, this.b1, this.b2, flag3, flag4, 2))
					{
						this.vec.Z = 0f;
					}
				}
			}
			this.inBarn = this.inSquare;
			float num3 = 1f;
			float num4 = -0.9f;
			if (this.inFarm == 2 && !this.nearItems && this.simulationState.npcPosition.Y > -306f)
			{
				num3 = 0.6f;
				num4 = -0.9f;
			}
			if (!this.inSquare)
			{
				float num5 = 0f;
				localPlayer.GetHeightFast(ref heights, this.simulationState.npcPosition + this.vec, ref this.simulationState.npcPosition.Y, ref num5, out this.normal);
				this.ground = this.simulationState.npcPosition.Y;
				if (flying)
				{
					this.ground = this.floaty;
				}
				float num6 = 1f;
				if (this.simulationState.npcPosition.Y - this.lastHite < 0f)
				{
					num6 = -1f;
				}
				num5 *= num6;
				this.slope = 0f;
				if (this.stickLen > 0.01f)
				{
					this.slope = num5 / 30f;
					if (this.slope <= num3 || flying)
					{
						this.simulationState.npcPosition = this.simulationState.npcPosition + this.vec;
					}
					else
					{
						localPlayer.GetHeightFast(ref heights, this.simulationState.npcPosition + new Vector3(this.vec.X, 0f, 0f) + Vector3.Zero, ref this.simulationState.npcPosition.Y, ref num5, out this.normal);
						num6 = 1f;
						if (this.simulationState.npcPosition.Y - this.lastHite < 0f)
						{
							num6 = -1f;
						}
						num5 *= num6;
						this.slope = num5 / 30f;
						if (this.slope <= num3)
						{
							this.simulationState.npcPosition.X = this.simulationState.npcPosition.X + this.vec.X;
						}
						else
						{
							localPlayer.GetHeightFast(ref heights, this.simulationState.npcPosition + new Vector3(0f, 0f, this.vec.Z) + Vector3.Zero, ref this.simulationState.npcPosition.Y, ref num5, out this.normal);
							num6 = 1f;
							if (this.simulationState.npcPosition.Y - this.lastHite < 0f)
							{
								num6 = -1f;
							}
							num5 *= num6;
							this.slope = num5 / 30f;
							if (this.slope <= num3)
							{
								this.simulationState.npcPosition.Z = this.simulationState.npcPosition.Z + this.vec.Z;
							}
							else
							{
								this.simulationState.npcPosition.Y = this.lastHite;
							}
						}
					}
					if (this.slope < num4 && !this.jumping)
					{
						this.jumpCalled = true;
						this.fallGrav = -1f;
						this.fallAcc = -1f;
					}
				}
			}
			else
			{
				this.simulationState.npcPosition = this.simulationState.npcPosition + this.vec;
			}
			if (this.jumping || flying)
			{
				this.lastHite += this.fallGrav;
				localPlayer.GetGround(ref heights, this.simulationState.npcPosition, ref this.ground, out this.normal);
				if (this.inBarn)
				{
					this.ground = 0f;
				}
				if (flying)
				{
					this.ground = this.floaty;
					this.lastHite = this.floaty;
				}
				this.simulationState.npcPosition.Y = MathHelper.Max(this.ground, this.lastHite);
			}
			if (this.simulationState.npcPosition.Y > this.ground || this.jumpCalled)
			{
				if (!this.jumping)
				{
					this.fallVec.X = this.vec.X;
					this.fallVec.Z = this.vec.Z;
				}
				this.jumpCalled = false;
				this.jumping = true;
				this.fallGrav += this.fallAcc;
				if (this.fallGrav < -40f)
				{
					this.fallGrav = -40f;
				}
				this.fallAcc -= 0.001f;
				if (this.fallGrav > 0f)
				{
					this.fallAcc -= 0.03f;
				}
				if (!flying && Math.Abs(this.normal.Y) < 0.5f && this.simulationState.npcPosition.Y <= this.ground + 40f && !this.inBarn)
				{
					this.motionVec.X = this.normal.X * 5f;
					this.motionVec.Z = this.normal.Z * 5f;
					this.fallVec.X = 0f;
					this.fallVec.Z = 0f;
				}
			}
			else
			{
				this.jumpCalled = false;
				this.jumping = false;
				this.jumpCount = 0;
				this.fallVec = Vector3.Zero;
				this.fallGrav = 0f;
				this.fallAcc = 0f;
				if (this.futureDamage > 0f)
				{
					this.damHealth(this.futureDamage, false);
				}
				this.futureDamage = 0f;
			}
			if (this.inFarm == 1)
			{
				this.simulationState.npcPosition.X = MathHelper.Clamp(this.simulationState.npcPosition.X, this.limitX.X, this.limitX.Y);
				this.simulationState.npcPosition.Z = MathHelper.Clamp(this.simulationState.npcPosition.Z, this.limitZ.X, this.limitZ.Y);
			}
			this.oldRot = this.displayState.npcRotation;
			this.oldPos = this.displayState.npcPosition;
			this.displayState = this.simulationState;
			this.calcClips();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x001C1660 File Offset: 0x001BF860
		private bool inbox(Vector2 p, Vector4 box)
		{
			bool flag = true;
			if (p.X < box.X || p.X > box.Y || p.Y < box.Z || p.Y > box.W)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x001C16B4 File Offset: 0x001BF8B4
		private bool breachx(Vector2 p, Vector4 b1, Vector4 b2, bool inbox1, bool inbox2, int weakwall)
		{
			bool flag = false;
			if (inbox1 && !inbox2)
			{
				if (p.X <= b1.X)
				{
					flag = true;
				}
				if (p.X >= b1.Y)
				{
					flag = true;
				}
			}
			if (!inbox1 && inbox2)
			{
				if (p.X <= b2.X && weakwall != 1)
				{
					flag = true;
				}
				if (p.X >= b2.Y && weakwall != 2)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x001C1728 File Offset: 0x001BF928
		private bool breachz(Vector2 p, Vector4 b1, Vector4 b2, bool inbox1, bool inbox2, int weakwall)
		{
			bool flag = false;
			if (inbox1 && !inbox2)
			{
				if (p.Y <= b1.Z)
				{
					flag = true;
				}
				if (p.Y >= b1.W)
				{
					flag = true;
				}
			}
			if (!inbox1 && inbox2)
			{
				if (p.Y <= b2.Z && weakwall != 3)
				{
					flag = true;
				}
				if (p.Y >= b2.W && weakwall != 4)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x001C179C File Offset: 0x001BF99C
		private void calcClips()
		{
			this.dir1 = 0f;
			this.dir2 = 0f;
			this.mySign = 1f;
			this.mult = 1.3f;
			this.cross = 0.3f;
			this.rotOffset = 0f;
			this.dist = Vector2.Distance(new Vector2(this.oldPos.X, this.oldPos.Z), new Vector2(this.displayState.npcPosition.X, this.displayState.npcPosition.Z));
			if (this.dist > 0f)
			{
				this.v1 = Vector2.Normalize(new Vector2(this.displayState.npcPosition.X, this.displayState.npcPosition.Z) - new Vector2(this.oldPos.X, this.oldPos.Z));
				this.v2 = Vector3.Transform(new Vector3(0f, 0f, 1f), Matrix.CreateRotationY(this.displayState.npcRotation));
				this.v3 = Vector3.Transform(new Vector3(1f, 0f, 0f), Matrix.CreateRotationY(this.displayState.npcRotation));
				this.dir1 = Vector2.Dot(this.v1, new Vector2(this.v2.X, this.v2.Z));
				this.dir2 = Vector2.Dot(this.v1, new Vector2(this.v3.X, this.v3.Z));
			}
			if (Math.Abs(this.dir1) < this.cross)
			{
				this.mult = 1.6f;
				this.mySign = 1f;
				if (this.dir2 < 0f)
				{
					this.mySign = -1f;
				}
			}
			else if (this.dir1 < 0f)
			{
				this.mySign = -1f;
			}
			if (Math.Abs(this.dir1) >= this.cross)
			{
				this.rotOffset = (1f - Math.Abs(this.dir1)) * 2.5f;
			}
			else
			{
				this.rotOffset = -Math.Abs(this.dir1) * 2.5f;
			}
			if (this.dir1 < 0f)
			{
				this.rotOffset *= -1f;
			}
			if (this.dir2 < 0f)
			{
				this.rotOffset *= -1f;
			}
			this.incry = (this.rotGoal - this.rotOffset) / 10f;
			this.rotGoal -= this.incry;
			if (this.milkEffects > 0)
			{
				this.mult = 1f;
			}
			this.incr = this.dist * this.mySign * this.mult;
			this.incr = MathHelper.Clamp(this.incr, -6f, 6f);
			float num = 0.8f;
			if (this.isDown)
			{
				num = 0.8f;
			}
			if (this.closeCam)
			{
				num = 1f - Math.Abs(this.displayState.npcTilt) / 1.5f;
			}
			if (this.displayState.npcRotation >= this.feetRot + num)
			{
				this.feetRot = this.displayState.npcRotation - num;
			}
			if (this.displayState.npcRotation <= this.feetRot - num)
			{
				this.feetRot = this.displayState.npcRotation + num;
			}
			this.moving = this.oldPos != this.displayState.npcPosition;
			this.notRotating = this.oldRot == this.displayState.npcRotation && !this.isDown;
			this.headfeetAligned = this.feetRot == this.displayState.npcRotation;
			if (this.moving || (this.notRotating && !this.headfeetAligned))
			{
				if (this.alreadyMoving)
				{
					this.rotLerp = 0f;
					this.restTime = 0;
					this.feetRot = this.displayState.npcRotation + this.rotGoal;
					this.lastFeetRot = this.feetRot;
				}
				else
				{
					this.restTime++;
					if (this.restTime > 70 || this.moving)
					{
						this.rotLerp += 0.025f;
						if (this.moving)
						{
							this.rotLerp += 0.05f;
						}
						if (this.rotLerp > 1f)
						{
							this.rotLerp = 0f;
							this.restTime = 0;
							this.lastFeetRot = this.displayState.npcRotation + this.rotGoal;
							this.alreadyMoving = true;
						}
						this.feetRot = MathHelper.Hermite(this.lastFeetRot, 0f, this.displayState.npcRotation + this.rotGoal, 0f, this.rotLerp);
					}
				}
			}
			else
			{
				this.restTime = 0;
				this.rotLerp = 0f;
				if (!this.moving)
				{
					this.lastFeetRot = this.feetRot;
				}
			}
			if (!this.moving)
			{
				this.alreadyMoving = false;
			}
			this.headRot = this.displayState.npcRotation - this.feetRot;
			this.transform = Matrix.CreateRotationY(this.feetRot) * Matrix.CreateTranslation(this.displayState.npcPosition);
			if (!this.isDown)
			{
				if (this.dist <= 0f && !this.jumping)
				{
					this.restTime2++;
					if (this.clip1 != 1 && this.restTime2 > 5 && !this.isLiftingOpponent)
					{
						this.restTime2 = 0;
						this.clip2 = 1;
						this.swapClips();
					}
					if (this.isLiftingOpponent && this.clip1 != 10)
					{
						this.clip2 = 10;
						this.swapClips();
					}
				}
				if (this.jumping && this.clip1 != 16)
				{
					this.restTime2 = 5;
					this.clip2 = 16;
					this.swapClips();
				}
				if (this.dist > 0f && !this.jumping)
				{
					this.restTime2 = 0;
					if (this.clip1 != 0 && Math.Abs(this.dir1) >= this.cross)
					{
						this.clip2 = 0;
						this.swapClips();
					}
					if (this.clip1 != 2 && Math.Abs(this.dir1) < this.cross)
					{
						this.clip2 = 2;
						this.swapClips();
					}
				}
				if (this.clip1 == 16 && this.tween == 1f)
				{
					this.incr = 1f;
				}
				else if (this.clip1 == 1 && this.tween == 1f)
				{
					this.incr = 0.4f;
				}
				else if (this.clip1 == 10 && this.tween == 1f)
				{
					this.incr = 0.4f;
				}
				this.step1 += this.incr;
				if (this.closeCam)
				{
					float num2 = MathHelper.Hermite(0.3f, 0f, 1f, 0f, Math.Abs(this.displayState.npcTilt * 0.833f));
					if (num2 >= 0.99f)
					{
						this.step1 = this.frame1;
					}
					this.incr *= num2;
				}
			}
			else
			{
				this.fallManager();
			}
			this.frame1 += this.incr;
			if (this.tween < 1f)
			{
				this.tween += 0.1f;
				this.tween = MathHelper.Clamp(this.tween, 0f, 1f);
			}
			if (this.frame1 < 0f)
			{
				this.frame1 = 100000f;
				this.step1 = this.frame1;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x001C1F7C File Offset: 0x001C017C
		private void swapClips()
		{
			this.tween = 1f - this.tween;
			this.tween = MathHelper.Clamp(this.tween, 0f, 1f);
			int num = this.clip2;
			this.clip2 = this.clip1;
			this.clip1 = num;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x001C1FD0 File Offset: 0x001C01D0
		private void fallManager()
		{
			if (!this.noArms && this.gunChoice != localPlayer.pType)
			{
				this.gunChoice = this.primaryChoice;
				this.lastWeapon = this.gunChoice;
				this.now.weapon = this.gunChoice;
			}
			if (this.now.health == 100f)
			{
				this.now.tempHealth -= 0.02f;
				if (this.now.tempHealth < 0f)
				{
					this.now.tempHealth = 0f;
				}
			}
			else
			{
				this.damHealth(0.02f, false);
				this.now.tempHealth = this.now.health;
			}
			if (this.now.tempHealth < 1f && this.fallState < 11)
			{
				this.now.health = 0f;
				this.now.tempHealth = 0f;
				this.frame1 = 0f;
				if (this.fallState == 2)
				{
					this.frame1 = 30f;
				}
				this.fallState = 11;
			}
			if (this.fallState == 0)
			{
				this.fallState = 1;
				this.frame1 = 0f;
				this.clip2 = 8;
				this.swapClips();
				this.incr = 0.8f;
				this.triggerEvent = 3;
				if (!this.closeCam && !this.autoCamOn)
				{
					this.autoCamOn = true;
					this.autoCamTarget = 1;
					this.autoCamTimer = 0f;
				}
				return;
			}
			if (this.fallState == 1)
			{
				this.incr = 0.8f;
				if (this.frame1 > 30f)
				{
					this.fallState = 2;
					this.clip2 = 9;
					this.swapClips();
				}
				return;
			}
			if (this.fallState == 2)
			{
				this.incr = 0.4f;
				return;
			}
			if (this.fallState == 3)
			{
				this.triggerEvent = 2;
				this.fallState = 4;
				this.now.health = 100f;
				this.frame1 = 0f;
				this.clip2 = 7;
				this.swapClips();
				this.incr = 0.4f;
				if (this.remoteUsingMilk)
				{
					this.incr = 0.7f;
				}
				if (this.closeCam && !this.autoCamOn)
				{
					this.autoCamOn = true;
					this.autoCamTarget = 1;
					this.autoCamTimer = 0f;
				}
				return;
			}
			if (this.fallState == 4)
			{
				this.incr = 0.4f;
				if (this.remoteUsingMilk)
				{
					this.incr = 0.7f;
				}
				this.now.health = 100f;
				if (this.frame1 > 85f)
				{
					this.fallState = 5;
					this.restTime2 = 0;
					this.clip2 = 1;
					this.swapClips();
				}
				return;
			}
			if (this.fallState == 5)
			{
				this.now.health = 100f;
				if (this.tween >= 0.9f)
				{
					this.fallState = 0;
					this.now.health = 198f;
					this.triggerEvent = 1;
					if (this.closeCam && !this.autoCamOn)
					{
						this.autoCamOn = false;
						this.autoCamTimer = 0f;
						this.autoCamTarget = 0;
						return;
					}
					this.autoCamOn = true;
					this.autoCamTarget = 0;
					this.autoCamTimer = 1f;
				}
				return;
			}
			if (this.fallState == 11)
			{
				this.now.health = 0f;
				this.clip2 = 11;
				this.swapClips();
				this.fallState = 12;
				this.incr = 0.6f;
				this.triggerEvent = 4;
				if (!this.autoCamOn)
				{
					this.autoCamOn = true;
					this.autoCamTarget = 1;
					this.autoCamTimer = 0f;
				}
				return;
			}
			if (this.fallState == 12)
			{
				this.incr = 0.6f;
				this.now.health = 0f;
				if (this.frame1 > 128f)
				{
					this.frame1 = 128f;
					this.incr = 0f;
				}
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x001C23B8 File Offset: 0x001C05B8
		public void damHealth(float dam, bool cheat)
		{
			if (cheat)
			{
				return;
			}
			if (this.now.health == 100f)
			{
				return;
			}
			this.now.health -= dam;
			if (this.now.health < 0f)
			{
				this.now.health = 0f;
			}
			if (this.now.health == 100f)
			{
				this.now.health = 98f;
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x001C2434 File Offset: 0x001C0634
		private static void GetHeightFast(ref float[,] heights, Vector3 position, ref float height, ref float rise, out Vector3 normal)
		{
			int num = (int)(position.X / localPlayer.Unit);
			int num2 = (int)(position.Z / localPlayer.Unit);
			float num3 = position.X % localPlayer.Unit / localPlayer.Unit;
			float num4 = position.Z % localPlayer.Unit / localPlayer.Unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			float num7 = MathHelper.Max(heights[num, num2], heights[num + 1, num2 + 1]);
			float num8 = MathHelper.Max(heights[num + 1, num2], heights[num, num2 + 1]);
			float num9 = MathHelper.Max(num7, num8);
			float num10 = MathHelper.Min(heights[num, num2], heights[num + 1, num2 + 1]);
			float num11 = MathHelper.Min(heights[num + 1, num2], heights[num, num2 + 1]);
			float num12 = MathHelper.Min(num10, num11);
			rise = num9 - num12;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x001C25B4 File Offset: 0x001C07B4
		private static void GetGround(ref float[,] heights, Vector3 position, ref float height, out Vector3 normal)
		{
			int num = (int)(position.X / localPlayer.Unit);
			int num2 = (int)(position.Z / localPlayer.Unit);
			float num3 = position.X % localPlayer.Unit / localPlayer.Unit;
			float num4 = position.Z % localPlayer.Unit / localPlayer.Unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x04001DFE RID: 7678
		private ScreenManager sc;

		// Token: 0x04001DFF RID: 7679
		public int inFarm = 1;

		// Token: 0x04001E00 RID: 7680
		public bool nearItems;

		// Token: 0x04001E01 RID: 7681
		public Vector3 vec;

		// Token: 0x04001E02 RID: 7682
		public int isLiftingIndex;

		// Token: 0x04001E03 RID: 7683
		public bool sentgameINFO;

		// Token: 0x04001E04 RID: 7684
		public bool infoRequest;

		// Token: 0x04001E05 RID: 7685
		public List<CSteamID> steamNameID = new List<CSteamID>();

		// Token: 0x04001E06 RID: 7686
		public float floaty;

		// Token: 0x04001E07 RID: 7687
		private bool inSquare;

		// Token: 0x04001E08 RID: 7688
		private Vector2 limitX;

		// Token: 0x04001E09 RID: 7689
		private Vector2 limitZ;

		// Token: 0x04001E0A RID: 7690
		public Vector3 normal;

		// Token: 0x04001E0B RID: 7691
		public bool bobblehead;

		// Token: 0x04001E0C RID: 7692
		public bool kicked;

		// Token: 0x04001E0D RID: 7693
		public Vector3 motionVec = Vector3.Zero;

		// Token: 0x04001E0E RID: 7694
		public Vector3 jump = Vector3.Zero;

		// Token: 0x04001E0F RID: 7695
		public byte uvIndex;

		// Token: 0x04001E10 RID: 7696
		public byte cuttyXcoord;

		// Token: 0x04001E11 RID: 7697
		public byte cuttyYcoord;

		// Token: 0x04001E12 RID: 7698
		public byte cuttyindexBit;

		// Token: 0x04001E13 RID: 7699
		public ushort cuttyhealth = 2500;

		// Token: 0x04001E14 RID: 7700
		public byte cuttyDamBit;

		// Token: 0x04001E15 RID: 7701
		public ushort cuttyDamage;

		// Token: 0x04001E16 RID: 7702
		public bool cuttyonFire;

		// Token: 0x04001E17 RID: 7703
		public byte bossindexBit;

		// Token: 0x04001E18 RID: 7704
		public ushort bosshealth = 2500;

		// Token: 0x04001E19 RID: 7705
		public byte bossDamBit;

		// Token: 0x04001E1A RID: 7706
		public ushort bossDamage;

		// Token: 0x04001E1B RID: 7707
		public float futureDamage;

		// Token: 0x04001E1C RID: 7708
		public int triggerEvent;

		// Token: 0x04001E1D RID: 7709
		public bool bloodExists;

		// Token: 0x04001E1E RID: 7710
		public int bloodCoil;

		// Token: 0x04001E1F RID: 7711
		public int bloodIndex;

		// Token: 0x04001E20 RID: 7712
		public float bloodPool = 16f;

		// Token: 0x04001E21 RID: 7713
		public Matrix bloodPos;

		// Token: 0x04001E22 RID: 7714
		public float bloodRot;

		// Token: 0x04001E23 RID: 7715
		public Matrix[] boneTransforms;

		// Token: 0x04001E24 RID: 7716
		public bool isDown;

		// Token: 0x04001E25 RID: 7717
		public int fallState;

		// Token: 0x04001E26 RID: 7718
		public bool isLiftingOpponent;

		// Token: 0x04001E27 RID: 7719
		public bool cheats;

		// Token: 0x04001E28 RID: 7720
		public bool onMilk;

		// Token: 0x04001E29 RID: 7721
		public bool onHulk;

		// Token: 0x04001E2A RID: 7722
		public bool remoteUsingMilk;

		// Token: 0x04001E2B RID: 7723
		public bool remoteUsingHulk;

		// Token: 0x04001E2C RID: 7724
		public bool noArms;

		// Token: 0x04001E2D RID: 7725
		public int armTimer;

		// Token: 0x04001E2E RID: 7726
		public float slope;

		// Token: 0x04001E2F RID: 7727
		public bool inBarn;

		// Token: 0x04001E30 RID: 7728
		private float x1;

		// Token: 0x04001E31 RID: 7729
		private float x2;

		// Token: 0x04001E32 RID: 7730
		private float z1;

		// Token: 0x04001E33 RID: 7731
		private float z2;

		// Token: 0x04001E34 RID: 7732
		private float u1;

		// Token: 0x04001E35 RID: 7733
		private float u2;

		// Token: 0x04001E36 RID: 7734
		private float w1;

		// Token: 0x04001E37 RID: 7735
		private float w2;

		// Token: 0x04001E38 RID: 7736
		private Vector4 b0;

		// Token: 0x04001E39 RID: 7737
		private Vector4 b1;

		// Token: 0x04001E3A RID: 7738
		private Vector4 b2;

		// Token: 0x04001E3B RID: 7739
		public float ground;

		// Token: 0x04001E3C RID: 7740
		private float lastHite;

		// Token: 0x04001E3D RID: 7741
		private bool moving;

		// Token: 0x04001E3E RID: 7742
		private bool notRotating;

		// Token: 0x04001E3F RID: 7743
		private bool headfeetAligned;

		// Token: 0x04001E40 RID: 7744
		private bool alreadyMoving;

		// Token: 0x04001E41 RID: 7745
		public bool falling;

		// Token: 0x04001E42 RID: 7746
		public bool jumping;

		// Token: 0x04001E43 RID: 7747
		public bool jumpCalled;

		// Token: 0x04001E44 RID: 7748
		public int jumpCount;

		// Token: 0x04001E45 RID: 7749
		public byte difficulty;

		// Token: 0x04001E46 RID: 7750
		private float dist;

		// Token: 0x04001E47 RID: 7751
		private float dir1;

		// Token: 0x04001E48 RID: 7752
		private float dir2;

		// Token: 0x04001E49 RID: 7753
		private float mySign = 1f;

		// Token: 0x04001E4A RID: 7754
		private float mult = 1.3f;

		// Token: 0x04001E4B RID: 7755
		private float cross = 0.3f;

		// Token: 0x04001E4C RID: 7756
		private float rotOffset;

		// Token: 0x04001E4D RID: 7757
		private Vector2 v1;

		// Token: 0x04001E4E RID: 7758
		private Vector3 v2;

		// Token: 0x04001E4F RID: 7759
		private Vector3 v3;

		// Token: 0x04001E50 RID: 7760
		private float incry;

		// Token: 0x04001E51 RID: 7761
		public Matrix cambone;

		// Token: 0x04001E52 RID: 7762
		public Matrix pistolHand;

		// Token: 0x04001E53 RID: 7763
		public Matrix oldpistolHand;

		// Token: 0x04001E54 RID: 7764
		public Matrix headbone;

		// Token: 0x04001E55 RID: 7765
		public Vector2 recoilVec;

		// Token: 0x04001E56 RID: 7766
		public Vector3 gunpos;

		// Token: 0x04001E57 RID: 7767
		public Vector3 gunlook;

		// Token: 0x04001E58 RID: 7768
		public float gunDelay;

		// Token: 0x04001E59 RID: 7769
		public int flashChoice;

		// Token: 0x04001E5A RID: 7770
		public int flashSide;

		// Token: 0x04001E5B RID: 7771
		public int nextgunChoice;

		// Token: 0x04001E5C RID: 7772
		public float sprint = 1.5f;

		// Token: 0x04001E5D RID: 7773
		public float sprintTime = 240f;

		// Token: 0x04001E5E RID: 7774
		public float sprintRest;

		// Token: 0x04001E5F RID: 7775
		public int gunChoice = 2;

		// Token: 0x04001E60 RID: 7776
		public int lastWeapon = 2;

		// Token: 0x04001E61 RID: 7777
		public int primaryChoice = 2;

		// Token: 0x04001E62 RID: 7778
		public int secondaryChoice = 2;

		// Token: 0x04001E63 RID: 7779
		public int[] resetmag = new int[]
		{
			7, 0, 15, 0, 10, 0, 40, 0, 10, 0,
			30, 0, 35, 0, 1, 0, 80, 0, 50, 0,
			30, 0
		};

		// Token: 0x04001E64 RID: 7780
		public int[] mag = new int[]
		{
			7, 0, 15, 0, 10, 0, 40, 0, 10, 0,
			30, 0, 35, 0, 1, 0, 80, 0, 50, 0,
			30, 0
		};

		// Token: 0x04001E65 RID: 7781
		public int[] resetammo = new int[]
		{
			50000, 0, 50000, 0, 50000, 0, 200, 0, 80, 0,
			240, 0, 300, 0, 0, 0, 50000, 0, 300, 0,
			250, 0
		};

		// Token: 0x04001E66 RID: 7782
		public int[] ammo = new int[]
		{
			50000, 0, 50000, 0, 50000, 0, 200, 0, 80, 0,
			240, 0, 300, 0, 0, 0, 50000, 0, 300, 0,
			250, 0
		};

		// Token: 0x04001E67 RID: 7783
		public int[] doubleammo = new int[]
		{
			50000, 0, 50000, 0, 50000, 0, 400, 0, 160, 0,
			480, 0, 600, 0, 0, 0, 50000, 0, 650, 0,
			500, 0
		};

		// Token: 0x04001E68 RID: 7784
		public int milkEffects;

		// Token: 0x04001E69 RID: 7785
		public int hulkEffects;

		// Token: 0x04001E6A RID: 7786
		public int stats_countdown = 600;

		// Token: 0x04001E6B RID: 7787
		public bool stats_ready;

		// Token: 0x04001E6C RID: 7788
		public bool stats_record;

		// Token: 0x04001E6D RID: 7789
		public bool stats_send;

		// Token: 0x04001E6E RID: 7790
		public int stats_sendTimer;

		// Token: 0x04001E6F RID: 7791
		public bool stats_show;

		// Token: 0x04001E70 RID: 7792
		public int stats_timer;

		// Token: 0x04001E71 RID: 7793
		public ushort stats_shotsfired;

		// Token: 0x04001E72 RID: 7794
		public ushort stats_shotshit;

		// Token: 0x04001E73 RID: 7795
		public ushort stats_headshots;

		// Token: 0x04001E74 RID: 7796
		public ushort stats_asshits;

		// Token: 0x04001E75 RID: 7797
		public ushort stats_bulkified;

		// Token: 0x04001E76 RID: 7798
		public ushort stats_shottied;

		// Token: 0x04001E77 RID: 7799
		public ushort stats_grenadier;

		// Token: 0x04001E78 RID: 7800
		public ushort stats_melees;

		// Token: 0x04001E79 RID: 7801
		public ushort stats_meleehits;

		// Token: 0x04001E7A RID: 7802
		public ushort stats_spinebounces;

		// Token: 0x04001E7B RID: 7803
		public ushort stats_oneshots;

		// Token: 0x04001E7C RID: 7804
		public ushort stats_knockdown;

		// Token: 0x04001E7D RID: 7805
		public float animCount = -1f;

		// Token: 0x04001E7E RID: 7806
		public float animTween;

		// Token: 0x04001E7F RID: 7807
		public List<int> animList = new List<int>();

		// Token: 0x04001E80 RID: 7808
		public int animClip = -1;

		// Token: 0x04001E81 RID: 7809
		public int animMax;

		// Token: 0x04001E82 RID: 7810
		public int animMin;

		// Token: 0x04001E83 RID: 7811
		public int animLoop;

		// Token: 0x04001E84 RID: 7812
		public float recoilTimer2;

		// Token: 0x04001E85 RID: 7813
		public float recoilTimer;

		// Token: 0x04001E86 RID: 7814
		public float flashTimer;

		// Token: 0x04001E87 RID: 7815
		public float blastTimer;

		// Token: 0x04001E88 RID: 7816
		public float blastRot;

		// Token: 0x04001E89 RID: 7817
		public bool dropshell45;

		// Token: 0x04001E8A RID: 7818
		public bool dropshellAK;

		// Token: 0x04001E8B RID: 7819
		public bool gunFired;

		// Token: 0x04001E8C RID: 7820
		public bool flashfromSide;

		// Token: 0x04001E8D RID: 7821
		public float gunsideScale;

		// Token: 0x04001E8E RID: 7822
		public float gunfrontScale;

		// Token: 0x04001E8F RID: 7823
		public static float Unit = 30f;

		// Token: 0x04001E90 RID: 7824
		public static int bitmap = 200;

		// Token: 0x04001E91 RID: 7825
		public static float Grid = 6000f;

		// Token: 0x04001E92 RID: 7826
		public static int pType = 25;

		// Token: 0x04001E93 RID: 7827
		public bool closeCam = true;

		// Token: 0x04001E94 RID: 7828
		public bool autoCamOn;

		// Token: 0x04001E95 RID: 7829
		public float autoCamTimer;

		// Token: 0x04001E96 RID: 7830
		public int autoCamTarget;

		// Token: 0x04001E97 RID: 7831
		public float headRot;

		// Token: 0x04001E98 RID: 7832
		public float feetRot;

		// Token: 0x04001E99 RID: 7833
		public float stickLen;

		// Token: 0x04001E9A RID: 7834
		public Vector3 fallVec = Vector3.Zero;

		// Token: 0x04001E9B RID: 7835
		public float fallPosition;

		// Token: 0x04001E9C RID: 7836
		public float fallGrav;

		// Token: 0x04001E9D RID: 7837
		public float fallAcc;

		// Token: 0x04001E9E RID: 7838
		private int restTime;

		// Token: 0x04001E9F RID: 7839
		private int restTime2;

		// Token: 0x04001EA0 RID: 7840
		private float rotLerp;

		// Token: 0x04001EA1 RID: 7841
		private float lastFeetRot;

		// Token: 0x04001EA2 RID: 7842
		private float rotGoal;

		// Token: 0x04001EA3 RID: 7843
		private float oldRot;

		// Token: 0x04001EA4 RID: 7844
		private Vector3 oldPos;

		// Token: 0x04001EA5 RID: 7845
		public localPlayer.nowState now;

		// Token: 0x04001EA6 RID: 7846
		public bool tunnelHeal;

		// Token: 0x04001EA7 RID: 7847
		public localPlayer.conductor creature;

		// Token: 0x04001EA8 RID: 7848
		public localPlayer.conductor creaturemulti;

		// Token: 0x04001EA9 RID: 7849
		public localPlayer.conductor creatureShock;

		// Token: 0x04001EAA RID: 7850
		public localPlayer.conductor shatter;

		// Token: 0x04001EAB RID: 7851
		public bool grenToss;

		// Token: 0x04001EAC RID: 7852
		public bool rocketLaunch;

		// Token: 0x04001EAD RID: 7853
		public ushort grenSeed;

		// Token: 0x04001EAE RID: 7854
		public Vector3 grenPos;

		// Token: 0x04001EAF RID: 7855
		public Vector3 grenVeloc;

		// Token: 0x04001EB0 RID: 7856
		public byte grenBounce;

		// Token: 0x04001EB1 RID: 7857
		public byte grenAge;

		// Token: 0x04001EB2 RID: 7858
		public byte grenCook;

		// Token: 0x04001EB3 RID: 7859
		public byte grenState;

		// Token: 0x04001EB4 RID: 7860
		public bool reload;

		// Token: 0x04001EB5 RID: 7861
		public bool farmerTalk;

		// Token: 0x04001EB6 RID: 7862
		public byte farmerIndex;

		// Token: 0x04001EB7 RID: 7863
		public int partTIME;

		// Token: 0x04001EB8 RID: 7864
		public int partTYPE;

		// Token: 0x04001EB9 RID: 7865
		public ushort partID;

		// Token: 0x04001EBA RID: 7866
		public byte partHIT;

		// Token: 0x04001EBB RID: 7867
		public Vector3 partPOS;

		// Token: 0x04001EBC RID: 7868
		public Vector3 partVEL;

		// Token: 0x04001EBD RID: 7869
		public Quaternion partQUAT;

		// Token: 0x04001EBE RID: 7870
		public byte pumpkinID;

		// Token: 0x04001EBF RID: 7871
		public float currentSmoothing;

		// Token: 0x04001EC0 RID: 7872
		public float smoothingDecay;

		// Token: 0x04001EC1 RID: 7873
		public localPlayer.npcState simulationState;

		// Token: 0x04001EC2 RID: 7874
		private localPlayer.npcState previousState;

		// Token: 0x04001EC3 RID: 7875
		public localPlayer.npcState displayState;

		// Token: 0x04001EC4 RID: 7876
		public int oldmyScore = -1;

		// Token: 0x04001EC5 RID: 7877
		public int oldremScore = -1;

		// Token: 0x04001EC6 RID: 7878
		public int oldColtAmmo = -1;

		// Token: 0x04001EC7 RID: 7879
		public int oldAkAmmo = -1;

		// Token: 0x04001EC8 RID: 7880
		public int oldAkMag = -1;

		// Token: 0x04001EC9 RID: 7881
		public Matrix transform;

		// Token: 0x04001ECA RID: 7882
		public float frame1;

		// Token: 0x04001ECB RID: 7883
		public float step1;

		// Token: 0x04001ECC RID: 7884
		public float tween = 1f;

		// Token: 0x04001ECD RID: 7885
		public float incr;

		// Token: 0x04001ECE RID: 7886
		public int clip1;

		// Token: 0x04001ECF RID: 7887
		public int clip2;

		// Token: 0x020000D4 RID: 212
		public class nowState
		{
			// Token: 0x04001ED0 RID: 7888
			public float health = 199f;

			// Token: 0x04001ED1 RID: 7889
			public byte whichLevel;

			// Token: 0x04001ED2 RID: 7890
			public byte pump1Level = 12;

			// Token: 0x04001ED3 RID: 7891
			public byte pump2Level = 12;

			// Token: 0x04001ED4 RID: 7892
			public bool leverOn;

			// Token: 0x04001ED5 RID: 7893
			public byte bonusnpc;

			// Token: 0x04001ED6 RID: 7894
			public bool rocketLoaded;

			// Token: 0x04001ED7 RID: 7895
			public float tempHealth;

			// Token: 0x04001ED8 RID: 7896
			public byte load;

			// Token: 0x04001ED9 RID: 7897
			public ushort myscore;

			// Token: 0x04001EDA RID: 7898
			public ushort remscore;

			// Token: 0x04001EDB RID: 7899
			public bool flashlight;

			// Token: 0x04001EDC RID: 7900
			public int animation;

			// Token: 0x04001EDD RID: 7901
			public int weapon;

			// Token: 0x04001EDE RID: 7902
			public int gunfired;

			// Token: 0x04001EDF RID: 7903
			public byte doorOpen;

			// Token: 0x04001EE0 RID: 7904
			public Vector3 destiny;

			// Token: 0x04001EE1 RID: 7905
			public ushort accuracy;

			// Token: 0x04001EE2 RID: 7906
			public int grinder;
		}

		// Token: 0x020000D5 RID: 213
		public struct conductor
		{
			// Token: 0x04001EE3 RID: 7907
			public byte type;

			// Token: 0x04001EE4 RID: 7908
			public ushort id;

			// Token: 0x04001EE5 RID: 7909
			public ushort id2;

			// Token: 0x04001EE6 RID: 7910
			public ushort id3;

			// Token: 0x04001EE7 RID: 7911
			public ushort id4;

			// Token: 0x04001EE8 RID: 7912
			public ushort id5;

			// Token: 0x04001EE9 RID: 7913
			public ushort id6;

			// Token: 0x04001EEA RID: 7914
			public ushort id7;

			// Token: 0x04001EEB RID: 7915
			public ushort id8;

			// Token: 0x04001EEC RID: 7916
			public byte action;

			// Token: 0x04001EED RID: 7917
			public byte bodypart;

			// Token: 0x04001EEE RID: 7918
			public ushort frame;

			// Token: 0x04001EEF RID: 7919
			public int time;

			// Token: 0x04001EF0 RID: 7920
			public float rot;

			// Token: 0x04001EF1 RID: 7921
			public byte speed;

			// Token: 0x04001EF2 RID: 7922
			public bool died;

			// Token: 0x04001EF3 RID: 7923
			public bool died2;

			// Token: 0x04001EF4 RID: 7924
			public bool died3;

			// Token: 0x04001EF5 RID: 7925
			public bool died4;

			// Token: 0x04001EF6 RID: 7926
			public bool died5;

			// Token: 0x04001EF7 RID: 7927
			public Vector3 veloc;
		}

		// Token: 0x020000D6 RID: 214
		public struct npcState
		{
			// Token: 0x04001EF8 RID: 7928
			public Vector3 npcPosition;

			// Token: 0x04001EF9 RID: 7929
			public float npcRotation;

			// Token: 0x04001EFA RID: 7930
			public float npcTilt;
		}
	}
}
