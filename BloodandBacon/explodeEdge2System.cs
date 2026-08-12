using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000071 RID: 113
	internal class explodeEdge2System : ParticleSystem
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x000E3C7A File Offset: 0x000E1E7A
		public explodeEdge2System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000E3C84 File Offset: 0x000E1E84
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "star";
			settings.MaxParticles = 8000;
			settings.Duration = TimeSpan.FromSeconds(6.0);
			settings.DurationRandomness = 2f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -20f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(255, 68, 68, 68);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = -6f;
			settings.MaxRotateSpeed = 6f;
			settings.MinStartSize = 13f;
			settings.MaxStartSize = 16f;
			settings.MinEndSize = 1f;
			settings.MaxEndSize = 4f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
