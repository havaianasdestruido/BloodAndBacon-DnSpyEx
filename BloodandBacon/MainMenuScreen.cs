using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x020000AA RID: 170
	internal class MainMenuScreen : MenuScreen
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00147538 File Offset: 0x00145738
		public MainMenuScreen()
			: base("Main Menu")
		{
			MenuEntry menuEntry = new MenuEntry("PLAY GAME");
			MenuEntry menuEntry2 = new MenuEntry("INTERFACE");
			this.BackGroundPlanet = new MenuEntry(string.Empty);
			MenuEntry menuEntry3 = new MenuEntry("SETTINGS");
			MenuEntry menuEntry4 = new MenuEntry("EXTRAS");
			this.BackGroundPlanet = new MenuEntry(string.Empty);
			this.SetMenuEntryText();
			menuEntry.Selected += this.PlayGameMenuEntrySelected;
			this.BackGroundPlanet.Selected += this.planetSelected;
			menuEntry2.Selected += this.interface1;
			menuEntry4.Selected += this.PlayTitleEntrySelected;
			menuEntry3.Selected += this.OptionsMenuEntrySelected;
			base.MenuEntries.Add(menuEntry);
			base.MenuEntries.Add(this.BackGroundPlanet);
			base.MenuEntries.Add(menuEntry2);
			base.MenuEntries.Add(menuEntry3);
			base.MenuEntries.Add(menuEntry4);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00147644 File Offset: 0x00145844
		private void SetMenuEntryText()
		{
			this.BackGroundPlanet.Text = "PLANET";
			this.BackGroundPlanet.Type = 2;
			this.BackGroundPlanet.Lists = new string[] { "Oor", "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto" };
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x001476D4 File Offset: 0x001458D4
		private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.aliasSet = 2;
			LoadingScreen2.Load(base.ScreenManager, true, new PlayerIndex?(e.PlayerIndex), new GameScreen[]
			{
				new GameplayScreen(1f)
			});
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0014771C File Offset: 0x0014591C
		private void interface1(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.menuflag = 1;
			base.ScreenManager.AddScreen(new Interface(), new PlayerIndex?(e.PlayerIndex));
			base.ScreenManager.AddScreen(new Construction(), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0014776B File Offset: 0x0014596B
		private void loadMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.menuflag = 1;
			base.ScreenManager.AddScreen(new RotoTyper(), new PlayerIndex?(e.PlayerIndex));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00147794 File Offset: 0x00145994
		private void PlayTitleEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			base.ScreenManager.AddScreen(new ExtrasScreen(), null);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x001477C6 File Offset: 0x001459C6
		private void builderEntrySelected(object sender, PlayerIndexEventArgs e)
		{
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x001477C8 File Offset: 0x001459C8
		private void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.drawflag = 0;
			base.ScreenManager.AddScreen(new OptionsScreen(), null);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0014783C File Offset: 0x00145A3C
		protected override void OnCancel(PlayerIndex playerIndex)
		{
			messagebox messagebox = new messagebox("You Are About To Exit", 1, 0);
			messagebox.Accepted += delegate
			{
				base.ScreenManager.inSpace = false;
				LoadingScreen1.Load(base.ScreenManager, false, null, null, new GameScreen[]
				{
					new MainMenu(false)
				});
			};
			base.ScreenManager.AddScreen(messagebox, new PlayerIndex?(playerIndex));
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0014787A File Offset: 0x00145A7A
		private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.SavePrefs();
			base.ScreenManager.Game.Exit();
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00147898 File Offset: 0x00145A98
		private void planetSelected(object sender, PlayerIndexEventArgs e)
		{
			if (this.BackGroundPlanet.Amount < 1f)
			{
				this.BackGroundPlanet.Amount = 1f;
			}
			base.ScreenManager.bgindex = (int)this.BackGroundPlanet.Amount;
			this.BackGroundPlanet.Text = this.BackGroundPlanet.Lists[base.ScreenManager.bgindex];
		}

		// Token: 0x04001814 RID: 6164
		private MenuEntry BackGroundPlanet;
	}
}
