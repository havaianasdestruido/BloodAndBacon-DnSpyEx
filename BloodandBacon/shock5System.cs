using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200004C RID: 76
	internal class shock5System : ParticleSystem
	{
		// Token: 0x06000319 RID: 793 RVA: 0x000CEB6E File Offset: 0x000CCD6E
		public shock5System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000CEB78 File Offset: 0x000CCD78
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "spire5";
			settings.MaxParticles = 25000;
			settings.Duration = TimeSpan.FromSeconds(1.0);
			settings.DurationRandomness = 0.1f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, -20f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(100, 100, 205, 60);
			settings.MaxColor = new Color(255, 255, 255, 200);
			settings.MinRotateSpeed = 2f;
			settings.MaxRotateSpeed = 8f;
			settings.MinStartSize = 5f;
			settings.MaxStartSize = 59f;
			settings.MinEndSize = 2f;
			settings.MaxEndSize = 6f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
