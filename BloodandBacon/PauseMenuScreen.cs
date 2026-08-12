using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000A0 RID: 160
	internal class PauseMenuScreen : MenuScreen
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060005CE RID: 1486 RVA: 0x0013C8B8 File Offset: 0x0013AAB8
		// (remove) Token: 0x060005CF RID: 1487 RVA: 0x0013C8F0 File Offset: 0x0013AAF0
		public event EventHandler<PlayerIndexEventArgs> Accepted;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060005D0 RID: 1488 RVA: 0x0013C928 File Offset: 0x0013AB28
		// (remove) Token: 0x060005D1 RID: 1489 RVA: 0x0013C960 File Offset: 0x0013AB60
		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		// Token: 0x060005D2 RID: 1490 RVA: 0x0013C998 File Offset: 0x0013AB98
		public PauseMenuScreen(string text)
			: base("Paused")
		{
			base.TransitionOnTime = TimeSpan.FromSeconds(0.6000000238418579);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.6000000238418579);
			MenuEntry menuEntry = new MenuEntry(text);
			MenuEntry menuEntry2 = new MenuEntry("Settings");
			MenuEntry menuEntry3 = new MenuEntry("Quit Game");
			MenuEntry menuEntry4 = new MenuEntry("Sensitivity");
			base.MenuEntries.Add(menuEntry);
			base.MenuEntries.Add(menuEntry2);
			base.MenuEntries.Add(menuEntry4);
			base.MenuEntries.Add(menuEntry3);
			menuEntry2.Selected += this.settingsX;
			menuEntry.Selected += this.controlsX;
			menuEntry4.Selected += this.sansaX;
			menuEntry3.Selected += this.quitgame;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0013CA78 File Offset: 0x0013AC78
		private void settingsX(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			if (base.ScreenManager.vehicleindex == 3)
			{
				base.ScreenManager.AddScreen(new OptionsScreen2(), null);
				return;
			}
			base.ScreenManager.AddScreen(new OptionsScreen(), null);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0013CAD4 File Offset: 0x0013ACD4
		private void controlsX(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.menuflag = 0;
			if (base.ScreenManager.usingMouse)
			{
				base.ScreenManager.AddScreen(new astrobindings("rebind"), new PlayerIndex?(e.PlayerIndex));
				return;
			}
			base.ScreenManager.AddScreen(new ControllerSettingsScreen(), null);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0013CB34 File Offset: 0x0013AD34
		private void sansaX(object sender, PlayerIndexEventArgs e)
		{
			if (base.ScreenManager.vehicleindex == 1)
			{
				base.ScreenManager.AddScreen(new gamesettingScreen1(), new PlayerIndex?(e.PlayerIndex));
			}
			if (base.ScreenManager.vehicleindex == 2)
			{
				base.ScreenManager.AddScreen(new gamesettingScreen2(), new PlayerIndex?(e.PlayerIndex));
			}
			if (base.ScreenManager.vehicleindex == 3)
			{
				base.ScreenManager.AddScreen(new gamesettingScreen3(), new PlayerIndex?(e.PlayerIndex));
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0013CC74 File Offset: 0x0013AE74
		private void quitgame(object sender, PlayerIndexEventArgs e)
		{
			messagebox messagebox = new messagebox("Exit Game?", 1, 0);
			messagebox.Accepted += delegate
			{
				this.ScreenManager.poppy = 0;
				if (this.Accepted != null)
				{
					this.Accepted(this, new PlayerIndexEventArgs(e.PlayerIndex));
				}
				this.ExitScreen();
			};
			messagebox.Cancelled += delegate
			{
				this.ScreenManager.Game.IsMouseVisible = true;
				messagebox messagebox2 = new messagebox("Stop Screwing Around", 0, 0);
				this.ScreenManager.AddScreen(messagebox2, new PlayerIndex?(e.PlayerIndex));
			};
			base.ScreenManager.AddScreen(messagebox, new PlayerIndex?(e.PlayerIndex));
		}
	}
}
