using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200004E RID: 78
	internal class sparksSystem : ParticleSystem
	{
		// Token: 0x06000320 RID: 800 RVA: 0x000CEDCA File Offset: 0x000CCFCA
		public sparksSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000CEDD4 File Offset: 0x000CCFD4
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "spark";
			settings.MaxParticles = 75000;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 1f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -50f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(119, 119, 119, 119);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = -8f;
			settings.MaxRotateSpeed = 8f;
			settings.MinStartSize = 9f;
			settings.MaxStartSize = 11f;
			settings.MinEndSize = 0f;
			settings.MaxEndSize = 1f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
