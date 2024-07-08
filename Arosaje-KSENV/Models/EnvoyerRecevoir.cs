using System;
using System.Collections.Generic;

namespace Arosaje_KSENV.Models;

public partial class EnvoyerRecevoir
{
    public int IdUtilisateur { get; set; }

    public int IdUtilisateur1 { get; set; }

    public int IdUtilisateur2 { get; set; }

    public int IdMessage { get; set; }

    public virtual Message IdMessageNavigation { get; set; } = null!;

    public virtual Botaniste IdUtilisateur1Navigation { get; set; } = null!;

    public virtual Proprio IdUtilisateur2Navigation { get; set; } = null!;

    public virtual Membre IdUtilisateurNavigation { get; set; } = null!;
}
