using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000086 RID: 134
	public class Overlay : GameScreen
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x00113C8C File Offset: 0x00111E8C
		public void Load(ContentManager content, ScreenManager screenmanage)
		{
			this.content = content;
			this.rect = new Rectangle(0, 0, screenmanage.GraphicsDevice.PresentationParameters.BackBufferWidth, screenmanage.GraphicsDevice.PresentationParameters.BackBufferHeight);
			this.sc = screenmanage;
			this.spriteBatch = new SpriteBatch(this.sc.GraphicsDevice);
			this.viewport = this.sc.GraphicsDevice.Viewport;
			this.screenCenter = new Vector2((float)this.viewport.Width, (float)this.viewport.Height) / 2f;
			this.font = content.Load<SpriteFont>("astro\\fonts\\datafont");
			this.stained = content.Load<Texture2D>("astro\\sprites\\stained");
			this.dot = content.Load<Texture2D>("astro\\sprites\\blackdot");
			this.LB_on = new Rectangle[5];
			this.LB_col = new Rectangle[5];
			this.RB_on = new Rectangle[5];
			this.RB_col = new Rectangle[5];
			this.LB_on[0] = new Rectangle(0, 160, 100, 40);
			this.LB_on[1] = new Rectangle(0, 160, 100, 40);
			this.LB_on[2] = new Rectangle(149, 160, 100, 40);
			this.LB_on[3] = new Rectangle(298, 160, 100, 40);
			this.LB_on[4] = new Rectangle(449, 160, 100, 40);
			this.RB_on[0] = new Rectangle(0, 200, 100, 40);
			this.RB_on[1] = new Rectangle(0, 200, 100, 40);
			this.RB_on[2] = new Rectangle(149, 200, 100, 40);
			this.RB_on[3] = new Rectangle(298, 200, 100, 40);
			this.RB_on[4] = new Rectangle(449, 200, 100, 40);
			this.LB_col[0] = new Rectangle(0, 80, 100, 40);
			this.LB_col[1] = new Rectangle(0, 80, 100, 40);
			this.LB_col[2] = new Rectangle(149, 80, 100, 40);
			this.LB_col[3] = new Rectangle(298, 80, 100, 40);
			this.LB_col[4] = new Rectangle(449, 80, 100, 40);
			this.RB_col[0] = new Rectangle(0, 120, 100, 40);
			this.RB_col[1] = new Rectangle(0, 120, 100, 40);
			this.RB_col[2] = new Rectangle(149, 120, 100, 40);
			this.RB_col[3] = new Rectangle(298, 120, 100, 40);
			this.RB_col[4] = new Rectangle(449, 120, 100, 40);
			this.compassBorder = content.Load<Texture2D>("astro\\sprites\\compass\\compassBorder");
			this.compass = content.Load<Texture2D>("astro\\sprites\\compass\\compass");
			this.landerChip = content.Load<Texture2D>("astro\\sprites\\compass\\landerChip");
			this.bouy1texture = content.Load<Texture2D>("astro\\sprites\\compass\\bouy1");
			this.bouy2texture = content.Load<Texture2D>("astro\\sprites\\compass\\bouy2");
			this.bouy3texture = content.Load<Texture2D>("astro\\sprites\\compass\\bouy3");
			this.farmtexture = content.Load<Texture2D>("astro\\sprites\\compass\\farmIcon");
			this.flowertexture = content.Load<Texture2D>("astro\\sprites\\compass\\flower1");
			this.manbouytexture = content.Load<Texture2D>("astro\\sprites\\compass\\littleman");
			this.facilityTexture = content.Load<Texture2D>("astro\\sprites\\compass\\facilityIcon");
			this.facilityTexture2 = content.Load<Texture2D>("astro\\sprites\\compass\\facilityIconBig");
			foreach (Overlay.Flare flare in this.flares)
			{
				flare.Texture = content.Load<Texture2D>(flare.TextureName);
			}
			this.startwidth2 = (float)this.sc.helmet1.Width;
			this.starthite2 = (float)this.sc.helmet1.Height;
			this.startaspect2 = this.startwidth2 / this.starthite2;
			this.stretch2 = this.startaspect2 / this.sc.aspectratio2;
			Vector2 vector = new Vector2(1280f, 720f) * 0.8f;
			if (vector.X / vector.Y >= this.sc.aspectratio2)
			{
				this.src = new Rectangle(0, 0, this.sc.helmet1.Width, this.sc.helmet1.Height);
				this.amt = (float)this.sc.helmet1.Width / vector.X;
				this.destn = new Rectangle((int)((float)this.sc.width * 0.5f), (int)((float)this.sc.hite * 0.5f), (int)((float)this.sc.width * this.amt), (int)((float)this.sc.hite / this.stretch2 * this.amt));
				this.ctr = new Vector2((float)this.sc.helmet1.Width, (float)this.sc.helmet1.Height) / 2f;
			}
			else
			{
				this.src = new Rectangle(0, 0, this.sc.helmet1.Width, this.sc.helmet1.Height);
				this.amt = (float)this.sc.helmet1.Height / vector.Y;
				this.destn = new Rectangle((int)((float)this.sc.width * 0.5f), (int)((float)this.sc.hite * 0.5f), (int)((float)this.sc.width * this.stretch2 * this.amt), (int)((float)this.sc.hite * this.amt));
				this.ctr = new Vector2((float)this.sc.helmet1.Width, (float)this.sc.helmet1.Height) / 2f;
			}
			this.bottom = 720 - this.RoverButtons.Height - 20;
			this.left = 640 - this.RoverButtons.Width / 2;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00114398 File Offset: 0x00112598
		public override void UnloadContent()
		{
			this.content.Unload();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x001143A8 File Offset: 0x001125A8
		public void Update(int x)
		{
			bool flag = false;
			if (this.projectedPosition.Z > 0f || this.scopeMode || Facility.inFacility)
			{
				flag = true;
			}
			if (flag)
			{
				this.lightPosition = new Vector2(this.projectedPosition.X, this.projectedPosition.Y);
				this.flareVector = this.screenCenter - this.lightPosition;
				this.occlusionAlpha = MathHelper.Clamp(-(this.LightDirection.Y - 0.1f) / 0.1f, 0f, 1.2f);
			}
			if (x == 1)
			{
				this.cellAMT = (int)MathHelper.Clamp((float)this.cellAMT, 5f, 100f);
				int num = this.cellAMT / 20;
				this.barcolor = this.cellColor[num];
				int num2 = (int)((float)this.cellAMT / 100f * (float)this.batteryBar.Height);
				int num3 = this.batteryBar.Height - num2;
				int num4 = (int)((float)num3 / this.cellScale) + (int)this.batpos.Y + (int)(this.batterybarOffset.Y / this.cellScale);
				this.destbar = new Rectangle((int)this.batpos.X + (int)(this.batterybarOffset.X / this.cellScale), num4, (int)((float)this.batteryBar.Width / this.cellScale), (int)((float)num2 / this.cellScale));
				this.source = new Rectangle(this.batteryBar.X, this.batteryBar.Y + num3, this.batteryBar.Width, num2);
				this.dest2 = new Rectangle((int)this.batpos.X, (int)this.batpos.Y, (int)((float)this.battery.Width / this.cellScale), (int)((float)this.battery.Height / this.cellScale));
				this.mposs1 = (float)(this.ww - 7) - 70.028f * this.bouy1Needle;
				this.drawbouy1 = this.mposs1 < (float)(this.ww + 118) && this.mposs1 > (float)(this.ww - 125) && this.bouy1 != Vector3.Zero;
				this.mposs2 = (float)(this.ww - 7) - 70.028f * this.bouy2Needle;
				this.drawbouy2 = this.mposs2 < (float)(this.ww + 118) && this.mposs2 > (float)(this.ww - 125) && this.bouy2 != Vector3.Zero;
				this.mposs3 = (float)(this.ww - 7) - 70.028f * this.bouymanNeedle;
				this.drawmanbouy = this.mposs3 < (float)(this.ww + 118) && this.mposs3 > (float)(this.ww - 125) && Overlay.manbouy != Vector3.Zero;
				this.mposs4 = (float)(this.ww - 7) - 70.028f * this.farmNeedle;
				this.drawfarmbouy = this.mposs4 < (float)(this.ww + 118) && this.mposs4 > (float)(this.ww - 125) && this.farm != Vector3.Zero;
				this.mposs5 = (float)(this.ww - 12) - 70.028f * this.landerNeedle;
				this.drawlanderbouy = this.mposs5 < (float)(this.ww + 112) && this.mposs5 > (float)(this.ww - 132);
				this.mposs6 = (float)(this.ww - 12) - 70.028f * this.flowerNeedle;
				this.drawflowerbouy = this.mposs6 < (float)(this.ww + 112) && this.mposs6 > (float)(this.ww - 132) && this.flower != Vector3.Zero;
				if (this.roverbutton1 != 0)
				{
					this.roverbutton1--;
					this.mult1 = (float)this.roverbutton1 / 40f;
				}
				if (this.roverbutton2 != 0)
				{
					this.roverbutton2--;
					this.mult2 = (float)this.roverbutton2 / 40f;
				}
				if (this.roverbutton3 != 0)
				{
					this.roverbutton3--;
					this.mult3 = (float)this.roverbutton3 / 40f;
				}
				if (this.roverbutton4 != 0)
				{
					this.roverbutton4--;
					this.mult4 = (float)this.roverbutton4 / 40f;
				}
			}
			if (x == 2)
			{
				this.fuelAMT = (int)MathHelper.Clamp((float)this.fuelAMT, 4f, 100f);
				int num5 = this.fuelAMT / 20;
				this.barcolor = this.cellColor2[num5];
				int num6 = (int)((float)this.fuelAMT / 100f * (float)this.fuelBar.Height);
				int num7 = this.fuelBar.Height - num6;
				int num8 = (int)((float)num7 / this.fuelScale) + (int)this.fuelpos.Y + (int)(this.fuelbarOffset.Y / this.fuelScale);
				this.destbar = new Rectangle((int)this.fuelpos.X + (int)(this.fuelbarOffset.X / this.fuelScale), num8, (int)((float)this.fuelBar.Width / this.fuelScale), (int)((float)num6 / this.fuelScale));
				this.source = new Rectangle(this.fuelBar.X, this.fuelBar.Y + num7, this.fuelBar.Width, num6);
				this.dest2 = new Rectangle((int)this.fuelpos.X, (int)this.fuelpos.Y, (int)((float)this.fuel.Width / this.fuelScale), (int)((float)this.fuel.Height / this.fuelScale));
				this.mposs1 = (float)(this.ww - 7) - 70.028f * this.bouy1Needle;
				this.drawbouy1 = this.mposs1 < (float)(this.ww + 118) && this.mposs1 > (float)(this.ww - 125) && this.bouy1 != Vector3.Zero;
				this.mposs2 = (float)(this.ww - 7) - 70.028f * this.bouy2Needle;
				this.drawbouy2 = this.mposs2 < (float)(this.ww + 118) && this.mposs2 > (float)(this.ww - 125) && this.bouy2 != Vector3.Zero;
				this.mposs3 = (float)(this.ww - 12) - 70.028f * this.facilityNeedle;
				this.drawfacility = this.mposs3 < (float)(this.ww + 112) && this.mposs3 > (float)(this.ww - 132) && this.facility != Vector3.Zero;
				this.mposs4 = (float)(this.ww - 7) - 70.028f * this.bouymanNeedle;
				this.drawmanbouy = this.mposs4 < (float)(this.ww + 118) && this.mposs4 > (float)(this.ww - 125) && Overlay.manbouy != Vector3.Zero;
				this.mposs5 = (float)(this.ww - 7) - 70.028f * this.farmNeedle;
				this.drawfarmbouy = this.mposs5 < (float)(this.ww + 118) && this.mposs5 > (float)(this.ww - 125) && this.farm != Vector3.Zero;
				this.mposs6 = (float)(this.ww - 12) - 70.028f * this.flowerNeedle;
				this.drawflowerbouy = this.mposs6 < (float)(this.ww + 112) && this.mposs6 > (float)(this.ww - 132) && this.flower != Vector3.Zero;
				if (this.landerbutton1 != 0)
				{
					this.landerbutton1--;
					this.mult1 = (float)this.landerbutton1 / 40f;
				}
				if (this.landerbutton2 != 0)
				{
					this.landerbutton2--;
					this.mult2 = (float)this.landerbutton2 / 40f;
				}
				if (this.landerbutton3 != 0)
				{
					this.landerbutton3--;
					this.mult3 = (float)(this.landerbutton3 % 52) / 40f;
				}
				if (this.landerbutton4 != 0)
				{
					this.landerbutton4--;
					this.mult4 = (float)this.landerbutton4 / 40f;
				}
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00114C84 File Offset: 0x00112E84
		public void Draw(int x)
		{
			this.scaler = this.choose[this.sc.aliasSetting];
			Matrix view = this.View;
			view.Translation = Vector3.Zero;
			this.projectedPosition = this.viewport.Project(-this.LightDirection, this.Projection, view, Matrix.Identity);
			if (x == 1)
			{
				this.DrawStatsRover();
			}
			if (x == 2)
			{
				this.DrawStatsLander();
			}
			if (x == 3)
			{
				this.DrawScreenHuman();
			}
			if (this.projectedPosition.Z > 0f || this.scopeMode || Facility.inFacility)
			{
				return;
			}
			this.DrawFlares(this.lightPosition, this.flareVector);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00114D38 File Offset: 0x00112F38
		private void DrawStatsRover()
		{
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, this.sc.ScaleMatrix1);
			this.spriteBatch.Draw(this.sc.hudbuttons, this.destbar, new Rectangle?(this.source), this.barcolor);
			this.spriteBatch.Draw(this.sc.hudbuttons, this.dest2, new Rectangle?(this.battery), Color.White);
			if (this.drawbouy1)
			{
				this.spriteBatch.Draw(this.bouy1texture, new Vector2(this.mposs1, (float)(32 + this.yy)), new Color(255, 255, 255, 190));
			}
			if (this.drawbouy2)
			{
				this.spriteBatch.Draw(this.bouy2texture, new Vector2(this.mposs2, (float)(32 + this.yy)), new Color(255, 255, 255, 190));
			}
			if (this.drawmanbouy)
			{
				this.spriteBatch.Draw(this.manbouytexture, new Vector2(this.mposs3, (float)(32 + this.yy)), new Color(255, 255, 255, 190));
			}
			if (this.drawfarmbouy)
			{
				this.spriteBatch.Draw(this.farmtexture, new Vector2(this.mposs4, (float)(32 + this.yy)), new Color(255, 255, 255, 190));
			}
			if (this.drawlanderbouy)
			{
				this.spriteBatch.Draw(this.landerChip, new Vector2(this.mposs5, (float)(32 + this.yy)), new Color(255, 255, 255, 190));
			}
			if (this.drawflowerbouy)
			{
				this.spriteBatch.Draw(this.flowertexture, new Vector2(this.mposs6, (float)(46 + this.yy)), null, Color.White, 0f, new Vector2(12f, 12f), this.flowerScale, SpriteEffects.None, 0f);
			}
			this.spriteBatch.Draw(this.compass, new Vector2((float)(this.ww - 110), (float)(30 + this.yy)), new Rectangle?(new Rectangle((int)(70.028f * this.needle), 0, 220, 45)), Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.compassBorder, new Vector2((float)(this.ww - this.compassBorder.Width / 2), (float)(30 + this.yy)), Color.White);
			this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom), new Rectangle?(this.RoverButtons), Color.White);
			this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom) + new Vector2((float)this.RB_on[this.buttonindex].X, 0f), new Rectangle?(this.RB_on[this.buttonindex]), Color.White);
			if (this.roverbutton1 != 0)
			{
				this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom) + new Vector2((float)this.RB_col[1].X, 0f), new Rectangle?(this.RB_col[1]), this.grn * this.mult1);
			}
			if (this.roverbutton2 != 0)
			{
				this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom) + new Vector2((float)this.RB_col[2].X, 0f), new Rectangle?(this.RB_col[2]), this.grn * this.mult2);
			}
			if (this.roverbutton3 != 0)
			{
				this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom) + new Vector2((float)this.RB_col[3].X, 0f), new Rectangle?(this.RB_col[3]), this.grn * this.mult3);
			}
			if (this.roverbutton4 != 0)
			{
				this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom) + new Vector2((float)this.RB_col[4].X, 0f), new Rectangle?(this.RB_col[4]), this.grn * this.mult4);
			}
			this.spriteBatch.End();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x001152BC File Offset: 0x001134BC
		private void DrawStatsLander()
		{
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, this.sc.ScaleMatrix1);
			this.spriteBatch.Draw(this.sc.hudbuttons, this.destbar, new Rectangle?(this.source), this.barcolor);
			this.spriteBatch.Draw(this.sc.hudbuttons, this.dest2, new Rectangle?(this.fuel), Color.White);
			if (this.drawbouy1)
			{
				this.spriteBatch.Draw(this.bouy1texture, new Vector2(this.mposs1, (float)(32 + this.yy)), new Color(255, 255, 255, 210));
			}
			if (this.drawbouy2)
			{
				this.spriteBatch.Draw(this.bouy2texture, new Vector2(this.mposs2, (float)(32 + this.yy)), new Color(255, 255, 255, 210));
			}
			if (this.drawfacility)
			{
				if (this.bigfacilitymarker)
				{
					this.spriteBatch.Draw(this.facilityTexture2, new Vector2(this.mposs3, (float)(32 + this.yy)), new Color(255, 255, 255, 210));
				}
				else
				{
					this.spriteBatch.Draw(this.facilityTexture, new Vector2(this.mposs3, (float)(32 + this.yy)), new Color(255, 255, 255, 210));
				}
			}
			if (this.drawmanbouy)
			{
				this.spriteBatch.Draw(this.manbouytexture, new Vector2(this.mposs4, (float)(32 + this.yy)), new Color(255, 255, 255, 210));
			}
			if (this.drawfarmbouy)
			{
				this.spriteBatch.Draw(this.farmtexture, new Vector2(this.mposs5, (float)(32 + this.yy)), new Color(255, 255, 255, 210));
			}
			if (this.drawflowerbouy)
			{
				this.spriteBatch.Draw(this.flowertexture, new Vector2(this.mposs6, (float)(46 + this.yy)), null, Color.White, 0f, new Vector2(12f, 12f), this.flowerScale, SpriteEffects.None, 0f);
			}
			this.spriteBatch.Draw(this.compass, new Vector2((float)(this.ww - 110), (float)(30 + this.yy)), new Rectangle?(new Rectangle((int)(70.028f * this.needle), 0, 220, 45)), Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(this.compassBorder, new Vector2((float)(this.ww - this.compassBorder.Width / 2), (float)(30 + this.yy)), Color.White);
			this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom), new Rectangle?(this.LanderButtons), Color.White);
			this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom) + new Vector2((float)this.LB_on[this.buttonindex].X, 0f), new Rectangle?(this.LB_on[this.buttonindex]), Color.White);
			if (this.landerbutton1 != 0)
			{
				this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom) + new Vector2((float)this.LB_col[1].X, 0f), new Rectangle?(this.LB_col[1]), this.grn * this.mult2);
			}
			if (this.landerbutton2 != 0)
			{
				this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom) + new Vector2((float)this.LB_col[2].X, 0f), new Rectangle?(this.LB_col[2]), this.grn * this.mult2);
			}
			if (this.landerbutton3 != 0)
			{
				this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom) + new Vector2((float)this.LB_col[3].X, 0f), new Rectangle?(this.LB_col[3]), this.grn * this.mult3);
			}
			if (this.landerbutton4 != 0)
			{
				this.spriteBatch.Draw(this.sc.hudbuttons, new Vector2((float)this.left, (float)this.bottom) + new Vector2((float)this.LB_col[4].X, 0f), new Rectangle?(this.LB_col[4]), this.grn * this.mult4);
			}
			this.spriteBatch.End();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0011588C File Offset: 0x00113A8C
		private void DrawScreenHuman()
		{
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			if (!this.damaged1)
			{
				this.spriteBatch.Draw(this.sc.helmet1, this.destn, new Rectangle?(this.src), Color.White, 0f, this.ctr, SpriteEffects.None, 0f);
			}
			if (this.damaged1)
			{
				this.spriteBatch.Draw(this.sc.helmet2, this.destn, new Rectangle?(this.src), Color.White, 0f, this.ctr, SpriteEffects.None, 0f);
			}
			this.spriteBatch.End();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00115940 File Offset: 0x00113B40
		private void DrawFlares(Vector2 lightPosition, Vector2 flareVector)
		{
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
			foreach (Overlay.Flare flare in this.flares)
			{
				Vector2 vector = lightPosition + flareVector * flare.Position;
				Vector4 vector2 = flare.Color.ToVector4();
				vector2.W *= this.occlusionAlpha;
				Vector2 vector3 = new Vector2((float)flare.Texture.Width, (float)flare.Texture.Height) / 2f;
				this.spriteBatch.Draw(flare.Texture, vector, null, new Color(vector2), 1f, vector3, flare.Scale, SpriteEffects.None, 0f);
			}
			this.spriteBatch.End();
		}

		// Token: 0x04001275 RID: 4725
		private const float glowSize = 100f;

		// Token: 0x04001276 RID: 4726
		private const float querySize = 50f;

		// Token: 0x04001277 RID: 4727
		public static Vector3 manbouy;

		// Token: 0x04001278 RID: 4728
		private Vector2 lightPosition;

		// Token: 0x04001279 RID: 4729
		private Vector2 flareVector;

		// Token: 0x0400127A RID: 4730
		private bool drawmanbouy = true;

		// Token: 0x0400127B RID: 4731
		private bool drawbouy1 = true;

		// Token: 0x0400127C RID: 4732
		private bool drawbouy2 = true;

		// Token: 0x0400127D RID: 4733
		private bool drawfarmbouy = true;

		// Token: 0x0400127E RID: 4734
		private bool drawflowerbouy;

		// Token: 0x0400127F RID: 4735
		private bool drawfacility = true;

		// Token: 0x04001280 RID: 4736
		private bool drawlanderbouy = true;

		// Token: 0x04001281 RID: 4737
		private Color barcolor;

		// Token: 0x04001282 RID: 4738
		private int barHite;

		// Token: 0x04001283 RID: 4739
		private int ypos;

		// Token: 0x04001284 RID: 4740
		private int ypos2;

		// Token: 0x04001285 RID: 4741
		private Rectangle destbar;

		// Token: 0x04001286 RID: 4742
		private Rectangle source;

		// Token: 0x04001287 RID: 4743
		private Rectangle dest2;

		// Token: 0x04001288 RID: 4744
		private Color grn = new Color(255, 255, 255, 255);

		// Token: 0x04001289 RID: 4745
		private float mult1;

		// Token: 0x0400128A RID: 4746
		private float mult2;

		// Token: 0x0400128B RID: 4747
		private float mult3;

		// Token: 0x0400128C RID: 4748
		private float mult4;

		// Token: 0x0400128D RID: 4749
		private float mposs1;

		// Token: 0x0400128E RID: 4750
		private float mposs2;

		// Token: 0x0400128F RID: 4751
		private float mposs3;

		// Token: 0x04001290 RID: 4752
		private float mposs4;

		// Token: 0x04001291 RID: 4753
		private float mposs5;

		// Token: 0x04001292 RID: 4754
		private float mposs6;

		// Token: 0x04001293 RID: 4755
		public float flowerScale = 1f;

		// Token: 0x04001294 RID: 4756
		private int ww = 640;

		// Token: 0x04001295 RID: 4757
		private int yy = -20;

		// Token: 0x04001296 RID: 4758
		private int bottom;

		// Token: 0x04001297 RID: 4759
		private int left;

		// Token: 0x04001298 RID: 4760
		public Matrix View;

		// Token: 0x04001299 RID: 4761
		public Matrix Projection;

		// Token: 0x0400129A RID: 4762
		public Vector3 LightDirection;

		// Token: 0x0400129B RID: 4763
		public float needle;

		// Token: 0x0400129C RID: 4764
		public float landerNeedle;

		// Token: 0x0400129D RID: 4765
		public float flowerNeedle;

		// Token: 0x0400129E RID: 4766
		public float bouy1Needle;

		// Token: 0x0400129F RID: 4767
		public float bouy2Needle;

		// Token: 0x040012A0 RID: 4768
		public float bouy3Needle;

		// Token: 0x040012A1 RID: 4769
		public float bouymanNeedle;

		// Token: 0x040012A2 RID: 4770
		public float facilityNeedle;

		// Token: 0x040012A3 RID: 4771
		public float farmNeedle;

		// Token: 0x040012A4 RID: 4772
		public static StringBuilder garbageBuild = new StringBuilder(32, 32);

		// Token: 0x040012A5 RID: 4773
		public string garbageString = "";

		// Token: 0x040012A6 RID: 4774
		public bool damaged1;

		// Token: 0x040012A7 RID: 4775
		private Rectangle hud1 = new Rectangle(0, 1116, 1280, 720);

		// Token: 0x040012A8 RID: 4776
		private Rectangle crackhud1 = new Rectangle(1280, 1116, 1280, 720);

		// Token: 0x040012A9 RID: 4777
		private Color offColor = new Color(0.4f, 0.4f, 0.4f);

		// Token: 0x040012AA RID: 4778
		private Color onColor = new Color(0.3f, 0.5f, 0.33f);

		// Token: 0x040012AB RID: 4779
		private Rectangle LanderButtons = new Rectangle(0, 0, 550, 40);

		// Token: 0x040012AC RID: 4780
		private Rectangle RoverButtons = new Rectangle(0, 40, 550, 40);

		// Token: 0x040012AD RID: 4781
		private Rectangle[] LB_on;

		// Token: 0x040012AE RID: 4782
		private Rectangle[] LB_col;

		// Token: 0x040012AF RID: 4783
		private Rectangle[] RB_on;

		// Token: 0x040012B0 RID: 4784
		private Rectangle[] RB_col;

		// Token: 0x040012B1 RID: 4785
		public float fuelTick;

		// Token: 0x040012B2 RID: 4786
		public float fuelDec = 1f;

		// Token: 0x040012B3 RID: 4787
		public float cellTick;

		// Token: 0x040012B4 RID: 4788
		public float cellDec = 1f;

		// Token: 0x040012B5 RID: 4789
		public int fuelAMT = 99;

		// Token: 0x040012B6 RID: 4790
		public int cellAMT = 70;

		// Token: 0x040012B7 RID: 4791
		private Color[] cellColor = new Color[]
		{
			Color.Red,
			Color.OrangeRed,
			Color.Orange,
			Color.Yellow,
			Color.YellowGreen,
			new Color(0, 240, 0, 255),
			Color.Green,
			Color.Green
		};

		// Token: 0x040012B8 RID: 4792
		private Color[] cellColor2 = new Color[]
		{
			Color.Red,
			Color.OrangeRed,
			Color.Orange,
			Color.Yellow,
			Color.Green,
			new Color(0, 240, 0, 255),
			Color.Green,
			Color.Green
		};

		// Token: 0x040012B9 RID: 4793
		private float cellScale = 1.6f;

		// Token: 0x040012BA RID: 4794
		private Vector2 batpos = new Vector2(1221f, 290f);

		// Token: 0x040012BB RID: 4795
		private Rectangle battery = new Rectangle(18, 265, 49, 327);

		// Token: 0x040012BC RID: 4796
		private Vector2 batterybarOffset = new Vector2(5f, 81f);

		// Token: 0x040012BD RID: 4797
		private Rectangle batteryBar = new Rectangle(260, 346, 33, 162);

		// Token: 0x040012BE RID: 4798
		private float fuelScale = 1.8f;

		// Token: 0x040012BF RID: 4799
		private Vector2 fuelpos = new Vector2(1192f, 290f);

		// Token: 0x040012C0 RID: 4800
		private Rectangle fuel = new Rectangle(87, 265, 78, 308);

		// Token: 0x040012C1 RID: 4801
		private Vector2 fuelbarOffset = new Vector2(7f, 35f);

		// Token: 0x040012C2 RID: 4802
		private Rectangle fuelBar = new Rectangle(179, 300, 51, 223);

		// Token: 0x040012C3 RID: 4803
		public bool bigfacilitymarker;

		// Token: 0x040012C4 RID: 4804
		private Texture2D compassBorder;

		// Token: 0x040012C5 RID: 4805
		private Texture2D compass;

		// Token: 0x040012C6 RID: 4806
		private Texture2D landerChip;

		// Token: 0x040012C7 RID: 4807
		private Texture2D bouy1texture;

		// Token: 0x040012C8 RID: 4808
		private Texture2D bouy2texture;

		// Token: 0x040012C9 RID: 4809
		private Texture2D farmtexture;

		// Token: 0x040012CA RID: 4810
		private Texture2D bouy3texture;

		// Token: 0x040012CB RID: 4811
		private Texture2D manbouytexture;

		// Token: 0x040012CC RID: 4812
		private Texture2D facilityTexture;

		// Token: 0x040012CD RID: 4813
		private Texture2D facilityTexture2;

		// Token: 0x040012CE RID: 4814
		private Texture2D stained;

		// Token: 0x040012CF RID: 4815
		private Texture2D dot;

		// Token: 0x040012D0 RID: 4816
		private Texture2D flowertexture;

		// Token: 0x040012D1 RID: 4817
		public Vector3 bouy1;

		// Token: 0x040012D2 RID: 4818
		public Vector3 bouy2;

		// Token: 0x040012D3 RID: 4819
		public Vector3 bouy3;

		// Token: 0x040012D4 RID: 4820
		public Vector3 facility;

		// Token: 0x040012D5 RID: 4821
		public Vector3 farm;

		// Token: 0x040012D6 RID: 4822
		public Vector3 flower;

		// Token: 0x040012D7 RID: 4823
		public int nextBouy = 1;

		// Token: 0x040012D8 RID: 4824
		public int roverbutton1;

		// Token: 0x040012D9 RID: 4825
		public int roverbutton2;

		// Token: 0x040012DA RID: 4826
		public int roverbutton3;

		// Token: 0x040012DB RID: 4827
		public int roverbutton4;

		// Token: 0x040012DC RID: 4828
		public bool scopeMode;

		// Token: 0x040012DD RID: 4829
		public int landerbutton1;

		// Token: 0x040012DE RID: 4830
		public int landerbutton2;

		// Token: 0x040012DF RID: 4831
		public int landerbutton3;

		// Token: 0x040012E0 RID: 4832
		public int landerbutton4;

		// Token: 0x040012E1 RID: 4833
		public float gems;

		// Token: 0x040012E2 RID: 4834
		public int buttonindex = 1;

		// Token: 0x040012E3 RID: 4835
		public int moonbuttonindex = 1;

		// Token: 0x040012E4 RID: 4836
		private Vector3 projectedPosition;

		// Token: 0x040012E5 RID: 4837
		private SpriteBatch spriteBatch;

		// Token: 0x040012E6 RID: 4838
		private SpriteFont font;

		// Token: 0x040012E7 RID: 4839
		private float occlusionAlpha;

		// Token: 0x040012E8 RID: 4840
		private ScreenManager sc;

		// Token: 0x040012E9 RID: 4841
		private ContentManager content;

		// Token: 0x040012EA RID: 4842
		private Viewport viewport;

		// Token: 0x040012EB RID: 4843
		private Vector2 screenCenter;

		// Token: 0x040012EC RID: 4844
		private Rectangle rect;

		// Token: 0x040012ED RID: 4845
		private float scaler;

		// Token: 0x040012EE RID: 4846
		private float[] choose = new float[] { 1f, 1f, 1f, 0.5f, 0.33f, 1f, 1f, 1f, 0.1f };

		// Token: 0x040012EF RID: 4847
		private Overlay.Flare[] flares = new Overlay.Flare[]
		{
			new Overlay.Flare(0.1f, 0.7f, new Color(50, 25, 50), "astro\\sprites\\lens\\flare1"),
			new Overlay.Flare(0.4f, 0.4f, new Color(100, 255, 200), "astro\\sprites\\lens\\flare1"),
			new Overlay.Flare(1.2f, 1f, new Color(50, 50, 50), "astro\\sprites\\lens\\flare1"),
			new Overlay.Flare(0.3f, 1f, new Color(130, 50, 100), "astro\\sprites\\lens\\flare2"),
			new Overlay.Flare(0.7f, 1.4f, new Color(100, 100, 100), "astro\\sprites\\lens\\flare2"),
			new Overlay.Flare(0.2f, 0.7f, new Color(50, 45, 25), "astro\\sprites\\lens\\flare3"),
			new Overlay.Flare(0f, 1f, new Color(55, 25, 25), "astro\\sprites\\lens\\flare3"),
			new Overlay.Flare(2f, 1.4f, new Color(99, 99, 110), "astro\\sprites\\lens\\flare3")
		};

		// Token: 0x040012F0 RID: 4848
		private float startwidth2;

		// Token: 0x040012F1 RID: 4849
		private float starthite2;

		// Token: 0x040012F2 RID: 4850
		private float startaspect2;

		// Token: 0x040012F3 RID: 4851
		private float stretch2;

		// Token: 0x040012F4 RID: 4852
		private Rectangle src;

		// Token: 0x040012F5 RID: 4853
		private Rectangle destn;

		// Token: 0x040012F6 RID: 4854
		private float amt;

		// Token: 0x040012F7 RID: 4855
		private Vector2 ctr;

		// Token: 0x02000087 RID: 135
		private class Flare
		{
			// Token: 0x060004B0 RID: 1200 RVA: 0x00115F02 File Offset: 0x00114102
			public Flare(float position, float scale, Color color, string textureName)
			{
				this.Position = position;
				this.Scale = scale;
				this.Color = color;
				this.TextureName = textureName;
			}

			// Token: 0x040012F8 RID: 4856
			public float Position;

			// Token: 0x040012F9 RID: 4857
			public float Scale;

			// Token: 0x040012FA RID: 4858
			public Color Color;

			// Token: 0x040012FB RID: 4859
			public string TextureName;

			// Token: 0x040012FC RID: 4860
			public Texture2D Texture;
		}
	}
}
