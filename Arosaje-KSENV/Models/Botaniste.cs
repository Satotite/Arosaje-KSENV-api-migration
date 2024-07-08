using System;
using System.Collections.Generic;

namespace Arosaje_KSENV.Models;

public partial class Botaniste
{
    public int IdUtilisateur { get; set; }

    public virtual ICollection<EnvoyerRecevoir> EnvoyerRecevoirs { get; set; } = new List<EnvoyerRecevoir>();

    public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;

    public virtual ICollection<DateTip> IdTips { get; set; } = new List<DateTip>();
}
