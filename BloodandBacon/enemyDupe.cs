using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Blood
{
	// Token: 0x02000163 RID: 355
	public class enemyDupe
	{
		// Token: 0x06000CD0 RID: 3280 RVA: 0x00376A30 File Offset: 0x00374C30
		public enemyDupe(int group, int variant, Vector3 startpos, int myMove, int seed, int age, ScreenManager sc, int h)
		{
			this.audioemitter = new AudioEmitter();
			this.audiolistener = new AudioListener();
			this.audioemitter.Position = new Vector3(0f, 0f, 0f);
			this.audiolistener.Position = new Vector3(9000f, 0f, 9000f);
			this.health = (float)h;
			this.enemypos = startpos;
			this.skelPos = startpos;
			this.nozombies = false;
			enemyDupe.sc = sc;
			enemyDupe.homingCount = 0;
			enemyDupe.homingCount2 = 0;
			this.splatIndex = 0;
			this.scale = 1f;
			this.blood = 0;
			this.mypos = startpos;
			this.move = myMove;
			this.tween = 1f;
			this.age = age;
			this.seed = seed;
			this.boarGroup = group;
			this.variant = variant;
			int gameSpectate = sc.gameSpectate;
			this.random = new Random();
			this.defaultClip = this.random.Next(1, 3);
			this.myRot = (float)this.random.Next(-800, 800) / 100f;
			this.turn = (float)this.random.Next(-20, 20) / 1000f;
			this.timer = 300f;
			this.clip1 = this.defaultClip;
			this.clip2 = this.defaultClip;
			this.temp1 = this.random.Next(20, 300);
			this.temp2 = this.random.Next(20, 300);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00376CBC File Offset: 0x00374EBC
		public void UpdateNormal(ref float[,] heights)
		{
			this.age++;
			this.hoff = MathHelper.Clamp(this.scale * 50f, 10f, 62f);
			this.targetFloor = this.enemypos.Y + this.scale * 30f;
			this.timer -= 1f;
			this.temp1++;
			this.temp2++;
			this.frame1 = this.temp1 % enemyDupe.sics[this.clip1 * 2] + enemyDupe.sics[this.clip1 * 2 + 1];
			this.frame2 = this.temp2 % enemyDupe.sics[this.clip2 * 2] + enemyDupe.sics[this.clip2 * 2 + 1];
			this.tween += 0.125f;
			if (this.timer <= 0f)
			{
				int num = this.defaultClip;
				int num2 = 0;
				int num3 = this.random.Next(1, 100);
				if (num3 < 20)
				{
					num = this.random.Next(3, 5);
					num2 = this.random.Next(70, 120);
				}
				if (num3 >= 20)
				{
					num = this.random.Next(0, 3);
					num2 = 0;
				}
				this.defaultClip = 1;
				this.makeCalc(num, (float)num2);
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00376E2C File Offset: 0x0037502C
		public void UpdateDeath(ref float[,] heights)
		{
			this.age++;
			this.timer -= 1f;
			this.tween += 0.1f;
			if (this.timer > 0f)
			{
				this.enemypos += this.veloc;
				this.veloc.Y = this.veloc.Y + this.gravity;
				if (this.veloc.Y < -4f)
				{
					this.veloc.Y = -4f;
				}
				if (this.enemypos.Y + this.hoff < this.targetFloor)
				{
					if (this.floorHit < 2)
					{
						this.floorHit++;
						this.veloc.Y = -this.veloc.Y * 0.7f;
						this.enemypos.Y = this.targetFloor + 1f - this.hoff;
					}
					else
					{
						this.enemypos.Y = this.targetFloor - this.hoff;
					}
				}
			}
			if (this.timer <= 120f)
			{
				this.dying += 0.007f;
				if (this.dying > 0.99f)
				{
					this.dying = 0.99f;
				}
			}
			if (this.timer <= 0f)
			{
				if (this.timer < -10f)
				{
					this.enemypos.Y = this.enemypos.Y - 0.07f;
				}
				if (this.enemypos.Y < this.targetFloor - 80f)
				{
					this.move = 0;
					return;
				}
			}
			else
			{
				this.temp1++;
				this.temp2++;
				this.frame1 = this.temp1 % enemyDupe.sics[this.clip1 * 2] + enemyDupe.sics[this.clip1 * 2 + 1];
				this.frame2 = this.temp2 % enemyDupe.sics[this.clip2 * 2] + enemyDupe.sics[this.clip2 * 2 + 1];
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00377062 File Offset: 0x00375262
		public void makeCalc(int ch, float tt)
		{
			if (tt == 0f)
			{
				this.timer = (float)this.random.Next(210, 400);
			}
			else
			{
				this.timer = tt;
			}
			this.setClips(ch);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00377098 File Offset: 0x00375298
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

		// Token: 0x06000CD5 RID: 3285 RVA: 0x003770F8 File Offset: 0x003752F8
		private static void GetHeightFast(ref float[,] heights, Vector2 position, out float height)
		{
			int num = (int)(position.X / enemyDupe.unit);
			int num2 = (int)(position.Y / enemyDupe.unit);
			num = (int)MathHelper.Clamp(position.X / enemyDupe.unit, 0f, (float)(enemyDupe.bitmap - 2));
			num2 = (int)MathHelper.Clamp(position.Y / enemyDupe.unit, 0f, (float)(enemyDupe.bitmap - 2));
			float num3 = position.X % enemyDupe.unit / enemyDupe.unit;
			float num4 = position.Y % enemyDupe.unit / enemyDupe.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
		}

		// Token: 0x04003430 RID: 13360
		private float targetFloor;

		// Token: 0x04003431 RID: 13361
		public static int isChasing = -1;

		// Token: 0x04003432 RID: 13362
		public static int chaseCount = 0;

		// Token: 0x04003433 RID: 13363
		public Vector3 enemyColor;

		// Token: 0x04003434 RID: 13364
		public bool enemyisTurning;

		// Token: 0x04003435 RID: 13365
		public bool enemyBlocked;

		// Token: 0x04003436 RID: 13366
		public int blockedCount;

		// Token: 0x04003437 RID: 13367
		public int blockedTimer;

		// Token: 0x04003438 RID: 13368
		public float[] skelPath;

		// Token: 0x04003439 RID: 13369
		public Vector3[] skelPath3;

		// Token: 0x0400343A RID: 13370
		public SoundEffectInstance enemySound;

		// Token: 0x0400343B RID: 13371
		public AudioListener audiolistener;

		// Token: 0x0400343C RID: 13372
		public AudioEmitter audioemitter;

		// Token: 0x0400343D RID: 13373
		public Vector2 destination;

		// Token: 0x0400343E RID: 13374
		public Vector2 myhome;

		// Token: 0x0400343F RID: 13375
		public Vector3 skelPos1;

		// Token: 0x04003440 RID: 13376
		public Vector3 skelPos2;

		// Token: 0x04003441 RID: 13377
		public Vector3 skelPos;

		// Token: 0x04003442 RID: 13378
		public Vector3 enemypos;

		// Token: 0x04003443 RID: 13379
		public Matrix skelRot = Matrix.CreateRotationY(0f);

		// Token: 0x04003444 RID: 13380
		public int skelIndex;

		// Token: 0x04003445 RID: 13381
		public int enemyCounter;

		// Token: 0x04003446 RID: 13382
		public float skelInc;

		// Token: 0x04003447 RID: 13383
		public float skelStep;

		// Token: 0x04003448 RID: 13384
		public float skelOrigRate = 700f;

		// Token: 0x04003449 RID: 13385
		public float skelFastRate = 1500f;

		// Token: 0x0400344A RID: 13386
		public float skelRate = 700f;

		// Token: 0x0400344B RID: 13387
		public int attackAgain = 1;

		// Token: 0x0400344C RID: 13388
		public int attackAgainOrig = 1;

		// Token: 0x0400344D RID: 13389
		public float skelNewStep;

		// Token: 0x0400344E RID: 13390
		public int skelStepTimer;

		// Token: 0x0400344F RID: 13391
		private int ch;

		// Token: 0x04003450 RID: 13392
		public static int homingCount = 0;

		// Token: 0x04003451 RID: 13393
		public static int homingCount2 = 0;

		// Token: 0x04003452 RID: 13394
		public float hoff;

		// Token: 0x04003453 RID: 13395
		public Vector3 veloc;

		// Token: 0x04003454 RID: 13396
		public float gravity = -0.2f;

		// Token: 0x04003455 RID: 13397
		public int floorHit;

		// Token: 0x04003456 RID: 13398
		public static float acc = 3000f;

		// Token: 0x04003457 RID: 13399
		public static float handicapSpeed = 1f;

		// Token: 0x04003458 RID: 13400
		public static float handicapTurn = 1f;

		// Token: 0x04003459 RID: 13401
		public bool undead;

		// Token: 0x0400345A RID: 13402
		public bool zombie;

		// Token: 0x0400345B RID: 13403
		public bool borders = true;

		// Token: 0x0400345C RID: 13404
		public bool interrupt;

		// Token: 0x0400345D RID: 13405
		public bool oldturning;

		// Token: 0x0400345E RID: 13406
		public float oldturn;

		// Token: 0x0400345F RID: 13407
		public float oldtimer;

		// Token: 0x04003460 RID: 13408
		public float oldspeed;

		// Token: 0x04003461 RID: 13409
		public int oldclip;

		// Token: 0x04003462 RID: 13410
		public int oldtemp;

		// Token: 0x04003463 RID: 13411
		public int oldframe;

		// Token: 0x04003464 RID: 13412
		public bool death;

		// Token: 0x04003465 RID: 13413
		public float amp = 1f;

		// Token: 0x04003466 RID: 13414
		public float freq = 20f;

		// Token: 0x04003467 RID: 13415
		public float charge = 1f;

		// Token: 0x04003468 RID: 13416
		public float turnlimit = 0.07f;

		// Token: 0x04003469 RID: 13417
		public float turnHelper = 0.07f;

		// Token: 0x0400346A RID: 13418
		public int boarGroup = 1;

		// Token: 0x0400346B RID: 13419
		public int variant;

		// Token: 0x0400346C RID: 13420
		public int isStun;

		// Token: 0x0400346D RID: 13421
		public int resetStun;

		// Token: 0x0400346E RID: 13422
		public int isChomp;

		// Token: 0x0400346F RID: 13423
		public int resetChomp;

		// Token: 0x04003470 RID: 13424
		public int isSpooked;

		// Token: 0x04003471 RID: 13425
		public int isGrow = -1;

		// Token: 0x04003472 RID: 13426
		public bool growTrigger;

		// Token: 0x04003473 RID: 13427
		public int isGrowAge;

		// Token: 0x04003474 RID: 13428
		private int resetSpooked;

		// Token: 0x04003475 RID: 13429
		public int isBlind;

		// Token: 0x04003476 RID: 13430
		public int isHead = -1;

		// Token: 0x04003477 RID: 13431
		public bool nozombies;

		// Token: 0x04003478 RID: 13432
		public int isShocked;

		// Token: 0x04003479 RID: 13433
		public int isZap;

		// Token: 0x0400347A RID: 13434
		public int isCrumbled;

		// Token: 0x0400347B RID: 13435
		public int exploded;

		// Token: 0x0400347C RID: 13436
		public int shottie;

		// Token: 0x0400347D RID: 13437
		public int timeofDeath;

		// Token: 0x0400347E RID: 13438
		public static int bitmap;

		// Token: 0x0400347F RID: 13439
		public static float unit;

		// Token: 0x04003480 RID: 13440
		public static float Grid;

		// Token: 0x04003481 RID: 13441
		public bool homing;

		// Token: 0x04003482 RID: 13442
		public int splatIndex;

		// Token: 0x04003483 RID: 13443
		public int move;

		// Token: 0x04003484 RID: 13444
		public float myRot;

		// Token: 0x04003485 RID: 13445
		public float myRotHeld;

		// Token: 0x04003486 RID: 13446
		public float angleX;

		// Token: 0x04003487 RID: 13447
		public float angleZ;

		// Token: 0x04003488 RID: 13448
		public float scale;

		// Token: 0x04003489 RID: 13449
		public float bluScale;

		// Token: 0x0400348A RID: 13450
		public Matrix myRot2;

		// Token: 0x0400348B RID: 13451
		public Vector3 mypos;

		// Token: 0x0400348C RID: 13452
		public bool isturning;

		// Token: 0x0400348D RID: 13453
		public int frame1 = 1;

		// Token: 0x0400348E RID: 13454
		public int frame2 = 1;

		// Token: 0x0400348F RID: 13455
		public int temp1;

		// Token: 0x04003490 RID: 13456
		public int temp2;

		// Token: 0x04003491 RID: 13457
		public int clip1;

		// Token: 0x04003492 RID: 13458
		public int clip2;

		// Token: 0x04003493 RID: 13459
		public float tween;

		// Token: 0x04003494 RID: 13460
		public int stunLength = 150;

		// Token: 0x04003495 RID: 13461
		public float speed;

		// Token: 0x04003496 RID: 13462
		public float turn;

		// Token: 0x04003497 RID: 13463
		public float timer;

		// Token: 0x04003498 RID: 13464
		public float wait;

		// Token: 0x04003499 RID: 13465
		public float startHealth = 100f;

		// Token: 0x0400349A RID: 13466
		public float health = 100f;

		// Token: 0x0400349B RID: 13467
		public Matrix transform;

		// Token: 0x0400349C RID: 13468
		public int blood;

		// Token: 0x0400349D RID: 13469
		public static List<int> sics;

		// Token: 0x0400349E RID: 13470
		public int seed;

		// Token: 0x0400349F RID: 13471
		private float groundHeight;

		// Token: 0x040034A0 RID: 13472
		public int age;

		// Token: 0x040034A1 RID: 13473
		public int localhitCount;

		// Token: 0x040034A2 RID: 13474
		public int remotehitCount;

		// Token: 0x040034A3 RID: 13475
		public float tint;

		// Token: 0x040034A4 RID: 13476
		public float oldtint;

		// Token: 0x040034A5 RID: 13477
		public float dying;

		// Token: 0x040034A6 RID: 13478
		public Random random;

		// Token: 0x040034A7 RID: 13479
		public Random randomDeath;

		// Token: 0x040034A8 RID: 13480
		public Random randomHead;

		// Token: 0x040034A9 RID: 13481
		public Random randomPartsFly;

		// Token: 0x040034AA RID: 13482
		public int immunity;

		// Token: 0x040034AB RID: 13483
		public int assexplode;

		// Token: 0x040034AC RID: 13484
		public static ScreenManager sc;

		// Token: 0x040034AD RID: 13485
		public float nightSpeed = 1f;

		// Token: 0x040034AE RID: 13486
		public float scaleSpeed = 1f;

		// Token: 0x040034AF RID: 13487
		public int defaultClip = 1;
	}
}
