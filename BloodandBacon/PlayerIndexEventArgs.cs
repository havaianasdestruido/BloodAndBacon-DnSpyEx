using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000161 RID: 353
	internal class PlayerIndexEventArgs : EventArgs
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x003768E4 File Offset: 0x00374AE4
		public PlayerIndexEventArgs(PlayerIndex playerIndex)
		{
			this.playerIndex = playerIndex;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x003768F3 File Offset: 0x00374AF3
		public PlayerIndex PlayerIndex
		{
			get
			{
				return this.playerIndex;
			}
		}

		// Token: 0x0400342F RID: 13359
		private PlayerIndex playerIndex;
	}
}
