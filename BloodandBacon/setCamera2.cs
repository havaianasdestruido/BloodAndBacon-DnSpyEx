using System;

namespace Blood
{
	// Token: 0x02000159 RID: 345
	internal class setCamera2 : MenuScreen
	{
		// Token: 0x06000C9F RID: 3231 RVA: 0x00374EBC File Offset: 0x003730BC
		public setCamera2(int indexer)
			: base("SetCamera2")
		{
			this.myindex = indexer;
			this.radiusEntry = new MenuEntry(string.Empty);
			this.orbitEntry = new MenuEntry(string.Empty);
			this.altitudeEntry = new MenuEntry(string.Empty);
			this.lensEntry = new MenuEntry(string.Empty);
			this.SetMenuEntryText(indexer);
			this.radiusEntry.Selected += this.setRadius;
			this.orbitEntry.Selected += this.setOrbit;
			this.altitudeEntry.Selected += this.setAlititude;
			this.lensEntry.Selected += this.setLens;
			base.MenuEntries.Add(this.radiusEntry);
			base.MenuEntries.Add(this.orbitEntry);
			base.MenuEntries.Add(this.altitudeEntry);
			base.MenuEntries.Add(this.lensEntry);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00374FC4 File Offset: 0x003731C4
		private void SetMenuEntryText(int indexer)
		{
			this.radiusEntry.Text = "Distance";
			this.orbitEntry.Text = "Orbiting";
			this.altitudeEntry.Text = "Altitude";
			this.lensEntry.Text = "Lensing";
			this.radiusEntry.Type = 1;
			this.orbitEntry.Type = 1;
			this.altitudeEntry.Type = 1;
			this.lensEntry.Type = 1;
			this.radiusEntry.Lists = new string[] { "Inside", "Near", "Medium", "Far", "Eagle" };
			this.orbitEntry.Lists = new string[] { "Free", "Follow" };
			this.altitudeEntry.Lists = new string[] { "Locked", "Manual" };
			this.lensEntry.Lists = new string[] { "Static", "Dynamic" };
			this.radiusEntry.Myindex = indexer;
			this.orbitEntry.Myindex = indexer;
			this.altitudeEntry.Myindex = indexer;
			this.lensEntry.Myindex = indexer;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0037512C File Offset: 0x0037332C
		private void setRadius(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.allcamsradius[this.radiusEntry.Myindex] = (int)this.radiusEntry.Amount;
			float[] array = new float[] { 350f, 350f, 700f, 1300f, 2100f };
			base.ScreenManager.landerdist[this.myindex - 1] = array[this.radiusEntry.Myindex];
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00375190 File Offset: 0x00373390
		private void setOrbit(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.allcamsorbit[this.orbitEntry.Myindex] = (int)this.orbitEntry.Amount;
			base.ScreenManager.allcamsorbit[1] = (int)this.orbitEntry.Amount;
			base.ScreenManager.allcamsorbit[2] = (int)this.orbitEntry.Amount;
			base.ScreenManager.allcamsorbit[3] = (int)this.orbitEntry.Amount;
			base.ScreenManager.landerrotlock = (int)this.orbitEntry.Amount;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00375224 File Offset: 0x00373424
		private void setAlititude(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.allcamsaltitude[this.altitudeEntry.Myindex] = (int)this.altitudeEntry.Amount;
			base.ScreenManager.allcamsaltitude[1] = (int)this.altitudeEntry.Amount;
			base.ScreenManager.allcamsaltitude[2] = (int)this.altitudeEntry.Amount;
			base.ScreenManager.allcamsaltitude[3] = (int)this.altitudeEntry.Amount;
			base.ScreenManager.landerhitelock = 1 - (int)this.altitudeEntry.Amount;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x003752B8 File Offset: 0x003734B8
		private void setLens(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.allcamslens[this.lensEntry.Myindex] = (int)this.lensEntry.Amount;
		}

		// Token: 0x04003412 RID: 13330
		private MenuEntry radiusEntry;

		// Token: 0x04003413 RID: 13331
		private MenuEntry orbitEntry;

		// Token: 0x04003414 RID: 13332
		private MenuEntry altitudeEntry;

		// Token: 0x04003415 RID: 13333
		private MenuEntry lensEntry;

		// Token: 0x04003416 RID: 13334
		private int myindex;
	}
}
