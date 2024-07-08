using System;
using System.Collections.Generic;

namespace Arosaje_KSENV.Models;

public partial class Utilisateur
{
    public int IdUtilisateur { get; set; }

    public string? Nom { get; set; }

    public string? Mdp { get; set; }

    public string? Email { get; set; }

    public string? Prenom { get; set; }

    public int? Age { get; set; }

    public virtual Botaniste? Botaniste { get; set; }

    public virtual Membre? Membre { get; set; }

    public virtual Proprio? Proprio { get; set; }

    public virtual ICollection<Ville> IdVilles { get; set; } = new List<Ville>();
}
