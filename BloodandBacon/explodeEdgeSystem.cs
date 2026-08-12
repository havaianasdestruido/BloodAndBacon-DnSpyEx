using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000B5 RID: 181
	internal class explodeEdgeSystem : ParticleSystem
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x00148D49 File Offset: 0x00146F49
		public explodeEdgeSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00148D54 File Offset: 0x00146F54
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
			settings.MaxRotateSpeed = 7f;
			settings.MinStartSize = 11f;
			settings.MaxStartSize = 14f;
			settings.MinEndSize = 24f;
			settings.MaxEndSize = 32f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
