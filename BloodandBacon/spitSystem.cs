using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000AC RID: 172
	internal class spitSystem : ParticleSystem
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x00147A1D File Offset: 0x00145C1D
		public spitSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00147A28 File Offset: 0x00145C28
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "tracer2";
			settings.MaxParticles = 5000;
			settings.Duration = TimeSpan.FromSeconds(1.0);
			settings.DurationRandomness = 0.1f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -90f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(255, 34, 19, 200);
			settings.MaxColor = new Color(255, 255, 230, 255);
			settings.MinRotateSpeed = 35f;
			settings.MaxRotateSpeed = 125f;
			settings.MinStartSize = 6f;
			settings.MaxStartSize = 4f;
			settings.MinEndSize = 3f;
			settings.MaxEndSize = 3f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
