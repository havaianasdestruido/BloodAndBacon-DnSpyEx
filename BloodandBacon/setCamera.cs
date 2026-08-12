using System;

namespace Blood
{
	// Token: 0x0200000D RID: 13
	internal class setCamera : MenuScreen
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x0002A17C File Offset: 0x0002837C
		public setCamera(int indexer)
			: base("SetCamera")
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

		// Token: 0x060000D4 RID: 212 RVA: 0x0002A284 File Offset: 0x00028484
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

		// Token: 0x060000D5 RID: 213 RVA: 0x0002A3EC File Offset: 0x000285EC
		private void setRadius(object sender, PlayerIndexEventArgs e)
		{
			float[] array = new float[] { 50f, 250f, 600f, 1200f, 1600f };
			base.ScreenManager.allcamsradius[this.radiusEntry.Myindex] = (int)this.radiusEntry.Amount;
			base.ScreenManager.roverdist[this.myindex - 1] = array[this.radiusEntry.Myindex];
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0002A450 File Offset: 0x00028650
		private void setOrbit(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.allcamsorbit[this.orbitEntry.Myindex] = (int)this.orbitEntry.Amount;
			base.ScreenManager.allcamsorbit[1] = (int)this.orbitEntry.Amount;
			base.ScreenManager.allcamsorbit[2] = (int)this.orbitEntry.Amount;
			base.ScreenManager.allcamsorbit[3] = (int)this.orbitEntry.Amount;
			base.ScreenManager.roverrotlock = (int)this.orbitEntry.Amount;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0002A4E4 File Offset: 0x000286E4
		private void setAlititude(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.allcamsaltitude[this.altitudeEntry.Myindex] = (int)this.altitudeEntry.Amount;
			base.ScreenManager.allcamsaltitude[1] = (int)this.altitudeEntry.Amount;
			base.ScreenManager.allcamsaltitude[2] = (int)this.altitudeEntry.Amount;
			base.ScreenManager.allcamsaltitude[3] = (int)this.altitudeEntry.Amount;
			base.ScreenManager.roverhitelock = 1 - (int)this.altitudeEntry.Amount;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0002A578 File Offset: 0x00028778
		private void setLens(object sender, PlayerIndexEventArgs e)
		{
			base.ScreenManager.allcamslens[this.lensEntry.Myindex] = (int)this.lensEntry.Amount;
		}

		// Token: 0x0400059B RID: 1435
		private MenuEntry radiusEntry;

		// Token: 0x0400059C RID: 1436
		private MenuEntry orbitEntry;

		// Token: 0x0400059D RID: 1437
		private MenuEntry altitudeEntry;

		// Token: 0x0400059E RID: 1438
		private MenuEntry lensEntry;

		// Token: 0x0400059F RID: 1439
		private int myindex;
	}
}
