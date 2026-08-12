using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200015A RID: 346
	internal class CamerasScreen2 : MenuScreen
	{
		// Token: 0x06000CA5 RID: 3237 RVA: 0x003752E0 File Offset: 0x003734E0
		public CamerasScreen2()
			: base("Camera Settings2")
		{
			MenuEntry menuEntry = new MenuEntry("CAM 01");
			MenuEntry menuEntry2 = new MenuEntry("CAM 02");
			MenuEntry menuEntry3 = new MenuEntry("CAM 03");
			menuEntry.Selected += this.cam1Selected;
			menuEntry2.Selected += this.cam2Selected;
			menuEntry3.Selected += this.cam3Selected;
			base.MenuEntries.Add(menuEntry);
			base.MenuEntries.Add(menuEntry2);
			base.MenuEntries.Add(menuEntry3);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00375373 File Offset: 0x00373573
		private void cam1Selected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			base.ScreenManager.AddScreen(new setCamera2(1), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0037539D File Offset: 0x0037359D
		private void cam2Selected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			base.ScreenManager.AddScreen(new setCamera2(2), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x003753C7 File Offset: 0x003735C7
		private void cam3Selected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			base.ScreenManager.AddScreen(new setCamera2(3), new PlayerIndex?(e.PlayerIndex));
		}
	}
}
