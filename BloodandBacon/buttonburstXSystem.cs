using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000109 RID: 265
	internal class buttonburstXSystem : ParticleSystem
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x00250A4C File Offset: 0x0024EC4C
		public buttonburstXSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00250A58 File Offset: 0x0024EC58
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "bluediamond";
			settings.MaxParticles = 2500;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 1f;
			settings.MaxHorizontalVelocity = 11f;
			settings.MinVerticalVelocity = 1f;
			settings.MaxVerticalVelocity = 11f;
			settings.Gravity = new Vector3(0f, -25f, 0f);
			settings.EndVelocity = 1f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 0.3f;
			settings.MaxRotateSpeed = 0.8f;
			settings.MinStartSize = 14f;
			settings.MaxStartSize = 14f;
			settings.MinEndSize = 79f;
			settings.MaxEndSize = 85f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
