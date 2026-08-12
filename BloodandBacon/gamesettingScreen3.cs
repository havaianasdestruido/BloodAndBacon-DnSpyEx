using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x02000181 RID: 385
	internal class gamesettingScreen3 : MenuScreen
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x00403484 File Offset: 0x00401684
		public gamesettingScreen3()
			: base("Game Settings3")
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

		// Token: 0x06000E4A RID: 3658 RVA: 0x004035A4 File Offset: 0x004017A4
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
			this.vibro.Text = "WALKING";
			this.vibro.Type = 1;
			this.vibro.Lists = new string[] { "", "", "" };
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00403790 File Offset: 0x00401990
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
			base.ScreenManager.space_winvertX = num;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x004037CC File Offset: 0x004019CC
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
			base.ScreenManager.space_winvertY = num;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00403808 File Offset: 0x00401A08
		private void xsenseSelected(object sender, PlayerIndexEventArgs e)
		{
			float num = MathHelper.Lerp(0.2f, 2f, this.xsenseMenuEntry.Amount / 10f);
			base.ScreenManager.space_wsentivityX = num;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00403844 File Offset: 0x00401A44
		private void ysenseSelected(object sender, PlayerIndexEventArgs e)
		{
			float num = MathHelper.Lerp(0.2f, 2f, this.ysenseMenuEntry.Amount / 10f);
			base.ScreenManager.space_wsentivityY = num;
		}

		// Token: 0x04003A96 RID: 14998
		private MenuEntry invertxMenuEntry;

		// Token: 0x04003A97 RID: 14999
		private MenuEntry invertyMenuEntry;

		// Token: 0x04003A98 RID: 15000
		private MenuEntry xsenseMenuEntry;

		// Token: 0x04003A99 RID: 15001
		private MenuEntry ysenseMenuEntry;

		// Token: 0x04003A9A RID: 15002
		private MenuEntry vibro;
	}
}
