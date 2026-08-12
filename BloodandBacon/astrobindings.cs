using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x02000082 RID: 130
	internal class astrobindings : GameScreen
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000470 RID: 1136 RVA: 0x000FECC0 File Offset: 0x000FCEC0
		// (remove) Token: 0x06000471 RID: 1137 RVA: 0x000FECF8 File Offset: 0x000FCEF8
		public event EventHandler<PlayerIndexEventArgs> Accepted;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000472 RID: 1138 RVA: 0x000FED30 File Offset: 0x000FCF30
		// (remove) Token: 0x06000473 RID: 1139 RVA: 0x000FED68 File Offset: 0x000FCF68
		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000474 RID: 1140 RVA: 0x000FEDA0 File Offset: 0x000FCFA0
		// (remove) Token: 0x06000475 RID: 1141 RVA: 0x000FEDD8 File Offset: 0x000FCFD8
		public event EventHandler<PlayerIndexEventArgs> CheatSent;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000476 RID: 1142 RVA: 0x000FEE10 File Offset: 0x000FD010
		// (remove) Token: 0x06000477 RID: 1143 RVA: 0x000FEE48 File Offset: 0x000FD048
		public event EventHandler<PlayerIndexEventArgs> LevelSent;

		// Token: 0x06000478 RID: 1144 RVA: 0x000FEEAC File Offset: 0x000FD0AC
		public astrobindings(string ch)
		{
			this.choice = ch;
			base.IsPopup = true;
			base.TransitionOnTime = TimeSpan.FromSeconds(0.0);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.0);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000FF7B4 File Offset: 0x000FD9B4
		public override void LoadContent()
		{
			ContentManager content = base.ScreenManager.Game.Content;
			this.sc = base.ScreenManager;
			this.spriteBatch = new SpriteBatch(base.ScreenManager.GraphicsDevice);
			this.rr = new Random();
			this.rot = 0f;
			this.drift = (float)this.rr.Next(250, 1900) / 100f;
			if (this.rr.Next(1, 100) < 50)
			{
				this.drift = -this.drift;
			}
			this.delayinput = true;
			this.scribbleFont = this.sc.scribblefont2;
			this.grnPen = new Color(30, 50, 255, 255);
			this.bluePen = new Color(30, 30, 140, 255);
			this.blackPen = new Color(0, 0, 0, 255);
			int num = this.rr.Next(0, 4);
			if (num == 0)
			{
				this.myRect = this.paper1Rect;
			}
			if (num == 1)
			{
				this.myRect = this.paper2Rect;
			}
			if (num == 2)
			{
				this.myRect = this.paper3Rect;
			}
			if (num == 3)
			{
				this.myRect = this.paper4Rect;
			}
			if (this.choice == "instructions")
			{
				this.jot = this.sc.jot1;
			}
			else
			{
				this.jot = this.sc.jot2;
			}
			this.drip = this.sc.drip;
			this.cashout = this.sc.cashout;
			this.scribble = this.sc.scribble;
			this.offset = new Vector2(-70f, 0f);
			if (this.choice == "video")
			{
				this.offset = new Vector2(-90f, 0f);
				this.myRect = this.paper5Rect;
				if (this.sc.usingMouse)
				{
					this.myRect = this.paper6Rect;
				}
			}
			if (this.choice == "options")
			{
				this.offset = new Vector2(-50f, 0f);
			}
			if (this.choice == "audio")
			{
				this.offset = new Vector2(-50f, 0f);
			}
			if (this.choice == "kicks")
			{
				this.offset = new Vector2(-50f, 0f);
			}
			this.scribble.Play(this.sc.ev * 0.8f, (float)this.rr.Next(-30, 30) / 100f, 0f);
			this.vecIndex = 9;
			this.vec = new Vector2[80];
			for (int i = 0; i < 80; i++)
			{
				this.vec[i] = new Vector2(0f, 0f);
			}
			if (this.choice == "restart")
			{
				this.updownIndex = this.sc.currentDay;
				this.vec[0] = new Vector2(4.6f, -58.4f);
				this.vec[1] = new Vector2(4.8f, -8.2f);
				this.vec[2] = new Vector2(-22.2f, 56.4f);
				this.vec[3] = new Vector2(52.2f, 163f);
				this.vec[4] = new Vector2(82.2f, 55.2f);
				this.vec[5] = new Vector2(157f, 163f);
				this.vec[6] = new Vector2(0f, 0f);
				this.vec[7] = new Vector2(0f, 0f);
				this.vec[8] = new Vector2(0f, 0f);
				this.vec[9] = new Vector2(0f, 0f);
			}
			if (this.choice == "kicks")
			{
				this.updownIndex = 0;
				this.vec[0] = new Vector2(10f, -85f);
				this.vec[1] = new Vector2(10f, -40f);
				this.vec[2] = new Vector2(10f, 5f);
				this.vec[3] = new Vector2(10f, 50f);
				this.vec[4] = new Vector2(10f, 95f);
				this.vec[5] = new Vector2(30f, 10f);
				this.vec[6] = new Vector2(30f, 55f);
				this.vec[7] = new Vector2(30f, 100f);
				this.vec[8] = new Vector2(30f, 145f);
				this.vec[9] = new Vector2(30f, 190f);
				this.vec[10] = new Vector2(0f, 0f);
				this.vec[11] = new Vector2(0f, 0f);
				this.vec[12] = new Vector2(0f, 0f);
				this.vec[13] = new Vector2(0f, 0f);
			}
			if (this.choice == "options")
			{
				this.updownIndex = 0;
				this.vec[0] = new Vector2(-25f, -52f);
				this.vec[1] = new Vector2(34f, 6.6f);
				this.vec[2] = new Vector2(-19.19f, 66.2f);
				this.vec[3] = new Vector2(35.19f, 41.99f);
				this.vec[4] = new Vector2(58f, 100.4f);
				this.vec[5] = new Vector2(38.99f, 162.19f);
				this.vec[6] = new Vector2(0f, 0f);
				this.vec[7] = new Vector2(0f, 0f);
				this.vec[8] = new Vector2(0f, 0f);
				this.vec[9] = new Vector2(0f, 0f);
			}
			if (this.choice == "audio")
			{
				this.updownIndex = 0;
				this.vec[0] = new Vector2(-36.8f, -89.2f);
				this.vec[1] = new Vector2(-1.8f, -21.8f);
				this.vec[2] = new Vector2(-44.6f, 35.4f);
				this.vec[3] = new Vector2(34f, 5.2f);
				this.vec[4] = new Vector2(60.2f, 71.4f);
				this.vec[5] = new Vector2(27.2f, 129.6f);
				this.vec[6] = new Vector2(63.2f, -87.8f);
				this.vec[7] = new Vector2(91.6f, -23f);
				this.vec[8] = new Vector2(55f, 35.8f);
				this.vec[9] = new Vector2(35.2f, 110.4f);
				this.vec[10] = new Vector2(94.6f, 216.2f);
				this.slider1 = (int)Math.Round(Math.Sqrt((double)(this.sc.mv * 100f)));
				this.slider2 = (int)Math.Round(Math.Sqrt((double)(this.sc.ev * 100f)));
				this.slider3 = (int)Math.Round(Math.Sqrt((double)(this.sc.vv * 100f)));
				this.slider1 = (int)MathHelper.Clamp((float)this.slider1, 0f, 10f);
				this.slider2 = (int)MathHelper.Clamp((float)this.slider2, 0f, 10f);
				this.slider3 = (int)MathHelper.Clamp((float)this.slider3, 0f, 10f);
				this.sliderbox1 = (float)this.slider1 * 16.8f - 84f;
				this.sliderbox2 = (float)this.slider2 * 16.8f - 84f;
				this.sliderbox3 = (float)this.slider3 * 16.8f - 84f;
			}
			if (this.choice == "video")
			{
				this.vec[0] = new Vector2(42.8f, -46.8f);
				this.vec[1] = new Vector2(137.4f, 98f);
				this.vec[2] = new Vector2(23f, -1f);
				this.vec[3] = new Vector2(90.2f, 144.8f);
				this.vec[4] = new Vector2(44.4f, 41.6f);
				this.vec[5] = new Vector2(136f, 189f);
				this.sliderbox1 = (float)this.sc.brightness * 0.6588f - 84f;
				this.sliderbox2 = (float)this.sc.contrast * 0.6588f - 84f;
				this.rampIN = 1f;
			}
			if (this.choice == "game")
			{
				this.vec[0] = new Vector2(-21.3f, -97.8f);
				this.vec[1] = new Vector2(208f, -2.5f);
				this.vec[2] = new Vector2(216f, -10.4f);
				this.vec[3] = new Vector2(-29.4f, -42.4f);
				this.vec[4] = new Vector2(83.4f, -43.99f);
				this.vec[5] = new Vector2(-5.4f, 14.8f);
				this.vec[6] = new Vector2(215.4f, 111.5f);
				this.vec[7] = new Vector2(226.2f, 103.2f);
				this.vec[8] = new Vector2(-47.8f, 73.9f);
				this.vec[9] = new Vector2(74.8f, 74.4f);
				this.vec[10] = new Vector2(32f, 122.6f);
				this.vec[11] = new Vector2(99.4f, 230.2f);
				this.vec[12] = new Vector2(36f, -4f);
				this.vec[13] = new Vector2(14f, 54f);
				this.vec[14] = new Vector2(46f, 112f);
				this.vec[15] = new Vector2(20f, 172f);
				if (this.sc.pad_invertY == 1f)
				{
					this.slider1 = 0;
				}
				if (this.sc.pad_invertY == -1f)
				{
					this.slider1 = 1;
				}
				this.slider2 = 4;
				if (this.sc.pad_sensitivity == 3.5f)
				{
					this.slider2 = 1;
				}
				if (this.sc.pad_sensitivity == 2.5f)
				{
					this.slider2 = 2;
				}
				if (this.sc.pad_sensitivity == 2f)
				{
					this.slider2 = 3;
				}
				if (this.sc.pad_sensitivity == 1.5f)
				{
					this.slider2 = 4;
				}
				if (this.sc.pad_sensitivity == 1f)
				{
					this.slider2 = 5;
				}
				if (this.sc.pad_sensitivity == 0.76f)
				{
					this.slider2 = 6;
				}
				if (this.sc.pad_sensitivity == 0.5f)
				{
					this.slider2 = 7;
				}
				if (this.sc.pad_sensitivity == 0.25f)
				{
					this.slider2 = 8;
				}
				if (!this.sc.pad_vibro)
				{
					this.slider3 = 0;
				}
				if (this.sc.pad_vibro)
				{
					this.slider3 = 1;
				}
				if (!this.sc.pad_togglesprint)
				{
					this.slider5 = 0;
				}
				if (this.sc.pad_togglesprint)
				{
					this.slider5 = 1;
				}
				this.slider4 = this.sc.df;
			}
			if (this.choice == "help")
			{
				this.vec[0] = new Vector2(37f, 52f);
				this.vec[1] = new Vector2(38f, 100f);
				this.vec[3] = new Vector2(40f, 135f);
				this.vec[4] = new Vector2(8.2f, -40f);
				this.vec[5] = new Vector2(8f, 10f);
				this.vec[6] = new Vector2(3.6f, 40f);
				this.scaler = 0f;
			}
			if (this.choice == "controller" || this.choice == "controller2")
			{
				this.drift = 100f;
				this.scaler = 0f;
			}
			if (this.choice == "instructions")
			{
				this.drift = 100f;
				this.scaler = 0f;
			}
			if (this.choice == "cheats")
			{
				this.updownIndex = 0;
				float num2 = -12f;
				this.vec[0] = new Vector2(-26.8f, -69.2f + num2);
				this.vec[1] = new Vector2(-21.8f, -16.8f + num2);
				this.vec[2] = new Vector2(-29.599998f, 41f + num2);
				this.vec[30] = new Vector2(-25f, 90f + num2);
				this.vec[3] = new Vector2(60f, 30f + num2);
				this.vec[4] = new Vector2(100f, 81.5f + num2);
				this.vec[5] = new Vector2(57f, 139.5f + num2);
				this.vec[31] = new Vector2(60f, 189f + num2);
				this.vec[6] = new Vector2(33.2f, -68f + num2);
				this.vec[7] = new Vector2(71.6f, -18f + num2);
				this.vec[8] = new Vector2(40f, 41f + num2);
				this.vec[32] = new Vector2(48f, 91f + num2);
				this.vec[9] = new Vector2(25.2f, 85.4f);
				this.vec[10] = new Vector2(94.6f, 191.2f);
				this.slider1 = 0;
				this.slider2 = 0;
				this.slider3 = 0;
				this.slider4 = 0;
				bool flag = true;
				for (int j = 0; j < 19; j += 2)
				{
				}
				if (this.sc.cheat_InfiniteAmmo)
				{
					this.slider1 = 1;
				}
				if (this.sc.cheat_Invincible)
				{
					this.slider2 = 1;
				}
				if (this.sc.cheat_skipday)
				{
					this.slider4 = 1;
				}
				if (flag)
				{
					this.slider3 = 1;
				}
			}
			if (this.choice == "hidden")
			{
				this.vec[0] = new Vector2(35f, 6.2f);
				this.vec[1] = new Vector2(54.6f, 65.6f);
				this.vec[2] = new Vector2(32.4f, 125.6f);
				this.vec[3] = new Vector2(47f, 185.4f);
				this.vec[4] = new Vector2(-0.8f, -87.6f);
				this.vec[5] = new Vector2(36f, -27.2f);
				this.vec[6] = new Vector2(10f, 31.6f);
				this.vec[7] = new Vector2(14.2f, 93f);
			}
			if (this.choice == "networking")
			{
				if (this.sc.debug_showNetwork)
				{
					this.slider1 = 1;
				}
				this.slider2 = (int)this.sc.networkQuality;
				this.vec[0] = new Vector2(25f, 14f);
				this.vec[1] = new Vector2(27f, 89f);
				this.vec[2] = new Vector2(41f, 153f);
				this.vec[3] = new Vector2(-27f, -81f);
				this.vec[4] = new Vector2(199f, 15f);
				this.vec[5] = new Vector2(212f, 4f);
				this.vec[6] = new Vector2(-26f, -6f);
				this.vec[7] = new Vector2(83f, -8f);
				this.vec[8] = new Vector2(-23f, 58f);
				this.vec[9] = new Vector2(89f, 57f);
				this.vec[10] = new Vector2(34f, 121f);
				this.vec[11] = new Vector2(96f, 228f);
			}
			if (this.choice == "debugging")
			{
				if (this.sc.debug_showGarbage)
				{
					this.slider1 = 1;
				}
				if (this.sc.debug_showHoming)
				{
					this.slider2 = 1;
				}
				if (this.sc.debug_showAccuracy)
				{
					this.slider3 = 1;
				}
				if (this.sc.debug_showSeed)
				{
					this.slider4 = 2;
				}
				this.vec[0] = new Vector2(29f, -4f);
				this.vec[1] = new Vector2(21f, 54f);
				this.vec[2] = new Vector2(35f, 108f);
				this.vec[3] = new Vector2(22f, 163f);
				this.vec[4] = new Vector2(0f, -96f);
				this.vec[5] = new Vector2(206f, -2f);
				this.vec[6] = new Vector2(216f, -10f);
				this.vec[7] = new Vector2(-28f, -38f);
				this.vec[8] = new Vector2(198f, 56f);
				this.vec[9] = new Vector2(211f, 47f);
				this.vec[10] = new Vector2(-7f, 12f);
				this.vec[11] = new Vector2(210f, 110f);
				this.vec[12] = new Vector2(221f, 101f);
				this.vec[13] = new Vector2(-18f, 67f);
				this.vec[14] = new Vector2(197f, 165f);
				this.vec[15] = new Vector2(208f, 156f);
				this.vec[16] = new Vector2(41f, 118f);
				this.vec[17] = new Vector2(104f, 226f);
			}
			if (this.choice == "cheat mode")
			{
				if (this.sc.cheat_Invincible)
				{
					this.slider1 = 1;
				}
				if (this.sc.cheat_FastFiring)
				{
					this.slider2 = 1;
				}
				if (this.sc.cheat_InfiniteAmmo)
				{
					this.slider3 = 1;
				}
				if (this.sc.cheat_AllExplode)
				{
					this.slider4 = 1;
				}
				if (this.sc.cheat_PickupPack)
				{
					this.slider5 = 1;
				}
				this.vec[0] = new Vector2(30f, -22f);
				this.vec[1] = new Vector2(55f, 40f);
				this.vec[2] = new Vector2(32f, 101f);
				this.vec[3] = new Vector2(58f, 154f);
				this.vec[4] = new Vector2(23f, 215f);
				this.vec[5] = new Vector2(-18f, -116f);
				this.vec[6] = new Vector2(209f, -21f);
				this.vec[7] = new Vector2(220f, -30f);
				this.vec[8] = new Vector2(10f, -55f);
				this.vec[9] = new Vector2(234f, 42f);
				this.vec[10] = new Vector2(244f, 33f);
				this.vec[11] = new Vector2(-6f, 3f);
				this.vec[12] = new Vector2(214f, 102f);
				this.vec[13] = new Vector2(225f, 91f);
				this.vec[14] = new Vector2(7f, 58f);
				this.vec[15] = new Vector2(235f, 156f);
				this.vec[16] = new Vector2(244f, 145f);
				this.vec[17] = new Vector2(-20f, 120f);
				this.vec[18] = new Vector2(200f, 215f);
				this.vec[19] = new Vector2(210f, 203f);
				this.vec[20] = new Vector2(48f, 172f);
				this.vec[21] = new Vector2(114f, 281f);
			}
			if (this.choice == "level edit")
			{
				this.updownIndex = 0;
				if (this.sc.dayTime == "am")
				{
					this.slider1 = 0;
				}
				else
				{
					this.slider1 = 1;
				}
				this.slider2 = this.sc.boar1Spawn;
				this.slider3 = this.sc.boarCount;
				this.slider4 = this.sc.boar1Health;
				this.slider5 = this.sc.boar1Attack;
				this.slider6 = this.sc.boar1MinSize;
				this.slider7 = this.sc.boar1MaxSize;
				this.slider8 = this.sc.boar1GiantOdds;
				this.slider9 = this.sc.boar1TinyOdds;
				this.slider10 = this.sc.boar1Charge;
				this.slider11 = this.sc.boar1TurnRate;
				this.slider12 = this.sc.boarDistance[0];
				this.slider14 = this.sc.boarDistance[1];
				this.slider16 = this.sc.boarDistance[2];
				this.slider13 = this.sc.boarHomingLimit[0];
				this.slider15 = this.sc.boarHomingLimit[1];
				this.slider17 = this.sc.boarHomingLimit[2];
				float num3 = -60f;
				this.vec[0] = new Vector2(-32f + num3, -90f);
				this.vec[1] = new Vector2(-32f + num3, -56f);
				this.vec[2] = new Vector2(-36f + num3, -18f);
				this.vec[3] = new Vector2(-30f + num3, 20f);
				this.vec[4] = new Vector2(-32f + num3, 56f);
				this.vec[5] = new Vector2(-34f + num3, 94f);
				this.vec[6] = new Vector2(-32f + num3, 134f);
				this.vec[7] = new Vector2(-32f + num3, 178f);
				this.vec[8] = new Vector2(-32f + num3, 218f);
				this.vec[9] = new Vector2(-32f + num3, 266f);
				this.vec[10] = new Vector2(-34f + num3, 310f);
				this.vec[11] = new Vector2(-42f, -196f);
				this.vec[12] = new Vector2(114f, -196f);
				this.vec[13] = new Vector2(-50f, -160f);
				this.vec[14] = new Vector2(120f, -160f);
				this.vec[15] = new Vector2(-48f, -124f);
				this.vec[16] = new Vector2(114f, -126f);
				this.vec[17] = new Vector2(-44f, -86f);
				this.vec[18] = new Vector2(112f, -86f);
				this.vec[19] = new Vector2(-40f, -48f);
				this.vec[20] = new Vector2(118f, -50f);
				this.vec[21] = new Vector2(-32f, -10f);
				this.vec[22] = new Vector2(118f, -10f);
				this.vec[23] = new Vector2(-46f, 30f);
				this.vec[24] = new Vector2(118f, 28f);
				this.vec[25] = new Vector2(-46f, 72f);
				this.vec[26] = new Vector2(116f, 68f);
				this.vec[27] = new Vector2(-58f, 114f);
				this.vec[28] = new Vector2(118f, 110f);
				this.vec[29] = new Vector2(-44f, 160f);
				this.vec[30] = new Vector2(120f, 156f);
				this.vec[31] = new Vector2(-48f, 206f);
				this.vec[32] = new Vector2(126f, 202f);
				this.vec[33] = new Vector2(30f, 270f);
				this.vec[34] = new Vector2(2f, 378f);
				this.vec[35] = new Vector2(-84f, -80f);
				this.vec[36] = new Vector2(-88f, -44f);
				this.vec[37] = new Vector2(-86f, -8f);
				this.vec[38] = new Vector2(-86f, 26f);
				this.vec[39] = new Vector2(-82f, 62f);
				this.vec[40] = new Vector2(-84f, 98f);
				this.vec[41] = new Vector2(-88f, 132f);
				this.vec[42] = new Vector2(-86f, 168f);
				this.vec[43] = new Vector2(-84f, 206f);
				this.vec[44] = new Vector2(-84f, 242f);
				this.vec[45] = new Vector2(-86f, 278f);
				this.vec[46] = new Vector2(-84f, 314f);
				this.vec[47] = new Vector2(-42f, -182f);
				this.vec[48] = new Vector2(116f, -184f);
				this.vec[49] = new Vector2(-36f, -148f);
				this.vec[50] = new Vector2(120f, -150f);
				this.vec[51] = new Vector2(-36f, -114f);
				this.vec[52] = new Vector2(128f, -114f);
				this.vec[53] = new Vector2(-34f, -78f);
				this.vec[54] = new Vector2(122f, -80f);
				this.vec[55] = new Vector2(-48f, -40f);
				this.vec[56] = new Vector2(126f, -44f);
				this.vec[57] = new Vector2(-44f, -6f);
				this.vec[58] = new Vector2(122f, -10f);
				this.vec[59] = new Vector2(-42f, 28f);
				this.vec[60] = new Vector2(124f, 26f);
				this.vec[61] = new Vector2(-34f, 64f);
				this.vec[62] = new Vector2(126f, 62f);
				this.vec[63] = new Vector2(-34f, 100f);
				this.vec[64] = new Vector2(126f, 98f);
				this.vec[65] = new Vector2(-36f, 138f);
				this.vec[66] = new Vector2(128f, 136f);
				this.vec[67] = new Vector2(-34f, 172f);
				this.vec[68] = new Vector2(126f, 170f);
				this.vec[69] = new Vector2(-36f, 208f);
				this.vec[70] = new Vector2(130f, 210f);
				this.vec[71] = new Vector2(-56f, 274f);
				this.vec[72] = new Vector2(-58f, 378f);
				this.vec[73] = new Vector2(126f, 270f);
				this.vec[74] = new Vector2(136f, 376f);
			}
			if (this.choice == "moon")
			{
				this.slider1 = (int)(this.sc.realDarkness * 10f);
				this.slider2 = this.sc.realMoon;
				this.vec[0] = new Vector2(29f, 44f);
				this.vec[1] = new Vector2(33f, 132f);
				this.vec[2] = new Vector2(-20f, -52f);
				this.vec[3] = new Vector2(88f, -53f);
				this.vec[4] = new Vector2(8f, 37f);
				this.vec[5] = new Vector2(7f, 36f);
				this.vec[6] = new Vector2(42f, 106f);
				this.vec[7] = new Vector2(100f, 214f);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x001021BC File Offset: 0x001003BC
		public override void HandleInput(InputState input)
		{
			EventHandler<PlayerIndexEventArgs> eventHandler = null;
			EventHandler<PlayerIndexEventArgs> eventHandler2 = null;
			if (this.sc.deactivated)
			{
				return;
			}
			if (base.ControllingPlayer != null)
			{
				int value = (int)base.ControllingPlayer.Value;
				this.gamestate = input.CurrentGamePadStates[value];
				this.prevstate = input.LastGamePadStates[value];
			}
			this.prevKey = input.lastKeyState;
			this.keyState = input.currentKeyState;
			this.prevMouse = this.mouseState;
			this.mouseState = Mouse.GetState();
			this.mm.X = (float)this.mouseState.X;
			this.mm.Y = (float)this.mouseState.Y;
			if (this.mouseState.X == (int)this.sc.mymouse.X)
			{
				int y = this.mouseState.Y;
				int num = (int)this.sc.mymouse.Y;
			}
			this.sc.mymouse.X = (float)this.mouseState.X;
			this.sc.mymouse.Y = (float)this.mouseState.Y;
			bool flag = this.mouseState.RightButton == ButtonState.Pressed && this.prevMouse.RightButton == ButtonState.Released;
			bool flag2 = this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released;
			bool flag3 = this.mouseState.LeftButton == ButtonState.Pressed;
			bool flag4 = this.mouseState.MiddleButton == ButtonState.Pressed && this.prevMouse.MiddleButton == ButtonState.Released;
			bool flag5 = this.mouseState.XButton1 == ButtonState.Pressed && this.prevMouse.XButton1 == ButtonState.Released;
			bool flag6 = this.mouseState.XButton2 == ButtonState.Pressed && this.prevMouse.XButton2 == ButtonState.Released;
			if (this.delayinput)
			{
				this.prevMouse = this.mouseState;
				this.prevKey = this.keyState;
				this.prevstate = this.gamestate;
				flag = false;
				flag2 = false;
				flag4 = false;
				flag5 = false;
				flag6 = false;
			}
			if (this.choice == "restart")
			{
				if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex) || (flag2 && this.myIndex == 3))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) || (flag2 && this.myIndex == 2))
				{
					this.sc.currentDay = this.updownIndex;
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuLeft(base.ControllingPlayer) || (flag2 && this.myIndex == 0))
				{
					this.updownIndex--;
					if (!this.sc.cheat_skipday && this.updownIndex < 1)
					{
						this.updownIndex = Math.Max((int)(this.sc.maxDay() + 1), 1);
					}
					if (this.updownIndex > 101)
					{
						this.updownIndex = 1;
					}
					if (this.updownIndex < 1)
					{
						this.updownIndex = 101;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuRight(base.ControllingPlayer) || (flag2 && this.myIndex == 1))
				{
					this.updownIndex++;
					if (!this.sc.cheat_skipday && this.updownIndex > (int)(this.sc.maxDay() + 1))
					{
						this.updownIndex = 1;
					}
					if (this.updownIndex > 101)
					{
						this.updownIndex = 1;
					}
					if (this.updownIndex < 1)
					{
						this.updownIndex = 101;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
			}
			if (this.choice == "kicks")
			{
				if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if ((flag2 && this.myIndex == 0) || (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 0))
				{
					if (this.sc.kickplayerID.Count >= 5)
					{
						this.sc.kickID = this.sc.kickplayerID[4];
						string text = this.sc.kickplayerName[4];
						this.sc.kickplayerID.Clear();
						this.sc.kickplayerName.Clear();
						this.sc.kickplayerName.Add(text);
						this.sc.kickplayerID.Add(this.sc.kickID);
						if (this.Accepted != null)
						{
							this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						base.ExitScreen();
					}
					else
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
				}
				else if ((flag2 && this.myIndex == 1) || (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 1))
				{
					if (this.sc.kickplayerID.Count >= 4)
					{
						this.sc.kickID = this.sc.kickplayerID[3];
						string text2 = this.sc.kickplayerName[3];
						this.sc.kickplayerID.Clear();
						this.sc.kickplayerName.Clear();
						this.sc.kickplayerName.Add(text2);
						this.sc.kickplayerID.Add(this.sc.kickID);
						if (this.Accepted != null)
						{
							this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						base.ExitScreen();
					}
					else
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
				}
				else if ((flag2 && this.myIndex == 2) || (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 2))
				{
					if (this.sc.kickplayerID.Count >= 3)
					{
						this.sc.kickID = this.sc.kickplayerID[2];
						string text3 = this.sc.kickplayerName[2];
						this.sc.kickplayerID.Clear();
						this.sc.kickplayerName.Clear();
						this.sc.kickplayerName.Add(text3);
						this.sc.kickplayerID.Add(this.sc.kickID);
						this.sc.abort.Play(1f, -1f, -1f);
						if (this.Accepted != null)
						{
							this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						base.ExitScreen();
					}
					else
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
				}
				else if ((flag2 && this.myIndex == 3) || (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 3))
				{
					if (this.sc.kickplayerID.Count >= 2)
					{
						this.sc.kickID = this.sc.kickplayerID[1];
						string text4 = this.sc.kickplayerName[1];
						this.sc.kickplayerID.Clear();
						this.sc.kickplayerName.Clear();
						this.sc.kickplayerName.Add(text4);
						this.sc.kickplayerID.Add(this.sc.kickID);
						this.sc.abort.Play(1f, 1f, 1f);
						if (this.Accepted != null)
						{
							this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						base.ExitScreen();
					}
					else
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
				}
				else if ((flag2 && this.myIndex == 4) || (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 4))
				{
					if (this.sc.kickplayerID.Count >= 1)
					{
						this.sc.kickID = this.sc.kickplayerID[0];
						string text5 = this.sc.kickplayerName[0];
						this.sc.kickplayerID.Clear();
						this.sc.kickplayerName.Clear();
						this.sc.kickplayerName.Add(text5);
						this.sc.kickplayerID.Add(this.sc.kickID);
						if (this.Accepted != null)
						{
							this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						base.ExitScreen();
					}
					else
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
					}
				}
				else if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 4;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 4)
					{
						this.updownIndex = 0;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
			}
			if (this.choice == "options")
			{
				if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if ((flag2 && this.myIndex == 0) || (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 0))
				{
					walletBoxBB walletBoxBB = new walletBoxBB("audio");
					walletBoxBB.Accepted += delegate
					{
						this.hideMenu = false;
						this.rampIN = 1f;
						this.cashout.Play(this.sc.ev * 0.9f, 0f, 0f);
					};
					walletBoxBB.Cancelled += delegate
					{
						this.hideMenu = false;
						this.rampIN = 1f;
						this.delayinput = true;
					};
					base.ScreenManager.AddScreen(walletBoxBB, new PlayerIndex?(this.playerIndex));
				}
				else if ((flag2 && this.myIndex == 1) || (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 1))
				{
					walletBoxBB walletBoxBB2 = new walletBoxBB("video");
					walletBoxBB2.Accepted += delegate
					{
						this.hideMenu = false;
						this.sc.showVideoSetup = false;
						this.sc.resetColors();
						this.rampIN = 1f;
						this.cashout.Play(this.sc.ev * 0.9f, 0f, 0f);
					};
					walletBoxBB2.Cancelled += delegate
					{
						this.hideMenu = false;
						this.sc.showVideoSetup = false;
						this.sc.resetColors();
						this.rampIN = 1f;
						this.delayinput = true;
					};
					base.ScreenManager.AddScreen(walletBoxBB2, new PlayerIndex?(this.playerIndex));
					this.hideMenu = true;
					this.sc.showVideoSetup = true;
				}
				else if ((flag2 && this.myIndex == 2) || (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 2))
				{
					walletBoxBB walletBoxBB3 = new walletBoxBB("game");
					walletBoxBB3.Accepted += delegate
					{
						this.hideMenu = false;
						this.rampIN = 1f;
						this.cashout.Play(this.sc.ev * 0.9f, 0f, 0f);
					};
					walletBoxBB3.Cancelled += delegate
					{
						this.hideMenu = false;
						this.rampIN = 1f;
						this.cashout.Play(this.sc.ev * 0.7f, 0f, 0f);
						this.delayinput = true;
					};
					base.ScreenManager.AddScreen(walletBoxBB3, new PlayerIndex?(this.playerIndex));
				}
				else if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 2;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 2)
					{
						this.updownIndex = 0;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
			}
			if (this.choice == "audio")
			{
				if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if ((flag2 && this.myIndex == 3) || input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					this.sc.mv = (float)(this.slider1 * this.slider1) / 100f;
					this.sc.ev = (float)(this.slider2 * this.slider2) / 100f;
					this.sc.vv = (float)(this.slider3 * this.slider3) / 100f;
					this.sc.SavePrefs();
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				if (this.sc.usingMouse)
				{
					if (this.myIndex == 0)
					{
						if (flag2)
						{
							this.origMouseX = this.adjustMouse().X;
							this.sliderbox1a = this.sliderbox1;
						}
						if (flag3)
						{
							this.sliderbox1 = this.sliderbox1a + (this.adjustMouse().X - this.origMouseX);
							this.sliderbox1 = MathHelper.Clamp(this.sliderbox1, -84f, 84f);
							if (this.slider1 != (int)((this.sliderbox1 + 84f) / 16.8f))
							{
								this.sc.tick.Play(0.8f, MathHelper.Clamp(-0.5f + (float)this.slider1 / 10f, -1f, 1f), 0f);
							}
							this.slider1 = (int)((this.sliderbox1 + 84f) / 16.8f);
							this.sc.mv = (float)(this.slider1 * this.slider1) / 100f;
						}
					}
					if (this.myIndex == 1)
					{
						if (flag2)
						{
							this.origMouseX = this.adjustMouse().X;
							this.sliderbox2a = this.sliderbox2;
						}
						if (flag3)
						{
							this.sliderbox2 = this.sliderbox2a + (this.adjustMouse().X - this.origMouseX);
							this.sliderbox2 = MathHelper.Clamp(this.sliderbox2, -84f, 84f);
							if (this.slider2 != (int)((this.sliderbox2 + 84f) / 16.8f))
							{
								this.sc.tick.Play(0.8f, MathHelper.Clamp(-0.5f + (float)this.slider2 / 10f, -1f, 1f), 0f);
							}
							this.slider2 = (int)((this.sliderbox2 + 84f) / 16.8f);
							this.sc.ev = (float)(this.slider2 * this.slider2) / 100f;
						}
					}
					if (this.myIndex == 2)
					{
						if (flag2)
						{
							this.origMouseX = this.adjustMouse().X;
							this.sliderbox3a = this.sliderbox3;
						}
						if (flag3)
						{
							this.sliderbox3 = this.sliderbox3a + (this.adjustMouse().X - this.origMouseX);
							this.sliderbox3 = MathHelper.Clamp(this.sliderbox3, -84f, 84f);
							if (this.slider3 != (int)((this.sliderbox3 + 84f) / 16.8f))
							{
								this.sc.tick.Play(0.8f, MathHelper.Clamp(-0.5f + (float)this.slider3 / 10f, -1f, 1f), 0f);
							}
							this.slider3 = (int)((this.sliderbox3 + 84f) / 16.8f);
							this.sc.vv = (float)(this.slider3 * this.slider3) / 100f;
						}
					}
				}
				if (!this.sc.usingMouse)
				{
					if (input.IsMenuUp(base.ControllingPlayer))
					{
						this.updownIndex--;
						if (this.updownIndex < 0)
						{
							this.updownIndex = 2;
						}
						this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
					}
					else if (input.IsMenuDown(base.ControllingPlayer))
					{
						this.updownIndex++;
						if (this.updownIndex > 2)
						{
							this.updownIndex = 0;
						}
						this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
					}
					else if (input.IsMenuRight(base.ControllingPlayer))
					{
						if (this.updownIndex == 0)
						{
							this.slider1++;
							if (this.slider1 > 10)
							{
								this.slider1 = 10;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
							this.sc.mv = (float)(this.slider1 * this.slider1) / 100f;
						}
						if (this.updownIndex == 1)
						{
							this.slider2++;
							if (this.slider2 > 10)
							{
								this.slider2 = 10;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
							this.sc.ev = (float)(this.slider2 * this.slider2) / 100f;
						}
						if (this.updownIndex == 2)
						{
							this.slider3++;
							if (this.slider3 > 10)
							{
								this.slider3 = 10;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
							this.sc.vv = (float)(this.slider3 * this.slider3) / 100f;
						}
					}
					else if (input.IsMenuLeft(base.ControllingPlayer))
					{
						if (this.updownIndex == 0)
						{
							this.slider1--;
							if (this.slider1 < 0)
							{
								this.slider1 = 0;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
							}
							this.sc.mv = (float)(this.slider1 * this.slider1) / 100f;
						}
						if (this.updownIndex == 1)
						{
							this.slider2--;
							if (this.slider2 < 0)
							{
								this.slider2 = 0;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
							}
							this.sc.ev = (float)(this.slider2 * this.slider2) / 100f;
						}
						if (this.updownIndex == 2)
						{
							this.slider3--;
							if (this.slider3 < 0)
							{
								this.slider3 = 0;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
							}
							this.sc.vv = (float)(this.slider3 * this.slider3) / 100f;
						}
					}
				}
			}
			if (this.choice == "video")
			{
				if (this.gamestate.Buttons.RightShoulder == ButtonState.Pressed || this.keyState.IsKeyDown(Keys.OemCloseBrackets))
				{
					this.sc.brightness++;
					if (this.sc.brightness > 230)
					{
						this.sc.brightness = 230;
					}
					this.sliderbox1 = (float)this.sc.brightness * 0.6588f - 84f;
					this.sliderbox2 = (float)this.sc.contrast * 0.6588f - 84f;
				}
				if (this.gamestate.Buttons.LeftShoulder == ButtonState.Pressed || this.keyState.IsKeyDown(Keys.OemOpenBrackets))
				{
					this.sc.brightness--;
					if (this.sc.brightness < 25)
					{
						this.sc.brightness = 25;
					}
					this.sliderbox1 = (float)this.sc.brightness * 0.6588f - 84f;
					this.sliderbox2 = (float)this.sc.contrast * 0.6588f - 84f;
				}
				if (this.gamestate.Triggers.Right > 0f || this.keyState.IsKeyDown(Keys.OemQuotes))
				{
					this.sc.contrast++;
					if (this.sc.contrast > 230)
					{
						this.sc.contrast = 230;
					}
					this.sliderbox1 = (float)this.sc.brightness * 0.6588f - 84f;
					this.sliderbox2 = (float)this.sc.contrast * 0.6588f - 84f;
				}
				if (this.gamestate.Triggers.Left > 0f || this.keyState.IsKeyDown(Keys.OemSemicolon))
				{
					this.sc.contrast--;
					if (this.sc.contrast < 25)
					{
						this.sc.contrast = 25;
					}
					this.sliderbox1 = (float)this.sc.brightness * 0.6588f - 84f;
					this.sliderbox2 = (float)this.sc.contrast * 0.6588f - 84f;
				}
				if (this.sc.usingMouse)
				{
					if (this.myIndex == 0)
					{
						if (flag2)
						{
							this.origMouseX = this.adjustMouse().X;
							this.sliderbox1a = this.sliderbox1;
						}
						if (flag3)
						{
							this.sliderbox1 = this.sliderbox1a + (this.adjustMouse().X - this.origMouseX);
							this.sliderbox1 = MathHelper.Clamp(this.sliderbox1, -84f, 84f);
							if (this.slider1 != (int)((this.sliderbox1 + 84f) / 0.6588f))
							{
								this.sc.tick.Play(0.8f, MathHelper.Clamp(this.sliderbox1 / 80f, -1f, 1f), 0f);
							}
							this.slider1 = (int)((this.sliderbox1 + 84f) / 0.6588f);
							this.sc.brightness = (int)MathHelper.Clamp((float)this.slider1, 25f, 230f);
						}
					}
					if (this.myIndex == 1)
					{
						if (flag2)
						{
							this.origMouseX = this.adjustMouse().X;
							this.sliderbox2a = this.sliderbox2;
						}
						if (flag3)
						{
							this.sliderbox2 = this.sliderbox2a + (this.adjustMouse().X - this.origMouseX);
							this.sliderbox2 = MathHelper.Clamp(this.sliderbox2, -84f, 84f);
							if (this.slider2 != (int)((this.sliderbox2 + 84f) / 0.6588f))
							{
								this.sc.tick.Play(0.8f, MathHelper.Clamp(this.sliderbox2 / 80f, -1f, 1f), 0f);
							}
							this.slider2 = (int)((this.sliderbox2 + 84f) / 0.6588f);
							this.sc.contrast = (int)MathHelper.Clamp((float)this.slider2, 25f, 230f);
						}
					}
					if (this.myIndex == 5)
					{
						this.updownIndex = 0;
						if (flag2)
						{
							this.origMouseXY = this.adjustMouse2();
							this.origPlaque = this.sc.hud_enemy;
						}
						if (flag3)
						{
							this.sc.hud_enemy = this.origPlaque + (this.adjustMouse2() - this.origMouseXY);
						}
					}
					if (this.myIndex == 6)
					{
						this.updownIndex = 1;
						if (flag2)
						{
							this.origMouseXY = this.adjustMouse2();
							this.origPlaque = this.sc.hud_clock;
						}
						if (flag3)
						{
							this.sc.hud_clock = this.origPlaque + (this.adjustMouse2() - this.origMouseXY);
						}
					}
					if (this.myIndex == 7)
					{
						this.updownIndex = 2;
						if (flag2)
						{
							this.origMouseXY = this.adjustMouse2();
							this.origPlaque = this.sc.hud_day;
						}
						if (flag3)
						{
							this.sc.hud_day = this.origPlaque + (this.adjustMouse2() - this.origMouseXY);
						}
					}
					if (this.myIndex == 8)
					{
						this.updownIndex = 3;
						if (flag2)
						{
							this.origMouseXY = this.adjustMouse2();
							this.origPlaque = this.sc.hud_weapons;
						}
						if (flag3)
						{
							this.sc.hud_weapons = this.origPlaque + (this.adjustMouse2() - this.origMouseXY);
						}
					}
					if (this.myIndex == 9)
					{
						this.updownIndex = 4;
						if (flag2)
						{
							this.origMouseXY = this.adjustMouse2();
							this.origPlaque = this.sc.hud_dpad;
						}
						if (flag3)
						{
							this.sc.hud_dpad = this.origPlaque + (this.adjustMouse2() - this.origMouseXY);
						}
					}
					if (this.myIndex == 10)
					{
						this.updownIndex = 5;
						if (flag2)
						{
							this.origMouseXY = this.adjustMouse2();
							this.origPlaque = this.sc.hud_player1;
						}
						if (flag3)
						{
							this.sc.hud_player1 = this.origPlaque + (this.adjustMouse2() - this.origMouseXY);
						}
					}
					if (this.myIndex == 11)
					{
						this.updownIndex = 6;
						if (flag2)
						{
							this.origMouseXY = this.adjustMouse2();
							this.origPlaque = this.sc.hud_player2;
						}
						if (flag3)
						{
							this.sc.hud_player2 = this.origPlaque + (this.adjustMouse2() - this.origMouseXY);
						}
					}
				}
				if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				if ((flag2 && this.myIndex == 4) || input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex) || input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					this.sc.SavePrefs();
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if ((flag2 && this.myIndex == 3) || input.IsNewButtonPress(Buttons.X, base.ControllingPlayer, out this.playerIndex) || (this.keyState.IsKeyDown(Keys.X) && this.prevKey.IsKeyUp(Keys.X)))
				{
					this.sc.resetVideo();
					this.sliderbox1 = (float)this.sc.brightness * 0.6588f - 84f;
					this.sliderbox2 = (float)this.sc.contrast * 0.6588f - 84f;
				}
				else if ((flag2 && this.myIndex == 2) || input.IsNewButtonPress(Buttons.Y, base.ControllingPlayer, out this.playerIndex) || (this.keyState.IsKeyDown(Keys.Y) && this.prevKey.IsKeyUp(Keys.Y)))
				{
					this.updownIndex++;
					if (this.updownIndex > 6)
					{
						this.updownIndex = 0;
					}
				}
				if (this.keyState.IsKeyDown(Keys.Left) || this.keyState.IsKeyDown(Keys.Right) || this.keyState.IsKeyDown(Keys.Up) || this.keyState.IsKeyDown(Keys.Down) || this.gamestate.ThumbSticks.Right.Length() > 0.05f || this.gamestate.ThumbSticks.Left.Length() > 0.05f)
				{
					float num2 = this.gamestate.ThumbSticks.Right.X * this.gamestate.ThumbSticks.Right.X * (float)Math.Sign(this.gamestate.ThumbSticks.Right.X) * 5f;
					float num3 = this.gamestate.ThumbSticks.Right.Y * this.gamestate.ThumbSticks.Right.Y * (float)Math.Sign(this.gamestate.ThumbSticks.Right.Y) * 5f;
					if (this.keyState.IsKeyDown(Keys.Left))
					{
						num2 -= 2f;
					}
					if (this.keyState.IsKeyDown(Keys.Right))
					{
						num2 += 2f;
					}
					if (this.keyState.IsKeyDown(Keys.Up))
					{
						num3 += 2f;
					}
					if (this.keyState.IsKeyDown(Keys.Down))
					{
						num3 -= 2f;
					}
					if (this.gamestate.ThumbSticks.Left.Length() > 0.05f)
					{
						num2 = this.gamestate.ThumbSticks.Left.X * this.gamestate.ThumbSticks.Left.X * (float)Math.Sign(this.gamestate.ThumbSticks.Left.X) * 5f;
						num3 = this.gamestate.ThumbSticks.Left.Y * this.gamestate.ThumbSticks.Left.Y * (float)Math.Sign(this.gamestate.ThumbSticks.Left.Y) * 5f;
					}
					if (this.updownIndex == 0)
					{
						ScreenManager screenManager = this.sc;
						screenManager.hud_enemy.X = screenManager.hud_enemy.X + num2;
						ScreenManager screenManager2 = this.sc;
						screenManager2.hud_enemy.Y = screenManager2.hud_enemy.Y - num3;
					}
					if (this.updownIndex == 1)
					{
						ScreenManager screenManager3 = this.sc;
						screenManager3.hud_clock.X = screenManager3.hud_clock.X + num2;
						ScreenManager screenManager4 = this.sc;
						screenManager4.hud_clock.Y = screenManager4.hud_clock.Y - num3;
					}
					if (this.updownIndex == 2)
					{
						ScreenManager screenManager5 = this.sc;
						screenManager5.hud_day.X = screenManager5.hud_day.X + num2;
						ScreenManager screenManager6 = this.sc;
						screenManager6.hud_day.Y = screenManager6.hud_day.Y - num3;
					}
					if (this.updownIndex == 3)
					{
						ScreenManager screenManager7 = this.sc;
						screenManager7.hud_weapons.X = screenManager7.hud_weapons.X + num2;
						ScreenManager screenManager8 = this.sc;
						screenManager8.hud_weapons.Y = screenManager8.hud_weapons.Y - num3;
					}
					if (this.updownIndex == 4)
					{
						ScreenManager screenManager9 = this.sc;
						screenManager9.hud_dpad.X = screenManager9.hud_dpad.X + num2;
						ScreenManager screenManager10 = this.sc;
						screenManager10.hud_dpad.Y = screenManager10.hud_dpad.Y - num3;
					}
					if (this.updownIndex == 5)
					{
						ScreenManager screenManager11 = this.sc;
						screenManager11.hud_player1.X = screenManager11.hud_player1.X + num2;
						ScreenManager screenManager12 = this.sc;
						screenManager12.hud_player1.Y = screenManager12.hud_player1.Y - num3;
					}
					if (this.updownIndex == 6)
					{
						ScreenManager screenManager13 = this.sc;
						screenManager13.hud_player2.X = screenManager13.hud_player2.X + num2;
						ScreenManager screenManager14 = this.sc;
						screenManager14.hud_player2.Y = screenManager14.hud_player2.Y - num3;
					}
				}
			}
			if (this.choice == "game")
			{
				if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					if (this.slider1 == 0)
					{
						this.sc.pad_invertY = 1f;
					}
					if (this.slider1 == 1)
					{
						this.sc.pad_invertY = -1f;
					}
					if (this.slider2 == 1)
					{
						this.sc.pad_sensitivity = 3.5f;
					}
					if (this.slider2 == 2)
					{
						this.sc.pad_sensitivity = 2.5f;
					}
					if (this.slider2 == 3)
					{
						this.sc.pad_sensitivity = 2f;
					}
					if (this.slider2 == 4)
					{
						this.sc.pad_sensitivity = 1.5f;
					}
					if (this.slider2 == 5)
					{
						this.sc.pad_sensitivity = 1f;
					}
					if (this.slider2 == 6)
					{
						this.sc.pad_sensitivity = 0.76f;
					}
					if (this.slider2 == 7)
					{
						this.sc.pad_sensitivity = 0.5f;
					}
					if (this.slider2 == 8)
					{
						this.sc.pad_sensitivity = 0.25f;
					}
					if (this.slider3 == 0)
					{
						this.sc.pad_vibro = false;
					}
					if (this.slider3 == 1)
					{
						this.sc.pad_vibro = true;
					}
					if (this.slider5 == 0)
					{
						this.sc.pad_togglesprint = false;
					}
					if (this.slider5 == 1)
					{
						this.sc.pad_togglesprint = true;
					}
					this.sc.df = this.slider4;
					this.sc.SavePrefs();
					base.ExitScreen();
				}
				else if ((flag2 && this.myIndex == 5) || input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.slider1 == 0)
					{
						this.sc.pad_invertY = 1f;
					}
					if (this.slider1 == 1)
					{
						this.sc.pad_invertY = -1f;
					}
					if (this.slider2 == 1)
					{
						this.sc.pad_sensitivity = 3.5f;
					}
					if (this.slider2 == 2)
					{
						this.sc.pad_sensitivity = 2.5f;
					}
					if (this.slider2 == 3)
					{
						this.sc.pad_sensitivity = 2f;
					}
					if (this.slider2 == 4)
					{
						this.sc.pad_sensitivity = 1.5f;
					}
					if (this.slider2 == 5)
					{
						this.sc.pad_sensitivity = 1f;
					}
					if (this.slider2 == 6)
					{
						this.sc.pad_sensitivity = 0.76f;
					}
					if (this.slider2 == 7)
					{
						this.sc.pad_sensitivity = 0.5f;
					}
					if (this.slider2 == 8)
					{
						this.sc.pad_sensitivity = 0.25f;
					}
					if (this.slider3 == 0)
					{
						this.sc.pad_vibro = false;
					}
					if (this.slider3 == 1)
					{
						this.sc.pad_vibro = true;
					}
					if (this.slider5 == 0)
					{
						this.sc.pad_togglesprint = false;
					}
					if (this.slider5 == 1)
					{
						this.sc.pad_togglesprint = true;
					}
					this.sc.df = this.slider4;
					this.sc.SavePrefs();
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				if (this.sc.usingMouse && this.myIndex != 5)
				{
					if (flag2)
					{
						if (this.myIndex == 0)
						{
							this.slider1++;
							if (this.slider1 > 1)
							{
								this.slider1 = 0;
							}
							this.drip.Play(this.sc.ev, 0f, 0f);
						}
						if (this.myIndex == 1)
						{
							this.slider2++;
							if (this.slider2 > 8)
							{
								this.slider2 = 1;
							}
							this.drip.Play(this.sc.ev, 0f, 0f);
						}
						if (this.myIndex == 2)
						{
							this.slider3++;
							if (this.slider3 > 1)
							{
								this.slider3 = 0;
							}
							this.drip.Play(this.sc.ev, 0f, 0f);
						}
						if (this.myIndex == 3)
						{
							this.slider5++;
							if (this.slider5 > 1)
							{
								this.slider5 = 0;
							}
							this.drip.Play(this.sc.ev, 0f, 0f);
						}
						if (this.myIndex == 4)
						{
							this.slider4++;
							if (this.slider4 > 2)
							{
								this.slider4 = 0;
							}
							this.drip.Play(this.sc.ev, 0f, 0f);
						}
					}
				}
				else if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 4;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 4)
					{
						this.updownIndex = 0;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuRight(base.ControllingPlayer))
				{
					if (this.updownIndex == 0)
					{
						this.slider1++;
						if (this.slider1 > 1)
						{
							this.slider1 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev, 0f, 0f);
						}
					}
					if (this.updownIndex == 1)
					{
						this.slider2++;
						if (this.slider2 > 8)
						{
							this.slider2 = 8;
						}
						else
						{
							this.drip.Play(this.sc.ev, 0f, 0f);
						}
					}
					if (this.updownIndex == 2)
					{
						this.slider3++;
						if (this.slider3 > 1)
						{
							this.slider3 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev, 0f, 0f);
						}
					}
					if (this.updownIndex == 3)
					{
						this.slider5++;
						if (this.slider5 > 1)
						{
							this.slider5 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev, 0f, 0f);
						}
					}
					if (this.updownIndex == 4)
					{
						this.slider4++;
						if (this.slider4 > 2)
						{
							this.slider4 = 2;
						}
						else
						{
							this.drip.Play(this.sc.ev, 0f, 0f);
						}
					}
				}
				else if (input.IsMenuLeft(base.ControllingPlayer))
				{
					if (this.updownIndex == 0)
					{
						this.slider1--;
						if (this.slider1 < 0)
						{
							this.slider1 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 1)
					{
						this.slider2--;
						if (this.slider2 < 1)
						{
							this.slider2 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 2)
					{
						this.slider3--;
						if (this.slider3 < 0)
						{
							this.slider3 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 3)
					{
						this.slider5--;
						if (this.slider5 < 0)
						{
							this.slider5 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 4)
					{
						this.slider4--;
						if (this.slider4 < 0)
						{
							this.slider4 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev, -0.2f, 0f);
						}
					}
				}
			}
			if (this.choice == "help")
			{
				if (this.sc.usingMouse)
				{
					if (flag2 && this.myIndex == 0)
					{
						this.sc.LoadSpaceKeys();
						walletBoxBB walletBoxBB4 = new walletBoxBB("rebind");
						walletBoxBB4.Accepted += delegate
						{
							this.delayinput = true;
						};
						walletBoxBB4.Cancelled += delegate
						{
							this.delayinput = true;
						};
						base.ScreenManager.AddScreen(walletBoxBB4, new PlayerIndex?(this.playerIndex));
						this.delayinput = true;
					}
					if (flag2 && this.myIndex == 1)
					{
						walletBoxBB walletBoxBB5 = new walletBoxBB("hotkeys");
						walletBoxBB5.Accepted += delegate
						{
							this.delayinput = true;
						};
						walletBoxBB5.Cancelled += delegate
						{
							this.delayinput = true;
						};
						base.ScreenManager.AddScreen(walletBoxBB5, new PlayerIndex?(this.playerIndex));
						this.delayinput = true;
					}
				}
				if (!this.sc.usingMouse)
				{
					if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 0)
					{
						walletBoxBB walletBoxBB6 = new walletBoxBB("instructions");
						walletBoxBB6.Accepted += delegate
						{
						};
						walletBoxBB6.Cancelled += delegate
						{
						};
						base.ScreenManager.AddScreen(walletBoxBB6, new PlayerIndex?(this.playerIndex));
					}
					if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 1)
					{
						walletBoxBB walletBoxBB7 = new walletBoxBB("controller");
						walletBoxBB7.Accepted += delegate
						{
						};
						walletBoxBB7.Cancelled += delegate
						{
						};
						base.ScreenManager.AddScreen(walletBoxBB7, new PlayerIndex?(this.playerIndex));
					}
				}
				if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 1;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 1)
					{
						this.updownIndex = 0;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
			}
			if (this.choice == "rebind")
			{
				if (this.keyState.IsKeyDown(Keys.Left) && this.prevKey.IsKeyUp(Keys.Left))
				{
					this.sc.ar1 -= 0.01f;
				}
				if (this.keyState.IsKeyDown(Keys.Right) && this.prevKey.IsKeyUp(Keys.Right))
				{
					this.sc.ar1 += 0.01f;
				}
				if (this.keyState.IsKeyDown(Keys.Up) && this.prevKey.IsKeyUp(Keys.Up))
				{
					this.sc.ar2 += 0.01f;
				}
				if (this.keyState.IsKeyDown(Keys.Down) && this.prevKey.IsKeyUp(Keys.Down))
				{
					this.sc.ar2 -= 0.01f;
				}
				Keys[] pressedKeys = this.keyState.GetPressedKeys();
				if (this.thisKEY != 0 && (flag2 || flag || flag4 || flag5 || flag6) && this.thisKEY == this.whichKEY)
				{
					Keys keys = Keys.VolumeUp;
					if (flag2)
					{
						keys = Keys.VolumeDown;
					}
					if (flag)
					{
						keys = Keys.VolumeUp;
					}
					if (flag4)
					{
						keys = Keys.VolumeMute;
					}
					if (flag5)
					{
						keys = Keys.Print;
					}
					if (flag6)
					{
						keys = Keys.PrintScreen;
					}
					if (this.thisKEY == 9)
					{
						this.sc.space_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 7)
					{
						this.sc.a_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 8)
					{
						this.sc.d_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 5)
					{
						this.sc.w_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 6)
					{
						this.sc.s_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 10)
					{
						this.sc.leftshift_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 11)
					{
						this.sc.r_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 12)
					{
						this.sc.x_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 13)
					{
						this.sc.t_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 28)
					{
						this.sc.e_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 14)
					{
						this.sc.enter_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 23)
					{
						this.sc.tab_key = keys;
						this.thisKEY = 0;
					}
					if (this.thisKEY == 24)
					{
						this.sc.u_key = keys;
						this.thisKEY = 0;
					}
				}
				if (this.thisKEY != 0 && pressedKeys.Length > 0)
				{
					if (this.thisKEY == 1)
					{
						this.sc.escape_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 2)
					{
						this.sc.lmb_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 3)
					{
						this.sc.rmb_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 5)
					{
						this.sc.w_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 6)
					{
						this.sc.s_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 7)
					{
						this.sc.a_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 8)
					{
						this.sc.d_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 9)
					{
						this.sc.space_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 10)
					{
						this.sc.leftshift_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 11)
					{
						this.sc.r_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 12)
					{
						this.sc.x_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 13)
					{
						this.sc.t_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 14)
					{
						this.sc.enter_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 15)
					{
						this.sc.f1_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 16)
					{
						this.sc.one_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 17)
					{
						this.sc.two_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 18)
					{
						this.sc.three_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 19)
					{
						this.sc.four_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 20)
					{
						this.sc.f1_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 23)
					{
						this.sc.tab_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 24)
					{
						this.sc.u_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 25)
					{
						this.sc.left_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 26)
					{
						this.sc.right_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 27)
					{
						this.sc.up_key = pressedKeys[0];
						this.thisKEY = 0;
					}
					if (this.thisKEY == 28)
					{
						this.sc.e_key = pressedKeys[0];
						this.thisKEY = 0;
					}
				}
				if (flag2 && this.whichKEY == 36)
				{
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				if (flag2)
				{
					if (this.whichKEY == 4)
					{
						this.sc.abort.Play(this.sc.ev, 0f, 0f);
						this.thisKEY = 0;
					}
					if (this.whichKEY == 30)
					{
						this.sc.SaveSpaceKeys();
						this.sc.getXkey();
						this.sc.harp2.Play(this.sc.ev * 0.4f, 0f, 0f);
						this.thisKEY = 0;
						this.delayinput = true;
						if (this.Cancelled != null)
						{
							this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						base.ExitScreen();
					}
					if (this.whichKEY == 33)
					{
						this.sc.tick.Play(this.sc.ev, 0f, 0f);
						this.thisKEY = 0;
						this.sc.rmb_key = Keys.VolumeUp;
						this.sc.lmb_key = Keys.VolumeDown;
						this.sc.mmb_key = Keys.VolumeMute;
						this.sc.but1_key = Keys.Print;
						this.sc.but2_key = Keys.PrintScreen;
						this.sc.escape_key = Keys.Escape;
						this.sc.w_key = Keys.W;
						this.sc.a_key = Keys.A;
						this.sc.s_key = Keys.S;
						this.sc.d_key = Keys.D;
						this.sc.f_key = Keys.F;
						this.sc.q_key = Keys.Q;
						this.sc.e_key = Keys.E;
						this.sc.x_key = Keys.X;
						this.sc.r_key = Keys.R;
						this.sc.up_key = Keys.Up;
						this.sc.down_key = Keys.Down;
						this.sc.left_key = Keys.Left;
						this.sc.space_key = Keys.Space;
						this.sc.one_key = Keys.D1;
						this.sc.two_key = Keys.D2;
						this.sc.three_key = Keys.D3;
						this.sc.four_key = Keys.D4;
						this.sc.tab_key = Keys.Tab;
						this.sc.f1_key = Keys.F1;
						this.sc.f2_key = Keys.F2;
						this.sc.f3_key = Keys.F3;
						this.sc.leftshift_key = Keys.LeftShift;
						this.sc.t_key = Keys.T;
						this.sc.enter_key = Keys.Enter;
					}
					if (this.whichKEY != 2 || this.whichKEY != 3 || this.whichKEY != 4 || this.whichKEY != 30 || this.whichKEY != 33 || this.whichKEY != 36)
					{
						if (this.thisKEY == this.whichKEY)
						{
							this.thisKEY = 0;
						}
						else
						{
							this.thisKEY = this.whichKEY;
						}
					}
					else
					{
						this.thisKEY = 0;
					}
				}
				if (flag && this.whichKEY == 0)
				{
					this.sc.switch2.Play(this.sc.ev * 0.2f, -0.2f, 0f);
					this.delayinput = true;
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				if (flag && this.thisKEY == 0)
				{
					this.whichKEY = 0;
				}
			}
			if (this.choice == "hotkeys" && (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex)))
			{
				if (this.Cancelled != null)
				{
					this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
				}
				base.ExitScreen();
			}
			if (this.choice == "instructions")
			{
				if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
					this.rot = (float)this.rr.Next(-30, 30) / 1000f;
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 0;
						if (this.Cancelled != null)
						{
							this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						base.ExitScreen();
						this.rot = 0f;
					}
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					this.updownIndex++;
					if (this.updownIndex > 6)
					{
						this.updownIndex = 6;
					}
					else
					{
						this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
						this.rot = (float)this.rr.Next(-30, 30) / 1000f;
					}
				}
				else if (input.IsMenuLeft(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 0;
					}
					else
					{
						this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
						this.rot = (float)this.rr.Next(-30, 30) / 1000f;
					}
				}
				else if (input.IsMenuRight(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 6)
					{
						this.updownIndex = 6;
					}
					else
					{
						this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
						this.rot = (float)this.rr.Next(-30, 30) / 1000f;
					}
				}
				this.scaler = this.scalerZoom + this.gamestate.Triggers.Right / 3f + this.gamestate.Triggers.Left / 3f;
			}
			if (this.choice == "controller")
			{
				if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				if (input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				this.scaler = this.scalerZoom + this.gamestate.Triggers.Right / 3f + this.gamestate.Triggers.Left / 3f;
			}
			if (this.choice == "controller2")
			{
				if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				if (input.IsNewButtonPress(Buttons.A, base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				this.scaler = this.scalerZoom + this.gamestate.Triggers.Right / 3f + this.gamestate.Triggers.Left / 3f;
			}
			if (this.choice == "cheats")
			{
				if ((this.sc.cheat_InfiniteAmmo && this.sc.revengeDay <= 0) || this.sc.cheat_Invincible || this.sc.cheat_allweapons)
				{
					this.sc.myplayerCheats = true;
				}
				else
				{
					this.sc.myplayerCheats = false;
				}
				if (flag || input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					this.cashout.Play(this.sc.ev * 0.9f, 0f, 0f);
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				if (this.sc.usingMouse && this.myIndex != 4 && flag2)
				{
					if (this.myIndex == 0)
					{
						this.slider1++;
						if (this.slider1 > 1)
						{
							this.slider1 = 0;
						}
						this.drip.Play(this.sc.ev, 0f, 0f);
					}
					if (this.myIndex == 1)
					{
						this.slider2++;
						if (this.slider2 > 1)
						{
							this.slider2 = 0;
						}
						this.drip.Play(this.sc.ev, 0f, 0f);
					}
					if (this.myIndex == 2)
					{
						this.slider3++;
						if (this.slider3 > 1)
						{
							this.slider3 = 0;
						}
						this.drip.Play(this.sc.ev, 0f, 0f);
					}
					if (this.myIndex == 3)
					{
						this.slider4++;
						if (this.slider4 > 1)
						{
							this.slider4 = 0;
						}
						this.drip.Play(this.sc.ev, 0f, 0f);
					}
					if (this.slider1 == 0)
					{
						this.sc.cheat_InfiniteAmmo = false;
					}
					if (this.slider1 == 1)
					{
						this.sc.cheat_InfiniteAmmo = true;
					}
					if (this.slider2 == 0)
					{
						this.sc.cheat_Invincible = false;
					}
					if (this.slider2 == 1)
					{
						this.sc.cheat_Invincible = true;
					}
					if (this.slider4 == 0)
					{
						this.sc.cheat_skipday = false;
					}
					if (this.slider4 == 1)
					{
						this.sc.cheat_skipday = true;
					}
					if (this.slider3 == 0)
					{
						this.sc.cheat_allweapons = false;
					}
					if (this.slider3 == 1)
					{
						this.sc.cheat_allweapons = true;
					}
				}
				if (!this.sc.usingMouse)
				{
					if (input.IsMenuUp(base.ControllingPlayer))
					{
						this.updownIndex--;
						if (this.updownIndex < 0)
						{
							this.updownIndex = 3;
						}
						this.jot.Play(this.sc.ev * 0.7f, (float)this.rr.Next(-20, 20) / 100f, 0f);
					}
					else if (input.IsMenuDown(base.ControllingPlayer))
					{
						this.updownIndex++;
						if (this.updownIndex > 3)
						{
							this.updownIndex = 0;
						}
						this.jot.Play(this.sc.ev * 0.7f, (float)this.rr.Next(-20, 20) / 100f, 0f);
					}
					else if (input.IsMenuRight(base.ControllingPlayer))
					{
						if (this.updownIndex == 0)
						{
							this.slider1++;
							if (this.slider1 > 1)
							{
								this.slider1 = 1;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
						}
						if (this.updownIndex == 1)
						{
							this.slider2++;
							if (this.slider2 > 1)
							{
								this.slider2 = 1;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
						}
						if (this.updownIndex == 2)
						{
							this.slider3++;
							if (this.slider3 > 1)
							{
								this.slider3 = 1;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
						}
						if (this.updownIndex == 3)
						{
							this.slider4++;
							if (this.slider4 > 1)
							{
								this.slider4 = 1;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
						}
						if (this.updownIndex == 0)
						{
							this.sc.cheat_InfiniteAmmo = true;
						}
						if (this.updownIndex == 1)
						{
							this.sc.cheat_Invincible = true;
						}
						if (this.updownIndex == 3)
						{
							this.sc.cheat_skipday = true;
						}
						if (this.updownIndex == 2)
						{
							this.sc.cheat_allweapons = true;
						}
					}
					else if (input.IsMenuLeft(base.ControllingPlayer))
					{
						if (this.updownIndex == 0)
						{
							this.slider1--;
							if (this.slider1 < 0)
							{
								this.slider1 = 0;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
						}
						if (this.updownIndex == 1)
						{
							this.slider2--;
							if (this.slider2 < 0)
							{
								this.slider2 = 0;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
						}
						if (this.updownIndex == 2)
						{
							this.slider3--;
							if (this.slider3 < 0)
							{
								this.slider3 = 0;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
						}
						if (this.updownIndex == 3)
						{
							this.slider4--;
							if (this.slider4 < 0)
							{
								this.slider4 = 0;
							}
							else
							{
								this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
							}
						}
						if (this.updownIndex == 0)
						{
							this.sc.cheat_InfiniteAmmo = false;
						}
						if (this.updownIndex == 1)
						{
							this.sc.cheat_Invincible = false;
						}
						if (this.updownIndex == 3)
						{
							this.sc.cheat_skipday = false;
						}
						if (this.updownIndex == 2)
						{
							this.sc.cheat_allweapons = false;
						}
					}
				}
			}
			if (this.choice == "hidden")
			{
				if (input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 0)
				{
					walletBoxBB walletBoxBB8 = new walletBoxBB("networking");
					walletBoxBB8.Accepted += delegate
					{
						this.cashout.Play(this.sc.ev * 0.9f, 0f, 0f);
					};
					base.ScreenManager.AddScreen(walletBoxBB8, new PlayerIndex?(this.playerIndex));
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 1)
				{
					walletBoxBB walletBoxBB9 = new walletBoxBB("debugging");
					walletBoxBB walletBoxBB10 = walletBoxBB9;
					if (eventHandler == null)
					{
						eventHandler = delegate
						{
							this.cashout.Play(this.sc.ev * 0.9f, 0.2f, 0f);
						};
					}
					walletBoxBB10.Accepted += eventHandler;
					base.ScreenManager.AddScreen(walletBoxBB9, new PlayerIndex?(this.playerIndex));
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 2)
				{
					walletBoxBB walletBoxBB11 = new walletBoxBB("cheat mode");
					walletBoxBB walletBoxBB12 = walletBoxBB11;
					if (eventHandler2 == null)
					{
						eventHandler2 = delegate
						{
							if (this.CheatSent != null)
							{
								this.CheatSent(this, new PlayerIndexEventArgs(this.playerIndex));
							}
							base.ExitScreen();
						};
					}
					walletBoxBB12.Accepted += eventHandler2;
					base.ScreenManager.AddScreen(walletBoxBB11, new PlayerIndex?(this.playerIndex));
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex == 3)
				{
					walletBoxBB walletBoxBB13 = new walletBoxBB("level edit");
					walletBoxBB13.Accepted += delegate
					{
						if (this.LevelSent != null)
						{
							this.LevelSent(this, new PlayerIndexEventArgs(this.playerIndex));
						}
						base.ExitScreen();
					};
					base.ScreenManager.AddScreen(walletBoxBB13, new PlayerIndex?(this.playerIndex));
				}
				else if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 3;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 3)
					{
						this.updownIndex = 0;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
			}
			if (this.choice == "networking")
			{
				if (input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.sc.debug_showNetwork = false;
						this.sc.networkQuality = 0;
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Accepted != null)
					{
						if (this.slider1 == 1)
						{
							this.sc.debug_showNetwork = true;
							this.sc.networkQuality = (byte)this.slider2;
						}
						else
						{
							this.sc.networkQuality = 0;
							this.sc.debug_showNetwork = false;
						}
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 2;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 2)
					{
						this.updownIndex = 0;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuRight(base.ControllingPlayer))
				{
					if (this.updownIndex == 0)
					{
						this.slider1++;
						if (this.slider1 > 1)
						{
							this.slider1 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					if (this.updownIndex == 1)
					{
						this.slider2++;
						if (this.slider2 > this.sc.lag.Length - 1)
						{
							this.slider2 = this.sc.lag.Length - 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					if (this.updownIndex == 2)
					{
						this.slider2++;
						if (this.slider2 > this.sc.lag.Length - 1)
						{
							this.slider2 = this.sc.lag.Length - 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
				}
				else if (input.IsMenuLeft(base.ControllingPlayer))
				{
					if (this.updownIndex == 0)
					{
						this.slider1--;
						if (this.slider1 < 0)
						{
							this.slider1 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 1)
					{
						this.slider2--;
						if (this.slider2 < 0)
						{
							this.slider2 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 2)
					{
						this.slider2--;
						if (this.slider2 < 0)
						{
							this.slider2 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
				}
			}
			if (this.choice == "debugging")
			{
				if (input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Cancelled != null)
					{
						this.sc.debug_showSeed = false;
						this.sc.debug_showAccuracy = false;
						this.sc.debug_showGarbage = false;
						this.sc.debug_showHoming = false;
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					if (this.Accepted != null)
					{
						if (this.slider1 == 1)
						{
							this.sc.debug_showGarbage = true;
						}
						else
						{
							this.sc.debug_showGarbage = false;
						}
						if (this.slider2 == 1)
						{
							this.sc.debug_showSeed = true;
						}
						else
						{
							this.sc.debug_showSeed = false;
						}
						if (this.slider3 == 1)
						{
							this.sc.debug_showAccuracy = true;
						}
						else
						{
							this.sc.debug_showAccuracy = false;
						}
						if (this.slider4 == 1)
						{
							this.sc.debug_showHoming = true;
						}
						else
						{
							this.sc.debug_showHoming = false;
						}
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 3;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 3)
					{
						this.updownIndex = 0;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuRight(base.ControllingPlayer))
				{
					if (this.updownIndex == 0)
					{
						this.slider1++;
						if (this.slider1 > 1)
						{
							this.slider1 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					if (this.updownIndex == 1)
					{
						this.slider2++;
						if (this.slider2 > 1)
						{
							this.slider2 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					if (this.updownIndex == 2)
					{
						this.slider3++;
						if (this.slider2 > 1)
						{
							this.slider2 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					if (this.updownIndex == 3)
					{
						this.slider4++;
						if (this.slider4 > 1)
						{
							this.slider4 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
				}
				else if (input.IsMenuLeft(base.ControllingPlayer))
				{
					if (this.updownIndex == 0)
					{
						this.slider1--;
						if (this.slider1 < 0)
						{
							this.slider1 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 1)
					{
						this.slider2--;
						if (this.slider2 < 0)
						{
							this.slider2 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 2)
					{
						this.slider3--;
						if (this.slider2 < 0)
						{
							this.slider2 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 3)
					{
						this.slider4--;
						if (this.slider4 < 0)
						{
							this.slider4 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
				}
			}
			if (this.choice == "cheat mode")
			{
				if (input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					this.sc.cheat_SendPackage = false;
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					this.sc.cheat_SendPackage = true;
					if (this.slider1 == 1)
					{
						this.sc.cheat_Invincible = true;
					}
					else
					{
						this.sc.cheat_Invincible = false;
					}
					if (this.slider2 == 1)
					{
						this.sc.cheat_FastFiring = true;
					}
					else
					{
						this.sc.cheat_FastFiring = false;
					}
					if (this.slider3 == 1)
					{
						this.sc.cheat_InfiniteAmmo = true;
					}
					else
					{
						this.sc.cheat_InfiniteAmmo = false;
					}
					if (this.slider4 == 1)
					{
						this.sc.cheat_AllExplode = true;
					}
					else
					{
						this.sc.cheat_AllExplode = false;
					}
					if (this.slider5 == 1)
					{
						this.sc.days[100] = 1;
						this.sc.cheat_PickupPack = true;
						this.sc.bloodLevel = 600f;
					}
					else
					{
						this.sc.days[100] = 0;
						this.sc.cheat_PickupPack = false;
					}
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 4;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 4)
					{
						this.updownIndex = 0;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuRight(base.ControllingPlayer))
				{
					if (this.updownIndex == 0)
					{
						this.slider1++;
						if (this.slider1 > 1)
						{
							this.slider1 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					if (this.updownIndex == 1)
					{
						this.slider2++;
						if (this.slider2 > 1)
						{
							this.slider2 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					if (this.updownIndex == 2)
					{
						this.slider3++;
						if (this.slider2 > 1)
						{
							this.slider2 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					if (this.updownIndex == 3)
					{
						this.slider4++;
						if (this.slider4 > 1)
						{
							this.slider4 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					if (this.updownIndex == 4)
					{
						this.slider5++;
						if (this.slider5 > 1)
						{
							this.slider5 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
				}
				else if (input.IsMenuLeft(base.ControllingPlayer))
				{
					if (this.updownIndex == 0)
					{
						this.slider1--;
						if (this.slider1 < 0)
						{
							this.slider1 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 1)
					{
						this.slider2--;
						if (this.slider2 < 0)
						{
							this.slider2 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 2)
					{
						this.slider3--;
						if (this.slider2 < 0)
						{
							this.slider2 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 3)
					{
						this.slider4--;
						if (this.slider4 < 0)
						{
							this.slider4 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 4)
					{
						this.slider5--;
						if (this.slider5 < 0)
						{
							this.slider5 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
				}
			}
			if (this.choice == "level edit")
			{
				if (input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex) && this.updownIndex <= 10)
				{
					this.sc.levelChange = false;
					if (this.Cancelled != null)
					{
						this.Cancelled(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex) && this.updownIndex >= 11)
				{
					this.updownIndex = 0;
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex >= 11)
				{
					if (this.slider1 == 0)
					{
						this.sc.newDayTime = "am";
					}
					if (this.slider1 == 1)
					{
						this.sc.newDayTime = "pm";
					}
					this.sc.boar1Spawn = this.slider2;
					this.sc.boar2Spawn = this.slider2;
					this.sc.boarCount = this.slider3;
					this.sc.boar1Health = this.slider4;
					this.sc.boar2Health = this.slider4;
					this.sc.boar1Attack = this.slider5;
					this.sc.boar2Attack = this.slider5;
					this.sc.boar1MinSize = this.slider6;
					this.sc.boar2MinSize = this.slider6;
					this.sc.boar1MaxSize = this.slider7;
					this.sc.boar2MaxSize = this.slider7;
					this.sc.boar1GiantOdds = this.slider8;
					this.sc.boar2GiantOdds = this.slider8;
					this.sc.boar1TinyOdds = this.slider9;
					this.sc.boar2TinyOdds = this.slider9;
					this.sc.boar1Charge = this.slider10;
					this.sc.boar2Charge = this.slider10;
					this.sc.boar1TurnRate = this.slider11;
					this.sc.boar2TurnRate = this.slider11;
					this.sc.boarDistance[0] = this.slider12;
					this.sc.boarDistance[1] = this.slider14;
					this.sc.boarDistance[2] = this.slider16;
					this.sc.boarHomingLimit[0] = this.slider13;
					this.sc.boarHomingLimit[1] = this.slider15;
					this.sc.boarHomingLimit[2] = this.slider17;
					this.sc.levelChange = true;
					this.sc.sendlevelChange = true;
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex) && this.updownIndex <= 10)
				{
					this.updownIndex = 11;
				}
				else if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 0;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 22)
					{
						this.updownIndex = 22;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsNewButtonPress(Buttons.DPadRight, base.ControllingPlayer, out this.playerIndex) || input.IsNewButtonPress(Buttons.LeftThumbstickRight, base.ControllingPlayer, out this.playerIndex))
				{
					if (this.updownIndex == 0)
					{
						this.slider1++;
						if (this.slider1 > 1)
						{
							this.slider1 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
						if (this.slider1 == 1)
						{
							walletBoxBB walletBoxBB14 = new walletBoxBB("moon");
							walletBoxBB14.Accepted += delegate
							{
								this.hideMenu = false;
							};
							walletBoxBB14.Cancelled += delegate
							{
								this.hideMenu = false;
							};
							base.ScreenManager.AddScreen(walletBoxBB14, new PlayerIndex?(this.playerIndex));
							this.hideMenu = true;
						}
					}
					else if (this.updownIndex == 1)
					{
						this.slider2++;
						if (this.slider2 > 22)
						{
							this.slider2 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 2)
					{
						this.slider3 += 6;
						if (this.slider3 > 1200)
						{
							this.slider3 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 3)
					{
						this.slider4 += 10;
						if (this.slider4 > 800)
						{
							this.slider4 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 4)
					{
						this.slider5 += 10;
						if (this.slider5 > 800)
						{
							this.slider5 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 5)
					{
						this.slider6++;
						if (this.slider6 > 35)
						{
							this.slider6 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 6)
					{
						this.slider7++;
						if (this.slider7 > 35)
						{
							this.slider7 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 7)
					{
						this.slider8++;
						if (this.slider8 > 100)
						{
							this.slider8 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 8)
					{
						this.slider9++;
						if (this.slider9 > 100)
						{
							this.slider9 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 9)
					{
						this.slider10 += 10;
						if (this.slider10 > 800)
						{
							this.slider10 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 10)
					{
						this.slider11 += 10;
						if (this.slider11 > 800)
						{
							this.slider11 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 11)
					{
						this.slider12 += 100;
						if (this.slider12 > 4500)
						{
							this.slider12 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 12)
					{
						this.slider13++;
						if (this.slider13 > 50)
						{
							this.slider13 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 13)
					{
						this.slider14 += 100;
						if (this.slider14 > 4500)
						{
							this.slider14 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 14)
					{
						this.slider15++;
						if (this.slider15 > 50)
						{
							this.slider15 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 15)
					{
						this.slider16 += 100;
						if (this.slider16 > 4500)
						{
							this.slider16 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 16)
					{
						this.slider17++;
						if (this.slider17 > 50)
						{
							this.slider17 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 17)
					{
						this.slider18 += 100;
						if (this.slider18 > 4500)
						{
							this.slider18 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 18)
					{
						this.slider19++;
						if (this.slider19 > 50)
						{
							this.slider19 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 19)
					{
						this.slider20 += 100;
						if (this.slider20 > 4500)
						{
							this.slider20 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 20)
					{
						this.slider21++;
						if (this.slider21 > 50)
						{
							this.slider21 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 21)
					{
						this.slider22 += 100;
						if (this.slider22 > 4500)
						{
							this.slider22 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					else if (this.updownIndex == 22)
					{
						this.slider23++;
						if (this.slider23 > 50)
						{
							this.slider23 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
				}
				else if (input.IsNewButtonPress(Buttons.DPadLeft, base.ControllingPlayer, out this.playerIndex) || input.IsNewButtonPress(Buttons.LeftThumbstickLeft, base.ControllingPlayer, out this.playerIndex))
				{
					if (this.updownIndex == 0)
					{
						this.slider1--;
						if (this.slider1 < 0)
						{
							this.slider1 = 1;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
						if (this.slider1 == 1)
						{
							walletBoxBB walletBoxBB15 = new walletBoxBB("moon");
							walletBoxBB15.Accepted += delegate
							{
								this.hideMenu = false;
							};
							walletBoxBB15.Cancelled += delegate
							{
								this.hideMenu = false;
							};
							base.ScreenManager.AddScreen(walletBoxBB15, new PlayerIndex?(this.playerIndex));
							this.hideMenu = true;
						}
					}
					else if (this.updownIndex == 1)
					{
						this.slider2--;
						if (this.slider2 < 0)
						{
							this.slider2 = 22;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 2)
					{
						this.slider3 -= 7;
						if (this.slider3 < 0)
						{
							this.slider3 = 1200;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 3)
					{
						this.slider4 -= 10;
						if (this.slider4 < 0)
						{
							this.slider4 = 800;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 4)
					{
						this.slider5 -= 10;
						if (this.slider5 < 0)
						{
							this.slider5 = 800;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 5)
					{
						this.slider6--;
						if (this.slider6 < 0)
						{
							this.slider6 = 35;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 6)
					{
						this.slider7--;
						if (this.slider7 < 0)
						{
							this.slider7 = 35;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 7)
					{
						this.slider8--;
						if (this.slider8 < 0)
						{
							this.slider8 = 100;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 8)
					{
						this.slider9--;
						if (this.slider9 < 0)
						{
							this.slider9 = 100;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 9)
					{
						this.slider10 -= 10;
						if (this.slider10 < 0)
						{
							this.slider10 = 800;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 10)
					{
						this.slider11 -= 10;
						if (this.slider11 < 0)
						{
							this.slider11 = 800;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 11)
					{
						this.slider12 -= 100;
						if (this.slider12 < 0)
						{
							this.slider12 = 4500;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 12)
					{
						this.slider13--;
						if (this.slider13 < 0)
						{
							this.slider13 = 50;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 13)
					{
						this.slider14 -= 100;
						if (this.slider14 < 0)
						{
							this.slider14 = 4500;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 14)
					{
						this.slider15--;
						if (this.slider15 < 0)
						{
							this.slider15 = 50;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 15)
					{
						this.slider16 -= 100;
						if (this.slider16 < 0)
						{
							this.slider16 = 4500;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 16)
					{
						this.slider17--;
						if (this.slider17 < 0)
						{
							this.slider17 = 50;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 17)
					{
						this.slider18 -= 100;
						if (this.slider18 < 0)
						{
							this.slider18 = 4500;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 18)
					{
						this.slider19--;
						if (this.slider19 < 0)
						{
							this.slider19 = 50;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 19)
					{
						this.slider20 -= 100;
						if (this.slider20 < 0)
						{
							this.slider20 = 4500;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 20)
					{
						this.slider21--;
						if (this.slider21 < 0)
						{
							this.slider21 = 50;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 21)
					{
						this.slider22 -= 100;
						if (this.slider22 < 0)
						{
							this.slider22 = 4500;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					else if (this.updownIndex == 22)
					{
						this.slider23--;
						if (this.slider23 < 0)
						{
							this.slider23 = 50;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
				}
				else if (input.IsNewButtonPress(Buttons.RightThumbstickRight, base.ControllingPlayer, out this.playerIndex))
				{
					if (this.updownIndex >= 11)
					{
						this.slider12 += 100;
						if (this.slider12 > 4500)
						{
							this.slider12 = 0;
						}
						this.slider14 += 100;
						if (this.slider14 > 4500)
						{
							this.slider14 = 0;
						}
						this.slider16 += 100;
						if (this.slider16 > 4500)
						{
							this.slider16 = 0;
						}
						this.slider18 += 100;
						if (this.slider18 > 4500)
						{
							this.slider18 = 0;
						}
						this.slider20 += 100;
						if (this.slider20 > 4500)
						{
							this.slider20 = 0;
						}
						this.slider22 += 100;
						if (this.slider22 > 4500)
						{
							this.slider22 = 0;
						}
						this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
					}
				}
				else if (input.IsNewButtonPress(Buttons.RightThumbstickLeft, base.ControllingPlayer, out this.playerIndex) && this.updownIndex >= 11)
				{
					this.slider12 -= 100;
					if (this.slider12 < 0)
					{
						this.slider12 = 4500;
					}
					this.slider14 -= 100;
					if (this.slider14 < 0)
					{
						this.slider14 = 4500;
					}
					this.slider16 -= 100;
					if (this.slider16 < 0)
					{
						this.slider16 = 4500;
					}
					this.slider18 -= 100;
					if (this.slider18 < 0)
					{
						this.slider18 = 4500;
					}
					this.slider20 -= 100;
					if (this.slider20 < 0)
					{
						this.slider20 = 4500;
					}
					this.slider22 -= 100;
					if (this.slider22 < 0)
					{
						this.slider22 = 4500;
					}
					this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
				}
			}
			if (this.choice == "moon")
			{
				if (input.IsMenuCancel(base.ControllingPlayer, out this.playerIndex))
				{
					this.sc.realMoon = this.slider2;
					this.sc.realDarkness = (float)this.slider1 / 10f;
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuSelect(base.ControllingPlayer, out this.playerIndex))
				{
					this.sc.realMoon = this.slider2;
					this.sc.realDarkness = (float)this.slider1 / 10f;
					if (this.Accepted != null)
					{
						this.Accepted(this, new PlayerIndexEventArgs(this.playerIndex));
					}
					base.ExitScreen();
				}
				else if (input.IsMenuUp(base.ControllingPlayer))
				{
					this.updownIndex--;
					if (this.updownIndex < 0)
					{
						this.updownIndex = 1;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuDown(base.ControllingPlayer))
				{
					this.updownIndex++;
					if (this.updownIndex > 1)
					{
						this.updownIndex = 0;
					}
					this.jot.Play(this.sc.ev * 0.5f, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				else if (input.IsMenuRight(base.ControllingPlayer))
				{
					if (this.updownIndex == 0)
					{
						this.slider1++;
						if (this.slider1 > 10)
						{
							this.slider1 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
					if (this.updownIndex == 1)
					{
						this.slider2++;
						if (this.slider2 > 8)
						{
							this.slider2 = 0;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, 0f, 0f);
						}
					}
				}
				else if (input.IsMenuLeft(base.ControllingPlayer))
				{
					if (this.updownIndex == 0)
					{
						this.slider1--;
						if (this.slider1 < 0)
						{
							this.slider1 = 10;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
					if (this.updownIndex == 1)
					{
						this.slider2--;
						if (this.slider2 < 0)
						{
							this.slider2 = 8;
						}
						else
						{
							this.drip.Play(this.sc.ev * 0.5f, -0.2f, 0f);
						}
					}
				}
			}
			this.delayinput = false;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x001091B0 File Offset: 0x001073B0
		public override void Draw(GameTime gameTime)
		{
			if (this.sc.deactivated)
			{
				return;
			}
			this.rampIN += 0.07f;
			if (this.rampIN > 1f)
			{
				this.rampIN = 1f;
			}
			if (!this.hideMenu)
			{
				if (this.choice == "controller" || this.choice == "controller2" || this.choice == "rebind" || this.choice == "hotkeys")
				{
					this.scc = 1f;
				}
				float num = MathHelper.Lerp(-1090f, 0f, this.rampIN);
				this.paperPos = new Vector2(1280f + num, 620f + num / this.drift) / 2f + this.offset;
				float num2 = 443f;
				int num3 = (int)(num2 * ((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width));
				if (this.sc.aspectratio > 1f)
				{
					num3 += (int)(((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio));
				}
				if (this.choice == "hotkeys" || this.choice == "rebind" || this.choice == "instructions" || this.choice == "controller" || this.choice == "video" || this.choice == "controller2")
				{
				}
				Matrix matrix = Matrix.CreateScale((float)this.sc.width / 1280f, (float)this.sc.hite / 720f, 1f);
				this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, matrix);
				if (this.choice == "restart")
				{
					int num4 = this.myIndex;
					this.myIndex = -1;
					Vector2 vector = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), Color.White, this.rot, vector, 1.2f, SpriteEffects.None, 0f);
					this.queryButton(this.leftArrowRect, new Vector2(-73f, -65f) + this.paperPos, 0);
					this.queryButton(this.rightArrowRect, new Vector2(35f, -65f) + this.paperPos, 1);
					if (this.sc.usingMouse)
					{
						if (this.myIndex == 0)
						{
							this.spriteBatch.Draw(this.sc.paper1, new Vector2(-73f, -65f) + this.paperPos, new Rectangle?(this.leftArrowGlowRect), Color.White);
						}
						else
						{
							this.spriteBatch.Draw(this.sc.paper1, new Vector2(-73f, -65f) + this.paperPos, new Rectangle?(this.leftArrowRect), Color.White);
						}
						if (this.myIndex == 1)
						{
							this.spriteBatch.Draw(this.sc.paper1, new Vector2(35f, -65f) + this.paperPos, new Rectangle?(this.rightArrowGlowRect), Color.White);
						}
						else
						{
							this.spriteBatch.Draw(this.sc.paper1, new Vector2(35f, -65f) + this.paperPos, new Rectangle?(this.rightArrowRect), Color.White);
						}
					}
					else
					{
						this.spriteBatch.Draw(this.sc.paper1, new Vector2(-73f, -65f) + this.paperPos, new Rectangle?(this.leftArrowRect), Color.White);
						this.spriteBatch.Draw(this.sc.paper1, new Vector2(35f, -65f) + this.paperPos, new Rectangle?(this.rightArrowRect), Color.White);
					}
					this.paperPos.Y = this.paperPos.Y - 30f;
					astrobindings.b1.Length = 0;
					astrobindings.b1.Append("Choose Day");
					this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[0], this.bluePen);
					astrobindings.b1.Length = 0;
					astrobindings.b1.Concat(this.updownIndex);
					this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[1], this.bluePen);
					astrobindings.b1.Length = 0;
					if (this.sc.cheat_skipday || this.updownIndex < (int)(this.sc.maxDay() + 1))
					{
						astrobindings.b1.Append(this.dayDescribe[this.updownIndex - 1]);
					}
					else
					{
						astrobindings.b1.Append("not completed");
					}
					this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, new Vector2(0f, 40f) + this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[1], new Color(120, 50, 0));
					astrobindings.b1.Length = 0;
					Color color = this.bluePen;
					if (this.sc.days[this.updownIndex] == 0)
					{
						astrobindings.b1.Append("");
					}
					if (this.sc.days[this.updownIndex] == 1)
					{
						astrobindings.b1.Append("beat on easy");
						color = new Color(120, 50, 0);
					}
					if (this.sc.days[this.updownIndex] == 2)
					{
						astrobindings.b1.Append("beat on normal");
						color = this.bluePen;
					}
					if (this.sc.days[this.updownIndex] == 3)
					{
						astrobindings.b1.Append("beat on hard");
						color = new Color(20, 120, 10);
					}
					this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, new Vector2(0f, 70f) + this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[1], color);
					if (!this.sc.usingMouse)
					{
						astrobindings.b1.Length = 0;
						astrobindings.b1.Append("yes");
						this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, new Vector2(0f, 50f) + this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[2], this.bluePen);
						this.spriteBatch.Draw(this.sc.paper1, new Vector2(0f, 50f) + this.paperPos - vector + this.vec[3], new Rectangle?(this.buttonARect), Color.White);
						astrobindings.b1.Length = 0;
						astrobindings.b1.Append("no");
						this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, new Vector2(0f, 50f) + this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[4], this.bluePen);
						this.spriteBatch.Draw(this.sc.paper1, new Vector2(0f, 50f) + this.paperPos - vector + this.vec[5], new Rectangle?(this.buttonBRect), Color.White);
					}
					else
					{
						this.queryButton(this.buttonBoxRect, new Vector2(0f, 50f) + this.paperPos - vector + this.vec[3], 2);
						this.queryButton(this.buttonBoxRect, new Vector2(20f, 50f) + this.paperPos - vector + this.vec[5], 3);
						astrobindings.b1.Length = 0;
						astrobindings.b1.Append("yes");
						this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, new Vector2(-5f, 50f) + this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[2], this.bluePen);
						astrobindings.b1.Length = 0;
						astrobindings.b1.Append("no");
						this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, new Vector2(5f, 50f) + this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[4], this.bluePen);
						if (this.myIndex == 2)
						{
							this.spriteBatch.Draw(this.sc.paper1, new Vector2(0f, 50f) + this.paperPos - vector + this.vec[3], new Rectangle?(this.buttonGlowRect), Color.White);
						}
						else
						{
							this.spriteBatch.Draw(this.sc.paper1, new Vector2(0f, 50f) + this.paperPos - vector + this.vec[3], new Rectangle?(this.buttonBoxRect), Color.White);
						}
						if (this.myIndex == 3)
						{
							this.spriteBatch.Draw(this.sc.paper1, new Vector2(20f, 50f) + this.paperPos - vector + this.vec[5], new Rectangle?(this.buttonGlowRect), Color.White);
						}
						else
						{
							this.spriteBatch.Draw(this.sc.paper1, new Vector2(20f, 50f) + this.paperPos - vector + this.vec[5], new Rectangle?(this.buttonBoxRect), Color.White);
						}
					}
					if (num4 != this.myIndex && this.myIndex != -1)
					{
						this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
					}
				}
				if (this.choice == "kicks")
				{
					int num5 = this.myIndex;
					this.myIndex = -1;
					Vector2 vector2 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), Color.White, 0f, vector2, 1.25f, SpriteEffects.None, 0f);
					if (this.sc.usingMouse)
					{
						this.queryButton(this.bigboxRect, this.paperPos - vector2 + this.vec[5], 0);
						this.queryButton(this.bigboxRect, this.paperPos - vector2 + this.vec[6], 1);
						this.queryButton(this.bigboxRect, this.paperPos - vector2 + this.vec[7], 2);
						this.queryButton(this.bigboxRect, this.paperPos - vector2 + this.vec[8], 3);
						this.queryButton(this.bigboxRect, this.paperPos - vector2 + this.vec[9], 4);
						this.updownIndex = -1;
					}
					if (this.updownIndex == 0 || this.myIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector2 + this.vec[5], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 1 || this.myIndex == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector2 + this.vec[6], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 2 || this.myIndex == 2)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector2 + this.vec[7], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 3 || this.myIndex == 3)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector2 + this.vec[8], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 4 || this.myIndex == 4)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector2 + this.vec[9], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					string text = "EMPTY SLOT";
					if (this.sc.kickplayerName.Count >= 5)
					{
						text = this.sc.kickplayerName[4];
					}
					this.spriteBatch.DrawString(this.scribbleFont, text, this.paperPos - this.scribbleFont.MeasureString(text) / 2f + this.vec[0], this.bluePen);
					text = "EMPTY SLOT";
					if (this.sc.kickplayerName.Count >= 4)
					{
						text = this.sc.kickplayerName[3];
					}
					this.spriteBatch.DrawString(this.scribbleFont, text, this.paperPos - this.scribbleFont.MeasureString(text) / 2f + this.vec[1], this.bluePen);
					text = "EMPTY SLOT";
					if (this.sc.kickplayerName.Count >= 3)
					{
						text = this.sc.kickplayerName[2];
					}
					this.spriteBatch.DrawString(this.scribbleFont, text, this.paperPos - this.scribbleFont.MeasureString(text) / 2f + this.vec[2], this.bluePen);
					text = "EMPTY SLOT";
					if (this.sc.kickplayerName.Count >= 2)
					{
						text = this.sc.kickplayerName[1];
					}
					this.spriteBatch.DrawString(this.scribbleFont, text, this.paperPos - this.scribbleFont.MeasureString(text) / 2f + this.vec[3], this.bluePen);
					text = "EMPTY SLOT";
					if (this.sc.kickplayerName.Count >= 1)
					{
						text = this.sc.kickplayerName[0];
					}
					this.spriteBatch.DrawString(this.scribbleFont, text, this.paperPos - this.scribbleFont.MeasureString(text) / 2f + this.vec[4], this.bluePen);
					if (num5 != this.myIndex && this.myIndex != -1)
					{
						this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
					}
				}
				if (this.choice == "options")
				{
					int num6 = this.myIndex;
					this.myIndex = -1;
					Vector2 vector3 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), Color.White, this.rot, vector3, 1.1f, SpriteEffects.None, 0f);
					if (this.sc.usingMouse)
					{
						this.queryButton(this.bigboxRect, this.paperPos - vector3 + this.vec[3], 0);
						this.queryButton(this.bigboxRect, this.paperPos - vector3 + this.vec[4], 1);
						this.queryButton(this.bigboxRect, this.paperPos - vector3 + this.vec[5], 2);
						this.updownIndex = -1;
					}
					if (this.updownIndex == 0 || this.myIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector3 + this.vec[3], new Rectangle?(this.bigboxRect), this.bluePen);
					}
					if (this.updownIndex == 1 || this.myIndex == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector3 + this.vec[4], new Rectangle?(this.bigboxRect), this.bluePen);
					}
					if (this.updownIndex == 2 || this.myIndex == 2)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector3 + this.vec[5], new Rectangle?(this.bigboxRect), this.bluePen);
					}
					string text2 = "Audio";
					this.spriteBatch.DrawString(this.scribbleFont, text2, this.paperPos - this.scribbleFont.MeasureString(text2) / 2f + this.vec[0], this.bluePen);
					text2 = "Video";
					this.spriteBatch.DrawString(this.scribbleFont, text2, this.paperPos - this.scribbleFont.MeasureString(text2) / 2f + this.vec[1], this.bluePen);
					text2 = "Game";
					this.spriteBatch.DrawString(this.scribbleFont, text2, this.paperPos - this.scribbleFont.MeasureString(text2) / 2f + this.vec[2], this.bluePen);
					if (num6 != this.myIndex && this.myIndex != -1)
					{
						this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
					}
				}
				if (this.choice == "audio")
				{
					int num7 = this.myIndex;
					this.myIndex = -1;
					Vector2 vector4 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), Color.White, 0f, vector4, 1.25f, SpriteEffects.None, 0f);
					if (!this.sc.usingMouse)
					{
						if (this.updownIndex == 0)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector4 + this.vec[3], new Rectangle?(this.bigboxRect), this.bluePen);
						}
						if (this.updownIndex == 1)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector4 + this.vec[4], new Rectangle?(this.bigboxRect), this.bluePen);
						}
						if (this.updownIndex == 2)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector4 + this.vec[5], new Rectangle?(this.bigboxRect), this.bluePen);
						}
					}
					string text3 = "music";
					this.spriteBatch.DrawString(this.scribbleFont, text3, this.paperPos - this.scribbleFont.MeasureString(text3) / 2f + this.vec[0], this.bluePen);
					text3 = "effects";
					this.spriteBatch.DrawString(this.scribbleFont, text3, this.paperPos - this.scribbleFont.MeasureString(text3) / 2f + this.vec[1], this.bluePen);
					text3 = "voice";
					this.spriteBatch.DrawString(this.scribbleFont, text3, this.paperPos - this.scribbleFont.MeasureString(text3) / 2f + this.vec[2], this.bluePen);
					astrobindings.b1.Length = 0;
					astrobindings.b1.Concat(this.slider1);
					this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[6], this.bluePen);
					if (this.sc.usingMouse)
					{
						this.queryButton(this.sliderBoxRect, this.paperPos + this.vec[0] + new Vector2(32f + this.sliderbox1, 4f), 0);
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[0] + new Vector2(-40f, 20f), new Rectangle?(this.sliderRect), Color.White);
						if (this.myIndex == 0)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[0] + new Vector2(32f + this.sliderbox1, 4f), new Rectangle?(this.sliderGlowRect), Color.White);
						}
						else
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[0] + new Vector2(32f + this.sliderbox1, 4f), new Rectangle?(this.sliderBoxRect), Color.White);
						}
					}
					astrobindings.b1.Length = 0;
					astrobindings.b1.Concat(this.slider2);
					this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[7], this.bluePen);
					if (this.sc.usingMouse)
					{
						this.queryButton(this.sliderBoxRect, this.paperPos + this.vec[1] + new Vector2(17f + this.sliderbox2, 4f), 1);
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[1] + new Vector2(-55f, 20f), new Rectangle?(this.sliderRect), Color.White);
						if (this.myIndex == 1)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[1] + new Vector2(17f + this.sliderbox2, 4f), new Rectangle?(this.sliderGlowRect), Color.White);
						}
						else
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[1] + new Vector2(17f + this.sliderbox2, 4f), new Rectangle?(this.sliderBoxRect), Color.White);
						}
					}
					astrobindings.b1.Length = 0;
					astrobindings.b1.Concat(this.slider3);
					this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[8], this.bluePen);
					if (this.sc.usingMouse)
					{
						this.queryButton(this.sliderBoxRect, this.paperPos + this.vec[2] + new Vector2(32f + this.sliderbox3, 4f), 2);
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[2] + new Vector2(-40f, 20f), new Rectangle?(this.sliderRect), Color.White);
						if (this.myIndex == 2)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[2] + new Vector2(32f + this.sliderbox3, 4f), new Rectangle?(this.sliderGlowRect), Color.White);
						}
						else
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[2] + new Vector2(32f + this.sliderbox3, 4f), new Rectangle?(this.sliderBoxRect), Color.White);
						}
					}
					text3 = "save";
					this.queryButton(this.buttonBoxRect, this.paperPos - vector4 + this.vec[10], 3);
					Color color2 = this.bluePen;
					if (this.myIndex == 3)
					{
						color2 = this.grnPen;
					}
					this.spriteBatch.DrawString(this.scribbleFont, text3, this.paperPos - this.scribbleFont.MeasureString(text3) / 2f + this.vec[9], color2);
					if (!this.sc.usingMouse)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector4 + this.vec[10], new Rectangle?(this.buttonARect), Color.White);
					}
					else if (this.myIndex == 3)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector4 + this.vec[10], new Rectangle?(this.buttonGlowRect), Color.White);
					}
					else
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector4 + this.vec[10], new Rectangle?(this.buttonBoxRect), Color.White);
					}
					if (num7 != this.myIndex && this.myIndex != -1)
					{
						this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
					}
				}
				if (this.choice == "video")
				{
					int num8 = this.myIndex;
					this.myIndex = -1;
					Vector2 vector5 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), Color.White, 0f, vector5, 1f, SpriteEffects.None, 0f);
					Vector2 vector6 = new Vector2(-40f, -115f);
					this.queryButton(this.sliderBoxRect, this.paperPos + vector6 + new Vector2(32f + this.sliderbox1, 4f), 0);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos + vector6 + new Vector2(-40f, 20f), new Rectangle?(this.sliderRect), Color.White);
					if (this.myIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + vector6 + new Vector2(32f + this.sliderbox1, 4f), new Rectangle?(this.sliderGlowRect), Color.White);
					}
					else
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + vector6 + new Vector2(32f + this.sliderbox1, 4f), new Rectangle?(this.sliderBoxRect), Color.White);
					}
					vector6 = new Vector2(-40f, 80f);
					this.queryButton(this.sliderBoxRect, this.paperPos + vector6 + new Vector2(32f + this.sliderbox2, 4f), 1);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos + vector6 + new Vector2(-40f, 20f), new Rectangle?(this.sliderRect), Color.White);
					if (this.myIndex == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + vector6 + new Vector2(32f + this.sliderbox2, 4f), new Rectangle?(this.sliderGlowRect), Color.White);
					}
					else
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + vector6 + new Vector2(32f + this.sliderbox2, 4f), new Rectangle?(this.sliderBoxRect), Color.White);
					}
					if (this.sc.usingMouse)
					{
						this.queryButton2(new Rectangle(0, 0, 160, 50), this.sc.hud_enemy, 5);
						this.queryButton2(new Rectangle(0, 0, 160, 50), new Vector2(this.sc.hud_clock.X + 100f, this.sc.hud_clock.Y), 6);
						this.queryButton2(new Rectangle(0, 0, 140, 50), new Vector2(this.sc.hud_day.X - 160f, this.sc.hud_day.Y), 7);
						this.queryButton2(new Rectangle(0, 0, 110, 60), this.sc.hud_weapons, 8);
						this.queryButton2(new Rectangle(0, 0, 110, 60), this.sc.hud_dpad, 9);
						this.queryButton2(new Rectangle(0, 0, 180, 60), this.sc.hud_player1, 10);
						this.queryButton2(new Rectangle(0, 0, 140, 60), this.sc.hud_player2, 11);
					}
					string text4 = "cycle";
					if (!this.sc.usingMouse)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector5 + this.vec[1], new Rectangle?(this.buttonYRect), Color.White);
					}
					else
					{
						this.queryButton(this.buttonBoxRect, this.paperPos - vector5 + this.vec[1], 2);
						if (this.myIndex == 2)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector5 + this.vec[1], new Rectangle?(this.buttonGlowRect), Color.White);
						}
						else
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector5 + this.vec[1], new Rectangle?(this.buttonBoxRect), Color.White);
						}
					}
					Color color3 = this.bluePen;
					if (this.myIndex == 2)
					{
						color3 = this.grnPen;
					}
					this.spriteBatch.DrawString(this.scribbleFont, text4, this.paperPos - this.scribbleFont.MeasureString(text4) / 2f + this.vec[0], color3);
					text4 = "defaults";
					if (!this.sc.usingMouse)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector5 + this.vec[3], new Rectangle?(this.buttonXRect), Color.White);
					}
					else
					{
						this.queryButton(this.buttonBoxRect, this.paperPos - vector5 + this.vec[3], 3);
						if (this.myIndex == 3)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector5 + this.vec[3], new Rectangle?(this.buttonGlowRect), Color.White);
						}
						else
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector5 + this.vec[3], new Rectangle?(this.buttonBoxRect), Color.White);
						}
					}
					color3 = this.bluePen;
					if (this.myIndex == 3)
					{
						color3 = this.grnPen;
					}
					this.spriteBatch.DrawString(this.scribbleFont, text4, this.paperPos - this.scribbleFont.MeasureString(text4) / 2f + this.vec[2], color3);
					text4 = "save";
					if (!this.sc.usingMouse)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector5 + this.vec[5], new Rectangle?(this.buttonARect), Color.White);
					}
					else
					{
						this.queryButton(this.buttonBoxRect, this.paperPos - vector5 + this.vec[5], 4);
						if (this.myIndex == 4)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector5 + this.vec[5], new Rectangle?(this.buttonGlowRect), Color.White);
						}
						else
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector5 + this.vec[5], new Rectangle?(this.buttonBoxRect), Color.White);
						}
					}
					color3 = this.bluePen;
					if (this.myIndex == 4)
					{
						color3 = this.grnPen;
					}
					this.spriteBatch.DrawString(this.scribbleFont, text4, this.paperPos - this.scribbleFont.MeasureString(text4) / 2f + this.vec[4], color3);
					float num9 = (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 40.0) / 2f + 0.6f;
					if (this.updownIndex == 0)
					{
						this.sc.color_enemy = new Color(210, 0, 0, 255) * num9;
					}
					else
					{
						this.sc.color_enemy = new Color(210, 0, 0, 255);
					}
					if (this.updownIndex == 2)
					{
						this.sc.color_day = new Color(210, 0, 0, 255) * num9;
					}
					else
					{
						this.sc.color_day = new Color(210, 0, 0, 255);
					}
					if (this.updownIndex == 1)
					{
						this.sc.color_clock = new Color(255, 255, 255, 255) * num9;
					}
					else
					{
						this.sc.color_clock = new Color(255, 255, 255, 255);
					}
					if (this.updownIndex == 5)
					{
						this.sc.color_player1 = new Color(255, 255, 255, 255) * num9;
					}
					else
					{
						this.sc.color_player1 = new Color(255, 255, 255, 255);
					}
					if (this.updownIndex == 6)
					{
						this.sc.color_player2 = new Color(255, 255, 255, 255) * num9;
					}
					else
					{
						this.sc.color_player2 = new Color(255, 255, 255, 255);
					}
					if (this.updownIndex == 3)
					{
						this.sc.color_weapons = new Color(255, 255, 255, 255) * num9;
					}
					else
					{
						this.sc.color_weapons = new Color(255, 255, 255, 255);
					}
					if (this.updownIndex == 4)
					{
						this.sc.color_dpad = new Color(255, 255, 255, 255) * num9;
					}
					else
					{
						this.sc.color_dpad = new Color(255, 255, 255, 255);
					}
					if (num8 != this.myIndex && this.myIndex != -1)
					{
						this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
					}
				}
				if (this.choice == "game")
				{
					int num10 = this.myIndex;
					this.myIndex = -1;
					this.paperPos.X = this.paperPos.X + 30f;
					Vector2 vector7 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), Color.White, this.rot, vector7, 1.5f, SpriteEffects.None, 0f);
					if (!this.sc.usingMouse)
					{
						if (this.updownIndex == 0)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[12] - new Vector2(0f, 30f), new Rectangle?(this.biggerboxRect), this.bluePen);
						}
						if (this.updownIndex == 1)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[13] - new Vector2(0f, 35f), new Rectangle?(this.biggerboxRect), this.bluePen);
						}
						if (this.updownIndex == 2)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[14] - new Vector2(10f, 45f), new Rectangle?(this.biggerboxRect), this.bluePen);
						}
						if (this.updownIndex == 3)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + new Vector2(46f, 126f), new Rectangle?(this.biggerboxRect), this.bluePen);
						}
						if (this.updownIndex == 4)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[15] + new Vector2(0f, 10f), new Rectangle?(this.biggerboxRect), this.bluePen);
						}
					}
					Vector2 vector8 = new Vector2(0f, -30f);
					string text5 = "invert-Y";
					this.spriteBatch.DrawString(this.scribbleFont, text5, this.paperPos - this.scribbleFont.MeasureString(text5) / 2f + this.vec[0] + vector8, this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[1] + vector8, new Rectangle?(this.littleboxRect), Color.White);
					this.queryButton(this.littleboxRect, this.paperPos - vector7 + this.vec[1] + vector8, 0);
					if (this.myIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[1] + vector8, new Rectangle?(this.littleglowRect), Color.White);
					}
					if (this.slider1 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[2] + vector8, new Rectangle?(this.checkmarkRect), Color.White);
					}
					vector8 = new Vector2(0f, -35f);
					text5 = "sensitivity";
					this.spriteBatch.DrawString(this.scribbleFont, text5, this.paperPos - this.scribbleFont.MeasureString(text5) / 2f + this.vec[3] + vector8, this.bluePen);
					astrobindings.b1.Length = 0;
					astrobindings.b1.Concat(this.slider2);
					this.queryButtonX(new Rectangle(0, 0, (int)this.scribbleFont.MeasureString(astrobindings.b1).X, (int)this.scribbleFont.MeasureString(astrobindings.b1).Y), this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[4] + vector8, 1);
					Color color4 = this.bluePen;
					if (this.myIndex == 1)
					{
						color4 = this.grnPen;
					}
					this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[4] + vector8, color4);
					vector8 = new Vector2(-10f, -45f);
					text5 = "vibration";
					this.spriteBatch.DrawString(this.scribbleFont, text5, this.paperPos - this.scribbleFont.MeasureString(text5) / 2f + this.vec[5] + vector8, this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[6] + vector8, new Rectangle?(this.littleboxRect), Color.White);
					this.queryButton(this.littleboxRect, this.paperPos - vector7 + this.vec[6] + vector8, 2);
					if (this.myIndex == 2)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[6] + vector8, new Rectangle?(this.littleglowRect), Color.White);
					}
					if (this.slider3 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[7] + vector8, new Rectangle?(this.checkmarkRect), Color.White);
					}
					text5 = "sprint-toggle";
					this.spriteBatch.DrawString(this.scribbleFont, text5, this.paperPos - this.scribbleFont.MeasureString(text5) / 2f + new Vector2(-6.4f, 28.8f), this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + new Vector2(218f, 125.5f), new Rectangle?(this.littleboxRect), Color.White);
					this.queryButton(this.littleboxRect, this.paperPos - vector7 + new Vector2(218f, 125.5f), 3);
					if (this.myIndex == 3)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + new Vector2(218f, 125.5f), new Rectangle?(this.littleglowRect), Color.White);
					}
					if (this.slider5 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + new Vector2(229f, 117.2f), new Rectangle?(this.checkmarkRect), Color.White);
					}
					vector8 = new Vector2(0f, 10f);
					text5 = "difficulty";
					this.spriteBatch.DrawString(this.scribbleFont, text5, this.paperPos - this.scribbleFont.MeasureString(text5) / 2f + this.vec[8] + vector8, this.bluePen);
					astrobindings.b1.Length = 0;
					astrobindings.b1.Append(this.diff[this.slider4]);
					this.queryButtonX(new Rectangle(0, 0, (int)this.scribbleFont.MeasureString(astrobindings.b1).X, (int)this.scribbleFont.MeasureString(astrobindings.b1).Y), this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[9] + vector8, 4);
					color4 = this.bluePen;
					if (this.myIndex == 4)
					{
						color4 = this.grnPen;
					}
					this.spriteBatch.DrawString(this.scribbleFont, astrobindings.b1, this.paperPos - this.scribbleFont.MeasureString(astrobindings.b1) / 2f + this.vec[9] + vector8, color4);
					vector8 = new Vector2(0f, 20f);
					text5 = "save";
					this.queryButton(this.buttonBoxRect, this.paperPos - vector7 + this.vec[11] + vector8, 5);
					color4 = this.bluePen;
					if (this.myIndex == 5)
					{
						color4 = this.grnPen;
					}
					this.spriteBatch.DrawString(this.scribbleFont, text5, this.paperPos - this.scribbleFont.MeasureString(text5) / 2f + this.vec[10] + vector8, color4);
					if (!this.sc.usingMouse)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[11] + vector8, new Rectangle?(this.buttonARect), Color.White);
					}
					else if (this.myIndex == 5)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[11] + vector8, new Rectangle?(this.buttonGlowRect), Color.White);
					}
					else
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector7 + this.vec[11] + vector8, new Rectangle?(this.buttonBoxRect), Color.White);
					}
					if (num10 != this.myIndex && this.myIndex != -1)
					{
						this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
					}
				}
				if (this.choice == "help")
				{
					int num11 = this.myIndex;
					this.myIndex = -1;
					Vector2 vector9 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), Color.White, this.rot, vector9, 1f, SpriteEffects.None, 0f);
					if (!this.sc.usingMouse)
					{
						if (this.updownIndex == 0)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector9 + this.vec[0], new Rectangle?(this.bigboxRect), this.bluePen);
						}
						if (this.updownIndex == 1)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector9 + this.vec[3], new Rectangle?(this.bigboxRect), this.bluePen);
						}
						string text6 = "Instructions";
						this.spriteBatch.DrawString(this.scribbleFont, text6, this.paperPos - this.scribbleFont.MeasureString(text6) / 2f + this.vec[4], this.bluePen);
						text6 = "Controller";
						this.spriteBatch.DrawString(this.scribbleFont, text6, this.paperPos - this.scribbleFont.MeasureString(text6) / 2f + this.vec[6], this.bluePen);
					}
					else
					{
						string text7 = "Key Bindings";
						this.spriteBatch.DrawString(this.scribbleFont, text7, this.paperPos - this.scribbleFont.MeasureString(text7) / 2f + this.vec[4], this.bluePen);
						text7 = "Game Hotkeys";
						this.spriteBatch.DrawString(this.scribbleFont, text7, this.paperPos - this.scribbleFont.MeasureString(text7) / 2f + this.vec[6], this.bluePen);
						this.queryButton(this.bigboxRect, this.paperPos - vector9 + this.vec[0], 0);
						this.queryButton(this.bigboxRect, this.paperPos - vector9 + this.vec[3], 1);
						if (this.myIndex == 0)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector9 + this.vec[0], new Rectangle?(this.bigboxRect), this.bluePen);
						}
						if (this.myIndex == 1)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector9 + this.vec[3], new Rectangle?(this.bigboxRect), this.bluePen);
						}
					}
					if (num11 != this.myIndex && this.myIndex != -1)
					{
						this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
					}
				}
				if (this.choice == "rebind")
				{
					this.sc.drawKeyBindings3(this.spriteBatch, ref this.whichKEY, ref this.thisKEY, this.sc.font);
				}
				if (this.choice == "hotkeys")
				{
					Vector2 vector10 = new Vector2((float)(640 - this.sc.instructions.Width / 2), (float)(370 - this.sc.instructions.Height / 2));
					this.spriteBatch.Draw(this.sc.instructions, vector10, Color.White);
				}
				if (this.choice == "controller")
				{
					this.scalerZoom += 0.1f;
					if (this.scalerZoom > 1.05f)
					{
						this.scalerZoom = 1.05f;
					}
					this.paperPos = new Vector2(680f, 360f);
					Vector2 vector11 = new Vector2((float)this.sc.controller.Width, (float)this.sc.controller.Height) / 2f;
					this.spriteBatch.Draw(this.sc.controller, this.paperPos, null, Color.White, this.rot, vector11, this.scaler, SpriteEffects.None, 0f);
				}
				if (this.choice == "instructions")
				{
					this.scalerZoom += 0.1f;
					if (this.scalerZoom > 1f)
					{
						this.scalerZoom = 1f;
					}
					this.paperPos = new Vector2(570f, 305f);
					Vector2 vector12 = new Vector2((float)this.sc.page1.Width, (float)this.sc.page1.Height) / 2f;
					if (this.updownIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.page1, this.paperPos, null, Color.White, this.rot, vector12, this.scaler, SpriteEffects.None, 0f);
					}
					if (this.updownIndex == 1)
					{
						this.spriteBatch.Draw(this.sc.page2, this.paperPos, null, Color.White, this.rot, vector12, this.scaler, SpriteEffects.None, 0f);
					}
					if (this.updownIndex == 2)
					{
						this.spriteBatch.Draw(this.sc.page3, this.paperPos, null, Color.White, this.rot, vector12, this.scaler, SpriteEffects.None, 0f);
					}
					if (this.updownIndex == 3)
					{
						this.spriteBatch.Draw(this.sc.page4, this.paperPos, null, Color.White, this.rot, vector12, this.scaler, SpriteEffects.None, 0f);
					}
					if (this.updownIndex == 4)
					{
						this.spriteBatch.Draw(this.sc.page5, this.paperPos, null, Color.White, this.rot, vector12, this.scaler, SpriteEffects.None, 0f);
					}
					if (this.updownIndex == 5)
					{
						this.spriteBatch.Draw(this.sc.page6, this.paperPos, null, Color.White, this.rot, vector12, this.scaler, SpriteEffects.None, 0f);
					}
					if (this.updownIndex == 6)
					{
						this.spriteBatch.Draw(this.sc.page7, this.paperPos, null, Color.White, this.rot, vector12, this.scaler, SpriteEffects.None, 0f);
					}
				}
				if (this.choice == "cheats")
				{
					int num12 = this.myIndex;
					this.myIndex = -1;
					Vector2 vector13 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), Color.White, 0f, vector13, 1.1f, SpriteEffects.None, 0f);
					if (!this.sc.usingMouse)
					{
						if (this.updownIndex == 0)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector13 + this.vec[3] + new Vector2(-35f, 0f), new Rectangle?(this.biggerboxRect), this.bluePen);
						}
						if (this.updownIndex == 1)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector13 + this.vec[4] + new Vector2(-75f, 0f), new Rectangle?(this.biggerboxRect), this.bluePen);
						}
						if (this.updownIndex == 2)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector13 + this.vec[5] + new Vector2(-35f, 0f), new Rectangle?(this.biggerboxRect), this.bluePen);
						}
						if (this.updownIndex == 3)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector13 + this.vec[31] + new Vector2(-35f, 0f), new Rectangle?(this.biggerboxRect), this.bluePen);
						}
					}
					string text8 = "super ammo";
					this.spriteBatch.DrawString(this.scribbleFont, text8, this.paperPos - this.scribbleFont.MeasureString(text8) / 2f + this.vec[0], this.bluePen);
					text8 = "invincible";
					this.spriteBatch.DrawString(this.scribbleFont, text8, this.paperPos - this.scribbleFont.MeasureString(text8) / 2f + this.vec[1], this.bluePen);
					text8 = "all weapons";
					this.spriteBatch.DrawString(this.scribbleFont, text8, this.paperPos - this.scribbleFont.MeasureString(text8) / 2f + this.vec[2], this.bluePen);
					text8 = "unlock days";
					this.spriteBatch.DrawString(this.scribbleFont, text8, this.paperPos - this.scribbleFont.MeasureString(text8) / 2f + this.vec[30], this.bluePen);
					astrobindings.b1.Length = 0;
					astrobindings.b1.Concat(this.slider1);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[6] + new Vector2(0f, -28f), new Rectangle?(this.littleboxRect), Color.White);
					this.queryButton(this.littleboxRect, this.paperPos + this.vec[6] + new Vector2(0f, -28f), 0);
					if (this.myIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[6] + new Vector2(0f, -28f), new Rectangle?(this.littleglowRect), Color.White);
					}
					if (this.slider1 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[6] + new Vector2(7f, -35f), new Rectangle?(this.checkmarkRect), Color.White);
					}
					astrobindings.b1.Length = 0;
					astrobindings.b1.Concat(this.slider2);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[7] + new Vector2(-40f, -28f), new Rectangle?(this.littleboxRect), Color.White);
					this.queryButton(this.littleboxRect, this.paperPos + this.vec[7] + new Vector2(-40f, -28f), 1);
					if (this.myIndex == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[7] + new Vector2(-40f, -28f), new Rectangle?(this.littleglowRect), Color.White);
					}
					if (this.slider2 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[7] + new Vector2(-33f, -35f), new Rectangle?(this.checkmarkRect), Color.White);
					}
					astrobindings.b1.Length = 0;
					astrobindings.b1.Concat(this.slider3);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[8] + new Vector2(0f, -28f), new Rectangle?(this.littleboxRect), Color.White);
					this.queryButton(this.littleboxRect, this.paperPos + this.vec[8] + new Vector2(0f, -28f), 2);
					if (this.myIndex == 2)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[8] + new Vector2(0f, -28f), new Rectangle?(this.littleglowRect), Color.White);
					}
					if (this.slider3 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[8] + new Vector2(7f, -35f), new Rectangle?(this.checkmarkRect), Color.White);
					}
					astrobindings.b1.Length = 0;
					astrobindings.b1.Concat(this.slider4);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[32] + new Vector2(0f, -28f), new Rectangle?(this.littleboxRect), Color.White);
					this.queryButton(this.littleboxRect, this.paperPos + this.vec[32] + new Vector2(0f, -28f), 3);
					if (this.myIndex == 3)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[32] + new Vector2(0f, -28f), new Rectangle?(this.littleglowRect), Color.White);
					}
					if (this.slider4 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos + this.vec[32] + new Vector2(7f, -35f), new Rectangle?(this.checkmarkRect), Color.White);
					}
					if (num12 != this.myIndex && this.myIndex != -1)
					{
						this.sc.tick.Play(this.sc.ev, 0.1f, 0f);
					}
				}
				if (this.choice == "hidden")
				{
					Vector2 vector14 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), new Color(255, 200, 200), this.rot, vector14, 1.1f, SpriteEffects.None, 0f);
					if (this.updownIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector14 + this.vec[0], new Rectangle?(this.bigboxRect), this.bluePen);
					}
					if (this.updownIndex == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector14 + this.vec[1], new Rectangle?(this.bigboxRect), this.bluePen);
					}
					if (this.updownIndex == 2)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector14 + this.vec[2], new Rectangle?(this.bigboxRect), this.bluePen);
					}
					if (this.updownIndex == 3)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector14 + this.vec[3], new Rectangle?(this.bigboxRect), this.bluePen);
					}
					string text9 = "networking";
					this.spriteBatch.DrawString(this.scribbleFont, text9, this.paperPos - this.scribbleFont.MeasureString(text9) / 2f + this.vec[4], this.bluePen);
					text9 = "debugging";
					this.spriteBatch.DrawString(this.scribbleFont, text9, this.paperPos - this.scribbleFont.MeasureString(text9) / 2f + this.vec[5], this.bluePen);
					text9 = "level edit";
					this.spriteBatch.DrawString(this.scribbleFont, text9, this.paperPos - this.scribbleFont.MeasureString(text9) / 2f + this.vec[7], this.bluePen);
					text9 = "cheat mode";
					this.spriteBatch.DrawString(this.scribbleFont, text9, this.paperPos - this.scribbleFont.MeasureString(text9) / 2f + this.vec[6], this.bluePen);
				}
				if (this.choice == "networking")
				{
					Vector2 vector15 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), new Color(255, 200, 200), this.rot, vector15, 1.25f, SpriteEffects.None, 0f);
					if (this.updownIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector15 + this.vec[0], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector15 + this.vec[1], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 2)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector15 + this.vec[2], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					string text10 = "show data";
					this.spriteBatch.DrawString(this.scribbleFont, text10, this.paperPos - this.scribbleFont.MeasureString(text10) / 2f + this.vec[3], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector15 + this.vec[4], new Rectangle?(this.littleboxRect), Color.White);
					if (this.slider1 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector15 + this.vec[5], new Rectangle?(this.checkmarkRect), Color.White);
					}
					text10 = "pack delay";
					this.spriteBatch.DrawString(this.scribbleFont, text10, this.paperPos - this.scribbleFont.MeasureString(text10) / 2f + this.vec[6], this.bluePen);
					text10 = this.sc.lag[this.slider2].ToString();
					this.spriteBatch.DrawString(this.scribbleFont, text10, this.paperPos - this.scribbleFont.MeasureString(text10) / 2f + this.vec[7], this.bluePen);
					text10 = "pack loss";
					this.spriteBatch.DrawString(this.scribbleFont, text10, this.paperPos - this.scribbleFont.MeasureString(text10) / 2f + this.vec[8], this.bluePen);
					text10 = ((int)(this.sc.loss[this.slider2] * 100f)).ToString() + "%";
					this.spriteBatch.DrawString(this.scribbleFont, text10, this.paperPos - this.scribbleFont.MeasureString(text10) / 2f + this.vec[9], this.bluePen);
					text10 = "okay";
					this.spriteBatch.DrawString(this.scribbleFont, text10, this.paperPos - this.scribbleFont.MeasureString(text10) / 2f + this.vec[10], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector15 + this.vec[11], new Rectangle?(this.buttonARect), Color.White);
				}
				if (this.choice == "debugging")
				{
					Vector2 vector16 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), new Color(255, 200, 200), this.rot, vector16, 1.25f, SpriteEffects.None, 0f);
					if (this.updownIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[0], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[1], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 2)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[2], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 3)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[3], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					string text11 = "Garbage";
					this.spriteBatch.DrawString(this.scribbleFont, text11, this.paperPos - this.scribbleFont.MeasureString(text11) / 2f + this.vec[4], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[5], new Rectangle?(this.littleboxRect), Color.White);
					if (this.slider1 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[6], new Rectangle?(this.checkmarkRect), Color.White);
					}
					text11 = "Seed #";
					this.spriteBatch.DrawString(this.scribbleFont, text11, this.paperPos - this.scribbleFont.MeasureString(text11) / 2f + this.vec[7], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[8], new Rectangle?(this.littleboxRect), Color.White);
					if (this.slider2 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[9], new Rectangle?(this.checkmarkRect), Color.White);
					}
					text11 = "Accuracy";
					this.spriteBatch.DrawString(this.scribbleFont, text11, this.paperPos - this.scribbleFont.MeasureString(text11) / 2f + this.vec[10], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[11], new Rectangle?(this.littleboxRect), Color.White);
					if (this.slider3 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[12], new Rectangle?(this.checkmarkRect), Color.White);
					}
					text11 = "Homing";
					this.spriteBatch.DrawString(this.scribbleFont, text11, this.paperPos - this.scribbleFont.MeasureString(text11) / 2f + this.vec[13], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[14], new Rectangle?(this.littleboxRect), Color.White);
					if (this.slider4 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[15], new Rectangle?(this.checkmarkRect), Color.White);
					}
					text11 = "okay";
					this.spriteBatch.DrawString(this.scribbleFont, text11, this.paperPos - this.scribbleFont.MeasureString(text11) / 2f + this.vec[16], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector16 + this.vec[17], new Rectangle?(this.buttonARect), Color.White);
				}
				if (this.choice == "cheat mode")
				{
					this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, astrobindings.clipit);
					Vector2 vector17 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					if (this.updownIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[0], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[1], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 2)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[2], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 3)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[3], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 4)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[4], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					string text12 = "Invincible";
					this.spriteBatch.DrawString(this.scribbleFont, text12, this.paperPos - this.scribbleFont.MeasureString(text12) / 2f + this.vec[5], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[6], new Rectangle?(this.littleboxRect), Color.White);
					if (this.slider1 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[7], new Rectangle?(this.checkmarkRect), Color.White);
					}
					text12 = "fastfiring";
					this.spriteBatch.DrawString(this.scribbleFont, text12, this.paperPos - this.scribbleFont.MeasureString(text12) / 2f + this.vec[8], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[9], new Rectangle?(this.littleboxRect), Color.White);
					if (this.slider2 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[10], new Rectangle?(this.checkmarkRect), Color.White);
					}
					text12 = "superAmmo";
					this.spriteBatch.DrawString(this.scribbleFont, text12, this.paperPos - this.scribbleFont.MeasureString(text12) / 2f + this.vec[11], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[12], new Rectangle?(this.littleboxRect), Color.White);
					if (this.slider3 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[13], new Rectangle?(this.checkmarkRect), Color.White);
					}
					text12 = "allExplode";
					this.spriteBatch.DrawString(this.scribbleFont, text12, this.paperPos - this.scribbleFont.MeasureString(text12) / 2f + this.vec[14], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[15], new Rectangle?(this.littleboxRect), Color.White);
					if (this.slider4 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[16], new Rectangle?(this.checkmarkRect), Color.White);
					}
					text12 = "pickupPack";
					this.spriteBatch.DrawString(this.scribbleFont, text12, this.paperPos - this.scribbleFont.MeasureString(text12) / 2f + this.vec[17], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[18], new Rectangle?(this.littleboxRect), Color.White);
					if (this.slider5 == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[19], new Rectangle?(this.checkmarkRect), Color.White);
					}
					text12 = "Send";
					this.spriteBatch.DrawString(this.scribbleFont, text12, this.paperPos - this.scribbleFont.MeasureString(text12) / 2f + this.vec[20], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector17 + this.vec[21], new Rectangle?(this.buttonARect), Color.White);
				}
				if (this.choice == "level edit")
				{
					Vector2 vector18 = new Vector2((float)this.narrowRect.Width, (float)this.narrowRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos + new Vector2(0f, 20f), new Rectangle?(this.narrowRect), new Color(255, 200, 200), this.rot, vector18, 2.4f, SpriteEffects.None, 0f);
					Color color5 = this.bluePen;
					Color color6 = new Color(255, 40, 40, 255);
					if (this.updownIndex <= 10)
					{
						if (this.updownIndex == 0)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[0], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 1)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[1], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 2)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[2], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 3)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[3], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 4)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[4], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 5)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[5], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 6)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[6], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 7)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[7], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 8)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[8], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 9)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[9], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 10)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[10], new Rectangle?(this.longboxRect), this.bluePen);
						}
						color5 = this.bluePen;
						if (this.updownIndex == 0)
						{
							color5 = color6;
						}
						string text13 = "Day or Night";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[11], color5);
						string[] array = new string[] { "am", "pm" };
						text13 = array[this.slider1];
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[12], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 1)
						{
							color5 = color6;
						}
						text13 = "SpawnArea";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[13], color5);
						string[] array2 = new string[]
						{
							"center", "borders", "grinder", "barn", "silos", "corner", "powerbox", "northLine", "southLine", "WestLine",
							"EastLine", "P1.1", "P2.1", "P4.1", "P4.2", "P1.3", "P4.3", "P2.4", "none", "none",
							"centBigX", "centTinyX", "borderX"
						};
						int num13 = this.slider2;
						if (num13 > array2.Length - 1)
						{
							text13 = "xxx";
						}
						else
						{
							text13 = array2[this.slider2];
						}
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[14], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 2)
						{
							color5 = color6;
						}
						text13 = "Boar Count";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[15], color5);
						text13 = this.slider3.ToString() + " x";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[16], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 3)
						{
							color5 = color6;
						}
						text13 = "Boar Health";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[17], color5);
						text13 = this.slider4.ToString() + "%";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[18], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 4)
						{
							color5 = color6;
						}
						text13 = "Boar Attack";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[19], color5);
						text13 = this.slider5.ToString() + "%";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[20], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 5)
						{
							color5 = color6;
						}
						text13 = "Boar MinSize";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[21], color5);
						text13 = this.slider6.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[22], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 6)
						{
							color5 = color6;
						}
						text13 = "Boar MaxSize";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[23], color5);
						text13 = this.slider7.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[24], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 7)
						{
							color5 = color6;
						}
						text13 = "Giant Odds";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[25], color5);
						text13 = this.slider8.ToString() + "%";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[26], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 8)
						{
							color5 = color6;
						}
						text13 = "Tiny Odds";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[27], color5);
						text13 = this.slider9.ToString() + "%";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[28], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 9)
						{
							color5 = color6;
						}
						text13 = "AttackSpeed";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[29], color5);
						text13 = this.slider10.ToString() + "%";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[30], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 10)
						{
							color5 = color6;
						}
						text13 = "TurningRate";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[31], color5);
						text13 = this.slider11.ToString() + "%";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[32], color5);
						text13 = "Next Page";
						this.spriteBatch.DrawString(this.scribbleFont, text13, this.paperPos - this.scribbleFont.MeasureString(text13) / 2f + this.vec[33], this.bluePen);
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[34], new Rectangle?(this.buttonARect), Color.White);
					}
					else
					{
						if (this.updownIndex == 11)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[35], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 12)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[36], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 13)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[37], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 14)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[38], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 15)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[39], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 16)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[40], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 17)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[41], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 18)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[42], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 19)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[43], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 20)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[44], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 21)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[45], new Rectangle?(this.longboxRect), this.bluePen);
						}
						else if (this.updownIndex == 22)
						{
							this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[46], new Rectangle?(this.longboxRect), this.bluePen);
						}
						color5 = this.bluePen;
						if (this.updownIndex == 11)
						{
							color5 = color6;
						}
						string text14 = "90% Radius";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[47], color5);
						text14 = this.slider12.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[48], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 12)
						{
							color5 = color6;
						}
						text14 = "90% Homing";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[49], color5);
						text14 = this.slider13.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[50], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 13)
						{
							color5 = color6;
						}
						text14 = "75% Radius";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[51], color5);
						text14 = this.slider14.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[52], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 14)
						{
							color5 = color6;
						}
						text14 = "75% Homing";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[53], color5);
						text14 = this.slider15.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[54], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 15)
						{
							color5 = color6;
						}
						text14 = "50% Radius";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[55], color5);
						text14 = this.slider16.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[56], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 16)
						{
							color5 = color6;
						}
						text14 = "50% Homing";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[57], color5);
						text14 = this.slider17.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[58], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 17)
						{
							color5 = color6;
						}
						text14 = "30% Radius";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[59], color5);
						text14 = this.slider18.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[60], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 18)
						{
							color5 = color6;
						}
						text14 = "30% Homing";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[61], color5);
						text14 = this.slider19.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[62], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 19)
						{
							color5 = color6;
						}
						text14 = "20% Radius";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[63], color5);
						text14 = this.slider20.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[64], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 20)
						{
							color5 = color6;
						}
						text14 = "20% Homing";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[65], color5);
						text14 = this.slider21.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[66], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 21)
						{
							color5 = color6;
						}
						text14 = "00% Radius";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[67], color5);
						text14 = this.slider22.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[68], color5);
						color5 = this.bluePen;
						if (this.updownIndex == 22)
						{
							color5 = color6;
						}
						text14 = "00% Homing";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[69], color5);
						text14 = this.slider23.ToString();
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[70], color5);
						text14 = "Prev";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[71], this.bluePen);
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[72], new Rectangle?(this.buttonBRect), Color.White);
						text14 = "Save";
						this.spriteBatch.DrawString(this.scribbleFont, text14, this.paperPos - this.scribbleFont.MeasureString(text14) / 2f + this.vec[73], this.bluePen);
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector18 + this.vec[74], new Rectangle?(this.buttonARect), Color.White);
					}
				}
				if (this.choice == "moon")
				{
					Vector2 vector19 = new Vector2((float)this.myRect.Width, (float)this.myRect.Height) / 2f;
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos, new Rectangle?(this.myRect), new Color(255, 200, 200), this.rot, vector19, 1.25f, SpriteEffects.None, 0f);
					if (this.updownIndex == 0)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector19 + this.vec[0], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					if (this.updownIndex == 1)
					{
						this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector19 + this.vec[1], new Rectangle?(this.biggerboxRect), this.bluePen);
					}
					string text15 = "Darkness ";
					this.spriteBatch.DrawString(this.scribbleFont, text15, this.paperPos - this.scribbleFont.MeasureString(text15) / 2f + this.vec[2], this.bluePen);
					text15 = this.slider1.ToString();
					this.spriteBatch.DrawString(this.scribbleFont, text15, this.paperPos - this.scribbleFont.MeasureString(text15) / 2f + this.vec[3], this.bluePen);
					string[] array3 = new string[] { "northDark", "northGlow", "northBrite", "southDark", "southGlow", "southBrite", "westDark", "westGlow", "westBrite" };
					text15 = array3[this.slider2].ToString();
					this.spriteBatch.DrawString(this.scribbleFont, text15, this.paperPos - this.scribbleFont.MeasureString(text15) / 2f + this.vec[5], this.bluePen);
					text15 = "okay";
					this.spriteBatch.DrawString(this.scribbleFont, text15, this.paperPos - this.scribbleFont.MeasureString(text15) / 2f + this.vec[6], this.bluePen);
					this.spriteBatch.Draw(this.sc.paper1, this.paperPos - vector19 + this.vec[7], new Rectangle?(this.buttonARect), Color.White);
				}
				this.spriteBatch.End();
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00111240 File Offset: 0x0010F440
		public void queryButton(Rectangle rr, Vector2 vv, int ch)
		{
			rr = new Rectangle((int)vv.X, (int)vv.Y, rr.Width, rr.Height);
			rr = new Rectangle(rr.X + 5, rr.Y + 5, rr.Width - 10, rr.Height - 10);
			Vector2 vector = this.mm;
			if (this.sc.aspectratio <= 1f)
			{
				vector.Y -= ((float)this.sc.screenSize.Height - (float)this.sc.screenSize.Height * this.sc.aspectratio) * (0.5f / this.sc.aspectratio);
			}
			else
			{
				vector.X -= ((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio);
			}
			Vector2 vector2 = this.scc * new Vector2((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height);
			vector /= vector2;
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				this.myIndex = ch;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0011141C File Offset: 0x0010F61C
		public void queryButtonX(Rectangle rr, Vector2 vv, int ch)
		{
			rr = new Rectangle((int)vv.X, (int)vv.Y, rr.Width, rr.Height);
			Vector2 vector = this.mm;
			if (this.sc.aspectratio <= 1f)
			{
				vector.Y -= ((float)this.sc.screenSize.Height - (float)this.sc.screenSize.Height * this.sc.aspectratio) * (0.5f / this.sc.aspectratio);
			}
			else
			{
				vector.X -= ((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio);
			}
			Vector2 vector2 = this.scc * new Vector2((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height);
			vector /= vector2;
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				this.myIndex = ch;
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x001115CC File Offset: 0x0010F7CC
		public void queryButton2(Rectangle rr, Vector2 vv, int ch)
		{
			rr = new Rectangle((int)vv.X, (int)vv.Y, rr.Width, rr.Height);
			Vector2 vector = this.mm;
			if (this.sc.aspectratio <= 1f)
			{
				vector.Y -= ((float)this.sc.screenSize.Height - (float)this.sc.screenSize.Height * this.sc.aspectratio) * (0.5f / this.sc.aspectratio);
			}
			else
			{
				vector.X -= ((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio);
			}
			Vector2 vector2 = 1f * new Vector2((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height);
			vector /= vector2;
			bool flag = vector.Y > (float)rr.Y && vector.Y <= (float)(rr.Y + rr.Height) && vector.X > (float)rr.X && vector.X <= (float)(rr.X + rr.Width);
			if (flag)
			{
				this.myIndex = ch;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0011177C File Offset: 0x0010F97C
		public Vector2 adjustMouse()
		{
			Vector2 vector = this.mm;
			if (this.sc.aspectratio <= 1f)
			{
				vector.Y -= ((float)this.sc.screenSize.Height - (float)this.sc.screenSize.Height * this.sc.aspectratio) * (0.5f / this.sc.aspectratio);
			}
			else
			{
				vector.X -= ((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio);
			}
			Vector2 vector2 = this.scc * new Vector2((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height);
			vector /= vector2;
			return vector;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x001118A4 File Offset: 0x0010FAA4
		public Vector2 adjustMouse2()
		{
			Vector2 vector = this.mm;
			if (this.sc.aspectratio <= 1f)
			{
				vector.Y -= ((float)this.sc.screenSize.Height - (float)this.sc.screenSize.Height * this.sc.aspectratio) * (0.5f / this.sc.aspectratio);
			}
			else
			{
				vector.X -= ((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio);
			}
			Vector2 vector2 = 1f * new Vector2((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height);
			vector /= vector2;
			return vector;
		}

		// Token: 0x040011CD RID: 4557
		public static RasterizerState clipit = new RasterizerState
		{
			ScissorTestEnable = true
		};

		// Token: 0x040011CE RID: 4558
		private bool isHost;

		// Token: 0x040011CF RID: 4559
		private bool exclude;

		// Token: 0x040011D0 RID: 4560
		private int myIndex = -1;

		// Token: 0x040011D1 RID: 4561
		private int thisKEY;

		// Token: 0x040011D2 RID: 4562
		private int whichKEY;

		// Token: 0x040011D3 RID: 4563
		private float origMouseX;

		// Token: 0x040011D4 RID: 4564
		private Vector2 origMouseXY;

		// Token: 0x040011D5 RID: 4565
		private Vector2 origPlaque;

		// Token: 0x040011D6 RID: 4566
		private float nowMouseX;

		// Token: 0x040011D7 RID: 4567
		private float sliderbox1;

		// Token: 0x040011D8 RID: 4568
		private float sliderbox1a;

		// Token: 0x040011D9 RID: 4569
		private float sliderbox2;

		// Token: 0x040011DA RID: 4570
		private float sliderbox2a;

		// Token: 0x040011DB RID: 4571
		private float sliderbox3;

		// Token: 0x040011DC RID: 4572
		private float sliderbox3a;

		// Token: 0x040011DD RID: 4573
		private float sliderbox4;

		// Token: 0x040011DE RID: 4574
		private float sliderbox4a;

		// Token: 0x040011DF RID: 4575
		private float scc = 1.2f;

		// Token: 0x040011E0 RID: 4576
		private Vector2 paperPos;

		// Token: 0x040011E1 RID: 4577
		private float drift = 10f;

		// Token: 0x040011E2 RID: 4578
		private float scaler = 1f;

		// Token: 0x040011E3 RID: 4579
		private float scalerZoom;

		// Token: 0x040011E4 RID: 4580
		private SoundEffect drip;

		// Token: 0x040011E5 RID: 4581
		private SoundEffect jot;

		// Token: 0x040011E6 RID: 4582
		private SoundEffect scribble;

		// Token: 0x040011E7 RID: 4583
		private SoundEffect cashout;

		// Token: 0x040011E8 RID: 4584
		private SpriteBatch spriteBatch;

		// Token: 0x040011E9 RID: 4585
		private SpriteFont scribbleFont;

		// Token: 0x040011EA RID: 4586
		private SpriteFont scribbleFont2;

		// Token: 0x040011EB RID: 4587
		private Rectangle paper1Rect = new Rectangle(0, 0, 286, 255);

		// Token: 0x040011EC RID: 4588
		private Rectangle paper2Rect = new Rectangle(286, 0, 286, 255);

		// Token: 0x040011ED RID: 4589
		private Rectangle paper3Rect = new Rectangle(0, 269, 286, 255);

		// Token: 0x040011EE RID: 4590
		private Rectangle paper4Rect = new Rectangle(286, 269, 286, 255);

		// Token: 0x040011EF RID: 4591
		private Rectangle paper5Rect = new Rectangle(23, 658, 321, 337);

		// Token: 0x040011F0 RID: 4592
		private Rectangle paper6Rect = new Rectangle(365, 658, 321, 337);

		// Token: 0x040011F1 RID: 4593
		private Rectangle narrowRect = new Rectangle(49, 0, 181, 254);

		// Token: 0x040011F2 RID: 4594
		private Rectangle leftArrowRect = new Rectangle(68, 546, 48, 46);

		// Token: 0x040011F3 RID: 4595
		private Rectangle leftArrowGlowRect = new Rectangle(68, 595, 48, 46);

		// Token: 0x040011F4 RID: 4596
		private Rectangle rightArrowRect = new Rectangle(171, 546, 48, 46);

		// Token: 0x040011F5 RID: 4597
		private Rectangle rightArrowGlowRect = new Rectangle(171, 595, 48, 46);

		// Token: 0x040011F6 RID: 4598
		private Rectangle buttonBoxRect = new Rectangle(627, 221, 45, 45);

		// Token: 0x040011F7 RID: 4599
		private Rectangle buttonGlowRect = new Rectangle(740, 221, 45, 45);

		// Token: 0x040011F8 RID: 4600
		private Rectangle buttonARect = new Rectangle(626, 139, 48, 48);

		// Token: 0x040011F9 RID: 4601
		private Rectangle buttonBRect = new Rectangle(737, 137, 48, 48);

		// Token: 0x040011FA RID: 4602
		private Rectangle buttonXRect = new Rectangle(737, 64, 48, 48);

		// Token: 0x040011FB RID: 4603
		private Rectangle buttonYRect = new Rectangle(626, 66, 48, 48);

		// Token: 0x040011FC RID: 4604
		private Rectangle littleboxRect = new Rectangle(626, 302, 60, 57);

		// Token: 0x040011FD RID: 4605
		private Rectangle littleglowRect = new Rectangle(567, 302, 60, 57);

		// Token: 0x040011FE RID: 4606
		private Rectangle bigboxRect = new Rectangle(576, 384, 211, 68);

		// Token: 0x040011FF RID: 4607
		private Rectangle biggerboxRect = new Rectangle(580, 456, 240, 62);

		// Token: 0x04001200 RID: 4608
		private Rectangle longboxRect = new Rectangle(255, 549, 377, 41);

		// Token: 0x04001201 RID: 4609
		private Rectangle checkmarkRect = new Rectangle(707, 300, 65, 58);

		// Token: 0x04001202 RID: 4610
		private Rectangle sliderBoxRect = new Rectangle(702, 548, 33, 42);

		// Token: 0x04001203 RID: 4611
		private Rectangle sliderGlowRect = new Rectangle(751, 548, 33, 42);

		// Token: 0x04001204 RID: 4612
		private Rectangle sliderRect = new Rectangle(631, 613, 176, 8);

		// Token: 0x04001205 RID: 4613
		private float[] musicVal = new float[]
		{
			0f, 10f, 19f, 31f, 52f, 87f, 145f, 240f, 360f, 600f,
			1000f
		};

		// Token: 0x04001206 RID: 4614
		private Vector2[] vec;

		// Token: 0x04001207 RID: 4615
		private string choice = "";

		// Token: 0x04001208 RID: 4616
		private float rampIN;

		// Token: 0x04001209 RID: 4617
		private bool hideMenu;

		// Token: 0x0400120A RID: 4618
		private Random rr;

		// Token: 0x0400120B RID: 4619
		private GamePadState gamestate;

		// Token: 0x0400120C RID: 4620
		private GamePadState prevstate;

		// Token: 0x0400120D RID: 4621
		private KeyboardState keyState;

		// Token: 0x0400120E RID: 4622
		private KeyboardState prevKey;

		// Token: 0x0400120F RID: 4623
		private MouseState prevMouse;

		// Token: 0x04001210 RID: 4624
		private MouseState mouseState;

		// Token: 0x04001211 RID: 4625
		private Vector2 mm = Vector2.Zero;

		// Token: 0x04001212 RID: 4626
		private bool delayinput;

		// Token: 0x04001213 RID: 4627
		private ScreenManager sc;

		// Token: 0x04001218 RID: 4632
		private Vector2 offset;

		// Token: 0x04001219 RID: 4633
		private int vecIndex;

		// Token: 0x0400121A RID: 4634
		private float rot;

		// Token: 0x0400121B RID: 4635
		private Color bluePen;

		// Token: 0x0400121C RID: 4636
		private Color blackPen;

		// Token: 0x0400121D RID: 4637
		private Color grnPen;

		// Token: 0x0400121E RID: 4638
		private int updownIndex;

		// Token: 0x0400121F RID: 4639
		private int slider1;

		// Token: 0x04001220 RID: 4640
		private int slider2;

		// Token: 0x04001221 RID: 4641
		private int slider3;

		// Token: 0x04001222 RID: 4642
		private int slider4;

		// Token: 0x04001223 RID: 4643
		private int slider5;

		// Token: 0x04001224 RID: 4644
		private int slider6;

		// Token: 0x04001225 RID: 4645
		private int slider7;

		// Token: 0x04001226 RID: 4646
		private int slider8;

		// Token: 0x04001227 RID: 4647
		private int slider9;

		// Token: 0x04001228 RID: 4648
		private int slider10;

		// Token: 0x04001229 RID: 4649
		private int slider11;

		// Token: 0x0400122A RID: 4650
		private int slider12;

		// Token: 0x0400122B RID: 4651
		private int slider13;

		// Token: 0x0400122C RID: 4652
		private int slider14;

		// Token: 0x0400122D RID: 4653
		private int slider15;

		// Token: 0x0400122E RID: 4654
		private int slider16;

		// Token: 0x0400122F RID: 4655
		private int slider17;

		// Token: 0x04001230 RID: 4656
		private int slider18;

		// Token: 0x04001231 RID: 4657
		private int slider19;

		// Token: 0x04001232 RID: 4658
		private int slider20;

		// Token: 0x04001233 RID: 4659
		private int slider21;

		// Token: 0x04001234 RID: 4660
		private int slider22;

		// Token: 0x04001235 RID: 4661
		private int slider23;

		// Token: 0x04001236 RID: 4662
		private int slider24;

		// Token: 0x04001237 RID: 4663
		private int slider25;

		// Token: 0x04001238 RID: 4664
		private Rectangle myRect;

		// Token: 0x04001239 RID: 4665
		private static StringBuilder b1 = new StringBuilder(32, 32);

		// Token: 0x0400123A RID: 4666
		private string[] diff = new string[] { "easy", "norm", "hard" };

		// Token: 0x0400123B RID: 4667
		private string[] dayDescribe = new string[]
		{
			"wildebeasts", "wildeboars", "grinder", "shockem", "night attack", "revenge day", "wildeboars", "mammoths", "pigrats", "needy greedy",
			"chargiboars", "brutiboars", "wilde  boars", "skelaboars", "pigrats", "mammoths", "revenge day", "night death", "chargiboars", "princess fight",
			"blue boars", "brutiboars", "pigadillos", "pigrats", "night attack", "revenge day", "mammoths", "skelaboars", "pigadillos", "the brethren",
			"hell hounds", "the stampede", "armadillo herds", "frutiboars", "revenge day", "mammoths", "hell hounds", "skelaboars", "armadillos", "boss fight",
			"the beast", "the innocents", "mammoths", "skelaboars", "revenge day", "brutiboars", "pigadillos", "chargiboars", "death combo", "mini boss",
			"mammoths", "stampede", "dusk til death", "pigadillos", "brute boars", "revenge day", "oh fatsnap", "mammoths", "death march", "big boss fight",
			"stampedes", "the herds", "pigrats", "mammoths", "pigadillos", "revenge day", "brute boars", "night mammoths", "brute boars", "TroikaBoss",
			"skelaboars", "pigadillos", "mammoths", "dusk stomp ", "revenge day", "boar hounds", "pigrats", "bulkaboars", "wilde boars", "boss fight",
			"boar hounds", "pigrats", "skelaboars", "mammoth hounds", "revenge day", "pigrats", "ambushed", "bulkaboars", "until dusk", "mini boss",
			"stampede", "pray for day", "mammoths", "wilde boars", "revenge day", "bulkaboars", "chargiboars", "death herds", "bulkaboars", "you're fucked",
			"happiness", "mammoths", "chargiboars", "night", "pigadillos", "brutiboars", "revenge day", "oh shit", "mammoths", "pigrats",
			"big boss fight", "stampedes", "the herds", "pigrats", "mammoths", "pigadillos", "revenge day", "brutiboars", "night mammoths", "brutiboars",
			"3 Mini Boss", "skelaboars", "pigadillos", "mammoths", "dusk stomp ", "revenge day", "boar hounds", "pigrats", "bulkaboars", "wilde boars",
			"boss fight", "boar hounds", "pigrats", "skelaboars", "mammoth hounds", "revenge day", "pigrats", "pigadillos", "chargiboars", "oh shit",
			"mini boss", "oh shit", "night fuck", "mammoths", "wilde boars", "revenge day", "bulkaboars", "chargiboars", "death herds", "bulkaboars",
			"you're fucked", "happiness"
		};

		// Token: 0x0400123C RID: 4668
		private PlayerIndex playerIndex;
	}
}
