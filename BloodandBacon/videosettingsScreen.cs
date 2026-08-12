using System;

namespace Blood
{
	// Token: 0x02000130 RID: 304
	internal class videosettingsScreen : MenuScreen
	{
		// Token: 0x06000B05 RID: 2821 RVA: 0x002DE83C File Offset: 0x002DCA3C
		public videosettingsScreen()
			: base("Video Settings")
		{
			this.brightness = new MenuEntry(string.Empty);
			this.contrast = new MenuEntry(string.Empty);
			this.SetMenuEntryText();
			this.brightness.Selected += this.setBrite;
			this.contrast.Selected += this.setCon;
			base.MenuEntries.Add(this.brightness);
			base.MenuEntries.Add(this.contrast);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x002DE8CC File Offset: 0x002DCACC
		private void SetMenuEntryText()
		{
			this.brightness.Text = "BRIGHTEN";
			this.contrast.Text = "CONTRAST";
			this.brightness.Type = 1;
			this.contrast.Type = 1;
			this.brightness.Lists = new string[]
			{
				"00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
				"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
				"20"
			};
			this.contrast.Lists = new string[]
			{
				"00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
				"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
				"20"
			};
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x002DEAA1 File Offset: 0x002DCCA1
		private void setBrite(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.brightness = (int)(28f + this.brightness.Amount * 10f);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x002DEAC6 File Offset: 0x002DCCC6
		private void setCon(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.contrast = (int)(28f + this.contrast.Amount * 10f);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x002DEAEB File Offset: 0x002DCCEB
		private void setBorder(object sender, PlayerIndexEventArgs e)
		{
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x002DEAED File Offset: 0x002DCCED
		private void intensitySelected(object sender, PlayerIndexEventArgs e)
		{
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x002DEAEF File Offset: 0x002DCCEF
		private void filterSelected(object sender, PlayerIndexEventArgs e)
		{
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x002DEAF1 File Offset: 0x002DCCF1
		private void bgfadeSelected(object sender, PlayerIndexEventArgs e)
		{
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x002DEAF3 File Offset: 0x002DCCF3
		private void planetSelected(object sender, PlayerIndexEventArgs e)
		{
		}

		// Token: 0x04002D62 RID: 11618
		private MenuEntry brightness;

		// Token: 0x04002D63 RID: 11619
		private MenuEntry contrast;
	}
}
