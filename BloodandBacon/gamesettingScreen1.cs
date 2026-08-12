using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200001C RID: 28
	internal class gamesettingScreen1 : MenuScreen
	{
		// Token: 0x0600014E RID: 334 RVA: 0x0002FA10 File Offset: 0x0002DC10
		public gamesettingScreen1()
			: base("Game Settings1")
		{
			this.vibro = new MenuEntry(string.Empty);
			this.invertxMenuEntry = new MenuEntry(string.Empty);
			this.invertyMenuEntry = new MenuEntry(string.Empty);
			this.xsenseMenuEntry = new MenuEntry(string.Empty);
			this.ysenseMenuEntry = new MenuEntry(string.Empty);
			this.SetMenuEntryText();
			this.invertxMenuEntry.Selected += this.invertxSelected;
			this.invertyMenuEntry.Selected += this.invertySelected;
			this.xsenseMenuEntry.Selected += this.xsenseSelected;
			this.ysenseMenuEntry.Selected += this.ysenseSelected;
			base.MenuEntries.Add(this.vibro);
			base.MenuEntries.Add(this.invertxMenuEntry);
			base.MenuEntries.Add(this.invertyMenuEntry);
			base.MenuEntries.Add(this.xsenseMenuEntry);
			base.MenuEntries.Add(this.ysenseMenuEntry);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0002FB30 File Offset: 0x0002DD30
		private void SetMenuEntryText()
		{
			this.invertxMenuEntry.Text = "INVERT X";
			this.invertyMenuEntry.Text = "INVERT Y";
			this.xsenseMenuEntry.Text = "TURN SPEED";
			this.ysenseMenuEntry.Text = "TILT SPEED";
			this.invertxMenuEntry.Type = 1;
			this.invertyMenuEntry.Type = 1;
			this.xsenseMenuEntry.Type = 1;
			this.ysenseMenuEntry.Type = 1;
			this.invertxMenuEntry.Lists = new string[] { "NO", "YES" };
			this.invertyMenuEntry.Lists = new string[] { "NO", "YES" };
			this.xsenseMenuEntry.Lists = new string[]
			{
				"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
				"10"
			};
			this.ysenseMenuEntry.Lists = new string[]
			{
				"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
				"10"
			};
			this.vibro.Text = "ROVER";
			this.vibro.Type = 1;
			this.vibro.Lists = new string[] { "", "", "" };
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0002FD1C File Offset: 0x0002DF1C
		private void invertxSelected(object sender, PlayerIndexEventArgs e)
		{
			int num = 1;
			if ((int)this.invertxMenuEntry.Amount == 0)
			{
				num = 1;
			}
			if ((int)this.invertxMenuEntry.Amount == 1)
			{
				num = -1;
			}
			base.ScreenManager.space_rinvertX = num;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0002FD58 File Offset: 0x0002DF58
		private void invertySelected(object sender, PlayerIndexEventArgs e)
		{
			int num = 1;
			if ((int)this.invertyMenuEntry.Amount == 0)
			{
				num = 1;
			}
			if ((int)this.invertyMenuEntry.Amount == 1)
			{
				num = -1;
			}
			base.ScreenManager.space_rinvertY = num;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0002FD94 File Offset: 0x0002DF94
		private void xsenseSelected(object sender, PlayerIndexEventArgs e)
		{
			float num = MathHelper.Lerp(0.2f, 2f, this.xsenseMenuEntry.Amount / 10f);
			base.ScreenManager.space_rsentivityX = num;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0002FDD0 File Offset: 0x0002DFD0
		private void ysenseSelected(object sender, PlayerIndexEventArgs e)
		{
			float num = MathHelper.Lerp(0.2f, 2f, this.ysenseMenuEntry.Amount / 10f);
			base.ScreenManager.space_rsentivityY = num;
		}

		// Token: 0x0400069D RID: 1693
		private MenuEntry invertxMenuEntry;

		// Token: 0x0400069E RID: 1694
		private MenuEntry invertyMenuEntry;

		// Token: 0x0400069F RID: 1695
		private MenuEntry xsenseMenuEntry;

		// Token: 0x040006A0 RID: 1696
		private MenuEntry ysenseMenuEntry;

		// Token: 0x040006A1 RID: 1697
		private MenuEntry vibro;
	}
}
