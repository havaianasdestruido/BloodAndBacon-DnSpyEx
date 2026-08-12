using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200000F RID: 15
	internal class ControllerSettingsScreen : MenuScreen
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0002A9CC File Offset: 0x00028BCC
		public ControllerSettingsScreen()
			: base("Controller Settings")
		{
			MenuEntry menuEntry = new MenuEntry("Walking");
			MenuEntry menuEntry2 = new MenuEntry("Rover");
			MenuEntry menuEntry3 = new MenuEntry("Lander");
			menuEntry3.Selected += this.landerEntrySelected;
			menuEntry2.Selected += this.roverMenuEntrySelected;
			menuEntry.Selected += this.v127EntrySelected;
			base.MenuEntries.Add(menuEntry);
			base.MenuEntries.Add(menuEntry2);
			base.MenuEntries.Add(menuEntry3);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0002AA60 File Offset: 0x00028C60
		private void SetMenuEntryText()
		{
			this.vibro.Text = "VIBRATION";
			this.vibro.Type = 1;
			this.vibro.Lists = new string[] { "OFF", "ON" };
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0002AAAC File Offset: 0x00028CAC
		private void vibroSelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.vibroSetting = (int)this.vibro.Amount;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0002AAC8 File Offset: 0x00028CC8
		private void landerEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.menuflag = 0;
			if (base.ScreenManager.usingMouse)
			{
				base.ScreenManager.abort.Play(base.ScreenManager.ev, 0f, 0f);
				base.ScreenManager.AddScreen(new astrobindings("rebind"), new PlayerIndex?(e.PlayerIndex));
				return;
			}
			base.ScreenManager.AddScreen(new LanderScreen(1, base.ScreenManager), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0002AB58 File Offset: 0x00028D58
		private void roverMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.menuflag = 0;
			if (base.ScreenManager.usingMouse)
			{
				base.ScreenManager.abort.Play(base.ScreenManager.ev, 0f, 0f);
				return;
			}
			base.ScreenManager.AddScreen(new LanderScreen(2, base.ScreenManager), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0002ABC8 File Offset: 0x00028DC8
		private void v127EntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.menuflag = 0;
			if (base.ScreenManager.usingMouse)
			{
				base.ScreenManager.abort.Play(base.ScreenManager.ev, 0f, 0f);
				return;
			}
			base.ScreenManager.AddScreen(new LanderScreen(3, base.ScreenManager), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0002AC37 File Offset: 0x00028E37
		private void menuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
		}

		// Token: 0x040005A5 RID: 1445
		private MenuEntry vibro;
	}
}
