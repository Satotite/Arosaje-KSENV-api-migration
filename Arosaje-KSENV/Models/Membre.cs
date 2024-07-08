using System;
using System.Collections.Generic;

namespace Arosaje_KSENV.Models;

public partial class Membre
{
    public int IdUtilisateur { get; set; }

    public virtual ICollection<EnvoyerRecevoir> EnvoyerRecevoirs { get; set; } = new List<EnvoyerRecevoir>();

    public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;

    public virtual ICollection<Plante> PlanteIdUtilisateur1Navigations { get; set; } = new List<Plante>();

    public virtual ICollection<Plante> PlanteIdUtilisateurNavigations { get; set; } = new List<Plante>();

    public virtual ICollection<Photo> IdPhotos { get; set; } = new List<Photo>();
}
