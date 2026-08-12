using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000182 RID: 386
	internal class CamerasScreen : MenuScreen
	{
		// Token: 0x06000E4F RID: 3663 RVA: 0x00403880 File Offset: 0x00401A80
		public CamerasScreen()
			: base("Camera Settings")
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

		// Token: 0x06000E50 RID: 3664 RVA: 0x00403913 File Offset: 0x00401B13
		private void cam1Selected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			base.ScreenManager.AddScreen(new setCamera(1), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0040393D File Offset: 0x00401B3D
		private void cam2Selected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			base.ScreenManager.AddScreen(new setCamera(2), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00403967 File Offset: 0x00401B67
		private void cam3Selected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			base.ScreenManager.AddScreen(new setCamera(3), new PlayerIndex?(e.PlayerIndex));
		}
	}
}
