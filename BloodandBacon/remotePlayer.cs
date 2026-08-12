using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Blood
{
	// Token: 0x0200008A RID: 138
	internal class remotePlayer
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x0011ACE8 File Offset: 0x00118EE8
		public remotePlayer(ContentManager content, Vector3 vv)
		{
			this.simulationState.npcPosition = vv;
			this.simulationState.npcRotation = -1.57f;
			this.feetRot = -1.57f;
			this.lastFeetRot = -1.57f;
			this.simulationState.npcTilt = 0f;
			this.displayState = this.simulationState;
			this.previousState = this.displayState;
			this.currentSmoothing = 1f;
			this.smoothingDecay = 0.1f;
			this.now = new remotePlayer.nowState();
			this.mySkin = new Matrix[29];
			this.myState.Capacity = 20;
			this.creature.id = 65000;
			this.partID = 0;
			this.frame1 = 100000f;
			this.animList.Capacity = 20;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0011AE84 File Offset: 0x00119084
		public void handleHealth()
		{
			if (this.isDown && this.now.health > 105f && this.fallState != 5 && this.fallState != 4)
			{
				this.fallState = 0;
				this.isDown = false;
				return;
			}
			if (this.fallState == 0 && this.now.health <= 99f && this.now.health > 0f && !this.isDown)
			{
				this.isDown = true;
				return;
			}
			if (this.fallState <= 2 && this.now.health == 100f)
			{
				this.fallState = 3;
				return;
			}
			if (this.fallState >= 3 && this.fallState <= 5 && this.now.health <= 99f && this.now.health > 0f)
			{
				this.isDown = true;
				this.fallState = 0;
				return;
			}
			if (this.fallState < 11 && this.now.health == 0f)
			{
				this.frame1 = 0f;
				this.isLiftingYou = false;
				if (this.fallState == 2)
				{
					this.frame1 = 30f;
				}
				this.isDown = true;
				this.fallState = 11;
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0011AFC4 File Offset: 0x001191C4
		public void handleList()
		{
			if (this.myState.Count < 1)
			{
				return;
			}
			this.previousState = this.displayState;
			this.currentSmoothing = 1f;
			float num = Vector3.DistanceSquared(this.myState[0].npcPosition, this.simulationState.npcPosition);
			this.smoothingDecay = this.myState[0].decay;
			this.simulationState.npcPosition = this.myState[0].npcPosition;
			this.simulationState.npcRotation = this.myState[0].npcRotation;
			this.simulationState.npcTilt = this.myState[0].npcTilt;
			this.incrOffset = this.ff[this.myState.Count];
			if (num > 1f)
			{
				this.smoothingDecay += this.incrOffset;
			}
			else
			{
				this.smoothingDecay += 0.02f;
			}
			this.currentSmoothing -= this.smoothingDecay;
			this.myState.RemoveAt(0);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0011B0EC File Offset: 0x001192EC
		public void UpdateRemote(ref float[,] heights)
		{
			this.currentSmoothing -= this.smoothingDecay;
			if (this.currentSmoothing <= 0f)
			{
				this.currentSmoothing = 0f;
				this.handleList();
			}
			this.oldPos = this.displayState.npcPosition;
			this.oldRot = this.displayState.npcRotation;
			this.displayState.npcPosition = Vector3.Lerp(this.simulationState.npcPosition, this.previousState.npcPosition, this.currentSmoothing);
			this.displayState.npcRotation = MathHelper.Lerp(this.simulationState.npcRotation, this.previousState.npcRotation, this.currentSmoothing);
			this.displayState.npcTilt = MathHelper.Lerp(this.simulationState.npcTilt, this.previousState.npcTilt, this.currentSmoothing);
			this.calcClips();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0011B1D8 File Offset: 0x001193D8
		private void calcClips()
		{
			this.dir1 = 0f;
			this.dir2 = 0f;
			this.mySign = 1f;
			this.mult = 1.3f;
			this.cross = 0.3f;
			this.rotOffset = 0f;
			this.dist = Vector2.Distance(new Vector2(this.displayState.npcPosition.X, this.displayState.npcPosition.Z), new Vector2(this.oldPos.X, this.oldPos.Z));
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
			this.incr = this.dist * this.mySign * this.mult;
			this.incr = MathHelper.Clamp(this.incr, -6f, 6f);
			this.distLimit = 1.3f;
			if (this.isDown)
			{
				this.distLimit = 0.8f;
			}
			if (this.displayState.npcRotation >= this.feetRot + this.distLimit)
			{
				this.feetRot = this.displayState.npcRotation - this.distLimit;
			}
			if (this.displayState.npcRotation <= this.feetRot - this.distLimit)
			{
				this.feetRot = this.displayState.npcRotation + this.distLimit;
			}
			this.moving = this.oldPos != this.displayState.npcPosition;
			this.notRotating = this.oldRot == this.displayState.npcRotation;
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
					if (this.clip1 != 1 && this.restTime2 > 5)
					{
						this.restTime2 = 0;
						this.clip2 = 1;
						this.swapClips();
					}
					if (this.isLiftingYou && this.clip1 != 10 && !this.pillTaken)
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
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0011B918 File Offset: 0x00119B18
		private void swapClips()
		{
			this.tween = 1f - this.tween;
			int num = this.clip2;
			this.clip2 = this.clip1;
			this.clip1 = num;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0011B954 File Offset: 0x00119B54
		private void fallManager()
		{
			this.now.liftHealth -= 0.02f;
			if (this.now.liftHealth < 0f)
			{
				this.now.liftHealth = 0f;
			}
			if (this.fallState == 0)
			{
				this.fallState = 1;
				this.frame1 = 0f;
				this.clip2 = 8;
				this.swapClips();
				this.incr = 0.8f;
				this.triggerEvent = 3;
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
				this.frame1 = 0f;
				this.clip2 = 7;
				this.swapClips();
				this.incr = 0.4f;
				if (this.weareUsingMilk)
				{
					this.incr = 0.7f;
				}
				return;
			}
			if (this.fallState == 4)
			{
				this.isDown = true;
				this.incr = 0.4f;
				if (this.weareUsingMilk)
				{
					this.incr = 0.7f;
				}
				if (this.frame1 > 85f || this.now.health > 105f)
				{
					if (this.now.health > 105f)
					{
						this.fallState = 5;
						this.restTime2 = 0;
						this.clip2 = 1;
						this.swapClips();
						return;
					}
					this.frame1 = 85f;
				}
				return;
			}
			if (this.fallState == 5)
			{
				this.isDown = true;
				this.frame1 = 94f;
				if (this.tween >= 0.9f)
				{
					this.isDown = false;
					this.fallState = 0;
					this.triggerEvent = 1;
				}
				return;
			}
			if (this.fallState == 11)
			{
				this.now.health = 0f;
				this.clip2 = 11;
				this.swapClips();
				this.fallState = 12;
				this.incr = 0.8f;
				this.triggerEvent = 4;
				return;
			}
			if (this.fallState == 12)
			{
				this.incr = 0.8f;
				this.now.health = 0f;
				if (this.frame1 > 128f)
				{
					this.frame1 = 128f;
					this.incr = 0f;
				}
			}
		}

		// Token: 0x04001340 RID: 4928
		private float distLimit = 1.3f;

		// Token: 0x04001341 RID: 4929
		public byte flag;

		// Token: 0x04001342 RID: 4930
		public int tempHealthVal;

		// Token: 0x04001343 RID: 4931
		public bool tempLiftVal;

		// Token: 0x04001344 RID: 4932
		public bool tempOnMilk;

		// Token: 0x04001345 RID: 4933
		public bool tempOnHulk;

		// Token: 0x04001346 RID: 4934
		public bool tempJumping;

		// Token: 0x04001347 RID: 4935
		public byte uvIndex;

		// Token: 0x04001348 RID: 4936
		public byte cuttyXcoord;

		// Token: 0x04001349 RID: 4937
		public byte cuttyYcoord;

		// Token: 0x0400134A RID: 4938
		public byte cuttyindexBit;

		// Token: 0x0400134B RID: 4939
		public ushort cuttyhealth;

		// Token: 0x0400134C RID: 4940
		public byte cuttyDamBit;

		// Token: 0x0400134D RID: 4941
		public ushort cuttyDamage;

		// Token: 0x0400134E RID: 4942
		public bool cuttyonFire;

		// Token: 0x0400134F RID: 4943
		public bool tempFire;

		// Token: 0x04001350 RID: 4944
		public bool sentgameNPC;

		// Token: 0x04001351 RID: 4945
		public bool pillTaken;

		// Token: 0x04001352 RID: 4946
		public byte bossindexBit;

		// Token: 0x04001353 RID: 4947
		public ushort bosshealth;

		// Token: 0x04001354 RID: 4948
		public byte bossDamBit;

		// Token: 0x04001355 RID: 4949
		public ushort bossDamage;

		// Token: 0x04001356 RID: 4950
		public float diff;

		// Token: 0x04001357 RID: 4951
		public bool isGone = true;

		// Token: 0x04001358 RID: 4952
		public bool stats_recieved;

		// Token: 0x04001359 RID: 4953
		public ushort stats_shotsfired;

		// Token: 0x0400135A RID: 4954
		public ushort stats_shotshit;

		// Token: 0x0400135B RID: 4955
		public ushort stats_headshots;

		// Token: 0x0400135C RID: 4956
		public ushort stats_asshits;

		// Token: 0x0400135D RID: 4957
		public ushort stats_bulkified;

		// Token: 0x0400135E RID: 4958
		public ushort stats_shottied;

		// Token: 0x0400135F RID: 4959
		public ushort stats_grenadier;

		// Token: 0x04001360 RID: 4960
		public ushort stats_melees;

		// Token: 0x04001361 RID: 4961
		public ushort stats_meleehits;

		// Token: 0x04001362 RID: 4962
		public ushort stats_spinebounces;

		// Token: 0x04001363 RID: 4963
		public ushort stats_oneshots;

		// Token: 0x04001364 RID: 4964
		public ushort stats_knockdown;

		// Token: 0x04001365 RID: 4965
		public float realDarkness;

		// Token: 0x04001366 RID: 4966
		public int realMoon;

		// Token: 0x04001367 RID: 4967
		public int newDayTime;

		// Token: 0x04001368 RID: 4968
		public int boarSpawn = -1;

		// Token: 0x04001369 RID: 4969
		public int boarCount;

		// Token: 0x0400136A RID: 4970
		public int boarHealth;

		// Token: 0x0400136B RID: 4971
		public int boarAttack;

		// Token: 0x0400136C RID: 4972
		public int boarMinSize;

		// Token: 0x0400136D RID: 4973
		public int boarMaxSize;

		// Token: 0x0400136E RID: 4974
		public int boarGiantOdds;

		// Token: 0x0400136F RID: 4975
		public int boarTinyOdds;

		// Token: 0x04001370 RID: 4976
		public int boarCharge;

		// Token: 0x04001371 RID: 4977
		public int boarTurnRate;

		// Token: 0x04001372 RID: 4978
		public int boarDist0;

		// Token: 0x04001373 RID: 4979
		public int boarDist1;

		// Token: 0x04001374 RID: 4980
		public int boarDist2;

		// Token: 0x04001375 RID: 4981
		public int boarDist3;

		// Token: 0x04001376 RID: 4982
		public int boarDist4;

		// Token: 0x04001377 RID: 4983
		public int boarDist5;

		// Token: 0x04001378 RID: 4984
		public int boarLimit0;

		// Token: 0x04001379 RID: 4985
		public int boarLimit1;

		// Token: 0x0400137A RID: 4986
		public int boarLimit2;

		// Token: 0x0400137B RID: 4987
		public int boarLimit3;

		// Token: 0x0400137C RID: 4988
		public int boarLimit4;

		// Token: 0x0400137D RID: 4989
		public int boarLimit5;

		// Token: 0x0400137E RID: 4990
		public int triggerEvent;

		// Token: 0x0400137F RID: 4991
		public bool bloodExists;

		// Token: 0x04001380 RID: 4992
		public int bloodCoil;

		// Token: 0x04001381 RID: 4993
		public int bloodIndex;

		// Token: 0x04001382 RID: 4994
		public float bloodPool = 16f;

		// Token: 0x04001383 RID: 4995
		public Matrix bloodPos;

		// Token: 0x04001384 RID: 4996
		public float bloodRot;

		// Token: 0x04001385 RID: 4997
		public bool isDown;

		// Token: 0x04001386 RID: 4998
		public int fallState;

		// Token: 0x04001387 RID: 4999
		public Matrix[] mySkin;

		// Token: 0x04001388 RID: 5000
		private bool moving;

		// Token: 0x04001389 RID: 5001
		private bool notRotating;

		// Token: 0x0400138A RID: 5002
		private bool headfeetAligned;

		// Token: 0x0400138B RID: 5003
		private bool alreadyMoving;

		// Token: 0x0400138C RID: 5004
		private float dist;

		// Token: 0x0400138D RID: 5005
		private float dir1;

		// Token: 0x0400138E RID: 5006
		private float dir2;

		// Token: 0x0400138F RID: 5007
		private float mySign = 1f;

		// Token: 0x04001390 RID: 5008
		private float mult = 1.3f;

		// Token: 0x04001391 RID: 5009
		private float cross = 0.3f;

		// Token: 0x04001392 RID: 5010
		private float rotOffset;

		// Token: 0x04001393 RID: 5011
		private Vector2 v1;

		// Token: 0x04001394 RID: 5012
		private Vector3 v2;

		// Token: 0x04001395 RID: 5013
		private Vector3 v3;

		// Token: 0x04001396 RID: 5014
		private float incry;

		// Token: 0x04001397 RID: 5015
		public int verifyTime;

		// Token: 0x04001398 RID: 5016
		public bool set_newTime;

		// Token: 0x04001399 RID: 5017
		public bool hasnoArms;

		// Token: 0x0400139A RID: 5018
		public int armTimer;

		// Token: 0x0400139B RID: 5019
		public int remoteTick;

		// Token: 0x0400139C RID: 5020
		public int last_remoteTick;

		// Token: 0x0400139D RID: 5021
		public int boarDropTimer = -1;

		// Token: 0x0400139E RID: 5022
		public int boarSeed;

		// Token: 0x0400139F RID: 5023
		public int boarHandicap;

		// Token: 0x040013A0 RID: 5024
		public Matrix cambone;

		// Token: 0x040013A1 RID: 5025
		public Matrix pistolHand;

		// Token: 0x040013A2 RID: 5026
		public Matrix headbone;

		// Token: 0x040013A3 RID: 5027
		public Vector2 recoilVec;

		// Token: 0x040013A4 RID: 5028
		public Vector3 gunpos;

		// Token: 0x040013A5 RID: 5029
		public Vector3 gunlook;

		// Token: 0x040013A6 RID: 5030
		public int gunChoice;

		// Token: 0x040013A7 RID: 5031
		public int guntimer;

		// Token: 0x040013A8 RID: 5032
		public int flashChoice;

		// Token: 0x040013A9 RID: 5033
		public int flashSide;

		// Token: 0x040013AA RID: 5034
		public int primaryChoice = 2;

		// Token: 0x040013AB RID: 5035
		public int secondaryChoice = 8;

		// Token: 0x040013AC RID: 5036
		public int lastWeapon = 2;

		// Token: 0x040013AD RID: 5037
		public float animCount = -1f;

		// Token: 0x040013AE RID: 5038
		public float animTween;

		// Token: 0x040013AF RID: 5039
		public List<int> animList = new List<int>();

		// Token: 0x040013B0 RID: 5040
		public int animClip = -1;

		// Token: 0x040013B1 RID: 5041
		public int animMax;

		// Token: 0x040013B2 RID: 5042
		public int animMin;

		// Token: 0x040013B3 RID: 5043
		public int animLoop;

		// Token: 0x040013B4 RID: 5044
		public float recoilTimer;

		// Token: 0x040013B5 RID: 5045
		public float flashTimer;

		// Token: 0x040013B6 RID: 5046
		public float blastTimer;

		// Token: 0x040013B7 RID: 5047
		public float blastRot;

		// Token: 0x040013B8 RID: 5048
		public bool flashfromSide;

		// Token: 0x040013B9 RID: 5049
		public float gunsideScale;

		// Token: 0x040013BA RID: 5050
		public float gunfrontScale;

		// Token: 0x040013BB RID: 5051
		public int hatindex;

		// Token: 0x040013BC RID: 5052
		private bool falling;

		// Token: 0x040013BD RID: 5053
		public bool jumping;

		// Token: 0x040013BE RID: 5054
		public byte difficulty;

		// Token: 0x040013BF RID: 5055
		public bool isLiftingYou;

		// Token: 0x040013C0 RID: 5056
		public bool onMilk;

		// Token: 0x040013C1 RID: 5057
		public bool onHulk;

		// Token: 0x040013C2 RID: 5058
		public int hulkTrans;

		// Token: 0x040013C3 RID: 5059
		public bool weareUsingMilk;

		// Token: 0x040013C4 RID: 5060
		public bool tempLever;

		// Token: 0x040013C5 RID: 5061
		public bool cheats;

		// Token: 0x040013C6 RID: 5062
		public remotePlayer.nowState now;

		// Token: 0x040013C7 RID: 5063
		public remotePlayer.conductor creature;

		// Token: 0x040013C8 RID: 5064
		public remotePlayer.conductor creaturemulti;

		// Token: 0x040013C9 RID: 5065
		public remotePlayer.conductor creatureShock;

		// Token: 0x040013CA RID: 5066
		public remotePlayer.conductor shatter;

		// Token: 0x040013CB RID: 5067
		public bool cheat_SendPackage;

		// Token: 0x040013CC RID: 5068
		public bool cheat_Invincible;

		// Token: 0x040013CD RID: 5069
		public bool cheat_FastFiring;

		// Token: 0x040013CE RID: 5070
		public bool cheat_InfiniteAmmo;

		// Token: 0x040013CF RID: 5071
		public bool cheat_AllExplode;

		// Token: 0x040013D0 RID: 5072
		public bool cheat_PickupPack;

		// Token: 0x040013D1 RID: 5073
		public bool cheat_UnlockAll;

		// Token: 0x040013D2 RID: 5074
		public int cutty_SendPackage = -1;

		// Token: 0x040013D3 RID: 5075
		public byte cutty_index;

		// Token: 0x040013D4 RID: 5076
		public byte cutty_homing;

		// Token: 0x040013D5 RID: 5077
		public float cutty_rot;

		// Token: 0x040013D6 RID: 5078
		public byte cutty_curveIndex;

		// Token: 0x040013D7 RID: 5079
		public float cutty_loop;

		// Token: 0x040013D8 RID: 5080
		public byte cutty_animtype;

		// Token: 0x040013D9 RID: 5081
		public byte cutty_talkindex;

		// Token: 0x040013DA RID: 5082
		public ushort cutty_dur;

		// Token: 0x040013DB RID: 5083
		public float cutty_destx;

		// Token: 0x040013DC RID: 5084
		public float cutty_destz;

		// Token: 0x040013DD RID: 5085
		public float cutty_targetRate;

		// Token: 0x040013DE RID: 5086
		public byte partTYPE;

		// Token: 0x040013DF RID: 5087
		public ushort partID;

		// Token: 0x040013E0 RID: 5088
		public byte partHIT;

		// Token: 0x040013E1 RID: 5089
		public int partTIME;

		// Token: 0x040013E2 RID: 5090
		public Vector3 partPOS;

		// Token: 0x040013E3 RID: 5091
		public Vector3 partVEL;

		// Token: 0x040013E4 RID: 5092
		public Quaternion partQUAT;

		// Token: 0x040013E5 RID: 5093
		public bool mirvToss;

		// Token: 0x040013E6 RID: 5094
		public Vector3 mirvPos = Vector3.Zero;

		// Token: 0x040013E7 RID: 5095
		public bool grenToss;

		// Token: 0x040013E8 RID: 5096
		public bool farmerTalk;

		// Token: 0x040013E9 RID: 5097
		public byte farmerIndex;

		// Token: 0x040013EA RID: 5098
		public ushort grenSeed;

		// Token: 0x040013EB RID: 5099
		public Vector3 grenPos;

		// Token: 0x040013EC RID: 5100
		public Vector3 grenVeloc;

		// Token: 0x040013ED RID: 5101
		public byte grenBounce;

		// Token: 0x040013EE RID: 5102
		public byte grenAge;

		// Token: 0x040013EF RID: 5103
		public int oldScore = -1;

		// Token: 0x040013F0 RID: 5104
		public int setClock;

		// Token: 0x040013F1 RID: 5105
		public byte netquality;

		// Token: 0x040013F2 RID: 5106
		private float incrOffset;

		// Token: 0x040013F3 RID: 5107
		private Vector3 oldPos;

		// Token: 0x040013F4 RID: 5108
		public float currentSmoothing;

		// Token: 0x040013F5 RID: 5109
		public float smoothingDecay;

		// Token: 0x040013F6 RID: 5110
		public remotePlayer.npcState simulationState;

		// Token: 0x040013F7 RID: 5111
		private remotePlayer.npcState previousState;

		// Token: 0x040013F8 RID: 5112
		public remotePlayer.npcState displayState;

		// Token: 0x040013F9 RID: 5113
		public List<remotePlayer.npcState> myState = new List<remotePlayer.npcState>();

		// Token: 0x040013FA RID: 5114
		public remotePlayer.npcState tempState;

		// Token: 0x040013FB RID: 5115
		public Vector3 lastPOS;

		// Token: 0x040013FC RID: 5116
		public Matrix transform;

		// Token: 0x040013FD RID: 5117
		public float frame1;

		// Token: 0x040013FE RID: 5118
		public float tween = 1f;

		// Token: 0x040013FF RID: 5119
		public float incr;

		// Token: 0x04001400 RID: 5120
		public int clip1;

		// Token: 0x04001401 RID: 5121
		public int clip2;

		// Token: 0x04001402 RID: 5122
		private int restTime;

		// Token: 0x04001403 RID: 5123
		public float feetRot;

		// Token: 0x04001404 RID: 5124
		public float headRot;

		// Token: 0x04001405 RID: 5125
		private int restTime2;

		// Token: 0x04001406 RID: 5126
		private float rotLerp;

		// Token: 0x04001407 RID: 5127
		private float lastFeetRot;

		// Token: 0x04001408 RID: 5128
		public float rotGoal;

		// Token: 0x04001409 RID: 5129
		private float[] ff = new float[] { -0.02f, -0.015f, -0.0035f, 0f, 0.001f, 0.001f, 0.001f, 0.001f, 0.002f, 0.003f };

		// Token: 0x0400140A RID: 5130
		private float oldRot;

		// Token: 0x0200008B RID: 139
		public class nowState
		{
			// Token: 0x0400140B RID: 5131
			public float health = 199f;

			// Token: 0x0400140C RID: 5132
			public byte whichLevel;

			// Token: 0x0400140D RID: 5133
			public byte pump1Level = 10;

			// Token: 0x0400140E RID: 5134
			public byte pump2Level = 10;

			// Token: 0x0400140F RID: 5135
			public bool leverOn;

			// Token: 0x04001410 RID: 5136
			public int leverRespond;

			// Token: 0x04001411 RID: 5137
			public bool rocketLoaded;

			// Token: 0x04001412 RID: 5138
			public byte bonusnpc;

			// Token: 0x04001413 RID: 5139
			public float liftHealth;

			// Token: 0x04001414 RID: 5140
			public byte load;

			// Token: 0x04001415 RID: 5141
			public byte loadDay = 1;

			// Token: 0x04001416 RID: 5142
			public ushort myscore;

			// Token: 0x04001417 RID: 5143
			public ushort remscore;

			// Token: 0x04001418 RID: 5144
			public bool flashlight;

			// Token: 0x04001419 RID: 5145
			public int animation;

			// Token: 0x0400141A RID: 5146
			public int weapon;

			// Token: 0x0400141B RID: 5147
			public int gunfired;

			// Token: 0x0400141C RID: 5148
			public byte doorOpen;

			// Token: 0x0400141D RID: 5149
			public Vector3 destiny;

			// Token: 0x0400141E RID: 5150
			public ushort accuracy;

			// Token: 0x0400141F RID: 5151
			public int grinder;

			// Token: 0x04001420 RID: 5152
			public int boarID = -1;

			// Token: 0x04001421 RID: 5153
			public int bloodpart = -1;
		}

		// Token: 0x0200008C RID: 140
		public struct conductor
		{
			// Token: 0x04001422 RID: 5154
			public byte type;

			// Token: 0x04001423 RID: 5155
			public ushort id;

			// Token: 0x04001424 RID: 5156
			public ushort id2;

			// Token: 0x04001425 RID: 5157
			public ushort id3;

			// Token: 0x04001426 RID: 5158
			public ushort id4;

			// Token: 0x04001427 RID: 5159
			public ushort id5;

			// Token: 0x04001428 RID: 5160
			public ushort id6;

			// Token: 0x04001429 RID: 5161
			public ushort id7;

			// Token: 0x0400142A RID: 5162
			public ushort id8;

			// Token: 0x0400142B RID: 5163
			public byte action;

			// Token: 0x0400142C RID: 5164
			public byte bodypart;

			// Token: 0x0400142D RID: 5165
			public ushort frame;

			// Token: 0x0400142E RID: 5166
			public int time;

			// Token: 0x0400142F RID: 5167
			public float rot;

			// Token: 0x04001430 RID: 5168
			public byte speed;

			// Token: 0x04001431 RID: 5169
			public bool died;

			// Token: 0x04001432 RID: 5170
			public bool died2;

			// Token: 0x04001433 RID: 5171
			public bool died3;

			// Token: 0x04001434 RID: 5172
			public bool died4;

			// Token: 0x04001435 RID: 5173
			public bool died5;

			// Token: 0x04001436 RID: 5174
			public Vector3 veloc;
		}

		// Token: 0x0200008D RID: 141
		public struct npcState
		{
			// Token: 0x04001437 RID: 5175
			public float decay;

			// Token: 0x04001438 RID: 5176
			public Vector3 npcPosition;

			// Token: 0x04001439 RID: 5177
			public float npcRotation;

			// Token: 0x0400143A RID: 5178
			public float npcTilt;
		}
	}
}
