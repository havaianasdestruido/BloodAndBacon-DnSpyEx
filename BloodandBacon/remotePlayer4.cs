using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;

namespace Blood
{
	// Token: 0x020000EA RID: 234
	public class remotePlayer4 : GameScreen
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x001D83F8 File Offset: 0x001D65F8
		public remotePlayer4(ContentManager content, Vector3 vv, ScreenManager scr)
		{
			this.sc = scr;
			this.target3 = new RenderTarget2D(this.sc.GraphicsDevice, 600, 600, true, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			this.target4 = new RenderTarget2D(this.sc.GraphicsDevice, 600, 600, true, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			this.skinTransforms = new Matrix[remotePlayer4.bonecount];
			this.bloodrem = new dropSystemR(this.sc.Game, content);
			this.bloodrem.Initialize();
			this.bloodrem.LoadContent(this.sc.GraphicsDevice);
			this.simulationState.npcPosition = vv;
			this.simulationState.npcRotation = -1.57f;
			this.feetRot = -1.57f;
			this.lastFeetRot = -1.57f;
			this.simulationState.npcTilt = 0f;
			this.player2Paint.index = new List<int>();
			this.player2Paint.x = new List<float>();
			this.player2Paint.z = new List<float>();
			this.displayState = this.simulationState;
			this.previousState = this.displayState;
			this.currentSmoothing = 1f;
			this.smoothingDecay = 0.1f;
			this.now = new remotePlayer4.nowState();
			this.mySkin = new Matrix[29];
			this.myState.Capacity = 20;
			this.creature.id = 65000;
			this.partID = 0;
			this.frame1 = 100000f;
			this.animList.Capacity = 20;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x001D86F8 File Offset: 0x001D68F8
		public void resetVars()
		{
			this.now.pump1Level = 12;
			this.now.pump2Level = 12;
			this.now.leverOn = false;
			this.now.myscore = 0;
			this.now.remscore = 0;
			this.now.health = 190f;
			this.bloodExists = false;
			this.radius2 = 0f;
			this.gunfiredRadius2 = 0f;
			this.gunfiredDest2 = 0f;
			this.gunfiredChase2 = 0;
			this.homingCount = 0;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x001D878C File Offset: 0x001D698C
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
				this.isLiftingYou = -1;
				if (this.fallState == 2)
				{
					this.frame1 = 30f;
				}
				this.isDown = true;
				this.fallState = 11;
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x001D88CC File Offset: 0x001D6ACC
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

		// Token: 0x06000807 RID: 2055 RVA: 0x001D89F4 File Offset: 0x001D6BF4
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

		// Token: 0x06000808 RID: 2056 RVA: 0x001D8AE0 File Offset: 0x001D6CE0
		private void calcClips()
		{
			this.dir1 = 0f;
			this.dir2 = 0f;
			this.mySign = 1f;
			this.mult = 1.3f;
			this.cross = 0.3f;
			this.rotOffset = 0f;
			this.dist = Vector2.Distance(new Vector2(this.displayState.npcPosition.X, this.displayState.npcPosition.Z), new Vector2(this.oldPos.X, this.oldPos.Z));
			this.vec = Vector3.Zero;
			if (this.dist > 0f)
			{
				this.v1 = Vector2.Normalize(new Vector2(this.displayState.npcPosition.X, this.displayState.npcPosition.Z) - new Vector2(this.oldPos.X, this.oldPos.Z));
				this.v2 = Vector3.Transform(new Vector3(0f, 0f, 1f), Matrix.CreateRotationY(this.displayState.npcRotation));
				this.v3 = Vector3.Transform(new Vector3(1f, 0f, 0f), Matrix.CreateRotationY(this.displayState.npcRotation));
				this.dir1 = Vector2.Dot(this.v1, new Vector2(this.v2.X, this.v2.Z));
				this.dir2 = Vector2.Dot(this.v1, new Vector2(this.v3.X, this.v3.Z));
				this.vec = new Vector3(this.v1.X, 0f, this.v1.Y) * 1.2f;
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
					if (this.isLiftingYou >= 0 && this.clip1 != 10 && !remotePlayer4.pillTaken)
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

		// Token: 0x06000809 RID: 2057 RVA: 0x001D925C File Offset: 0x001D745C
		private void swapClips()
		{
			this.tween = 1f - this.tween;
			int num = this.clip2;
			this.clip2 = this.clip1;
			this.clip1 = num;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x001D9298 File Offset: 0x001D7498
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
				if (remotePlayer4.weareUsingMilk)
				{
					this.incr = 0.7f;
				}
				return;
			}
			if (this.fallState == 4)
			{
				this.isDown = true;
				this.incr = 0.4f;
				if (remotePlayer4.weareUsingMilk)
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

		// Token: 0x040020FD RID: 8445
		public Texture2D ttWorld2;

		// Token: 0x040020FE RID: 8446
		public Vector3 lightPos2;

		// Token: 0x040020FF RID: 8447
		public Matrix view2World2 = Matrix.Identity;

		// Token: 0x04002100 RID: 8448
		public float flicker2;

		// Token: 0x04002101 RID: 8449
		public bool remotelight;

		// Token: 0x04002102 RID: 8450
		public int oddnumrem;

		// Token: 0x04002103 RID: 8451
		public RenderTarget2D target3;

		// Token: 0x04002104 RID: 8452
		public RenderTarget2D target4;

		// Token: 0x04002105 RID: 8453
		public Model remoteModel;

		// Token: 0x04002106 RID: 8454
		public Model remoteModelwhole;

		// Token: 0x04002107 RID: 8455
		public Model remoteModelnoArms;

		// Token: 0x04002108 RID: 8456
		public Texture2D player2Texture;

		// Token: 0x04002109 RID: 8457
		public Texture2D player2TextureOrig;

		// Token: 0x0400210A RID: 8458
		public Texture2D player2TextureGreen;

		// Token: 0x0400210B RID: 8459
		public Texture2D player2TextureReload;

		// Token: 0x0400210C RID: 8460
		public Texture2D remNPCdead;

		// Token: 0x0400210D RID: 8461
		public ParticleSystem bloodrem;

		// Token: 0x0400210E RID: 8462
		public CSteamID steamID = default(CSteamID);

		// Token: 0x0400210F RID: 8463
		public Color myColor = Color.White;

		// Token: 0x04002110 RID: 8464
		public string remgamertag = "No Name";

		// Token: 0x04002111 RID: 8465
		public bool isHost;

		// Token: 0x04002112 RID: 8466
		public int remoteID = 6;

		// Token: 0x04002113 RID: 8467
		public float vol;

		// Token: 0x04002114 RID: 8468
		public float remoteClean;

		// Token: 0x04002115 RID: 8469
		public bool remoteCleanOn;

		// Token: 0x04002116 RID: 8470
		public int homingRemote = -1;

		// Token: 0x04002117 RID: 8471
		public int homingCount;

		// Token: 0x04002118 RID: 8472
		public SoundEffect primaryCock_rem;

		// Token: 0x04002119 RID: 8473
		public SoundEffect secondaryCock_rem;

		// Token: 0x0400211A RID: 8474
		public SoundEffect primBang_rem;

		// Token: 0x0400211B RID: 8475
		public SoundEffect primMuffle_rem;

		// Token: 0x0400211C RID: 8476
		public SoundEffect secBang_rem;

		// Token: 0x0400211D RID: 8477
		public SoundEffect secMuffle_rem;

		// Token: 0x0400211E RID: 8478
		public Matrix[] skinTransforms;

		// Token: 0x0400211F RID: 8479
		public static int bonecount;

		// Token: 0x04002120 RID: 8480
		public Rectangle rect_alivebodyRemote = new Rectangle(689, 549, 255, 89);

		// Token: 0x04002121 RID: 8481
		public Rectangle rect_alivebodyCheat = new Rectangle(689, 549, 255, 89);

		// Token: 0x04002122 RID: 8482
		public int remBloodColor;

		// Token: 0x04002123 RID: 8483
		public float player2player;

		// Token: 0x04002124 RID: 8484
		public float playerDotplayer;

		// Token: 0x04002125 RID: 8485
		public remotePlayer4.paintBody player2Paint;

		// Token: 0x04002126 RID: 8486
		public static int leverRespond = 0;

		// Token: 0x04002127 RID: 8487
		public float radius2;

		// Token: 0x04002128 RID: 8488
		public float gunfiredRadius2;

		// Token: 0x04002129 RID: 8489
		public float gunfiredDest2;

		// Token: 0x0400212A RID: 8490
		public int gunfiredChase2;

		// Token: 0x0400212B RID: 8491
		public static bool isGone = false;

		// Token: 0x0400212C RID: 8492
		private float distLimit = 1.3f;

		// Token: 0x0400212D RID: 8493
		public byte flag;

		// Token: 0x0400212E RID: 8494
		public int tempHealthVal;

		// Token: 0x0400212F RID: 8495
		public int tempLiftVal = -1;

		// Token: 0x04002130 RID: 8496
		public bool tempOnMilk;

		// Token: 0x04002131 RID: 8497
		public bool cheats;

		// Token: 0x04002132 RID: 8498
		public bool tempOnHulk;

		// Token: 0x04002133 RID: 8499
		public bool tempJumping;

		// Token: 0x04002134 RID: 8500
		public byte uvIndex;

		// Token: 0x04002135 RID: 8501
		public byte cuttyXcoord;

		// Token: 0x04002136 RID: 8502
		public byte cuttyYcoord;

		// Token: 0x04002137 RID: 8503
		public byte cuttyindexBit;

		// Token: 0x04002138 RID: 8504
		public ushort cuttyhealth;

		// Token: 0x04002139 RID: 8505
		public byte cuttyDamBit;

		// Token: 0x0400213A RID: 8506
		public ushort cuttyDamage;

		// Token: 0x0400213B RID: 8507
		public bool cuttyonFire;

		// Token: 0x0400213C RID: 8508
		public bool tempFire;

		// Token: 0x0400213D RID: 8509
		public static bool pillTaken = false;

		// Token: 0x0400213E RID: 8510
		public bool LiftingUs;

		// Token: 0x0400213F RID: 8511
		public byte bossindexBit;

		// Token: 0x04002140 RID: 8512
		public ushort bosshealth;

		// Token: 0x04002141 RID: 8513
		public byte bossDamBit;

		// Token: 0x04002142 RID: 8514
		public ushort bossDamage;

		// Token: 0x04002143 RID: 8515
		public float diff;

		// Token: 0x04002144 RID: 8516
		public bool stats_recieved;

		// Token: 0x04002145 RID: 8517
		public ushort stats_shotsfired;

		// Token: 0x04002146 RID: 8518
		public ushort stats_shotshit;

		// Token: 0x04002147 RID: 8519
		public ushort stats_headshots;

		// Token: 0x04002148 RID: 8520
		public ushort stats_asshits;

		// Token: 0x04002149 RID: 8521
		public ushort stats_bulkified;

		// Token: 0x0400214A RID: 8522
		public ushort stats_shottied;

		// Token: 0x0400214B RID: 8523
		public ushort stats_grenadier;

		// Token: 0x0400214C RID: 8524
		public ushort stats_melees;

		// Token: 0x0400214D RID: 8525
		public ushort stats_meleehits;

		// Token: 0x0400214E RID: 8526
		public ushort stats_spinebounces;

		// Token: 0x0400214F RID: 8527
		public ushort stats_oneshots;

		// Token: 0x04002150 RID: 8528
		public ushort stats_knockdown;

		// Token: 0x04002151 RID: 8529
		public float realDarkness;

		// Token: 0x04002152 RID: 8530
		public int realMoon;

		// Token: 0x04002153 RID: 8531
		public int newDayTime;

		// Token: 0x04002154 RID: 8532
		public int boarSpawn = -1;

		// Token: 0x04002155 RID: 8533
		public int boarCount;

		// Token: 0x04002156 RID: 8534
		public int boarHealth;

		// Token: 0x04002157 RID: 8535
		public int boarAttack;

		// Token: 0x04002158 RID: 8536
		public int boarMinSize;

		// Token: 0x04002159 RID: 8537
		public int boarMaxSize;

		// Token: 0x0400215A RID: 8538
		public int boarGiantOdds;

		// Token: 0x0400215B RID: 8539
		public int boarTinyOdds;

		// Token: 0x0400215C RID: 8540
		public int boarCharge;

		// Token: 0x0400215D RID: 8541
		public int boarTurnRate;

		// Token: 0x0400215E RID: 8542
		public int boarDist0;

		// Token: 0x0400215F RID: 8543
		public int boarDist1;

		// Token: 0x04002160 RID: 8544
		public int boarDist2;

		// Token: 0x04002161 RID: 8545
		public int boarDist3;

		// Token: 0x04002162 RID: 8546
		public int boarDist4;

		// Token: 0x04002163 RID: 8547
		public int boarDist5;

		// Token: 0x04002164 RID: 8548
		public int boarLimit0;

		// Token: 0x04002165 RID: 8549
		public int boarLimit1;

		// Token: 0x04002166 RID: 8550
		public int boarLimit2;

		// Token: 0x04002167 RID: 8551
		public int boarLimit3;

		// Token: 0x04002168 RID: 8552
		public int boarLimit4;

		// Token: 0x04002169 RID: 8553
		public int boarLimit5;

		// Token: 0x0400216A RID: 8554
		public Vector3 m2_location = Vector3.Zero;

		// Token: 0x0400216B RID: 8555
		public float m2_rot;

		// Token: 0x0400216C RID: 8556
		public bool insideMinimap;

		// Token: 0x0400216D RID: 8557
		public float m2_xx;

		// Token: 0x0400216E RID: 8558
		public float m2_yy;

		// Token: 0x0400216F RID: 8559
		public bool bobble;

		// Token: 0x04002170 RID: 8560
		public int triggerEvent;

		// Token: 0x04002171 RID: 8561
		public bool bloodExists;

		// Token: 0x04002172 RID: 8562
		public int bloodCoil;

		// Token: 0x04002173 RID: 8563
		public int bloodIndex;

		// Token: 0x04002174 RID: 8564
		public float bloodPool = 16f;

		// Token: 0x04002175 RID: 8565
		public Matrix bloodPos;

		// Token: 0x04002176 RID: 8566
		public float bloodRot;

		// Token: 0x04002177 RID: 8567
		public bool isDown;

		// Token: 0x04002178 RID: 8568
		public int fallState;

		// Token: 0x04002179 RID: 8569
		public Matrix[] mySkin;

		// Token: 0x0400217A RID: 8570
		private bool moving;

		// Token: 0x0400217B RID: 8571
		private bool notRotating;

		// Token: 0x0400217C RID: 8572
		private bool headfeetAligned;

		// Token: 0x0400217D RID: 8573
		private bool alreadyMoving;

		// Token: 0x0400217E RID: 8574
		private float dist;

		// Token: 0x0400217F RID: 8575
		public Vector3 vec = Vector3.Zero;

		// Token: 0x04002180 RID: 8576
		private float dir1;

		// Token: 0x04002181 RID: 8577
		private float dir2;

		// Token: 0x04002182 RID: 8578
		private float mySign = 1f;

		// Token: 0x04002183 RID: 8579
		private float mult = 1.3f;

		// Token: 0x04002184 RID: 8580
		private float cross = 0.3f;

		// Token: 0x04002185 RID: 8581
		private float rotOffset;

		// Token: 0x04002186 RID: 8582
		private Vector2 v1;

		// Token: 0x04002187 RID: 8583
		private Vector3 v2;

		// Token: 0x04002188 RID: 8584
		private Vector3 v3;

		// Token: 0x04002189 RID: 8585
		private float incry;

		// Token: 0x0400218A RID: 8586
		public int verifyTime;

		// Token: 0x0400218B RID: 8587
		public bool set_newTime;

		// Token: 0x0400218C RID: 8588
		public bool hasnoArms;

		// Token: 0x0400218D RID: 8589
		public int armTimer;

		// Token: 0x0400218E RID: 8590
		public int remoteTick;

		// Token: 0x0400218F RID: 8591
		public int last_remoteTick;

		// Token: 0x04002190 RID: 8592
		public int boarDropTimer = -1;

		// Token: 0x04002191 RID: 8593
		public int boarSeed;

		// Token: 0x04002192 RID: 8594
		public int boarHandicap;

		// Token: 0x04002193 RID: 8595
		public Matrix cambone;

		// Token: 0x04002194 RID: 8596
		public Matrix pistolHand;

		// Token: 0x04002195 RID: 8597
		public Matrix headbone;

		// Token: 0x04002196 RID: 8598
		public Vector2 recoilVec;

		// Token: 0x04002197 RID: 8599
		public Vector3 gunpos;

		// Token: 0x04002198 RID: 8600
		public Vector3 gunlook;

		// Token: 0x04002199 RID: 8601
		public int spotlight;

		// Token: 0x0400219A RID: 8602
		public int gunChoice;

		// Token: 0x0400219B RID: 8603
		public int guntimer;

		// Token: 0x0400219C RID: 8604
		public int flashChoice;

		// Token: 0x0400219D RID: 8605
		public int flashSide;

		// Token: 0x0400219E RID: 8606
		public int primaryChoice = 2;

		// Token: 0x0400219F RID: 8607
		public int secondaryChoice = 8;

		// Token: 0x040021A0 RID: 8608
		public int lastWeapon = 2;

		// Token: 0x040021A1 RID: 8609
		public float animCount = -1f;

		// Token: 0x040021A2 RID: 8610
		public float animTween;

		// Token: 0x040021A3 RID: 8611
		public List<int> animList = new List<int>();

		// Token: 0x040021A4 RID: 8612
		public int animClip = -1;

		// Token: 0x040021A5 RID: 8613
		public int animMax;

		// Token: 0x040021A6 RID: 8614
		public int animMin;

		// Token: 0x040021A7 RID: 8615
		public int animLoop;

		// Token: 0x040021A8 RID: 8616
		public float recoilTimer;

		// Token: 0x040021A9 RID: 8617
		public float flashTimer;

		// Token: 0x040021AA RID: 8618
		public float blastTimer;

		// Token: 0x040021AB RID: 8619
		public float blastRot;

		// Token: 0x040021AC RID: 8620
		public bool flashfromSide;

		// Token: 0x040021AD RID: 8621
		public float gunsideScale;

		// Token: 0x040021AE RID: 8622
		public float gunfrontScale;

		// Token: 0x040021AF RID: 8623
		private bool falling;

		// Token: 0x040021B0 RID: 8624
		public bool jumping;

		// Token: 0x040021B1 RID: 8625
		public byte difficulty;

		// Token: 0x040021B2 RID: 8626
		public int isLiftingYou = -1;

		// Token: 0x040021B3 RID: 8627
		public bool gunfired;

		// Token: 0x040021B4 RID: 8628
		public bool onMilk;

		// Token: 0x040021B5 RID: 8629
		public bool onHulk;

		// Token: 0x040021B6 RID: 8630
		public bool UsingHulk;

		// Token: 0x040021B7 RID: 8631
		public int hatindex;

		// Token: 0x040021B8 RID: 8632
		public int hulkTrans;

		// Token: 0x040021B9 RID: 8633
		public static bool weareUsingMilk = false;

		// Token: 0x040021BA RID: 8634
		public bool tempLever;

		// Token: 0x040021BB RID: 8635
		public remotePlayer4.nowState now;

		// Token: 0x040021BC RID: 8636
		public remotePlayer4.conductor creature;

		// Token: 0x040021BD RID: 8637
		public remotePlayer4.conductor creaturemulti;

		// Token: 0x040021BE RID: 8638
		public remotePlayer4.conductor creatureShock;

		// Token: 0x040021BF RID: 8639
		public remotePlayer4.conductor shatter;

		// Token: 0x040021C0 RID: 8640
		public bool cheat_SendPackage;

		// Token: 0x040021C1 RID: 8641
		public bool cheat_Invincible;

		// Token: 0x040021C2 RID: 8642
		public bool cheat_FastFiring;

		// Token: 0x040021C3 RID: 8643
		public bool cheat_InfiniteAmmo;

		// Token: 0x040021C4 RID: 8644
		public bool cheat_AllExplode;

		// Token: 0x040021C5 RID: 8645
		public bool cheat_PickupPack;

		// Token: 0x040021C6 RID: 8646
		public bool cheat_UnlockAll;

		// Token: 0x040021C7 RID: 8647
		public int cutty_SendPackage = -1;

		// Token: 0x040021C8 RID: 8648
		public byte cutty_index;

		// Token: 0x040021C9 RID: 8649
		public byte cutty_homing;

		// Token: 0x040021CA RID: 8650
		public float cutty_rot;

		// Token: 0x040021CB RID: 8651
		public byte cutty_curveIndex;

		// Token: 0x040021CC RID: 8652
		public float cutty_loop;

		// Token: 0x040021CD RID: 8653
		public byte cutty_animtype;

		// Token: 0x040021CE RID: 8654
		public byte cutty_talkindex;

		// Token: 0x040021CF RID: 8655
		public ushort cutty_dur;

		// Token: 0x040021D0 RID: 8656
		public float cutty_destx;

		// Token: 0x040021D1 RID: 8657
		public float cutty_destz;

		// Token: 0x040021D2 RID: 8658
		public float cutty_targetRate;

		// Token: 0x040021D3 RID: 8659
		public byte pumpkinID = byte.MaxValue;

		// Token: 0x040021D4 RID: 8660
		public byte partTYPE;

		// Token: 0x040021D5 RID: 8661
		public ushort partID;

		// Token: 0x040021D6 RID: 8662
		public byte partHIT;

		// Token: 0x040021D7 RID: 8663
		public int partTIME;

		// Token: 0x040021D8 RID: 8664
		public Vector3 partPOS;

		// Token: 0x040021D9 RID: 8665
		public Vector3 partVEL;

		// Token: 0x040021DA RID: 8666
		public Quaternion partQUAT;

		// Token: 0x040021DB RID: 8667
		public bool mirvToss;

		// Token: 0x040021DC RID: 8668
		public Vector3 mirvPos = Vector3.Zero;

		// Token: 0x040021DD RID: 8669
		public bool grenToss;

		// Token: 0x040021DE RID: 8670
		public bool farmerTalk;

		// Token: 0x040021DF RID: 8671
		public byte farmerIndex;

		// Token: 0x040021E0 RID: 8672
		public ushort grenSeed;

		// Token: 0x040021E1 RID: 8673
		public Vector3 grenPos;

		// Token: 0x040021E2 RID: 8674
		public Vector3 grenVeloc;

		// Token: 0x040021E3 RID: 8675
		public byte grenBounce;

		// Token: 0x040021E4 RID: 8676
		public byte grenAge;

		// Token: 0x040021E5 RID: 8677
		public int oldScore = -1;

		// Token: 0x040021E6 RID: 8678
		public int setClock;

		// Token: 0x040021E7 RID: 8679
		public byte netquality;

		// Token: 0x040021E8 RID: 8680
		private float incrOffset;

		// Token: 0x040021E9 RID: 8681
		private Vector3 oldPos;

		// Token: 0x040021EA RID: 8682
		public float currentSmoothing;

		// Token: 0x040021EB RID: 8683
		public float smoothingDecay;

		// Token: 0x040021EC RID: 8684
		public remotePlayer4.npcState simulationState;

		// Token: 0x040021ED RID: 8685
		private remotePlayer4.npcState previousState;

		// Token: 0x040021EE RID: 8686
		public remotePlayer4.npcState displayState;

		// Token: 0x040021EF RID: 8687
		public List<remotePlayer4.npcState> myState = new List<remotePlayer4.npcState>();

		// Token: 0x040021F0 RID: 8688
		public remotePlayer4.npcState tempState;

		// Token: 0x040021F1 RID: 8689
		public Vector3 lastPOS;

		// Token: 0x040021F2 RID: 8690
		public Matrix transform;

		// Token: 0x040021F3 RID: 8691
		public float frame1;

		// Token: 0x040021F4 RID: 8692
		public float tween = 1f;

		// Token: 0x040021F5 RID: 8693
		public float incr;

		// Token: 0x040021F6 RID: 8694
		public int clip1;

		// Token: 0x040021F7 RID: 8695
		public int clip2;

		// Token: 0x040021F8 RID: 8696
		private int restTime;

		// Token: 0x040021F9 RID: 8697
		public float feetRot;

		// Token: 0x040021FA RID: 8698
		public float headRot;

		// Token: 0x040021FB RID: 8699
		private int restTime2;

		// Token: 0x040021FC RID: 8700
		private float rotLerp;

		// Token: 0x040021FD RID: 8701
		public float lastFeetRot;

		// Token: 0x040021FE RID: 8702
		public float rotGoal;

		// Token: 0x040021FF RID: 8703
		private float[] ff = new float[] { -0.02f, -0.015f, -0.0035f, 0f, 0.001f, 0.001f, 0.001f, 0.001f, 0.002f, 0.003f };

		// Token: 0x04002200 RID: 8704
		public float oldRot;

		// Token: 0x04002201 RID: 8705
		private ScreenManager sc;

		// Token: 0x020000EB RID: 235
		public struct paintBody
		{
			// Token: 0x04002202 RID: 8706
			public List<int> index;

			// Token: 0x04002203 RID: 8707
			public List<float> x;

			// Token: 0x04002204 RID: 8708
			public List<float> z;
		}

		// Token: 0x020000EC RID: 236
		public class nowState
		{
			// Token: 0x04002205 RID: 8709
			public float health = 199f;

			// Token: 0x04002206 RID: 8710
			public byte whichLevel;

			// Token: 0x04002207 RID: 8711
			public byte pump1Level = 10;

			// Token: 0x04002208 RID: 8712
			public byte pump2Level = 10;

			// Token: 0x04002209 RID: 8713
			public bool leverOn;

			// Token: 0x0400220A RID: 8714
			public int leverRespond;

			// Token: 0x0400220B RID: 8715
			public bool rocketLoaded;

			// Token: 0x0400220C RID: 8716
			public byte bonusnpc;

			// Token: 0x0400220D RID: 8717
			public float liftHealth;

			// Token: 0x0400220E RID: 8718
			public byte load;

			// Token: 0x0400220F RID: 8719
			public byte loadDay = 1;

			// Token: 0x04002210 RID: 8720
			public ushort myscore;

			// Token: 0x04002211 RID: 8721
			public ushort remscore;

			// Token: 0x04002212 RID: 8722
			public bool flashlight;

			// Token: 0x04002213 RID: 8723
			public int animation;

			// Token: 0x04002214 RID: 8724
			public int weapon;

			// Token: 0x04002215 RID: 8725
			public int gunfired;

			// Token: 0x04002216 RID: 8726
			public byte doorOpen;

			// Token: 0x04002217 RID: 8727
			public Vector3 destiny;

			// Token: 0x04002218 RID: 8728
			public ushort accuracy;

			// Token: 0x04002219 RID: 8729
			public int grinder;

			// Token: 0x0400221A RID: 8730
			public int boarID = -1;

			// Token: 0x0400221B RID: 8731
			public int bloodpart = -1;
		}

		// Token: 0x020000ED RID: 237
		public struct conductor
		{
			// Token: 0x0400221C RID: 8732
			public byte type;

			// Token: 0x0400221D RID: 8733
			public ushort id;

			// Token: 0x0400221E RID: 8734
			public ushort id2;

			// Token: 0x0400221F RID: 8735
			public ushort id3;

			// Token: 0x04002220 RID: 8736
			public ushort id4;

			// Token: 0x04002221 RID: 8737
			public ushort id5;

			// Token: 0x04002222 RID: 8738
			public ushort id6;

			// Token: 0x04002223 RID: 8739
			public ushort id7;

			// Token: 0x04002224 RID: 8740
			public ushort id8;

			// Token: 0x04002225 RID: 8741
			public byte action;

			// Token: 0x04002226 RID: 8742
			public byte bodypart;

			// Token: 0x04002227 RID: 8743
			public ushort frame;

			// Token: 0x04002228 RID: 8744
			public int time;

			// Token: 0x04002229 RID: 8745
			public float rot;

			// Token: 0x0400222A RID: 8746
			public byte speed;

			// Token: 0x0400222B RID: 8747
			public bool died;

			// Token: 0x0400222C RID: 8748
			public bool died2;

			// Token: 0x0400222D RID: 8749
			public bool died3;

			// Token: 0x0400222E RID: 8750
			public bool died4;

			// Token: 0x0400222F RID: 8751
			public bool died5;

			// Token: 0x04002230 RID: 8752
			public Vector3 veloc;
		}

		// Token: 0x020000EE RID: 238
		public struct npcState
		{
			// Token: 0x04002231 RID: 8753
			public float decay;

			// Token: 0x04002232 RID: 8754
			public Vector3 npcPosition;

			// Token: 0x04002233 RID: 8755
			public float npcRotation;

			// Token: 0x04002234 RID: 8756
			public float npcTilt;
		}
	}
}
