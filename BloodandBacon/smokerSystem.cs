using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000083 RID: 131
	internal class smokerSystem : ParticleSystem
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x001119FA File Offset: 0x0010FBFA
		public smokerSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00111A04 File Offset: 0x0010FC04
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "tracer2";
			settings.MaxParticles = 6200;
			settings.Duration = TimeSpan.FromSeconds(3.200000047683716);
			settings.DurationRandomness = 2f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 3f;
			settings.MaxHorizontalVelocity = 15f;
			settings.MinVerticalVelocity = 3f;
			settings.MaxVerticalVelocity = 15f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 3f;
			settings.MinColor = new Color(85, 85, 85, 85);
			settings.MaxColor = new Color(238, 238, 238, 238);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 2f;
			settings.MaxStartSize = 22f;
			settings.MinEndSize = 156f;
			settings.MaxEndSize = 250f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
