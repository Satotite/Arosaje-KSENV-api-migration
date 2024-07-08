using System;
using System.Collections.Generic;

namespace Arosaje_KSENV.Models;

public partial class Ville
{
    public int IdVille { get; set; }

    public string? Nom { get; set; }

    public string? Desc { get; set; }

    public virtual ICollection<Plante> Plantes { get; set; } = new List<Plante>();

    public virtual ICollection<Utilisateur> IdUtilisateurs { get; set; } = new List<Utilisateur>();
}
