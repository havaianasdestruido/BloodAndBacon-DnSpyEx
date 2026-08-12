using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200004B RID: 75
	internal class electricSystem : ParticleSystem
	{
		// Token: 0x06000317 RID: 791 RVA: 0x000CEA3E File Offset: 0x000CCC3E
		public electricSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000CEA48 File Offset: 0x000CCC48
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "electric";
			settings.MaxParticles = 5000;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 0.2f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, -250f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(145, 108, 208, 200);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 6f;
			settings.MaxRotateSpeed = 12f;
			settings.MinStartSize = 1f;
			settings.MaxStartSize = 7f;
			settings.MinEndSize = 1f;
			settings.MaxEndSize = 1f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
