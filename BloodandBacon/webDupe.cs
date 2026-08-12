using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000A8 RID: 168
	public class webDupe
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x001438C0 File Offset: 0x00141AC0
		public webDupe(int group, int variant, Vector3 startpos, int myMove, int seed, int age, ScreenManager sc, string timeofday, int formation, bool noZboars)
		{
			this.enemypos = startpos;
			this.skelPos = startpos;
			this.nozombies = noZboars;
			webDupe.sc = sc;
			webDupe.homingCount = 0;
			webDupe.homingCount2 = 0;
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
			this.defaultClip = 1;
			this.nightSpeed = 1f;
			this.random = new Random();
			this.myRot = (float)this.random.Next(-800, 800) / 100f;
			this.speed = this.scale * (webDupe.acc / 1000f) * this.nightSpeed;
			this.turn = (float)this.random.Next(-20, 20) / 1000f;
			this.timer = 300f;
			this.clip1 = this.defaultClip;
			this.clip2 = this.defaultClip;
			this.temp1 = this.random.Next(20, 300);
			this.temp2 = this.random.Next(20, 300);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00143AE8 File Offset: 0x00141CE8
		public void UpdateWeb(ref float[,] heights)
		{
			this.age++;
			this.wait -= 1f;
			this.clipchange += 1f;
			if (this.wait <= 0f)
			{
				this.timer -= 1f;
				this.temp1++;
				this.temp2++;
				this.frame1 = this.temp1 % webDupe.sics[this.clip1 * 2] + webDupe.sics[this.clip1 * 2 + 1];
				this.frame2 = this.temp2 % webDupe.sics[this.clip2 * 2] + webDupe.sics[this.clip2 * 2 + 1];
				this.tween += 0.1f;
				if (this.timer <= 0f)
				{
					if (this.clipchange > 1000f)
					{
						this.defaultClip = this.random.Next(0, 4);
						this.clipchange = 0f;
					}
					this.makeCalc(this.defaultClip);
					this.wait = (float)this.random.Next(100, 450);
				}
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00143C3C File Offset: 0x00141E3C
		public void makeCalc(int ch)
		{
			this.timer = (float)this.random.Next(240, 680);
			this.speed = 0f;
			this.turn = 0f;
			this.setClips(ch);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00143C78 File Offset: 0x00141E78
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

		// Token: 0x06000604 RID: 1540 RVA: 0x00143CD8 File Offset: 0x00141ED8
		private static void GetHeightFast(ref float[,] heights, Vector2 position, out float height)
		{
			int num = (int)(position.X / webDupe.unit);
			int num2 = (int)(position.Y / webDupe.unit);
			num = (int)MathHelper.Clamp(position.X / webDupe.unit, 0f, (float)(webDupe.bitmap - 2));
			num2 = (int)MathHelper.Clamp(position.Y / webDupe.unit, 0f, (float)(webDupe.bitmap - 2));
			float num3 = position.X % webDupe.unit / webDupe.unit;
			float num4 = position.Y % webDupe.unit / webDupe.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
		}

		// Token: 0x04001766 RID: 5990
		public static int isChasing = -1;

		// Token: 0x04001767 RID: 5991
		public static int chaseCount = 0;

		// Token: 0x04001768 RID: 5992
		public Vector3 enemyColor;

		// Token: 0x04001769 RID: 5993
		public bool enemyisTurning;

		// Token: 0x0400176A RID: 5994
		public bool enemyBlocked;

		// Token: 0x0400176B RID: 5995
		public float[] skelPath;

		// Token: 0x0400176C RID: 5996
		public Vector2 destination;

		// Token: 0x0400176D RID: 5997
		public Vector3 skelPos1;

		// Token: 0x0400176E RID: 5998
		public Vector3 skelPos2;

		// Token: 0x0400176F RID: 5999
		public Vector3 skelPos;

		// Token: 0x04001770 RID: 6000
		public Vector3 enemypos;

		// Token: 0x04001771 RID: 6001
		public Matrix skelRot = Matrix.CreateRotationY(0f);

		// Token: 0x04001772 RID: 6002
		public int skelIndex;

		// Token: 0x04001773 RID: 6003
		public int enemyCounter;

		// Token: 0x04001774 RID: 6004
		public float skelInc;

		// Token: 0x04001775 RID: 6005
		public float skelStep;

		// Token: 0x04001776 RID: 6006
		public int pauseCount;

		// Token: 0x04001777 RID: 6007
		public float skelOrigRate = 700f;

		// Token: 0x04001778 RID: 6008
		public float skelFastRate = 1500f;

		// Token: 0x04001779 RID: 6009
		public float skelNewStep;

		// Token: 0x0400177A RID: 6010
		public int skelStepTimer;

		// Token: 0x0400177B RID: 6011
		private int ch;

		// Token: 0x0400177C RID: 6012
		public static int homingCount = 0;

		// Token: 0x0400177D RID: 6013
		public static int homingCount2 = 0;

		// Token: 0x0400177E RID: 6014
		public static int currentDay = 1;

		// Token: 0x0400177F RID: 6015
		public static float acc = 3000f;

		// Token: 0x04001780 RID: 6016
		public static float handicapSpeed = 1f;

		// Token: 0x04001781 RID: 6017
		public static float handicapTurn = 1f;

		// Token: 0x04001782 RID: 6018
		public bool undead;

		// Token: 0x04001783 RID: 6019
		public bool zombie;

		// Token: 0x04001784 RID: 6020
		public bool borders = true;

		// Token: 0x04001785 RID: 6021
		public bool interrupt;

		// Token: 0x04001786 RID: 6022
		public bool oldturning;

		// Token: 0x04001787 RID: 6023
		public float oldturn;

		// Token: 0x04001788 RID: 6024
		public float oldtimer;

		// Token: 0x04001789 RID: 6025
		public float oldspeed;

		// Token: 0x0400178A RID: 6026
		public int oldclip;

		// Token: 0x0400178B RID: 6027
		public int oldtemp;

		// Token: 0x0400178C RID: 6028
		public int oldframe;

		// Token: 0x0400178D RID: 6029
		public bool death;

		// Token: 0x0400178E RID: 6030
		public float amp = 1f;

		// Token: 0x0400178F RID: 6031
		public float freq = 20f;

		// Token: 0x04001790 RID: 6032
		public float charge = 1f;

		// Token: 0x04001791 RID: 6033
		public float turnlimit = 0.07f;

		// Token: 0x04001792 RID: 6034
		public float turnHelper = 0.07f;

		// Token: 0x04001793 RID: 6035
		public int boarGroup = 1;

		// Token: 0x04001794 RID: 6036
		public int variant;

		// Token: 0x04001795 RID: 6037
		public int isStun;

		// Token: 0x04001796 RID: 6038
		public int resetStun;

		// Token: 0x04001797 RID: 6039
		public int isChomp;

		// Token: 0x04001798 RID: 6040
		public int resetChomp;

		// Token: 0x04001799 RID: 6041
		public int isSpooked;

		// Token: 0x0400179A RID: 6042
		public int isGrow = -1;

		// Token: 0x0400179B RID: 6043
		public bool growTrigger;

		// Token: 0x0400179C RID: 6044
		public int isGrowAge;

		// Token: 0x0400179D RID: 6045
		private int resetSpooked;

		// Token: 0x0400179E RID: 6046
		public int isBlind;

		// Token: 0x0400179F RID: 6047
		public int isHead = -1;

		// Token: 0x040017A0 RID: 6048
		public bool nozombies;

		// Token: 0x040017A1 RID: 6049
		public int isShocked;

		// Token: 0x040017A2 RID: 6050
		public int isZap;

		// Token: 0x040017A3 RID: 6051
		public int isCrumbled;

		// Token: 0x040017A4 RID: 6052
		public int exploded;

		// Token: 0x040017A5 RID: 6053
		public int shottie;

		// Token: 0x040017A6 RID: 6054
		public int timeofDeath;

		// Token: 0x040017A7 RID: 6055
		public static int bitmap;

		// Token: 0x040017A8 RID: 6056
		public static float unit;

		// Token: 0x040017A9 RID: 6057
		public static float Grid;

		// Token: 0x040017AA RID: 6058
		public int homing;

		// Token: 0x040017AB RID: 6059
		public int splatIndex;

		// Token: 0x040017AC RID: 6060
		public int move;

		// Token: 0x040017AD RID: 6061
		public float myRot;

		// Token: 0x040017AE RID: 6062
		public float myRotHeld;

		// Token: 0x040017AF RID: 6063
		public float angleX;

		// Token: 0x040017B0 RID: 6064
		public float angleZ;

		// Token: 0x040017B1 RID: 6065
		public float scale;

		// Token: 0x040017B2 RID: 6066
		public float bluScale;

		// Token: 0x040017B3 RID: 6067
		public Matrix myRot2;

		// Token: 0x040017B4 RID: 6068
		public Vector3 mypos;

		// Token: 0x040017B5 RID: 6069
		public bool isturning;

		// Token: 0x040017B6 RID: 6070
		public int frame1 = 1;

		// Token: 0x040017B7 RID: 6071
		public int frame2 = 1;

		// Token: 0x040017B8 RID: 6072
		public int temp1;

		// Token: 0x040017B9 RID: 6073
		public int temp2;

		// Token: 0x040017BA RID: 6074
		public int clip1;

		// Token: 0x040017BB RID: 6075
		public int clip2;

		// Token: 0x040017BC RID: 6076
		public float tween;

		// Token: 0x040017BD RID: 6077
		public int stunLength = 150;

		// Token: 0x040017BE RID: 6078
		public float speed;

		// Token: 0x040017BF RID: 6079
		public float turn;

		// Token: 0x040017C0 RID: 6080
		public float timer;

		// Token: 0x040017C1 RID: 6081
		public float wait;

		// Token: 0x040017C2 RID: 6082
		public float clipchange;

		// Token: 0x040017C3 RID: 6083
		public float startHealth = 5f;

		// Token: 0x040017C4 RID: 6084
		public float health = 5f;

		// Token: 0x040017C5 RID: 6085
		public Matrix transform;

		// Token: 0x040017C6 RID: 6086
		public int blood;

		// Token: 0x040017C7 RID: 6087
		public static List<int> sics;

		// Token: 0x040017C8 RID: 6088
		public int seed;

		// Token: 0x040017C9 RID: 6089
		private float groundHeight;

		// Token: 0x040017CA RID: 6090
		public int age;

		// Token: 0x040017CB RID: 6091
		public float tint;

		// Token: 0x040017CC RID: 6092
		public float oldtint;

		// Token: 0x040017CD RID: 6093
		public Random random;

		// Token: 0x040017CE RID: 6094
		public int immunity;

		// Token: 0x040017CF RID: 6095
		public int assexplode;

		// Token: 0x040017D0 RID: 6096
		public static ScreenManager sc;

		// Token: 0x040017D1 RID: 6097
		public float nightSpeed = 1f;

		// Token: 0x040017D2 RID: 6098
		public float scaleSpeed = 1f;

		// Token: 0x040017D3 RID: 6099
		public int defaultClip = 1;
	}
}
