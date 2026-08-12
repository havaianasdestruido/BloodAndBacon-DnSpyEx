using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000DB RID: 219
	internal class OptionsScreen : MenuScreen
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x001C4E64 File Offset: 0x001C3064
		public OptionsScreen()
			: base("Game Options")
		{
			MenuEntry menuEntry = new MenuEntry("AUDIO");
			MenuEntry menuEntry2 = new MenuEntry("VIDEO");
			MenuEntry menuEntry3 = new MenuEntry("CAMERA");
			menuEntry.Selected += this.audioMenuEntrySelected;
			menuEntry2.Selected += this.videoMenuEntrySelected;
			menuEntry3.Selected += this.camSelected;
			base.MenuEntries.Add(menuEntry);
			base.MenuEntries.Add(menuEntry2);
			base.MenuEntries.Add(menuEntry3);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x001C4EF7 File Offset: 0x001C30F7
		private void notesSelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.menuflag = 1;
			base.ScreenManager.AddScreen(new Notes(), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x001C4F20 File Offset: 0x001C3120
		private void audioMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			base.ScreenManager.AddScreen(new audiosettingScreen(), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x001C4F4C File Offset: 0x001C314C
		private void videoMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			if (base.ScreenManager.menutype == 0)
			{
				base.ScreenManager.AddScreen(new videosettingsScreen(), new PlayerIndex?(e.PlayerIndex));
				return;
			}
			base.ScreenManager.AddScreen(new videosettingsScreen(), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x001C4FA9 File Offset: 0x001C31A9
		private void gameX(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x001C4FB7 File Offset: 0x001C31B7
		private void savePrefsEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.SavePrefs();
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x001C4FC4 File Offset: 0x001C31C4
		private void loadPrefsEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.LoadPrefs();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x001C4FD4 File Offset: 0x001C31D4
		private void camSelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			if (base.ScreenManager.vehicleindex == 1)
			{
				base.ScreenManager.AddScreen(new CamerasScreen(), new PlayerIndex?(e.PlayerIndex));
			}
			if (base.ScreenManager.vehicleindex == 2)
			{
				base.ScreenManager.AddScreen(new CamerasScreen2(), new PlayerIndex?(e.PlayerIndex));
			}
		}
	}
}
