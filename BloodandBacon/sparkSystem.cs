using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200005E RID: 94
	internal class sparkSystem : ParticleSystem
	{
		// Token: 0x06000372 RID: 882 RVA: 0x000D5A55 File Offset: 0x000D3C55
		public sparkSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000D5A60 File Offset: 0x000D3C60
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "sparke";
			settings.MaxParticles = 12000;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 0.2f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, -256f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(245, 108, 108, 200);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 5f;
			settings.MaxRotateSpeed = 15f;
			settings.MinStartSize = 2.5f;
			settings.MaxStartSize = 6.2f;
			settings.MinEndSize = 2f;
			settings.MaxEndSize = 3f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
