using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200001A RID: 26
	internal class MenuEntry
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0002CD94 File Offset: 0x0002AF94
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0002CD9C File Offset: 0x0002AF9C
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0002CDA5 File Offset: 0x0002AFA5
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0002CDAD File Offset: 0x0002AFAD
		public int Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0002CDB6 File Offset: 0x0002AFB6
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0002CDBE File Offset: 0x0002AFBE
		public float Amount
		{
			get
			{
				return this.amount;
			}
			set
			{
				this.amount = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0002CDC7 File Offset: 0x0002AFC7
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0002CDCF File Offset: 0x0002AFCF
		public string[] Lists
		{
			get
			{
				return this.lists;
			}
			set
			{
				this.lists = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0002CDD8 File Offset: 0x0002AFD8
		// (set) Token: 0x06000133 RID: 307 RVA: 0x0002CDE0 File Offset: 0x0002AFE0
		public int Myindex
		{
			get
			{
				return this.myindex;
			}
			set
			{
				this.myindex = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0002CDE9 File Offset: 0x0002AFE9
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0002CDF1 File Offset: 0x0002AFF1
		public bool Active
		{
			get
			{
				return this.active;
			}
			set
			{
				this.active = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000136 RID: 310 RVA: 0x0002CDFC File Offset: 0x0002AFFC
		// (remove) Token: 0x06000137 RID: 311 RVA: 0x0002CE34 File Offset: 0x0002B034
		public event EventHandler<PlayerIndexEventArgs> Selected;

		// Token: 0x06000138 RID: 312 RVA: 0x0002CE69 File Offset: 0x0002B069
		protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
		{
			if (this.Selected != null)
			{
				this.Selected(this, new PlayerIndexEventArgs(playerIndex));
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0002CE88 File Offset: 0x0002B088
		public MenuEntry(string text)
		{
			this.text = text;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0002CEFC File Offset: 0x0002B0FC
		public void LoadContent(MenuScreen screen)
		{
			this.sc = screen.ScreenManager;
			if (this.content == null)
			{
				this.content = new ContentManager(this.sc.Game.Services, "Content");
			}
			this.spriteBatch = this.sc.SpriteBatch;
			this.aspectRatio = (float)this.sc.GraphicsDevice.Viewport.Width / (float)this.sc.GraphicsDevice.Viewport.Height;
			this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30f), 1.77f, 1f, 490000f);
			this.viewMatrix = Matrix.CreateLookAt(this.campos, this.camlookpos, Vector3.Up);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0002CFC8 File Offset: 0x0002B1C8
		public virtual void Draw(MenuScreen screen, Vector2 position, bool isSelected, GameTime gameTime, float index, float count, float timeadjust)
		{
			if (this.sc.menutype != 1)
			{
				this.sc.halo = this.sc.halo2;
			}
			count += timeadjust * 1f;
			Vector3 vector;
			if (this.sc.menutype == 1)
			{
				vector = (isSelected ? new Vector3(2.5f, 2.5f, 2.5f) : new Vector3(0.2f, 0.5f, 0.78f));
			}
			else
			{
				vector = (isSelected ? new Vector3(2.5f, 2.5f, 2.5f) : new Vector3(0.2f, 0.5f, 0.75f));
			}
			Vector2 vector2 = new Vector2((1280f - this.sc.halo.MeasureString(this.text).X) / 2f, 360f);
			if (this.type == 1)
			{
				vector2.X = 640f - (position.X + 240f) / 2f;
			}
			Vector3 vector3 = new Vector3(vector2.X, position.Y, 0f);
			Vector3 vector4 = new Vector3(640f + (position.X + 90f) / 2f, position.Y + 15f, 0f);
			Vector3 vector5 = new Vector3(0f, 0f, 0f);
			float num = screen.TransitionPosition * 1.2f;
			if (screen.ScreenState == ScreenState.TransitionOn)
			{
				vector5.Y += MathHelper.Clamp((num - (1f - (index + 2f) / count)) * count, 0f, 1f) * 2f;
				vector3.X += -1400f + (float)Math.Cos((double)vector5.Y) * 1400f;
				vector4.X += -1400f + (float)Math.Cos((double)vector5.Y) * 1400f;
			}
			else if (screen.ScreenState == ScreenState.TransitionOff)
			{
				vector5.Y -= MathHelper.Clamp((num - index / count) * count, 0f, 1f) * 2f;
				vector3.X -= vector5.Y * 910f;
				vector4.X -= vector5.Y * 910f;
			}
			this.DrawMenu(this.text, vector3, vector4, isSelected, vector);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0002D252 File Offset: 0x0002B452
		public virtual float GetHeight(MenuScreen screen)
		{
			return (float)(this.sc.halo.LineSpacing + 3);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0002D268 File Offset: 0x0002B468
		private void DrawMenu(string text, Vector3 position, Vector3 trans2, bool isSelected, Vector3 color)
		{
			string text2 = "";
			if (this.Type == 1)
			{
				text2 = this.lists[(int)this.amount] ?? "";
			}
			Color color2 = new Color(110, 160, 255, 150);
			color2 = new Color(5, 125, 248, 160);
			if (this.sc.menutype == 1)
			{
				color2 = new Color(5, 125, 248, 170);
			}
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, this.sc.ScaleMatrix1);
			float num = this.sc.halo.MeasureString(text).X + 100f;
			float num2 = this.sc.halo.MeasureString(text).Y + 22f;
			if (isSelected)
			{
				this.spriteBatch.Draw(this.sc.glow1, new Rectangle((int)position.X - 50, (int)position.Y - 15, (int)num, (int)num2), color2);
			}
			this.spriteBatch.DrawString(this.sc.halo, text, new Vector2(position.X, position.Y), new Color(color.X, color.Y, color.Z, 155f));
			if (this.Type == 1)
			{
				this.spriteBatch.DrawString(this.sc.halo, text2, new Vector2(trans2.X - this.sc.halo.MeasureString(text2).X / 2f, position.Y), new Color(color.X, color.Y, color.Z, 155f));
			}
			this.spriteBatch.End();
		}

		// Token: 0x040005FB RID: 1531
		private string[] lists;

		// Token: 0x040005FC RID: 1532
		private string text;

		// Token: 0x040005FD RID: 1533
		private int myindex;

		// Token: 0x040005FE RID: 1534
		private int type;

		// Token: 0x040005FF RID: 1535
		private float amount;

		// Token: 0x04000600 RID: 1536
		private bool active;

		// Token: 0x04000601 RID: 1537
		private ContentManager content;

		// Token: 0x04000602 RID: 1538
		private Matrix projectionMatrix;

		// Token: 0x04000603 RID: 1539
		private Matrix viewMatrix;

		// Token: 0x04000604 RID: 1540
		private float aspectRatio;

		// Token: 0x04000605 RID: 1541
		private Vector3 camlookpos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000606 RID: 1542
		private Vector3 campos = new Vector3(0f, 0f, 2000f);

		// Token: 0x04000607 RID: 1543
		private ScreenManager sc;

		// Token: 0x04000608 RID: 1544
		private SpriteBatch spriteBatch;

		// Token: 0x04000609 RID: 1545
		private Vector2 textPosition = new Vector2(256f);

		// Token: 0x0400060A RID: 1546
		private float radians = Convert.ToSingle(0.6283185307179586);
	}
}
