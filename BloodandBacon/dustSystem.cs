using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000DF RID: 223
	internal class dustSystem : ParticleSystem
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x001CAB45 File Offset: 0x001C8D45
		public dustSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x001CAB50 File Offset: 0x001C8D50
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "tracer3";
			settings.MaxParticles = 25000;
			settings.Duration = TimeSpan.FromSeconds(1.0);
			settings.DurationRandomness = 0.1f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -150f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(255, 34, 19, 200);
			settings.MaxColor = new Color(255, 255, 230, 255);
			settings.MinRotateSpeed = 35f;
			settings.MaxRotateSpeed = 125f;
			settings.MinStartSize = 16f;
			settings.MaxStartSize = 18f;
			settings.MinEndSize = 2f;
			settings.MaxEndSize = 2f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
