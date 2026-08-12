using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200000E RID: 14
	internal class gamesettingScreen2 : MenuScreen
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x0002A5A0 File Offset: 0x000287A0
		public gamesettingScreen2()
			: base("Game Settings2")
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
			this.vibro.Selected += this.vibroSelected;
			base.MenuEntries.Add(this.vibro);
			base.MenuEntries.Add(this.invertxMenuEntry);
			base.MenuEntries.Add(this.invertyMenuEntry);
			base.MenuEntries.Add(this.xsenseMenuEntry);
			base.MenuEntries.Add(this.ysenseMenuEntry);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0002A6D8 File Offset: 0x000288D8
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
			this.vibro.Text = "LANDER";
			this.vibro.Type = 1;
			this.vibro.Lists = new string[] { "", "", "" };
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0002A8C3 File Offset: 0x00028AC3
		private void vibroSelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.vibroSetting = (int)this.vibro.Amount;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0002A8DC File Offset: 0x00028ADC
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
			base.ScreenManager.space_invertX = num;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0002A918 File Offset: 0x00028B18
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
			base.ScreenManager.space_invertY = num;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0002A954 File Offset: 0x00028B54
		private void xsenseSelected(object sender, PlayerIndexEventArgs e)
		{
			float num = MathHelper.Lerp(0.2f, 2f, this.xsenseMenuEntry.Amount / 10f);
			base.ScreenManager.space_sentivityX = num;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0002A990 File Offset: 0x00028B90
		private void ysenseSelected(object sender, PlayerIndexEventArgs e)
		{
			float num = MathHelper.Lerp(0.2f, 2f, this.ysenseMenuEntry.Amount / 10f);
			base.ScreenManager.space_sentivityY = num;
		}

		// Token: 0x040005A0 RID: 1440
		private MenuEntry invertxMenuEntry;

		// Token: 0x040005A1 RID: 1441
		private MenuEntry invertyMenuEntry;

		// Token: 0x040005A2 RID: 1442
		private MenuEntry xsenseMenuEntry;

		// Token: 0x040005A3 RID: 1443
		private MenuEntry ysenseMenuEntry;

		// Token: 0x040005A4 RID: 1444
		private MenuEntry vibro;
	}
}
