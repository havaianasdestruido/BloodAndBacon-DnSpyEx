using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000DC RID: 220
	internal class OptionsScreen2 : MenuScreen
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x001C5040 File Offset: 0x001C3240
		public OptionsScreen2()
			: base("Game Options2")
		{
			MenuEntry menuEntry = new MenuEntry("AUDIO");
			MenuEntry menuEntry2 = new MenuEntry("VIDEO");
			menuEntry.Selected += this.audioMenuEntrySelected;
			menuEntry2.Selected += this.videoMenuEntrySelected;
			base.MenuEntries.Add(menuEntry);
			base.MenuEntries.Add(menuEntry2);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x001C50AA File Offset: 0x001C32AA
		private void notesSelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.menuflag = 1;
			base.ScreenManager.AddScreen(new Notes(), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x001C50D3 File Offset: 0x001C32D3
		private void audioMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			base.ScreenManager.AddScreen(new audiosettingScreen(), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x001C50FC File Offset: 0x001C32FC
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

		// Token: 0x060007A0 RID: 1952 RVA: 0x001C5159 File Offset: 0x001C3359
		private void gameX(object sender, PlayerIndexEventArgs e)
		{
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x001C515B File Offset: 0x001C335B
		private void savePrefsEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.SavePrefs();
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x001C5168 File Offset: 0x001C3368
		private void loadPrefsEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.LoadPrefs();
		}
	}
}
