using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200000B RID: 11
	public abstract class GameScreen
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00027F36 File Offset: 0x00026136
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00027F3E File Offset: 0x0002613E
		public bool IsPopup
		{
			get
			{
				return this.isPopup;
			}
			protected set
			{
				this.isPopup = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00027F47 File Offset: 0x00026147
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00027F4F File Offset: 0x0002614F
		public bool IsHandled
		{
			get
			{
				return this.isHandled;
			}
			protected set
			{
				this.isHandled = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00027F58 File Offset: 0x00026158
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00027F60 File Offset: 0x00026160
		public TimeSpan TransitionOnTime
		{
			get
			{
				return this.transitionOnTime;
			}
			protected set
			{
				this.transitionOnTime = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00027F69 File Offset: 0x00026169
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00027F71 File Offset: 0x00026171
		public TimeSpan TransitionOffTime
		{
			get
			{
				return this.transitionOffTime;
			}
			protected set
			{
				this.transitionOffTime = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00027F7A File Offset: 0x0002617A
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00027F82 File Offset: 0x00026182
		public float TransitionPosition
		{
			get
			{
				return this.transitionPosition;
			}
			protected set
			{
				this.transitionPosition = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00027F8B File Offset: 0x0002618B
		public byte TransitionAlpha
		{
			get
			{
				return (byte)(255f - this.TransitionPosition * 255f);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00027FA0 File Offset: 0x000261A0
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00027FA8 File Offset: 0x000261A8
		public ScreenState ScreenState
		{
			get
			{
				return this.screenState;
			}
			protected set
			{
				this.screenState = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00027FB1 File Offset: 0x000261B1
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00027FB9 File Offset: 0x000261B9
		public bool IsExiting
		{
			get
			{
				return this.isExiting;
			}
			protected internal set
			{
				this.isExiting = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00027FC2 File Offset: 0x000261C2
		public bool IsActive
		{
			get
			{
				return !this.otherScreenHasFocus && (this.screenState == ScreenState.TransitionOn || this.screenState == ScreenState.Active);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00027FE1 File Offset: 0x000261E1
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00027FE9 File Offset: 0x000261E9
		public ScreenManager ScreenManager
		{
			get
			{
				return this.screenManager;
			}
			internal set
			{
				this.screenManager = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00027FF2 File Offset: 0x000261F2
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00027FFA File Offset: 0x000261FA
		public PlayerIndex? ControllingPlayer
		{
			get
			{
				return this.controllingPlayer;
			}
			internal set
			{
				this.controllingPlayer = value;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00028003 File Offset: 0x00026203
		public virtual void LoadContent()
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00028005 File Offset: 0x00026205
		public virtual void UnloadContent()
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00028008 File Offset: 0x00026208
		public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			this.otherScreenHasFocus = otherScreenHasFocus;
			if (this.isExiting)
			{
				this.screenState = ScreenState.TransitionOff;
				if (!this.UpdateTransition(gameTime, this.transitionOffTime, 1))
				{
					this.ScreenManager.RemoveScreen(this);
					return;
				}
			}
			else if (coveredByOtherScreen)
			{
				if (this.UpdateTransition(gameTime, this.transitionOffTime, 1))
				{
					this.screenState = ScreenState.TransitionOff;
					return;
				}
				this.screenState = ScreenState.Hidden;
				return;
			}
			else
			{
				if (this.UpdateTransition(gameTime, this.transitionOnTime, -1))
				{
					this.screenState = ScreenState.TransitionOn;
					return;
				}
				this.screenState = ScreenState.Active;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0002808C File Offset: 0x0002628C
		private bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
		{
			float num;
			if (time == TimeSpan.Zero)
			{
				num = 1f;
			}
			else
			{
				num = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);
			}
			this.transitionPosition += num * (float)direction;
			if ((direction < 0 && this.transitionPosition <= 0f) || (direction > 0 && this.transitionPosition >= 1f))
			{
				this.transitionPosition = MathHelper.Clamp(this.transitionPosition, 0f, 1f);
				return false;
			}
			return true;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00028117 File Offset: 0x00026317
		public virtual void HandleInput(InputState input)
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00028119 File Offset: 0x00026319
		public virtual void Draw(GameTime gameTime)
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0002811B File Offset: 0x0002631B
		public void ExitScreen()
		{
			if (this.TransitionOffTime == TimeSpan.Zero)
			{
				this.ScreenManager.RemoveScreen(this);
				return;
			}
			this.isExiting = true;
		}

		// Token: 0x04000566 RID: 1382
		public bool isSigner;

		// Token: 0x04000567 RID: 1383
		private bool isPopup;

		// Token: 0x04000568 RID: 1384
		private bool isHandled = true;

		// Token: 0x04000569 RID: 1385
		public bool drawlast;

		// Token: 0x0400056A RID: 1386
		private TimeSpan transitionOnTime = TimeSpan.Zero;

		// Token: 0x0400056B RID: 1387
		private TimeSpan transitionOffTime = TimeSpan.Zero;

		// Token: 0x0400056C RID: 1388
		private float transitionPosition = 1f;

		// Token: 0x0400056D RID: 1389
		private ScreenState screenState;

		// Token: 0x0400056E RID: 1390
		private bool isExiting;

		// Token: 0x0400056F RID: 1391
		private bool otherScreenHasFocus;

		// Token: 0x04000570 RID: 1392
		private ScreenManager screenManager;

		// Token: 0x04000571 RID: 1393
		private PlayerIndex? controllingPlayer;
	}
}
