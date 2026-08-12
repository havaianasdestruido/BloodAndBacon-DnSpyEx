using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000184 RID: 388
	internal class starburstSystem : ParticleSystem
	{
		// Token: 0x06000E55 RID: 3669 RVA: 0x00403AC5 File Offset: 0x00401CC5
		public starburstSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00403AD0 File Offset: 0x00401CD0
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "star";
			settings.MaxParticles = 6000;
			settings.Duration = TimeSpan.FromSeconds(8.0);
			settings.DurationRandomness = 1f;
			settings.EmitterVelocitySensitivity = 3f;
			settings.MinHorizontalVelocity = 5f;
			settings.MaxHorizontalVelocity = 5f;
			settings.MinVerticalVelocity = 5f;
			settings.MaxVerticalVelocity = 5f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 5f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 0.3f;
			settings.MaxRotateSpeed = 0.8f;
			settings.MinStartSize = 25f;
			settings.MaxStartSize = 25f;
			settings.MinEndSize = 39f;
			settings.MaxEndSize = 45f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
