using System;

namespace Blood
{
	// Token: 0x0200005A RID: 90
	internal class audiosettingScreen : MenuScreen
	{
		// Token: 0x06000367 RID: 871 RVA: 0x000D53C4 File Offset: 0x000D35C4
		public audiosettingScreen()
			: base("Audio Settings")
		{
			this.sfxMenuEntry = new MenuEntry(string.Empty);
			this.musicMenuEntry = new MenuEntry(string.Empty);
			this.voiceMenuEntry = new MenuEntry(string.Empty);
			this.SetMenuEntryText();
			this.sfxMenuEntry.Selected += this.sfxMenuEntrySelected;
			this.musicMenuEntry.Selected += this.musicMenuEntrySelected;
			this.voiceMenuEntry.Selected += this.voiceMenuEntrySelected;
			base.MenuEntries.Add(this.sfxMenuEntry);
			base.MenuEntries.Add(this.musicMenuEntry);
			base.MenuEntries.Add(this.voiceMenuEntry);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000D548C File Offset: 0x000D368C
		private void SetMenuEntryText()
		{
			this.sfxMenuEntry.Text = "SOUND";
			this.musicMenuEntry.Text = "MUSIC";
			this.voiceMenuEntry.Text = "RADIO";
			this.sfxMenuEntry.Type = 1;
			this.musicMenuEntry.Type = 1;
			this.voiceMenuEntry.Type = 1;
			this.sfxMenuEntry.Lists = new string[]
			{
				"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
				"10"
			};
			this.musicMenuEntry.Lists = new string[]
			{
				"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
				"10"
			};
			this.voiceMenuEntry.Lists = new string[]
			{
				"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
				"10"
			};
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000D5637 File Offset: 0x000D3837
		private void sfxMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.ev = this.sfxMenuEntry.Amount * this.sfxMenuEntry.Amount / 100f;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000D5662 File Offset: 0x000D3862
		private void musicMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.mv = this.musicMenuEntry.Amount * this.musicMenuEntry.Amount / 100f;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000D568D File Offset: 0x000D388D
		private void voiceMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.voiceVolume = this.voiceMenuEntry.Amount * this.voiceMenuEntry.Amount / 100f;
		}

		// Token: 0x04000E6F RID: 3695
		private MenuEntry sfxMenuEntry;

		// Token: 0x04000E70 RID: 3696
		private MenuEntry musicMenuEntry;

		// Token: 0x04000E71 RID: 3697
		private MenuEntry voiceMenuEntry;
	}
}
