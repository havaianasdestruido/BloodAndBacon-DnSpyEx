using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200006E RID: 110
	internal class highvelocitySystem : ParticleSystem
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x000E38EB File Offset: 0x000E1AEB
		public highvelocitySystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000E38F8 File Offset: 0x000E1AF8
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "tracer2";
			settings.MaxParticles = 5000;
			settings.Duration = TimeSpan.FromSeconds(1.600000023841858);
			settings.DurationRandomness = 1f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 1f;
			settings.MaxHorizontalVelocity = 26f;
			settings.MinVerticalVelocity = 1f;
			settings.MaxVerticalVelocity = 26f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(85, 85, 85, 85);
			settings.MaxColor = new Color(238, 238, 238, 238);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 4f;
			settings.MaxStartSize = 15f;
			settings.MinEndSize = 22f;
			settings.MaxEndSize = 25f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
