using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x02000025 RID: 37
	public class myGame : Game
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00031730 File Offset: 0x0002F930
		public myGame()
		{
			base.Content.RootDirectory = "Content";
			this.graphics = new GraphicsDeviceManager(this);
			Mouse.WindowHandle = base.Window.Handle;
			this.gamewindow = base.Window;
			this.gamewindow.AllowUserResizing = false;
			base.IsMouseVisible = false;
			this.gamewindow.Title = "Blood and Bacon";
			this.graphics.SynchronizeWithVerticalRetrace = true;
			base.IsFixedTimeStep = true;
			base.TargetElapsedTime = TimeSpan.FromSeconds(0.016666666666666666);
			this.screenManager = new ScreenManager(true, 800, 600, this, this.graphics);
			base.Components.Add(this.screenManager);
			this.screenManager.AddScreen(new MainMenu(true), null);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0003180D File Offset: 0x0002FA0D
		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00031816 File Offset: 0x0002FA16
		protected override void Draw(GameTime gameTime)
		{
			this.graphics.GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
		}

		// Token: 0x040006FA RID: 1786
		public GraphicsDeviceManager graphics;

		// Token: 0x040006FB RID: 1787
		private ScreenManager screenManager;

		// Token: 0x040006FC RID: 1788
		private GameWindow gamewindow;
	}
}
