using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200005B RID: 91
	internal class skidsSystem : ParticleSystem
	{
		// Token: 0x0600036C RID: 876 RVA: 0x000D56B8 File Offset: 0x000D38B8
		public skidsSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000D56C4 File Offset: 0x000D38C4
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "skid";
			settings.MaxParticles = 9000;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 0f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, 1f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(255, 255, 255, 190);
			settings.MaxColor = new Color(255, 255, 255, 190);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 12f;
			settings.MaxStartSize = 12f;
			settings.MinEndSize = 6f;
			settings.MaxEndSize = 6f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
