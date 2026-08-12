using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200004D RID: 77
	internal class ExtrasScreen : MenuScreen
	{
		// Token: 0x0600031B RID: 795 RVA: 0x000CEC98 File Offset: 0x000CCE98
		public ExtrasScreen()
			: base("Extras")
		{
			MenuEntry menuEntry = new MenuEntry("Play Intro");
			MenuEntry menuEntry2 = new MenuEntry("Play MiniGame");
			MenuEntry menuEntry3 = new MenuEntry("MorseCode");
			MenuEntry menuEntry4 = new MenuEntry("STORAGE");
			menuEntry.Selected += this.PlayEntrySelected;
			menuEntry2.Selected += this.miniEntrySelected;
			menuEntry3.Selected += this.morseEntrySelected;
			menuEntry4.Selected += this.storageEntrySelected;
			base.MenuEntries.Add(menuEntry);
			base.MenuEntries.Add(menuEntry2);
			base.MenuEntries.Add(menuEntry3);
			base.MenuEntries.Add(menuEntry4);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000CED54 File Offset: 0x000CCF54
		private void PlayEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.SavePrefs();
			LoadingScreen2.Load(base.ScreenManager, false, null, new GameScreen[]
			{
				new titles()
			});
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000CED91 File Offset: 0x000CCF91
		private void miniEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.menuflag = 1;
			base.ScreenManager.AddScreen(new MiniGame(), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000CEDBA File Offset: 0x000CCFBA
		private void morseEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.menuflag = 1;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000CEDC8 File Offset: 0x000CCFC8
		private void storageEntrySelected(object sender, PlayerIndexEventArgs e)
		{
		}
	}
}
