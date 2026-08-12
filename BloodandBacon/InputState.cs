using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x0200015E RID: 350
	public class InputState
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x00375D54 File Offset: 0x00373F54
		public InputState()
		{
			this.CurrentGamePadStates = new GamePadState[4];
			this.LastGamePadStates = new GamePadState[4];
			this.GamePadWasConnected = new bool[4];
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00375D80 File Offset: 0x00373F80
		public void Update()
		{
			for (int i = 0; i < 4; i++)
			{
				this.LastGamePadStates[i] = this.CurrentGamePadStates[i];
				this.CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);
				if (this.CurrentGamePadStates[i].IsConnected)
				{
					this.GamePadWasConnected[i] = true;
				}
			}
			this.lastKeyState = this.currentKeyState;
			this.currentKeyState = Keyboard.GetState();
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00375E08 File Offset: 0x00374008
		public Vector2 getLeftStick(PlayerIndex? controllingPlayer)
		{
			if (controllingPlayer != null)
			{
				int value = (int)controllingPlayer.Value;
				return this.CurrentGamePadStates[value].ThumbSticks.Left;
			}
			if (this.CurrentGamePadStates[0].ThumbSticks.Left != Vector2.Zero)
			{
				return this.CurrentGamePadStates[0].ThumbSticks.Left;
			}
			if (this.CurrentGamePadStates[1].ThumbSticks.Left != Vector2.Zero)
			{
				return this.CurrentGamePadStates[1].ThumbSticks.Left;
			}
			if (this.CurrentGamePadStates[2].ThumbSticks.Left != Vector2.Zero)
			{
				return this.CurrentGamePadStates[2].ThumbSticks.Left;
			}
			if (this.CurrentGamePadStates[3].ThumbSticks.Left != Vector2.Zero)
			{
				return this.CurrentGamePadStates[3].ThumbSticks.Left;
			}
			return Vector2.Zero;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00375F48 File Offset: 0x00374148
		public bool IsMenuGame(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00375F60 File Offset: 0x00374160
		public float Trigger(string button, PlayerIndex? controllingPlayer, out float xxx)
		{
			if (controllingPlayer != null)
			{
				PlayerIndex value = controllingPlayer.Value;
				xxx = 0f;
				int num = (int)value;
				if (button == "right")
				{
					xxx = this.CurrentGamePadStates[num].Triggers.Right;
				}
				if (button == "left")
				{
					xxx = this.CurrentGamePadStates[num].Triggers.Left;
				}
				return xxx;
			}
			xxx = this.Trigger(button, new PlayerIndex?(PlayerIndex.One), out xxx) + this.Trigger(button, new PlayerIndex?(PlayerIndex.Two), out xxx) + this.Trigger(button, new PlayerIndex?(PlayerIndex.Three), out xxx) + this.Trigger(button, new PlayerIndex?(PlayerIndex.Four), out xxx);
			return xxx;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00376019 File Offset: 0x00374219
		public float IsTrigger(string val, PlayerIndex? controllingPlayer, out float xxx)
		{
			return this.Trigger(val, controllingPlayer, out xxx);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00376024 File Offset: 0x00374224
		public float Stick(string button, PlayerIndex? controllingPlayer, out float xxx)
		{
			if (controllingPlayer != null)
			{
				PlayerIndex value = controllingPlayer.Value;
				xxx = 0f;
				int num = (int)value;
				if (button == "updown")
				{
					xxx = this.CurrentGamePadStates[num].ThumbSticks.Right.Y;
				}
				if (button == "leftright")
				{
					xxx = this.CurrentGamePadStates[num].ThumbSticks.Right.X;
				}
				return xxx;
			}
			xxx = this.Stick(button, new PlayerIndex?(PlayerIndex.One), out xxx) + this.Stick(button, new PlayerIndex?(PlayerIndex.Two), out xxx) + this.Stick(button, new PlayerIndex?(PlayerIndex.Three), out xxx) + this.Stick(button, new PlayerIndex?(PlayerIndex.Four), out xxx);
			return xxx;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x003760E7 File Offset: 0x003742E7
		public float IsStick(string val, PlayerIndex? controllingPlayer, out float xxx)
		{
			return this.Stick(val, controllingPlayer, out xxx);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x003760F4 File Offset: 0x003742F4
		public bool Bumper(string button, PlayerIndex? controllingPlayer, out bool yyy)
		{
			if (controllingPlayer == null)
			{
				return this.Bumper(button, new PlayerIndex?(PlayerIndex.One), out yyy) || this.Bumper(button, new PlayerIndex?(PlayerIndex.Two), out yyy) || this.Bumper(button, new PlayerIndex?(PlayerIndex.Three), out yyy) || this.Bumper(button, new PlayerIndex?(PlayerIndex.Four), out yyy);
			}
			PlayerIndex value = controllingPlayer.Value;
			yyy = false;
			int num = (int)value;
			if (button == "left")
			{
				return this.CurrentGamePadStates[num].IsButtonDown(Buttons.LeftShoulder);
			}
			if (button == "right")
			{
				return this.CurrentGamePadStates[num].IsButtonDown(Buttons.RightShoulder);
			}
			return yyy;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x003761A2 File Offset: 0x003743A2
		public bool IsBumper(string val, PlayerIndex? controllingPlayer, out bool yyy)
		{
			return this.Bumper(val, controllingPlayer, out yyy);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x003761B0 File Offset: 0x003743B0
		public bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
		{
			if (controllingPlayer != null)
			{
				playerIndex = controllingPlayer.Value;
				int num = (int)playerIndex;
				return this.CurrentGamePadStates[num].IsButtonDown(button) && this.LastGamePadStates[num].IsButtonUp(button);
			}
			return this.IsNewButtonPress(button, new PlayerIndex?(PlayerIndex.One), out playerIndex) || this.IsNewButtonPress(button, new PlayerIndex?(PlayerIndex.Two), out playerIndex) || this.IsNewButtonPress(button, new PlayerIndex?(PlayerIndex.Three), out playerIndex) || this.IsNewButtonPress(button, new PlayerIndex?(PlayerIndex.Four), out playerIndex);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0037623C File Offset: 0x0037443C
		public bool IsMenuSelect(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
		{
			return this.IsNewButtonPress(Buttons.A, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Enter) && this.lastKeyState.IsKeyUp(Keys.Enter)) || (this.currentKeyState.IsKeyDown(Keys.A) && this.lastKeyState.IsKeyUp(Keys.A));
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00376298 File Offset: 0x00374498
		public bool IsMenuCancel(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
		{
			return this.IsNewButtonPress(Buttons.B, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Escape) && this.lastKeyState.IsKeyUp(Keys.Escape)) || (this.currentKeyState.IsKeyDown(Keys.Back) && this.lastKeyState.IsKeyUp(Keys.Back)) || (this.currentKeyState.IsKeyDown(Keys.Delete) && this.lastKeyState.IsKeyUp(Keys.Delete)) || (this.currentKeyState.IsKeyDown(Keys.B) && this.lastKeyState.IsKeyUp(Keys.B));
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0037633C File Offset: 0x0037453C
		public bool IsMenuUp(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.RightThumbstickUp, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Up) && this.lastKeyState.IsKeyUp(Keys.Up));
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00376398 File Offset: 0x00374598
		public bool IsMenuDown(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.RightThumbstickDown, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Down) && this.lastKeyState.IsKeyUp(Keys.Down));
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x003763F4 File Offset: 0x003745F4
		public bool IsMenuUpX(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Up) && this.lastKeyState.IsKeyUp(Keys.Up));
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00376440 File Offset: 0x00374640
		public bool IsMenuDownX(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Down) && this.lastKeyState.IsKeyUp(Keys.Down));
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0037648C File Offset: 0x0037468C
		public bool IsMenuRight(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.DPadRight, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.LeftThumbstickRight, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.RightThumbstickRight, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Right) && this.lastKeyState.IsKeyUp(Keys.Right));
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x003764E8 File Offset: 0x003746E8
		public bool IsMenuLeft(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.DPadLeft, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.LeftThumbstickLeft, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.RightThumbstickLeft, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Left) && this.lastKeyState.IsKeyUp(Keys.Left));
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00376544 File Offset: 0x00374744
		public bool IsMenuRightExclusive(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.LeftThumbstickRight, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.RightThumbstickRight, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Right) && this.lastKeyState.IsKeyUp(Keys.Right));
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00376594 File Offset: 0x00374794
		public bool IsMenuLeftExclusive(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.LeftThumbstickLeft, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.RightThumbstickLeft, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Left) && this.lastKeyState.IsKeyUp(Keys.Left));
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x003765E4 File Offset: 0x003747E4
		public bool IsMenuRightX(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.DPadRight, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.LeftThumbstickRight, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Right) && this.lastKeyState.IsKeyUp(Keys.Right));
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00376630 File Offset: 0x00374830
		public bool IsMenuLeftX(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.DPadLeft, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.LeftThumbstickLeft, controllingPlayer, out playerIndex) || (this.currentKeyState.IsKeyDown(Keys.Left) && this.lastKeyState.IsKeyUp(Keys.Left));
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0037667C File Offset: 0x0037487C
		public bool IsPauseGame(PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			return this.IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex);
		}

		// Token: 0x04003429 RID: 13353
		public const int MaxInputs = 4;

		// Token: 0x0400342A RID: 13354
		public readonly GamePadState[] CurrentGamePadStates;

		// Token: 0x0400342B RID: 13355
		public readonly GamePadState[] LastGamePadStates;

		// Token: 0x0400342C RID: 13356
		public readonly bool[] GamePadWasConnected;

		// Token: 0x0400342D RID: 13357
		public KeyboardState currentKeyState;

		// Token: 0x0400342E RID: 13358
		public KeyboardState lastKeyState;
	}
}
