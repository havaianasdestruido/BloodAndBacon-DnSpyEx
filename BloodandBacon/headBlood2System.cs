using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000016 RID: 22
	internal class headBlood2System : ParticleSystem
	{
		// Token: 0x0600011C RID: 284 RVA: 0x0002BF28 File Offset: 0x0002A128
		public headBlood2System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0002BF34 File Offset: 0x0002A134
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "heart";
			settings.MaxParticles = 0;
			settings.Duration = TimeSpan.FromSeconds(2.0);
			settings.DurationRandomness = 0.3f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -150f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(80, 80, 80, 45);
			settings.MaxColor = new Color(245, 245, 245, 130);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 0.5f;
			settings.MaxStartSize = 5f;
			settings.MinEndSize = 0.3f;
			settings.MaxEndSize = 0.3f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
